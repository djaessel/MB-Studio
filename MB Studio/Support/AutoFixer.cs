using MB_Decompiler;
using MB_Decompiler_Library.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace MB_Studio.Support
{
    public class AutoFixer
    {
        public static void FixAll()
        {
            foreach (string file in CodeReader.Files)
                Fix(file);
        }

        public static void Fix(string fileName)
        {
            List<string[]> orgLines = GetLinesFromFileWithSpaces(ProgramConsole.OriginalModPath + "\\" + fileName);
            List<string[]> genLines = GetLinesFromFileWithSpaces(ProgramConsole.DestinationModPath + "\\" + fileName);
            List<int[]> errorIndices = new List<int[]>();
            int errorCount = 0;

            List<string> logLines = new List<string>();

            for (int i = 0; i < Math.Min(orgLines.Count, genLines.Count); i++)
            {
                List<int> errors = new List<int>();
                string lineLogIDX = "line[" + i + "]";
                logLines.Add(lineLogIDX);
                if (!orgLines[i].Equals(genLines[i]))
                {
                    for (int j = 0; j < Math.Min(orgLines[i].Length, genLines[i].Length); j++)
                    {
                        if (!orgLines[i][j].Equals(genLines[i][j]))
                        {
                            errors.Add(j);
                            int column = 0;
                            for (int x = 0; x < j; x++)
                                column += genLines[i][j].Length + 1;
                            logLines.Add(lineLogIDX + "[" + j + "]: |" + orgLines[i][j] + "| != |" + genLines[i][j] + '|');
                        }
                    }
                }
                //if (errors.Count > 0)
                //{
                errorCount += errors.Count;
                logLines.Add(lineLogIDX + " - Errorcount: " + errors.Count); //Console.WriteLine(lineLogIDX + " - Errorcount: " + errors.Count);
                //}
                errorIndices.Add(errors.ToArray());
            }

            logLines.Add("Errors in " + fileName + ": " + errorCount);
            //Console.WriteLine(logLines[logLines.Count - 1]);

            for (int i = 0; i < errorIndices.Count; i++)
                if (errorIndices[i].Length > 0)
                    if (errorIndices[i].Length != orgLines[i].Length - errorIndices[i][0]
                     && errorIndices[i].Length != genLines[i].Length - errorIndices[i][0])
                        for (int j = 0; j < errorIndices[i].Length; j++)
                            genLines[i][errorIndices[i][j]] = orgLines[i][errorIndices[i][j]];

            logLines.Add(SaveFixedDataToFile(fileName, genLines));

            string logFolder = Path.GetFullPath(".\\Logs");
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);
            using (StreamWriter wr = new StreamWriter(logFolder + "\\" + fileName + "_log.txt"))
                foreach (string line in logLines)
                    wr.WriteLine(line);
        }

        public static string SaveFixedDataToFile(string fileName, List<string[]> fixedGenLines)
        {
            using (StreamWriter wr = new StreamWriter(ProgramConsole.DestinationModPath + "\\" + fileName))
            {
                for (int i = 0; i < fixedGenLines.Count; i++)
                {
                    for (int j = 0; j < fixedGenLines[i].Length; j++)
                    {
                        wr.Write(fixedGenLines[i][j]);
                        if (j < fixedGenLines[i].Length - 1)
                            wr.Write(" ");
                    }
                    wr.WriteLine();
                }
            }
            return "Fixed Data saved!";
        }

        public static List<string[]> GetLinesFromFileWithSpaces(string filePath)
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
    }
}
