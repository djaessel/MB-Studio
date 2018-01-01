using importantLib;

namespace MB_Studio.Main
{
    partial class ToolForm
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
            this.searchType_SearchTextBox = new importantLib.SearchTextBox();
            this.idINFO_lbl = new System.Windows.Forms.Label();
            this.save_btn = new System.Windows.Forms.Button();
            this.closeAll_btn = new System.Windows.Forms.Button();
            this.typeSelect_lb = new System.Windows.Forms.ListBox();
            this.typeSelect_lbl = new System.Windows.Forms.Label();
            this.min_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.title_lbl = new System.Windows.Forms.Label();
            this.plural_name_lbl = new System.Windows.Forms.Label();
            this.plural_name_txt = new System.Windows.Forms.TextBox();
            this.name_lbl = new System.Windows.Forms.Label();
            this.name_txt = new System.Windows.Forms.TextBox();
            this.id_lbl = new System.Windows.Forms.Label();
            this.id_txt = new System.Windows.Forms.TextBox();
            this.toolPanel = new System.Windows.Forms.Panel();
            this.showGroup_0_btn = new System.Windows.Forms.Button();
            this.groupBox_0_gb = new System.Windows.Forms.GroupBox();
            this.language_lbl = new System.Windows.Forms.Label();
            this.pluralNameTranslation_lbl = new System.Windows.Forms.Label();
            this.pluralNameTranslation_txt = new System.Windows.Forms.TextBox();
            this.singleNameTranslation_lbl = new System.Windows.Forms.Label();
            this.singleNameTranslation_txt = new System.Windows.Forms.TextBox();
            this.language_cbb = new System.Windows.Forms.ComboBox();
            this.save_translation_btn = new System.Windows.Forms.Button();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchType_SearchTextBox
            // 
            this.searchType_SearchTextBox.BackColor = System.Drawing.Color.DimGray;
            this.searchType_SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchType_SearchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchType_SearchTextBox.ForeColor = System.Drawing.Color.White;
            this.searchType_SearchTextBox.Location = new System.Drawing.Point(127, 35);
            this.searchType_SearchTextBox.Name = "searchType_SearchTextBox";
            this.searchType_SearchTextBox.Size = new System.Drawing.Size(277, 22);
            this.searchType_SearchTextBox.TabIndex = 36;
            this.searchType_SearchTextBox.Text = "Search ...";
            this.searchType_SearchTextBox.TextChanged += new System.EventHandler(this.SearchType_SearchTextBox_TextChanged);
            // 
            // idINFO_lbl
            // 
            this.idINFO_lbl.AutoSize = true;
            this.idINFO_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idINFO_lbl.Location = new System.Drawing.Point(525, 57);
            this.idINFO_lbl.Name = "idINFO_lbl";
            this.idINFO_lbl.Size = new System.Drawing.Size(159, 13);
            this.idINFO_lbl.TabIndex = 35;
            this.idINFO_lbl.Text = "( adding \"ID_\" is optional )";
            // 
            // save_btn
            // 
            this.save_btn.BackColor = System.Drawing.Color.DimGray;
            this.save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_btn.Location = new System.Drawing.Point(592, 149);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(202, 23);
            this.save_btn.TabIndex = 34;
            this.save_btn.Text = "SAVE";
            this.save_btn.UseVisualStyleBackColor = false;
            this.save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // closeAll_btn
            // 
            this.closeAll_btn.BackColor = System.Drawing.Color.DimGray;
            this.closeAll_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeAll_btn.Location = new System.Drawing.Point(410, 149);
            this.closeAll_btn.Name = "closeAll_btn";
            this.closeAll_btn.Size = new System.Drawing.Size(177, 23);
            this.closeAll_btn.TabIndex = 33;
            this.closeAll_btn.Text = "CLOSE ALL";
            this.closeAll_btn.UseVisualStyleBackColor = false;
            this.closeAll_btn.Click += new System.EventHandler(this.CloseAll_btn_Click);
            // 
            // typeSelect_lb
            // 
            this.typeSelect_lb.BackColor = System.Drawing.Color.DimGray;
            this.typeSelect_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.typeSelect_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeSelect_lb.ForeColor = System.Drawing.Color.White;
            this.typeSelect_lb.FormattingEnabled = true;
            this.typeSelect_lb.ItemHeight = 16;
            this.typeSelect_lb.Items.AddRange(new object[] {
            "New"});
            this.typeSelect_lb.Location = new System.Drawing.Point(127, 58);
            this.typeSelect_lb.Name = "typeSelect_lb";
            this.typeSelect_lb.Size = new System.Drawing.Size(277, 114);
            this.typeSelect_lb.TabIndex = 32;
            this.typeSelect_lb.SelectedIndexChanged += new System.EventHandler(this.TypeSelect_lb_SelectedIndexChanged);
            // 
            // typeSelect_lbl
            // 
            this.typeSelect_lbl.AutoSize = true;
            this.typeSelect_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeSelect_lbl.Location = new System.Drawing.Point(56, 37);
            this.typeSelect_lbl.Name = "typeSelect_lbl";
            this.typeSelect_lbl.Size = new System.Drawing.Size(65, 16);
            this.typeSelect_lbl.TabIndex = 31;
            this.typeSelect_lbl.Text = "Choose:";
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
            this.min_btn.Location = new System.Drawing.Point(747, -2);
            this.min_btn.Name = "min_btn";
            this.min_btn.Size = new System.Drawing.Size(32, 26);
            this.min_btn.TabIndex = 22;
            this.min_btn.TabStop = false;
            this.min_btn.Text = "_";
            this.min_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.min_btn.UseVisualStyleBackColor = false;
            this.min_btn.Click += new System.EventHandler(this.Min_btn_Click);
            // 
            // exit_btn
            // 
            this.exit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit_btn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_btn.ForeColor = System.Drawing.Color.White;
            this.exit_btn.Location = new System.Drawing.Point(779, 1);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(32, 23);
            this.exit_btn.TabIndex = 23;
            this.exit_btn.TabStop = false;
            this.exit_btn.Text = "X";
            this.exit_btn.UseVisualStyleBackColor = false;
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.ForeColor = System.Drawing.Color.Silver;
            this.title_lbl.Location = new System.Drawing.Point(0, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(814, 24);
            this.title_lbl.TabIndex = 30;
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plural_name_lbl
            // 
            this.plural_name_lbl.AutoSize = true;
            this.plural_name_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plural_name_lbl.Location = new System.Drawing.Point(414, 112);
            this.plural_name_lbl.Name = "plural_name_lbl";
            this.plural_name_lbl.Size = new System.Drawing.Size(97, 16);
            this.plural_name_lbl.TabIndex = 29;
            this.plural_name_lbl.Text = "Plural Name:";
            this.plural_name_lbl.Visible = false;
            // 
            // plural_name_txt
            // 
            this.plural_name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plural_name_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plural_name_txt.Location = new System.Drawing.Point(512, 110);
            this.plural_name_txt.Name = "plural_name_txt";
            this.plural_name_txt.Size = new System.Drawing.Size(282, 22);
            this.plural_name_txt.TabIndex = 26;
            this.plural_name_txt.Visible = false;
            // 
            // name_lbl
            // 
            this.name_lbl.AutoSize = true;
            this.name_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_lbl.Location = new System.Drawing.Point(410, 87);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(101, 16);
            this.name_lbl.TabIndex = 28;
            this.name_lbl.Text = "Single Name:";
            // 
            // name_txt
            // 
            this.name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_txt.Location = new System.Drawing.Point(512, 85);
            this.name_txt.Name = "name_txt";
            this.name_txt.Size = new System.Drawing.Size(282, 22);
            this.name_txt.TabIndex = 25;
            // 
            // id_lbl
            // 
            this.id_lbl.AutoSize = true;
            this.id_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_lbl.Location = new System.Drawing.Point(479, 37);
            this.id_lbl.Name = "id_lbl";
            this.id_lbl.Size = new System.Drawing.Size(27, 16);
            this.id_lbl.TabIndex = 27;
            this.id_lbl.Text = "ID:";
            // 
            // id_txt
            // 
            this.id_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.id_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_txt.Location = new System.Drawing.Point(512, 35);
            this.id_txt.Name = "id_txt";
            this.id_txt.Size = new System.Drawing.Size(282, 22);
            this.id_txt.TabIndex = 24;
            this.id_txt.TextChanged += new System.EventHandler(this.Id_txt_TextChanged);
            // 
            // toolPanel
            // 
            this.toolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolPanel.Controls.Add(this.showGroup_0_btn);
            this.toolPanel.Controls.Add(this.groupBox_0_gb);
            this.toolPanel.Location = new System.Drawing.Point(18, 178);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(778, 30);
            this.toolPanel.TabIndex = 37;
            // 
            // showGroup_0_btn
            // 
            this.showGroup_0_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_0_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_0_btn.Location = new System.Drawing.Point(3, 4);
            this.showGroup_0_btn.Name = "showGroup_0_btn";
            this.showGroup_0_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_0_btn.TabIndex = 22;
            this.showGroup_0_btn.Tag = "-25";
            this.showGroup_0_btn.Text = "v";
            this.showGroup_0_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_0_gb
            // 
            this.groupBox_0_gb.Controls.Add(this.language_lbl);
            this.groupBox_0_gb.Controls.Add(this.pluralNameTranslation_lbl);
            this.groupBox_0_gb.Controls.Add(this.pluralNameTranslation_txt);
            this.groupBox_0_gb.Controls.Add(this.singleNameTranslation_lbl);
            this.groupBox_0_gb.Controls.Add(this.singleNameTranslation_txt);
            this.groupBox_0_gb.Controls.Add(this.language_cbb);
            this.groupBox_0_gb.Controls.Add(this.save_translation_btn);
            this.groupBox_0_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_0_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_0_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_0_gb.Location = new System.Drawing.Point(39, 2);
            this.groupBox_0_gb.Name = "groupBox_0_gb";
            this.groupBox_0_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_0_gb.TabIndex = 21;
            this.groupBox_0_gb.TabStop = false;
            this.groupBox_0_gb.Tag = "";
            this.groupBox_0_gb.Text = "Translation";
            // 
            // language_lbl
            // 
            this.language_lbl.AutoSize = true;
            this.language_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.language_lbl.Location = new System.Drawing.Point(151, 50);
            this.language_lbl.Name = "language_lbl";
            this.language_lbl.Size = new System.Drawing.Size(81, 16);
            this.language_lbl.TabIndex = 20;
            this.language_lbl.Text = "Language:";
            // 
            // pluralNameTranslation_lbl
            // 
            this.pluralNameTranslation_lbl.AutoSize = true;
            this.pluralNameTranslation_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluralNameTranslation_lbl.Location = new System.Drawing.Point(364, 63);
            this.pluralNameTranslation_lbl.Name = "pluralNameTranslation_lbl";
            this.pluralNameTranslation_lbl.Size = new System.Drawing.Size(97, 16);
            this.pluralNameTranslation_lbl.TabIndex = 19;
            this.pluralNameTranslation_lbl.Text = "Plural Name:";
            this.pluralNameTranslation_lbl.Visible = false;
            // 
            // pluralNameTranslation_txt
            // 
            this.pluralNameTranslation_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pluralNameTranslation_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pluralNameTranslation_txt.Location = new System.Drawing.Point(467, 61);
            this.pluralNameTranslation_txt.Name = "pluralNameTranslation_txt";
            this.pluralNameTranslation_txt.Size = new System.Drawing.Size(257, 22);
            this.pluralNameTranslation_txt.TabIndex = 17;
            this.pluralNameTranslation_txt.Visible = false;
            // 
            // singleNameTranslation_lbl
            // 
            this.singleNameTranslation_lbl.AutoSize = true;
            this.singleNameTranslation_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleNameTranslation_lbl.Location = new System.Drawing.Point(360, 38);
            this.singleNameTranslation_lbl.Name = "singleNameTranslation_lbl";
            this.singleNameTranslation_lbl.Size = new System.Drawing.Size(101, 16);
            this.singleNameTranslation_lbl.TabIndex = 18;
            this.singleNameTranslation_lbl.Text = "Single Name:";
            // 
            // singleNameTranslation_txt
            // 
            this.singleNameTranslation_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.singleNameTranslation_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleNameTranslation_txt.Location = new System.Drawing.Point(467, 36);
            this.singleNameTranslation_txt.Name = "singleNameTranslation_txt";
            this.singleNameTranslation_txt.Size = new System.Drawing.Size(257, 22);
            this.singleNameTranslation_txt.TabIndex = 16;
            // 
            // language_cbb
            // 
            this.language_cbb.BackColor = System.Drawing.Color.DimGray;
            this.language_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.language_cbb.ForeColor = System.Drawing.Color.White;
            this.language_cbb.FormattingEnabled = true;
            this.language_cbb.Items.AddRange(new object[] {
            "cz (Český)",
            "de (Deutsch)",
            "en (English)",
            "es (Español)",
            "fr (Français)",
            "hu (Magyar)",
            "pl (Polskie)",
            "rus (Pусский)",
            "tr (Türkçe)"});
            this.language_cbb.Location = new System.Drawing.Point(233, 44);
            this.language_cbb.Name = "language_cbb";
            this.language_cbb.Size = new System.Drawing.Size(120, 28);
            this.language_cbb.Sorted = true;
            this.language_cbb.TabIndex = 15;
            this.language_cbb.SelectedIndexChanged += new System.EventHandler(this.Language_cbb_SelectedIndexChanged);
            // 
            // save_translation_btn
            // 
            this.save_translation_btn.BackColor = System.Drawing.Color.DimGray;
            this.save_translation_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_translation_btn.Location = new System.Drawing.Point(7, 39);
            this.save_translation_btn.Name = "save_translation_btn";
            this.save_translation_btn.Size = new System.Drawing.Size(138, 40);
            this.save_translation_btn.TabIndex = 14;
            this.save_translation_btn.Text = "Save Translation";
            this.save_translation_btn.UseVisualStyleBackColor = false;
            this.save_translation_btn.Click += new System.EventHandler(this.Save_translation_btn_Click);
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.ClientSize = new System.Drawing.Size(814, 220);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.searchType_SearchTextBox);
            this.Controls.Add(this.idINFO_lbl);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.closeAll_btn);
            this.Controls.Add(this.typeSelect_lb);
            this.Controls.Add(this.typeSelect_lbl);
            this.Controls.Add(this.min_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.title_lbl);
            this.Controls.Add(this.plural_name_lbl);
            this.Controls.Add(this.plural_name_txt);
            this.Controls.Add(this.name_lbl);
            this.Controls.Add(this.name_txt);
            this.Controls.Add(this.id_lbl);
            this.Controls.Add(this.id_txt);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToolForm";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ToolForm";
            this.Load += new System.EventHandler(this.ToolForm_Load);
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected SearchTextBox searchType_SearchTextBox;
        protected System.Windows.Forms.Label idINFO_lbl;
        protected System.Windows.Forms.Button save_btn;
        protected System.Windows.Forms.Button closeAll_btn;
        protected System.Windows.Forms.ListBox typeSelect_lb;
        protected System.Windows.Forms.Label typeSelect_lbl;
        protected System.Windows.Forms.Label title_lbl;
        protected System.Windows.Forms.Label plural_name_lbl;
        protected System.Windows.Forms.TextBox plural_name_txt;
        protected System.Windows.Forms.Label name_lbl;
        protected System.Windows.Forms.TextBox name_txt;
        protected System.Windows.Forms.Label id_lbl;
        protected System.Windows.Forms.TextBox id_txt;
        protected System.Windows.Forms.Panel toolPanel;
        protected System.Windows.Forms.Button showGroup_0_btn;
        protected System.Windows.Forms.GroupBox groupBox_0_gb;
        protected System.Windows.Forms.Label language_lbl;
        protected System.Windows.Forms.Label pluralNameTranslation_lbl;
        protected System.Windows.Forms.TextBox pluralNameTranslation_txt;
        protected System.Windows.Forms.Label singleNameTranslation_lbl;
        protected System.Windows.Forms.TextBox singleNameTranslation_txt;
        protected System.Windows.Forms.ComboBox language_cbb;
        protected System.Windows.Forms.Button save_translation_btn;
        protected System.Windows.Forms.Button min_btn;
        protected System.Windows.Forms.Button exit_btn;
    }
}