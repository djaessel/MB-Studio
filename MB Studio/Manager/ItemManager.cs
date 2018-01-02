using brfManager;
using importantLib;
using MB_Decompiler;
using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects;
using MB_Studio.Main;
using skillhunter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MB_Studio.Manager
{
    public partial class ItemManager : ToolForm
    {
        #region Attributes

        private bool show3D_Override = true;

        private int meshTag;
        private const byte MESH_CONTROLS_COUNT = 6;//5 ohne Bild
        private const byte MESH_INFO_CONTROLS_COUNT = 6;
        private const byte MESH_CONTROLS_TOP_HEIGHT = 32;
        private const byte MESH_CONTROLS_TOP_DEFAULT = 24;

        //private Thread openBrfThread;
        private OpenBrfManager openBrfManager = null;

        private List<int[]> memberValues = new List<int[]>();
        private List<SimpleTrigger> itemTrigger = new List<SimpleTrigger>();

        #endregion

        #region Loading

        public ItemManager() : base(Skriptum.ObjectType.ITEM)
        {
            //types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectTypeID); // später vielleicht wieder in ToolForm, falls BUG gehoben!
            InitializeComponent();
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            return new Item(raw_data);
        }

        protected override void LoadSettingsAndLists(bool loadSavedTypes = true)
        {
            base.LoadSettingsAndLists(loadSavedTypes);

            /*Invoke((MethodInvoker)delegate
            {
                openBrfThread = new Thread(StartOpenBrfManager) { IsBackground = true };
                openBrfThread.Start();
            });*/
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();

            type_cbb.SelectedIndex = 0;
            attach_cbb.SelectedIndex = 0;
            custom_kill_info_cbb.SelectedIndex = 0;

            meshTag = int.Parse(showGroup_3_btn.Tag.ToString());

            ResetControls();
        }

        #endregion

        #region Setups

        protected override void ResetControls()
        {
            base.ResetControls();

            foreach (Control c in groupBox_1_gb.Controls)
            {
                c.ForeColor = Color.White;
                foreach (Control c2 in c.Controls)
                    c2.ForeColor = Color.White;
            }

            foreach (GroupBox gb in groupBox_2_gb.Controls)
            {
                foreach (Control gbc in gb.Controls)
                {
                    string nameEnd = GetNameEndOfControl(gbc);
                    if (nameEnd.Equals("gb"))
                    {
                        foreach (Control gbc2 in gbc.Controls)
                        {
                            gbc2.ForeColor = Color.WhiteSmoke;
                            if (nameEnd.Equals("rb"))
                                ((RadioButton)gbc2).Checked = false;
                            else if (nameEnd.Equals("cb"))
                                ((CheckBox)gbc2).CheckState = CheckState.Unchecked;
                        }
                    }
                    else
                    {
                        gbc.ForeColor = Color.WhiteSmoke;
                        if (nameEnd.Equals("rb"))
                            ((RadioButton)gbc).Checked = false;
                        else if (nameEnd.Equals("cb"))
                            ((CheckBox)gbc).CheckState = CheckState.Unchecked;
                    }
                }
            }

            Label[] labels = new Label[] { id_column_lbl, resourceName_column_lbl, meshKind_column_lbl, modifierBits_column_lbl, show_column_lbl, delete_column_lbl };
            Button tmp_btn = addMesh_btn;

            groupBox_3_gb.Controls.Clear();

            id_column_lbl = labels[0];
            resourceName_column_lbl = labels[1];
            meshKind_column_lbl = labels[2];
            modifierBits_column_lbl = labels[3];
            show_column_lbl = labels[4];
            delete_column_lbl = labels[5];

            addMesh_btn = tmp_btn;

            groupBox_3_gb.Controls.Add(id_column_lbl);
            groupBox_3_gb.Controls.Add(resourceName_column_lbl);
            groupBox_3_gb.Controls.Add(meshKind_column_lbl);
            groupBox_3_gb.Controls.Add(modifierBits_column_lbl);
            groupBox_3_gb.Controls.Add(show_column_lbl);
            groupBox_3_gb.Controls.Add(delete_column_lbl);

            groupBox_3_gb.Controls.Add(addMesh_btn);

            foreach (Control c in groupBox_4_gb.Controls)
                c.ForeColor = Color.WhiteSmoke;

            foreach (Control c in groupBox_6_gb.Controls)
                c.ForeColor = Color.WhiteSmoke;
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            Item item = (Item)type;
            name_txt.Text = item.Name;//will later be removed
            plural_name_txt.Text = item.PluralName;//will maybe removed later

            #region Property Flags

            string[] itemProperties = item.Properties.Split('|');
            foreach (string itemProperty in itemProperties)
            {
                if (itemProperty.Length > 4)
                {
                    foreach (Control c in groupBox_1_gb.Controls)
                    {
                        string controlKind = GetNameEndOfControl(c);
                        if (controlKind.Equals("gb"))
                        {
                            foreach (Control cx in ((GroupBox)c).Controls)
                            {
                                if (cx.Name.Remove(cx.Name.LastIndexOf('_')).Equals(itemProperty.Substring(4)))
                                {
                                    ((CheckBox)cx).CheckState = CheckState.Checked;
                                    cx.ForeColor = Color.Red;
                                }
                            }
                        }
                        else if (c.Name.Remove(c.Name.LastIndexOf('_')).Equals(itemProperty.Substring(4)))
                        {
                            ((CheckBox)c).CheckState = CheckState.Checked;
                            c.ForeColor = Color.Red;
                        }
                    }

                    if (itemProperty.StartsWith("itp_type_"))
                        type_cbb.SelectedIndex = type_cbb.Items.IndexOf(itemProperty.Substring(9));
                    else if (itemProperty.Contains("_attach_"))
                        attach_cbb.SelectedIndex = attach_cbb.Items.IndexOf(itemProperty.Substring(4));
                    else if (itemProperty.StartsWith("custom_kill_info("))
                        custom_kill_info_cbb.SelectedIndex = int.Parse(itemProperty.Substring(17, 1));
                }
            }

            #endregion

            #region Capability Flags

            string[] itemCapabilityFlags = item.CapabilityFlags.Split('|');
            foreach (string itemCFlag in itemCapabilityFlags) // RADIOBUTTONS ÜBERARBEITEN MIT DEAKTIVIERUNGSFUNKTION UND VIELLEICHT NEU SORTIEREN!!!
            {
                if (itemCFlag.Length > 4)
                {
                    foreach (GroupBox gb in groupBox_2_gb.Controls)
                    {
                        foreach (Control gbc in gb.Controls)
                        {
                            string nameEnd = GetNameEndOfControl(gbc);
                            if (nameEnd.Equals("gb"))
                            {
                                foreach (Control gbc2 in gbc.Controls)
                                {
                                    if (gbc2.Name.Remove(gbc2.Name.LastIndexOf('_')).Equals(itemCFlag))
                                    {
                                        string nameEnd2 = GetNameEndOfControl(gbc2);
                                        if (nameEnd2.Equals("cb"))
                                            ((CheckBox)gbc2).CheckState = CheckState.Checked;
                                        else if (nameEnd2.Equals("rb"))
                                            ((RadioButton)gbc2).Checked = true;
                                        gbc2.ForeColor = Color.Red;
                                    }
                                }
                            }
                            else if (gbc.Name.Remove(gbc.Name.LastIndexOf('_')).Equals(itemCFlag))
                            {
                                if (nameEnd.Equals("cb"))
                                    ((CheckBox)gbc).CheckState = CheckState.Checked;
                                else if (nameEnd.Equals("rb"))
                                    ((RadioButton)gbc).Checked = true;
                                gbc.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }

            #endregion

            #region Meshes

            SetupMeshes(item);

            #endregion

            #region Modifier Bits

            string[] imodbits = item.ModBits.Trim('|').Split('|');

            if (groupBox_4_gb.Controls.Count == 0)
            {
                HeaderVariable[] headerVars = Item.GetHeaderIMODBITS();
                string[] headerVarNames = new string[headerVars.Length];
                for (int i = 0; i < headerVars.Length; i++)
                    headerVarNames[i] = headerVars[i].VariableName;
                InitializeGroupBoxCheckBoxesByArray_Create(groupBox_4_gb, headerVarNames, imodbits);
            }
            else
                InitializeGroupBoxCheckBoxesByArray_Change(groupBox_4_gb, imodbits);

            #endregion

            #region Price & Stats

            price_num.Value = item.Price;
            weight_num.Value = (decimal)item.Weight;//FKZ
            abundance_num.Value = item.Abundance;
            head_armor_num.Value = item.HeadArmor;
            body_armor_num.Value = item.BodyArmor;
            leg_armor_num.Value = item.LegArmor;
            difficulty_num.Value = item.Difficulty;
            hit_points_num.Value = item.HitPoints;
            speed_rating_num.Value = item.SpeedRating;
            missile_speed_num.Value = item.MissileSpeed;
            weapon_length_num.Value = item.WeaponLength;
            max_ammo_num.Value = item.MaxAmmo;

            int thrustDamage = item.ThrustDamage;
            thrust_damage_num.Value = 0xff & thrustDamage;
            thrust_damage_type_cbb.SelectedIndex = 0x3 & (thrustDamage >> 8);

            int swingDamage = item.SwingDamage;
            swing_damage_num.Value = swingDamage;
            swing_damage_type_cbb.SelectedIndex = 0x3 & (swingDamage >> 8);

            // später mit for schleife und index als tag
            //int[] stats = item.ItemStats;
            //
            // Label Texte anpassen für spezielle Item Types
            //

            #endregion

            #region Faction

            string[] facs = new string[item.Factions.Count];

            for (int i = 0; i < facs.Length; i++)
                facs[i] = CodeReader.Factions[item.Factions[i]];

            if (groupBox_6_gb.Controls.Count == 0)
                InitializeGroupBoxCheckBoxesByArray_Create(groupBox_6_gb, CodeReader.Factions, facs);
            else
                InitializeGroupBoxCheckBoxesByArray_Change(groupBox_6_gb, facs);

            #endregion

            #region Trigger

            itemTrigger.Clear();
            condition_cbb.Items.Clear();
            condition_cbb.Text = "None";

            string[] trigger = item.Triggers.ToArray();
            if (trigger.Length > 0)
            {
                foreach (string t in trigger)
                {
                    string[] scriptLines = t.Split();
                    SimpleTrigger simpleTrigger = new SimpleTrigger(double.Parse(CodeReader.Repl_DotWComma(scriptLines[0])));
                    string[] tmp = new string[int.Parse(scriptLines[1]) + 1];
                    tmp[0] = "SIMPLE_TRIGGER";
                    scriptLines = CodeReader.GetStringArrayStartFromIndex(scriptLines, 1);
                    simpleTrigger.ConsequencesBlock = CodeReader.GetStringArrayStartFromIndex(CodeReader.DecompileScriptCode(tmp, scriptLines), 1);

                    itemTrigger.Add(simpleTrigger);

                    string cond = simpleTrigger.CheckInterval;
                    if (!ImportantMethods.IsNumeric(cond, true))
                        condition_cbb.Items.Add(ImportantMethods.ToUpperAfterBlank(cond.Substring(cond.IndexOf('_') + 1).Replace('_', ' ')));
                    else
                        condition_cbb.Items.Add(cond);
                }

                condition_cbb.SelectedIndex = 0;
            }

            #endregion
        }

        #region Setup Meshes

        private void SetupMeshes(Item item, bool normal = true)
        {
            int actualCount = GetMeshesCount() + 1;
            int meshCount = item.Meshes.Count;
            int ccc = meshCount;
            int lastTopLocation = 0;
            bool openChange = groupBox_3_gb.Height != GROUP_HEIGHT_MIN;

            if (!normal)
                //if (actualCount > ccc)
                    ccc = actualCount;

            actualCount--;

            if (openChange)
                showGroup_3_btn.PerformClick();

            showGroup_3_btn.Tag = meshTag + ccc * MESH_CONTROLS_TOP_HEIGHT;

            if (normal)
                for (int i = 0; i < meshCount; i++)
                    lastTopLocation = AddMeshControls(item, i);
            else
                lastTopLocation = AddMeshControls(item, actualCount, false);

            addMesh_btn.Top = lastTopLocation + MESH_CONTROLS_TOP_HEIGHT;

            if (openChange)
                showGroup_3_btn.PerformClick();

            show3D_Override = false;
        }

        private int AddMeshControls(Item item, int i, bool normal = true)
        {
            int lastTopLocation = MESH_CONTROLS_TOP_DEFAULT + MESH_CONTROLS_TOP_HEIGHT - 6 + i * MESH_CONTROLS_TOP_HEIGHT;
            string[] tmp;

            if (normal)
                tmp = item.Meshes[i].Split();
            else
                tmp = new string[] { "invalid_item", "0" }; // or string.Empty, "0"

            // ixmesh_lbl
            Label ixmesh_lbl = new Label
            {
                //AutoSize = true,
                Size = new Size(id_column_lbl.Width - 8, MESH_CONTROLS_TOP_HEIGHT - 4),
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
                Location = new Point(id_column_lbl.Left + 4, lastTopLocation),
                Name = "ixmesh_" + i + "_lbl",
                //Text = "Extra Mesh " + i + ":",
                Text = i.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
            };

            // ixmesh_txt
            TextBox ixmesh_txt = new TextBox
            {
                Location = new Point(ixmesh_lbl.Left + ixmesh_lbl.Width + 8, lastTopLocation + 1),
                Name = "ixmesh_" + i + "_txt",
                Size = new Size(resourceName_column_lbl.Width - 8, MESH_CONTROLS_TOP_HEIGHT - 7),
                Text = tmp[0],
            };

            // ixmesh_cbb
            ComboBox ixmesh_cbb = new ComboBox
            {
                BackColor = Color.FromArgb(56, 56, 56),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.WhiteSmoke,
                FormattingEnabled = true,
                ItemHeight = 20,
                Location = new Point(ixmesh_txt.Left + ixmesh_txt.Width + 8, lastTopLocation),
                Name = "ixmesh_" + i + "_cbb",
                Size = new Size(meshKind_column_lbl.Width - 12, MESH_CONTROLS_TOP_HEIGHT - 4),
            };
            ixmesh_cbb.Items.AddRange(new object[] {
                    "none",
                    "inventory",
                    "flying_ammo",
                    "carry"
                }
            );

            string meshKind = Item.GetMeshKindFromValue(SkillHunter.Dec2Hex_16CHARS(tmp[1]));
            if (!meshKind.Equals("0"))
                ixmesh_cbb.SelectedItem = meshKind.Split('|')[0].Substring(7);
            else
                ixmesh_cbb.SelectedIndex = 0;
            //ixmesh_cbb.SelectedIndex = byte.Parse(SkillHunter.Dec2Hex_16CHARS(tmp[1])[0].ToString());

            if (meshKind.Contains("|"))
                meshKind = meshKind.Substring(meshKind.IndexOf('|') + 1);
            else
                meshKind = string.Empty;

            // ixmesh2_txt
            TextBox ixmesh2_txt = new TextBox
            {
                Location = new Point(ixmesh_cbb.Left + ixmesh_cbb.Width + 8, lastTopLocation + 1),
                Name = "ixmesh2_" + i + "_txt",
                Size = new Size(modifierBits_column_lbl.Width - 8, MESH_CONTROLS_TOP_HEIGHT - 7),
                Text = meshKind,
            };

            CheckBox show_cb = new CheckBox
            {
                Name = "ixmesh_" + i + "_cb",
                Text = string.Empty,
                Tag = i,
                Checked = true,
                Size = new Size(show_column_lbl.Width / 2 - 8, MESH_CONTROLS_TOP_HEIGHT - 6),
                Location = new Point(ixmesh2_txt.Left + ixmesh2_txt.Width + 8 + show_column_lbl.Width / 3, lastTopLocation + 1),
            };
            show_cb.CheckedChanged += Show_cb_CheckedChanged;

            // ixmesh_btn
            Button ixmesh_btn = new Button()
            {
                Name = "ixmesh_" + i + "_btn",
                Text = "X",
                ForeColor = Color.Red,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(delete_column_lbl.Width - 8, MESH_CONTROLS_TOP_HEIGHT - 6),
                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
                Location = new Point(delete_column_lbl.Left + 4, lastTopLocation + 1),
            };
            ixmesh_btn.Click += Ixmesh_btn_Click;

            groupBox_3_gb.Controls.Add(ixmesh_lbl);
            groupBox_3_gb.Controls.Add(ixmesh_txt);
            groupBox_3_gb.Controls.Add(ixmesh_cbb);
            groupBox_3_gb.Controls.Add(ixmesh2_txt);
            groupBox_3_gb.Controls.Add(show_cb);
            groupBox_3_gb.Controls.Add(ixmesh_btn);

            return lastTopLocation;
        }

        private void Show_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (!show3D_Override)
                Change3DView();
        }

        private void Ixmesh_btn_Click(object sender, EventArgs e)
        {
            Control cx = (Control)sender;
            int idx = int.Parse(cx.Name.Split('_')[1]);
            bool openChange = groupBox_3_gb.Height != GROUP_HEIGHT_MIN;
            //List<Control> delControls = new List<Control>(groupBox_3_gb.Controls.OfType<Control>().Where(c => int.Parse(c.Name.Split('_')[1]) == idx));
            //foreach (Control c in delControls)
            //    groupBox_3_gb.Controls.Remove(c);
            string[] nameEnds = new string[] { "lbl", "txt", "cbb", "btn", };
            foreach (string nameEnd in nameEnds)
                groupBox_3_gb.Controls.RemoveByKey("ixmesh_" + idx + "_" + nameEnd);
            foreach (Control cx2 in groupBox_3_gb.Controls)
            {
                string[] nameX = cx2.Name.Split('_');
                if (int.TryParse(nameX[1], out int idxX))
                {
                    if (idx < idxX)
                    {
                        idxX--;
                        if (GetNameEndOfControl(cx2).Equals("lbl"))
                            cx2.Text = "Extra Mesh " + idxX + ":";
                        cx2.Name = nameX[0] + "_" + idxX + "_" + nameX[2];
                        cx2.Top -= MESH_CONTROLS_TOP_HEIGHT;
                    }
                }
            }
            addMesh_btn.Top -= MESH_CONTROLS_TOP_HEIGHT;

            if (openChange)
                showGroup_3_btn.PerformClick();

            showGroup_3_btn.Tag = meshTag + GetMeshesCount() * MESH_CONTROLS_TOP_HEIGHT;

            if (openChange)
                showGroup_3_btn.PerformClick();
        }

        private void AddMesh_btn_Click(object sender, EventArgs e)
        {
            int index = typesIDs.IndexOf(typeSelect_lb.SelectedItem.ToString());
            if (index >= 0 && index < types.Count)
            {
                Item xitem = (Item)types[index];
                SetupMeshes(xitem, false);
            }
        }

        #endregion

        #endregion

        #region Save

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0] + " "
                + plural_name_txt.Text.Replace(' ','_') + " "
                + GetMeshesCount() + GetMeshes() + " "
                + GetItemProperties() + " "
                + GetCFlags() + " "
                + price_num.Value + " "
                + GetModBits() + " "
                + GetFixedFKZ(CodeReader.Repl_CommaWDot(weight_num.Value.ToString())) + " "
                + abundance_num.Value + " "
                + head_armor_num.Value + " "
                + body_armor_num.Value + " "
                + leg_armor_num.Value + " "
                + difficulty_num.Value + " "
                + hit_points_num.Value + " "
                + speed_rating_num.Value + " "
                + missile_speed_num.Value + " "
                + weapon_length_num.Value + " "
                + max_ammo_num.Value + " "
                + ((int)thrust_damage_num.Value | thrust_damage_type_cbb.SelectedIndex << 8).ToString() + " "
                + ((int)swing_damage_num.Value | swing_damage_type_cbb.SelectedIndex << 8).ToString();

            tmp += ';' + GetFactions();

            tmp += ';' + GetTrigger();

            values.Clear();
            values = new List<string>(tmp.Split(';'));
            string[] valuesX = values.ToArray();
            Item i = new Item(valuesX);

            MB_Studio.SavePseudoCodeByType(i, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, i);
        }

        #region Calculations

        #region Comment

        /*  #equipment slots
            ek_item_0 = 0
            ek_item_1 = 1
            ek_item_2 = 2
            ek_item_3 = 3
            ek_head   = 4
            ek_body   = 5
            ek_foot   = 6
            ek_gloves = 7
            ek_horse  = 8
            ek_food   = 9


            max_inventory_items = 96
            num_equipment_kinds = ek_food + 1
            num_weapon_proficiencies = 7

            #damage types:
            cut    = 0
            pierce = 1
            blunt  = 2


            ibf_armor_mask           = 0x00000000000000000000000ff
            ibf_damage_mask          = 0x00000000000000000000003ff
            ibf_10bit_mask           = 0x00000000000000000000003ff

            ibf_head_armor_bits      = 0
            ibf_body_armor_bits      = 8
            ibf_leg_armor_bits       = 16
            ibf_weight_bits          = 24
            ibf_difficulty_bits      = 32

            ibf_hitpoints_mask       = 0x0000ffff
            ibf_hitpoints_bits       = 40

            #iwf_damage_mask             = 0x10000000000000ff #make sure value is 64 bits so that << will work
            iwf_swing_damage_bits       = 50
            iwf_swing_damage_type_bits  = 58
            iwf_thrust_damage_bits      = 60
            iwf_thrust_damage_type_bits = 68
            iwf_weapon_length_bits      = 70
            iwf_speed_rating_bits       = 80
            iwf_shoot_speed_bits        = 90

            iwf_max_ammo_bits           = 100 # use this for shield endurance too?
            iwf_abundance_bits          = 110
            iwf_accuracy_bits           = 16  #reuse leg_armor for accuracy  
            #iwf_horse_speed_bits        = 8
            #iwf_horse_maneuver_bits     = 16
            #iwf_horse_charge_bits       = 24

            iwf_damage_type_bits = 8


            def weight(x):
              a = int(4.0 * x)
              a = ibf_armor_mask & a
              return (((bignum | a) & ibf_armor_mask) << ibf_weight_bits)

            def get_weight(y):
              a = (y >> ibf_weight_bits) & ibf_armor_mask
              return 0.25 * a 

            def head_armor(x):
              return (((bignum | x) & ibf_armor_mask) << ibf_head_armor_bits)

            def get_head_armor(y):
              return (y >> ibf_head_armor_bits) & ibf_armor_mask

            def body_armor(x):
              return (((bignum | x) & ibf_armor_mask) << ibf_body_armor_bits)

            def get_body_armor(y):
              return (y >> ibf_body_armor_bits) & ibf_armor_mask

            def leg_armor(x):
              return (((bignum | x) & ibf_armor_mask) << ibf_leg_armor_bits)

            def get_leg_armor(y):
              return (y >> ibf_leg_armor_bits) & ibf_armor_mask

            def difficulty(x):
              return (((bignum | x) & ibf_armor_mask) << ibf_difficulty_bits)

            def get_difficulty(y):
              return (y >> ibf_difficulty_bits) & ibf_armor_mask

            def hit_points(x):
              return (((bignum | x) & ibf_hitpoints_mask) << ibf_hitpoints_bits)

            def get_hit_points(y):
              return (y >> ibf_hitpoints_bits) & ibf_hitpoints_mask

            def spd_rtng(x):
            #  return (((bignum | x) & ibf_armor_mask) << iwf_speed_rating_bits)
              return (((bignum | x) & ibf_10bit_mask) << iwf_speed_rating_bits)

            def get_speed_rating(y):
            #  return (y >> iwf_speed_rating_bits) & ibf_armor_mask
              return (y >> iwf_speed_rating_bits) & ibf_10bit_mask

            def shoot_speed(x):
              return (((bignum | x) & ibf_10bit_mask) << iwf_shoot_speed_bits)

            def get_missile_speed(y):
              return (y >> iwf_shoot_speed_bits) & ibf_10bit_mask

            def horse_scale(x):
              return (((bignum | x) & ibf_10bit_mask) << iwf_weapon_length_bits)

            def weapon_length(x):
              return (((bignum | x) & ibf_10bit_mask) << iwf_weapon_length_bits)

            def shield_width(x):
              return weapon_length(x)

            def shield_height(x):
              return shoot_speed(x)

            def get_weapon_length(y):
              return ((y >> iwf_weapon_length_bits) & ibf_10bit_mask)

            def max_ammo(x):
              return (((bignum | x) & ibf_armor_mask) << iwf_max_ammo_bits)

            def get_max_ammo(y):
              return (y >> iwf_max_ammo_bits) & ibf_armor_mask

            def swing_damage(damage,damage_type):
              x = ((damage_type << iwf_damage_type_bits) | (damage & ibf_armor_mask))
              return (((bignum | x) & ibf_damage_mask) << iwf_swing_damage_bits)

            def get_swing_damage(y):
              return (y >> iwf_swing_damage_bits) & ibf_damage_mask

            def thrust_damage(damage,damage_type):
              x = ((damage_type << iwf_damage_type_bits) | (damage & ibf_armor_mask))
              return (((bignum | x) & ibf_damage_mask) << iwf_thrust_damage_bits)

            def get_thrust_damage(y):
              return (y >> iwf_thrust_damage_bits) & ibf_damage_mask

            def horse_speed(x):
              return shoot_speed(x)

            def horse_maneuver(x):
              return spd_rtng(x)

            def horse_charge(x):
              return thrust_damage(x,0)

            def food_quality(x):
              return head_armor(x)

            def abundance(x):
              return (((bignum | x) & ibf_armor_mask) << iwf_abundance_bits)

            def get_abundance(y):
              abnd = (y >> iwf_abundance_bits) & ibf_armor_mask
              if (abnd == 0):
                abnd = 100
              return abnd

            def accuracy(x):
              return leg_armor(x)
        */

        #endregion
        
        private ulong GetItemProperties()
        {
            #region Comment
            /*
            *
            * In skillhunter.dll prüfen ob custom_kill_info berücksichtigt wird!!!
            *
            def custom_kill_info(x): # you have to add ico_custom_x (where x is a number between 1 and 7) mesh in order to display it correctly.
              return (((bignum | x) & (itp_kill_info_mask >> itp_kill_info_bits)) << itp_kill_info_bits)
            */
            #endregion

            ulong itemProperties = (ulong)type_cbb.SelectedIndex; // itemPoints sind imodbits und itp ist item properties!!! umtauschen! :D

            if (attach_cbb.SelectedIndex == 1)
                itemProperties |= 0x0000000000000100;//force_attach_left_hand
            else if (attach_cbb.SelectedIndex == 2)
                itemProperties |= 0x0000000000000200;//force_attach_right_hand
            else if (attach_cbb.SelectedIndex == 3)
                itemProperties |= 0x0000000000000300;//force_attach_left_forearm
            else if (attach_cbb.SelectedIndex == 4)
                itemProperties |= 0x0000000000000f00;//attach_armature or itp_attachment_mask

            if (unique_cb.Checked)
                itemProperties |= 0x0000000000001000;
            if (always_loot_cb.Checked)
                itemProperties |= 0x0000000000002000;
            if (no_parry_cb.Checked)
                itemProperties |= 0x0000000000004000;
            if (default_ammo_cb.Checked)
                itemProperties |= 0x0000000000008000;
            if (merchandise_cb.Checked)
                itemProperties |= 0x0000000000010000;
            if (wooden_attack_cb.Checked)
                itemProperties |= 0x0000000000020000;
            if (wooden_parry_cb.Checked)
                itemProperties |= 0x0000000000040000;
            if (food_cb.Checked)
                itemProperties |= 0x0000000000080000;

            if (cant_reload_on_horseback_cb.Checked)
                itemProperties |= 0x0000000000100000;
            if (two_handed_cb.Checked)
                itemProperties |= 0x0000000000200000;
            if (primary_cb.Checked)
                itemProperties |= 0x0000000000400000;
            if (secondary_cb.Checked)
                itemProperties |= 0x0000000000800000;
            if (covers_legs_cb.Checked|| doesnt_cover_hair_cb.Checked|| can_penetrate_shield_cb.Checked)
                itemProperties |= 0x0000000001000000;
            if (consumable_cb.Checked)
                itemProperties |= 0x0000000002000000;
            if (bonus_against_shield_cb.Checked)
                itemProperties |= 0x0000000004000000;
            if (penalty_with_shield_cb.Checked)
                itemProperties |= 0x0000000008000000;
            if (cant_use_on_horseback_cb.Checked)
                itemProperties |= 0x0000000010000000;
            if (civilian_cb.Checked || next_item_as_melee_cb.Checked)
                itemProperties |= 0x0000000020000000;
            if (fit_to_head_cb.Checked || offset_lance_cb.Checked)
                itemProperties |= 0x0000000040000000;
            if (covers_head_cb.Checked || couchable_cb.Checked)
                itemProperties |= 0x0000000080000000;
            if (crush_through_cb.Checked)
                itemProperties |= 0x0000000100000000;
            if (knock_back_cb.Checked)
                itemProperties |= 0x0000000200000000;//being used?
            if (remove_item_on_use_cb.Checked)
                itemProperties |= 0x0000000400000000;
            if (unbalanced_cb.Checked)
                itemProperties |= 0x0000000800000000;

            if (covers_beard_cb.Checked)
                itemProperties |= 0x0000001000000000;//remove beard mesh
            if (no_pick_up_from_ground_cb.Checked)
                itemProperties |= 0x0000002000000000;
            if (can_knock_down_cb.Checked)
                itemProperties |= 0x0000004000000000;
            if (covers_hair_cb.Checked)
                itemProperties |= 0x0000008000000000;//remove hair mesh for armors only

            if (force_show_body_cb.Checked)
                itemProperties |= 0x0000010000000000;// forces showing body (works on body armor items)
            if (force_show_left_hand_cb.Checked)
                itemProperties |= 0x0000020000000000;// forces showing left hand (works on hand armor items)
            if (force_show_right_hand_cb.Checked)
                itemProperties |= 0x0000040000000000;// forces showing right hand (works on hand armor items)
            if (covers_hair_partially_cb.Checked)
                itemProperties |= 0x0000080000000000;

            if (extra_penetration_cb.Checked)
                itemProperties |= 0x0000100000000000;
            if (has_bayonet_cb.Checked)
                itemProperties |= 0x0000200000000000;
            if (cant_reload_while_moving_cb.Checked)
                itemProperties |= 0x0000400000000000;
            if (ignore_gravity_cb.Checked)
                itemProperties |= 0x0000800000000000;
            if (ignore_friction_cb.Checked)
                itemProperties |= 0x0001000000000000;
            if (is_pike_cb.Checked)
                itemProperties |= 0x0002000000000000;
            if (offset_musket_cb.Checked)
                itemProperties |= 0x0004000000000000;
            if (no_blur_cb.Checked)
                itemProperties |= 0x0008000000000000;

            if (cant_reload_while_moving_mounted_cb.Checked)
                itemProperties |= 0x0010000000000000;
            if (has_upper_stab_cb.Checked)
                itemProperties |= 0x0020000000000000;
            if (disable_agent_sounds_cb.Checked)
                itemProperties |= 0x0040000000000000;//disable agent related sounds, but not voices. useful for animals

            if (custom_kill_info_cbb.SelectedIndex > 0)
                itemProperties |= ((ulong)custom_kill_info_cbb.SelectedIndex & 0x7) << 56;//custom_ico_id

            return itemProperties;
        }

        private ulong GetCFlags()
        {
            #region Comment
            /*
            #combined capabilities                                                    // NA - COMBOBOX ???
            itc_cleaver = itcf_force_64_bits | (itcf_overswing_onehanded|itcf_slashright_onehanded|itcf_slashleft_onehanded |
                                                itcf_horseback_slashright_onehanded|itcf_horseback_slashleft_onehanded)
            itc_dagger  = itc_cleaver | itcf_thrust_onehanded

            itc_parry_onehanded = itcf_force_64_bits | itcf_parry_forward_onehanded| itcf_parry_up_onehanded | itcf_parry_right_onehanded |itcf_parry_left_onehanded
            itc_longsword = itc_dagger | itc_parry_onehanded
            itc_scimitar  = itc_cleaver | itc_parry_onehanded

            itc_parry_two_handed = itcf_force_64_bits | itcf_parry_forward_twohanded | itcf_parry_up_twohanded | itcf_parry_right_twohanded | itcf_parry_left_twohanded
            itc_cut_two_handed = itcf_force_64_bits | (itcf_slashright_twohanded | itcf_slashleft_twohanded | itcf_overswing_twohanded | 
                                                       itcf_horseback_slashright_onehanded|itcf_horseback_slashleft_onehanded)
            itc_greatsword = itc_cut_two_handed |  itcf_thrust_twohanded | itc_parry_two_handed |itcf_thrust_onehanded_lance
            itc_nodachi    = itc_cut_two_handed | itc_parry_two_handed

            itc_bastardsword = itc_cut_two_handed |  itcf_thrust_twohanded | itc_parry_two_handed |itc_dagger
            itc_morningstar = itc_cut_two_handed |  itc_parry_two_handed |itc_cleaver

            itc_parry_polearm = itcf_parry_forward_polearm | itcf_parry_up_polearm | itcf_parry_right_polearm | itcf_parry_left_polearm
            itc_poleaxe    = itc_parry_polearm| itcf_overswing_polearm |itcf_thrust_polearm|itcf_slashright_polearm|itcf_slashleft_polearm
            itc_staff      = itc_parry_polearm| itcf_thrust_onehanded_lance |itcf_thrust_onehanded_lance_horseback| itcf_overswing_polearm |itcf_thrust_polearm|itcf_slashright_polearm|itcf_slashleft_polearm
            itc_spear      = itc_parry_polearm| itcf_thrust_onehanded_lance |itcf_thrust_onehanded_lance_horseback | itcf_thrust_polearm
            itc_cutting_spear = itc_spear|itcf_overswing_polearm
            itc_pike       = itcf_thrust_onehanded_lance |itcf_thrust_onehanded_lance_horseback | itcf_thrust_polearm
            itc_guandao    = itc_parry_polearm|itcf_overswing_polearm|itcf_thrust_polearm|itcf_slashright_polearm|itcf_slashleft_polearm|itcf_horseback_slashright_onehanded|itcf_horseback_slashleft_onehanded|itcf_horseback_slash_polearm

            itc_greatlance = itcf_thrust_onehanded_lance |itcf_thrust_onehanded_lance_horseback| itcf_thrust_polearm
            itc_musket_melee = itc_parry_polearm|itcf_overswing_musket|itcf_thrust_musket|itcf_slashright_twohanded|itcf_slashleft_twohanded
             */
            #endregion

            Item item = (Item)types[type_cbb.SelectedIndex];

            ulong cflags = 0;

            if (itcf_thrust_onehanded_cb.Checked)
                cflags |= 0x0000000000000001;
            if (itcf_overswing_onehanded_cb.Checked)
                cflags |= 0x0000000000000002;
            if (itcf_slashright_onehanded_cb.Checked)
                cflags |= 0x0000000000000004;
            if (itcf_slashleft_onehanded_cb.Checked)
                cflags |= 0x0000000000000008;

            if (itcf_thrust_twohanded_cb.Checked)
                cflags |= 0x0000000000000010;
            if (itcf_overswing_twohanded_cb.Checked)
                cflags |= 0x0000000000000020;
            if (itcf_slashright_twohanded_cb.Checked)
                cflags |= 0x0000000000000040;
            if (itcf_slashleft_twohanded_cb.Checked)
                cflags |= 0x0000000000000080;

            if (itcf_thrust_polearm_cb.Checked)
                cflags |= 0x0000000000000100;
            if (itcf_overswing_polearm_cb.Checked)
                cflags |= 0x0000000000000200;
            if (itcf_slashright_polearm_cb.Checked)
                cflags |= 0x0000000000000400;
            if (itcf_slashleft_polearm_cb.Checked)
                cflags |= 0x0000000000000800;

            if (itcf_shoot_bow_rb.Checked)
                cflags |= 0x0000000000001000;
            if (itcf_shoot_javelin_rb.Checked)
                cflags |= 0x0000000000002000;
            if (itcf_shoot_crossbow_rb.Checked)
                cflags |= 0x0000000000004000;

            if (itcf_throw_stone_rb.Checked)
                cflags |= 0x0000000000010000;
            if (itcf_throw_knife_rb.Checked)
                cflags |= 0x0000000000020000;
            if (itcf_throw_axe_rb.Checked)
                cflags |= 0x0000000000030000;
            if (itcf_throw_javelin_rb.Checked)
                cflags |= 0x0000000000040000;
            if (itcf_shoot_pistol_rb.Checked)
                cflags |= 0x0000000000070000;
            if (itcf_shoot_musket_rb.Checked)
                cflags |= 0x0000000000080000;

            if (itcf_horseback_thrust_onehanded_cb.Checked)
                cflags |= 0x0000000000100000;
            if (itcf_horseback_overswing_right_onehanded_cb.Checked)
                cflags |= 0x0000000000200000;
            if (itcf_horseback_overswing_left_onehanded_cb.Checked)
                cflags |= 0x0000000000400000;
            if (itcf_horseback_slashright_onehanded_cb.Checked)
                cflags |= 0x0000000000800000;
            if (itcf_horseback_slashleft_onehanded_cb.Checked)
                cflags |= 0x0000000001000000;
            if (itcf_thrust_onehanded_lance_cb.Checked)
                cflags |= 0x0000000004000000;
            if (itcf_thrust_onehanded_lance_horseback_cb.Checked)
                cflags |= 0x0000000008000000;

            foreach (Control c in itcf_carry_gb.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (!nameEnd.Equals("gb"))
                {
                    if (((RadioButton)c).Checked)
                        cflags |= ulong.Parse(c.Tag.ToString()) << 28;
                }
                else
                {
                    foreach (Control c2 in ((GroupBox)c).Controls)
                        if (((RadioButton)c2).Checked)
                            cflags |= ulong.Parse(c2.Tag.ToString()) << 28;
                }
            }

            if (itcf_show_holster_when_drawn_cb.Checked)
                cflags |= 0x0000000800000000;

            if (itcf_reload_pistol_cb.Checked)
                cflags |= 0x0000007000000000;
            if (itcf_reload_musket_cb.Checked)
                cflags |= 0x0000008000000000;

            if (itcf_parry_forward_onehanded_cb.Checked)
                cflags |= 0x0000010000000000;
            if (itcf_parry_up_onehanded_cb.Checked)
                cflags |= 0x0000020000000000;
            if (itcf_parry_right_onehanded_cb.Checked)
                cflags |= 0x0000040000000000;
            if (itcf_parry_left_onehanded_cb.Checked)
                cflags |= 0x0000080000000000;

            if (itcf_parry_forward_twohanded_cb.Checked)
                cflags |= 0x0000100000000000;
            if (itcf_parry_up_twohanded_cb.Checked)
                cflags |= 0x0000200000000000;
            if (itcf_parry_right_twohanded_cb.Checked)
                cflags |= 0x0000400000000000;
            if (itcf_parry_left_twohanded_cb.Checked)
                cflags |= 0x0000800000000000;

            if (itcf_parry_forward_polearm_cb.Checked)
                cflags |= 0x0001000000000000;
            if (itcf_parry_up_polearm_cb.Checked)
                cflags |= 0x0002000000000000;
            if (itcf_parry_right_polearm_cb.Checked)
                cflags |= 0x0004000000000000;
            if (itcf_parry_left_polearm_cb.Checked)
                cflags |= 0x0008000000000000;

            if (itcf_horseback_slash_polearm_cb.Checked)
                cflags |= 0x0010000000000000;
            if (itcf_overswing_spear_cb.Checked)
                cflags |= 0x0020000000000000;
            if (itcf_overswing_musket_cb.Checked)
                cflags |= 0x0040000000000000;
            if (itcf_thrust_musket_cb.Checked)
                cflags |= 0x0080000000000000;

            if (itcf_force_64_bits_cb.Checked)
                cflags |= 0x8000000000000000;

            /*
             * Die kombinierten Flags in seperater groupbox abhandeln?
            */

            return cflags;
        }

        private string GetMeshes()
        {
            ulong meshKindValue;
            int meshCount = GetMeshesCount();
            string tmp = string.Empty;
            for (int idx = 0; idx < meshCount; idx++)
            {
                meshKindValue = 0ul;
                foreach (Control c in groupBox_3_gb.Controls)
                {
                    if (int.TryParse(c.Name.Split('_')[1], out int index))
                    {
                        if (index == idx)
                        {
                            string nameEnd = GetNameEndOfControl(c);
                            if (nameEnd.Equals("txt"))
                            {
                                if (!c.Name.Split('_')[0].EndsWith("2"))
                                    tmp += ' ' + c.Text + ' ';
                                else
                                    tmp += (meshKindValue | GetModBitStringToValue(c.Text)).ToString();
                            }
                            else if (nameEnd.Equals("cbb"))
                                meshKindValue = HexToUInt64(((ComboBox)c).SelectedIndex + Item.ZERO_15_CHARS);
                            //else if (nameEnd.Equals("cbb"))
                            //    tmp += SkillHunter.Hex2Dec_16CHARS(((ComboBox)c).SelectedIndex.ToString() + Item.ZERO_15_CHARS).ToString() + ' ';
                        }
                    }
                }
            }
            return tmp;
        }

        private ulong GetModBits()
        {
            ulong modbits = 0ul;

            HeaderVariable[] headerVars = Item.GetHeaderIMODBITS();
            foreach (HeaderVariable var in headerVars)
                if (((CheckBox)groupBox_4_gb.Controls.Find(var.VariableName + "_cb", false)[0]).Checked)
                    modbits |= HexToUInt64(var.VariableValue);

            return modbits;
        }

        private string GetFactions()
        {
            byte x = 0;
            string facS = string.Empty;

            List<string> codeFacs = new List<string>(CodeReader.Factions);
            foreach (CheckBox c in groupBox_6_gb.Controls)
            {
                if (c.Checked)
                {
                    facS += codeFacs.IndexOf(c.Name.Remove(c.Name.LastIndexOf('_'))) + " ";
                    x++;
                }
            }

            if (x > 0)
                facS = x + ";" + facS;
            else
                facS = x.ToString();

            return facS;
        }

        private string GetFixedFKZ(string s)
        {
            if (!s.Contains("."))
                s += ".";
            string[] tmp = s.Split('.');
            int count = tmp[1].Length;
            for (int i = 0; i < (6 - count); i++)
                s += "0";
            return s;
        }

        private string GetTrigger()
        {
            string triggerS = itemTrigger.Count.ToString();
            TriggerSelector triggerSelector = new TriggerSelector(ObjectType);
            List<string> list = new List<string>(triggerSelector.TriggerNames);

            foreach (SimpleTrigger strigger in itemTrigger)
            {
                triggerS += ";";
                if (!ImportantMethods.IsNumeric(strigger.CheckInterval, true))
                {
                    if (list.Contains(strigger.CheckInterval))
                        triggerS += GetFixedFKZ(triggerSelector.TriggerCheckIntervals[list.IndexOf(strigger.CheckInterval)].ToString());
                    else
                        MessageBox.Show("ERROR! - 0x9915" + Environment.NewLine + strigger.CheckInterval, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                    triggerS += GetFixedFKZ(strigger.CheckInterval);
                triggerS += CodeReader.GetCompiledCodeLines(strigger.ConsequencesBlock);
            }

            return triggerS;
        }

        #endregion

        #endregion

        #region Control Events

        private void Consequence_rtb_TextChanged(object sender, EventArgs e)
        {
            string[] lines = consequence_rtb.Lines;
            itemTrigger[condition_cbb.SelectedIndex].ConsequencesBlock = lines;
            codeLines_b_lbl.Text = lines.Length.ToString();
        }

        private void AddTrigger_btn_Click(object sender, EventArgs e)
        {
            TriggerSelector selector = new TriggerSelector(ObjectType);
            selector.ShowDialog();

            string trigger = selector.SelectedTrigger;
            if (trigger.Length != 0)
            {
                bool found = false;
                foreach (string listitem in condition_cbb.Items)
                    if (listitem.Equals(trigger))
                        found = true;
                if (!found)
                {
                    SimpleTrigger simpleTrigger = new SimpleTrigger(selector.SelectedCheckInterval);
                    itemTrigger.Add(simpleTrigger);
                    condition_cbb.Items.Add(selector.SelectedTrigger);
                    condition_cbb.SelectedIndex = 0;
                }
                else
                    MessageBox.Show("\"" + trigger + "\" is already in use! Please add code to the given trigger!",
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1
                                    );
            }
        }

        private void Condition_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = condition_cbb.SelectedIndex;
            if (index >= 0 && itemTrigger.Count > index)
                consequence_rtb.Lines = itemTrigger[index].ConsequencesBlock;
        }

        #endregion

        #region USEFUL METHODS

        private static ulong HexToUInt64(string value)
        {
            return ulong.Parse(SkillHunter.Hex2Dec_16CHARS(value).ToString());
        }

        private ulong GetModBitStringToValue(string modbitString)
        {
            ulong value = 0ul;
            HeaderVariable[] headerVars = Item.GetHeaderIMODBITS();
            string[] tmp = modbitString.Trim(' ', '|').Split('|');

            foreach (HeaderVariable var in headerVars)
            {
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (var.VariableName.Equals(tmp[i]))
                    {
                        value |= HexToUInt64(var.VariableValue);
                        i = tmp.Length;
                    }
                }
            }

            return value;
        }

        private int GetMeshesCount()
        {
            return (groupBox_3_gb.Controls.Count - MESH_INFO_CONTROLS_COUNT) / MESH_CONTROLS_COUNT;
        }

        private void InitializeGroupBoxCheckBoxesByArray_Change(GroupBox groupBox, string[] actualValues)
        {
            foreach (string aValue in actualValues)
            {
                foreach (CheckBox c in groupBox.Controls)
                {
                    if (c.Name.Remove(c.Name.LastIndexOf('_')).Equals(aValue))
                    {
                        c.Checked = true;
                        c.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void InitializeGroupBoxCheckBoxesByArray_Create(GroupBox groupBox, string[] values, string[] actualValues = null)
        {
            int topD = 24;
            int top = topD;
            int leftD = 12;
            int left = leftD;
            int cHeight = 0;

            List<string> list;
            if (actualValues != null)
                list = new List<string>(actualValues);
            else
                list = new List<string>();

            for (int i = 0; i < values.Length; i++)
            {
                CheckBox checkBox = new CheckBox
                {
                    AutoSize = true,
                    Name = values[i] + "_cb",
                    Text = ImportantMethods.ToUpperAfterBlank(values[i].Substring(values[i].IndexOf('_') + 1).Replace('_', ' ')),
                    Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    Location = new Point(left, top),
                    Checked = list.Contains(values[i]),
                };

                if (checkBox.Checked)
                    checkBox.ForeColor = Color.Red;

                cHeight = checkBox.Height;

                groupBox.Controls.Add(checkBox);

                if (left + checkBox.Width > (groupBox.Width - 8 * leftD))
                {
                    left = leftD;
                    top += checkBox.Height;
                }
                else
                    left += checkBox.Width;
            }
            string btnName = "showGroup_" + groupBox.Name.Split('_')[1] + "_btn";
            Controls.Find(btnName, true)[0].Tag = top + cHeight - GROUP_HEIGHT_DIF;
        }

        #endregion

        #region OpenBrf

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (openBrfManager != null)
                KillOpenBrfThread();

            base.OnHandleDestroyed(e);
        }

        //[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        private void KillOpenBrfThread()
        {
            openBrfManager.Close();
            //Console.WriteLine("openBrfThread.IsAlive: " + openBrfThread.IsAlive);
        }

        private void StartOpenBrfManager()
        {
            if (openBrfManager == null && _3DView_btn.Enabled)
                Invoke((MethodInvoker)delegate { _3DView_btn.PerformClick(); });
            else if (_3DView_btn.Enabled)
            {
                Invoke((MethodInvoker)delegate { _3DView_btn.Enabled = false; });
                Thread t = new Thread(new ThreadStart(AddOpenBrfAsChildThread)) { IsBackground = true };
                t.Start();
            }
        }

        private void AddOpenBrfAsChildThread()
        {
            while (!openBrfManager.IsShown)
                Thread.Sleep(10);
            Invoke((MethodInvoker)delegate
            {
                openBrfManager.AddWindowHandleToControlsParent(this);

                Thread.Sleep(50);

                int idx = typesIDs.IndexOf(typeSelect_lb.SelectedItem.ToString());
                if (idx >= 0)
                    LoadMeshWithOpenBrf(((Item)types[idx]).Meshes[0].Split()[0]);

                // Update UI
                Invoke(new UpdateUIDelegate(UpdateUI), new object[] { true });

                Console.WriteLine("Loaded 3D View successfully! - laut Programmablauf");
            });
        }

        private void LoadMeshWithOpenBrf(string resourceName)
        {
            try
            {
                /*Console.WriteLine("SUCCESS: " + */openBrfManager.AddMeshToTroopDummy(resourceName);//SelectItemNameByKind(resourceName));
                Thread.Sleep(500);
                openBrfManager.SelectItemNameByKind("JSYS");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowGroup_3_btn_Click(object sender, EventArgs e)
        {
            if (showGroup_3_btn.Text.Equals("v"))
                StartOpenBrfManager();
        }

        private void _3DView_btn_Click(object sender, EventArgs e)
        {
            //Button _3DView_btn = (Button)sender;
            if (openBrfManager == null && _3DView_btn.Enabled)
            {
                //_3DView_btn.Text = _3DView_btn.Text.Remove(_3DView_btn.Text.LastIndexOf(' ')) + " Enabled";
                _3DView_btn.Visible = false;

                string mabPath = ProgramConsole.GetModuleInfoPath();
                mabPath = mabPath.Remove(mabPath.IndexOf('%')).TrimEnd('\\');
                mabPath = mabPath.Remove(mabPath.LastIndexOf('\\'));
                openBrfManager = new OpenBrfManager(ProgramConsole.OriginalMod, mabPath);

                showGroup_3_btn.PerformClick();

                Console.WriteLine("DEBUGMODE: " + MB_Studio.DebugMode);
                int result = openBrfManager.Show(MB_Studio.DebugMode);
                Console.WriteLine("OPENBRF_EXIT_CODE:" + result);
            }
        }

        private void TypeSelect_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Change3DView();
        }

        private void Change3DView()
        {
            int idx = typesIDs.IndexOf(typeSelect_lb.SelectedItem.ToString());
            if (idx >= 0 && openBrfManager != null)
            {
                string[] meshes = ((Item)types[idx]).Meshes.ToArray();
                for (int i = 0; i < meshes.Length; i++)
                    if (ShowMesh(i))
                        openBrfManager.SelectItemNameByKind(meshes[i].Split()[0]);
            }
        }

        private bool ShowMesh(int i)
        {
            bool b = false;
            CheckBox[] w = groupBox_3_gb.Controls.OfType<CheckBox>().Where(c => (int)c.Tag == i).ToArray();
            if (w.Length != 0)
                b = w[0].Checked;
            else
            {
                for (int j = 0; j < groupBox_3_gb.Controls.Count; j++)
                {
                    string nameEnd = GetNameEndOfControl(groupBox_3_gb.Controls[j]);
                    if (nameEnd.Equals("cb"))
                    {
                        if ((int)groupBox_3_gb.Controls[j].Tag == i)
                        {
                            b = ((CheckBox)groupBox_3_gb.Controls[j]).Checked;
                            j = groupBox_3_gb.Controls.Count;
                        }
                    }
                }
            }
            return b;
        }

        #endregion
    }
}
