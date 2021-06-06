using System;
using System.Diagnostics;
using System.Text;

namespace IVySoft.VPlatform.Process.Cmd
{
    public class ConsoleCommand
    {
        public static int Execute(ProcessStartInfo processStart, Action<string> cmdOutput)
        {
            processStart.UseShellExecute = false;
            processStart.RedirectStandardOutput = true;
            processStart.RedirectStandardError = true;
            processStart.CreateNoWindow = true;
            
            var proc = System.Diagnostics.Process.Start(processStart);
            ChildProcessTracker.AddProcess(proc);
            proc.OutputDataReceived += (sender, e) => {
                if (null != e.Data)
                {
                    cmdOutput(e.Data);
                }
            };
            proc.ErrorDataReceived += (sender, e) => {
                if (null != e.Data)
                {
                    cmdOutput(e.Data);
                }
            };
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
            return proc.ExitCode;
        }
        public static System.Diagnostics.Process Start(ProcessStartInfo processStart)
        {
            var proc = System.Diagnostics.Process.Start(processStart);
            ChildProcessTracker.AddProcess(proc);
            return proc;
        }
    }
}
