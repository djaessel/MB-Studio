using importantLib;

namespace MB_Studio_Library.Objects
{
    public class Quest : Skriptum
    {
        public Quest(string[] raw_data) : base(raw_data[0], ObjectType.Quest)
        {
            Name = raw_data[1];
            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                FlagsGZ = ulong.Parse(raw_data[2]);
                SetFlags();
            }
            else
            {
                Flags = raw_data[2];
                SetFlagsGZ();
            }
            Description = raw_data[3];
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((FlagsGZ & 0x1) == 1)
                flags += "qf_show_progression";
            if ((FlagsGZ & 0x2) == 2)
                flags += "|qf_random_quest";

            if (flags.Length != 0)
                flags = flags.TrimStart('|');
            else
                flags = FlagsGZ.ToString();

            Flags = flags;
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;

            string[] fl = Flags.Split('|');
            foreach (string flag in fl)
            {
                if (flag.Equals("qf_show_progression"))
                    flagsGZ |= 0x1;
                else if (flag.Equals("qf_random_quest"))
                    flagsGZ |= 0x2;
            }

            FlagsGZ = flagsGZ;
        }

        public bool ShowProgression
        {
            get
            {
                return (FlagsGZ & 0b1) == 0b1;
            }
            set
            {
                if (value)
                    FlagsGZ |= 0b0001;
                else
                    FlagsGZ &= 0b1110;
                SetFlags();
            }
        }

        public bool RandomQuest
        {
            get
            {
                return (FlagsGZ & 0b10) == 0b10;
            }
            set
            {
                if (value)
                    FlagsGZ |= 0b0010;
                else
                    FlagsGZ &= 0b1101;
                SetFlags();
            }
        }

        public ulong FlagsGZ { get; private set; }

        public string Flags { get; private set; }

        public string Name { get; }

        public string Description { get; }

    }
}