using importantLib;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects;
using MB_Studio.Manager.Support.External;
using MB_Decompiler_Library.Objects.Support;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace MB_Studio.Manager
{
    public partial class TroopManager : ToolForm
    {
        #region Attributes

        public const string FACE_CODE_ZERO = "0x0000000000000000000000000000000000000000000000000000000000000000";

        private List<string> items = new List<string>();
        private List<ulong> inventoryItemFlags = new List<ulong>();

        private List<Skriptum> itemsRList = new List<Skriptum>();

        #endregion

        #region Loading

        public TroopManager() : base(Skriptum.ObjectType.TROOP, true)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectType);// ansonsten für alle in Toolform

            InitializeComponent();
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            return new Troop(raw_data);
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();

            CodeReader cr = new CodeReader(CodeReader.ModPath + CodeReader.Files[(int)Skriptum.ObjectType.ITEM]);
            itemsRList.AddRange(cr.ReadItem());

            for (int i = 0; i < CodeReader.Items.Count; i++)
                items.Add(i + " - " + CodeReader.Items[i]);

            if (Has3DView)
                AddBrfFileEntryToModuleIni("Troop3DPreview");

            LoadSets();
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();

            foreach (string item in items)
                items_lb.Items.Add(item);
            foreach (string scene in CodeReader.Scenes)
                scenes_lb.Items.Add(scene);
            foreach (string faction in CodeReader.Factions)
                factions_lb.Items.Add(faction);
            foreach (Skriptum troop in types)
            {
                upgradeTroop1_lb.Items.Add(((Troop)troop).ID);
                upgradeTroop2_lb.Items.Add(((Troop)troop).ID);
            }
            //upgradeTroop1_lb.Items.RemoveAt(1);//maybe a problem for other versions
            //upgradeTroop2_lb.Items.RemoveAt(1);//maybe a problem for other versions

            CreateSkillGroupBox();

            foreach (Control c in showItemsInOpenBrf_gb.Controls)
                if (GetNameEndOfControl(c).Equals("cbb"))
                    ((ComboBox)c).SelectedIndexChanged += TroopManager_OpenBrfItems_SelectedIndexChanged;

            ResetControls();
        }

        private void TroopManager_OpenBrfItems_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            if (IsDataLoaded && Has3DView)
            {//change later so only the specific bone will be updated!
                OpenBrfManager.Troop3DPreviewClearData();//doesn't clear correct?
                //openBrfManager.Troop3DPreviewShow();//workaround (it saves cleared state)
                foreach (Control c in showItemsInOpenBrf_gb.Controls)
                    if (GetNameEndOfControl(c).Equals("cbb"))
                        if (((ComboBox)c).SelectedIndex >= 0)
                            SetupTroopItemBone(((ComboBox)c).SelectedItem.ToString());
                OpenBrfManager.Troop3DPreviewShow();//change later so only the specific bone will be updated!
            }
        }

        private void CreateSkillGroupBox()
        {
            Font font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Color backColor = Color.FromArgb(56, 56, 56);
            Color foreColor = Color.White;
            for (int i = 0; i < SkillHunter.Skillnames.Length - 8; i++)
            {
                int tabIndex = (i / 8 + 1) * 8 - (i - (i / 8) * 8) - 1;

                // label
                Label skillLabelX = new Label()
                {
                    BackColor = backColor,
                    Font = font,
                    ForeColor = foreColor,
                    Location = new Point(6 + (i / 8) * 145, 27 + i * 26 - (i / 8) * 208),
                    Name = SkillHunter.Skillnames[i] + "lbl",
                    TabIndex = tabIndex,
                    Tag = i,
                    TabStop = false,
                    Size = new Size(100, 13),
                    TextAlign = ContentAlignment.MiddleRight
                };
                string text = SkillHunter.Skillnames[i].Replace('_', ' ').TrimEnd();
                int length = Math.Min(text.Length - 1, 12);
                text = text.Substring(0, 1).ToUpper() + text.Substring(1, length) + ':';
                skillLabelX.Text = text;

                // numeric
                NumericUpDown skillNumericX = new NumericUpDown()
                {
                    BackColor = backColor,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = font,
                    ForeColor = foreColor,
                    Location = new Point(110 + (i / 8) * 145, 25 + i * 26 - (i / 8) * 208),
                    Maximum = 15,
                    Name = SkillHunter.Skillnames[i] + "num",
                    Size = new Size(34, 20),
                    TabIndex = tabIndex,
                    Tag = i,
                    TabStop = false
                };

                groupBox_6_gb.Controls.Add(skillLabelX);
                groupBox_6_gb.Controls.Add(skillNumericX);
            }
        }

        #endregion

        #region GUI

        protected override void ResetControls()
        {
            base.ResetControls();

            usedItems_lb.Items.Clear();

            foreach (Control c in showItemsInOpenBrf_gb.Controls)
                if (c.Name.Substring(c.Name.LastIndexOf('_') + 1).Equals("cbb"))
                    ((ComboBox)c).Items.Clear();

            if (items_lb.Items.Count != 0 && OpenBrfManager != null)
                if (OpenBrfManager.IsShown && items_lb.SelectedIndex != 0)
                    items_lb.SelectedIndex = 0;

            face1_txt.Text = FACE_CODE_ZERO;
            face2_txt.Text = face1_txt.Text;
            reserved_txt.Text = "reserved";
        }

        protected override void AddFromOtherMod(AddTypeFromOtherMod f = null)
        {
            f = new AddTroopFromOtherMod();

            base.AddFromOtherMod(f);
        }

        #endregion

        #region Setups

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            Troop troop = (Troop)type;

            #region GROUP1 - Flags & Guarantee

            skins_lb.SelectedIndex = troop.FlagsGZ & 0xF;
            if (troop.FlagsGZ > 0)
            {
                foreach (string flag in troop.Flags.Split('|'))
                {
                    Control[] cc = groupBox_1_gb.Controls.Find(flag.Substring(3) + "_cb", false);
                    if (cc.Length == 1 && !flag.Contains("tf_guarantee_"))
                        ((CheckBox)cc[0]).CheckState = CheckState.Checked;
                    //else if (cc.Length > 1 && !flag.Contains("tf_guarantee_"))
                    //    MessageBox.Show("ERROR: Double Flags found! --> " + cc.Length); // enable if needed again
                    else
                    {
                        for (int i = 0; i < guarantee_gb.Controls.Count; i++)
                        {
                            CheckBox c = (CheckBox)guarantee_gb.Controls[i];
                            if (c.Name.Remove(c.Name.LastIndexOf('_')).Equals(flag.Replace("tf_guarantee_", string.Empty)))
                            {
                                c.CheckState = CheckState.Checked;
                                i = guarantee_gb.Controls.Count;
                            }
                        }
                    }
                }
            }

            #endregion

            #region GROUP2 - Faction & Special Values

            factions_lb.SelectedIndex = troop.FactionID;
            reserved_txt.Text = troop.Reserved;
            string[] sceneCode = troop.SceneCode.Split('|');
            if (IsNumeric(sceneCode[0]))
                scenes_lb.SelectedIndex = int.Parse(sceneCode[0]);
            else
                scenes_lb.SelectedIndex = 0;
            if (sceneCode.Length > 1)
                entryPoint_numeric.Value = byte.Parse(sceneCode[1]);

            #endregion

            #region GROUP3 - Items

            IsDataLoaded = false;

            foreach (int itemID in troop.Items)
            {
                Item itemX = (Item)itemsRList[itemID];
                AddItemToInventarComboboxByKind(itemID, itemX.Prefix + itemX.ID);
                usedItems_lb.Items.Add(itemID + " - " + itemX.Prefix + itemX.ID);
            }

            inventoryItemFlags = troop.ItemFlags;

            SelectFirstInventarComboBoxItems();

            #endregion

            #region GROUP4 - Attributes & Level

            str_txt.Text = troop.Strength.ToString();
            agi_txt.Text = troop.Agility.ToString();
            int_txt.Text = troop.Intelligence.ToString();
            cha_txt.Text = troop.Charisma.ToString();

            level_txt.Text = troop.Level.ToString();

            #endregion

            #region GROUP5 - Proficiencies

            onehandedWeapon_txt.Text = troop.OneHanded.ToString();
            twohandedWeapon_txt.Text = troop.TwoHanded.ToString();
            polearms_txt.Text = troop.Polearm.ToString();
            archery_txt.Text = troop.Archery.ToString();
            crossbows_txt.Text = troop.Crossbow.ToString();
            throwing_txt.Text = troop.Throwing.ToString();
            firearms_txt.Text = troop.Firearm.ToString();

            #endregion

            #region GROUP6 - Skills

            SetupTroopSkills(troop);

            #endregion

            #region GROUP7 - Faces & Upgrade Paths

            face1_txt.Text = troop.Face1;
            face2_txt.Text = troop.Face2;

            if (troop.UpgradeTroop1 < upgradeTroop1_lb.Items.Count)
                upgradeTroop1_lb.SelectedIndex = troop.UpgradeTroop1;
            else
                MessageBox.Show("TROOP_UPGRADE_PATH1:" + Environment.NewLine + troop.UpgradeTroop1ErrorCode);
            if (troop.UpgradeTroop2 < upgradeTroop2_lb.Items.Count)
                upgradeTroop2_lb.SelectedIndex = troop.UpgradeTroop2;
            else
                MessageBox.Show("TROOP_UPGRADE_PATH2:" + Environment.NewLine + troop.UpgradeTroop2ErrorCode);

            #endregion
        }

        private void SelectFirstInventarComboBoxItems()
        {
            head_cbb.SelectedIndex = (head_cbb.Items.Count != 0) ? 0 : -1;
            body_cbb.SelectedIndex = (body_cbb.Items.Count != 0) ? 0 : -1;
            feet_cbb.SelectedIndex = (feet_cbb.Items.Count != 0) ? 0 : -1;
            hand_cbb.SelectedIndex = (hand_cbb.Items.Count != 0) ? 0 : -1;
            weapon_cbb.SelectedIndex = (weapon_cbb.Items.Count != 0) ? 0 : -1;
            shield_cbb.SelectedIndex = (shield_cbb.Items.Count != 0) ? 0 : -1;
            horse_cbb.SelectedIndex = (horse_cbb.Items.Count != 0) ? 0 : -1;
            calfR_cbb.SelectedIndex = (calfR_cbb.Items.Count != 0) ? 0 : -1;

            IsDataLoaded = true;

            TroopManager_OpenBrfItems_SelectedIndexChanged();
        }

        private void SetupTroopItemBone(string itemId)
        {
            Item itemX = null;
            if (itemId.StartsWith("itm_"))
                itemId = itemId.Substring(4);
            for (int i = 0; i < itemsRList.Count; i++)
            {
                if (itemsRList[i].ID.Equals(itemId))
                {
                    itemX = (Item)itemsRList[i];
                    i = itemsRList.Count;
                }
            }
            if (itemX != null)
                SetupTroopItemBone(itemX);
        }

        private void SetupTroopItemBone(Item item)
        {
            #region ID OVERVIEW

            /* SKELETONS */
            // 0 human
            // 1 human_horse

            /* BONES */
            // 0 abdomen   -  X X
            // 1 thighL    -  4 thighR
            // 2 calfL     -  5 calfR
            // 3 footL     -  6 footR
            // 7 spine     -  X X
            // 8 thorax    -  X X
            // 9 head      -  X X
            //10 shoulderL - 15 shoulderR 
            //11 upperarmL - 16 upperarmR
            //12 forearmL  - 17 forearmR
            //13 handL     - 18 handR
            //14 itemL     - 19 itemR

            /* CARRYPOSITIONS */
            // 0 itcf_carry_sword_left_hip
            // 1 itcf_carry_axe_left_hip
            // 2 itcf_carry_mace_left_hip
            // 3 itcf_carry_dagger_front_left
            // 4 itcf_carry_dagger_front_right
            // 5 itcf_carry_axe_back
            // 6 itcf_carry_sword_back
            // 7 itcf_carry_spear
            // 8 itcf_carry_kite_shield
            // 9 itcf_carry_board_shield
            //10 itcf_carry_round_shield
            //11 itcf_carry_crossbow_back
            //12 itcf_carry_bow_back
            //13 itcf_carry_quiver_front_right
            //14 itcf_carry_quiver_back_right
            //15 itcf_carry_quiver_right_vertical
            //16 itcf_carry_quiver_back
            //17 itcf_carry_buckler_left
            //18 itcf_carry_revolver_right
            //19 itcf_carry_pistol_front_left
            //20 itcf_carry_bowcase_left
            //21 itcf_carry_katana
            //22 itcf_carry_wakizashi

            #endregion

            //bool isAtOrigin = true;
            int boneIndex = 0;
            int skeletonId = 0;
            int carryPosition = -1;
            switch (ItemManager.GetItemTypeIndex(item))
            {
                case 1: //Horse
                    skeletonId++;//is this correct for horse?
                    break;
                case 7: //Shield
                    boneIndex = 14;
                    break;
                case 12://HeadArmor
                    boneIndex = 9;
                    break;
                case 13://BodyArmor
                    boneIndex = 8;//thorax
                    break;
                case 14://FootArmor
                    boneIndex = 9;
                    break;
                case 15://HandArmor
                    boneIndex = 13;
                    break;
                case 8: //Bow
                    carryPosition = 12;
                    break;
                case 9: //Crossbow
                    carryPosition = 11;
                    break;
                case 16://Pistol
                    carryPosition = 19;//18
                    break;
                case 2: //Onehanded
                case 3: //Twohanded
                case 4: //Polearm
                case 10://Thrown
                case 17://Musket
                    boneIndex = 19;
                    break;
                case 5: //Arrows
                    carryPosition = 16;
                    break;
                case 6: //Bolts
                    carryPosition = 15;
                    break;
                case 18://Bullets
                    carryPosition = 14;
                    break;
                //case 11://Goods
                //case 19://Animal
                //case 20://Book
                default://none = 0
                    boneIndex = 5;
                    break;
            }

            if (Has3DView)
            {
                if (skeletonId == 0)//no horse on screen!
                {
                    foreach (string meshName in item.Meshes)// bone (0 to 19) // skeleton (0 to 1) // carryPosition (0 to ... (depends on file data)) // carryPosition before bone!!!
                    {
                        string mName = meshName.Split()[0].Trim();
                        if (OpenBrfManager.AddMeshToTroop3DPreview(mName, boneIndex, skeletonId, carryPosition))//error with file path and mod path
                            Console.WriteLine("ADDED '" + mName + "' to Troop3DPreview:" + Environment.NewLine + "  --> openBrfManager.AddMeshToTroop3DPreview(" + mName + ", " + boneIndex + ", " + skeletonId + ", " + carryPosition/* + ", " + isAtOrigin*/ + ")");
                        else
                            Console.WriteLine("ADDING '" + mName + "' to Troop3DPreview FAILED!");
                    }
                }
                //else/* if (skeletonId == 1)*/
                //{
                //    // CODE
                //}
            }
        }

        private void SetupTroopSkills(Troop troop)
        {
            for (int i = 0; i < troop.Skills.Length; i++)
            {
                for (int j = 0; j < groupBox_6_gb.Controls.Count; j++)
                {
                    Control c = groupBox_6_gb.Controls[j];
                    if (c.Tag != null && c.Name.Substring(c.Name.LastIndexOf('_')).Equals("_num"))
                    {
                        if (i == int.Parse(c.Tag.ToString()))
                        {
                            ((NumericUpDown)c).Value = troop.Skills[i];
                            j = groupBox_6_gb.Controls.Count;
                        }
                    }
                }
            }
        }

        #endregion

        #region Save

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            /// Check if is set by default now - if not make it happen
            if (troopImage_txt.Text.Length == 0)
                troopImage_txt.Text = "0";
            if (reserved_txt.Text.Length == 0)
                reserved_txt.Text = "0";
            if (face1_txt.Text.Length == 0)
                face1_txt.Text = FACE_CODE_ZERO;
            if (face2_txt.Text.Length == 0)
                face2_txt.Text = FACE_CODE_ZERO;
            /// Check if is set by default now - if not make it happen
            
            /*if (!id_txt.Text.StartsWith("trp_"))//should already be done in ToolForm
                id_txt.Text = "trp_" + id_txt.Text;
            else
                id_txt.Text = "trp_" + id_txt.Text;*/
            
            string tmp = 
                /*id_txt.Text.Replace(' ', '_')*/values[0] + ' ' + 
                name_txt.Text.Replace(' ', '_') + ' ' + 
                plural_name_txt.Text.Replace(' ', '_') + ' ' + 
                troopImage_txt.Text.Replace(' ', '_') + ' ' +
                GetFlagsValue().ToString() + ' ' + 
                GetSceneCode().ToString() + ' ' + 
                reserved_txt.Text + ' ' +
                factions_lb.SelectedIndex.ToString() + ' ' + 
                upgradeTroop1_lb.SelectedIndex.ToString() + ' ' + 
                upgradeTroop2_lb.SelectedIndex.ToString() + ";  ";

            for (int i = 0; i < usedItems_lb.Items.Count; i++)
                tmp += usedItems_lb.Items[i].ToString().Split('-')[0] + inventoryItemFlags[i] + " ";//could be a problem when itemFlags are fucked up

            for (int i = 0; i < (64 - usedItems_lb.Items.Count); i++)
                tmp += "-1 0 ";

            tmp += ";  ";

            tmp += 
                str_txt.Text + ' ' + 
                agi_txt.Text + ' ' + 
                int_txt.Text + ' ' + 
                cha_txt.Text + ' ' + 
                level_txt.Text + ' ' + 
                onehandedWeapon_txt.Text + ' ' + 
                twohandedWeapon_txt.Text + ' ' + 
                polearms_txt.Text + ' ' + 
                archery_txt.Text + ' ' + 
                crossbows_txt.Text + ' ' + 
                throwing_txt.Text + ' ' +
                firearms_txt.Text + ';';

            SuperGZ_192Bit skillsValue = new SuperGZ_192Bit(GetSkillCodes());
            foreach (uint u in skillsValue.ValueUInt)
                tmp += u + " ";
            tmp += ";  ";

            SuperGZ_256Bit faceCode1 = new SuperGZ_256Bit(face1_txt.Text);
            SuperGZ_256Bit faceCode2 = new SuperGZ_256Bit(face2_txt.Text);
            for (int i = 0; i < faceCode1.ValueULong.Length; i++)
                tmp += faceCode1.ValueULong[i] + " ";
            for (int i = 0; i < faceCode2.ValueULong.Length; i++)
                tmp += faceCode2.ValueULong[i] + " ";

            /*bool newOne = false;
            Troop changed = new Troop(values);

            MB_Studio.SavePseudoCodeByType(changed, values);

            if (selectedIndex < typeSelect_lb.Items.Count - 1)
                types[selectedIndex] = changed;
            else
            {
                types.Add(changed);
                typeSelect_lb.Items.Add(changed.ID);
                newOne = !newOne;
            }

            foreach (Troop troop in types)
                list.Add(troop);

            SourceWriter.WriteAllObjects();
            new SourceWriter().WriteTroops(list);

            if (newOne)
                typeSelect_lb.SelectedIndex = typeSelect_lb.Items.Count - 1;*/
            
            values.Clear();
            values = new List<string>(tmp.Split(';'));
            string[] valuesX = values.ToArray();
            Troop t = new Troop(valuesX);

            MB_Studio.SavePseudoCodeByType(t, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, t);
        }

        #region Calculations

        private uint GetFlagsValue()
        {
            uint x = 0x00000000;

            if (skins_lb.SelectedIndex > 0)
                x |= (uint)skins_lb.SelectedIndex; // skins

            if (hero_cb.Checked)
                x |= 0x00000010; // tf_hero
            if (inactive_cb.Checked)
                x |= 0x00000020; // tf_inactive
            if (unkillable_cb.Checked)
                x |= 0x00000040; // tf_unkillable
            if (allways_fall_dead_cb.Checked)
                x |= 0x00000080; // tf_allways_fall_dead
            if (no_capture_alive_cb.Checked)
                x |= 0x00000100; // tf_no_capture_alive
            if (mounted_cb.Checked)
                x |= 0x00000400; // tf_mounted
            if (is_merchant_cb.Checked)
                x |= 0x00001000; // tf_is_merchant
            if (randomize_face_cb.Checked)
                x |= 0x00008000; // tf_randomize_face 

            if (unmoveable_in_party_window_cb.Checked)
                x |= 0x10000000; // tf_unmoveable_in_party_window 

            #region Guarantee

            if (boots_cb.Checked)
                x |= 0x00100000; // tf_guarantee_boots
            if (armor_cb.Checked)
                x |= 0x00200000; // tf_guarantee_armor 
            if (helmet_cb.Checked)
                x |= 0x00400000; // tf_guarantee_helmet
            if (gloves_cb.Checked)
                x |= 0x00800000; // tf_guarantee_gloves 
            if (horse_cb.Checked)
                x |= 0x01000000; // tf_guarantee_horse 
            if (shield_cb.Checked)
                x |= 0x02000000; // tf_guarantee_shield 
            if (ranged_cb.Checked)
                x |= 0x04000000; // tf_guarantee_ranged
            if (polearm_cb.Checked)
                x |= 0x08000000; // tf_guarantee_polearm

            #endregion

            return x;
        }

        private uint GetSceneCode()
        {
            uint tsf_entry_mask = 0x00ff0000;
            byte tsf_entry_bits = 16;

            uint scnCode = 0x00000000;
            uint entryPoint = 0;

            if (scenes_lb.SelectedIndex > 0)
                scnCode = (uint)scenes_lb.SelectedIndex;
            if (entryPoint_numeric.Value > 0)
                entryPoint = ((uint)entryPoint_numeric.Value << tsf_entry_bits) & tsf_entry_mask;
            scnCode |= entryPoint;

            return scnCode;
        }

        private uint[] GetSkillCodes()
        {
            uint[] skillCodes = new uint[6];
            string tmp = string.Empty;
            for (int i = 0; i < SkillHunter.Skillnames.Length - 6; i++)
            {
                for (int j = 0; j < groupBox_6_gb.Controls.Count; j++)
                {
                    Control num = groupBox_6_gb.Controls[j];
                    if (num.TabIndex == i && num.Name.Substring(num.Name.LastIndexOf('_') + 1).Equals("num"))
                    {
                        tmp += HexConverter.Dec2Hex(((NumericUpDown)num).Value).Substring(7);
                        j = groupBox_6_gb.Controls.Count;
                    }
                }
            }
            tmp += "000000"; // maybe replace later if there are more than 42 skills possible
            for (int i = 5; i >= 0; i--)
                skillCodes[i] = uint.Parse(HexConverter.Hex2Dec(ImportantMethods.ReverseString(tmp.Substring(i * 8, 8))).ToString());
            //MessageBox.Show(skillCode);
            //IEnumerable<string> skillCodes = ImportantMethods.WholeChunks(skillCode, 8);
            //for (int i = 0; i < sss.Length; i++)
            //{
            //    sss[i] = skillCodes.GetEnumerator().Current;
            //    if (i < sss.Length - 1)
            //        skillCodes.GetEnumerator().MoveNext();
            //}
            //MessageBox.Show(HexConverter.Hex2Dec(sss[0]) + "|" + HexConverter.Hex2Dec(sss[1]) + "|" + HexConverter.Hex2Dec(sss[2]) + "|" + HexConverter.Hex2Dec(sss[3]) + "|" + HexConverter.Hex2Dec(sss[4]) + "|" + HexConverter.Hex2Dec(sss[5]));
            return skillCodes;
        }

        #endregion

        #endregion

        #region Items

        private void AddItemToUsedItems_btn_Click(object sender, EventArgs e)
        {
            if ((items_lb.SelectedItems.Count + usedItems_lb.Items.Count) <= 64)
            {
                foreach (string item in items_lb.SelectedItems)
                {
                    int itemID = int.Parse(item.Split('-')[0].TrimEnd());
                    AddItemToInventarComboboxByKind(itemID, item);
                    //SetupTroopItemBone(itemsRList[itemID]);
                    usedItems_lb.Items.Add(item);
                }
                inventoryItemFlags.Add(0);//check
            }
            else
                MessageBox.Show("You have too many items selected!"
                                + Environment.NewLine + "Only 64 itemslots are available!"
                                + Environment.NewLine + " --> Used itemslots: " + usedItems_lb.Items.Count
                                + Environment.NewLine + " --> Selected items: " + items_lb.SelectedItems.Count);
        }

        private void AddItemToInventarComboboxByKind(int itemID, string itemName)
        {
            switch (ItemManager.GetItemTypeIndex((Item)itemsRList[itemID]))
            {
                case 1: //Horse
                    horse_cbb.Items.Add(itemName);
                    break;
                case 7: //Shield
                    shield_cbb.Items.Add(itemName);
                    break;
                case 12://HeadArmor
                    head_cbb.Items.Add(itemName);
                    break;
                case 13://BodyArmor
                    body_cbb.Items.Add(itemName);
                    break;
                case 14://FootArmor
                    feet_cbb.Items.Add(itemName);
                    break;
                case 15://HandArmor
                    hand_cbb.Items.Add(itemName);
                    break;
                case 2: //Onehanded
                case 3: //Twohanded
                case 4: //Polearm
                case 8: //Bow
                case 9: //Crossbow
                case 10://Thrown
                case 16://Pistol
                case 17://Musket
                    weapon_cbb.Items.Add(itemName);
                    break;
                //case 11://Goods
                //case 5: //Arrows
                //case 6: //Bolts
                //case 18://Bullets
                //case 19://Animal
                //case 20://Book
                default://none = 0
                    calfR_cbb.Items.Add(itemName);
                    break;
            }
        }

        private void UsedItemUP_btn_Click(object sender, EventArgs e)
        {
            if (usedItems_lb.SelectedIndex > 0)
            {
                foreach (int i in usedItems_lb.SelectedIndices)
                {
                    string tmp = usedItems_lb.Items[i - 1].ToString();
                    usedItems_lb.Items[i - 1] = usedItems_lb.Items[i];
                    usedItems_lb.Items[i] = tmp;
                    //usedItems_lb.SelectedIndex -= 1; // rethink this
                }
            }
        }

        private void UsedItemDOWN_btn_Click(object sender, EventArgs e)
        {
            if (usedItems_lb.SelectedIndex < usedItems_lb.Items.Count - 1)
            {
                foreach (int i in usedItems_lb.SelectedIndices)
                {
                    string tmp = usedItems_lb.Items[i + 1].ToString();
                    usedItems_lb.Items[i + 1] = usedItems_lb.Items[i];
                    usedItems_lb.Items[i] = tmp;
                    //usedItems_lb.SelectedIndex += 1; // rethink this
                }
            }
        }

        private void UsedItemREMOVE_btn_Click(object sender, EventArgs e)
        {
            if (usedItems_lb.Items.Count > 0)
            {
                int i = usedItems_lb.SelectedIndex;
                if (i < 0)
                    i = usedItems_lb.Items.Count - 1;
                    usedItems_lb.Items.RemoveAt(i);
                    inventoryItemFlags.RemoveAt(i); // check
            }
        }

        private void SelectedItemFlags_txt_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(selectedItemFlags_txt.Text))
                selectedItemFlags_txt.ForeColor = Color.White;
            else
                selectedItemFlags_txt.ForeColor = Color.Red;
            if ((setItemFlags_btn.Enabled && selectedItemFlags_txt.ForeColor == Color.Red)
              ||(!setItemFlags_btn.Enabled && selectedItemFlags_txt.ForeColor == Color.White))
                setItemFlags_btn.Enabled = !setItemFlags_btn.Enabled;
        }

        private void SetItemFlags_btn_Click(object sender, EventArgs e)
        {
            inventoryItemFlags[usedItems_lb.SelectedIndex] = ulong.Parse(selectedItemFlags_txt.Text);
        }

        private void UsedItems_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = usedItems_lb.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (inventoryItemFlags.Count > selectedIndex)
                    selectedItemFlags_txt.Text = inventoryItemFlags[selectedIndex].ToString();
                if (Has3DView)
                    LoadCurrentMeshWithOpenBrf((ListBox)sender);
            }
        }

        private void SearchItems_TextChanged(object sender, EventArgs e)
        {
            SearchForContaining(items_lb, itemsRList.ToArray(), searchItems_SearchTextBox.Text, null, true);
        }

        private void SearchUsedItems_txt_TextChanged(object sender, EventArgs e)
        {
            List<int> troopItems = ((Troop)types[typeSelect_lb.SelectedIndex]).Items;
            List<Skriptum> list = new List<Skriptum>();
            for (int i = 0; i < itemsRList.Count; i++)
                if (troopItems.Contains(i))
                    list.Add(itemsRList[i]);
            SearchForContaining(usedItems_lb, itemsRList, searchUsedItems_SearchTextBox.Text, list, true);
        }

        private void LoadSets()
        {
            string[] names = new string[Properties.Settings.Default.setNames.Count];
            Properties.Settings.Default.setNames.CopyTo(names, 0);
            foreach (Control c in itemSets_gb.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (nameEnd.Equals("btn"))
                {
                    Invoke((MethodInvoker)delegate
                    {
                        c.Text = names[int.Parse(c.Name.Split('_')[1]) - 1];
                        c.Click += Set_X_Click;
                    });
                }
            }
        }

        private void Set_X_Click(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            int i = int.Parse(c.Name.Split('_')[1]) - 1;
            string[] itemsFromSet = GetItemsFromSetByIndex(i);
            if (itemsFromSet != null)
            {
                usedItems_lb.Items.Clear();
                foreach (string itemID in itemsFromSet)
                    usedItems_lb.Items.Add(items[int.Parse(itemID)]);
            }
            else
                MessageBox.Show(c.Text +  " doesn't have items yet!", "Itemsets", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static string[] GetItemsFromSetByIndex(int index)
        {
            string[] array;
            string[] setXItems = Properties.Settings.Default.setItems[index].Split('|');
            if (!setXItems[0].Equals("-"))
            {
                array = new string[setXItems.Length];
                for (int i = 0; i < array.Length; i++)
                    array[i] = setXItems[i];
            }
            else
                array = null;
            return array;
        }

        public static string[] GetItemsFlagsFromSetByIndex(int index)
        {
            StringCollection setItemsFlags = Properties.Settings.Default.setItemsFlags;
            string[] array = new string[setItemsFlags.Count];
            for (int i = 0; i < array.Length; i++)
                array[i] = setItemsFlags[i];
            return array;
        }

        #endregion

        #region OpenBrf

        protected override void OnHandleDestroyed()
        {
            base.OnHandleDestroyed();

            //if (Has3DView)
            RemoveBrfFileEntryFromModuleIni("Troop3DPreview");
        }

        private void Items_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Has3DView && OpenBrfManager != null)
                LoadCurrentMeshWithOpenBrf((ListBox)sender);
        }

        private void LoadCurrentMeshWithOpenBrf(ListBox lb)
        {
            int selectedIndex = lb.SelectedIndex;
            if (selectedIndex >= 0)
            {
                try
                {
                    string itemID = lb.SelectedItem.ToString().Split('-')[1].TrimStart();
                    itemID = itemID.Substring(itemID.IndexOf('_') + 1);
                    for (int i = 0; i < itemsRList.Count; i++)
                    {
                        if (itemID.Equals(itemsRList[i].ID))
                        {
                            Item itemX = (Item)itemsRList[i];
                            for (int j = 0; j < itemX.Meshes.Count; j++)
                            {
                                string sss = itemX.Meshes[j].Split()[0].Trim();
                                Console.WriteLine("|" + sss + "|");
                                Console.WriteLine("void LoadCurrentMeshWithOpenBrf(ListBox lb) : " + OpenBrfManager.SelectItemNameByKind(sss));
                            }
                            i = itemsRList.Count;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void Troop3DPreview(ListBox lb)
        {
            int selectedIndex = lb.SelectedIndex;
            if (selectedIndex >= 0)
            {
                try
                {
                    string itemID = lb.SelectedItem.ToString().Split('-')[1].TrimStart();
                    itemID = itemID.Substring(itemID.IndexOf('_') + 1);
                    for (int i = 0; i < itemsRList.Count; i++)
                    {
                        if (itemID.Equals(itemsRList[i].ID))
                        {
                            Item itemX = (Item)itemsRList[i];
                            for (int j = 0; j < itemX.Meshes.Count; j++)
                            {
                                string sss = itemX.Meshes[j].Split()[0].Trim();
                                Console.WriteLine("|" + sss + "|");
                                Console.WriteLine("void Troop3DPreview(ListBox lb) : " + OpenBrfManager.AddMeshToTroop3DPreview(sss, 18));//item.R --> highest bone index (-1 to 18));

                            }
                            i = itemsRList.Count;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        #endregion
    }
}
