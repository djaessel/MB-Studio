using importantLib;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MB_Decompiler_Library.IO
{
    public class CodeWriter
    {
        public static bool IsFinished { get; private set; }
        public static string ModuleSystem { get; private set; }
        public static string DefaultModuleSystemPath { get; private set; }

        //private string sourcePath, destinationPath;

        public const byte DEFAULT_FILES = 4;

        /*public CodeWriter(string sourcePath, string destinationPath)
        {
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            CheckPaths();
        }*/

        public static void CheckPaths()
        {
            if (DefaultModuleSystemPath == null)
                DefaultModuleSystemPath = Path.GetFullPath(CodeReader.FILES_PATH + "\\moduleSystem\\");
            if (ModuleSystem == null)
                ModuleSystem = Path.GetFullPath(CodeReader.ProjectPath + "\\moduleSystem\\");
        }

        public static void WriteAllCode(Control consoleOutput)
        {
            CheckPaths();
            Thread t = new Thread(new ParameterizedThreadStart(WriteCode)) { IsBackground = true };
            t.Start(consoleOutput);
        }

        private static void WriteCode(object param)
        {
            IsFinished = false;

            PrepareAndProcessFiles();

            Control consoleOutput = (Control)param;
            ControlWriter controlTextWriter = new ControlWriter(consoleOutput, consoleOutput.FindForm());

            Form parent = consoleOutput.FindForm();

            Console.SetError(controlTextWriter); //new ControlWriter(consoleOutput, consoleOutput.FindForm())
            Console.SetOut(controlTextWriter);   //new ControlWriter(consoleOutput, consoleOutput.FindForm())

            WriteIDFiles();

            ReadProcessAndBuild();

            IsFinished = true;

            Console.SetOut(Console.Out);
        }

        private static void WriteIDFiles()
        {
            Console.Write("Writing ID files...");

            /*using (StreamWriter wr = new StreamWriter(ModuleSystem + "ID_map_icons.py")) // for all later - just testing right now
                for (int i = 0; i < CodeReader.MapIcons.Length; i++)
                    wr.WriteLine(CodeReader.MapIcons[i] + " = " + i);*/

            int sourceIndex = 0;
            for (int i = 0; i < CodeReader.Elements.Length - 2; i++) // OHNE QUICKSTRINGS UND GLOABLAVARIABLES
            {
                if (i == 15 || i == 21)
                    sourceIndex++;
                else if (i == 7)
                    sourceIndex += 2;
                using (StreamWriter wr = new StreamWriter(GetIDFileNameByIndex(sourceIndex)))
                    for (int j = 0; j < CodeReader.Elements[i].Length; j++)
                        wr.WriteLine(CodeReader.Elements[i][j] + " = " + j);
                sourceIndex++;
            }

            Console.WriteLine("Done!");
        }

        private static string GetIDFileNameByIndex(int index)
        {
            string idFile = ModuleSystem + "ID";
            if (SourceWriter.SOURCES[index].Contains("game_menus"))
                idFile += "_menus.py";
            else if (SourceWriter.SOURCES[index].Contains("postfx"))
                idFile += "_postfx_params.py";
            else
                idFile += SourceWriter.SOURCES[index].Substring(6);
            return idFile;
        }

        private static void ReadProcessAndBuild()
        {
            using (StreamReader sr = new StreamReader(ModuleSystem + "build_module.bat.list"))
            {
                while (!sr.EndOfStream)
                {
                    string[] parameters = sr.ReadLine().Trim().Split();
                    parameters[0] = parameters[0].Replace(".\\", ModuleSystem);
                    parameters[1] = ModuleSystem + parameters[1];

                    ImportantMethods.ExecuteCommandSync("\"\"" + parameters[0] + "\" \"" + parameters[1] + "\"\"", ModuleSystem); // Try to change workingdirectory in importantLib!
                }
            }

            Console.WriteLine("__________________________________________________" + Environment.NewLine
                            + " Finished compiling!" + Environment.NewLine
                            + " Cleaning up...");

            string[] files = Directory.GetFiles(ModuleSystem);
            foreach (string file in files)
            {
                string nameEnd = file.Substring(file.LastIndexOf('.') + 1);
                if (nameEnd.Equals("pyc"))
                    File.Delete(file);
            }

            Console.WriteLine(" Done.");
        }

        private static void PrepareAndProcessFiles()
        {
            string headerFilesPath = CodeReader.ProjectPath + "\\headerFiles";
            string moduleFilesPath = CodeReader.ProjectPath + "\\moduleFiles";

            string[] header_files = Directory.GetFiles(headerFilesPath);
            string[] module_files = Directory.GetFiles(moduleFilesPath);

            if (module_files.Length <= DEFAULT_FILES)
            {
                SourceWriter.WriteAllObjects();
                module_files = Directory.GetFiles(moduleFilesPath);
            }

            foreach (string file in header_files)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            foreach (string file in module_files)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            //string module_info = ModuleSystem + "module_info.py";
            //File.WriteAllText(module_info, File.ReadAllText(module_info).Replace("%MOD_NAME%", objs[1].ToString()));
        }
    }
}
