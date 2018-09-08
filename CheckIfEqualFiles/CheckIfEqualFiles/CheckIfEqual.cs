using importantLib;
using MB_Studio_CLI;
using MB_Studio_Library.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace CheckIfEqualFiles
{
    public class CheckIfEqual
    {
        #region Attributes

        private static readonly string[] Modes = new string[] { "Default", "Single File" };
        private static string modulePath, originalModPath, generatedFolderPath, backupPath;

        private const string EXIT = "exit";
        private const string CHANGE_MODULE = "cmod";
        private const string LANGUAGE_INI = "language.ini";

        private const byte MAX_ARGUMENTS = 2;

        protected static bool DeepValidationActive { get; private set; } = false;

        protected static bool DebugMode { get; private set; } = false;

        private static readonly short languageCode = 0; // index for ini - NO REAL LANGUAGE CODE!!!

        private static List<char> languageAcceptLetter = new List<char>();
        private static List<char> languageDeniedLetter = new List<char>();

        #endregion

        /// <summary>
        /// Checks whether the generated and original files of a mod project are equal or not.
        /// <para>If they are different an error occured or there was code which wasn't recognized by the compiler.</para>
        /// <para>Contact the publisher in case of an error or misbehaviour.</para>
        /// </summary>
        /// <param name="args">By default these are the commandline arguments for an application and are handled as such</param>
        public static void RunCheck(string[] args)
        {
            InitializeConsole(args);
            MainAnalyseProgram(Directory.GetDirectories(modulePath));
        }

        private static void InitializeConsole(string[] args)
        {
            if (args.Length != 0 && args.Length <= MAX_ARGUMENTS)
            {
                DebugMode = args[0].Equals("-deep");
                if (args[0].Equals("-debug"))
                    DebugMode = true;
                else if (DebugMode)
                    DeepValidationActive = true;

                if (args.Length > 1)
                    modulePath = args[1].TrimStart('-');
            }
            else
            {
                //modulePath = @"F:\Program Files\Steam\steamapps\common\MountBlade Warband\Modules";
                modulePath = ProgramConsole.GetModuleInfoPath();
                if (modulePath.IndexOf('%') >= 0)
                    modulePath = modulePath.Remove(modulePath.IndexOf('%') - 1);
            }

            Console.WriteLine("ModulePath set to: \"" + modulePath + "\"" + Environment.NewLine);

            Console.Title = "CheckIfEqualFiles";
            Console.SetWindowSize(Console.WindowWidth + 32, Console.WindowHeight + 16);

            backupPath = modulePath + "\\BACKUP-TXT";

            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);

            if (File.Exists(LANGUAGE_INI))
            {
                string[] lines = File.ReadAllLines(LANGUAGE_INI);
                string[] codes;
                for (int i = 0; i < lines.Length; i++)
                {
                    codes = lines[i].Split('=');
                    languageAcceptLetter.Add(codes[1][0]);
                    languageDeniedLetter.Add(codes[1][2]);
                    Console.WriteLine("Language [" + codes[0].ToUpper() + "] loaded!");
                }
                Console.WriteLine(Environment.NewLine + lines.Length + " languages loaded!" + Environment.NewLine);
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "No language file found!");
                Console.WriteLine("Loading default language: [EN]" + Environment.NewLine);
                languageAcceptLetter.Add('Y');
                languageDeniedLetter.Add('N');
            }

            if (!DebugMode)
                Console.Clear();
        }

        private static void MainAnalyseProgram(string[] modDirs, int mode = 0)
        {
            string input;
            do
            {
                Console.WriteLine(Environment.NewLine + "- - - - - - - - - - - - - - - -" + Environment.NewLine + "MODULES:" + Environment.NewLine + "- - - - - - - - - - - - - - - -" + Environment.NewLine);

                for (int i = 0; i < modDirs.Length; i++)
                    Console.WriteLine(i + " - " + modDirs[i].Substring(modDirs[i].LastIndexOf('\\') + 1));

                Console.WriteLine(Environment.NewLine + "- - - - - - - - - - - - - - - -" + Environment.NewLine);

                //modulePath += '\\';

                Console.Write("Original Mod: ");
                input = Console.ReadLine();
                if (ImportantMethods.IsNumericGZ(input))
                {
                    originalModPath = modDirs[int.Parse(input)] + '\\';

                    Console.Write("Genereated Mod: ");
                    input = Console.ReadLine();
                    if (ImportantMethods.IsNumericGZ(input))
                    {
                        generatedFolderPath = modDirs[int.Parse(input)] + '\\';

                        while (!input.Equals(EXIT) && !input.Equals(CHANGE_MODULE))
                        {
                            Console.WriteLine(Environment.NewLine + "Modes:");
                            for (int i = 0; i < Modes.Length; i++)
                                Console.WriteLine(i + " - " + Modes[i]);

                            do
                            {
                                Console.Write(Environment.NewLine + "Mode: ");
                                input = Console.ReadLine();
                                if (!int.TryParse(input, out mode))
                                    mode = 0;
                            } while (!ModeExists(mode) && !input.Equals(EXIT));

                            if (!input.Equals(EXIT) && RequestAcception("Run"))
                            {
                                do { ManageMode(mode); } while (RequestAcception("Run Again"));
                                if (RequestAcception("Change Module"))
                                    input = CHANGE_MODULE;
                                else
                                    input = EXIT;
                            }
                        }
                    }
                }
            } while (!input.Equals(EXIT));
        }

        private static bool RequestAcception(string text, bool question = true)
        {
            bool accepted, defaultAccepted;
            string key, questionMarkText = string.Empty;

            if (question)
                questionMarkText += '?';

            questionMarkText += ' ';

            Console.Write(text + questionMarkText + "[" + languageAcceptLetter[languageCode] + "/" + languageDeniedLetter[languageCode] + "]: ");

            key = Console.ReadLine().ToUpper(); //ReadKey ?

            accepted = key.Equals(languageAcceptLetter[languageCode].ToString());
            defaultAccepted = key.Length == 0;

            return (defaultAccepted || accepted);
        }

        private static bool ModeExists(int mode)
        {
            bool b = (mode >= 0 && mode < Modes.Length);
            if (!b)
                Console.WriteLine("Mode" + mode + " not found!");
            return b;
        }

        private static void ManageMode(int mode)
        {
            Console.WriteLine();
            if (mode == 0)              // DEFAULT
                AnalyseAllTXTFiles();
            else if (mode == 1)         // Single File Mode
                AnalyseSingleFile();
            else
                Console.WriteLine("Mode" + mode + " not found!");
        }

        private static bool FileExists(string fileName) //In Both Modules 
        {
            bool fileExists = fileName != null;
            if (fileExists)
                fileExists = (File.Exists(originalModPath + fileName) && File.Exists(generatedFolderPath + fileName));
            return fileExists;
        }

        private static void AnalyseAllTXTFiles()
        {
            int failCount = 0;
            int matchCount = 0;
            List<string> failFiles = new List<string>();
            List<string> matchFiles = new List<string>();
            foreach (string file in CodeReader.Files)
            {
                if (AnalyseSingleFile(file))
                {
                    matchFiles.Add(file);
                    matchCount++;
                }
                else
                    failFiles.Add(file);
            }
            int filesCount = CodeReader.Files.Count;
            failCount = filesCount - matchCount;
            Console.WriteLine(Environment.NewLine + "Files Analysed: " + filesCount);
            //Console.WriteLine(Environment.NewLine + "Matches: " + matchCount);
            //Console.WriteLine("Fails: " + failCount);

            Console.WriteLine(Environment.NewLine + "Files Matched (" + matchCount + ") [");
            foreach (string file in matchFiles)
                Console.WriteLine("  " + file + ',');
            Console.WriteLine("]" + Environment.NewLine);

            Console.WriteLine(Environment.NewLine + "Files Failed (" + failCount + ") [");
            foreach (string file in failFiles)
                Console.WriteLine("  " + file + ',');
            Console.WriteLine("]" + Environment.NewLine);
        }

        private static bool AnalyseSingleFile(string fileName = null)
        {
            bool fileMatch = FileExists(fileName);
            while (fileName == null || !fileMatch)
            {
                Console.Write("Filename: ");
                fileName = Console.ReadLine();
                if (!fileName.Contains("."))
                    fileName += ".txt";
                string orgFile = originalModPath + fileName;
                string genFile = generatedFolderPath + fileName;
                if (!File.Exists(orgFile) || !File.Exists(genFile))
                {
                    if (!File.Exists(orgFile))
                        ShowErrorFileNotFound(orgFile);
                    if (!File.Exists(genFile))
                        ShowErrorFileNotFound(genFile);
                    fileMatch = false;
                }
                else
                    fileMatch = true;
            }

            Console.WriteLine(Environment.NewLine + "Analyse " + fileName + ':');

            if (fileMatch)
            {
                string orgFile = originalModPath + fileName;
                string genFile = generatedFolderPath + fileName;
                List<string[]> orgLines = GetLinesFromFile(orgFile);
                List<string[]> genLines = GetLinesFromFile(genFile);
                List<int[]> idxs = new List<int[]>();
                fileMatch = AnalyseMethod(ref orgLines, ref genLines, ref idxs);
                //Fix(genFile, ref genLines, ref idxs);
            }

            if (fileMatch)
                Console.WriteLine(fileName + " matched!");
            else
                Console.WriteLine(fileName + " didn't match!");

            return fileMatch;
        }

        private static bool AnalyseMethod(ref List<string[]> orgLines, ref List<string[]> genLines, ref List<int[]> idxs)
        {
            bool fileMatch = true;

            for (int i = 0; i < Math.Min(orgLines.Count, genLines.Count); i++)
            {
                for (int j = 0; j < Math.Min(orgLines[i].Length, genLines[i].Length); j++)
                {
                    if (!orgLines[i][j].Equals(genLines[i][j]))
                    {
                        bool match = false;
                        orgLines[i][j] = CodeReader.Repl_DotWComma(orgLines[i][j]);
                        genLines[i][j] = CodeReader.Repl_DotWComma(genLines[i][j]);
                        if (ImportantMethods.IsNumericGZ(orgLines[i][j]) && ImportantMethods.IsNumericGZ(genLines[i][j]))
                            match = CheckIfIsEqualNumericGZ(ref orgLines, ref genLines, ref idxs, i, j);
                        else if (ImportantMethods.IsNumericFKZ(orgLines[i][j]) && ImportantMethods.IsNumericFKZ(genLines[i][j]))
                            match = (double.Parse(orgLines[i][j]) == double.Parse(genLines[i][j]));
                        else
                            match = CorrectSimilarIDError(ref orgLines, ref genLines, ref idxs, i, j);
                        if (!match)
                        {
                            fileMatch = false;
                            Console.WriteLine("line[" + i + "][" + j + "]\t-->\t<|" + CodeReader.Repl_CommaWDot(orgLines[i][j]) + "| != |" + CodeReader.Repl_CommaWDot(genLines[i][j]) + "|> ");
                        }
                    }
                }
            }

            if (fileMatch)
            {
                string orgCode = GetLineCode(orgLines);
                string genCode = GetLineCode(genLines);

                fileMatch = (orgCode.Length == genCode.Length);

                if (fileMatch && DeepValidationActive)
                    fileMatch = (fileMatch && orgCode.Equals(genCode));
            }

            if (!fileMatch && genLines.Count < 5)
                Console.WriteLine("Warning: Generated file is probably empty!");

            return fileMatch;
        }

        private static string GetLineCode(List<string[]> lineValues)
        {
            string code = string.Empty;
            foreach (string[] line in lineValues)
                foreach (string lineVal in line)
                    code += lineVal;
            code = code.Replace('\t', ' ').Replace(" ", string.Empty);
            return code;
        }

        /*private static void Fix(string filePath, ref List<string[]> genLines, ref List<int[]> idxs)
        {
            // write code here but only replace on give position if possible and not remove any spaces by complete rewrite
            string[] allLines = File.ReadAllLines(filePath);
            for (int i = 0; i < idxs.Count; i++)
            {
                int collumnCount = 0, spacesCount = 0, j = 0, xxx = 0;
                int lineIndex = idxs[i][0];
                int collumnIndex = idxs[i][1];

                string tmp = allLines[lineIndex].Replace("\t", "    ");
                string tmp2 = genLines[lineIndex][collumnIndex].Replace("\t", "    ");

                char[] tmpC = tmp.ToCharArray();

                bool ddd = false;
                for (j = 0; j < tmpC.Length; j++)
                {
                    if (tmpC[j] == ' ')
                    {
                        if (j > 0)
                            collumnCount++;
                        do
                        {
                            spacesCount++;
                            if (j < tmpC.Length - 1)
                                j++;
                            else
                                ddd = true;
                        } while (tmpC[j] == ' ' && !ddd);
                    }
                    if (collumnCount == collumnIndex)
                    {
                        xxx = j;
                        j = tmpC.Length;
                    }
                }

                // Irgendwo hier oder an einer anderen Stelle an der zum Beispiel idxs gefüllt wird, kommt es zu einem Fehler sodass bei jedem Durchauf nur ein Feher behoben wird (pro Datei?)

                //Console.WriteLine("tmp:" + tmp);
                string tmp3 = tmp.Substring(0, xxx);
                //Console.WriteLine("tmp3:" + tmp3);
                tmp = tmp.Substring(tmp3.Length);
                //Console.WriteLine("tmp:" + tmp);
                tmp3 += tmp2;
                //Console.WriteLine("tmp3:" + tmp3);
                if (tmp.Contains(" "))
                    tmp = tmp.Substring(tmp.IndexOf(' '));
                else
                    tmp = string.Empty;
                //Console.WriteLine("tmp:" + tmp);
                tmp3 += tmp;
                //Console.WriteLine("tmp3:" + tmp3);
                allLines[lineIndex] = tmp3;
            }
            if (idxs.Count > 0)
            {
                File.Copy(filePath, backupPath + "\\" + Path.GetFileName(filePath), true);
                File.WriteAllLines(filePath, allLines);
                Console.WriteLine(Path.GetFileName(filePath) + " fixed!");
            }
        }*/

        private static bool CheckIfIsEqualNumericGZ(ref List<string[]> orgLines, ref List<string[]> genLines, ref List<int[]> idxs, int i, int j)
        {
            decimal u1 = decimal.Parse(orgLines[i][j]);
            decimal u2 = decimal.Parse(genLines[i][j]);
            bool match = (u1 == u2);
            if (!match)
            {
                if (u1 < CodeReader.QUICKSTRING_MAX && u1 >= CodeReader.QUICKSTRING_MIN
                    && u2 < CodeReader.QUICKSTRING_MAX && u2 >= CodeReader.QUICKSTRING_MIN)
                {
                    string s1 = File.ReadAllLines(originalModPath + "quick_strings.txt")[(int)(u1 - CodeReader.QUICKSTRING_MIN) + 1].Split()[1];
                    string s2 = File.ReadAllLines(generatedFolderPath + "quick_strings.txt")[(int)(u2 - CodeReader.QUICKSTRING_MIN) + 1].Split()[1];
                    match = s1.Equals(s2);
                }
                else
                    match = CorrectSimilarIDError(ref orgLines, ref genLines, ref idxs, i, j);
            }
            return match;
        }

        private static bool CorrectSimilarIDError(ref List<string[]> orgLines, ref List<string[]> genLines, ref List<int[]> idxs, int i, int j)
        {
            bool match = false;
            int orgX = 0, genX = 0, bothX = 0;
            List<int[]> idxs2 = new List<int[]>();
            for (int f = 0; f < Math.Min(orgLines.Count, genLines.Count); f++)
            {
                for (int g = 0; g < Math.Min(orgLines[f].Length, genLines[f].Length); g++)
                {
                    bool orgB = orgLines[i][j].Equals(orgLines[f][g]);
                    bool genB = genLines[i][j].Equals(genLines[f][g]);
                    if (orgB)
                        orgX++;
                    if (genB)
                        genX++;
                    if (orgB && genB)
                    {
                        idxs2.Add(new int[] { f, g });
                        bothX++;
                    }
                }
            }
            if (orgX >= genX && genX == bothX)
            {
                foreach (int[] idx in idxs2)
                    genLines[idx[0]][idx[1]] = orgLines[idx[0]][idx[1]];
                idxs.Add(new int[] { i, j });
                match = true;
            }
            return match;
        }

        private static List<string[]> GetLinesFromFile(string filePath)
        {
            List<string[]> lines = new List<string[]>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Replace('\t', ' ').Trim();
                    while (line.Contains("  "))
                        line = line.Replace("  ", " ");
                    lines.Add(line.Split());
                }
            }
            return lines;
        }

        private static void ShowErrorFileNotFound(string file)
        {
            Console.WriteLine("FILE NOT FOUND: " + file);
        }

    }
}
