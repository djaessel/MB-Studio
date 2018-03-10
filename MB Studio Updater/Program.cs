using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace MB_Studio_Updater
{
    class Program
    {
        private static bool fileDownloadActive = false;
        private static string consoleTitle = "MB Studio Updater [by JSYS]";
        private static string curFile, channel;

        static void Main(string[] args)
        {
            Console.Title = consoleTitle;

            channel = "stable";
            string folderPath = Path.GetFullPath(".");
            if (args.Length != 0)
            {
                channel = args[0].TrimStart('-');
                if (args.Length > 1)
                {
                    folderPath = args[1];
                    if (folderPath.Contains("\"") && folderPath[folderPath.Length - 1] != '\"')
                    {
                        for (int i = 2; i < args.Length; i++)
                        {
                            if (folderPath[folderPath.Length - 1] == '\"')
                                i = args.Length;
                            folderPath += args[i];
                        }
                    }
                }
            }

            if (channel.Equals("testing"))
                folderPath = @"F:\WORKINGAREA\Visual Studio Projects\MB_Decompiler\MB Studio\bin\x86\Release";//only for Debug!

            consoleTitle += " Channel: " + channel; 
            Console.Title = consoleTitle;

            string pathExtra;
            if (channel.Equals("dev"))
                pathExtra = "3hb1y883a23520v";
            else if (channel.Equals("beta"))
                pathExtra = "h7fh3m5i0pi7zwl";
            else
                pathExtra = "x6fznmxh99b1mgn";

            pathExtra += "/" + channel;

            List<FileInfo> files = GetAllFiles(folderPath);
            List<string> list = new List<string>();

            foreach (FileInfo file in files)
                if (!UnusedFile(file))
                    list.Add(MD5Maker.CreateMD5ChecksumString(file.FullName) + "|" + file.LastWriteTimeUtc.ToFileTime() + "|." + file.FullName.Substring(folderPath.Length));

            if (channel.Equals("testing"))
            {
                File.WriteAllLines("stable.index.mbi", list);
            }
            else
            {
                using (WebClient client = new WebClient())
                    client.DownloadFile("https://www.dropbox.com/s/" + pathExtra + ".index.mbi?dl=1", "index.mbi");

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
                            if (infoList[2].Equals(infoIndex[2]) && ulong.Parse(infoList[1]) < ulong.Parse(infoIndex[1]))//if (outdated)
                            {
                                updateFiles.Add(new string[] { infoIndex[2], infoIndex[3] });
                                i = list.Count;
                            }
                        }
                    }
                }

                Console.WriteLine(Environment.NewLine + " --> Es werden " + updateFiles.Count + " Dateien aktualisiert:" + Environment.NewLine);

                if (channel.Equals("testing2"))
                    folderPath = "E:\\tmp\\testDownload\\";//only for Debug!

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    for (int i = 0; i < updateFiles.Count; i++)
                    {
                        curFile = " - Aktualisiere " + updateFiles[i][0].Substring(2);
                        Console.WriteLine(curFile);
                        fileDownloadActive = true;
                        client.DownloadFileAsync(new Uri("https://www.dropbox.com/s/" + updateFiles[i][1] + "?dl=1"), folderPath + updateFiles[i][0].Substring(1));
                        while (fileDownloadActive) Thread.Sleep(10);
                    }
                }

                Console.Title = consoleTitle + " - Finished Updating";
            }

            //Console.Write(Environment.NewLine + " Press any key to close ...");//only for Debug!
            //Console.ReadKey();//only for Debug!
        }

        private static void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            fileDownloadActive = false;
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Title = consoleTitle + curFile + " Progress: " + e.ProgressPercentage + " %";
        }

        private static bool UnusedFile(FileInfo file)
        {
            string ext = file.Extension.Substring(1);
            bool unused = file.Name.Contains("skillhunter.xml");
            if (!unused)
                unused = (ext.Equals("pdb") || ext.Equals("mbi") || ext.Equals("config"));
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
    }
}
