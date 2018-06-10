using System;
using System.IO;
using System.Collections.Generic;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;
using static MB_Decompiler_Library.Objects.Skriptum;

namespace MB_Decompiler_Library.IO
{
    public class SourceWriter
    {
        private static bool makeBackup = false;
        private static DataBankList[] allDataBankObjects = new DataBankList[0];

        public static string ModuleFilesPath = @".\moduleFiles\";

        #region Konstanten

        private const string COMMENT_BREAK = "#######################################################################################################################";

        private const string NO_FACE = "0x0000000000000000000000000000000000000000000000000000000000000000";

        public static readonly string[] SOURCES = { "module_scripts.py", "module_mission_templates.py", "module_presentations.py", "module_game_menus.py", "module_troops.py", "module_items.py",
                "module_strings.py", "module_simple_triggers.py", "module_triggers.py", "module_info_pages.py", "module_meshes.py", "module_music.py", "module_quests.py", "module_sounds.py",
                "module_scene_props.py", "module_tableau_materials.py", "module_map_icons.py", "module_dialogs.py", "module_factions.py", "module_animations.py", "module_party_templates.py",
                "module_parties.py", "module_skills.py", "module_postfx.py", "module_skins.py", "module_particle_systems.py", "module_scenes.py" };

        #region SOURCES

        public static string SCRIPT_SOURCE { get { return ModuleFilesPath + SOURCES[0]; } }
        public static string MISSION_TEMPLATE_SOURCE { get { return ModuleFilesPath + SOURCES[1]; } }
        public static string PRESENTATION_SOURCE { get { return ModuleFilesPath + SOURCES[2]; } }
        public static string GAME_MENU_SOURCE { get { return ModuleFilesPath + SOURCES[3]; } }
        public static string TROOPS_SOURCE { get { return ModuleFilesPath + SOURCES[4]; } }
        public static string ITEMS_SOURCE { get { return ModuleFilesPath + SOURCES[5]; } }
        public static string GAME_STRING_SOURCE { get { return ModuleFilesPath + SOURCES[6]; } }
        public static string SIMPLE_TRIGGER_SOURCE { get { return ModuleFilesPath + SOURCES[7]; } }
        public static string TRIGGER_SOURCE { get { return ModuleFilesPath + SOURCES[8]; } }
        public static string INFO_PAGE_SOURCE { get { return ModuleFilesPath + SOURCES[9]; } }
        public static string MESH_SOURCE { get { return ModuleFilesPath + SOURCES[10]; } }
        public static string MUSIC_SOURCE { get { return ModuleFilesPath + SOURCES[11]; } }
        public static string QUEST_SOURCE { get { return ModuleFilesPath + SOURCES[12]; } }
        public static string SOUND_SOURCE { get { return ModuleFilesPath + SOURCES[13]; } }
        public static string SCENE_PROP_SOURCE { get { return ModuleFilesPath + SOURCES[14]; } }
        public static string TABLEAU_MATERIAL_SOURCE { get { return ModuleFilesPath + SOURCES[15]; } }
        public static string MAP_ICON_SOURCE { get { return ModuleFilesPath + SOURCES[16]; } }
        public static string DIALOG_SOURCE { get { return ModuleFilesPath + SOURCES[17]; } }
        public static string FACTION_SOURCE { get { return ModuleFilesPath + SOURCES[18]; } }
        public static string ANIMATION_SOURCE { get { return ModuleFilesPath + SOURCES[19]; } }
        public static string PARTY_TEMPLATE_SOURCE { get { return ModuleFilesPath + SOURCES[20]; } }
        public static string PARTY_SOURCE { get { return ModuleFilesPath + SOURCES[21]; } }
        public static string SKILL_SOURCE { get { return ModuleFilesPath + SOURCES[22]; } }
        public static string POST_FX_SOURCE { get { return ModuleFilesPath + SOURCES[23]; } }
        public static string SKIN_SOURCE { get { return ModuleFilesPath + SOURCES[24]; } }
        public static string PARTICLE_SYSTEM_SOURCE { get { return ModuleFilesPath + SOURCES[25]; } }
        public static string SCENE_SOURCE { get { return ModuleFilesPath + SOURCES[26]; } }

        #endregion

        #endregion

        public static bool MakeBackup
        {
            set { makeBackup = value; }
            get { return makeBackup; }
        }

        private static void MakeBackupOfFile(string source)
        {
            string sourceBACKUP = source + ".BAK";
            if (File.Exists(source))
                if (MakeBackup)
                {
                    if (File.Exists(sourceBACKUP))
                        File.Delete(sourceBACKUP);
                    File.Copy(source, sourceBACKUP);
                }
        }

        public static int WriteAllObjects(List<List<Skriptum>> objects = null)
        {
            SourceWriter w = new SourceWriter();
            if (objects == null)
                objects = CodeReader.ReadAllObjects();
            foreach (List<Skriptum> list in objects)
            {
                if (list.Count > 0)
                {
                    string sourceFile = ModuleFilesPath + SOURCES[list[0].Typ];
                    if (sourceFile.Equals(SCRIPT_SOURCE))
                        w.WriteScripts(list);
                    else if (sourceFile.Equals(MISSION_TEMPLATE_SOURCE))
                        w.WriteMissionTemplates(list);
                    else if (sourceFile.Equals(PRESENTATION_SOURCE))
                        w.WritePresentations(list);
                    else if (sourceFile.Equals(GAME_MENU_SOURCE))
                        w.WriteGameMenus(list);
                    else if (sourceFile.Equals(GAME_STRING_SOURCE))
                        w.WriteGameStrings(list);
                    else if (sourceFile.Equals(SIMPLE_TRIGGER_SOURCE))
                        w.WriteSimpleTriggers(list);
                    else if (sourceFile.Equals(TRIGGER_SOURCE))
                        w.WriteTriggers(list);
                    else if (sourceFile.Equals(INFO_PAGE_SOURCE))
                        w.WriteInfoPages(list);
                    else if (sourceFile.Equals(MESH_SOURCE))
                        w.WriteMeshes(list);
                    else if (sourceFile.Equals(MUSIC_SOURCE))
                        w.WriteMusic(list);
                    else if (sourceFile.Equals(QUEST_SOURCE))
                        w.WriteQuests(list);
                    else if (sourceFile.Equals(SOUND_SOURCE))
                        w.WriteSounds(list);
                    else if (sourceFile.Equals(SCENE_PROP_SOURCE))
                        w.WriteSceneProps(list);
                    else if (sourceFile.Equals(TABLEAU_MATERIAL_SOURCE))
                        w.WriteTableauMaterials(list);
                    else if (sourceFile.Equals(MAP_ICON_SOURCE))
                        w.WriteMapIcons(list);
                    else if (sourceFile.Equals(DIALOG_SOURCE))
                        w.WriteDialogs(list);
                    else if (sourceFile.Equals(FACTION_SOURCE))
                        w.WriteFactions(list);
                    else if (sourceFile.Equals(ANIMATION_SOURCE))
                        w.WriteAnimations(list);
                    else if (sourceFile.Equals(PARTY_TEMPLATE_SOURCE))
                        w.WritePartyTemplates(list);
                    else if (sourceFile.Equals(PARTY_SOURCE))
                        w.WriteParties(list);
                    else if (sourceFile.Equals(SKILL_SOURCE))
                        w.WriteSkills(list);
                    else if (sourceFile.Equals(POST_FX_SOURCE))
                        w.WritePostFXs(list);
                    else if (sourceFile.Equals(SKIN_SOURCE))
                        w.WriteSkins(list);
                    else if (sourceFile.Equals(PARTICLE_SYSTEM_SOURCE))
                        w.WriteParticleSystems(list);
                    else if (sourceFile.Equals(SCENE_SOURCE))
                        w.WriteScenes(list);
                    else if (sourceFile.Equals(TROOPS_SOURCE))
                        w.WriteTroops(list);
                    else if (sourceFile.Equals(ITEMS_SOURCE))
                        w.WriteItems(list);
                    else
                        Console.WriteLine("UNKNOWN: " + list[0].ObjectTyp.ToString() + ':' + list.Count);
                }
                else
                    w.WriteObjectType(list, objects.IndexOf(list)); //Console.WriteLine("TYPE: " + list.GetType().ToString() + "; " + list.GetEnumerator().ToString());
            }
            return objects.Count;
        }

