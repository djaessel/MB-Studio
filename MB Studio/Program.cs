using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace MB_Studio
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            HandleArguments(args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MB_Studio());
        }

        // Put this in a handler class later - so no code is in Program
        private static void HandleArguments(string[] args)
        {
            if (args.Length == 0) return;

            if (args[0].Equals("-au"))
            {
                UpdateRegistryData();
            }
            else
            {
                Console.WriteLine("Unkown parameter: " + args[0]);
                Environment.Exit(1); // maybe change error code later
            }
        }

        // Put this in a handler class later - so no code is in Program
        private static void UpdateRegistryData()
        {
            string productName = Application.ProductName;
            productName = productName.Remove(productName.IndexOf('.'));

            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                string regSubKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\" + productName;
                using (RegistryKey key = baseKey.OpenSubKey(regSubKey, true))
                {
                    UpdateRegKey(key, "DisplayVerion", Application.ProductVersion);
                    UpdateRegKey(key, "DisplayName", productName);
                    UpdateRegKey(key, "Publisher", Application.CompanyName);

                    key.Flush();
                }
            }
        }

        // Put this in a handler class later - so no code is in Program
        private static bool UpdateRegKey(
            RegistryKey key,
            string valueName,
            object newValue,
            RegistryValueKind keyKind = RegistryValueKind.String
        )
        {
            bool needUpdate = false;
            object oldValue = key.GetValue(valueName);
            needUpdate = !oldValue.Equals(newValue);
            if (needUpdate)
                key.SetValue(valueName, newValue, keyKind);
            return needUpdate;
        }
    }
}
