using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using MB_Studio.Main;
using skillhunter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MB_Studio.Manager
{
    public partial class MenuManager : ToolForm
    {
        private bool colorOverrideMode = false;
        private const byte LANGUAGE_EN_GZ = 2;
        private GameMenuOption[] currentGameMenuOptions;
        private MenuDesigner designer;
        private string[] mno_translations;

        public MenuManager() : base(Skriptum.ObjectType.GAME_MENU)
        {
            types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectTypeID); // später vielleicht wieder in ToolForm, falls BUG gehoben!
            InitializeComponent();
        }

        protected override void LoadSettingsAndLists(bool loadSavedTypes = true)
        {
            mno_translations = new string[sbyte.MaxValue + 1];

            base.LoadSettingsAndLists(loadSavedTypes);

            designer = new MenuDesigner();
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();

            importantLib.ImportantMethods.AddWindowHandleToControl(designer.Handle, Parent, Height, Width, Top);

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
            GameMenu menu = (GameMenu)type;
            mno_translations = new string[menu.MenuOptions.Length];
            name_txt.Text = menu.Text;

            base.SetupType(type);

            //int idx = typeSelect_lb.SelectedIndex - 1;
            //if (idx < types.Count && idx >= 0)
            //    designer.UpdateGameMenu((GameMenu)types[idx], menu.TextColor);

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

            //MessageBox.Show(SkillHunter.Dec2Hex_16CHARS(menu.FlagsGZ));
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

            menuOptions_lb.Items.Clear(); // Check why this isn't cleared automaticly

            //if (menuOptions_lb.Items.Count == 0)
                menuOptions_lb.Items.Add("New");

            currentGameMenuOptions = menu.MenuOptions;
            foreach (GameMenuOption mno in currentGameMenuOptions)
                menuOptions_lb.Items.Add(mno.Name);

            menuOptions_lb.SelectedIndex = 0;

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
            tmp += " " + (menuOptions_lb.Items.Count - 1);
            tmp += ';';

            for (int i = 0; i < currentGameMenuOptions.Length; i++)
            {
                tmp += " " + currentGameMenuOptions[i].Name;
                tmp += SourceReader.GetCompiledCodeLines(currentGameMenuOptions[i].ConditionBlock);
                tmp += " " + currentGameMenuOptions[i].Text.Replace(' ', '_');
                tmp += SourceReader.GetCompiledCodeLines(currentGameMenuOptions[i].ConsequenceBlock);
                tmp += " " + currentGameMenuOptions[i].DoorText.Replace(' ', '_') + " ";
            }

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            GameMenu m = new GameMenu(valuesX);

            MB_Studio.SavePseudoCodeByType(m, valuesX);

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
                flags |= ulong.Parse(SkillHunter.Hex2Dec(hexColor_txt.Text).ToString()) << 32;
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

                    alpha_num.Value = int.Parse(SkillHunter.Hex2Dec("000000" + value.Substring(0, 2)).ToString());
                    red_num.Value = int.Parse(SkillHunter.Hex2Dec("000000" + value.Substring(2, 2)).ToString());
                    green_num.Value = int.Parse(SkillHunter.Hex2Dec("000000" + value.Substring(4, 2)).ToString());
                    blue_num.Value = int.Parse(SkillHunter.Hex2Dec("000000" + value.Substring(6, 2)).ToString());

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

        private void SetHexCode()
        {
            if (!colorOverrideMode)
            {
                if (hexColor_txt.Text.Length == 10)
                {
                    string hexCode = "0x";
                    hexCode += SkillHunter.Dec2Hex(alpha_num.Value).Substring(6);
                    hexCode += SkillHunter.Dec2Hex(red_num.Value).Substring(6);
                    hexCode += SkillHunter.Dec2Hex(green_num.Value).Substring(6);
                    hexCode += SkillHunter.Dec2Hex(blue_num.Value).Substring(6);
                    hexColor_txt.Text = hexCode;
                }
            }
            textColor_lbl.BackColor = Color.FromArgb((int)alpha_num.Value, (int)red_num.Value, (int)green_num.Value, (int)blue_num.Value);
        }

        private void MenuOptions_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndex = menuOptions_lb.SelectedIndex;
            if (selIndex > 0)
            {
                selIndex--;

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
            else if (selIndex == 0)
            {
                ResetGroupBox(options_gb);
                mno_Text_text.Text = string.Empty;
                mno_DoorText_text.Text = string.Empty;
            }
        }

        private void PrepareOptionsLanguage()
        {
            string[] tmp;
            bool found;
            int index = typeSelect_lb.SelectedIndex - 1;
            if (index >= 0)
            {
                string filePath = CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT, GetLanguageFromIndex(language_cbb.SelectedIndex));
                GameMenuOption[] mnos = ((GameMenu)types[index]).MenuOptions;
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
                                    if (tmp[0].Equals(mnos[i].Name) && tmp.Length > 1)
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
                //else
                //    System.Windows.Forms.MessageBox.Show("PATH DOESN'T EXIST --> CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT)" + Environment.NewLine + CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT));
            }
        }

        protected override void Language_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Language_cbb_SelectedIndexChanged(sender, e);

            int idx = typeSelect_lb.SelectedIndex - 1;
            if (idx >= 0)
            {
                GameMenu menu = (GameMenu)types[idx];

                if (language_cbb.SelectedIndex == LANGUAGE_EN_GZ && singleNameTranslation_txt.Text.Length == 0)
                    singleNameTranslation_txt.Text = menu.Text.Replace('_', ' ');

                if (language_cbb.SelectedIndex >= 0 && designer != null)
                {
                    designer.UpdateGameMenuText(singleNameTranslation_txt.Text);
                    PrepareOptionsLanguage();
                    string[] a;
                    if (mno_translations[0] != null)
                    {
                        a = CodeReader.GetStringArrayStartFromIndex(mno_translations, 0, mno_translations.Length - menu.MenuOptions.Length);
                    }
                    else
                    {
                        a = new string[menu.MenuOptions.Length];
                        for (int i = 0; i < a.Length; i++)
                            a[i] = menu.MenuOptions[i].Text.Replace('_', ' ');
                    }
                    designer.UpdateGameMenuOptionsTexts(a);
                }
            }
        }

        protected override void Save_translation_btn_Click(object sender, EventArgs e)
        {
            if (language_cbb.SelectedIndex != LANGUAGE_EN_GZ)
            {
                base.Save_translation_btn_Click(sender, e);
            }
            else
            {
                name_txt.Text = singleNameTranslation_txt.Text.Replace('_', ' ');
                //int idx = typeSelect_lb.SelectedIndex - 1;
                //if (idx >= 0)
                //  ((GameMenu)types[idx]).SetText(singleNameTranslation_txt.Text.Replace(' ', '_'));
            }
        }
    }
}
