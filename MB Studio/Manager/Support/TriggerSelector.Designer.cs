namespace MB_Studio
{
    partial class TriggerSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            this.trigger_cbb = new System.Windows.Forms.ComboBox();
            this.trigger_lbl = new System.Windows.Forms.Label();
            this.addTrigger_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // min_btn
            // 
            this.min_btn.FlatAppearance.BorderSize = 0;
            this.min_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.min_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.min_btn.Location = new System.Drawing.Point(352, -2);
            // 
            // exit_btn
            // 
            this.exit_btn.FlatAppearance.BorderSize = 0;
            this.exit_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.exit_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.exit_btn.Location = new System.Drawing.Point(384, 1);
            // 
            // title_lbl
            // 
            this.title_lbl.Size = new System.Drawing.Size(352, 24);
            // 
            // trigger_cbb
            // 
            this.trigger_cbb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56)))));
            this.trigger_cbb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.trigger_cbb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.trigger_cbb.FormattingEnabled = true;
            this.trigger_cbb.ItemHeight = 13;
            this.trigger_cbb.Location = new System.Drawing.Point(81, 40);
            this.trigger_cbb.Name = "trigger_cbb";
            this.trigger_cbb.Size = new System.Drawing.Size(286, 21);
            this.trigger_cbb.TabIndex = 49;
            this.trigger_cbb.Text = "None";
            // 
            // trigger_lbl
            // 
            this.trigger_lbl.AutoSize = true;
            this.trigger_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trigger_lbl.Location = new System.Drawing.Point(12, 41);
            this.trigger_lbl.Name = "trigger_lbl";
            this.trigger_lbl.Size = new System.Drawing.Size(63, 16);
            this.trigger_lbl.TabIndex = 50;
            this.trigger_lbl.Text = "Trigger:";
            // 
            // addTrigger_btn
            // 
            this.addTrigger_btn.BackColor = System.Drawing.Color.DimGray;
            this.addTrigger_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addTrigger_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addTrigger_btn.Location = new System.Drawing.Point(373, 38);
            this.addTrigger_btn.Name = "addTrigger_btn";
            this.addTrigger_btn.Size = new System.Drawing.Size(31, 25);
            this.addTrigger_btn.TabIndex = 51;
            this.addTrigger_btn.Tag = "0";
            this.addTrigger_btn.Text = "+";
            this.addTrigger_btn.UseVisualStyleBackColor = false;
            this.addTrigger_btn.Click += new System.EventHandler(this.AddTrigger_btn_Click);
            // 
            // TriggerSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 76);
            this.Controls.Add(this.addTrigger_btn);
            this.Controls.Add(this.trigger_lbl);
            this.Controls.Add(this.trigger_cbb);
            this.Name = "TriggerSelector";
            this.Text = "TriggerSelector";
            this.Load += new System.EventHandler(this.TriggerSelector_Load);
            this.Controls.SetChildIndex(this.title_lbl, 0);
            this.Controls.SetChildIndex(this.exit_btn, 0);
            this.Controls.SetChildIndex(this.min_btn, 0);
            this.Controls.SetChildIndex(this.trigger_cbb, 0);
            this.Controls.SetChildIndex(this.trigger_lbl, 0);
            this.Controls.SetChildIndex(this.addTrigger_btn, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox trigger_cbb;
        protected System.Windows.Forms.Label trigger_lbl;
        private System.Windows.Forms.Button addTrigger_btn;
    }
}