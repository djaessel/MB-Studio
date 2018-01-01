using System.Windows.Forms;
using importantLib;

namespace MB_Studio.Main
{
    public partial class Loader : SplashForm
    {
        public Loader(Form mainForm, bool adjustToParent = true) : base(mainForm, adjustToParent)
        {
            InitializeComponent();
            information_lbl.MouseDown += Control_MoveForm_MouseDown;
        }
    }
}
