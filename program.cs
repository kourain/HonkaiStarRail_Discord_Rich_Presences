using HSRRichPresence.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSR_Discord_RPC
{
    class Program
    {
        static void Main()
        {

            NotifyIcon notifyIcon = new NotifyIcon();
            // Application Icon
            notifyIcon.Icon = new Icon("assets\\NotifyAreaIcon.ico");
            //Shows the Text whenever Mouse is Hover on the
            notifyIcon.Text = "HSR_Discord_RPC";
            //Set the Notification visibility to true
            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Test1", null, GoToGitClick);
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitClick);
            notifyIcon.Visible = true;
            HSR_RPC.Start();
        }
        static void GoToGitClick(object sender, EventArgs e)
        {
            var uri = "https://stackoverflow.com/";
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
    }
}