namespace MB_Studio.Manager.Support
{
    partial class AddItemFromOtherMod
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
            this.meshName_cbb = new System.Windows.Forms.ComboBox();
            this.meshName_rb = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // module_cbb
            // 
            this.module_cbb.Text = " < SELECT >";
            // 
            // addTypeFromMod_btn
            // 
            this.addTypeFromMod_btn.Location = new System.Drawing.Point(90, 138);
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
            // meshName_cbb
            // 
            this.meshName_cbb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.meshName_cbb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.meshName_cbb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.meshName_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.meshName_cbb.Enabled = false;
            this.meshName_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.meshName_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meshName_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.meshName_cbb.FormattingEnabled = true;
            this.meshName_cbb.ItemHeight = 20;
            this.meshName_cbb.Location = new System.Drawing.Point(90, 103);
            this.meshName_cbb.Name = "meshName_cbb";
            this.meshName_cbb.Size = new System.Drawing.Size(238, 28);
            this.meshName_cbb.Sorted = true;
            this.meshName_cbb.TabIndex = 3;
            this.meshName_cbb.TabStop = false;
            this.meshName_cbb.SelectedIndexChanged += new System.EventHandler(this.MeshName_cbb_SelectedIndexChanged);
            // 
            // meshName_rb
            // 
            this.meshName_rb.AutoSize = true;
            this.meshName_rb.Enabled = false;
            this.meshName_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meshName_rb.Location = new System.Drawing.Point(14, 104);
            this.meshName_rb.Name = "meshName_rb";
            this.meshName_rb.Size = new System.Drawing.Size(75, 24);
            this.meshName_rb.TabIndex = 4;
            this.meshName_rb.TabStop = true;
            this.meshName_rb.Text = "Mesh:";
            this.meshName_rb.UseVisualStyleBackColor = true;
            this.meshName_rb.CheckedChanged += new System.EventHandler(this.MeshName_rb_CheckedChanged);
            // 
            // AddItemFromOtherMod
            // 
            this.ClientSize = new System.Drawing.Size(340, 174);
            this.Controls.Add(this.meshName_rb);
            this.Controls.Add(this.meshName_cbb);
            this.Name = "AddItemFromOtherMod";
            this.Controls.SetChildIndex(this.module_cbb, 0);
            this.Controls.SetChildIndex(this.module_lbl, 0);
            this.Controls.SetChildIndex(this.addTypeFromMod_btn, 0);
            this.Controls.SetChildIndex(this.type_rb, 0);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.type_cbb, 0);
            this.Controls.SetChildIndex(this.meshName_cbb, 0);
            this.Controls.SetChildIndex(this.meshName_rb, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox meshName_cbb;
        protected System.Windows.Forms.RadioButton meshName_rb;
    }
}
