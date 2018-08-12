using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static MB_Studio_Updater.MBStudioUpdater;

namespace MB_Studio_Updater
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RunUpdater(args);
        }

        // Put this in a handler class later - so no code is in Program
        private static void RunUpdater(string[] args)
        {
            MBStudioUpdater updater = HandleArguments(args);

            if (updater.UseGUI)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GUI(updater));
            }
            else
            {
                if (updater.SelfUpdateActive)
                    updater.SelfUpdate();
                else if (updater.WriteIndexActive)
                    updater.WriteIndexFile();
                else
                    updater.CheckForUpdates();
            }
        }

        private static MBStudioUpdater HandleArguments(string[] args)
        {
            bool useGUI = false;
            bool startOE = false;
            bool writeIndex = false;
            bool selfUpdate = false;
            bool addNewFiles = false;

            bool is64Bit = Environment.Is64BitOperatingSystem;
            bool is32Bit = !is64Bit;

            MBStudioUpdater updater;
            MB_UPDATER_MODE updaterMode = 0;

            bool hasArguments = (args.Length != 0);
            List<string> textArguments = new List<string>();

            if (hasArguments)
            {
                useGUI = args[0].Equals("-gui"); // else -no-gui
                if (args.Length > 1)
                    selfUpdate = args[1].Equals("-su");
            }

            if (hasArguments && !selfUpdate && args.Length > 1)
            {
                string channel = args[1].TrimStart('-');
                string folderPath = ".";

                if (args.Length > 2)
                {
                    folderPath = args[2];
                    if (folderPath.Contains("\"") && folderPath[folderPath.Length - 1] != '\"')
                    {
                        for (int i = 2; i < args.Length; i++)
                        {
                            if (folderPath[folderPath.Length - 1] == '\"')
                                i = args.Length;
                            folderPath += args[i];
                        }
                    }

                    if (args.Length > 3)
                    {
                        writeIndex = args[3].Equals("-index");
                        args[3].Equals("-startOE");

                        if (args.Length > 4)
                        {
                            if (args[4].StartsWith("-f-arch=") ||
                                args[4].StartsWith("--force-arch=") ||
                                args[4].StartsWith("--force-architecture="))
                            {
                                string architecture = args[4].Split('=')[1];
                                is32Bit = architecture.Equals("x86");
                                is64Bit = architecture.Equals("x64");
                            }

                            if (args.Length > 5)
                            {
                                addNewFiles = (args[5].Equals("-anf") || args[5].Equals("--add-new-files"));
                            }
                        }
                    }
                }

                folderPath = Path.GetFullPath(folderPath);

                if (useGUI)
                    updaterMode |= MB_UPDATER_MODE.USE_GUI;

                if (startOE)
                    updaterMode |= MB_UPDATER_MODE.START_STUDIO_ON_EXIT;

                if (writeIndex)
                    updaterMode |= MB_UPDATER_MODE.WRITE_INDEX;

                if (selfUpdate)
                    updaterMode |= MB_UPDATER_MODE.SELF_UPDATE;

                if (addNewFiles)
                    updaterMode |= MB_UPDATER_MODE.ADD_NEW_FILES;

                if (is32Bit)
                    updaterMode |= MB_UPDATER_MODE.FORCE_32_BIT;
            }
            else if (selfUpdate)
                updaterMode |= MB_UPDATER_MODE.SELF_UPDATE;

            updater = new MBStudioUpdater(updaterMode, textArguments);

            return updater;
        }
    }
}
