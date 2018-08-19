using importantLib;
using MB_Studio_Library.Objects;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using MB_Studio_Library.Objects.Support;

namespace MB_Studio_Library.IO
{
    public class CodeWriter
    {
        public static bool IsFinished { get; private set; }
        public static string ModuleSystem { get; private set; }
        public static string DefaultModuleSystemPath { get; private set; }

        private static List<object> lhsOperations = new List<object>();
        private static List<object> globalLhsOperations = new List<object>();

        // GET REAL MODULE VARIABLES IF NEEDED !!!
        private static List<string> reservedVariables = new List<string>();

        //private string sourcePath, destinationPath;

        public const string EOF_TXT = "EOF";

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

        public static void WriteAllCode(Control consoleOutput, string exportDir)
        {
            CheckPaths();
            var paras = new object[] { consoleOutput, exportDir };
            Thread t = new Thread(new ParameterizedThreadStart(WriteCode)) { IsBackground = true };
            t.Start(paras);
        }

        private static void WriteCode(object param)
        {
            IsFinished = false;

            PrepareAndProcessFiles();

            object[] paras = (object[])param;

            RichTextBox consoleOutput = (RichTextBox)paras[0];
            Form parentForm = consoleOutput.FindForm();
            ControlWriter controlTextWriter = new ControlWriter(consoleOutput, parentForm);

            //maybe make separat control for errors
            Console.SetError(controlTextWriter); //new ControlWriter(consoleOutput, consoleOutput.FindForm())
            Console.SetOut(controlTextWriter);   //new ControlWriter(consoleOutput, consoleOutput.FindForm())

            WriteIDFiles();

            ReadProcessAndBuild((string)paras[1]);

            IsFinished = true;

            Console.SetError(Console.Error);
            Console.SetOut(Console.Out);
        }

