namespace MB_Studio
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Projects Folder");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Generate Header");
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
            this.projectsPathBrowser_fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.projects_panel.SuspendLayout();
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
            treeNode1.Name = "projectsFolder_node";
            treeNode1.Text = "Projects Folder";
            treeNode2.Name = "generateHeader_node";
            treeNode2.Text = "Generate Header";
            this.options_tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
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
            // projectsPathBrowser_fbd
            // 
            this.projectsPathBrowser_fbd.Description = "Select the new directory for your MB Studio projects here";
            this.projectsPathBrowser_fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // MBOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1187, 308);
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
    }
}