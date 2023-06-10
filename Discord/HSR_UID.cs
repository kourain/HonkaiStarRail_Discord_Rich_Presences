using Microsoft.Win32;

namespace HSRRichPresence.Discord
{
    class HSR_Reg
    {
        public static string UID()
        {
            string UID = "";
            while (UID == "")
                UID = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Cognosphere\Star Rail", "App_LastUserID_h2841727341", "").ToString();
            //Console.WriteLine(UID);
            return UID;
        }
        public static string HoyolabId()
        {
            string hoyolabid = "";
            return hoyolabid;
        }    
    }
}