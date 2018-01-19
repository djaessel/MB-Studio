using importantLib;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;
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
                flagsGZ = ulong.Parse(raw_data[1]);
                flags = SetFlagsX(headerVariablesNormal);
                ulong x = (flagsGZ & 0xff000000);
                if (x != 0)
                    flags = "|" + ACF_ANIM_LENGTH + (x >> 24) + ")";
                flags = flags.TrimStart('0', '|');
            }
            else
            {
                flags = raw_data[1];
                flagsGZ = SetFlagsXGZ(headerVariablesNormal);
                if (flags.Contains(ACF_ANIM_LENGTH))
                    flagsGZ |= ulong.Parse(flags.Substring(flags.IndexOf(ACF_ANIM_LENGTH)).Split(')')[0].Trim());
            }

            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                masterFlagsGZ = ulong.Parse(raw_data[2]);
                masterFlags = SetFlagsX(headerVariablesMaster);
            }
            else
            {
                masterFlags = raw_data[2];
                masterFlagsGZ = SetFlagsXGZ(headerVariablesMaster);
            }
        }

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

        private ulong SetFlagsXGZ(HeaderVariable[] variables)
        {
            string[] tmp = flags.Split('|');
            ulong flagsGZ = 0;
            foreach (string flag in tmp)
                foreach (HeaderVariable var in variables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());
            return flagsGZ;
        }

        private string SetFlagsX(HeaderVariable[] variables)
        {
            ulong x;
            string flags = string.Empty;
            foreach (HeaderVariable var in variables)
            {
                x = ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());
                if ((x & flagsGZ) == x)
                    flags += var.VariableName + '|';
            }
            return flags;
        }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public ulong MasterFlagsGZ { get { return masterFlagsGZ; } }

        public string MasterFlags { get { return masterFlags; } }

        public AnimationSequence[] Sequences
        {
            get { return sequences; }
            set { sequences = value; }
        }

    }
}
