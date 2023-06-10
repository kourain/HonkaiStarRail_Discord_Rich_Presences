
namespace HSR_Discord_RPC
{
    public partial class main : Form
    {
        protected Thread RPC_Thread = new Thread(RPC_RUN);

        public main()
        {
            InitializeComponent();
            RPC_Thread.Start();

            //main form, not show now
            this.BackgroundImage = Image.FromFile("assets//BG.jpg");
            this.Icon = new Icon("assets//NotifyAreaIcon.ico");

            // notifyIcon Icon
            notifyIcon.Icon = new Icon("assets\\NotifyAreaIcon.ico");
            //Shows the Text whenever Mouse is Hover on the
            notifyIcon.Text = "HSR_Discord_RPC";
            //contextmenustrip
            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("1.1", null, GoToGitClick);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitClick);
        }
        static void RPC_RUN()
        {
            HSR_RPC.Start();
        }
        static void GoToGitClick(object sender, EventArgs e)
        {
            var uri = "https://github.com/kourain/HonkaiStarRail_Discord_RPC";
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = uri
            };
            System.Diagnostics.Process.Start(psi);
        }
        static void ExitClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            return;
        }
    }
}
