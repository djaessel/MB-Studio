using System;
using System.Windows.Forms;

namespace MB_Decompiler_GUI
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DecompilerGUI());

            #region OLD CODE
            /*Form f = null;
            if (args.Length == 0)
            {
                f = new Menu();
                Application.Run(f);
                if (!Menu.Exit)
                {
                    if (Menu.MB_Studio)
                        f = new MB_Studio(Menu.StudioMode);
                    else
                        f = new DecompilerGUI();
                }
            }
            else if (args.Length > 0)
            {
                if (args[0].Equals("decompiler"))
                    f = new DecompilerGUI();
                else if (args[0].Equals("mb_studio") && args.Length == 2)
                    f = new MB_Studio((MB_Studio.Mode)Convert.ToInt32(args[1]));
            }
            try
            {
                if (f != null)
                    if (!f.IsDisposed)
                        Application.Run(f);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
            #endregion
        }
    }
}
