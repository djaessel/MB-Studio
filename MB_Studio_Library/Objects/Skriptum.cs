namespace MB_Studio_Library.Objects
{
    public abstract class Skriptum
    {
        public const string ADDITIONAL_HEADER = "header_mb_decompiler.py";

        public static readonly string[] Prefixes = {"script", "mt", "prsnt", "menu", "trp", "itm", "str", "simple_trigger", "trigger", "ip", "mesh", "track", "qst", "snd", "spr", "tableau", "icon",
                                                    "dialog", "fac", "anim", "pt", "p", "skl", "pfx", "skin", "psys", "scn"};

        public static readonly string[] CodePrefixes = {"script", "mt", "prsnt", "menu", "trp", "itm", "str", "simple_trigger", "trigger", "ip", "mesh", "track", "qst", "snd", "spr", "tab", "icon",
                                                    "dlga", "fac", "anim", "pt", "p", "skl", "pfx", "skin", "psys", "scn"};

        [System.Flags]
        public enum ObjectType : int
        {
            START,
            Script = START,
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
            Scene,
            END = Scene,
        }

        public string ID { get; }

        public ObjectType ObjectTyp { get; }

        public int Typ { get { return (int)ObjectTyp; } }

        public string Prefix { get { return Prefixes[Typ] + '_'; } }

        public string CodePrefix { get { return CodePrefixes[Typ] + '_'; } }

        public Skriptum(string sIdName, ObjectType type)
        {
            ObjectTyp = type;
            ID = RemovePrefix(RemoveCodePrefix(sIdName.Trim()));
        }

        protected string RemovePrefix(string sIdName)
        {
            if (sIdName.StartsWith(Prefix))
                sIdName = sIdName.Substring(Prefix.Length);
            return sIdName;
        }

        protected string RemoveCodePrefix(string sIdName)
        {
            if (sIdName.StartsWith(CodePrefix))
                sIdName = sIdName.Substring(CodePrefix.Length);
            return sIdName;
        }
    }
}
