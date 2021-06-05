using System;
using System.Diagnostics;
using System.Text;

namespace IVySoft.VPlatform.Process.Cmd
{
    public class ConsoleCommand
    {
        public static int Execute(ProcessStartInfo processStart)
        {
            processStart.UseShellExecute = false;
            processStart.RedirectStandardOutput = true;
            processStart.CreateNoWindow = true;

            var sb = new StringBuilder();
            var proc = System.Diagnostics.Process.Start(processStart);
            ChildProcessTracker.AddProcess(proc);
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                sb.Append(line);
            }

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
