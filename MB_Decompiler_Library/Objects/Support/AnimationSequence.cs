using System;
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

            duration = double.Parse(CodeReader.Repl_DotWComma(raw_data[0]));
            resourceName = raw_data[1];
            beginFrame = int.Parse(raw_data[2]);
            endFrame = int.Parse(raw_data[3]);
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
            
            lastNumberINT = ulong.Parse(raw_data[5]);
            string tmp;
            for (int i = 0; i < lastNumbersDOUBLE.Length; i++)
            {
                tmp = CodeReader.Repl_DotWComma(raw_data[i + 6]);
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

        //CHECK IF ALL IF CASES ARE NEEDED IN THIS SCENARIO
        private static void InitializeHeaderVariables(string file = "header_animations.py", List<HeaderVariable> listX = null)
        {
            string file2 = "header_mb_decompiler.py";
            List<HeaderVariable> list;
            if (listX == null)
                list = new List<HeaderVariable>();
            else
                list = listX;

            using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + file))
            {
                string s;
                string[] sp;
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split('#')[0];
                    if (s.StartsWith("arf_"))
                    {
                        sp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');
                        sp[1] = CodeReader.Repl_DotWComma(sp[1]);

                        if (!ImportantMethods.IsNumeric(sp[1], true))
                        {
                            if (sp[1].Contains("+") || sp[1].Contains("-"))
                            {
                                string[] tmp;
                                bool plus = sp[1].Contains("+");

                                if (plus)
                                    tmp = sp[1].Split('+');
                                else
                                    tmp = sp[1].Split('-');

                                for (int j = 0; j < tmp.Length; j++)
                                {
                                    if (!ImportantMethods.IsNumeric(tmp[j], true))
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            if (list[i].VariableName.Equals(tmp[j]))
                                            {
                                                tmp[j] = list[i].VariableValue;
                                                if (tmp[j].StartsWith("0x"))
                                                    tmp[j] = SkillHunter.Hex2Dec_16CHARS(tmp[j].Substring(2)).ToString();
                                                i = list.Count;
                                            }
                                        }
                                    }
                                }

                                if (tmp[0].Contains(","))
                                {
                                    double dx;
                                    double[] d = new double[tmp.Length];
                                    for (int i = 0; i < d.Length; i++)
                                        d[i] = double.Parse(tmp[i]);
                                    dx = d[0];
                                    for (int i = 1; i < d.Length; i++)
                                    {
                                        if (plus)
                                            dx += d[i];
                                        else
                                            dx -= d[i];
                                    }
                                    sp[1] = d.ToString();
                                }
                                else
                                {
                                    ulong ux;
                                    ulong[] u = new ulong[tmp.Length];
                                    for (int i = 0; i < u.Length; i++)
                                        u[i] = ulong.Parse(tmp[i]);
                                    ux = u[0];
                                    for (int i = 1; i < u.Length; i++)
                                    {
                                        if (plus)
                                            ux += u[i];
                                        else
                                            ux -= u[i];
                                    }
                                    sp[1] = u.ToString();
                                }
                            }
                            else if (!sp[1].StartsWith("0x"))
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    if (list[i].VariableName.Equals(sp[1]))
                                    {
                                        sp[1] = list[i].VariableValue;
                                        if (sp[1].StartsWith("0x"))
                                            sp[1] = SkillHunter.Hex2Dec_16CHARS(sp[1].Substring(2)).ToString();
                                        i = list.Count;
                                    }
                                }
                            }
                        }

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
            throw new NotImplementedException();
        }

        private void SetFlags()
        {
            throw new NotImplementedException();
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