using importantLib;
using MB_Decompiler_Library.Objects.Support;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MB_Decompiler_Library.Objects
{
    public class Troop : Skriptum
    {
        #region Consts And Attributes

        public const int GUARANTEE_ALL = 0x07F00000;//133169152‬

        private const string ERROR_MSG_1 = "You probably have too many lines found in troop init! Check your files!";
        private const string ERROR_MSG_2 = "Too few lines found in troop init! Check your files!";

        private static List<HeaderVariable> headerFlags = null;
        private static SkillHunter sk = new SkillHunter();

        #endregion

        #region Properties

        #region General

        public static bool ShortProficies { get; private set; } = false;

        public string Name { get; private set; }
        public string PluralName { get; private set; }

        public string Flags { get; private set; }
        public int FlagsGZ { get; private set; }

        public string DialogImage { get; private set; }
        //public int DialogImageGZ { get; private set; }

        public string SceneCode { get; private set; }
        public ulong SceneCodeGZ { get; private set; }

        public string Reserved { get; private set; }
        public int ReservedGZ { get; private set; }

        public int FactionID { get; set; }

        public List<int> Items { get; private set; } = new List<int>();
        public List<ulong> ItemFlags { get; private set; } = new List<ulong>();

        #endregion

        #region UpgradeTroop

        public int UpgradeTroop1 { get; set; }

        public int UpgradeTroop2 { get; set; }

        public string UpgradeTroop1ErrorCode { get; private set; }

        public string UpgradeTroop2ErrorCode { get; private set; }

        #endregion

        #region Faces

        public string Face1 { get; private set; }

        public string Face2 { get; private set; }

        #endregion

        #region Skills

        public int[] Skills { get; private set; } = new int[42];

        public int Persuasion
        {
            get { return Skills[0]; }
            set { Skills[0] = value; }
        }

        public int PrisonerManagement
        {
            get { return Skills[1]; }
            set { Skills[1] = value; }
        }

        public int Leadership
        {
            get { return Skills[2]; }
            set { Skills[2] = value; }
        }

        public int Trade
        {
            get { return Skills[3]; }
            set { Skills[3] = value; }
        }

        public int Tactics
        {
            get { return Skills[4]; }
            set { Skills[4] = value; }
        }

        public int Pathfinding
        {
            get { return Skills[5]; }
            set { Skills[5] = value; }
        }

        public int Spotting
        {
            get { return Skills[6]; }
            set { Skills[6] = value; }
        }

        public int InventoryManagement
        {
            get { return Skills[7]; }
            set { Skills[7] = value; }
        }

        public int WoundTreatment
        {
            get { return Skills[8]; }
            set { Skills[8] = value; }
        }

        public int Surgery
        {
            get { return Skills[9]; }
            set { Skills[9] = value; }
        }

        public int FirstAid
        {
            get { return Skills[10]; }
            set { Skills[10] = value; }
        }

        public int Engineer
        {
            get { return Skills[11]; }
            set { Skills[11] = value; }
        }

        public int HorseArchery
        {
            get { return Skills[12]; }
            set { Skills[12] = value; }
        }

        public int Looting
        {
            get { return Skills[13]; }
            set { Skills[13] = value; }
        }

        public int Training
        {
            get { return Skills[14]; }
            set { Skills[14] = value; }
        }

        public int Tracking
        {
            get { return Skills[15]; }
            set { Skills[15] = value; }
        }

        public int WeaponMaster
        {
            get { return Skills[16]; }
            set { Skills[16] = value; }
        }

        public int Shield
        {
            get { return Skills[17]; }
            set { Skills[17] = value; }
        }

        public int Athletics
        {
            get { return Skills[18]; }
            set { Skills[18] = value; }
        }

        public int Riding
        {
            get { return Skills[19]; }
            set { Skills[19] = value; }
        }

        public int Ironflesh
        {
            get { return Skills[20]; }
            set { Skills[20] = value; }
        }

        public int PowerStrike
        {
            get { return Skills[21]; }
            set { Skills[21] = value; }
        }

        public int PowerThrow
        {
            get { return Skills[22]; }
            set { Skills[22] = value; }
        }

        public int PowerDraw
        {
            get { return Skills[23]; }
            set { Skills[23] = value; }
        }

        #endregion

        #region Attributes

        public int[] Attributes { get; private set; } = new int[5];

        public int Strength
        {
            get { return Attributes[0]; }
            set { Attributes[0] = value; }
        }

        public int Agility
        {
            get { return Attributes[1]; }
            set { Attributes[1] = value; }
        }

        public int Intelligence
        {
            get { return Attributes[2]; }
            set { Attributes[2] = value; }
        }

        public int Charisma
        {
            get { return Attributes[3]; }
            set { Attributes[3] = value; }
        }

        public int Level
        {
            get { return Attributes[4]; }
            set { Attributes[4] = value; }
        }

        #endregion

        #region Proficiencies

        public string ProficienciesSC { get; private set; }

        public int[] Proficiencies { get; private set; } = new int[7];

        public int OneHanded
        {
            get { return Proficiencies[0]; }
            set { Proficiencies[0] = value; }
        }

        public int TwoHanded
        {
            get { return Proficiencies[1]; }
            set { Proficiencies[1] = value; }
        }

        public int Polearm
        {
            get { return Proficiencies[2]; }
            set { Proficiencies[2] = value; }
        }

        public int Archery
        {
            get { return Proficiencies[3]; }
            set { Proficiencies[3] = value; }
        }

        public int Crossbow
        {
            get { return Proficiencies[4]; }
            set { Proficiencies[4] = value; }
        }

        public int Throwing
        {
            get { return Proficiencies[5]; }
            set { Proficiencies[5] = value; }
        }

        public int Firearm
        {
            get { return Proficiencies[6]; }
            set { Proficiencies[6] = value; }
        }

        #endregion

        #endregion

        #region Initializing

        public Troop(string[] values) : base(values[0].TrimStart().Split()[0], ObjectType.Troop)
        {
            Init(values);
        }

        private void Init(string[] values)
        {
            Reset();
            if (values.Length > 5 && values.Length < 8)
            {
                SetFirstLine(values[0]);
                SetItems(values[1]);
                SetAttributes(values[2]);
                SetProficiencies(values[3]);
                SetSkills(values[4]);
                SetFaceCodes(values[5]);
                return;
            }
            SendErrorMessage();
        }

        private void InitializeHeaderFlags(string file = "header_troops.py", List<HeaderVariable> itemPointsX = null)
        {
            List<HeaderVariable> itemPoints = new List<HeaderVariable>();
            string file2 = "header_mb_decompiler.py";

            itemPoints = itemPointsX ?? new List<HeaderVariable>();

            using (StreamReader sr = new StreamReader(SkillHunter.FilesPath + file))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine().Split('#')[0];
                    if (s.Split('_')[0].Equals("tf"))
                    {
                        string[] sp = s.Replace(" ", string.Empty).Replace("\t", string.Empty).Split('=');
                        bool isHexValue = sp[1].Contains("0x");
                        bool isNumeric = ImportantMethods.IsNumericGZ(sp[1]);
                        if (isHexValue)
                        {
                            s = sp[1].Substring(2);
                        }
                        else if (isNumeric)
                        {
                            s = string.Empty;
                            int leftCount = 8 - sp[1].Length;// because of 8 character hex -> 00000000
                            for (int i = 0; i < leftCount; i++)
                                s += '0';
                            s += sp[1];
                        }
                        //else
                        // FIND tf_female|tf_hero or something like that in here

                        if (isHexValue || isNumeric)
                        {
                            RemoveHeaderVariableListEquals(ref itemPoints, s);
                            itemPoints.Add(new HeaderVariable(s, sp[0]));
                        }
                    }
                }
            }

            if (!file.Equals(file2))
                InitializeHeaderFlags(file2, itemPoints);
            else
                headerFlags = itemPoints;
        }

        private static void RemoveHeaderVariableListEquals(ref List<HeaderVariable> list, string hfValue)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].VariableValue.Equals(hfValue))
                {
                    list.RemoveAt(i);
                    return;
                }
            }
        }

        #endregion

        #region Reset And ErrorMessages

        private int[] ResetIntArrays(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = 0;
            return array;
        }

        private void Reset()
        {
            Name = string.Empty;
            PluralName = string.Empty;

            Attributes = ResetIntArrays(Attributes);
            Proficiencies = ResetIntArrays(Proficiencies);
            Skills = ResetIntArrays(Skills);

            Items.Clear();
            ItemFlags.Clear();

            int tmpX = 0;
            UpgradeTroop1 = tmpX;
            UpgradeTroop2 = tmpX;
            FactionID = tmpX;

            string tmp = "0";
            DialogImage = tmp;
            SceneCode = tmp;
            Reserved = tmp;
        }

        private void SendErrorMessage(bool errorOne = false)
        {
            MessageBox.Show((errorOne) ? ERROR_MSG_1 : ERROR_MSG_2);
        }

        private void SendUpgradePathErrorMessage(byte number, string errorCode)
        {
            MessageBox.Show("ERROR (0x494" + number + ") - " + ID + " - UPGRADE_TROOP_" + number + ": " + errorCode, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        #endregion

        #region Set Methods

        private void SetFirstLine(string line)
        {
            string[] lineData = line.Trim().Split();

            if (lineData.Length >= 10)
            {
                //ID = lineData[0];
                Name = lineData[1].Replace('_', ' ');
                PluralName = lineData[2].Replace('_', ' ');

                DialogImage = lineData[3];

                Flags = lineData[4].Trim();
                if (ImportantMethods.IsNumericGZ(Flags))
                {
                    FlagsGZ = int.Parse(Flags);
                    Flags = GetFlagsFromValues(HexConverter.Dec2Hex(FlagsGZ));
                }
                else
                    FlagsGZ = GetFlagsGZFromString(Flags);

                SetSceneCode(lineData[5]);

                SetReserved(lineData[6]);

                FactionID = int.Parse(lineData[7]);//if not ID/numeric --> ERROR!!!

                try
                {
                    UpgradeTroop1 = int.Parse(lineData[8]);
                }
                catch (Exception)
                {
                    UpgradeTroop1ErrorCode = lineData[8];
                    SendUpgradePathErrorMessage(1, UpgradeTroop1ErrorCode);
                }

                try
                {
                    UpgradeTroop2 = int.Parse(lineData[9]);
                }
                catch (Exception)
                {
                    UpgradeTroop2ErrorCode = lineData[9];
                    SendUpgradePathErrorMessage(2, UpgradeTroop2ErrorCode);
                }
            }
            else
                SendErrorMessage(true);
        }

        public void SetItems(string items)
        {
            Items.Clear();
            ItemFlags.Clear();

            List<string> tmpList = new List<string>(items.Trim().Split());
            int itemCount = tmpList.Count / 2 - 1;
            for (int i = 0; i < itemCount; i++)
            {
                string itemX2 = tmpList[i * 2].Trim();
                if (!itemX2.Equals("-1") && itemX2.Length != 0)
                {
                    string itemFlag = tmpList[i * 2 + 1];
                    Items.Add(int.Parse(itemX2));
                    ItemFlags.Add(ulong.Parse(itemFlag) >> 24);
                }
            }
        }

        public void SetSceneCode(string sceneCode)
        {
            SceneCodeGZ = ulong.Parse(sceneCode);
            if (SceneCodeGZ == 0)
                SceneCode = "no_scene";
            else
            {
                byte entryPoint = (byte)((SceneCodeGZ >> 16) & byte.MaxValue);
                ulong code = SceneCodeGZ & ushort.MaxValue;
                SceneCode = code + "|" + entryPoint;
            }
        }

        private void SetReserved(string reserved)
        {
            string resV = "reserved";
            ReservedGZ = (reserved.Equals(resV)) ? 0 : int.Parse(reserved);
            Reserved = (ReservedGZ == 0) ? resV : reserved;
        }

        private void SetAttributes(string attributes)
        {
            string[] sp = attributes.TrimStart().Split();
            if (sp.Length >= 5)
            {
                Strength = int.Parse(sp[0]);
                Agility = int.Parse(sp[1]);
                Intelligence = int.Parse(sp[2]);
                Charisma = int.Parse(sp[3]);
                Level = int.Parse(sp[4]);
                return;
            }
            else if (sp.Length != 1)
            {
                if (ImportantMethods.IsNumericGZ(sp[0]))
                {
                    ulong attribs = ulong.Parse(sp[0]);
                    Strength = (int)(attribs & byte.MaxValue);
                    Agility = (int)((attribs >> 8) & byte.MaxValue);
                    Intelligence = (int)((attribs >> 16) & byte.MaxValue);
                    Charisma = (int)((attribs >> 24) & byte.MaxValue);
                    Level = (int)((attribs >> 32) & byte.MaxValue);
                    return;
                }
            }
            SendErrorMessage();
        }

        private void SetProficiencies(string proficies)
        {
            string[] profS = proficies.Substring(1).Split();
            if (profS.Length >= 7)
            {
                for (int i = 0; i < profS.Length; i++)
                    Proficiencies[i] = int.Parse(profS[i]);
                SetProficiesSC();
                return;
            }
            else if (profS.Length == 1)
            {
                if (ImportantMethods.IsNumericGZ(profS[1]))
                {
                    Proficiencies = GetProficiesFromSC(profS[0]);
                    return;
                }
            }
            SendErrorMessage();
        }

        private void SetProficiesSC()
        {
            string tmp = string.Empty;

            if (Proficiencies[0] == Proficiencies[1] &&
                Proficiencies[0] == Proficiencies[2] &&
                Proficiencies[0] == Proficiencies[3] &&
                Proficiencies[0] == Proficiencies[4] &&
                Proficiencies[0] == Proficiencies[5])
                tmp = "wp(" + Proficiencies[0].ToString();
            else if (Proficiencies[0] == Proficiencies[1] &&
                     Proficiencies[0] == Proficiencies[2])
                tmp = "wpe(" + Proficiencies[2] + ", " + Proficiencies[3] + ", " + Proficiencies[4] + ", " + Proficiencies[5];
            else if (Proficiencies[1] == (Proficiencies[0] + 20) &&
                     Proficiencies[1] == (Proficiencies[2] + 10))
                tmp = "wp_melee(" + Proficiencies[1];
            else if (!ShortProficies)
            {
                if (OneHanded > 0)
                    tmp += "wp_one_handed(" + Proficiencies[0];
                if (TwoHanded > 0)
                    tmp += "wp_two_handed(" + Proficiencies[1];
                if (Polearm > 0)
                    tmp += "wp_polearm(" + Proficiencies[2];
                if (Archery > 0)
                    tmp += "wp_archery(" + Proficiencies[3];
                if (Crossbow > 0)
                    tmp += "wp_crossbow(" + Proficiencies[4];
                if (Throwing > 0)
                    tmp += "wp_throwing(" + Proficiencies[5];
            }
            else
            {
                tmp = "wpex(";
                int profMinOne = Proficiencies.Length - 1;
                for (int i = 0; i < profMinOne; i++)
                    tmp += Proficiencies[i] + ",";
                tmp = tmp.TrimEnd(',');
            }

            if (Firearm > 0)
                tmp += ")|wp_firearm(" + Proficiencies[6];

            ProficienciesSC = (tmp.Length == 0) ? "0" : tmp.TrimStart(')','|') + ')';
        }

        public void SetSkills(string knowledge)
        {
            sk.ReadSkills(knowledge);
            for (int i = 0; i < Skills.Length; i++)
                Skills[i] = sk.Skills[i];
        }

        private void SetFaceCodes(string faceCode)
        {
            FaceFinder ff = new FaceFinder();
            ff.ReadFaceCode(faceCode);
            Face1 = ff.Face1;
            Face2 = ff.Face2;
        }

        #endregion

        #region Get Methods

        private int[] GetProficiesFromSC(string sc)
        {
            int[] profs = new int[7];
            string[] sp = sc.Split('|');
            foreach (string s in sp)
            {
                int x;
                string[] sp2 = s.Replace(" ", string.Empty).TrimEnd(')').Split('(');
                if (sp2[0].Equals("wp"))
                {
                    x = int.Parse(sp2[1]);
                    profs[0] = profs[0] | x;
                    profs[1] = profs[1] | x;
                    profs[2] = profs[2] | x;
                    profs[3] = profs[3] | x;
                    profs[4] = profs[4] | x;
                    profs[5] = profs[5] | x;
                }
                else if (sp2[0].Equals("wpe"))
                {
                    sp2 = sp2[1].Split(',');
                    x = int.Parse(sp2[1]);
                    profs[0] = profs[0] | x;
                    profs[1] = profs[1] | x;
                    profs[2] = profs[2] | x;
                    profs[3] = profs[3] | int.Parse(sp2[1]);
                    profs[4] = profs[4] | int.Parse(sp2[2]);
                    profs[5] = profs[5] | int.Parse(sp2[3]);
                }
                else if (sp2[0].Equals("wp_melee"))
                {
                    x = int.Parse(sp2[1]);
                    profs[0] = profs[0] | (x + 20);
                    profs[1] = profs[1] | x;
                    profs[2] = profs[2] | (x + 10);
                }
                else if (sp2[0].Equals("wpex"))
                {
                    sp2 = sp2[1].Split(',');
                    profs[0] = profs[0] | int.Parse(sp2[0]);
                    profs[1] = profs[1] | int.Parse(sp2[1]);
                    profs[2] = profs[2] | int.Parse(sp2[2]);
                    profs[3] = profs[3] | int.Parse(sp2[3]);
                    profs[4] = profs[4] | int.Parse(sp2[4]);
                    profs[5] = profs[5] | int.Parse(sp2[5]);
                }
                else if (sp2[0].Equals("wp_one_handed"))
                    profs[0] = profs[0] | int.Parse(sp2[1]);
                else if (sp2[1].Equals("wp_two_handed"))
                    profs[1] = profs[1] | int.Parse(sp2[1]);
                else if (sp2[2].Equals("wp_polearm"))
                    profs[2] = profs[2] | int.Parse(sp2[1]);
                else if (sp2[3].Equals("wp_archery"))
                    profs[3] = profs[3] | int.Parse(sp2[1]);
                else if (sp2[4].Equals("wp_crossbow"))
                    profs[4] = profs[4] | int.Parse(sp2[1]);
                else if (sp2[5].Equals("wp_throwing"))
                    profs[5] = profs[5] | int.Parse(sp2[1]);
                else if (sp2[6].Equals("wp_firearms"))
                    profs[6] = profs[6] | int.Parse(sp2[1]);
            }
            return profs;
        }

        private int GetFlagsGZFromString(string flags)
        {
            int flagsGZ = 0;

            if (headerFlags == null)
                InitializeHeaderFlags();

            foreach (string flag in Flags.Split('|'))
            {
                bool foundX = false;
                for (int i = 0; i < headerFlags.Count; i++)
                {
                    if (headerFlags[i].VariableName.Equals(flag))
                    {
                        string tmp = headerFlags[i].VariableName;
                        if (tmp.Contains("0x"))
                            tmp = HexConverter.Hex2Dec(tmp.Replace("0x", string.Empty)).ToString();
                        flagsGZ |= int.Parse(tmp);
                        i = headerFlags.Count;
                    }
                }
                if (!foundX && ImportantMethods.IsNumericGZ(flag))
                    flagsGZ |= int.Parse(flag);
                else
                    MessageBox.Show("ERROR 0x4943 - FLAG_NOT_FOUND " + flag);
            }

            return flagsGZ;
        }

        private string GetFlagsFromValues(string value)
        {
            string tmp, retur = string.Empty;

            if (headerFlags == null)
                InitializeHeaderFlags();

            for (int i = 0; i < headerFlags.Count; i++)
            {
                tmp = headerFlags[i].VariableValue.TrimStart('0');

                if (tmp.Length == 0)
                    tmp = "0";

                int curIdx = value.Length - tmp.Length;
                if (tmp[0] == value[curIdx] && tmp.Length > 1)
                    retur += "|" + headerFlags[i].VariableName;
                else if (value[curIdx] != '0')
                {
                    List<HeaderVariable> list = new List<HeaderVariable>();
                    ulong x_counter = 0;
                    ulong x_tmp = ulong.Parse(HexConverter.Hex2Dec(value[curIdx].ToString()).ToString());
                    if (tmp.Length > 1)
                    {
                        foreach (HeaderVariable hVar in headerFlags)
                            if (hVar.VariableValue.TrimStart('0').Length == value.Substring(curIdx).Length)
                                list.Add(hVar);
                        list.Reverse();
                        foreach (HeaderVariable variable in list)
                        {
                            if (x_counter < x_tmp)
                            {
                                string valre = variable.VariableValue.Trim('0');
                                valre = HexConverter.Hex2Dec(valre).ToString();
                                ulong x_tmp2 = ulong.Parse(valre);
                                ulong ttttt = x_tmp2 + x_counter;
                                if (x_tmp2 <= x_tmp && ttttt <= x_tmp)// vielleicht nochmal überprüfen irgendwanm
                                {
                                    x_counter += x_tmp2;
                                    if (!retur.Contains(variable.VariableName))
                                        retur += "|" + variable.VariableName;
                                }
                            }
                        }
                    }
                }
            }

            tmp = value.Substring(value.Length - 1);
            if (!tmp.Equals("0"))
            {
                foreach (HeaderVariable troopFlag in headerFlags)
                {
                    string tmp2 = troopFlag.VariableValue.TrimStart('0').ToLower();
                    if (tmp2.Length < 2)
                        if (tmp.ToLower().Equals(tmp2))
                            retur += '|' + troopFlag.VariableName;
                }
            }

            if (retur.Length != 0)
                retur = retur.Substring(1);

            string[] tmpS = retur.Split('|');
            SkillHunter.RemoveItemDuplicatesFromArray(ref tmpS);
            retur = string.Empty;
            int minusOne = tmpS.Length - 1;
            for (int i = 0; i < tmpS.Length; i++)
            {
                retur += tmpS[i];
                if (i < minusOne)
                    retur += '|';
            }

            if (retur.Length == 0)
                retur = "0";

            return retur;
        }

        #endregion
    }
}
