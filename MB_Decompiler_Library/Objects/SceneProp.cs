using System;
using importantLib;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class SceneProp : Skriptum
    {
        private ulong flagsGZ;
        private int hitPoints;
        private string meshName, physicsObjectName, flags;
        private SimpleTrigger[] s_triggers = new SimpleTrigger[0];

        private static HeaderVariable[] headerVariables = null;

        public SceneProp(string[] raw_data) : base(raw_data[0].Substring(4), ObjectType.SCENE_PROP) //    ("barrier_20m",sokf_invisible|sokf_type_barrier,"barrier_20m","bo_barrier_20m", []),
        {                                                                                           //    spr_barrier_20m 16393 0 barrier_20m bo_barrier_20m 0
            if (headerVariables == null)
                InitializeHeaderVariables();
            if (ImportantMethods.IsNumericGZ(raw_data[1]))
            {
                flagsGZ = ulong.Parse(raw_data[1]);
                hitPoints = int.Parse(raw_data[2]);
                meshName = raw_data[3];
                physicsObjectName = raw_data[4];
                SetFlags();
            }
            else
            {
                flags = raw_data[1];
                meshName = raw_data[2];
                physicsObjectName = raw_data[3];
                SetFlagsGZAndHitPoints();
            }
        }

        private void InitializeHeaderVariables()
        {
            throw new NotImplementedException();
        }

        private void SetFlagsGZAndHitPoints()
        {
            #region Comment
/*
spbf_hit_points_mask       = 0x00000000000000FF
spbf_hit_points_bits       = 20
spbf_init_use_time_mask    = 0x00000000000000FF
spbf_use_time_bits         = 28

def spr_hit_points(x):
return ((x & spbf_hit_points_mask) << spbf_hit_points_bits)

def get_spr_hit_points(y):
return (y >> spbf_hit_points_bits) & spbf_hit_points_mask

def spr_use_time(x):
return ((x & spbf_init_use_time_mask) << spbf_use_time_bits)

def get_spr_use_time(y):
return (y >> spbf_use_time_bits) & spbf_init_use_time_mask
*/
            #endregion

            string[] tmp = flags.Split('|');
            ulong flagsGZ = 0;

            foreach (string flag in tmp)
                foreach (HeaderVariable var in headerVariables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue.Substring(2)).ToString());

            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i].Contains("(") && tmp[i].Contains(")"))
                {
                    uint x = uint.Parse(tmp[i].Substring(tmp[i].IndexOf("(") + 1).Split(')')[0]);
                    if (tmp[i].StartsWith("spr_hit_points"))
                    {
                        hitPoints = (int)x;
                        flagsGZ |= ((x & 0xFF) << 20);
                    }
                    else if (tmp[i].StartsWith("spr_use_time"))
                        flagsGZ |= ((x & 0xFF) << 28);
                }
            }

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            throw new NotImplementedException();
        }

        public ulong FlagsGZ { get { return flagsGZ; } }

        public string Flags { get { return flags; } }

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