using importantLib;
using System;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects.Support
{
    public class SkillHunter
    {
        #region Consts And Properties

        public const string KNOWS = "knows_";
        public const string EMPTY = "nothing";
        public const string FilesPath = ".\\files\\";

        public static bool DebugMode { get; } = false;

        public int[] Skills { get; } = new int[48]; // was 41 before for 42 slots

        public static string[] Skillnames { get; } = new string[] { "persuasion", "reserved_4", "reserved_3", "reserved_2", "reserved_1", "prisoner_management", "leadership", "trade", "tactics", "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer", "horse_archery", "looting", "reserved_8", "reserved_7", "reserved_6", "reserved_5", "trainer", "tracking", "reserved_12", "reserved_11", "reserved_10", "reserved_9", "weapon_master", "shield", "athletics", "riding", "reserved_16", "reserved_15", "reserved_14", "ironflesh", "power_strike", "power_throw", "power_draw", "reserved_13", "reserved_17", "reserved_18", "reserved_19", "reserved_20", "reserved_21", "reserved_22", "reserved_23", "reserved_24" }; // Change known later in header files if needed to real skills instead of reserved_X - V5

        #endregion

        public SkillHunter()
        {
            InitialiseArrays();
        }

        private void InitialiseArrays()
        {
            for (int i = 0; i < Skillnames.Length; i++)
                if (!Skillnames[i].Substring(Skillnames[i].Length - 1).Equals("_"))
                    Skillnames[i] += '_';
            ResetSkillArray();
        }

        private void ResetSkillArray()
        {
            for (int i = 0; i < Skills.Length; i++)
                Skills[i] = -1;
        }

        public void ReadSkills(string skillLine)
        {
            ResetSkillArray();
            //StartUp(skillLine);//only 24 skills
            StartUpAll(skillLine);//all 48 Skills (are there more?)
        }

        private void StartUp(string skillLine)
        {
            throw new NotImplementedException();

            /*Dim HexString As String
            Dim tempArray As String() '274 131072 0 1 0 0
            tempArray = Split(skill_line)

            'read first set of values (if value is A then set it to 10)
            'HexString = Right$("0000000" & Hex$(TempArray(0)), 8)
            HexString = Dec2Hex(tempArray(0))
            Skills(0) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'persuasion
            Skills(1) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'prisoner_management
            Skills(2) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'leadership
            Skills(3) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'trade

            'read second set of values
            'Hex$ method breaks on Stannis Bartheon (large number in TempArray(1))
            'HexString = Right$("0000000" & Hex$(TempArray(1)), 8)
            HexString = Dec2Hex(tempArray(1))
            Skills(4) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'tactics
            Skills(5) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'pathfinding
            Skills(6) = Int(ReplaceHex(Mid(HexString, 3, 1))) 'spotting
            Skills(7) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'inventory_management
            Skills(8) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'wound_treatment
            Skills(9) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'surgery
            Skills(10) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'first_aid
            Skills(11) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'engineer

            'read third set of values
            'HexString = Right$("0000000" & Hex$(TempArray(2)), 8)
            HexString = Dec2Hex(tempArray(2))
            Skills(12) = Int(ReplaceHex(Mid(HexString, 1, 1))) 'horse_archery
            Skills(13) = Int(ReplaceHex(Mid(HexString, 2, 1))) 'looting
            Skills(14) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'trainer
            Skills(15) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'tracking

            'read fourth set of values
            'HexString = Right$("0000000" & Hex$(TempArray(3)), 8)
            HexString = Dec2Hex(tempArray(3))
            Skills(16) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'weapon_master
            Skills(17) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'shield
            Skills(18) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'athletics
            Skills(19) = Int(ReplaceHex(Mid(HexString, 8, 1))) 'riding

            'read fifth set of values
            'HexString = Right$("0000000" & Hex$(TempArray(4)), 8)
            HexString = Dec2Hex(tempArray(4))
            Skills(20) = Int(ReplaceHex(Mid(HexString, 4, 1))) 'ironflesh
            Skills(21) = Int(ReplaceHex(Mid(HexString, 5, 1))) 'power_strike
            Skills(22) = Int(ReplaceHex(Mid(HexString, 6, 1))) 'power_throw
            Skills(23) = Int(ReplaceHex(Mid(HexString, 7, 1))) 'power_draw
            */
        }

        private void StartUpAll(string skillLine)
        {
            string hexString;
            string[] tmpArray = skillLine.Split();// example: 274 131072 0 1 0 0

            //read first set of values (if value is A then set it to 10)
            //hexString = Right$("0000000" & Hex$(tmpArray(0)), 8)
            hexString = HexConverter.Dec2Hex(tmpArray[0]);
            Skills[0] = int.Parse(ReplaceHex(hexString[0])); //persuasion | - - - X - - -
            Skills[1] = int.Parse(ReplaceHex(hexString[1])); //reserved_IV | - - - X - - -
            Skills[2] = int.Parse(ReplaceHex(hexString[2])); //reserved_III | - - - X - - -
            Skills[3] = int.Parse(ReplaceHex(hexString[3])); //reserved_II | - - - X - - -
            Skills[4] = int.Parse(ReplaceHex(hexString[4])); //reserved_I | - - - X - - -
            Skills[5] = int.Parse(ReplaceHex(hexString[5])); //prisoner_management | - - - X - - -
            Skills[6] = int.Parse(ReplaceHex(hexString[6])); //leadership | - - - X - - -
            Skills[7] = int.Parse(ReplaceHex(hexString[7])); //trade | - - - X - - -

            //read second set of values
            //Hex$ method breaks on Stannis Bartheon (large number in tmpArray(1))
            //hexString = Right$("0000000" & Hex$(tmpArray(1)), 8)
            hexString = HexConverter.Dec2Hex(tmpArray[1]);
            Skills[8] = int.Parse(ReplaceHex(hexString[0])); //tactics | - - - X - - -
            Skills[9] = int.Parse(ReplaceHex(hexString[1])); //pathfinding | - - - X - - -
            Skills[10] = int.Parse(ReplaceHex(hexString[2])); //spotting | - - - X - - -
            Skills[11] = int.Parse(ReplaceHex(hexString[3])); //inventory_management | - - - X - - -
            Skills[12] = int.Parse(ReplaceHex(hexString[4])); //wound_treatment | - - - X - - -
            Skills[13] = int.Parse(ReplaceHex(hexString[5])); //surgery | - - - X - - -
            Skills[14] = int.Parse(ReplaceHex(hexString[6])); //first_aid | - - - X - - -
            Skills[15] = int.Parse(ReplaceHex(hexString[7])); //engineer | - - - X - - -

            //read third set of values
            //hexString = Right$("0000000" & Hex$(tmpArray(2)), 8)
            hexString = HexConverter.Dec2Hex(tmpArray[2]);
            Skills[16] = int.Parse(ReplaceHex(hexString[0])); //horse_archery | - - - X - - -
            Skills[17] = int.Parse(ReplaceHex(hexString[1])); //looting | - - - X - - -
            Skills[18] = int.Parse(ReplaceHex(hexString[2])); //reserved_VIII | - - - X - - -
            Skills[19] = int.Parse(ReplaceHex(hexString[3])); //reserved_VII | - - - X - - -
            Skills[20] = int.Parse(ReplaceHex(hexString[4])); //reserved_VI | - - - X - - -
            Skills[21] = int.Parse(ReplaceHex(hexString[5])); //reserved_V | - - - X - - -
            Skills[22] = int.Parse(ReplaceHex(hexString[6])); //trainer | - - - X - - -
            Skills[23] = int.Parse(ReplaceHex(hexString[7])); //tracking | - - - X - - -

            //read fourth set of values
            //hexString = Right$("0000000" & Hex$(tmpArray(3)), 8)
            hexString = HexConverter.Dec2Hex(tmpArray[3]);
            Skills[24] = int.Parse(ReplaceHex(hexString[0])); //reserved_XII
            Skills[25] = int.Parse(ReplaceHex(hexString[1])); //reserved_XI
            Skills[26] = int.Parse(ReplaceHex(hexString[2])); //reserved_X
            Skills[27] = int.Parse(ReplaceHex(hexString[3])); //reserved_IV | - - - X - - -
            Skills[28] = int.Parse(ReplaceHex(hexString[4])); //weapon_master | - - - X - - -
            Skills[29] = int.Parse(ReplaceHex(hexString[5])); //shield | - - - X - - -
            Skills[30] = int.Parse(ReplaceHex(hexString[6])); //athletics | - - - X - - -
            Skills[31] = int.Parse(ReplaceHex(hexString[7])); //riding | - - - X - - -

            //read fifth set of values
            //hexString = Right$("0000000" & Hex$(tmpArray(4)), 8)
            hexString = HexConverter.Dec2Hex(tmpArray[4]);
            Skills[32] = int.Parse(ReplaceHex(hexString[0])); //reserved_XVI ?
            Skills[33] = int.Parse(ReplaceHex(hexString[1])); //reserved_XV | - - - X - - -
            Skills[34] = int.Parse(ReplaceHex(hexString[2])); //reserved_XIV | - - - X - - -
            Skills[35] = int.Parse(ReplaceHex(hexString[3])); //ironflesh | - - - X - - -
            Skills[36] = int.Parse(ReplaceHex(hexString[4])); //power_strike | - - - X - - -
            Skills[37] = int.Parse(ReplaceHex(hexString[5])); //power_throw | - - - X - - -
            Skills[38] = int.Parse(ReplaceHex(hexString[6])); //power_draw | - - - X - - -
            Skills[39] = int.Parse(ReplaceHex(hexString[7])); //reserved_XIII

            //read sixth set of values
            //hexString = Right$("0000000" & Hex$(tmpArray(5)), 8)
            hexString = HexConverter.Dec2Hex(tmpArray[5]);
            Skills[40] = int.Parse(ReplaceHex(hexString[0])); //reserved_XVII
            Skills[41] = int.Parse(ReplaceHex(hexString[1])); //reserved_XVIII

            if (hexString.Length <= 8) return;

            Skills[42] = int.Parse(ReplaceHex(hexString[2])); //reserved_XIX ???
            Skills[43] = int.Parse(ReplaceHex(hexString[3])); //reserved_XX ???
            Skills[44] = int.Parse(ReplaceHex(hexString[4])); //reserved_XXI ???
            Skills[45] = int.Parse(ReplaceHex(hexString[5])); //reserved_XXII ???
            Skills[46] = int.Parse(ReplaceHex(hexString[6])); //reserved_XXIII ???
            Skills[47] = int.Parse(ReplaceHex(hexString[7])); //reserved_XXIV ???
        }

        private static string ReplaceHex(char hexChar)
        {
            string retur;
            switch (hexChar)
            {
                case 'A':
                    retur = "10";
                    break;
                case 'B':
                    retur = "11";
                    break;
                case 'C':
                    retur = "12";
                    break;
                case 'D':
                    retur = "13";
                    break;
                case 'E':
                    retur = "14";
                    break;
                case 'F':
                    retur = "15";
                    break;
                default:
                    retur = string.Empty;
                    break;
            }
            return retur;
        }

        private static string ReplaceHex(string hextex)
        {
            hextex = hextex.Replace("A", "10");
            hextex = hextex.Replace("B", "11");
            hextex = hextex.Replace("C", "12");
            hextex = hextex.Replace("D", "13");
            hextex = hextex.Replace("E", "14");
            hextex = hextex.Replace("F", "15");
            return hextex;
        }

        public static string[] RemoveItemDoublesFromArray(string[] array)
        {
            List<string> retList = new List<string>();
            foreach (string s in array)
                if (retList.Contains(s))
                    retList.Add(s);
            return retList.ToArray();
        }

    }
}
