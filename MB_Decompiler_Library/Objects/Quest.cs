using System;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Quest : Skriptum
    {
        private ulong flags;
        private string questName, description;

        public Quest(string[] raw_data) : base(raw_data[0], ObjectType.QUEST)
        {
            questName = raw_data[1];
            flags = ulong.Parse(raw_data[2]);
            description = raw_data[3];
        }

        public ulong Flags { get { return flags; } }

        public string QuestName { get { return questName; } }

        public string Description { get { return description; } }

    }
}