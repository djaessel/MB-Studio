using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;

namespace MB_Studio_Updater
{
    public class MBStudioUpdater
    {
        #region Constants

        public const string MB_STUDIO_UPDATER = "MB Studio Updater.exe";
        public const string MB_STUDIO_UPDATER_TEMP = "mbstudioupdater_temp";

        #region UPDATE_DOWNLOAD_TOKENS

        /// - - - CHANGE IF OUTDATED - - - ///
        private const string UPDATER_STABLE_64BIT_TOKEN = "bz1wa88ptglc1st";
        private const string UPDATER_STABLE_32BIT_TOKEN = "nsv3p53i1nf4my0";//old: kc61q6vzrizxxrp

        private const string UPDATER_BETA_64BIT_TOKEN = "t8lcwxf0b1ppgfm";
        private const string UPDATER_BETA_32BIT_TOKEN = "xa1dtzhci0866v4";

        private const string UPDATER_DEV_64BIT_TOKEN = "gu0nu5zyb28z7kx";
        private const string UPDATER_DEV_32BIT_TOKEN = "m6hobz4t927cy8m";
        /// - - - CHANGE IF OUTDATED - - - ///

        #endregion

        #region INDEX_FILE_DOWNLOAD_TOKENS

        /// - - - CHANGE IF OUTDATED - - - ///
        private const string INDEX_STABLE_64BIT_TOKEN = "x6fznmxh99b1mgn";
        private const string INDEX_STABLE_32BIT_TOKEN = "7q27bh2kzemz01k";

        private const string INDEX_BETA_64BIT_TOKEN = "h7fh3m5i0pi7zwl";
        private const string INDEX_BETA_32BIT_TOKEN = "6e63rtfhqdt2y6w";

        private const string INDEX_DEV_64BIT_TOKEN = "3hb1y883a23520v";
        private const string INDEX_DEV_32BIT_TOKEN = "dd4c75fu8ap6klf";
        /// - - - CHANGE IF OUTDATED - - - ///

        #endregion

        #endregion

        #region Attributes

        private bool StartStudioAfterUpdate { get; }

        public static string ConsoleTitle = "MB Studio Updater [by JSYS]";

        private bool Force32BitBinaries { get; }

        private bool Is64BitBinary { get { return Is64Bit(); } }

        private string Channel { get; }
        private string CurFile { get; set; }
        private string FolderPath { get; }

        private List<string> list;

        public bool AddNewFiles { get; }
        public bool SelfUpdateActive { get; }

        public static bool IsConsole { get; private set; } = false;

        private bool IsTemp { get { return IsTempByCurDir(); } }

        #endregion

        public MBStudioUpdater(bool selfUpdate = false, bool addNewFiles = false, bool force32BitBinaries = false, string channel = "stable", string folderPath = ".", bool startOE = false)
        {
            SelfUpdateActive = selfUpdate;
            AddNewFiles = addNewFiles;

            Force32BitBinaries = force32BitBinaries;

            Channel = channel;
            FolderPath = Path.GetFullPath(folderPath);
            StartStudioAfterUpdate = startOE;

            SetIsConsole();
        }

        #region WriteIndexFile

        public void WriteIndexFile()
        {
            LoadData();

            List<FileVersionCode> downloadedFVCs = FileVersionCode.ConvertToFileVersions(File.ReadAllLines("index.mbi"));
            List<FileVersionCode> generatedFVCs = FileVersionCode.ConvertToFileVersions(list);

            list.Clear();

            foreach (FileVersionCode generatedFVC in generatedFVCs)
            {
                bool foundFile = false;

                for (int i = 0; i < downloadedFVCs.Count; i++)
                {
                    foundFile = generatedFVC.FilePath.Equals(downloadedFVCs[i].FilePath);
                    if (foundFile)
                    {
                        FileVersionCode f = FileVersionCode.CombineFileVersionCodes(downloadedFVCs[i], generatedFVC);
                        list.Add(f.Code);
                        i = downloadedFVCs.Count;
                    }
                }

                if (!foundFile && AddNewFiles)
                    list.Add(generatedFVC.Code);
            }

            File.WriteAllLines(Channel + ".index.mbi", list);
        }

        #endregion

        #region CheckForUpdates

        private bool IsInList(List<string> list, string fileName)
        {
            bool found = false;
            foreach (string item in list)
                if (fileName.Equals(item.Split('|')[2]))
                    found = true;
            return found;
        }

