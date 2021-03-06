using importantLib;
using MB_Studio_CLI;
using MB_Studio_Library.IO;
using MB_Studio_Library.Objects;
using MB_Studio.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static MB_Studio_Library.Objects.Skriptum;
using MB_Studio_Library.Objects.Support;
using System.Drawing;
using System.Globalization;

namespace MB_Studio.Main
{
    public partial class ProjectProperties : SpecialForm
    {
        private int currentSetIndex;
        private List<string> itemsIDs = new List<string>();
        private List<Item> items = new List<Item>();
        private List<ulong> itemsFlags = new List<ulong>();
        private Dictionary<string, string> iniDict = new Dictionary<string, string>();
        private Dictionary<string, string> iniDictDefaults = new Dictionary<string, string>();

        private Panel moduleIniPanel;

        public ProjectProperties()
        {
            InitializeComponent();

            DoubleBuffered = true;
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
        }

        private void ExtraOptions_Load(object sender, EventArgs e)
        {
            title_lbl.Text = Text;

            for (int i = 0; i < CodeReader.Items.Count; i++)
                itemsIDs.Add(i + " - " + CodeReader.Items[i]);

            CodeReader cr = new CodeReader(CodeReader.ModPath + CodeReader.Files[(int)ObjectType.Item]);
            foreach (Item item in cr.ReadItem())
                items.Add(item);

            moduleIniPanel = new Panel
            {
                Width = itemsets_panel.Width,
                Height = itemsets_panel.Height,
                Left = itemsets_panel.Left,
                Top = itemsets_panel.Top,
                AutoScroll = true,
            };
            Controls.Add(moduleIniPanel);
            moduleIniPanel.SendToBack();

            CreateModuleIniPanel();

            ResetControls();
            LoadItemSets();
        }

        private Button saveIni_btn;//move later
        private void CreateModuleIniPanel()
        {
            string curName;
            var iniVars = ReadModuleIniSettings(out int lastKnownIndex);

            saveIni_btn = new Button
            {
                Name = "saveIni_btn",
                Width = 128,
                Height = 32,
                Text = "Save module.ini",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 11f, FontStyle.Bold, GraphicsUnit.Point),
                FlatStyle = FlatStyle.Flat,
            };

            Control valueBox = null;
            for (int i = 0; i < iniVars.Count; i++)
            {
                curName = iniVars[i].VariableName.ToLower();

                Label label = new Label
                {
                    Name = curName + "_lbl",
                    AutoSize = false,
                    Width = 256,
                    Height = 42,
                    Text = iniVars[i].VariableName,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Left = 8,
                    Top = 8,
                    Font = saveIni_btn.Font,
                };
                if (valueBox != null)
                    label.Top = valueBox.Top + valueBox.Height + 4;
                moduleIniPanel.Controls.Add(label);

                if (iniDict.TryGetValue(iniVars[i].VariableName, out string varVal))
                {
                    switch (varVal)
                    {
                        case "Boolean":
                            valueBox = new CheckBox {
                                AutoSize = false,
                                Name = curName + "_cb",
                                Checked = iniVars[i].VariableValue.Equals("1"),
                                Text = string.Empty,
                                Tag = iniVars[i].VariableValue.Equals("1").ToString(),
                                CheckAlign = ContentAlignment.MiddleLeft,
                                Font = new Font("Arial", 13f, FontStyle.Bold),
                            };
                            break;
                        case "UInt16":
                        case "Int16":
                        case "UInt32":
                        case "Int32":
                        case "UInt64":
                        case "Int64":
                        case "Float":
                        case "Double":
                        case "Decimal":
                            valueBox = new NumericUpDown
                            {
                                AutoSize = false,
                                Name = curName + "_num",
                                Minimum = decimal.MinValue,//change later to specific type
                                Maximum = decimal.MaxValue,//change later to specific type
                                Value = decimal.Parse(iniVars[i].VariableValue, NumberStyles.Number),
                                Tag = decimal.Parse(iniVars[i].VariableValue, NumberStyles.Number).ToString(),
                                TextAlign = HorizontalAlignment.Left,
                                Font = new Font("Arial", 13f, FontStyle.Bold),
                            };
                            break;
                        case "String":
                        default:
                            valueBox = new TextBox
                            {
                                Name = curName + "_txt",
                                Text = iniVars[i].VariableValue,
                                Tag = iniVars[i].VariableValue,
                                TextAlign = HorizontalAlignment.Left,
                                Font = new Font("Arial", 13f, FontStyle.Bold),
                                Multiline = true,
                            };
                            break;
                    }

                    valueBox.Width = label.Width/* / 2*/;
                    //valueBox.Height = label.Height;
                    valueBox.Left = label.Left + label.Width + 8;
                    valueBox.Top = label.Top + 8;
                    //valueBox.Font = label.Font;
                    //valueBox.LostFocus += ValueBox_LostFocus;

                    moduleIniPanel.Controls.Add(valueBox);

                    if (i > lastKnownIndex)
                    {
                        CheckBox checkBox = new CheckBox
                        {
                            Name = curName + "_abc",
                            Top = valueBox.Top,
                            Left = valueBox.Left + valueBox.Width + 8,
                            Height = valueBox.Height,
                            Width = 64,
                            Checked = false,
                            FlatStyle = FlatStyle.Flat,
                        };
                        moduleIniPanel.Controls.Add(checkBox);
                    }
                }
            }

