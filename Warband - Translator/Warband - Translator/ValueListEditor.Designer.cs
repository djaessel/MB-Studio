namespace WarbandTranslator
{
    partial class ValueListEditor
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
        private void InitializeComponent()
        {
            this.save_btn = new System.Windows.Forms.Button();
            this.value_txt = new System.Windows.Forms.TextBox();
            this.property_name_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(12, 187);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(508, 23);
            this.save_btn.TabIndex = 0;
            this.save_btn.Text = "OK";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // value_txt
            // 
            this.value_txt.Location = new System.Drawing.Point(12, 36);
            this.value_txt.Multiline = true;
            this.value_txt.Name = "value_txt";
            this.value_txt.Size = new System.Drawing.Size(508, 145);
            this.value_txt.TabIndex = 1;
            // 
            // property_name_lbl
            // 
            this.property_name_lbl.AutoSize = true;
            this.property_name_lbl.Location = new System.Drawing.Point(12, 20);
            this.property_name_lbl.Name = "property_name_lbl";
            this.property_name_lbl.Size = new System.Drawing.Size(52, 13);
            this.property_name_lbl.TabIndex = 2;
            this.property_name_lbl.Text = "Property: ";
            // 
            // ValueListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 222);
            this.Controls.Add(this.property_name_lbl);
            this.Controls.Add(this.value_txt);
            this.Controls.Add(this.save_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ValueListEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Value - Editor";
            this.Load += new System.EventHandler(this.ValueListEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.TextBox value_txt;
        private System.Windows.Forms.Label property_name_lbl;
    }
}