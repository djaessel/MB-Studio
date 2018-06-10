namespace MB_Decompiler_Library.Objects
{
    public abstract class Skriptum
    {
        public static readonly string[] Prefixes = {"script", "mt", "prsnt", "menu", "trp", "itm", "str", "simple_trigger", "trigger", "ip", "mesh", "track", "qst", "snd", "spr", "tableau", "icon",
                                                    "dialog", "fac", "anim", "pt", "p", "skl", "pfx", "skin", "psys", "scn"};

        public enum ObjectType
        {
            SCRIPT,
            MISSION_TEMPLATE,
            PRESENTATION,
            GAME_MENU,
            TROOP,
            ITEM,
            GAME_STRING,
            SIMPLE_TRIGGER,
            TRIGGER,
            INFO_PAGE,
            MESH,
            MUSIC,
            QUEST,
            SOUND,
            SCENE_PROP,
            TABLEAU_MATERIAL,
            MAP_ICON,
            DIALOG,
            FACTION,
            ANIMATION,
            PARTY_TEMPLATE,
            PARTY,
            SKILL,
            POST_FX,
            SKIN,
            PARTICLE_SYSTEM,
            SCENE,
        }

        public string ID { get; }

        public ObjectType ObjectTyp { get; }

        public int Typ { get { return (int)ObjectTyp; } }

        public string Prefix { get { return Prefixes[Typ] + '_'; } }

        public Skriptum(string sIdName, ObjectType type)
        {
            sIdName = sIdName.Trim();
            if (sIdName.StartsWith(Prefix))
                sIdName = sIdName.Substring(Prefix.Length);

            ID = sIdName;
            ObjectTyp = type;
        }

    }
}
