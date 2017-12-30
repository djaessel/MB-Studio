using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class SceneProp : Skriptum
    {
        private ulong flags;
        private int hitPoints;
        private string meshName, physicsObjectName;
        private SimpleTrigger[] s_triggers = new SimpleTrigger[0];

        public SceneProp(string[] raw_data) : base(raw_data[0].Substring(4), ObjectType.SCENE_PROP) //    ("barrier_20m",sokf_invisible|sokf_type_barrier,"barrier_20m","bo_barrier_20m", []),
        {                                                                                           //    spr_barrier_20m 16393 0 barrier_20m bo_barrier_20m 0
            flags = ulong.Parse(raw_data[1]);
            hitPoints = int.Parse(raw_data[2]);
            meshName = raw_data[3];
            physicsObjectName = raw_data[4];
        }

        public ulong Flags { get { return flags; } }

        public int HitPoints { get { return hitPoints; } }

        public string MeshName { get { return meshName; } }

        public string PhysicsObjectName { get { return physicsObjectName; } }

        public SimpleTrigger[] SimpleTriggers
        {
            get { return s_triggers; }
            set { s_triggers = value; }
        }

    }
}