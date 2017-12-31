using importantLib;
using MB_Decompiler_Library.Objects;
using MB_Decompiler_Library.Objects.Support;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MB_Studio
{
    public partial class MenuDesigner : SpecialFormBlack
    {
        //private GameMenu menu;

        public MenuDesigner(/*GameMenu menu = null*/)
        {
            //this.menu = menu;
            InitializeComponent();
            menuText_panel.Parent = background_pb;
            options_panel.Parent = background_pb;
            SizeChanged += MenuDesigner_SizeChanged;
        }

        private void MenuDesigner_SizeChanged(object sender, EventArgs e)
        {
            menuText_panel.Top = background_pb.Top + 8;
            menuText_panel.Height = background_pb.Height / 2 - 64;

            options_panel.Top = background_pb.Top + (background_pb.Height / 8) * 3 + 16;
            options_panel.Height = menuText_panel.Height + 68;
        }

        private void MenuDesigner_Load(object sender, EventArgs e)
        {
            SetFullScreenByScreen();
            //if (menu != null)
            //    UpdateGameMenu(menu);
        }

        public void UpdateGameMenuTextColor(Color color)
        {
            if (color.Equals(default(Color)))
                color = Color.Black;
            menuText.ForeColor = color;
        }

        public void UpdateGameMenuOptionsTexts(string[] texts)
        {
            options_panel.SuspendLayout();
            options_panel.Controls.Clear();
            //options_panel.Refresh();

            for (int i = 0; i < texts.Length; i++)
            {
                Button x = new Button()
                {
                    AutoSize = false,
                    BackColor = Color.Transparent,
                    ForeColor = menuText.ForeColor,
                    //BackgroundImage = "";//use original image/mesh
                    FlatStyle = FlatStyle.Flat,
                    Width = options_panel.Width - 20,
                    Font = menuText.Font,
                    Height = 32,
                    Tag = i,
                    Text = texts[i].Replace('_', ' '),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(2, 2 + i * 32),
                    //Location = new Point(menuText_lbl.Left, 32 + i * 40),
                    Visible = true,
                };

                x.FlatAppearance.MouseDownBackColor = Color.FromArgb(Math.Max(Color.Yellow.R - 64, 0), Math.Max(Color.Yellow.G - 64, 0), Math.Max(Color.Yellow.B - 64, 0));
                x.FlatAppearance.MouseOverBackColor = Color.FromArgb(Math.Max(Color.Yellow.R - 32, 0), Math.Max(Color.Yellow.G - 32, 0), Math.Max(Color.Yellow.B - 32, 0));
                x.FlatAppearance.BorderSize = 0;

                options_panel.Controls.Add(x);

                //x.Parent = background_pb;

                x.BringToFront();
            }

            options_panel.Refresh();
            options_panel.ResumeLayout();
        }

        public void UpdateGameMenuOptions(GameMenuOption[] gameMenuOptions)
        {
            string[] texts = new string[gameMenuOptions.Length];
            for (int i = 0; i < texts.Length; i++)
                texts[i] = gameMenuOptions[i].Text;
            UpdateGameMenuOptionsTexts(texts);
        }

        public void UpdateGameMenuText(string text)
        {
            char[] cc = text.Replace('_', ' ').ToCharArray();

            menuText.Text = string.Empty;

            int distance = menuText_panel.Width / 3 + 8;
            bool foundMaxLength = false;

            for (int i = 0; i < cc.Length; i++)
            {
                if (!foundMaxLength)
                {
                    if (menuText.Width + menuText.Left + distance >= menuText_panel.Width)
                    {
                        distance = i;
                        foundMaxLength = !foundMaxLength; // true
                    }
                }
                menuText.Text += cc[i];
                if (foundMaxLength)
                    if (i % distance == 0)
                        menuText.Text += Environment.NewLine;
            }
        }

        public void UpdateGameMenu(GameMenu menu, Color textColor = default(Color))
        {
            UpdateGameMenuText(menu.Text);
            UpdateGameMenuTextColor(textColor);
            UpdateGameMenuOptions(menu.MenuOptions);
            //this.menu = menu;
        }
    }
}
