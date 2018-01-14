using importantLib;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;
using System;
using System.Collections.Generic;
using System.IO;
using static skillhunter.Skriptum;

namespace MB_Decompiler_Library.IO
{
    public class SourceReader
    {
        private static string modPath = string.Empty; //SHOULD BE string.Empty in the Release Version!!!

        //change this to source files
        public static readonly string[] Files = { "scripts.txt", "mission_templates.txt", "presentations.txt", "menus.txt", "troops.txt", "item_kinds1.txt", "strings.txt", "simple_triggers.txt",
                "triggers.txt", "info_pages.txt", "meshes.txt", "music.txt", "quests.txt", "sounds.txt", "scene_props.txt", "tableau_materials.txt", "map_icons.txt", "conversation.txt",
                "factions.txt", "actions.txt", "party_templates.txt", "parties.txt", "skills.txt", "postfx.txt", "skins.txt", "particle_systems.txt", "scenes.txt" };

        private static LocalVariableInterpreter localVariableInterpreter;

        private string filePath;

        public SourceReader(string filePath = null)
        {
            this.filePath = filePath;
        }

        public static void SetModPath(string modPath) { SourceReader.modPath = modPath; }

        public static string ModPath { get { return modPath; } }

        private static string GetNegationCode(string code)
        {
            string ret;
            if (code.Equals("lt"))
                ret = "neg|ge";
            else if (code.Equals("neq"))
                ret = "neg|eq";
            else if (code.Equals("le"))
                ret = "neg|gt";
            else
                ret = code;
            return ret;
        }

