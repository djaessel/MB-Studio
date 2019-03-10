#if NET462
using Microsoft.Win32;
#endif
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace importantLib
{
    public class SteamSearch
    {
        private static List<string> libraryPaths = new List<string>();

        public static string SteamPath { get; private set; } = string.Empty;

        public static string[] LibraryFolderPaths { get { return libraryPaths.ToArray(); } }

        public static bool SearchSteamPath(bool showErrorMsg, string errorInformationMessage = "Please edit the file \".\\files\\module_info.path\" and change the example path to the real location!")
        {
            bool foundIt = SearchSteamRegistryInstallPath();
            if (!foundIt)
            {
                string vName = string.Empty;
                string steamNeededFolders = "Steam\\SteamApps\\common";
                foreach (DriveInfo drinfo in DriveInfo.GetDrives())
                {
                    vName = drinfo.Name;
                    if (drinfo.IsReady)
                    {
                        if (Directory.Exists(vName + "Program Files\\" + steamNeededFolders))
                            SteamPath = vName + "Program Files";
                        else if (Directory.Exists(vName + "Program Files (x86)\\" + steamNeededFolders))
                            SteamPath = vName + "Program Files (x86)";
                        else if (Directory.Exists(vName + steamNeededFolders))
                            SteamPath = vName.Substring(0, vName.Length - 1);
                        if (SteamPath.Length > 0)
                        {
                            SteamPath += "\\Steam";
                            foundIt = true;
                        }
                    }
                }
            }

            if (!foundIt)
            {
                errorInformationMessage = "The application couldn't find the Steam path!" + Environment.NewLine + errorInformationMessage;
                if (showErrorMsg)
                    MessageBox.Show(errorInformationMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    Console.WriteLine(errorInformationMessage);
            }

            return foundIt;
        }

        public static bool SearchSteamRegistryInstallPath()
        {
            bool found_it = false;

            #if NET462
            
            RegistryKey steamRegKey = null;
            try
            {
                if (ImportantMethods.Is64BitOperatingSystem())
                    steamRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Valve\\Steam", RegistryKeyPermissionCheck.Default, System.Security.AccessControl.RegistryRights.ReadKey);
            }
            catch (Exception) { }
            try
            {
                if (!ImportantMethods.Is64BitOperatingSystem())
                    steamRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Valve\\Steam", RegistryKeyPermissionCheck.Default, System.Security.AccessControl.RegistryRights.ReadKey);
            }
            catch (Exception) { }
            if (steamRegKey != null)
            {
                string installPath = steamRegKey.GetValue("InstallPath", string.Empty).ToString();
                if (!installPath.Equals(string.Empty))
                {
                    SteamPath = installPath;
                    found_it = true;
                }
            }

            #endif

            return found_it;
        }

        public static bool HasLibraryFolders
        {
            get
            {
                bool hasFolders = false;
                if (!SteamPath.Equals(string.Empty))
                {
                    string tmp = SteamPath + "\\steamapps";
                    if (Directory.Exists(tmp))
                    {
                        string filePath = tmp + "\\libraryfolders.vdf";
                        if (File.Exists(filePath))
                        {
                            libraryPaths.Clear();
                            using (StreamReader sr = new StreamReader(filePath))
                            {
                                string[] tmpS;
                                while (!sr.EndOfStream && !tmp.Trim().Equals("}"))
                                {
                                    tmpS = sr.ReadLine().Split('\"');
                                    if (tmpS.Length > 1)
                                        if (ImportantMethods.IsNumericFKZ128(tmpS[1]) && tmpS.Length > 4)
                                            libraryPaths.Add(tmpS[3].Replace("\\\\", "\\"));
                                }
                            }
                        }
                    }
                    if (libraryPaths.Count > 0)
                        hasFolders = true;
                }
                return hasFolders;
            }
        }
    }
}