            saveIni_btn.Left = moduleIniPanel.Width - saveIni_btn.Width - 32;
            saveIni_btn.Click += SaveIni_btn_Click;
            moduleIniPanel.Controls.Add(saveIni_btn);

            moduleIniPanel.Paint += ModuleIniPanel_Paint;
        }

        private void ModuleIniPanel_Paint(object sender, PaintEventArgs e)
        {
            moduleIniPanel.SuspendLayout();

            saveIni_btn.Top = 8;

            moduleIniPanel.ResumeLayout();
        }

        private void SaveIni_btn_Click(object sender, EventArgs e)
        {
            ValueBoxesSaveAll();
        }

        private void ValueBoxesSaveAll()
        {
            string moduleIniFile = CodeReader.ModPath + "module.ini";
            List<string> iniLines = new List<string>(File.ReadAllLines(moduleIniFile));

            List<string> excludedTypes = new List<string>() { "lbl", "btn", "pb", "panel", "abc" };
            foreach (Control c in moduleIniPanel.Controls)
                if (!excludedTypes.Contains(GetNameEndOfControl(c)))
                    Zeta(c, ref iniLines);

            File.WriteAllLines(moduleIniFile, iniLines);

            MessageBox.Show("Saved data to \"" + moduleIniFile + "\"", "Saved");
        }

