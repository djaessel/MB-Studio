using System;
using System.IO;

namespace MB_Studio_Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            RunUpdater(args);
        }

        private static void RunUpdater(string[] args)
        {
            bool writeIndex = false;
            MBStudioUpdater updater;

            bool is64Bit = Environment.Is64BitOperatingSystem;
            bool is32Bit = !is64Bit;

            bool selfUpdate = false;
            bool hasArguments = (args.Length != 0);

            if (hasArguments)
                selfUpdate = args[0].Equals("-su");

            if (hasArguments && !selfUpdate)
            {
                string channel = args[0].TrimStart('-');
                string folderPath = ".";

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
                    {
                        writeIndex = args[2].Equals("-index");

                        if (args.Length > 3)
                        {
                            if (args[3].StartsWith("-f-arch=") ||
                                args[3].StartsWith("--force-arch=") ||
                                args[3].StartsWith("--force-architecture="))
                            {
                                string architecture = args[3].Split('=')[1];
                                is32Bit = architecture.Equals("x86");
                                is64Bit = architecture.Equals("x64");
                            }
                        }
                    }
                }

                folderPath = Path.GetFullPath(folderPath);

                updater = new MBStudioUpdater(selfUpdate, is32Bit, channel, folderPath, args[2].Equals("-startOE"));
            }
            else
                updater = new MBStudioUpdater(selfUpdate, is32Bit);

            if (selfUpdate)
                updater.SelfUpdate();
            else if (!writeIndex)
                updater.CheckForUpdates();
            else
                updater.WriteIndexFile();
        }
    }
}
