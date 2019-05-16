using importantLib;
using MB_Studio_CLI;
using MB_Studio.Main;
using MB_Studio.Manager;
using MB_Studio_Library.IO;
using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Reflection;

namespace MB_Studio
{
    public partial class MB_Studio : SpecialForm
    {
        #region Constants

        public enum TabMode
        {
            Default = 0,
            FillWidth = 1,
            FillHeight = 2,
            FillTab = FillWidth | FillHeight,
        }

        private const int LEADING_SPACE = 12;
        private const int CLOSE_SPACE = 15;
        private const int CLOSE_AREA = CLOSE_SPACE;

        private const int C_GRIP = 16;                      // Grip size
        private const int C_CAPTION = 32;                   // Caption bar height;
        private const int HT_BOTTOM_RIGHT = 17;             // Caption position
        private const int WM_NCHITTEST = 0x84;              // Message Type

        public const string CSV_FORMAT = ".csv";
        public const string TEXT_FORMAT = ".txt";

        #endregion

        #region Attributes

        private bool IsEqualVersion = true;

        private bool FullScreen = true;

        private readonly ScriptCommander scriptCommander = new ScriptCommander();

        // instance member to keep reference to splash form
        private SplashForm frmSplash;

        // delegate for the UI updater
        private delegate void UpdateUIDelegate(bool IsDataLoaded);

        private Thread loadingThread;

        #endregion

        #region Properties

        public static bool DebugMode = false;

        public static bool RunAutoUpdate { get; private set; } = Properties.Settings.Default.runAutoUpdate;

        public static bool Show3DView { get { return Properties.Settings.Default.show3DView; } }

        #endregion

        #region Loading

        public MB_Studio()
        {
            StartLoadingForm();

            UpdateVersionData();

            if (Properties.Settings.Default.firstRun)
                AskForOldConfigImport();

            if (RunAutoUpdate)
                RunAutoUpdate = !File.Exists("debugMode.enabled");

            if (RunAutoUpdate)
                CheckForUpdates();

            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);

            ResizeEnd += MB_Studio_ResizeEnd;
            name_lbl.MouseDown += Control_MoveForm_MouseDown;
            file_btn.Click += Button_Open_ContextMenuStrip_Click;
            project_btn.Click += Button_Open_ContextMenuStrip_Click;
        }

        private void AskForOldConfigImport()
        {
            string[] curVal = Application.ProductVersion.Split('.');
            uint[] last = null;
            uint[] curVersCodes = new uint[curVal.Length];
            for (int i = 0; i < curVersCodes.Length; i++)
                curVersCodes[i] = uint.Parse(curVal[i]);
            
            string configDirPath = Path.GetDirectoryName(Application.LocalUserAppDataPath);
            configDirPath = Path.GetDirectoryName(configDirPath);
            string appName = Application.ProductName.Replace(' ', '_');

            string path = string.Empty; // TODO: use correct path later
            foreach (string dir in Directory.GetDirectories(configDirPath))
            {
                string fxxx = Path.GetFileName(dir).Replace(' ', '_');
                if (fxxx.StartsWith(appName))
                {
                    foreach (string dir2 in Directory.GetDirectories(dir))
                    {
                        string[] val = Path.GetFileName(dir2).Split('.');
                        uint[] versCodes = new uint[val.Length];
                        for (int i = 0; i < versCodes.Length; i++)
                            versCodes[i] = uint.Parse(val[i]);
                        if (curVersCodes[0] >= versCodes[0])
                        {
                            bool higher = curVersCodes[0] > versCodes[0];
                            if (!higher)
                            {
                                for (int i = 0; i < versCodes.Length && !higher; i++)
                                    if (curVersCodes[i] >= versCodes[i])
                                        higher = (curVersCodes[i] > versCodes[i]);
                            }
                            if (higher)
                            {
                                bool lastLower = false;
                                if (last != null)
                                {
                                    for (int i = 0; i < versCodes.Length && !lastLower; i++)
                                        if (last[i] <= versCodes[i])
                                            lastLower = (last[i] < versCodes[i]);
                                }
                                else
                                    lastLower = true;
                                if (lastLower)
                                {
                                    last = versCodes;
                                    path = Path.GetFullPath(dir2);
                                }
                            }
                        }
                    }
                }
            }

            if (last != null)
            {
                DialogResult result = DialogResult.Yes;
                /*DialogResult result = MessageBox.Show(
                    "Do you want to import the old config from version " +
                    last[0] + "." + last[1] + "." + last[2] + "." + last[3] +
                    "?"// + 
                    //Environment.NewLine + path + "\\user.config"
                    ,
                    Application.ProductName,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                );*/

                if (result != DialogResult.Cancel)
                {
                    if (result == DialogResult.Yes)
                    {
                        //File.Copy(path, "studio.config");
                        Properties.Settings.Default.Upgrade();
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();
                    }

                    Properties.Settings.Default.firstRun = false;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                }
                else
                    Environment.Exit(0);
            }
        }

