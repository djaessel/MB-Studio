using System.Collections.Generic;
using System.IO;
using importantLib;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Music : Skriptum
    {
        private ulong flagsGZ, continueFlagsGZ;
        private string trackFile, flags, continueFlags;

        private static HeaderVariable[] headerVariables = null;

        public Music(string[] raw_data) : base(raw_data[0], ObjectType.MUSIC)
        {
            if (headerVariables == null)
                InitializeHeaderVariables();

            trackFile = raw_data[1];

            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                flagsGZ = ulong.Parse(raw_data[2]);
                flags = SetFlags(flagsGZ);
            }
            else
            {
                flags = raw_data[2];
                flagsGZ = SetFlagsGZ(flags);
            }

            if (ImportantMethods.IsNumericGZ(raw_data[3]))
            {
                continueFlagsGZ = ulong.Parse(raw_data[3]);
                continueFlags = SetFlags(continueFlagsGZ);
            }
            else
            {
                continueFlags = raw_data[3];
                continueFlagsGZ = SetFlagsGZ(continueFlags);
            }
        }

        private void InitializeHeaderVariables(string file = "header_music.py", List<HeaderVariable> listX = null)
        {
            const string file2 = "header_mb_decompiler.py";
            List<HeaderVariable> list;

            if (listX == null)
                list = new List<HeaderVariable>();
            else
                list = listX;

            if (!file.Equals(file2))
                file = "moduleSystem\\" + file;

            if (File.Exists(SkillHunter.FilesPath + file))
            {
                using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + file))
                {
                    string s;
                    string[] sp;
                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine().Split('#')[0];
                        if (s.StartsWith("mtf_"))
                        {
                            sp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');
                            if (sp[1].StartsWith("0x"))
                            {
                                s = sp[1].Substring(2);
                                list = RemoveHeaderVariableListEquals(list, s);
                                list.Add(new HeaderVariable(s, sp[0]));
                            }
                        }
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
            {
                list.RemoveAt(x);
            }
            return list;
        }

        private string SetFlags(ulong flagsGZ)
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

            return flags;
        }

        private ulong SetFlagsGZ(string flags)
        {
            ulong flagsGZ = 0;
            string[] tmp = flags.Split('|');

            foreach (string flag in tmp)
                foreach (HeaderVariable var in headerVariables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());

            return flagsGZ;
        }

        public string TrackFile { get { return trackFile; } }//change to Name if possible

        public string TrackFlags { get { return flags; } }

        public ulong TrackFlagsGZ { get { return flagsGZ; } }

        public string ContinueTrackFlags { get { return continueFlags; } }

        public ulong ContinueTrackFlagsGZ { get { return continueFlagsGZ; } }

    }
}