namespace MB_Studio.Manager
{
    partial class SkillManager
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
            this.description_txt = new System.Windows.Forms.TextBox();
            this.description_lbl = new System.Windows.Forms.Label();
            this.showGroup_1_btn = new System.Windows.Forms.Button();
            this.groupBox_1_gb = new System.Windows.Forms.GroupBox();
            this.maxLevel_lbl = new System.Windows.Forms.Label();
            this.maxLevel_num = new System.Windows.Forms.NumericUpDown();
            this.base_attribute_gb = new System.Windows.Forms.GroupBox();
            this.cha_rb = new System.Windows.Forms.RadioButton();
            this.int_rb = new System.Windows.Forms.RadioButton();
            this.agi_rb = new System.Windows.Forms.RadioButton();
            this.str_rb = new System.Windows.Forms.RadioButton();
            this.effects_party_cb = new System.Windows.Forms.CheckBox();
            this.inactive_cb = new System.Windows.Forms.CheckBox();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.groupBox_1_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxLevel_num)).BeginInit();
            this.base_attribute_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.Text = "ToolForm";
            // 
            // toolPanel
            // 
            this.toolPanel.Controls.Add(this.groupBox_1_gb);
            this.toolPanel.Controls.Add(this.showGroup_1_btn);
            this.toolPanel.Size = new System.Drawing.Size(778, 56);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_0_gb, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_0_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.showGroup_1_btn, 0);
            this.toolPanel.Controls.SetChildIndex(this.groupBox_1_gb, 0);
            // 
            // showGroup_0_btn
            // 
            this.showGroup_0_btn.Tag = "120";
            // 
            // groupBox_0_gb
            // 
            this.groupBox_0_gb.Controls.Add(this.description_lbl);
            this.groupBox_0_gb.Controls.Add(this.description_txt);
            this.groupBox_0_gb.Controls.SetChildIndex(this.save_translation_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.language_cbb, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.singleNameTranslation_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.singleNameTranslation_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.pluralNameTranslation_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.pluralNameTranslation_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.language_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.description_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.description_lbl, 0);
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
            // description_txt
            // 
            this.description_txt.Location = new System.Drawing.Point(233, 86);
            this.description_txt.Multiline = true;
            this.description_txt.Name = "description_txt";
            this.description_txt.Size = new System.Drawing.Size(491, 153);
            this.description_txt.TabIndex = 22;
            this.description_txt.Text = "0";
            // 
            // description_lbl
            // 
            this.description_lbl.AutoSize = true;
            this.description_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.description_lbl.Location = new System.Drawing.Point(136, 92);
            this.description_lbl.Name = "description_lbl";
            this.description_lbl.Size = new System.Drawing.Size(91, 16);
            this.description_lbl.TabIndex = 23;
            this.description_lbl.Text = "Description:";
            // 
            // showGroup_1_btn
            // 
            this.showGroup_1_btn.BackColor = System.Drawing.Color.DimGray;
            this.showGroup_1_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGroup_1_btn.Location = new System.Drawing.Point(3, 31);
            this.showGroup_1_btn.Name = "showGroup_1_btn";
            this.showGroup_1_btn.Size = new System.Drawing.Size(26, 25);
            this.showGroup_1_btn.TabIndex = 23;
            this.showGroup_1_btn.Tag = "-35";
            this.showGroup_1_btn.Text = "v";
            this.showGroup_1_btn.UseVisualStyleBackColor = false;
            // 
            // groupBox_1_gb
            // 
            this.groupBox_1_gb.Controls.Add(this.inactive_cb);
            this.groupBox_1_gb.Controls.Add(this.effects_party_cb);
            this.groupBox_1_gb.Controls.Add(this.base_attribute_gb);
            this.groupBox_1_gb.Controls.Add(this.maxLevel_num);
            this.groupBox_1_gb.Controls.Add(this.maxLevel_lbl);
            this.groupBox_1_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_1_gb.ForeColor = System.Drawing.Color.White;
            this.groupBox_1_gb.Location = new System.Drawing.Point(39, 31);
            this.groupBox_1_gb.Name = "groupBox_1_gb";
            this.groupBox_1_gb.Size = new System.Drawing.Size(737, 25);
            this.groupBox_1_gb.TabIndex = 24;
            this.groupBox_1_gb.TabStop = false;
            this.groupBox_1_gb.Text = "Flags && MaxLevel";
            // 
            // maxLevel_lbl
            // 
            this.maxLevel_lbl.AutoSize = true;
            this.maxLevel_lbl.Location = new System.Drawing.Point(356, 52);
            this.maxLevel_lbl.Name = "maxLevel_lbl";
            this.maxLevel_lbl.Size = new System.Drawing.Size(83, 20);
            this.maxLevel_lbl.TabIndex = 0;
            this.maxLevel_lbl.Text = "Max Level:";
            // 
            // maxLevel_num
            // 
            this.maxLevel_num.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maxLevel_num.Location = new System.Drawing.Point(445, 50);
            this.maxLevel_num.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.maxLevel_num.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.maxLevel_num.Name = "maxLevel_num";
            this.maxLevel_num.Size = new System.Drawing.Size(42, 26);
            this.maxLevel_num.TabIndex = 1;
            this.maxLevel_num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // base_attribute_gb
            // 
            this.base_attribute_gb.Controls.Add(this.cha_rb);
            this.base_attribute_gb.Controls.Add(this.int_rb);
            this.base_attribute_gb.Controls.Add(this.agi_rb);
            this.base_attribute_gb.Controls.Add(this.str_rb);
            this.base_attribute_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.base_attribute_gb.ForeColor = System.Drawing.Color.White;
            this.base_attribute_gb.Location = new System.Drawing.Point(7, 28);
            this.base_attribute_gb.Name = "base_attribute_gb";
            this.base_attribute_gb.Size = new System.Drawing.Size(340, 54);
            this.base_attribute_gb.TabIndex = 25;
            this.base_attribute_gb.TabStop = false;
            this.base_attribute_gb.Text = "Base Attribute";
            // 
            // cha_rb
            // 
            this.cha_rb.AutoSize = true;
            this.cha_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cha_rb.Location = new System.Drawing.Point(255, 24);
            this.cha_rb.Name = "cha_rb";
            this.cha_rb.Size = new System.Drawing.Size(83, 20);
            this.cha_rb.TabIndex = 9;
            this.cha_rb.TabStop = true;
            this.cha_rb.Text = "Charisma";
            this.cha_rb.UseVisualStyleBackColor = true;
            // 
            // int_rb
            // 
            this.int_rb.AutoSize = true;
            this.int_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.int_rb.Location = new System.Drawing.Point(155, 24);
            this.int_rb.Name = "int_rb";
            this.int_rb.Size = new System.Drawing.Size(94, 20);
            this.int_rb.TabIndex = 8;
            this.int_rb.TabStop = true;
            this.int_rb.Text = "Intelligence";
            this.int_rb.UseVisualStyleBackColor = true;
            // 
            // agi_rb
            // 
            this.agi_rb.AutoSize = true;
            this.agi_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agi_rb.Location = new System.Drawing.Point(87, 24);
            this.agi_rb.Name = "agi_rb";
            this.agi_rb.Size = new System.Drawing.Size(62, 20);
            this.agi_rb.TabIndex = 7;
            this.agi_rb.TabStop = true;
            this.agi_rb.Text = "Agility";
            this.agi_rb.UseVisualStyleBackColor = true;
            // 
            // str_rb
            // 
            this.str_rb.AutoSize = true;
            this.str_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.str_rb.Location = new System.Drawing.Point(6, 24);
            this.str_rb.Name = "str_rb";
            this.str_rb.Size = new System.Drawing.Size(75, 20);
            this.str_rb.TabIndex = 6;
            this.str_rb.TabStop = true;
            this.str_rb.Text = "Strength";
            this.str_rb.UseVisualStyleBackColor = true;
            // 
            // effects_party_cb
            // 
            this.effects_party_cb.AutoSize = true;
            this.effects_party_cb.Location = new System.Drawing.Point(499, 57);
            this.effects_party_cb.Name = "effects_party_cb";
            this.effects_party_cb.Size = new System.Drawing.Size(119, 24);
            this.effects_party_cb.TabIndex = 26;
            this.effects_party_cb.Text = "Effects Party";
            this.effects_party_cb.UseVisualStyleBackColor = true;
            // 
            // inactive_cb
            // 
            this.inactive_cb.AutoSize = true;
            this.inactive_cb.Location = new System.Drawing.Point(499, 34);
            this.inactive_cb.Name = "inactive_cb";
            this.inactive_cb.Size = new System.Drawing.Size(83, 24);
            this.inactive_cb.TabIndex = 27;
            this.inactive_cb.Text = "Inactive";
            this.inactive_cb.UseVisualStyleBackColor = true;
            // 
            // SkillManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 246);
            this.Name = "SkillManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.groupBox_1_gb.ResumeLayout(false);
            this.groupBox_1_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxLevel_num)).EndInit();
            this.base_attribute_gb.ResumeLayout(false);
            this.base_attribute_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox description_txt;
        private System.Windows.Forms.Label description_lbl;
        private System.Windows.Forms.GroupBox groupBox_1_gb;
        private System.Windows.Forms.NumericUpDown maxLevel_num;
        private System.Windows.Forms.Label maxLevel_lbl;
        protected System.Windows.Forms.Button showGroup_1_btn;
        private System.Windows.Forms.GroupBox base_attribute_gb;
        private System.Windows.Forms.RadioButton cha_rb;
        private System.Windows.Forms.RadioButton int_rb;
        private System.Windows.Forms.RadioButton agi_rb;
        private System.Windows.Forms.RadioButton str_rb;
        private System.Windows.Forms.CheckBox inactive_cb;
        private System.Windows.Forms.CheckBox effects_party_cb;
    }
}
