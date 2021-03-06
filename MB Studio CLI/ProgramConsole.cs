using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using importantLib;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects;
using MB_Studio_Library.Objects.Support;
using System.Diagnostics;

namespace MB_Studio_CLI
{
    public class ProgramConsole
    {
        public static string OriginalMod { get; private set; }
        public static string DestinationMod { get; private set; }

        private static bool DebugMode = false;
        private static bool IsConsole = true;

        public const string DEFAULT_STEAMPATH = @"path\to\game_directory\MountBlade Warband\Modules\%MOD_NAME%\";

        private static string currentProjectPath;

        private static string ModuleInfoPathFile { get { return Path.GetFullPath(CodeReader.FILES_PATH + "module_info.path"); } }
        //private static string ModuleInfoRealFile { get { return CodeReader.FILES_PATH + "module_info.py"; } }

        static void Main(string[] args)
        {
            SetWindow();
            //CheckFilesAndFolders();
            if (args.Length > 0 || DebugMode)
            {
                for (int i = 0; i < args.Length; i++)
                    args[i] = args[i].TrimStart('-');
                foreach (string arg in args)
                {
                    //if (arg.Equals("console"))
                    //    IsConsole = true;
                    /*else */
                    if (arg.Equals("debug"))
                        DebugMode = true;
                }
                if (IsConsole || DebugMode)
                    ConsoleProgram();
            }
            else
                Process.Start("MB Studio.exe");
        }

        public static bool SaveModuleInfoPath(string modulesDir)
        {
            bool success = false;
            if (modulesDir != null)
            {
                modulesDir = modulesDir.TrimEnd('\\');
                success = Directory.Exists(modulesDir);
                if (success)
                {
                    modulesDir += @"\%MOD_NAME%\";
                    File.WriteAllText(ModuleInfoPathFile, modulesDir);
                }
            }
            return success;
        }

        private static void ConsoleProgram()
        {
            IsConsole = true;
            Initialize(null, false);
            string s;
            do
            {
                Console.Write(Environment.NewLine + "> ");
                s = Console.ReadLine();

                if (s.Equals("rw -all"))
                    Rw_All();
                else if (s.Contains("rw -i "))
                    Rw_Index_Item(s);
                else if (!s.Equals("exit"))
                    PrintErrorWrongParamters();

            } while (!s.Equals("exit")) ;

            Console.Write(Environment.NewLine + "Press ENTER to close the application...");
            Console.ReadLine();//END
        }

        #region OPTIONS

        private static void PrintErrorWrongParamters()
        {
            ShowErrorMsg("ERROR WRONG PARAMETERS!");
        }

        private static void Rw_All()
        {
            //Console.WriteLine(Environment.NewLine + "FILES_READ: " + SourceWriter.WriteAllObjects(CodeReader.ReadAllObjects()));
            PrintCodeReaderInfo();
        }

        private static void Rw_Index_Item(string s)
        {
            string[] sp = s.Split();
            if (sp.Length > 2)
            {
                int typeID = int.Parse(sp[2]);
                if (typeID < CodeReader.Files.Count && typeID >= 0)
                {
                    List<List<Skriptum>> sss = new List<List<Skriptum>>();
                    CodeReader cr = new CodeReader(CodeReader.ModPath + CodeReader.Files[typeID]);
                    sss.Add(cr.ReadObjectType(typeID, true));
                    SourceWriter.WriteAllObjects(sss);
                    PrintCodeReaderInfo();
                    sss.Clear();//
                }
                else
                {
                    Console.Write("INDEX OUT OF RANGE! --> ");
                    if (typeID >= CodeReader.Files.Count)
                        Console.WriteLine("FILE COUNT: " + CodeReader.Files.Count + " is lower than " + typeID);
                    else
                        Console.WriteLine("POSITIVE NUMBERS ONLY!!!");
                }
            }
            else
                PrintErrorWrongParamters();
        }

        public static string[] GetLastOpenedProjectItemList()
        {
            StringCollection tmp = Properties.Settings.Default.lastOpenedProjects;
            string[] tmpX = new string[tmp.Count];
            tmp.CopyTo(tmpX, 0);
            return tmpX;
        }

        public static string[] GetLastOpenedProjectPaths()
        {
            string[] tmp = GetLastOpenedProjectItemList();
            if (tmp.Length != 0)
            {
                string[] tmp2 = new string[tmp.Length];
                for (int i = 0; i < tmp.Length; i++)
                    tmp2[i] = tmp[i].Split('|')[0];
                tmp = tmp2;
            }
            return tmp;
        }

