using importantLib;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects;
using MB_Studio_Library.Objects.Support;
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace MB_Studio.Manager
{
    public partial class MenuManager : ToolForm
    {
        #region Attributes AND Constants

        private bool colorOverrideMode = false;
        private bool mnoOverride = false;

        private const string NEW_OPTION = " < NEW > ";
        private const byte LANGUAGE_EN_GZ = 2;

        private MenuDesigner designer;
        private string[] mno_translations;

        private List<GameMenuOption> currentGameMenuOptions = new List<GameMenuOption>();

        #endregion

        public MenuManager() : base(Skriptum.ObjectType.GameMenu)
        {
            if (DesignMode && LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectTypeID);// ansonsten für alle in Toolform

            InitializeComponent();
        }

        protected override void LoadSettingsAndLists()
        {
            base.LoadSettingsAndLists();

            designer = new MenuDesigner();
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();

            ImportantMethods.AddWindowHandleToControl(designer.Handle, Parent, Height, Width, Top);

            mno_choose_lb.Items.Add(NEW_OPTION);
            mno_ID_text.KeyUp += Mno_ID_text_KeyUp;

            ResetControls();
        }

        protected override Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            return new GameMenu(raw_data);
        }

        #region Setups

        protected override void ResetControls()
        {
            string s = name_txt.Text;

            base.ResetControls();

            name_txt.Text = s;
        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            GameMenu menu = (GameMenu)type;
            name_txt.Text = menu.Text;

            #region Group2 - Flags & Textcolor & Meshname

            meshName_txt.Text = menu.MeshName;

            List<string> flags = new List<string>(menu.Flags.Split('|'));

            if (flags.Contains("mnf_join_battle"))
                mnf_join_battle_cb.Checked = true;
            if (flags.Contains("mnf_auto_enter"))
                mnf_auto_enter_cb.Checked = true;
            if (flags.Contains("mnf_enable_hot_keys"))
                mnf_enable_hot_keys_cb.Checked = true;
            if (flags.Contains("mnf_disable_all_keys"))
                mnf_disable_all_keys_cb.Checked = true;
            if (flags.Contains("mnf_scale_picture"))
                mnf_scale_picture_cb.Checked = true;

            for (int i = 0; i < flags.Count; i++)
            {
                if (flags[i].Contains("menu_text_color"))
                {
                    string tmp = flags[i].Substring(flags[i].IndexOf('(') + 1);
                    tmp = tmp.Remove(tmp.IndexOf(')'));
                    hexColor_txt.Text = tmp;
                    i = flags.Count;
                }
            }

            //MessageBox.Show(HexConverter.Dec2Hex_16CHARS(menu.FlagsGZ));
            /*
            mnf_join_battle            = 0x00000001 #Consider this menu when the player joins a battle
            mnf_auto_enter             = 0x00000010 #Automatically enter the town with the first menu option. 
            mnf_enable_hot_keys        = 0x00000100 #Enables P,I,C keys
            mnf_disable_all_keys       = 0x00000200 #Disables all keys
            mnf_scale_picture          = 0x00001000 #Scale menu picture to offest screen aspect ratio
            def menu_text_color(color):*/

            #endregion

            #region Group3 - Operations Code

            string[] opCode = menu.OperationBlock;
            foreach (string codeLine in opCode)
                opCodes_rtb.AppendText(codeLine + Environment.NewLine);

            #endregion

            #region Group4 - Menu Options

            mno_choose_lb.Items.Clear(); // Check why this isn't cleared automaticly

            currentGameMenuOptions = new List<GameMenuOption>(menu.MenuOptions);
            foreach (GameMenuOption mno in currentGameMenuOptions)
                mno_choose_lb.Items.Add(mno.Name);

            //if (mno_choose_lb.Items.Count == 0)
                mno_choose_lb.Items.Add(NEW_OPTION);

            mno_choose_lb.SelectedIndex = 0;

            #endregion
        }

        #endregion

        #region Save

        protected override void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            string tmp = values[0].Split()[0] + " " + GetFlags();

            //language_cbb.SelectedIndex = LANGUAGE_EN_GZ;
            //tmp += ' ' + singleNameTranslation_txt.Text.Replace(' ', '_');
            tmp += ' ' + name_txt.Text.Replace(' ', '_');

            tmp += ' ' + meshName_txt.Text;
            tmp += SourceReader.GetCompiledCodeLines(opCodes_rtb.Lines);
            tmp += " " + (mno_choose_lb.Items.Count - 1);
            tmp += ';';

            foreach (GameMenuOption c_mno in currentGameMenuOptions)
            {
                tmp += " " + c_mno.Name;
                tmp += SourceReader.GetCompiledCodeLines(c_mno.ConditionBlock);
                tmp += " " + c_mno.Text.Replace(' ', '_');
                tmp += SourceReader.GetCompiledCodeLines(c_mno.ConsequenceBlock);
                tmp += " " + c_mno.DoorText.Replace(' ', '_') + ' ';
            }

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            GameMenu m = new GameMenu(valuesX);

            CodeWriter.SavePseudoCodeByType(m, valuesX);

            base.SaveTypeByIndex(values, selectedIndex, m);
        }

        #region Calculations

        private ulong GetFlags()
        {
            ulong flags = 0ul;

            if (mnf_join_battle_cb.Checked)
                flags |= 0x00000001;
            if (mnf_auto_enter_cb.Checked)
                flags |= 0x00000010;
            if (mnf_enable_hot_keys_cb.Checked)
                flags |= 0x00000100;
            if (mnf_disable_all_keys_cb.Checked)
                flags |= 0x00000200;
            if (mnf_scale_picture_cb.Checked)
                flags |= 0x00001000;

            string hexCode = hexColor_txt.Text;

            if (hexCode.Contains("0x") && !hexColor_txt.ForeColor.Equals(Color.Red))
            {
                hexCode = hexCode.Substring(2);
                flags |= ulong.Parse(HexConverter.Hex2Dec(hexColor_txt.Text).ToString()) << 32;
            }

            return flags;
        }

        #endregion

        #endregion

        private void HexColor_txt_TextChanged(object sender, EventArgs e)
        {
            if (!colorOverrideMode)
            {
                if (IsNumeric(hexColor_txt.Text))
                    hexColor_txt.Text = "0x00000000";

                if (hexColor_txt.Text.Length == 10)
                {
                    hexColor_txt.ForeColor = Color.Black;

                    string value = hexColor_txt.Text.Substring(2);

                    colorOverrideMode = !colorOverrideMode; // true

                    alpha_num.Value = int.Parse(HexConverter.Hex2Dec("000000" + value.Substring(0, 2)).ToString());
                    red_num.Value = int.Parse(HexConverter.Hex2Dec("000000" + value.Substring(2, 2)).ToString());
                    green_num.Value = int.Parse(HexConverter.Hex2Dec("000000" + value.Substring(4, 2)).ToString());
                    blue_num.Value = int.Parse(HexConverter.Hex2Dec("000000" + value.Substring(6, 2)).ToString());

                    if (hexColor_txt.Text.Equals("0x00000000"))// default?
                        textColor_lbl.BackColor = Color.Black;

                    designer.UpdateGameMenuTextColor(textColor_lbl.BackColor);

                    colorOverrideMode = !colorOverrideMode; // false
                }
                else
                    hexColor_txt.ForeColor = Color.Red;
            }
        }

        private void Alpha_num_ValueChanged(object sender, EventArgs e)
        {
            SetHexCode();
        }

        private void Red_num_ValueChanged(object sender, EventArgs e)
        {
            SetHexCode();
        }

        private void Green_num_ValueChanged(object sender, EventArgs e)
        {
            SetHexCode();
        }

        private void Blue_num_ValueChanged(object sender, EventArgs e)
        {
            SetHexCode();
        }

        private void SetHexCode(int alpha = -1, int red = -1, int green = -1, int blue = -1)
        {
            if (alpha < 0)
                alpha = (int)alpha_num.Value;
            if (red < 0)
                red = (int)red_num.Value;
            if (green < 0)
                green = (int)green_num.Value;
            if (blue < 0)
                blue = (int)blue_num.Value;

            if (!colorOverrideMode)
            {
                if (hexColor_txt.Text.Length == 10)
                {

                    string hexCode = "0x";
                    hexCode += HexConverter.Dec2Hex(alpha).Substring(6);
                    hexCode += HexConverter.Dec2Hex(red).Substring(6);
                    hexCode += HexConverter.Dec2Hex(green).Substring(6);
                    hexCode += HexConverter.Dec2Hex(blue).Substring(6);
                    hexColor_txt.Text = hexCode;
                }
                else
                    MessageBox.Show("ERROR - 0x9920 - COLOR_HEX_FORMAT", "ERROR");
            }

            textColor_lbl.BackColor = Color.FromArgb(alpha, red, green, blue);
        }

        private void Mno_ID_text_KeyUp(object sender, KeyEventArgs e)
        {
            string newId = mno_ID_text.Text.Trim();
            if (e.KeyCode == Keys.Enter && newId.Length != 0)
            {
                int idx = mno_choose_lb.SelectedIndex;
                if (idx >= 0 && !mno_choose_lb.SelectedItem.ToString().Equals(NEW_OPTION))
                {
                    GameMenuOption mno = currentGameMenuOptions[idx];
                    mno = CreateGameMenuOption(newId, mno.Text, mno.ConditionBlock, mno.ConsequenceBlock, mno.DoorText);
                    currentGameMenuOptions.RemoveAt(idx);
                    currentGameMenuOptions.Insert(
                        idx,
                        mno
                    );
                    ChangeListBoxItemAtIndex(mno_choose_lb, idx, mno.Name);
                }
            }
        }

        private void ChangeListBoxItemAtIndex(ListBox lb, int index, string item)
        {
            mnoOverride = !mnoOverride;
            lb.Items.RemoveAt(index);
            lb.Items.Insert(index, item);
            mnoOverride = !mnoOverride;
            mno_choose_lb.SelectedIndex = index;
        }

        private void MenuOptions_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mnoOverride)
            {
                int selIndex = mno_choose_lb.SelectedIndex;
                bool isDefaultItem = mno_choose_lb.SelectedItem.ToString().Equals(NEW_OPTION);
                if (selIndex >= 0 && !isDefaultItem)
                {
                    addAndRemoveMenuOption_btn.Text = "REMOVE";

                    mno_ID_text.Text = currentGameMenuOptions[selIndex].Name;
                    mno_Text_text.Text = currentGameMenuOptions[selIndex].Text.Replace('_', ' ');

                    string tmp = string.Empty;

                    foreach (string codeLine in currentGameMenuOptions[selIndex].ConditionBlock)
                        tmp += codeLine + Environment.NewLine;
                    mno_Condition_rtb.Text = tmp;

                    tmp = string.Empty;
                    foreach (string codeLine in currentGameMenuOptions[selIndex].ConsequenceBlock)
                        tmp += codeLine + Environment.NewLine;
                    mno_Consequence_rtb.Text = tmp;

                    mno_DoorText_text.Text = currentGameMenuOptions[selIndex].DoorText.Replace('_', ' ');
                }
                else if (isDefaultItem)
                {
                    ResetGroupBox(options_gb);
                    mno_ID_text.Text = "mno_" + id_txt.Text + "_option_" + (currentGameMenuOptions.Count + 1);
                    mno_Text_text.ResetText();
                    mno_DoorText_text.ResetText();
                    addAndRemoveMenuOption_btn.Text = "ADD";
                }
            }
        }

        private void PrepareOptionsLanguage()
        {
            string[] tmp;
            bool found;
            if (CurrentTypeIndex >= 0)
            {
                string filePath = CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT, GetLanguageFromIndex(language_cbb.SelectedIndex));
                GameMenuOption[] mnos = ((GameMenu)types[CurrentTypeIndex]).MenuOptions;
                mno_translations = new string[mnos.Length];
                if (File.Exists(filePath))
                {
                    for (int i = 0; i < mnos.Length; i++)
                    {
                        tmp = null;
                        found = false;
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            while (!sr.EndOfStream && !found)
                            {
                                tmp = sr.ReadLine().Split('|');
                                if (!found)
                                {
                                    if (tmp[0].Equals(mnos[i].Name) && tmp.Length > 1)//second condition wrong? --> key exists with empty text = empty text for language?
                                    {
                                        mno_translations[i] = tmp[1].Replace('_', ' ');
                                        found = true;
                                    }
                                }
                            }
                        }
                        if (!found)
                            mno_translations[i] = mnos[i].Text.Replace('_', ' ');
                    }
                }
            }
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);

            if (CurrentTypeIndex >= 0)
            {
                GameMenu menu = (GameMenu)types[CurrentTypeIndex];

                if (language_cbb.SelectedIndex == LANGUAGE_EN_GZ && singleNameTranslation_txt.Text.Length == 0)
                    singleNameTranslation_txt.Text = menu.Text.Replace('_', ' ');

                if (language_cbb.SelectedIndex >= 0 && designer != null)
                {
                    designer.UpdateGameMenuText(singleNameTranslation_txt.Text);
                    PrepareOptionsLanguage();

                    string[] a = null;
                    if (mno_translations.Length > 0)
                    {
                        if (mno_translations[0] == null)
                        {
                            a = new string[menu.MenuOptions.Length];
                            for (int i = 0; i < a.Length; i++)
                                a[i] = menu.MenuOptions[i].Text.Replace('_', ' ');
                        }
                    }

                    if (a == null)
                        a = CodeReader.GetStringArrayStartFromIndex(mno_translations, 0, mno_translations.Length - menu.MenuOptions.Length);

                    designer.UpdateGameMenuOptionsTexts(a);
                }
            }
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            if (language_cbb.SelectedIndex != LANGUAGE_EN_GZ)
                base.Save_translation_btn_Click(sender, e);
            else
                name_txt.Text = singleNameTranslation_txt.Text.Replace('_', ' ');
        }

        private void AddAndRemoveMenuOption_btn_Click(object sender, EventArgs e)
        {
            int newIdx = -1;
            string name_id = mno_ID_text.Text.Replace(' ', '_');
            if (addAndRemoveMenuOption_btn.Text.Equals("ADD"))
            {
                newIdx = mno_choose_lb.Items.Count - 1;
                currentGameMenuOptions.Add(
                    CreateGameMenuOption(name_id, mno_Text_text.Text, mno_Condition_rtb.Lines, mno_Consequence_rtb.Lines, mno_DoorText_text.Text)
                );
                mno_choose_lb.Items.Insert(newIdx, name_id);
                mno_choose_lb.SelectedIndex = newIdx;
            }
            else
            {
                for (int i = 0; i < currentGameMenuOptions.Count; i++)
                {
                    if (currentGameMenuOptions[i].Name.Equals(name_id))
                    {
                        newIdx = i;
                        i = currentGameMenuOptions.Count;
                    }
                }
                if (newIdx >= 0)
                {
                    currentGameMenuOptions.RemoveAt(newIdx);
                    mno_choose_lb.Items.RemoveAt(newIdx);
                    if (currentGameMenuOptions.Count == 0)
                        mno_choose_lb.Items.Add(NEW_OPTION);
                    mno_choose_lb.SelectedIndex = 0;
                }
            }
            designer.UpdateGameMenuOptions(currentGameMenuOptions.ToArray());
        }

        private GameMenuOption CreateGameMenuOption(string id, string text, string[] conditionLines, string[] consequenceLines, string doorText)
        {
            List<string> codes = new List<string>() { id.Trim().Replace(' ', '_') };

            string[] tmp = SourceReader.GetCompiledCodeLines(conditionLines).Trim().Split();
            codes.AddRange(tmp);

            codes.Add(text.Replace(' ', '_'));

            tmp = SourceReader.GetCompiledCodeLines(consequenceLines).Trim().Split();
            codes.AddRange(tmp);

            codes.Add(doorText.Replace(' ', '_'));

            return new GameMenuOption(codes.ToArray());
        }

        private void Mno_ID_text_TextChanged(object sender, EventArgs e)
        {
            if (!mnoOverride)
            {
                string id = mno_ID_text.Text.Trim().Replace(' ', '_');

                /*if (id.Length != 0)
                    if (mno_choose_lb.Items.Count != 0)
                        if (!mno_choose_lb.Items[0].ToString().Equals(NEW_OPTION))
                            foreach (string item in mno_choose_lb.Items)
                                if (item.Equals(id))
                                    mno_choose_lb.SelectedItem = item;*/
                                    
                mnoOverride = !mnoOverride;//true
                mno_ID_text.Text = id;
                mnoOverride = !mnoOverride;//false
            }
        }

        private void TextColor_lbl_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            if (colorDialog.Color != null)
            {
                if (!colorDialog.Color.Equals(default(Color)))
                {
                    Color c = colorDialog.Color;
                    SetHexCode(c.A, c.R, c.G, c.B);
                    textColor_lbl.BackColor = c;
                }
            }
        }
    }
}
