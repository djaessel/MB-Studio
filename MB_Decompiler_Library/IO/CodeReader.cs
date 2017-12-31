using System.IO;
using System.Collections.Generic;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using System;
using importantLib;
using skillhunter;

namespace MB_Decompiler_Library.IO
{
    public class CodeReader
    {
        #region Attributes

        private string filepath;

        private static int objectsRead;
        private static int objectsExpected;

        private static string modPath = string.Empty; //SHOULD BE string.Empty in the Release Version!!!

        //private static List<List<int>> unusedIndicies;//, unusedIndiciesEXTRA;
        private static LocalVariableInterpreter localVariableInterpreter;

        #endregion

        #region References

        private static List<string> codeDeclarations = new List<string>();
        private static List<ulong> codeValue = new List<ulong>();

        #region Elements

        /*private static List<string> globalVariables = new List<string>(); // 0
        private static List<string> scriptNames = new List<string>();       // 1
        private static List<string> quick_strings = new List<string>();     // 2
        private static List<string> presentations = new List<string>();     // 3
        private static List<string> strings = new List<string>();           // 4
        private static List<string> items = new List<string>();             // 5
        private static List<string> factions = new List<string>();          // 6
        private static List<string> troops = new List<string>();            // 7
        private static List<string> scenes = new List<string>();            // 8
        private static List<string> scene_props = new List<string>();       // 9
        private static List<string> parties = new List<string>();           // 10
        private static List<string> party_templates = new List<string>();   // 11
        private static List<string> mission_templates = new List<string>(); // 12
        private static List<string> skills = new List<string>();            // 13
        private static List<string> sounds = new List<string>();            // 14
        private static List<string> particles = new List<string>();         // 15
        private static List<string> menus = new List<string>();             // 16
        private static List<string> quests = new List<string>();            // 17
        private static List<string> tableau_materials = new List<string>(); // 18
        private static List<string> meshes = new List<string>();            // 19
        private static List<string> animations = new List<string>();        // 20
        private static List<string> tracks = new List<string>();            // 21
        private static List<string> mapicons = new List<string>();*/        // 22

        private static List<string[]> elements = new List<string[]>();
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string[][] Elements { get { return elements.ToArray(); } }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string[] Scripts { get { return elements[0]; } }
        public static string[] MissionTemplates { get { return elements[1]; } }
        public static string[] Presentations { get { return elements[2]; } }
        public static string[] GameMenus { get { return elements[3]; } }
        public static string[] Troops { get { return elements[4]; } }
        public static string[] Items { get { return elements[5]; } }
        public static string[] Strings { get { return elements[6]; } }
        //SimpleTriggers    - NONE
        //Triggers          - NONE
        public static string[] InfoPages { get { return elements[7]; } }
        public static string[] Meshes { get { return elements[8]; } }
        public static string[] Tracks { get { return elements[9]; } }
        public static string[] Quests { get { return elements[10]; } }
        public static string[] Sounds { get { return elements[11]; } }
        public static string[] SceneProps { get { return elements[12]; } }
        public static string[] TableauMaterials { get { return elements[13]; } }
        public static string[] MapIcons { get { return elements[14]; } }
        //Dialogs           - NONE
        public static string[] Factions { get { return elements[15]; } }
        public static string[] Animations { get { return elements[16]; } }
        public static string[] PartyTemplates { get { return elements[17]; } }
        public static string[] Parties { get { return elements[18]; } }
        public static string[] Skills { get { return elements[19]; } }
        public static string[] PostFXParams { get { return elements[20]; } }
        //Skins             - NONE
        public static string[] ParticleSystems { get { return elements[21]; } }
        public static string[] Scenes { get { return elements[22]; } }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static string[] QuickStrings { get { return elements[23]; } } // Always last if possible
        public static string[] GlobalVariables { get { return elements[24]; } } // Always last if possible

        #endregion

        public static string ModPath { get { return modPath; } }

        /*public static string ModName {
            get
            {
                string modName = modPath.TrimEnd('\\');
                modName = modName.Substring(modName.LastIndexOf('\\') + 1);
                return modName;
            }
        }*/

        public const string FILES_PATH = @".\files\";

        public static string ProjectPath { get; set; }

        public static int ObjectsRead { get { return objectsRead; } }

        #endregion

        #region Konstanten

        public const string MINUS_ZERO = "-0,0000001337";
        public const string MINUS_ZERO_ALT = "-1.337E-07";
        public const string MINUS = "-";

        // ÄNDERN ?!
        public static readonly string[] Files = { "scripts.txt", "mission_templates.txt", "presentations.txt", "menus.txt", "troops.txt", "item_kinds1.txt", "strings.txt", "simple_triggers.txt",
                "triggers.txt", "info_pages.txt", "meshes.txt", "music.txt", "quests.txt", "sounds.txt", "scene_props.txt", "tableau_materials.txt", "map_icons.txt", "conversation.txt",
                "factions.txt", "actions.txt", "party_templates.txt", "parties.txt", "skills.txt", "postfx.txt", "skins.txt", "particle_systems.txt", "scenes.txt" };

        private static string[] UNUSED_CODES = { "else_try_begin", "end_try" };

        public const ulong LOCAL_MIN = 1224979098644774912;
        public const ulong LOCAL_MAX = 1224979098644775040; // max. 128 local variables

        public const ulong REG0 = 72057594037927936;
        public const ulong REG127 = 72057594037928063;

        public const ulong QUICKSTRING_MIN = 1585267068834414592;
        public const ulong QUICKSTRING_MAX = 1600000000000000000;

        private const ulong TRP_PLAYER = 360287970189639680;
        private const ulong TROOP_MAX = 370000000000000000;

        private const ulong GLOBAL_MIN = 144115188075855872;
        private const ulong GLOBAL_MAX = 145000000000000000;

        private const ulong SCRIPT_MIN = 936748722493063168;
        private const ulong SCRIPT_MAX = 940000000000000000;

        private const ulong STRING_MIN = 216172782113783808;
        private const ulong STRING_MAX = 217000000000000000;

        private const ulong SPR_MIN = 1080863910568919040;
        private const ulong SPR_MAX = 1100000000000000000;

        private const ulong PRSNT_MIN = 1513209474796486656;
        private const ulong PRSNT_MAX = 1513210000000000000;

        private const ulong FAC_MIN = 432345564227567616;
        private const ulong FAC_MAX = 433000000000000000;

        private const ulong P_MAIN_PARTY = 648518346341351424;
        private const ulong P_MAX = 648600000000000000;

        private const ulong ITM_MIN = 288230376151711744;
        private const ulong ITM_MAX = 290000000000000000;

        private const ulong SCENE_MIN = 720575940379279360;
        private const ulong SCENE_MAX = 720575940380000000;

        private const ulong MESH_MIN = 1441151880758558720;
        private const ulong MESH_MAX = 1450000000000000000;

        private const ulong PT_MIN = 576460752303423488;
        private const ulong PT_MAX = 576500000000000000;

        private const ulong MT_MIN = 792633534417207296;
        private const ulong MT_MAX = 792700000000000000;

        private const ulong SKL_MIN = 1369094286720630784;
        private const ulong SKL_MAX = 1369094286720700000;

        private const ulong SND_MIN = 1152921504606846976;
        private const ulong SND_MAX = 1152921504607000000;

        private const ulong PSYS_MIN = 1008806316530991104;
        private const ulong PSYS_MAX = 1009000000000000000;

        private const ulong MENU_MIN = 864691128455135232;
        private const ulong MENU_MAX = 865000000000000000;

        private const ulong QUEST_MIN = 504403158265495552;
        private const ulong QUEST_MAX = 504500000000000000;

        private const ulong TABLEAU_MAT_MIN = 1729382256910270464;
        private const ulong TABLEAU_MAT_MAX = 1730000000000000000;

        private const ulong ANIM_MIN = 1801439850948198400;
        private const ulong ANIM_MAX = 1810000000000000000; //private const ulong ANIM_MAX = ulong.MaxValue - int.MaxValue;

        private const ulong TRACK_MIN = 1657324662872342528;
        private const ulong TRACK_MAX = 1660000000000000000;

        #endregion

        public CodeReader(string filepath = null) { this.filepath = filepath; }

        #region Read References

        public static void LoadAll()
        {
            Reset();

            ReadAndSetDeclarations();

            elements.Add(ReadScriptNames());
            elements.Add(ReadMissionTemplates());
            elements.Add(ReadPresentations());
            elements.Add(ReadGameMenus());
            elements.Add(ReadTroops());
            elements.Add(ReadItems());
            elements.Add(ReadStrings());
            elements.Add(ReadInfoPages());
            elements.Add(ReadMeshes());
            elements.Add(ReadTracks());
            elements.Add(ReadQuests());
            elements.Add(ReadSounds());
            elements.Add(ReadSceneProps());
            elements.Add(ReadTableauMaterials());
            elements.Add(ReadMapIcons());
            elements.Add(ReadFactions());
            elements.Add(ReadAnimations());
            elements.Add(ReadPartyTemplates());
            elements.Add(ReadParties());
            elements.Add(ReadSkills());
            elements.Add(ReadPostFXParams());
            elements.Add(ReadParticles());
            elements.Add(ReadScenes());

            elements.Add(ReadQuickStrings());
            elements.Add(ReadGlobalVariables());
        }

