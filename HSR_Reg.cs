using Microsoft.Win32;
namespace HSR_Discord_RPC
{
    class HSR_Reg
    {
        public static string UID()
        {
            string UID = "";
            while (UID == "")
            {     
                UID = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Cognosphere\Star Rail", "App_LastUserID_h2841727341", "").ToString();
            }
            return UID;
        }
        public static List<string> HoyolabId()
        {
            List<string> value = new List<string>();

            foreach (string Keyname in Registry.CurrentUser.OpenSubKey(@"Software\Cognosphere\Star Rail").GetValueNames())
            {
                if(Keyname.IndexOf("MIHOYOSDK_PROTOCOL_SHOW_FLAG_") == 0)
                {
                    string[] temp = Keyname.Substring(29).Split('_');
                    
                    if (temp[0] == "1")
                    {
                        value.Add(temp[1]);
                    }
                }       
            }
            return value;
        }
    }
}