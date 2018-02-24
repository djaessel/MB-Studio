namespace WarbandTranslator
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.file_btn = new System.Windows.Forms.Button();
            this.language_btn = new System.Windows.Forms.Button();
            this.save_btn = new System.Windows.Forms.Button();
            this.file_cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectModuleFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripComboBox();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ausrufezeichenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ohneAusrufezeichenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notTranslateableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCurrentLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.info_panel = new System.Windows.Forms.Panel();
            this.pageSwitcher_numupdown = new System.Windows.Forms.NumericUpDown();
            this.searchText_txt = new System.Windows.Forms.TextBox();
            this.itemCount_lbl = new System.Windows.Forms.Label();
            this.page_text_lbl = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.languages_cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.czToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.huToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.file_cms.SuspendLayout();
            this.info_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageSwitcher_numupdown)).BeginInit();
            this.languages_cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // file_btn
            // 
            this.file_btn.FlatAppearance.BorderSize = 0;
            this.file_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.file_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.file_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.file_btn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.file_btn.Location = new System.Drawing.Point(3, 3);
            this.file_btn.Name = "file_btn";
            this.file_btn.Size = new System.Drawing.Size(75, 23);
            this.file_btn.TabIndex = 1;
            this.file_btn.Text = "File";
            this.file_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.file_btn.UseVisualStyleBackColor = true;
            this.file_btn.Click += new System.EventHandler(this.file_btn_Click);
            // 
            // language_btn
            // 
            this.language_btn.FlatAppearance.BorderSize = 0;
            this.language_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.language_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.language_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.language_btn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.language_btn.Location = new System.Drawing.Point(78, 3);
            this.language_btn.Name = "language_btn";
            this.language_btn.Size = new System.Drawing.Size(75, 23);
            this.language_btn.TabIndex = 2;
            this.language_btn.Text = "Language";
            this.language_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.language_btn.UseVisualStyleBackColor = true;
            this.language_btn.Click += new System.EventHandler(this.language_btn_Click);
            // 
            // save_btn
            // 
            this.save_btn.FlatAppearance.BorderSize = 0;
            this.save_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.save_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_btn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.save_btn.Location = new System.Drawing.Point(153, 3);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 3;
            this.save_btn.Text = "Save";
            this.save_btn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // file_cms
            // 
            this.file_cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectModuleFolderToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.saveSettingsToolStripMenuItem,
            this.loadCurrentLanguageToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.file_cms.Name = "file_cms";
            this.file_cms.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.file_cms.ShowImageMargin = false;
            this.file_cms.Size = new System.Drawing.Size(174, 136);
            this.file_cms.Text = "File";
            // 
            // selectModuleFolderToolStripMenuItem
            // 
            this.selectModuleFolderToolStripMenuItem.Name = "selectModuleFolderToolStripMenuItem";
            this.selectModuleFolderToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.selectModuleFolderToolStripMenuItem.Text = "Select Module Folder...";
            this.selectModuleFolderToolStripMenuItem.Click += new System.EventHandler(this.selectModuleFolderToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.openFileToolStripMenuItem.Text = "Open File...";
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eToolStripMenuItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eToolStripMenuItem.Items.AddRange(new object[] {
            "conversation",
            "factions",
            "menus",
            "strings",
            "info_pages",
            "item_kinds1",
            "parties",
            "party_templates",
            "quests",
            "quick_strings",
            "skills",
            "skins",
            "troops"});
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(152, 23);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ausrufezeichenItem,
            this.ohneAusrufezeichenToolStripMenuItem,
            this.notTranslateableToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // ausrufezeichenItem
            // 
            this.ausrufezeichenItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ausrufezeichenItem.Name = "ausrufezeichenItem";
            this.ausrufezeichenItem.Size = new System.Drawing.Size(162, 22);
            this.ausrufezeichenItem.Text = "without {!}";
            this.ausrufezeichenItem.Click += new System.EventHandler(this.ausrufezeichenItem_Click);
            // 
            // ohneAusrufezeichenToolStripMenuItem
            // 
            this.ohneAusrufezeichenToolStripMenuItem.Name = "ohneAusrufezeichenToolStripMenuItem";
            this.ohneAusrufezeichenToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.ohneAusrufezeichenToolStripMenuItem.Text = "show only {!}";
            this.ohneAusrufezeichenToolStripMenuItem.Click += new System.EventHandler(this.showOnlyToolStripMenuItem_Click);
            // 
            // notTranslateableToolStripMenuItem
            // 
            this.notTranslateableToolStripMenuItem.Name = "notTranslateableToolStripMenuItem";
            this.notTranslateableToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.notTranslateableToolStripMenuItem.Text = "not translateable";
            this.notTranslateableToolStripMenuItem.Click += new System.EventHandler(this.notTranslateableToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Enabled = false;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem1_Click);
            // 
            // loadCurrentLanguageToolStripMenuItem
            // 
            this.loadCurrentLanguageToolStripMenuItem.Enabled = false;
            this.loadCurrentLanguageToolStripMenuItem.Name = "loadCurrentLanguageToolStripMenuItem";
            this.loadCurrentLanguageToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.loadCurrentLanguageToolStripMenuItem.Text = "Load Current Language";
            this.loadCurrentLanguageToolStripMenuItem.Click += new System.EventHandler(this.loadCurrentLanguageToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // info_panel
            // 
            this.info_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.info_panel.BackColor = System.Drawing.Color.Black;
            this.info_panel.Controls.Add(this.pageSwitcher_numupdown);
            this.info_panel.Controls.Add(this.searchText_txt);
            this.info_panel.Controls.Add(this.itemCount_lbl);
            this.info_panel.Controls.Add(this.save_btn);
            this.info_panel.Controls.Add(this.file_btn);
            this.info_panel.Controls.Add(this.language_btn);
            this.info_panel.Controls.Add(this.page_text_lbl);
            this.info_panel.Location = new System.Drawing.Point(-4, -4);
            this.info_panel.Name = "info_panel";
            this.info_panel.Size = new System.Drawing.Size(722, 28);
            this.info_panel.TabIndex = 4;
            // 
            // pageSwitcher_numupdown
            // 
            this.pageSwitcher_numupdown.BackColor = System.Drawing.Color.DarkGray;
            this.pageSwitcher_numupdown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pageSwitcher_numupdown.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageSwitcher_numupdown.ForeColor = System.Drawing.Color.SaddleBrown;
            this.pageSwitcher_numupdown.Location = new System.Drawing.Point(405, 6);
            this.pageSwitcher_numupdown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageSwitcher_numupdown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageSwitcher_numupdown.Name = "pageSwitcher_numupdown";
            this.pageSwitcher_numupdown.Size = new System.Drawing.Size(37, 20);
            this.pageSwitcher_numupdown.TabIndex = 5;
            this.pageSwitcher_numupdown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pageSwitcher_numupdown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageSwitcher_numupdown.Visible = false;
            this.pageSwitcher_numupdown.ValueChanged += new System.EventHandler(this.pageSwitcher_numupdown_ValueChanged);
            // 
            // searchText_txt
            // 
            this.searchText_txt.BackColor = System.Drawing.Color.DarkGray;
            this.searchText_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchText_txt.ForeColor = System.Drawing.Color.Black;
            this.searchText_txt.Location = new System.Drawing.Point(234, 6);
            this.searchText_txt.Name = "searchText_txt";
            this.searchText_txt.Size = new System.Drawing.Size(132, 20);
            this.searchText_txt.TabIndex = 4;
            this.searchText_txt.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // itemCount_lbl
            // 
            this.itemCount_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemCount_lbl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.itemCount_lbl.Location = new System.Drawing.Point(448, 6);
            this.itemCount_lbl.Name = "itemCount_lbl";
            this.itemCount_lbl.Size = new System.Drawing.Size(269, 20);
            this.itemCount_lbl.TabIndex = 5;
            this.itemCount_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // page_text_lbl
            // 
            this.page_text_lbl.AutoSize = true;
            this.page_text_lbl.Location = new System.Drawing.Point(370, 8);
            this.page_text_lbl.Name = "page_text_lbl";
            this.page_text_lbl.Size = new System.Drawing.Size(35, 13);
            this.page_text_lbl.TabIndex = 6;
            this.page_text_lbl.Text = "Page:";
            this.page_text_lbl.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 26);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(690, 330);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // languages_cms
            // 
            this.languages_cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.czToolStripMenuItem,
            this.deToolStripMenuItem,
            this.enToolStripMenuItem,
            this.esToolStripMenuItem,
            this.frToolStripMenuItem,
            this.huToolStripMenuItem,
            this.plToolStripMenuItem,
            this.trToolStripMenuItem,
            this.cntToolStripMenuItem,
            this.cntToolStripMenuItem1});
            this.languages_cms.Name = "languages_cms";
            this.languages_cms.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.languages_cms.ShowImageMargin = false;
            this.languages_cms.Size = new System.Drawing.Size(154, 224);
            this.languages_cms.Text = "Languages";
            // 
            // czToolStripMenuItem
            // 
            this.czToolStripMenuItem.Name = "czToolStripMenuItem";
            this.czToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.czToolStripMenuItem.Text = "cz";
            // 
            // deToolStripMenuItem
            // 
            this.deToolStripMenuItem.BackColor = System.Drawing.Color.Gainsboro;
            this.deToolStripMenuItem.Name = "deToolStripMenuItem";
            this.deToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deToolStripMenuItem.Text = "de";
            // 
            // enToolStripMenuItem
            // 
            this.enToolStripMenuItem.Name = "enToolStripMenuItem";
            this.enToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.enToolStripMenuItem.Text = "en";
            // 
            // esToolStripMenuItem
            // 
            this.esToolStripMenuItem.Name = "esToolStripMenuItem";
            this.esToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.esToolStripMenuItem.Text = "es";
            // 
            // frToolStripMenuItem
            // 
            this.frToolStripMenuItem.Name = "frToolStripMenuItem";
            this.frToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.frToolStripMenuItem.Text = "fr";
            // 
            // huToolStripMenuItem
            // 
            this.huToolStripMenuItem.Name = "huToolStripMenuItem";
            this.huToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.huToolStripMenuItem.Text = "hu";
            // 
            // plToolStripMenuItem
            // 
            this.plToolStripMenuItem.Name = "plToolStripMenuItem";
            this.plToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.plToolStripMenuItem.Text = "pl";
            // 
            // trToolStripMenuItem
            // 
            this.trToolStripMenuItem.Name = "trToolStripMenuItem";
            this.trToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.trToolStripMenuItem.Text = "tr";
            // 
            // cntToolStripMenuItem
            // 
            this.cntToolStripMenuItem.Enabled = false;
            this.cntToolStripMenuItem.Name = "cntToolStripMenuItem";
            this.cntToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.cntToolStripMenuItem.Text = "cns (not supported)";
            // 
            // cntToolStripMenuItem1
            // 
            this.cntToolStripMenuItem1.Enabled = false;
            this.cntToolStripMenuItem1.Name = "cntToolStripMenuItem1";
            this.cntToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.cntToolStripMenuItem1.Text = "cnt (not supported)";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(714, 361);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.info_panel);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 200);
            this.Name = "Main";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.file_cms.ResumeLayout(false);
            this.info_panel.ResumeLayout(false);
            this.info_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageSwitcher_numupdown)).EndInit();
            this.languages_cms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button file_btn;
        private System.Windows.Forms.Button language_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.ContextMenuStrip file_cms;
        private System.Windows.Forms.ToolStripMenuItem selectModuleFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox eToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel info_panel;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip languages_cms;
        private System.Windows.Forms.ToolStripMenuItem czToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem huToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cntToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCurrentLanguageToolStripMenuItem;
        private System.Windows.Forms.Label itemCount_lbl;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ausrufezeichenItem;
        private System.Windows.Forms.ToolStripMenuItem ohneAusrufezeichenToolStripMenuItem;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox searchText_txt;
        private System.Windows.Forms.NumericUpDown pageSwitcher_numupdown;
        private System.Windows.Forms.Label page_text_lbl;
        private System.Windows.Forms.ToolStripMenuItem notTranslateableToolStripMenuItem;
    }
}

