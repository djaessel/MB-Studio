using System;
using System.Windows.Forms;

namespace MB_Studio
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MB_Studio());
        }
    }
}
