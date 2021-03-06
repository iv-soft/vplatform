using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IVySoft.VPlatform.Network
{
    public class WebSocketPipeline : IDisposable
    {
        private WebSocket websocket_;
        private CancellationToken cancel_token_;
        private SingleThreadApartment send_thread_ = new SingleThreadApartment();
        private SingleThreadApartment receive_thread_ = new SingleThreadApartment();
        private CancellationTokenSource shutdown_ = new CancellationTokenSource();
        private Thread work_thread_;
        private Func<string, Task> input_handler_;
        private Func<byte[], Task> binary_handler_;
        private Exception failed_ = null;
        private Action<Exception> error_handler_;

        public void start(
            WebSocket websocket,
            CancellationToken cancel_token,
            Func<string, Task> input_handler,
            Action<Exception> error_handler,
            Func<byte[], Task> binary_handler = null)
        {
            this.websocket_ = websocket;
            this.cancel_token_ = cancel_token;
            this.cancel_token_.Register(() => this.shutdown_.Cancel());
            this.input_handler_ = input_handler;
            this.binary_handler_ = binary_handler;
            this.error_handler_ = error_handler;

            this.work_thread_ = new Thread(receive_thread);
            this.work_thread_.Start();
        }

        public void stop()
        {
            this.error_handler_ = null;
            this.shutdown_.Cancel();
            this.send_thread_.join();
            this.receive_thread_.join();

            if (this.work_thread_ != null)
            {
                this.work_thread_.Join();
            }
        }
        public void enqueue(System.Threading.CancellationToken token, byte[] message)
        {
            if (null != this.failed_)
            {
                throw this.failed_;
            }

            try
            {
                this.send_thread_.invoke(() => send_thread(token, message));
            }
            catch (Exception exception)
            {
                this.set_error(exception);
                throw;
            }
        }

        private async Task send_thread(System.Threading.CancellationToken token, byte[] message)
        {
            await this.websocket_.SendAsync(new System.ArraySegment<byte>(message), WebSocketMessageType.Text, true, token);
        }

        private void receive_thread()
        {
            try
            {
                var buffer = new System.IO.MemoryStream();
                for(; ; )
                {
                    var rcvBytes = new byte[1024];
                    var rcvBuffer = new ArraySegment<byte>(rcvBytes);

                    var rcvResult = this.websocket_.ReceiveAsync(
                        rcvBuffer,
                        this.shutdown_.Token);

                    rcvResult.Wait();

                    buffer.Write(rcvBytes, rcvBuffer.Offset, rcvResult.Result.Count);

                    if (!rcvResult.Result.EndOfMessage)
                    {
                        continue;
                    }

                    switch (rcvResult.Result.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            {
                                var body = Encoding.UTF8.GetString(buffer.ToArray());
                                buffer.SetLength(0);
                                this.receive_thread_.invoke(() => this.input_handler_(body));
                                break;
                            }
                        case WebSocketMessageType.Binary:
                            {
                                if (null == this.binary_handler_)
                                {
                                    throw new InvalidOperationException();
                                }

                                var body = buffer.ToArray();
                                buffer.SetLength(0);
                                this.receive_thread_.invoke(() => this.binary_handler_(body));

                                break;
                            }
                        case WebSocketMessageType.Close:
                            return;

                        default:
                            throw new InvalidOperationException();
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                this.set_error(ex);
            }
        }

        private void set_error(Exception exception)
        {
            this.failed_ = exception;
            if (null != this.error_handler_)
            {
                this.error_handler_(exception);
            }
        }

        public void Dispose()
        {
            if (null != this.websocket_)
            {
                if (this.websocket_.State == WebSocketState.Open || this.websocket_.State == WebSocketState.CloseReceived)
                {
                    using (var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30)))
                    {
                        this.websocket_.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, timeout.Token).Wait();
                    }
                }
                this.stop();
                this.websocket_.Dispose();
            }
        }
    }
}