        public static string[] GetLastOpenedProjectNames()
        {
            string[] tmp = GetLastOpenedProjectItemList();
            if (tmp.Length != 0)
            {
                string[] tmp2 = new string[tmp.Length];
                for (int i = 0; i < tmp.Length; i++)
                    tmp2[i] = tmp[i].Split('|')[1];
                tmp = tmp2;
            }
            return tmp;
        }

        public static void AddProjectPathToLastOpened(string projectPath)
        {
            string[] lastProjects = GetLastOpenedProjectItemList();
            StringCollection sc = new StringCollection();
            string newPath = projectPath + '|' + ReadProjectFileInfoInFolder(projectPath)[0];
            if (lastProjects.Length > 0)
                foreach (string s in lastProjects)
                    if (!s.Equals(newPath))
                        sc.Add(s);
            if (!sc.Contains(newPath))
                sc.Add(newPath);
            Properties.Settings.Default.lastOpenedProjects = sc;
            Properties.Settings.Default.Save();
        }

        public static void RemoveProjectPathFromLastOpened(string projectPath)
        {
            string[] lastProjects = GetLastOpenedProjectItemList();
            StringCollection sc = new StringCollection();
            if (lastProjects.Length != 0)
            {
                for (int i = 0; i < lastProjects.Length; i++)
                    if (!lastProjects[i].Split('|')[0].Equals(projectPath))
                        sc.Add(lastProjects[i]);
            }
            Properties.Settings.Default.lastOpenedProjects = sc;
            Properties.Settings.Default.Save();
        }

        public static bool IsInLastOpenedProjectPaths(string projectPath)
        {
            bool found = false;
            string[] lastOpenedProjects = GetLastOpenedProjectPaths();
            for (int i = 0; i < lastOpenedProjects.Length; i++)
            {
                if (lastOpenedProjects[i].Equals(projectPath))
                {
                    found = !found;
                    i = lastOpenedProjects.Length;
                }
            }
            return found;
        }

        private static void PrintCodeReaderInfo()
        {
            Console.WriteLine("OBJECTS READ: " + CodeReader.ObjectsRead);
            Console.WriteLine("OVERFLOW: " + CodeReader.Overflow);
        }

        #endregion

        #region SETUPS

        private static void SetWindow()
        {
            Console.Title = "MB_Studio_CLI";
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.BufferHeight = Console.LargestWindowHeight;
        }

        public static void Initialize(string projectPath = null, bool showErrorMsg = true)
        {
            //if (check)
            //    CheckFilesAndFolders();

            string headerFilesPath = "\\headerFiles";
            string moduleFilesPath = "\\moduleFiles";
            string moduleSystemPath = "\\moduleSystem";

            if (projectPath == null)
            {
                //projectPath = CodeReader.FILES_PATH + @"tmp";
                //if (!Directory.Exists(projectPath))
                //    Directory.CreateDirectory(projectPath);
                //ShowErrorMsg("Pfad des Projekts wurde in " + Path.GetFullPath(projectPath) + " geändert!");
                ShowErrorMsg("No project found!");
                return;
            }

            currentProjectPath = projectPath;

            if (!IsInLastOpenedProjectPaths(projectPath))
                AddProjectPathToLastOpened(projectPath);

            headerFilesPath = projectPath + headerFilesPath;
            moduleFilesPath = projectPath + moduleFilesPath;
            moduleSystemPath = projectPath + moduleSystemPath;

            //if (!Directory.Exists(headerFilesPath))
                Directory.CreateDirectory(headerFilesPath);
            //if (!Directory.Exists(moduleFilesPath))
                Directory.CreateDirectory(moduleFilesPath);
            //if (!Directory.Exists(moduleSystemPath))
                Directory.CreateDirectory(moduleSystemPath);

            CodeReader.ProjectPath = projectPath;
            moduleFilesPath += '\\';
            SourceWriter.ModuleFilesPath = moduleFilesPath;

            if (SetModPath(showErrorMsg))
            {
                CodeReader.LoadAll();
                ImportsManager importsManager = new ImportsManager(CodeReader.FILES_PATH);
                //SourceWriter.MakeBackup = !SourceWriter.MakeBackup; /// --> DEFAULT IS NOW FALSE ///
                SourceWriter.SetImportsForModuleFiles(importsManager.ReadDataBankInfos());
                ConstantsFinder.InitializeConstants(CodeReader.FILES_PATH + "module_constants.py");

                CopyDefaultFiles();
            }
        }

