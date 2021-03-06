using importantLib;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects;
using MB_Studio_Library.Objects.Support;
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace MB_Studio.Manager
{
    public partial class PartyManager : ToolForm
    {
        private bool flagOverride = false;
        private bool setOverride = false;

        private List<int[]> memberValues = new List<int[]>();

        public PartyManager() : base(Skriptum.ObjectType.Party)
        {
            if (DesignMode && LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectTypeID, true);// ansonsten für alle in Toolform

            InitializeComponent();
            foreach (Control c in groupBox_1_gb.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (nameEnd.Equals("cb"))
                    ((CheckBox)c).CheckedChanged += Flags_CheckedChanged;
            }
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();
            foreach (string mapIcon in CodeReader.MapIcons)
                map_icon_cbb.Items.Add(mapIcon);
            foreach (string troop in CodeReader.Troops)
                troops_lb.Items.Add(troop);
            foreach (string pt in CodeReader.PartyTemplates)
                party_template_cbb.Items.Add(pt);
            foreach (string p in CodeReader.Parties)
                ai_target_p_cbb.Items.Add(p);
            foreach (string fac in CodeReader.Factions)
                faction_cbb.Items.Add(fac);
            for (int i = 1; i < CodeReader.GameMenus.Count; i++)//start with 1 correct?
                menuID_cbb.Items.Add(CodeReader.GameMenus[i]);
            ResetControls();
        }

        #region Flags

        private void Flags_CheckedChanged(object sender, EventArgs e)
        {
            if (!setOverride)
            {
                int setID = 0;
                for (int i = 1; i < 4; i++)
                    if (IsSetActive(i))
                        setID = i;
                flagOverride = true;
                if (setID == 0)
                    no_set_rb.Checked = true;
                else if (setID == 1)
                    village_rb.Checked = true;
                else if (setID == 2)
                    castle_rb.Checked = true;
                else
                    town_rb.Checked = true;
                flagOverride = false;
            }
        }

        private bool IsSetActive(int setID)
        {
            bool b = true;
            List<CheckBox> cbs = new List<CheckBox>
            {
                is_static_cb,
                always_visible_cb
            };
            if (setID == 3 || setID == 2)
                cbs.Add(show_faction_cb);
            else if (setID == 1)
                cbs.Add(hide_defenders_cb);
            foreach (CheckBox cb in cbs)
                if (!cb.Checked)
                    b = false;
            if (b)
            {
                if (setID == 1 && !small_label_rb.Checked
                 || setID == 2 && !medium_label_rb.Checked
                 || setID == 3 && !large_label_rb.Checked)
                    b = false;
                if (b)
                    b = !IsNoneSetActive(cbs);
            }
            return b;
        }

        private bool IsNoneSetActive(List<CheckBox> checkBoxes)
        {
            bool b = false;
            foreach (Control c in groupBox_1_gb.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (nameEnd.Equals("cb"))
                {
                    CheckBox cb = (CheckBox)c;
                    if (!checkBoxes.Contains(cb) && cb.Checked)
                        b = true;
                }
            }
            return b;
        }

        private void No_Label_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RadioButton rb in label_gb.Controls)
                rb.Enabled = !rb.Enabled;
        }

        private void SetCenterFlags(bool village = false)
        {
            foreach (Control c in groupBox_1_gb.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (nameEnd.Equals("cb"))
                    ((CheckBox)c).CheckState = CheckState.Unchecked;
            }
            is_static_cb.CheckState = CheckState.Checked;
            always_visible_cb.CheckState = CheckState.Checked;
            if (!village)
                show_faction_cb.CheckState = CheckState.Checked;
            else
                hide_defenders_cb.CheckState = CheckState.Checked;
            ResetMBLabelSize();
        }

        private void ResetMBLabelSize()
        {
            foreach (RadioButton rb in label_gb.Controls)
                rb.Checked = false;
        }

        private void Village_rb_CheckedChanged(object sender, EventArgs e)
        {
            setOverride = true;
            if (village_rb.Checked && !flagOverride)
            {
                SetCenterFlags(true);
                small_label_rb.Checked = true;
            }
            setOverride = false;
        }

        private void Castle_rb_CheckedChanged(object sender, EventArgs e)
        {
            setOverride = true;
            if (castle_rb.Checked && !flagOverride)
            {
                SetCenterFlags();
                medium_label_rb.Checked = true;
            }
            setOverride = false;
        }

        private void Town_rb_CheckedChanged(object sender, EventArgs e)
        {
            setOverride = true;
            if (town_rb.Checked && !flagOverride)
            {
                SetCenterFlags();
                large_label_rb.Checked = true;
            }
            setOverride = false;
        }

        private void None_Set_rb_CheckedChanged(object sender, EventArgs e)
        {
            setOverride = true;
            if (no_set_rb.Checked && !flagOverride)
            {
                ResetMBLabelSize();
                small_label_rb.Checked = true;
            }
            setOverride = false;
        }

        #endregion

        #region Setups

        protected override void ResetControls()
        {
            base.ResetControls();

            no_set_rb.Checked = true;
            small_label_rb.Checked = true;
            stack_troops_lb.Items.Clear();
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            Party party = (Party)type;
            name_txt.Text = party.Name;

            #region GROUP2 - Flags

            List<string> flags = new List<string>(party.Flags.Split('|'));
            List<string> mapicons = new List<string>(CodeReader.MapIcons);

            map_icon_cbb.SelectedIndex = mapicons.IndexOf(flags[0]);

            if (flags.Contains("pf_label_large"))
                large_label_rb.Checked = true;
            else if (flags.Contains("pf_label_medium"))
                medium_label_rb.Checked = true;
            else if (flags.Contains("pf_label_small"))
                small_label_rb.Checked = true;
            if (flags.Contains("pf_no_label"))
                no_label_rb.Checked = true;

            Control c;
            string nameEnd;
            bool found = false;
            foreach (string flag in flags)
            {
                for (int i = 0; i < groupBox_1_gb.Controls.Count; i++)
                {
                    c = groupBox_1_gb.Controls[i];
                    nameEnd = GetNameEndOfControl(c);
                    if (flag.Equals("pf_" + c.Name.Remove(c.Name.LastIndexOf('_'))))
                    {
                        if (nameEnd.Equals("rb"))
                            ((RadioButton)c).Checked = true;
                        else
                            ((CheckBox)c).CheckState = CheckState.Checked;
                        found = true;
                    }
                    else if (flag.Contains("carries_"))
                    {
                        string f = flag.Substring(flag.IndexOf('(') + 1).Split(')')[0];
                        if (flag.Contains("goods"))
                            carries_goods_txt.Text = f;
                        else
                            carries_gold_txt.Text = f;
                        found = true;
                    }
                    if (found)
                        i = groupBox_1_gb.Controls.Count;
                }
            }

            #endregion

            #region GROUP3

            menuID_cbb.SelectedIndex = party.MenuID;
            party_template_cbb.SelectedIndex = party.PartyTemplateID;
            faction_cbb.SelectedIndex = party.FactionID;
            ai_bhvr_cbb.SelectedIndex = party.AIBehavior;
            ai_target_p_cbb.SelectedIndex = party.AITargetParty;

            #endregion

            #region GROUP4

            char[] personality = HexConverter.Dec2Hex(party.Personality).ToString().Substring(5).ToCharArray();
            courage_num.Value = int.Parse(personality[0].ToString());
            aggressiveness_num.Value = int.Parse(personality[1].ToString());
            if (personality[2] != '0')
                banditness_cb.CheckState = CheckState.Checked;
            double[] coords = party.InitialCoordinates;
            x_axis_txt.Text = CodeReader.Repl_CommaWDot(coords[0].ToString());
            y_axis_txt.Text = CodeReader.Repl_CommaWDot(coords[1].ToString());
            direction_in_degrees_txt.Text = CodeReader.Repl_CommaWDot(party.PartyDirectionInDegrees.ToString());

            #endregion

            #region Stack Troops - GROUP5

            memberValues.Clear();
            stack_troops_lb.Items.Clear();
            if (party.Members.Length > 0)
            {
                foreach (PMember member in party.Members)
                {
                    stack_troops_lb.Items.Add(member.Troop);
                    int[] values = new int[3];
                    values[0] = member.MinimumTroops;
                    values[1] = member.MaximumTroops;
                    values[2] = member.Flags;
                    memberValues.Add(values);
                }
                if (party.Members.Length > 0)
                    stack_troops_lb.SelectedIndex = 0;
            }

            #endregion
        }

        #endregion

        #region Save

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = "   " + values[0] + " " + values[1] + " " + GetFlags() + " " + menuID_cbb.SelectedIndex + " " + party_template_cbb.SelectedIndex + " " + faction_cbb.SelectedIndex + " ";
            tmp += GetPersonality() + "  " + ai_bhvr_cbb.SelectedIndex + " " + ai_target_p_cbb.SelectedIndex + "  ";
            tmp += x_axis_txt.Text + " " + y_axis_txt.Text + "      " + stack_troops_lb.Items.Count;

            for (int i = 0; i < stack_troops_lb.Items.Count; i++)
                tmp += " " + CodeReader.Troops.IndexOf("trp_" + stack_troops_lb.Items[i].ToString()) + " " + memberValues[i][0] + " " + memberValues[i][1] + " " + memberValues[i][2];

            //for invalid troops - still needed?
            //for (int i = 0; i < 6 - stack_troops_lb.Items.Count; i++)
            //    values[0] += " -1 0 0 0";
            //for invalid troops

            values.Clear();
            values = new List<string>(tmp.Split()) {
                direction_in_degrees_txt.Text
            };
            string[] valuesX = values.ToArray();
            Party p = new Party(valuesX);

            CodeWriter.SavePseudoCodeByType(p, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, p);
        }

        #region Calculations

        private ushort GetPersonality()
        {
            string aggresiveness = HexConverter.Dec2Hex((ulong)aggressiveness_num.Value).TrimStart('0');
            string courage = HexConverter.Dec2Hex((ulong)courage_num.Value).TrimStart('0');
            byte bandit = 0;
            if (banditness_cb.Checked)
                bandit++;
            string personality = "00000" + bandit + aggresiveness + courage;

            return ushort.Parse(HexConverter.Hex2Dec(personality).ToString());
        }

        private ulong GetFlags()
        {
            ulong flags = 0;
            if (map_icon_cbb.SelectedIndex >= 0)
                flags += (ulong)map_icon_cbb.SelectedIndex;

            if (disabled_cb.Checked)
                flags |= 0x00000100;
            if (is_ship_cb.Checked)
                flags |= 0x00000200;
            if (is_static_cb.Checked)
                flags |= 0x00000400;

            if (medium_label_rb.Checked)
                flags |= 0x00001000;
            if (large_label_rb.Checked)
                flags |= 0x00002000;

            if (always_visible_cb.Checked)
                flags |= 0x00004000;
            if (default_behavior_cb.Checked)
                flags |= 0x00010000;
            if (auto_remove_in_town_cb.Checked)
                flags |= 0x00020000;
            if (quest_party_cb.Checked)
                flags |= 0x00040000;
            if (no_label_rb.Checked)
                flags |= 0x00080000;
            if (limit_members_cb.Checked)
                flags |= 0x00100000;
            if (hide_defenders_cb.Checked)
                flags |= 0x00200000;
            if (show_faction_cb.Checked)
                flags |= 0x00400000;
            //if (hidden_cb.Checked)
            //    flags |= 0x01000000; //#used in the engine, do not overwrite this flag
            if (dont_attack_civilians_cb.Checked)
                flags |= 0x02000000;
            if (civilian_cb.Checked)
                flags |= 0x04000000;

            #region Not Implemented Fully

            /*
pf_carry_goods_bits    = 48
pf_carry_gold_bits     = 56
pf_carry_gold_multiplier = 20
pf_carry_goods_mask    = 0x00ff000000000000
pf_carry_gold_mask     = 0xff00000000000000

def carries_goods(x):
  return (((bignum | x) << 48) & 0x00ff000000000000)
def carries_gold(x):
  if (x > 10000): x =10000
  if (x < 0): x = 0
  return ((big_num | (x / pf_carry_gold_multiplier)) << pf_carry_gold_bits) & pf_carry_gold_mask
            */

            int gold = 0;
            if (ImportantMethods.IsNumericFKZ128(carries_gold_txt.Text))
                gold = int.Parse(carries_gold_txt.Text);
            flags |= (ulong)((gold / 20) << 56) & 0xff00000000000000;

            int goods = 0;
            if (ImportantMethods.IsNumericFKZ128(carries_goods_txt.Text))
                goods = int.Parse(carries_gold_txt.Text);
            flags |= (ulong)(goods << 48) & 0x00ff000000000000;

            #endregion

            return flags;
        }

        #endregion

        #endregion

        #region Stack Troops

        private void Stack_troops_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = stack_troops_lb.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (!stack_troops_lb.SelectedItem.ToString().Equals("None"))
                {
                    stackTroopCount_txt.Text = memberValues[selectedIndex][0].ToString();
                    if (memberValues[selectedIndex][0] != memberValues[selectedIndex][1])
                        stackTroopCount_txt.Text += "-" + memberValues[selectedIndex][1];
                    setOverride = true;
                    if (memberValues[selectedIndex][2] == 1)
                        is_prisoner_cb.CheckState = CheckState.Checked;
                    else if (memberValues[selectedIndex][2] == 0)
                        is_prisoner_cb.CheckState = CheckState.Unchecked;
                    else
                        MessageBox.Show("(1) - UNKNOWN PARTYMEMBER FLAG FOUND: " + memberValues[selectedIndex][2], Application.ProductName);
                    setOverride = false;
                }
            }
        }

        private void StackAddTroop_btn_Click(object sender, EventArgs e)
        {
            stack_troops_lb.Items.Add(troops_lb.SelectedItem);
            memberValues.Add(new int[] { 1, 1, 0 });
        }

        private void Is_prisoner_cb_CheckedChanged(object sender, EventArgs e)
        {
            int selectedIndex = stack_troops_lb.SelectedIndex;
            if (selectedIndex >= 0 && !setOverride)
            {
                if (is_prisoner_cb.Checked && memberValues[selectedIndex][2] == 0)
                    memberValues[selectedIndex][2] = 1;
                else if (!is_prisoner_cb.Checked && memberValues[selectedIndex][2] == 1)
                    memberValues[selectedIndex][2] = 0;
                else
                    MessageBox.Show("(2) -UNKNOWN PARTYMEMBER FLAG FOUND: " + memberValues[selectedIndex][2], Application.ProductName);
            }
        }

        private void StackTroopCount_txt_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = stack_troops_lb.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (stackTroopCount_txt.Text.Length > 0)
                {
                    string[] sp = stackTroopCount_txt.Text.Split('-');
                    if (ImportantMethods.IsNumericFKZ128(sp[0]))
                    {
                        if (sp.Length == 1)
                        {
                            memberValues[selectedIndex][0] = int.Parse(sp[0]);
                            memberValues[selectedIndex][1] = int.Parse(sp[0]);
                        }
                        else if (ImportantMethods.IsNumericFKZ128(sp[1]))
                        {
                            memberValues[selectedIndex][0] = int.Parse(sp[0]);
                            memberValues[selectedIndex][1] = int.Parse(sp[1]);
                        }
                    }
                }
                else
                {
                    memberValues[selectedIndex][0] = 0;
                    memberValues[selectedIndex][1] = 0;
                }
            }
        }

        private void StackRemoveTroop_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = stack_troops_lb.SelectedIndex;
            memberValues.RemoveAt(selectedIndex);
            stack_troops_lb.Items.RemoveAt(selectedIndex);
        }

        private void StackUpTroop_btn_Click(object sender, EventArgs e)
        {
            if (stack_troops_lb.SelectedIndex > 0)
            {
                foreach (int i in stack_troops_lb.SelectedIndices)
                {
                    string tmp = stack_troops_lb.Items[i - 1].ToString();
                    stack_troops_lb.Items[i - 1] = stack_troops_lb.Items[i];
                    stack_troops_lb.Items[i] = tmp;
                    stack_troops_lb.SelectedIndex -= 1; // rethink this
                }
            }
        }

        private void StackDownTroop_btn_Click(object sender, EventArgs e)
        {
            if (stack_troops_lb.SelectedIndex < stack_troops_lb.Items.Count - 1)
            {
                foreach (int i in stack_troops_lb.SelectedIndices)
                {
                    string tmp = stack_troops_lb.Items[i + 1].ToString();
                    stack_troops_lb.Items[i + 1] = stack_troops_lb.Items[i];
                    stack_troops_lb.Items[i] = tmp;
                    stack_troops_lb.SelectedIndex += 1; // rethink this
                }
            }
        }

        #endregion
    }
}
