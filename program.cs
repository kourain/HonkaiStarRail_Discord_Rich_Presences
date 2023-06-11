using System.Diagnostics;
namespace HSR_Discord_RPC
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string hoyolabid = "";
            if (File.Exists("cfg.txt"))
                hoyolabid = File.ReadAllText("cfg.txt");
            Process[] old = Process.GetProcessesByName("HSR_Discord_RPC");
            foreach(var temp in old)
            {
                if(temp.Id != Process.GetCurrentProcess().Id)
                {
                    temp.Kill();
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main mainform = new main(ref hoyolabid);
            Application.Run();
        }
    }
}