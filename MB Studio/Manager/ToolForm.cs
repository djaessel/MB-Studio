using importantLib;
using MB_Decompiler_Library.IO;
using skillhunter;
using static skillhunter.Skriptum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WarbandTranslator;
using System.ComponentModel;
using MB_Studio.Main;

namespace MB_Studio.Manager
{
    // (DEADCTIVATE types initialization in LoadSettingsAndLists(bool loadSavedTypes = true) and the frmSplash (Loading Window for editing)!!! - am Ende abstract)
    // ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  < DESIGNER ERRORS >  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^  ^
    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public partial class ToolForm : SpecialForm
    {
        #region Attributes

        protected FileSaver fileSaver;

        public const int GROUP_HEIGHT_MIN = 25;
        public const int GROUP_HEIGHT_DIF = 100;
        public const int GROUP_HEIGHT_MAX = GROUP_HEIGHT_DIF + GROUP_HEIGHT_MIN;

        protected List<string[]> translations = new List<string[]>();
        protected List<string> typesIDs = new List<string>();
        protected List<Skriptum> types = new List<Skriptum>();
        
        // instance member to keep reference to splash form
        private SplashForm frmSplash;
        // delegate for the UI updater
        protected delegate void UpdateUIDelegate(bool IsDataLoaded);

        #region Properties

        public ObjectType ObjectType { get; private set; }

        public int ObjectTypeID
        {
            get { return (int)ObjectType; }
            private set { ObjectType = (ObjectType)value; }
        }

        public string Prefix { get { return Prefixes[ObjectTypeID] + '_'; } }

        #endregion

        #endregion

        #region Loading

        public ToolForm()
        {
            Init();
        }

        public ToolForm(ObjectType objectType) : base()
        {
            Init(objectType);
        }

        private void Init(ObjectType objectType = ObjectType.SCRIPT)
        {
            ObjectType = objectType;
            InitializeComponent();
            idINFO_lbl.Text = idINFO_lbl.Text.Replace("ID_", Prefix);
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
            Shown += ToolForm_Shown;
        }

        protected virtual void ToolForm_Shown(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void ToolForm_Load(object sender, EventArgs e)
        {
            title_lbl.Text = Text;

            // Update UI
            UpdateUI(false);

            // Show the splash form
            if (!DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                frmSplash = new Loader(this, false) {
                    StartPosition = FormStartPosition.CenterScreen
                };
                frmSplash.Show();/**/

                // Do some time consuming work in separate thread
                Thread t = new Thread(new ThreadStart(LoadControlsAndSettings)) { IsBackground = true };
                t.Start();/**/
            }
            else
                LoadControlsAndSettings();// USE THIS ONE HERE WHEN THREAD IS DEACTIVATED FOR EDITING
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        protected void UpdateUI(bool IsDataLoaded)
        {
            if (IsDataLoaded)
                if (frmSplash != null)
                    frmSplash.Close();
        }

        private void LoadControlsAndSettings()
        {
            LoadSettingsAndLists();

            Invoke((MethodInvoker)delegate { InitializeControls(); });

            // Update UI
            if (!DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                Invoke(new UpdateUIDelegate(UpdateUI), new object[] { true });
        }

        protected virtual void InitializeControls()
        {
            foreach (string typeID in typesIDs)
                typeSelect_lb.Items.Add(typeID);
            for (int i = 0; i < language_cbb.Items.Count; i++)
                translations.Add(new string[2]);

            typeSelect_lb.SelectedIndex = 0;

            //ResetControls();//removed because as well in FormShown Event(?)
        }

        protected /*abstract*/virtual Skriptum GetNewTypeFromClass(string[] raw_data)
        {
            throw new NotImplementedException();
        }

        protected virtual void LoadSettingsAndLists()
        {
            if (!DesignMode && LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                types = new CodeReader(CodeReader.ModPath + CodeReader.Files[ObjectTypeID]).ReadObjectType(ObjectTypeID);

            if (Properties.Settings.Default.loadSavedObjects)//maybe change the access way later
            {
                List<Skriptum> savedTypes = new List<Skriptum>();
                List<List<string>> savedTypesDatas = MB_Studio.LoadAllPseudoCodeByObjectTypeID(ObjectTypeID);

                foreach (List<string> savedTroopData in savedTypesDatas)
                    savedTypes.Add(GetNewTypeFromClass(CodeReader.GetStringArrayStartFromIndex(savedTroopData.ToArray(), 1)));

                for (int j = 0; j < types.Count; j++)
                {
                    for (int i = 0; i < savedTypes.Count; i++)
                    {
                        if (types[j].ID.Equals(savedTypes[i].ID))
                        {
                            types[j] = savedTypes[i];
                            i = savedTypes.Count;
                        }
                    }
                }
            }

            for (int i = 0; i < types.Count; i++)
            {
                string id = types[i].ID;
                if (!ContainsPrefix(id))
                    id = Prefix + id;
                typesIDs.Add(id);
            }

            foreach (Control c in toolPanel.Controls)
                if (c.Name.Split('_')[0].Equals("showGroup"))
                    c.Click += C_Click;
        }

        protected bool ContainsPrefix(string text)
        {
            bool b = false;
            if (text.Contains(Prefix))
                if (text.Substring(0, Prefix.Length).Equals(Prefix))
                    b = !b;//true
            return b;
        }

        #endregion

        #region Click Events

        protected void C_Click(object sender, EventArgs e)
        {
            bool sub = false;
            bool closed;
            Control button = (Control)sender;
            int index = int.Parse(button.Name.Split('_')[1]);
            int tag;
            if (button.Tag.ToString().Contains("-"))
                tag = -int.Parse(button.Tag.ToString().Substring(1));
            else
                tag = int.Parse(button.Tag.ToString());
            Control groupBox = toolPanel.Controls.Find("groupBox_" + index + "_gb", false)[0];
            closed = groupBox.Height == GROUP_HEIGHT_MIN;
            if (closed)
            {
                groupBox.Height = GROUP_HEIGHT_MAX;
                if (tag > 0)
                    groupBox.Height += tag;
                else
                    groupBox.Height -= -tag;
                //button.Text = "ʌ";
            }
            else
            {
                groupBox.Height = GROUP_HEIGHT_MIN;
                //button.Text = "v";
                sub = !sub;
            }
            button.Height = groupBox.Height;
            int differ = GROUP_HEIGHT_DIF;
            if (tag != 0)
                differ = differ + tag;
            foreach (Control c in toolPanel.Controls)
            {
                if (int.Parse(c.Name.Split('_')[1]) > index)
                {
                    if (!sub)
                        c.Top += differ;
                    else
                        c.Top -= differ;
                }
            }
            if (!sub)
                Height += differ;
            else
                Height -= differ;
            if (closed)
                button.Text = "ʌ";
            else
                button.Text = "v";
        }

        private void Min_btn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseAll_btn_Click(object sender, EventArgs e)
        {
            int x = 16;
            Control[] c;
            for (int i = 0; i < x; i++)
            {
                c = toolPanel.Controls.Find("showGroup_" + i + "_btn", false);
                if (c.Length > 0)
                {
                    c[0].Top = 3 + i * 30;
                    c[0].Height = 25;
                    c[0].Text = "v";
                }
                else
                    x = i;
            }
            for (int i = 0; i < x; i++)
            {
                c = toolPanel.Controls.Find("groupBox_" + i + "_gb", false);
                c[0].Top = 2 + i * 30;
                c[0].Height = 25;
            }
            Height = 190 + x * 30;
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            if (!ContainsPrefix(id_txt.Text))
                id_txt.Text = Prefix + id_txt.Text;

            List<string> list = new List<string> {
                id_txt.Text.Replace(' ', '_') + ' ' + name_txt.Text.Replace(' ', '_')
            };

            int index;
            if (typeSelect_lb.SelectedIndex > 0/* && typeSelect_lb.SelectedIndex < typeSelect_lb.Items.Count*/)
            {
                if (save_btn.Text.Equals("SAVE"))
                    index = GetIndexOfTypeByID(typeSelect_lb.SelectedItem.ToString());
                else
                    index = typeSelect_lb.Items.Count;
            }
            else
                index = typeSelect_lb.Items.Count;

            SaveTypeByIndex(list, index);
        }

        #endregion

        #region GUI

        private void Id_txt_TextChanged(object sender, EventArgs e)
        {
            bool isInList = false;
            string id = id_txt.Text.Trim();
            if (id.IndexOf(Prefix) != 0)
                id = Prefix + id;
            for (int i = 0; i < typeSelect_lb.Items.Count; i++)
            {
                if (typeSelect_lb.Items[i].Equals(id))
                {
                    isInList = true;
                    i = typeSelect_lb.Items.Count;
                }
            }
            if (!isInList)
                save_btn.Text = "CREATE";
            else if (save_btn.Text.Equals("CREATE"))
                save_btn.Text = "SAVE";
            if (isInList)
                typeSelect_lb.SelectedIndex = typeSelect_lb.Items.IndexOf(id);
        }

        private void SearchType_SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            typeSelect_lb.Items.Clear();
            if (!searchType_SearchTextBox.Text.Contains("Search ...") && searchType_SearchTextBox.Text.Length > 0)
            {
                foreach (Skriptum type in types)
                {
                    if (type.ID.Contains(searchType_SearchTextBox.Text) || type.ID.Contains(searchType_SearchTextBox.Text))
                    {
                        string typeID = type.ID;
                        //if (type.ObjectTyp != ObjectType.ITEM)
                            typeID = Prefix + typeID;
                        typeSelect_lb.Items.Add(Prefix + type.ID);//2 times prefix ???
                    }
                }
            }
            else
            {
                foreach (Skriptum type in types)
                {
                    string typeID = type.ID;
                    //if (type.ObjectTyp != ObjectType.ITEM)
                        typeID = Prefix + typeID;
                    typeSelect_lb.Items.Add(Prefix + type.ID);//2 times prefix ???s
                }
            }
        }

        private void TypeSelect_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeSelect_lb.Items.Count > 0 && typeSelect_lb.SelectedItem != null)
            {
                if (/*(*/typeSelect_lb.SelectedIndex > 0/* && save_btn.Text.Equals("SAVE"))*/ || !typeSelect_lb.SelectedItem.ToString().Equals("New"))
                {
                    int idx = GetIndexOfTypeByID(typeSelect_lb.SelectedItem.ToString());
                    Skriptum type = types[idx];
                    SetupType(type);
                }
                else
                    ResetControls();
            }
        }

        #endregion

        #region Setups

        protected virtual void ResetControls()
        {
            id_txt.ResetText();
            name_txt.ResetText();
            plural_name_txt.ResetText();

            List<string> excluded = new List<string>() { language_cbb.Name };

            foreach (Control groupC in toolPanel.Controls)
                if (GetNameEndOfControl(groupC).Equals("gb"))
                    ResetGroupBox((GroupBox)groupC, excluded);

            singleNameTranslation_txt.ResetText();
            pluralNameTranslation_txt.ResetText();
        }

        protected virtual void ResetGroupBox(GroupBox groupBox, List<string> exclude = null)
        {
            if (exclude == null)
                exclude = new List<string>();
            foreach (Control c in groupBox.Controls)
            {
                if (!exclude.Contains(c.Name))
                {
                    string nameEnd = GetNameEndOfControl(c);
                    if (nameEnd.Equals("txt"))
                        c.Text = "0";
                    else if (nameEnd.Equals("num") || nameEnd.Equals("numeric"))
                        ((NumericUpDown)c).Value = 0;
                    else if (nameEnd.Equals("lb"))
                    {
                        ListBox lb = (ListBox)c;
                        if (lb.Items.Count > 0)
                            lb.SelectedIndex = 0;
                    }
                    else if (nameEnd.Equals("cbb"))
                    {
                        ComboBox cbb = (ComboBox)c;
                        if (cbb.Items.Count > 0)
                            cbb.SelectedIndex = 0;
                    }
                    else if (nameEnd.Equals("rb"))
                        ((RadioButton)c).Checked = false;
                    else if (nameEnd.Equals("cb"))
                        ((CheckBox)c).CheckState = CheckState.Unchecked;
                    else if (nameEnd.Equals("gb"))
                        ResetGroupBox((GroupBox)c);
                    else if (nameEnd.Equals("SearchTextBox"))
                        c.Text = "Search ...";
                    else if (nameEnd.Equals("rtb") || nameEnd.Equals("text"))
                        c.Text = string.Empty;
                }
            }
        }

        protected virtual void SetupType(Skriptum type)
        {
            ResetControls();

            id_txt.Text = type.ID;
            //name_txt.Text = type.Name; //maybe implement later

            #region Translation

            for (int i = 0; i < translations.Count; i++)
                translations[i] = new string[2];

            for (int i = 0; i < language_cbb.Items.Count; i++)
                PrepareLanguageByIndex(i);
            if (language_cbb.SelectedIndex != Properties.Settings.Default.languageIndex)
                language_cbb.SelectedIndex = Properties.Settings.Default.languageIndex; // other code is in the SelectedIndex Changed Event
            else
                Language_cbb_SelectedIndexChanged(); //Rethink

            #endregion
        }

        #endregion

        #region Save

        protected virtual void SaveTypeByIndex(List<string> values, int selectedIndex, Skriptum changed = null)
        {
            if (changed != null)
            {
                bool newOne = false;

                if (selectedIndex < 0)
                    selectedIndex = typeSelect_lb.SelectedIndex;

                if (selectedIndex >= 0)
                {
                    if (selectedIndex < typesIDs.Count)//was typesIDs.Count - 1 before
                        types[selectedIndex] = changed;
                    else
                    {
                        types.Add(changed);
                        typesIDs.Add(changed.ID);
                        typeSelect_lb.Items.Add(changed.ID);//maybe check name is always correct - if needed
                        newOne = !newOne;//true
                    }
                    
                    SourceWriter.WriteAllObjects();
                    new SourceWriter().WriteObjectType(types, ObjectTypeID);

                    if (newOne)
                        typeSelect_lb.SelectedIndex = typeSelect_lb.Items.Count - 1;
                }
                else
                    MessageBox.Show("ERROR: 0xf3 - INVALID_INDEX" + Environment.NewLine + " --> Please report Bug!");
            }
            else
                Console.WriteLine("Warning: Not saved yet!");
        }

        #endregion

        #region Translation

        private void PrepareLanguageByIndex(int index, bool plural = false)
        {
            string[] tmp;
            bool foundSingleName = false;
            bool foundPluralName = !plural; //ObjectType == ObjectType.ITEM; // because Item plurals are most times the same
            string filePath = CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT, GetLanguageFromIndex(index));
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream && (!foundSingleName || !foundPluralName))
                    {
                        tmp = sr.ReadLine().Split('|');
                        if (!foundSingleName)
                        {
                            if (tmp[0].Equals(Prefix + id_txt.Text) && tmp.Length > 1)
                            {
                                translations[index][0] = tmp[1]; //singleNameTranslation_txt.Text = tmp[1];
                                foundSingleName = true;
                            }
                        }
                        if (!foundPluralName)
                        {
                            if (tmp[0].Equals(Prefix + id_txt.Text + "_pl") && tmp.Length > 1)
                            {
                                translations[index][1] = tmp[1]; //pluralNameTranslation_txt.Text = tmp[1];
                                foundPluralName = true;
                            }
                        }
                    }
                }  
            }
            //else
            //    MessageBox.Show("PATH DOESN'T EXIST --> CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT)" + Environment.NewLine + CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT));
        }

        protected virtual void Language_cbb_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            if (translations.Count > 0)
            {
                string filePath = CodeReader.ModPath + GetSecondFilePath(MB_Studio.CSV_FORMAT);
                int index = language_cbb.SelectedIndex;
                if (translations[index][0] != null)
                    singleNameTranslation_txt.Text = translations[index][0];
                else
                    singleNameTranslation_txt.Text = name_txt.Text;
                if (translations[index][1] != null)
                    pluralNameTranslation_txt.Text = translations[index][1];
                else if (plural_name_txt.Visible)
                    pluralNameTranslation_txt.Text = plural_name_txt.Text;
                else
                    plural_name_txt.ResetText();
                fileSaver = new FileSaver(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
            }
        }

        protected string GetLanguageFromIndex(int index) { return language_cbb.Items[index].ToString().Split('(')[0].TrimEnd(); }

        protected string CurrentLanguage { get { return GetLanguageFromIndex(language_cbb.SelectedIndex); } }

        protected string GetSecondFilePath(string fileFormat, string language = null)
        {
            if (language == null)
                language = CurrentLanguage;
            return "languages\\" + language + "\\" + GetCorrectFileName() + fileFormat;
        }

        protected string GetCorrectFileName()
        {
            string filename = CodeReader.Files[ObjectTypeID].Split('.')[0];
            if (filename.Equals("menus"))
                filename = "game_menus";
            else if (filename.Equals("item_kinds1"))
                filename = filename.Substring(0, filename.Length - 1);
            else if (filename.Equals("conversation"))
                filename = "dialogs";
            else if (filename.Equals("strings"))
                filename = "game_strings";
            return filename;
        }

        protected virtual void Save_translation_btn_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string id = id_txt.Text.Replace(' ', '_');
            bool singleFound = false;
            bool pluralFound = false;
            string[] orgLines = new string[] { FileSaver.LockItVersion.ToString() };
            if (File.Exists(fileSaver.FilePath))
            {
                orgLines = File.ReadAllLines(fileSaver.FilePath);
                for (int i = 0; i < orgLines.Length; i++)
                {
                    string s = orgLines[i].Split('|')[0];
                    if (!singleFound)
                    {
                        if (s.Equals(id))
                        {
                            orgLines[i] = id + '|' + singleNameTranslation_txt.Text;
                            singleFound = true;
                        }
                    }
                    if (!pluralFound)
                    {
                        if (s.Equals(id + "_pl"))
                        {
                            orgLines[i] = id + "_pl|" + pluralNameTranslation_txt.Text;
                            pluralFound = true;
                        }
                    }
                    if (pluralFound && singleFound)
                        i = orgLines.Length;
                }
            }
            if (!singleFound)
                list.Add(id + "|" + singleNameTranslation_txt.Text);
            if (!pluralFound)
                list.Add(id + "_pl|" + pluralNameTranslation_txt.Text);
            fileSaver.SaveFile(list, ImportantMethods.StringArrayToList(orgLines, 1));
        }

        #endregion

        #region Useful Methods

        protected int GetIndexOfSkriptumByID(string id, string[] skriptumList)
        {
            int index = -1;
            for (int i = 0; i < skriptumList.Length; i++)
            {
                if (skriptumList[i].Equals(id))
                {
                    index = i;
                    i = skriptumList.Length;
                }
            }
            return index;
        }

        protected int GetIndexOfTypeByID(string id)
        {
            int index = -1;
            if (types.Count != 0)
            {
                if (!types[0].ID.StartsWith(Prefix))//if (ObjectType != ObjectType.ITEM)
                    id = id.Substring(id.IndexOf('_') + 1);
            }
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].ID.Equals(id))
                {
                    index = i;
                    i = types.Count;
                }
            }
            return index;
        }

        #endregion
    }
}
