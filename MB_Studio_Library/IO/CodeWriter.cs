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

        private static List<object> lhsOperations = new List<object>();
        private static List<object> globalLhsOperations = new List<object>();

        // GET REAL MODULE VARIABLES IF NEEDED !!!
        private static List<string> reservedVariables = new List<string>();

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
            /*using (StreamReader sr = new StreamReader(ModuleSystem + "build_module.bat.list"))
            {
                while (!sr.EndOfStream)
                {
                    string[] parameters = sr.ReadLine().Trim().Split();
                    parameters[0] = parameters[0].Replace(".\\", ModuleSystem);
                    parameters[1] = ModuleSystem + parameters[1];

                    //ImportantMethods.ExecuteCommandSync(parameters[0], parameters[1]);
                }
            }*/

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

            //if (moduleFiles.Length <= DEFAULT_FILES)//MAKE OPTION FOR REWRITE LATER
            //{
            SourceWriter.WriteAllObjects();
            moduleFiles = Directory.GetFiles(moduleFilesPath);
            //}

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

            // SET NULL TO LIST LATER !!!
            CompileAllGlobalVars(variables, variablesUses, null, null, null, null, null, null, null, null);

            SaveVariables(exportDir, variables, variablesUses);
        }

        private static void ProcessStrings(string exportDir)
        {
            Console.WriteLine("Exporting strings...");

            // LOAD GAME STRINGS HERE
            List<GameString> gameStrings = new List<GameString>();

            // save python header
            using (StreamWriter writer = new StreamWriter(".\\ID_strings.py"))
            {
                for (int i = 0; i < gameStrings.Count; i++)
                    writer.WriteLine("str_" + ConvertToIdentifier(gameStrings[i].ID) + " = " + i);
                writer.WriteLine(Environment.NewLine);
            }

            // save game string
            using (StreamWriter writer = new StreamWriter(exportDir + "string.txt"))
            {
                writer.WriteLine("stringsfile version 1");//change version if needed
                writer.WriteLine(gameStrings.Count);
                for (int i = 0; i < gameStrings.Count; i++)
                    writer.WriteLine("str_%s %s", ConvertToIdentifier(gameStrings[0].ID), ReplaceSpaces(gameStrings[0].Text));
            }
        }

        private static void ProcessSkills(string exportDir)
        {
            Console.WriteLine("Exporting skills...");

            // LOAD SKILLS HERE
            List<Skill> skills = new List<Skill>();

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

            // LOAD MUSIC HERE
            List<Music> tracks = new List<Music>();

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

            // LOAD ANIMATIONS HRE
            List<Animation> animations = new List<Animation>();
            List<int> animationIndices = new List<int>();

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
                    writer.Write(" %s %d %d ", action.ID, action.FlagsGZ, action.MasterFlagsGZ); //print flags
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
            /*
def write_actions(action_set,num_action_codes,action_codes,file_name):
  file = open(export_dir + file_name,"w")
  file.write("%d\n"%num_action_codes)
  for i_action_code in xrange(num_action_codes):
    action_found = 0
    for action in action_set:
      if action[0] == i_action_code:
        file.write(" %s %d %d "%(action_codes[i_action_code],action[1], action[2])) #print flags
        file.write(" %d\n"%(len(action)-3))
        for elem in action[3:]:
          file.write("  %f %s %d %d %d "%(elem[0],elem[1],elem[2],elem[3],elem[4]))
          if (len(elem) > 5):
            file.write("%d "%elem[5])
          else:
            file.write("0 ")
          if (len(elem) > 6):
            file.write("%f %f %f  "%elem[6])
          else:
            file.write("0.0 0.0 0.0 ")
          if (len(elem) > 7):
            file.write("%f \n"%(elem[7]))
          else:
            file.write("0.0 \n")
        action_found = 1
        break
    if not action_found:
      file.write(" none 0 0\n") #oops
            */
        }

        private static void ProcessMeshes(string exportDir)
        {
            Console.WriteLine("Exporting meshes...");

            // LOAD MESHES HERE
            List<Mesh> meshes = new List<Mesh>();

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

            // LOAD SOUNDS HERE
            List<Sound> sounds = new List<Sound>();
            List<List<object[]>> soundsArray = new List<List<object[]>>();

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
                    writer.Write("snd_%s %d %d ", sounds[i].ID, sounds[i].FlagsGZ, sounds[i].SoundFiles.Length);
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

            // LOAD SKINS HERE
            List<Skin> skins = new List<Skin>();

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

            // LOAD FACTIONS HERE
            List<Faction> factions = new List<Faction>();

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
                    writer.WriteLine("fac_%s %s %d %d ",
                        ConvertToIdentifier(factions[i].ID),
                        ReplaceSpaces(factions[i].Name),
                        factions[i].FlagsGZ,
                        HexConverter.Hex2Dec(factions[i].ColorCode.Replace("0x", string.Empty))
                    );

                    foreach (double relation in relations[i])
                        writer.Write(" %f ", relation);
                    writer.WriteLine();

                    writer.Write("%d ", factions[i].Ranks.Length);
                    foreach (string rank in factions[i].Ranks)
                        writer.Write(" %s ", ReplaceSpaces(rank));
                }
            }
        }

        #endregion

        #region Helper Methods

        private static List<double[]> CompileRelations(List<Faction> factions)
        {
            List<double[]> relations = new List<double[]>();
            for (int i = 0; i < factions.Count; i++)
                relations.Add(new double[factions.Count]);//r = [0.0 for j in range(len(factions))]; relations.append(r);

            for (int i = 0; i < factions.Count; i++)
            {
                relations[i][i] = factions[i].FactionCoherence;
                double[] rels = factions[i].Relations;
                for (int j = 0; j < rels.Length; j++)
                {
                    int otherPos = -1;
                    string relName = factions[i].Ranks[j];
                    for (int k = 0; k < factions.Count; k++)
                        if (factions[k].Name.Equals(relName))
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
            idText = idText.Replace(' ', '_');
            idText = idText.Replace("'", "_");
            idText = idText.Replace('`', '_');
            idText = idText.Replace('(', '_');
            idText = idText.Replace(')', '_');
            idText = idText.Replace('-', '_');
            idText = idText.Replace(',', '_');
            idText = idText.Replace('|', '_');
            idText = idText.Replace('\t', '_');// Tab
            idText = idText.ToLower();
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

        private static bool IsLhsOperationForGlobalVars(object opcode)
        {
            return lhsOperations.Contains(opcode) ||
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
                if (IsLhsOperationForGlobalVars(opcode))
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

        private static void CompileAllGlobalVars(
            List<string> variableList,
            List<int> variableUses,
            List<Trigger> triggers,
            List<Dialog> senctences,
            List<GameMenu> gameMenus,
            List<MissionTemplate> missionTemplates,
            List<SceneProp> sceneProps,
            List<Presentation> presentations,
            List<Script> scripts,
            List<SimpleTrigger> simpleTriggers
        ) {
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

            foreach (Dialog dialog in senctences)//code?
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

            foreach (GameMenu gameMenu in gameMenus)
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
