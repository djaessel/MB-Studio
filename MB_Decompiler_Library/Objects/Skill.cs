using System;
using importantLib;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Skill : Skriptum
    {
        private int maxLevel;
        private ulong flagsGZ;
        private string skillName, flags, description;

        public Skill(string[] raw_data) : base(raw_data[0], ObjectType.SKILL)
        {
            skillName = raw_data[1].Replace('_', ' ');
            if (ImportantMethods.IsNumericGZ(raw_data[2]))
            {
                flagsGZ = ulong.Parse(raw_data[2]);
                SetFlags();
            }
            else
            {
                flags = raw_data[2];
                SetFlagsGZ();
            }
            maxLevel = int.Parse(raw_data[3]);
            description = raw_data[4].Replace('_', ' ');
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] sp = flags.Split('|');

            foreach (string flag in sp)
            {
                if (flag.Equals("sf_base_att_agi"))
                    flagsGZ |= 0x001;
                else if (flag.Equals("sf_base_att_int"))
                    flagsGZ |= 0x002;
                else if (flag.Equals("sf_base_att_cha"))
                    flagsGZ |= 0x003;
                else if (flags.Equals("sf_effects_party"))
                    flagsGZ |= 0x010;
                else if (flags.Equals("sf_inactive"))
                    flagsGZ |= 0x100;
            }

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            string flags;

            byte base_att = (byte)(flagsGZ & 0x3);

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

            if ((flagsGZ & 0x010) == 0x010)
                flags += "|sf_effects_party";

            if ((flagsGZ & 0x100) == 0x100)
                flags += "|sf_inactive";

            this.flags = flags.TrimStart('|');
        }

        public string SkillName { get { return skillName; } }

        public string Description { get { return description; } }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public int MaxLevel { get { return maxLevel; } }

    }
}