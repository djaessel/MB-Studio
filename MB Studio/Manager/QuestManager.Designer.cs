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
            this.random_quest_cb = new System.Windows.Forms.CheckBox();
            this.show_progression_cb = new System.Windows.Forms.CheckBox();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.groupBox_1_gb.SuspendLayout();
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
            this.plural_name_lbl.Visible = true;
            // 
            // plural_name_txt
            // 
            this.plural_name_txt.Visible = true;
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
            this.toolPanel.Size = new System.Drawing.Size(778, 60);
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
            this.pluralNameTranslation_lbl.Visible = true;
            // 
            // pluralNameTranslation_txt
            // 
            this.pluralNameTranslation_txt.Multiline = true;
            this.pluralNameTranslation_txt.Size = new System.Drawing.Size(257, 95);
            this.pluralNameTranslation_txt.Visible = true;
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
            this.showGroup_1_btn.Location = new System.Drawing.Point(3, 33);
            this.showGroup_1_btn.Name = "showGroup_1_btn";
            this.showGroup_1_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_1_btn.TabIndex = 24;
            this.showGroup_1_btn.Tag = "-60";
            this.showGroup_1_btn.Text = "v";
            this.showGroup_1_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_1_gb
            // 
            this.groupBox_1_gb.Controls.Add(this.random_quest_cb);
            this.groupBox_1_gb.Controls.Add(this.show_progression_cb);
            this.groupBox_1_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_1_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_1_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_1_gb.Location = new System.Drawing.Point(39, 31);
            this.groupBox_1_gb.Name = "groupBox_1_gb";
            this.groupBox_1_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_1_gb.TabIndex = 23;
            this.groupBox_1_gb.TabStop = false;
            this.groupBox_1_gb.Text = "Flags";
            // 
            // random_quest_cb
            // 
            this.random_quest_cb.AutoSize = true;
            this.random_quest_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.random_quest_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.random_quest_cb.Location = new System.Drawing.Point(166, 33);
            this.random_quest_cb.Name = "random_quest_cb";
            this.random_quest_cb.Size = new System.Drawing.Size(126, 20);
            this.random_quest_cb.TabIndex = 8;
            this.random_quest_cb.Text = "Random Quest";
            this.random_quest_cb.UseVisualStyleBackColor = true;
            this.random_quest_cb.CheckedChanged += new System.EventHandler(this.Random_quest_cb_CheckedChanged);
            // 
            // show_progression_cb
            // 
            this.show_progression_cb.AutoSize = true;
            this.show_progression_cb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show_progression_cb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show_progression_cb.Location = new System.Drawing.Point(11, 33);
            this.show_progression_cb.Name = "show_progression_cb";
            this.show_progression_cb.Size = new System.Drawing.Size(149, 20);
            this.show_progression_cb.TabIndex = 7;
            this.show_progression_cb.Text = "Show Progression";
            this.show_progression_cb.UseVisualStyleBackColor = true;
            this.show_progression_cb.CheckedChanged += new System.EventHandler(this.Show_progression_cb_CheckedChanged);
            // 
            // QuestManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 250);
            this.Name = "QuestManager";
            this.Text = "QuestManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.groupBox_1_gb.ResumeLayout(false);
            this.groupBox_1_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showGroup_1_btn;
        private System.Windows.Forms.GroupBox groupBox_1_gb;
        private System.Windows.Forms.CheckBox show_progression_cb;
        private System.Windows.Forms.CheckBox random_quest_cb;
    }
}
