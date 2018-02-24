using System;
using System.Windows.Forms;

namespace WarbandTranslator
{
    public partial class ValueList : UserControl
    {
        private Main mainx;
        private bool languageMode;

        public ValueList(Main mainx, bool languageMode = false, string property = "", string value = "")
        {
            this.mainx = mainx;
            this.languageMode = languageMode;
            InitializeComponent();
            Property = property;
            Value = value;
            value_txt.Click += Value_txt_Click;
            Click += ValueList_Click;
            property_lbl.Click += ValueList_Click;
        }

        private void ValueList_Click(object sender, EventArgs e)
        {
            if (languageMode)
            {
                DialogResult dlr = MessageBox.Show("Do you want to delete this translation?", Application.ProductName + " - DELETE",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (dlr == DialogResult.Yes)
                {
                    mainx.deleteTranslation(Property + Reader.SPLIT1 + Value);
                    //Parent.Controls.Remove(this);
                }
            }
        }

        private void Value_txt_Click(object sender, EventArgs e)
        {
            new ValueListEditor(property_lbl.Text, value_txt.Text.Replace('_', ' '), (TextBox)sender).Show();
        }

        public string BaseString
        {
            get
            {
                return property_lbl.Text + "|" + value_txt.Text.Replace(' ', '_');
            }
        }

        public string Value
        {
            get
            {
                return value_txt.Text;
            }
            set
            {
                value_txt.Text = value;
            }
        }

        public string Property
        {
            get
            {
                return property_lbl.Text;
            }
            set
            {
                property_lbl.Text = value;
            }
        }

    }
}
