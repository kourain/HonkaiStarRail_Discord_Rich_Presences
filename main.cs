namespace HSR_Discord_RPC
{
    public partial class main : Form
    {
        protected static Thread RPC_Thread = new Thread(RPC_RUN);
        protected static string HoyolabId = "";
        public main(ref string hoyolabid)
        {
            HoyolabId = hoyolabid;
            InitializeComponent();

            //main form, not show now
            this.BackgroundImage = Image.FromFile("assets//BG.jpg");
            this.Icon = new Icon("assets//NotifyAreaIcon.ico");

            // notifyIcon Icon
            notifyIcon.Icon = new Icon("assets\\NotifyAreaIcon.ico");

            //Shows the Text whenever Mouse is Hover on the
            notifyIcon.Text = "HSR_Discord_RPC";

            //hoyolabid list from regedit
            var hoyolabidlist = HSR_Reg.HoyolabId();
            if (HoyolabId == "" && hoyolabidlist.Count > 0)
            {
                HoyolabId = hoyolabidlist[0];
            }
            //start thread
            RPC_Thread.Start(HoyolabId);

            //contextmenustrip
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("1.2", null, GoToGitClick);
            notifyIcon.ContextMenuStrip.Items.Add("Hoyolab Profile", null, null);
            (notifyIcon.ContextMenuStrip.Items[1] as ToolStripMenuItem).DropDownItems.Add($"Hide Hoyolab Profile", null, HideHoyolabInfo);
            int count = 1;
            foreach (var item in hoyolabidlist)
            {
                (notifyIcon.ContextMenuStrip.Items[1] as ToolStripMenuItem).DropDownItems.Add($"{item}", null, HoyolabIdClick);
                if (item == HoyolabId)
                {
                    ((notifyIcon.ContextMenuStrip.Items[1] as ToolStripMenuItem).DropDownItems[count] as ToolStripMenuItem).Checked = true;
                }
                count++;
            }
            notifyIcon.ContextMenuStrip.Items.Add("Restart", null, Restart);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitClick);

        }
        static void RPC_RUN(object hoyolabid)
        {
            HSR_RPC.Start(hoyolabid);
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
        static void Restart(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Environment.Exit(0);
        }
        static void HoyolabIdClick(object sender, EventArgs e)
        {

            if (HoyolabId != (sender as ToolStripMenuItem).Text)
            {
                HoyolabId = (sender as ToolStripMenuItem).Text;
                //write cfg
                File.WriteAllText("cfg.txt", HoyolabId);
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
        }
        static void HideHoyolabInfo(object sender, EventArgs e)
        {
            HoyolabId = (sender as ToolStripMenuItem).Text;
            //write cfg
            File.WriteAllText("cfg.txt", "hide");
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Environment.Exit(0);
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
