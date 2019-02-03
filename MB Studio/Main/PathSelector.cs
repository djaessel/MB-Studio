using importantLib;
using System;

namespace MB_Studio.Main
{
    public partial class PathSelector : SpecialFormBlack
    {
        public string SelectedPath { get { return selectedDir_txt.Text; } }

        public PathSelector(string searchName = null)
        {
            InitializeComponent();

            if (searchName != null)
            {
                dir_lbl.Text = searchName + ":";
                selectedDir_txt.Left = dir_lbl.Left + dir_lbl.Width + 5;

                searchName = "Select '" + searchName + "' directory";
                Text = searchName;
            }
        }

        private void OpenSelectDialog_btn_Click(object sender, EventArgs e)
        {
            folderBrowser_dlg.ShowDialog();
            if (folderBrowser_dlg.SelectedPath == null)
            {
                folderBrowser_dlg.SelectedPath = string.Empty;
            }
            selectedDir_txt.Text = folderBrowser_dlg.SelectedPath;
        }

        private void Check_btn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
