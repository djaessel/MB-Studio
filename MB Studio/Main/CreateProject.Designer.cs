namespace MB_Studio.Main
{
    partial class CreateProject
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            this.destinationModul_txt = new System.Windows.Forms.TextBox();
            this.name_txt = new System.Windows.Forms.TextBox();
            this.create_btn = new System.Windows.Forms.Button();
            this.copyTextFiles_cb = new System.Windows.Forms.CheckBox();
            this.destinationModul_lbl = new System.Windows.Forms.Label();
            this.name_lbl = new System.Windows.Forms.Label();
            this.useDefaultVariables_cb = new System.Windows.Forms.CheckBox();
            this.path_txt = new System.Windows.Forms.TextBox();
            this.path_lbl = new System.Windows.Forms.Label();
            this.originalModule_lbl = new System.Windows.Forms.Label();
            this.modules_cbb = new System.Windows.Forms.ComboBox();
            this.copyNonTextFiles_cb = new System.Windows.Forms.CheckBox();
            this.useOriginalMod_cb = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.Enabled = false;
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(372, -2);
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(404, 0);
            // 
            // title_lbl
            // 
            this.title_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.title_lbl.Size = new System.Drawing.Size(372, 24);
            this.title_lbl.Text = "";
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // destinationModul_txt
            // 
            this.destinationModul_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinationModul_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationModul_txt.Location = new System.Drawing.Point(141, 102);
            this.destinationModul_txt.Name = "destinationModul_txt";
            this.destinationModul_txt.Size = new System.Drawing.Size(282, 22);
            this.destinationModul_txt.TabIndex = 25;
            this.destinationModul_txt.TextChanged += new System.EventHandler(this.DestinationModul_txt_TextChanged);
            // 
            // name_txt
            // 
            this.name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_txt.Location = new System.Drawing.Point(141, 46);
            this.name_txt.Name = "name_txt";
            this.name_txt.Size = new System.Drawing.Size(282, 22);
            this.name_txt.TabIndex = 24;
            this.name_txt.TextChanged += new System.EventHandler(this.Name_txt_TextChanged);
            // 
            // create_btn
            // 
            this.create_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.create_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.create_btn.FlatAppearance.BorderSize = 0;
            this.create_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.create_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.create_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.create_btn.ForeColor = System.Drawing.Color.White;
            this.create_btn.Location = new System.Drawing.Point(12, 277);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(411, 26);
            this.create_btn.TabIndex = 23;
            this.create_btn.TabStop = false;
            this.create_btn.Text = "Create";
            this.create_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.create_btn.UseVisualStyleBackColor = false;
            this.create_btn.Click += new System.EventHandler(this.Create_btn_Click);
            // 
            // copyTextFiles_cb
            // 
            this.copyTextFiles_cb.AutoCheck = false;
            this.copyTextFiles_cb.AutoSize = true;
            this.copyTextFiles_cb.Checked = true;
            this.copyTextFiles_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.copyTextFiles_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyTextFiles_cb.Location = new System.Drawing.Point(142, 215);
            this.copyTextFiles_cb.Name = "copyTextFiles_cb";
            this.copyTextFiles_cb.Size = new System.Drawing.Size(256, 22);
            this.copyTextFiles_cb.TabIndex = 22;
            this.copyTextFiles_cb.Text = "Copy text files (.txt, .ini, .h, .fx, .csv)";
            this.copyTextFiles_cb.UseVisualStyleBackColor = true;
            this.copyTextFiles_cb.CheckedChanged += new System.EventHandler(this.CopyTextFiles_cb_CheckedChanged);
            // 
            // destinationModul_lbl
            // 
            this.destinationModul_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationModul_lbl.AutoSize = true;
            this.destinationModul_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationModul_lbl.ForeColor = System.Drawing.Color.Silver;
            this.destinationModul_lbl.Location = new System.Drawing.Point(44, 102);
            this.destinationModul_lbl.Name = "destinationModul_lbl";
            this.destinationModul_lbl.Size = new System.Drawing.Size(91, 18);
            this.destinationModul_lbl.TabIndex = 21;
            this.destinationModul_lbl.Text = "Ziel Modul:";
            this.destinationModul_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // name_lbl
            // 
            this.name_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name_lbl.AutoSize = true;
            this.name_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_lbl.ForeColor = System.Drawing.Color.Silver;
            this.name_lbl.Location = new System.Drawing.Point(19, 46);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(116, 18);
            this.name_lbl.TabIndex = 20;
            this.name_lbl.Text = "Projekt Name:";
            this.name_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // useDefaultVariables_cb
            // 
            this.useDefaultVariables_cb.AutoSize = true;
            this.useDefaultVariables_cb.Checked = true;
            this.useDefaultVariables_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useDefaultVariables_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useDefaultVariables_cb.Location = new System.Drawing.Point(142, 243);
            this.useDefaultVariables_cb.Name = "useDefaultVariables_cb";
            this.useDefaultVariables_cb.Size = new System.Drawing.Size(185, 22);
            this.useDefaultVariables_cb.TabIndex = 26;
            this.useDefaultVariables_cb.Text = "Use Default variables.txt";
            this.useDefaultVariables_cb.UseVisualStyleBackColor = true;
            this.useDefaultVariables_cb.CheckedChanged += new System.EventHandler(this.UseDefaultVariables_cb_CheckedChanged);
            // 
            // path_txt
            // 
            this.path_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.path_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.path_txt.Location = new System.Drawing.Point(141, 129);
            this.path_txt.Name = "path_txt";
            this.path_txt.Size = new System.Drawing.Size(282, 22);
            this.path_txt.TabIndex = 28;
            // 
            // path_lbl
            // 
            this.path_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.path_lbl.AutoSize = true;
            this.path_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.path_lbl.ForeColor = System.Drawing.Color.Silver;
            this.path_lbl.Location = new System.Drawing.Point(88, 129);
            this.path_lbl.Name = "path_lbl";
            this.path_lbl.Size = new System.Drawing.Size(47, 18);
            this.path_lbl.TabIndex = 27;
            this.path_lbl.Text = "Pfad:";
            this.path_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // originalModule_lbl
            // 
            this.originalModule_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalModule_lbl.AutoSize = true;
            this.originalModule_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalModule_lbl.ForeColor = System.Drawing.Color.Silver;
            this.originalModule_lbl.Location = new System.Drawing.Point(13, 74);
            this.originalModule_lbl.Name = "originalModule_lbl";
            this.originalModule_lbl.Size = new System.Drawing.Size(122, 18);
            this.originalModule_lbl.TabIndex = 29;
            this.originalModule_lbl.Text = "Original Modul:";
            this.originalModule_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // modules_cbb
            // 
            this.modules_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modules_cbb.FormattingEnabled = true;
            this.modules_cbb.Location = new System.Drawing.Point(141, 73);
            this.modules_cbb.Name = "modules_cbb";
            this.modules_cbb.Size = new System.Drawing.Size(282, 24);
            this.modules_cbb.Sorted = true;
            this.modules_cbb.TabIndex = 30;
            this.modules_cbb.SelectedIndexChanged += new System.EventHandler(this.Modules_cbb_SelectedIndexChanged);
            // 
            // copyNonTextFiles_cb
            // 
            this.copyNonTextFiles_cb.AutoSize = true;
            this.copyNonTextFiles_cb.Checked = true;
            this.copyNonTextFiles_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.copyNonTextFiles_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyNonTextFiles_cb.Location = new System.Drawing.Point(142, 187);
            this.copyNonTextFiles_cb.Name = "copyNonTextFiles_cb";
            this.copyNonTextFiles_cb.Size = new System.Drawing.Size(149, 22);
            this.copyNonTextFiles_cb.TabIndex = 31;
            this.copyNonTextFiles_cb.Text = "Copy non-text files";
            this.copyNonTextFiles_cb.UseVisualStyleBackColor = true;
            this.copyNonTextFiles_cb.CheckedChanged += new System.EventHandler(this.CopyNonTextFiles_cb_CheckedChanged);
            // 
            // useOriginalMod_cb
            // 
            this.useOriginalMod_cb.AutoSize = true;
            this.useOriginalMod_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useOriginalMod_cb.Location = new System.Drawing.Point(142, 159);
            this.useOriginalMod_cb.Name = "useOriginalMod_cb";
            this.useOriginalMod_cb.Size = new System.Drawing.Size(142, 22);
            this.useOriginalMod_cb.TabIndex = 32;
            this.useOriginalMod_cb.Text = "Use Original Mod";
            this.useOriginalMod_cb.UseVisualStyleBackColor = true;
            this.useOriginalMod_cb.CheckedChanged += new System.EventHandler(this.UseOriginalMod_cb_CheckedChanged);
            // 
            // CreateProject
            // 
            this.ClientSize = new System.Drawing.Size(437, 314);
            this.Controls.Add(this.useOriginalMod_cb);
            this.Controls.Add(this.copyNonTextFiles_cb);
            this.Controls.Add(this.modules_cbb);
            this.Controls.Add(this.originalModule_lbl);
            this.Controls.Add(this.path_txt);
            this.Controls.Add(this.path_lbl);
            this.Controls.Add(this.useDefaultVariables_cb);
            this.Controls.Add(this.destinationModul_txt);
            this.Controls.Add(this.name_txt);
            this.Controls.Add(this.create_btn);
            this.Controls.Add(this.copyTextFiles_cb);
            this.Controls.Add(this.destinationModul_lbl);
            this.Controls.Add(this.name_lbl);
            this.Name = "CreateProject";
            this.Text = " Create Project";
            this.Load += new System.EventHandler(this.CreateProject_Load);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.name_lbl, 0);
            this.Controls.SetChildIndex(this.destinationModul_lbl, 0);
            this.Controls.SetChildIndex(this.copyTextFiles_cb, 0);
            this.Controls.SetChildIndex(this.create_btn, 0);
            this.Controls.SetChildIndex(this.name_txt, 0);
            this.Controls.SetChildIndex(this.destinationModul_txt, 0);
            this.Controls.SetChildIndex(this.useDefaultVariables_cb, 0);
            this.Controls.SetChildIndex(this.path_lbl, 0);
            this.Controls.SetChildIndex(this.path_txt, 0);
            this.Controls.SetChildIndex(this.originalModule_lbl, 0);
            this.Controls.SetChildIndex(this.modules_cbb, 0);
            this.Controls.SetChildIndex(this.copyNonTextFiles_cb, 0);
            this.Controls.SetChildIndex(this.useOriginalMod_cb, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox modules_cbb;
        protected System.Windows.Forms.TextBox destinationModul_txt;
        protected System.Windows.Forms.TextBox name_txt;
        protected System.Windows.Forms.Button create_btn;
        protected System.Windows.Forms.CheckBox copyTextFiles_cb;
        protected System.Windows.Forms.Label destinationModul_lbl;
        protected System.Windows.Forms.CheckBox useDefaultVariables_cb;
        protected System.Windows.Forms.TextBox path_txt;
        protected System.Windows.Forms.Label path_lbl;
        protected System.Windows.Forms.Label originalModule_lbl;
        protected System.Windows.Forms.CheckBox copyNonTextFiles_cb;
        protected System.Windows.Forms.CheckBox useOriginalMod_cb;
        protected System.Windows.Forms.Label name_lbl;
    }
}