        public static bool SetModPath(bool showErrorMsg)
        {
            bool found = false;
            //string selectedMod = "\\MountBlade Warband\\Modules\\" + Properties.Settings.Default.SelectedMod;
            //string modPath = "/steamapps/common" + selectedMod.Replace('\\', '/') + '/';
            string modPath = @"\steamapps\common\MountBlade Warband\Modules\%MOD_NAME%\";

            string getMIP = GetModuleInfoPath();
            Console.WriteLine(getMIP + ":" + DEFAULT_STEAMPATH);
            if (getMIP.Equals(DEFAULT_STEAMPATH))
            {
                found = SteamSearch.SearchSteamPath(showErrorMsg);
                if (found)
                {
                    modPath = SetModPathWithSteam(modPath);
                    if (modPath != null)
                    {
                        modPath += "\\"; //CodeReader.SetModPath(modPath + '\\');
                        found = true;
                    }
                    else
                        ShowErrorMsg("ERRORCODE: 0x61 - Set Modulespath in " + ModuleInfoPathFile/* + Environment.NewLine + "Current Modpath: " + modPath*/);
                }
            }

            if (!found)
                modPath = GetModPathWithoutSteam(modPath);

            string realPath = null;
            if (modPath != null)
            {
                realPath = modPath.TrimEnd('\\');
                realPath = realPath.Remove(realPath.LastIndexOf('\\'));
            }

            if (realPath != null && Directory.Exists(realPath))
            {
                getMIP = GetModuleInfoPath();
                Console.WriteLine(getMIP + ":" + modPath);
                if (!getMIP.Equals(modPath)) /// TODO: Check if there could be a problem with libraries or Non-Steam installations!
                //{
                    //if (File.Exists(ModuleInfoPathFile)) // Make a backup just to be sure
                    //    File.Copy(ModuleInfoPathFile, ModuleInfoPathFile + ".bak", true);
                    WriteModuleInfoPath(modPath);
                //}

                modPath = GetDestinationModPathFromVariable(modPath);

                //string realText = GetRealModuleInfoText();
                //string newRealText = realText.Split('=')[1].Trim();
                //newRealText = newRealText.Substring(newRealText.IndexOf('\"') + 1).Split('\"')[0];
                //newRealText = realText.Replace(newRealText, modPath.Replace('\\', '/'));

                /*#region DEBUG
                if (DebugMode)
                    ShowErrorMsg("PATH: " + newRealText);
                #endregion*/

                //if (!realText.Equals(newRealText))
                //{
                //if (File.Exists(ModuleInfoRealFile)) // Make a backup just to be sure
                //    File.Copy(ModuleInfoRealFile, ModuleInfoRealFile + ".bak", true);
                //    WriteRealModuleInfo(newRealText);
                //}

                if (!Directory.Exists(modPath.TrimEnd('\\')))
                {
                    SaveNewMods("Native", "Native - Modified"); //SaveNewOriginalMod("Native");
                    ShowErrorMsg("MODPATH: " + modPath + " - NOT FOUND!");
                    modPath = GetDestinationModPathFromVariable(GetModuleInfoPath());
                    ShowErrorMsg("MODPATH reset to: " + modPath);
                }
                else
                    found = true;

                CodeReader.SetModPath(modPath);
            }
            else
                ShowErrorMsg("ERRORCODE: 0x72 - No Modpath set!");

            return found;
        }

        private static string SetModPathWithSteam(string modPath)
        {
            #region DEBUG
            if (DebugMode)
                ShowErrorMsg("DEBUG1: " + modPath + Environment.NewLine + "DEBUG2: " + SteamSearch.SteamPath);
            #endregion

            if (!SteamSearch.HasLibraryFolders)
                modPath = SteamSearch.SteamPath + modPath;
            else
            {
                foreach (string path in SteamSearch.LibraryFolderPaths)
                {
                    if (Directory.Exists(GetOriginalModPathFromVariable(path.TrimEnd('\\') + modPath)))
                    {
                        #region DEBUG
                        if (DebugMode)
                            ShowErrorMsg("DEBUG3X: " + path.TrimEnd('\\') + " + " + modPath);
                        #endregion

                        modPath = path.TrimEnd('\\') + modPath;
                    }
                }
                string tmp = SteamSearch.SteamPath + modPath;
                if (Directory.Exists(GetOriginalModPathFromVariable(tmp)))
                    modPath = tmp;
            }
            
            #region DEBUG
            if (DebugMode)
                ShowErrorMsg("DEBUG4: " + modPath);
            #endregion

            modPath = modPath.Replace('/', '\\').TrimEnd('\\');
            
            #region DEBUG
            if (DebugMode)
                ShowErrorMsg("DEBUG5: " + modPath);
            #endregion

            if (!Directory.Exists(GetOriginalModPathFromVariable(modPath)))
                modPath = null;

            return modPath;
        }

