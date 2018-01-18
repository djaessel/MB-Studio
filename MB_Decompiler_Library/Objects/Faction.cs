using System;
using importantLib;
using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Faction : Skriptum
    {
        private static int lastID;

        private int id;
        private ulong flagsGZ;
        private double[] relations;
        private string factionName, flags, colorCode;
        private string[] ranks = new string[0];

        public Faction(string[] raw_data) : base(raw_data[0].Split()[0].Replace("fac_", string.Empty), ObjectType.FACTION)
        {
            string[] tmp = raw_data[0].Split();
            factionName = tmp[1];
            if (ImportantMethods.IsNumericGZ(tmp[2]))
            {
                flagsGZ = ulong.Parse(tmp[2]);
                SetFlags();
            }
            else
            {
                flags = tmp[2].Replace('\t', ' ').Replace(" ", string.Empty);
                SetFlagsGZ();
            }

            if (ImportantMethods.IsNumericGZ(tmp[3]))
                colorCode = SkillHunter.Dec2Hex(int.Parse(tmp[3])).Substring(2);
            else if (tmp[3].StartsWith("0x"))
                colorCode = tmp[3].Substring(2);
            else
                colorCode = tmp[3];

            lastID++;
            id = lastID;

            string[] relationsString = raw_data[1].Split();
            relations = new double[relationsString.Length];
            for (int i = 0; i < relationsString.Length; i++)
                relations[i] = double.Parse(CodeReader.Repl_DotWComma(relationsString[i]));

            tmp = raw_data[2].Split();

            int x = int.Parse(tmp[0]);

            if (x > 0)
            {
                ranks = new string[x];
                for (int i = 0; i < x; i++)
                    ranks[i] = tmp[i + 1];
            }
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] sp = flags.Split('|');

            foreach (string flag in sp)
            {
                if (flag.Equals("ff_always_hide_label"))
                    flagsGZ |= 0x00000001;
                else if (flag.StartsWith("max_player_rating("))
                {
                    ulong r = ulong.Parse(flag.Split('(')[1].Split(')')[0].Trim());
                    r = 100 - r;
                    r = (r << 8) & 0x0000ff00;
                    flagsGZ |= r;
                }
            }

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((flagsGZ & 0x00000001) == 0x00000001)
                flags = "ff_always_hide_label";

            if ((flagsGZ & 0x0000ff00) != 0)
            {
                byte x = (byte)((flagsGZ & 0x0000ff00) >> 8);
                x = (byte)(100 - x);
                flags += "|max_player_rating(" + x + ")";
            }

            if (flags.Length == 0)
                flags = flagsGZ.ToString();
            else
                flags = flags.TrimStart('|');

            this.flags = flags;
        }

        public static void ResetIDs() { lastID = -1; }

        public string FactionName { get { return factionName; } }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public double FactionCoherence { get { return relations[id]; } }

        public string ColorCode { get { return colorCode; } }

        public double[] Relations { get { return relations; } }

        public string[] Ranks { get { return ranks; } }

    }
}
