using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MB_Studio_Updater
{
    public class ControlWriter : TextWriter
    {
        private Form parent;
        private RichTextBox control;

        public ControlWriter(RichTextBox control, Form parent)
        {
            this.control = control;
            this.parent = parent;
        }

        public override void Write(char value)
        {
            parent.Invoke((MethodInvoker)delegate { control.AppendText(value.ToString()); });
        }

        public override void Write(string value)
        {
            parent.Invoke((MethodInvoker)delegate { control.AppendText(value); });
        }

        public override void WriteLine()
        {
            parent.Invoke((MethodInvoker)delegate { control.AppendText(NewLine); });
        }

        public override void WriteLine(char value)
        {
            parent.Invoke((MethodInvoker)delegate { control.AppendText(value + NewLine); });
        }

        public override void WriteLine(string value)
        {
            parent.Invoke((MethodInvoker)delegate { control.AppendText(value + NewLine); });
        }

        public override Encoding Encoding { get { return Encoding.Unicode; } }
    }
}