        public static string GetOriginalModPathFromVariable(string path, string variable = "%MOD_NAME%")
        {
            return path.Replace(variable, OriginalMod);
        }

        public static string GetDestinationModPathFromVariable(string path, string variable = "%MOD_NAME%")
        {
            return path.Replace(variable, DestinationMod);
        }

        public static string OriginalModPath { get { return GetModuleInfoPath().Replace("%MOD_NAME%", OriginalMod); } }

        public static string DestinationModPath { get { return GetModuleInfoPath().Replace("%MOD_NAME%", DestinationMod); } }

        //private static string GetPathFromVariable(string path, string mod_name = "Native", string variable = "%MOD_NAME%")

        private static string GetModPathWithoutSteam(string modPath)
        {
            string errorMessage = "ERRORCODE: 0x71 - No Modpath set!" + Environment.NewLine + "Current Modpath: " + modPath;

            if (!GetModuleInfoPath().Equals(DEFAULT_STEAMPATH))
            {
                string path;
                /*using (StreamReader sr = new StreamReader(ModuleInfoRealFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string tmp = sr.ReadLine().Split('#')[0].Replace('/', '\\').Replace("\"", string.Empty);
                        if (tmp.Contains("="))
                            path = tmp.Split('=')[1].Trim(); //CodeReader.SetModPath(tmp.Split('=')[1].Trim());
                    }
                }*/

                path = GetModuleInfoPath();

                if (path != null)
                    modPath = path;
                else
                    ShowErrorMsg(errorMessage);

                //string mod = CodeReader.ModPath.TrimEnd('\\');
                //mod = mod.Substring(mod.LastIndexOf('\\') + 1);
                //if (!mod.Equals(Properties.Settings.Default.SelectedMod))
                //    CodeReader.SetModPath(CodeReader.ModPath.Replace(mod, Properties.Settings.Default.SelectedMod));
            }
            else
                ShowErrorMsg(errorMessage);

            return modPath;
        }

        public static string[] SetMods(string projectPath)
        {
            string[] info = ReadProjectFileInfoInFolder(projectPath);
            OriginalMod = info[2];
            DestinationMod = info[4];
            return info;
        }

        public static void LoadProject(string projectPath, bool showErrorMsg)
        {
            Initialize(SetMods(projectPath)[1], showErrorMsg);
        }

        private static void CheckResetProjectPathToTemp(string projectPath)
        {
            if (projectPath == null)
            {
                projectPath = @".\files\tmp";
                ShowErrorMsg("Projekt-Pfad wurde auf " + Path.GetFullPath(projectPath) + " geändert!");
            }
        }

        private static void SaveNewMods(string originalMod, string destinationMod, string projectPath = null)
        {
            OriginalMod = originalMod;
            DestinationMod = destinationMod;

            CheckResetProjectPathToTemp(projectPath);

            string[] info = ReadProjectFileInfoFromFile(projectPath);
            info[2] = originalMod;
            info[3] = GetOriginalModPathFromVariable(GetModuleInfoPath());
            info[4] = destinationMod;
            info[5] = GetDestinationModPathFromVariable(GetModuleInfoPath());

            SaveProjectFileInfo(projectPath, info);
        }

        public static void SaveNewOriginalMod(string originalMod, string projectPath = null)
        {
            OriginalMod = originalMod; //Properties.Settings.Default.SelectedMod = modName;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            CheckResetProjectPathToTemp(projectPath);

            string[] info = ReadProjectFileInfoFromFile(projectPath);
            info[2] = originalMod;
            info[3] = GetOriginalModPathFromVariable(GetModuleInfoPath());

            SaveProjectFileInfo(projectPath, info);

            Initialize(projectPath, true);
        }

        public static void SaveNewDestinationMod(string destinationMod, string projectPath = null)
        {
            DestinationMod = destinationMod;

            CheckResetProjectPathToTemp(projectPath);

            string[] info = ReadProjectFileInfoFromFile(projectPath);
            info[4] = destinationMod;
            info[5] = GetDestinationModPathFromVariable(GetModuleInfoPath());

            SaveProjectFileInfo(projectPath, info);

            Initialize(projectPath, true);
        }

