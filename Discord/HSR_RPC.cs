using DiscordRPC;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace HSRRichPresence.Discord
{
    class HSR_RPC
    {
        private static readonly string game = "launcher";
        private static readonly string token = "1098657418218586174";
        private static bool Initialized = false;

        private static readonly DiscordRpcClient client = new(token);
        private static readonly RichPresence presence = new()
        {
            Assets = new Assets()
            {
                LargeImageText = "Honkai: Star Rail",
                LargeImageKey = "starrail",
                SmallImageText = "Honkai: Star Rail",
                SmallImageKey = "starrail",
            }
        };

        private async static void RPCStart()
        {
            string uuid = HSR_Reg.UID();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://mmmmm.me/sr_info/");//$"https://api.mihomo.me/sr_info/{uuid}?lang=en");
            string content = await response.Content.ReadAsStringAsync();
            if (content == "")
            {
                return;
            }
            //Console.WriteLine(content);
            var account = JObject.Parse(content);
            
            Console.WriteLine(account["detailInfo"]["nickname"]);
            presence.Timestamps = Timestamps.Now;
            presence.Details = $"IGN: {account["detailInfo"]["nickname"]} | Lv: {account["detailInfo"]["level"]} | Achv: {account["detailInfo"]["recordInfo"]["achievementCount"]}";
            presence.State = $"Signature: {account["detailInfo"]["signature"]}";
            /*presence.Buttons = new Button[] {
                new Button() { Label = "View Profile", Url = config.Profile },
                new Button() { Label = "HoYoLab Profile", Url = config.Hoyolab },
            };*/

            if (!Initialized)
            {
                client.Initialize();
                Initialized = true;
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

        public static void Start()
        {
            var isRunning = false;

            while (true)
            {
                Process[] starRail = Process.GetProcessesByName(game);

                if (starRail.Length > 0 && !isRunning)
                {
                    RPCStart();
                    isRunning = true;
                }
                else if (starRail.Length == 0 && isRunning)
                {
                    Cancel();
                    isRunning = false;
                }
                Thread.Sleep(5000);
            }
        }
    }
}
