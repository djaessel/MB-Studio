using importantLib;
using MB_Decompiler_Library.Objects.Support;
using System;
using System.Collections.Generic;

namespace MB_Decompiler_Library.IO
{
    public class SourceReader
    {
        private static LocalVariableInterpreter localVariableInterpreter;

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

    }
}
