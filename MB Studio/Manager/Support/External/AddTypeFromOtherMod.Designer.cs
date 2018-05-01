namespace MB_Studio.Manager.Support.External
{
    partial class AddTypeFromOtherMod
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
            this.module_cbb = new System.Windows.Forms.ComboBox();
            this.module_lbl = new System.Windows.Forms.Label();
            this.addTypeFromMod_btn = new System.Windows.Forms.Button();
            this.type_cbb = new System.Windows.Forms.ComboBox();
            this.type_rb = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(276, -2);
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(308, 0);
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.Size = new System.Drawing.Size(276, 24);
            this.title_lbl.Text = "TEXT";
            // 
            // module_cbb
            // 
            this.module_cbb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.module_cbb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.module_cbb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.module_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.module_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.module_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.module_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.module_cbb.FormattingEnabled = true;
            this.module_cbb.ItemHeight = 20;
            this.module_cbb.Location = new System.Drawing.Point(90, 34);
            this.module_cbb.Name = "module_cbb";
            this.module_cbb.Size = new System.Drawing.Size(238, 28);
            this.module_cbb.Sorted = true;
            this.module_cbb.TabIndex = 0;
            this.module_cbb.TabStop = false;
            this.module_cbb.SelectedIndexChanged += new System.EventHandler(this.Module_cbb_SelectedIndexChanged);
            // 
            // module_lbl
            // 
            this.module_lbl.AutoSize = true;
            this.module_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.module_lbl.Location = new System.Drawing.Point(12, 37);
            this.module_lbl.Name = "module_lbl";
            this.module_lbl.Size = new System.Drawing.Size(72, 20);
            this.module_lbl.TabIndex = 0;
            this.module_lbl.Text = "Module:";
            // 
            // addTypeFromMod_btn
            // 
            this.addTypeFromMod_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTypeFromMod_btn.BackColor = System.Drawing.Color.DimGray;
            this.addTypeFromMod_btn.Enabled = false;
            this.addTypeFromMod_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTypeFromMod_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTypeFromMod_btn.Location = new System.Drawing.Point(90, 102);
            this.addTypeFromMod_btn.Name = "addTypeFromMod_btn";
            this.addTypeFromMod_btn.Size = new System.Drawing.Size(238, 27);
            this.addTypeFromMod_btn.TabIndex = 1;
            this.addTypeFromMod_btn.Tag = "";
            this.addTypeFromMod_btn.Text = "ADD TYPE";
            this.addTypeFromMod_btn.UseVisualStyleBackColor = false;
            this.addTypeFromMod_btn.Click += new System.EventHandler(this.AddTypeFromMod_btn_Click);
            // 
            // type_cbb
            // 
            this.type_cbb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.type_cbb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.type_cbb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.type_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.type_cbb.Enabled = false;
            this.type_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.type_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.type_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.type_cbb.FormattingEnabled = true;
            this.type_cbb.ItemHeight = 20;
            this.type_cbb.Location = new System.Drawing.Point(90, 68);
            this.type_cbb.Name = "type_cbb";
            this.type_cbb.Size = new System.Drawing.Size(238, 28);
            this.type_cbb.Sorted = true;
            this.type_cbb.TabIndex = 0;
            this.type_cbb.TabStop = false;
            this.type_cbb.SelectedIndexChanged += new System.EventHandler(this.Types_cbb_SelectedIndexChanged);
            // 
            // type_rb
            // 
            this.type_rb.AutoSize = true;
            this.type_rb.Checked = true;
            this.type_rb.Enabled = false;
            this.type_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.type_rb.Location = new System.Drawing.Point(14, 69);
            this.type_rb.Name = "type_rb";
            this.type_rb.Size = new System.Drawing.Size(70, 24);
            this.type_rb.TabIndex = 2;
            this.type_rb.TabStop = true;
            this.type_rb.Text = "Type:";
            this.type_rb.UseVisualStyleBackColor = true;
            this.type_rb.CheckedChanged += new System.EventHandler(this.Type_rb_CheckedChanged);
            // 
            // AddTypeFromOtherMod
            // 
            this.ClientSize = new System.Drawing.Size(340, 136);
            this.Controls.Add(this.type_rb);
            this.Controls.Add(this.type_cbb);
            this.Controls.Add(this.addTypeFromMod_btn);
            this.Controls.Add(this.module_lbl);
            this.Controls.Add(this.module_cbb);
            this.Name = "AddTypeFromOtherMod";
            this.Text = "ADD TYPE TO MOD";
            this.Load += new System.EventHandler(this.AddTypeFromOtherMod_Load);
            this.Controls.SetChildIndex(this.module_cbb, 0);
            this.Controls.SetChildIndex(this.module_lbl, 0);
            this.Controls.SetChildIndex(this.addTypeFromMod_btn, 0);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.type_cbb, 0);
            this.Controls.SetChildIndex(this.type_rb, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox module_cbb;
        protected System.Windows.Forms.Label module_lbl;
        protected System.Windows.Forms.Button addTypeFromMod_btn;
        protected System.Windows.Forms.ComboBox type_cbb;
        protected System.Windows.Forms.RadioButton type_rb;
    }
}
