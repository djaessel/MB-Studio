using importantLib;
using System;
using System.Windows.Forms;
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

        private Form parentForm;

        private List<TutorStep> tutorSteps = new List<TutorStep>();

        public TutorForm(Form parentForm)
        {
            this.parentForm = parentForm;

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

        private Control GetStepControl(TutorStep step)
        {
            Control[] controls = parentForm.Controls.Find(step.ControlName, true);
            bool foundIt = (controls.Length >= 1);// only == 1 should be correct - investigate later
            if (foundIt)
            {
                //if (controls.Length > 1)
                //    Console.WriteLine("Found multiple " + step.ControlName + "!");
                //else
                Console.WriteLine("Found " + step.ControlName + "!");

                return controls[0];
            }
            else
                Console.WriteLine(step.ControlName + " not found!");

            return null;
        }

        private void AddStepControlEvents(TutorStep step)
        {
            Control c = GetStepControl(step);
            if (c != null)
            {
                if (step.Options == TutorStep.Option.None) return;

                if ((step.Options & TutorStep.Option.Click) == TutorStep.Option.Click)
                    c.Click += C_Click;
                if ((step.Options & TutorStep.Option.Hover) == TutorStep.Option.Hover)
                    c.MouseHover += C_MouseHover;
                if ((step.Options & TutorStep.Option.Enter) == TutorStep.Option.Enter)
                    c.Enter += C_Enter;
                if ((step.Options & TutorStep.Option.Leave) == TutorStep.Option.Leave)
                    c.Leave += C_Leave;
                if ((step.Options & TutorStep.Option.Input) == TutorStep.Option.Input)
                    c.TextChanged += C_TextChanged;
            }
        }

        private void RemoveStepControlEvents(TutorStep step)
        {
            Control c = GetStepControl(step);
            if (c != null)
            {
                if (step.Options == TutorStep.Option.None) return;

                if ((step.Options & TutorStep.Option.Click) == TutorStep.Option.Click)
                    c.Click -= C_Click;
                if ((step.Options & TutorStep.Option.Hover) == TutorStep.Option.Hover)
                    c.MouseHover -= C_MouseHover;
                if ((step.Options & TutorStep.Option.Enter) == TutorStep.Option.Enter)
                    c.Enter -= C_Enter;
                if ((step.Options & TutorStep.Option.Leave) == TutorStep.Option.Leave)
                    c.Leave -= C_Leave;
                if ((step.Options & TutorStep.Option.Input) == TutorStep.Option.Input)
                    c.TextChanged -= C_TextChanged;
            }
        }

        private void C_TextChanged(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            c.TextChanged -= C_TextChanged;
            MessageBox.Show("You changed " + tutorSteps[CurStep - 1].ControlName + "s text!");
        }

        private void C_Leave(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            c.Leave -= C_Leave;
            MessageBox.Show("You left " + tutorSteps[CurStep - 1].ControlName + "!");
        }

        private void C_Enter(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            c.Enter -= C_Enter;
            MessageBox.Show("You entered " + tutorSteps[CurStep - 1].ControlName + "!");
        }

        private void C_MouseHover(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            c.MouseHover -= C_MouseHover;
            MessageBox.Show("You hovered over " + tutorSteps[CurStep - 1].ControlName + "!");
        }

        private void C_Click(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            c.Click -= C_Click;
            MessageBox.Show("You clicked " + tutorSteps[CurStep - 1].ControlName + "!");
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
            if (curIdx < tutorSteps.Count && curIdx >= 0)
            {
                title_lbl.Text = tutorSteps[curIdx].Heading;
                info_lbl.Text = tutorSteps[curIdx].InfoText;
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
            int x = tutorSteps.Count - 1;
            MinStep = tutorSteps.Count - x;
            CurStep = MinStep;
            MaxStep = tutorSteps.Count;

            UpdateGui();
        }

        internal void AddTutorStep(TutorStep step, int number = 0)
        {
            number--; // for index
            AddStepControlEvents(step);
            if (number < tutorSteps.Count && number > 0)
                tutorSteps.Insert(number, step);
            else
                tutorSteps.Add(step);
            InitializeTutorSteps();
        }

        internal void AddTutorSteps(List<TutorStep> steps, int startNumber = 0)
        {
            startNumber--; // for index
            for (int i = 0; i < steps.Count; i++)
                AddStepControlEvents(steps[i]);
            if (startNumber < steps.Count && startNumber > 0)
                tutorSteps.InsertRange(startNumber, steps);
            else
                tutorSteps.AddRange(steps);
            InitializeTutorSteps();
        }

        internal void RemoveTutorStep(TutorStep step)
        {
            RemoveStepControlEvents(step);
            tutorSteps.Remove(step);
            InitializeTutorSteps();
        }

        internal void RemoveTutorStep(int number)
        {
            number--; // for index
            RemoveStepControlEvents(tutorSteps[number]);
            tutorSteps.RemoveAt(number);
            InitializeTutorSteps();
        }

        internal void Reset()
        {
            MaxStep = 1;
            CurStep = MaxStep;
            MinStep = MaxStep;

            foreach (TutorStep step in tutorSteps)
                RemoveStepControlEvents(step);
            tutorSteps.Clear();

            InitializeTutorSteps();
        }
    }
}
