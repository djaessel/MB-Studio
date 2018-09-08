using importantLib;
using System.Collections.Generic;

namespace MB_Studio_Library.Objects.Support
{
    public class SkillHunter
    {
        #region Consts And Properties

        public const string KNOWS = "knows_";
        public const string EMPTY = "nothing";
        public const string FilesPath = ".\\files\\";

        public static bool DebugMode { get; } = false;

        public int[] Skills { get; } = new int[48]; // had 42 slots before

        public static string[] Skillnames { get; } = new string[] { "persuasion", "reserved_4", "reserved_3", "reserved_2", "reserved_1", "prisoner_management", "leadership", "trade",
                                                                    "tactics", "pathfinding", "spotting", "inventory_management", "wound_treatment", "surgery", "first_aid", "engineer",
                                                                    "horse_archery", "looting", "reserved_8", "reserved_7", "reserved_6", "reserved_5", "trainer", "tracking",
                                                                    "reserved_12", "reserved_11", "reserved_10", "reserved_9", "weapon_master", "shield", "athletics", "riding",
                                                                    "reserved_16", "reserved_15", "reserved_14", "ironflesh", "power_strike", "power_throw", "power_draw", "reserved_13",
                                                                    "reserved_17", "reserved_18", "reserved_19", "reserved_20", "reserved_21", "reserved_22", "reserved_23", "reserved_24" };/*
                                                                    Change known later in header files if needed to real skills instead of reserved_X - V5
                                                                    */

        #endregion

        #region Init

        public SkillHunter()
        {
            InitialiseArrays();
        }

        public void ReadSkills(string skillLine)
        {
            ResetSkillArray();
            skillLine = skillLine.Trim();
            //StartUpDefault(skillLine.Split());//only 24 skills
            StartUpAll(skillLine.Split());//all skills (48 - are there more?)
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
                Skills[i] = 0;//-1;
        }

