using System;
using System.Drawing;
using System.Windows.Forms;

namespace importantLib
{
    public partial class SpecialFormBlack : SpecialForm
    {
        protected int titleButtonWidth = 32;
        protected int titleElementDefaultHeight = 24;

        protected Button min_btn;
        protected Button exit_btn;
        protected Label title_lbl;

        public SpecialFormBlack()
        {
            InitializeComponent();
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
            min_btn.Click += Min_btn_Click;
            exit_btn.Click += Exit_btn_Click;
            Load += SpecialFormBlack_Load;
        }

        private void SpecialFormBlack_Load(object sender, EventArgs e)
        {
            if (Text.Length != 0)
                title_lbl.Text = Text;
            else
                title_lbl.Text = "Form";
        }

        private void Min_btn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private new void InitializeComponent()
        {
            exit_btn = new Button();
            min_btn = new Button();
            title_lbl = new Label();

            SuspendLayout();

            // 
            // SpecialFormBlack
            //
            ClientSize = new Size(500, 256);
            BackColor = Color.FromArgb(64, 64, 64);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SpecialFormBlack";
            StartPosition = FormStartPosition.CenterScreen;

            // 
            // exit_btn
            // 
            exit_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exit_btn.BackColor = Color.FromArgb(56, 56, 56);
            exit_btn.FlatAppearance.BorderSize = 0;
            exit_btn.FlatAppearance.MouseDownBackColor = Color.Black;
            exit_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 72, 72);
            exit_btn.FlatStyle = FlatStyle.Flat;
#if NET472
            exit_btn.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
#endif
            exit_btn.ForeColor = Color.White;
            exit_btn.Name = "exit_btn";
            exit_btn.Size = new Size(titleButtonWidth, titleElementDefaultHeight);
            exit_btn.Location = new Point(Width - exit_btn.Width, 0);
            exit_btn.TabIndex = 0;
            exit_btn.TabStop = false;
            exit_btn.Text = "X";
            exit_btn.UseVisualStyleBackColor = false;

            // 
            // min_btn
            // 
            min_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            min_btn.BackColor = Color.FromArgb(56, 56, 56);
            min_btn.FlatAppearance.BorderSize = 0;
            min_btn.FlatAppearance.MouseDownBackColor = Color.Black;
            min_btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(72, 72, 72);
            min_btn.FlatStyle = FlatStyle.Flat;
#if NET472
            min_btn.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
#endif
            min_btn.ForeColor = Color.White;
            min_btn.Name = "min_btn";
            min_btn.Size = new Size(titleButtonWidth, titleElementDefaultHeight + 2);
            min_btn.Location = new Point(Width - min_btn.Width - exit_btn.Width, -2);
            min_btn.TabIndex = 0;
            min_btn.TabStop = false;
            min_btn.Text = "_";
#if NET472
            min_btn.TextAlign = ContentAlignment.TopCenter;
#endif
            min_btn.UseVisualStyleBackColor = false;

            // 
            // title_lbl
            // 
            title_lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            title_lbl.BackColor = Color.FromArgb(56, 56, 56);
#if NET472
            title_lbl.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
#endif
            title_lbl.ForeColor = Color.Silver;
            title_lbl.Location = new Point(0, 0);
            title_lbl.Name = "title_lbl";
            title_lbl.Size = new Size(Width - min_btn.Width - exit_btn.Width, titleElementDefaultHeight);
            title_lbl.TabIndex = 0;
#if NET472
            title_lbl.TextAlign = ContentAlignment.MiddleCenter;
#endif

            Controls.Add(exit_btn);
            Controls.Add(min_btn);
            Controls.Add(title_lbl);

            ResumeLayout(false);
        }
    }
}