        private void Zeta(Control valBox, ref List<string> iniLines)
        {
            string valType = GetNameEndOfControl(valBox);

            string moduleIniFile = CodeReader.ModPath + "module.ini";
            string varName = valBox.Name.Remove(valBox.Name.LastIndexOf('_'));
            try
            {
                var label = moduleIniPanel.Controls.Find(varName + "_lbl", false)[0];
                var abc = moduleIniPanel.Controls.Find(varName + "_abc", false);

                string tmp;
                if (abc.Length == 0)
                {
                    for (int i = 0; i < iniLines.Count; i++)
                    {
                        if (iniLines[i].TrimStart().StartsWith(label.Text))
                        {
                            switch (valType)
                            {
                                case "cb":
                                    tmp = label.Text + " = " + (((CheckBox)valBox).Checked ? '1' : '0');
                                    break;
                                case "num":
                                    tmp = label.Text + " = " + ((NumericUpDown)valBox).Value.ToString();//correct format later
                                    break;
                                case "txt":
                                default:
                                    tmp = label.Text + " = " + valBox.Text.Replace("\t", "    ").Replace(' ', '_');
                                    break;
                            }
                            iniLines[i] = tmp;
                            i = iniLines.Count;
                        }
                    }
                }
                else
                {
                    CheckBox checkBox = (CheckBox)abc[0];
                    if (checkBox.Checked)
                    {
                        switch (valType)
                        {
                            case "cb":
                                tmp = label.Text + " = " + (((CheckBox)valBox).Checked ? '1' : '0');
                                break;
                            case "num":
                                tmp = label.Text + " = " + ((NumericUpDown)valBox).Value.ToString();//correct format later
                                break;
                            case "txt":
                            default:
                                tmp = label.Text + " = " + valBox.Text.Replace("\t", "    ").Replace(' ', '_');
                                break;
                        }
                        iniLines.Add(tmp);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(varName);
            }

            switch (valType)
            {
                case "cb":
                    valBox.Tag = ((CheckBox)valBox).Checked;
                    break;
                case "num":
                    valBox.Tag = ((NumericUpDown)valBox).Value;
                    break;
                case "txt":
                default:
                    valBox.Tag = valBox.Text;
                    break;
            }
        }

        private void InitDictionryForModuleIni()
        {
            string tmp;
            string[] sp;
            int defaultIdx = 0;
            foreach (string s in File.ReadAllLines(CodeReader.FILES_PATH + "module.ini.wdb"))
            {
                tmp = s.Split('#')[0];
                if (tmp.Length != 0)
                {
                    sp = tmp.Split('=', '|');
                    iniDict.Add(sp[0], sp[2]);
                    iniDictDefaults.Add(sp[0], sp[1].TrimStart('[').TrimEnd(']').Split(',')[defaultIdx]);
                }
            }
        }

        private List<HeaderVariable> ReadModuleIniSettings(out int lastKnownIndex)
        {
            if (iniDict.Count == 0)
                InitDictionryForModuleIni();

            List<HeaderVariable> variables = new List<HeaderVariable>();
            using (StreamReader sr = new StreamReader(CodeReader.ModPath + "module.ini"))
            {
                string[] sp;
                string line;
                while (!sr.EndOfStream)
                {
                    line = CodeReader.RemNTrimAllXtraSp(sr.ReadLine().Split('#')[0]); // check wether # not only for comment
                    if (line.Length != 0)
                    {
                        sp = line.Replace(" ", string.Empty).Split('=');
                        line = sp[0].ToUpper();
                        if (!line.Equals("LOAD_RESOURCE") && !line.Equals("LOAD_MOD_RESOURCE"))
                        {
                            if (sp.Length > 1)
                                variables.Add(new HeaderVariable(sp[1], sp[0]));
                            else
                                variables.Add(new HeaderVariable("!VALUE_NOT_FOUND!", sp[0]));

                            if (!iniDict.ContainsKey(sp[0]))
                            {
                                bool isNumericComma;
                                string type = "String";
                                bool isNumeric = IsNumeric(sp[1], true);
                                if (isNumeric)
                                {
                                    type = "Int64";
                                    isNumericComma = IsNumericComma(sp[1]);
                                    if (isNumericComma)
                                        type = "Double";
                                }
                                iniDict.Add(sp[0], type);
                            }
                        }
                    }
                }
            }

            lastKnownIndex = variables.Count - 1;

            foreach (var item in iniDictDefaults)
            {
                bool foundX = false;
                for (int i = 0; i < variables.Count; i++)
                {
                    if (variables[i].VariableName.Equals(item.Key))
                    {
                        foundX = true;
                        i = variables.Count;
                    }
                }
                if (!foundX)
                    variables.Add(new HeaderVariable(item.Value, item.Key));
            }

            return variables;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Min_btn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void LoadItemSets()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            TreeNode[] nodX = new TreeNode[extraOptions_tree.Nodes.Count];
            extraOptions_tree.Nodes.CopyTo(nodX, 0);
            string[] names = new string[Properties.Settings.Default.setNames.Count];
            Properties.Settings.Default.setNames.CopyTo(names, 0);
            for (int i = 1; i <= 6; i++)
                extraOptions_tree.Nodes[1].Nodes[0].Nodes.Find("set_" + i, true)[0].Text = names[i - 1];
            currentSetIndex = 0;
            SetupCurrentSet();
        }

        private void SetupCurrentSet()
        {
            setName_txt.Text = Properties.Settings.Default.setNames[currentSetIndex];
            string[] attribsX = Properties.Settings.Default.setAttribs[currentSetIndex].Split('|');
            string[] itemsX = Properties.Settings.Default.setItems[currentSetIndex].Split('|');
            string[] itemsFlagsX = Properties.Settings.Default.setItemsFlags[currentSetIndex].Split('|');
            headArmor_txt.Text = attribsX[0];
            bodyArmor_txt.Text = attribsX[1];
            legArmor_txt.Text = attribsX[2];
            thrustDamage_txt.Text = attribsX[3];
            swingDamage_txt.Text = attribsX[4];
            usedItems_lb.Items.Clear();
            if (itemsX.Length > 1 || !itemsX[0].Equals("-"))
                if (itemsX.Length > 1 || !itemsX[0].Equals("-"))
                if (itemsX.Length > 1 || !itemsX[0].Equals("-"))
                        if (itemsX.Length > 1 || !itemsX[0].Equals("-"))
                foreach (string item in itemsX)
                    usedItems_lb.Items.Add(itemsIDs[int.Parse(item)]);
            else
                foreach (string item in itemsIDs)
                    usedItems_lb.Items.Add(item);
            itemsFlags.Clear();
            if (itemsFlagsX.Length > 1 || !itemsFlagsX[0].Equals("-"))
                foreach (string itemFlag in itemsFlagsX)
                    itemsFlags.Add(ulong.Parse(HexConverter.Hex2Dec_16CHARS(itemFlag).ToString()));
            else
                for (int i = 0; i < itemsIDs.Count; i++)
                    itemsFlags.Add(0);
            //something_txt.Text = attribs[5].ToString();
        }

        private void ExtraOptions_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            itemsets_panel.Visible = false;
            projects_panel.Visible = false;
            moduleIniPanel.Visible = false;

            if (e.Node.Name.StartsWith("set_"))
            {
                itemsets_panel.Visible = true;
                itemsets_panel.BringToFront();
                currentSetIndex = int.Parse(e.Node.Name.Split('_')[1]) - 1;
                SetupCurrentSet();
            }
            else if (e.Node.Name.StartsWith("proj"))
            {
                projects_panel.Visible = true;
                projects_panel.BringToFront();
                string[] info = ProgramConsole.ReadProjectFileInfoCurrent();
                string modFolder = info[3]; //CodeReader.ModPath.TrimEnd('\\');
                string modules = modFolder.Remove(modFolder.LastIndexOf('\\'));
                foreach (string folder in Directory.GetDirectories(modules))
                {
                    string tmp = folder.Substring(folder.LastIndexOf('\\') + 1);
                    originalMod_cbb.Items.Add(tmp);
                    destinationMod_cbb.Items.Add(tmp);
                }
                projectFolder_txt.Text = info[1];
                originalMod_cbb.SelectedItem = info[2];
                destinationMod_cbb.SelectedItem = info[4];
                copyDefaultVariables_cb.Checked = bool.Parse(info[6]);
            }
            else if (e.Node.Name.StartsWith("gener"))
            {
                new HeaderValueTool().ShowDialog();
            }
            else if (e.Node.Name.StartsWith("module_ini"))
            {
                moduleIniPanel.Visible = true;
                moduleIniPanel.BringToFront();
            }
        }

        private void ResetControls()
        {
            foreach (Control c in itemsets_panel.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (nameEnd.Equals("txt"))
                    c.ResetText();
                else if (nameEnd.Equals("SearchTextBox"))
                    c.Text = "Search ...";
            }
            items_lb.Items.Clear();
            usedItems_lb.Items.Clear();
            for (int i = 0; i < itemsIDs.Count; i++)
                items_lb.Items.Add(itemsIDs[i]);
        }

        private bool IsNumberRange(string s)
        {
            bool b = false;
            string[] sp = s.Split('-');
            if (sp.Length == 2)
                if (IsNumeric(sp[0]) && IsNumeric(sp[1]))
                    b = true;
            return b;
        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            int headArmorMin, headArmorMax, bodyArmorMin, bodyArmorMax, legArmorMin, legArmorMax, thrustDamageMin, thrustDamageMax, swingDamageMin, swingDamageMax;

            #region SetValues

            foreach (Control c in itemsets_panel.Controls)
            {
                string nameEnd = GetNameEndOfControl(c);
                if (nameEnd.Equals("txt"))
                    if (!c.Text.Equals(setName_txt.Text))
                        if (c.Text.Length == 0 || !IsNumberRange(c.Text) && !IsNumeric(c.Text)) 
                            c.Text = "0";
            }

            string[] tmpS = headArmor_txt.Text.Replace(" ", string.Empty).Split('-');

            headArmorMin = int.Parse(tmpS[0]);
            if (tmpS.Length > 1)
                headArmorMax = int.Parse(tmpS[1]);
            else
                headArmorMax = headArmorMin;

            tmpS = bodyArmor_txt.Text.Replace(" ", string.Empty).Split('-');
            bodyArmorMin = int.Parse(tmpS[0]);
            if (tmpS.Length > 1)
                bodyArmorMax = int.Parse(tmpS[1]);
            else
                bodyArmorMax = bodyArmorMin;

            tmpS = legArmor_txt.Text.Replace(" ", string.Empty).Split('-');
            legArmorMin = int.Parse(tmpS[0]);
            if (tmpS.Length > 1)
                legArmorMax = int.Parse(tmpS[1]);
            else
                legArmorMax = legArmorMin;

            tmpS = thrustDamage_txt.Text.Replace(" ", string.Empty).Split('-');
            thrustDamageMin = int.Parse(tmpS[0]);
            if (tmpS.Length > 1)
                thrustDamageMax = int.Parse(tmpS[1]);
            else
                thrustDamageMax = thrustDamageMin;

            tmpS = swingDamage_txt.Text.Replace(" ", string.Empty).Split('-');
            swingDamageMin = int.Parse(tmpS[0]);
            if (tmpS.Length > 1)
                swingDamageMax = int.Parse(tmpS[1]);
            else
                swingDamageMax = swingDamageMin;

            #endregion

            items_lb.Items.Clear();
            foreach (Item item in items)
                if (item.HeadArmor >= headArmorMin && item.HeadArmor <= headArmorMax
                 && item.BodyArmor >= bodyArmorMin && item.BodyArmor <= bodyArmorMax
                 && item.LegArmor >= legArmorMin && item.LegArmor <= legArmorMax
                 && item.ThrustDamage >= thrustDamageMin && item.ThrustDamage <= thrustDamageMax
                 && item.SwingDamage >= swingDamageMin && item.SwingDamage <= swingDamageMax
                 /*&& item.Something >= somethingMin && item.Something <= somethingMax*/
                 )
                    items_lb.Items.Add(itemsIDs[CodeReader.GetItemIndexFromID(item.ID)]);
        }

        private void SearchItems_SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            usedItems_lb.Items.Clear();
            string[] itemsX = Properties.Settings.Default.setItems[currentSetIndex].Split('|');
            if (!itemsX[0].Equals("-"))
                for (int i = 0; i < itemsX.Length; i++)
                    itemsX[i] = itemsIDs[int.Parse(itemsX[i])];
            else
                itemsX = new string[0];
            if (!searchItems_SearchTextBox.Text.Contains("Search ...") && searchItems_SearchTextBox.Text.Length > 0)
            {
                foreach (string item in itemsX)
                    if (item.Replace(" ", string.Empty).Split('-')[1].Contains(searchItems_SearchTextBox.Text))
                        usedItems_lb.Items.Add(item);
            }
            //else if (searchItems_SearchTextBox.Text.Length == 0)
            //    usedItems_lb.Items.Clear();
            else
                foreach (string item in itemsX)
                    usedItems_lb.Items.Add(item);
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            // save to current set
            Properties.Settings.Default.setNames[currentSetIndex] = setName_txt.Text;
            Properties.Settings.Default.setAttribs[currentSetIndex] = headArmor_txt.Text + '|' + bodyArmor_txt.Text + '|' + legArmor_txt.Text
                                                                        + '|' + thrustDamage_txt.Text + '|' + swingDamage_txt.Text + "|0"; // last isn't used yet!
            string itemsString = string.Empty;
            string itemsFlagsString = string.Empty;
            for (int i = 0; i < usedItems_lb.Items.Count; i++)
            {
                itemsString += usedItems_lb.Items[i].ToString().Split('-')[0].TrimEnd() + '|';
                if (i < itemsFlags.Count)
                    itemsFlagsString += HexConverter.Dec2Hex_16CHARS(itemsFlags[i]);
                else
                    itemsFlagsString += '0';
                itemsFlagsString += '|';
            }
            Properties.Settings.Default.setItemsFlags[currentSetIndex] = itemsFlagsString.TrimEnd('|');
            Properties.Settings.Default.setItems[currentSetIndex] = itemsString.TrimEnd('|');
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void Abort_btn_Click(object sender, EventArgs e)
        {
            ResetControls(); // later change to reset to values which are saved!
        }

        private void SearchTextBox_AllItems_TextChanged(object sender, EventArgs e)
        {
            items_lb.Items.Clear();
            if (!searchItems_AllItems_SearchTextBox.Text.Contains("Search ..."))
            {
                foreach (string item in itemsIDs)
                    if (item.Replace(" ", string.Empty).Split('-')[1].Contains(searchItems_AllItems_SearchTextBox.Text))
                        items_lb.Items.Add(item);
            }
            else
                foreach (string item in itemsIDs)
                    items_lb.Items.Add(item);
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            if ((items_lb.SelectedItems.Count + usedItems_lb.Items.Count) <= 64)
            {
                foreach (string item in items_lb.SelectedItems)
                    usedItems_lb.Items.Add(item);
                itemsFlags.Add(0); // check
            }
            else
                MessageBox.Show("You have too many items selected!"
                                + Environment.NewLine + "Only 64 itemslots are available!"
                                + Environment.NewLine + " --> Used itemslots: " + usedItems_lb.Items.Count
                                + Environment.NewLine + " --> Selected items: " + items_lb.SelectedItems.Count);
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
                itemsFlags.RemoveAt(i); // check
            }
        }

