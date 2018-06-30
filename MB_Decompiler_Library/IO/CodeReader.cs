using System.IO;
using System.Collections.Generic;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using System;
using importantLib;

namespace MB_Decompiler_Library.IO
{
    public class CodeReader
    {
        #region Attributes

        private readonly string filepath;
        private static int objectsExpected;

        //private static List<List<int>> unusedIndicies;//, unusedIndiciesEXTRA;
        private static LocalVariableInterpreter localVariableInterpreter;

        #endregion

        #region References

        private static List<string> codeDeclarations = new List<string>();
        private static List<ulong> codeValue = new List<ulong>();

        public static string[] CodeDeclarations { get { return codeDeclarations.ToArray(); } }
        public static ulong[] CodeValues { get { return codeValue.ToArray(); } }

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

        private static List<List<string>> elements = new List<List<string>>();
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static List<string>[] Elements { get { return elements.ToArray(); } }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static List<string> Scripts { get { return elements[0]; } }
        public static List<string> MissionTemplates { get { return elements[1]; } }
        public static List<string> Presentations { get { return elements[2]; } }
        public static List<string> GameMenus { get { return elements[3]; } }
        public static List<string> Troops { get { return elements[4]; } }
        public static List<string> Items { get { return elements[5]; } }
        public static List<string> Strings { get { return elements[6]; } }
        //SimpleTriggers    - NONE
        //Triggers          - NONE
        public static List<string> InfoPages { get { return elements[7]; } }
        public static List<string> Meshes { get { return elements[8]; } }
        public static List<string> Tracks { get { return elements[9]; } }
        public static List<string> Quests { get { return elements[10]; } }
        public static List<string> Sounds { get { return elements[11]; } }
        public static List<string> SceneProps { get { return elements[12]; } }
        public static List<string> TableauMaterials { get { return elements[13]; } }
        public static List<string> MapIcons { get { return elements[14]; } }
        //Dialogs           - NONE
        public static List<string> Factions { get { return elements[15]; } }
        public static List<string> Animations { get { return elements[16]; } }
        public static List<string> PartyTemplates { get { return elements[17]; } }
        public static List<string> Parties { get { return elements[18]; } }
        public static List<string> Skills { get { return elements[19]; } }
        public static List<string> PostFXParams { get { return elements[20]; } }
        //Skins             - NONE
        public static List<string> ParticleSystems { get { return elements[21]; } }
        public static List<string> Scenes { get { return elements[22]; } }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static List<string> QuickStrings { get { return elements[23]; } } // Always last if possible
        public static List<string> GlobalVariables { get { return elements[24]; } } // Always last if possible

        #endregion

        public static string ModPath { get; private set; } = string.Empty;

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

        public static int ObjectsRead { get; private set; }

        #endregion

        #region Konstanten

        public const string MINUS_ZERO = "-0,0000001337";
        public const string MINUS_ZERO_ALT = "-1.337E-07";
        public const string MINUS = "-";

        // ÄNDERN ?!
        public static readonly List<string> Files = new List<string> { "scripts.txt", "mission_templates.txt", "presentations.txt", "menus.txt", "troops.txt", "item_kinds1.txt", "strings.txt", "simple_triggers.txt",
                "triggers.txt", "info_pages.txt", "meshes.txt", "music.txt", "quests.txt", "sounds.txt", "scene_props.txt", "tableau_materials.txt", "map_icons.txt", "conversation.txt",
                "factions.txt", "actions.txt", "party_templates.txt", "parties.txt", "skills.txt", "postfx.txt", "skins.txt", "particle_systems.txt", "scenes.txt"
        };

        private static string[] UNUSED_CODES = { "else_try_begin", "end_try" };

        public const ulong LOCAL_MIN = 1224979098644774912;
        public const ulong LOCAL_MAX = 1224979098644775040;//max. 128 local variables

        public const ulong REG0 = 72057594037927936;
        public const ulong REG127 = 72057594037928063;//max. 128 registers

        public const ulong QUICKSTRING_MIN = 1585267068834414592;
        public const ulong QUICKSTRING_MAX = 1600000000000000000;

        public const ulong GLOBAL_MIN = 144115188075855872;
        public const ulong GLOBAL_MAX = 145000000000000000;

        #region TYPES

