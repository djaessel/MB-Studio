using System.IO;
using System.Text;
using System.Windows.Forms;

namespace importantLib
{
    public class ControlWriter : TextWriter
    {
        private Control control;
        private Form parent;
        public ControlWriter(Control control, Form parent)
        {
            this.control = control;
            this.parent = parent;
        }

        public override void Write(char value)
        {
            parent.Invoke((MethodInvoker)delegate { control.Text += value; });
        }

        public override void Write(string value)
        {
            parent.Invoke((MethodInvoker)delegate { control.Text += value; });
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
