using importantLib;
using MB_Studio_Library.IO;
using System;
using System.IO;
using System.Windows.Forms;

namespace MB_Studio.Support
{
    public partial class HeaderValueTool : Form
    {
        public HeaderValueTool()
        {
            InitializeComponent();
        }

        private void Copy_to_clipboard_btn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(exampleHeaderCode_txt.Text);
        }

        private void Generate_header_btn_Click(object sender, EventArgs e)
        {
            byte shift = byte.Parse(shift_txt.Text);
            int minValue = int.Parse(minHeaderValue_txt.Text);
            int maxValue = int.Parse(maxHeaderValue_txt.Text);
            using (StreamWriter wr = new StreamWriter(CodeReader.FILES_PATH + "header_mb_decompiler.py", true))
            {
                wr.WriteLine();
                for (int i = minValue; i < maxValue + 1; i++)
                    wr.WriteLine(exampleHeaderCode_txt.Text.Replace("VAR_INT", i.ToString()).Replace("VAR_HEX", HexConverter.Dec2Hex(i << shift)).ToLower());
            }
        }
    }
}
