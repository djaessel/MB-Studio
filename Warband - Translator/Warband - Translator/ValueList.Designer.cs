namespace WarbandTranslator
{
    partial class ValueList
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.property_lbl = new System.Windows.Forms.Label();
            this.value_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // property_lbl
            // 
            this.property_lbl.Location = new System.Drawing.Point(-3, 0);
            this.property_lbl.Name = "property_lbl";
            this.property_lbl.Size = new System.Drawing.Size(127, 30);
            this.property_lbl.TabIndex = 0;
            this.property_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // value_txt
            // 
            this.value_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.value_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.value_txt.Location = new System.Drawing.Point(127, 7);
            this.value_txt.Name = "value_txt";
            this.value_txt.ReadOnly = true;
            this.value_txt.Size = new System.Drawing.Size(109, 20);
            this.value_txt.TabIndex = 1;
            this.value_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ValueList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.value_txt);
            this.Controls.Add(this.property_lbl);
            this.Name = "ValueList";
            this.Size = new System.Drawing.Size(239, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label property_lbl;
        private System.Windows.Forms.TextBox value_txt;
    }
}
