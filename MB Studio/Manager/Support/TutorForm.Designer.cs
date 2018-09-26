namespace MB_Studio.Manager.Support
{
    partial class TutorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            this.info_lbl = new System.Windows.Forms.Label();
            this.step_lbl = new System.Windows.Forms.Label();
            this.step_left_btn = new System.Windows.Forms.Button();
            this.step_right_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(400, -2);
            this.min_btn.Size = new System.Drawing.Size(10, 26);
            this.min_btn.Visible = false;
            // 
            // exit_btn
            // 
            this.exit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_btn.Location = new System.Drawing.Point(388, 0);
            this.exit_btn.Size = new System.Drawing.Size(32, 32);
            // 
            // title_lbl
            // 
            this.title_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.Size = new System.Drawing.Size(420, 32);
            this.title_lbl.Text = "Form";
            // 
            // info_lbl
            // 
            this.info_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.info_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_lbl.Location = new System.Drawing.Point(38, 42);
            this.info_lbl.Name = "info_lbl";
            this.info_lbl.Size = new System.Drawing.Size(344, 24);
            this.info_lbl.TabIndex = 1;
            this.info_lbl.Text = "Info";
            this.info_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // step_lbl
            // 
            this.step_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.step_lbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.step_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.step_lbl.Location = new System.Drawing.Point(0, 74);
            this.step_lbl.Name = "step_lbl";
            this.step_lbl.Size = new System.Drawing.Size(420, 26);
            this.step_lbl.TabIndex = 5;
            this.step_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // step_left_btn
            // 
            this.step_left_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.step_left_btn.FlatAppearance.BorderSize = 0;
            this.step_left_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.step_left_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.step_left_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.step_left_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.step_left_btn.Location = new System.Drawing.Point(0, 35);
            this.step_left_btn.Name = "step_left_btn";
            this.step_left_btn.Size = new System.Drawing.Size(32, 36);
            this.step_left_btn.TabIndex = 6;
            this.step_left_btn.Text = "<";
            this.step_left_btn.UseVisualStyleBackColor = true;
            this.step_left_btn.Click += new System.EventHandler(this.Step_left_btn_Click);
            // 
            // step_right_btn
            // 
            this.step_right_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.step_right_btn.FlatAppearance.BorderSize = 0;
            this.step_right_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.step_right_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.step_right_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.step_right_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.step_right_btn.Location = new System.Drawing.Point(388, 35);
            this.step_right_btn.Name = "step_right_btn";
            this.step_right_btn.Size = new System.Drawing.Size(32, 36);
            this.step_right_btn.TabIndex = 7;
            this.step_right_btn.Text = ">";
            this.step_right_btn.UseVisualStyleBackColor = true;
            this.step_right_btn.Click += new System.EventHandler(this.Step_right_btn_Click);
            // 
            // TutorForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.ClientSize = new System.Drawing.Size(420, 100);
            this.Controls.Add(this.step_right_btn);
            this.Controls.Add(this.step_left_btn);
            this.Controls.Add(this.step_lbl);
            this.Controls.Add(this.info_lbl);
            this.Name = "TutorForm";
            this.Load += new System.EventHandler(this.TutorForm_Load);
            this.Controls.SetChildIndex(this.info_lbl, 0);
            this.Controls.SetChildIndex(this.step_lbl, 0);
            this.Controls.SetChildIndex(this.step_left_btn, 0);
            this.Controls.SetChildIndex(this.step_right_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label info_lbl;
        private System.Windows.Forms.Label step_lbl;
        private System.Windows.Forms.Button step_left_btn;
        private System.Windows.Forms.Button step_right_btn;
    }
}