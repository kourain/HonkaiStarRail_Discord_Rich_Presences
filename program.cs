namespace HSR_Discord_RPC
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            main mainform = new main();
            Application.Run();
        }
    }
}