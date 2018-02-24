using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WarbandTranslator
{
    public partial class Main : Form
    {
        public const byte FORM_TITLE_HEIGHT = 30;
        private const byte DEFAULT = 0;
        private const byte LANGUAGE = 1; 
        public const string BACKSLASH = "\\";
        private const string LOADING = " - LOADING...";
        private const string NOTHING_FOUND = "ALL ITEMS SHOULD BE TRANSLATED!";
        private const string TEXT_FORMAT = ".txt";
        private const string CSV_FORMAT = ".csv";

        private string module_path;
        private string language;
        private string file;
        private static bool saved = false;
        private bool loadNewFile;
        private bool nextPage;
        private bool fileOpened = false;
        private int fileMode = DEFAULT;
        private int currentPage;
        private int maxPage;
        private int currentValueListControls;

        private List<string> properties = new List<string>();
        private List<string> language_properties = new List<string>();
        private List<string> combined_properties = new List<string>();

        private List<ValueList> filteredPropertiesForLayout = new List<ValueList>();
        private List<ValueList> tempValueListList = new List<ValueList>();

        private FileSaver fileSaver;

        public Main()
        {
            DoubleBuffered = true;
            InitializeComponent();
            FormClosing += Form1_FormClosing;
            SizeChanged += Main_SizeChanged;
            MouseWheel += FlowLayoutPanel_MouseWheel;
            FileSaver.LockItVersion = 2896;
        }

        private void FlowLayoutPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int currentPageTMP = currentPage;
            if (e.Delta > 0 && currentPage > 1)
                currentPage--;
            else if (e.Delta < 0 && currentPage < maxPage)
                currentPage++;
            if (currentPageTMP != currentPage)
            {
                nextPage = true;
                refreshLayoutControls();
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            refreshLayoutControls();
        }

        public void deleteTranslation(string s)
        {
            if (language_properties.Contains(s))
            {
                for (int i = 0; i < filteredPropertiesForLayout.Count; i++)
                {
                    if (s.Equals(filteredPropertiesForLayout[i].BaseString))
                    {
                        filteredPropertiesForLayout.RemoveAt(i);
                        i = filteredPropertiesForLayout.Count;
                    }
                }
                language_properties.Remove(s);
                refreshLayoutControls();
            }
        }

        public List<ValueList> Values
        {
            get
            {
                return filteredPropertiesForLayout;
            }
        }

        public void changeListValueAtIndex(int index, string s)
        {
            if (index < filteredPropertiesForLayout.Count)
                filteredPropertiesForLayout[index].Value = s;
        }

        public static bool Saved
        {
            get
            {
                return saved;
            }
            set
            {
                saved = value;
            }
        }

        public string ModulePath
        {
            get
            {
                return module_path;
            }
            set
            {
                module_path = value;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = checkSavedBeforeExit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;

            module_path = Properties.Settings.Default.module_path;
            language = Properties.Settings.Default.language;
            file = Properties.Settings.Default.file;
            ausrufezeichenItem.Checked = Properties.Settings.Default.filterAUSRUF;
            ohneAusrufezeichenToolStripMenuItem.Checked = Properties.Settings.Default.filterOHNEAUSRUF;
            notTranslateableToolStripMenuItem.Checked = Properties.Settings.Default.filterNotTranslateable;

            languages_cms.ItemClicked += Languages_cms_ItemClicked;
            eToolStripMenuItem.SelectedIndex = 0;
            eToolStripMenuItem.SelectedIndexChanged += EToolStripMenuItem_SelectedIndexChanged;

            foreach (ToolStripItem item in languages_cms.Items)
                if (item.Text.Equals(Properties.Settings.Default.language))
                    item.BackColor = Color.LightBlue;
                else
                    item.BackColor = Color.Gainsboro;
        }

        private void EToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCurrentLanguageToolStripMenuItem.Enabled = true;
            reset();
            file = eToolStripMenuItem.SelectedItem.ToString();
            loadFile();
            flowLayoutPanel1.Visible = true;
        }

        private void loadFile()
        {
            file_cms.Hide();
            //reset();
            pageSwitcher_numupdown.Visible = true;
            page_text_lbl.Visible = true;
            read_File();
            fileMode = DEFAULT;
            refreshToolStripMenuItem.Enabled = true;
            fileOpened = true;
            flowLayoutPanel1.Visible = true;
        }

        private void Languages_cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            language = e.ClickedItem.Text;
            foreach (ToolStripItem item in languages_cms.Items)
                item.BackColor = Color.Gainsboro;
            e.ClickedItem.BackColor = Color.LightBlue;
        }

        private void language_btn_Click(object sender, EventArgs e)
        {
            languages_cms.Show(Location.X + language_btn.Location.X, Location.Y + language_btn.Height + FORM_TITLE_HEIGHT);
        }

        private void file_btn_Click(object sender, EventArgs e)
        {
            file_cms.Show(Location.X + file_btn.Location.X, Location.Y + file_btn.Height + FORM_TITLE_HEIGHT);
        }

        private string getSecondFilePath(string fileFormat)
        {
            return "languages" + BACKSLASH + language + BACKSLASH + getCorrectFileName(file) + fileFormat;
        }

        private void read_File()
        {
            string secFilePath = getSecondFilePath(CSV_FORMAT);

            Reader r = new Reader(module_path, file + TEXT_FORMAT);
            properties = r.GetOriginalFileProperties(properties);
            if (File.Exists(module_path + secFilePath))
            {
                Reader r2 = new Reader(module_path, secFilePath);
                language_properties = r2.GetLanguageFileProperties(language_properties);
                fileSaver = new FileSaver(Path.GetDirectoryName(module_path + secFilePath), Path.GetFileName(secFilePath));
            }
            filter_equal_list_properties();
        }

        private void filter_equal_list_properties()
        {
            string[] splitter;
            string tmp;
            ValueList vlist;
            Text += LOADING;
            for (int i = 0; i < properties.Count; i++)
            {
                tmp = properties[i].Split(Reader.SPLIT1)[0];
                /*if (tmp.Contains("zendar"))
                {
                    if (!ListEqualsPartString(tmp, language_properties))
                        MessageBox.Show("GOOD: " + properties[i]);
                    else
                        MessageBox.Show("BAD: " + properties[i]);
                }*/
                if (!ListEqualsPartString(tmp, language_properties))
                {
                    splitter = properties[i].Split(Reader.SPLIT1);
                    if (splitter.Length >= 2)
                    {
                        if (((ausrufezeichenItem.Checked && !splitter[1].Contains("{!}")) || !ausrufezeichenItem.Checked) &&
                            ((ohneAusrufezeichenToolStripMenuItem.Checked && splitter[1].Contains("{!}")) || !ohneAusrufezeichenToolStripMenuItem.Checked) &&
                            ((!containsNotTranslateableText(splitter[1]) && notTranslateableToolStripMenuItem.Checked) || !notTranslateableToolStripMenuItem.Checked))
                        {
                            vlist = new ValueList(this);
                            vlist.Property = splitter[0];
                            vlist.Value = splitter[1].Replace("_", " ");
                            filteredPropertiesForLayout.Add(vlist);
                        }
                    }
                }
            }
            refreshLayoutControls();
            Text = Text.Remove(Text.Length - LOADING.Length);
        }

        public void refreshLayoutControls()
        {
            if (filteredPropertiesForLayout.Count > 0)
                addPageToLayout(currentPage);
            else
                itemCount_lbl.Text = NOTHING_FOUND;
            if (itemCount_lbl.Text.Equals(NOTHING_FOUND))
            {
                page_text_lbl.Visible = false;
                pageSwitcher_numupdown.Visible = false;
            }
        }

        private void addPageToLayout(int x, List<ValueList> tmpList = null)
        {
            ValueList vl;
            List<ValueList> tmpL;

            if (tmpList == null)
                tmpL = filteredPropertiesForLayout;
            else
                tmpL = tmpList;

            vl = tmpL[0];

            double d = flowLayoutPanel1.Width / (vl.Width + 8) *
                        flowLayoutPanel1.Height / (vl.Height + 9);
            int quad = (int)d;
            //double x = Math.Floor(d); // get the number after the . --> 1.423 -> 0.423 
            double c = (quad * 0.075);//+ 0.3
            quad += (int)c;

            int minItems, maxItems;
            double remainingItems;
            if (currentValueListControls != quad || nextPage || loadNewFile)
            {
                try
                {
                    if (tmpL.Count >= quad)
                        currentValueListControls = quad;
                    else
                        currentValueListControls = tmpL.Count;

                    maxPage = tmpL.Count / currentValueListControls;
                    remainingItems = tmpL.Count % currentValueListControls;

                    if (remainingItems > 0d)
                        maxPage++;

                    flowLayoutPanel1.Controls.Clear();

                    for (int i = (x - 1) * quad; i < tmpL.Count; i++)
                        if (i < (x * quad))
                            flowLayoutPanel1.Controls.Add(tmpL[i]);
                        else
                            i = tmpL.Count;

                    flowLayoutPanel1.SuspendLayout();
                    for (int i = flowLayoutPanel1.Controls.Count - 1; i > 0; i--)
                    {
                        vl = (ValueList)flowLayoutPanel1.Controls[i];
                        if ((vl.Left + vl.Width) > (Width - (Width - flowLayoutPanel1.Width)))
                            flowLayoutPanel1.Controls.Remove(vl);
                        else
                            i = 0;
                    }
                    flowLayoutPanel1.ResumeLayout();

                    loadNewFile = false;

                    if (currentPage != maxPage)
                        maxItems = (currentPage * currentValueListControls);
                    else
                        maxItems = tmpL.Count;

                    minItems = maxItems - currentValueListControls + 1;
                    pageSwitcher_numupdown.Maximum = maxPage;
                    itemCount_lbl.Text = "Page: " + currentPage + " of " + maxPage + " | Items:" + minItems + " - " + maxItems + " | Item Count:" + tmpL.Count;
                }
                catch (Exception) { }
            }
        }

        private bool ListEqualsPartString(string s, List<string> list)
        {
            bool b = false;
            for (int i = 0; i < list.Count; i++)
                if (list[i].Split(Reader.SPLIT1)[0].Equals(s))
                    b = true;
            return b;
        }

        private int ListGetIndexOfContainingItem(string item, List<string> list)
        {
            int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Contains(item))
                {
                    index = i;
                    i = list.Count;
                }
            }
            return index;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkSavedBeforeExit();
        }

        private bool checkSavedBeforeExit()
        {
            bool cancel = false;
            DialogResult dl = (DialogResult)7;

            if (!Saved && fileOpened)
            {
                dl = MessageBox.Show("Do you want to save your work?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                if (dl == DialogResult.Yes)
                    Save();
            }

            if (dl != DialogResult.Cancel)
                Application.Exit();
            else
                cancel = true;

            return cancel;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            //if (!Saved && fileOpened)
            if (fileOpened)
                Save();
        }

        private void Save()
        {

            if (fileMode == DEFAULT)
                SaveDefault();
            else if (fileMode == LANGUAGE)
                SaveModifiedLanguageFile();
            else
                MessageBox.Show("ERROR - SAVING FAILED!");
        }

        private void SaveDefault()
        {
            string tmp;
            combined_properties.Clear();
            foreach (ValueList item in flowLayoutPanel1.Controls)
            {
                tmp = item.BaseString;
                if (!language_properties.Contains(tmp) && !properties.Contains(tmp) && !combined_properties.Contains(tmp))
                {
                    combined_properties.Add(tmp);
                }
            }
            fileSaver.SaveFile(combined_properties, language_properties);
        }

        private void SaveModifiedLanguageFile()
        {
            //combined_properties.Clear();
            foreach (ValueList item in flowLayoutPanel1.Controls)
                if (!language_properties.Contains(getRealItemCode(item.BaseString)))
                    combined_properties.Add(item.BaseString);
            foreach (string s in combined_properties)
                for (int i = 0; i < language_properties.Count; i++)
                    if (language_properties[i].Contains(s.Split(Reader.SPLIT1)[0] + Reader.SPLIT1))
                        language_properties[i] = s;
            combined_properties.Clear();
            fileSaver.SaveFile(combined_properties, language_properties);
        }

        private string getRealItemCode(string s)
        {
            string[] sx = s.Split(Reader.SPLIT1);
            return sx[0] + Reader.SPLIT1 + sx[1].Replace('_', Reader.SPLIT2);
        }

        private void saveSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.module_path = module_path;
            Properties.Settings.Default.language = language;
            Properties.Settings.Default.file = file;
            Properties.Settings.Default.filterAUSRUF = ausrufezeichenItem.Checked;
            Properties.Settings.Default.filterOHNEAUSRUF = ohneAusrufezeichenToolStripMenuItem.Checked;
            Properties.Settings.Default.filterNotTranslateable = notTranslateableToolStripMenuItem.Checked;

            Properties.Settings.Default.Save();
        }

        private void selectModuleFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ModulePath(this).Show();
        }

        private string getCorrectFileName(string filename)
        {
            string newfilename = filename;

            if (filename.Equals("menus"))
                newfilename = "game_menus";
            else if (filename.Equals("item_kinds1"))
                newfilename = newfilename.Substring(0, newfilename.Length - 1);
            else if (filename.Equals("conversation"))
                newfilename = "dialogs";
            else if (filename.Equals("strings"))
                newfilename = "game_strings";

            return newfilename;
        }

        private void reset()
        {
            fileOpened = false;
            loadNewFile = true;
            Saved = false;

            searchText_txt.ResetText();
            
            flowLayoutPanel1.Visible = false;
            pageSwitcher_numupdown.Visible = false;
            page_text_lbl.Visible = false;
            flowLayoutPanel1.Controls.Clear();
            filteredPropertiesForLayout.Clear();
            tempValueListList.Clear();

            currentValueListControls = 0;
            currentPage = 1;
            maxPage = 1;
            pageSwitcher_numupdown.Maximum = maxPage;
            nextPage = false;

            properties.Clear();
            language_properties.Clear();

            itemCount_lbl.ResetText();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*reset();
            loadFile();
            flowLayoutPanel1.Visible = true;*/
            //eToolStripMenuItem.PerformClick();
            loadCurrentLanguageToolStripMenuItem.Enabled = true;
            reset();
            //file = eToolStripMenuItem.SelectedItem.ToString();
            loadFile();
            flowLayoutPanel1.Visible = true;
        }

        private void loadCurrentLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string txt = Text;
            string[] splitter;
            ValueList vlist;
            reset();
            Reader rd = new Reader(module_path, getSecondFilePath(CSV_FORMAT));
            language_properties = rd.GetLanguageFileProperties(new List<string>());
            Text += LOADING;

            for (int i = 0; i < language_properties.Count; i++)
            {
                splitter = language_properties[i].Split(Reader.SPLIT1);
                if (splitter.Length >= 2)
                {
                    if (((ausrufezeichenItem.Checked && !splitter[1].Contains("{!}")) || !ausrufezeichenItem.Checked) &&
                        ((ohneAusrufezeichenToolStripMenuItem.Checked && splitter[1].Contains("{!}")) || !ohneAusrufezeichenToolStripMenuItem.Checked) &&
                        ((!containsNotTranslateableText(splitter[1]) && notTranslateableToolStripMenuItem.Checked) || !notTranslateableToolStripMenuItem.Checked))
                    {
                        vlist = new ValueList(this, true);
                        vlist.Property = splitter[0];
                        if (splitter[1].Contains("_"))
                            splitter[1] = splitter[1].Replace("_", " ");
                        vlist.Value = splitter[1];
                        filteredPropertiesForLayout.Add(vlist);
                    }
                }
            }

            refreshLayoutControls();

            Saved = false;
            fileMode = LANGUAGE;
            fileOpened = true;
            refreshToolStripMenuItem.Enabled = false;
            flowLayoutPanel1.Visible = true;
            pageSwitcher_numupdown.Visible = true;
            page_text_lbl.Visible = true;
            Text = txt;
        }

        private bool containsNotTranslateableText(string s)
        {
            char[] ca;
            int x = -1;
            bool b = false, c = false;
            string tmp = string.Empty, tmp2;
            List<string> sxtmp = new List<string>();

            string sx = s.Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("!", string.Empty).Replace(",", string.Empty).Replace(".", string.Empty).Replace(":", string.Empty).Replace(";", string.Empty).Replace("\"", string.Empty).Replace("§", string.Empty);
            sx = sx.Replace("$", string.Empty).Replace("%", string.Empty).Replace("&", string.Empty).Replace("/", string.Empty).Replace("\\", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace("=", string.Empty).Replace("?", string.Empty);
            sx = sx.Replace("´", string.Empty).Replace("`", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty).Replace("*", string.Empty).Replace("-", string.Empty).Replace("+", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            sx = sx.Replace("|", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty).Replace("_", string.Empty).Replace("€", string.Empty).Replace("@", string.Empty).Replace("'", string.Empty).Replace("#", string.Empty).Replace("~", string.Empty);
            sx = sx.Replace("1", string.Empty).Replace("2", string.Empty).Replace("3", string.Empty).Replace("4", string.Empty).Replace("5", string.Empty).Replace("6", string.Empty).Replace("7", string.Empty).Replace("8", string.Empty).Replace("9", string.Empty).Replace("0", string.Empty);
            sx = sx.Replace("0", string.Empty).Replace("²", string.Empty).Replace("³", string.Empty).Replace("°", string.Empty).Replace("^", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty);

            if (sx.Equals(string.Empty))
                b = !b;
            else if (sx.Contains("{") && sx.Contains("}"))
            {
                ca = sx.ToCharArray();
                
                for (int i = 0; i < ca.Length; i++)
                {
                    if (ca[i] == '{')
                    {
                        sxtmp.Add(string.Empty);
                        x++;
                        c = true;
                    }
                    else if (ca[i] == '}')
                        c = false;
                    else if (!c)
                        tmp = tmp + ca[i].ToString();
                    else
                        sxtmp[x] += ca[i].ToString();
                }
                if (sxtmp.Count > 0 && tmp.Length == 0)
                    b = !b;
                if (tmp.Length == 0)
                {
                    for (int i = 0; i < sxtmp.Count; i++)
                    {
                        tmp2 = sxtmp[i];
                        if (!tmp2.Equals("s") && !tmp2.Equals("reg") && !tmp2.Equals("playername"))
                        {
                            b = !b;
                            i = sxtmp.Count;
                        }
                    }
                }
            }
            return b;
        }

        public static bool IsNumeric(string v)
        {
            int a;
            return int.TryParse(v, out a); 
        }

        private void ausrufezeichenItem_Click(object sender, EventArgs e)
        {
            ausrufezeichenItem.Checked = !ausrufezeichenItem.Checked;
            if (ausrufezeichenItem.Checked && ohneAusrufezeichenToolStripMenuItem.Checked)
                ohneAusrufezeichenToolStripMenuItem.Checked = false;
        }

        private void showOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ohneAusrufezeichenToolStripMenuItem.Checked = !ohneAusrufezeichenToolStripMenuItem.Checked;
            if (ausrufezeichenItem.Checked && ohneAusrufezeichenToolStripMenuItem.Checked)
                ausrufezeichenItem.Checked = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (searchText_txt.Text.Length > 0)
                foreach (ValueList item in filteredPropertiesForLayout)
                    if (item.Value.Contains(searchText_txt.Text) || item.Property.Contains(searchText_txt.Text))
                    {
                        item.BackColor = Color.YellowGreen;
                        item.ForeColor = Color.Black;
                    }
                    else
                    {
                        item.BackColor = BackColor;
                        item.ForeColor = ForeColor;
                    }
            else
                foreach (ValueList item in filteredPropertiesForLayout)
                {
                    item.BackColor = BackColor;
                    item.ForeColor = ForeColor;
                }
        }

        private void pageSwitcher_numupdown_ValueChanged(object sender, EventArgs e)
        {
            int currentPageTMP = currentPage;
            currentPage = (int)pageSwitcher_numupdown.Value;
            if (currentPageTMP != currentPage)
            {
                nextPage = true;
                refreshLayoutControls();
            }
        }

        private void notTranslateableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notTranslateableToolStripMenuItem.Checked = !notTranslateableToolStripMenuItem.Checked;
        }
    }
}
