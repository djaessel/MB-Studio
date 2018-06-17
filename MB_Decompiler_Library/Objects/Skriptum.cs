namespace MB_Decompiler_Library.Objects
{
    public abstract class Skriptum
    {
        public static readonly string[] Prefixes = {"script", "mt", "prsnt", "menu", "trp", "itm", "str", "simple_trigger", "trigger", "ip", "mesh", "track", "qst", "snd", "spr", "tableau", "icon",
                                                    "dialog", "fac", "anim", "pt", "p", "skl", "pfx", "skin", "psys", "scn"};

        public enum ObjectType
        {
            Script,
            MissionTemplate,
            Presentation,
            GameMenu,
            Troop,
            Item,
            GameString,
            SimpleTrigger,
            Trigger,
            InfoPage,
            Mesh,
            Music,
            Quest,
            Sound,
            SceneProp,
            TableauMaterial,
            MapIcon,
            Dialog,
            Faction,
            Animation,
            PartyTemplate,
            Party,
            Skill,
            PostFX,
            Skin,
            ParticleSystem,
            Scene
        }

        public string ID { get; }

        public ObjectType ObjectTyp { get; }

        public int Typ { get { return (int)ObjectTyp; } }

        public string Prefix { get { return Prefixes[Typ] + '_'; } }

        public Skriptum(string sIdName, ObjectType type)
        {
            ObjectTyp = type;

            sIdName = sIdName.Trim();
            if (sIdName.StartsWith(Prefix))
                sIdName = sIdName.Substring(Prefix.Length);
            ID = sIdName;
        }

    }
}
