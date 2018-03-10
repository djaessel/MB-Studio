using System;
using System.IO;

namespace MB_Studio_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            RunUpdater(args);
        }

        private static void Init()
        {
            Console.Title = MBStudioUpdater.ConsoleTitle;
        }

        private static void RunUpdater(string[] args)
        {
            bool writeIndex = false;
            MBStudioUpdater updater;

            if (args.Length != 0)
            {
                string channel = args[0].TrimStart('-');
                string folderPath = Path.GetFullPath(".");

                //if (channel.Equals("testing"))
                //    folderPath = @"F:\WORKINGAREA\Visual Studio Projects\MB_Decompiler\MB Studio\bin\x86\Release";//only for Debug!

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

                    if (args.Length > 2)
                        writeIndex = (args[2].Equals("-index")) ? true : false;
                }

                updater = new MBStudioUpdater(channel, folderPath);
            }
            else
                updater = new MBStudioUpdater();

            if (!writeIndex)
                updater.CheckForUpdates();
            else
                updater.WriteIndexFile();
        }
    }
}