        private static void WriteIDFiles()
        {
            Console.Write("Writing ID files");

            int sourceIndex = 0;
            int countFiles = CodeReader.Elements.Length - 2;
            for (int i = 0; i < countFiles; i++) // OHNE QUICKSTRINGS UND GLOABLAVARIABLES
            {
                if (i == 7 || i == 15 || i == 21)// CHECK IF ALL CORRECT!!!
                {
                    sourceIndex++;
                    if (i == 7)
                        sourceIndex++;
                    Console.Write('.');
                }

                using (StreamWriter wr = new StreamWriter(GetIDFileNameByIndex(sourceIndex)))
                    for (int j = 0; j < CodeReader.Elements[i].Count; j++)
                        wr.WriteLine(CodeReader.Elements[i][j] + " = " + j);

                sourceIndex++;
            }

            Console.WriteLine("Done");
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

        private static void ReadProcessAndBuild(string exportDir)
        {
            /*using (StreamReader sr = new StreamReader(ModuleSystem + "build_module.bat.list"))
            {
                while (!sr.EndOfStream)
                {
                    string[] parameters = sr.ReadLine().Trim().Split();
                    parameters[0] = parameters[0].Replace(".\\", ModuleSystem);
                    parameters[1] = ModuleSystem + parameters[1];

                    //ImportantMethods.ExecuteCommandSync(parameters[0], parameters[1]);
                }
            }*/

            SaveAllCodes(exportDir);

            Console.Write("__________________________________________________" + Environment.NewLine
                        + " Finished compiling!" + Environment.NewLine + Environment.NewLine
                        + " Cleaning up...");

            string[] files = Directory.GetFiles(ModuleSystem);
            foreach (string file in files)
            {
                string nameEnd = file.Substring(file.LastIndexOf('.') + 1);
                if (nameEnd.Equals("pyc"))
                    File.Delete(file);
            }

            Console.WriteLine("Done" + Environment.NewLine);
        }

        private static void SaveAllCodes(string exportDir)
        {
            /// USE SavePseudoCodeByType code and SourceReader to create code here

            List<List<Skriptum>> allTypes = new List<List<Skriptum>>();
            List<List<string[]>> allTypesCodes = new List<List<string[]>>();

            ProcessInit(exportDir);
            ProcessGlobalVariables(exportDir);

            
            /// USE SavePseudoCodeByType code and SourceReader to create code here
        }

        private static void CompileGlobalVars(string[] statementBlock, List<string> variableList, List<int> variableUses)
        {
            foreach (string statement in statementBlock)
                CompileGlobalVarsInStatement(statement, variableList, variableUses);
        }

        public static bool IsGenericList(object o)
        {
            var oType = o.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }

        private static bool IsLhsOperationForGlobalVars(object opcode)
        {
            return lhsOperations.Contains(opcode) ||
                globalLhsOperations.Contains(opcode);
        }

        private static void CompileGlobalVarsInStatement(object statement, List<string> variableList, List<int> variableUses)
        {
            object opcode = 0;
            if (!IsGenericList(statement) && !statement.GetType().Equals(typeof(string[])))
                opcode = statement;
            else
            {
                // check this part again because list and array are incompatible in C#
                string[] statementA = (string[])statement;
                opcode = statementA[0];
                if (IsLhsOperationForGlobalVars(opcode))
                {
                    if (statementA.Length > 1)
                    {
                        //object param = statementA[1];
                        //if (param.GetType().Equals(typeof(string))) // not necessary if string array/list is proven!
                        if (statementA[1][0] == '$')
                            AddVariable(statementA[1].Substring(1), variableList, variableUses);
                    }
                }
            }
        }

        private static void AddVariable(string variableString, List<string> variableList, List<int> variableUses)
        {
            bool found = false;
            for (int i = 0; i < variableList.Count; i++)
            {
                if (variableString.Equals(variableList[i]))
                {
                    found = true;
                    variableUses[i]--;
                    i = variableList.Count;//break;
                }
            }
            if (!found)
            {
                variableList.Add(variableString);
                variableUses.Add(-1);
            }
        }

        private static void CompileAllGlobalVars(
            List<string> variableList,
            List<int> variableUses,
            List<Trigger> triggers,
            List<Dialog> senctences,
            List<GameMenu> gameMenus,
            List<MissionTemplate> missionTemplates,
            List<SceneProp> sceneProps,
            List<Presentation> presentations,
            List<Script> scripts,
            List<SimpleTrigger> simpleTriggers
        ) {
            List<object> tempList = new List<object>();
            var listType = tempList.GetType(); // not necessary later because is always (generic) IList

            foreach (string variable in reservedVariables)
            {
                try
                {
                    AddVariable(variable, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in variable: " + variable);
                }
            }

            foreach (Trigger trigger in triggers)
            {
                try
                {
                    CompileGlobalVars(trigger.ConditionBlock, variableList, variableUses);
                    CompileGlobalVars(trigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in trigger: " + trigger.ID);//code?
                }
            }

            foreach (SceneProp sceneProp in sceneProps)
            {
                try
                {
                    SimpleTrigger[] spTriggers = sceneProp.SimpleTriggers;
                    foreach (SimpleTrigger spTrigger in spTriggers)
                        CompileGlobalVars(spTrigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in scene prop: " + sceneProp.ID);//code?
                }
            }

            foreach (Dialog dialog in senctences)//code?
            {
                try
                {
                    CompileGlobalVars(dialog.ConditionBlock, variableList, variableUses);
                    CompileGlobalVars(dialog.ConsequenceBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in dialog line: " + dialog.ID);//code?
                }
            }

            foreach (GameMenu gameMenu in gameMenus)
            {
                try
                {
                    CompileGlobalVars(gameMenu.OperationBlock, variableList, variableUses);
                    foreach (GameMenuOption menuOption in gameMenu.MenuOptions)
                    {
                        CompileGlobalVars(menuOption.ConditionBlock, variableList, variableUses);
                        CompileGlobalVars(menuOption.ConsequenceBlock, variableList, variableUses);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in game menu: " + gameMenu.ID);//code?
                }
            }

            foreach (MissionTemplate missionTemplate in missionTemplates)
            {
                try
                {
                    foreach (Trigger trigger in missionTemplate.Triggers)
                    {
                        CompileGlobalVars(trigger.ConditionBlock, variableList, variableUses);
                        CompileGlobalVars(trigger.ConsequencesBlock, variableList, variableUses);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in mission template: " + missionTemplate.ID);//code?
                }
            }
            
            foreach (Presentation presentation in presentations)
            {
                try
                {
                    foreach (SimpleTrigger trigger in presentation.SimpleTriggers)
                        CompileGlobalVars(trigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in presentation: " + presentation.ID);//code?
                }
            }
            
            foreach (Script script in scripts)
            {
                try
                {
                    CompileGlobalVars(script.Code, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in script: " + script.ID);//code?
                }
            }

            foreach (SimpleTrigger simpleTrigger in simpleTriggers)
            {
                try
                {
                    CompileGlobalVars(simpleTrigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in simple trigger: " + simpleTrigger.ID);//code?
                }
            }
        }

        private static void ProcessGlobalVariables(string exportDir)
        {
            Console.WriteLine("Compiling all global variables...");

            List<string> variables = LoadVariables(exportDir, out List<int> variablesUses);

            // SET NULL TO LIST LATER !!!
            CompileAllGlobalVars(variables, variablesUses, null, null, null, null, null, null, null, null);

            SaveVariables(exportDir, variables, variablesUses);
        }

        private static List<string> LoadVariables(string exportDir, out List<int> variableUses)
        {
            List<string> variables = new List<string>();
            variableUses = new List<int>();

            try
            {
                string[] varList = File.ReadAllLines(exportDir + "variables.txt");
                foreach (string v in varList)
                {
                    string vv = v.Trim();
                    if (vv.Length != 0)
                        variables.Add(vv);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("variables.txt not found. Creating new variables.txt file");
            }

            try
            {
                string[] varList = File.ReadAllLines(exportDir + "variable_uses.txt");
                foreach (string v in varList)
                {
                    string vv = v.Trim();
                    if (vv.Length != 0)
                        variableUses.Add(int.Parse(vv));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("variable_uses.txt not found. Creating new variable_uses.txt file");
            }

            return variables;
        }

        private static void TryFileDelete(string exportDir, string fileName, string ext = ".txt")
        {
            try
            {
                File.Delete(exportDir + fileName + ext);
            }
            catch (Exception) { }
        }

        private static void ProcessInit(string exportDir)
        {
            Console.Write("Initializing...");

            TryFileDelete(exportDir, "tag_uses");
            TryFileDelete(exportDir, "quick_strings");
            TryFileDelete(exportDir, "variables");
            TryFileDelete(exportDir, "variable_uses");

            List<string> variables = new List<string>();
            List<int> variableUses = new List<int>();

            try
            {
                string[] varList = File.ReadAllLines(ModuleSystem + "variables.txt");
                foreach (string v in varList)
                {
                    string vv = v.Trim();
                    if (vv.Length != 0)
                    {
                        variables.Add(vv);
                        variableUses.Add(1);
                    }
                }
                SaveVariables(exportDir, variables, variableUses);
            }
            catch (Exception)
            {
                Console.WriteLine("variables.txt not found.Creating new variables.txt file");
            }

            Console.WriteLine("Done");
        }

        private static void SaveVariables(string exportDir, List<string> variables, List<int> variableUses)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "variables.txt"))
                foreach (string variable in variables)
                    writer.WriteLine(variable);
            using (StreamWriter writer = new StreamWriter(exportDir + "variable_uses.txt"))
                foreach (int variableUse in variableUses)
                    writer.WriteLine(variableUse);
        }

        public static void SavePseudoCodeByType(Skriptum type, string[] code)
        {
            bool found = false;
            string id = ":";
            List<List<string>> typesCodes;
            //bool isTroop = (type.ObjectTyp == Skriptum.ObjectType.TROOP);
            string pseudoCodeFile = CodeReader.ProjectPath + "\\pseudoCodes\\" + CodeReader.Files[type.Typ].Split('.')[0] + ".mbpc";
            string directory = Path.GetDirectoryName(pseudoCodeFile);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            //if (!isTroop)
            id += type.ID;
            //else
            //    id += ((Troop)type).ID;

            List<string> typeCode = new List<string> { id };
            for (int i = 0; i < code.Length; i++)
                typeCode.Add(code[i]);

            if (File.Exists(pseudoCodeFile))
            {
                typesCodes = LoadAllPseudoCodeByFile(pseudoCodeFile);
                for (int i = 0; i < typesCodes.Count; i++)
                {
                    string tmp = typesCodes[i][0].Substring(1);
                    bool b;
                    //if (!isTroop)
                    b = tmp.Equals(type.ID);
                    //else
                    //    b = tmp.Equals(((Troop)type).ID);
                    if (b)
                        typesCodes[i] = typeCode;
                    found = b;
                    if (found)
                        i = typesCodes.Count;
                }
            }
            else
                typesCodes = new List<List<string>>();

            if (!found)
                typesCodes.Add(typeCode);

            using (StreamWriter wr = new StreamWriter(pseudoCodeFile))
            {
                foreach (List<string> typeCodeX in typesCodes)
                    foreach (string line in typeCodeX)
                        wr.WriteLine(line);
                wr.Write(EOF_TXT);
            }
        }

        public static List<List<string>> LoadAllPseudoCodeByFile(string pseudoCodeFile)
        {
            List<List<string>> typesCodes = new List<List<string>>();
            if (File.Exists(pseudoCodeFile))
            {
                using (StreamReader sr = new StreamReader(pseudoCodeFile))
                {
                    string line = string.Empty;
                    while (!sr.EndOfStream)
                    {
                        if (IsNewPseudoCode(line))
                        {
                            List<string> typeCodeX = new List<string>();
                            do
                            {
                                typeCodeX.Add(line);
                                line = sr.ReadLine();
                            } while (!IsNewPseudoCode(line) && !line.Equals(EOF_TXT));
                            typesCodes.Add(typeCodeX);
                        }
                        else
                            line = sr.ReadLine();
                    }
                }
            }
            return typesCodes;
        }

        public static List<List<string>> LoadAllPseudoCodeByObjectTypeID(int objectTypeID)
        {
            return LoadAllPseudoCodeByFile(CodeReader.ProjectPath + "\\pseudoCodes\\" + CodeReader.Files[objectTypeID].Split('.')[0] + ".mbpc");
        }

        private static bool IsNewPseudoCode(string s)
        {
            bool b = false;
            if (s.Length > 1)
                if (s.Substring(0, 1).Equals(":") && !s.Contains(" "))
                    b = true;
            return b;
        }

        private static void PrepareAndProcessFiles()
        {
            string headerFilesPath = CodeReader.ProjectPath + "\\headerFiles";
            string moduleFilesPath = CodeReader.ProjectPath + "\\moduleFiles";

            string[] headerFiles = Directory.GetFiles(headerFilesPath);
            string[] moduleFiles;// = Directory.GetFiles(moduleFilesPath);

            //if (moduleFiles.Length <= DEFAULT_FILES)//MAKE OPTION FOR REWRITE LATER
            //{
                SourceWriter.WriteAllObjects();
                moduleFiles = Directory.GetFiles(moduleFilesPath);
            //}

            foreach (string file in headerFiles)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            foreach (string file in moduleFiles)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            //string module_info = ModuleSystem + "module_info.py";
            //File.WriteAllText(module_info, File.ReadAllText(module_info).Replace("%MOD_NAME%", objs[1].ToString()));
        }
    }
}