        /// <summary>
        /// Updates version in version.dat if necessary
        /// </summary>
        /// <returns>Update was necessary</returns>
        private bool UpdateVersionData()
        {
            string versionFile = "version.dat";
            bool fileExists = File.Exists(versionFile);
            if (fileExists) IsEqualVersion = File.ReadAllText(versionFile).Equals(ProductVersion);

            if (!IsEqualVersion)
                File.WriteAllText(versionFile, Application.ProductVersion);

            UpdateRegistryData();

            return !IsEqualVersion;
        }

        private void UpdateRegistryData()
        {
            bool foundKey = false;
            bool updateProductVersion = false;
            bool updateProductName = false;

            string regKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\MB Studio";
            try
            {
                using (RegistryKey reg32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    using (RegistryKey appReg = reg32.OpenSubKey(regKey))
                    {
                        string oldProductVersion = appReg.GetValue("DisplayVersion", string.Empty).ToString();
                        updateProductVersion = (oldProductVersion.Length != 0 && !oldProductVersion.Equals(Application.ProductVersion));

                        string oldProductName = appReg.GetValue("DisplayName", string.Empty).ToString();
                        updateProductName = (oldProductName.Length != 0 && !oldProductName.Equals(Application.ProductName));

                        foundKey = true;
                    }
                }
            }
            catch (Exception) { }

            if (!updateProductName && !updateProductVersion && foundKey) return;

            string arguments = "\"" + regKey + "\" \"" + Application.ProductVersion + "\" \"" + Application.ProductName + "\"";
            if (!foundKey)
            {
                arguments += " -createNew";
            }

            var psi = new ProcessStartInfo {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = "RegUpdater.exe",
                Arguments = arguments
            };

            var process = new Process {
                StartInfo = psi
            };
            process.Start();
            process.WaitForExit();
        }
        
        private void CheckForUpdates()
        {
            Process process = new Process();
            process.StartInfo.Arguments = "-gui " + Properties.Settings.Default.updateChannel + " . -startOE";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = Application.StartupPath + "\\MB Studio Updater.exe";
            process.Start();
        }

        private void SetTabControlFixedHeight()
        {
            tabControl.Height = projectExplorer_group.Height + 4;
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
            //file_cm.RenderMode = ToolStripRenderMode.Custom;//search for custom Renderer!!!
            name_lbl.Text = Text;
            InitializeTabControl();

            Shown += MB_Studio_Shown;//SetFullScreenByHandle(Handle);

            name_lbl.Text += " - " + Properties.Resources.buildName;

            InitializeProject();
            LoadLastOpenedProjects();

            LoadColorsAndView();

            CloseLoadingForm();
        }

        private void MB_Studio_Shown(object sender, EventArgs e)
        {
            SetFullScreenByHandle();
            SetTabControlFixedHeight();

            if (!IsEqualVersion)
            {
                Changelog changelog = new Changelog();
                changelog.LoadCurrentChangelog();
                changelog.ShowDialog();
            }
        }