        // Only the default 24 Skills
        private void StartUpDefault(string[] tempArray)// example: 274 131072 0 1 0 0
        {
            //read first set of values (if value is A then set it to 10)
            string hexString = HexConverter.Dec2Hex(tempArray[0]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out int[] hexValues);
            Skills[0] = hexValues[0];   //persuasion
            Skills[1] = hexValues[5];   //prisoner_management
            Skills[2] = hexValues[6];   //leadership
            Skills[3] = hexValues[7];   //trade

            //read second set of values
            hexString = HexConverter.Dec2Hex(tempArray[1]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[4] = hexValues[0];   //tactics
            Skills[5] = hexValues[1];   //pathfinding
            Skills[6] = hexValues[2];   //spotting
            Skills[7] = hexValues[3];   //inventory_management
            Skills[8] = hexValues[4];   //wound_treatment
            Skills[9] = hexValues[5];   //surgery
            Skills[10] = hexValues[6];  //first_aid
            Skills[11] = hexValues[7];  //engineer

            //read third set of values
            hexString = HexConverter.Dec2Hex(tempArray[2]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[12] = hexValues[0];  //horse_archery
            Skills[13] = hexValues[1];  //looting
            Skills[14] = hexValues[6];  //trainer
            Skills[15] = hexValues[7];  //tracking

            //read fourth set of values
            hexString = HexConverter.Dec2Hex(tempArray[3]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[16] = hexValues[4];  //weapon_master
            Skills[17] = hexValues[5];  //shield
            Skills[18] = hexValues[6];  //athletics
            Skills[19] = hexValues[7];  //riding

            //read fifth set of values
            hexString = HexConverter.Dec2Hex(tempArray[4]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);            
            Skills[20] = hexValues[3];  //ironflesh
            Skills[21] = hexValues[4];  //power_strike
            Skills[22] = hexValues[5];  //power_throw
            Skills[23] = hexValues[6];  //power_draw
        }

        // All known 48 skills (maybe more available)
        private void StartUpAll(string[] tmpArray)// example: 274 131072 0 1 0 0
        {
            //read first set of values (if value is A then set it to 10)
            string hexString = HexConverter.Dec2Hex(tmpArray[0]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out int[] hexValues);
            Skills[0] = hexValues[0];   //persuasion | - - - X - - -
            Skills[1] = hexValues[1];   //reserved_IV | - - - X - - -
            Skills[2] = hexValues[2];   //reserved_III | - - - X - - -
            Skills[3] = hexValues[3];   //reserved_II | - - - X - - -
            Skills[4] = hexValues[4];   //reserved_I | - - - X - - -
            Skills[5] = hexValues[5];   //prisoner_management | - - - X - - -
            Skills[6] = hexValues[6];   //leadership | - - - X - - -
            Skills[7] = hexValues[7];   //trade | - - - X - - -

            //read second set of values
            hexString = HexConverter.Dec2Hex(tmpArray[1]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[8] = hexValues[0];   //tactics | - - - X - - -
            Skills[9] = hexValues[1];   //pathfinding | - - - X - - -
            Skills[10] = hexValues[2];  //spotting | - - - X - - -
            Skills[11] = hexValues[3];  //inventory_management | - - - X - - -
            Skills[12] = hexValues[4];  //wound_treatment | - - - X - - -
            Skills[13] = hexValues[5];  //surgery | - - - X - - -
            Skills[14] = hexValues[6];  //first_aid | - - - X - - -
            Skills[15] = hexValues[7];  //engineer | - - - X - - -

            //read third set of values
            hexString = HexConverter.Dec2Hex(tmpArray[2]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[16] = hexValues[0];  //horse_archery | - - - X - - -
            Skills[17] = hexValues[1];  //looting | - - - X - - -
            Skills[18] = hexValues[2];  //reserved_VIII | - - - X - - -
            Skills[19] = hexValues[3];  //reserved_VII | - - - X - - -
            Skills[20] = hexValues[4];  //reserved_VI | - - - X - - -
            Skills[21] = hexValues[5];  //reserved_V | - - - X - - -
            Skills[22] = hexValues[6];  //trainer | - - - X - - -
            Skills[23] = hexValues[7];  //tracking | - - - X - - -

            //read fourth set of values
            hexString = HexConverter.Dec2Hex(tmpArray[3]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[24] = hexValues[0];  //reserved_XII
            Skills[25] = hexValues[1];  //reserved_XI
            Skills[26] = hexValues[2];  //reserved_X
            Skills[27] = hexValues[3];  //reserved_IV | - - - X - - -
            Skills[28] = hexValues[4];  //weapon_master | - - - X - - -
            Skills[29] = hexValues[5];  //shield | - - - X - - -
            Skills[30] = hexValues[6];  //athletics | - - - X - - -
            Skills[31] = hexValues[7];  //riding | - - - X - - -

            //read fifth set of values
            hexString = HexConverter.Dec2Hex(tmpArray[4]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[32] = hexValues[0];  //reserved_XVI ?
            Skills[33] = hexValues[1];  //reserved_XV | - - - X - - -
            Skills[34] = hexValues[2];  //reserved_XIV | - - - X - - -
            Skills[35] = hexValues[3];  //ironflesh | - - - X - - -
            Skills[36] = hexValues[4];  //power_strike | - - - X - - -
            Skills[37] = hexValues[5];  //power_throw | - - - X - - -
            Skills[38] = hexValues[6];  //power_draw | - - - X - - -
            Skills[39] = hexValues[7];  //reserved_XIII

            //read sixth set of values
            hexString = HexConverter.Dec2Hex(tmpArray[5]);
            HexConverter.ConvertSingleHexCodeToIntArray(hexString, out hexValues);
            Skills[40] = hexValues[0];  //reserved_XVII
            Skills[41] = hexValues[1];  //reserved_XVIII

            if (hexString.Length <= 2) return;

            Skills[42] = hexValues[2];  //reserved_XIX ???
            Skills[43] = hexValues[3];  //reserved_XX ???
            Skills[44] = hexValues[4];  //reserved_XXI ???
            Skills[45] = hexValues[5];  //reserved_XXII ???
            Skills[46] = hexValues[6];  //reserved_XXIII ???
            Skills[47] = hexValues[7];  //reserved_XXIV ???
        }

        #endregion

        #region Useful Methods

        public static void RemoveItemDuplicatesFromArray(ref string[] array)
        {
            List<string> retList = new List<string>();
            foreach (string s in array)
                if (!retList.Contains(s))
                    retList.Add(s);
            array = retList.ToArray();
        }

        public static void RemoveItemDuplicatesFromList(ref List<string> list)
        {
            List<string> retList = new List<string>();
            foreach (string s in list)
                if (!retList.Contains(s))
                    retList.Add(s);
            list = retList;
        }

        #endregion
    }
}
