using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eos.SCM.Helpers
{

  internal sealed class CmdLineHelper
  {

    bool abort = false;
    bool running = false;
    readonly List<string> output = new List<string>();
    readonly List<string> error = new List<string>();
    Process proc = null;


    public int LastExitCode { get; private set; }

    public string WorkingDir { get; set; }

    public TimeSpan TimeoutPeriod { get; set; }

    public string CredentialsString { get; set; }


    public CmdLineHelper(TimeSpan timeout, string workingDir = "")
    {
      WorkingDir = workingDir;
      TimeoutPeriod = timeout;
    }

    public CmdLineHelper(string workingDir = "") : this(TimeSpan.FromMinutes(2), workingDir)
    {
    }


    private void Abort(string message)
    {
      abort = true;
      lock (error)
        error.Add(message);
    }

    private void StartWatchdog()
    {
      Task.Run(() =>
      {
        while (running)
        {
          Thread.Sleep(150);
          if (abort)
          {
            proc.Kill();
            running = false;
          }
        }
      });
    }

    public string[] Call(string command, string args)
    {
      if (running)
        throw new InvalidOperationException();

      output.Clear();
      error.Clear();
      abort = false;
      running = false;

      if (!String.IsNullOrEmpty(CredentialsString))
        args = $"{CredentialsString} {args}";

      proc = new Process
      {
        StartInfo =
        {
          StandardOutputEncoding = Encoding.Default,
          StandardErrorEncoding = Encoding.Default,
          FileName = command,
          Arguments = args
        }
      };
      if (!String.IsNullOrEmpty(WorkingDir) && Directory.Exists(WorkingDir))
        proc.StartInfo.WorkingDirectory = WorkingDir;
      proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
      proc.StartInfo.RedirectStandardOutput = true;
      proc.StartInfo.RedirectStandardError = true;
      proc.StartInfo.RedirectStandardInput = true;
      proc.StartInfo.UseShellExecute = false;
      proc.StartInfo.CreateNoWindow = true;

      proc.OutputDataReceived += Proc_OutputDataReceived;
      proc.ErrorDataReceived += Proc_ErrorDataReceived;

      proc.Start();
      proc.BeginOutputReadLine();
      proc.BeginErrorReadLine();

      // makes manual aborting possible
      running = true;
      StartWatchdog();
      proc.WaitForExit();
      running = false;

      if (error.Count > 0)
        throw new Exception(String.Join(Environment.NewLine, error));

      LastExitCode = proc.ExitCode;

      return output.Take(output.Count - 1).ToArray();
    }

    private void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
      if (e.Data != null)
        error.Add(e.Data);
    }
    private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      string data = e.Data != null ? e.Data : "";
      if (data != null)
      {
        if (data.Contains("authorization required"))
        {
          StringBuilder b = new StringBuilder();
          b.AppendLine("hg authentication failed:");
          b.AppendLine(data);
          Abort(b.ToString());
        }
        output.Add(data);
      }
    }
  }
}
