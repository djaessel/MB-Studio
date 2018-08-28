using importantLib;
using MB_Studio_Library.Objects;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using MB_Studio_Library.Objects.Support;

namespace MB_Studio_Library.IO
{
    public class CodeWriter
    {
        public static bool IsFinished { get; private set; }
        public static string ModuleSystem { get; private set; }
        public static string DefaultModuleSystemPath { get; private set; }

        private static List<string> canFailOperations = new List<string>();
        private static List<string> lhsOperations = new List<string>();
        private static List<string> globalLhsOperations = new List<string>();

        #region MODULE TYPE LISTS

        private static List<string> reservedVariables = new List<string>();// MODULE VARIABLES

        private static List<string> globalVarsList = new List<string>();
        private static List<int> globalVarsUses = new List<int>();

        private static List<Skriptum> strings = new List<Skriptum>();

        private static List<Skriptum> skills = new List<Skriptum>();

        private static List<Skriptum> tracks = new List<Skriptum>();

        private static List<Skriptum> animations = new List<Skriptum>();
        private static List<int> animationIndices = new List<int>();

        private static List<Skriptum> meshes = new List<Skriptum>();

        private static List<Skriptum> sounds = new List<Skriptum>();
        private static List<List<object[]>> soundsArray = new List<List<object[]>>();

        private static List<Skriptum> skins = new List<Skriptum>();

        private static List<Skriptum> factions = new List<Skriptum>();

        private static List<Skriptum> scenes = new List<Skriptum>();

        private static List<Skriptum> particleSystems = new List<Skriptum>();

        private static List<Skriptum> sceneProps = new List<Skriptum>();

        private static List<Skriptum> troops = new List<Skriptum>();

        private static List<Skriptum> tableaus = new List<Skriptum>();

        private static List<Skriptum> scripts = new List<Skriptum>();

        private static List<Skriptum> quests = new List<Skriptum>();

        private static List<Skriptum> presentations = new List<Skriptum>();

        private static List<Skriptum> missionTemplates = new List<Skriptum>();

        private static List<Skriptum> menus = new List<Skriptum>();

        private static List<Skriptum> mapIcons = new List<Skriptum>();

        private static List<Skriptum> items = new List<Skriptum>();

        private static List<Skriptum> partyTemplates = new List<Skriptum>();

        private static List<Skriptum> parties = new List<Skriptum>();

        private static List<Skriptum> dialogs = new List<Skriptum>();

        private static List<Skriptum> simpleTriggers = new List<Skriptum>();

        private static List<Skriptum> triggers = new List<Skriptum>();

        private static List<Skriptum> infoPages = new List<Skriptum>();

        private static List<Skriptum> postfxParams = new List<Skriptum>();

        #endregion

        enum TagType : int
        {
            Register,
            Variable,
            String,
            Item,
            Troop,
            Faction,
            Quest,
            PartyTemplate,
            Party,
            Scene,
            MissionTemplate,
            Menu,
            Script,
            ParticleSystem,
            SceneProp,
            Sound,
            LocalVariable,
            MapIcon,
            Skill,
            Mesh,
            Presentation,
            QuickString,
            Track,
            Tableau,
            Animation,
            // ...
            End
        }

        private const int OP_NUM_VALUE_BITS = 24 + 32;

        private const int OP_MASK_REGISTER = ((int)TagType.Register) << OP_NUM_VALUE_BITS;
        private const int OP_MASK_VARIABLE = ((int)TagType.Variable) << OP_NUM_VALUE_BITS;
        private const int OP_MASK_QUEST_INDEX = ((int)TagType.Quest) << OP_NUM_VALUE_BITS;
        private const int OP_MASK_LOCAL_VARIABLE = ((int)TagType.LocalVariable) << OP_NUM_VALUE_BITS;
        private const int OP_MASK_QUICK_STRING = ((int)TagType.QuickString) << OP_NUM_VALUE_BITS;

        //private string sourcePath, destinationPath;

        public const string EOF_TXT = "EOF";

        public const byte DEFAULT_FILES = 4;

        /*public CodeWriter(string sourcePath, string destinationPath)
        {
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
            CheckPaths();
        }*/

        public static void CheckPaths()
        {
            if (DefaultModuleSystemPath == null)
                DefaultModuleSystemPath = Path.GetFullPath(CodeReader.FILES_PATH + "\\moduleSystem\\");
            if (ModuleSystem == null)
                ModuleSystem = Path.GetFullPath(CodeReader.ProjectPath + "\\moduleSystem\\");
        }

        public static void WriteAllCode(Control consoleOutput, string exportDir)
        {
            CheckPaths();
            var paras = new object[] { consoleOutput, exportDir };
            Thread t = new Thread(new ParameterizedThreadStart(WriteCode)) { IsBackground = true };
            t.Start(paras);
        }

        private static void WriteCode(object param)
        {
            IsFinished = false;

            PrepareAndProcessFiles();

            object[] paras = (object[])param;

            RichTextBox consoleOutput = (RichTextBox)paras[0];
            Form parentForm = consoleOutput.FindForm();
            ControlWriter controlTextWriter = new ControlWriter(consoleOutput, parentForm);

            //maybe make separat control for errors
            Console.SetError(controlTextWriter); //new ControlWriter(consoleOutput, consoleOutput.FindForm())
            Console.SetOut(controlTextWriter);   //new ControlWriter(consoleOutput, consoleOutput.FindForm())

            WriteIDFiles();

            ReadProcessAndBuild((string)paras[1]);

            IsFinished = true;

            Console.SetError(Console.Error);
            Console.SetOut(Console.Out);
        }

        private static void WriteIDFiles()
        {
            Console.Write("Writing ID files");

            int sourceIndex = 0;
            int countFiles = CodeReader.Elements.Length - 2;
            for (int i = 0; i < countFiles; i++) // OHNE QUICKSTRINGS UND GLOABLAVARIABLES
            {
                if (i == 7 || i == 15 || i == 21)// CHECK IF ALL CORRECT!!!
                {
                    sourceIndex++;
                    if (i == 7)
                        sourceIndex++;
                    Console.Write('.');
                }

                using (StreamWriter wr = new StreamWriter(GetIDFileNameByIndex(sourceIndex)))
                    for (int j = 0; j < CodeReader.Elements[i].Count; j++)
                        wr.WriteLine(CodeReader.Elements[i][j] + " = " + j);

                sourceIndex++;
            }

            Console.WriteLine("Done");
        }

        private static string GetIDFileNameByIndex(int index)
        {
            string idFile = ModuleSystem + "ID";
            if (SourceWriter.SOURCES[index].Contains("game_menus"))
                idFile += "_menus.py";
            else if (SourceWriter.SOURCES[index].Contains("postfx"))
                idFile += "_postfx_params.py";
            else
                idFile += SourceWriter.SOURCES[index].Substring(6);
            return idFile;
        }

        private static void ReadProcessAndBuild(string exportDir)
        {
            SaveAllCodes(exportDir);

            Console.Write("__________________________________________________" + Environment.NewLine
                        + " Finished compiling!" + Environment.NewLine + Environment.NewLine
                        + " Cleaning up...");

            string[] files = Directory.GetFiles(ModuleSystem);
            foreach (string file in files)
            {
                string nameEnd = file.Substring(file.LastIndexOf('.') + 1);
                if (nameEnd.Equals("pyc"))
                    File.Delete(file);
            }

            Console.WriteLine("Done" + Environment.NewLine);
        }

        private static void PrepareAndProcessFiles()
        {
            string headerFilesPath = CodeReader.ProjectPath + "\\headerFiles";
            string moduleFilesPath = CodeReader.ProjectPath + "\\moduleFiles";

            string[] headerFiles = Directory.GetFiles(headerFilesPath);
            string[] moduleFiles;// = Directory.GetFiles(moduleFilesPath);

            SourceWriter.WriteAllObjects();
            moduleFiles = Directory.GetFiles(moduleFilesPath);

            foreach (string file in headerFiles)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            foreach (string file in moduleFiles)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            //string module_info = ModuleSystem + "module_info.py";
            //File.WriteAllText(module_info, File.ReadAllText(module_info).Replace("%MOD_NAME%", objs[1].ToString()));
        }

        private static void SaveAllCodes(string exportDir)
        {
            /// USE SavePseudoCodeByType code and SourceReader to create code here

            List<List<Skriptum>> allTypes = new List<List<Skriptum>>();
            List<List<string[]>> allTypesCodes = new List<List<string[]>>();

            /// USE PseudoCode and SourceReader to create object lists here or in methods themselves

            ProcessInit(exportDir);
            ProcessGlobalVariables(exportDir);
            ProcessStrings(exportDir);
            ProcessSkills(exportDir);
            ProcessMusic(exportDir);
            ProcessAnimations(exportDir);
            ProcessMeshes(exportDir);
            ProcessSounds(exportDir);
            ProcessSkins(exportDir);
            ProcessFactions(exportDir);
            ProcessScenes(exportDir);
            ProcessParticleSys(exportDir);
            ProcessSceneProps(exportDir);
            ProcessQuests(exportDir);
            ProcessInfoPages(exportDir);
            ProcessSimpleTriggers(exportDir);
            ProcessTriggers(exportDir);
            ProcessDialogs(exportDir);
            ProcessPostfxParams(exportDir);
            ProcessItems(exportDir);
            ProcessMapIcons(exportDir);
            
            ProcessTroops(exportDir);
            ProcessTableauMaterials(exportDir);
            ProcessPresentations(exportDir);
            ProcessScripts(exportDir);
            ProcessMenus(exportDir);
            ProcessMissionTemplates(exportDir);
            ProcessPartyTemplates(exportDir);
            ProcessParties(exportDir);

            ProcessGlobalVariablesUnused(exportDir);

        }

