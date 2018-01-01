using importantLib;
using MB_Decompiler;
using MB_Studio.Support;
using System;
using System.IO;
using System.Windows.Forms;

namespace MB_Studio.Main
{
    public partial class MBOptions : SpecialForm
    {
        public MBOptions()
        {
            InitializeComponent();
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
        }

        private void ExtraOptions_Load(object sender, EventArgs e)
        {
            title_lbl.Text = Name;
            projectsFolder_txt.Text = Path.GetFullPath(Properties.Settings.Default.projectsFolderPath);
            options_tree.SelectedNode = options_tree.Nodes[0];
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
            string eNodeBegin = e.Node.Name.Substring(0, 5);

            projects_panel.Visible = false;

            if (eNodeBegin.Equals("gener"))
                new HeaderValueTool().ShowDialog();
            else if (eNodeBegin.Equals("proje"))
            {
                projects_panel.Visible = true;
                projects_panel.BringToFront();
            }
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
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

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
    }
}