        private List<string> GetUnknownFiles(List<string> indexLines)
        {
            List<string> unknownFiles = new List<string>();
            foreach (string item in list)
                if (!IsInList(indexLines, item.Split('|')[2]))
                    unknownFiles.Add(item);
            return unknownFiles;
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
                List<string> indexList = new List<string> (File.ReadAllLines("index.mbi"));
                foreach (string item in indexList)
                {
                    string[] infoIndex = item.Split('|');
                    string[] newOrUpdated = new string[] { infoIndex[2], infoIndex[3] };

                    if (IsInList(list, infoIndex[2]))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string[] infoList = list[i].Split('|');
                            if (infoList[2].Equals(infoIndex[2])
                                && (
                                /*ulong.Parse(infoList[1]) < ulong.Parse(infoIndex[1])//check differently later maybe
                                && */!infoList[0].Equals(infoIndex[0])))//if (outdated)
                            {
                                Console.WriteLine(infoList[2] + " --> " + infoList[0] + " != " + infoIndex[0]);
                                updateFiles.Add(newOrUpdated);
                                i = list.Count;
                            }
                        }
                    }
                    else
                        updateFiles.Add(newOrUpdated);
                }


                logInfo = "Removing old binary files..." + Environment.NewLine;

                wr.WriteLine(Environment.NewLine + "[" + DateTime.Now + "]  " + logInfo);

                if (IsConsole)
                    Console.Write(Environment.NewLine + logInfo);

                RemoveUnknownBinaries(indexList);


                logInfo = " --> Files to be updated: " + updateFiles.Count + " " + Environment.NewLine;

                if (IsConsole)
                    Console.WriteLine(Environment.NewLine + logInfo);

                wr.WriteLine("[" + DateTime.Now + "]  " + logInfo);

                if (IsUpdaterOutdated(updateFiles))
                {
                    wr.WriteLine("[" + DateTime.Now + "]  Executing Self Update...");

                    wr.Flush();//necessary?
                    wr.Close();

                    SelfUpdate();
                    return;
                }

                wr.WriteLine("[" + DateTime.Now + "]  Updater up-to-date");

                if (updateFiles.Count != 0)
                {
                    wr.Write("[" + DateTime.Now + "]  Closing MB Studio (if open)...");
                    CloseAllMBStudioIfRunning();
                    wr.WriteLine("Done" + Environment.NewLine);

                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            foreach (string[] updateFile in updateFiles)
                            {
                                string file = updateFile[0].Substring(2);
                                //file = Path.GetFileName(file);//if path not needed to be shown
                                CurFile = " Updating \"" + file + '\"';
                                wr.WriteLine("[" + DateTime.Now + "]" + CurFile);
                                if (IsConsole)
                                    Console.Write(CurFile);
                                file = FolderPath + updateFile[0].Substring(1);
                                string folder = Path.GetDirectoryName(file);
                                Directory.CreateDirectory(folder);
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

                        string curPath = Path.GetFullPath(".");
                        string filePath = curPath + "\\MB Studio.exe";

                        ProcessStartInfo studioPsi = new ProcessStartInfo
                        {
                            FileName = filePath,
                            WorkingDirectory = curPath,
                            UseShellExecute = true,
                            CreateNoWindow = true,
                            Arguments = "-au"
                        };

                        Process.Start(studioPsi);
                        
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
                if (IsConsole)
                    Console.WriteLine(file);
                if (file.Equals(MB_STUDIO_UPDATER))
                    updaterOutdated = true;
            }
            if (IsConsole)
                Console.WriteLine("Updater outdated: " + updaterOutdated);
            return updaterOutdated;
        }

        private static void CloseAllMBStudioIfRunning()
        {
            Process[] processes = Process.GetProcessesByName("MB Studio");
            while (processes.Length != 0)
            {
                Process p = processes[0];
                p.CloseMainWindow();
                p.WaitForExit();
            }
        }

        public void SelfUpdate()
        {
            CloseAllMBStudioIfRunning();

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

            if (!IsTemp)
                SelfUpdateMoveToTemp(ref updater, currentPath);
            else
                SelfUpdateDownloadNew(ref updater, currentPath);

            if (IsConsole)
                Console.Write(Environment.NewLine + "Executing Updater...");

            updater.StartInfo.UseShellExecute = true;
            updater.Start();

            Environment.Exit(0);//necessary?
        }