        public bool WriteObjectType(List<Skriptum> list, int type)
        {
            bool b = true;
            string sourceFile = ModuleFilesPath + SOURCES[type];
            if (sourceFile.Equals(SCRIPT_SOURCE))
                WriteScripts(list);
            else if (sourceFile.Equals(MISSION_TEMPLATE_SOURCE))
                WriteMissionTemplates(list);
            else if (sourceFile.Equals(PRESENTATION_SOURCE))
                WritePresentations(list);
            else if (sourceFile.Equals(GAME_MENU_SOURCE))
                WriteGameMenus(list);
            else if (sourceFile.Equals(GAME_STRING_SOURCE))
                WriteGameStrings(list);
            else if (sourceFile.Equals(SIMPLE_TRIGGER_SOURCE))
                WriteSimpleTriggers(list);
            else if (sourceFile.Equals(TRIGGER_SOURCE))
                WriteTriggers(list);
            else if (sourceFile.Equals(INFO_PAGE_SOURCE))
                WriteInfoPages(list);
            else if (sourceFile.Equals(MESH_SOURCE))
                WriteMeshes(list);
            else if (sourceFile.Equals(MUSIC_SOURCE))
                WriteMusic(list);
            else if (sourceFile.Equals(QUEST_SOURCE))
                WriteQuests(list);
            else if (sourceFile.Equals(SOUND_SOURCE))
                WriteSounds(list);
            else if (sourceFile.Equals(SCENE_PROP_SOURCE))
                WriteSceneProps(list);
            else if (sourceFile.Equals(TABLEAU_MATERIAL_SOURCE))
                WriteTableauMaterials(list);
            else if (sourceFile.Equals(MAP_ICON_SOURCE))
                WriteMapIcons(list);
            else if (sourceFile.Equals(DIALOG_SOURCE))
                WriteDialogs(list);
            else if (sourceFile.Equals(FACTION_SOURCE))
                WriteFactions(list);
            else if (sourceFile.Equals(ANIMATION_SOURCE))
                WriteAnimations(list);
            else if (sourceFile.Equals(PARTY_TEMPLATE_SOURCE))
                WritePartyTemplates(list);
            else if (sourceFile.Equals(PARTY_SOURCE))
                WriteParties(list);
            else if (sourceFile.Equals(SKILL_SOURCE))
                WriteSkills(list);
            else if (sourceFile.Equals(POST_FX_SOURCE))
                WritePostFXs(list);
            else if (sourceFile.Equals(SKIN_SOURCE))
                WriteSkins(list);
            else if (sourceFile.Equals(PARTICLE_SYSTEM_SOURCE))
                WriteParticleSystems(list);
            else if (sourceFile.Equals(SCENE_SOURCE))
                WriteScenes(list);
            else if (sourceFile.Equals(TROOPS_SOURCE))
                WriteTroops(list);
            else if (sourceFile.Equals(ITEMS_SOURCE))
                WriteItems(list);
            else
                b = !b; // ERROR
            return b;
        }

        public static void SetImportsForModuleFiles(DataBankList[] importsForFiles)
        {
            allDataBankObjects = new DataBankList[27];
            for (int i = 0; i < importsForFiles.Length; i++)
                allDataBankObjects[i] = importsForFiles[i];
        }

        /*private static bool ExistImports
        {
            get
            {
                bool b = true;
                if (allDataBankObjects.Length == 0)
                    b = false;
                else
                    for (int i = 0; i < allDataBankObjects.Length; i++)
                        if (allDataBankObjects[i] == null)
                            b = false;
                        else if (allDataBankObjects[i].Imports.Length == 0)
                            b = false;
                return b;
            }
        }*/

        public static void Reset(bool makeBackup = false)
        {
            SourceWriter.makeBackup = makeBackup;
            allDataBankObjects = new DataBankList[0];
        }

        #region WRITE_METHODS

        private void WriteImportsDescriptionAndOptionalCode(StreamWriter wr, ObjectType id, bool writeKind = true)
        {
            DataBankList impList = null;
            for (int i = 0; i < allDataBankObjects.Length; i++)
            {
                if (allDataBankObjects[i].ObjectTypeID == (int)id)
                {
                    impList = allDataBankObjects[i];
                    i = allDataBankObjects.Length;
                }
            }
            if (impList != null)
            {
                foreach (string import in impList.Imports)
                    wr.WriteLine(import);
                wr.WriteLine();
                if (impList.DescriptionLines.Length > 0)
                {
                    wr.WriteLine(COMMENT_BREAK);
                    foreach (string descriptionLine in impList.DescriptionLines)
                        wr.WriteLine('#' + descriptionLine);
                }
                wr.WriteLine(COMMENT_BREAK + Environment.NewLine);
                if (impList.CodeLines.Length > 0)
                {
                    wr.WriteLine();
                    foreach (string codeLine in impList.CodeLines)
                        wr.WriteLine(codeLine);
                    wr.WriteLine();
                }
                if (writeKind)
                    wr.WriteLine(impList.Kind + " = [");
            }
            else
                Console.Error.WriteLine("IMPORTLIST ERROR!");
        }

        public int WriteScripts(List<Skriptum> objects)
        {
            MakeBackupOfFile(SCRIPT_SOURCE);
            using (StreamWriter wr = new StreamWriter(SCRIPT_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SCRIPT); //wr.Write(scriptStart);
                foreach (Script s in objects)
                {
                    wr.WriteLine(Environment.NewLine + " (\"" + s.ID + "\",");
                    wr.Write("  [");

                    for (int i = 0; i < s.Code.Length; i++)
                        wr.Write(Environment.NewLine + s.Code[i]);

                    wr.WriteLine("\t]),");
                }
                wr.WriteLine(Environment.NewLine + "] # SCRIPTS END");
            }
            return objects.Count;
        }

        public int WriteMissionTemplates(List<Skriptum> objects)
        {
            MakeBackupOfFile(MISSION_TEMPLATE_SOURCE);
            using (StreamWriter wr = new StreamWriter(MISSION_TEMPLATE_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.MISSION_TEMPLATE); //wr.Write(missionTemplateStart);
                foreach (MissionTemplate m in objects)
                {
                    wr.WriteLine(Environment.NewLine + "  (\"" + m.ID + "\"," + m.Flags + ',' + m.MissionType + ',');
                    wr.WriteLine("    \"" + m.Description + "\",");
                    wr.Write("    [");

                    foreach (Entrypoint entryPoint in m.EntryPoints)
                    {
                        wr.Write("(" + entryPoint.EntryPointNo + ", " + entryPoint.SpawnFlags + ", " + entryPoint.AlterFlags + ", "
                            + entryPoint.AIFlags + ", " + entryPoint.TroopCount + ", [");
                        for (int i = 0; i < entryPoint.SpawnItems.Length; i++)
                        {
                            wr.Write(entryPoint.SpawnItems[i]);
                            if (i < entryPoint.SpawnItems.Length - 1)
                                wr.Write(",");
                        }
                        wr.Write("])," + Environment.NewLine + "\t ");
                    }

                    wr.WriteLine("]," + Environment.NewLine + "\t[");

                    foreach (Trigger trigger in m.Triggers)
                        WriteATriggers(wr, trigger);

                    wr.WriteLine("\t]," + Environment.NewLine + "  ),");
                }

                wr.WriteLine(Environment.NewLine + "] # MISSION_TEMPLATES END");
            }
            return objects.Count;
        }

        public int WritePresentations(List<Skriptum> objects)
        {
            MakeBackupOfFile(PRESENTATION_SOURCE);
            using (StreamWriter wr = new StreamWriter(PRESENTATION_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.PRESENTATION); //wr.Write(presentationStart);
                foreach (Presentation p in objects)
                {
                    wr.Write(Environment.NewLine + "  (\"" + p.ID + "\", " + p.Flags + ", " + p.MeshID + ", [");
                    if (p.SimpleTriggers.Length > 0)
                    {
                        foreach (SimpleTrigger s_trigger in p.SimpleTriggers)
                        {
                            wr.Write(Environment.NewLine + "\t (" + s_trigger.CheckInterval + ',');
                            if (s_trigger.ConsequencesBlock.Length > 2)
                            {
                                wr.WriteLine(Environment.NewLine + "\t   [" + s_trigger.ConsequencesBlock[1].TrimStart('\t', ' '));
                                for (int i = 2; i < s_trigger.ConsequencesBlock.Length; i++)
                                    wr.WriteLine('\t' + s_trigger.ConsequencesBlock[i]);
                                wr.Write("\t\t");
                            }
                            else if (s_trigger.ConsequencesBlock.Length == 2)
                                wr.Write(" [" + s_trigger.ConsequencesBlock[1]);
                            else
                                wr.Write(" [");
                            wr.Write("]),");
                        }
                        wr.WriteLine(Environment.NewLine + "\t]),");
                    }
                    else
                        wr.WriteLine("]),");
                }
                wr.WriteLine(Environment.NewLine + "] # PRESENTATIONS END");
            }
            return objects.Count;
        }

