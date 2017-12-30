using MB_Decompiler_Library.IO;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using skillhunter;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MB_Studio
{
    public partial class MenuManager : ToolForm
    {
        private bool colorOverrideMode = false;
        private GameMenuOption[] currentGameMenuOptions;
        private MenuDesigner designer;

        public MenuManager() : base(Skriptum.ObjectType.GAME_MENU)
        {
            types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectTypeID); // später vielleicht wieder in ToolForm, falls BUG gehoben!
            InitializeComponent();
        }

        protected override void LoadSettingsAndLists(bool loadSavedTypes = true)
        {
            base.LoadSettingsAndLists(loadSavedTypes);

            designer = new MenuDesigner();
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();

            importantLib.ImportantMethods.AddWindowHandleToControl(designer.Handle, Parent, Height, Width, Top);

            ResetControls();
        }

        #region Setups

        protected override void ResetControls()
        {
            base.ResetControls();


        }

        protected override void SetupType(Skriptum type)
        {
            base.SetupType(type);

            GameMenu menu = (GameMenu)type;

            if (typeSelect_lb.SelectedIndex - 1 < types.Count && typeSelect_lb.SelectedIndex >= 0)
                designer.UpdateGameMenu((GameMenu)types[typeSelect_lb.SelectedIndex - 1], menu.TextColor);

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
            string tmp = values[0] + " " + GetFlags();

            language_cbb.SelectedItem = "en (English)";
            tmp += " " + singleNameTranslation_txt.Text.Replace(' ', '_');

            tmp += " " + meshName_txt.Text;

            tmp += GetNumbersFromCode(mno_Condition_rtb.Lines);

            tmp += " " + (menuOptions_lb.Items.Count - 1);

            tmp += ';';

            for (int i = 0; i < currentGameMenuOptions.Length; i++)
            {
                tmp += " " + currentGameMenuOptions[i].Name;
                tmp += GetNumbersFromCode(currentGameMenuOptions[i].ConditionBlock);
                tmp += " " + currentGameMenuOptions[i].Text.Replace(' ', '_') + " ";
                tmp += GetNumbersFromCode(currentGameMenuOptions[i].ConsequenceBlock);
                tmp += " " + currentGameMenuOptions[i].DoorText.Replace(' ', '_') + " ";
            }

            //tmp += ';';

            values.Clear();
            values = new List<string>(tmp.Split(';'));

            string[] valuesX = values.ToArray();
            foreach (string var in valuesX)
            {
                System.Windows.Forms.MessageBox.Show(var);
            }
            
            //GameMenu m = new GameMenu(valuesX);

            //MB_Studio.SavePseudoCodeByType(m, valuesX);

            //base.SaveTypeByIndex(values, selectedIndex, m);
        }

        private string GetNumbersFromCode(string[] lines)
        {
            string ret = string.Empty;

            ret = " [CODE_GOES_HERE]"; // WRITE COMPILER CLASS IN IO FROM LIBRARY

            return ret;
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

        private void ShowDesigner_btn_Click(object sender, EventArgs e)
        {
            int selectedIndex = typeSelect_lb.SelectedIndex;
            if (selectedIndex + 1 < types.Count && selectedIndex >= 0)
            {
                //maybe check for background_image in script and flags
                new MenuDesigner((GameMenu)types[selectedIndex + 1]).Show();
            }
        }

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

        private void Language_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (language_cbb.SelectedItem.ToString().Equals("en (English)"))
            {
                singleNameTranslation_txt.Text = ((GameMenu)types[typeSelect_lb.SelectedIndex - 1]).Text.Replace('_', ' ');
            }
        }
    }
}
