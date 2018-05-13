namespace MB_Studio.Manager
{
    partial class InfoPageManager
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
            this.text_txt = new System.Windows.Forms.TextBox();
            this.text_lbl = new System.Windows.Forms.Label();
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.Text = "ToolForm";
            // 
            // showGroup_0_btn
            // 
            this.showGroup_0_btn.Tag = "120";
            // 
            // groupBox_0_gb
            // 
            this.groupBox_0_gb.Controls.Add(this.text_lbl);
            this.groupBox_0_gb.Controls.Add(this.text_txt);
            this.groupBox_0_gb.Controls.SetChildIndex(this.save_translation_btn, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.language_cbb, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.singleNameTranslation_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.singleNameTranslation_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.pluralNameTranslation_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.pluralNameTranslation_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.language_lbl, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.text_txt, 0);
            this.groupBox_0_gb.Controls.SetChildIndex(this.text_lbl, 0);
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
            // text_txt
            // 
            this.text_txt.Location = new System.Drawing.Point(233, 86);
            this.text_txt.Multiline = true;
            this.text_txt.Name = "text_txt";
            this.text_txt.Size = new System.Drawing.Size(491, 153);
            this.text_txt.TabIndex = 21;
            this.text_txt.Text = "0";
            // 
            // text_lbl
            // 
            this.text_lbl.AutoSize = true;
            this.text_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_lbl.Location = new System.Drawing.Point(190, 92);
            this.text_lbl.Name = "text_lbl";
            this.text_lbl.Size = new System.Drawing.Size(42, 16);
            this.text_lbl.TabIndex = 22;
            this.text_lbl.Text = "Text:";
            // 
            // InfoPageManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 220);
            this.Name = "InfoPageManager";
            this.Text = "InfoPageManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label text_lbl;
        private System.Windows.Forms.TextBox text_txt;
    }
}
