namespace MB_Studio.Manager
{
    partial class StringManager
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
            this.toolPanel.SuspendLayout();
            this.groupBox_0_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.Text = "Manager";
            // 
            // name_lbl
            // 
            this.name_lbl.Location = new System.Drawing.Point(469, 87);
            this.name_lbl.Size = new System.Drawing.Size(42, 16);
            this.name_lbl.Text = "Text:";
            // 
            // singleNameTranslation_lbl
            // 
            this.singleNameTranslation_lbl.Location = new System.Drawing.Point(419, 38);
            this.singleNameTranslation_lbl.Size = new System.Drawing.Size(42, 16);
            this.singleNameTranslation_lbl.Text = "Text:";
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
            // StringManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(814, 220);
            this.Name = "StringManager";
            this.Text = "StringManager";
            this.toolPanel.ResumeLayout(false);
            this.groupBox_0_gb.ResumeLayout(false);
            this.groupBox_0_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
