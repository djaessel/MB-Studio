using System.Collections.Generic;
using System.IO;
using importantLib;
using MB_Studio_Library.Objects.Support;

namespace MB_Studio_Library.Objects
{
    public class Music : Skriptum
    {
        private static HeaderVariable[] headerVariables = null;

        public Music(string[] raw_data) : base(raw_data[0], ObjectType.Music)
        {
            if (headerVariables == null)
                InitializeHeaderVariables();

            TrackFile = raw_data[1];

            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                TrackFlagsGZ = ulong.Parse(raw_data[2]);
                TrackFlags = SetFlags(TrackFlagsGZ);
            }
            else
            {
                TrackFlags = raw_data[2];
                TrackFlagsGZ = SetFlagsGZ(TrackFlags);
            }

            if (ImportantMethods.IsNumericGZ(raw_data[3]))
            {
                ContinueTrackFlagsGZ = ulong.Parse(raw_data[3]);
                ContinueTrackFlags = SetFlags(ContinueTrackFlagsGZ);
            }
            else
            {
                ContinueTrackFlags = raw_data[3];
                ContinueTrackFlagsGZ = SetFlagsGZ(ContinueTrackFlags);
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

        private ulong SetFlagsGZ(string flags)
        {
            ulong flagsGZ = 0;
            string[] tmp = flags.Split('|');

            foreach (string flag in tmp)
                foreach (HeaderVariable var in headerVariables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(HexConverter.Hex2Dec_16CHARS(var.VariableValue).ToString());

            return flagsGZ;
        }

        public string TrackFile { get; }//change to Name if possible

        public string TrackFlags { get; }

        public ulong TrackFlagsGZ { get; }

        public string ContinueTrackFlags { get; }

        public ulong ContinueTrackFlagsGZ { get; }

    }
}