        #region Process Methods

        private static void ProcessInit(string exportDir)
        {
            Console.Write("Initializing...");

            TryFileDelete(exportDir, "tag_uses");
            TryFileDelete(exportDir, "quick_strings");
            TryFileDelete(exportDir, "variables");
            TryFileDelete(exportDir, "variable_uses");

            List<string> variables = new List<string>();
            List<int> variableUses = new List<int>();

            try
            {
                string[] varList = File.ReadAllLines(ModuleSystem + "variables.txt");
                foreach (string v in varList)
                {
                    string vv = v.Trim();
                    if (vv.Length != 0)
                    {
                        variables.Add(vv);
                        variableUses.Add(1);
                    }
                }
                SaveVariables(exportDir, variables, variableUses);
            }
            catch (Exception)
            {
                Console.WriteLine("variables.txt not found.Creating new variables.txt file");
            }

            Console.WriteLine("Done");
        }

        private static void ProcessGlobalVariables(string exportDir)
        {
            Console.WriteLine("Compiling all global variables...");

            List<string> variables = LoadVariables(exportDir, out List<int> variablesUses);

            CompileAllGlobalVars(variables, variablesUses);

            SaveVariables(exportDir, variables, variablesUses);
        }

        private static void ProcessStrings(string exportDir)
        {
            Console.WriteLine("Exporting strings...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_strings.py"))
            {
                for (int i = 0; i < strings.Count; i++)
                    writer.WriteLine("str_" + ConvertToIdentifier(strings[i].ID) + " = " + i);
                writer.WriteLine(Environment.NewLine);
            }

            // save game string
            using (StreamWriter writer = new StreamWriter(exportDir + "string.txt"))
            {
                writer.WriteLine("stringsfile version 1");//change version if needed
                writer.WriteLine(strings.Count);
                foreach (GameString gameString in strings)
                    writer.WriteLine("str_%s %s", ConvertToIdentifier(gameString.ID), ReplaceSpaces(gameString.Text));
            }
        }

        private static void ProcessSkills(string exportDir)
        {
            Console.WriteLine("Exporting skills...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_skills.py"))
            {
                for (int i = 0; i < skills.Count; i++)
                    writer.WriteLine("skl_" + ConvertToIdentifier(skills[i].ID) + " = " + i);
                writer.WriteLine(Environment.NewLine);
            }

