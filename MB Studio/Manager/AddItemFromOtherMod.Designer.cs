namespace MB_Studio.Manager
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
            this.module_cbb = new System.Windows.Forms.ComboBox();
            this.module_lbl = new System.Windows.Forms.Label();
            this.addItemFromMod_btn = new System.Windows.Forms.Button();
            this.addKind_gb = new System.Windows.Forms.GroupBox();
            this.item_rb = new System.Windows.Forms.RadioButton();
            this.meshName_rb = new System.Windows.Forms.RadioButton();
            this.item_cbb = new System.Windows.Forms.ComboBox();
            this.meshName_cbb = new System.Windows.Forms.ComboBox();
            this.addKind_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(310, -2);
            this.min_btn.TabIndex = 0;
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(342, 1);
            this.exit_btn.TabIndex = 0;
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.Size = new System.Drawing.Size(310, 24);
            this.title_lbl.TabIndex = 0;
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
            this.module_cbb.Location = new System.Drawing.Point(137, 38);
            this.module_cbb.Name = "module_cbb";
            this.module_cbb.Size = new System.Drawing.Size(226, 28);
            this.module_cbb.Sorted = true;
            this.module_cbb.TabIndex = 0;
            this.module_cbb.TabStop = false;
            this.module_cbb.Text = " < SELECT MODULE >";
            this.module_cbb.SelectedIndexChanged += new System.EventHandler(this.Module_cbb_SelectedIndexChanged);
            // 
            // module_lbl
            // 
            this.module_lbl.AutoSize = true;
            this.module_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.module_lbl.Location = new System.Drawing.Point(27, 41);
            this.module_lbl.Name = "module_lbl";
            this.module_lbl.Size = new System.Drawing.Size(72, 20);
            this.module_lbl.TabIndex = 0;
            this.module_lbl.Text = "Module:";
            // 
            // addItemFromMod_btn
            // 
            this.addItemFromMod_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addItemFromMod_btn.BackColor = System.Drawing.Color.DimGray;
            this.addItemFromMod_btn.Enabled = false;
            this.addItemFromMod_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addItemFromMod_btn.Location = new System.Drawing.Point(137, 170);
            this.addItemFromMod_btn.Name = "addItemFromMod_btn";
            this.addItemFromMod_btn.Size = new System.Drawing.Size(226, 23);
            this.addItemFromMod_btn.TabIndex = 1;
            this.addItemFromMod_btn.Tag = "";
            this.addItemFromMod_btn.Text = "ADD ITEM";
            this.addItemFromMod_btn.UseVisualStyleBackColor = false;
            this.addItemFromMod_btn.Click += new System.EventHandler(this.AddItemFromMod_btn_Click);
            // 
            // addKind_gb
            // 
            this.addKind_gb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addKind_gb.Controls.Add(this.item_rb);
            this.addKind_gb.Controls.Add(this.meshName_rb);
            this.addKind_gb.Controls.Add(this.item_cbb);
            this.addKind_gb.Controls.Add(this.meshName_cbb);
            this.addKind_gb.Enabled = false;
            this.addKind_gb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addKind_gb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addKind_gb.ForeColor = System.Drawing.Color.White;
            this.addKind_gb.Location = new System.Drawing.Point(4, 70);
            this.addKind_gb.Name = "addKind_gb";
            this.addKind_gb.Size = new System.Drawing.Size(366, 90);
            this.addKind_gb.TabIndex = 0;
            this.addKind_gb.TabStop = false;
            this.addKind_gb.Text = "Add Kind";
            // 
            // item_rb
            // 
            this.item_rb.AutoSize = true;
            this.item_rb.FlatAppearance.BorderSize = 0;
            this.item_rb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item_rb.Location = new System.Drawing.Point(8, 53);
            this.item_rb.Name = "item_rb";
            this.item_rb.Size = new System.Drawing.Size(67, 24);
            this.item_rb.TabIndex = 0;
            this.item_rb.Text = "Item:";
            this.item_rb.UseVisualStyleBackColor = true;
            this.item_rb.CheckedChanged += new System.EventHandler(this.Item_rb_CheckedChanged);
            // 
            // meshName_rb
            // 
            this.meshName_rb.AutoSize = true;
            this.meshName_rb.Checked = true;
            this.meshName_rb.FlatAppearance.BorderSize = 0;
            this.meshName_rb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.meshName_rb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meshName_rb.Location = new System.Drawing.Point(8, 19);
            this.meshName_rb.Name = "meshName_rb";
            this.meshName_rb.Size = new System.Drawing.Size(118, 24);
            this.meshName_rb.TabIndex = 0;
            this.meshName_rb.TabStop = true;
            this.meshName_rb.Text = "Meshname:";
            this.meshName_rb.UseVisualStyleBackColor = true;
            this.meshName_rb.CheckedChanged += new System.EventHandler(this.MeshName_rb_CheckedChanged);
            // 
            // item_cbb
            // 
            this.item_cbb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.item_cbb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.item_cbb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.item_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.item_cbb.Enabled = false;
            this.item_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.item_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.item_cbb.FormattingEnabled = true;
            this.item_cbb.ItemHeight = 20;
            this.item_cbb.Location = new System.Drawing.Point(133, 52);
            this.item_cbb.Name = "item_cbb";
            this.item_cbb.Size = new System.Drawing.Size(226, 28);
            this.item_cbb.Sorted = true;
            this.item_cbb.TabIndex = 0;
            this.item_cbb.TabStop = false;
            this.item_cbb.SelectedIndexChanged += new System.EventHandler(this.Item_cbb_SelectedIndexChanged);
            // 
            // meshName_cbb
            // 
            this.meshName_cbb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.meshName_cbb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.meshName_cbb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.meshName_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.meshName_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.meshName_cbb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meshName_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.meshName_cbb.FormattingEnabled = true;
            this.meshName_cbb.ItemHeight = 20;
            this.meshName_cbb.Location = new System.Drawing.Point(133, 18);
            this.meshName_cbb.Name = "meshName_cbb";
            this.meshName_cbb.Size = new System.Drawing.Size(226, 28);
            this.meshName_cbb.Sorted = true;
            this.meshName_cbb.TabIndex = 0;
            this.meshName_cbb.TabStop = false;
            this.meshName_cbb.SelectedIndexChanged += new System.EventHandler(this.MeshName_cbb_SelectedIndexChanged);
            // 
            // AddItemFromOtherMod
            // 
            this.ClientSize = new System.Drawing.Size(375, 203);
            this.Controls.Add(this.addKind_gb);
            this.Controls.Add(this.addItemFromMod_btn);
            this.Controls.Add(this.module_lbl);
            this.Controls.Add(this.module_cbb);
            this.Name = "AddItemFromOtherMod";
            this.Load += new System.EventHandler(this.AddItemFromOtherMod_Load);
            this.Controls.SetChildIndex(this.module_cbb, 0);
            this.Controls.SetChildIndex(this.module_lbl, 0);
            this.Controls.SetChildIndex(this.addItemFromMod_btn, 0);
            this.Controls.SetChildIndex(this.addKind_gb, 0);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.addKind_gb.ResumeLayout(false);
            this.addKind_gb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox module_cbb;
        private System.Windows.Forms.Label module_lbl;
        private System.Windows.Forms.Button addItemFromMod_btn;
        private System.Windows.Forms.GroupBox addKind_gb;
        private System.Windows.Forms.RadioButton item_rb;
        private System.Windows.Forms.RadioButton meshName_rb;
        private System.Windows.Forms.ComboBox item_cbb;
        private System.Windows.Forms.ComboBox meshName_cbb;
    }
}