        public const ulong TRP_PLAYER = 360287970189639680;
        public const ulong TROOP_MAX = 370000000000000000;

        public const ulong SCRIPT_MIN = 936748722493063168;
        public const ulong SCRIPT_MAX = 940000000000000000;

        public const ulong STRING_MIN = 216172782113783808;
        public const ulong STRING_MAX = 217000000000000000;

        public const ulong SPR_MIN = 1080863910568919040;
        public const ulong SPR_MAX = 1100000000000000000;

        public const ulong PRSNT_MIN = 1513209474796486656;
        public const ulong PRSNT_MAX = 1513210000000000000;

        public const ulong FAC_MIN = 432345564227567616;
        public const ulong FAC_MAX = 433000000000000000;

        public const ulong P_MAIN_PARTY = 648518346341351424;
        public const ulong P_MAX = 648600000000000000;

        public const ulong ITM_MIN = 288230376151711744;
        public const ulong ITM_MAX = 290000000000000000;

        public const ulong SCENE_MIN = 720575940379279360;
        public const ulong SCENE_MAX = 720575940380000000;

        public const ulong MESH_MIN = 1441151880758558720;
        public const ulong MESH_MAX = 1450000000000000000;

        public const ulong PT_MIN = 576460752303423488;
        public const ulong PT_MAX = 576500000000000000;

        public const ulong MT_MIN = 792633534417207296;
        public const ulong MT_MAX = 792700000000000000;

        public const ulong SKL_MIN = 1369094286720630784;
        public const ulong SKL_MAX = 1369094286720700000;

        public const ulong SND_MIN = 1152921504606846976;
        public const ulong SND_MAX = 1152921504607000000;

        public const ulong PSYS_MIN = 1008806316530991104;
        public const ulong PSYS_MAX = 1009000000000000000;

        public const ulong MENU_MIN = 864691128455135232;
        public const ulong MENU_MAX = 865000000000000000;

        public const ulong QUEST_MIN = 504403158265495552;
        public const ulong QUEST_MAX = 504500000000000000;

        public const ulong TABLEAU_MAT_MIN = 1729382256910270464;
        public const ulong TABLEAU_MAT_MAX = 1730000000000000000;

        public const ulong TRACK_MIN = 1657324662872342528;
        public const ulong TRACK_MAX = 1660000000000000000;

        public const ulong MAP_ICON_MIN = 129703669268270;
        public const ulong MAP_ICON_MAX = 130000000000000;

        //INFO_PAGE
        //DIALOG
        //POST_FX

        public const ulong ANIM_MIN = 1801439850948198400;
        public const ulong ANIM_MAX = 1810000000000000000;//public const ulong ANIM_MAX = ulong.MaxValue - int.MaxValue;

        #endregion

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

        public static int GetTypeIndexFromID(List<string> typeList, string id)
        {
            int index = -1;
            for (int i = 0; i < typeList.Count; i++)
            {
                if (typeList[i].Equals(id))
                {
                    index = i;
                    i = typeList.Count;
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

        private static List<string> ReadGlobalVariables()
        {
            List<string> globalVariables = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "variables.txt"))
                while (!sr.EndOfStream)
                    globalVariables.Add('$' + sr.ReadLine());
            return globalVariables;
        }

        private static List<string> ReadScriptNames()
        {
            string s;
            List<string> scriptNames = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "scripts.txt"))
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
            return scriptNames;
        }

