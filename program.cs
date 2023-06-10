using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HSR_Discord_RPC
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            Process[] old = Process.GetProcessesByName("HSR_Discord_RPC");
            foreach(var temp  in old)
            {
                if(temp.Id != Process.GetCurrentProcess().Id)
                {
                    temp.Kill();
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main mainform = new main();
            Application.Run();
        }
    }
}