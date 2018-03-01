using importantLib;
using MB_Decompiler;
using MB_Decompiler_Library.IO;
using MB_Studio.Main;
using MB_Studio.Manager;
using skillhunter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MB_Studio
{
    public partial class MB_Studio : SpecialForm
    {
        #region Attributes

        private const int LEADING_SPACE = 12;
        private const int CLOSE_SPACE = 15;
        private const int CLOSE_AREA = CLOSE_SPACE;

        public const string CSV_FORMAT = ".csv";
        public const string TEXT_FORMAT = ".txt";

        public const string EOF_TXT = "EOF";

        private bool FullScreen = true;

        private const int C_GRIP = 16;                      // Grip size
        private const int C_CAPTION = 32;                   // Caption bar height;
        //private const int HT_CAPTION = 2;                 // Make Caption
        private const int HT_BOTTOM_RIGHT = 17;             // Caption position

        private const int WM_NCHITTEST = 0x84;              // Message Type

        public static bool DebugMode = false;

        public static bool Show3DView { get { return Properties.Settings.Default.show3DView; } }

        #endregion

        #region Loading

        public MB_Studio()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            ResizeEnd += MB_Studio_ResizeEnd;
            name_lbl.MouseDown += Control_MoveForm_MouseDown;
            file_btn.Click += Button_Open_ContextMenuStrip_Click;
            project_btn.Click += Button_Open_ContextMenuStrip_Click;
        }

        private void MB_Studio_ResizeEnd(object sender, EventArgs e)
        {
            tabControl.Update();
        }

        private void MB_Studio_Load(object sender, EventArgs e)
        {
            PrepareToolBar(false);
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;
            tabControl.MouseClick += TabControl_MouseClick;
            //file_cm.RenderMode = ToolStripRenderMode.Custom; // search for custom Renderer!!!
            name_lbl.Text = Text;
            SetFullScreenByHandle();
            InitializeTabControl();
            InitializeProject();
            LoadLastOpenedProjects();
        }

        private void LoadProject(string projectPath)
        {
            ProgramConsole.LoadProject(projectPath);
            RemoveAllTabPagesExeptConsole();
            LoadProjectExplorer();
        }

        private void LoadLastOpenedProjects()
        {
            string[] lastOpenedProjectPaths = ProgramConsole.GetLastOpenedProjectPaths();
            string[] lastOpenedProjectNames = ProgramConsole.GetLastOpenedProjectNames();
            for (int i = 0; i < lastOpenedProjectPaths.Length; i++)
                zuletztVerwendet_panel.Controls.Add(GetLastProjectControl(lastOpenedProjectNames[i], lastOpenedProjectPaths[i]));
        }

        private Control GetLastProjectControl(string name, string path)
        {
            ProjectObject PObject = new ProjectObject()
            {
                ProjectName = name, //Text = name,
                ProjectPath = path, //
                AutoSize = false,
                //Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                //Size = new Size(zuletztVerwendet_panel.Width - 20, 72),
                Width = zuletztVerwendet_panel.Width - 20,
                Top = zuletztVerwendet_lbl.Top + zuletztVerwendet_lbl.Height + 8 + (zuletztVerwendet_panel.Controls.Count - 1) * 58,
                Left = zuletztVerwendet_lbl.Left + 5,
                //FlatStyle = FlatStyle.Flat,
                //ForeColor = Color.FromArgb(0, 128, 254),
                //TextAlign = ContentAlignment.MiddleLeft,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Parent = zuletztVerwendet_panel,
                Tag = path
            };
            //b.FlatAppearance.BorderSize = 0;
            //b.FlatAppearance.MouseDownBackColor = b.Parent.BackColor;
            //b.FlatAppearance.MouseOverBackColor = b.Parent.BackColor;
            PObject.Click += ProjectObject_Click;
            return PObject;
        }

        private void ProjectObject_Click(object sender, EventArgs e)
        {
            ProjectObject pObject = (ProjectObject)sender; //Button b = (Button)sender;
            string path = pObject.ProjectPath; //b.Tag.ToString();
            if (Directory.Exists(path))
                LoadProject(path);
            else
            {
                var result = MessageBox.Show("Dieses Projekt konnte nicht gefunden werden! Soll der Eintrag entfernt werden?",
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    ProgramConsole.RemoveProjectPathFromLastOpened(path);
                    Label x = zuletztVerwendet_lbl;
                    zuletztVerwendet_panel.Controls.Clear();
                    zuletztVerwendet_panel.Controls.Add(x);
                    Properties.Settings.Default.Reload();
                    LoadLastOpenedProjects();
                }
            }
        }

        private void InitializeProject()
        {
            string s = ProgramConsole.GetModuleInfoPath();
            if (s.Equals("STEAMPATH"))
                ProgramConsole.SetModPath();

            projectVorlagenSearch_txt.Click += ProjectVorlagenSearch_txt_Click;
            projectVorlagenSearch_txt.LostFocus += ProjectVorlagenSearch_txt_LostFocus;

            string projectsPath = Properties.Settings.Default.projectsFolderPath.TrimEnd('\\'); //Application.StartupPath + "\\Projects";

            if (Directory.Exists(projectsPath))
            {
                foreach (string dir in Directory.GetDirectories(projectsPath))
                {
                    string[] files = Directory.GetFiles(dir);
                    foreach (string file in files)
                        if (file.Contains("."))
                            if (file.Substring(file.LastIndexOf('.')).Equals(".mbsp"))
                                projects_cbb.Items.Add(ImportantMethods.GetDirectoryNameOnly(dir));
                }
                if (projects_cbb.Items.Count > 0)
                    projects_cbb.SelectedIndex = 0;
            }
            else
                Directory.CreateDirectory(projectsPath);
        }

        private void ProjectVorlagenSearch_txt_LostFocus(object sender, EventArgs e)
        {
            if (projectVorlagenSearch_txt.Text.Length == 0)
                projectVorlagenSearch_txt.Text = " Projektvorlagen suchen";
        }

        private void InitializeTabControl()
        {
            // get the inital length
            int tabLength = tabControl.ItemSize.Width;
            // measure the text in each tab and make adjustment to the size
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                TabPage currentPage = tabControl.TabPages[i];
                int currentTabLength = TextRenderer.MeasureText(currentPage.Text, tabControl.Font).Width;
                // adjust the length for what text is written
                currentTabLength += LEADING_SPACE + CLOSE_SPACE + CLOSE_AREA;
                if (currentTabLength > tabLength)
                    tabLength = currentTabLength;
            }
            // create the new size
            Size newTabSize = new Size(tabLength, tabControl.ItemSize.Height);
            tabControl.ItemSize = newTabSize;
        }

        #endregion

        #region ClickEvents

        private void Min_btn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Maxnorm_btn_Click(object sender, EventArgs e)
        {
            Screen s = Screen.FromHandle(Handle);
            if (FullScreen)
                SetNormalScreen(s); //WindowState = FormWindowState.Maximized;
            else
                SetFullScreenByHandle();   //WindowState = FormWindowState.Normal;
            FullScreen = !FullScreen;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button_Open_ContextMenuStrip_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Point p = PointToScreen(b.Location);
            p = new Point(p.X, p.Y + b.Height);
            b.ContextMenuStrip.Show(p);
        }

        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                Rectangle tabRect = tabControl.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                Rectangle imageRect = new Rectangle(tabRect.Left + tabRect.Width - (int)tabControl.Font.SizeInPoints,
                                         tabRect.Top + (tabRect.Height - tabControl.Font.Height) / 2,
                                         (int)tabControl.Font.SizeInPoints,
                                         tabControl.Font.Height);
                if (imageRect.Contains(e.Location))
                {
                    tabControl.TabPages[i].Dispose();//.RemoveAt(i);
                    i = tabControl.TabPages.Count;
                }
            }
        }

        private void Build_btn_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you really want to overwrite the files in " + ProgramConsole.DestinationMod + "!?",
                                                Application.ProductName, 
                                                MessageBoxButtons.YesNo, 
                                                MessageBoxIcon.Warning);
            if (dlr == DialogResult.Yes)
            {
                tabControl.SelectedIndex = 0;
                CodeWriter.WriteAllCode(console_richTxt);
                /*Thread t = new Thread(new ThreadStart((MethodInvoker)delegate
                {
                    while (!CodeWriter.IsFinished) ;
                    AutoFixer.FixAll();
                }))
                {
                    IsBackground = true
                };
                t.Start();*/
            }
        }

        private void MbOptions_btn_Click(object sender, EventArgs e)
        {
            AddNewTab(new MBOptions());
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will include all options you know from other tools to change your files and projects as you want!");
        }

        private void AddElement_ToolStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here you will be able to add new elements such as presentations, meshes and other game components!",
                             Application.ProductName,
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
        }

        private void ManageImports_ToolStrip_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you know what you are doing?",
                                                Application.ProductName,
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Warning);
            if (dlr == DialogResult.Yes)
                AddNewTab(new ImportsManagerGUI()); //new ImportsManagerGUI().Show();
        }

        private void Properties_ToolStrip_Click(object sender, EventArgs e)
        {
            AddNewTab(new ProjectProperties());//.ShowDialog();
        }

        private void Help_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("At the moment the only help you can get is from the publisher! Sorry! :)");
        }

        private void ProjectNew_ToolStrip_Click(object sender, EventArgs e)
        {
            CreateNewProject();
            LoadProjectExplorer();
        }

        private void ProjectOpen_ToolStrip_Click(object sender, EventArgs e)
        {
            OpenFile_Click();
        }

        private void ProjectVorlagenSearch_txt_Click(object sender, EventArgs e)
        {
            if (projectVorlagenSearch_txt.Text.Equals(" Projektvorlagen suchen"))
                projectVorlagenSearch_txt.Text = string.Empty;
        }

        private void OpenSelectedProject_btn_Click(object sender, EventArgs e)
        {
            string projectPath = Properties.Settings.Default.projectsFolderPath + projects_cbb.SelectedItem.ToString();
            LoadProject(projectPath);
        }

        private void CreateNewProject_btn_Click(object sender, EventArgs e)
        {
            CreateProject cp = CreateNewProject();
            if (cp.ProjectCreated)
            {
                RemoveAllTabPagesExeptConsole();
                LoadProjectExplorer();
            }
        }

        private void OpenFile_Click(object sender = null, EventArgs e = null)
        {
            openFile_ofd.ShowDialog();
        }

        private void SelectProject_btn_Click(object sender, EventArgs e)
        {
            projectBrowser_fbd.ShowDialog();
            if (projectBrowser_fbd.SelectedPath != null)
                if (!projectBrowser_fbd.SelectedPath.Equals(string.Empty))
                    LoadProject(projectBrowser_fbd.SelectedPath);
        }

        private void FileOpen_toolStrip_Click(object sender, EventArgs e)
        {
            OpenFile_Click();
        }

        private void FileNew_toolStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hier kann in Zukunft eine Datei erstellt werden, wenn nötig!");
        }

        #endregion

        #region GUI

        protected override void SetFullScreenByHandle(IntPtr hWnd = default(IntPtr))
        {
            base.SetFullScreenByHandle(hWnd);
            maxnorm_btn.Text = "◱";//"⬜"
        }

        private void SetNormalScreen(Screen s)
        {
            maxnorm_btn.Text = "◼";
            Size = new Size(816, 512);
            DesktopLocation = new Point(s.WorkingArea.Width / 2 - Width / 2, s.WorkingArea.Height / 2 - Height / 2);
        }

        public static Rectangle GetRTLCoordinates(Rectangle container, Rectangle drawRectangle)
        {
            return new Rectangle(
                container.Width - drawRectangle.Width - drawRectangle.X,
                drawRectangle.Y,
                drawRectangle.Width,
                drawRectangle.Height
            );
        }

        /*private void MB_Studio_MdiChildActivate(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
                tabControl.Visible = false;
            // If no any child form, hide tabControl 
            else
            {
                ActiveMdiChild.WindowState = FormWindowState.Maximized;
                // Child form always maximized 

                // If child form is new and no has tabPage, 
                // create new tabPage 
                if (ActiveMdiChild.Tag == null)
                {
                    // Add a tabPage to tabControl with child 
                    // form caption 
                    TabPage tp = new TabPage(ActiveMdiChild.Text)
                    {
                        Tag = ActiveMdiChild,
                        Parent = tabControl
                    };
                    tabControl.SelectedTab = tp;

                    ActiveMdiChild.Tag = tp;
                    ActiveMdiChild.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);
                }

                if (!tabControl.Visible)
                    tabControl.Visible = true;
            }
        }*/

        private void AddNewTab(Form frm)
        {
            bool specialForm = false;
            TabPage tab = new TabPage(frm.Text) {
                Name = frm.Name//is the same name good idea?
            };
            Control[] ccc = frm.Controls.Find("title_lbl", false);

            if (ccc.Length != 0)
                specialForm = !specialForm; // true

            frm.TopLevel = false;
            frm.Parent = tab;

            //frm.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            //frm.Dock = DockStyle.Left;

            tabControl.TabPages.Add(tab);

            tabControl.SelectedTab = tab;
            tab.BackColor = frm.BackColor;
            tab.AutoScroll = true;

            if (!specialForm)
                frm.FormBorderStyle = FormBorderStyle.None;
            else
                frm.Top = frm.Top - ccc[0].Height;

            frm.Visible = true;
        }

        public void AddFormToTab(Form frm, int index = 0, bool autoAdjust = false)
        {
            bool specialForm = false;
            Control[] ccc = frm.Controls.Find("title_lbl", false);

            if (ccc.Length != 0)
                specialForm = !specialForm; // true

            frm.TopLevel = false;
            frm.Parent = tabControl.TabPages[index];

            if (!specialForm)
                frm.FormBorderStyle = FormBorderStyle.None;
            else
                frm.Top = frm.Top - ccc[0].Height;
            
            if (autoAdjust && tabControl.TabPages[index].HasChildren)
            {
                int count = tabControl.TabPages[index].Controls.Count;
                int maxWidth = 0, maxHeight = 0;
                for (int i = 0; i < count; i++)
                {
                    if (tabControl.TabPages[index].Controls[i].Width > maxWidth)
                        maxWidth = tabControl.TabPages[index].Controls[i].Width;
                    if (tabControl.TabPages[index].Controls[i].Height > maxHeight)
                        maxHeight = tabControl.TabPages[index].Controls[i].Height;
                }
                /*MessageBox.Show(//Debug MessageBox for resize
                    frm.Height + ";" + frm.Width + ";" + maxWidth + ";" + maxHeight,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );*/
                if (frm.Width > (tabControl.TabPages[index].Width - maxWidth))
                {
                    if (frm.Height <= (tabControl.TabPages[index].Height - maxHeight))
                    {
                        frm.Top = maxHeight;
                        frm.Left = 0;
                    }
                    else
                    {
                        MessageBox.Show(
                            "ERROR: RESIZE_NOT_IMPLEMENTED",
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    frm.Left = maxWidth;
                }
            }

            frm.Visible = true;
        }

        private bool IsShownAsTab(SpecialForm form)
        {
            bool b = false;
            foreach (TabPage tab in tabControl.TabPages)
                if (tab.Text.Equals(form.Text))
                    b = true;
            return b;
        }

        /*private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((sender as ToolForm).Tag as TabPage).Dispose();
        }*/

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            //This code will render a "x" mark at the end of the Tab caption. 
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - CLOSE_AREA, e.Bounds.Top + 4);
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + LEADING_SPACE, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void Console_richTxt_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            console_richTxt.SelectionStart = console_richTxt.Text.Length;
            // scroll it automatically
            console_richTxt.ScrollToCaret();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(ClientSize.Width - C_GRIP, ClientSize.Height - C_GRIP, C_GRIP, C_GRIP);
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor, rc);
            rc = new Rectangle(0, 0, ClientSize.Width, C_CAPTION);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST) // Trap WM_NCHITTEST
            {   
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = PointToClient(pos);
                if (pos.Y < C_CAPTION)
                {
                    m.Result = (IntPtr)HT_CAPTION;  // HTCAPTION
                    return;
                }
                if (pos.X >= ClientSize.Width - C_GRIP && pos.Y >= ClientSize.Height - C_GRIP)
                {
                    m.Result = (IntPtr)HT_BOTTOM_RIGHT; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void OpenFile_ofd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string fileName = Path.GetFileName(openFile_ofd.FileName);
            string endPoint = fileName.Substring(fileName.LastIndexOf('.') + 1);

            //RemoveAllTabPagesExeptConsole();

            if (endPoint.Equals("mbsp"))
                LoadProject(ProgramConsole.ReadProjectFileInfoFromFile(openFile_ofd.FileName)[1]);
            else
                MessageBox.Show(
                    "Invalid Projectformat: " + endPoint + Environment.NewLine + "Selected file: " + openFile_ofd.FileName,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
        }

        private void RemoveAllTabPagesExeptConsole()
        {
            int consoleIndex = tabControl.TabPages.IndexOfKey("console_tab");
            int tabsCount = tabControl.TabPages.Count;
            for (int i = tabsCount - 1; i >= 0; i--)
                if (i != consoleIndex)
                    tabControl.TabPages.RemoveAt(i);
        }

        private void LoadProjectExplorer(bool projectShown = true)
        {
            PrepareToolBar(projectShown);
            projectFiles_lb.Items.Clear();

            // VORERST // - AddRange later with optimized file names(?)
            projectFiles_lb.Items.Add("Troops");
            projectFiles_lb.Items.Add("Party Templates");
            projectFiles_lb.Items.Add("Parties");
            projectFiles_lb.Items.Add("Menus");
            projectFiles_lb.Items.Add("Items");
            //projectFiles_lb.Items.Add("Presentations");
            //projectFiles_lb.Items.Add("...");
            // VORERST //
        }

        private void ProjectFiles_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpecialForm form = null;

            if (projectFiles_lb.SelectedIndex >= 0)
            {
                int index = -1;
                string item = projectFiles_lb.SelectedItem.ToString();

                if (item.Equals("Troops"))
                {
                    form = new TroopManager();
                    index = tabControl.TabPages.IndexOfKey("ItemManager");
                }
                else if (item.Equals("Party Templates"))
                    form = new PartyTemplateManager();
                else if (item.Equals("Parties"))
                    form = new PartyManager();
                else if (item.Equals("Menus"))
                    form = new MenuManager();
                else if (item.Equals("Items"))
                {
                    form = new ItemManager();
                    index = tabControl.TabPages.IndexOfKey("TroopManager");
                }
                /*else if (item.Equals("Presentations"))
                    form = new PresentationManager();*/
                
                if (index >= 0)
                    tabControl.TabPages[index].Dispose();
            }

            if (form != null)
            {
                if (!IsShownAsTab(form))
                    AddNewTab(form);
                else
                    tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey(form.Name);//TabControlGetIndexOf(form.Text);
            }
        }

        private void PrepareToolBar(bool projectShown = true)
        {
            bool projectOpenAlready = project_btn.Visible;
            if (projectOpenAlready != projectShown)
            {
                int widthToMove = 0;
                List<Button> unusedButtons = new List<Button> {
                    project_btn,
                    build_btn
                };

                foreach (Button b in unusedButtons)
                {
                    widthToMove += b.Width;
                    b.Visible = !b.Visible;
                }

                if (projectShown && !projectOpenAlready)// maybe change this later as well --> automated on used Controls / Buttons
                {
                    help_btn.Left += widthToMove;
                    mbOptions_btn.Left += widthToMove;
                }
                else if (!projectShown && projectOpenAlready)
                {
                    help_btn.Left -= widthToMove;
                    mbOptions_btn.Left -= widthToMove;
                }
            }
        }

        #endregion

        #region Useful Methods

        public static string GetCorrectFileName(string filename)
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

        private CreateProject CreateNewProject()
        {
            CreateProject cp = new CreateProject();
            cp.ShowDialog();
            return cp;
        }

        public static List<List<string>> LoadAllPseudoCodeByFile(string pseudoCodeFile)
        {
            List<List<string>> typesCodes = new List<List<string>>();
            if (File.Exists(pseudoCodeFile))
            {
                using (StreamReader sr = new StreamReader(pseudoCodeFile))
                {
                    string line = string.Empty;
                    while (!sr.EndOfStream)
                    {
                        if (IsNewPseudoCode(line))
                        {
                            List<string> typeCodeX = new List<string>();
                            do
                            {
                                typeCodeX.Add(line);
                                line = sr.ReadLine();
                            } while (!IsNewPseudoCode(line) && !line.Equals(EOF_TXT));
                            typesCodes.Add(typeCodeX);
                        }
                        else
                            line = sr.ReadLine();
                    }
                }
            }
            return typesCodes;
        }

        public static List<List<string>> LoadAllPseudoCodeByObjectTypeID(int objectTypeID)
        {
            return LoadAllPseudoCodeByFile(CodeReader.ProjectPath + "\\pseudoCodes\\" + CodeReader.Files[objectTypeID].Split('.')[0] + ".mbpc");
        }

        public static void SavePseudoCodeByType(Skriptum type, string[] code)
        {
            List<List<string>> typesCodes;

            string pseudoCodeFile = CodeReader.ProjectPath + "\\pseudoCodes\\" + CodeReader.Files[type.Typ].Split('.')[0] + ".mbpc";
            string directory = Path.GetDirectoryName(pseudoCodeFile);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            bool found = false;
            string id = ":";
            bool isTroop = type.ObjectTyp == Skriptum.ObjectType.TROOP;
            if (!isTroop)
                id += type.ID;
            else
                id += ((Troop)type).ID;

            List<string> typeCode = new List<string> { id };
            for (int i = 0; i < code.Length; i++)
                typeCode.Add(code[i]);

            if (File.Exists(pseudoCodeFile))
            {
                typesCodes = LoadAllPseudoCodeByFile(pseudoCodeFile);
                for (int i = 0; i < typesCodes.Count; i++)
                {
                    string tmp = typesCodes[i][0].Substring(1);
                    bool b;
                    if (!isTroop)
                        b = tmp.Equals(type.ID);
                    else
                        b = tmp.Equals(((Troop)type).ID);
                    if (b)
                        typesCodes[i] = typeCode;
                    found = b;
                    if (found)
                        i = typesCodes.Count;
                }
            }
            else
                typesCodes = new List<List<string>>();

            if (!found)
                typesCodes.Add(typeCode);

            using (StreamWriter wr = new StreamWriter(pseudoCodeFile))
            {
                foreach (List<string> typeCodeX in typesCodes)
                    foreach (string line in typeCodeX)
                        wr.WriteLine(line);
                wr.Write(EOF_TXT);
            }
        }

        private static bool IsNewPseudoCode(string s)
        {
            bool b = false;
            if (s.Length > 1)
                if (s.Substring(0, 1).Equals(":") && !s.Contains(" "))
                    b = true;
            return b;
        }

        #endregion
    }
}