        public static string GetCompiledCodeLines(string[] lines)
        {
            int usedLines = 0;
            string ret = string.Empty;

            localVariableInterpreter = new LocalVariableInterpreter();

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

            string[] declarations = CodeReader.CodeDeclarations;

            if (ImportantMethods.ArrayContainsString(declarations, parts[0]))
                parts[0] = CodeReader.CodeValues[ImportantMethods.ArrayIndexOfString(declarations, parts[0])].ToString();
            else if (parts[0].Contains("|"))
            {
                ulong border1 = (ulong)1 + int.MaxValue, border2 = border1 / 2;
                string[] tmp = parts[0].Split('|');
                ulong u = 0ul;
                tmp[0] = GetNegationCode(tmp[0]); 
                if (tmp[0].Equals("neg"))
                    u += border1;
                else if (tmp[0].Equals("this_or_next"))
                    u += border2;
                if (tmp[1].Equals("this_or_next"))
                {
                    u += border2;
                    if (ImportantMethods.ArrayContainsString(declarations, tmp[2]))
                        u += CodeReader.CodeValues[ImportantMethods.ArrayIndexOfString(declarations, tmp[2])];
                    else
                        Console.WriteLine("FATAL ERROR! - 0x9913", "ERROR");
                }
                else if (ImportantMethods.ArrayContainsString(declarations, tmp[1]))
                    u += CodeReader.CodeValues[ImportantMethods.ArrayIndexOfString(declarations, tmp[1])];
                else
                    Console.WriteLine("FATAL ERROR! - 0x9914", "ERROR");
            }

            for (int i = 2; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim(' ', '\"');

                if (!ImportantMethods.IsNumericGZ(parts[i]))
                {
                    if (ImportantMethods.IsNumericGZ(parts[i].Replace("reg", string.Empty)))
                        parts[i] = (CodeReader.REG0 + ulong.Parse(parts[i].Replace("reg", string.Empty))).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.QuickStrings, parts[i]))
                        parts[i] = (CodeReader.QUICKSTRING_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.QuickStrings, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.GlobalVariables, parts[i]))
                        parts[i] = (CodeReader.GLOBAL_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.GlobalVariables, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Scripts, parts[i]))
                        parts[i] = (CodeReader.SCRIPT_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Scripts, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.SceneProps, parts[i]))
                        parts[i] = (CodeReader.SPR_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.SceneProps, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Strings, parts[i]))
                        parts[i] = (CodeReader.STRING_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Strings, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Factions, parts[i]))
                        parts[i] = (CodeReader.FAC_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Factions, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Troops, parts[i]))
                        parts[i] = (CodeReader.TRP_PLAYER + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Troops, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Presentations, parts[i]))
                        parts[i] = (CodeReader.PRSNT_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Presentations, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Scenes, parts[i]))
                        parts[i] = (CodeReader.SCENE_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Scenes, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Meshes, parts[i]))
                        parts[i] = (CodeReader.MESH_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Meshes, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Items, parts[i]))
                        parts[i] = (CodeReader.ITM_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Items, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Parties, parts[i]))
                        parts[i] = (CodeReader.P_MAIN_PARTY + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Parties, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.PartyTemplates, parts[i]))
                        parts[i] = (CodeReader.PT_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.PartyTemplates, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.MissionTemplates, parts[i]))
                        parts[i] = (CodeReader.MT_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.MissionTemplates, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Animations, parts[i]))
                        parts[i] = (CodeReader.ANIM_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Animations, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Skills, parts[i]))
                        parts[i] = (CodeReader.SKL_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Skills, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Sounds, parts[i]))
                        parts[i] = (CodeReader.SND_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Sounds, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.ParticleSystems, parts[i]))
                        parts[i] = (CodeReader.PSYS_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.ParticleSystems, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.GameMenus, parts[i]))
                        parts[i] = (CodeReader.MENU_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.GameMenus, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Quests, parts[i]))
                        parts[i] = (CodeReader.QUEST_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Quests, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.TableauMaterials, parts[i]))
                        parts[i] = (CodeReader.TABLEAU_MAT_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.TableauMaterials, parts[i])).ToString();
                    else if (ImportantMethods.ArrayContainsString(CodeReader.Tracks, parts[i]))
                        parts[i] = (CodeReader.TRACK_MIN + (ulong)ImportantMethods.ArrayIndexOfString(CodeReader.Tracks, parts[i])).ToString();
                    else if (ImportantMethods.IsNumericGZ(parts[i]))
                        parts[i] = (ulong.MaxValue + (ulong)(int.Parse(parts[i]) + 1)).ToString();
                    else if (ConstantsFinder.ContainsConst(parts[i]))
                        parts[i] = ConstantsFinder.TranslateConst(parts[i]).ToString();
                    else if (parts[i].StartsWith(":"))
                        parts[i] = localVariableInterpreter.InterpretBack(parts[i]).ToString();
                    else
                        Console.WriteLine("FATAL ERROR! 0x9912" + Environment.NewLine + parts[i], "ERROR");
                }
                else
                    Console.WriteLine("FATAL ERROR! 0x9919" + Environment.NewLine + parts[i], "ERROR");
            }

            codeLine = string.Empty;
            foreach (string part in parts)
                codeLine += part + ' ';

            return codeLine.TrimEnd();
        }

        /*public static void LoadAll()
        {
            //Reset();

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
        }*/

        #region MainReader
        
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
            if (objectType == (int)ObjectType.SCRIPT)
                foreach (Skriptum s in ReadScript())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.MISSION_TEMPLATE)
                foreach (Skriptum s in ReadMissionTemplate())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.PRESENTATION)
                foreach (Presentation p in ReadPresentation())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.GAME_MENU)
                foreach (GameMenu g in ReadGameMenu())
                    skriptums.Add(g);
            else if (objectType == (int)ObjectType.GAME_STRING)
                foreach (GameString s in ReadString())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.SIMPLE_TRIGGER)
                foreach (SimpleTrigger t in ReadSimpleTrigger())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.TRIGGER)
                foreach (Trigger t in ReadTrigger())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.INFO_PAGE)
                foreach (InfoPage p in ReadInfoPage())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.SOUND)
                foreach (Sound s in ReadSound())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.QUEST)
                foreach (Quest q in ReadQuest())
                    skriptums.Add(q);
            else if (objectType == (int)ObjectType.SCENE)
                foreach (Scene s in ReadScene())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.SCENE_PROP)
                foreach (SceneProp s in ReadSceneProp())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.TABLEAU_MATERIAL)
                foreach (TableauMaterial t in ReadTableauMaterial())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.MUSIC)
                foreach (Music m in ReadMusic())
                    skriptums.Add(m);
            else if (objectType == (int)ObjectType.MESH)
                foreach (Mesh m in ReadMesh())
                    skriptums.Add(m);
            else if (objectType == (int)ObjectType.FACTION)
                foreach (Faction f in ReadFaction())
                    skriptums.Add(f);
            else if (objectType == (int)ObjectType.MAP_ICON)
                foreach (MapIcon m in ReadMapIcon())
                    skriptums.Add(m);
            else if (objectType == (int)ObjectType.ANIMATION)
                foreach (Animation a in ReadAnimation())
                    skriptums.Add(a);
            else if (objectType == (int)ObjectType.PARTY_TEMPLATE)
                foreach (PartyTemplate p in ReadPartyTemplate())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.DIALOG)
                foreach (Dialog d in ReadDialog())
                    skriptums.Add(d);
            else if (objectType == (int)ObjectType.PARTY)
                foreach (Party p in ReadParty())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.SKILL)
                foreach (Skill s in ReadSkill())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.POST_FX)
                foreach (PostFX p in ReadPostFX())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.PARTICLE_SYSTEM)
                foreach (ParticleSystem p in ReadParticleSystem())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.SKIN)
                foreach (Skin s in ReadSkin())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.TROOP)
                foreach (Troop t in ReadTroop())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.ITEM)
                foreach (Item itm in ReadItem())
                    skriptums.Add(itm);
            return skriptums;
        }

        public static List<List<Skriptum>> ReadAllObjects()
        {
            //Reset();
            List<List<Skriptum>> objects = new List<List<Skriptum>>();
            for (int i = 0; i < Files.Length; i++)
                objects.Add(new SourceReader(ModPath + Files[i]).ReadObjectType(i));
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
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                //objectsExpected += int.Parse(sr.ReadLine().Trim());
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

                        //missionTemplate = DecompileMissionTemplateCode(missionTemplate.HeaderInfo, entryPoints, triggers);
                    }
                }
            }
            if (missionTemplate != null)
                missionTemplates.Add(missionTemplate);
            //objectsRead += missionTemplates.Count;
            return missionTemplates.ToArray();
        }

        public Presentation[] ReadPresentation()
        {
            string line;
            string[] scriptLines;
            Presentation presentation = null;
            List<Presentation> presentations = new List<Presentation>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                //objectsExpected += int.Parse(sr.ReadLine());
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
                            SimpleTrigger simpleTrigger = new SimpleTrigger(double.Parse(CodeReader.Repl_DotWComma(scriptLines[0])));
                            string[] tmp = new string[int.Parse(scriptLines[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            scriptLines = CodeReader.GetStringArrayStartFromIndex(scriptLines, 2, 1);
                            //simpleTrigger.ConsequencesBlock = DecompileScriptCode(tmp, scriptLines);
                            presentation.addSimpleTriggerToFreeIndex(simpleTrigger, i);
                        }
                    }
                }
            }
            if (presentation != null)
                presentations.Add(presentation);
            //objectsRead += presentations.Count;
            return presentations.ToArray();
        }

        public GameMenu[] ReadGameMenu()
        {
            string s;
            List<GameMenu> game_menus = new List<GameMenu>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                //objectsExpected += int.Parse(sr.ReadLine().TrimStart());
                while (!sr.EndOfStream)
                {
                    s = RemNTrimAllXtraSp(sr.ReadLine());
                    if (s.Length > 1)
                        game_menus.Add(new GameMenu(new string[] { s, RemNTrimAllXtraSp(sr.ReadLine()) }));
                }
            }
            //objectsRead += game_menus.Count;
            return game_menus.ToArray();
        }

        public Script[] ReadScript() // ADDED 
        {
            string line = string.Empty;
            List<Script> scripts = new List<Script>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !RemNTrimAllXtraSp(line).Replace(" ", string.Empty).StartsWith("scripts=["))
                    line = sr.ReadLine().Trim('\t', ' ');

                line = line.Substring(9).Split('#')[0];

                while (!sr.EndOfStream)
                {
                    if (line.StartsWith("(\"") && line.EndsWith("\"),"))
                    {
                        line = line.Substring(line.IndexOf('\"') + 1).Split('\"')[0];
                        List<string> codeLines = new List<string>();
                        string tmp = line;
                        do
                        {
                            line = sr.ReadLine().Trim('\t', ' ');
                            codeLines.Add(line.Split(']')[0]);
                        } while (!line.Contains("]"));
                        codeLines = new List<string>() { tmp };
                        codeLines.AddRange(CodeReader.GetStringArrayStartFromIndex(GetCompiledCodeLines(codeLines.ToArray()).Trim().Split(), 1));
                        scripts.Add(new Script(codeLines.ToArray()));
                    }

                    line = sr.ReadLine().Trim('\t', ' ').Split('#')[0];
                }
            }

            return scripts.ToArray();
        }

        public Troop[] ReadTroop()
        {
            string[] tempus = new string[7];
            Troop[] troops;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int maxTroops = int.Parse(sr.ReadLine().TrimEnd());
                troops = new Troop[maxTroops];
                //objectsExpected += maxTroops;
                for (int i = 0; i < maxTroops; i++)
                {
                    for (int j = 0; j < 7; j++)
                        tempus[j] = sr.ReadLine();
                    troops[i] = new Troop(tempus);
                }
            }
            //objectsRead += troops.Length;
            return troops;
        }

        public Item[] ReadItem()
        {
            int i = -1;
            string tempus;
            List<string> lines = new List<string>();
            Item[] items;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int maxItems = int.Parse(sr.ReadLine());
                //objectsExpected += maxItems;
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

            //objectsRead += items.Length;
            return items;
        }

        public GameString[] ReadString() // ADDED 
        {
            string line;
            string[] sp;
            List<GameString> strings = new List<GameString>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if ((line.IndexOf("(\"") < line.IndexOf("#") || line.IndexOf("#") < 0) && line.StartsWith("(\"") && line.EndsWith("\"),"))
                    {
                        sp = line.Split('\"');
                        sp = new string[] { sp[1], sp[3] };
                        strings.Add(new GameString(sp));
                    }
                }
            }
            return strings.ToArray();
        }

        public SimpleTrigger[] ReadSimpleTrigger() // ADDED 
        {
            string line;
            //string[] scriptLines;
            List<SimpleTrigger> simpleTriggers = new List<SimpleTrigger>();
            List<string> codeLines = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                /*sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                simple_triggers = new SimpleTrigger[count];
                for (int i = 0; i < simple_triggers.Length; i++)
                {
                    scriptLines = sr.ReadLine().Split();
                    simple_triggers[i] = new SimpleTrigger(double.Parse(CodeReader.Repl_DotWComma(scriptLines[0])));
                    string[] tmp = new string[int.Parse(scriptLines[2]) + 1];
                    tmp[0] = "SIMPLE_TRIGGER";
                    scriptLines = CodeReader.GetStringArrayStartFromIndex(scriptLines, 2, 1);
                    //simple_triggers[i].ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(DecompileScriptCode(tmp, scriptLines), 1);
                }*/

                do
                {
                    line = RemNTrimAllXtraSp(sr.ReadLine());
                } while (!line.StartsWith("simple_triggers = ["));

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.IndexOf("#") < 0 || line.IndexOf("#") > line.IndexOf("(") && line.IndexOf("#") > line.IndexOf(","))
                    {
                        line = RemNTrimAllXtraSp(line);
                        line = line.Substring(line.IndexOf("(") + 1).Split(',')[0].Trim();
                        sr.ReadLine();
                        SimpleTrigger simpleTrigger = new SimpleTrigger(line);
                        codeLines.Clear();
                        do
                        {
                            line = sr.ReadLine().Trim('\t', ' ');
                            if (!line.Equals("]),"))
                                codeLines.Add(line);
                        } while (!line.Equals("]),"));
                        simpleTrigger.ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(GetCompiledCodeLines(codeLines.ToArray()).Trim().Split(), 1);
                        simpleTriggers.Add(simpleTrigger);
                    }
                }
            }
            return simpleTriggers.ToArray();
        }

        public Trigger[] ReadTrigger()
        {
            Trigger[] triggers;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                triggers = new Trigger[count];
                for (int i = 0; i < triggers.Length; i++)
                    triggers[i] = null;//DecompileTrigger(sr.ReadLine().Split());
            }
            //objectsRead += triggers.Length;
            return triggers;
        }

        public InfoPage[] ReadInfoPage() // ADDED 
        {
            string line;
            string[] sp;
            List<InfoPage> infoPages = new List<InfoPage>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                do
                {
                    line = RemNTrimAllXtraSp(sr.ReadLine());
                } while (!line.StartsWith("info_pages = ["));

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim();
                    if (line.IndexOf("#") < 0 || line.IndexOf("#") > line.IndexOf("(\""))
                    {
                        sp = line.Split('\"');
                        sp = new string[] { "ip_" + sp[1], sp[3], sp[5] };
                        infoPages.Add(new InfoPage(sp));
                    } 
                }
            }
            return infoPages.ToArray();
        }

        public Mesh[] ReadMesh()
        {
            Mesh[] meshes;
            using (StreamReader sr = new StreamReader(filePath))
            {
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                meshes = new Mesh[count];
                for (int i = 0; i < meshes.Length; i++)
                    meshes[i] = new Mesh(sr.ReadLine().Substring(5).Split());
            }
            //objectsRead += meshes.Length;
            return meshes;
        }

        public Music[] ReadMusic()
        {
            Music[] musicTracks;
            using (StreamReader sr = new StreamReader(filePath))
            {
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                musicTracks = new Music[count];
                for (int i = 0; i < musicTracks.Length; i++)
                {
                    string[] sts = sr.ReadLine().Split();
                    //musicTracks[i] = new Music(new string[] { Tracks[i].Substring(6), sts[0], sts[1], sts[2] });
                }
            }
            //objectsRead += musicTracks.Length;
            return musicTracks;
        }

        public Quest[] ReadQuest()
        {
            Quest[] quests;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                quests = new Quest[count];
                for (int i = 0; i < quests.Length; i++)
                    quests[i] = new Quest(sr.ReadLine().Substring(4).Split());
            }
            //objectsRead += quests.Length;
            return quests;
        }

        public Sound[] ReadSound()
        {
            string line;
            Sound[] sounds;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                sr.ReadLine();
                do { line = sr.ReadLine(); } while (!ImportantMethods.IsNumericFKZ2(line));
                int count = int.Parse(line);
                //objectsExpected += count;
                sounds = new Sound[count];
                for (int i = 0; i < sounds.Length && !sr.EndOfStream; i++)
                    sounds[i] = new Sound(sr.ReadLine().Substring(4).Split());
            }
            //objectsRead += sounds.Length;
            return sounds;
        }

        public Scene[] ReadScene()
        {
            string firstLine;
            string[] otherScenes, chestTroops, tmp;
            Scene[] _scenes;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                //int tmpX;
                int count = int.Parse(sr.ReadLine().TrimStart());
                //objectsExpected += count;
                _scenes = new Scene[count];
                for (int i = 0; i < _scenes.Length; i++)
                {
                    firstLine = sr.ReadLine();
                    tmp = sr.ReadLine().Substring(2).TrimEnd().Replace("  ", " ").Split();
                    otherScenes = new string[int.Parse(tmp[0])];
                    for (int j = 0; j < otherScenes.Length; j++)
                    {
                        //tmpX = int.Parse(tmp[j + 1]);
                        //if (Scenes.Length > tmpX && tmpX >= 0)
                        //    otherScenes[j] = Scenes[tmpX];
                        //else if (tmpX == 100000)
                        //    otherScenes[j] = "exit";
                        //else
                        //    otherScenes[j] = "(ERROR)" + tmpX; //CHECK!!!
                    }
                    tmp = sr.ReadLine().Substring(2).TrimEnd().Replace("  ", " ").Split();
                    chestTroops = new string[int.Parse(tmp[0])];
                    //for (int j = 0; j < chestTroops.Length; j++)
                    //   chestTroops[j] = Troops[int.Parse(tmp[j + 1])];
                    _scenes[i] = new Scene(firstLine.Split(), otherScenes, chestTroops, sr.ReadLine().Trim());
                }
            }
            //objectsRead += _scenes.Length;
            return _scenes;
        }

        public TableauMaterial[] ReadTableauMaterial()
        {
            TableauMaterial[] tableaus;
            using (StreamReader sr = new StreamReader(filePath))
            {
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                tableaus = new TableauMaterial[count];
                for (int i = 0; i < tableaus.Length; i++)
                    tableaus[i] = new TableauMaterial(sr.ReadLine().Substring(4).TrimEnd().Split());
            }
            //objectsRead += tableaus.Length;
            return tableaus;
        }

        public SceneProp[] ReadSceneProp()
        {
            int tCount;
            string[] lines;
            SceneProp[] sceneProps;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().TrimStart());
                //objectsExpected += count;
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
                            s_triggers[j] = new SimpleTrigger(double.Parse(CodeReader.Repl_DotWComma(lines[0])));
                            string[] tmp = new string[int.Parse(lines[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            lines = CodeReader.GetStringArrayStartFromIndex(lines, 2, 1);
                            //s_triggers[j].ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(DecompileScriptCode(tmp, lines), 1);
                        }
                        sceneProps[i].SimpleTriggers = s_triggers;
                    }
                    sr.ReadLine();
                    sr.ReadLine();
                }
            }
            //objectsRead += sceneProps.Length;
            return sceneProps;
        }

        public Faction[] ReadFaction()
        {
            int c;
            string line;
            Faction[] _factions;
            Faction.ResetIDs();
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                _factions = new Faction[count];
                for (int i = 0; i < _factions.Length; i++)
                {
                    do { c = sr.Read(); } while ((char)c != 'f');
                    _factions[i] = new Faction(((char)c + sr.ReadLine().TrimEnd()).Split());
                    string[] sp = sr.ReadLine().Trim().Replace("  ", " ").Split();
                    double[] dd = new double[sp.Length];
                    for (int j = 0; j < sp.Length; j++)
                        dd[j] = double.Parse(CodeReader.Repl_DotWComma(sp[j]));
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
            //objectsRead += _factions.Length;
            return _factions;
        }

        public MapIcon[] ReadMapIcon()
        {
            int tCount;
            string[] sp;
            MapIcon[] mapIcons;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
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
                            s_triggers[j] = new SimpleTrigger(double.Parse(CodeReader.Repl_DotWComma(sp[0])));
                            string[] tmp = new string[int.Parse(sp[2]) + 1];
                            tmp[0] = "SIMPLE_TRIGGER";
                            sp = CodeReader.GetStringArrayStartFromIndex(sp, 2, 1);
                            //s_triggers[j].ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(DecompileScriptCode(tmp, sp), 1);
                        }
                        mapIcons[i].SimpleTriggers = s_triggers;
                    }
                    sr.ReadLine();
                    sr.ReadLine();
                }
            }
            //objectsRead += mapIcons.Length;
            return mapIcons;
        }

        public Animation[] ReadAnimation()
        {
            string[] sp;
            Animation[] animations;
            using (StreamReader sr = new StreamReader(filePath))
            {
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
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
            //objectsRead += animations.Length;
            return animations;
        }

        public PartyTemplate[] ReadPartyTemplate()
        {
            PartyTemplate[] partyTemplates;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                partyTemplates = new PartyTemplate[count];
                for (int i = 0; i < partyTemplates.Length; i++)
                    partyTemplates[i] = new PartyTemplate(sr.ReadLine().Substring(3).TrimEnd().Split());
            }
            //objectsRead += partyTemplates.Length;
            return partyTemplates;
        }

        public Dialog[] ReadDialog()
        {
            Dialog[] dialogs;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
                dialogs = new Dialog[count];
                for (int i = 0; i < dialogs.Length; i++)
                    dialogs[i] = new Dialog(sr.ReadLine().Substring(5).TrimEnd().Split());
            }
            //objectsRead += dialogs.Length;
            return dialogs;
        }

        public Party[] ReadParty()
        {
            string line;
            Party[] parties;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().Split()[0]);
                //objectsExpected += count;
                parties = new Party[count];
                for (int i = 0; i < parties.Length; i++)
                {
                    line = sr.ReadLine().Trim();
                    double degrees = double.Parse(CodeReader.Repl_DotWComma(sr.ReadLine()));
                    degrees = Math.Round(degrees / (3.1415926 / 180d), 4);
                    line += " " + degrees; // maybe check if values are still correct!
                    parties[i] = new Party(line.Split());
                }
            }
            //objectsRead += parties.Length;
            return parties;
        }

        public Skill[] ReadSkill()
        {
            Skill[] skills;
            using (StreamReader sr = new StreamReader(filePath))
            {
                int count = int.Parse(sr.ReadLine().Split()[0]);
                //objectsExpected += count;
                skills = new Skill[count];
                for (int i = 0; i < skills.Length; i++)
                    skills[i] = new Skill(sr.ReadLine().Substring(4).Split());
            }
            //objectsRead += skills.Length;
            return skills;
        }

        public PostFX[] ReadPostFX()
        {
            PostFX[] postfxs;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine().Split()[0]);
                //objectsExpected += count;
                postfxs = new PostFX[count];
                for (int i = 0; i < postfxs.Length; i++)
                    postfxs[i] = new PostFX(sr.ReadLine().Substring(4));
            }
            //objectsRead += postfxs.Length;
            return postfxs;
        }

        public ParticleSystem[] ReadParticleSystem()
        {
            ParticleSystem[] particleSystems;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
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
            //objectsRead += particleSystems.Length;
            return particleSystems;
        }

        public Skin[] ReadSkin()
        {
            Skin[] skins;
            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                int count = int.Parse(sr.ReadLine());
                //objectsExpected += count;
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
            //objectsRead += skins.Length;
            return skins;
        }

        #endregion
    }
}
