﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace MB_Studio_Updater
{
    public class MBStudioUpdater
    {
        #region Attributes

        public const string MB_STUDIO_UPDATER = "MB Studio Updater.exe";
        public const string MB_STUDIO_UPDATER_TEMP = "mbstudioupdater_temp";

        private bool startMBStudioAU;

        public static string ConsoleTitle = "MB Studio Updater [by JSYS]";//must be changeable for console

        private string channel;
        private string curFile;
        private string pathExtra;
        private string folderPath;

        private List<string> list;

        public bool IsConsole { get; private set; } = false;

        #endregion

        public MBStudioUpdater(string channel = "stable", string folderPath = ".", bool startMBStudioAU = false)
        {
            this.channel = channel;
            this.folderPath = Path.GetFullPath(folderPath);
            this.startMBStudioAU = startMBStudioAU;

            try
            {
                Console.Title = MB_STUDIO_UPDATER;
                IsConsole = true;
            } catch (Exception) { }

            CleanUpdaterTemp();
        }

        #region WriteIndexFile

        public void WriteIndexFile()
        {
            LoadData();

            File.WriteAllLines(channel + ".index.mbi", list);
        }

        #endregion

        #region CheckForUpdates

        public void CheckForUpdates()
        {
            LoadData();

            using (WebClient client = new WebClient())
                client.DownloadFile("https://www.dropbox.com/s/" + pathExtra + ".index.mbi?dl=1", "index.mbi");

            if (IsConsole)
                Console.Write(Environment.NewLine + "Überprüfe " + list.Count + " Dateien auf Updates ..." + Environment.NewLine);

            List<string[]> updateFiles = new List<string[]>();
            string[] indexList = File.ReadAllLines("index.mbi");
            foreach (string item in indexList)
            {
                if (!list.Contains(item))
                {
                    string[] infoIndex = item.Split('|');
                    for (int i = 0; i < list.Count; i++)
                    {
                        string[] infoList = list[i].Split('|');
                        if (infoList[2].Equals(infoIndex[2])
                            && (ulong.Parse(infoList[1]) < ulong.Parse(infoIndex[1])
                            && !infoList[0].Equals(infoIndex[0])))//if (outdated)
                        {
                            updateFiles.Add(new string[] { infoIndex[2], infoIndex[3] });
                            i = list.Count;
                        }
                    }
                }
            }

            if (IsConsole)
                Console.WriteLine(Environment.NewLine + " --> Es werden " + updateFiles.Count + " Dateien aktualisiert" + Environment.NewLine);

            if (IsUpdaterOutdated(updateFiles))
                SelfUpdate();

            if (updateFiles.Count != 0)
            {
                CloseMBStudioIfRunning();

                using (WebClient client = new WebClient())
                {
                    try
                    {
                        for (int i = 0; i < updateFiles.Count; i++)//foreach block console_output - but would be better(?)
                        {
                            string file = updateFiles[i][0].Substring(2);
                            //file = file.Substring(file.LastIndexOf('\\') + 1);//if path not needed to show
                            curFile = " - Aktualisiere " + file;
                            if (IsConsole)
                                Console.Write(curFile);
                            file = folderPath + updateFiles[i][0].Substring(1);
                            if (IsConsole)
                                Console.WriteLine(" >> " + file);
                            client.DownloadFile("https://www.dropbox.com/s/" + updateFiles[i][1] + "?dl=1", file);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(Environment.NewLine + ex.ToString() + Environment.NewLine);
                    }
                }

                if (startMBStudioAU)
                    Process.Start("MB Studio.exe");
            }

            if (IsConsole)
                Console.Title = ConsoleTitle + " - Finished Updating";
        }

        private static bool IsUpdaterOutdated(List<string[]> updateFiles)
        {
            bool updaterOutdated = false;
            foreach (string[] a in updateFiles)
            {
                string file = a[0].Substring(2);
                file = file.Substring(file.LastIndexOf('\\') + 1);
                if (!file.Equals(MB_STUDIO_UPDATER))
                    updaterOutdated = true;
            }
            return updaterOutdated;
        }

        private static void CloseMBStudioIfRunning()
        {
            Process[] processes = Process.GetProcessesByName("MB Studio");
            if (processes.Length != 0)
            {
                Process p = processes[0];
                p.CloseMainWindow();
                p.WaitForExit();
            }
        }

        private static void SelfUpdate()
        {
            CloseMBStudioIfRunning();

            string path = Path.GetFullPath(".");
            if (!path.Substring(path.LastIndexOf('\\') + 1).ToLower().Equals(MB_STUDIO_UPDATER_TEMP))
            {
                string tempPath = path + '\\' + MB_STUDIO_UPDATER_TEMP;
                Directory.CreateDirectory(tempPath);
                File.Copy(path + '\\' + MB_STUDIO_UPDATER, tempPath + '\\' + MB_STUDIO_UPDATER);
                File.WriteAllText(tempPath + "\\path.info", path);
            }
            else
            {
                string downloadPart;
                if (Environment.Is64BitOperatingSystem)
                    downloadPart = "bz1wa88ptglc1st";//update if changed!!!
                else
                    downloadPart = "kc61q6vzrizxxrp";//update if changed!!!
                path = File.ReadAllText("path.info");
                File.Delete(path);
                using (WebClient client = new WebClient())
                    client.DownloadFile("https://www.dropbox.com/s/" + downloadPart + "/MB%20Studio%20Updater.exe?dl=1", path);
            }
        }

        #endregion

        #region Helper Methods

        private void CleanUpdaterTemp()
        {
            if (Directory.Exists(MB_STUDIO_UPDATER_TEMP))
                Directory.Delete(MB_STUDIO_UPDATER_TEMP, true);
        }

        private void LoadData(bool forceLoading = false)
        {
            if (pathExtra != null && list != null && !forceLoading) return;

            ConsoleTitle += " Channel: " + channel;

            if (IsConsole)
                Console.Title = ConsoleTitle;

            bool Is64Bit = Environment.Is64BitOperatingSystem;

            if (channel.Equals("dev"))
                pathExtra = (Is64Bit) ? "3hb1y883a23520v" : "dd4c75fu8ap6klf";//change if invalid
            else if (channel.Equals("beta"))
                pathExtra = (Is64Bit) ? "h7fh3m5i0pi7zwl" : "6e63rtfhqdt2y6w";//change if invalid
            else// if (channel.Equals("stable"))
                pathExtra = (Is64Bit) ? "x6fznmxh99b1mgn" : "7q27bh2kzemz01k";//change if invalid

            pathExtra += "/" + channel;

            list = new List<string>();
            List<FileInfo> files = GetAllFiles(folderPath);
            foreach (FileInfo file in files)
                if (!UnusedFile(file))
                    list.Add(MD5Maker.CreateMD5ChecksumString(file.FullName) + "|" + file.LastWriteTimeUtc.ToFileTime() + "|." + file.FullName.Substring(folderPath.Length));
        }

        private static bool UnusedFile(FileInfo file)
        {
            string ext = file.Extension.Substring(1);

            bool unused = file.Name.Contains("skillhunter.xml");

            if (!unused) unused = file.Name.Contains("module_info.path");
            if (!unused) unused = (ext.Equals("pdb") || ext.Equals("mbi") || ext.Equals("config"));
            if (!unused) unused = file.DirectoryName.Contains(Path.GetFullPath(".\\Projects"));

            return unused;
        }

        private static List<FileInfo> GetAllFiles(string folderPath)
        {
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            List<FileInfo> allFiles = new List<FileInfo>();
            DirectoryInfo[] subDirs = dir.GetDirectories();

            allFiles.AddRange(dir.GetFiles());

            if (subDirs.Length != 0)
                foreach (DirectoryInfo dirs in subDirs)
                    allFiles.AddRange(GetAllFiles(dirs.FullName));

            return allFiles;
        }

        #endregion
    }
}
