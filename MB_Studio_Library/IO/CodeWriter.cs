using importantLib;
using MB_Studio_Library.Objects;
using MB_Studio_Library.Objects.Support;
using static MB_Studio_Library.Objects.Skriptum;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace MB_Studio_Library.IO
{
    public class CodeWriter
    {
        #region Attributes

        public static bool IsFinished { get; private set; }
        public static string ModuleSystem { get; private set; }
        public static string DefaultModuleSystemPath { get; private set; }

        private static List<Variable> allOpcodes = null;
        private static List<Variable> allIntervalVars = null;
        private static List<Variable> commonPythonVariables = null;

        private static List<string> canFailOperations = null;
        private static List<string> lhsOperations = null;
        private static List<string> globalLhsOperations = null;

        #endregion

        #region MODULE TYPE LISTS

        public static List<string> ReservedVariables { get; private set; } = new List<string>();// MODULE VARIABLES
        public static List<string> GlobalVarsList { get; private set; } = new List<string>();
        public static List<int> GlobalVarsUses { get; private set; } = new List<int>();

        private static List<List<Skriptum>> types = null;

        private static List<int> animationIndices = new List<int>();
        private static List<List<string>> soundsArray = new List<List<string>>();

        #endregion
        
        #region Consts

        enum TagType
        {
            Register = 1,
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

        private const int OP_NUM_VALUE_BITS = 56; // 24 + 32;

        private const ulong OP_MASK_REGISTER = ((ulong)((int)TagType.Register)) << OP_NUM_VALUE_BITS;
        private const ulong OP_MASK_VARIABLE = ((ulong)((int)TagType.Variable)) << OP_NUM_VALUE_BITS;
        private const ulong OP_MASK_QUEST_INDEX = ((ulong)((int)TagType.Quest)) << OP_NUM_VALUE_BITS;
        private const ulong OP_MASK_LOCAL_VARIABLE = ((ulong)((int)TagType.LocalVariable)) << OP_NUM_VALUE_BITS;
        private const ulong OP_MASK_QUICK_STRING = ((ulong)((int)TagType.QuickString)) << OP_NUM_VALUE_BITS;

        //private string sourcePath, destinationPath;

        public const string EOF_TXT = "EOF";

        public const byte DEFAULT_FILES = 4;

        #endregion

        #region Prepare

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

            object[] paras = (object[])param;

            RichTextBox consoleOutput = (RichTextBox)paras[0];
            Form parentForm = consoleOutput.FindForm();
            ControlWriter controlTextWriter = new ControlWriter(consoleOutput, parentForm);

            //maybe make separat control for errors
            Console.SetError(controlTextWriter); //new ControlWriter(consoleOutput, consoleOutput.FindForm())
            Console.SetOut(controlTextWriter);   //new ControlWriter(consoleOutput, consoleOutput.FindForm())

            PrepareAndProcessFiles();

            //WriteIDFiles();

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

        private static void SetDecimalSeparatorToUSFormat()
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = "."; 
            Thread.CurrentThread.CurrentCulture = customCulture;
        }

        private static void ReadProcessAndBuild(string exportDir)
        {
            SetDecimalSeparatorToUSFormat();

            SaveAllCodes(exportDir);

            Console.Write("__________________________________________________" + Environment.NewLine
                        + Environment.NewLine
                        + " Finished compiling!" + Environment.NewLine
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
            /*string moduleFilesPath = CodeReader.ProjectPath + "\\moduleFiles";*/

            string[] headerFiles = Directory.GetFiles(headerFilesPath);
            /*string[] moduleFiles;// = Directory.GetFiles(moduleFilesPath);

            SourceWriter.WriteAllObjects();
            moduleFiles = Directory.GetFiles(moduleFilesPath);*/
            types = CodeReader.ReadAllObjects();

            foreach (string file in headerFiles)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);

            /*foreach (string file in moduleFiles)
                File.Copy(file, ModuleSystem + Path.GetFileName(file), true);*/

            //string module_info = ModuleSystem + "module_info.py";
            //File.WriteAllText(module_info, File.ReadAllText(module_info).Replace("%MOD_NAME%", objs[1].ToString()));
        }

        private static void SaveAllCodes(string exportDir)
        {
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

        #endregion

        #region Process Methods

        private static void ProcessInit(string exportDir)
        {
            Console.WriteLine("Initializing...");

            TryFileDelete(exportDir, "tag_uses");
            TryFileDelete(exportDir, "quick_strings");
            TryFileDelete(exportDir, "variables");
            TryFileDelete(exportDir, "variable_uses");

            List<string> variables = new List<string>();
            List<int> variableUses = new List<int>();

            /*
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
                Console.WriteLine("variables.txt not found. Creating new variables.txt file");
            }
            */

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessGlobalVariables(string exportDir)
        {
            Console.WriteLine("Compiling all global variables...");

            List<string> variables = LoadVariables(exportDir, out List<int> variablesUses);

            CompileAllGlobalVars(ref variables, ref variablesUses);

            SaveVariables(exportDir, variables, variablesUses);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessStrings(string exportDir)
        {
            Console.WriteLine("Exporting strings...");

            List<Skriptum> strings = types[(int)ObjectType.GameString];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_strings.py"))
            {
                for (int i = 0; i < strings.Count; i++)
                    writer.WriteLine("str_{0} = {1}", ConvertToIdentifier(strings[i].ID), i);
                writer.WriteLine(Environment.NewLine);
            }

            // save game string
            using (StreamWriter writer = new StreamWriter(exportDir + "strings.txt"))
            {
                writer.WriteLine("stringsfile version 1");//change version if needed
                writer.WriteLine(strings.Count);
                foreach (GameString gameString in strings)
                    writer.WriteLine("str_{0} {1}", ConvertToIdentifier(gameString.ID), ReplaceSpaces(gameString.Text));
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessSkills(string exportDir)
        {
            Console.WriteLine("Exporting skills...");

            List<Skriptum> skills = types[(int)ObjectType.Skill];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_skills.py"))
            {
                for (int i = 0; i < skills.Count; i++)
                    writer.WriteLine("skl_{0} = {1}", ConvertToIdentifier(skills[i].ID), i);
                writer.WriteLine(Environment.NewLine);
            }

            // save skills
            using (StreamWriter writer = new StreamWriter(exportDir + "skills.txt"))
            {
                writer.WriteLine(skills.Count);
                foreach (Skill skill in skills)
                {
                    writer.Write("skl_{0} {1} ", ConvertToIdentifier(skill.ID), ReplaceSpaces(skill.Name));
                    writer.WriteLine("{0} {1} {2}", skill.FlagsGZ, skill.MaxLevel, skill.Description.Replace(' ', '_'));
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessMusic(string exportDir)
        {
            Console.WriteLine("Exporting tracks...");

            List<Skriptum> tracks = types[(int)ObjectType.Music];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_music.py"))
            {
                for (int i = 0; i < tracks.Count; i++)
                    writer.WriteLine("track_{0} = {1}", tracks[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save tracks
            using (StreamWriter writer = new StreamWriter(exportDir + "music.txt"))
            {
                writer.WriteLine(tracks.Count);
                foreach (Music track in tracks)
                {
                    writer.WriteLine(
                        "{0} {1} {2}",
                        track.TrackFile,
                        track.TrackFlagsGZ,
                        (track.TrackFlagsGZ | track.ContinueTrackFlagsGZ)
                    );
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessAnimations(string exportDir)
        {
            Console.WriteLine("Exporting animations...");

            List<Skriptum> animations = types[(int)ObjectType.Animation];

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
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_actions.py"))
            {
                for (int i = 0; i < animations.Count; i++)
                    writer.WriteLine("anim_{0} = {1}", animations[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save animations
            using (StreamWriter writer = new StreamWriter(exportDir + "actions.txt"))
            {
                writer.WriteLine(actionCodes.Count);
                foreach (Animation action in actionCodes)
                {
                    writer.Write(" {0} {1} {2} ", action.ID, action.FlagsGZ, action.MasterFlagsGZ);//print flags
                    writer.WriteLine(" {0}", action.Sequences.Length);
                    foreach (AnimationSequence sequence in action.Sequences)
                    {
                        writer.Write("  {0:F6} {1} {2} {3} {4} ",
                            sequence.Duration,
                            sequence.ResourceName,
                            sequence.BeginFrame,
                            sequence.EndFrame,
                            sequence.FlagsGZ
                        );
                        writer.Write("{0} ", sequence.LastNumberGZ);

                        double[] lastNums = sequence.LastNumbersFKZ;
                        for (int i = 0; i < lastNums.Length; i++)
                        {
                            if (!lastNums[i].Equals(double.NaN))
                            {
                                if (i == (lastNums.Length - 1))
                                    writer.Write(" ");
                                if (lastNums[i] < 0d && Math.Round(lastNums[i], 6) == 0d)
                                    writer.Write("-");
                                writer.Write("{0:F6} ", lastNums[i]);
                            }
                            else
                                writer.Write("0.0 ");
                        }
                        writer.WriteLine();
                    }
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessMeshes(string exportDir)
        {
            Console.WriteLine("Exporting meshes...");

            List<Skriptum> meshes = types[(int)ObjectType.Mesh];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_meshes.py"))
            {
                for (int i = 0; i < meshes.Count; i++)
                    writer.WriteLine("mesh_{0} = {1}", meshes[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save meshes
            using (StreamWriter writer = new StreamWriter(exportDir + "meshes.txt"))
            {
                writer.WriteLine(meshes.Count);
                foreach (Mesh mesh in meshes)
                {
                    writer.WriteLine("mesh_{0} {1} {2} {3:F6} {4:F6} {5:F6} {6:F6} {7:F6} {8:F6} {9:F6} {10:F6} {11:F6}",
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

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessSounds(string exportDir)
        {
            Console.WriteLine("Exporting sounds...");

            List<Skriptum> sounds = types[(int)ObjectType.Sound];

            // compile/filter sounds
            List<string[]> allSounds = new List<string[]>();
            foreach (Sound sound in sounds)
            {
                string[] soundFiles = sound.SoundFiles;
                ulong soundFlags = sound.FlagsGZ;
                for (int i = 0; i < soundFiles.Length; i++)
                {
                    bool found = false;
                    int soundNo = 0;
                    string[] soundFile = soundFiles[i].Split();
                    if (soundFile.Length == 1)
                        soundFile = new string[] { soundFile[0], "0" };
                    while (soundNo < allSounds.Count && !found)
                    {
                        if (allSounds[soundNo][0].Equals(soundFile[0]))
                            found = true;
                        else
                            soundNo++;
                    }
                    if (!found)
                    {
                        soundNo = allSounds.Count;
                        allSounds.Add(new string[] { soundFile[0], soundFlags.ToString() });
                    }
                    soundFiles[i] = soundNo + " " + soundFile[1];
                }
                List<string> vs = new List<string>();
                foreach (string v in soundFiles)
                    vs.Add(v);
                soundsArray.Add(vs);
            }

            // save sounds
            using (StreamWriter writer = new StreamWriter(exportDir + "sounds.txt"))
            {
                writer.WriteLine("soundsfile version 3");//change version if necessary

                writer.WriteLine(allSounds.Count);
                foreach (var soundSample in allSounds)
                    writer.WriteLine(" {0} {1}", soundSample);

                writer.WriteLine(sounds.Count);
                for (int i = 0; i < sounds.Count; i++)
                {
                    Sound sound = (Sound)sounds[i];
                    writer.Write("snd_{0} {1} {2} ", sound.ID, sound.FlagsGZ, sound.SoundFiles.Length);
                    foreach (string sample in soundsArray[i])
                    {
                        writer.Write("{0} {1} ", sample.Split());
                    }
                    writer.WriteLine();
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_sounds.py"))
            {
                for (int i = 0; i < sounds.Count; i++)
                    writer.WriteLine("snd_{0} = {1}", sounds[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessSkins(string exportDir)
        {
            int maxSkinCount = 16;//change if higher for bannerlord or other versions

            Console.WriteLine("Exporting skins...");

            List<Skriptum> skins = types[(int)ObjectType.Skin];

            // save skins
            using (StreamWriter writer = new StreamWriter(exportDir + "skins.txt"))
            {
                writer.WriteLine("skins_file version 1");//change version if necessary
                if (skins.Count > maxSkinCount)
                    skins.RemoveRange(maxSkinCount, skins.Count - maxSkinCount);
                writer.WriteLine(skins.Count);

                foreach (Skin skin in skins)
                {
                    writer.WriteLine("{0} {1}" + Environment.NewLine + " {2} {3} {4}", skin.ID, skin.Flags, skin.BodyMesh, skin.CalfMesh, skin.HandMesh);
                    writer.Write(" {0} {1} ", skin.HeadMesh, skin.FaceKeys.Length);

                    foreach (FaceKey faceKey in skin.FaceKeys)
                    {
                        writer.Write("skinkey_{0} {1} {2} {3:F6} {4:F6} {5} ", 
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
                        writer.Write(" {0} ", hairMeshName);

                    writer.WriteLine(Environment.NewLine + " " + skin.BeardMeshes.Length);
                    foreach (string beardMeshName in skin.BeardMeshes)
                        writer.WriteLine("  {0}", beardMeshName);
                    writer.WriteLine();

                    WriteTextures(writer, skin.HairTextures);
                    WriteTextures(writer, skin.BeardTextures);
                    WriteFaceTex(writer, skin.FaceTextures);
                    WriteVoices(writer, skin.Voices);

                    writer.Write(" {0} {1:F6} ", skin.SkeletonName, skin.Scale);
                    writer.WriteLine(Environment.NewLine + "{0} {1}", skin.BloodParticle1GZ, skin.BloodParticle2GZ);

                    writer.WriteLine(skin.FaceKeyConstraints.Length);
                    foreach (var constraint in skin.FaceKeyConstraints)
                    {
                        writer.Write(Environment.NewLine + "{0:F6} {1} {2} ", constraint.Number, constraint.CompMode, constraint.ValuesINT.Length);
                        for (int i = 0; i < constraint.ValuesINT.Length; i++)
                            writer.Write(" {0:F6} {1}", constraint.ValuesDOUBLE[i], constraint.ValuesINT[i]);
                    }
                    writer.WriteLine();
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessFactions(string exportDir)
        {
            Console.WriteLine("Exporting faction data...");

            List<Skriptum> factions = types[(int)ObjectType.Faction];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_factions.py"))
            {
                for (int i = 0; i < factions.Count; i++)
                    writer.WriteLine("fac_{0} = {1}", factions[i].ID, i);
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

                    writer.WriteLine("fac_{0} {1} {2} {3} ",
                        ConvertToIdentifier(faction.ID),
                        ReplaceSpaces(faction.Name),
                        faction.FlagsGZ,
                        HexConverter.Hex2Dec(faction.ColorCode.Replace("0x", string.Empty))
                    );

                    foreach (double relation in relations[i])
                        writer.Write(" {0:F6} ", relation);
                    writer.WriteLine();

                    writer.Write("{0} ", faction.Ranks.Length);
                    foreach (string rank in faction.Ranks)
                        writer.Write(" {0} ", ReplaceSpaces(rank));
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessScenes(string exportDir)
        {
            Console.WriteLine("Exporting scene data...");

            List<Skriptum> scenes = types[(int)ObjectType.Scene];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_scenes.py"))
            {
                for (int i = 0; i < scenes.Count; i++)
                    writer.WriteLine("scn_{0} = {1}", scenes[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            // save scenes
            using (StreamWriter writer = new StreamWriter(exportDir + "scenes.txt"))
            {
                writer.WriteLine("scenesfile version 1");//change version if necessary
                writer.WriteLine(" {0}", scenes.Count);
                foreach (Scene scene in scenes)
                {
                    writer.WriteLine("scn_{0} {1} {2} {3} {4} {5:F6} {6:F6} {7:F6} {8:F6} {9:F6} {10} ",
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

                    writer.Write("  {0} ", scene.OtherScenes.Length);
                    foreach (string passage in scene.OtherScenes)
                        WritePassage(writer, scenes, passage);
                    writer.WriteLine();

                    string troopPrefix = types[(int)ObjectType.Troop][0].Prefix;
                    writer.Write("  {0} ", scene.ChestTroops.Length);
                    foreach (string chestTroop in scene.ChestTroops)
                    {
                        string troopId = chestTroop;
                        if (troopId.StartsWith(troopPrefix))
                            troopId = troopId.Substring(troopPrefix.Length);
                        int troopNo = FindObject(ObjectType.Troop, troopId);
                        if (troopNo < 0)
                        {
                            Console.WriteLine("Error unable to find chest-troop: " + troopId);
                            troopNo = 0;
                        }
                        else
                            AddTagUse(tagUses, TagType.Troop, troopNo);
                        writer.Write(" {0} ", troopNo);
                    }
                    writer.WriteLine();

                    writer.WriteLine(" {0} ", scene.TerrainBase);
                }
            }

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessParticleSys(string exportDir)
        {
            Console.WriteLine("Exporting particle data...");

            List<Skriptum> particleSystems = types[(int)ObjectType.ParticleSystem];

            // save particle systems
            using (StreamWriter writer = new StreamWriter(exportDir + "particle_systems.txt"))
            {
                writer.WriteLine("particle_systemsfile version 1");//change version if necessary
                writer.WriteLine(particleSystems.Count);
                foreach (ParticleSystem psys in particleSystems)
                {
                    writer.Write("psys_{0} {1} {2}  ", psys.ID, psys.FlagsGZ, psys.MeshName);
                    writer.Write("{0} {1:F6} {2:F6} ", psys.ParticlesPerSecond, psys.ParticleLifeTime, psys.Damping);

                    if (psys.GravityStrength < 0d && Math.Round(psys.GravityStrength, 6) == 0d)
                        writer.Write("-");//-0.000000
                    writer.Write("{0:F6} ", psys.GravityStrength);

                    writer.WriteLine("{0:F6} {1:F6} ", psys.TurbulanceSize, psys.TurbulanceStrength);

                    SavePsysKey(writer, psys.AlphaKeys);
                    SavePsysKey(writer, psys.RedKeys);
                    SavePsysKey(writer, psys.GreenKeys);
                    SavePsysKey(writer, psys.BlueKeys);
                    SavePsysKey(writer, psys.ScaleKeys);

                    writer.Write("{0:F6} {1:F6} {2:F6}   ", psys.EmitBoxScale[0], psys.EmitBoxScale[1], psys.EmitBoxScale[2]);
                    writer.Write("{0:F6} {1:F6} {2:F6}   ", psys.EmitVelocity[0], psys.EmitVelocity[1], psys.EmitVelocity[2]);
                    writer.WriteLine("{0:F6} ", psys.EmitDirectionRandomness);

                    if (!psys.ParticleRotationSpeed.Equals(double.NaN))
                        writer.Write("{0:F6} ", psys.ParticleRotationSpeed);
                    else
                        writer.Write("0.0 ");

                    if (!psys.ParticleRotationDamping.Equals(double.NaN))
                        writer.Write("{0:F6} ", psys.ParticleRotationDamping);
                    else
                        writer.Write("0.0 ");

                    writer.WriteLine();
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_particle_systems.py"))
            {
                for (int i = 0; i < particleSystems.Count; i++)
                    writer.WriteLine("psys_{0} = {1}", particleSystems[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessSceneProps(string exportDir)
        {
            Console.WriteLine("Exporting scene props...");

            List<Skriptum> sceneProps = types[(int)ObjectType.SceneProp];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_scene_props.py"))
            {
                for (int i = 0; i < sceneProps.Count; i++)
                    writer.WriteLine("spr_{0} = {1}", sceneProps[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            // save scene props
            using (StreamWriter writer = new StreamWriter(exportDir + "scene_props.txt"))
            {
                writer.WriteLine("scene_propsfile version 1");//change version if necessary
                writer.WriteLine(" {0}", sceneProps.Count);
                foreach (SceneProp sceneProp in sceneProps)
                {
                    writer.Write("spr_{0} {1} {2} {3} {4} ", sceneProp.ID, sceneProp.FlagsGZ, sceneProp.HitPoints, sceneProp.MeshName, sceneProp.PhysicsObjectName);
                    SaveSimpleTriggers(writer, sceneProp.SimpleTriggers, variableList, variableUses, tagUses, quickStrings);
                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessQuests(string exportDir)
        {
            Console.WriteLine("Exporting quest data...");

            List<Skriptum> quests = types[(int)ObjectType.Quest];

            // save quest
            using (StreamWriter writer = new StreamWriter(exportDir + "quests.txt"))
            {
                writer.WriteLine("questsfile version 1");//change version if necessary
                writer.WriteLine(quests.Count);
                foreach (Quest quest in quests)
                {
                    writer.Write("qst_{0} {1} {2} ", quest.ID, quest.Name.Replace(' ', '_'), quest.FlagsGZ);
                    writer.WriteLine("{0} ", quest.Description.Replace(' ', '_'));
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_quests.py"))
            {
                for (int i = 0; i < quests.Count; i++)
                    writer.WriteLine("qst_{0} = {1}", quests[i].ID, i);
                for (int i = 0; i < quests.Count; i++)
                {
                    ulong idx = ((ulong)i);
                    writer.WriteLine("qsttag_{0} = {1}", quests[i].ID, OP_MASK_QUEST_INDEX | idx);
                }
                writer.WriteLine(Environment.NewLine);
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessInfoPages(string exportDir)
        {
            Console.WriteLine("Exporting info_page data...");

            List<Skriptum> infoPages = types[(int)ObjectType.InfoPage];

            // save info pages
            using (StreamWriter writer = new StreamWriter(exportDir + "info_pages.txt"))
            {
                writer.WriteLine("infopagesfile version 1");//change version if necessary
                writer.WriteLine(infoPages.Count);
                foreach (InfoPage infoPage in infoPages)
                    writer.WriteLine("ip_{0} {1} {2}", infoPage.ID, infoPage.Name.Replace(' ', '_'), infoPage.Text.Replace(' ', '_'));
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_info_pages.py"))
            {
                for (int i = 0; i < infoPages.Count; i++)
                    writer.WriteLine("ip_{0} = {1}", infoPages[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessSimpleTriggers(string exportDir)
        {
            Console.WriteLine("Exporting simple triggers...");

            List<Skriptum> simpleTriggers = types[(int)ObjectType.SimpleTrigger];

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

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessTriggers(string exportDir)
        {
            Console.WriteLine("Exporting triggers...");

            List<Skriptum> triggers = types[(int)ObjectType.Trigger];

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "triggers.txt"))
            {
                writer.WriteLine("triggersfile version 1");//change version if necessary
                writer.WriteLine(triggers.Count);
                foreach (Trigger trigger in triggers)
                {
                    writer.Write("{0:F6} {1:F6} {2:F6} ",
                        GetIntervalValue(trigger.CheckInterval),
                        GetIntervalValue(trigger.DelayInterval),
                        GetIntervalValue(trigger.ReArmInterval)
                    );
                    SaveStatementBlock(writer, 0, true, trigger.ConditionBlock, variables, variableUses, tagUses, quickStrings);
                    SaveStatementBlock(writer, 0, true, trigger.ConsequencesBlock, variables, variableUses, tagUses, quickStrings);
                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessDialogs(string exportDir)
        {
            Console.WriteLine("Exporting dialogs...");

            List<Skriptum> dialogs = types[(int)ObjectType.Dialog];

            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            List<int> outputStates = CompileSentenceTokens(exportDir, dialogs, out List<int> inputStates);
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
                        writer.Write("{0} {1} {2} ", dialogId, dialog.TalkingPartnerCode, inputStates[i]);
                        SaveStatementBlock(writer, 0, true, dialog.ConditionBlock, variables, variableUses, tagUses, quickStrings);

                        writer.Write("{0} ", dialog.DialogText.Replace(' ', '_'));
                        if (dialog.DialogText.Length == 0)
                            writer.Write("NO_TEXT ");
                        writer.Write(" {0} ", outputStates[i]);
                        SaveStatementBlock(writer, 0, true, dialog.ConsequenceBlock, variables, variableUses, tagUses, quickStrings);

                        if (dialog.VoiceOverSoundFile.Length > 0)
                            writer.WriteLine("{0} ", dialog.VoiceOverSoundFile);
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

            SaveVariables(exportDir, variables, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessPostfxParams(string exportDir)
        {
            Console.WriteLine("Exporting postfx_params...");

            List<Skriptum> postfxParams = types[(int)ObjectType.PostFX];

            // save postfx_params
            using (StreamWriter writer = new StreamWriter(exportDir + "postfx.txt"))
            {
                writer.WriteLine("postfx_paramsfile version 1");//change version if necessary
                writer.WriteLine(postfxParams.Count);
                foreach (PostFX postFX in postfxParams)
                {
                    writer.Write("pfx_{0} {1} {2}", postFX.ID, postFX.FlagsGZ, postFX.TonemapOperatorType);

                    writer.Write("  {0:F6} {1:F6} {2:F6} {3:F6}",
                        postFX.ShaderParameter1[0],
                        postFX.ShaderParameter1[1],
                        postFX.ShaderParameter1[2],
                        postFX.ShaderParameter1[3]
                    );

                    writer.Write("  {0:F6} {1:F6} {2:F6} {3:F6}",
                        postFX.ShaderParameter2[0],
                        postFX.ShaderParameter2[1],
                        postFX.ShaderParameter2[2],
                        postFX.ShaderParameter2[3]
                    );

                    writer.Write("  {0:F6} {1:F6} {2:F6} {3:F6}",
                        postFX.ShaderParameter3[0],
                        postFX.ShaderParameter3[1],
                        postFX.ShaderParameter3[2],
                        postFX.ShaderParameter3[3]
                    );

                    writer.WriteLine();
                }
            }

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_postfx_params.py"))
            {
                for (int i = 0; i < postfxParams.Count; i++)
                    writer.WriteLine("pfx_{0} = {1}", postfxParams[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessItems(string exportDir)
        {
            Console.WriteLine("Exporting item data...");

            List<Skriptum> items = types[(int)ObjectType.Item];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_items.py"))
            {
                for (int i = 0; i < items.Count; i++)
                    writer.WriteLine("itm_{0} = {1}", ConvertToIdentifier(items[i].ID), i);
                writer.WriteLine(Environment.NewLine);
            }

            // save items
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "item_kinds1.txt"))
            {
                writer.WriteLine("itemsfile version 3");//change version if necessary
                writer.WriteLine(items.Count);

                foreach (Item item in items)
                {
                    if (item.Properties.Contains("itp_merchandise"))
                    {
                        int idNo = FindObject(ObjectType.Item, ConvertToIdentifier(item.ID));
                        AddTagUse(tagUses, TagType.Item, idNo);
                    }

                    writer.Write(" itm_{0} {1} {2} {3} ",
                        ConvertToIdentifier(item.ID),
                        ReplaceSpaces(item.Name),
                        ReplaceSpaces(item.PluralName),
                        item.Meshes.Count
                    );

                    foreach (var mesh in item.Meshes)
                        writer.Write(" {0} {1} ", mesh.Name, mesh.Value);

                    writer.WriteLine(" {0} {1} {2} {3} {4:F6} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16}",
                        item.PropertiesGZ,
                        item.CapabilityFlagsGZ,
                        item.Price,
                        item.ModBitsGZ,
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

                    writer.WriteLine(" {0}", item.Factions.Count);
                    foreach (int faction in item.Factions)
                        writer.Write(" {0}", faction);
                    if (item.Factions.Count > 0)
                        writer.WriteLine();

                    SaveSimpleTriggers(writer, item.SimpleTriggers.ToArray(), variableList, variableUses, tagUses, quickStrings);//activate if working!!!
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessMapIcons(string exportDir)
        {
            Console.WriteLine("Exporting map icons...");

            List<Skriptum> mapIcons = types[(int)ObjectType.MapIcon];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_map_icons.py"))
            {
                for (int i = 0; i < mapIcons.Count; i++)
                    writer.WriteLine("icon_{0} = {1}", mapIcons[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save map icons
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "map_icons.txt"))
            {
                writer.WriteLine("map_icons_file version 1");//change version if necessary
                writer.WriteLine(mapIcons.Count);

                foreach (MapIcon mapIcon in mapIcons)
                {
                    writer.Write("{0} {1} {2} {3:F6} {4} ",
                        mapIcon.ID,
                        mapIcon.FlagsGZ,
                        mapIcon.MapIconName,
                        mapIcon.Scale,
                        mapIcon.SoundID
                    );

                    if (!mapIcon.OffsetX.Equals(double.NaN))
                        writer.Write("{0:F6} {1:F6} {2:F6} ", mapIcon.OffsetX, mapIcon.OffsetY, mapIcon.OffsetZ);
                    else
                        writer.Write("0 0 0 ");

                    SaveSimpleTriggers(writer, mapIcon.SimpleTriggers, variableList, variableUses, tagUses, quickStrings);

                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessTroops(string exportDir)
        {
            Console.WriteLine("Exporting troops data...");

            List<Skriptum> troops = types[(int)ObjectType.Troop];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_troops.py"))
            {
                for (int i = 0; i < troops.Count; i++)
                    writer.WriteLine("trp_{0} = {1}", troops[i].ID, i);
                writer.WriteLine(Environment.NewLine);
            }

            // save troops
            using (StreamWriter writer = new StreamWriter(exportDir + "troops.txt"))
            {
                writer.WriteLine("troopsfile version 2");//change version if necessary
                writer.Write(troops.Count);

                foreach (Troop troop in troops)
                {
                    if (troop.SceneCodeGZ > 0)
                    {
                        //ulong TSF_SIZE_ID_MASK = 0x0000ffff;
                        //AddTagUse(tagUses, TagType.Scene, troop.SceneCodeGZ & TSF_SIZE_ID_MASK);
                        int idNo = FindObject(ObjectType.Troop, ConvertToIdentifier(troop.ID));
                        //if (idNo >= 0)
                        //    AddTagUse(tagUses, TagType.Troop, idNo);
                        //if (troop.FactionID > 0)
                        //    AddTagUse(tagUses, TagType.Faction, troop.FactionID);
                    }

                    writer.WriteLine(Environment.NewLine + "trp_{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                        ConvertToIdentifier(troop.ID),
                        ReplaceSpaces(troop.Name),
                        ReplaceSpaces(troop.PluralName),
                        troop.DialogImage,
                        troop.FlagsGZ,
                        troop.SceneCodeGZ,
                        troop.ReservedGZ,
                        troop.FactionID,
                        troop.UpgradeTroop1,
                        troop.UpgradeTroop2
                    );

                    writer.Write("  ");
                    for (int i = 0; i < troop.Items.Count; i++)
                    {
                        //AddTagUse(tagUses, TagType.Item, item);
                        writer.Write("{0} {1} ", troop.Items[i], troop.ItemFlags[i] << 24);//check if 24 is done before
                    }

                    int maxItemCount = 64;// is 64 enough or maybe later more?
                    int leftOver = maxItemCount - troop.Items.Count;
                    for (int i = 0; i < leftOver; i++)
                        writer.Write("-1 0 ");
                    writer.Write(Environment.NewLine + " ");

                    writer.WriteLine(" {0} {1} {2} {3} {4}", troop.Strength, troop.Agility, troop.Intelligence, troop.Charisma, troop.Level);

                    foreach (int wp in troop.Proficiencies)
                        writer.Write(" {0}", wp);
                    writer.WriteLine();

                    int skillWordCount = 6;
                    string tmp = string.Empty;
                    foreach (int skill in troop.Skills)
                        tmp += HexConverter.Dec2Hex((ulong)skill).Substring(7);//only 4 Bit per skill --> only one hex character
                    tmp += "000000"; // maybe replace later if there are more than 42 skills possible 192 / 4 = 48

                    uint[] skillCodes = new uint[skillWordCount];
                    for (int i = skillWordCount - 1; i >= 0; i--)
                        skillCodes[i] = uint.Parse(HexConverter.Hex2Dec(/*ImportantMethods.ReverseString(*/tmp.Substring(i * 8, 8)/*)*/).ToString());

                    SuperGZ_192Bit skillVals = new SuperGZ_192Bit(skillCodes);
                    foreach (uint skillCode in skillVals.ValueUInt)
                        writer.Write("{0} ", skillCode);

                    writer.Write(Environment.NewLine + "  ");

                    int numFaceNumericKeys = 4;
                    string[] faces = new string[] { troop.Face1, troop.Face2 };
                    foreach (string face in faces)
                    {
                        if (face.StartsWith("0x"))
                        {
                            string faceTmp = face.Substring(2);
                            if (faceTmp.Length == (numFaceNumericKeys * 16))
                            {
                                List<ulong> wordKeys = new List<ulong>();
                                for (int i = 0; i < numFaceNumericKeys; i++)
                                    wordKeys.Add(HexConverter.Hex2Dec_16CHARS(faceTmp.Substring((numFaceNumericKeys - (i + 1)) * 16, 16)));
                                int maxCount = numFaceNumericKeys - 1;
                                for (int i = 0; i < wordKeys.Count; i++)
                                    writer.Write("{0} ", wordKeys[maxCount - i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numFaceNumericKeys; i++)
                                writer.Write("{0} ", 0);
                            Console.WriteLine("FACE_FORMAT_ERROR: " + face);
                        }
                    }

                    writer.WriteLine();
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessTableauMaterials(string exportDir)
        {
            Console.WriteLine("Exporting tableau materials data...");

            List<Skriptum> tableaus = types[(int)ObjectType.TableauMaterial];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_tableau_materials.py"))
            {
                for (int i = 0; i < tableaus.Count; i++)
                    writer.WriteLine("tableau_{0} = {1}", tableaus[i].ID, i);
            }

            // save tableaus
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "tableau_materials.txt"))
            {
                writer.WriteLine(tableaus.Count);
                foreach (TableauMaterial tableau in tableaus)
                {
                    writer.Write("tab_{0} {1} {2} {3} {4} {5} {6} {7} {8}",
                        tableau.ID,
                        tableau.FlagsGZ,
                        tableau.SampleMaterialName,
                        tableau.Width,
                        tableau.Height,
                        tableau.MinX,
                        tableau.MinY,
                        tableau.MaxX,
                        tableau.MaxY
                    );
                    SaveStatementBlock(writer, 0, true, tableau.OperationBlock, variableList, variableUses, tagUses, quickStrings);
                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }
        
        private static void ProcessPresentations(string exportDir)
        {
            Console.WriteLine("Exporting presentations...");

            List<Skriptum> presentations = types[(int)ObjectType.Presentation];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_presentations.py"))
            {
                for (int i = 0; i < presentations.Count; i++)
                    writer.WriteLine("prsnt_{0} = {1}", presentations[i].ID, i);
            }

            // save presentations
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "presentations.txt"))
            {
                writer.WriteLine("presentationsfile version 1");//change version if necessary
                writer.WriteLine(" {0}", presentations.Count);
                foreach (Presentation presentation in presentations)
                {
                    writer.Write("prsnt_{0} {1} {2} ", presentation.ID, presentation.Flags, presentation.MeshID);
                    SaveSimpleTriggers(writer, presentation.SimpleTriggers, variableList, variableUses, tagUses, quickStrings);
                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }
        
        private static void ProcessScripts(string exportDir)
        {
            Console.WriteLine("Exporting scripts...");

            List<Skriptum> scripts = types[(int)ObjectType.Script];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_scripts.py"))
                for (int i = 0; i < scripts.Count; i++)
                    writer.WriteLine("script_{0} = {1}", scripts[i].ID, i);

            // save presentations
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "scripts.txt"))
            {
                writer.WriteLine("scriptsfile version 1");//change version if necessary
                writer.WriteLine(scripts.Count);
                foreach (Script script in scripts)
                {
                    float unknownValue = -1;
                    string scriptId = ConvertToIdentifier(script.ID);
                    writer.Write("{0}", scriptId);
                    if (unknownValue < 0)
                        writer.WriteLine(" {0}", unknownValue);
                    else
                        writer.WriteLine(" {0:F6}", unknownValue);
                    SaveStatementBlock(writer, scriptId, false, script.Code, variableList, variableUses, tagUses, quickStrings);
                    /*
    if (type(func[1]) == list_type):
      file.write("{0} -1\n"%(convert_to_identifier(func[0])))
      save_statement_block(file,convert_to_identifier(func[0]), 0,func[1], variable_list,variable_uses,tag_uses,quick_strings)
    else:
      file.write("{0} {1:F6}\n"%(convert_to_identifier(func[0]), func[1]))
      save_statement_block(file,convert_to_identifier(func[0]), 0,func[2], variable_list,variable_uses,tag_uses,quick_strings)
                    */

                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }
        
        private static void ProcessMenus(string exportDir)
        {
            Console.WriteLine("Exporting menus...");

            List<Skriptum> menus = types[(int)ObjectType.GameMenu];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_menus.py"))
            {
                for (int i = 0; i < menus.Count; i++)
                    writer.WriteLine("menu_{0} = {1}", menus[i].ID, i);
            }

            // save menus
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "menus.txt"))
            {
                writer.WriteLine("menusfile version 1");//change version if necessary
                writer.WriteLine(" {0}", menus.Count);
                foreach (GameMenu menu in menus)
                {
                    writer.Write("menu_{0} {1} {2} {3}", menu.ID, menu.FlagsGZ, menu.Text.Replace(' ', '_'), menu.MeshName);
                    SaveStatementBlock(writer, 0, true, menu.OperationBlock, variableList, variableUses, tagUses, quickStrings);
                    writer.WriteLine(menu.MenuOptions.Length);

                    foreach (GameMenuOption option in menu.MenuOptions)
                    {
                        writer.Write(" {0} ", option.Name);
                        SaveStatementBlock(writer, 0, true, option.ConditionBlock, variableList, variableUses, tagUses, quickStrings);
                        writer.Write(" {0} ", option.Text.Replace(' ', '_'));
                        SaveStatementBlock(writer, 0, true, option.ConsequenceBlock, variableList, variableUses, tagUses, quickStrings);
                        writer.Write(" {0} ", option.DoorText.Replace(' ', '_'));
                    }

                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }
        
        private static void ProcessMissionTemplates(string exportDir)
        {
            Console.WriteLine("Exporting mission_template data...");

            List<Skriptum> missionTemplates = types[(int)ObjectType.MissionTemplate];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_mission_templates.py"))
            {
                for (int i = 0; i < missionTemplates.Count; i++)
                    writer.WriteLine("mst_{0} = {1}", missionTemplates[i].ID, i);
            }

            // save mission templates
            List<string> variableList = LoadVariables(exportDir, out List<int> variableUses);
            List<List<int>> tagUses = LoadTagUses(exportDir);
            List<string[]> quickStrings = LoadQuickStrings(exportDir);

            using (StreamWriter writer = new StreamWriter(exportDir + "mission_templates.txt"))
            {
                writer.WriteLine("missionsfile version 1");//change version if necessary
                writer.WriteLine(" {0}", missionTemplates.Count);
                foreach (MissionTemplate missionTemplate in missionTemplates)
                {
                    writer.Write("mst_{0} {1} {2} ",
                        ConvertToIdentifier(missionTemplate.ID),
                        ConvertToIdentifier(missionTemplate.ID),
                        missionTemplate.FlagsGZ
                    );

                    writer.WriteLine(" {0}", missionTemplate.MissionTypeGZ);
                    writer.WriteLine("{0} ", missionTemplate.Description.Replace(' ', '_'));
                    writer.Write(Environment.NewLine + "{0} ", missionTemplate.EntryPoints.Length);

                    foreach (Entrypoint entryPointGroup in missionTemplate.EntryPoints)
                        SaveMissionTemplateGroup(writer, entryPointGroup, tagUses);

                    SaveTriggers(writer, missionTemplate.Triggers, variableList, variableUses, tagUses, quickStrings);

                    writer.WriteLine();
                }
            }

            SaveVariables(exportDir, variableList, variableUses);
            SaveTagUses(exportDir, tagUses);
            SaveQuickStrings(exportDir, quickStrings);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessPartyTemplates(string exportDir)
        {
            Console.WriteLine("Exporting party_template data...");

            List<Skriptum> partyTemplates = types[(int)ObjectType.PartyTemplate];

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_party_templates.py"))
                for (int i = 0; i < partyTemplates.Count; i++)
                    writer.WriteLine("pt_{0} = {1}", partyTemplates[i].ID, i);

            // save party template
            using (StreamWriter writer = new StreamWriter(exportDir + "party_templates.txt"))
            {
                writer.WriteLine("partytemplatesfile version 1");//change version if necessary
                writer.WriteLine(partyTemplates.Count);
                foreach (PartyTemplate partyTemplate in partyTemplates)
                {
                    //AddTagUse(tagUses, TagType.Faction, partyTemplate.FactionID);
                    writer.Write("pt_{0} {1} {2} {3} {4} {5} ",
                        ConvertToIdentifier(partyTemplate.ID),
                        ReplaceSpaces(partyTemplate.Name),
                        partyTemplate.FlagsGZ,
                        partyTemplate.MenuID,
                        partyTemplate.FactionID,
                        partyTemplate.Personality
                    );

                    foreach (PMember member in partyTemplate.Members)
                        SavePartyTemplateTroop(writer, member);

                    int maxMembers = 6;
                    for (int i = 0; i < maxMembers - partyTemplate.Members.Length; i++)
                        SavePartyTemplateTroop(writer, PMember.DEFAULT_MEMBER);

                    writer.WriteLine();
                }
            }

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessParties(string exportDir)
        {
            Console.WriteLine("Exporting parties...");

            List<Skriptum> parties = types[(int)ObjectType.Party];

            List<List<int>> tagUses = LoadTagUses(exportDir);

            // save python header
            using (StreamWriter writer = new StreamWriter(ModuleSystem + "ID_parties.py"))
                for (int i = 0; i < parties.Count; i++)
                    writer.WriteLine("p_{0} = {1}", parties[i].ID, i);

            // save parties
            using (StreamWriter writer = new StreamWriter(exportDir + "parties.txt"))
            {
                writer.WriteLine("partiesfile version 1");//change version if necessary
                writer.WriteLine("{0} {0}", parties.Count);

                for (int i = 0; i < parties.Count; i++)
                {
                    Party party = (Party)parties[i];

                    if (party.FactionID >= 0)
                        AddTagUse(tagUses, TagType.Faction, party.FactionID);

                    writer.Write(" 1 {0} {0} ", i);
                    //writer.Write(" 1 {0} ", i);
                    writer.Write("p_{0} {1} {2} ", ConvertToIdentifier(party.ID), ReplaceSpaces(party.Name), party.FlagsGZ);

                    //int menuNo = FindObject(menus, party.Menu);
                    //if (menuNo < 0)
                    //    Console.WriteLine("Error: Unable to find menu-id: " + party.Menu);

                    writer.Write("{0} ", party.MenuID);

                    writer.Write("{0} {1} {2} {3} {4} ",
                        party.PartyTemplateID,
                        party.FactionID,
                        party.Personality,
                        party.Personality,
                        party.AIBehavior
                    );

                    writer.Write("{0} {0} ", party.AITargetParty);

                    double[] defaultBehaviorLocation = party.InitialCoordinates;
                    writer.Write("{0:F6} {1:F6} ", defaultBehaviorLocation[0], defaultBehaviorLocation[1]);
                    writer.Write("{0:F6} {1:F6} ", defaultBehaviorLocation[0], defaultBehaviorLocation[1]);
                    writer.Write("{0:F6} {1:F6} 0.0 ", party.InitialCoordinates[0], party.InitialCoordinates[1]);

                    string troopTag = "trp_";
                    writer.Write("{0} ", party.Members.Length);
                    foreach (PMember member in party.Members)
                    {
                        string troopS = member.Troop;
                        if (!troopS.StartsWith(troopTag))
                            troopS = troopTag + troopS;
                        int troopNo = CodeReader.Troops.IndexOf(troopS);
                        AddTagUse(tagUses, TagType.Troop, troopNo);
                        writer.Write("{0} {1} 0 {2} ", troopNo, member.MinimumTroops, member.Flags);
                    }

                    double bearing = (3.1415926 / 180d) * party.PartyDirectionInDegrees;
                    writer.WriteLine(Environment.NewLine + "{0:F6}", bearing);
                }
            }

            SaveTagUses(exportDir, tagUses);

            //Console.WriteLine(/*"Done"*/);
        }

        private static void ProcessGlobalVariablesUnused(string exportDir)
        {
            Console.WriteLine("Checking global variable usages...");
            List<string> variables = LoadVariables(exportDir, out List<int> variableUses);
            for (int i = 0; i < variables.Count; i++)
                if (variableUses[i] == 0)
                    Console.WriteLine("WARNING: Global variable never used: " + variables[i]);
            //Console.WriteLine(/*"Done"*/);
        }

        #endregion

        #region Helper Methods

        private static ulong GetOpcodeValue(string opcode)
        {
            if (allOpcodes == null)
                InitializeAllOpcodes();

            ulong opcodeValue = ulong.MinValue;
            opcode = opcode.Replace('\t', ' ').Replace(" ", string.Empty);
            string[] opcodeParts = opcode.Split('|');
            foreach (string opcodeX in opcodeParts)
            {
                for (int i = 0; i < allOpcodes.Count; i++)
                {
                    if (allOpcodes[i].Name.Equals(opcodeX))
                    {
                        opcodeValue |= (ulong)allOpcodes[i].Value;
                        i = allOpcodes.Count;
                    }
                }
            }
            return opcodeValue;
        }

        private static void InitializeAllOpcodes()
        {
            List<Variable> list = new List<Variable>();
            using (StreamReader reader = new StreamReader(ModuleSystem + "header_operations.py"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Split('#')[0];
                    line = line.Replace('\t', ' ').Replace(" ", string.Empty);
                    if (line.Contains("="))
                    {
                        string[] data = line.Split('=');
                        if (ulong.TryParse(data[1], out ulong val))
                            list.Add(new Variable(data[0], val));
                        else if (data[1].StartsWith("0x"))
                        {
                            data[1] = data[1].Substring(2);
                            if (data[1].Length <= 16)
                                list.Add(new Variable(data[0], HexConverter.Hex2Dec_16CHARS(data[1])));
                        }
                        else if (data[1].Contains("|"))
                        {
                            ulong ux = 0;
                            string[] sp = data[1].Split('|');
                            foreach (string s in sp)
                            {
                                bool found = false;
                                for (int i = 0; i < list.Count; i++)
                                {
                                    if (list[i].Name.Equals(s))
                                    {
                                        ux |= (ulong)list[i].Value;
                                        found = true;
                                        i = list.Count;
                                    }
                                }
                                if (!found)
                                    Console.WriteLine("ERROR: Operand '" + s + "' not found!");
                            }
                            list.Add(new Variable(data[0], ux));
                        }
                    }
                }
            }
            allOpcodes = list;
        }

        private static void SavePartyTemplateTroop(StreamWriter writer, PMember member)
        {
            if (member != null)
            {
                string troopTag = "trp_";
                string troopS = member.Troop;
                if (!troopS.StartsWith(troopTag))
                    troopS = troopTag + troopS;
                int troopNo = CodeReader.Troops.IndexOf(troopS);
                writer.Write("{0} ", troopNo);
                if (troopNo >= 0)
                {
                    //AddTagUse(tagUses, TagType.Troop, troopNo);
                    writer.Write("{0} {1} {2} ", member.MinimumTroops, member.MaximumTroops, member.Flags);
                }
            }
            else
                writer.Write("-1 ");
        }

        private static void SaveMissionTemplateGroup(StreamWriter writer, Entrypoint entryPointGroup, List<List<int>> tagUses)
        {
            int maxSpawnItems = 8;
            if (entryPointGroup.SpawnItems.Length > maxSpawnItems)
            {
                Console.WriteLine("ERROR: Too many item_overrides!");
                //error();
            }

            writer.Write("{0} {1} {2} {3} {4} {5}  ",
                entryPointGroup.EntryPointNo,
                entryPointGroup.SpawnFlags,
                entryPointGroup.AlterFlags,
                entryPointGroup.AIFlags,
                entryPointGroup.TroopCount,
                entryPointGroup.SpawnItemIDs.Length
            );

            foreach (int itemOverride in entryPointGroup.SpawnItemIDs)
            {
                AddTagUse(tagUses, TagType.Item, itemOverride);
                writer.Write("{0} ", itemOverride);
            }

            writer.WriteLine();
        }

        private static void SaveTriggers(
            StreamWriter writer,
            Trigger[] triggers,
            List<string> variableList,
            List<int> variableUses,
            List<List<int>> tagUses,
            List<string[]> quickStrings
            )
        {
            writer.WriteLine(triggers.Length);
            foreach (Trigger trigger in triggers)
            {
                writer.Write("{0:F6} {1:F6} {2:F6} ",
                    GetIntervalValue(trigger.CheckInterval),
                    GetIntervalValue(trigger.DelayInterval),
                    GetIntervalValue(trigger.ReArmInterval)
                );
                SaveStatementBlock(writer, 0, true, trigger.ConditionBlock, variableList, variableUses, tagUses, quickStrings);
                SaveStatementBlock(writer, 0, true, trigger.ConsequencesBlock, variableList, variableUses, tagUses, quickStrings);
                writer.WriteLine();
            }
            writer.WriteLine();
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
                writer.Write("{0:F6} ", GetIntervalValue(trigger.CheckInterval));
                SaveStatementBlock(writer, 0, true, trigger.ConsequencesBlock, variableList, variableUses, tagUses, quickStrings);
                writer.WriteLine();
            }
            writer.WriteLine();
        }

        private static double GetIntervalValue(string checkInterval)
        {
            if (allIntervalVars == null)
                InitializeAllIntervalVars();

            if (double.TryParse(checkInterval.Replace('.', ','), out double intervalValue))
                return intervalValue;

            intervalValue = 0d;
            for (int i = 0; i < allIntervalVars.Count; i++)
            {
                if (allIntervalVars[i].Name.Equals(checkInterval))
                {
                    intervalValue = (double)allIntervalVars[i].Value;
                    i = allIntervalVars.Count;
                }
            }
            return intervalValue;
        }

        private static void InitializeAllIntervalVars()
        {
            List<Variable> list = new List<Variable>();
            using (StreamReader sr = new StreamReader(ModuleSystem + "header_triggers.py"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine().Split('#')[0];

                    if (!s.Contains("=")) continue;

                    string[] sp = s.Replace(" ", string.Empty).Split('=');
                    if (sp.Length > 1)
                        if (sp[0].StartsWith("ti_"))
                            if (ImportantMethods.IsNumeric(sp[1], true))
                                list.Add(new Variable(sp[0], decimal.Parse(sp[1])));
                }
            }
            allIntervalVars = list;
        }

        private static string HandlePythonVariables(string param)
        {
            if (commonPythonVariables == null)
                InitializeCommonPythonVariables();

            string ret = param;
            for (int i = 0; i < commonPythonVariables.Count; i++)
            {
                if (commonPythonVariables[i].Name.Equals(param))
                {
                    ret = commonPythonVariables[i].Value.ToString();
                    i = commonPythonVariables.Count;
                }
            }
            return ret;
        }

        private static void InitializeCommonPythonVariables()
        {
            List<Variable> list = new List<Variable>();
            List<int> maskIndices = new List<int>();

            using (StreamReader sr = new StreamReader(ModuleSystem + "header_common.py"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine().Split('#')[0];

                    if (!s.Contains("=")) continue;

                    string[] sp = s.Replace(" ", string.Empty).Split('=');
                    if (sp.Length > 1)
                        if (ImportantMethods.IsNumericGZ(sp[1]))
                            list.Add(new Variable(sp[0], int.Parse(sp[1])));
                }
            }

            for (int i = 0; i < list.Count; i++)
                if (list[i].Name.EndsWith("mask"))
                    maskIndices.Add(i);
            maskIndices.Reverse();
            foreach (int i in maskIndices)
                list.RemoveAt(i);

            commonPythonVariables = list;
        }

        private static void SaveStatementBlock(StreamWriter writer, object statementName, bool canFailStatement, string[] statementBlock, List<string> variableList, List<int> variableUses, List<List<int>> tagUses, List<string[]> quickStrings)
        {
            List<string> localVarList = new List<string>();
            List<int> localVarsUses = new List<int>();

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

            int maxStatement = statementBlock.Length - 1;
            if (maxStatement >= 0)
            {
                if (statementBlock[maxStatement] != null)
                    maxStatement++;
            }
            else
                maxStatement++;

            writer.Write(" {0} ", maxStatement);

            for (int i = 0; i < maxStatement; i++)
            {
                List<string> codeParts = GenerateCodePartsFromSourceCode(statementBlock[i]);
                string opcode = codeParts[0];
                bool noVariables = (codeParts.Count == 1);

                if (tryOpcodes.Contains(opcode))
                    currentDepth++;
                else if (opcode.Equals("try_end"))
                    currentDepth--;
                else if (!noVariables)
                {
                    if (opcode.Equals("store_script_param_1") || (opcode.Equals("store_script_param") && codeParts[2].Equals("1")))
                        storeScriptParam1Uses++;
                    else if (opcode.Equals("store_script_param_2") || (opcode.Equals("store_script_param") && codeParts[2].Equals("2")))
                        storeScriptParam2Uses++;
                    if (!canFailStatement && currentDepth == 0 &&
                        (IsCanFailOperation(opcode) || (opcode.Equals("call_script") && codeParts[1].TrimStart()/*.Substring(7)*/.StartsWith("cf_"))) &&
                        !statementName.ToString().StartsWith("cf_"))
                        Console.WriteLine("WARNING: Script can fail at operation #" + i + ". Use cf_ at the beginning of its name: " + statementName);
                }

                SaveStatement(writer, opcode, noVariables, codeParts.ToArray(), ref variableList, ref variableUses, localVarList, localVarsUses, tagUses, quickStrings);
            }

            if (storeScriptParam1Uses > 1)
                Console.WriteLine("WARNING: store_script_param_1 is used more than once:" + statementName);
            if (storeScriptParam2Uses > 1)
                Console.WriteLine("WARNING: store_script_param_2 is used more than once:" + statementName);

            for (int i = 0; i < localVarList.Count; i++)
                if (localVarsUses[i] == 0 && !localVarList[i].StartsWith("unused"))//make output optional
                    Console.WriteLine("WARNING: Local variable never used: " + localVarList[i] + ", at: " + statementName);

            if (localVarList.Count > 128)
                Console.WriteLine("WARNING: Script uses more than 128 local wariables: " + statementName + " --> variables count:" + localVarList.Count);
        }

        private static void SaveStatement(StreamWriter writer, string opcode, bool noVariables, string[] statementSp, ref List<string> variableList, ref List<int> variableUses, List<string> localVarList, List<int> localVarsUses, List<List<int>> tagUses, List<string[]> quickStrings)
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
                            AddVariable(param.Substring(1), ref localVarList, ref localVarsUses);
                    }
                }
            }

            writer.Write("{0} {1} ", GetOpcodeValue(opcode), lenStatement);

            for (int i = 0; i < lenStatement; i++)
            {
                decimal operand = ProcessParam(statementSp[i + 1], variableList, variableUses, localVarList, localVarsUses, tagUses, quickStrings);
                writer.Write("{0} ", operand);
            }
        }

        private static decimal ProcessParam(string param, List<string> variableList, List<int> variableUses, List<string> localVarList, List<int> localVarsUses, List<List<int>> tagUses, List<string[]> quickStrings)
        {
            decimal result = 0;
            param = param.Trim();
            if (param.Length != 0)
            {
                if (!ImportantMethods.IsNumericGZ(param))
                {
                    if (param[0] == '$')
                    {
                        CheckVariableNotDefined(param.Substring(1), localVarList);
                        result = GetVariable(param, GlobalVarsList, GlobalVarsUses);
                        result = ((ulong)result) | OP_MASK_VARIABLE;
                    }
                    else if (param[0] == ':')
                    {
                        CheckVariableNotDefined(param.Substring(1), GlobalVarsList);
                        result = GetVariable(param, localVarList, localVarsUses);
                        result = ((ulong)result) | OP_MASK_LOCAL_VARIABLE;
                    }
                    else if (param[0] == '@')
                    {
                        result = InsertQuickStringWithAutoId(param.Substring(1), quickStrings);
                        result = ((ulong)result) | OP_MASK_QUICK_STRING;
                    }
                    else
                    {
                        result = GetIdentifierValue(param.ToLower(), tagUses);
                        if (result < 0)
                            Console.WriteLine("ERROR: Illegal Identifier:" + param);
                    }
                }
                else
                    result = decimal.Parse(param);
            }
            return result;
        }

        private static decimal GetIdentifierValue(string str, List<List<int>> tagUses)
        {
            decimal result = -1;
            int underscorePos = str.IndexOf('_');
            if (underscorePos > 0)
            {
                string tagStr = str.Remove(underscorePos);
                string idStr = str.Substring(underscorePos + 1);
                ulong idNo = (ulong)GetIdValue(tagStr, idStr, tagUses, out TagType tagType);
                if (tagType > 0 && tagType < TagType.End)
                {
                    if (idNo < 0)
                        Console.WriteLine("Error: Unable to find object: " + str);
                    else
                        result = idNo | ((ulong)tagType << OP_NUM_VALUE_BITS);
                }
                else
                    Console.WriteLine("Error: Unrecognized tag: '" + tagStr + "' in object: " + str);
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
                    idNo = FindObject(ObjectType.GameString, identifier);
                    tagType = TagType.String;
                    break;
                case "itm":
                    idNo = FindObject(ObjectType.Item, identifier);
                    tagType = TagType.Item;
                    break;
                case "trp":
                    idNo = FindObject(ObjectType.Troop, identifier);
                    tagType = TagType.Troop;
                    break;
                case "fac":
                    idNo = FindObject(ObjectType.Faction, identifier);
                    tagType = TagType.Faction;
                    break;
                case "qst":
                    idNo = FindObject(ObjectType.Quest, identifier);
                    tagType = TagType.Quest;
                    break;
                case "pt":
                    idNo = FindObject(ObjectType.PartyTemplate, identifier);
                    tagType = TagType.PartyTemplate;
                    break;
                case "p":
                    idNo = FindObject(ObjectType.Party, identifier);
                    tagType = TagType.Party;
                    break;
                case "scn":
                    idNo = FindObject(ObjectType.Scene, identifier);
                    tagType = TagType.Scene;
                    break;
                case "mt":
                    idNo = FindObject(ObjectType.MissionTemplate, identifier);
                    tagType = TagType.MissionTemplate;
                    break;
                case "mnu":
                    idNo = FindObject(ObjectType.GameMenu, identifier);
                    tagType = TagType.Menu;
                    break;
                case "script":
                    idNo = FindObject(ObjectType.Script, identifier);
                    tagType = TagType.Script;
                    break;
                case "psys":
                    idNo = FindObject(ObjectType.ParticleSystem, identifier);
                    tagType = TagType.ParticleSystem;
                    break;
                case "spr":
                    idNo = FindObject(ObjectType.SceneProp, identifier);
                    tagType = TagType.SceneProp;
                    break;
                case "prsnt":
                    idNo = FindObject(ObjectType.Presentation, identifier);
                    tagType = TagType.Presentation;
                    break;
                case "snd":
                    idNo = FindObject(ObjectType.Sound, identifier);
                    tagType = TagType.Sound;
                    break;
                case "icon":
                    idNo = FindObject(ObjectType.MapIcon, identifier);
                    tagType = TagType.MapIcon;
                    break;
                case "skl":
                    idNo = FindObject(ObjectType.Skill, identifier);
                    tagType = TagType.Skill;
                    break;
                case "track":
                    idNo = FindObject(ObjectType.Music, identifier);
                    tagType = TagType.Track;
                    break;
                case "mesh":
                    idNo = FindObject(ObjectType.Mesh, identifier);
                    tagType = TagType.Mesh;
                    break;
                case "anim":
                    idNo = FindObject(ObjectType.Animation, identifier);
                    tagType = TagType.Animation;
                    break;
                case "tab":
                case "tableau":
                    idNo = FindObject(ObjectType.TableauMaterial, identifier);
                    tagType = TagType.Tableau;
                    break;
                default:
                    break;
            }

            if (tagType != TagType.End && idNo > -1)
                AddTagUse(tagUses, tagType, idNo);

            return idNo;
        }

        private static int FindObject(ObjectType objectType, string id)
        {
            int result = -1;
            int type = (int)objectType;
            id = id.ToLower();
            for (int i = 0; i < types[type].Count; i++)
                if (types[type][i].ID.ToLower().Equals(id))
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
                    //Console.WriteLine("WARNING: Usage of unassigned global variable: " + varString);
                }
                //else
                //    Console.WriteLine("ERROR: Usage of unassigned local variable: " + varString);
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
            if (lhsOperations == null)
                lhsOperations = GetHeaderOperationsList("lhs_operations");
            return lhsOperations.Contains(opcode);
        }

        private static bool IsCanFailOperation(string opcode)
        {
            if (canFailOperations == null)
                canFailOperations = GetHeaderOperationsList("can_fail_operations");
            return canFailOperations.Contains(opcode);
        }

        private static List<string> GetHeaderOperationsList(string listName)
        {
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(ModuleSystem + "header_operations.py"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Split('#')[0];
                    line = line.Replace('\t', ' ').Replace(" ", string.Empty);
                    if (line.StartsWith(listName))
                    {
                        string curOpcode = line.Split('=')[1].TrimStart('[').TrimEnd(',').Trim();
                        if (curOpcode.Length != 0)
                            list.Add(curOpcode);
                        while (!reader.EndOfStream && !curOpcode.EndsWith("]"))
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
            writer.WriteLine("{0:F6} {1:F6}   {2:F6} {3:F6}", keys12[0], keys12[1], keys12[2], keys12[3]);
        }

        private static void SaveQuickStrings(string exportDir, List<string[]> quickStrings)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "quick_strings.txt"))
            {
                writer.WriteLine("{0}", quickStrings.Count);
                foreach (string[] quickString in quickStrings)
                    writer.WriteLine("{0} {1}", quickString[0], ReplaceSpaces(quickString[1]));
            }
        }

        private static void SaveTagUses(string exportDir, List<List<int>> tagUses)
        {
            using (StreamWriter writer = new StreamWriter(exportDir + "tag_uses.txt"))
            {
                for (int i = 0; i < tagUses.Count; i++)
                    for (int j = 0; j < tagUses[i].Count; j++)
                        writer.Write("{0} {1} {2};", i, j, tagUses[i][j]);//semicolon correct?
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
            string scnPrefix = scenes[0].Prefix;
            if (passage.StartsWith(scnPrefix))
                passage = passage.Substring(scnPrefix.Length);
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
                //do_error(); //?
            }

            writer.Write(" {0} ", sceneIdx);
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
                    string relID = factions[j].ID;
                    for (int k = 0; k < factions.Count; k++)
                        if (factions[k].ID.Equals(relID))
                            otherPos = k;
                    if (otherPos >= 0)
                    {
                        relations[otherPos][i] = rels[j];
                        relations[i][otherPos] = rels[j];
                    }
                    else
                        Console.WriteLine("ERROR faction not found: " + relID);
                }
            }

            return relations;

            /*
  relations = []
  for i in xrange(len(factions)):
    r = [0.0 for j in range(len(factions))]
    relations.append(r)
  for i_faction in xrange(len(factions)):
    relations[i_faction][i_faction] = factions[i_faction][faction_coherence_pos]
    rels = factions[i_faction][faction_relations_pos]
    for rel in rels:
      rel_name = rel[0]
      other_pos = -1
      for j_f in xrange(len(factions)):
        if factions[j_f][faction_name_pos] == rel_name:
          other_pos = j_f
      if other_pos == -1:
        print "ERROR faction not found: "+ rel_name
      else:
        relations[other_pos][i_faction] = rel[1]
        relations[i_faction][other_pos] = rel[1]
  return relations
            */
        }

        private static void WriteTextures(StreamWriter writer, string[] textures)
        {
            writer.Write(" {0} ", textures.Length);
            foreach (string texture in textures)
                writer.Write(" {0} ", texture);
            writer.WriteLine();
        }

        private static void WriteVoices(StreamWriter writer, Variable[] voices)
        {
            writer.Write(" {0} ", voices.Length);
            foreach (var voice in voices)
                writer.Write(" {0} {1} ", voice.Value, voice.Name);
            writer.WriteLine();
        }

        private static void WriteFaceTex(StreamWriter writer, FaceTexture[] faceTextures)
        {
            writer.Write(" {0} ", faceTextures.Length);
            foreach (FaceTexture texture in faceTextures)
            {
                writer.Write(" {0} {1} {2} {3} ", texture.Name, texture.Color, texture.HairMaterials.Length, texture.HairColors.Length);
                foreach (string material in texture.HairMaterials)
                    writer.Write(" {0} ", ReplaceSpaces(material));
                foreach (uint color in texture.HairColors)
                    writer.Write(" {0} ", color);
            }
            writer.WriteLine();
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

        private static void CompileGlobalVars(string[] statementBlock, ref List<string> variableList, ref List<int> variableUses)
        {
            if (statementBlock.Length == 0) return;

            foreach (string statement in statementBlock)
            {
                if (statement == null) continue;

                string tmp = statement.Trim();
                if (tmp.Length != 0)
                    CompileGlobalVarsInStatement(tmp, ref variableList, ref variableUses);
            }
        }

        public static bool IsGenericList(object o)
        {
            var oType = o.GetType();
            bool isList = (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
            return isList;
        }

        private static bool IsLhsOperationForGlobalVars(string opcode)
        {
            if (globalLhsOperations == null)
                globalLhsOperations = GetHeaderOperationsList("global_lhs_operations");
            return (IsLhsOperation(opcode) || globalLhsOperations.Contains(opcode));
        }

        private static void CompileGlobalVarsInStatement(string statement, ref List<string> variableList, ref List<int> variableUses)
        {
            List<string> codeParts = GenerateCodePartsFromSourceCode(statement);
            string opcode = codeParts[0];
            if (codeParts.Count > 1)
                if (IsLhsOperationForGlobalVars(opcode))
                    if (codeParts[1][0] == '$')
                        AddVariable(codeParts[1].Substring(1), ref variableList, ref variableUses);
        }

        private static List<string> GenerateCodePartsFromSourceCode(string statement)
        {
            string tmp = statement;
            int idxt = tmp.IndexOf('(');
            if (idxt >= 0)
                tmp = tmp.Substring(idxt + 1);
            idxt = tmp.LastIndexOf(')');
            if (idxt >= 0)
                tmp = tmp.Remove(idxt);

            List<string> codeParts = new List<string>();
            if (!tmp.Contains("@"))
            {
                string[] sp = tmp.Split(',');
                for (int j = 0; j < sp.Length; j++)
                {
                    sp[j] = sp[j].Trim(',', ' ', '\t', ')', '(', '\"');
                    sp[j] = HandlePythonVariables(sp[j]);
                    if (sp[j].StartsWith("reg"))
                    {
                        tmp = sp[j].Replace("reg", string.Empty);
                        if (ulong.TryParse(tmp, out ulong reg))
                        {
                            reg |= OP_MASK_REGISTER;
                            sp[j] = reg.ToString();
                        }
                    }
                    codeParts.Add(sp[j]);
                }
            }
            else
            {
                do
                {
                    string tmp2 = tmp.Remove(tmp.IndexOf('@'));
                    string tmp3 = tmp2.Trim(',', ' ', '\t', ')', '(', '\"');
                    string[] sp = tmp3.Split(',');
                    for (int j = 0; j < sp.Length; j++)
                    {
                        string xxxx = sp[j];
                        xxxx = xxxx.Trim(',', ' ', '\t', ')', '(', '\"');
                        xxxx = HandlePythonVariables(xxxx);
                        if (xxxx.StartsWith("reg"))
                        {
                            string tmp4 = xxxx.Replace("reg", string.Empty);
                            if (ulong.TryParse(tmp4, out ulong reg))
                            {
                                reg |= OP_MASK_REGISTER;
                                xxxx = reg.ToString();
                            }
                        }
                        codeParts.Add(xxxx);
                    }
                    tmp = tmp.Substring(tmp2.Length);

                    int xyt = tmp.IndexOf('\"');
                    if (xyt >= 0)
                    {
                        tmp2 = tmp.Remove(xyt);
                        codeParts.Add(tmp2);
                        tmp = tmp.Substring(tmp2.Length);
                    }
                    else
                        Console.WriteLine("Warning: Trailing '\"' not found!");
                }
                while (tmp.Contains("@"));
            }
            return codeParts;
        }

        private static void AddVariable(string variableString, ref List<string> variableList, ref List<int> variableUses)
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

        private static void CompileAllGlobalVars(ref List<string> variableList, ref List<int> variableUses)
        {
            foreach (string variable in ReservedVariables)
            {
                try
                {
                    AddVariable(variable, ref variableList, ref variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in variable: " + variable);
                }
            }

            foreach (Trigger trigger in types[(int)ObjectType.Trigger])
            {
                try
                {
                    CompileGlobalVars(trigger.ConditionBlock, ref variableList, ref variableUses);
                    CompileGlobalVars(trigger.ConsequencesBlock, ref variableList, ref variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in trigger: " + trigger.ID);//code?
                }
            }

            foreach (SceneProp sceneProp in types[(int)ObjectType.SceneProp])
            {
                try
                {
                    SimpleTrigger[] spTriggers = sceneProp.SimpleTriggers;
                    foreach (SimpleTrigger spTrigger in spTriggers)
                        CompileGlobalVars(spTrigger.ConsequencesBlock, ref variableList, ref variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in scene prop: " + sceneProp.ID);//code?
                }
            }

            foreach (Dialog dialog in types[(int)ObjectType.Dialog])
            {
                try
                {
                    CompileGlobalVars(dialog.ConditionBlock, ref variableList, ref variableUses);
                    CompileGlobalVars(dialog.ConsequenceBlock, ref variableList, ref variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in dialog line: " + dialog.ID);//code?
                }
            }

            foreach (GameMenu gameMenu in types[(int)ObjectType.GameMenu])
            {
                try
                {
                    CompileGlobalVars(gameMenu.OperationBlock, ref variableList, ref variableUses);
                    foreach (GameMenuOption menuOption in gameMenu.MenuOptions)
                    {
                        CompileGlobalVars(menuOption.ConditionBlock, ref variableList, ref variableUses);
                        CompileGlobalVars(menuOption.ConsequenceBlock, ref variableList, ref variableUses);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in game menu: " + gameMenu.ID);//code?
                }
            }

            foreach (MissionTemplate missionTemplate in types[(int)ObjectType.MissionTemplate])
            {
                try
                {
                    foreach (Trigger trigger in missionTemplate.Triggers)
                    {
                        CompileGlobalVars(trigger.ConditionBlock, ref variableList, ref variableUses);
                        CompileGlobalVars(trigger.ConsequencesBlock, ref variableList, ref variableUses);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in mission template: " + missionTemplate.ID);//code?
                }
            }
            
            foreach (Presentation presentation in types[(int)ObjectType.Presentation])
            {
                try
                {
                    foreach (SimpleTrigger trigger in presentation.SimpleTriggers)
                        CompileGlobalVars(trigger.ConsequencesBlock, ref variableList, ref variableUses);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in presentation: " + presentation.ID);//code?
                }
            }
            
            foreach (Script script in types[(int)ObjectType.Script])
            {
                try
                {
                    CompileGlobalVars(script.Code, ref variableList, ref variableUses);//last block is null??? fix for script code
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in script: " + script.ID);//code?
                }
            }

            foreach (SimpleTrigger simpleTrigger in types[(int)ObjectType.SimpleTrigger])
            {
                try
                {
                    CompileGlobalVars(simpleTrigger.ConsequencesBlock, ref variableList, ref variableUses);
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
