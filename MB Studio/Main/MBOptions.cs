using importantLib;
using MB_Studio_CLI;
using MB_Studio.Support;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MB_Studio.Main
{
    public partial class MBOptions : SpecialForm
    {
        public MBOptions()
        {
            InitializeComponent();
            BackColor = Properties.Settings.Default.baseColor;
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
        }

        private void ExtraOptions_Load(object sender, EventArgs e)
        {
            title_lbl.Text = Name;
            projectsFolder_txt.Text = Path.GetFullPath(Properties.Settings.Default.projectsFolderPath);
            show3DView_cb.Checked = Properties.Settings.Default.show3DView;
            loadSavedObjects_cb.Checked = Properties.Settings.Default.loadSavedObjects;

            int langIdx = Properties.Settings.Default.languageIndex;
            if (langIdx < language_cbb.Items.Count)
                language_cbb.SelectedIndex = langIdx;
            else
                language_cbb.SelectedIndex = 2;//default en

            int updateChannelIndex = updateChannel_cbb.Items.IndexOf(Properties.Settings.Default.updateChannel);
            if (updateChannelIndex < 0)
                updateChannelIndex = 0;
            updateChannel_cbb.SelectedIndex = updateChannelIndex;

            options_tree.HideSelection = false;
            options_tree.SelectedNode = options_tree.Nodes[0];
        }

        private static void PropertiesSaveAndReload()
        {
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Min_btn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Options_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string eNodeName = e.Node.Name;

            foreach (Control panel in Controls)
                if (GetNameEndOfControl(panel).Equals("panel"))
                    panel.Visible = false;

            if (eNodeName.StartsWith("general"))
            {
                generalSettings_panel.Visible = true;
                generalSettings_panel.BringToFront();
            }
            else if (eNodeName.StartsWith("projectsS"))
                options_tree.SelectedNode = options_tree.SelectedNode.Nodes[0];
            else if (eNodeName.StartsWith("projectsF"))
            {
                projects_panel.Visible = true;
                projects_panel.BringToFront();
            }
            else if (eNodeName.StartsWith("generate"))
                new HeaderValueTool().ShowDialog();
        }

        private void Save_ProjectsFolder_btn_Click(object sender, EventArgs e)
        {
            DialogResult dlr = DialogResult.No;
            string currentPath = Path.GetFullPath(Properties.Settings.Default.projectsFolderPath);
            string newPath = Path.GetFullPath(projectsFolder_txt.Text);
            bool copy = false, overrideExisting = false, useCurrentProjects = false;

            if (Directory.GetFileSystemEntries(currentPath).Length > 0)
            {
                dlr = MessageBox.Show("Do you want to have all existing projects in the new projectsfolder as well?",
                        Application.ProductName,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                useCurrentProjects = dlr == DialogResult.Yes;
                if (useCurrentProjects && dlr != DialogResult.Cancel)
                {
                    dlr = MessageBox.Show("Do you want to move all projects from \"" + currentPath + "\" to \"" + newPath + "\"?" + Environment.NewLine
                                        + "If 'NO', they will be copied instead of moving!",
                                        Application.ProductName,
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Information,
                                        MessageBoxDefaultButton.Button1);
                    copy = dlr == DialogResult.No;
                }
            }
            if (dlr != DialogResult.Cancel)
            {
                if (useCurrentProjects)
                {
                    ChangeProjectPathsToNewOne(currentPath, newPath);

                    dlr = MessageBox.Show("Do you want to override existing files?",
                        Application.ProductName,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    overrideExisting = dlr == DialogResult.Yes;
                    if (dlr != DialogResult.Cancel)
                    {
                        CopyPathDirectories(currentPath, newPath, copy, overrideExisting);
                        if (!copy)
                            foreach (string dir in Directory.GetDirectories(currentPath))
                                Directory.Delete(dir, true);
                    }
                }

                Properties.Settings.Default.projectsFolderPath = newPath;
                PropertiesSaveAndReload();

                MessageBox.Show("Path successfully changed to:" + Environment.NewLine + Properties.Settings.Default.projectsFolderPath,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void ChangeProjectPathsToNewOne(string currentPath, string newPath)
        {
            foreach (string dir in Directory.GetDirectories(currentPath))
            {
                string[] info = ProgramConsole.ReadProjectFileInfoInFolder(dir);
                if (info != null)
                {
                    info[1] = newPath + ImportantMethods.GetDirectoryNameOnly(info[1]);
                    ProgramConsole.SaveProjectFileInfo(dir, info);
                }
            }
        }

        private void CopyPathDirectories(string path, string destPath, bool copy = false, bool overrideExisting = false)
        {
            foreach (string dir in Directory.GetDirectories(path))
            {
                string newDir = destPath + "\\" + ImportantMethods.GetDirectoryNameOnly(dir);
                if (!Directory.Exists(newDir))
                    Directory.CreateDirectory(newDir);
                if (Directory.GetDirectories(dir).Length > 0)
                    CopyPathDirectories(dir, newDir, copy);
                if (copy)
                    CopyPathFiles(dir, newDir, overrideExisting);
                else
                    MovePathFiles(dir, newDir);
            }
        }

        private void CopyPathFiles(string path, string destPath, bool overrideExisting = false)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                if (!File.Exists(destPath + "\\" + Path.GetFileName(file)) || overrideExisting)
                    File.Copy(file, destPath + "\\" + Path.GetFileName(file), overrideExisting);
                else
                    File.Copy(file, destPath + "\\" + Path.GetFileName(file.Remove(file.LastIndexOf('.'))) + " (2)" + file.Substring(file.LastIndexOf('.')));
            }
        }

        private void MovePathFiles(string path, string destPath)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                if (!File.Exists(destPath + "\\" + Path.GetFileName(file)))
                    File.Move(file, destPath + "\\" + Path.GetFileName(file));
                else
                    File.Move(file, destPath + "\\" + Path.GetFileName(file.Remove(file.LastIndexOf('.'))) + " (2)" + file.Substring(file.LastIndexOf('.')));
            }
        }

        private void SelectProjectsFolder_btn_Click(object sender, EventArgs e)
        {
            projectsPathBrowser_fbd.ShowDialog();
            if (projectsPathBrowser_fbd.SelectedPath != null)
                if (!projectsPathBrowser_fbd.SelectedPath.Equals(string.Empty))
                    projectsFolder_txt.Text = projectsPathBrowser_fbd.SelectedPath + '\\';
        }

        private void LoadSavedObjects_cb_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.loadSavedObjects = loadSavedObjects_cb.Checked;
            PropertiesSaveAndReload();
        }

        private void Show3DView_cb_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.show3DView = show3DView_cb.Checked;
            PropertiesSaveAndReload();
        }

        private void BaseColor_lbl_Click(object sender, EventArgs e)
        {
            colorBase_cd.Color = baseColor_lbl.BackColor;

            colorBase_cd.ShowDialog();

            DialogResult result = MessageBox.Show(
                "Is used after restart!" + Environment.NewLine + "Restart now?",
                Application.ProductName,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1
            );

            if (result == DialogResult.Yes || result == DialogResult.No)
            {
                baseColor_lbl.BackColor = colorBase_cd.Color;

                Properties.Settings.Default.baseColor = colorBase_cd.Color;
                PropertiesSaveAndReload();

                if (result == DialogResult.Yes)
                {
                    Process.Start(Application.ExecutablePath);
                    Application.Exit();
                }
            }
        }

        private void UpdateChannel_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.updateChannel = updateChannel_cbb.SelectedItem.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void Language_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.languageIndex = (byte)language_cbb.SelectedIndex;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
