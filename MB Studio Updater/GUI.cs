using importantLib;
using System;
using System.Threading;
using System.Windows.Forms;

namespace MB_Studio_Updater
{
    public partial class GUI : SpecialFormBlack
    {
        MBStudioUpdater updater;

        public GUI(MBStudioUpdater updater = null)
        {
            if (updater == null)
                updater = new MBStudioUpdater();

            this.updater = updater;

            Shown += GUI_Shown;

            InitializeComponent();
        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(RunUpdater)) {
                IsBackground = true
            };
            t.Start();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            // Load data here
        }

        private void RunUpdater()
        {
            try
            {
                updater.SetGuiConsole(console_richtxt);

                if (updater.SelfUpdateActive)
                    updater.SelfUpdate();
                else if (updater.WriteIndexActive)
                    updater.WriteIndexFile();
                else
                    updater.CheckForUpdates();

                /*MessageBox.Show("MODE: " +
                    updater.SelfUpdateActive + " | " +
                    updater.WriteIndexActive + " | " +
                    updater.UseGUI + " | " +
                    updater.StartStudioOnExit + " | " +
                    updater.Is64BitBinary + " | " +
                    updater.AddNewFiles
                );*/

                Thread.Sleep(2000);// make optional or depending on lines shown later

                Application.Exit();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
    }
}
