using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects.Support
{
    public class LocalVariable
    {
        private string name;            // gold
        private string localName;       // gold1
        private int internCount;        // 11 (Bei zum Beispiel 12 Variablen)
        private int nameCount = 1;      // 1 (bei zum Beispiel 2 Gold Variablen) --> Variable 12 ist gold2

        private List<int> codeLineIndicies = new List<int>();

        private static List<string> names = new List<string>();
        private static List<int> nameCounts = new List<int>();
        private static List<int> currentCounts = new List<int>();
        private static List<string> defaultVariables = new List<string>();

        public LocalVariable(string name, int internCount, int codeIndex)
        {
            this.name = name;
            this.internCount = internCount;
            currentCounts.Add(internCount);
            codeLineIndicies.Add(codeIndex);
            /*if (internCount < codeLineIndicies.Count)
            {
                codeLineIndicies[internCount].Add(codeIndex);
            }
            else
            {
                List<int> idxs = new List<int>();
                idxs.Add(codeIndex);
                codeLineIndicies.Add(idxs);
            }*/
            if (names.Contains(name))
            {
                int nameIndex = names.IndexOf(name);
                nameCounts[nameIndex]++;
                nameCount = nameCounts[nameIndex];
            }
            else
            {
                names.Add(name);
                nameCounts.Add(nameCount);//1
            }

            localName = name + nameCount;
        }

        public LocalVariable(string localName, int internCount)
        {
            this.localName = localName;
            this.internCount = internCount;
            currentCounts.Add(internCount);
        }

        public void AddCodeIndex(int codeIndex)
        {
            codeLineIndicies.Add(codeIndex);
        }

        public static List<string> CurrentNames { get { return names; } }

        public static List<int> CurrentNameCounts { get { return nameCounts; } }

        public static List<int> CurrentCounts { get { return currentCounts; } }

        public int[] CodeLineIndicies { get { return codeLineIndicies.ToArray(); } }

        public static List<string> DefaultVariables { get { return defaultVariables; } }

        public string LocalName { get { return localName; } }

        public int InternCount { get { return internCount; } }

        public static void AddDefaultVariable(string s)
        {
            defaultVariables.Add(s);
        }

        public static void ResetLists()
        {
            names.Clear();
            nameCounts.Clear();
            currentCounts.Clear();
            defaultVariables.Clear();
        }

    }
}