        private void UsedItems_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sindex = usedItems_lb.SelectedIndex;
            if (sindex >= 0)
                selectedItemFlags_txt.Text = itemsFlags[sindex].ToString();
        }

        private void SetItemFlags_btn_Click(object sender, EventArgs e)
        {
            int sindex = usedItems_lb.SelectedIndex;
            if (sindex >= 0)
                itemsFlags[sindex] = ulong.Parse(selectedItemFlags_txt.Text);
        }

        private void Save_Mod_btn_Click(object sender, EventArgs e)
        {
            ProgramConsole.SaveNewOriginalMod(originalMod_cbb.SelectedItem.ToString());
            CodeWriter.CheckPaths();

            string backupFolder = CodeWriter.ModuleSystem + "BACKUP";
            foreach (string file in Directory.GetFiles(backupFolder))
                File.Copy(file, CodeWriter.ModuleSystem + Path.GetFileName(file), true);
            string variablesProcess = CodeWriter.ModuleSystem + "variables.txt";
            string variablesModule = CodeReader.ModPath + "variables.txt";

            string[] info = ProgramConsole.ReadProjectFileInfoFromFile(CodeReader.ProjectPath);
            bool useVariables = bool.Parse(info[6]);
            if (File.Exists(variablesModule) && useVariables)
                File.Copy(variablesModule, variablesProcess, true);
            else if (File.Exists(variablesProcess) && !useVariables)
                    File.Delete(variablesProcess);
            //else
            //    MessageBox.Show("ERROR?!");
            MessageBox.Show("Changed Original Module to " + ProgramConsole.OriginalMod + " successfully!");
        }

        private void Save_DestinationMod_btn_Click(object sender, EventArgs e)
        {
            ProgramConsole.SaveNewDestinationMod(destinationMod_cbb.SelectedItem.ToString());
            CodeWriter.CheckPaths();

            MessageBox.Show("Changed Destination Module to " + ProgramConsole.DestinationMod + " successfully!");
        }

        private void SaveProjectFolder_btn_Click(object sender, EventArgs e)
        {

        }

        private void ProjectFolder_OpenFolderDialog_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
