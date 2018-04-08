namespace importantLib
{
    public partial class SpecialFormBlack : SpecialForm
    {
        protected System.Windows.Forms.Button min_btn;
        protected System.Windows.Forms.Button exit_btn;
        protected System.Windows.Forms.Label title_lbl;

        public SpecialFormBlack()
        {
            InitializeComponent();
            title_lbl.MouseDown += Control_MoveForm_MouseDown;
            min_btn.Click += Min_btn_Click;
            exit_btn.Click += Exit_btn_Click;
            Load += SpecialFormBlack_Load;
        }

        private void SpecialFormBlack_Load(object sender, System.EventArgs e)
        {
            if (Text.Length != 0)
                title_lbl.Text = Text;
            else
                title_lbl.Text = "Form";
        }

        private void Min_btn_Click(object sender, System.EventArgs e)
        {
            WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void Exit_btn_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private new void InitializeComponent()
        {
            this.min_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.title_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.min_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.min_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.min_btn.ForeColor = System.Drawing.Color.White;
            this.min_btn.Location = new System.Drawing.Point(435, -2);
            this.min_btn.Name = "min_btn";
            this.min_btn.Size = new System.Drawing.Size(32, 26);
            this.min_btn.TabIndex = 0;
            this.min_btn.TabStop = false;
            this.min_btn.Text = "_";
            this.min_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.min_btn.UseVisualStyleBackColor = false;
            // 
            // exit_btn
            // 
            this.exit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_btn.ForeColor = System.Drawing.Color.White;
            this.exit_btn.Location = new System.Drawing.Point(467, 0);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(32, 24);
            this.exit_btn.TabIndex = 0;
            this.exit_btn.TabStop = false;
            this.exit_btn.Text = "X";
            this.exit_btn.UseVisualStyleBackColor = false;
            // 
            // title_lbl
            // 
            this.title_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.ForeColor = System.Drawing.Color.Silver;
            this.title_lbl.Location = new System.Drawing.Point(0, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(435, 24);
            this.title_lbl.TabIndex = 0;
            this.title_lbl.Text = "Form";
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SpecialFormBlack
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(499, 261);
            this.Controls.Add(this.min_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.title_lbl);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SpecialFormBlack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