        private void SelfUpdateMoveToTemp(ref Process updater, string currentPath)
        {
            Directory.CreateDirectory(MB_STUDIO_UPDATER_TEMP);

            currentPath += "\\" + MB_STUDIO_UPDATER;
            File.Copy(currentPath, MB_STUDIO_UPDATER_TEMP + '\\' + MB_STUDIO_UPDATER, true);
            File.WriteAllText(MB_STUDIO_UPDATER_TEMP + "\\path.info", currentPath);

            string fullTempPath = Path.GetFullPath(MB_STUDIO_UPDATER_TEMP);
            updater.StartInfo.FileName = fullTempPath + '\\' + MB_STUDIO_UPDATER;
            updater.StartInfo.WorkingDirectory = fullTempPath;
            updater.StartInfo.Arguments = "-su";
        }

        private void SelfUpdateDownloadNew(ref Process updater, string currentPath)
        {
            bool is64Bit = Is64BitBinary;

            string downloadPart;
            switch (Channel)
            {
                case "dev":
                    downloadPart = (is64Bit) ? UPDATER_DEV_64BIT_TOKEN : UPDATER_DEV_32BIT_TOKEN;
                    break;
                case "beta":
                    downloadPart = (is64Bit) ? UPDATER_BETA_64BIT_TOKEN : UPDATER_BETA_32BIT_TOKEN;
                    break;
                //case "stable":
                default:
                    downloadPart = (is64Bit) ? UPDATER_STABLE_64BIT_TOKEN : UPDATER_STABLE_32BIT_TOKEN;
                    break;
            }

            currentPath = File.ReadAllText("path.info");

            string fullRootPath = Path.GetFullPath(@"..\");
            string updatedUpdaterPath = fullRootPath + MB_STUDIO_UPDATER;

            if (IsConsole)
            {
                Console.WriteLine("Done");
                Console.Write("Downloading new updater version...");
            }

            string downloadedFile = currentPath + ".tmp";
            using (WebClient client = new WebClient())
                client.DownloadFile("https://www.dropbox.com/s/" + downloadPart + "/MB%20Studio%20Updater.exe?dl=1", downloadedFile);

            if (IsConsole)
            {
                Console.WriteLine("Done");
                Console.Write("Replacing old with new version...");
            }

            string backupFile = currentPath + ".bak";
            File.Replace(downloadedFile, currentPath, backupFile);//maybe copy later

            if (IsConsole)
                Console.WriteLine("Done");

            updater.StartInfo.FileName = updatedUpdaterPath;
            updater.StartInfo.WorkingDirectory = fullRootPath;
            updater.StartInfo.Arguments = "-" + Channel + " . -startOE";//maybe change arguments later if needed
        }

        private void RemoveUnknownBinaries(List<string> unknownFiles)
        {
            List<string> delFileExtensions = new List<string>() {
                ".dll", ".exe", ".bat", ".vbs"//, ...
            };

            foreach (string unknownFile in unknownFiles)
            {
                string ext = unknownFile.Substring(unknownFile.LastIndexOf('.'));
                if (delFileExtensions.Contains(ext))
                    File.Delete(unknownFile);
            }
        }

        #endregion

        #region Helper Methods

        private static void SetIsConsole()
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
            {
                if (IsConsole)
                    Console.Write("Deleting temporary files...");

                Directory.Delete(MB_STUDIO_UPDATER_TEMP, true);
                
                if (IsConsole)
                    Console.WriteLine("Done");
            }
        }

        private void LoadData(bool forceLoading = false)
        {
            if (list != null && !forceLoading) return;

            ConsoleTitle += " Channel: " + Channel;

            if (IsConsole)
                Console.Title = ConsoleTitle;

            bool is64Bit = Is64BitBinary;

            string pathExtra;
            switch (Channel)
            {
                case "dev":
                    pathExtra = (is64Bit) ? INDEX_DEV_64BIT_TOKEN : INDEX_DEV_32BIT_TOKEN;
                    break;
                case "beta":
                    pathExtra = (is64Bit) ? INDEX_BETA_64BIT_TOKEN : INDEX_BETA_32BIT_TOKEN;
                    break;
                //case "stable":
                default:
                    pathExtra = (is64Bit) ? INDEX_STABLE_64BIT_TOKEN : INDEX_STABLE_32BIT_TOKEN;
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

        private bool Is64Bit()
        {
            bool is64Bit = Environment.Is64BitOperatingSystem;
            if (Force32BitBinaries)
                is64Bit = false;
            return is64Bit;
        }

        #endregion
    }
}
