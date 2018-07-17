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

        private bool StartStudioAfterUpdate { get; }

        public static string ConsoleTitle = "MB Studio Updater [by JSYS]";//must be changeable for console

        private string Channel { get; }
        private string CurFile { get; set; }
        private string FolderPath { get; }

        private List<string> list;

        public bool IsConsole { get; private set; } = false;

        #endregion

        public MBStudioUpdater(string channel = "stable", string folderPath = ".", bool startStudioAfterUpdate = false)
        {
            Channel = channel;
            FolderPath = Path.GetFullPath(folderPath);
            StartStudioAfterUpdate = startStudioAfterUpdate;

            SetIsConsole();
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

        public void CheckForUpdates()
        {
            using (StreamWriter wr = new StreamWriter("update_log.mbi", true))
            {
                LoadData();

                wr.WriteLine("[" + DateTime.Now + "]  Data loadad." + Environment.NewLine);

                string logInfo = "Überprüfe " + list.Count + " Dateien auf Updates ..." + Environment.NewLine;

                wr.WriteLine(Environment.NewLine + "[" + DateTime.Now + "]  " + logInfo);

                if (IsConsole)
                    Console.Write(Environment.NewLine + logInfo);

                List<string[]> updateFiles = new List<string[]>();
                string[] indexList = File.ReadAllLines("index.mbi");
                foreach (string item in indexList)
                {
                    string[] infoIndex = item.Split('|');
                    string[] newOrUpdated = new string[] { infoIndex[2], infoIndex[3] };
                    if (list.Contains(item))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string[] infoList = list[i].Split('|');
                            if (infoList[2].Equals(infoIndex[2])
                                && (/*ulong.Parse(infoList[1]) < ulong.Parse(infoIndex[1])//deactivated for now because of old files - replace with version later
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

                logInfo = " --> Es werden " + updateFiles.Count + " Dateien heruntergeladen und aktualisiert" + Environment.NewLine;

                if (IsConsole)
                    Console.WriteLine(Environment.NewLine + logInfo);

                wr.WriteLine("[" + DateTime.Now + "]  " + logInfo);

                //if (IsUpdaterOutdated(updateFiles)) // Maybe reactive later if good working solution found
                //{
                //    wr.WriteLine("[" + DateTime.Now + "]  Executing Self Update");
                //    wr.WriteLine("[" + DateTime.Now + "] Please contact publisher!");
                //    SelfUpdate();
                //}
                //else
                //{
                //    wr.WriteLine("[" + DateTime.Now + "]  Updater up-to-date");

                if (updateFiles.Count != 0)
                {
                    wr.Write("[" + DateTime.Now + "]  Closing MB Studio (if open)...");
                    CloseMBStudioIfRunning();
                    wr.WriteLine("Done" + Environment.NewLine);

                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            for (int i = 0; i < updateFiles.Count; i++)//foreach block console_output - but would be better(?)
                            {
                                string file = updateFiles[i][0].Substring(2);
                                //file = file.Substring(file.LastIndexOf('\\') + 1);//if path not needed to show
                                CurFile = " Updating \"" + file + '\"';
                                wr.WriteLine("[" + DateTime.Now + "]" + CurFile);
                                if (IsConsole)
                                    Console.Write(CurFile);
                                file = FolderPath + updateFiles[i][0].Substring(1);
                                if (IsConsole)
                                    Console.WriteLine(" >> " + file);
                                wr.WriteLine("[" + DateTime.Now + "]  Download Token: " + updateFiles[i][1]);
                                wr.WriteLine("[" + DateTime.Now + "]  Destination: \"" + file + '\"');
                                client.DownloadFile("https://www.dropbox.com/s/" + updateFiles[i][1] + "?dl=1", file);
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.ToString() + Environment.NewLine;
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

        private void SelfUpdate()
        {
            CloseMBStudioIfRunning();

            string path = Path.GetFullPath(".");

            bool isTemp = Path.GetDirectoryName(path).Equals(MB_STUDIO_UPDATER_TEMP);

            if (IsConsole)
            {
                Console.WriteLine(
                    "PATH_X: " + Path.GetDirectoryName(path)
                    + Environment.NewLine + "PATH_T: " + MB_STUDIO_UPDATER_TEMP
                    + Environment.NewLine + "IsTemp: " + isTemp
                );
            }

            if (!isTemp)//if (!path.Substring(path.LastIndexOf('\\') + 1).ToLower().Equals(MB_STUDIO_UPDATER_TEMP))
            {
                string tempPath = path + '\\' + MB_STUDIO_UPDATER_TEMP;

                Directory.CreateDirectory(tempPath);

                File.Copy(path + '\\' + MB_STUDIO_UPDATER, tempPath + '\\' + MB_STUDIO_UPDATER);
                File.WriteAllText(tempPath + "\\path.info", path);

                Process.Start(tempPath + '\\' + MB_STUDIO_UPDATER);
            }
            else
            {
                string downloadPart;
                if (Environment.Is64BitOperatingSystem)
                    downloadPart = "bz1wa88ptglc1st";//update if changed!!!
                else
                    downloadPart = "kc61q6vzrizxxrp";//update if changed!!!
                path = File.ReadAllText("path.info");

                System.Threading.Thread.Sleep(2500);//should be not needed!!!

                File.Delete(path);

                using (WebClient client = new WebClient())
                    client.DownloadFile("https://www.dropbox.com/s/" + downloadPart + "/MB%20Studio%20Updater.exe?dl=1", path);
            }
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

        private void CleanUpdaterTemp()
        {
            if (Directory.Exists(MB_STUDIO_UPDATER_TEMP))
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
                    pathExtra = (Is64Bit) ? "3hb1y883a23520v" : "dd4c75fu8ap6klf";//change if invalid
                    break;
                case "beta":
                    pathExtra = (Is64Bit) ? "h7fh3m5i0pi7zwl" : "6e63rtfhqdt2y6w";//change if invalid
                    break;
                default://case "stable":
                    pathExtra = (Is64Bit) ? "x6fznmxh99b1mgn" : "7q27bh2kzemz01k";//change if invalid
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

            if (!unused) unused = file.DirectoryName.Contains(Path.GetFullPath(".\\Projects"));
            if (!unused) unused = file.DirectoryName.Contains(Path.GetFullPath(".\\Python"));

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
