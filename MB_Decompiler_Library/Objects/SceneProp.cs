using System.Collections.Generic;
using System.IO;
using importantLib;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class SceneProp : Skriptum
    {
        private ulong flagsGZ;
        private int hitPoints;
        private int useTime;
        private string meshName, physicsObjectName, flags;
        private SimpleTrigger[] s_triggers = new SimpleTrigger[0];

        private static HeaderVariable[] headerVariables = null;

        public SceneProp(string[] raw_data) : base(raw_data[0].Substring(4), ObjectType.SCENE_PROP)
        {
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

        private void InitializeHeaderVariables(string file = "header_scene_props.py", List<HeaderVariable> listX = null)
        {
            const string file2 = "header_mb_decompiler.py";
            List<HeaderVariable> list;
            if (listX == null)
                list = new List<HeaderVariable>();
            else
                list = listX;
            if (File.Exists(SkillHunter.FilesPath + file))
            {
                using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + file))
                {
                    string s;
                    string[] sp;
                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine().Split('#')[0];
                        if (s.StartsWith("sokf_"))
                        {
                            sp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');
                            if (sp[1].StartsWith("0x"))
                            {
                                s = sp[1].Substring(2);
                                list = RemoveHeaderVariableListEquals(list, s);
                                list.Add(new HeaderVariable(s, sp[0]));
                            }
                        }
                    }
                }
            }

            if (!file2.Equals(file))
                InitializeHeaderVariables(file2, list);
            else
                headerVariables = list.ToArray();
        }

        private static List<HeaderVariable> RemoveHeaderVariableListEquals(List<HeaderVariable> list, string hfValue)
        {
            int x = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].VariableValue.Equals(hfValue))
                {
                    x = i;
                    i = list.Count;
                }
            }
            if (x >= 0)
                list.RemoveAt(x);
            return list;
        }

        private void SetFlagsGZAndHitPoints()
        {
            string[] tmp = flags.Split('|');
            ulong flagsGZ = 0;

            foreach (string flag in tmp)
                foreach (HeaderVariable var in headerVariables)
                    if (var.VariableName.Equals(flag))
                        flagsGZ |= ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());

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
                    {
                        useTime = (int)x;
                        flagsGZ |= ((x & 0xFF) << 28);
                    }
                }
            }

            this.flagsGZ = flagsGZ;
        }

        private void SetFlags()
        {
            ulong x;
            string flags = string.Empty;

            foreach (HeaderVariable var in headerVariables)
            {
                x = ulong.Parse(SkillHunter.Hex2Dec_16CHARS(var.VariableValue).ToString());
                if ((x & flagsGZ) == x)
                    flags += var.VariableName + '|';
            }

            //hitPoints = (int)(flagsGZ >> 20) & byte.MaxValue; //already read
            useTime = (int)(flagsGZ >> 28) & byte.MaxValue;

            if (hitPoints != 0)
                flags += "spr_hit_points(" + hitPoints + ")|";

            if (useTime != 0)
                flags += "spr_use_time(" + useTime + ")|";

            if (flags.Length != 0)
                flags = flags.TrimEnd('|');
            else
                flags = flagsGZ.ToString();

            this.flags = flags;
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