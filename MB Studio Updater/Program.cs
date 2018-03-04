using System;
using System.IO;
using System.Collections.Generic;

namespace MB_Studio_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"F:\WORKINGAREA\Visual Studio Projects\MB_Decompiler\MB Studio\bin\x86\Release";
            List<FileInfo> files = GetAllFiles(folderPath);
            List<string> list = new List<string>();

            foreach (FileInfo file in files)
            {
                string codeX = MD5Maker.CreateMD5ChecksumString(file.FullName) + "|" + file.LastWriteTimeUtc.ToFileTime() + "|." + file.FullName.Substring(folderPath.Length);
                list.Add(codeX);
                Console.WriteLine("Code: " + codeX + Environment.NewLine);
            }

            File.WriteAllLines(folderPath + "\\index.mbi", list);

            Console.WriteLine("Root Folder: " + folderPath);
            Console.WriteLine("Files: " + files.Count + Environment.NewLine);

            Console.ReadKey();
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
