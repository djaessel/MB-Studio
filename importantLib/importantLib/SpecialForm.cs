using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace importantLib
{
    public partial class SpecialForm : Form
    {
        protected const int WM_NCLBUTTONDOWN = 0xA1;
        protected const int HT_CAPTION = 0x2;

        internal class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool ReleaseCapture();
        }

        public SpecialForm()
        {

        }

        /// <summary>
        /// Gets the screen by index and sets the application to its values
        /// </summary>
        /// <param name="index"></param>
        protected virtual void SetFullScreenByIndex(int index)
        {
            if (index < Screen.AllScreens.Length)
                SetFullScreenByScreen(Screen.AllScreens[index]);
        }

        /// <summary>
        /// Defaul screen (screen == null) is the current primary screen
        /// </summary>
        /// <param name="screen"></param>
        protected virtual void SetFullScreenByScreen(Screen screen = null)
        {
            if (screen == null)
                screen = Screen.PrimaryScreen;
            Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);
            DesktopLocation = new Point(0, 0);
        }

        /// <summary>
        /// Gets the screen of the handle and sets the application to its values
        /// </summary>
        /// <param name="hWnd"></param>
        protected virtual void SetFullScreenByHandle(IntPtr hWnd = default(IntPtr))
        {
            Screen screen = null;
            if (!hWnd.Equals(default(IntPtr)))
                try { screen = Screen.FromHandle(hWnd); } catch (Exception) { }
            else
                screen = Screen.FromHandle(Handle);
            SetFullScreenByScreen(screen);
        }

        protected void Control_MoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

        protected bool IsNumeric(string s)
        {
            return ImportantMethods.IsNumericFKZ2(s);
        }

        protected bool IsNumeric(string s, bool all)
        {
            return ImportantMethods.IsNumeric(s, all);
        }

        protected bool IsNumericComma(string s)
        {
            return ImportantMethods.IsNumericFKZ(s);
        }

        protected string GetNameEndOfControl(Control c)
        {
            return c.Name.Substring(c.Name.LastIndexOf('_') + 1);
        }

        protected void InitializeComponent()
        {
            SuspendLayout();
            // 
            // SpecialForm
            // 
            ClientSize = new System.Drawing.Size(284, 261);
            Name = "SpecialForm";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
        }

        protected bool ComboBoxContainsInLines(ComboBox comboBox, string searchText, char seperator = '\0', int idx = 0)
        {
            bool b = false;
            bool notDefaultSep = (seperator != '\0');
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                string tmp = comboBox.Items[i].ToString();
                if (notDefaultSep)
                    tmp = tmp.Split(seperator)[idx].Trim();
                if (tmp.Equals(searchText))
                {
                    b = !b;//true
                    i = comboBox.Items.Count;
                }
            }
            return b;
        }
    }
}
