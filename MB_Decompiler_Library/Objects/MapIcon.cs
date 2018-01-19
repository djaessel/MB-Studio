using importantLib;
using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class MapIcon : Skriptum
    {
        private SimpleTrigger[] s_triggers = new SimpleTrigger[0];
        private byte flagsGZ;
        private double scale, offsetX, offsetY, offsetZ;
        private string mapIconName, flags, sound;

        public MapIcon(string[] raw_data) : base(raw_data[0], ObjectType.MAP_ICON)
        {
            if (ImportantMethods.IsNumericGZ(raw_data[1]))
            {
                flagsGZ = byte.Parse(raw_data[1]);
                SetFlags();
            }
            else
            {
                flags = raw_data[1];
                SetFlagsGZ();
            }

            mapIconName = raw_data[2];
            scale = double.Parse(CodeReader.Repl_DotWComma(raw_data[3]));
            sound = CodeReader.Sounds[int.Parse(raw_data[4])];

            string x = CodeReader.Repl_DotWComma(raw_data[5]);
            string y = CodeReader.Repl_DotWComma(raw_data[6]);
            string z = CodeReader.Repl_DotWComma(raw_data[7]);
            if (x.Length > 1 && y.Length > 1 && z.Length > 1)
            {
                offsetX = double.Parse(x);
                offsetY = double.Parse(y);
                offsetZ = double.Parse(z);
            }
            else
            {
                offsetX = double.NaN;
                offsetY = double.NaN;
                offsetZ = double.NaN;
            }
        }

        private void SetFlagsGZ()
        {
            byte flagsGZ = 0;

            if (flags.Equals("mcn_no_shadow"))
                flagsGZ++;

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if (flagsGZ == 0x1)
                flags = "mcn_no_shadow";

            if (flags.Length == 0)
                flags = flagsGZ.ToString();

            this.flags = flags;
        }

        public SimpleTrigger[] SimpleTriggers
        {
            get { return s_triggers; }
            set { s_triggers = value; }
        }

        public byte FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public double Scale { get { return scale; } }

        public double OffsetX { get { return offsetX; } }

        public double OffsetY { get { return offsetY; } }

        public double OffsetZ { get { return offsetZ; } }

        public string MapIconName { get { return mapIconName; } }

        public string Sound { get { return sound; } }

    }
}
