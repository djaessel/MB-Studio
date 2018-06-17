using importantLib;

namespace MB_Decompiler_Library.Objects
{
    public class Skill : Skriptum
    {
        public Skill(string[] raw_data) : base(raw_data[0], ObjectType.Skill)
        {
            Name = raw_data[1].Replace('_', ' ');
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
            MaxLevel = int.Parse(raw_data[3]);
            Description = raw_data[4].Replace('_', ' ');
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] sp = Flags.Split('|');

            foreach (string flag in sp)
            {
                if (flag.Equals("sf_base_att_agi"))
                    flagsGZ |= 0x001;
                else if (flag.Equals("sf_base_att_int"))
                    flagsGZ |= 0x002;
                else if (flag.Equals("sf_base_att_cha"))
                    flagsGZ |= 0x003;
                else if (Flags.Equals("sf_effects_party"))
                    flagsGZ |= 0x010;
                else if (Flags.Equals("sf_inactive"))
                    flagsGZ |= 0x100;
            }

            this.FlagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags;
            byte base_att = (byte)(FlagsGZ & 0x3);

            switch (base_att)
            {
                case 1:
                    flags = "sf_base_att_agi";
                    break;
                case 2:
                    flags = "sf_base_att_int";
                    break;
                case 3:
                    flags = "sf_base_att_cha";
                    break;
                default:
                    flags = "sf_base_att_str";
                    break;
            }

            if ((FlagsGZ & 0x010) == 0x010)
                flags += "|sf_effects_party";

            if ((FlagsGZ & 0x100) == 0x100)
                flags += "|sf_inactive";

            this.Flags = flags.TrimStart('|');
        }

        public string Name { get; }

        public string Description { get; }

        public string Flags { get; private set; }

        public ulong FlagsGZ { get; private set; }

        public int MaxLevel { get; }

    }
}