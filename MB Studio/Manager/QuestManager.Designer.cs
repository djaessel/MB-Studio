namespace MB_Studio.Manager
{
    partial class QuestManager
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
        private new void InitializeComponent()
        {
            this.showGroup_1_btn = new System.Windows.Forms.Button();
            this.groupBox_1_gb = new System.Windows.Forms.GroupBox();
            this.hero_cb = new System.Windows.Forms.CheckBox();
            this.type_lbl = new System.Windows.Forms.Label();
            this.troopTypes_lb = new System.Windows.Forms.ListBox();
            this.guarantee_gb = new System.Windows.Forms.GroupBox();
            this.boots_cb = new System.Windows.Forms.CheckBox();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.groupBox_1_gb.SuspendLayout();
            this.guarantee_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.Text = "Manager";
            // 
            // plural_name_lbl
            // 
            this.plural_name_lbl.Location = new System.Drawing.Point(415, 112);
            this.plural_name_lbl.Size = new System.Drawing.Size(91, 16);
            this.plural_name_lbl.Text = "Description:";
            // 
            // name_lbl
            // 
            this.name_lbl.Location = new System.Drawing.Point(453, 87);
            this.name_lbl.Size = new System.Drawing.Size(53, 16);
            this.name_lbl.Text = "Name:";
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.showGroup_1_btn);
            this.toolPanel.Controls.Add(this.groupBox_1_gb);
            this.toolPanel.Size = new System.Drawing.Size(778, 167);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_0_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_0_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_1_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_1_btn, 0);
            // 
            // showGroup_0_btn
            // 
            this.showGroup_0_btn.Tag = "40";
            // 
            // pluralNameTranslation_lbl
            // 
            this.pluralNameTranslation_lbl.Location = new System.Drawing.Point(370, 63);
            this.pluralNameTranslation_lbl.Size = new System.Drawing.Size(91, 16);
            this.pluralNameTranslation_lbl.Text = "Description:";
            // 
            // pluralNameTranslation_txt
            // 
            this.pluralNameTranslation_txt.Multiline = true;
            this.pluralNameTranslation_txt.Size = new System.Drawing.Size(257, 95);
            // 
            // singleNameTranslation_lbl
            // 
            this.singleNameTranslation_lbl.Location = new System.Drawing.Point(408, 38);
            this.singleNameTranslation_lbl.Size = new System.Drawing.Size(53, 16);
            this.singleNameTranslation_lbl.Text = "Name:";
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            // 
            // showGroup_1_btn
            // 
            this.showGroup_1_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_1_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_1_btn.Location = new System.Drawing.Point(3, 32);
            this.showGroup_1_btn.Name = "showGroup_1_btn";
            this.showGroup_1_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_1_btn.TabIndex = 24;
            this.showGroup_1_btn.Tag = "0";
            this.showGroup_1_btn.Text = "v";
            this.showGroup_1_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_1_gb
            // 
            this.groupBox_1_gb.Controls.Add(this.hero_cb);
            this.groupBox_1_gb.Controls.Add(this.type_lbl);
            this.groupBox_1_gb.Controls.Add(this.troopTypes_lb);
            this.groupBox_1_gb.Controls.Add(this.guarantee_gb);
            this.groupBox_1_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_1_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_1_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_1_gb.Location = new System.Drawing.Point(39, 30);
            this.groupBox_1_gb.Name = "groupBox_1_gb";
            this.groupBox_1_gb.Size = new System.Drawing.Size(737, 128);
            this.groupBox_1_gb.TabIndex = 23;
            this.groupBox_1_gb.TabStop = false;
            this.groupBox_1_gb.Text = "Flags";
            // 
            // hero_cb
            // 
            this.hero_cb.AutoSize = true;
            this.hero_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hero_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hero_cb.Location = new System.Drawing.Point(184, 29);
            this.hero_cb.Name = "hero_cb";
            this.hero_cb.Size = new System.Drawing.Size(58, 20);
            this.hero_cb.TabIndex = 7;
            this.hero_cb.Text = "Hero";
            this.hero_cb.UseVisualStyleBackColor = true;
            // 
            // type_lbl
            // 
            this.type_lbl.AutoSize = true;
            this.type_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.type_lbl.Location = new System.Drawing.Point(2, 30);
            this.type_lbl.Name = "type_lbl";
            this.type_lbl.Size = new System.Drawing.Size(48, 16);
            this.type_lbl.TabIndex = 6;
            this.type_lbl.Text = "Type:";
            // 
            // troopTypes_lb
            // 
            this.troopTypes_lb.BackColor = System.Drawing.Color.DimGray;
            this.troopTypes_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.troopTypes_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.troopTypes_lb.ForeColor = System.Drawing.Color.White;
            this.troopTypes_lb.FormattingEnabled = true;
            this.troopTypes_lb.ItemHeight = 18;
            this.troopTypes_lb.Items.AddRange(new object[] {
            "Type0 (Male)",
            "Type1 (Female)",
            "Type2",
            "Type3",
            "Type4",
            "Type5",
            "Type6",
            "Type7",
            "Type8",
            "Type9",
            "Type10",
            "Type11",
            "Type12",
            "Type13",
            "Type14",
            "Type15"});
            this.troopTypes_lb.Location = new System.Drawing.Point(51, 28);
            this.troopTypes_lb.Name = "troopTypes_lb";
            this.troopTypes_lb.Size = new System.Drawing.Size(124, 74);
            this.troopTypes_lb.TabIndex = 5;
            // 
            // guarantee_gb
            // 
            this.guarantee_gb.Controls.Add(this.boots_cb);
            this.guarantee_gb.ForeColor = System.Drawing.Color.White;
            this.guarantee_gb.Location = new System.Drawing.Point(482, 25);
            this.guarantee_gb.Name = "guarantee_gb";
            this.guarantee_gb.Size = new System.Drawing.Size(248, 84);
            this.guarantee_gb.TabIndex = 0;
            this.guarantee_gb.TabStop = false;
            this.guarantee_gb.Text = "Guarantee";
            // 
            // boots_cb
            // 
            this.boots_cb.AutoSize = true;
            this.boots_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.boots_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boots_cb.Location = new System.Drawing.Point(181, 26);
            this.boots_cb.Name = "boots_cb";
            this.boots_cb.Size = new System.Drawing.Size(50, 17);
            this.boots_cb.TabIndex = 3;
            this.boots_cb.Text = "Boots";
            this.boots_cb.UseVisualStyleBackColor = true;
            // 
            // QuestManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 357);
            this.Name = "QuestManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.groupBox_1_gb.ResumeLayout(false);
            this.groupBox_1_gb.PerformLayout();
            this.guarantee_gb.ResumeLayout(false);
            this.guarantee_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showGroup_1_btn;
        private System.Windows.Forms.GroupBox groupBox_1_gb;
        private System.Windows.Forms.CheckBox hero_cb;
        private System.Windows.Forms.Label type_lbl;
        private System.Windows.Forms.ListBox troopTypes_lb;
        private System.Windows.Forms.GroupBox guarantee_gb;
        private System.Windows.Forms.CheckBox boots_cb;
    }
}
