using System;
using System.Windows.Forms;

namespace WarbandTranslator
{
    public partial class ModulePath : Form
    {
        private Main mainx;

        public ModulePath(Main mainx)
        {
            this.mainx = mainx;
            InitializeComponent();
        }

        private void ModulePath_Load(object sender, EventArgs e)
        {
            path_txt.Text = mainx.ModulePath;
        }

        private void searchPath_btn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            path_txt.Text = folderBrowserDialog.SelectedPath;
        }

        private void abort_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            mainx.ModulePath = path_txt.Text;
            if (mainx.ModulePath.Length > 3 && !mainx.ModulePath.Substring(mainx.ModulePath.Length - 1).Equals("\\"))
                mainx.ModulePath += Main.BACKSLASH;
            Close();
        }

    }
}
