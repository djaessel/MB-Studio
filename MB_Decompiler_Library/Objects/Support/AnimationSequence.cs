using System.Collections.Generic;
using System.IO;
using importantLib;
using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects.Support
{
    public class AnimationSequence
    {
        private double duration;
        private double[] lastNumbersDOUBLE = new double[4];
        private int beginFrame, endFrame;
        private ulong flagsGZ, lastNumberINT;
        private string resourceName, flags;

        private static HeaderVariable[] headerVariables = null;

        public AnimationSequence(string[] raw_data)
        {
            if (headerVariables == null)
                InitializeHeaderVariables();

            //if (ImportantMethods.IsNumeric(CodeReader.Repl_DotWComma(raw_data[0]), true))
                duration = double.Parse(CodeReader.Repl_DotWComma(raw_data[0]));//change if needed

            resourceName = raw_data[1];

            //if (ImportantMethods.IsNumericGZ(raw_data[2]))
                beginFrame = int.Parse(raw_data[2]);//change if needed

            //if (ImportantMethods.IsNumericGZ(raw_data[3]))
                endFrame = int.Parse(raw_data[3]);//change if needed

            if (ImportantMethods.IsNumericGZ(raw_data[4]))
            {
                flagsGZ = ulong.Parse(raw_data[4]);
                SetFlags();
            }
            else
            {
                flags = raw_data[4];
                SetFlagsGZ();
            }
            
            if (ImportantMethods.IsNumericGZ(raw_data[5]))
                lastNumberINT = ulong.Parse(raw_data[5]);

            for (int i = 0; i < lastNumbersDOUBLE.Length; i++)
            {
                string tmp = CodeReader.Repl_DotWComma(raw_data[i + 6]);
                if (tmp.Length > 3)
                {
                    lastNumbersDOUBLE[i] = double.Parse(tmp);
                    if (tmp.Contains("-") && lastNumbersDOUBLE[i] == 0d)
                        lastNumbersDOUBLE[i] = -0.000001;
                }
                else
                    lastNumbersDOUBLE[i] = double.NaN;
            }
        }

        private static void InitializeHeaderVariables(string file = "header_animations.py", List<HeaderVariable> listX = null)
        {
            string path = SkillHunter.FilesPath;
            string file2 = "header_mb_decompiler.py";
            List<HeaderVariable> list;
            if (listX == null)
                list = new List<HeaderVariable>();
            else
                list = listX;

            if (!File.Exists(path + file))
                path += "moduleSystem\\";

            path += file;

            using (StreamReader sr = new StreamReader(path))
            {
                string s;
                string[] sp;
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split('#')[0];
                    if ((s.StartsWith("arf_") || s.StartsWith("blend_")) && s.Contains("0x"))
                    {
                        sp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');
                        sp[1] = CodeReader.Repl_DotWComma(sp[1]);
                        s = sp[1];
                        list = RemoveHeaderVariableListEquals(list, s);
                        list.Add(new HeaderVariable(s, sp[0]));
                    }
                }
            }

            if (!file2.Equals(file))
                InitializeHeaderVariables(file2, list);
            else
                headerVariables = list.ToArray();
        }

        private static List<HeaderVariable> RemoveHeaderVariableListEquals(List<HeaderVariable> list, string hfValue)
        {
            int x = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].VariableValue.Equals(hfValue))
                {
                    x = i;
                    i = list.Count;
                }
            }
            if (x >= 0)
                list.RemoveAt(x);
            return list;
        }

        private void SetFlagsGZ()
        {
            string[] tmp = flags.Split('|');
            ulong flagsGZ = 0;

            foreach (string flag in tmp)
                foreach (HeaderVariable var in headerVariables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            ulong x;
            string flags = string.Empty;

            foreach (HeaderVariable var in headerVariables)
            {
                x = ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());
                if ((x & flagsGZ) == x)
                    flags += var.VariableName + '|';
            }

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = flagsGZ.ToString();

            this.flags = flags;
        }

        public double Duration { get { return duration; } }

        public int BeginFrame { get { return beginFrame; } }

        public int EndFrame { get { return endFrame; } }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public string ResourceName { get { return resourceName; } }

        public ulong LastNumberGZ { get { return lastNumberINT; } }
        
        public double[] LastNumbersFKZ { get { return lastNumbersDOUBLE; } }

    }
}