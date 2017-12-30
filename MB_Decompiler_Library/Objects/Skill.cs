using System;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Skill : Skriptum
    {
        private string skillName, description;
        private int maxLvl;
        private ulong flags;

        public Skill(string[] raw_data) : base(raw_data[0], ObjectType.SKILL)
        {
            skillName = raw_data[1].Replace('_', ' ');
            flags = ulong.Parse(raw_data[2]);
            maxLvl = int.Parse(raw_data[3]);
            description = raw_data[4].Replace('_', ' ');
        }

        public string SkillName { get { return skillName; } }

        public string Description { get { return description; } }

        public ulong Flags { get { return flags; } }

        public int MaxLevel { get { return maxLvl; } }

    }
}