        public int WriteGameMenus(List<Skriptum> objects)
        {
            MakeBackupOfFile(GAME_MENU_SOURCE);
            using (StreamWriter wr = new StreamWriter(GAME_MENU_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.GAME_MENU); //wr.WriteLine(gameMenusStart);
                foreach (GameMenu gameMenu in objects)
                {
                    wr.WriteLine(Environment.NewLine + "  (\"" + gameMenu.ID + "\", " + gameMenu.Flags + ',');
                    wr.WriteLine("\t\"" + gameMenu.Text.Replace('_', ' ') + "\",");
                    wr.Write("\t\"" + gameMenu.MeshName + "\",");
                    if (gameMenu.OperationBlock.Length > 0)
                    {
                        wr.Write(Environment.NewLine + "\t[");
                        for (int i = 0; i < gameMenu.OperationBlock.Length; i++)
                            wr.Write(Environment.NewLine + '\t' + gameMenu.OperationBlock[i]);
                        wr.Write(Environment.NewLine + "\t],");
                    }
                    else
                        wr.Write(" [],");
                    if (gameMenu.MenuOptions.Length > 0)
                    {
                        wr.Write(Environment.NewLine + "\t[");
                        foreach (GameMenuOption gameMenuOption in gameMenu.MenuOptions)
                        {
                            wr.Write(Environment.NewLine + "\t  (\"" + gameMenuOption.Name.Substring(gameMenuOption.Name.IndexOf('_') + 1) + "\",");
                            if (gameMenuOption.ConditionBlock.Length > 0)
                            {
                                wr.Write(Environment.NewLine + "\t   [");
                                for (int i = 0; i < gameMenuOption.ConditionBlock.Length; i++)
                                    wr.Write(Environment.NewLine + "\t  " + gameMenuOption.ConditionBlock[i]);
                                wr.Write(Environment.NewLine + "\t\t],");
                            }
                            else
                                wr.Write(" [],");
                            wr.Write(" \"" + gameMenuOption.Text.Replace('_', ' ') + "\",");
                            if (gameMenuOption.ConsequenceBlock.Length > 0)
                            {
                                wr.Write(Environment.NewLine + "\t   [");
                                for (int i = 0; i < gameMenuOption.ConsequenceBlock.Length; i++)
                                    wr.Write(Environment.NewLine + "\t  " + gameMenuOption.ConsequenceBlock[i]);
                                wr.Write(Environment.NewLine + "\t\t]");
                            }
                            else
                                wr.Write(" []");
                            if (!gameMenuOption.DoorText.Trim().Equals("."))
                                wr.Write(", \"" + gameMenuOption.DoorText + '\"');
                            wr.Write("),");
                        }
                        wr.WriteLine(Environment.NewLine + "\t]),");
                    }
                    else
                        wr.WriteLine(" []),");
                }
                wr.WriteLine(Environment.NewLine + "] # GAME_MENUS END");
            }
            return objects.Count;
        }

        public static List<HeaderVariable> GetTroopModuleConstants()
        {
            List<HeaderVariable> codes = new List<HeaderVariable>();
            ImportsManager impManager = new ImportsManager(CodeReader.FILES_PATH);
            DataBankList dbList = impManager.ReadDataBankInfos()[(byte)ObjectType.TROOP];
            for (int i = 0; i < dbList.CodeLines.Length; i++)
            {
                string tmpCode = dbList.CodeLines[i].Split('#')[0];
                if (tmpCode.Contains("="))
                {
                    if (!tmpCode.Substring(0, 1).Equals(" "))
                    {
                        string[] tmpS = RemoveSpaceAndTab(tmpCode).Split('=');
                        codes.Add(new HeaderVariable(tmpS[1], tmpS[0]));
                    }
                }
            }
            return codes;
        }

        public int WriteTroops(List<Skriptum> objects)
        {
            string tmpCode;
            string[] tmpS;
            List<HeaderVariable> codes = GetTroopModuleConstants();
            using (StreamWriter wr = new StreamWriter(TROOPS_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.TROOP);

                foreach (Troop troop in objects)
                {
                    string scnCode;
                    if (troop.SceneCode.Contains("|"))
                    {
                        tmpS = troop.SceneCode.Split('|');
                        scnCode = CodeReader.Scenes[int.Parse(tmpS[0])];
                        if (int.Parse(tmpS[1]) > 0)
                            scnCode += "|entry(" + tmpS[1].TrimStart();
                        scnCode += ')';
                    }
                    else
                        scnCode = troop.SceneCode;

                    //Troop.GetFlagsFromValue(SkillHunter.Dec2Hex(troop.Flags)) --> troop.Flags
                    wr.Write(Environment.NewLine + " [\"" + troop.ID.Substring(4) + "\",\"" + troop.Name + "\",\"" + troop.PluralName + "\"," + troop.Flags + "," + scnCode + ","
                        + troop.Reserved + "," + CodeReader.Factions[troop.FactionID] + "," + Environment.NewLine + "  [");

                    for (int i = 0; i < troop.Items.Count; i++)
                    {
                        if (troop.ItemFlags[i] != 0)
                            wr.Write("(" + CodeReader.Items[troop.Items[i]] + ", " + Item.GetItemModifiers_IMODBITS(SkillHunter.Dec2Hex_16CHARS(troop.ItemFlags[i])).Substring(1) + ")");//maybe as property as well - later
                        else
                            wr.Write(CodeReader.Items[troop.Items[i]]);
                        if (i < troop.Items.Count - 1)
                            wr.Write(",");
                    }

                    //string attribs = translateAnAttribute("str", troop.Strength, 30, true);
                    //attribs += translateAnAttribute("agi", troop.Agility, 30);
                    //attribs += translateAnAttribute("int", troop.Inteligence, 30);
                    //attribs += translateAnAttribute("cha", troop.Charisma, 30);

                    tmpCode = "str_" + troop.Strength + "|agi_" + troop.Agility + "|int_" + troop.Intelligence + "|cha_" + troop.Charisma; // attribs

                    /*attribs += "|agi_";
                    if (troop.Agility <= 30)
                        attribs += troop.Agility;
                    else
                        attribs += "CHANGE CODE HERE";

                    attribs += "|int_";
                    if (troop.Inteligence <= 30)
                        attribs += troop.Inteligence;
                    else
                        attribs += "CHANGE CODE HERE";

                    attribs += "|cha_";
                    if (troop.Charisma <= 30)
                        attribs += troop.Charisma;
                    else
                        attribs += "CHANGE CODE HERE";
                    */

                    tmpCode = GetTroopAttribs(codes, tmpCode + "|level(" + troop.Level + ")"); // attribs

                    wr.Write("]," + Environment.NewLine + "  " + tmpCode + ',');               // attribs

                    /*for (int i = 0; i < troop.Proficiencies.Length; i++)
                    {
                        wr.Write(troop.Proficiencies[i]);
                        if (i < troop.Proficiencies.Length - 1)
                            wr.Write("|");
                    }
                    wr.Write(",");*/

                    // VERBESSERN DAS 0er NICHT MEHR GESCHRIEBEN WERDEN ???!!!???
                    //wr.Write("wp_one_handed(" + troop.OneHanded + ")|wp_two_handed(" + troop.TwoHanded + ")|wp_polearm(" + troop.Polearm + ")|wp_archery(" + troop.Archery
                    //    + ")|wp_crossbow(" + troop.Crossbow + ")|wp_throwing(" + troop.Throwing);
                    //if (troop.Firearm > 0)
                    //    wr.Write(")|wp_firearm(" + troop.Firearm);
                    //wr.Write("),");
                    // VERBESSERN DAS 0er NICHT MEHR GESCHRIEBEN WERDEN ???!!!???

                    wr.Write(troop.ProficienciesSC + ',');

                    bool first = true;
                    for (int i = 0; i < troop.Skills.Length; i++) // RETHINK THIS PART!!!
                    {
                        if (troop.Skills[i] > 0)
                        {
                            if (i > 0 && !first)
                                wr.Write("|");
                            //if (troop.Skills[i] <= 10)
                                wr.Write(SkillHunter.KNOWS + SkillHunter.Skillnames[i] + troop.Skills[i]);
                            //else
                            //    wr.Write(SkillHunter.KNOWS + SkillHunter.Skillnames[i]                  ); // change code here so any number will work -> 10 + n
                            //wr.Write(translateAnAttribute(SkillHunter.KNOWS + SkillHunter.Skillnames[i].Substring(0, SkillHunter.Skillnames[i].Length - 1), troop.Skills[i], 10, first));
                            first = false;
                        }
                    }
                    if (first)
                        wr.Write("0");

                    if (!troop.Face1.Equals(NO_FACE))
                    {
                        string face1 = troop.Face1;
                        for (int i = 0; i < codes.Count; i++)
                        {
                            if (codes[i].VariableValue.Equals(troop.Face1))
                            {
                                face1 = codes[i].VariableName;
                                i = codes.Count;
                            }
                        }
                        wr.Write(',' + face1);
                        if (!troop.Face2.Equals(NO_FACE))
                        {
                            string face2 = troop.Face2;
                            for (int i = 0; i < codes.Count; i++)
                            {
                                if (codes[i].VariableValue.Equals(troop.Face2))
                                {
                                    face2 = codes[i].VariableName;
                                    i = codes.Count;
                                }
                            }
                            wr.Write(',' + face2);
                        }
                    }

                    if (!troop.DialogImage.Equals("0"))
                        wr.Write(',' + troop.DialogImage);

                    wr.WriteLine("],");
                }
                wr.WriteLine(Environment.NewLine + "] # TROOPS END" + Environment.NewLine);
                wr.WriteLine(Environment.NewLine + "#Troop upgrade declarations");

                foreach (Troop troop in objects)
                {
                    if (troop.UpgradeTroop1 > 0 && troop.UpgradeTroop2 <= 0)
                        wr.WriteLine("upgrade(troops,\"" + troop.ID.Substring(4) + "\",\"" + CodeReader.Troops[troop.UpgradeTroop1].Substring(4) + "\"),");
                    else if (troop.UpgradeTroop1ErrorCode.Length > 0)
                        wr.WriteLine("# " + troop.ID + " UPGRADEPATH - ERRORCODE1: " + troop.UpgradeTroop1ErrorCode);
                    if (troop.UpgradeTroop2 > 0)
                            wr.WriteLine("upgrade2(troops,\"" + troop.ID.Substring(4) + "\",\"" + CodeReader.Troops[troop.UpgradeTroop1].Substring(4) + "\","
                                                                                         + "\"" + CodeReader.Troops[troop.UpgradeTroop2].Substring(4) + "\"),");
                    else if (troop.UpgradeTroop2ErrorCode.Length > 0)
                        wr.WriteLine("# " + troop.ID + " UPGRADEPATH - ERRORCODE2: " + troop.UpgradeTroop2ErrorCode);
                }
            }
            return objects.Count;
        }

