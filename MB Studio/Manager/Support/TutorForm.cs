using importantLib;
using System;
using System.Collections.Generic;

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

        protected int MinStep { get; private set; }
        protected int CurStep { get; private set; }
        protected int MaxStep { get; private set; }

        private List<TutorStep> tutors = new List<TutorStep>();

        public TutorForm()
        {
            InitializeComponent();
            Reset();
            info_lbl.TextChanged += Info_lbl_TextChanged;
        }

        private void TutorForm_Load(object sender, EventArgs e)
        {
            UpdateGui();
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
            UpdateGui();
        }

        private void Step_right_btn_Click(object sender, EventArgs e)
        {
            CurStep++;
            UpdateGui();
        }

        private void HandleStepButtons()
        {
            step_left_btn.Enabled = (CurStep > MinStep);
            step_right_btn.Enabled = (CurStep < MaxStep);

            step_lbl.Text = STEP_TEXT + CurStep + ST_OF_TEXT + MaxStep;
        }

        private void UpdateGui()
        {
            int curIdx = CurStep - 1;
            if (curIdx < tutors.Count && curIdx >= 0)
            {
                title_lbl.Text = tutors[curIdx].Heading;
                info_lbl.Text = tutors[curIdx].InfoText;
            }
            else
            {
                title_lbl.ResetText();
                info_lbl.ResetText();
            }
            HandleStepButtons();
        }

        private void InitializeTutorSteps()
        {
            int x = tutors.Count - 1;
            MinStep = tutors.Count - x;
            CurStep = MinStep;
            MaxStep = tutors.Count;

            UpdateGui();
        }

        internal void AddTutorStep(TutorStep tutor, int number = 0)
        {
            number--; // for index
            if (number < tutors.Count && number > 0)
                tutors.Insert(number, tutor);
            else
                tutors.Add(tutor);

            InitializeTutorSteps();
        }

        internal void AddTutorSteps(List<TutorStep> tutors, int startNumber = 0)
        {
            startNumber--; // for index
            if (startNumber < tutors.Count && startNumber > 0)
                this.tutors.InsertRange(startNumber, tutors);
            else
                this.tutors.AddRange(tutors);

            InitializeTutorSteps();
        }

        internal void RemoveTutorStep(TutorStep tutor)
        {
            tutors.Remove(tutor);

            InitializeTutorSteps();
        }

        internal void RemoveTutorStep(int number)
        {
            number--; // for index
            tutors.RemoveAt(number);

            InitializeTutorSteps();
        }

        internal void Reset()
        {
            MaxStep = 1;
            CurStep = MaxStep;
            MinStep = MaxStep;

            tutors.Clear();

            InitializeTutorSteps();
        }
    }
}
