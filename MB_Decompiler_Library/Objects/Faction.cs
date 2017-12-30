using System;
using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class Faction : Skriptum
    {
        private static int lastID;

        private int id;
        private ulong flags;
        private double[] relations;
        private string factionName, colorCode;
        private string[] ranks = new string[0];

        public Faction(string[] raw_data) : base(raw_data[0].Substring(4), ObjectType.FACTION)
        {
            factionName = raw_data[1];
            flags = ulong.Parse(raw_data[2]);
            colorCode = SkillHunter.Dec2Hex(int.Parse(raw_data[3])).Substring(2); //FaceFinder.Dec2Hex(int.Parse(raw_data[3]));
            lastID++;
            id = lastID;
        }

        public static void ResetIDs() { lastID = -1; }

        public string FactionName { get { return factionName; } }

        public ulong Flags { get { return flags; } }

        public double FactionCoherence { get { return relations[id]; } }

        public string ColorCode { get { return colorCode; } }

        public double[] Relations {
            get { return relations; }
            set { relations = value; }
        }

        public string[] Ranks
        {
            get { return ranks; }
            set { ranks = value; }
        }

    }
}
