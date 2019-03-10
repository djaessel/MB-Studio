using importantLib;
using MB_Studio_CLI;
using MB_Studio_Library.IO;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MB_Studio.Main
{
    public partial class CreateProject : SpecialFormBlack
    {
        #region Properties

        public bool ProjectCreated { get; private set; }

        #endregion

        public CreateProject() : base()
        {
            ProjectCreated = false;
            InitializeComponent();
        }

        private void CreateProject_Load(object sender, EventArgs e)
        {
            string modulesDir = ImportantMethods.GetDirectoryPathOnly(ProgramConsole.GetModuleInfoPath());

            bool invalidModule = true;
            bool noModuleIni = true;
            DialogResult dialogResult = DialogResult.OK;
            PathSelector pathSelector = new PathSelector("Modules");
            while (invalidModule && dialogResult == DialogResult.OK)
            {
                dialogResult = pathSelector.ShowDialog();
                modulesDir = pathSelector.SelectedPath;
                noModuleIni = !File.Exists(modulesDir + "\\module.ini");

                invalidModule = (!Directory.Exists(modulesDir) || noModuleIni);

                pathSelector.SetError(invalidModule);
            }

            if (dialogResult != DialogResult.OK)
            {
                Close();
            }

            modulesDir += '\\';

            string[] modules = Directory.GetDirectories(modulesDir.TrimEnd('\\'));
            foreach (string module in modules)
                modules_cbb.Items.Add(ImportantMethods.GetDirectoryNameOnly(module));

            string s = "Native";
            if (!Directory.Exists(modulesDir + s))
            {

                if (modules.Length > 0)
                {
                    s = modules[0];
                    if (Directory.Exists(modulesDir + s))
                    {
                        int i = 0;
                        do
                        {
                            i++;
                        } while (Directory.Exists(modulesDir + s + i));
                        s += i;
                    }
                }
                else
                    s = string.Empty;
            }
            modules_cbb.SelectedIndex = modules_cbb.Items.IndexOf(s);
        }

        private void UseDefaultVariables_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (!useDefaultVariables_cb.Checked && copyTextFiles_cb.CheckState == CheckState.Checked)
                copyTextFiles_cb.CheckState = CheckState.Indeterminate;
            else if (useDefaultVariables_cb.Checked && copyTextFiles_cb.CheckState != CheckState.Checked)
                copyTextFiles_cb.CheckState = CheckState.Checked;
        }

        private void CopyTextFiles_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (copyTextFiles_cb.CheckState != CheckState.Indeterminate)
                useDefaultVariables_cb.CheckState = copyTextFiles_cb.CheckState;
        }

        private void Create_btn_Click(object sender, EventArgs e)
        {
            bool newDir = false;
            bool isOK = false;
            if (!useOriginalMod_cb.Checked)
            {
                // !!! HAS TO BE HERE FOR NOW !!! //
                string destModPath = ImportantMethods.GetDirectoryPathOnly(ProgramConsole.GetModuleInfoPath()) + '\\' + destinationModul_txt.Text;
                newDir = !Directory.Exists(destModPath);
                if (newDir)
                    Directory.CreateDirectory(destModPath);
                // !!! HAS TO BE HERE FOR NOW !!! //
            }

            isOK = CreateProjectFolder();
            if (!useOriginalMod_cb.Checked && isOK)
                isOK = CreateModuleFolder(newDir);

            if (isOK)
            {
                ProgramConsole.LoadProject(CodeReader.ProjectPath, true);
                ProjectCreated = true;
            }

            Close();
        }

        private bool CreateProjectFolder()
        {
            if (!copyTextFiles_cb.Checked) return true;

            string destPath = path_txt.Text;
            bool directoryNew = !Directory.Exists(destPath);
            DialogResult forceOverride = DialogResult.Yes;

            if (!directoryNew)
                forceOverride = ShowErrorPathAlreadyExists(destPath);

            if (!directoryNew && forceOverride != DialogResult.Yes) return false;

            string headerFiles = "headerFiles";
            string moduleFiles = "moduleFiles";
            string moduleSystem = "moduleSystem";

            //if (!directoryNew)
            Directory.CreateDirectory(destPath);

            destPath += '\\';

            //if (!Directory.Exists(destPath + headerFiles))
            Directory.CreateDirectory(destPath + headerFiles);
            //if (!Directory.Exists(destPath + moduleFiles))
            Directory.CreateDirectory(destPath + moduleFiles);
            //if (!Directory.Exists(destPath + moduleSystem))
            Directory.CreateDirectory(destPath + moduleSystem);

            //ProgramConsole.SaveNewSelectedMod(modules_cbb.SelectedItem.ToString(), destPath);

            //CodeReader.ProjectPath = destPath; // Initialize??? because this needs to have a own mod and destinion for each project!!! so why not change the extraoption SetMod to this location

            //File.WriteAllText(destPath + "module_info.py", "export_dir = \"" + CodeReader.ModPath.Replace('\\', '/') + '\"');

            string module_info__path = File.ReadAllText(CodeReader.FILES_PATH + "module_info.path");
            string[] info = new string[]
            {
                        name_txt.Text,
                        destPath,
                        modules_cbb.SelectedItem.ToString(),
                        module_info__path.Replace("%MOD_NAME%", modules_cbb.SelectedItem.ToString()),
                        destinationModul_txt.Text,
                        module_info__path.Replace("%MOD_NAME%", destinationModul_txt.Text),
                     // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                        useDefaultVariables_cb.Checked.ToString(),
            };

            CodeReader.ProjectPath = destPath.Trim('\\');

            ProgramConsole.SaveProjectFileInfo(CodeReader.ProjectPath, info); //File.WriteAllLines(destPath + Path.GetFileName(destPath.TrimEnd('\\') + ".mbsp"), info);
            ProgramConsole.SetMods(CodeReader.ProjectPath);

            moduleSystem += '\\';

            CodeWriter.CheckPaths();
            foreach (string file in Directory.GetFiles(CodeWriter.DefaultModuleSystemPath))
                File.Copy(file, destPath + moduleSystem + Path.GetFileName(file), !directoryNew);

            if (useDefaultVariables_cb.Checked)
                CopyVariables(destPath, moduleSystem);

            return true;
        }

        private bool CreateModuleFolder(bool directoryNew = true)
        {
            string modPath = ProgramConsole.OriginalModPath;
            string destPath = ProgramConsole.DestinationModPath;
            List<string> textFileEndings = new List<string>() { "TXT", "INI", "H", "FX", "CSV" };
            DialogResult forceOverride = DialogResult.Yes;

            if (!directoryNew)
                directoryNew = !Directory.Exists(destPath);

            if (!directoryNew)
                forceOverride = ShowErrorPathAlreadyExists(destPath);

            if (!directoryNew && forceOverride != DialogResult.Yes) return false;

            //if (!directoryNew)
            Directory.CreateDirectory(destPath);

            if (!copyTextFiles_cb.Checked) return true;

            List<string> paths = GetAllPaths(modPath.TrimEnd('\\'));
            foreach (string path in paths)
            {
                string px = path.Replace(modPath, string.Empty);
                string fileEnding = string.Empty;

                if (px.Contains("."))
                    fileEnding += path.Substring(path.LastIndexOf('.') + 1).ToUpper();

                if (CodeReader.CountCharInString(px, '\\') > 0)
                {
                    string dir = destPath + Path.GetDirectoryName(px);
                    //if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                    if (textFileEndings.Contains(fileEnding) || copyNonTextFiles_cb.CheckState != CheckState.Unchecked)
                        File.Copy(path, dir + '\\' + Path.GetFileName(px), !directoryNew);
                }
                else if (textFileEndings.Contains(fileEnding) || copyNonTextFiles_cb.CheckState != CheckState.Unchecked)
                    File.Copy(path, destPath + Path.GetFileName(path), !directoryNew);
            }

            return true;
        }

        private void CopyVariables(string destPath, string moduleSystem)
        {
            string vars = "variables.txt";
            DialogResult dlr = DialogResult.Ignore;
            string originalModPath = ProgramConsole.OriginalModPath;
            do
            {
                if (File.Exists(originalModPath + vars))
                    File.Copy(originalModPath + vars, destPath + moduleSystem + vars, true);
                else
                {
                    dlr = MessageBox.Show("The file " + vars + " isn't available!" + Environment.NewLine + "In this case, the file will be generated later!",
                                           Application.ProductName,
                                           MessageBoxButtons.AbortRetryIgnore,
                                           MessageBoxIcon.Warning);
                    if (dlr == DialogResult.Ignore)
                        if (File.Exists(destPath + vars))
                            File.Delete(destPath + vars);
                }
            } while (dlr == DialogResult.Retry);
        }

        private List<string> GetAllPaths(string directory, List<string> paths = null)
        {
            if (Directory.Exists(directory))
            {
                if (paths == null)
                    paths = new List<string>();
                string[] dirs = Directory.GetDirectories(directory);
                foreach (string dir in dirs)
                    foreach (string path in GetAllPaths(dir))
                        paths.Add(path);
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                    paths.Add(file);
            }
            return paths;
        }

        private static DialogResult ShowErrorPathAlreadyExists(string path)
        {
            return MessageBox.Show("The path:" + Environment.NewLine + path + Environment.NewLine + "already exists!"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Existing data will be overwritten!"
                    + Environment.NewLine + "Continue anyway?",
                    Application.ProductName,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
        }

        private void Modules_cbb_SelectedIndexChanged(object sender, EventArgs e)
        {
            name_txt.Text = modules_cbb.SelectedItem + " - Modified";
            destinationModul_txt.Text = name_txt.Text;
        }

        private void Name_txt_TextChanged(object sender, EventArgs e)
        {
            path_txt.Text = Path.GetFullPath(Properties.Settings.Default.projectsFolderPath.TrimEnd('\\')) + '\\' + name_txt.Text + '\\';
        }

        private void DestinationModul_txt_TextChanged(object sender, EventArgs e)
        {
            if (destinationModul_txt.Text.Equals(modules_cbb.SelectedItem.ToString()))
            {
                useOriginalMod_cb.Checked = true;
                copyNonTextFiles_cb.Enabled = false;
                copyTextFiles_cb.Enabled = false;
            }
            else
            {
                useOriginalMod_cb.Checked = false;
                copyNonTextFiles_cb.Enabled = true;
                copyTextFiles_cb.Enabled = true;
            }
        }

        private void UseOriginalMod_cb_CheckedChanged(object sender, EventArgs e)
        {
            string curModulName = modules_cbb.SelectedItem.ToString();
            if (useOriginalMod_cb.Checked)
                destinationModul_txt.Text = curModulName;
            else
                destinationModul_txt.Text = curModulName + " - Modified";
        }

        private void CopyNonTextFiles_cb_CheckedChanged(object sender, EventArgs e)
        {
            string infoText;
            if (copyNonTextFiles_cb.Checked)
                infoText = "With this checked, the creating process could take a while!";
            else
                infoText = "Without this files, the mod can't be started! But if you are just testing around (with MB Studio) it's ok.";
            MessageBox.Show(infoText, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
    }
}