        public static void SaveProjectFileInfo(string projectPath, string[] info)
        {
            string[] newInfo = new string[]
            {
                "ProjectName:\"" + info[0] + '\"',
                "ProjectPath:\"" + info[1].TrimEnd('\\') + '\"',
                "ModName:\"" + info[2] + '\"',
                "ModPath:\"" + info[3].TrimEnd('\\') + '\"',
                "NewModName:\"" + info[4] + '\"',
                "NewModPath:\"" + info[5].TrimEnd('\\') + '\"',
                "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
                "CopyDefaultVariables:" + info[6],
                // more will come below
            };
            File.WriteAllLines(GetProjectFilePath(projectPath), newInfo);
        }

        public static void SaveProjectFileInfo(string projectPath, string info, int index = 0)
        {
            string[] infos = ReadProjectFileInfoInFolder(projectPath);
            infos[index] = info;
            SaveProjectFileInfo(projectPath, infos);
        }

        public static string[] ReadProjectFileInfoCurrent()
        {
            if (currentProjectPath != null)
                return ReadProjectFileInfoInFolder(currentProjectPath);
            else
                return new string[0];
        }

        public static string[] ReadProjectFileInfoInFolder(string projectPath)
        {
            string projectFile = GetProjectFilePath(projectPath.TrimEnd('\\'));
            return ReadProjectFileInfoFromFile(projectFile);
        }
        
        public static string[] ReadProjectFileInfoFromFile(string projectFile)
        {
            string[] info = new string[7];
            if (File.Exists(projectFile))
            {
                info = File.ReadAllLines(projectFile);
                List<string> list = new List<string>();
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i].Contains(":"))
                    {
                        info[i] = info[i].Substring(info[i].IndexOf(':') + 1);
                        if (info[i].Contains("\""))
                            info[i] = info[i].Substring(1, info[i].Length - 2);
                        list.Add(info[i]);
                    }
                }
                info = list.ToArray();
            }
            return info;
        }

        public static string GetProjectFilePath(string projectPath)
        {
            string projectFilePath = projectPath.TrimEnd('\\') + '\\' + ImportantMethods.GetDirectoryNameOnly(projectPath) + ".mbsp";
            if (!File.Exists(projectFilePath))
            {
                string[] info = Directory.GetFiles(projectPath);
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i].Contains("."))
                    {
                        if (Path.GetFileName(info[i]).Split('.')[1].Equals("mbsp"))
                        {
                            projectFilePath = info[i];
                            i = info.Length;
                        }
                    }
                }
            }
            return projectFilePath;
        }

        /*private static string GetRealModuleInfoText()
        {
            return File.ReadAllText(ModuleInfoRealFile);
        }*/

        public static string GetModuleInfoPath()
        {
            if (!File.Exists(ModuleInfoPathFile))
                File.WriteAllText(ModuleInfoPathFile, DEFAULT_STEAMPATH);
            return File.ReadAllText(ModuleInfoPathFile);
        }

        /*private static void WriteRealModuleInfo(string text)
        {
            File.WriteAllText(ModuleInfoRealFile, text);
        }*/

        private static void WriteModuleInfoPath(string path)
        {
            File.WriteAllText(ModuleInfoPathFile, path);
        }

        /*private static bool ModuleInfoTextContains(string s)
        {
            return GetRealModuleInfoText().Contains(s);
        }*/

        private static void CopyDefaultFiles()
        {
            string path = SourceWriter.ModuleFilesPath;
            Console.WriteLine(path);
            File.WriteAllText(path + "module_info.py", "export_dir = \"" + GetDestinationModPathFromVariable(GetModuleInfoPath()).Replace('\\', '/') + '\"');//File.Copy(ModuleInfoRealFile, path + "module_info.py", true);
            File.Copy(CodeReader.FILES_PATH + "module_constants.py", path + "module_constants.py", true);
            //File.Copy(CodeReader.FILES_PATH + "module_my_mod_set.py", path + "module_my_mod_set.py", true);//probably unused!
            File.Copy(CodeReader.FILES_PATH + "header_mb_decompiler.py", ImportantMethods.GetDirectoryPathOnly(path) + "\\headerFiles\\header_mb_decompiler.py", true);
        }

        #endregion

        #region Error Messages

        private static void ShowErrorMsg(string msg)
        {
            if (!IsConsole)
                System.Windows.Forms.MessageBox.Show(msg);
            else
                Console.WriteLine(msg);
        }

        #endregion
    }
}
