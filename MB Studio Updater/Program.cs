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

            bool selfUpdate = false;
            bool hasArguments = (args.Length != 0);

            if (hasArguments)
                selfUpdate = args[0].Equals("-su");

            System.Console.Write("INFO X " + selfUpdate);
            System.Console.ReadLine();

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
                        writeIndex = (args[2].Equals("-index")) ? true : false;
                }

                folderPath = Path.GetFullPath(folderPath);

                updater = new MBStudioUpdater(channel, folderPath, args[2].Equals("-startOE"));
            }
            else
                updater = new MBStudioUpdater();

            if (selfUpdate)
                updater.SelfUpdate();
            else if (!writeIndex)
                updater.CheckForUpdates();
            else
                updater.WriteIndexFile();
        }
    }
}
