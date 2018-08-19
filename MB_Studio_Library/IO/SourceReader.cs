using importantLib;
using MB_Studio_Library.Objects;
using MB_Studio_Library.Objects.Support;
using System;
using System.Collections.Generic;
using System.IO;
using static MB_Studio_Library.Objects.Skriptum;

namespace MB_Studio_Library.IO
{
    public class SourceReader
    {
        //change this to source files
        public static readonly string[] Files = { "scripts.txt", "mission_templates.txt", "presentations.txt", "menus.txt", "troops.txt", "item_kinds1.txt", "strings.txt", "simple_triggers.txt",
                "triggers.txt", "info_pages.txt", "meshes.txt", "music.txt", "quests.txt", "sounds.txt", "scene_props.txt", "tableau_materials.txt", "map_icons.txt", "conversation.txt",
                "factions.txt", "actions.txt", "party_templates.txt", "parties.txt", "skills.txt", "postfx.txt", "skins.txt", "particle_systems.txt", "scenes.txt" };

        private static LocalVariableInterpreter localVariableInterpreter;

        private readonly string filePath;

        public SourceReader(string filePath = null)
        {
            this.filePath = filePath;
        }

        public static void SetModPath(string modPath) { ModPath = modPath; }

        public static string ModPath { get; private set; } = string.Empty;

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
                        Console.WriteLine("FATAL ERROR! - 0x9913");
                }
                else if (ImportantMethods.ArrayContainsString(declarations, tmp[1]))
                    u += CodeReader.CodeValues[ImportantMethods.ArrayIndexOfString(declarations, tmp[1])];
                else
                    Console.WriteLine("FATAL ERROR! - 0x9914");
            }

            for (int i = 2; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim(' ', '\"');

                if (!ImportantMethods.IsNumericGZ(parts[i]))
                {
                    if (ImportantMethods.IsNumericGZ(parts[i].Replace("reg", string.Empty)))
                        parts[i] = (CodeReader.REG0 + ulong.Parse(parts[i].Replace("reg", string.Empty))).ToString();
                    else if (CodeReader.QuickStrings.Contains(parts[i]))
                        parts[i] = (CodeReader.QUICKSTRING_MIN + (ulong)CodeReader.QuickStrings.IndexOf(parts[i])).ToString();
                    else if (CodeReader.GlobalVariables.Contains(parts[i]))
                        parts[i] = (CodeReader.GLOBAL_MIN + (ulong)CodeReader.GlobalVariables.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Scripts.Contains(parts[i]))
                        parts[i] = (CodeReader.SCRIPT_MIN + (ulong)CodeReader.Scripts.IndexOf(parts[i])).ToString();
                    else if (CodeReader.SceneProps.Contains(parts[i]))
                        parts[i] = (CodeReader.SPR_MIN + (ulong)CodeReader.SceneProps.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Strings.Contains(parts[i]))
                        parts[i] = (CodeReader.STRING_MIN + (ulong)CodeReader.Strings.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Factions.Contains(parts[i]))
                        parts[i] = (CodeReader.FAC_MIN + (ulong)CodeReader.Factions.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Troops.Contains(parts[i]))
                        parts[i] = (CodeReader.TRP_PLAYER + (ulong)CodeReader.Troops.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Presentations.Contains(parts[i]))
                        parts[i] = (CodeReader.PRSNT_MIN + (ulong)CodeReader.Presentations.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Scenes.Contains(parts[i]))
                        parts[i] = (CodeReader.SCENE_MIN + (ulong)CodeReader.Scenes.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Meshes.Contains(parts[i]))
                        parts[i] = (CodeReader.MESH_MIN + (ulong)CodeReader.Meshes.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Items.Contains(parts[i]))
                        parts[i] = (CodeReader.ITM_MIN + (ulong)CodeReader.Items.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Parties.Contains(parts[i]))
                        parts[i] = (CodeReader.P_MAIN_PARTY + (ulong)CodeReader.Parties.IndexOf(parts[i])).ToString();
                    else if (CodeReader.PartyTemplates.Contains(parts[i]))
                        parts[i] = (CodeReader.PT_MIN + (ulong)CodeReader.PartyTemplates.IndexOf(parts[i])).ToString();
                    else if (CodeReader.MissionTemplates.Contains(parts[i]))
                        parts[i] = (CodeReader.MT_MIN + (ulong)CodeReader.MissionTemplates.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Animations.Contains(parts[i]))
                        parts[i] = (CodeReader.ANIM_MIN + (ulong)CodeReader.Animations.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Skills.Contains(parts[i]))
                        parts[i] = (CodeReader.SKL_MIN + (ulong)CodeReader.Skills.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Sounds.Contains(parts[i]))
                        parts[i] = (CodeReader.SND_MIN + (ulong)CodeReader.Sounds.IndexOf(parts[i])).ToString();
                    else if (CodeReader.ParticleSystems.Contains(parts[i]))
                        parts[i] = (CodeReader.PSYS_MIN + (ulong)CodeReader.ParticleSystems.IndexOf(parts[i])).ToString();
                    else if (CodeReader.GameMenus.Contains(parts[i]))
                        parts[i] = (CodeReader.MENU_MIN + (ulong)CodeReader.GameMenus.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Quests.Contains(parts[i]))
                        parts[i] = (CodeReader.QUEST_MIN + (ulong)CodeReader.Quests.IndexOf(parts[i])).ToString();
                    else if (CodeReader.TableauMaterials.Contains(parts[i]))
                        parts[i] = (CodeReader.TABLEAU_MAT_MIN + (ulong)CodeReader.TableauMaterials.IndexOf(parts[i])).ToString();
                    else if (CodeReader.MapIcons.Contains(parts[i]))
                        parts[i] = (CodeReader.MAP_ICON_MIN + (ulong)CodeReader.MapIcons.IndexOf(parts[i])).ToString();
                    else if (CodeReader.Tracks.Contains(parts[i]))
                        parts[i] = (CodeReader.TRACK_MIN + (ulong)CodeReader.Tracks.IndexOf(parts[i])).ToString();
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
            if (objectType == (int)ObjectType.Script)
                foreach (Skriptum s in ReadScript())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.MissionTemplate)
                foreach (Skriptum s in ReadMissionTemplate())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.Presentation)
                foreach (Presentation p in ReadPresentation())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.GameMenu)
                foreach (GameMenu g in ReadGameMenu())
                    skriptums.Add(g);
            else if (objectType == (int)ObjectType.GameString)
                foreach (GameString s in ReadString())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.SimpleTrigger)
                foreach (SimpleTrigger t in ReadSimpleTrigger())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.Trigger)
                foreach (Trigger t in ReadTrigger())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.InfoPage)
                foreach (InfoPage p in ReadInfoPage())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.Sound)
                foreach (Sound s in ReadSound())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.Quest)
                foreach (Quest q in ReadQuest())
                    skriptums.Add(q);
            else if (objectType == (int)ObjectType.Scene)
                foreach (Scene s in ReadScene())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.SceneProp)
                foreach (SceneProp s in ReadSceneProp())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.TableauMaterial)
                foreach (TableauMaterial t in ReadTableauMaterial())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.Music)
                foreach (Music m in ReadMusic())
                    skriptums.Add(m);
            else if (objectType == (int)ObjectType.Mesh)
                foreach (Mesh m in ReadMesh())
                    skriptums.Add(m);
            else if (objectType == (int)ObjectType.Faction)
                foreach (Faction f in ReadFaction())
                    skriptums.Add(f);
            else if (objectType == (int)ObjectType.MapIcon)
                foreach (MapIcon m in ReadMapIcon())
                    skriptums.Add(m);
            else if (objectType == (int)ObjectType.Animation)
                foreach (Animation a in ReadAnimation())
                    skriptums.Add(a);
            else if (objectType == (int)ObjectType.PartyTemplate)
                foreach (PartyTemplate p in ReadPartyTemplate())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.Dialog)
                foreach (Dialog d in ReadDialog())
                    skriptums.Add(d);
            else if (objectType == (int)ObjectType.Party)
                foreach (Party p in ReadParty())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.Skill)
                foreach (Skill s in ReadSkill())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.PostFX)
                foreach (PostFX p in ReadPostFX())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.ParticleSystem)
                foreach (ParticleSystem p in ReadParticleSystem())
                    skriptums.Add(p);
            else if (objectType == (int)ObjectType.Skin)
                foreach (Skin s in ReadSkin())
                    skriptums.Add(s);
            else if (objectType == (int)ObjectType.Troop)
                foreach (Troop t in ReadTroop())
                    skriptums.Add(t);
            else if (objectType == (int)ObjectType.Item)
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

        // NOT IMPLEMENTED YET
        public MissionTemplate[] ReadMissionTemplate()
        {
            string line = string.Empty;
            List<MissionTemplate> missionTemplates = new List<MissionTemplate>();
            List<string> lines = new List<string>();
            List<string[]> entryPoints = new List<string[]>();
            List<string[]> triggers = new List<string[]>();
            List<string> varTriggerNames = new List<string>();
            List<Trigger> varTriggers = new List<Trigger>();
            List<string> varTriggerArrayNames = new List<string>();
            List<Trigger[]> varTriggerArrays = new List<Trigger[]>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("mission_templates = ["))
                {
                    line = sr.ReadLine().Trim('\t', ' ').Replace(" ", string.Empty);
                    if (line.EndsWith("=("))//single trigger
                    {
                        line = line.Split('=')[0];
                        varTriggerNames.Add(line); //lines.Add(line);
                        while (!sr.EndOfStream && !line.Equals("])"))
                        {
                            line = sr.ReadLine().Trim('\t', ' ');
                            lines.Add(line);
                        }

                        string[] sp = lines[1].TrimEnd(',').Split(',');
                        string[] conditionLines = null;
                        string[] consequenceLines = null;

                        if (sp.Length > 3)
                            if (sp[3].Equals("[]"))
                                conditionLines = new string[0];

                        if (lines[lines.Count].EndsWith("[])"))
                            consequenceLines = new string[0];

                        int xcx = 1;

                        if (conditionLines == null)
                        {
                            for (int i = xcx; i < lines.Count; i++)
                            {
                                line = lines[i];
                                if (line.Contains("]"))
                                {
                                    conditionLines = new string[i - xcx];
                                    xcx += conditionLines.Length;
                                    i = lines.Count;
                                }
                            }
                            conditionLines = CodeReader.GetStringArrayStartFromIndex(conditionLines, 1, 1);
                        }

                        if (consequenceLines == null)
                        {
                            for (int i = xcx; i < lines.Count; i++)
                            {
                                line = lines[i];
                                if (line.Contains("]"))
                                {
                                    consequenceLines = new string[i - xcx];
                                    i = lines.Count;
                                }
                            }
                            consequenceLines = CodeReader.GetStringArrayStartFromIndex(consequenceLines, 1, 1);
                        }

                        varTriggers.Add(new Trigger(sp[0], sp[1], sp[2])
                        {
                            ConditionBlock = conditionLines,
                            ConsequencesBlock = consequenceLines
                        });
                    }
                    else if (line.Contains("=["))//trigger list
                    {
                        lines.Add(line);

                        // only one for now, but add trigger array code anyways

                    }
                }

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("(\""))
                    {
                        //code for missiontemplate
                    }
                }
            }
            return missionTemplates.ToArray();
        }

        // NOT IMPLEMENTED
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
                            SimpleTrigger simpleTrigger = new SimpleTrigger(scriptLines[0]);
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

        // ADDED
        public GameMenu[] ReadGameMenu()
        {
            int x = 0;
            string[] sp;
            string tmp;
            string line = string.Empty;
            List<GameMenu> gameMenus = new List<GameMenu>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("game_menus = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("(\""))
                    {
                        sp = line.Split('\"');

                        line = string.Empty;

                        while (!sr.EndOfStream && x < 2)
                        {
                            line += sr.ReadLine().Trim('\t', ' ');
                            x = CodeReader.CountCharInString(line, '\t');
                        }

                        if (!sr.EndOfStream)
                            tmp = sr.ReadLine().Split('\"')[1];
                        else
                            tmp = string.Empty;

                        sp = new string[] {
                            sp[1],
                            sp[2].Trim(',', ' '),
                            line.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\\", string.Empty).Split('\"')[1],
                            tmp
                        };

                        x = 0;

                        List<string> codeLines = new List<string>();

                        while (!sr.EndOfStream && x != 0)
                        {
                            line = sr.ReadLine().Trim('\t', ' ');

                            x += CodeReader.CountCharInString(line, '[');

                            tmp = line.Trim('[', ']', ' ', '\t');

                            if (x == 1 && tmp.Length != 0)
                                codeLines.Add(tmp);

                            x -= CodeReader.CountCharInString(line, ']');
                        }

                        codeLines = new List<string>(GetCompiledCodeLines(codeLines.ToArray()).Trim().Split());
                        codeLines.InsertRange(0, sp);

                        line = string.Empty;

                        foreach (string s in sp)
                            line += s + ' ';

                        sp = new string[2];
                        sp[0] = line;

                        x = 0;

                        int x2 = 0;
                        //bool textMode = false;
                        //bool codeMode = false;
                        //List<string> texts = new List<string>();

                        line = string.Empty;

                        codeLines.Clear();

                        while (!sr.EndOfStream && x != 0)
                        {
                            tmp = Char.ConvertFromUtf32(sr.Read()).Trim('\t', ' ');

                            if (tmp.Equals("["))
                                x++;
                            else if (tmp.Equals("]"))
                                x--;

                            if (x == 1 && tmp.Length != 0)
                            {
                                if (tmp.Equals("("))
                                    x2++;
                                else if (tmp.Equals(")"))
                                {
                                    x2--;
                                    if (x2 == 0)
                                    {
                                        codeLines.Add(line);//add mno code
                                        line = string.Empty;
                                    }
                                }

                                if (x2 == 1)
                                {
                                    /*if (textMode || codeMode)
                                        line += tmp;

                                    if (tmp.Equals("\""))
                                    {
                                        textMode = !textMode;
                                        if (!textMode)
                                        {
                                            texts.Add(line.TrimEnd('\"').Trim('\t', ' '));
                                            line = string.Empty;
                                        }
                                    }
                                    else if (tmp.Equals("[") || tmp.Equals("]"))
                                    {
                                        codeMode = !codeMode;
                                        if (!codeMode)
                                        {
                                            codeLines.Add(line.TrimEnd(']').Trim('\t', ' '));
                                            line = string.Empty;
                                        }
                                    }*/
                                    line += tmp;
                                }
                            }
                        }

                        string[] xxs = codeLines.ToArray();//source mno codes
                        string[,] mno_codes = new string[codeLines.Count, 4];//for raw data
                        line = sp[0];
                        codeLines.Clear();

                        for (int i = 0; i < xxs.Length; i++)
                        {
                            sp = xxs[i].Split('[');
                            mno_codes[i, 0] = sp[0].Split('\"')[1].Replace(' ', '_');
                            tmp = sp[1].Split(']')[0].Trim('\t', ' ').Replace('\r', '\n').Replace("\n\n", "\n").Trim('\n');
                            mno_codes[i, 1] = GetCompiledCodeLines(tmp.Split('\n')).Trim();
                            mno_codes[i, 2] = sp[1].Split(']')[1].Split('\"')[1].Replace('\r', '\\').Replace('\n', '\\').Replace("\\", string.Empty).Replace(' ', '_');
                            tmp = sp[2].Split(']')[0].Trim('\t', ' ').Replace('\r', '\n').Replace("\n\n", "\n").Trim('\n');
                            mno_codes[i, 3] = GetCompiledCodeLines(tmp.Split('\n')).Trim();
                        }

                        tmp = string.Empty;

                        for (int i = 0; i < xxs.Length; i++)
                            for (int j = 0; j < 4; j++)
                                tmp += mno_codes[i, j] + ' ';

                        tmp = tmp.TrimEnd();

                        sp = new string[] { line, tmp };

                        /*using (StreamWriter wr = new StreamWriter("testFile.txt")) // just for testing
                        {
                            wr.WriteLine(sp[0]);
                            wr.WriteLine(sp[1]);
                        }*/

                        gameMenus.Add(new GameMenu(sp));
                    }
                }
            }
            return gameMenus.ToArray();
        }

        // ADDED
        public Script[] ReadScript()
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

        // NOT IMPLEMENTED YET
        public Troop[] ReadTroop()
        {
            string[] sp0, sp1, sp2, sp3;
            string line = string.Empty;
            List<Troop> troops = new List<Troop>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("troops = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("[\""))
                    {
                        int x = 0;
                        do
                        {
                            x += CodeReader.CountCharInString(line, '[');
                            x -= CodeReader.CountCharInString(line, ']');
                            if (x != 0)
                                line += sr.ReadLine().Trim('\t', ' ');
                        } while (!sr.EndOfStream && x != 0);
                        line = line.TrimStart('[').Remove(line.LastIndexOf(']')).Replace("],", "]");
                        sp0 = line.Split('[');

                        sp1 = sp0[0].TrimEnd(' ', ',').Split(',');
                        sp1 = new string[] { sp1[0].Split('\"')[1], sp1[1].Split('\"')[1], sp1[2].Split('\"')[1], sp1[3].Trim(), sp1[4].Trim(), sp1[5].Trim(), sp1[6].Trim() };
                        List<string> listSp1 = new List<string>(sp1);

                        //sp0[1] = RemNTrimAllXtraSp(sp0[1].Split(']')[0]).Replace(" ", string.Empty).Trim(',');
                        sp0[1] = RemNTrimAllXtraSp(sp0[1]).Replace(" ", string.Empty).Trim(',');

                        sp2 = sp0[1].Split(']');

                        sp2[0] = sp2[0].Replace("(", string.Empty).Replace("),", ";");

                        if (!sp2[0].Contains(";") && !sp2[0].Contains(")"))
                        {
                            line = string.Empty;
                            for (int i = 0; i < sp2.Length; i++)
                                line += sp2[i] + ",0;";
                            sp2[0] = line.TrimEnd(';');
                        }

                        string tmpX = RemNTrimAllXtraSp(sp2[1].Split(']')[0]).Replace(" ", string.Empty).Trim(',');

                        sp3 = tmpX.Split(',');

                        tmpX += ',';

                        sp2 = sp2[0].TrimEnd(')').Split(';');

                        switch (sp3.Length)
                        {
                            case 11:
                                tmpX += "0,0,0,0,0";
                                break;
                            case 12:
                                tmpX += "0,0,0,0";
                                break;
                            case 13:
                                tmpX += "0,0,0";
                                break;
                            case 14:
                                tmpX += "0,0";
                                break;
                            case 15:
                                tmpX += "0";
                                break;
                            default:
                                break;
                        }

                        tmpX = tmpX.TrimEnd(',');

                        sp3 = tmpX.Split(',');

                        sp0 = new string[6];

                        List<string> itemIds = new List<string>(CodeReader.Items);

                        tmpX = string.Empty;
                        foreach (string s in sp2)
                            tmpX += itemIds.IndexOf(s) + " ";

                        sp0[1] = tmpX.TrimEnd();

                        tmpX = sp3[0];
                        List<HeaderVariable> codes = SourceWriter.GetTroopModuleConstants();
                        foreach (HeaderVariable varX in codes)
                        {
                            if (varX.VariableName.Equals(tmpX))
                                sp3[0] = varX.VariableValue.Trim();
                            else if (tmpX.Contains(varX.VariableName) && tmpX.Contains("|"))
                            {
                                List<string> tmpList = new List<string>(tmpX.Split('|'));
                                if (tmpList.Contains(varX.VariableName))
                                    tmpList[tmpList.IndexOf(varX.VariableName)] = varX.VariableValue.Trim();
                                string tmpX2 = string.Empty;
                                foreach (string item in tmpList)
                                    tmpX2 += item + '|';
                                sp3[0] = tmpX2.TrimEnd('|');
                            }
                        }

                        sp0[2] = sp3[0];
                        sp0[3] = sp3[1];
                        sp0[4] = sp3[2];
                        sp0[5] = sp3[3];

                        listSp1.Insert(3, sp3[4]);
                        listSp1.Add(sp1[sp1.Length - 2]);
                        listSp1.Add(sp1[sp1.Length - 1]);

                        if (listSp1[5].Equals("no_scene"))
                            listSp1[5] = "0";
                        if (!ImportantMethods.IsNumericGZ(listSp1[5]))
                        {
                            string[] tmpX2 = listSp1[5].Split('|');
                            ulong sceneCode = (ulong)new List<string>(CodeReader.Scenes).IndexOf(tmpX2[0]);
                            if (tmpX2.Length > 1)
                                sceneCode |= ulong.Parse(tmpX2[1]) << 16;
                            listSp1[5] = sceneCode.ToString();
                        }

                        if (!ImportantMethods.IsNumericGZ(listSp1[6]))
                            listSp1[6] = new List<string>(CodeReader.Factions).IndexOf(listSp1[6]).ToString();

                        List<string> troopIds = new List<string>(CodeReader.Troops);

                        if (!ImportantMethods.IsNumericGZ(listSp1[8]))
                            listSp1[8] = troopIds.IndexOf(listSp1[8]).ToString();

                        if (!ImportantMethods.IsNumericGZ(listSp1[9]))
                            listSp1[9] = troopIds.IndexOf(listSp1[9]).ToString();

                        tmpX = string.Empty;
                        foreach (string s in listSp1)
                            tmpX += s + ' ';
                        sp0[0] = tmpX.TrimEnd();

                        tmpX = string.Empty;
                        foreach (string item in sp0)
                        {
                            tmpX += item + Environment.NewLine;
                            Console.WriteLine(item);
                        }
                        
                        System.Windows.Forms.MessageBox.Show(tmpX);

                        troops.Add(new Troop(sp0));
                    }
                }
            }
            return troops.ToArray();
        }

        // NOT IMPLEMENTED
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

        // ADDED
        public GameString[] ReadString()
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

        // ADDED
        public SimpleTrigger[] ReadSimpleTrigger()
        {
            string line;
            //string[] scriptLines;
            List<SimpleTrigger> simpleTriggers = new List<SimpleTrigger>();
            List<string> codeLines = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
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

        // ADDED
        public Trigger[] ReadTrigger()
        {
            bool newLine = false;
            string line = string.Empty;
            List<Trigger> triggers = new List<Trigger>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("triggers = ["))
                    line = RemNTrimAllXtraSp(sr.ReadLine());

                while (!line.Equals("]") && !sr.EndOfStream)
                {
                    line = string.Empty;
                    char tmp = (char)sr.Read();
                    if (tmp == '(')
                    {
                        while (!sr.EndOfStream && tmp != '[')
                        {
                            tmp = (char)sr.Read();
                            if (tmp != '#')
                            {
                                line += tmp;
                            }
                            else
                            {
                                newLine = false;
                                while (!sr.EndOfStream && !newLine)
                                {
                                    tmp = (char)sr.Read();
                                    newLine = tmp == '\n' || tmp == '\r';
                                }
                                if (newLine)
                                {
                                    tmp = (char)sr.Read();
                                    if (tmp != '\n')
                                        line += '\n';
                                    line += tmp;
                                }
                            }
                        }

                        line = RemNTrimAllXtraSp(line).Replace(" ", string.Empty);
                        string[] intervals = line.Remove(line.LastIndexOf(',')).Split(',');

                        line = string.Empty;

                        while (!sr.EndOfStream && tmp != ']')
                        {
                            tmp = (char)sr.Read();
                            if (tmp != '#')
                            {
                                line += tmp;
                            }
                            else
                            {
                                newLine = false;
                                while (!sr.EndOfStream && !newLine)
                                {
                                    tmp = (char)sr.Read();
                                    newLine = tmp == '\n' || tmp == '\r';
                                }
                                if (newLine)
                                {
                                    tmp = (char)sr.Read();
                                    if (tmp != '\n')
                                        line += '\n';
                                    line += tmp;
                                }
                            }
                        }

                        string[] conditionLines = GetCompiledCodeLines(line.Split('\n')).Trim().Split();

                        line = string.Empty;

                        while (!sr.EndOfStream && tmp != '[')
                            tmp = (char)sr.Read();

                        while (!sr.EndOfStream && tmp != ']')
                        {
                            tmp = (char)sr.Read();
                            if (tmp != '#')
                            {
                                line += tmp;
                            }
                            else
                            {
                                newLine = false;
                                while (!sr.EndOfStream && !newLine)
                                {
                                    tmp = (char)sr.Read();
                                    newLine = tmp == '\n' || tmp == '\r';
                                }
                                if (newLine)
                                {
                                    tmp = (char)sr.Read();
                                    if (tmp != '\n')
                                        line += '\n';
                                    line += tmp;
                                }
                            }
                        }

                        string[] consequenceLines = GetCompiledCodeLines(line.Split('\n')).Trim().Split();

                        Trigger trigger = new Trigger(intervals[0], intervals[1], intervals[2])
                        {
                            ConditionBlock = conditionLines,
                            ConsequencesBlock = consequenceLines
                        };
                        triggers.Add(trigger);
                    }
                } 
            }
            return triggers.ToArray();
        }

        // ADDED
        public InfoPage[] ReadInfoPage()
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

        // ADDED
        public Mesh[] ReadMesh()
        {
            string line = string.Empty;
            List<Mesh> meshes = new List<Mesh>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("meshes = ["))
                    line = sr.ReadLine();
                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = RemNTrimAllXtraSp(sr.ReadLine());
                    if (line.StartsWith("(\""))
                    {
                        line = line.TrimStart('(').Replace('\"', ' ').Replace(" ", string.Empty).Split(')')[0];
                        meshes.Add(new Mesh(line.Split(',')));
                    }
                }
            }
            return meshes.ToArray();
        }

        // ADDED
        public Music[] ReadMusic()
        {
            string[] sp;
            string line = string.Empty;
            List<Music> musicTracks = new List<Music>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("tracks = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("(\""))
                    {
                        sp = line.Split(',');
                        sp = new string[] { sp[0].Split('\"')[1], sp[1].Split('\"')[1], sp[2].Trim(), sp[3].Split(')')[0].Trim() };
                        musicTracks.Add(new Music(sp));
                    }
                }
            }
            return musicTracks.ToArray();
        }

        // ADDED
        public Quest[] ReadQuest()
        {
            string[] sp;
            string line = string.Empty;
            List<Quest> quests = new List<Quest>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("quests = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim();
                    if (line.StartsWith("(\""))
                    {
                        while (!sr.EndOfStream && !line.Contains("),") && CodeReader.CountCharInString(line, '\"') == 6)
                            line += sr.ReadLine();

                        sp = line.Remove(line.IndexOf("),")).Substring(1).Trim().Split('\"');

                        quests.Add(new Quest(new string[] { sp[1], sp[3], sp[4].Trim(',', ' ', '\n', '\r'), sp[5] }));
                    }
                }
            }
            return quests.ToArray();
        }

        // ADDED
        public Sound[] ReadSound()
        {
            string[] xss = new string[3];
            string line = string.Empty;
            List<Sound> sounds = new List<Sound>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("sounds = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = RemNTrimAllXtraSp(sr.ReadLine());
                    if (line.StartsWith("(\""))
                    {
                        line = line.Substring(1).Replace(" ", string.Empty);
                        string tmp = line.Split('[')[0].Replace("\"", string.Empty);
                        xss[0] = tmp.Split(',')[0];
                        xss[1] = tmp.Split(',')[1];
                        if (line.Contains("]"))
                            xss[1] = line.Split('[')[1].Replace("\"", string.Empty).Split(']')[0].Trim(',', ' ').Replace(" ", string.Empty).Replace(',', ' '); // rethink trim
                        else
                        {
                            line = line.Split('[')[1];
                            while (!sr.EndOfStream && !line.Contains("]"))
                                line += sr.ReadLine();
                            string[] tmpX = line.Split('\"');
                            line = string.Empty;
                            for (int i = 1; i < tmpX.Length; i += 2)
                                line += tmpX[i] + ' ';
                            line.TrimEnd();
                            xss[1] = line;
                        }
                    }
                }
            }
            return sounds.ToArray();
        }

        // ADDED
        public Scene[] ReadScene()
        {
            string[] sp1, sp2, sp3;
            string line = string.Empty;
            List<Scene> scenes = new List<Scene>();
            List<string> sceneLines = new List<string>();
            List<string> sceneIDs = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("scenes = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("(\""))
                    {
                        line = line.TrimEnd('\r', '\n', '\t', ' ');
                        if (!sr.EndOfStream)
                            line += sr.ReadLine().Trim('\t', ' ');
                        sceneLines.Add(line);
                    }
                }
            }

            foreach (string sline in sceneLines)
                sceneIDs.Add(sline.Split('\"')[1]);

            foreach (string sline in sceneLines)
            {
                sp3 = sline.Split('[');
                line = "," + RemNTrimAllXtraSp(sp3[0]).Replace('\"', ' ').Replace('(', ' ').Replace(')', ' ').Replace(" ", string.Empty).TrimEnd(',');
                sp1 = line.Split(',');
                sp3[1] = sp3[1].Split(']')[0];

                if (sp3[1].Contains(","))
                    sp2 = sp3[1].Split(',');
                else
                    sp2 = new string[0];

                if (sp3[2].Contains(","))
                    line = sp3[2].Split(',')[1];
                else
                    line = "0";

                sp3 = sp3[2].Split(']')[0].Split(',');

                for (int j = 0; j < sceneIDs.Count; j++)
                {
                    for (int i = 0; i < sp2.Length; i++)
                    {
                        if (sceneIDs[j].Equals(sp2[i]))
                        {
                            sp2[i] = j.ToString();
                            i = sp2.Length;
                        }
                    }
                }

                for (int i = 0; i < sp3.Length; i++)
                    sp3[i] = CodeReader.Troops.IndexOf("trp_" + sp3[i]).ToString();//is trp_ needed?

                scenes.Add(new Scene(sp1, sp2, sp3, line));
            }

            return scenes.ToArray();
        }

        // ADDED
        public TableauMaterial[] ReadTableauMaterial()
        {
            string[] sp, sp2;
            string line = string.Empty;
            List<TableauMaterial> tableaus = new List<TableauMaterial>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("tableaus = ["))
                    line = sr.ReadLine();
                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = RemNTrimAllXtraSp(sr.ReadLine()).Replace('\"', ' ').Replace(" ", string.Empty);
                    if (line.StartsWith("(\""))
                    {
                        while (!sr.EndOfStream && !line.Contains("]"))
                            line += sr.ReadLine().Trim('\t', ' ');
                        line = line.Split(']')[0].TrimStart('(');
                        sp = line.Split('[');
                        sp2 = sp[0].Split(',');
                        sp[1] = sp[1].Replace("),", "°");
                        sp = sp[1].Split('°');
                        for (int i = 0; i < sp.Length; i++)
                            sp[i] = sp[i] + "),";
                        List<string> sss = new List<string>(sp2);
                        sss.AddRange(sp);
                        sp = sss.ToArray();
                        tableaus.Add(new TableauMaterial(sp, true));
                    }
                }
            }
            return tableaus.ToArray();
        }

        // ADDED
        public SceneProp[] ReadSceneProp()
        {
            int x = 0;
            bool found = false;
            string line = string.Empty;
            List<SceneProp> sceneProps = new List<SceneProp>();
            List<List<string>> definedTriggers = new List<List<string>>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !found)
                {
                    line = sr.ReadLine();
                    if (!line.Equals("scene_props = ["))
                    {
                        if (line.Contains("="))
                        {
                            string[] tmp = line.Split('=');
                            List<string> list = new List<string>
                            {
                                tmp[0].Trim(),
                                tmp[1].Trim()
                            };
                            while (!line.Contains("])") && !sr.EndOfStream)
                            {
                                line = sr.ReadLine().Replace('\t', ' ').Trim();
                                if (line.Length != 0)
                                    list.Add(line);
                            }
                            definedTriggers.Add(list);
                        }
                    }
                    else
                        found = true;
                }

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    List<SimpleTrigger> sTriggerList = new List<SimpleTrigger>();
                    if (line.StartsWith("(\"") && line.Contains("["))
                    {
                        string[] tmp = line.Split('\"');
                        SceneProp sceneProp = new SceneProp(new string[] { tmp[1], tmp[2].Trim(',', '\t', ' '), tmp[3], tmp[5] });
                        if (!line.Contains("]"))
                        {
                            x = 1;
                            found = false;
                            List<List<string>> triggers = new List<List<string>>();
                            List<string> trigger = new List<string>();
                            while (!sr.EndOfStream && !found)
                            {
                                line = sr.ReadLine().Trim('\t', ' ');
                                if (line.Length != 0)
                                    trigger.Add(line);

                                int x2 = x;

                                x += CodeReader.CountCharInString(line, '[');
                                x -= CodeReader.CountCharInString(line, ']');

                                if (x2 == 2 && x == 1)
                                {
                                    triggers.Add(trigger);
                                    trigger = new List<string>();
                                }

                                found = x == 0;
                            }

                            foreach (List<string> t in triggers)
                            {
                                SimpleTrigger sTrigger = new SimpleTrigger(t[0].Trim('(', ',', ' '))
                                {
                                    ConsequencesBlock = GetCompiledCodeLines(CodeReader.GetStringArrayStartFromIndex(t.ToArray(), 2, 1)).Trim().Split()
                                };
                                sTriggerList.Add(sTrigger);
                            }
                        }
                        else
                        {
                            string tmp2 = line.Replace('\t', ' ').Replace(" ", string.Empty);
                            if (tmp2.IndexOf('[') + 1 != tmp2.IndexOf("]"))
                            {
                                tmp2 = line.Substring(line.IndexOf("[") + 1);
                                tmp2 = tmp2.Remove(tmp2.IndexOf("]"));
                                tmp = tmp2.Split(',');
                                foreach (string trigger in tmp)
                                {
                                    for (int i = 0; i < definedTriggers.Count; i++)
                                    {
                                        if (definedTriggers[i][0].Equals(trigger))
                                        {
                                            SimpleTrigger sTrigger = new SimpleTrigger(definedTriggers[i][1].Trim('(', ',', ' '))
                                            {
                                                ConsequencesBlock = GetCompiledCodeLines(CodeReader.GetStringArrayStartFromIndex(definedTriggers[i].ToArray(), 2, 1)).Trim().Split()
                                            };
                                            sTriggerList.Add(sTrigger);
                                            i = definedTriggers.Count;
                                        }
                                    }
                                }
                            }
                        }

                        sceneProp.SimpleTriggers = sTriggerList.ToArray();
                        sceneProps.Add(sceneProp);
                    }
                }
            }
            return sceneProps.ToArray();
        }

        // ADDED
        public Faction[] ReadFaction()
        {
            string[] sp;
            string[] sp2;
            string[] relations;
            string line = string.Empty;
            List<Faction> factions = new List<Faction>();
            List<string> factionLines = new List<string>();
            List<string> factionIDs = new List<string>();
            Faction.ResetIDs();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("factions = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("(\""))
                        factionLines.Add(line);
                }
            }

            relations = new string[factionLines.Count];
            for (int i = 0; i < relations.Length; i++)
                relations[i] = "0";//"0.000000"

            foreach (string fline in factionLines)
                factionIDs.Add(fline.Split('\"')[1]);

            foreach (string fline in factionLines)
            {
                string firstline = string.Empty;
                string secondLine = string.Empty;

                sp = fline.Split('[');
                sp2 = sp[0].Trim(' ', '\t', ',').Split(',');

                sp2 = new string[] { sp2[0].Split('\"')[1], sp2[1].Split('\"')[1], sp2[2].Trim('\t', ' '), sp2[3].Trim('\t', ' ') };

                foreach (string s in sp)
                    firstline += s + ' ';
                firstline = firstline.TrimEnd();

                sp[1] = RemNTrimAllXtraSp(sp[1].Split(']')[0]).Replace(",(", string.Empty).Replace('(', ' ').Replace('\"', ' ').Replace(" ", string.Empty);
                sp2 = sp[1].Split(')');

                foreach (string relPack in sp2)
                    relations[factionIDs.IndexOf(relPack.Split(',')[0])] = relPack.Split(',')[1];

                foreach (string rel in relations)
                    secondLine += rel + ' ';
                secondLine = secondLine.TrimEnd();

                sp[2] = RemNTrimAllXtraSp(sp[2].Split(']')[0]).Replace('\"', ' ').Replace(" ", string.Empty);
                if (sp[2].Length != 0)
                    sp2 = sp[2].Split(',');
                else
                    sp2 = new string[0];

                line = sp2.Length.ToString();
                foreach (string rank in sp2)
                    line += ' ' + rank;

                sp = new string[] { firstline, secondLine, line };

                factions.Add(new Faction(sp));
            }

            return factions.ToArray();
        }

        // ADDED
        public MapIcon[] ReadMapIcon()
        {
            int x;
            bool hasTrigger;
            string[] sp;
            string line = string.Empty;
            List<MapIcon> mapIcons = new List<MapIcon>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("map_icons = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("(\""))
                    {
                        x = CodeReader.CountCharInString(line, ',');
                        hasTrigger = !line.Contains(")");

                        line = RemNTrimAllXtraSp(line.Replace('\"', ' ')).Replace(" ", string.Empty).TrimStart('(').TrimEnd(',', ')');

                        if (x == 5)//simple icon
                            line += ",0,0,0";

                        sp = line.Split(',');

                        if (sp[3].Equals("banner_scale"))
                            sp[3] = "0.3";
                        else if (sp[3].Equals("avatar_scale"))
                            sp[3] = "0.15";

                        if (!ImportantMethods.IsNumericGZ(sp[4]))
                            sp[4] = CodeReader.Sounds.IndexOf(sp[4]).ToString();

                        MapIcon icon = new MapIcon(sp);

                        if (hasTrigger)
                        {
                            List<SimpleTrigger> simpleTriggers = new List<SimpleTrigger>();
                            List<string> codeLines = new List<string>();

                            x = 1;
                            int x2 = 1;

                            if (!sr.EndOfStream)
                                sr.ReadLine();//[

                            while (!sr.EndOfStream && x != 0 && x2 != 0)
                            {
                                line = RemNTrimAllXtraSp(sr.ReadLine()).Replace(" ", string.Empty);

                                x += CodeReader.CountCharInString(line, '(');
                                x -= CodeReader.CountCharInString(line, ')');

                                if (x == 2)
                                {
                                    codeLines.Clear();
                                    SimpleTrigger simpleTrigger = new SimpleTrigger(line.TrimStart('(').TrimEnd(','));
                                    while (!sr.EndOfStream && x2 > 1)
                                    {
                                        line = sr.ReadLine().Trim('\t', ' ');

                                        x2 += CodeReader.CountCharInString(line, '[');
                                        x2 -= CodeReader.CountCharInString(line, ']');

                                        if (line.Length > 1)
                                            codeLines.Add(line);
                                    }
                                    simpleTrigger.ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(GetCompiledCodeLines(codeLines.ToArray()).Trim().Split(), 1);
                                    simpleTriggers.Add(simpleTrigger);
                                }
                            }

                            icon.SimpleTriggers = simpleTriggers.ToArray();
                        }

                        mapIcons.Add(icon);
                    }
                }
            }
            return mapIcons.ToArray();
        }

        // ADDED
        public Animation[] ReadAnimation()
        {
            int x;
            string[] sp;
            string line = string.Empty;
            List<Animation> animations = new List<Animation>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("animations = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = RemNTrimAllXtraSp(sr.ReadLine()).Replace(" ", string.Empty);
                    x = 0;
                    if (line.StartsWith("[\""))
                    {
                        while (!sr.EndOfStream && x != 0) 
                        {
                            x += CodeReader.CountCharInString(line, '[');
                            x -= CodeReader.CountCharInString(line, ']');

                            if (x != 0)
                            {
                                line = line.TrimEnd('\r', '\n');
                                line += RemNTrimAllXtraSp(sr.ReadLine()).Replace(" ", string.Empty);
                            }
                        }

                        line = line.TrimStart('[').TrimEnd(',').Replace(",[", "[").Replace("],", " ").Replace('\"', ' ').Replace(" ", string.Empty);

                        sp = line.Split('[');

                        Animation animation = new Animation(sp[0].Split(','));

                        AnimationSequence[] sequences = new AnimationSequence[sp.Length - 1];
                        for (int i = 0; i < sp.Length; i++)
                        {
                            string tmp;
                            string[] sp2 = sp[i + 1].Split('(');
                            string[] sp3 = sp2[0].TrimEnd(',').Split(',');

                            line = string.Empty;

                            for (int j = 0; j < sp3.Length - 1; j++)
                                line += sp3[j] + ' ';

                            if (ImportantMethods.IsNumericGZ(sp3[sp3.Length - 1]))
                            {
                                line += sp3[sp3.Length - 1];
                            }
                            else if (sp2.Length > 1)
                            {
                                tmp = sp3[sp3.Length - 1];
                                if (tmp.Equals("pack2f") || tmp.Equals("pack4f"))
                                {
                                    sp3 = sp2[1].Split(')')[0].Split(',');

                                    byte ai = GetByte(float.Parse(CodeReader.Repl_DotWComma(sp3[0])));
                                    byte bi = GetByte(float.Parse(CodeReader.Repl_DotWComma(sp3[1])));
                                    byte ci = 0;
                                    byte di = 0;

                                    if (tmp.Equals("pack4f") && sp3.Length >= 4)
                                    {
                                        ci = GetByte(float.Parse(CodeReader.Repl_DotWComma(sp3[2])));
                                        di = GetByte(float.Parse(CodeReader.Repl_DotWComma(sp3[3])));
                                    }

                                    line += (ulong)(di << 24 | ci << 16 | bi << 8 | ai);
                                }
                            }

                            if (sp2.Length > 2)
                            {
                                sp3 = sp2[2].Split(')')[0].Split(',');
                                for (int k = 0; k < sp3.Length; k++)
                                {
                                    line += " " + sp3[k];
                                }
                                tmp = sp2[2].Split(')')[1].TrimStart(',');
                                line += ' ';
                                if (tmp.Length != 0)
                                    line += tmp;
                                else
                                    line += "0.0";
                            }
                            else
                                line += " 0.0, 0.0, 0.0, 0.0";

                            sp2 = line.Split();

                            sp2[0] = CodeReader.Repl_DotWComma(sp2[0]);

                            if (!ImportantMethods.IsNumeric(sp2[0], true))
                                sp[0] = ConvertAnimationConst(sp[0], true);
                            if (!ImportantMethods.IsNumericGZ(sp2[2]))
                                sp[2] = ConvertAnimationConst(sp[2]);
                            if (!ImportantMethods.IsNumericGZ(sp2[3]))
                                sp[3] = ConvertAnimationConst(sp[3]);

                            sequences[i] = new AnimationSequence(sp2);
                        }

                        animation.Sequences = sequences;

                        animations.Add(animation);
                    }
                }
            }
            return animations.ToArray();
        }

        // ADDED
        public PartyTemplate[] ReadPartyTemplate()
        {
            string line = string.Empty;
            string[] sp;
            List<PartyTemplate> partyTemplates = new List<PartyTemplate>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("parties = ["))
                    line = RemNTrimAllXtraSp(sr.ReadLine());

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim();
                    if (line.StartsWith("(\""))
                    {
                        line = line.Remove(line.LastIndexOf("),")).Replace('[', '\t').Replace(']', '\t').Replace('\t', ' ');

                        sp = line.Split(',');
                        for (int i = 0; i < sp.Length; i++)
                            sp[i] = sp[i].Trim();

                        partyTemplates.Add(new PartyTemplate(sp, true));//find better solution like in other classes (also for Party.cs) !!!
                    }
                }
            }
            return partyTemplates.ToArray();
        }

        // NOT IMPLEMENTED
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
        
        // ADDED
        public Party[] ReadParty()
        {
            bool hasDegrees;
            string line = string.Empty;
            string[] sp;
            List<Party> parties = new List<Party>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("parties = ["))
                    line = RemNTrimAllXtraSp(sr.ReadLine());

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim();
                    if (line.StartsWith("(\""))
                    {
                        line = line.Remove(line.LastIndexOf("),")).Replace('[', '\t').Replace(']', '\t').Replace('\t', ' ');

                        hasDegrees = line.Trim().EndsWith("]),");

                        sp = line.Split(',');
                        for (int i = 0; i < sp.Length; i++)
                            sp[i] = sp[i].Trim();

                        parties.Add(new Party(sp, hasDegrees));
                    }
                }
            }
            return parties.ToArray();
        }
        
        // ADDED
        public Skill[] ReadSkill()
        {
            string[] sp;
            string line = string.Empty;
            List<Skill> skills = new List<Skill>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("skills = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim('\t', ' ');
                    if (line.StartsWith("\""))
                    {
                        sp = line.Split('\"');
                        line = sp[4].Trim(' ', '\t', ',');
                        sp[4] = line.Split(',')[0];
                        line = line.Split(',')[1];
                        
                        sp = new string[] { sp[1], sp[3], sp[4], line, sp[5] };

                        skills.Add(new Skill(sp));
                    }
                }
            }
            return skills.ToArray();
        }

        // ADDED
        public PostFX[] ReadPostFX()
        {
            string line = string.Empty;
            List<PostFX> postfxs = new List<PostFX>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("post_fxs = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = RemNTrimAllXtraSp(
                        sr.ReadLine().Split('#')[0].TrimStart('(').Replace("],", " ").Replace('[', ' ').Replace('\"', ' ').Replace(',', ' ')
                    ).TrimEnd(')').Trim();
                    if (!line.Equals("]") && line.Length != 0)
                        postfxs.Add(new PostFX(line));
                }
            }
            return postfxs.ToArray();
        }

        // ADDED
        public ParticleSystem[] ReadParticleSystem()
        {
            List<string[]> rawData = new List<string[]>();
            string line = string.Empty;
            List<ParticleSystem> particleSystems = new List<ParticleSystem>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream && !line.Equals("post_fxs = ["))
                    line = sr.ReadLine();

                while (!sr.EndOfStream && !line.Equals("]"))
                {
                    line = sr.ReadLine().Trim();

                    if (line.StartsWith("(\""))
                    {
                        List<string> list = new List<string>();

                        do
                        {
                            line = RemNTrimAllXtraSp(line.Split('#')[0].Replace('(', ' ').Replace(')', ' ').Replace('\"', ' ').Replace(',', ' ')).Trim();

                            list.Add(line);

                            line = sr.ReadLine().Trim();

                        } while (!line.Equals("),") && line.Length != 0);

                        if (list.Count >= 10)//remove this if - if you want
                        {
                            rawData.Add(new string[] { list[0], list[1] });

                            for (int i = 2; i < 7; i++)
                                rawData.Add(list[i].Split());

                            rawData.Add(new string[] { list[7], list[8], list[9] });

                            if (list.Count == 10)
                                line = "0.000000 0.000000";
                            else
                                line = list[10];

                            rawData.Add(line.Split());

                            //list.Clear();//should be deleted anyways (or not?)

                            particleSystems.Add(new ParticleSystem(rawData));

                            rawData.Clear();
                        }
                    }
                }
            }
            return particleSystems.ToArray();
        }

        // NOT IMPLEMENTED
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

        #region SUPPORT METHODS

        //place in Skriptum ? for every Skriptum Object and public
        private List<HeaderVariable> GetHeaderVariableList(bool duration, List<HeaderVariable> list = null, string file = "header_animations.py")
        {
            string[] tmp;
            string s = string.Empty;
            string startPattern = "animation_const_";
            byte a = 0;
            char[] sp_c = new char[] { 'a', 'b' };

            if (list == null)
                list = new List<HeaderVariable>();

            if (duration)
                a++;

            startPattern += sp_c[a];

            if (startPattern != null)
            {
                using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + file))
                {
                    while (!sr.EndOfStream && !s.Equals(startPattern))
                        s = sr.ReadLine().Split('#')[0];
                    while (!sr.EndOfStream && !s.Equals(string.Empty))
                    {
                        s = sr.ReadLine().Split('#')[0];
                        if (s.Length != 0)
                        {
                            tmp = s.Replace('\t', ' ').Replace(" ", string.Empty).Split('=');
                            tmp[1] = CodeReader.Repl_DotWComma(tmp[1]);
                            s = tmp[1];
                            int x = -1;
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (list[i].VariableValue.Equals(s))
                                {
                                    x = i;
                                    i = list.Count;
                                }
                            }
                            if (x >= 0)
                                list.RemoveAt(x);

                            list.Add(new HeaderVariable(s, tmp[0]));
                        }
                    }
                }
            }
            return list;
        }

        //place in Animation(Sequence) ?
        private string ConvertAnimationConst(string value, bool duration = false)
        {
            string[] tmp;
            List<HeaderVariable> list = GetHeaderVariableList(duration);
            list = GetHeaderVariableList(duration, list, "header_mb_decompiler.py");

            bool plus = value.Contains("+");

            if (plus)
                tmp = value.Split('+');
            else
                tmp = value.Split('-');

            for (int j = 0; j < tmp.Length; j++)
            {
                if (!ImportantMethods.IsNumeric(tmp[j], true))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].VariableName.Equals(tmp[j]))
                        {
                            tmp[j] = list[i].VariableValue;
                            if (tmp[j].StartsWith("0x"))
                                tmp[j] = HexConverter.Hex2Dec_16CHARS(tmp[j].Substring(2)).ToString();
                            i = list.Count;
                        }
                    }
                }
            }

            if (value.Contains("+") || value.Contains("-"))
            {
                if (tmp[0].Contains(","))
                {
                    double dx;
                    double[] d = new double[tmp.Length];
                    for (int i = 0; i < d.Length; i++)
                        d[i] = double.Parse(tmp[i]);
                    dx = d[0];
                    for (int i = 1; i < d.Length; i++)
                        if (plus)
                            dx += d[i];
                        else
                            dx -= d[i];
                    value = d.ToString();
                }
                else
                {
                    ulong ux;
                    ulong[] u = new ulong[tmp.Length];
                    for (int i = 0; i < u.Length; i++)
                        u[i] = ulong.Parse(tmp[i]);
                    ux = u[0];
                    for (int i = 1; i < u.Length; i++)
                        if (plus)
                            ux += u[i];
                        else
                            ux -= u[i];
                    value = u.ToString();
                }
            }

            return value;
        }

        //place in Animation(Sequence) ?
        private static byte GetByte(float f)
        {
            if (f == 0.0)
                return 0;
            int i = (int)(f * 255.0);
            if (i < 1)
                i = 1;
            else if (i > 255)
                i = 255;
            return (byte)i;
        }

        #endregion

        #endregion
    }
}