        private static List<string> ReadQuickStrings()
        {
            List<string> quick_strings = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "quick_strings.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                    quick_strings.Add('@' + sr.ReadLine().Split(' ')[1].Replace('_',' ').Replace("\"", "\\\""));
            }
            return quick_strings;
        }

        private static List<string> ReadPresentations()
        {
            string s;
            List<string> presentations = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "presentations.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("prsnt_"))
                        presentations.Add(s);
                }
            }
            return presentations;
        }

        private static List<string> ReadStrings()
        {
            string tmp;
            List<string> strings = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "strings.txt"))
            {
                sr.ReadLine();
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine().Split(' ')[0].Trim();
                    if (tmp.Length != 0)
                        strings.Add(tmp);
                }
            }
            return strings;
        }

        private static List<string> ReadItems()
        {
            string s;
            List<string> items = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "item_kinds1.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ')[0];
                    if (s.Contains("itm_"))
                        items.Add(s);
                }
            }
            return items;
        }

        private static List<string> ReadFactions()
        {
            string[] s;
            List<string> factions = new List<string>();
            //factions.Add("fac_no_faction"); // Why was this used before???
            using (StreamReader sr = new StreamReader(ModPath + "factions.txt"))
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
            return factions;
        }

        private static List<string> ReadTroops()
        {
            string s;
            List<string> troops = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "troops.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("trp_"))
                        troops.Add(s);
                }
            }
            return troops;
        }

        private static List<string> ReadSceneProps()
        {
            string s;
            List<string> scene_props = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "scene_props.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("spr_"))
                        scene_props.Add(s);
                }
            }
            return scene_props;
        }

        private static List<string> ReadParties()
        {
            string[] s;
            List<string> parties = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "parties.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ');
                    if (s.Length > 3)
                        parties.Add(s[3]);
                }
            }
            return parties;
        }

        private static List<string> ReadPartyTemplates()
        {
            string s;
            List<string> party_templates = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "party_templates.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("pt_"))
                        party_templates.Add(s);
                }
            }
            return party_templates;
        }

        private static List<string> ReadMeshes()
        {
            string s;
            List<string> meshes = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "meshes.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("mesh_"))
                        meshes.Add(s);
                }
            }
            return meshes;
        }

        private static List<string> ReadSkills()
        {
            string s;
            List<string> skills = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "skills.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("skl_"))
                        skills.Add(s);
                }
            }
            return skills;
        }

        private static List<string> ReadMissionTemplates()
        {
            string s;
            List<string> mission_templates = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "mission_templates.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("mst_"))
                        mission_templates.Add('m' + s.Substring(2));
                }
            }
            return mission_templates;
        }

        private static List<string> ReadSounds()
        {
            string s;
            List<string> sounds = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "sounds.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("snd_"))
                        sounds.Add(s);
                }
            }
            return sounds;
        }

        private static List<string> ReadParticles()
        {
            string s;
            List<string> particles = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "particle_systems.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("psys_"))
                        particles.Add(s);
                }
            }
            return particles;
        }

        private static List<string> ReadGameMenus()
        {
            string s;
            List<string> menus = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "menus.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split()[0];
                    if (s.Contains("menu_"))
                        if (s.Substring(0, 4).Equals("menu"))
                            menus.Add("m" + s.Substring(2));
                }
            }
            return menus;
        }

        private static List<string> ReadQuests()
        {
            string s;
            List<string> quests = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "quests.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("qst_"))
                        quests.Add(s);
                }
            }
            return quests;
        }

        private static List<string> ReadTableauMaterials()
        {
            string s;
            List<string> tableau_materials = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "tableau_materials.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("tab_"))
                        tableau_materials.Add(s.Replace("tab_", "tableau_"));
                }
            }
            return tableau_materials;
        }

        private static List<string> ReadAnimations()
        {
            string s;
            List<string> animations = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "actions.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart().Split(' ')[0];
                    if (!ImportantMethods.IsNumericFKZ2(s))
                        animations.Add("anim_" + s);
                }
            }
            return animations;
        }

        private static List<string> ReadScenes()
        {
            string s;
            List<string> scenes = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "scenes.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("scn_"))
                        scenes.Add(s);
                }
            }
            return scenes;
        }

        private static List<string> ReadTracks()
        {
            string s;
            List<string> tracks = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "music.txt"))
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
            return tracks;
        }

        private static List<string> ReadMapIcons()
        {
            string s;
            List<string> map_icons = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "map_icons.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().TrimStart('-').Split()[0];
                    if (!ImportantMethods.IsNumeric(s, true) && !s.Trim().Equals(string.Empty))
                        map_icons.Add("icon_" + s);
                }
            }
            return map_icons;
        }

        private static List<string> ReadInfoPages()
        {
            string s;
            List<string> infoPages = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "info_pages.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("ip_"))
                        infoPages.Add(s);
                }
            }
            return infoPages;
        }

        private static List<string> ReadPostFXParams()
        {
            string s;
            List<string> postFXParams = new List<string>();
            using (StreamReader sr = new StreamReader(ModPath + "postfx.txt"))
            {
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Split(' ')[0];
                    if (s.Contains("pfx_"))
                        postFXParams.Add(s);
                }
            }
            return postFXParams;
        }

        #endregion

        #region Main Reader

        public static int Overflow { get { return ObjectsRead - objectsExpected; } }

        public static string RemNTrimAllXtraSp(string s)
        {
            s = s.Replace('\t', ' ').Trim();
            while (s.Contains("  "))
                s = s.Replace("  ", " ");
            return s;
        }

        public List<Skriptum> ReadObjectType(int objectTypeID)
        {
            return ReadObjectType((Skriptum.ObjectType)objectTypeID);
        }

        public List<Skriptum> ReadObjectType(Skriptum.ObjectType objectType)
        {
            List<Skriptum> skriptums = new List<Skriptum>();
            if (objectType == Skriptum.ObjectType.Script)
                foreach (Skriptum s in ReadScript())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.MissionTemplate)
                foreach (Skriptum s in ReadMissionTemplate())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.Presentation)
                foreach (Presentation p in ReadPresentation())
                    skriptums.Add(p);
            else if (objectType == Skriptum.ObjectType.GameMenu)
                foreach (GameMenu g in ReadGameMenu())
                    skriptums.Add(g);
            else if (objectType == Skriptum.ObjectType.GameString)
                foreach (GameString s in ReadString())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.SimpleTrigger)
                foreach (SimpleTrigger t in ReadSimpleTrigger())
                    skriptums.Add(t);
            else if (objectType == Skriptum.ObjectType.Trigger)
                foreach (Trigger t in ReadTrigger())
                    skriptums.Add(t);
            else if (objectType == Skriptum.ObjectType.InfoPage)
                foreach (InfoPage p in ReadInfoPage())
                    skriptums.Add(p);
            else if (objectType == Skriptum.ObjectType.Sound)
                foreach (Sound s in ReadSound())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.Quest)
                foreach (Quest q in ReadQuest())
                    skriptums.Add(q);
            else if (objectType == Skriptum.ObjectType.Scene)
                foreach (Scene s in ReadScene())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.SceneProp)
                foreach (SceneProp s in ReadSceneProp())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.TableauMaterial)
                foreach (TableauMaterial t in ReadTableauMaterial())
                    skriptums.Add(t);
            else if (objectType == Skriptum.ObjectType.Music)
                foreach (Music m in ReadMusic())
                    skriptums.Add(m);
            else if (objectType == Skriptum.ObjectType.Mesh)
                foreach (Mesh m in ReadMesh())
                    skriptums.Add(m);
            else if (objectType == Skriptum.ObjectType.Faction)
                foreach (Faction f in ReadFaction())
                    skriptums.Add(f);
            else if (objectType == Skriptum.ObjectType.MapIcon)
                foreach (MapIcon m in ReadMapIcon())
                    skriptums.Add(m);
            else if (objectType == Skriptum.ObjectType.Animation)
                foreach (Animation a in ReadAnimation())
                    skriptums.Add(a);
            else if (objectType == Skriptum.ObjectType.PartyTemplate)
                foreach (PartyTemplate p in ReadPartyTemplate())
                    skriptums.Add(p);
            else if (objectType == Skriptum.ObjectType.Dialog)
                foreach (Dialog d in ReadDialog())
                    skriptums.Add(d);
            else if (objectType == Skriptum.ObjectType.Party)
                foreach (Party p in ReadParty())
                    skriptums.Add(p);
            else if (objectType == Skriptum.ObjectType.Skill)
                foreach (Skill s in ReadSkill())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.PostFX)
                foreach (PostFX p in ReadPostFX())
                    skriptums.Add(p);
            else if (objectType == Skriptum.ObjectType.ParticleSystem)
                foreach (ParticleSystem p in ReadParticleSystem())
                    skriptums.Add(p);
            else if (objectType == Skriptum.ObjectType.Skin)
                foreach (Skin s in ReadSkin())
                    skriptums.Add(s);
            else if (objectType == Skriptum.ObjectType.Troop)
                foreach (Troop t in ReadTroop())
                    skriptums.Add(t);
            else if (objectType == Skriptum.ObjectType.Item)
                foreach (Item itm in ReadItem())
                    skriptums.Add(itm);
            return skriptums;
        }

        public static List<List<Skriptum>> ReadAllObjects()
        {
            Reset();
            List<List<Skriptum>> objects = new List<List<Skriptum>>();
            for (int i = 0; i < Files.Count; i++)
                objects.Add(new CodeReader(ModPath + Files[i]).ReadObjectType(i));
            return objects;
        }

        public List<MissionTemplate> ReadMissionTemplate()
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
            ObjectsRead += missionTemplates.Count;
            return missionTemplates;
        }

        public List<Presentation> ReadPresentation()
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
                            SimpleTrigger simpleTrigger = new SimpleTrigger(scriptLines[0]);
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
            ObjectsRead += presentations.Count;
            return presentations;
        }

        public List<GameMenu> ReadGameMenu()
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
            ObjectsRead += game_menus.Count;
            return game_menus;
        }

        public List<Script> ReadScript()
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
            ObjectsRead += scripts.Count;
            return scripts;
        }

        public List<Troop> ReadTroop()
        {
            List<Troop> troops = new List<Troop>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int maxTroops = int.Parse(sr.ReadLine().TrimEnd());
                objectsExpected += maxTroops;
                for (int i = 0; i < maxTroops; i++)
                {
                    string[] tempus = new string[7];
                    for (int j = 0; j < tempus.Length; j++)
                        tempus[j] = sr.ReadLine();
                    troops.Add(new Troop(tempus));
                }
            }
            ObjectsRead += troops.Count;
            return troops;
        }

        public List<Item> ReadItem()
        {
            int i = -1;
            string tempus;
            List<string> lines = new List<string>();
            List<Item> items = new List<Item>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int maxItems = int.Parse(sr.ReadLine());
                objectsExpected += maxItems;
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
                        items.Add(new Item(lines.ToArray()));
                    }
                }
            }

            ObjectsRead += items.Count;
            return items;
        }

        public List<GameString> ReadString()
        {
            List<GameString> strings = new List<GameString>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    strings.Add(new GameString(sr.ReadLine().Substring(4).Split()));
            }
            ObjectsRead += strings.Count;
            return strings;
        }

        public List<SimpleTrigger> ReadSimpleTrigger()
        {
            string[] scriptLines;
            List<SimpleTrigger> simple_triggers = new List<SimpleTrigger>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    scriptLines = sr.ReadLine().Split();
                    simple_triggers.Add(new SimpleTrigger(scriptLines[0]));
                    string[] tmp = new string[int.Parse(scriptLines[2]) + 1];
                    tmp[0] = "SIMPLE_TRIGGER";
                    scriptLines = GetStringArrayStartFromIndex(scriptLines, 2, 1);
                    simple_triggers[i].ConsequencesBlock = GetStringArrayStartFromIndex(DecompileScriptCode(tmp, scriptLines), 1);
                }
            }
            ObjectsRead += simple_triggers.Count;
            return simple_triggers;
        }

        public List<Trigger> ReadTrigger()
        {
            List<Trigger> triggers = new List<Trigger>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    triggers.Add(DecompileTrigger(sr.ReadLine().Split()));
            }
            ObjectsRead += triggers.Count;
            return triggers;
        }

        public List<InfoPage> ReadInfoPage()
        {
            List<InfoPage> info_pages = new List<InfoPage>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    info_pages.Add(new InfoPage(sr.ReadLine().Split()));
            }
            ObjectsRead += info_pages.Count;
            return info_pages;
        }

        public List<Mesh> ReadMesh()
        {
            List<Mesh> meshes = new List<Mesh>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    meshes.Add(new Mesh(sr.ReadLine().Split()));
            }
            ObjectsRead += meshes.Count;
            return meshes;
        }

        public List<Music> ReadMusic()
        {
            List<Music> musicTracks = new List<Music>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    string[] sts = sr.ReadLine().Split();
                    musicTracks.Add(new Music(new string[] { Tracks[i], sts[0], sts[1], sts[2] }));
                }
            }
            ObjectsRead += musicTracks.Count;
            return musicTracks;
        }

        public List<Quest> ReadQuest()
        {
            List<Quest> quests = new List<Quest>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    quests.Add(new Quest(sr.ReadLine().Split()));
            }
            ObjectsRead += quests.Count;
            return quests;
        }

        public List<Sound> ReadSound()
        {
            string line;
            List<Sound> sounds = new List<Sound>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                sr.ReadLine();
                do { line = sr.ReadLine(); } while (!ImportantMethods.IsNumericFKZ2(line));
                int count = int.Parse(line);
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    sounds.Add(new Sound(sr.ReadLine().Split()));
            }
            ObjectsRead += sounds.Count;
            return sounds;
        }

        public List<Scene> ReadScene()
        {
            string firstLine;
            string[] otherScenes, chestTroops, tmp;
            List<Scene> scenes = new List<Scene>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int tmpX;
                int count = int.Parse(sr.ReadLine().TrimStart());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    firstLine = sr.ReadLine();
                    tmp = sr.ReadLine().Substring(2).TrimEnd().Replace("  ", " ").Split();
                    otherScenes = new string[int.Parse(tmp[0])];
                    for (int j = 0; j < otherScenes.Length; j++)
                    {
                        tmpX = int.Parse(tmp[j + 1]);
                        if (Scenes.Count > tmpX && tmpX >= 0)
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
                    scenes.Add(new Scene(firstLine.Split(), otherScenes, chestTroops, sr.ReadLine().Trim()));
                }
            }
            ObjectsRead += scenes.Count;
            return scenes;
        }

        public List<TableauMaterial> ReadTableauMaterial()
        {
            List<TableauMaterial> tableaus = new List<TableauMaterial>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    tableaus[i] = new TableauMaterial(sr.ReadLine().TrimEnd().Split());
            }
            ObjectsRead += tableaus.Count;
            return tableaus;
        }

        public List<SceneProp> ReadSceneProp()
        {
            int tCount;
            string[] lines;
            List<SceneProp> sceneProps = new List<SceneProp>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().TrimStart());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    string tmpSSS = sr.ReadLine().Replace("  ", " ").Replace('\t', ' ');
                    while (tmpSSS.Contains("  "))
                        tmpSSS = tmpSSS.Replace("  ", " ");
                    lines = tmpSSS.Split();

                    sceneProps.Add(new SceneProp(lines));
                    tCount = int.Parse(lines[lines.Length - 1]);
                    if (tCount > 0)
                    {
                        SimpleTrigger[] s_triggers = new SimpleTrigger[tCount];
                        for (int j = 0; j < s_triggers.Length; j++)
                        {
                            lines = sr.ReadLine().Split();
                            s_triggers[j] = new SimpleTrigger(lines[0]);
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
            ObjectsRead += sceneProps.Count;
            return sceneProps;
        }

        public List<Faction> ReadFaction()
        {
            int c;
            string line;
            List<Faction> factions = new List<Faction>();
            Faction.ResetIDs();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    do { c = sr.Read(); } while ((char)c != 'f');
                    //_factions[i] = new Faction(((char)c + sr.ReadLine().TrimEnd()).Split());
                    //string[] sp = sr.ReadLine().Trim().Replace("  ", " ").Split();
                    //double[] dd = new double[sp.Length];
                    //for (int j = 0; j < sp.Length; j++)
                    //    dd[j] = double.Parse(Repl_DotWComma(sp[j]));
                    //_factions[i].Relations = dd;
                    string firstLine = Char.ConvertFromUtf32(c) + sr.ReadLine().TrimEnd();
                    string secondLine = sr.ReadLine().Trim().Replace("  ", " ");
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
                        line = Char.ConvertFromUtf32(c);
                        foreach (string s in tmp)
                            line += ' ' + s;
                    }
                    else
                        line = Char.ConvertFromUtf32(c);
                    factions.Add(new Faction(new string[] { firstLine, secondLine, line }));
                }
            }
            ObjectsRead += factions.Count;
            return factions;
        }

        public List<MapIcon> ReadMapIcon()
        {
            int tCount;
            string[] sp;
            List<MapIcon> mapIcons = new List<MapIcon>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    sp = sr.ReadLine().Split();
                    tCount = int.Parse(sp[sp.Length - 1]);
                    mapIcons.Add(new MapIcon(sp));
                    if (tCount > 0)
                    {
                        SimpleTrigger[] s_triggers = new SimpleTrigger[tCount];
                        for (int j = 0; j < s_triggers.Length; j++)
                        {
                            sp = sr.ReadLine().Split();
                            s_triggers[j] = new SimpleTrigger(sp[0]);
                            string[] tmp = new string[int.Parse(sp[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            sp = GetStringArrayStartFromIndex(sp, 2, 1);
                            s_triggers[j].ConsequencesBlock = GetStringArrayStartFromIndex(DecompileScriptCode(tmp, sp), 1);
                        }
                        mapIcons[i].SimpleTriggers = s_triggers;
                    }
                    sr.ReadLine();//are there some values here in other versions?
                    sr.ReadLine();//are there some values here in other versions?
                }
            }
            ObjectsRead += mapIcons.Count;
            return mapIcons;
        }

        public List<Animation> ReadAnimation()
        {
            string[] sp;
            List<Animation> animations = new List<Animation>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    sp = sr.ReadLine().Substring(1).Replace("  ", " ").Split();
                    animations.Add(new Animation(sp));
                    AnimationSequence[] sequences = new AnimationSequence[int.Parse(sp[sp.Length - 1])];
                    for (int j = 0; j < sequences.Length; j++)
                        sequences[j] = new AnimationSequence(sr.ReadLine().Trim().Replace("  ", " ").Split());
                    animations[i].Sequences = sequences;
                }
            }
            ObjectsRead += animations.Count;
            return animations;
        }

        public List<PartyTemplate> ReadPartyTemplate()
        {
            List<PartyTemplate> partyTemplates = new List<PartyTemplate>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    partyTemplates.Add(new PartyTemplate(sr.ReadLine().TrimEnd().Split()));
            }
            ObjectsRead += partyTemplates.Count;
            return partyTemplates;
        }

        public List<Dialog> ReadDialog()
        {
            List<Dialog> dialogs = new List<Dialog>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    dialogs.Add(new Dialog(sr.ReadLine().TrimEnd().Split()));
            }
            ObjectsRead += dialogs.Count;
            return dialogs;
        }

        public List<Party> ReadParty()
        {
            string line;
            List<Party> parties = new List<Party>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().Split()[0]);
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                {
                    line = sr.ReadLine().Trim();
                    double degrees = double.Parse(Repl_DotWComma(sr.ReadLine()));
                    degrees = Math.Round(degrees / (3.1415926 / 180d), 4);
                    line += " " + degrees; // maybe check if values are still correct!
                    parties.Add(new Party(line.Split()));
                }
            }
            ObjectsRead += parties.Count;
            return parties;
        }

        public List<Skill> ReadSkill()
        {
            List<Skill> skills = new List<Skill>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                int count = int.Parse(sr.ReadLine().Split()[0]);
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    skills.Add(new Skill(sr.ReadLine().Split()));
            }
            ObjectsRead += skills.Count;
            return skills;
        }

        public List<PostFX> ReadPostFX()
        {
            List<PostFX> postfxs = new List<PostFX>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().Split()[0]);
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
                    postfxs.Add(new PostFX(sr.ReadLine()));
            }
            ObjectsRead += postfxs.Count;
            return postfxs;
        }

        public List<ParticleSystem> ReadParticleSystem()
        {
            List<ParticleSystem> particleSystems = new List<ParticleSystem>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
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
                    particleSystems.Add(new ParticleSystem(list));
                }
            }
            ObjectsRead += particleSystems.Count;
            return particleSystems;
        }

        public List<Skin> ReadSkin()
        {
            List<Skin> skins = new List<Skin>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                objectsExpected += count;
                for (int i = 0; i < count && !sr.EndOfStream; i++)
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
                    skins.Add(new Skin(list));
                }
            }
            ObjectsRead += skins.Count;
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
                codeLine += QuickStrings[GetObjIdx(code, QUICKSTRING_MIN)];
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
            else if (code >= MAP_ICON_MIN && code < MAP_ICON_MAX)
                codeLine += MapIcons[GetObjIdx(code, MAP_ICON_MAX)];
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
            Trigger trigger = new Trigger(array[0], array[1], array[2]);
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

        public static void SetModPath(string modPath) { CodeReader.ModPath = modPath; }

        public static void Reset(bool clearAllLists = false, bool resetModpath = false)
        {
            objectsExpected = 0;
            ObjectsRead = 0;
            if (resetModpath)
                ModPath = string.Empty;
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