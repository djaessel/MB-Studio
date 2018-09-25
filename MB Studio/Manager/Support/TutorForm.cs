using importantLib;
using System;

namespace MB_Studio.Manager.Support
{
    public partial class TutorForm : SpecialFormBlack
    {
        private const int DEFAULT_HEIGHT = 100;
        private const int DEFAULT_INFO_HEIGHT = 24;
        private const int DEFAULT_INFO_SPACE = 4;

        private const int INFO_TEXT_LENGTH = 32;

        private const string STEP_TEXT = "Step "; // default - later multi lingual?
        private const string ST_OF_TEXT = " of "; // default - later multi lingual?

        protected int MinStep { get; private set; } = 1;
        protected int CurStep { get; private set; } = 1;
        protected int MaxStep { get; private set; } = 1;

        public TutorForm()
        {
            InitializeComponent();

            info_lbl.TextChanged += Info_lbl_TextChanged;
        }

        private void TutorForm_Load(object sender, EventArgs e)
        {
            HandleStepButtons();
        }

        private void Info_lbl_TextChanged(object sender, EventArgs e)
        {
            int multi = info_lbl.Text.Length / INFO_TEXT_LENGTH;
            //if (info_lbl.Text.Length % INFO_TEXT_LENGTH != 0)
            //    multi++;
            Height = DEFAULT_HEIGHT + multi * (DEFAULT_INFO_HEIGHT + DEFAULT_INFO_SPACE);
        }

        private void Step_left_btn_Click(object sender, EventArgs e)
        {
            CurStep--;
            HandleStepButtons();
        }

        private void Step_right_btn_Click(object sender, EventArgs e)
        {
            CurStep++;
            HandleStepButtons();
        }

        private void HandleStepButtons()
        {
            step_left_btn.Enabled = (CurStep > MinStep);
            step_right_btn.Enabled = (CurStep < MaxStep);

            step_lbl.Text = STEP_TEXT + CurStep + ST_OF_TEXT + MaxStep;
        }
    }
}
