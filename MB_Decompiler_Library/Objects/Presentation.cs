using System;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Presentation : Skriptum
    {
        private ulong flags;
        private int mesh_id;
        private SimpleTrigger[] simple_triggers;

        public Presentation(string name, ulong flags, int mesh_id, int triggerCount) : base(name, ObjectType.PRESENTATION)
        {
            this.flags = flags;
            this.mesh_id = mesh_id;
            simple_triggers = new SimpleTrigger[triggerCount];
        }

        public void addSimpleTriggerToFreeIndex(SimpleTrigger simpleTrigger, int index = -1)
        {
            if (index < 0)
            {
                for (int i = 0; i < simple_triggers.Length; i++)
                {
                    if (simple_triggers[i] == null)
                    {
                        simple_triggers[i] = simpleTrigger;
                        i = simple_triggers.Length;
                    }
                }
            }
            else
            {
                if (index < simple_triggers.Length)
                    simple_triggers[index] = simpleTrigger;
            }
        }

        public ulong Flags { get { return flags; } }

        public int MeshID { get { return mesh_id; } }

        public SimpleTrigger[] SimpleTriggers { get { return simple_triggers; } }

    }
}