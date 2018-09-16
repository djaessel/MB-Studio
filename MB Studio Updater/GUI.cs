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

            InitializeComponent();
        }

        private void Console_richtxt_TextChanged(object sender, EventArgs e)
        {
            progressInfo_lbl.Text = console_richtxt.Lines[console_richtxt.Lines.LongLength];
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
            Shown += GUI_Shown;

            console_richtxt.TextChanged += Console_richtxt_TextChanged;
        }

        private void RunUpdater()
        {
            try
            {
                updater.SetGuiConsole(console_richtxt);
                updater.SetInfoControl(progressInfo_lbl);
                updater.SetProgressBar(update_pb);

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

        private void Show_hide_details_btn_Click(object sender, EventArgs e)
        {
            bool show = show_hide_details_btn.Text.Equals("Show details");
            if (show)
                show_hide_details_btn.Text = "Hide details";
            else
                show_hide_details_btn.Text = "Show details";
            console_richtxt.Visible = show;
        }
    }
}
