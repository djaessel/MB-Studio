using System;
using System.Windows.Forms;

namespace WarbandTranslator
{
    public partial class ValueListEditor : Form
    {
        private string name;
        private string value;
        private TextBox senderTXT;

        public ValueListEditor(string name, string value, TextBox senderTXT)
        {
            InitializeComponent();
            this.name = name;
            this.value = value;
            this.senderTXT = senderTXT;
        }

        private void ValueListEditor_Load(object sender, EventArgs e)
        {
            property_name_lbl.Text += name;
            value_txt.Text = value;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            senderTXT.Text = value_txt.Text;
            Close();
        }
    }
}
