using System;
using System.Drawing;
using System.Windows.Forms;

namespace importantLib
{
    /// <summary>
    /// Summary description for SplashForm.
    /// </summary>
    public partial class SplashForm : SpecialForm
    {
        // instance member to keep a reference to main form
        private Form MainForm;
        
        public bool AdjustToParent { get; set; }

        // flag to indicate if the form has been closed
        private bool IsClosed = false;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashForm" /> class.
        /// </summary>
        public SplashForm()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashForm" /> class.
        /// </summary>
        public SplashForm(Form mainForm, bool adjustToParent = true) : this()
        {
            // Store the reference to parent form
            MainForm = mainForm;
            // Decide to adjust automatically or change position manually
            AdjustToParent = adjustToParent;

            // Attach to parent form events
            MainForm.Deactivate += new EventHandler(MainForm_Deactivate);
            MainForm.Activated += new EventHandler(MainForm_Activated);
            MainForm.Move += new EventHandler(MainForm_Move);

            // Adjust appearance
            ShowInTaskbar = false; // do not show form in task bar
            TopMost = true; // show splash form on top of main form
            //StartPosition = FormStartPosition.Manual;
            Visible = false;

            // Adjust location
            //if (AdjustToParent)
            //   AdjustLocation();
        }

        #endregion

        #region Methods

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                if (!IsClosed)
                {
                    Visible = false;
                }
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                if (!IsClosed)
                {
                    Visible = true;
                }
            }
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            // Adjust location
            if (AdjustToParent)
                AdjustLocation();
        }

        private void SplashForm_Closed(object sender, EventArgs e)
        {
            IsClosed = true;
        }

        private void AdjustLocation()
        {
            // Adjust the position relative to main form
            int dx = (MainForm.Width - Width) / 2;
            int dy = (MainForm.Height - Height) / 2;
            Point loc = new Point(MainForm.Location.X, MainForm.Location.Y);
            loc.Offset(dx, dy);
            Location = loc;
        }

        #endregion
    }
}