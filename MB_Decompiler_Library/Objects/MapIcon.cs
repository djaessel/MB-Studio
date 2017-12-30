using MB_Decompiler_Library.IO;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class MapIcon : Skriptum
    {
        private SimpleTrigger[] s_triggers = new SimpleTrigger[0];
        private ulong flags;
        private double scale, offsetX, offsetY, offsetZ;
        private string mapIconName, sound;

        public MapIcon(string[] raw_data) : base(raw_data[0], ObjectType.MAP_ICON)
        {
            flags = ulong.Parse(raw_data[1]);
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
                offsetX = 0.0000001337;
                offsetY = offsetX;
                offsetZ = offsetX;
            }
        }

        public SimpleTrigger[] SimpleTriggers
        {
            get { return s_triggers; }
            set { s_triggers = value; }
        }

        public ulong Flags { get { return flags; } }

        public double Scale { get { return scale; } }

        public double OffsetX { get { return offsetX; } }

        public double OffsetY { get { return offsetY; } }

        public double OffsetZ { get { return offsetZ; } }

        public string MapIconName { get { return mapIconName; } }

        public string Sound { get { return sound; } }

    }
}