        //Not finished yet - just started
        private void LoadColorsAndView()
        {
            Color baseColor = Properties.Settings.Default.baseColor;
            Color minorColor1 = Color.FromArgb(Math.Max(baseColor.R - 14, 0), Math.Max(baseColor.G - 14, 0), Math.Max(baseColor.B - 14, 0));

            ToolForm.BaseColor = baseColor;

            BackColor = baseColor;
            projectFiles_lb.BackColor = baseColor;
            toolbarAndHead_lbl.BackColor = baseColor;
            name_lbl.BackColor = baseColor;
            company_icon_pb.BackColor = baseColor;
            min_btn.BackColor = baseColor;
            exit_btn.BackColor = baseColor;
            maxnorm_btn.BackColor = baseColor;
            
            foreach (TabPage tab in tabControl.TabPages)
                tab.BackColor = minorColor1;
        }

        private void StartLoadingForm()
        {
            loadingThread = new Thread(new ThreadStart(ShowLoadingForm)) {
                IsBackground = true
            };
            loadingThread.Start();
        }

        private void ShowLoadingForm()
        {
            // Update UI
            UpdateUI(false);

            // Show the splash form
            if (!DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                WindowState = FormWindowState.Minimized;
                frmSplash = new Loader(this, false) {
                    StartPosition = FormStartPosition.CenterScreen
                };
                Application.Run(frmSplash);
            }
        }

        private void CloseLoadingForm()
        {
            Invoke((MethodInvoker)delegate {
                UpdateUI(true);
            });
        }

        /// <summary>
        /// Updates the UI
        /// </summary>
        protected void UpdateUI(bool IsDataLoaded)
        {
            if (IsDataLoaded)
            {
                if (frmSplash != null)
                    frmSplash.Close();
                WindowState = FormWindowState.Normal;
            }
        }

