using importantLib;
using MB_Decompiler_Library.IO;

namespace MB_Decompiler_Library.Objects
{
    public class Faction : Skriptum
    {
        private static int lastID;

        private readonly int id;

        public Faction(string[] raw_data) : base(raw_data[0].Split()[0].Replace("fac_", string.Empty), ObjectType.FACTION)
        {
            string[] tmp = raw_data[0].Split();
            Name = tmp[1];
            if (ImportantMethods.IsNumericGZ(tmp[2]))
            {
                FlagsGZ = ulong.Parse(tmp[2]);
                SetFlags();
            }
            else
            {
                Flags = tmp[2].Replace('\t', ' ').Replace(" ", string.Empty);
                SetFlagsGZ();
            }

            if (ImportantMethods.IsNumericGZ(tmp[3]))
                ColorCode = HexConverter.Dec2Hex(int.Parse(tmp[3])).Substring(2);
            else if (tmp[3].StartsWith("0x"))
                ColorCode = tmp[3].Substring(2);
            else
                ColorCode = tmp[3];

            lastID++;
            id = lastID;

            string[] relationsString = raw_data[1].Split();
            Relations = new double[relationsString.Length];
            for (int i = 0; i < relationsString.Length; i++)
                Relations[i] = double.Parse(CodeReader.Repl_DotWComma(relationsString[i]));

            tmp = raw_data[2].Split();

            int x = int.Parse(tmp[0]);

            if (x > 0)
            {
                Ranks = new string[x];
                for (int i = 0; i < x; i++)
                    Ranks[i] = tmp[i + 1];
            }
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] sp = Flags.Split('|');

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

            this.FlagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((FlagsGZ & 0x00000001) == 0x00000001)
                flags = "ff_always_hide_label";

            if ((FlagsGZ & 0x0000ff00) != 0)
            {
                byte x = (byte)((FlagsGZ & 0x0000ff00) >> 8);
                x = (byte)(100 - x);
                flags += "|max_player_rating(" + x + ")";
            }

            if (flags.Length == 0)
                flags = FlagsGZ.ToString();
            else
                flags = flags.TrimStart('|');

            this.Flags = flags;
        }

        public static void ResetIDs() { lastID = -1; }

        public string Name { get; }

        public ulong FlagsGZ { get; private set; }

        public string Flags { get; private set; }

        public double FactionCoherence { get { return Relations[id]; } }

        public string ColorCode { get; }

        public double[] Relations { get; }

        public string[] Ranks { get; } = new string[0];
    }
}
