namespace MB_Studio.Main
{
    partial class MBOptions
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
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Projects Folder");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Projects", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Generate Header");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Tools", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.min_btn = new System.Windows.Forms.Button();
            this.maxnorm_btn = new System.Windows.Forms.Button();
            this.exit_btn = new System.Windows.Forms.Button();
            this.title_lbl = new System.Windows.Forms.Label();
            this.options_tree = new System.Windows.Forms.TreeView();
            this.save_ProjectsFolder_btn = new System.Windows.Forms.Button();
            this.projectsFolder_lbl = new System.Windows.Forms.Label();
            this.projectsFolder_txt = new System.Windows.Forms.TextBox();
            this.selectProjectsFolder_btn = new System.Windows.Forms.Button();
            this.projects_panel = new System.Windows.Forms.Panel();
            this.generalSettings_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.updateChannel_cbb = new System.Windows.Forms.ComboBox();
            this.baseColor_lbl = new System.Windows.Forms.Label();
            this.loadSavedObjects_cb = new System.Windows.Forms.CheckBox();
            this.show3DView_cb = new System.Windows.Forms.CheckBox();
            this.projectsPathBrowser_fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.colorBase_cd = new System.Windows.Forms.ColorDialog();
            this.language_cbb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.projects_panel.SuspendLayout();
            this.generalSettings_panel.SuspendLayout();
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
            this.min_btn.Location = new System.Drawing.Point(1092, 0);
            this.min_btn.Name = "min_btn";
            this.min_btn.Size = new System.Drawing.Size(32, 28);
            this.min_btn.TabIndex = 19;
            this.min_btn.Text = "_";
            this.min_btn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.min_btn.UseVisualStyleBackColor = false;
            this.min_btn.Click += new System.EventHandler(this.Min_btn_Click);
            // 
            // maxnorm_btn
            // 
            this.maxnorm_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxnorm_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.maxnorm_btn.Enabled = false;
            this.maxnorm_btn.FlatAppearance.BorderSize = 0;
            this.maxnorm_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.maxnorm_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.maxnorm_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxnorm_btn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxnorm_btn.ForeColor = System.Drawing.Color.White;
            this.maxnorm_btn.Location = new System.Drawing.Point(1124, 1);
            this.maxnorm_btn.Name = "maxnorm_btn";
            this.maxnorm_btn.Size = new System.Drawing.Size(32, 26);
            this.maxnorm_btn.TabIndex = 18;
            this.maxnorm_btn.Text = "◼";
            this.maxnorm_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.maxnorm_btn.UseVisualStyleBackColor = false;
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
            this.exit_btn.Location = new System.Drawing.Point(1156, 1);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(32, 26);
            this.exit_btn.TabIndex = 17;
            this.exit_btn.Text = "X";
            this.exit_btn.UseVisualStyleBackColor = false;
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.title_lbl.Location = new System.Drawing.Point(0, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(1187, 34);
            this.title_lbl.TabIndex = 16;
            this.title_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // options_tree
            // 
            this.options_tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.options_tree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.options_tree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.options_tree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.options_tree.ForeColor = System.Drawing.Color.White;
            this.options_tree.LineColor = System.Drawing.Color.White;
            this.options_tree.Location = new System.Drawing.Point(8, 42);
            this.options_tree.Name = "options_tree";
            treeNode6.Name = "generalSettings_node";
            treeNode6.Text = "General";
            treeNode6.ToolTipText = "General Settings";
            treeNode7.Name = "projectsFolder_node";
            treeNode7.Text = "Projects Folder";
            treeNode7.ToolTipText = "Folder where all the mod projects are stored";
            treeNode8.Name = "projectsSettings_node";
            treeNode8.Text = "Projects";
            treeNode8.ToolTipText = "Projects Settings";
            treeNode9.Name = "generateHeader_node";
            treeNode9.Text = "Generate Header";
            treeNode10.Name = "Tools_node";
            treeNode10.Text = "Tools";
            treeNode10.ToolTipText = "Extra Tools";
            this.options_tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode8,
            treeNode10});
            this.options_tree.Size = new System.Drawing.Size(192, 258);
            this.options_tree.TabIndex = 20;
            this.options_tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Options_tree_AfterSelect);
            // 
            // save_ProjectsFolder_btn
            // 
            this.save_ProjectsFolder_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.save_ProjectsFolder_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.save_ProjectsFolder_btn.FlatAppearance.BorderSize = 0;
            this.save_ProjectsFolder_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.save_ProjectsFolder_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.save_ProjectsFolder_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_ProjectsFolder_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_ProjectsFolder_btn.ForeColor = System.Drawing.Color.White;
            this.save_ProjectsFolder_btn.Location = new System.Drawing.Point(619, 45);
            this.save_ProjectsFolder_btn.Name = "save_ProjectsFolder_btn";
            this.save_ProjectsFolder_btn.Size = new System.Drawing.Size(44, 22);
            this.save_ProjectsFolder_btn.TabIndex = 27;
            this.save_ProjectsFolder_btn.Text = "Save";
            this.save_ProjectsFolder_btn.UseVisualStyleBackColor = false;
            this.save_ProjectsFolder_btn.Click += new System.EventHandler(this.Save_ProjectsFolder_btn_Click);
            // 
            // projectsFolder_lbl
            // 
            this.projectsFolder_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectsFolder_lbl.AutoSize = true;
            this.projectsFolder_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.projectsFolder_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectsFolder_lbl.ForeColor = System.Drawing.Color.DarkGray;
            this.projectsFolder_lbl.Location = new System.Drawing.Point(199, 48);
            this.projectsFolder_lbl.Name = "projectsFolder_lbl";
            this.projectsFolder_lbl.Size = new System.Drawing.Size(118, 16);
            this.projectsFolder_lbl.TabIndex = 29;
            this.projectsFolder_lbl.Text = "Projects Folder:";
            this.projectsFolder_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // projectsFolder_txt
            // 
            this.projectsFolder_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.projectsFolder_txt.Location = new System.Drawing.Point(323, 45);
            this.projectsFolder_txt.Name = "projectsFolder_txt";
            this.projectsFolder_txt.Size = new System.Drawing.Size(246, 22);
            this.projectsFolder_txt.TabIndex = 30;
            // 
            // selectProjectsFolder_btn
            // 
            this.selectProjectsFolder_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectProjectsFolder_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.selectProjectsFolder_btn.FlatAppearance.BorderSize = 0;
            this.selectProjectsFolder_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.selectProjectsFolder_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.selectProjectsFolder_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectProjectsFolder_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectProjectsFolder_btn.ForeColor = System.Drawing.Color.White;
            this.selectProjectsFolder_btn.Location = new System.Drawing.Point(569, 45);
            this.selectProjectsFolder_btn.Name = "selectProjectsFolder_btn";
            this.selectProjectsFolder_btn.Size = new System.Drawing.Size(44, 22);
            this.selectProjectsFolder_btn.TabIndex = 31;
            this.selectProjectsFolder_btn.Text = "...";
            this.selectProjectsFolder_btn.UseVisualStyleBackColor = false;
            this.selectProjectsFolder_btn.Click += new System.EventHandler(this.SelectProjectsFolder_btn_Click);
            // 
            // projects_panel
            // 
            this.projects_panel.Controls.Add(this.selectProjectsFolder_btn);
            this.projects_panel.Controls.Add(this.projectsFolder_txt);
            this.projects_panel.Controls.Add(this.projectsFolder_lbl);
            this.projects_panel.Controls.Add(this.save_ProjectsFolder_btn);
            this.projects_panel.Location = new System.Drawing.Point(209, 44);
            this.projects_panel.Name = "projects_panel";
            this.projects_panel.Size = new System.Drawing.Size(978, 251);
            this.projects_panel.TabIndex = 22;
            this.projects_panel.Visible = false;
            // 
            // generalSettings_panel
            // 
            this.generalSettings_panel.Controls.Add(this.label2);
            this.generalSettings_panel.Controls.Add(this.language_cbb);
            this.generalSettings_panel.Controls.Add(this.label1);
            this.generalSettings_panel.Controls.Add(this.updateChannel_cbb);
            this.generalSettings_panel.Controls.Add(this.baseColor_lbl);
            this.generalSettings_panel.Controls.Add(this.loadSavedObjects_cb);
            this.generalSettings_panel.Controls.Add(this.show3DView_cb);
            this.generalSettings_panel.Location = new System.Drawing.Point(206, 41);
            this.generalSettings_panel.Name = "generalSettings_panel";
            this.generalSettings_panel.Size = new System.Drawing.Size(978, 251);
            this.generalSettings_panel.TabIndex = 23;
            this.generalSettings_panel.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(102, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 28);
            this.label1.TabIndex = 35;
            this.label1.Text = "Update Channel:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateChannel_cbb
            // 
            this.updateChannel_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.updateChannel_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateChannel_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateChannel_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.updateChannel_cbb.FormattingEnabled = true;
            this.updateChannel_cbb.Items.AddRange(new object[] {
            "dev",
            "beta",
            "stable"});
            this.updateChannel_cbb.Location = new System.Drawing.Point(253, 147);
            this.updateChannel_cbb.Name = "updateChannel_cbb";
            this.updateChannel_cbb.Size = new System.Drawing.Size(88, 28);
            this.updateChannel_cbb.TabIndex = 34;
            this.updateChannel_cbb.SelectedIndexChanged += new System.EventHandler(this.UpdateChannel_cbb_SelectedIndexChanged);
            // 
            // baseColor_lbl
            // 
            this.baseColor_lbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.baseColor_lbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.baseColor_lbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.baseColor_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baseColor_lbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.baseColor_lbl.Location = new System.Drawing.Point(102, 118);
            this.baseColor_lbl.Name = "baseColor_lbl";
            this.baseColor_lbl.Size = new System.Drawing.Size(239, 23);
            this.baseColor_lbl.TabIndex = 33;
            this.baseColor_lbl.Text = "Color Base";
            this.baseColor_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.baseColor_lbl.Click += new System.EventHandler(this.BaseColor_lbl_Click);
            // 
            // loadSavedObjects_cb
            // 
            this.loadSavedObjects_cb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.loadSavedObjects_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadSavedObjects_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadSavedObjects_cb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.loadSavedObjects_cb.Location = new System.Drawing.Point(103, 87);
            this.loadSavedObjects_cb.Name = "loadSavedObjects_cb";
            this.loadSavedObjects_cb.Size = new System.Drawing.Size(238, 24);
            this.loadSavedObjects_cb.TabIndex = 32;
            this.loadSavedObjects_cb.Text = "Load Saved Objects";
            this.loadSavedObjects_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.loadSavedObjects_cb.UseVisualStyleBackColor = false;
            this.loadSavedObjects_cb.CheckedChanged += new System.EventHandler(this.LoadSavedObjects_cb_CheckedChanged);
            // 
            // show3DView_cb
            // 
            this.show3DView_cb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.show3DView_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show3DView_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show3DView_cb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.show3DView_cb.Location = new System.Drawing.Point(103, 57);
            this.show3DView_cb.Name = "show3DView_cb";
            this.show3DView_cb.Size = new System.Drawing.Size(238, 24);
            this.show3DView_cb.TabIndex = 31;
            this.show3DView_cb.Text = "Show 3D View";
            this.show3DView_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.show3DView_cb.UseVisualStyleBackColor = false;
            this.show3DView_cb.CheckedChanged += new System.EventHandler(this.Show3DView_cb_CheckedChanged);
            // 
            // projectsPathBrowser_fbd
            // 
            this.projectsPathBrowser_fbd.Description = "Select the new directory for your MB Studio projects here";
            this.projectsPathBrowser_fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // colorBase_cd
            // 
            this.colorBase_cd.AnyColor = true;
            this.colorBase_cd.Color = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.colorBase_cd.FullOpen = true;
            // 
            // language_cbb
            // 
            this.language_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.language_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.language_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.language_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
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
            this.language_cbb.Location = new System.Drawing.Point(205, 181);
            this.language_cbb.Name = "language_cbb";
            this.language_cbb.Size = new System.Drawing.Size(136, 28);
            this.language_cbb.Sorted = true;
            this.language_cbb.TabIndex = 36;
            this.language_cbb.SelectedIndexChanged += new System.EventHandler(this.Language_cbb_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(102, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 28);
            this.label2.TabIndex = 37;
            this.label2.Text = "Language:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MBOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1187, 308);
            this.Controls.Add(this.generalSettings_panel);
            this.Controls.Add(this.options_tree);
            this.Controls.Add(this.min_btn);
            this.Controls.Add(this.maxnorm_btn);
            this.Controls.Add(this.exit_btn);
            this.Controls.Add(this.title_lbl);
            this.Controls.Add(this.projects_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MBOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MB Studio - Options";
            this.Load += new System.EventHandler(this.ExtraOptions_Load);
            this.projects_panel.ResumeLayout(false);
            this.projects_panel.PerformLayout();
            this.generalSettings_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button min_btn;
        private System.Windows.Forms.Button maxnorm_btn;
        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.TreeView options_tree;
        private System.Windows.Forms.Button save_ProjectsFolder_btn;
        private System.Windows.Forms.Label projectsFolder_lbl;
        private System.Windows.Forms.TextBox projectsFolder_txt;
        private System.Windows.Forms.Button selectProjectsFolder_btn;
        private System.Windows.Forms.Panel projects_panel;
        private System.Windows.Forms.FolderBrowserDialog projectsPathBrowser_fbd;
        private System.Windows.Forms.Panel generalSettings_panel;
        private System.Windows.Forms.CheckBox show3DView_cb;
        private System.Windows.Forms.CheckBox loadSavedObjects_cb;
        private System.Windows.Forms.ColorDialog colorBase_cd;
        private System.Windows.Forms.Label baseColor_lbl;
        private System.Windows.Forms.ComboBox updateChannel_cbb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.ComboBox language_cbb;
    }
}