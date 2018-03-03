using MB_Decompiler_Library.Objects.Support;
using skillhunter;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects
{
    public class MissionTemplate : Skriptum
    {
        private string flags;
        private string missionType;
        private string description;

        private List<Entrypoint> entryPoints = new List<Entrypoint>();
        private List<Trigger> triggers = new List<Trigger>();

        public MissionTemplate(string[] headerInfo) : base(headerInfo[0], ObjectType.MISSION_TEMPLATE) // base(name, type)
        {
            flags = headerInfo[1];
            missionType = headerInfo[2];
            description = headerInfo[3];
        }

        public void AddEntryPoint(Entrypoint entryPoint) { entryPoints.Add(entryPoint); }

        public void AddTrigger(Trigger trigger) { triggers.Add(trigger); }

        public string[] HeaderInfo { get { return new string[] { ID, flags, missionType, description }; } }

        public string Flags { get { return flags; } }

        public string MissionType { get { return missionType; } }

        public string Description { get { return description; } }

        public Entrypoint[] EntryPoints { get { return entryPoints.ToArray(); } }

        public Trigger[] Triggers { get { return triggers.ToArray(); } }

    }
}
