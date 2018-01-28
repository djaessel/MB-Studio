using importantLib;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Quest : Skriptum
    {
        private ulong flagsGZ;
        private string questName, description, flags;

        public Quest(string[] raw_data) : base(raw_data[0], ObjectType.QUEST)
        {
            questName = raw_data[1];
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
            description = raw_data[3];
        }

        private void SetFlags()
        {
            string flags = string.Empty;

            if ((flagsGZ & 0x1) == 1)
                flags += "qf_show_progression";
            if ((flagsGZ & 0x2) == 2)
                flags += "|qf_random_quest";

            if (flags.Length != 0)
                flags = flags.TrimStart('|');
            else
                flags = flagsGZ.ToString();

            this.flags = flags;
        }

        private void SetFlagsGZ()
        {
            ulong flagsGZ = 0;
            string[] fl = flags.Split('|');
            foreach (string flag in fl)
            {
                if (flag.Equals("qf_show_progression"))
                    flagsGZ |= 0x1;
                else if (flag.Equals("qf_random_quest"))
                    flagsGZ |= 0x2;
            }
            this.flagsGZ = flagsGZ;
        }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

        public string QuestName { get { return questName; } }

        public string Description { get { return description; } }

    }
}