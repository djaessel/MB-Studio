using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace importantLib
{
    public class SteamSearch
    {
        private static string steamPath = string.Empty;
        private static List<string> libraryPaths = new List<string>();

        public static bool SearchSteamPath(string errorInformationMessage = "Please edit the file \"module_info.py\" and set \"STEAMPATH\" to the real location!")
        {
            bool found_it = SearchSteamRegistryInstallPath();
            if (!found_it)
            {
                string v_name = string.Empty;
                string steamNeededFolders = "Steam\\SteamApps\\common";
                foreach (DriveInfo drinfo in DriveInfo.GetDrives())
                {
                    v_name = drinfo.Name;
                    if (drinfo.IsReady)
                    {
                        if (Directory.Exists(v_name + "Program Files\\" + steamNeededFolders))
                            steamPath = v_name + "Program Files";
                        else if (Directory.Exists(v_name + "Program Files (x86)\\" + steamNeededFolders))
                            steamPath = v_name + "Program Files (x86)";
                        else if (Directory.Exists(v_name + steamNeededFolders))
                            steamPath = v_name.Substring(0, v_name.Length - 1);
                        if (steamPath.Length > 0)
                        {
                            steamPath += "\\Steam";
                            found_it = true;
                        }
                    }
                }
            }
            if (!found_it)
                MessageBox.Show("The application couldn't find the Steam path!" + Environment.NewLine + errorInformationMessage, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return found_it;
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
                    steamPath = installPath;
                    found_it = true;
                }
            }

            #endif

            return found_it;
        }

        public static string SteamPath { get { return steamPath; } }

        public static bool HasLibraryFolders
        {
            get
            {
                bool hasFolders = false;
                if (!steamPath.Equals(string.Empty))
                {
                    string tmp = steamPath + "\\steamapps";
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
                                        if (ImportantMethods.IsNumericFKZ2(tmpS[1]) && tmpS.Length > 4)
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

        public static string[] LibraryFolderPaths { get { return libraryPaths.ToArray(); } }

    }
}
