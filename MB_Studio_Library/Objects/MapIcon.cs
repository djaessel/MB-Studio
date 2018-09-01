using importantLib;
using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects
{
    public class MapIcon : Skriptum
    {
        public MapIcon(string[] raw_data) : base(raw_data[0], ObjectType.MapIcon)
        {
            if (ImportantMethods.IsNumericGZ(raw_data[1]))
            {
                FlagsGZ = int.Parse(raw_data[1]);
                SetFlags();
            }
            else
            {
                Flags = raw_data[1];
                SetFlagsGZ();
            }

            MapIconName = raw_data[2];
            Scale = double.Parse(CodeReader.Repl_DotWComma(raw_data[3]));

            SoundID = int.Parse(raw_data[4]);
            Sound = CodeReader.Sounds[SoundID];

            string x = CodeReader.Repl_DotWComma(raw_data[5]);
            string y = CodeReader.Repl_DotWComma(raw_data[6]);
            string z = CodeReader.Repl_DotWComma(raw_data[7]);
            if (x.Length > 1 && y.Length > 1 && z.Length > 1)
            {
                OffsetX = double.Parse(x);
                OffsetY = double.Parse(y);
                OffsetZ = double.Parse(z);
            }
            else
            {
                OffsetX = double.NaN;
                OffsetY = double.NaN;
                OffsetZ = double.NaN;
            }
        }

        private void SetFlagsGZ()
        {
            int flagsGZ = 0;

            if (Flags.Equals("mcn_no_shadow"))
                flagsGZ++;

            FlagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if (FlagsGZ == 0x1)
                flags = "mcn_no_shadow";

            if (flags.Length == 0)
                flags = FlagsGZ.ToString();

            Flags = flags;
        }

        public SimpleTrigger[] SimpleTriggers { get; set; } = new SimpleTrigger[0];

        public int FlagsGZ { get; private set; }

        public string Flags { get; private set; }

        public double Scale { get; }

        public double OffsetX { get; }

        public double OffsetY { get; }

        public double OffsetZ { get; }

        public string MapIconName { get; }

        public string Sound { get; }

        public int SoundID { get; }

    }
}
