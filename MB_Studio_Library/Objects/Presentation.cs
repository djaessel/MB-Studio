namespace MB_Studio_Library.Objects
{
    public class Presentation : Skriptum
    {
        public Presentation(string name, ulong flags, int mesh_id, int triggerCount) : base(name, ObjectType.Presentation)
        {
            Flags = flags;
            MeshID = mesh_id;
            SimpleTriggers = new SimpleTrigger[triggerCount];
        }

        public void addSimpleTriggerToFreeIndex(SimpleTrigger simpleTrigger, int index = -1)
        {
            if (index < 0)
            {
                for (int i = 0; i < SimpleTriggers.Length; i++)
                {
                    if (SimpleTriggers[i] == null)
                    {
                        SimpleTriggers[i] = simpleTrigger;
                        i = SimpleTriggers.Length;
                    }
                }
            }
            else
            {
                if (index < SimpleTriggers.Length)
                    SimpleTriggers[index] = simpleTrigger;
            }
        }

        public ulong Flags { get; }

        public int MeshID { get; }

        public SimpleTrigger[] SimpleTriggers { get; }

    }
}