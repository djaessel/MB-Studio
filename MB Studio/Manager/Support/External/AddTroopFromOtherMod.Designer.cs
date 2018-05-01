namespace MB_Studio.Manager.Support.External
{
    partial class AddTroopFromOtherMod
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
            this.SuspendLayout();
            // 
            // module_cbb
            // 
            this.module_cbb.Text = " < SELECT >";
            // 
            // type_cbb
            // 
            this.type_cbb.Text = " < SELECT >";
            this.type_cbb.SelectedIndexChanged += new System.EventHandler(this.Types_cbb_SelectedIndexChanged);
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
            // title_lbl
            // 
            this.title_lbl.Text = "Add Type To Mod";
            // 
            // AddTroopFromOtherMod
            // 
            this.ClientSize = new System.Drawing.Size(340, 138);
            this.Name = "AddTroopFromOtherMod";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
