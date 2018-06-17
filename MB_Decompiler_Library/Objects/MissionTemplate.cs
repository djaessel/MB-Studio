using MB_Decompiler_Library.Objects.Support;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects
{
    public class MissionTemplate : Skriptum
    {
        private List<Entrypoint> entryPoints = new List<Entrypoint>();
        private List<Trigger> triggers = new List<Trigger>();

        public MissionTemplate(string[] headerInfo) : base(headerInfo[0], ObjectType.MissionTemplate) // base(name, type)
        {
            Flags = headerInfo[1];
            MissionType = headerInfo[2];
            Description = headerInfo[3];
        }

        public void AddEntryPoint(Entrypoint entryPoint) { entryPoints.Add(entryPoint); }

        public void AddTrigger(Trigger trigger) { triggers.Add(trigger); }

        public string[] HeaderInfo { get { return new string[] { ID, Flags, MissionType, Description }; } }

        public string Flags { get; }

        public string MissionType { get; }

        public string Description { get; }

        public Entrypoint[] EntryPoints { get { return entryPoints.ToArray(); } }

        public Trigger[] Triggers { get { return triggers.ToArray(); } }

    }
}