            // save skills
            using (StreamWriter writer = new StreamWriter(exportDir + "skills.txt"))
            {
                writer.WriteLine(skills.Count);
                foreach (Skill skill in skills)
                {
                    writer.Write("skl_%s %s", ConvertToIdentifier(skill.ID), ReplaceSpaces(skill.Name));
                    writer.WriteLine("%d %d %s", skill.FlagsGZ, skill.MaxLevel, skill.Description.Replace(' ', '_'));
                }
            }
        }

        private static void ProcessMusic(string exportDir)
        {
            Console.WriteLine("Exporting tracks...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_music.py"))
            {
                for (int i = 0; i < tracks.Count; i++)
                    writer.WriteLine("track_%s = %d", tracks[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save tracks
            using (StreamWriter writer = new StreamWriter(exportDir + "music.txt"))
            {
                writer.WriteLine(tracks.Count);
                foreach (Music track in tracks)
                {
                    writer.WriteLine(
                        "%s %d %d",
                        track.TrackFile,
                        track.TrackFlagsGZ,
                        (track.TrackFlagsGZ | track.ContinueTrackFlagsGZ)
                    );
                }
            }
        }

        private static void ProcessAnimations(string exportDir)
        {
            Console.WriteLine("Exporting animations...");

            // compile/filter action sets
            List<Animation> actionCodes = new List<Animation>();
            foreach (Animation action in animations)
            {
                int index = -1;
                for (int i = 0; i < actionCodes.Count; i++)
                {
                    if (actionCodes[i].ID == action.ID)
                    {
                        index = i;
                        i = actionCodes.Count;//break;
                    }
                }
                if (index < 0)
                {
                    int pos = actionCodes.Count;
                    actionCodes.Add(action);
                    animationIndices.Add(pos);//action[0] = pos;
                }
                else
                    animationIndices.Add(index);//action[0] = index;
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_actions.py"))
            {
                for (int i = 0; i < animations.Count; i++)
                    writer.WriteLine("anim_%s = %d", animations[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save animations
            using (StreamWriter writer = new StreamWriter(exportDir + "actions.txt"))
            {
                writer.WriteLine(actionCodes.Count);
                foreach (Animation action in actionCodes)
                {
                    writer.Write(" %s %d %d ", action.ID, action.FlagsGZ, action.MasterFlagsGZ);//print flags
                    writer.WriteLine(" %d", action.Sequences.Length);
                    foreach (AnimationSequence sequence in action.Sequences)
                    {
                        writer.Write("  %f %s %d %d %d ",
                            sequence.Duration,
                            sequence.ResourceName,
                            sequence.BeginFrame,
                            sequence.EndFrame,
                            sequence.FlagsGZ
                        );
                        writer.Write("%d ", sequence.LastNumberGZ);
                        writer.Write("%f %f %f %f ", sequence.LastNumbersFKZ);
                    }
                }
            }
        }

        private static void ProcessMeshes(string exportDir)
        {
            Console.WriteLine("Exporting meshes...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_meshes.py"))
            {
                for (int i = 0; i < meshes.Count; i++)
                    writer.WriteLine("mesh_%s = %d", meshes[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save meshes
            using (StreamWriter writer = new StreamWriter(exportDir + "meshes.txt"))
            {
                writer.WriteLine(meshes.Count);
                foreach (Mesh mesh in meshes)
                {
                    writer.WriteLine("mesh_%s %d %s %f %f %f %f %f %f %f %f %f",
                        mesh.ID,
                        mesh.Flags,
                        ReplaceSpaces(mesh.ResourceName),
                        mesh.AxisTranslation[0],//x
                        mesh.AxisTranslation[1],//y
                        mesh.AxisTranslation[2],//z
                        mesh.RotationAngle[0],//x
                        mesh.RotationAngle[1],//y
                        mesh.RotationAngle[2],//z
                        mesh.Scale[0],//x
                        mesh.Scale[1],//y
                        mesh.Scale[2]//z
                    );
                }
            }
        }

        private static void ProcessSounds(string exportDir)
        {
            Console.WriteLine("Exporting sounds...");

            // compile/filter sounds
            List<object[]> allSounds = new List<object[]>();
            foreach (Sound sound in sounds)
            {
                object[] soundFiles = sound.SoundFiles;
                ulong soundFlags = sound.FlagsGZ;
                for (int i = 0; i < sound.SoundFiles.Length; i++)
                {
                    bool found = false;
                    int soundNo = 0;
                    object[] soundFile = soundFiles[i].ToString().Split();
                    if (soundFile.Length == 1)
                        soundFile = new object[] { soundFile[0], 0 };
                    while (soundNo < allSounds.Count && !found)
                    {
                        if (allSounds[soundNo][0].Equals(soundFile[0].ToString()))//.ToString() necessary?
                            found = true;
                        else
                            soundNo++;
                    }
                    if (!found)
                    {
                        soundNo = allSounds.Count;
                        allSounds.Add(new object[] { soundFile[0], soundFlags });
                    }
                    soundFiles[i] = new object[] { soundNo, soundFile[1] };
                }
                List<object[]> vs = new List<object[]>();
                foreach (object[] v in soundFiles)
                    vs.Add(v);
                soundsArray.Add(vs);
            }

            // save sounds
            using (StreamWriter writer = new StreamWriter(exportDir + "sounds.txt"))
            {
                writer.WriteLine("soundsfile version 3");//change version if necessary

                writer.WriteLine(allSounds.Count);
                foreach (var soundSample in allSounds)
                    writer.WriteLine(" %s %d", soundSample);

                writer.WriteLine(sounds.Count);
                for (int i = 0; i < sounds.Count; i++)
                {
                    Sound sound = (Sound)sounds[i];
                    writer.Write("snd_%s %d %d ", sound.ID, sound.FlagsGZ, sound.SoundFiles.Length);
                    foreach (object[] sample in soundsArray[i])
                        writer.Write("%d %d ", sample);
                    writer.WriteLine();
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_sounds.py"))
            {
                for (int i = 0; i < sounds.Count; i++)
                    writer.WriteLine("snd_%s = %d", sounds[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }
        }

        private static void ProcessSkins(string exportDir)
        {
            int maxSkinCount = 16;//change if higher for bannerlord or other versions

            Console.WriteLine("Exporting skins...");

            // save skins
            using (StreamWriter writer = new StreamWriter(exportDir + "skins.txt"))
            {
                writer.WriteLine("skins_file version 1");//change version if necessary
                if (skins.Count > maxSkinCount)
                    skins.RemoveRange(maxSkinCount, skins.Count - maxSkinCount);
                writer.WriteLine(skins.Count);

                foreach (Skin skin in skins)
                {
                    writer.WriteLine("%s %d\n %s %s %s", skin.ID, skin.Flags, skin.BodyMesh, skin.CalfMesh, skin.HandMesh);
                    writer.Write(" %s %d ", skin.HeadMesh, skin.FaceKeys.Length);

                    foreach (FaceKey faceKey in skin.FaceKeys)
                    {
                        writer.Write("skinkey_%s %d %d %f %f %s ", 
                            ConvertToIdentifier(faceKey.ID),
                            faceKey.Width,
                            faceKey.Height,
                            faceKey.CorX,
                            faceKey.CorY,
                            ReplaceSpaces(faceKey.Text)
                        );
                    }

                    writer.WriteLine(Environment.NewLine + skin.HairMeshes.Length);
                    foreach (string hairMeshName in skin.HairMeshes)
                        writer.Write(" %s ", hairMeshName);

                    writer.WriteLine(Environment.NewLine + " " + skin.BeardMeshes.Length);
                    foreach (string beardMeshName in skin.BeardMeshes)
                        writer.WriteLine("  %s", beardMeshName);
                    writer.WriteLine();

                    WriteTextures(writer, skin.HairTextures);
                    WriteTextures(writer, skin.BeardTextures);
                    WriteFaceTex(writer, skin.FaceTextures);
                    WriteVoices(writer, skin.Voices);

                    writer.Write(" %s %f ", skin.SkeletonName, skin.Scale);
                    writer.WriteLine(Environment.NewLine + "%d %d", skin.BloodParticle1GZ, skin.BloodParticle2GZ);

                    writer.WriteLine(skin.FaceKeyConstraints.Length);
                    foreach (var constraint in skin.FaceKeyConstraints)
                    {
                        writer.Write(Environment.NewLine + "%f %d %d ", constraint.Number, constraint.CompMode, constraint.ValuesINT.Length);
                        for (int i = 0; i < constraint.ValuesINT.Length; i++)
                            writer.Write(" %f %d", constraint.ValuesDOUBLE[i], constraint.ValuesINT[i]);
                    }
                    writer.WriteLine();
                }
            }
        }

        private static void ProcessFactions(string exportDir)
        {
            Console.WriteLine("Exporting faction data...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_factions.py"))
            {
                for (int i = 0; i < factions.Count; i++)
                    writer.WriteLine("fac_%s = %d", factions[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            List<double[]> relations = CompileRelations(factions);

            // save faction data
            using (StreamWriter writer = new StreamWriter(exportDir + "factions.txt"))
            {
                writer.WriteLine("factionsfile version 1");//change version if necessary
                writer.WriteLine(factions.Count);
                for (int i = 0; i < factions.Count; i++)
                {
                    Faction faction = (Faction)factions[i];

                    writer.WriteLine("fac_%s %s %d %d ",
                        ConvertToIdentifier(faction.ID),
                        ReplaceSpaces(faction.Name),
                        faction.FlagsGZ,
                        HexConverter.Hex2Dec(faction.ColorCode.Replace("0x", string.Empty))
                    );

                    foreach (double relation in relations[i])
                        writer.Write(" %f ", relation);
                    writer.WriteLine();

                    writer.Write("%d ", faction.Ranks.Length);
                    foreach (string rank in faction.Ranks)
                        writer.Write(" %s ", ReplaceSpaces(rank));
                }
            }
        }

        private static void ProcessScenes(string exportDir)
        {
            Console.WriteLine("Exporting scene data...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_scenes.py"))
            {
                for (int i = 0; i < scenes.Count; i++)
                    writer.WriteLine("scn_%s = %d", scenes[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            // save scenes
            using (StreamWriter writer = new StreamWriter(exportDir + "scenes.txt"))
            {
                writer.WriteLine("scenesfile version 1");//change version if necessary
                writer.WriteLine(" %d", scenes.Count);
                foreach (Scene scene in scenes)
                {
                    writer.WriteLine("scn_%s %s %d %s %s %f %f %f %f %f %s ",
                        ConvertToIdentifier(scene.ID),
                        ReplaceSpaces(scene.ID),
                        scene.FlagsGZ,
                        scene.MeshName,
                        scene.BodyName,
                        scene.MinPosition[0],
                        scene.MinPosition[1],
                        scene.MaxPosition[0],
                        scene.MaxPosition[1],
                        scene.WaterLevel,
                        scene.TerrainCode
                    );

                    writer.Write("  %d ", scene.OtherScenes.Length);
                    foreach (string passage in scene.OtherScenes)
                        WritePassage(writer, scenes, passage);
                    writer.WriteLine();

                    writer.Write("  %d ", scene.ChestTroops.Length);
                    foreach (string chestTroop in scene.ChestTroops)
                    {
                        int troopNo = FindObject(troops, chestTroop);//FindTroop(troops, chestTroop);
                        if (troopNo < 0)
                        {
                            Console.WriteLine("Error unable to find chest-troop: " + chestTroop);
                            troopNo = 0;
                        }
                        else
                            AddTagUse(tagUses, TagType.Troop, troopNo);
                        writer.Write(" %d ", troopNo);
                    }
                    writer.WriteLine();

                    writer.WriteLine(" %s ", scene.TerrainBase);
                }
            }

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void ProcessParticleSys(string exportDir)
        {
            Console.WriteLine("Exporting particle data...");

            // save particle systems
            using (StreamWriter writer = new StreamWriter(exportDir + "particle_systems.txt"))
            {
                writer.WriteLine("particle_systemsfile version 1");//change version if necessary
                writer.WriteLine(particleSystems.Count);
                foreach (ParticleSystem psys in particleSystems)
                {
                    writer.Write("psys_%s %d %s  ", psys.ID, psys.FlagsGZ, psys.MeshName);
                    writer.WriteLine("%d %f %f %f %f %f ",
                        psys.ParticlesPerSecond,
                        psys.ParticleLifeTime,
                        psys.Damping,
                        psys.GravityStrength,
                        psys.TurbulanceSize,
                        psys.TurbulanceStrength
                    );

                    SavePsysKey(writer, psys.AlphaKeys);
                    SavePsysKey(writer, psys.RedKeys);
                    SavePsysKey(writer, psys.GreenKeys);
                    SavePsysKey(writer, psys.BlueKeys);
                    SavePsysKey(writer, psys.ScaleKeys);

                    writer.Write("%f %f %f   ", psys.EmitBoxScale[0], psys.EmitBoxScale[1], psys.EmitBoxScale[2]);
                    writer.Write("%f %f %f   ", psys.EmitVelocity[0], psys.EmitVelocity[1], psys.EmitVelocity[2]);
                    writer.WriteLine("%f ", psys.EmitDirectionRandomness);
                    writer.WriteLine("%f %f ", psys.ParticleRotationSpeed, psys.ParticleRotationDamping);
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_particle_systems.py"))
            {
                for (int i = 0; i < particleSystems.Count; i++)
                    writer.WriteLine("psys_%s = %d", particleSystems[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }
        }

        private static void ProcessSceneProps(string exportDir)
        {
            Console.WriteLine("Exporting scene props...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_scene_props.py"))
            {
                for (int i = 0; i < sceneProps.Count; i++)
                    writer.WriteLine("spr_%s = %d", sceneProps[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            // save scene props
            using (StreamWriter writer = new StreamWriter(exportDir + "scene_props.txt"))
            {
                writer.WriteLine("scene_propsfile version 1");//change version if necessary
                writer.WriteLine(" %d", sceneProps.Count);
                foreach (SceneProp sceneProp in sceneProps)
                {
                    writer.Write("spr_%s %d %d %s %s ", sceneProp.ID, sceneProp.FlagsGZ, sceneProp.HitPoints, sceneProp.MeshName, sceneProp.PhysicsObjectName);
                    SaveSimpleTriggers(writer, sceneProp.SimpleTriggers, variableList, variableUses, tagUses, quickStrings);
                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void ProcessQuests(string exportDir)
        {
            Console.WriteLine("Exporting quest data...");

            // save quest
            using (StreamWriter writer = new StreamWriter(exportDir + "quest.txt"))
            {
                writer.WriteLine("questsfile version 1");//change version if necessary
                writer.WriteLine(quests.Count);
                foreach (Quest quest in quests)
                {
                    writer.Write("qst_%s %s %d ", quest.ID, quest.Name.Replace(' ', '_'), quest.FlagsGZ);
                    writer.WriteLine("%s ", quest.Description.Replace(' ', '_'));
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_quests.py"))
            {
                for (int i = 0; i < quests.Count; i++)
                    writer.WriteLine("qst_%s = %d", quests[i].ID, i);
                for (int i = 0; i < quests.Count; i++)
                    writer.WriteLine("qsttag_%s = %d", quests[i].ID, OP_MASK_QUEST_INDEX | i);
                writer.WriteLine(Environment.NewLine);
            }
        }

        private static void ProcessInfoPages(string exportDir)
        {
            Console.WriteLine("Exporting info_page data...");

            // save info pages
            using (StreamWriter writer = new StreamWriter(exportDir + "info_pages.txt"))
            {
                writer.WriteLine("infopagesfile version 1");//change version if necessary
                writer.WriteLine(infoPages.Count);
                foreach (InfoPage infoPage in infoPages)
                    writer.WriteLine("ip_%s %s %s", infoPage.ID, infoPage.Name.Replace(' ', '_'), infoPage.Text.Replace(' ', '_'));
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_info_pages.py"))
            {
                for (int i = 0; i < infoPages.Count; i++)
                    writer.WriteLine("ip_%s = %d", infoPages[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }
        }

        private static void ProcessSimpleTriggers(string exportDir)
        {
            Console.WriteLine("Exporting simple triggers...");

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "simple_triggers.txt"))
            {
                writer.WriteLine("simple_triggers_file version 1");//change version if necessary
                SaveSimpleTriggers(writer, simpleTriggers.ToArray(), variables, variableUses, tagUses, quickStrings);
            }

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void ProcessTriggers(string exportDir)
        {
            Console.WriteLine("Exporting triggers...");

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "triggers.txt"))
            {
                writer.WriteLine("triggersfile version 1");//change version if necessary
                writer.WriteLine(triggers.Count);
                foreach (Trigger trigger in triggers)
                {
                    writer.Write("%f %f %f ", trigger.CheckInterval, trigger.DelayInterval, trigger.ReArmInterval);
                    SaveStatementBlock(writer, 0, true, trigger.ConditionBlock, variables, variableUses, tagUses, quickStrings);
                    SaveStatementBlock(writer, 0, true, trigger.ConsequencesBlock, variables, variableUses, tagUses, quickStrings);
                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void ProcessDialogs(string exportDir)
        {
            Console.WriteLine("Exporting dialogs...");

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            List<int> outputStates = CompileSentenceTokens(exportDir, dialogs, out List<int> inputStates);
            SaveSentence(exportDir, variables, variableUses, dialogs, tagUses, quickStrings, inputStates, outputStates);

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void ProcessPostfxParams(string exportDir)
        {
            Console.WriteLine("Exporting postfx_params...");

            // save postfx_params
            using (StreamWriter writer = new StreamWriter(exportDir + "postfx.txt"))
            {
                writer.WriteLine("postfx_paramsfile version 1");//change version if necessary
                writer.WriteLine(postfxParams.Count);
                foreach (PostFX postFX in postfxParams)
                {
                    writer.Write("pfx_%s %d %d", postFX.ID, postFX.FlagsGZ, postFX.TonemapOperatorType);

                    writer.Write("  %f %f %f %f",
                        postFX.ShaderParameter1[0],
                        postFX.ShaderParameter1[1],
                        postFX.ShaderParameter1[2],
                        postFX.ShaderParameter1[3]
                    );

                    writer.Write("  %f %f %f %f",
                        postFX.ShaderParameter2[0],
                        postFX.ShaderParameter2[1],
                        postFX.ShaderParameter2[2],
                        postFX.ShaderParameter2[3]
                    );

                    writer.Write("  %f %f %f %f",
                        postFX.ShaderParameter3[0],
                        postFX.ShaderParameter3[1],
                        postFX.ShaderParameter3[2],
                        postFX.ShaderParameter3[3]
                    );

                    writer.WriteLine();
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_postfx_params.py"))
            {
                for (int i = 0; i < postfxParams.Count; i++)
                    writer.WriteLine("pfx_%s = %d", postfxParams[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }
        }

        private static void ProcessItems(string exportDir)
        {
            Console.WriteLine("Exporting item data...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_items.py"))
            {
                for (int i = 0; i < items.Count; i++)
                    writer.WriteLine("itm_%s = %d", ConvertToIdentifier(items[i].ID), i);
                writer.WriteLine(Environment.NewLine);
            }

            // save items
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            WriteItems(exportDir, variableList, variableUses, tagUses, quickStrings);

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void ProcessMapIcons(string exportDir)
        {
            Console.WriteLine("Exporting map icons...");

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_map_icons.py"))
            {
                for (int i = 0; i < mapIcons.Count; i++)
                    writer.WriteLine("icon_%s = %d", mapIcons[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save map icons
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            SaveMapIcons(exportDir, variableList, variableUses, tagUses, quickStrings);

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);
        }

        private static void SaveMapIcons(string exportDir, List<string> variableList, List<int> variableUses, List<List<int>> tagUses, List<string[]> quickStrings)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "map_icons.txt"))
            {
                writer.WriteLine("map_icons_file version 1");//change version if necessary
                writer.WriteLine(mapIcons.Count);

                throw new NotImplementedException();

                foreach (MapIcon mapIcon in mapIcons)
                {
                    writer.Write("%s %d %s %f %d %f %f %f ",
                        mapIcon.ID,
                        mapIcon.FlagsGZ,
                        mapIcon.MapIconName,
                        mapIcon.Scale,
                        mapIcon.Sound,//GET NUMBER!!!
                        mapIcon.OffsetX,
                        mapIcon.OffsetY,
                        mapIcon.OffsetZ
                    );

                    SaveSimpleTriggers(writer, mapIcon.SimpleTriggers, variableList, variableUses, tagUses, quickStrings);

                    writer.WriteLine();
                }
            }
        }

        private static void ProcessTroops(string exportDir)
        {
            Console.WriteLine("Exporting f...");

            // 


            //

        }
        
        private static void ProcessTableauMaterials(string exportDir)
        {
            Console.WriteLine("Exporting g...");

            // 


            //

        }
        
        private static void ProcessPresentations(string exportDir)
        {
            Console.WriteLine("Exporting h...");

            // 


            //

        }
        
        private static void ProcessScripts(string exportDir)
        {
            Console.WriteLine("Exporting i...");

            // 


            //

        }
        
        private static void ProcessMenus(string exportDir)
        {
            Console.WriteLine("Exporting j...");

            // 


            //

        }
        
        private static void ProcessMissionTemplates(string exportDir)
        {
            Console.WriteLine("Exporting k...");

            // 


            //

        }
        
        private static void ProcessPartyTemplates(string exportDir)
        {
            Console.WriteLine("Exporting l...");

            // 


            //

        }

        private static void ProcessParties(string exportDir)
        {
            Console.WriteLine("Exporting m...");

            // 


            //

        }

        private static void ProcessGlobalVariablesUnused(string exportDir)
        {
            Console.WriteLine("Checking global variable usages...");
            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            for (int i = 0; i < variables.Count; i++)
                if (variableUses[i] == 0)
                    Console.WriteLine("WARNING: Global variable never used: " + variables[i]);
        }

        #endregion

        #region Helper Methods

        private static void WriteItems(
            string exportDir,
            List<string> variableList,
            List<int> variableUses,
            List<List<int>> tagUses,
            List<string[]> quickStrings
            )
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "item_kinds1.txt"))
            {
                writer.WriteLine("itemsfile version 3");//change version if necessary
                writer.WriteLine(items.Count);

                foreach (Item item in items)
                {
                    if (item.ItemProperties.Contains("itp_merchandise"))
                    {
                        int idNo = FindObject(items, ConvertToIdentifier(item.ID));
                        AddTagUse(tagUses, TagType.Item, idNo);
                    }

                    writer.Write(" itm_%s %s %s %d ",
                        ConvertToIdentifier(item.ID),
                        ReplaceSpaces(item.Name),
                        ReplaceSpaces(item.PluralName),
                        item.Meshes.Count
                    );

                    foreach (string mesh in item.Meshes)
                    {
                        string[] meshVals = mesh.Split();
                        writer.Write(" %s %d ", meshVals[0], meshVals[1]);
                    }

                    throw new NotImplementedException();

                    writer.WriteLine(" %d %d %d %d %f %d %d %d %d %d %d %d %d %d %d %d %d",
                        item.ItemProperties,//GET NUMBER!!!
                        item.CapabilityFlags,//GET NUMBER!!!
                        item.Price,
                        item.ModBits,//GET NUMBER!!!
                        item.Weight,
                        item.Abundance,
                        item.HeadArmor,
                        item.BodyArmor,
                        item.LegArmor,
                        item.Difficulty,
                        item.HitPoints,
                        item.SpeedRating,
                        item.MissileSpeed,
                        item.WeaponLength,
                        item.MaxAmmo,
                        item.ThrustDamage,
                        item.SwingDamage
                    );

                    writer.WriteLine(" %d", item.Factions.Count);
                    foreach (int faction in item.Factions)
                        writer.Write(" %d", faction);
                    if (item.Factions.Count > 0)
                        writer.WriteLine();

                    List<string> triggerList = item.Triggers;

                    /// JUST FOR TESTING!!!
                    foreach (string trigger in triggerList)
                        Console.WriteLine(trigger);
                    /// JUST FOR TESTING!!!

                    //SaveSimpleTriggers(writer, triggerList, variableList, variableUses, tagUses, quickStrings);
                }
            }
        }

        private static void SaveSentence(
            string exportDir,
            List<string> variables,
            List<int> variableUses,
            List<Skriptum> dialogs,
            List<List<int>> tagUses,
            List<string[]> quickStrings,
            List<int> inputStates,
            List<int> outputStates
            )
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "conversation.txt"))
            {
                writer.WriteLine("dialogsfile version 2");//change version if necessary
                writer.WriteLine(dialogs.Count);

                List<string[]> autoIds = new List<string[]>();

                for (int i = 0; i < dialogs.Count; i++)
                {
                    Dialog dialog = (Dialog)dialogs[i];
                    try
                    {
                        string dialogId = CreateAutoId2(dialog, autoIds);
                        writer.Write("%s %d %d ", dialogId, dialog.TalkingPartnerCode, inputStates[i]);
                        SaveStatementBlock(writer, 0, true, dialog.ConditionBlock, variables, variableUses, tagUses, quickStrings);

                        writer.Write("%s ", dialog.DialogText.Replace(' ', '_'));
                        if (dialog.DialogText.Length == 0)
                            writer.Write("NO_TEXT ");
                        writer.Write(" %d ", outputStates[i]);
                        SaveStatementBlock(writer, 0, true, dialog.ConsequenceBlock, variables, variableUses, tagUses, quickStrings);

                        if (dialog.VoiceOverSoundFile.Length > 0)
                            writer.WriteLine("%s ", dialog.VoiceOverSoundFile);
                        else
                            writer.WriteLine("NO_VOICEOVER ");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error in dialog line:");
                        Console.WriteLine(dialog.ID + " " + dialog.StartDialogState + " " + dialog.EndDialogState + " " + dialog.DialogText);
                    }
                }
            }
        }

        private static string CreateAutoId2(Dialog dialog, List<string[]> autoIds)
        {
            string text = dialog.DialogText;
            string tokenInput = ConvertToIdentifier(dialog.StartDialogState);
            string tokenOutput = ConvertToIdentifier(dialog.EndDialogState);
            string autoId = "dlga_" + tokenInput + ":" + tokenOutput;

            bool done = false;

            bool autoIdsHasKey = AutoIdsHasKey(autoIds, autoId);
            if (!autoIdsHasKey || (autoIdsHasKey && AutoIdsGetValue(autoIds, autoId).Equals(text)))
                done = true;

            if (!done)
            {
                string newAutoId;
                int number = 0;
                do
                {
                    number++;
                    newAutoId = autoId + "." + number;
                } while (AutoIdsHasKey(autoIds, newAutoId));
                autoId = newAutoId;
            }

            AutoIdsSetValue(autoIds, autoId, text);

            return autoId;
        }

        private static bool AutoIdsHasKey(List<string[]> autoIds, string key)
        {
            bool found = false;
            for (int i = 0; i < autoIds.Count; i++)
            {
                if (autoIds[i][0].Equals(key))
                {
                    found = true;
                    i = autoIds.Count;
                }
            }
            return found;
        }

        private static string AutoIdsGetValue(List<string[]> autoIds, string key)
        {
            string val = null;
            for (int i = 0; i < autoIds.Count; i++)
            {
                if (autoIds[i][0].Equals(key))
                {
                    val = autoIds[i][1];
                    i = autoIds.Count;
                }
            }
            return val;
        }

        private static void AutoIdsSetValue(List<string[]> autoIds, string key, string val)
        {
            for (int i = 0; i < autoIds.Count; i++)
            {
                if (autoIds[i][0].Equals(key))
                {
                    autoIds[i][1] = val;
                    i = autoIds.Count;
                }
            }
        }

        private static List<int> CompileSentenceTokens(string exportDir, List<Skriptum> dialogs, out List<int> inputTokens)
        {
            inputTokens = new List<int>();
            List<int> outputTokens = new List<int>();
            List<string> dialogStates = new List<string>() {
                "start",
                "party_encounter",
                "prisoner_liberated",
                "enemy_defeated",
                "party_relieved",
                "event_triggered",
                "close_window",
                "trade",
                "exchange_members",
                "trade_prisoners",
                "buy_mercenaries",
                "view_char",
                "training",
                "member_chat",
                "prisoner_chat"
            };
            List<int> dialogStateUsages = new List<int>() {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };

            foreach (Dialog dialog in dialogs)
            {
                bool found = false;
                int outputTokenId = -1;
                string outputToken = dialog.EndDialogState;

                for (int i = 0; i < dialogStates.Count; i++)
                {
                    if (outputToken.Equals(dialogStates[i]))
                    {
                        outputTokenId = i;
                        found = true;
                        i = dialogStates.Count;
                    }
                }

                if (!found)
                {
                    outputTokenId = dialogStates.Count;
                    dialogStates.Add(outputToken);
                    dialogStateUsages.Add(0);
                }

                outputTokens.Add(outputTokenId);
            }

            foreach (Dialog dialog in dialogs)
            {
                bool found = false;
                int inputTokenId = -1;
                string inputToken = dialog.StartDialogState;

                for (int i = 0; i < dialogStates.Count; i++)
                {
                    if (inputToken.Equals(dialogStates[i]))
                    {
                        inputTokenId = i;
                        dialogStateUsages[i]++;
                        found = true;
                        i = dialogStates.Count;
                    }
                }

                if (!found)
                {
                    Console.WriteLine(dialog.StartDialogState);
                    Console.WriteLine(dialog.DialogText);
                    Console.WriteLine(dialog.EndDialogState);
                    Console.WriteLine("**********************************************************************************");
                    Console.WriteLine("ERROR: INPUT TOKEN NOT FOUND:" + inputToken);
                    Console.WriteLine("**********************************************************************************");
                    Console.WriteLine("**********************************************************************************");
                }

                inputTokens.Add(inputTokenId);
            }

            SaveDialogStates(exportDir, dialogStates);

            for (int i = 0; i < dialogStates.Count; i++)
                if (dialogStateUsages[i] == 0)
                    Console.WriteLine("ERROR: Output token not found: " + dialogStates[i]);

            return outputTokens;
        }

        private static void SaveDialogStates(string exportDir, List<string> dialogStates)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "dialog_states.txt"))
                foreach (string dialogState in dialogStates)
                    writer.WriteLine(dialogState);
        }

        private static void SaveSimpleTriggers(
            StreamWriter writer,
            Skriptum[] simpleTriggers,
            List<string> variableList,
            List<int> variableUses,
            List<List<int>> tagUses,
            List<string[]> quickStrings
            )
        {
            writer.WriteLine(simpleTriggers.Length);
            foreach (SimpleTrigger trigger in simpleTriggers)
            {
                writer.Write("%f ", trigger.CheckInterval);
                SaveStatementBlock(writer, 0, true, trigger.ConsequencesBlock, variableList, variableUses, tagUses, quickStrings);
                writer.WriteLine();
            }
            writer.WriteLine();
        }

        private static void SaveStatementBlock(StreamWriter writer, object statementName, bool canFailStatement, string[] statementBlock, List<string> variableList, List<int> variableUses, List<List<int>> tagUses, List<string[]> quickStrings)
        {
            List<string> localVars = new List<string>();
            List<int> localVarsUses = new List<int>();
            writer.Write(" %d ", statementBlock.Length);

            int storeScriptParam1Uses = 0;
            int storeScriptParam2Uses = 0;
            int currentDepth = 0;
            //bool canFail = false;

            List<string> tryOpcodes = new List<string>()
            {
                "try_begin",
                "try_for_range",
                "try_for_range_backwards",
                "try_for_parties",
                "try_for_agents"
            };

            for (int i = 0; i < statementBlock.Length; i++)
            {
                string[] statementSp = statementBlock[i].Split();
                string opcode = statementSp[0];
                bool noVariables = (statementSp.Length == 1);

                if (tryOpcodes.Contains(opcode))
                    currentDepth++;
                else if (opcode.Equals("try_end"))
                    currentDepth--;
                else if (opcode.Equals("store_script_param_1") || (opcode.Equals("store_script_param") && statementSp[2].Equals("1")))
                    storeScriptParam1Uses++;
                else if (opcode.Equals("store_script_param_2") || (opcode.Equals("store_script_param") && statementSp[2].Equals("2")))
                    storeScriptParam2Uses++;
                else if (!canFailStatement && currentDepth == 0 &&
                    (IsCanFailOperation(opcode) ||
                    (opcode.Equals("call_script") && statementSp[1].TrimStart()/*.Substring(7)*/.StartsWith("cf_"))) &&
                    !statementName.ToString().StartsWith("cf_"))
                    Console.WriteLine("WARNING: Script can fail at operation #" + i + ". Use cf_ at the beginning of its name: " + statementName);

                SaveStatement(writer, opcode, noVariables, statementSp, variableList, variableUses, localVars, localVarsUses, tagUses, quickStrings);
            }

            if (storeScriptParam1Uses > 1)
                Console.WriteLine("WARNING: store_script_param_1 is used more than once:" + statementName);
            if (storeScriptParam2Uses > 1)
                Console.WriteLine("WARNING: store_script_param_2 is used more than once:" + statementName);

            for (int i = 0; i < localVars.Count; i++)
                if (localVarsUses[i] == 0 && !localVars[i].StartsWith("unused"))//make output optional
                    Console.WriteLine("WARNING: Local variable never used: " + localVars[i] + ", at: " + statementName);

            if (localVars.Count > 128)
                Console.WriteLine("WARNING: Script uses more than 128 local wariables: " + statementName + " --> variables count:" + localVars.Count);
        }

        private static void SaveStatement(StreamWriter writer, string opcode, bool noVariables, string[] statementSp, List<string> variableList, List<int> variableUses, List<string> localVars, List<int> localVarsUses, List<List<int>> tagUses, List<string[]> quickStrings)
        {
            int lenStatement = 0;
            if (!noVariables)
            {
                lenStatement = statementSp.Length - 1;
                if (IsLhsOperation(opcode))
                {
                    if (lenStatement > 0)
                    {
                        string param = statementSp[1];
                        if (param[0] == ':')
                            AddVariable(param.Substring(1), localVars, localVarsUses);
                    }
                }
            }

            writer.Write("%d %d ", opcode, lenStatement);

            for (int i = 0; i < lenStatement; i++)
            {
                int operand = ProcessParam(statementSp[i + 1], variableList, variableUses, localVars, localVarsUses, tagUses, quickStrings);
                writer.Write("%d ", operand);
            }
        }

        private static int ProcessParam(object param, List<string> variableList, List<int> variableUses, List<string> localVarList, List<int> localVarsUses, List<List<int>> tagUses, List<string[]> quickStrings)
        {
            int result = 0;
            if (param.GetType().Equals(typeof(string)))
            {
                string paramS = (string)param;
                if (paramS[0] == '$')
                {
                    CheckVariableNotDefined(paramS.Substring(1), localVarList);
                    result = GetVariable(paramS, globalVarsList, globalVarsUses);
                    result |= OP_MASK_VARIABLE;
                }
                else if (paramS[0] == ':')
                {
                    CheckVariableNotDefined(paramS.Substring(1), globalVarsList);
                    result = GetVariable(paramS, localVarList, localVarsUses);
                    result |= OP_MASK_LOCAL_VARIABLE;
                }
                else if (paramS[0] == '@')
                {
                    result = InsertQuickStringWithAutoId(paramS.Substring(1), quickStrings);
                    result |= OP_MASK_QUICK_STRING;
                }
                else
                {
                    result = GetIdentifierValue(paramS.ToLower(), tagUses);
                    if (result < 0)
                        Console.WriteLine("ERROR: Illegal Identifier:" + param);
                }
            }
            else
                result = (int)param;
            return result;
        }

        private static int GetIdentifierValue(string str, List<List<int>> tagUses)
        {
            int underscorePos = str.IndexOf('_');
            int result = -1;

            if (underscorePos > 0)
            {
                string tagStr = str.Remove(underscorePos);
                string idStr = str.Substring(underscorePos + 1);
                int idNo = GetIdValue(tagStr, idStr, tagUses, out TagType tagType);
                if (tagType > 0 && tagType < TagType.End)
                {
                    if (idNo < 0)
                        Console.WriteLine("Error: Unable to find object: " + str);
                    else
                        result = idNo | ((int)tagType << OP_NUM_VALUE_BITS);
                }
                else
                    Console.WriteLine("Error: Unrecognized tag: " + tagStr + "in object: " + str);
            }
            else
                Console.WriteLine("Error: Invalid object: " + str + ". Variables should start with $ sign and references should start with a tag");

            return result;
        }

        private static int GetIdValue(string tag, string identifier, List<List<int>> tagUses, out TagType tagType)
        {
            tagType = TagType.End;
            int idNo = -1;

            switch (tag)
            {
                case "str":
                    idNo = FindObject(strings, identifier);
                    tagType = TagType.String;
                    break;
                case "itm":
                    idNo = FindObject(items, identifier);
                    tagType = TagType.Item;
                    break;
                case "trp":
                    idNo = FindObject(troops, identifier);
                    tagType = TagType.Troop;
                    break;
                case "fac":
                    idNo = FindObject(factions, identifier);
                    tagType = TagType.Faction;
                    break;
                case "qst":
                    idNo = FindObject(quests, identifier);
                    tagType = TagType.Quest;
                    break;
                case "pt":
                    idNo = FindObject(partyTemplates, identifier);
                    tagType = TagType.PartyTemplate;
                    break;
                case "p":
                    idNo = FindObject(parties, identifier);
                    tagType = TagType.Party;
                    break;
                case "scn":
                    idNo = FindObject(scenes, identifier);
                    tagType = TagType.Scene;
                    break;
                case "mt":
                    idNo = FindObject(missionTemplates, identifier);
                    tagType = TagType.MissionTemplate;
                    break;
                case "mnu":
                    idNo = FindObject(menus, identifier);
                    tagType = TagType.Menu;
                    break;
                case "script":
                    idNo = FindObject(scripts, identifier);
                    tagType = TagType.Script;
                    break;
                case "psys":
                    idNo = FindObject(particleSystems, identifier);
                    tagType = TagType.ParticleSystem;
                    break;
                case "spr":
                    idNo = FindObject(sceneProps, identifier);
                    tagType = TagType.SceneProp;
                    break;
                case "prsnt":
                    idNo = FindObject(presentations, identifier);
                    tagType = TagType.Presentation;
                    break;
                case "snd":
                    idNo = FindObject(sounds, identifier);
                    tagType = TagType.Sound;
                    break;
                case "icon":
                    idNo = FindObject(mapIcons, identifier);
                    tagType = TagType.MapIcon;
                    break;
                case "skl":
                    idNo = FindObject(skills, identifier);
                    tagType = TagType.Skill;
                    break;
                case "track":
                    idNo = FindObject(tracks, identifier);
                    tagType = TagType.Track;
                    break;
                case "mesh":
                    idNo = FindObject(meshes, identifier);
                    tagType = TagType.Mesh;
                    break;
                case "anim":
                    idNo = FindObject(animations, identifier);
                    tagType = TagType.Animation;
                    break;
                case "tableua":
                    idNo = FindObject(tableaus, identifier);
                    tagType = TagType.Tableau;
                    break;
                default:
                    break;
            }

            if (tagType != TagType.End && idNo > -1)
                AddTagUse(tagUses, tagType, idNo);

            return idNo;
        }

        private static int FindObject(List<Skriptum> skripta, string id)
        {
            int result = -1;
            id = id.ToLower();
            for (int i = 0; i < skripta.Count; i++)
                if (skripta[i].ID.ToLower().Equals(id))
                    result = i;
            return result;
        }

        private static int InsertQuickStringWithAutoId(string sentence, List<string[]> quickStrings)
        {
            bool done = false;
            string text = ConvertToIdentifierWithNoLowerCase(sentence);

            int i = 20;
            int lt = text.Length;

            if (i > lt)
                i = lt;

            string autoId = "qstr_" + text.Substring(0, i);
            sentence = ReplaceSpaces(sentence);

            int index = SearchQuickStringKeys(autoId, quickStrings);
            if (index >= 0 && quickStrings[index][1].Equals(sentence))
                done = true;

            while (i <= lt && !done)
            {
                autoId = "qstr_" + text.Substring(0, i);
                index = SearchQuickStringKeys(autoId, quickStrings);
                if (index >= 0)
                {
                    if (quickStrings[index][1].Equals(sentence))
                        done = true;
                    else
                        i++;
                }
                else
                {
                    done = true;
                    index = quickStrings.Count;
                    quickStrings.Add(new string[] { autoId, sentence});
                }
            }

            if (!done)
            {
                int number = 0;
                string newAutoId;
                do
                {
                    number++;
                    newAutoId = autoId + number;
                } while (QuickStringsHasKey(newAutoId, quickStrings));
                autoId = newAutoId;
                index = quickStrings.Count;
                quickStrings.Add(new string[] { autoId, sentence });
            }

            return index;
        }

        private static int SearchQuickStringKeys(string key, List<string[]> quickStrings)
        {
            int index = -1;
            for (int i = 0; i < quickStrings.Count; i++)
            {
                if (quickStrings[i][0].Equals(key))
                {
                    index = i;
                    i = quickStrings.Count;
                }
            }
            return index;
        }

        private static bool QuickStringsHasKey(string key, List<string[]> quickStrings)
        {
            bool found = false;
            for (int i = 0; i < quickStrings.Count; i++)
            {
                if (quickStrings[i][0].Equals(key))
                {
                    found = true;
                    i = quickStrings.Count;
                }
            }
            return found;
        }

        private static int GetVariable(string param, List<string> varList, List<int> varUses)
        {
            bool found = false;
            int result = -1;
            string varString = param.Substring(1);

            for (int i = 0; i < varList.Count; i++)
            {
                if (varString.Equals(varList[i]))
                {
                    found = true;
                    result = i;
                    varUses[i]++;
                    i = varList.Count;
                }
            }

            if (!found)
            {
                if (param[0] == '$')
                {
                    varList.Add(varString);
                    varUses.Add(0);
                    result = varList.Count - 1;
                    Console.WriteLine("WARNING: Usage of unassigned global variable: " + varString);
                }
                else
                    Console.WriteLine("ERROR: Usage of unassigned local variable: " + varString);
            }

            return result;
        }

        private static void CheckVariableNotDefined(string varString, List<string> varList)
        {
            for (int i = 0; i < varList.Count; i++)
            {
                if (varString.Equals(varList[i]))
                {
                    Console.WriteLine("WARNING: Variable name used for both local and global contexts:" + varString);
                    i = varList.Count;
                }
            }
        }

        private static bool IsLhsOperation(string opcode)
        {
            if (lhsOperations.Count == 0)
                lhsOperations = GetHeaderOperationsList("lhs_operations");
            return lhsOperations.Contains(opcode);
        }

        private static bool IsCanFailOperation(string opcode)
        {
            if (canFailOperations.Count == 0)
                canFailOperations = GetHeaderOperationsList("can_fail_operations");
            return canFailOperations.Contains(opcode);
        }

        private static List<string> GetHeaderOperationsList(string listName)
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(ModuleSystem + "header_common.py"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Split('#')[0];
                    if (line.StartsWith(listName))
                    {
                        line = line.Replace('\t', ' ').Replace(" ", string.Empty);
                        string curOpcode = line.Split('=')[1].TrimStart('[').TrimEnd(',').Trim();
                        if (curOpcode.Length != 0)
                            list.Add(curOpcode);
                        while (!reader.EndOfStream && curOpcode.EndsWith("]"))
                        {
                            curOpcode = reader.ReadLine().Split('#')[0];
                            curOpcode = curOpcode.Trim(' ', '\t').TrimEnd(',');
                            if (!curOpcode.Equals("]"))
                                list.Add(curOpcode.TrimEnd(']'));
                        }
                    }
                }
            }
            return list;
        }

        private static void SavePsysKey(StreamWriter writer, double[] keys12)
        {
            writer.WriteLine("%f %f   %f %f", keys12[0], keys12[1], keys12[2], keys12[3]);
        }

        private static void SaveQuickStrings(string exportDir, List<string[]> quickStrings)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "quick_strings.txt"))
            {
                writer.WriteLine("%d", quickStrings.Count);
                foreach (string[] quickString in quickStrings)
                    writer.WriteLine("%s %s", quickString[0], ReplaceSpaces(quickString[1]));
            }
        }

        private static void SaveTagUses(string exportDir, List<List<int>> tagUses)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "tag_uses.txt"))
            {
                for (int i = 0; i < tagUses.Count; i++)
                    for (int j = 0; j < tagUses[i].Count; j++)
                        writer.Write("%d %d %d;", i, j, tagUses[i][j]);
                writer.WriteLine();
            }
        }

        private static void AddTagUse(List<List<int>> tagUses, TagType tagType, int objectNo)
        {
            // TODO: Uncomment to make build_module_check_tags work
            //EnsureTagUse(tagUses, tagNo, objectNo);
            //tagUses[tagNo][objectNo]++;
            //pass
        }

        /*private static int FindTroop(List<Troop> troops, string chestTroop)
        {
            int result = -1;
            for (int i = 0; i < troops.Count; i++)
            {
                if (troops[i].ID.Equals(chestTroop))
                {
                    result = i;
                    i = troops.Count;
                }
            }
            return result;
        }*/

        private static List<string[]> LoadQuickStrings(string exportDir)
        {
            List<string[]> quickStrings = new List<string[]>();
            try
            {
                string[] strList = File.ReadAllLines(exportDir + "quick_strings.txt");
                foreach (string s in strList)
                {
                    string[] sp = s.Trim().Split();
                    if (sp.Length == 2)
                        quickStrings.Add(sp);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Creating new quick_strings.txt file...");
            }
            return quickStrings;
        }

        private static List<List<int>> LoadTagUses(string exportDir)
        {
            int tagsEnd = 26;//header_common.py
            List<List<int>> tagUses = new List<List<int>>();
            for (int i = 0; i < tagsEnd; i++)
                tagUses.Add(new List<int>());//subTagUses

            try
            {
                string[] varList = File.ReadAllLines(exportDir + "tag_uses.txt");
                foreach (string v in varList)
                {
                    string[] vv = v.Trim().Split(';');
                    foreach (string v2 in vv)
                    {
                        string[] vvv = v2.Split();
                        if (vvv.Length >= 3)
                        {
                            int tagNo = int.Parse(vvv[0]);
                            int objectNo = int.Parse(vvv[1]);
                            EnsureTagUse(tagUses, tagNo, objectNo);
                            tagUses[tagNo][objectNo] = int.Parse(vvv[2]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Creating new tag_uses.txt file...");
            }

            return tagUses;
        }

        private static void EnsureTagUse(List<List<int>> tagUses, int tagNo, int objectNo)
        {
            if (tagUses[tagNo].Count <= objectNo)
            {
                int numToAdd = objectNo - tagUses[tagNo].Count;
                numToAdd++;
                for (int i = 0; i < numToAdd; i++)
                    tagUses[tagNo].Add(0);
            }
        }

        private static void WritePassage(StreamWriter writer, List<Skriptum> scenes, string passage)
        {
            int sceneIdx = 0;
            bool found = false;
            while (!found && sceneIdx < scenes.Count)
            {
                if (scenes[sceneIdx].ID.Equals(passage))
                    found = true;
                else
                    sceneIdx++;
            }

            if (passage.Equals("exit"))
                sceneIdx = 100000;
            else if (passage.Equals(string.Empty))
                sceneIdx = 0;
            else if (!found)
            {
                Console.WriteLine("Error passage not found: " + passage);
                //do_error(); // ?
            }

            writer.Write(" %d ", sceneIdx);
        }

        private static List<double[]> CompileRelations(List<Skriptum> factions)
        {
            List<double[]> relations = new List<double[]>();
            for (int i = 0; i < factions.Count; i++)
                relations.Add(new double[factions.Count]);//r = [0.0 for j in range(len(factions))]; relations.append(r);

            for (int i = 0; i < factions.Count; i++)
            {
                Faction faction = (Faction)factions[i];
                relations[i][i] = faction.FactionCoherence;
                double[] rels = faction.Relations;
                for (int j = 0; j < rels.Length; j++)
                {
                    int otherPos = -1;
                    string relName = faction.Ranks[j];
                    for (int k = 0; k < factions.Count; k++)
                        if (((Faction)factions[k]).Name.Equals(relName))
                            otherPos = k;
                    if (otherPos >= 0)
                    {
                        relations[otherPos][i] = rels[i];
                        relations[i][otherPos] = rels[i];
                    }
                    else
                        Console.WriteLine("ERROR faction not found: " + relName);
                }
            }

            return relations;
        }

        private static void WriteTextures(StreamWriter writer, string[] textures)
        {
            writer.Write(" %d ", textures.Length);
            foreach (string texture in textures)
                writer.Write(" %s ", texture);
            writer.WriteLine();
        }

        private static void WriteVoices(StreamWriter writer, Variable[] voices)
        {
            writer.Write(" %d ", voices.Length);
            foreach (var voice in voices)
                writer.Write(" %d %s ", voice.Value, voice.Name);
            writer.WriteLine();
        }

        private static void WriteFaceTex(StreamWriter writer, FaceTexture[] faceTextures)
        {
            writer.Write(" %d ", faceTextures.Length);
            foreach (FaceTexture texture in faceTextures)
            {
                writer.Write(" %s %d %d %d ", texture.Name, texture.Color, texture.HairMaterials.Length, texture.HairColors.Length);
                foreach (string material in texture.HairMaterials)
                    writer.Write(" %s ", ReplaceSpaces(material));
                foreach (uint color in texture.HairColors)
                    writer.Write(" %d ", color);
                writer.WriteLine();
            }
        }

        private static string ConvertToIdentifier(string idText)
        {
            return ConvertToIdentifierWithNoLowerCase(idText).ToLower();
        }

        private static string ConvertToIdentifierWithNoLowerCase(string idText)
        {
            idText = idText.Replace(' ', '_');
            idText = idText.Replace("'", "_");
            idText = idText.Replace('`', '_');
            idText = idText.Replace('(', '_');
            idText = idText.Replace(')', '_');
            idText = idText.Replace('-', '_');
            idText = idText.Replace(',', '_');
            idText = idText.Replace('|', '_');
            idText = idText.Replace('\t', '_');// Tab
            return idText;
        }

        private static string ReplaceSpaces(string text)
        {
            return text.Replace('\t', '_').Replace(' ', '_');
        }

        private static void CompileGlobalVars(string[] statementBlock, List<string> variableList, List<int> variableUses)
        {
            foreach (string statement in statementBlock)
                CompileGlobalVarsInStatement(statement, variableList, variableUses);
        }

        public static bool IsGenericList(object o)
        {
            var oType = o.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }

        private static bool IsLhsOperationForGlobalVars(string opcode)
        {
            if (globalLhsOperations.Count == 0)
                globalLhsOperations = GetHeaderOperationsList("global_lhs_operations");
            return IsLhsOperation(opcode) ||
                globalLhsOperations.Contains(opcode);
        }

        private static void CompileGlobalVarsInStatement(object statement, List<string> variableList, List<int> variableUses)
        {
            object opcode = 0;
            if (!IsGenericList(statement) && !statement.GetType().Equals(typeof(string[])))
                opcode = statement;
            else
            {
                // check this part again because list and array are incompatible in C#
                string[] statementA = (string[])statement;
                opcode = statementA[0];
                if (IsLhsOperationForGlobalVars(opcode.ToString()))
                {
                    if (statementA.Length > 1)
                    {
                        //object param = statementA[1];
                        //if (param.GetType().Equals(typeof(string))) // not necessary if string array/list is proven!
                        if (statementA[1][0] == '$')
                            AddVariable(statementA[1].Substring(1), variableList, variableUses);
                    }
                }
            }
        }

        private static void AddVariable(string variableString, List<string> variableList, List<int> variableUses)
        {
            bool found = false;
            for (int i = 0; i < variableList.Count; i++)
            {
                if (variableString.Equals(variableList[i]))
                {
                    found = true;
                    variableUses[i]--;
                    i = variableList.Count;//break;
                }
            }
            if (!found)
            {
                variableList.Add(variableString);
                variableUses.Add(-1);
            }
        }

        private static void CompileAllGlobalVars(List<string> variableList, List<int> variableUses)
        {
            List<object> tempList = new List<object>();
            var listType = tempList.GetType(); // not necessary later because is always (generic) IList

            foreach (string variable in reservedVariables)
            {
                try
                {
                    AddVariable(variable, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in variable: " + variable);
                }
            }

            foreach (Trigger trigger in triggers)
            {
                try
                {
                    CompileGlobalVars(trigger.ConditionBlock, variableList, variableUses);
                    CompileGlobalVars(trigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in trigger: " + trigger.ID);//code?
                }
            }

            foreach (SceneProp sceneProp in sceneProps)
            {
                try
                {
                    SimpleTrigger[] spTriggers = sceneProp.SimpleTriggers;
                    foreach (SimpleTrigger spTrigger in spTriggers)
                        CompileGlobalVars(spTrigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in scene prop: " + sceneProp.ID);//code?
                }
            }

            foreach (Dialog dialog in dialogs)
            {
                try
                {
                    CompileGlobalVars(dialog.ConditionBlock, variableList, variableUses);
                    CompileGlobalVars(dialog.ConsequenceBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in dialog line: " + dialog.ID);//code?
                }
            }

            foreach (GameMenu gameMenu in menus)
            {
                try
                {
                    CompileGlobalVars(gameMenu.OperationBlock, variableList, variableUses);
                    foreach (GameMenuOption menuOption in gameMenu.MenuOptions)
                    {
                        CompileGlobalVars(menuOption.ConditionBlock, variableList, variableUses);
                        CompileGlobalVars(menuOption.ConsequenceBlock, variableList, variableUses);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in game menu: " + gameMenu.ID);//code?
                }
            }

            foreach (MissionTemplate missionTemplate in missionTemplates)
            {
                try
                {
                    foreach (Trigger trigger in missionTemplate.Triggers)
                    {
                        CompileGlobalVars(trigger.ConditionBlock, variableList, variableUses);
                        CompileGlobalVars(trigger.ConsequencesBlock, variableList, variableUses);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in mission template: " + missionTemplate.ID);//code?
                }
            }
            
            foreach (Presentation presentation in presentations)
            {
                try
                {
                    foreach (SimpleTrigger trigger in presentation.SimpleTriggers)
                        CompileGlobalVars(trigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in presentation: " + presentation.ID);//code?
                }
            }
            
            foreach (Script script in scripts)
            {
                try
                {
                    CompileGlobalVars(script.Code, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in script: " + script.ID);//code?
                }
            }

            foreach (SimpleTrigger simpleTrigger in simpleTriggers)
            {
                try
                {
                    CompileGlobalVars(simpleTrigger.ConsequencesBlock, variableList, variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in simple trigger: " + simpleTrigger.ID);//code?
                }
            }
        }

        private static List<string> LoadVariables(string exportDir, out List<int> variableUses)
        {
            List<string> variables = new List<string>();
            variableUses = new List<int>();

            try
            {
                string[] varList = File.ReadAllLines(exportDir + "variables.txt");
                foreach (string v in varList)
                {
                    string vv = v.Trim();
                    if (vv.Length != 0)
                        variables.Add(vv);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("variables.txt not found. Creating new variables.txt file");
            }

            try
            {
                string[] varList = File.ReadAllLines(exportDir + "variable_uses.txt");
                foreach (string v in varList)
                {
                    string vv = v.Trim();
                    if (vv.Length != 0)
                        variableUses.Add(int.Parse(vv));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("variable_uses.txt not found. Creating new variable_uses.txt file");
            }

            return variables;
        }

        private static void TryFileDelete(string exportDir, string fileName, string ext = ".txt")
        {
            try
            {
                File.Delete(exportDir + fileName + ext);
            }
            catch (Exception) { }
        }

        private static void SaveVariables(string exportDir, List<string> variables, List<int> variableUses)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "variables.txt"))
                foreach (string variable in variables)
                    writer.WriteLine(variable);
            using (StreamWriter writer = new StreamWriter(exportDir + "variable_uses.txt"))
                foreach (int variableUse in variableUses)
                    writer.WriteLine(variableUse);
        }

        #endregion

        #region Pseudo Code

        public static void SavePseudoCodeByType(Skriptum type, string[] code)
        {
            bool found = false;
            string id = ":";
            List<List<string>> typesCodes;
            //bool isTroop = (type.ObjectTyp == Skriptum.ObjectType.TROOP);
            string pseudoCodeFile = CodeReader.ProjectPath + "\\pseudoCodes\\" + CodeReader.Files[type.Typ].Split('.')[0] + ".mbpc";
            string directory = Path.GetDirectoryName(pseudoCodeFile);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            //if (!isTroop)
            id += type.ID;
            //else
            //    id += ((Troop)type).ID;

            List<string> typeCode = new List<string> { id };
            for (int i = 0; i < code.Length; i++)
                typeCode.Add(code[i]);

            if (File.Exists(pseudoCodeFile))
            {
                typesCodes = LoadAllPseudoCodeByFile(pseudoCodeFile);
                for (int i = 0; i < typesCodes.Count; i++)
                {
                    string tmp = typesCodes[i][0].Substring(1);
                    bool b;
                    //if (!isTroop)
                    b = tmp.Equals(type.ID);
                    //else
                    //    b = tmp.Equals(((Troop)type).ID);
                    if (b)
                        typesCodes[i] = typeCode;
                    found = b;
                    if (found)
                        i = typesCodes.Count;
                }
            }
            else
                typesCodes = new List<List<string>>();

            if (!found)
                typesCodes.Add(typeCode);

            using (StreamWriter wr = new StreamWriter(pseudoCodeFile))
            {
                foreach (List<string> typeCodeX in typesCodes)
                    foreach (string line in typeCodeX)
                        wr.WriteLine(line);
                wr.Write(EOF_TXT);
            }
        }

        public static List<List<string>> LoadAllPseudoCodeByFile(string pseudoCodeFile)
        {
            List<List<string>> typesCodes = new List<List<string>>();
            if (File.Exists(pseudoCodeFile))
            {
                using (StreamReader sr = new StreamReader(pseudoCodeFile))
                {
                    string line = string.Empty;
                    while (!sr.EndOfStream)
                    {
                        if (IsNewPseudoCode(line))
                        {
                            List<string> typeCodeX = new List<string>();
                            do
                            {
                                typeCodeX.Add(line);
                                line = sr.ReadLine();
                            } while (!IsNewPseudoCode(line) && !line.Equals(EOF_TXT));
                            typesCodes.Add(typeCodeX);
                        }
                        else
                            line = sr.ReadLine();
                    }
                }
            }
            return typesCodes;
        }

        public static List<List<string>> LoadAllPseudoCodeByObjectTypeID(int objectTypeID)
        {
            return LoadAllPseudoCodeByFile(CodeReader.ProjectPath + "\\pseudoCodes\\" + CodeReader.Files[objectTypeID].Split('.')[0] + ".mbpc");
        }

        private static bool IsNewPseudoCode(string s)
        {
            bool b = false;
            if (s.Length > 1)
                if (s.Substring(0, 1).Equals(":") && !s.Contains(" "))
                    b = true;
            return b;
        }

        #endregion
    }
}