        public static int GetItemIndexFromID(string id)
        {
            return GetTypeIndexFromID(Items, id);
        }

        public static int GetTypeIndexFromID(string[] typeList, string id)
        {
            int index = -1;
            for (int i = 0; i < typeList.Length; i++)
            {
                if (typeList[i].Equals(id))
                {
                    index = i;
                    i = typeList.Length;
                }
            }
            return index;
        }

        private static void ReadAndSetDeclarations()
        {
            int x;
            string line;
            string[] dec;
            using (StreamReader sr = new StreamReader(FILES_PATH + "header_operations.py"))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split('#')[0].Replace(" ", "").Replace("\t", "");
                    if (line.Length > 0 && line.Contains("=") && !line.Contains("0x"))
                    {
                        dec = line.Split('=');
                        if (ImportantMethods.IsNumericFKZ2(dec[1]))
                        {
                            codeDeclarations.Add(dec[0]);
                            codeValue.Add(ulong.Parse(dec[1]));
                        }
                    }
                }
            }
            for (int i = 0; i < UNUSED_CODES.Length; i++)
            {
                x = codeDeclarations.IndexOf(UNUSED_CODES[i]);
                codeDeclarations.Remove(UNUSED_CODES[i]);
                codeValue.RemoveAt(x);
            }
        }

        private static string[] ReadGlobalVariables()
        {
            List<string> globalVariables = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "variables.txt"))
                while (!sr.EndOfStream)
                    globalVariables.Add('$' + sr.ReadLine());
            return globalVariables.ToArray();
        }

        private static string[] ReadScriptNames()
        {
            string s;
            List<string> scriptNames = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "scripts.txt"))
            {
                sr.ReadLine();
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Length > 0)
                        scriptNames.Add("script_" + s);
                }
            }
            return scriptNames.ToArray();
        }

        private static string[] ReadQuickStrings()
        {
            List<string> quick_strings = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "quick_strings.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                    quick_strings.Add('@' + sr.ReadLine().Split(' ')[1].Replace('_',' ').Replace("\"", "\\\""));
            }
            return quick_strings.ToArray();
        }

        private static string[] ReadPresentations()
        {
            string s;
            List<string> presentations = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "presentations.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("prsnt_"))
                        presentations.Add(s);
                }
            }
            return presentations.ToArray();
        }

        private static string[] ReadStrings()
        {
            List<string> strings = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "strings.txt"))
            {
                sr.ReadLine();
                sr.ReadLine();
                while (!sr.EndOfStream)
                    strings.Add(sr.ReadLine().Split(' ')[0]);
            }
            return strings.ToArray();
        }

        private static string[] ReadItems()
        {
            string s;
            List<string> items = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "item_kinds1.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ')[0];
                    if (s.Contains("itm_"))
                        items.Add(s);
                }
            }
            return items.ToArray();
        }

        private static string[] ReadFactions()
        {
            string[] s;
            List<string> factions = new List<string>();
            //factions.Add("fac_no_faction"); // Why was this used before???
            using (StreamReader sr = new StreamReader(modPath + "factions.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ');
                    if (s.Length > 1)
                    {
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (s[i].Contains("fac_"))
                            {
                                factions.Add(s[i]);
                                i = s.Length;
                            }
                        }
                    }
                }
            }
            return factions.ToArray();
        }

        private static string[] ReadTroops()
        {
            string s;
            List<string> troops = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "troops.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("trp_"))
                        troops.Add(s);
                }
            }
            return troops.ToArray();
        }

        private static string[] ReadSceneProps()
        {
            string s;
            List<string> scene_props = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "scene_props.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("spr_"))
                        scene_props.Add(s);
                }
            }
            return scene_props.ToArray();
        }

        private static string[] ReadParties()
        {
            string[] s;
            List<string> parties = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "parties.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ');
                    if (s.Length > 3)
                        parties.Add(s[3]);
                }
            }
            return parties.ToArray();
        }

        private static string[] ReadPartyTemplates()
        {
            string s;
            List<string> party_templates = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "party_templates.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("pt_"))
                        party_templates.Add(s);
                }
            }
            return party_templates.ToArray();
        }

        private static string[] ReadMeshes()
        {
            string s;
            List<string> meshes = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "meshes.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("mesh_"))
                        meshes.Add(s);
                }
            }
            return meshes.ToArray();
        }

        private static string[] ReadSkills()
        {
            string s;
            List<string> skills = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "skills.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("skl_"))
                        skills.Add(s);
                }
            }
            return skills.ToArray();
        }

        private static string[] ReadMissionTemplates()
        {
            string s;
            List<string> mission_templates = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "mission_templates.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("mst_"))
                        mission_templates.Add('m' + s.Substring(2));
                }
            }
            return mission_templates.ToArray();
        }

        private static string[] ReadSounds()
        {
            string s;
            List<string> sounds = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "sounds.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("snd_"))
                        sounds.Add(s);
                }
            }
            return sounds.ToArray();
        }

        private static string[] ReadParticles()
        {
            string s;
            List<string> particles = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "particle_systems.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("psys_"))
                        particles.Add(s);
                }
            }
            return particles.ToArray();
        }

        private static string[] ReadGameMenus()
        {
            string s;
            List<string> menus = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "menus.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split()[0];
                    if (s.Contains("menu_"))
                        if (s.Substring(0, 4).Equals("menu"))
                            menus.Add("m" + s.Substring(2));
                }
            }
            return menus.ToArray();
        }

        private static string[] ReadQuests()
        {
            string s;
            List<string> quests = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "quests.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("qst_"))
                        quests.Add(s);
                }
            }
            return quests.ToArray();
        }

        private static string[] ReadTableauMaterials()
        {
            string s;
            List<string> tableau_materials = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "tableau_materials.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("tab_"))
                        tableau_materials.Add(s.Replace("tab_", "tableau_"));
                }
            }
            return tableau_materials.ToArray();
        }

        private static string[] ReadAnimations()
        {
            string s;
            List<string> animations = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "actions.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ')[0];
                    if (!ImportantMethods.IsNumericFKZ2(s))
                        animations.Add("anim_" + s);
                }
            }
            return animations.ToArray();
        }

        private static string[] ReadScenes()
        {
            string s;
            List<string> scenes = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "scenes.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("scn_"))
                        scenes.Add(s);
                }
            }
            return scenes.ToArray();
        }

        private static string[] ReadTracks()
        {
            string s;
            List<string> tracks = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "music.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    if (!ImportantMethods.IsNumeric(s, true) && !s.Trim(' ', '\t').Equals(string.Empty))
                    {
                        //if (s.Contains(".ogg"))
                        //    s = s.Remove(s.IndexOf(".ogg "));
                        //else if (s.Contains(".mp3"))
                        //    s = s.Remove(s.IndexOf(".mp3 "));
                        //else
                            s = s.Remove(s.LastIndexOf('.'));
                        s = "track_" + s;
                        int trackXCount = ImportantMethods.CountStringAccurancyInList(s, tracks);
                        if (trackXCount == 0)
                            tracks.Add(s);
                        else
                            tracks.Add(s + "_" + (trackXCount + 1));
                    }
                }
            }
            return tracks.ToArray();
        }

        private static string[] ReadMapIcons()
        {
            string s;
            List<string> map_icons = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "map_icons.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart('-').Split()[0];
                    if (!ImportantMethods.IsNumeric(s, true) && !s.Trim().Equals(string.Empty))
                        map_icons.Add("icon_" + s);
                }
            }
            return map_icons.ToArray();
        }

        private static string[] ReadInfoPages()
        {
            string s;
            List<string> infoPages = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "info_pages.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("ip_"))
                        infoPages.Add(s);
                }
            }
            return infoPages.ToArray();
        }

        private static string[] ReadPostFXParams()
        {
            string s;
            List<string> postFXParams = new List<string>();
            using (StreamReader sr = new StreamReader(modPath + "postfx.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("pfx_"))
                        postFXParams.Add(s);
                }
            }
            return postFXParams.ToArray();
        }

        #endregion

        #region Main Reader

        public static int Overflow { get { return objectsRead - objectsExpected; } }

        public static string RemNTrimAllXtraSp(string s)
        {
            s = s.Replace('\t', ' ').Trim();
            while (s.Contains("  "))
                s = s.Replace("  ", " ");
            return s;
        }

        public List<Skriptum> ReadObjectType(int objectType)
        {
            List<Skriptum> skriptums = new List<Skriptum>();
            if (objectType == (int)Skriptum.ObjectType.SCRIPT)
                foreach (Skriptum s in ReadScript())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.MISSION_TEMPLATE)
                foreach (Skriptum s in ReadMissionTemplate())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.PRESENTATION)
                foreach (Presentation p in ReadPresentation())
                    skriptums.Add(p);
            else if (objectType == (int)Skriptum.ObjectType.GAME_MENU)
                foreach (GameMenu g in ReadGameMenu())
                    skriptums.Add(g);
            else if (objectType == (int)Skriptum.ObjectType.GAME_STRING)
                foreach (GameString s in ReadString())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.SIMPLE_TRIGGER)
                foreach (SimpleTrigger t in ReadSimpleTrigger())
                    skriptums.Add(t);
            else if (objectType == (int)Skriptum.ObjectType.TRIGGER)
                foreach (Trigger t in ReadTrigger())
                    skriptums.Add(t);
            else if (objectType == (int)Skriptum.ObjectType.INFO_PAGE)
                foreach (InfoPage p in ReadInfoPage())
                    skriptums.Add(p);
            else if (objectType == (int)Skriptum.ObjectType.SOUND)
                foreach (Sound s in ReadSound())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.QUEST)
                foreach (Quest q in ReadQuest())
                    skriptums.Add(q);
            else if (objectType == (int)Skriptum.ObjectType.SCENE)
                foreach (Scene s in ReadScene())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.SCENE_PROP)
                foreach (SceneProp s in ReadSceneProp())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.TABLEAU_MATERIAL)
                foreach (TableauMaterial t in ReadTableauMaterial())
                    skriptums.Add(t);
            else if (objectType == (int)Skriptum.ObjectType.MUSIC)
                foreach (Music m in ReadMusic())
                    skriptums.Add(m);
            else if (objectType == (int)Skriptum.ObjectType.MESH)
                foreach (Mesh m in ReadMesh())
                    skriptums.Add(m);
            else if (objectType == (int)Skriptum.ObjectType.FACTION)
                foreach (Faction f in ReadFaction())
                    skriptums.Add(f);
            else if (objectType == (int)Skriptum.ObjectType.MAP_ICON)
                foreach (MapIcon m in ReadMapIcon())
                    skriptums.Add(m);
            else if (objectType == (int)Skriptum.ObjectType.ANIMATION)
                foreach (Animation a in ReadAnimation())
                    skriptums.Add(a);
            else if (objectType == (int)Skriptum.ObjectType.PARTY_TEMPLATE)
                foreach (PartyTemplate p in ReadPartyTemplate())
                    skriptums.Add(p);
            else if (objectType == (int)Skriptum.ObjectType.DIALOG)
                foreach (Dialog d in ReadDialog())
                    skriptums.Add(d);
            else if (objectType == (int)Skriptum.ObjectType.PARTY)
                foreach (Party p in ReadParty())
                    skriptums.Add(p);
            else if (objectType == (int)Skriptum.ObjectType.SKILL)
                foreach (Skill s in ReadSkill())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.POST_FX)
                foreach (PostFX p in ReadPostFX())
                    skriptums.Add(p);
            else if (objectType == (int)Skriptum.ObjectType.PARTICLE_SYSTEM)
                foreach (ParticleSystem p in ReadParticleSystem())
                    skriptums.Add(p);
            else if (objectType == (int)Skriptum.ObjectType.SKIN)
                foreach (Skin s in ReadSkin())
                    skriptums.Add(s);
            else if (objectType == (int)Skriptum.ObjectType.TROOP)
                foreach (Troop t in ReadTroop())
                    skriptums.Add(t);
            else if (objectType == (int)Skriptum.ObjectType.ITEM)
                foreach (Item itm in ReadItem())
                    skriptums.Add(itm);
            return skriptums;
        }

        public static List<List<Skriptum>> ReadAllObjects()
        {
            Reset();
            List<List<Skriptum>> objects = new List<List<Skriptum>>();
            for (int i = 0; i < Files.Length; i++)
                objects.Add(new CodeReader(ModPath + Files[i]).ReadObjectType(i));
            return objects;
        }

        public MissionTemplate[] ReadMissionTemplate()
        {
            string line;
            string[] scriptLines;
            MissionTemplate missionTemplate = null;
            List<MissionTemplate> missionTemplates = new List<MissionTemplate>();
            List<string[]> entryPoints = new List<string[]>();
            List<string[]> triggers = new List<string[]>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                objectsExpected += int.Parse(sr.ReadLine().Trim());
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("mst_"))
                    {
                        if (missionTemplate != null)
                        {
                            missionTemplates.Add(missionTemplate);
                            entryPoints.Clear();
                            triggers.Clear();
                        }
                        scriptLines = line.Split();
                        line = sr.ReadLine().TrimEnd().Replace('_', ' ');
                        missionTemplate = new MissionTemplate(new string[] { scriptLines[1], scriptLines[2], scriptLines[4], line });

                        sr.ReadLine();
                        line = sr.ReadLine();
                        scriptLines = line.Split(' ');
                        int max = int.Parse(scriptLines[0]);
                        if (max > 0)
                        {
                            scriptLines = line.Substring(line.IndexOf(' ') + 1).Split();

                            entryPoints.Add(scriptLines);

                            for (int i = 1; i < max; i++)
                                entryPoints.Add(sr.ReadLine().Split());

                            max = int.Parse(sr.ReadLine());
                        }
                        else
                            max = int.Parse(scriptLines[1]);

                        for (int i = 0; i < max; i++)
                            triggers.Add(sr.ReadLine().Split());

                        missionTemplate = DecompileMissionTemplateCode(missionTemplate.HeaderInfo, entryPoints, triggers);
                    }
                }
            }
            if (missionTemplate != null)
                missionTemplates.Add(missionTemplate);
            objectsRead += missionTemplates.Count;
            return missionTemplates.ToArray();
        }

        public Presentation[] ReadPresentation()
        {
            string line;
            string[] scriptLines;
            Presentation presentation = null;
            List<Presentation> presentations = new List<Presentation>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                objectsExpected += int.Parse(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("prsnt_"))
                    {
                        if (presentation != null)
                            presentations.Add(presentation);
                        scriptLines = line.Split(' ');
                        presentation = new Presentation(scriptLines[0].Substring(scriptLines[0].IndexOf('_') + 1), ulong.Parse(scriptLines[1]), int.Parse(scriptLines[2]), int.Parse(scriptLines[3]));
                        for (int i = 0; i < presentation.SimpleTriggers.Length; i++)
                        {
                            scriptLines = sr.ReadLine().Split();
                            SimpleTrigger simpleTrigger = new SimpleTrigger(double.Parse(Repl_DotWComma(scriptLines[0])));
                            string[] tmp = new string[int.Parse(scriptLines[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            scriptLines = GetStringArrayStartFromIndex(scriptLines, 2, 1);
                            simpleTrigger.ConsequencesBlock = DecompileScriptCode(tmp, scriptLines);
                            presentation.addSimpleTriggerToFreeIndex(simpleTrigger, i);
                        }
                    }
                }
            }
            if (presentation != null)
                presentations.Add(presentation);
            objectsRead += presentations.Count;
            return presentations.ToArray();
        }

        public GameMenu[] ReadGameMenu()
        {
            string s;
            List<GameMenu> game_menus = new List<GameMenu>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                objectsExpected += int.Parse(sr.ReadLine().TrimStart());
                while (!sr.EndOfStream)
                {
                    s = RemNTrimAllXtraSp(sr.ReadLine());
                    if (s.Length > 1)
                        game_menus.Add(new GameMenu(new string[] { s, RemNTrimAllXtraSp(sr.ReadLine()) }));
                }
            }
            objectsRead += game_menus.Count;
            return game_menus.ToArray();
        }

        public Script[] ReadScript()
        {
            List<Script> scripts = new List<Script>();
            string[] script = null;
            string[] scriptLines;
            string line;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                objectsExpected += int.Parse(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Length > 1)
                    {
                        if (ImportantMethods.IsNumericFKZ2(line.Substring(1, 1)))
                        {
                            scriptLines = line.Substring(1, line.Length - 2).Split();
                            line = script[0];
                            script = new string[int.Parse(scriptLines[0] + 1)];
                            script[0] = line;
                            //System.Windows.Forms.MessageBox.Show(line); // shows the script name
                            script = DecompileScriptCode(script, scriptLines);
                        }
                        else
                        {
                            if (script != null)
                                scripts.Add(new Script(script));
                            script = new string[1];
                            script[0] = line.Split()[0];
                        }
                    }
                }
            }
            if (script != null)
                scripts.Add(new Script(script));
            objectsRead += scripts.Count;
            return scripts.ToArray();
        }

        public Troop[] ReadTroop()
        {
            string[] tempus = new string[7];
            Troop[] troops;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int maxTroops = int.Parse(sr.ReadLine().TrimEnd());
                troops = new Troop[maxTroops];
                objectsExpected += maxTroops;
                for (int i = 0; i < maxTroops; i++)
                {
                    for (int j = 0; j < 7; j++)
                        tempus[j] = sr.ReadLine();
                    troops[i] = new Troop(tempus);
                }
            }
            objectsRead += troops.Length;
            return troops;
        }

        public Item[] ReadItem()
        {
            int i = -1;
            string tempus;
            List<string> lines = new List<string>();
            Item[] items;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int maxItems = int.Parse(sr.ReadLine());
                objectsExpected += maxItems;
                items = new Item[maxItems];
                while (!sr.EndOfStream)
                {
                    tempus = sr.ReadLine();
                    if (tempus.Contains(" itm_"))
                    {
                        i++;
                        lines.Clear();
                        do
                        {
                            tempus = tempus.Replace('\t', ' ');
                            while (tempus.Contains("  "))
                                tempus = tempus.Replace("  ", " ");
                            tempus = tempus.Trim();
                            if (!tempus.Equals(string.Empty))
                                lines.Add(tempus);
                            tempus = sr.ReadLine().TrimStart();
                        } while (!tempus.Equals(string.Empty) || lines.Count < 3);
                        items[i] = new Item(lines.ToArray());
                    }
                }
            }

            objectsRead += items.Length;
            return items;
        }

        public GameString[] ReadString()
        {
            GameString[] strings;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                strings = new GameString[count];
                for (int i = 0; i < strings.Length; i++)
                    strings[i] = new GameString(sr.ReadLine().Substring(4).Split());
            }
            objectsRead += strings.Length;
            return strings;
        }

        public SimpleTrigger[] ReadSimpleTrigger()
        {
            string[] scriptLines;
            SimpleTrigger[] simple_triggers;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                simple_triggers = new SimpleTrigger[count];
                for (int i = 0; i < simple_triggers.Length; i++)
                {
                    scriptLines = sr.ReadLine().Split();
                    simple_triggers[i] = new SimpleTrigger(double.Parse(Repl_DotWComma(scriptLines[0])));
                    string[] tmp = new string[int.Parse(scriptLines[2]) + 1];
                    tmp[0] = "SIMPLE_TRIGGER";
                    scriptLines = GetStringArrayStartFromIndex(scriptLines, 2, 1);
                    simple_triggers[i].ConsequencesBlock = GetStringArrayStartFromIndex(DecompileScriptCode(tmp, scriptLines), 1);
                }
            }
            objectsRead += simple_triggers.Length;
            return simple_triggers;
        }

        public Trigger[] ReadTrigger()
        {
            Trigger[] triggers;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                triggers = new Trigger[count];
                for (int i = 0; i < triggers.Length; i++)
                    triggers[i] = DecompileTrigger(sr.ReadLine().Split());
            }
            objectsRead += triggers.Length;
            return triggers;
        }

        public InfoPage[] ReadInfoPage()
        {
            InfoPage[] info_pages;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                info_pages = new InfoPage[count];
                for (int i = 0; i < info_pages.Length; i++)
                    info_pages[i] = new InfoPage(sr.ReadLine().Split());
            }
            objectsRead += info_pages.Length;
            return info_pages;
        }

        public Mesh[] ReadMesh()
        {
            Mesh[] meshes;
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                meshes = new Mesh[count];
                for (int i = 0; i < meshes.Length; i++)
                    meshes[i] = new Mesh(sr.ReadLine().Substring(5).Split());
            }
            objectsRead += meshes.Length;
            return meshes;
        }

        public Music[] ReadMusic()
        {
            Music[] musicTracks;
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                musicTracks = new Music[count];
                for (int i = 0; i < musicTracks.Length; i++)
                {
                    string[] sts = sr.ReadLine().Split();
                    musicTracks[i] = new Music(new string[] { Tracks[i].Substring(6), sts[0], sts[1], sts[2] });
                }
            }
            objectsRead += musicTracks.Length;
            return musicTracks;
        }

        public Quest[] ReadQuest()
        {
            Quest[] quests;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                quests = new Quest[count];
                for (int i = 0; i < quests.Length; i++)
                    quests[i] = new Quest(sr.ReadLine().Substring(4).Split());
            }
            objectsRead += quests.Length;
            return quests;
        }

        public Sound[] ReadSound()
        {
            string line;
            Sound[] sounds;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                sr.ReadLine();
                do { line = sr.ReadLine(); } while (!ImportantMethods.IsNumericFKZ2(line));
                int count = int.Parse(line);
                objectsExpected += count;
                sounds = new Sound[count];
                for (int i = 0; i < sounds.Length && !sr.EndOfStream; i++)
                    sounds[i] = new Sound(sr.ReadLine().Substring(4).Split());
            }
            objectsRead += sounds.Length;
            return sounds;
        }

        public Scene[] ReadScene()
        {
            string firstLine;
            string[] otherScenes, chestTroops, tmp;
            Scene[] _scenes;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int tmpX;
                int count = int.Parse(sr.ReadLine().TrimStart());
                objectsExpected += count;
                _scenes = new Scene[count];
                for (int i = 0; i < _scenes.Length; i++)
                {
                    firstLine = sr.ReadLine();
                    tmp = sr.ReadLine().Substring(2).TrimEnd().Replace("  ", " ").Split();
                    otherScenes = new string[int.Parse(tmp[0])];
                    for (int j = 0; j < otherScenes.Length; j++)
                    {
                        tmpX = int.Parse(tmp[j + 1]);
                        if (Scenes.Length > tmpX && tmpX >= 0)
                            otherScenes[j] = Scenes[tmpX];
                        else if (tmpX == 100000)
                            otherScenes[j] = "exit";
                        else
                            otherScenes[j] = "(ERROR)" + tmpX; //CHECK!!!
                    }
                    tmp = sr.ReadLine().Substring(2).TrimEnd().Replace("  ", " ").Split();
                    chestTroops = new string[int.Parse(tmp[0])];
                    for (int j = 0; j < chestTroops.Length; j++)
                        chestTroops[j] = Troops[int.Parse(tmp[j + 1])];
                    _scenes[i] = new Scene(firstLine.Split(), otherScenes, chestTroops, sr.ReadLine().Trim());
                }
            }
            objectsRead += _scenes.Length;
            return _scenes;
        }

        public TableauMaterial[] ReadTableauMaterial()
        {
            TableauMaterial[] tableaus;
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                tableaus = new TableauMaterial[count];
                for (int i = 0; i < tableaus.Length; i++)
                    tableaus[i] = new TableauMaterial(sr.ReadLine().Substring(4).TrimEnd().Split());
            }
            objectsRead += tableaus.Length;
            return tableaus;
        }

        public SceneProp[] ReadSceneProp()
        {
            int tCount;
            string[] lines;
            SceneProp[] sceneProps;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().TrimStart());
                objectsExpected += count;
                sceneProps = new SceneProp[count];
                for (int i = 0; i < sceneProps.Length; i++)
                {
                    string tmpSSS = sr.ReadLine().Replace("  ", " ").Replace('\t', ' ');
                    while (tmpSSS.Contains("  "))
                        tmpSSS = tmpSSS.Replace("  ", " ");
                    lines = tmpSSS.Split();

                    sceneProps[i] = new SceneProp(lines);
                    tCount = int.Parse(lines[lines.Length - 1]);
                    if (tCount > 0)
                    {
                        SimpleTrigger[] s_triggers = new SimpleTrigger[tCount];
                        for (int j = 0; j < s_triggers.Length; j++)
                        {
                            lines = sr.ReadLine().Split();
                            s_triggers[j] = new SimpleTrigger(double.Parse(Repl_DotWComma(lines[0])));
                            string[] tmp = new string[int.Parse(lines[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            lines = GetStringArrayStartFromIndex(lines, 2, 1);
                            s_triggers[j].ConsequencesBlock = GetStringArrayStartFromIndex(DecompileScriptCode(tmp, lines), 1);
                        }
                        sceneProps[i].SimpleTriggers = s_triggers;
                    }
                    sr.ReadLine();
                    sr.ReadLine();
                }
            }
            objectsRead += sceneProps.Length;
            return sceneProps;
        }

        public Faction[] ReadFaction()
        {
            int c;
            string line;
            Faction[] _factions;
            Faction.ResetIDs();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                _factions = new Faction[count];
                for (int i = 0; i < _factions.Length; i++)
                {
                    do { c = sr.Read(); } while ((char)c != 'f');
                    _factions[i] = new Faction(((char)c + sr.ReadLine().TrimEnd()).Split());
                    string[] sp = sr.ReadLine().Trim().Replace("  ", " ").Split();
                    double[] dd = new double[sp.Length];
                    for (int j = 0; j < sp.Length; j++)
                        dd[j] = double.Parse(Repl_DotWComma(sp[j]));
                    _factions[i].Relations = dd;
                    c = sr.Read();
                    if ((char)c != '0')
                    {
                        sr.Read();
                        string[] tmp = new string[c];
                        for (int j = 0; j < tmp.Length; j++)
                        {
                            line = string.Empty;
                            do
                            {
                                c = sr.Read();
                                line += (char)c;
                            } while ((char)c != ' ');
                            tmp[j] = line.TrimEnd();
                        }
                    }
                }
            }
            objectsRead += _factions.Length;
            return _factions;
        }

        public MapIcon[] ReadMapIcon()
        {
            int tCount;
            string[] sp;
            MapIcon[] mapIcons;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                mapIcons = new MapIcon[count];
                for (int i = 0; i < mapIcons.Length; i++)
                {
                    sp = sr.ReadLine().Split();
                    tCount = int.Parse(sp[sp.Length - 1]);
                    mapIcons[i] = new MapIcon(sp);
                    if (tCount > 0)
                    {
                        SimpleTrigger[] s_triggers = new SimpleTrigger[tCount];
                        for (int j = 0; j < s_triggers.Length; j++)
                        {
                            sp = sr.ReadLine().Split();
                            s_triggers[j] = new SimpleTrigger(double.Parse(Repl_DotWComma(sp[0])));
                            string[] tmp = new string[int.Parse(sp[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            sp = GetStringArrayStartFromIndex(sp, 2, 1);
                            s_triggers[j].ConsequencesBlock = GetStringArrayStartFromIndex(DecompileScriptCode(tmp, sp), 1);
                        }
                        mapIcons[i].SimpleTriggers = s_triggers;
                    }
                    sr.ReadLine();
                    sr.ReadLine();
                }
            }
            objectsRead += mapIcons.Length;
            return mapIcons;
        }

        public Animation[] ReadAnimation()
        {
            string[] sp;
            Animation[] animations;
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                animations = new Animation[count];
                for (int i = 0; i < animations.Length; i++)
                {
                    sp = sr.ReadLine().Substring(1).Replace("  ", " ").Split();
                    animations[i] = new Animation(sp);
                    AnimationSequence[] sequences = new AnimationSequence[int.Parse(sp[sp.Length - 1])];
                    for (int j = 0; j < sequences.Length; j++)
                        sequences[j] = new AnimationSequence(sr.ReadLine().Trim().Replace("  ", " ").Split());
                    animations[i].Sequences = sequences;
                }
            }
            objectsRead += animations.Length;
            return animations;
        }

        public PartyTemplate[] ReadPartyTemplate()
        {
            PartyTemplate[] partyTemplates;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                partyTemplates = new PartyTemplate[count];
                for (int i = 0; i < partyTemplates.Length; i++)
                    partyTemplates[i] = new PartyTemplate(sr.ReadLine().Substring(3).TrimEnd().Split());
            }
            objectsRead += partyTemplates.Length;
            return partyTemplates;
        }

        public Dialog[] ReadDialog()
        {
            Dialog[] dialogs;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                dialogs = new Dialog[count];
                for (int i = 0; i < dialogs.Length; i++)
                    dialogs[i] = new Dialog(sr.ReadLine().Substring(5).TrimEnd().Split());
            }
            objectsRead += dialogs.Length;
            return dialogs;
        }

        public Party[] ReadParty()
        {
            string line;
            Party[] parties;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().Split()[0]);
                objectsExpected += count;
                parties = new Party[count];
                for (int i = 0; i < parties.Length; i++)
                {
                    line = sr.ReadLine().Trim();
                    double degrees = double.Parse(Repl_DotWComma(sr.ReadLine()));
                    degrees = Math.Round(degrees / (3.1415926 / 180d), 4);
                    line += " " + degrees; // maybe check if values are still correct!
                    parties[i] = new Party(line.Split());
                }
            }
            objectsRead += parties.Length;
            return parties;
        }

        public Skill[] ReadSkill()
        {
            Skill[] skills;
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine().Split()[0]);
                objectsExpected += count;
                skills = new Skill[count];
                for (int i = 0; i < skills.Length; i++)
                    skills[i] = new Skill(sr.ReadLine().Substring(4).Split());
            }
            objectsRead += skills.Length;
            return skills;
        }

        public PostFX[] ReadPostFX()
        {
            PostFX[] postfxs;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().Split()[0]);
                objectsExpected += count;
                postfxs = new PostFX[count];
                for (int i = 0; i < postfxs.Length; i++)
                    postfxs[i] = new PostFX(sr.ReadLine().Substring(4));
            }
            objectsRead += postfxs.Length;
            return postfxs;
        }

        public ParticleSystem[] ReadParticleSystem()
        {
            ParticleSystem[] particleSystems;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                particleSystems = new ParticleSystem[count];
                for (int i = 0; i < particleSystems.Length; i++)
                {
                    List<string[]> list = new List<string[]>
                    {
                        sr.ReadLine().Substring(5).Replace("  ", ":").TrimEnd().Split(':'),
                        sr.ReadLine().Replace("   ", " ").Split(' '),
                        sr.ReadLine().Replace("   ", " ").Split(' '),
                        sr.ReadLine().Replace("   ", " ").Split(' '),
                        sr.ReadLine().Replace("   ", " ").Split(' '),
                        sr.ReadLine().Replace("   ", " ").Split(' '),
                        sr.ReadLine().Replace("   ", ":").TrimEnd().Split(':'),
                        sr.ReadLine().Replace("   ", ":").TrimEnd().Split(':')
                    };
                    particleSystems[i] = new ParticleSystem(list);
                }
            }
            objectsRead += particleSystems.Length;
            return particleSystems;
        }

        public Skin[] ReadSkin()
        {
            Skin[] skins;
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                skins = new Skin[count];
                for (int i = 0; i < skins.Length; i++)
                {
                    List<string[]> list = new List<string[]>
                    {
                        sr.ReadLine().Split(),
                        sr.ReadLine().TrimStart().Split(),
                        sr.ReadLine().Trim().Split()
                    };
                    sr.ReadLine();
                    list.Add(sr.ReadLine().Replace("  ", " ").Trim().Split());
                    string[] sp = new string[int.Parse(sr.ReadLine().Trim())];
                    for (int j = 0; j < sp.Length; j++)
                        sp[j] = sr.ReadLine().Substring(2);
                    sr.ReadLine();
                    list.Add(sp);
                    list.Add(sr.ReadLine().Replace("  ", " ").Trim().Split());
                    list.Add(sr.ReadLine().Replace("  ", " ").Trim().Split());
                    list.Add(sr.ReadLine().Replace("  ", " ").Trim().Split());
                    list.Add(sr.ReadLine().Replace("  ", ":").Trim().Split(':'));
                    list.Add(sr.ReadLine().Trim().Split());
                    list.Add(sr.ReadLine().Split());
                    count = int.Parse(sr.ReadLine());
                    sr.ReadLine();
                    if (count > 0)
                    {
                        list.Add(new string[] { count.ToString() });
                        for (int j = 0; j < count; j++)
                            list.Add(sr.ReadLine().Split());
                    }
                    else
                        list.Add(new string[] { "0" });
                    skins[i] = new Skin(list);
                }
            }
            objectsRead += skins.Length;
            return skins;
        }

        public static string[] GetStringArrayStartFromIndex(string[] s_array, int index, int minus_length = 0)
        {
            string[] array = new string[s_array.Length - index - minus_length];
            for (int i = 0; i < array.Length; i++)
                array[i] = s_array[i + index];
            return array;
        }

        #endregion

        #region Decompiling

        private static int GetObjIdx(ulong a, ulong b) { return (int)(a - b); }

        public static string[] DecompileScriptCode(string[] script, string[] scriptCode)
        {
            ulong border1 = (ulong)1 + int.MaxValue, border2 = border1 / 2;
            bool neg, this_or_next, plus = false, else_try = false;
            int elements, activeScriptLine = 1, adder = 0, mul, i = 0;
            ulong[] codes = new ulong[scriptCode.Length - 1];

            /*string sss = string.Empty; // FOR DEBUGGING
            for (int j = 0; j < scriptCode.Length; j++)
                sss += scriptCode[j] + '\n';
            sss += "|END";
            System.Windows.Forms.MessageBox.Show(sss);*/

            localVariableInterpreter = new LocalVariableInterpreter();

            for (int j = 0; j < codes.Length; j++)
            {
                if (scriptCode[j + 1].Contains("-"))
                {
                    mul = -1;
                    scriptCode[j + 1] = scriptCode[j + 1].Substring(1);
                }
                else
                    mul = 1;
                codes[j] = ulong.Parse(scriptCode[j + 1]);
                codes[j] = (ulong)mul * codes[j];
            }

            while (i < codes.Length)
            {
                this_or_next = false;
                neg = false;
                if (codes[i] > border2)
                {
                    if (codes[i] > int.MaxValue)
                    {
                        codes[i] -= border1;
                        neg = true;
                    }
                    if (codes[i] > border2)
                    {
                        codes[i] -= border2;
                        this_or_next = true;
                    }
                }

                if (plus)
                {
                    adder++;
                    plus = !plus;
                }

                for (int j = 0; j < codeValue.Count; j++)
                {
                    if (codeValue[j] == codes[i])
                    {
                        script[activeScriptLine] = codeDeclarations[j];
                        j = codeValue.Count;
                    }
                }

                if (script[activeScriptLine] == null)
                    script[activeScriptLine] = codes[i].ToString();

                if (script[activeScriptLine] != null)
                {
                    if (script[activeScriptLine].Equals("try_begin") || script[activeScriptLine].Contains("try_for"))
                        plus = !plus;
                    else if (script[activeScriptLine].Equals("else_try"))
                        else_try = !else_try;
                    else if (script[activeScriptLine].Equals("try_end"))
                        adder--;
                }

                if (this_or_next)
                {
                    if (!neg)
                        script[activeScriptLine] = script[activeScriptLine];
                    else
                        script[activeScriptLine] = GetNegationCode("(neg|" + script[activeScriptLine]).TrimStart('('); // A FEW TIMES DANGERZONE !!!
                    script[activeScriptLine] = "(this_or_next|" + script[activeScriptLine];
                }
                else if (neg)
                    script[activeScriptLine] = GetNegationCode("(neg|" + script[activeScriptLine]); // A FEW TIMES DANGERZONE !!!
                else
                    script[activeScriptLine] = '(' + script[activeScriptLine];

                

                if (else_try)
                    adder--;

                for (int z = 0; z < (adder + 1); z++)
                    script[activeScriptLine] = "    " + script[activeScriptLine];

                if (else_try)
                {
                    adder++;
                    else_try = !else_try;
                }

                i++;
                elements = (int)codes[i];
                i++;

                int x = i + elements;
                for (int j = i; j < x; j++)
                    //if (j < codes.Length)
                        script[activeScriptLine] = GetDecompiledCodeParameters(script[activeScriptLine] + ", \"", codes[j], activeScriptLine); // SOMETIMES THIS IS THE DANGERZONE !!!
                    //else
                    //    System.Windows.Forms.MessageBox.Show(script[activeScriptLine] + "!!!" + Environment.NewLine + script[0] + Environment.NewLine + codes.Length + Environment.NewLine + x);

                script[activeScriptLine] += "),";

                i = x;
                activeScriptLine++;
            }

            for (int z = 0; z < script.Length; z++)
            {
                if (script[z] == null || z == (script.Length - 1))
                {
                    i = z + 1;
                    z = script.Length;
                }
            }

            //for (int ccc = 0; ccc < 8; ccc++)
                //script = ReplaceUnusedVariables(script);
            
            scriptCode = new string[i];
            for (int z = 0; z < scriptCode.Length; z++)
                scriptCode[z] = script[z];

            return scriptCode;
        }

        #region UnusedVariables

        /*
        private static string[] ReplaceUnusedVariables(string[] script)
        {
            int i = 0, idx = 0;
            string tmpS;
            string[] scriptCode;
            unusedIndicies = new List<List<int>>();
            //unusedIndiciesEXTRA = new List<List<int>>();
            List<LocalVariable> _vars = localVariableInterpreter.getUnusedLocalVariables();
            foreach (LocalVariable _var in _vars)
            {
                List<int> _idxs = new List<int>();
                List<int> _extras = new List<int>();
                _idxs.Add(_var.CodeLineIndicies[0]);
                _extras.Add(0);
                unusedIndicies.Add(_idxs);
                //unusedIndiciesEXTRA.Add(_extras);
            }
            foreach (LocalVariable _var in localVariableInterpreter.getPossibleUsedLocalVariables())
                if (!CheckForUnusedVariables(script, _var, true))
                    _vars.Add(_var);
            RemoveDoubleIndicies();
            foreach (LocalVariable _var in _vars)
            {
                for (int j = unusedIndicies[i].Count - 1; j >= 0; j--)
                {
                    idx = unusedIndicies[i][j];
                    tmpS = script[idx];
                    tmpS = tmpS.Split('#')[0];
                    if (tmpS.Contains(":") && tmpS.Contains("\""))
                    {
                        scriptCode = tmpS.Split('\"');
                        for (int z = 0; z < scriptCode.Length; z++)
                        {
                            if (scriptCode[z].Equals(_var.LocalName))
                            {
                                scriptCode[z] = ":unused";
                                //if (unusedIndiciesEXTRA[i][j] > 0)
                                //    scriptCode[z] += "_" + unusedIndiciesEXTRA[i][j];
                                z = scriptCode.Length;
                            }
                        }
                        tmpS = string.Empty;
                        foreach (string s in scriptCode)
                            tmpS += '\"' + s;
                        tmpS = tmpS.Substring(1);
                        if (script[idx].Contains("#"))
                            tmpS += script[idx].Substring(script[idx].IndexOf('#'));
                        script[idx] = tmpS;
                    }
                }
                i++;
            }
            return CheckFor2ndUnusedInBlock(script);
            //return script;
        }

        private static void RemoveDoubleIndicies()
        {
            bool betra = false;
            int y = 0, x = 0;
            do
            {
                for (int po = 0; po < unusedIndicies.Count; po++)
                {
                    for (int pi = 0; pi < unusedIndicies[po].Count; pi++)
                    {
                        for (int pa = 0; pa < unusedIndicies.Count; pa++)
                        {
                            if (po != pa)
                            {
                                if (unusedIndicies[pa].Contains(unusedIndicies[po][pi]))
                                {
                                    y = unusedIndicies[po][pi];
                                    x = pa;
                                    betra = true;
                                    pa = unusedIndicies.Count;
                                }
                            }
                        }
                        if (betra)
                            pi = unusedIndicies[po].Count;
                    }
                    if (betra)
                        po = unusedIndicies.Count;
                }
                if (betra)
                    unusedIndicies[x].Remove(y);
            } while (betra);
        }

        private static bool CheckForUnusedVariables(string[] script, LocalVariable _var, bool b)
        {
            bool b2;
            string tmpS;
            int counter;

            List<int> idxs = new List<int>();
            List<int> extras = new List<int>();

            foreach (int iii in _var.CodeLineIndicies)
            {
                tmpS = script[iii].Split('#')[0];
                int textLength = tmpS.IndexOf(_var.LocalName);
                if (textLength != -1)
                {
                    if (tmpS.Contains("try_for") && CountCharInString(tmpS.Substring(0, textLength), ',') == 1)
                    {
                        counter = 1;
                        b2 = false;
                        for (int z = iii + 1; z < script.Length; z++)
                        {
                            if (script[z] != null)
                            {
                                tmpS = script[z].Split('#')[0];
                                if (tmpS.Contains(_var.LocalName))
                                {
                                    if (!tmpS.Contains("try_for"))
                                        b2 = true;
                                    else if (CountCharInString(tmpS, ',') > 2)
                                        if (!tmpS.Substring(tmpS.IndexOf(',') + 1).Split(',')[0].Trim().Equals('\"' + _var.LocalName + '\"'))
                                            b2 = true;
                                        else if (!ScriptCotainsVariableUsed(script, _var, iii))
                                        {
                                            idxs.Add(z);
                                            if (extras.Count == 0)
                                                extras.Add(1);
                                            else
                                                extras.Add(extras[extras.Count - 1] + 1);
                                        }
                                }
                                else if (tmpS.Contains("try_begin") || tmpS.Contains("try_for"))
                                    counter++;
                                else if (tmpS.Contains("try_end"))
                                    counter--;
                                else if (counter == 0 || b2)
                                    z = script.Length;
                            }
                            else if (counter == 0)
                                z = script.Length;
                        }
                        if (!b2 && !ScriptCotainsVariableUsed(script, _var, iii)) 
                        {
                            idxs.Add(iii);
                            extras.Add(0);
                            b = false;
                        }
                    }
                }
            }
            if (!b)
            //{
                unusedIndicies.Add(idxs);
                //unusedIndiciesEXTRA.Add(extras);
            //}
            return b;
        }

        private static bool ScriptCotainsVariableUsed(string[] script, LocalVariable _var, int _varIndex)
        {
            string searchX = "try_for";
            string varLocalName = '\"' + _var.LocalName + '\"';
            bool used = false;
            for (int i = 0; i < script.Length; i++)
            {
                if (script[i] != null)
                {
                    if (script[i].Contains(varLocalName) && !script[i].Substring(0, script[i].IndexOf(varLocalName)).Contains(searchX))
                        used = true;
                }
                else
                    i = script.Length;
            }
            return used;
        }

        private static string[] CheckFor2ndUnusedInBlock(string[] script)
        {
            bool plus = false;
            int adder = 0, count;
            List<List<int>> unusedVarsLines = new List<List<int>>();
            //unusedVarsLines.Add(new List<int>());
            //bool bundit = false;
            //if (script[0].Equals("get_quest"))
            //{
            //    bundit = true;
            //    if (File.Exists("varDebug.txt"))
            //        File.Delete("varDebug.txt");
            //}
            //using (StreamWriter wr = new StreamWriter("varDebug.txt", true))
            //{
            //if (bundit)
            //    wr.WriteLine("START WITH: " + script[0]);
            //try
            //{
            for (int i = 0; i < script.Length; i++)
            {
                if (script[i] != null)
                {
                    if (plus)
                    {
                        adder++;
                        plus = !plus;
                    }
                    if (adder == unusedVarsLines.Count)
                        unusedVarsLines.Add(new List<int>());
                    //if (bundit)
                    //    wr.WriteLine("ADDER = " + adder + " // " + script[i]);
                    if (script[i].Trim().Equals("(try_begin),") || script[i].Contains("try_for"))
                        plus = !plus;
                    else if (script[i].Trim().Equals("(try_end),"))
                    {
                        count = 0;
                        for (int j = 0; j < adder; j++)
                        {
                            //if (bundit)
                            //    wr.WriteLine("COUNT = " + count + " + " + unusedVarsLines[j].Count);
                            count += unusedVarsLines[j].Count;
                        }
                        //if (bundit)
                        //    wr.WriteLine("COUNT = " + count);
                        for (int j = 0; j < unusedVarsLines[adder].Count; j++)
                        {
                            if (count > 0 || j > 0)
                            {
                                script[unusedVarsLines[adder][j]] = script[unusedVarsLines[adder][j]].Replace("\":unused\"", "\":unused_" + (count + j + 1) + "\"");
                                //if (bundit)
                                //    wr.WriteLine(script[unusedVarsLines[adder][j]]);
                            }
                        }
                        unusedVarsLines.RemoveAt(adder);
                        adder--;
                    }
                    //if (bundit)
                    //    wr.WriteLine("ADDER = " + adder);
                    if (script[i].Contains("\":unused\""))
                    {
                        if (script[i].Contains("try_for"))
                        {
                            adder++;
                            plus = false;
                            if (adder == unusedVarsLines.Count)
                                unusedVarsLines.Add(new List<int>());
                        }
                        unusedVarsLines[adder].Add(i);
                        //if (bundit)
                        //    wr.WriteLine("unusedVarsLines[adder].Add(" + i + ");  // adder = " + adder);
                    }
                }
                else
                    i = script.Length;
            }
            //if (unusedVarsLines[adder] != null)
            //{
            if (unusedVarsLines[adder].Count > 1)
            {
                for (int i = 1; i < unusedVarsLines[adder].Count; i++)
                {
                    script[unusedVarsLines[adder][i]] = script[unusedVarsLines[adder][i]].Replace("\":unused\"", "\":unused_" + (i + 1) + "\"");
                    //if (bundit)
                    //    wr.WriteLine(script[unusedVarsLines[adder][i]]);
                }
            }
            //}
            //}
            /*}
            catch (Exception ex)
            {
                string sedef = string.Empty;
                for (int i = 0; i < script.Length; i++)
                {
                    if (script[i] != null)
                        sedef += script[i] + Environment.NewLine;
                    else
                        i = script.Length;
                }
                System.Windows.Forms.MessageBox.Show(ex.ToString() + Environment.NewLine + Environment.NewLine + sedef);
            }*/
            //return script;
        //}

        #endregion

        private static string GetNegationCode(string code)
        {
            string ret;
            if (code.Equals("ge") || code.Equals("(neg|ge"))
                ret = "lt";
            else if (code.Equals("eq") || code.Equals("(neg|eq"))
                ret = "neq";
            else if (code.Equals("gt") || code.Equals("(neg|gt"))
                ret = "le";
            else
                ret = code;
            if (code.Contains("(neg|") && !ret.Equals(code))
                ret = '(' + ret;
            return ret;
        }

        private static string GetDecompiledCodeParameters(string codeLine, ulong code, int codeIndex = -1)
        {
            if (code >= LOCAL_MIN && code < LOCAL_MAX)
                codeLine += localVariableInterpreter.Interpret(codeLine, code, codeIndex); // Sollte noch verfeinert werden --> EXCHANGE OBJECTS        // OLD: ":var" + (code - LOCAL_MIN + 1)
            else if (code >= REG0 && code <= REG127)
                codeLine = codeLine.TrimEnd('\"') + "reg" + (code - REG0);
            else if (code >= QUICKSTRING_MIN && code < QUICKSTRING_MAX)
                codeLine +=  QuickStrings[GetObjIdx(code, QUICKSTRING_MIN)];
            else if (code >= GLOBAL_MIN && code < GLOBAL_MAX)
                codeLine += GlobalVariables[GetObjIdx(code, GLOBAL_MIN)];
            else if (code >= SCRIPT_MIN && code < SCRIPT_MAX)
                codeLine += Scripts[GetObjIdx(code, SCRIPT_MIN)];
            else if (code >= SPR_MIN && code < SPR_MAX)
                codeLine += SceneProps[GetObjIdx(code, SPR_MIN)];
            else if (code >= STRING_MIN && code < STRING_MAX)
                codeLine += Strings[GetObjIdx(code, STRING_MIN)];
            else if (code >= FAC_MIN && code < FAC_MAX)
                codeLine += Factions[GetObjIdx(code, FAC_MIN)];
            else if (code >= TRP_PLAYER && code < TROOP_MAX)
                codeLine += Troops[GetObjIdx(code, TRP_PLAYER)];
            else if (code >= PRSNT_MIN && code < PRSNT_MAX)
                codeLine += Presentations[GetObjIdx(code, PRSNT_MIN)];
            else if (code >= SCENE_MIN && code < SCENE_MAX)
                codeLine += Scenes[GetObjIdx(code, SCENE_MIN)];
            else if (code >= MESH_MIN && code < MESH_MAX)
                codeLine += Meshes[GetObjIdx(code, MESH_MIN)];
            else if (code >= ITM_MIN && code < ITM_MAX)
                codeLine += Items[GetObjIdx(code, ITM_MIN)];
            else if (code >= P_MAIN_PARTY && code < P_MAX)
                codeLine += Parties[GetObjIdx(code, P_MAIN_PARTY)];
            else if (code >= PT_MIN && code < PT_MAX)
                codeLine += PartyTemplates[GetObjIdx(code, PT_MIN)];
            else if (code >= MT_MIN && code < MT_MAX)
                codeLine += MissionTemplates[GetObjIdx(code, MT_MIN)];
            else if (code >= ANIM_MIN && code < ANIM_MAX)
                codeLine += Animations[GetObjIdx(code, ANIM_MIN)];
            else if (code >= SKL_MIN && code < SKL_MAX)
                codeLine += Skills[GetObjIdx(code, SKL_MIN)];
            else if (code >= SND_MIN && code < SND_MAX)
                codeLine += Sounds[GetObjIdx(code, SND_MIN)];
            else if (code >= PSYS_MIN && code < PSYS_MAX)
                codeLine += ParticleSystems[GetObjIdx(code, PSYS_MIN)];
            else if (code >= MENU_MIN && code < MENU_MAX)
                codeLine += GameMenus[GetObjIdx(code, TABLEAU_MAT_MIN)];
            else if (code >= QUEST_MIN && code < QUEST_MAX)
                codeLine += Quests[GetObjIdx(code, QUEST_MIN)];
            else if (code >= TABLEAU_MAT_MIN && code < TABLEAU_MAT_MAX)
                codeLine += TableauMaterials[GetObjIdx(code, TABLEAU_MAT_MIN)];
            else if (code >= TRACK_MIN && code < TRACK_MAX)
                codeLine += Tracks[GetObjIdx(code, TRACK_MIN)];
            else if (code <= ulong.MaxValue && code >= ANIM_MAX)
            {
                codeLine = codeLine.TrimEnd('\"') + (GetObjIdx(code, ulong.MaxValue) - 1);
                code = REG0;
            }
            else
            {
                codeLine = codeLine.TrimEnd('\"') + ConstantsFinder.FindConst(codeLine, code);
                code = REG0;
            }
            if (code < REG0 || code > REG127)
                codeLine += '\"';
            return codeLine;
        }

        private static bool ArrayContainsString(string[] array, string s)
        {
            bool found = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(s))
                {
                    found = true;
                    i = array.Length;
                }
            }
            return found;
        }

        private static int ArrayIndexOfString(string[] array, string s)
        {
            int idx = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(s))
                {
                    idx = i;
                    i = array.Length;
                }
            }
            return idx;
        }

        public static string GetCompiledCodeLines(string[] lines)
        {
            int usedLines = 0;
            string ret = string.Empty;//" " + lines.Length;
            foreach (string line in lines)
            {
                if (line.Length != 0)
                {
                    if (line.Contains("(") && line.Contains("),"))
                    {
                        ret += ' ' + GetCompiledCodeLine(line);
                        usedLines++;
                    }
                }
            }
            ret = " " + usedLines + ret;
            return ret;
        }

        private static string GetCompiledCodeLine(string codeLine)
        {
            codeLine = codeLine.Trim().TrimStart('(').TrimEnd(')', ',');
            char[] cc = codeLine.ToCharArray();
            codeLine = string.Empty;
            bool textMode = false;
            for (int i = 0; i < cc.Length; i++)
            {
                if (cc[i] == '\"')
                    textMode = !textMode;
                if (!textMode || cc[i] != ' ')
                    codeLine += cc[i].ToString();
            }

            string[] parts = codeLine.Split(',');
            List<string> partXX = new List<string> { parts[0], (parts.Length - 1).ToString() };
            for (int i = 1; i < parts.Length; i++)
                partXX.Add(parts[i]);
            parts = partXX.ToArray();

            string[] declarations = codeDeclarations.ToArray();

            if (ArrayContainsString(declarations, parts[0]))
                parts[0] = codeValue[ArrayIndexOfString(declarations, parts[0])].ToString();
            else if (parts[0].Contains("|"))
            {
                ulong border1 = (ulong)1 + int.MaxValue, border2 = border1 / 2;
                string[] tmp = parts[0].Split('|');
                ulong u = 0ul;
                if (tmp[0].Equals("neg"))
                    u += border1;
                else if (tmp[0].Equals("this_or_next"))
                    u += border2;
                if (tmp[1].Equals("this_or_next"))
                {
                    u += border2;
                    if (ArrayContainsString(declarations, tmp[2]))
                        u += codeValue[ArrayIndexOfString(declarations, tmp[2])];
                    else
                        System.Windows.Forms.MessageBox.Show("FATAL ERROR! - 0x9913", "ERROR");
                }
                else if (ArrayContainsString(declarations, tmp[1]))
                    u += codeValue[ArrayIndexOfString(declarations, tmp[1])];
                else
                    System.Windows.Forms.MessageBox.Show("FATAL ERROR! - 0x9914", "ERROR");
            }

            for (int i = 2; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim(' ', '\"');

                //if (code >= LOCAL_MIN && code < LOCAL_MAX)
                //    codeLine += localVariableInterpreter.Interpret(codeLine, code, codeIndex); // Sollte noch verfeinert werden --> EXCHANGE OBJECTS        // OLD: ":var" + (code - LOCAL_MIN + 1)
                /*else */if (!ImportantMethods.IsNumericGZ(parts[i]))
                    if (ImportantMethods.IsNumericGZ(parts[i].Replace("reg", string.Empty)))
                        parts[i] = (REG0 + ulong.Parse(parts[i].Replace("reg", string.Empty))).ToString();
                else if (ArrayContainsString(QuickStrings, parts[i]))
                    parts[i] = (QUICKSTRING_MIN + (ulong)ArrayIndexOfString(QuickStrings, parts[i])).ToString();
                else if (ArrayContainsString(GlobalVariables, parts[i]))
                    parts[i] = (GLOBAL_MIN + (ulong)ArrayIndexOfString(GlobalVariables, parts[i])).ToString();
                else if (ArrayContainsString(Scripts, parts[i]))
                    parts[i] = (SCRIPT_MIN + (ulong)ArrayIndexOfString(Scripts, parts[i])).ToString();
                else if (ArrayContainsString(SceneProps, parts[i]))
                    parts[i] = (SPR_MIN + (ulong)ArrayIndexOfString(SceneProps, parts[i])).ToString();
                else if (ArrayContainsString(Strings, parts[i]))
                    parts[i] = (STRING_MIN + (ulong)ArrayIndexOfString(Strings, parts[i])).ToString();
                else if (ArrayContainsString(Factions, parts[i]))
                    parts[i] = (FAC_MIN + (ulong)ArrayIndexOfString(Factions, parts[i])).ToString();
                else if (ArrayContainsString(Troops, parts[i]))
                    parts[i] = (TRP_PLAYER + (ulong)ArrayIndexOfString(Troops, parts[i])).ToString();
                else if (ArrayContainsString(Presentations, parts[i]))
                    parts[i] = (PRSNT_MIN + (ulong)ArrayIndexOfString(Presentations, parts[i])).ToString();
                else if (ArrayContainsString(Scenes, parts[i]))
                    parts[i] = (SCENE_MIN + (ulong)ArrayIndexOfString(Scenes, parts[i])).ToString();
                else if (ArrayContainsString(Meshes, parts[i]))
                    parts[i] = (MESH_MIN + (ulong)ArrayIndexOfString(Meshes, parts[i])).ToString();
                else if (ArrayContainsString(Items, parts[i]))
                    parts[i] = (ITM_MIN + (ulong)ArrayIndexOfString(Items, parts[i])).ToString();
                else if (ArrayContainsString(Parties, parts[i]))
                    parts[i] = (P_MAIN_PARTY + (ulong)ArrayIndexOfString(Parties, parts[i])).ToString();
                else if (ArrayContainsString(PartyTemplates, parts[i]))
                    parts[i] = (PT_MIN + (ulong)ArrayIndexOfString(PartyTemplates, parts[i])).ToString();
                else if (ArrayContainsString(MissionTemplates, parts[i]))
                    parts[i] = (MT_MIN + (ulong)ArrayIndexOfString(MissionTemplates, parts[i])).ToString();
                else if (ArrayContainsString(Animations, parts[i]))
                    parts[i] = (ANIM_MIN + (ulong)ArrayIndexOfString(Animations, parts[i])).ToString();
                else if (ArrayContainsString(Skills, parts[i]))
                    parts[i] = (SKL_MIN + (ulong)ArrayIndexOfString(Skills, parts[i])).ToString();
                else if (ArrayContainsString(Sounds, parts[i]))
                    parts[i] = (SND_MIN + (ulong)ArrayIndexOfString(Sounds, parts[i])).ToString();
                else if (ArrayContainsString(ParticleSystems, parts[i]))
                    parts[i] = (PSYS_MIN + (ulong)ArrayIndexOfString(ParticleSystems, parts[i])).ToString();
                else if (ArrayContainsString(GameMenus, parts[i]))
                    parts[i] = (MENU_MIN + (ulong)ArrayIndexOfString(GameMenus, parts[i])).ToString();
                else if (ArrayContainsString(Quests, parts[i]))
                    parts[i] = (QUEST_MIN + (ulong)ArrayIndexOfString(Quests, parts[i])).ToString();
                else if (ArrayContainsString(TableauMaterials, parts[i]))
                    parts[i] = (TABLEAU_MAT_MIN + (ulong)ArrayIndexOfString(TableauMaterials, parts[i])).ToString();
                else if (ArrayContainsString(Tracks, parts[i]))
                    parts[i] = (TRACK_MIN + (ulong)ArrayIndexOfString(Tracks, parts[i])).ToString();
                else if (ImportantMethods.IsNumericGZ(parts[i]))
                    parts[i] = (ulong.MaxValue + (ulong)(int.Parse(parts[i]) + 1)).ToString();
                else if (ConstantsFinder.ContainsConst(parts[i]))
                    parts[i] = ConstantsFinder.TranslateConst(parts[i]).ToString();
                else
                    System.Windows.Forms.MessageBox.Show("FATAL ERROR! 0x9912" + Environment.NewLine + parts[i], "ERROR");
            }

            codeLine = string.Empty;
            foreach (string part in parts)
                codeLine += part + ' ';

            return codeLine.TrimEnd();
        }

        private static MissionTemplate DecompileMissionTemplateCode(string[] headerX, List<string[]> entryPoints, List<string[]> triggers)
        {
            MissionTemplate newMissionTemplate = null;
            if (headerX.Length == 4)
            {
                newMissionTemplate = new MissionTemplate(headerX);
                foreach (string[] array in entryPoints)
                    newMissionTemplate.AddEntryPoint(new Entrypoint(array));
                foreach (string[] array in triggers)
                    newMissionTemplate.AddTrigger(DecompileTrigger(array));
            }
            /*else
            {
                Console.WriteLine("ERROR ON MISSION_TEMPLATE: " + headerX);
                newMissionTemplate = null;
            }*/
            return newMissionTemplate;
        }

        private static Trigger DecompileTrigger(string[] array)
        {
            int mode = 1, x;
            string[] scriptLines, code;
            Trigger trigger = new Trigger(double.Parse(Repl_DotWComma(array[0])), double.Parse(Repl_DotWComma(array[1])), double.Parse(Repl_DotWComma(array[2])));
            for (int i = 4; i < array.Length; i++)
            {
                if (!array[i].Equals(string.Empty))
                {
                    x = 0;
                    for (int z = i + 1; z < array.Length; z++)
                        if (array[z].Equals(string.Empty))
                            z = array.Length;
                        else
                            x++;

                    if (x > 0)
                    {
                        scriptLines = new string[x + 1];
                        for (int z = 0; z < scriptLines.Length; z++)
                            scriptLines[z] = array[z + i];
                        code = new string[int.Parse(array[i]) + 1];
                        code[0] = "TRIGGER_MODE=" + mode;
                        code = GetStringArrayStartFromIndex(DecompileScriptCode(code, scriptLines), 1);
                    }
                    else if (x == 0)
                        code = new string[x];
                    else
                        code = null;

                    if (mode == 1)
                    {
                        trigger.ConditionBlock = code;
                        for (int b = i; b < array.Length; b++)
                        {
                            if (array[b].Equals(string.Empty))
                            {
                                i = b - 1;
                                b = array.Length;
                            }
                        }
                    }
                    else if (mode == 2)
                    {
                        trigger.ConsequencesBlock = code;
                        i = array.Length;
                    }
                }
                else
                    mode++;
            }
            return trigger;
        }

        #endregion

        #region SIMPLE_SUPPORT_METHODS

        public static string Repl_DotWComma(string s) { return s.Replace('.', ','); }

        public static string Repl_CommaWDot(string s) { return s.Replace(',', '.'); }

        public static string Repl_MinusZero(double d) { return Repl_CommaWDot(d.ToString()).Replace(MINUS_ZERO_ALT, MINUS + "0.000000"); }

        public static int CountCharInString(string s, char c)
        {
            int x = 0;
            char[] cc = s.ToCharArray();
            foreach (char cs in cc)
                if (cs == c)
                    x++;
            return x;
        }

        public static string[] ReInitializeArrayAndRemoveEmpty(string[] orgArray, int count, int splitAfterIndex)
        {
            int x = 0, t = 0, t2 = 0;
            string[] newArray = new string[count];
            for (int i = 0; i < newArray.Length; i++)
                newArray[i] = string.Empty;
            x = 0;
            t2 = splitAfterIndex;
            for (int j = 0; j < newArray.Length; j++)
            {
                for (int i = x; i < orgArray.Length; i++)
                {
                    if (!orgArray[i].Equals(string.Empty))
                        newArray[j] += ' ' + orgArray[i];
                    else
                        t++;
                    if (t == t2)
                    {
                        x = i + 1;
                        t2 = t2 + splitAfterIndex - 1;
                        i = orgArray.Length;
                    }
                }
            }
            return newArray;
        }

        public static void SetModPath(string modPath) { CodeReader.modPath = modPath; }

        public static void Reset(bool clearAllLists = false, bool resetModpath = false)
        {
            objectsExpected = 0;
            objectsRead = 0;
            if (resetModpath)
                modPath = string.Empty;
            if (clearAllLists)
                ClearLists();
        }

        public static void ClearLists()
        {
            codeDeclarations.Clear();
            codeValue.Clear();
            elements.Clear(); // before here were almost all other lists which are now include in elements here!
        }

        #endregion
    }
}