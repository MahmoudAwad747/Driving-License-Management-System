using DVLD_Buisness;
using Microsoft.Win32;
using System;



namespace DVLD.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            // Store the Username and Password in the Registry (HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginInfo)
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginInfo";

            string valueName = "LoginCurrentUser";
            string valueData = Username + "#//#" + Password;

            try
            {
                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginInfo";

            string valueName = "LoginCurrentUser";

            try
            {
                string value = Registry.GetValue(keyPath, valueName, null) as string;

                if (value != null)
                {
                    string[] valueData = value.Split(new string[] { "#//#" }, StringSplitOptions.None);

                    if (valueData.Length == 2)
                    {
                        Username = valueData[0];
                        Password = valueData[1];
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"an error occared {ex.Message}");
            }

            return false;

        }
    }
}
