using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace MB_Studio_Updater
{
    public class MBStudioUpdater
    {
        #region Constants

        //public const string IMPORTANT_LIB = "importantLib.dll";

        public const string MB_STUDIO_UPDATER = "MB Studio Updater.exe";
        public const string MB_STUDIO_UPDATER_TEMP = "mbstudioupdater_temp";

        private const string DEFAULT_CONSOLE_TITLE = "MB Studio Updater [by JSYS]";

        private const string DROPBOX_URL_START = "https://www.dropbox.com/s/";
        private const string DROPBOX_URL_END = "?dl=1";

        public enum MB_UPDATER_MODE : byte
        {
            USE_GUI = 1,
            START_STUDIO_ON_EXIT = 2,
            WRITE_INDEX = 4,
            SELF_UPDATE = 8,
            ADD_NEW_FILES = 16,
            FORCE_32_BIT = 32,
            RESERVED_2 = 64,
            RESERVED_1 = 128,
        }

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

        private GUI gui;
        
        public static string ConsoleTitle = DEFAULT_CONSOLE_TITLE;

        public string Channel { get; private set; }
        public string CurFile { get; private set; }
        public string FolderPath { get; private set; }

        public int CurProgress { get; private set; } = 0;

        private List<string> list;

        public bool UseGUI { get; }
        public bool StartStudioOnExit { get; }
        public bool WriteIndexActive { get; }
        public bool SelfUpdateActive { get; }
        public bool AddNewFiles { get; }

        private bool Forced32Bit { get; }
        public bool Is64BitBinary { get { return Is64Bit(); } }

        public static bool IsConsole { get; private set; } = false;

        private bool IsTemp { get { return IsTempByCurDir(); } }

        #endregion

        public MBStudioUpdater(MB_UPDATER_MODE mode = 0, List<string> textArguments = null)
        {
            UseGUI = ((mode & MB_UPDATER_MODE.USE_GUI) == MB_UPDATER_MODE.USE_GUI);
            StartStudioOnExit = ((mode & MB_UPDATER_MODE.START_STUDIO_ON_EXIT) == MB_UPDATER_MODE.START_STUDIO_ON_EXIT);
            WriteIndexActive = ((mode & MB_UPDATER_MODE.WRITE_INDEX) == MB_UPDATER_MODE.WRITE_INDEX);
            SelfUpdateActive = ((mode & MB_UPDATER_MODE.SELF_UPDATE) == MB_UPDATER_MODE.SELF_UPDATE);
            AddNewFiles = ((mode & MB_UPDATER_MODE.ADD_NEW_FILES) == MB_UPDATER_MODE.ADD_NEW_FILES);
            Forced32Bit = ((mode & MB_UPDATER_MODE.FORCE_32_BIT) == MB_UPDATER_MODE.FORCE_32_BIT);

            SetTextArguments(ref textArguments);

            Channel = textArguments[0];
            FolderPath = Path.GetFullPath(textArguments[1]);

            SetIsConsole();

            CloseAllOtherInstances();
        }

        private void SetTextArguments(ref List<string> textArguments)
        {
            string channel = "stable";
            string folderPath = Path.GetFullPath(".");

            if (textArguments == null)
                textArguments = new List<string>();

            switch (textArguments.Count)
            {
                case 0:
                    textArguments = new List<string>() { channel, folderPath };
                    break;
                case 1:
                    textArguments.Add(folderPath);
                    break;
                default:
                    break;
            }
        }

        public void SetGui(GUI gui)
        {
            this.gui = gui;

            ControlWriter writer = new ControlWriter(gui.console_richtxt, gui);
            Console.SetOut(writer);
        }

        #region WriteIndexFile

        public void WriteIndexFile()
        {
            LoadData();

            List<FileVersionCode> downloadedFVCs = FileVersionCode.ConvertToFileVersions(File.ReadAllLines("index.mbi"));
            List<FileVersionCode> generatedFVCs = FileVersionCode.ConvertToFileVersions(list);

            list.Clear();

            Console.WriteLine("Writing " + Channel + " index file..." + Environment.NewLine);

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
                {
                    list.Add(generatedFVC.Code);
                    Console.WriteLine("Added new file: " + generatedFVC.Code);
                }
            }

            File.WriteAllLines(Channel + ".index.mbi", list);

            Console.WriteLine(Environment.NewLine + "Finished." + Environment.NewLine);
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
            string logFileName = "update_log.mbi";
            int maxLogSize = short.MaxValue * byte.MaxValue; // 8.355.585 Bytes -> ca. 8 MB

            if (File.Exists(logFileName))
            {
                FileInfo logFileInfo = new FileInfo(logFileName);
                if (logFileInfo.Length >= maxLogSize)
                {
                    // zip logfile here
                    File.Copy(logFileName, logFileName + ".bak"); // replace this with zipping part later!

                    File.Delete(logFileName);
                }
            }

            using (StreamWriter wr = new StreamWriter(logFileName, true))
            {
                LoadData();

                WriteLog(wr, "Data loaded." + Environment.NewLine + Environment.NewLine);

                string logInfo = "Check " + list.Count + " files for Updates..." + Environment.NewLine;
                Console.WriteLine(logInfo);
                WriteLog(wr, logInfo);

                List<string[]> updateFiles = new List<string[]>();
                List<string> indexList = new List<string>(File.ReadAllLines("index.mbi"));
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
                                /*ulong.Parse(infoList[1]) < ulong.Parse(infoIndex[1])// check version here later maybe
                                && */!infoList[0].Equals(infoIndex[0])))
                            {
                                //Console.WriteLine(infoList[2] + " --> " + infoList[0] + " != " + infoIndex[0]);
                                updateFiles.Add(newOrUpdated);
                                i = list.Count;
                            }
                        }
                    }
                    else
                        updateFiles.Add(newOrUpdated);
                }

                logInfo = "Removing old binary files..." + Environment.NewLine;

                Console.WriteLine(logInfo);
                WriteLog(wr, string.Empty, false);
                WriteLog(wr, logInfo);

                RemoveUnknownBinaries(indexList);

                logInfo = " --> Files to be updated: " + updateFiles.Count + " " + Environment.NewLine;

                Console.WriteLine(Environment.NewLine + logInfo);
                WriteLog(wr, logInfo);

                if (IsUpdaterOutdated(updateFiles))
                {
                    logInfo = "Executing Self Update...";
                    WriteLog(wr, logInfo);

                    wr.Flush();
                    wr.Close();

                    Console.WriteLine(Environment.NewLine + logInfo);

                    SelfUpdate();

                    return;
                }

                WriteLog(wr, "Updater up-to-date");

                if (updateFiles.Count != 0)
                {
                    WriteLog(wr, "Closing MB Studio (if open)...", true, false);
                    CloseAllMBStudioIfRunning();
                    WriteLog(wr, "Done" + Environment.NewLine, false);

                    Thread clientThread = new Thread(new ParameterizedThreadStart(ClientWork)) { IsBackground = true };
                    clientThread.Start(new object[] { wr, updateFiles });

                    if (StartStudioOnExit)
                    {
                        WriteLog(wr, string.Empty, false);
                        WriteLog(wr, "Starting MB Studio...", true, false);

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

                        WriteLog(wr, "Done", false);
                    }
                }

                WriteLog(wr, Environment.NewLine + " - - - Updating finished - - - " + Environment.NewLine, false);

                Console.Title = ConsoleTitle + " - Finished Updating";
            }
        }

        private void ClientWork(object param)
        {
            object[] paramS = (object[])param;

            StreamWriter wr = (StreamWriter)paramS[0];
            List<string[]> updateFiles = (List<string[]>)paramS[1];

            using (WebClient client = new WebClient())
            {
                try
                {
                    int fileNo = 0;
                    foreach (string[] updateFile in updateFiles)
                    {
                        string file = updateFile[0].Substring(2);
                        //file = Path.GetFileName(file);//if path not needed to be shown
                        CurFile = "Updating \"" + file + '\"';
                        UpdateInfoText();
                        Console.Write(" " + CurFile);
                        WriteLog(wr, CurFile, true, false);

                        file = FolderPath + updateFile[0].Substring(1);

                        string folder = Path.GetDirectoryName(file);
                        Directory.CreateDirectory(folder);
                        string logInfo = " >> " + file;
                        Console.WriteLine(logInfo);
                        WriteLog(wr, logInfo, false);

                        WriteLog(wr, "Download Token: " + updateFile[1]);
                        WriteLog(wr, "Destination: \"" + file + '\"');

                        client.DownloadFile(DROPBOX_URL_START + updateFile[1] + DROPBOX_URL_END, file);
                        fileNo++;

                        CurProgress = fileNo / updateFiles.Count * 100;
                        UpdateInfoText();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.ToString() + Environment.NewLine;
                    Console.WriteLine(error);
                    WriteLog(wr, error);
                }
            }
        }

        private void UpdateInfoText()
        {
            gui.Invoke((MethodInvoker)delegate
            {
                gui.progressInfo_lbl.Text = "( " + CurProgress + " % ) " + CurFile + "...";
                gui.update_pb.Value = CurProgress;
            });
        }

        private static void WriteLog(StreamWriter logWriter, string text, bool writeDateTime = true, bool newLine = true)
        {
            string dateTimeNow = "[" + DateTime.Now + "]";
            if (writeDateTime)
                text = dateTimeNow + "  " + text;
            if (newLine)
                logWriter.WriteLine(text);
            else
                logWriter.Write(text);
        }

        public static bool IsUpdaterOutdated(List<string[]> updateFiles)
        {
            bool updaterOutdated = false;
            foreach (string[] a in updateFiles)
            {
                string file = a[0].Substring(2);
                file = file.Substring(file.LastIndexOf('\\') + 1);
                Console.WriteLine(file);
                if (file.Equals(MB_STUDIO_UPDATER))
                    updaterOutdated = true;
            }
            Console.WriteLine("Updater outdated: " + updaterOutdated);
            return updaterOutdated;
        }

        private static void CloseAllOtherInstances()
        {
            Console.Write(Environment.NewLine + "Closing all MB Studio instances...");

            Process curProc = Process.GetCurrentProcess();

            string productName = Application.ProductName.Split('.')[0];
            Process[] processes;
            do
            {
                processes = Process.GetProcessesByName(productName);

                if (processes.Length > 1)
                {
                    Console.WriteLine("Running Instances: " + processes.Length);

                    Process p = processes[0];
                    if (p.Id.Equals(curProc.Id))
                        p = processes[1];

                    p.CloseMainWindow();
                    p.WaitForExit();
                }
            } while (processes.Length > 1);

            Console.WriteLine("Done" + Environment.NewLine);
        }

        private static void CloseAllMBStudioIfRunning()
        {
            Console.Write(Environment.NewLine + "Closing all MB Studio instances...");

            Process[] processes;
            do
            {
                processes = Process.GetProcessesByName("MB Studio");

                if (processes.Length != 0)
                {
                    Console.WriteLine("Running Instances: " + processes.Length);

                    Process p = processes[0];
                    p.CloseMainWindow();
                    p.WaitForExit();
                }
            } while (processes.Length != 0);

            Console.WriteLine("Done" + Environment.NewLine);
        }

        public void SelfUpdate()
        {
            CloseAllMBStudioIfRunning();

            string currentPath = Path.GetFullPath(".");

            Console.WriteLine(
                "CurrentPath: " + currentPath + Environment.NewLine +
                "TempPath: " + MB_STUDIO_UPDATER_TEMP + Environment.NewLine +
                "IsTemp: " + IsTemp
            );

            Process updater = new Process();

            if (!IsTemp)
                SelfUpdateMoveToTemp(ref updater, currentPath);
            else
                SelfUpdateDownloadNew(ref updater, currentPath);

            Console.Write(Environment.NewLine + "Executing Updater...");

            updater.StartInfo.UseShellExecute = true;
            updater.Start();

            Environment.Exit(0);//necessary?
        }

        private void SelfUpdateMoveToTemp(ref Process updater, string currentPath)
        {
            Directory.CreateDirectory(MB_STUDIO_UPDATER_TEMP);

            File.Copy(currentPath + '\\' + MB_STUDIO_UPDATER, MB_STUDIO_UPDATER_TEMP + '\\' + MB_STUDIO_UPDATER, true);
            //File.Copy(currentPath + '\\' + IMPORTANT_LIB, MB_STUDIO_UPDATER_TEMP + '\\' + IMPORTANT_LIB, true);
            File.WriteAllText(MB_STUDIO_UPDATER_TEMP + "\\path.info", currentPath);

            string fullTempPath = Path.GetFullPath(MB_STUDIO_UPDATER_TEMP);
            updater.StartInfo.FileName = fullTempPath + '\\' + MB_STUDIO_UPDATER;
            updater.StartInfo.WorkingDirectory = fullTempPath;
            updater.StartInfo.Arguments = "-gui -su";
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

            Console.WriteLine("Done");
            Console.Write("Downloading new updater version...");

            string downloadedFile = currentPath + "\\" + Application.ProductName + ".tmp";
            using (WebClient client = new WebClient())
                client.DownloadFile("https://www.dropbox.com/s/" + downloadPart + "/MB%20Studio%20Updater.exe?dl=1", downloadedFile);

            Console.WriteLine("Done");
            Console.Write("Replacing old with new version...");

            string backupFile = updatedUpdaterPath + ".bak";
            File.Replace(downloadedFile, updatedUpdaterPath, backupFile);//maybe copy later

            Console.WriteLine("Done");

            updater.StartInfo.FileName = updatedUpdaterPath;
            updater.StartInfo.WorkingDirectory = fullRootPath;
            updater.StartInfo.Arguments = "-gui -" + Channel + " . -startOE";//maybe change arguments later if needed
        }

        private void RemoveUnknownBinaries(List<string> unknownFiles)
        {
            List<string> delFileExtensions = new List<string>() {
                ".dll", ".exe", ".bat", ".vbs"//, ...
            };

            foreach (string unknownFile in unknownFiles)
            {
                try
                {
                    string ext = unknownFile.Substring(unknownFile.LastIndexOf('.'));
                    if (delFileExtensions.Contains(ext))
                        Console.WriteLine(unknownFile);// File.Delete(unknownFile);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
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
                Console.Write("Deleting temporary files...");

                Directory.Delete(MB_STUDIO_UPDATER_TEMP, true);

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

            Console.Write(Environment.NewLine + "Indexing local files...");

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

            Console.WriteLine("Done");
            Console.Write("Downloading current index file...");

            using (WebClient client = new WebClient())
                client.DownloadFile("https://www.dropbox.com/s/" + pathExtra + ".index.mbi?dl=1", "index.mbi");

            Console.WriteLine("Done" + Environment.NewLine);
        }

        private static bool UnusedFile(FileInfo file)
        {
            List<string> unusedFileExtensions = new List<string>() {
                ".pdb",
                ".mbi",
                ".config",
                ".path",
                ".enabled",
                ".dat",
            };

            List<string> unusedDirectories = new List<string>() {
                Path.GetFullPath(@".\Projects"),
                Path.GetFullPath(@".\Python"),
            };

            bool unused = unusedFileExtensions.Contains(file.Extension);
            if (!unused)
            {
                foreach (string unusedDir in unusedDirectories)
                    if (file.DirectoryName.Contains(unusedDir))
                        unused = true;
            }
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
            if (Forced32Bit)
                is64Bit = false;
            return is64Bit;
        }

        #endregion
    }
}
