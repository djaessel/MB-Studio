using importantLib;
using MB_Decompiler_Library.Objects.Support;
using System.Collections.Generic;
using System.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Animation : Skriptum
    {
        private const string SP_NORMAL_FLAGS = "acf_";
        private const string SP_MASTER_FLAGS = "amf_";
        private const string ACF_ANIM_LENGTH = "acf_anim_length(";

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
                FlagsGZ = ulong.Parse(raw_data[1]);
                Flags = SetFlagsX(FlagsGZ, headerVariablesNormal);
                ulong x = (FlagsGZ & 0xff000000);
                if (x != 0)
                {
                    Flags += "|" + ACF_ANIM_LENGTH + (x >> 24) + ")";
                    Flags = Flags.TrimStart('0', '|');
                }
            }
            else
            {
                Flags = raw_data[1];
                FlagsGZ = SetFlagsXGZ(Flags, headerVariablesNormal);
                if (Flags.Contains(ACF_ANIM_LENGTH))
                    FlagsGZ |= ulong.Parse(Flags.Substring(Flags.IndexOf(ACF_ANIM_LENGTH)).Split(')')[0].Trim());
            }

            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                MasterFlagsGZ = ulong.Parse(raw_data[2]);
                MasterFlags = SetFlagsX(MasterFlagsGZ, headerVariablesMaster);
            }
            else
            {
                MasterFlags = raw_data[2];
                MasterFlagsGZ = SetFlagsXGZ(MasterFlags, headerVariablesMaster);
            }
        }

        private static void InitializeHeaderVariables(string searchPattern, string file = "header_animations.py", List<HeaderVariable> listX = null)
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
                    if (s.StartsWith(searchPattern) && s.Contains("0x"))//normal and masterflags
                    {
                        sp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');
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

        private ulong SetFlagsXGZ(string flags, HeaderVariable[] variables)
        {
            ulong flagsGZ = 0;
            string[] tmp = flags.Split('|');
            foreach (string flag in tmp)
                foreach (HeaderVariable var in variables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(HexConverter.Hex2Dec_16CHARS(var.VariableValue).ToString());
            return flagsGZ;
        }

        private string SetFlagsX(ulong flagsGZ, HeaderVariable[] variables)
        {
            ulong x;
            string flags = string.Empty;

            foreach (HeaderVariable var in variables)
            {
                x = ulong.Parse(HexConverter.Hex2Dec_16CHARS(var.VariableValue).ToString());
                if ((x & flagsGZ) == x)
                    flags += var.VariableName + '|';
            }

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = flagsGZ.ToString();

            return flags;
        }

        public ulong FlagsGZ { get; }

        public string Flags { get; }

        public ulong MasterFlagsGZ { get; }

        public string MasterFlags { get; }

        public AnimationSequence[] Sequences { get; set; } = new AnimationSequence[0];
    }
}
