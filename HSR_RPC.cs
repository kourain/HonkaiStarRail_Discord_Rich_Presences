using DiscordRPC;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace HSR_Discord_RPC
{
    class HSR_RPC
    {
        private static readonly string game = "launcher";
        private static readonly string token = "1117169565508567150";

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
            //Console.WriteLine(content);
            var account = JObject.Parse(content);

            Console.WriteLine(account["detailInfo"]["nickname"]);
            presence.Timestamps = Timestamps.Now;
            presence.Details = $"IGN: {account["detailInfo"]["nickname"]} | UID: {uuid} | Lv: {account["detailInfo"]["level"]}";
            presence.State = $"Signature: {account["detailInfo"]["signature"]}";
            /*presence.Buttons = new Button[] {
                new Button() { Label = "HoYoLab Profile", Url = $"https://www.hoyolab.com/accountCenter/postList?id={HSR_Reg.HoyolabId}" },
            };*/

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
            client.Initialize();
                
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