        public int WriteItems(List<Skriptum> objects)
        {
            //["winged_great_helmet","Winged Great Helmet",[("maciejowski_helmet_new",0)],itp_merchandise|itp_type_head_armor|itp_covers_head,0,1240,weight(2.75)|abundance(100)|head_armor(55)|body_armor(0)|leg_armor(0)|difficulty(10),imodbits_plate],
            using (StreamWriter wr = new StreamWriter(ITEMS_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.ITEM);
                foreach (Item item in objects)
                {
                    wr.Write(Environment.NewLine + "[\"" + item.ID + "\",\"" + item.Name + "\",[");
                    for (int i = 0; i < item.Meshes.Count; i++)
                    {
                        string[] sp = item.Meshes[i].Trim().Split();
                        wr.Write("(\"" + sp[0] + "\"," + Item.GetMeshKindFromValue(SkillHunter.Dec2Hex_16CHARS(sp[1])) + ')');
                        if (i < item.Meshes.Count - 1)
                            wr.Write(",");
                    }
                    wr.Write("], " + item.Properties);//Item.GetItemPropertiesFromValue(SkillHunter.Dec2Hex_16CHARS(item.SpecialValues[0])));
                    wr.Write(", " + item.CapabilityFlags + ", "//Item.GetItemCapabilityFlagsFromValue(SkillHunter.Dec2Hex_16CHARS(item.SpecialValues[1]), item.ID) + ", "
                        + item.Price + ", weight(" + CodeReader.Repl_CommaWDot(item.Weight.ToString()) + ')');
                    //bool first = true;
                        for (int i = 0; i < item.ItemStats.Length; i++)
                            //{
                            if (item.ItemStats[i] != 0)
                                //{
                                //if (i > 0 && !first)
                                //  wr.Write("|");
                                wr.Write("|" + Item.ItemStatsNames[i + 1] + "(" + ConvertItemStats(item.ItemStats[i], i) + ")");
                    //first = false;
                    //}
                    //}
                    wr.Write(", " + item.ModBits); //string imodbits_SOMETHING = SkillHunter.Dec2Hex_16CHARS(item.SpecialValues[item.SpecialValues.Length - 1]);
                                                      //wr.Write(", " + Item.GetItemModifiers_IMODBITS(imodbits_SOMETHING, true));
                    if (item.Triggers.Count != 0)
                    {
                        wr.Write(", [");
                        for (int i = 0; i < item.Triggers.Count; i++)
                        {
                            string[] scriptLines = item.Triggers[i].Split();
                            SimpleTrigger sTrigger = new SimpleTrigger(scriptLines[0]);
                            string[] tmp = new string[int.Parse(scriptLines[1]) + 1];
                            scriptLines = CodeReader.GetStringArrayStartFromIndex(scriptLines, 1);
                            tmp[0] = "SIMPLE_TRIGGER";
                            sTrigger.ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(CodeReader.DecompileScriptCode(tmp, scriptLines), 1);
                            WriteASimpleTrigger(wr, sTrigger);
                        }
                        wr.Write("]");
                    }
                    else if (item.Triggers.Count == 0 && item.Factions.Count != 0)
                        wr.Write(", []");
                    if (item.Factions.Count != 0)
                    {
                        wr.Write(", [ ");
                        for (int i = 0; i < item.Factions.Count; i++)
                        {
                            wr.Write(CodeReader.Factions[item.Factions[i]]);
                            if (i < item.Factions.Count - 1)
                                wr.Write(",");
                        }
                        wr.Write(" ]");
                    }

                    wr.Write("],");
                }
                wr.WriteLine(Environment.NewLine + "] # ITEMS END");
            }
            return objects.Count;
        }

        public int WriteGameStrings(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(GAME_STRING_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.GAME_STRING);
                for (int i = 0; i < 4; i++)
                    wr.WriteLine("  (\"" + objects[i].ID + "\", \"" + ((GameString)objects[i]).Text + "\"),");
                wr.WriteLine("# Strings before this point are hardwired.");
                string[] tmpS;
                for (int i = 4; i < objects.Count; i++)
                {
                    tmpS = ((GameString)objects[i]).Text.Split('^');
                    wr.Write("  (\"" + objects[i].ID + "\", \"" + tmpS[0]);
                    for (int j = 1; j < tmpS.Length; j++)
                        wr.Write('\\' + Environment.NewLine + '^' + tmpS[j]);
                    wr.WriteLine("\"),");
                }
                wr.WriteLine("] # STRINGS END");
            }
            return objects.Count;
        }

