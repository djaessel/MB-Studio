namespace MB_Studio
{
    partial class HeaderValueTool
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
            this.shift_txt = new System.Windows.Forms.TextBox();
            this.minHeaderValue_txt = new System.Windows.Forms.TextBox();
            this.maxHeaderValue_txt = new System.Windows.Forms.TextBox();
            this.exampleHeaderCode_txt = new System.Windows.Forms.TextBox();
            this.generate_header_btn = new System.Windows.Forms.Button();
            this.copy_to_clipboard_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // shift_txt
            // 
            this.shift_txt.Location = new System.Drawing.Point(461, 14);
            this.shift_txt.Name = "shift_txt";
            this.shift_txt.Size = new System.Drawing.Size(54, 20);
            this.shift_txt.TabIndex = 10;
            this.shift_txt.Text = "0";
            this.shift_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // minHeaderValue_txt
            // 
            this.minHeaderValue_txt.Location = new System.Drawing.Point(341, 14);
            this.minHeaderValue_txt.Name = "minHeaderValue_txt";
            this.minHeaderValue_txt.Size = new System.Drawing.Size(54, 20);
            this.minHeaderValue_txt.TabIndex = 9;
            this.minHeaderValue_txt.Text = "0";
            this.minHeaderValue_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // maxHeaderValue_txt
            // 
            this.maxHeaderValue_txt.Location = new System.Drawing.Point(401, 14);
            this.maxHeaderValue_txt.Name = "maxHeaderValue_txt";
            this.maxHeaderValue_txt.Size = new System.Drawing.Size(54, 20);
            this.maxHeaderValue_txt.TabIndex = 8;
            this.maxHeaderValue_txt.Text = "255";
            this.maxHeaderValue_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exampleHeaderCode_txt
            // 
            this.exampleHeaderCode_txt.Location = new System.Drawing.Point(124, 14);
            this.exampleHeaderCode_txt.Name = "exampleHeaderCode_txt";
            this.exampleHeaderCode_txt.Size = new System.Drawing.Size(211, 20);
            this.exampleHeaderCode_txt.TabIndex = 7;
            // 
            // generate_header_btn
            // 
            this.generate_header_btn.Location = new System.Drawing.Point(12, 12);
            this.generate_header_btn.Name = "generate_header_btn";
            this.generate_header_btn.Size = new System.Drawing.Size(106, 23);
            this.generate_header_btn.TabIndex = 6;
            this.generate_header_btn.Text = "Generate Header";
            this.generate_header_btn.UseVisualStyleBackColor = true;
            this.generate_header_btn.Click += new System.EventHandler(this.Generate_header_btn_Click);
            // 
            // copy_to_clipboard_btn
            // 
            this.copy_to_clipboard_btn.Enabled = false;
            this.copy_to_clipboard_btn.Location = new System.Drawing.Point(521, 12);
            this.copy_to_clipboard_btn.Name = "copy_to_clipboard_btn";
            this.copy_to_clipboard_btn.Size = new System.Drawing.Size(113, 23);
            this.copy_to_clipboard_btn.TabIndex = 11;
            this.copy_to_clipboard_btn.Text = "Copy to Clipboard";
            this.copy_to_clipboard_btn.UseVisualStyleBackColor = true;
            this.copy_to_clipboard_btn.Click += new System.EventHandler(this.Copy_to_clipboard_btn_Click);
            // 
            // HeaderValueTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 47);
            this.Controls.Add(this.copy_to_clipboard_btn);
            this.Controls.Add(this.shift_txt);
            this.Controls.Add(this.minHeaderValue_txt);
            this.Controls.Add(this.maxHeaderValue_txt);
            this.Controls.Add(this.exampleHeaderCode_txt);
            this.Controls.Add(this.generate_header_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "HeaderValueTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate - Headervalue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox shift_txt;
        private System.Windows.Forms.TextBox minHeaderValue_txt;
        private System.Windows.Forms.TextBox maxHeaderValue_txt;
        private System.Windows.Forms.TextBox exampleHeaderCode_txt;
        private System.Windows.Forms.Button generate_header_btn;
        private System.Windows.Forms.Button copy_to_clipboard_btn;
    }
}