        private bool LoadProject(string projectPath)
        {
            bool success = false;
            try
            {
                ProgramConsole.LoadProject(projectPath, true);
                RemoveAllTabPagesExeptConsole();
                LoadProjectExplorer();
                success = true;
            }
            catch (Exception) { }
            return success;
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
            string iconLocation = path + "\\project.ico";
            if (!File.Exists(iconLocation))
            {
                iconLocation = CodeReader.FILES_PATH + "images\\warband.ico"; // later depending on game (version)
            }

            ProjectObject PObject = new ProjectObject()
            {
                ProjectName = name, //Text = name,
                ProjectPath = path, //
                AutoSize = false,
                IconLocation = iconLocation,
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
            ProjectObject pObject = (ProjectObject)sender;//Button b = (Button)sender;
            string path = pObject.ProjectPath;//b.Tag.ToString();

            bool loadSuccess = false;
            if (Directory.Exists(path))
            {
                loadSuccess = LoadProject(path);
            }

            if (!loadSuccess)
            {
                DialogResult result = MessageBox.Show(
                    "The selected project couldn't be found or is corrupted!" + Environment.NewLine +
                    "Do you want to remove this entry?",
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
            if (s.Equals(ProgramConsole.DEFAULT_STEAMPATH))
                ProgramConsole.SetModPath(false);

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
                projectVorlagenSearch_txt.Text = " Search project template";
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
                SetNormalScreen(s);//WindowState = FormWindowState.Maximized;
            else
                SetFullScreenByHandle();//WindowState = FormWindowState.Normal;
            SetTabControlFixedHeight();
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
            DialogResult dlr = MessageBox.Show(
                "Do you really want to overwrite the files in " + ProgramConsole.DestinationMod + "!?",
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (dlr == DialogResult.Yes)
            {
                tabControl.SelectedIndex = 0;
                CodeWriter.WriteAllCode(console_richTxt, ProgramConsole.DestinationModPath);
                /*Thread t = new Thread(new ThreadStart((MethodInvoker)delegate
                {
                    while (!CodeWriter.IsFinished) ;
                    AutoFixer.FixAll();
                })) { IsBackground = true };
                t.Start();*/
            }
        }

        private void MbOptions_btn_Click(object sender, EventArgs e)
        {
            AddNewTab(new MBOptions(), TabMode.FillTab);
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
            DialogResult dlr = MessageBox.Show(
                "Do you know what you are doing?",                            
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (dlr == DialogResult.Yes)
                AddNewTab(new ImportsManagerGUI(), TabMode.FillTab); //new ImportsManagerGUI().Show();
        }

        private void Properties_ToolStrip_Click(object sender, EventArgs e)
        {
            AddNewTab(new ProjectProperties(), TabMode.FillTab);//.ShowDialog();
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
            MessageBox.Show("Here you will be able to create a new file, if needed!");
        }

        #endregion

        #region GUI

        protected override void SetFullScreenByHandle(IntPtr hWnd = default)
        {
            base.SetFullScreenByHandle(hWnd);
            tabControl.Width = Width - projectExplorer_group.Width - 12;
            maxnorm_btn.Text = "\u25F1";// = "◱" //"⬜"
        }

        private void SetNormalScreen(Screen s)
        {
            maxnorm_btn.Text = "\u25A0";//"◼";
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

        private void AddNewTab(SpecialForm form, TabMode tabMode = TabMode.Default)
        {
            if (IsShownAsTab(form))
            {
                tabControl.SelectedIndex = tabControl.TabPages.IndexOfKey(form.Name);//TabControlGetIndexOf(form.Text);
                return;
            }

            bool specialForm = false;
            TabPage tab = new TabPage(form.Text) {
                Name = form.Name,//is the same name good idea?
                //Parent = tabControl,
            };
            Control[] ccc = form.Controls.Find("title_lbl", false);

            if (ccc.Length != 0)
                specialForm = !specialForm; // true

            tabControl.TabPages.Add(tab);

            form.TopLevel = false;
            form.Parent = tab;

            tabControl.SelectedTab = tab;

            tab.BackColor = form.BackColor;//check for problems with baseColor here
            tab.AutoScroll = true;

            if (!specialForm)
                form.FormBorderStyle = FormBorderStyle.None;
            else
                form.Top -= ccc[0].Height;

            if (tabMode != TabMode.Default)
            {
                if ((tabMode & TabMode.FillWidth) == TabMode.FillWidth)
                {
                    form.Width = tab.Width;
                }
                if ((tabMode & TabMode.FillHeight) == TabMode.FillHeight)
                {
                    form.Height = tab.Height;
                }
            }
            //else
            //{
            //    form.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            //    form.Dock = DockStyle.Left;
            //}

            form.Visible = true;
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
                frm.Top -= ccc[0].Height;
            
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
                    tabControl.TabPages[i].Dispose();//RemoveAt(i);
        }

        private void LoadProjectExplorer(bool projectShown = true)
        {
            PrepareToolBar(projectShown);
            projectFiles_lb.Items.Clear();

            // Load custom manager
            scriptCommander.LoadManagers();

            List<string> managerItems = new List<string>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                foreach (Type t in assembly.GetTypes())
                    if (t.BaseType != null && t.BaseType.ToString().ToUpper().EndsWith("TOOLFORM"))
                        managerItems.Add(ConvertManagerNameToItemName(t.Name));

            managerItems.Sort();
            projectFiles_lb.Items.AddRange(managerItems.ToArray());
        }

        private void ProjectFiles_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectFiles_lb.SelectedIndex >= 0)
            {
                SpecialForm form = null;
                string item = ConvertItemNameToManagerName(projectFiles_lb.SelectedItem.ToString());
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type t in assembly.GetTypes())
                    {
                        if (t.BaseType != null && t.BaseType.ToString().ToUpper().EndsWith("TOOLFORM"))
                            if (t.Name.Equals(item))
                                form = (ToolForm)ImportantMethods.GetInstanceByClassName(t.FullName);
                    }
                }

                if (form != null)
                    AddNewTab(form, TabMode.FillHeight);
            }
        }

        private string ConvertItemNameToManagerName(string itemName)
        {
            itemName = itemName.TrimEnd('s');

            if (itemName.EndsWith("ie"))
                itemName = itemName.Remove(itemName.Length - 2) + 'y'; // check later

            itemName += "Manager";

            return itemName;
        }

        private string ConvertManagerNameToItemName(string managerName)
        {
            if (managerName.EndsWith("Manager"))
                managerName = managerName.Remove(managerName.LastIndexOf("Manager"));

            if (managerName.EndsWith("y"))
                managerName = managerName.TrimEnd('y') + "ie"; // check later

            managerName += "s";

            return managerName;
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

        #endregion
    }
}