        public int WriteSimpleTriggers(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(SIMPLE_TRIGGER_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SIMPLE_TRIGGER);
                foreach (SimpleTrigger strigger in objects)
                    WriteASimpleTrigger(wr, strigger);
                wr.WriteLine(Environment.NewLine + "] # SIMPLE_TRIGGERS END");
            }
            return objects.Count;
        }

        public int WriteTriggers(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(TRIGGER_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.TRIGGER);
                foreach (Trigger trigger in objects)
                    WriteATriggers(wr, trigger);
                wr.WriteLine(Environment.NewLine + "] # TRIGGERS END");
            }
            return objects.Count;
        }

        public int WriteInfoPages(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(INFO_PAGE_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.INFO_PAGE);
                foreach (InfoPage infoPage in objects)
                    wr.WriteLine(" (\"" + infoPage.ID + "\", \"" + infoPage.Name + "\", \"" + infoPage.Text + "\"),");
                wr.WriteLine(Environment.NewLine + "] # INFO_PAGES END");
            }
            return objects.Count;
        }

        public int WriteMeshes(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(MESH_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.MESH); //wr.WriteLine("from header_meshes import *");
                foreach (Mesh mesh in objects)
                    wr.WriteLine("  (\"" + mesh.ID + "\", " + mesh.Flags + ", \"" + mesh.ResourceName + "\", "
                        + CodeReader.Repl_CommaWDot(mesh.AxisTranslation[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(mesh.AxisTranslation[1].ToString()) + ", " + CodeReader.Repl_CommaWDot(mesh.AxisTranslation[2].ToString()) + ", "
                        + CodeReader.Repl_CommaWDot(mesh.RotationAngle[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(mesh.RotationAngle[1].ToString()) + ", " + CodeReader.Repl_CommaWDot(mesh.RotationAngle[2].ToString()) + ", "
                        + CodeReader.Repl_CommaWDot(mesh.Scale[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(mesh.Scale[1].ToString()) + ", " + CodeReader.Repl_CommaWDot(mesh.Scale[2].ToString()) + "),");
                wr.WriteLine(Environment.NewLine + "] # MESHES END");
            }
            return objects.Count;
        }

        public int WriteMusic(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(MUSIC_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.MUSIC);
                foreach (Music mtrack in objects)
                    wr.WriteLine("  (\"" + mtrack.ID + "\", \"" + mtrack.TrackFile + "\", " + mtrack.TrackFlags + ", " + mtrack.ContinueTrackFlags + "),");
                wr.WriteLine("] # MUSIC END");
            }
            return objects.Count;
        }

        public int WriteQuests(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(QUEST_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.QUEST); //wr.WriteLine("from header_quests import *");
                foreach (Quest quest in objects)
                    wr.WriteLine(" (\"" + quest.ID + "\", \"" + quest.Name + "\", " + quest.Flags + "," + Environment.NewLine + "  \"" + quest.Description + '\"' + Environment.NewLine + "  ),");
                wr.WriteLine(Environment.NewLine + "] # QUESTS END");
            }
            return objects.Count;
        }

        public int WriteSounds(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(SOUND_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SOUND); //wr.WriteLine("from header_sounds import *");
                foreach (Sound sound in objects)
                {
                    wr.Write(" (\"" + sound.ID + "\", " + sound.Flags + ", [");
                    for (int i = 0; i < sound.SoundFiles.Length; i++)
                    {
                        wr.Write('\"' + sound.SoundFiles[i] + '\"');
                        if (i < sound.SoundFiles.Length - 1)
                            wr.Write(",");
                    }
                    wr.WriteLine("]),");
                }
                wr.WriteLine("] # SOUNDS END");
            }
            return objects.Count;
        }

        public int WriteSceneProps(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(SCENE_PROP_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SCENE_PROP);
                foreach (SceneProp sceneProp in objects)
                {
                    wr.Write(Environment.NewLine + " (\"" + sceneProp.ID + "\", " + sceneProp.Flags + ", \"" + sceneProp.MeshName + "\", \"" + sceneProp.PhysicsObjectName + "\", [");
                    foreach (SimpleTrigger strigger in sceneProp.SimpleTriggers)
                    {
                        wr.WriteLine();
                        WriteASimpleTrigger(wr, strigger);
                        wr.WriteLine();
                    }
                    wr.WriteLine("]),");
                }
                wr.WriteLine(Environment.NewLine + "] # SCENE_PROPS END");
            }
            return objects.Count;
        }

        public int WriteTableauMaterials(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(TABLEAU_MATERIAL_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.TABLEAU_MATERIAL);
                foreach (TableauMaterial tableau in objects)
                {
                    wr.Write(Environment.NewLine + "  (\"" + tableau.ID + "\", " + tableau.Flags + ", \"" + tableau.SampleMaterialName + "\", " + tableau.Width + ", " + tableau.Height + ", "
                        + tableau.MinX + ", " + tableau.MinY + ", " + tableau.MaxX + ", " + tableau.MaxY + ",");
                    if (tableau.OperationBlock.Length > 0)
                    {
                        wr.WriteLine(Environment.NewLine + "   [");
                        for (int i = 0; i < tableau.OperationBlock.Length; i++)
                            wr.WriteLine("   " + tableau.OperationBlock[i]);
                        wr.WriteLine("\t   ]),");
                    }
                    else
                        wr.WriteLine(" []),");
                }
                wr.WriteLine("] # TABLEAU_MATERIALS END");
            }
            return objects.Count;
        }

        public int WriteMapIcons(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(MAP_ICON_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.MAP_ICON);
                foreach (MapIcon mapIcon in objects)
                {
                    wr.Write("  (\"" + mapIcon.ID + "\", " + mapIcon.Flags + ", \"" + mapIcon.MapIconName + "\", ");

                    if (mapIcon.Scale == 0.3) // change later
                        wr.Write("banner_scale");
                    else if (mapIcon.Scale == 0.15) // change later
                        wr.Write("avatar_scale");
                    else
                        wr.Write(CodeReader.Repl_CommaWDot(mapIcon.Scale.ToString()));

                    if (!mapIcon.Sound.Equals(CodeReader.Sounds[0]))
                        wr.Write(", " + mapIcon.Sound);
                    else
                        wr.Write(", 0");

                    if (!double.IsNaN(mapIcon.OffsetZ)/* && !double.IsNaN(mapIcon.OffsetY) && !double.IsNaN(mapIcon.OffsetZ)*/) // change if needed
                    {
                        wr.Write(", " + CodeReader.Repl_CommaWDot(mapIcon.OffsetX.ToString()) + ", " + CodeReader.Repl_CommaWDot(mapIcon.OffsetY.ToString()) + ", "
                            + CodeReader.Repl_CommaWDot(mapIcon.OffsetZ.ToString()));
                    }

                    if (mapIcon.SimpleTriggers.Length > 0)
                    {
                        wr.WriteLine("," + Environment.NewLine + "   [");
                        foreach (SimpleTrigger strigger in mapIcon.SimpleTriggers)
                            WriteASimpleTrigger(wr, strigger);
                        wr.Write(Environment.NewLine + "\t ]");
                    }

                    /*if (mapIcon.SimpleTriggers.Length > 0)
                    {
                        wr.WriteLine("," + Environment.NewLine + "   [");
                        foreach (SimpleTrigger strigger in mapIcon.SimpleTriggers)
                            WriteASimpleTrigger(wr, strigger);
                        wr.Write(Environment.NewLine + "\t ]");
                    }
                    else if (mapIcon.OffsetX != double.NaN || mapIcon.OffsetY != double.NaN || mapIcon.OffsetZ != double.NaN)// maybe change later
                        wr.Write(", " + CodeReader.Repl_CommaWDot(mapIcon.OffsetX.ToString()) + ", " + CodeReader.Repl_CommaWDot(mapIcon.OffsetY.ToString()) + ", "
                            + CodeReader.Repl_CommaWDot(mapIcon.OffsetZ.ToString()));*/
                    
                    wr.WriteLine("),");
                }
                wr.WriteLine("] # MAP_ICONS END");
            }
            return objects.Count;
        }

        public int WriteDialogs(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(DIALOG_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.DIALOG);
                foreach (Dialog dialog in objects)
                {
                    wr.WriteLine(Environment.NewLine + "[" + dialog.TalkingPartnerCode + ", \"" + dialog.StartDialogState + "\", ");
                    wr.Write("[");
                    for (int i = 0; i < dialog.ConditionBlock.Length; i++)
                    {
                        wr.Write(dialog.ConditionBlock[i]);
                        if (i < dialog.ConditionBlock.Length - 1)
                            wr.WriteLine();
                    }
                    wr.WriteLine("],");
                    wr.WriteLine('\"' + dialog.DialogText + "\", \"" + dialog.EndDialogState + "\",");
                    wr.Write("[");
                    for (int i = 0; i < dialog.ConsequenceBlock.Length; i++)
                    {
                        wr.Write(dialog.ConsequenceBlock[i]);
                        if (i < dialog.ConsequenceBlock.Length - 1)
                            wr.WriteLine();
                    }
                    wr.WriteLine("]],");
                }
                wr.WriteLine(Environment.NewLine + "] # DIALOGS END");
            }
            return objects.Count;
        }

        public int WriteFactions(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(FACTION_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.FACTION);
                for (int i = 0; i < objects.Count; i++)
                {
                    Faction fac = (Faction)objects[i];
                    wr.Write("  (\"" + fac.ID + "\", \"" + fac.Name + "\", " + fac.Flags + ", " + CodeReader.Repl_CommaWDot(fac.Relations[i].ToString()) + ", [");
                    for (int j = 0; j < fac.Relations.Length; j++)
                    {
                        if (fac.Relations[j] != 0f)
                        {
                            wr.Write("(\"" + objects[j].ID + "\"," + CodeReader.Repl_CommaWDot(fac.Relations[j].ToString()) + ')');
                            if (j < fac.Relations.Length - 1)
                                wr.Write(",");
                        }
                    }
                    wr.Write("], [");
                    for (int j = 0; j < fac.Ranks.Length; j++)
                    {

                    }
                    if (!fac.ColorCode.Equals("AAAAAA"))
                        wr.WriteLine("], 0x" + fac.ColorCode + "),");
                    else
                        wr.WriteLine("]),");
                }
                wr.WriteLine("] # FACTIONS END");
            }
            return objects.Count;
        }

        public int WriteAnimations(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(ANIMATION_SOURCE))
            {
                string tmp;
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.ANIMATION);
                foreach (Animation animation in objects)
                {
                    wr.WriteLine(" [\"" + animation.ID + "\", " + animation.Flags + ", " + animation.MasterFlags + ',');
                    foreach (AnimationSequence anim_sequence in animation.Sequences)
                    {
                        wr.Write("   [" + CodeReader.Repl_CommaWDot(anim_sequence.Duration.ToString()) + ", \"" + anim_sequence.ResourceName + "\", " + anim_sequence.BeginFrame + ", "
                            + anim_sequence.EndFrame + ", " + anim_sequence.Flags);
                        if (anim_sequence.LastNumberGZ != 0 && anim_sequence.LastNumbersFKZ[0].Equals(double.NaN))
                            wr.Write(", " + anim_sequence.LastNumberGZ);
                        else if (!anim_sequence.LastNumbersFKZ[0].Equals(double.NaN))
                        {
                            wr.Write(", " + anim_sequence.LastNumberGZ);
                            wr.Write(", (");
                            tmp = string.Empty;
                            for (int i = 0; i < anim_sequence.LastNumbersFKZ.Length - 1; i++)
                            {
                                if (anim_sequence.LastNumbersFKZ[i] < 0d && Math.Round(anim_sequence.LastNumbersFKZ[i], 5) == 0d)
                                    tmp += '-';
                                tmp += CodeReader.Repl_CommaWDot(Math.Round(anim_sequence.LastNumbersFKZ[i], 5).ToString());
                                if (anim_sequence.LastNumbersFKZ[i] < 0d && Math.Round(anim_sequence.LastNumbersFKZ[i], 5) == 0d)
                                    tmp += ".0";
                                tmp += ','; //wr.Write(CodeReader.repl_CommaWDot(anim_sequence.LastNumbersFKZ[i].ToString()));
                            }
                            wr.Write(tmp.TrimEnd(','));
                            wr.Write(")");
                            if (!anim_sequence.LastNumbersFKZ[anim_sequence.LastNumbersFKZ.Length - 1].Equals(double.NaN))
                                wr.Write(", " + CodeReader.Repl_CommaWDot(Math.Round(anim_sequence.LastNumbersFKZ[anim_sequence.LastNumbersFKZ.Length - 1], 5).ToString()));
                        }
                        wr.WriteLine("],");
                    }
                    wr.WriteLine(" ],");
                }
                wr.WriteLine("] # ANIMATIONS END");
            }
            return objects.Count;
        }

        public int WritePartyTemplates(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(PARTY_TEMPLATE_SOURCE))
            {
                PMember pMember;
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.PARTY_TEMPLATE);
                foreach (PartyTemplate partyTemplate in objects)
                {
                    wr.Write("  (\"" + partyTemplate.ID + "\",\"" + partyTemplate.Name + "\"," + partyTemplate.Flags + "," + partyTemplate.MenuID + ","
                        + partyTemplate.Faction + "," + partyTemplate.Personality + ",[");
                    for (int i = 0; i < partyTemplate.Members.Length; i++)
                    {
                        pMember = partyTemplate.Members[i];
                        if (!pMember.Troop.Equals(PMember.INVALID_TROOP))
                        {
                            wr.Write('(' + pMember.Troop + "," + pMember.MinimumTroops + "," + pMember.MaximumTroops);
                            if (pMember.Flags == 0)
                                wr.Write(")");
                            else
                                wr.Write("," + pMember.Flags + ")");
                            if (i < partyTemplate.Members.Length - 1)
                                if (!partyTemplate.Members[i + 1].Troop.Equals(PMember.INVALID_TROOP))
                                    wr.Write(",");
                        }
                    }
                    wr.WriteLine("]),");
                }
                wr.WriteLine("] # PARTY_TEMPLATE END");
            }
            return objects.Count;
        }

        public int WriteParties(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(PARTY_SOURCE))
            {
                PMember pMember;
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.PARTY);
                foreach (Party party in objects)
                {
                    wr.Write("  (\"" + party.ID + "\", \"" + party.Name + "\", " + party.Flags + ", " + party.MenuID + ", " + party.PartyTemplate + ", " + party.Faction + ", "
                        + party.Personality + ", " + party.AIBehavior + ", " + party.AITargetParty + ", (" + CodeReader.Repl_CommaWDot(party.InitialCoordinates[0].ToString()) + ", "
                        + CodeReader.Repl_CommaWDot(party.InitialCoordinates[1].ToString()) + "), [");
                    for (int i = 0; i < party.Members.Length; i++)
                    {
                        pMember = party.Members[i];
                        if (!pMember.Troop.Equals(PMember.INVALID_TROOP))
                        {
                            wr.Write('(' + pMember.Troop + "," + pMember.MinimumTroops + ","); //+ pMember.Flags);
                            /*if (pMember.Flags == 0)
                                wr.Write(")");
                            else
                                wr.Write("," + pMember.Flags + ")");*/
                            if (pMember.MaximumTroops > 0)
                                wr.Write(pMember.MaximumTroops + ",");
                            wr.Write(pMember.Flags + ")");
                            if (i < party.Members.Length - 1)
                                if (!party.Members[i + 1].Troop.Equals(PMember.INVALID_TROOP))
                                    wr.Write(",");
                        }
                    }
                    wr.Write("]");
                    if (party.PartyDirectionInDegrees != 0f)
                        wr.WriteLine(", " + CodeReader.Repl_CommaWDot(party.PartyDirectionInDegrees.ToString()) + "),");
                    else
                        wr.WriteLine("),");
                }
                wr.WriteLine("] # PARTIES END");
            }
            return objects.Count;
        }

        public int WriteSkills(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(SKILL_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SKILL);
                foreach (Skill skill in objects)
                    wr.WriteLine("  (\"" + skill.ID + "\",\"" + skill.Name + "\"," + skill.Flags + "," + skill.MaxLevel + ",\"" + skill.Description + "\"),");
                wr.WriteLine("] # SKILLS END");
            }
            return objects.Count;
        }

        public int WritePostFXs(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(POST_FX_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.POST_FX);
                foreach (PostFX postFX in objects)
                {
                    wr.Write("  (\"" + postFX.ID + "\", " + postFX.Flags + ", " + postFX.TonemapOperatorType + ", ");
                    for (int i = 0; i < postFX.AllShaderParameters.Count; i++)
                    {
                        WriteAShaderParameter(wr, postFX.AllShaderParameters[i]);
                        if (i < postFX.AllShaderParameters.Count - 1)
                            wr.Write(", ");
                    }
                    wr.WriteLine("),");
                }
                wr.WriteLine("] # POST_FX END");
            }
            return objects.Count;
        }

        public int WriteParticleSystems(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(PARTICLE_SYSTEM_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.PARTICLE_SYSTEM);
                foreach (ParticleSystem pSystem in objects)
                {
                    wr.WriteLine(Environment.NewLine + "\t(\"" + pSystem.ID + "\", " + pSystem.Flags + ", \"" + pSystem.MeshName + "\",");
                    wr.Write("\t " + pSystem.ParticlesPerSecond + ", " + CodeReader.Repl_CommaWDot(pSystem.ParticleLifeTime.ToString()) + ", "
                        + CodeReader.Repl_CommaWDot(pSystem.Damping.ToString()) + ", ");
                    if (pSystem.GravityStrength == -0.00000000001337)
                        wr.Write("-0.0");
                    else
                        wr.Write(CodeReader.Repl_CommaWDot(Math.Round(pSystem.GravityStrength, 6).ToString()));
                    wr.WriteLine(", "
                        + CodeReader.Repl_CommaWDot(pSystem.TurbulanceSize.ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.TurbulanceStrength.ToString()) + ",\t # num_particles, life, damping, gravity_strength, turbulance_size, turbulance_strength");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.AlphaKeys[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.AlphaKeys[1].ToString()) + "), ("
                        + CodeReader.Repl_CommaWDot(pSystem.AlphaKeys[2].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.AlphaKeys[3].ToString()) + "),\t\t # alpha keys");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.RedKeys[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.RedKeys[1].ToString()) + "), ("
                        + CodeReader.Repl_CommaWDot(pSystem.RedKeys[2].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.RedKeys[3].ToString()) + "),\t\t # red keys");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.GreenKeys[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.GreenKeys[1].ToString()) + "), ("
                        + CodeReader.Repl_CommaWDot(pSystem.GreenKeys[2].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.GreenKeys[3].ToString()) + "),\t\t # green keys");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.BlueKeys[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.BlueKeys[1].ToString()) + "), ("
                        + CodeReader.Repl_CommaWDot(pSystem.BlueKeys[2].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.BlueKeys[3].ToString()) + "),\t\t # blue keys");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.ScaleKeys[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.ScaleKeys[1].ToString()) + "),   ("
                        + CodeReader.Repl_CommaWDot(pSystem.ScaleKeys[2].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.ScaleKeys[3].ToString()) + "),\t\t # scale keys");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.EmitBoxScale[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.EmitBoxScale[1].ToString()) + ", "
                        + CodeReader.Repl_CommaWDot(pSystem.EmitBoxScale[2].ToString()) + "),\t\t\t # emit box size");
                    wr.WriteLine("\t (" + CodeReader.Repl_CommaWDot(pSystem.EmitVelocity[0].ToString()) + ", " + CodeReader.Repl_CommaWDot(pSystem.EmitVelocity[1].ToString()) + ", "
                        + CodeReader.Repl_CommaWDot(pSystem.EmitVelocity[2].ToString()) + "),\t\t\t # emit velocity");
                    wr.WriteLine("\t " + CodeReader.Repl_CommaWDot(pSystem.EmitDirectionRandomness.ToString()) + ", \t\t\t\t\t # emit dir randomness");
                    if (!pSystem.ParticleRotationSpeed.Equals(double.NaN))
                    {
                        wr.WriteLine("\t " + CodeReader.Repl_CommaWDot(pSystem.ParticleRotationSpeed.ToString()) + ", \t\t\t\t\t # rotation speed");
                        wr.WriteLine("\t " + CodeReader.Repl_CommaWDot(pSystem.ParticleRotationDamping.ToString()) + ", \t\t\t\t\t # rotation damping");
                    }
                    wr.WriteLine("\t),");
                }
                wr.WriteLine("] # PARTICLE_SYSTEMS END");
            }
            return objects.Count;
        }

        public int WriteSkins(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(SKIN_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SKIN, false);

                foreach (Skin skin in objects)
                {
                    wr.WriteLine(Environment.NewLine + skin.ID + "_face_keys = [");
                    foreach (FaceKey faceKey in skin.FaceKeys)
                        wr.WriteLine("(" + faceKey.Width + "," + faceKey.Height + ", " + CodeReader.Repl_MinusZero(faceKey.CorX) + ','
                            + CodeReader.Repl_MinusZero(faceKey.CorY) + ", \"" + faceKey.Text + "\"),");
                    wr.WriteLine("]");
                }

                foreach (Skin skin in objects)
                {
                    wr.WriteLine(Environment.NewLine + '#' + skin.ID);
                    for (int i = 0; i < skin.FaceKeys.Length; i++)
                        wr.WriteLine(skin.ID + '_' + skin.FaceKeys[i].ID + " = " + i);
                }

                wr.WriteLine(Environment.NewLine + "comp_less_than = -1" + Environment.NewLine + "comp_greater_than = 1");

                wr.Write(Environment.NewLine + "skins = [");
                foreach (Skin skin in objects)
                {
                    wr.WriteLine(Environment.NewLine + "  (");
                    wr.WriteLine("\t\"" + skin.ID + "\", " + Skin.SkinFlags[skin.Flags] + ",");
                    wr.WriteLine("\t\"" + skin.BodyMesh + "\", \"" + skin.CalfMesh + "\", \"" + skin.HandMesh + "\",");
                    wr.WriteLine("\t\"" + skin.HeadMesh + "\", " + skin.ID + "_face_keys,");
                    wr.Write("\t[");
                    for (int i = 0; i < skin.HairMeshes.Length; i++)
                    {
                        wr.Write('\"' + skin.HairMeshes[i] + '\"');
                        if (i < skin.HairMeshes.Length - 1)
                            wr.Write(",");
                    }
                    wr.WriteLine("], #hair_meshes");
                    wr.Write("\t[");
                    for (int i = 0; i < skin.BeardMeshes.Length; i++)
                    {
                        wr.Write('\"' + skin.BeardMeshes[i] + '\"');
                        if (i < skin.BeardMeshes.Length - 1)
                            wr.Write(",");
                    }
                    wr.WriteLine("], #beard_meshes");
                    wr.Write("\t[");
                    for (int i = 0; i < skin.HairTextures.Length; i++)
                    {
                        wr.Write('\"' + skin.HairTextures[i] + '\"');
                        if (i < skin.HairTextures.Length - 1)
                            wr.Write(",");
                    }
                    wr.WriteLine("], #hair_textures");
                    wr.Write("\t[");
                    for (int i = 0; i < skin.BeardTextures.Length; i++)
                    {
                        wr.Write('\"' + skin.BeardTextures[i] + '\"');
                        if (i < skin.BeardTextures.Length - 1)
                            wr.Write(",");
                    }
                    wr.WriteLine("], #beard_materials");
                    wr.WriteLine("\t[");
                    foreach (FaceTexture faceTexture in skin.FaceTextures)
                    {
                        wr.Write("\t (\"" + faceTexture.Name + "\",0x" + SkillHunter.Dec2Hex_16CHARS(faceTexture.PrimaryHexValue).ToLower().TrimStart('0') + ",[");
                        for (int i = 0; i < faceTexture.Textures.Length; i++)
                        {
                            wr.Write('\"' + faceTexture.Textures[i] + '\"');
                            if (i < faceTexture.Textures.Length - 1)
                                wr.Write(",");
                        }
                        wr.Write("],[");
                        for (int i = 0; i < faceTexture.TextureHexValues.Length; i++)
                        {
                            wr.Write("0x" + SkillHunter.Dec2Hex_16CHARS(faceTexture.TextureHexValues[i]).ToLower().TrimStart('0'));
                            if (i < faceTexture.TextureHexValues.Length - 1)
                                wr.Write(",");
                        }
                        wr.WriteLine("]),");
                    }
                    wr.WriteLine("\t ], #face_textures");
                    wr.Write("\t[");
                    for (int i = 0; i < skin.Voices.Length; i++)
                    {
                        wr.Write("(" + Skin.SoundKinds[(int)skin.Voices[i].Value] + ",\"" + skin.Voices[i].Name + "\")");
                        if (i < skin.Voices.Length - 1)
                            wr.Write(",");
                    }
                    wr.WriteLine("], #voice sounds");
                    wr.WriteLine("\t\"" + skin.SkeletonName + "\", " + CodeReader.Repl_CommaWDot(skin.Scale.ToString()) + ',');
                    wr.WriteLine('\t' + skin.BloodParticle1 + ',' + skin.BloodParticle2 + ',');
                    if (skin.FaceKeyConstraints.Length > 0)
                    {
                        wr.WriteLine("\t[");
                        string faceKeyID;
                        foreach (FaceKeyConstraint faceKeyConstraint in skin.FaceKeyConstraints)
                        {
                            wr.Write("\t [" + CodeReader.Repl_CommaWDot(faceKeyConstraint.Number.ToString()) + ", " + Skin.GetCompMode(faceKeyConstraint.CompMode) + ", ");
                            for (int i = 0; i < faceKeyConstraint.ValuesINT.Length; i++)
                            {
                                faceKeyID = string.Empty;
                                if (skin.FaceKeys.Length > faceKeyConstraint.ValuesINT[i])
                                    faceKeyID += skin.ID + '_' + skin.FaceKeys[faceKeyConstraint.ValuesINT[i]].ID;
                                else
                                    faceKeyID += faceKeyConstraint.ValuesINT[i];
                                wr.Write("(" + faceKeyConstraint.ValuesDOUBLE[i] + "," + faceKeyID + ")");
                                if (i < faceKeyConstraint.ValuesINT.Length - 1)
                                    wr.Write(", ");
                            }
                            wr.WriteLine("],");
                        }
                        wr.WriteLine("\t ]");
                    }
                    wr.WriteLine("  ),");
                }
                wr.WriteLine("] # SKINS END");
            }
            return objects.Count;
        }

        public int WriteScenes(List<Skriptum> objects)
        {
            using (StreamWriter wr = new StreamWriter(SCENE_SOURCE))
            {
                WriteImportsDescriptionAndOptionalCode(wr, ObjectType.SCENE);
                foreach (Scene scene in objects)
                {
                    wr.WriteLine("  (\"" + scene.ID + "\", " + scene.Flags + ", \"" + scene.MeshName + "\", \"" + scene.BodyName + "\", ("
                        + CodeReader.Repl_CommaWDot(scene.MinPosition[0].ToString()) + ',' + CodeReader.Repl_CommaWDot(scene.MinPosition[1].ToString()) + "),("
                        + CodeReader.Repl_CommaWDot(scene.MaxPosition[0].ToString()) + ',' + CodeReader.Repl_CommaWDot(scene.MaxPosition[1].ToString()) + "),"
                        + CodeReader.Repl_CommaWDot(scene.WaterLevel.ToString()) + ",\"" + scene.TerrainCode + "\",");
                    wr.Write("    [");
                    for (int i = 0; i < scene.OtherScenes.Length; i++)
                    {
                        wr.Write('\"' + scene.OtherScenes[i].Substring(scene.OtherScenes[i].IndexOf('_') + 1) + '\"');
                        if (i < scene.OtherScenes.Length - 1)
                            wr.Write(",");
                    }
                    wr.Write("],[");
                    for (int i = 0; i < scene.ChestTroops.Length; i++)
                    {
                        wr.Write('\"' + scene.ChestTroops[i].Substring(scene.ChestTroops[i].IndexOf('_') + 1) + '\"');
                        if (i < scene.ChestTroops.Length - 1)
                            wr.Write(",");
                    }
                    if (scene.TerrainBase.Length > 1)
                        wr.WriteLine("], \"" + scene.TerrainBase + "\"),");
                    else
                        wr.WriteLine("]),");
                }
                wr.WriteLine("] # SCENES END");
            }
            return objects.Count;
        }

        #region SUPPORT_METHODS

        private string ConvertItemStats(int value, int index)
        {
            string retur = string.Empty;
            string itemStat = SkillHunter.Dec2Hex_16CHARS(value).TrimStart('0');
            if (index > 9)
            {
                if (itemStat[0] == '2' && itemStat.Length == 3)
                    retur = Convert.ToString(SkillHunter.Hex2Dec(itemStat.Substring(1))) + ",blunt";
                else if (itemStat[0] == '1' && itemStat.Length == 3)
                    retur = Convert.ToString(SkillHunter.Hex2Dec(itemStat.Substring(1))) + ",pierce";
                else
                    retur = Convert.ToString(SkillHunter.Hex2Dec(itemStat)) + ",cut";
            }
            else
                retur = value.ToString();
            return retur;
        }

        private static string TranslateAnAttribute(string attribute, int value, int maxValue, bool isFirstAttribute = false)
        {
            int q = 0;
            string attribX = Get2ndAttribSprtr(attribute);
            if (isFirstAttribute)
                attribX = attribX.Substring(1);
            if (value <= maxValue)
                attribX += value;
            else
            {
                int thirtys = value / maxValue;
                int rest = value % maxValue;
                if (thirtys > 1)
                {
                    int x = maxValue;
                    int y = 0;
                    for (int i = 0; i < thirtys; i++)
                    {
                        if (i > 0)
                            attribX += Get2ndAttribSprtr(attribute);
                        attribX += x;
                        q += x;
                        x--;
                        y += i;
                    }

                    int rest2 = y + rest;
                    int z = rest2 - x;
                    if (rest2 > 0)
                    {
                        if (z >= 0)
                        {
                            attribX += Get2ndAttribSprtr(attribute) + x;
                            if (z > 0)
                                attribX += Get2ndAttribSprtr(attribute) + z;
                            q += x + z;
                        }
                        else
                        {
                            attribX += Get2ndAttribSprtr(attribute) + rest2;
                            q += rest2;
                        }
                    }
                    else
                        attribX += "|ERRORCODE_1";
                }
                else
                {
                    attribX += maxValue;
                    if (rest > 0)
                    {
                        attribX += Get2ndAttribSprtr(attribute) + rest;
                        q += maxValue + rest;
                    }
                    else
                        attribX += "|ERRORCODE_2";
                }
                if (q != value)
                    attribX += "|ERRORCODE_3";
            }
            return attribX;
        }

        private static string Get2ndAttribSprtr(string s) { return '|' + s + '_'; }

        private void WriteATriggers(StreamWriter wr, Trigger trigger)
        {
            wr.Write("\t  " + Environment.NewLine + "\t  (" + trigger.CheckInterval + ", " + trigger.DelayInterval + ", " + trigger.ReArmInterval + ',');

            if (trigger.ConditionBlock.Length > 0)
            {
                wr.WriteLine(Environment.NewLine + "\t  [");
                for (int i = 0; i < trigger.ConditionBlock.Length; i++)
                    wr.WriteLine('\t' + trigger.ConditionBlock[i]);
                wr.Write("\t  ");
            }
            else
                wr.Write(" [");

            wr.Write("],");

            if (trigger.ConsequencesBlock.Length > 0)
            {
                wr.WriteLine(Environment.NewLine + "\t  [");

                for (int i = 0; i < trigger.ConsequencesBlock.Length; i++)
                    wr.WriteLine('\t' + trigger.ConsequencesBlock[i]);

                wr.WriteLine("\t  ]),");
            }
            else
                wr.WriteLine(" []),");
        }

        private void WriteASimpleTrigger(StreamWriter wr, SimpleTrigger strigger)
        {
            wr.WriteLine(Environment.NewLine + "  (" + strigger.CheckInterval + ',');
            wr.WriteLine("   [");
            for (int i = 0; i < strigger.ConsequencesBlock.Length; i++)
                wr.WriteLine(strigger.ConsequencesBlock[i]);
            wr.WriteLine("\t]),");
        }

        private void WriteAShaderParameter(StreamWriter wr, string[] s)
        {
            wr.Write("[");
            for (int i = 0; i < s.Length; i++)
            {
                wr.Write(CodeReader.Repl_CommaWDot(s[i]));
                if (i < s.Length - 1)
                    wr.Write(", ");
            }
            wr.Write("]");
        }

        public static string RemoveSpaceAndTab(string s)
        {
            return s.Replace(" ", string.Empty).Replace("\t", string.Empty);
        }

        private string GetTroopAttribs(HeaderVariable codeX, string attribs)
        {
            //byte woutLevel = 0;
            string retur = string.Empty;
            string[] split = attribs.Split('|');
            string[] split2 = codeX.VariableValue.Split('|');
            //if (!codeX.VariableValue.Contains("level"))
            //    woutLevel++;
            if (split2.Length == split.Length - /*(woutLevel * /**/1/*)/**/)
            {
                bool b = false;
                for (int i = 0; i < split2.Length; i++)
                {
                    if (b == true || i == 0)
                    {
                        string attribXN, attribXV;
                        string tmp = RemoveSpaceAndTab(split2[i]);
                        b = false;
                        //if (!split2[i].Contains("level"))
                        //{
                        attribXN = tmp.Remove(tmp.LastIndexOf('_'));
                        attribXV = tmp.Substring(tmp.LastIndexOf('_') + 1);
                        /*}
                        else
                        {
                            attribXN = "level";
                            attribXV = tmp.TrimEnd(')').Substring(6);
                        }*/
                        for (int j = 0; j < split.Length - 1; j++)
                        {
                            string attribTroopN, attribTroopV;
                            string tmp2 = RemoveSpaceAndTab(split[j]);
                            //if (!split[j].Contains("level"))
                            //{
                            attribTroopN = tmp2.Remove(tmp2.LastIndexOf('_'));
                            attribTroopV = tmp2.Substring(tmp2.LastIndexOf('_') + 1);
                            /*}
                            else
                            {
                                attribTroopN = "level";
                                attribTroopV = tmp.TrimEnd(')').Substring(6);
                            }*/
                            if (attribTroopN.Equals(attribXN) && attribTroopV.Equals(attribXV))
                            {
                                b = true;
                                j = split.Length;
                            }
                            //}
                        }
                        //}
                    }
                }
                if (b)
                {
                    /*if (woutLevel == 0)
                    {
                        string t = attribs;
                        string t2 = codeX.VariableValue;
                        t = t.Substring(t.IndexOf("level(") + 1).Split(')')[0];
                        t2 = t2.Substring(t2.IndexOf("level(") + 1).Split(')')[0];
                        if (t.Equals(t2))
                            retur = codeX.VariableName;
                    }
                    else*/
                        retur = codeX.VariableName + attribs.Substring(attribs.LastIndexOf('|'));
                    //x = codes.Count;
                    //MessageBox.Show(retur);
                }
            }
            if (retur.Equals(string.Empty))
                retur = attribs;
            return retur;
        }

        private string GetTroopAttribs(HeaderVariable codeX, string attribs, bool ttt)
        {
            //byte woutLevel = 0;
            string retur = string.Empty;
            string[] split = attribs.Split('|');
            string[] split2 = codeX.VariableValue.Split('|');
            //if (!codeX.VariableValue.Contains("level"))
            //    woutLevel++;
            if (split2.Length == split.Length)
            {
                bool b = false;
                for (int i = 0; i < split2.Length - 1; i++)
                {
                    string attribXN, attribXV;
                    string tmp = RemoveSpaceAndTab(split2[i]);
                    b = false;
                    //if (!split2[i].Contains("level"))
                    //{
                    attribXN = tmp.Remove(tmp.LastIndexOf('_'));
                    attribXV = tmp.Substring(tmp.LastIndexOf('_') + 1);
                    /*}
                    else
                    {
                        attribXN = "level";
                        attribXV = tmp.TrimEnd(')').Substring(6);
                    }*/
                    for (int j = 0; j < split.Length - 1; j++)
                    {
                        string attribTroopN, attribTroopV;
                        tmp = RemoveSpaceAndTab(split[j]);
                        //if (!split[j].Contains("level"))
                        //{
                        attribTroopN = tmp.Remove(tmp.LastIndexOf('_'));
                        attribTroopV = tmp.Substring(tmp.LastIndexOf('_') + 1);
                        /*}
                        else
                        {
                            attribTroopN = "level";
                            attribTroopV = tmp.TrimEnd(')').Substring(6);
                        }*/
                        if (attribTroopN.Equals(attribXN) && attribTroopV.Equals(attribXV))
                        {
                            b = true;
                            j = split.Length;
                        }
                        //}
                    }
                    //}
                }
                if (b)
                {
                    /*if (woutLevel == 0)
                    {*/
                        
                    try
                    {
                        string t = attribs;
                        string t2 = codeX.VariableValue;
                        t = t.Substring(t.IndexOf("level(") + 1).Split(')')[0];
                        t2 = t2.Substring(t2.IndexOf("level(") + 1).Split(')')[0];
                        if (t.Equals(t2))
                            retur = codeX.VariableName;
                    }
                    catch (Exception et)
                    {
                        System.Windows.Forms.MessageBox.Show(et.ToString());
                    }
                       
                    /*}
                    else*/
                    //retur = codeX.VariableName + attribs.Substring(attribs.LastIndexOf('|'));
                    //x = codes.Count;
                }
            }
            if (retur.Equals(string.Empty))
                retur = attribs;
            return retur;
        }

        private string GetTroopAttribs(List<HeaderVariable> codes, string attribs)
        {
            string retur = string.Empty;

            for (int x = 0; x < codes.Count; x++)
                if (codes[x].VariableValue.Contains("|") && codes[x].VariableValue.Contains("str_") && codes[x].VariableValue.Contains("agi_")
                        && codes[x].VariableValue.Contains("int_") && codes[x].VariableValue.Contains("cha_")/* && !codes[x].VariableValue.Contains("level")*/)
                {
                    if (!codes[x].VariableValue.Contains("level"))
                        retur = GetTroopAttribs(codes[x], attribs);
                    else
                        retur = GetTroopAttribs(codes[x], attribs, true);
                    if (!retur.Equals(attribs))
                        x = codes.Count;
                }

            if (retur.Equals(string.Empty))
                retur = attribs;
            //string retur = getTroopAttribsWithoutLevel(codes, attribs);
            //if (!retur.Equals(attribs) && !retur.Contains("multi"))
            //    MessageBox.Show(retur + Environment.NewLine + attribs);
            return retur;
        }

        #endregion

        #endregion

    }
}
