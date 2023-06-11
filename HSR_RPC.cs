using DiscordRPC;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
namespace HSR_Discord_RPC
{
    class HSR_RPC
    {
        //private const string game = "launcher";
        private const string game = "StarRail";
        private const string token = "1117169565508567150";
        private static string hoyolabid = "";
        private static readonly DiscordRpcClient client = new(token);
        private static readonly RichPresence presence = new()
        {
            Assets = new Assets()
            {
                LargeImageText = "Honkai: Star Rail",
                LargeImageKey = "logohsr",
            }
        };
        private async static void RPCStart()
        {
            string uuid = HSR_Reg.UID();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.mihomo.me/sr_info/{uuid}?lang=en");
            string content = await response.Content.ReadAsStringAsync();
            if (content == "")
            {
                return;
            }
            try
            {
                var account = JObject.Parse(content);
                presence.Timestamps = Timestamps.Now;
                presence.Details = $"IGN: {account["detailInfo"]["nickname"]}";
                presence.State = $"UID: {uuid} | Lv: {account["detailInfo"]["level"]}";
                if (hoyolabid.ToLower() != "hide")
                {
                    presence.Buttons = new DiscordRPC.Button[] {
                        new DiscordRPC.Button() { Label = "HoYoLab Profile", Url = $"https://www.hoyolab.com/accountCenter/postList?id={hoyolabid}"},
                        new DiscordRPC.Button() { Label = "Battle Chronicle", Url = $"https://act.hoyolab.com/app/community-game-records-sea/index.html?bbs_presentation_style=fullscreen&bbs_auth_required=true&gid=2&user_id={hoyolabid}&utm_source=hoyolab&utm_medium=gamecard&bbs_theme=dark&bbs_theme_device=1#/hsr" },
                    };
                }
            }
            catch
            {
                return;
            }
            client.SetPresence(presence);
        }

        private static void Cancel()
        {
            if (client != null && client.IsInitialized)
            {
                presence.Timestamps = null;
                client.ClearPresence();
            }
        }

        public static void Start(object hoyolabidinp)
        {
            hoyolabid = (hoyolabidinp as string);
            var isRunning = false;
            client.Initialize();
                
            while (true)
            {
                Process[] ProcessList = Process.GetProcessesByName(game);

                if (ProcessList.Length > 0 && !isRunning)
                {
                    RPCStart();
                    isRunning = true;
                }
                else if (ProcessList.Length == 0 && isRunning)
                {
                    Cancel();
                    isRunning = false;
                }
                Thread.Sleep(5000);
            }
        }
    }
}
