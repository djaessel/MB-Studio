using importantLib;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;
using System;
using System.Collections.Generic;
using System.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Animation : Skriptum
    {
        private ulong flagsGZ, masterFlagsGZ;
        string flags, masterFlags;
        private AnimationSequence[] sequences = new AnimationSequence[0];

        private const string SP_NORMAL_FLAGS = "acf_";
        private const string SP_MASTER_FLAGS = "amf_";
        private static HeaderVariable[] headerVariablesNormal = null;
        private static HeaderVariable[] headerVariablesMaster = null;

        public Animation(string[] raw_data) : base(raw_data[0], ObjectType.ANIMATION)
        {
            if (headerVariablesNormal == null)
                InitializeHeaderVariables(SP_NORMAL_FLAGS);
            if (headerVariablesMaster == null)
                InitializeHeaderVariables(SP_MASTER_FLAGS);

            if (ImportantMethods.IsNumericGZ(raw_data[1]))
            {
                flagsGZ = ulong.Parse(raw_data[1]);
                SetFlags();
            }
            else
            {
                flags = raw_data[1];
                SetFlagsGZ();
            }
            
            masterFlagsGZ = ulong.Parse(raw_data[2]);
        }

        //CHECK IF ALL IF CASES ARE NEEDED IN THIS SCENARIO
        private static void InitializeHeaderVariables(string searchPattern, string file = "header_animations.py", List<HeaderVariable> listX = null)
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
                    if (s.StartsWith(searchPattern))//normal and masterflags
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
                InitializeHeaderVariables(searchPattern, file2, list);
            else if (searchPattern.Equals(SP_NORMAL_FLAGS))
                headerVariablesNormal = list.ToArray();
            else
                headerVariablesMaster = list.ToArray();
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

        public ulong Flags { get { return flagsGZ; } }

        public ulong MasterFlags { get { return masterFlagsGZ; } }

        public AnimationSequence[] Sequences
        {
            get { return sequences; }
            set { sequences = value; }
        }

    }
}
