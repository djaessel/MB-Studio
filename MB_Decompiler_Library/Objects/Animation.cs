using System;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Animation : Skriptum
    {
        private ulong flags, masterFlags;
        private AnimationSequence[] sequences = new AnimationSequence[0];

        public Animation(string[] raw_data) : base(raw_data[0], ObjectType.ANIMATION)
        {
            flags = ulong.Parse(raw_data[1]);
            masterFlags = ulong.Parse(raw_data[2]);
        }

        public ulong Flags { get { return flags; } }

        public ulong MasterFlags { get { return masterFlags; } }

        public AnimationSequence[] Sequences
        {
            get { return sequences; }
            set { sequences = value; }
        }

    }
}
