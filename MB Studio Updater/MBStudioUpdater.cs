using System;
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

        /// - - - CHANGE IF OUTDATED - - - ///
        private const string UPDATER_64BIT_TOKEN = "bz1wa88ptglc1st";
        private const string UPDATER_32BIT_TOKEN = "kc61q6vzrizxxrp";

        private const string INDEX_STABLE_64BIT_TOKEN = "x6fznmxh99b1mgn";
        private const string INDEX_STABLE_32BIT_TOKEN = "7q27bh2kzemz01k";

        private const string INDEX_BETA_64BIT_TOKEN = "h7fh3m5i0pi7zwl";
        private const string INDEX_BETA_32BIT_TOKEN = "6e63rtfhqdt2y6w";

        private const string INDEX_DEV_64BIT_TOKEN = "3hb1y883a23520v";
        private const string INDEX_DEV_32BIT_TOKEN = "dd4c75fu8ap6klf";
        /// - - - CHANGE IF OUTDATED - - - ///

        private bool StartStudioAfterUpdate { get; }

        public static string ConsoleTitle = "MB Studio Updater [by JSYS]";

        private string Channel { get; }
        private string CurFile { get; set; }
        private string FolderPath { get; }

        private List<string> list;

        public bool SelfUpdateActive { get; }

        public bool IsConsole { get; private set; } = false;

        private bool IsTemp { get { return IsTempByCurDir(); } }

        #endregion

        public MBStudioUpdater(bool selfUpdate = false, string channel = "stable", string folderPath = ".", bool startStudioAfterUpdate = false)
        {
            SelfUpdateActive = selfUpdate;

            Channel = channel;
            FolderPath = Path.GetFullPath(folderPath);
            StartStudioAfterUpdate = startStudioAfterUpdate;

            SetIsConsole();

            if (!SelfUpdateActive)
                CleanUpdaterTemp();
        }

        #region WriteIndexFile

        public void WriteIndexFile()
        {
            LoadData();

            List<FileVersionCode> downloadedFVCs = FileVersionCode.ConvertToFileVersions(File.ReadAllLines("index.mbi"));
            List<FileVersionCode> generatedFVCs = FileVersionCode.ConvertToFileVersions(list);

            list.Clear();

            foreach (FileVersionCode downloadedFVC in downloadedFVCs)
            {
                for (int i = 0; i < generatedFVCs.Count; i++)
                {
                    if (generatedFVCs[i].FilePath.Equals(downloadedFVC.FilePath))
                    {
                        FileVersionCode f = FileVersionCode.CombineFileVersionCodes(downloadedFVC, generatedFVCs[i]);
                        list.Add(f.Code);
                        i = generatedFVCs.Count;
                    }
                }
            }

            File.WriteAllLines(Channel + ".index.mbi", list);
        }

        #endregion

        #region CheckForUpdates

        private bool IsInList(string[] indexBlocks)
        {
            bool found = false;
            foreach (string item in list)
                if (indexBlocks[2].Equals(item.Split('|')[2]))
                    found = true;
            return found;
        }

        public void CheckForUpdates()
        {
            using (StreamWriter wr = new StreamWriter("update_log.mbi", true))
            {
                LoadData();

                wr.WriteLine("[" + DateTime.Now + "]  Data loadad." + Environment.NewLine);

                string logInfo = "Check " + list.Count + " for Updates..." + Environment.NewLine;

                wr.WriteLine(Environment.NewLine + "[" + DateTime.Now + "]  " + logInfo);

                if (IsConsole)
                    Console.Write(Environment.NewLine + logInfo);

                List<string[]> updateFiles = new List<string[]>();
                string[] indexList = File.ReadAllLines("index.mbi");
                foreach (string item in indexList)
                {
                    string[] infoIndex = item.Split('|');
                    string[] newOrUpdated = new string[] { infoIndex[2], infoIndex[3] };

                    if (IsInList(infoIndex))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string[] infoList = list[i].Split('|');
                            if (infoList[2].Equals(infoIndex[2])
                                && (
                                /*ulong.Parse(infoList[1]) < ulong.Parse(infoIndex[1])//check differently later maybe
                                && */!infoList[0].Equals(infoIndex[0])))//if (outdated)
                            {
                                updateFiles.Add(newOrUpdated);
                                i = list.Count;
                            }
                        }
                    }
                    else
                        updateFiles.Add(newOrUpdated);
                }

                logInfo = " --> Files to be updated: " + updateFiles.Count + " " + Environment.NewLine;

                if (IsConsole)
                    Console.WriteLine(Environment.NewLine + logInfo);

                wr.WriteLine("[" + DateTime.Now + "]  " + logInfo);

                if (IsUpdaterOutdated(updateFiles))
                {
                    wr.WriteLine("[" + DateTime.Now + "]  Executing Self Update...");
                    SelfUpdate();
                    return;
                }

                wr.WriteLine("[" + DateTime.Now + "]  Updater up-to-date");

                if (updateFiles.Count != 0)
                {
                    wr.Write("[" + DateTime.Now + "]  Closing MB Studio (if open)...");
                    CloseMBStudioIfRunning();
                    wr.WriteLine("Done" + Environment.NewLine);

                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            foreach (string[] updateFile in updateFiles)
                            {
                                string file = updateFile[0].Substring(2);
                                //file = Path.GetFileName(file);;//if path not needed to be shown
                                CurFile = " Updating \"" + file + '\"';
                                wr.WriteLine("[" + DateTime.Now + "]" + CurFile);
                                if (IsConsole)
                                    Console.Write(CurFile);
                                file = FolderPath + updateFile[0].Substring(1);
                                if (IsConsole)
                                    Console.WriteLine(" >> " + file);
                                wr.WriteLine("[" + DateTime.Now + "]  Download Token: " + updateFile[1]);
                                wr.WriteLine("[" + DateTime.Now + "]  Destination: \"" + file + '\"');
                                client.DownloadFile("https://www.dropbox.com/s/" + updateFile[1] + "?dl=1", file);
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.ToString() + Environment.NewLine;
                            if (IsConsole)
                                Console.WriteLine(error);
                            wr.WriteLine("[" + DateTime.Now + "]  " + error);
                        }
                    }

                    if (StartStudioAfterUpdate)
                    {
                        wr.Write(Environment.NewLine + "[" + DateTime.Now + "]  Starting MB Studio...");
                        Process.Start("MB Studio.exe");
                        wr.WriteLine("Done");
                    }

                }

                wr.WriteLine(Environment.NewLine + " - - - Updating finished - - - " + Environment.NewLine);

                if (IsConsole)
                    Console.Title = ConsoleTitle + " - Finished Updating";
            }
        }

        public static bool IsUpdaterOutdated(List<string[]> updateFiles)
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

        public void SelfUpdate()
        {
            CloseMBStudioIfRunning();

            string currentPath = Path.GetFullPath(".");

            if (IsConsole)
            {
                Console.WriteLine(
                    "CurrentPath: " + currentPath + Environment.NewLine +
                    "TempPath: " + MB_STUDIO_UPDATER_TEMP + Environment.NewLine +
                    "IsTemp: " + IsTemp
                );
            }

            Process updater = new Process();
            updater.StartInfo.UseShellExecute = true;

            if (!IsTemp)
            {
                Directory.CreateDirectory(MB_STUDIO_UPDATER_TEMP);

                currentPath += "\\" + MB_STUDIO_UPDATER;
                File.Copy(currentPath, MB_STUDIO_UPDATER_TEMP + '\\' + MB_STUDIO_UPDATER);
                File.WriteAllText(MB_STUDIO_UPDATER_TEMP + "\\path.info", currentPath);

                string fullTempPath = Path.GetFullPath(MB_STUDIO_UPDATER_TEMP);
                updater.StartInfo.FileName = fullTempPath + '\\' + MB_STUDIO_UPDATER;
                updater.StartInfo.WorkingDirectory = fullTempPath;
                updater.StartInfo.Arguments = "-su";
            }
            else
            {
                string downloadPart;
                if (Environment.Is64BitOperatingSystem)
                    downloadPart = UPDATER_64BIT_TOKEN;
                else
                    downloadPart = UPDATER_32BIT_TOKEN;

                currentPath = File.ReadAllText("path.info");

                if (IsConsole)
                    Console.Write("Deleting '" + currentPath + "' ...");

                File.Delete(currentPath);

                using (WebClient client = new WebClient())
                    client.DownloadFile("https://www.dropbox.com/s/" + downloadPart + "/MB%20Studio%20Updater.exe?dl=1", currentPath);

                updater.StartInfo.FileName =  @"..\" + MB_STUDIO_UPDATER;
                updater.StartInfo.Arguments = "-stable . -startOE";//add channel and path here (or more if needed) later
            }

            if (IsConsole)
                Console.Write(Environment.NewLine + "Execute Updater...");

            updater.Start();
        }

        #endregion

        #region Helper Methods

        private void SetIsConsole()
        {
            try
            {
                Console.Title = MB_STUDIO_UPDATER;
                IsConsole = true;
            }
            catch (Exception) { }
        }

        private bool IsTempByCurDir()
        {
            string currentPath = Path.GetFullPath(".");
            int curDirNameIndex = currentPath.LastIndexOf(Path.DirectorySeparatorChar) + 1;
            currentPath = currentPath.Substring(curDirNameIndex);
            return currentPath.Equals(MB_STUDIO_UPDATER_TEMP);
        }

        private void CleanUpdaterTemp()
        {
            if (Directory.Exists(MB_STUDIO_UPDATER_TEMP) && !IsTemp)
                Directory.Delete(MB_STUDIO_UPDATER_TEMP, true);
        }

        private void LoadData(bool forceLoading = false)
        {
            if (list != null && !forceLoading) return;

            ConsoleTitle += " Channel: " + Channel;

            if (IsConsole)
                Console.Title = ConsoleTitle;

            bool Is64Bit = Environment.Is64BitOperatingSystem;

            string pathExtra;
            switch (Channel)
            {
                case "dev":
                    pathExtra = (Is64Bit) ? INDEX_DEV_64BIT_TOKEN : INDEX_DEV_32BIT_TOKEN;
                    break;
                case "beta":
                    pathExtra = (Is64Bit) ? INDEX_BETA_64BIT_TOKEN : INDEX_BETA_32BIT_TOKEN;
                    break;
                default://case "stable":
                    pathExtra = (Is64Bit) ? INDEX_STABLE_64BIT_TOKEN : INDEX_STABLE_32BIT_TOKEN;
                    break;
            }
            pathExtra += "/" + Channel;

            list = new List<string>();
            List<FileInfo> files = GetAllFiles(FolderPath);
            foreach (FileInfo file in files)
            {
                if (!UnusedFile(file))
                {
                    string md5Checksum = MD5Maker.CreateMD5ChecksumString(file.FullName);
                    long lastWriteTime = file.LastWriteTimeUtc.ToFileTime();
                    string relativeFilePath = file.FullName.Substring(FolderPath.Length);

                    list.Add(md5Checksum + "|" + lastWriteTime + "|." + relativeFilePath);
                }
            }

            using (WebClient client = new WebClient())
                client.DownloadFile("https://www.dropbox.com/s/" + pathExtra + ".index.mbi?dl=1", "index.mbi");
        }

        private static bool UnusedFile(FileInfo file)
        {
            bool unused = false;

            if (!unused)
            {
                unused = (
                    file.Extension.Equals(".pdb") || 
                    file.Extension.Equals(".mbi") || 
                    file.Extension.Equals(".config")
                );
            }

            if (!unused) unused = file.Name.EndsWith("module_info.path");

            if (!unused) unused = file.DirectoryName.Contains(Path.GetFullPath(@".\Projects"));
            if (!unused) unused = file.DirectoryName.Contains(Path.GetFullPath(@".\Python"));

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
