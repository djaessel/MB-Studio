using System;
using System.Drawing;
using System.Windows.Forms;

namespace MB_Studio.Main
{
    public partial class ProjectObject : UserControl
    {
        private string projectPath, projectName;
        private Color highlightColor, defaultColor;

        public ProjectObject()
        {
            InitializeComponent();

            highlightColor = Color.FromArgb(Math.Min(BackColor.R + 16, byte.MaxValue), Math.Min(BackColor.G + 16, byte.MaxValue), Math.Min(BackColor.B + 16, byte.MaxValue));
            defaultColor = BackColor;

            SizeChanged += ProjectObject_SizeChanged;
            MouseHover += ProjectObject_MouseHover;
            MouseEnter += ProjectObject_MouseEnter;
            MouseLeave += ProjectObject_MouseLeave;

            foreach (Control c in Controls)
            {
                c.MouseEnter += ProjectObject_MouseEnter;
                c.MouseHover += ProjectObject_MouseHover;
                c.MouseLeave += ProjectObject_MouseLeave;
                c.Parent = this;
                c.Click += C_Click;
            }
        }

        private void C_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void ProjectObject_MouseLeave(object sender, EventArgs e)
        {
            BackColor = defaultColor;
        }

        private void ProjectObject_MouseEnter(object sender, EventArgs e)
        {
            BackColor = highlightColor;
        }

        private void ProjectObject_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show(projectPath, (Control)sender);
        }

        private void ProjectObject_SizeChanged(object sender, EventArgs e)
        {
            if (projectPath_lbl.Left + projectPath_lbl.Width > Width)
            {
                projectPath_lbl.Text = projectPath_lbl.Text.Remove(projectPath_lbl.Text.Length - 4);
                while (projectPath_lbl.Left + projectPath_lbl.Width > Width)
                    projectPath_lbl.Text = projectPath_lbl.Text.Remove(projectPath_lbl.Text.Length - 1);
                projectPath_lbl.Text += "...";
            }
            if (projectName_lbl.Left + projectName_lbl.Width > Width)
            {
                projectName_lbl.Text = projectName_lbl.Text.Remove(projectName_lbl.Text.Length - 4);
                while (projectName_lbl.Left + projectName_lbl.Width > Width)
                    projectName_lbl.Text = projectName_lbl.Text.Remove(projectName_lbl.Text.Length - 1);
                projectName_lbl.Text += "...";
            }
        }

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                projectName_lbl.Text = projectName;
            }
        }

        public string ProjectPath
        {
            get { return projectPath; }
            set
            {
                projectPath = value;
                projectPath_lbl.Text = projectPath;
            }
        }

        public Image Icon
        {
            get { return icon_pb.Image; }
            set { icon_pb.Image = value; }
        }

        public string IconLocation
        {
            get { return icon_pb.ImageLocation; }
            set { icon_pb.ImageLocation = value; }
        }
    }
}
