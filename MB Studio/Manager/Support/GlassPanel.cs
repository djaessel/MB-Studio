using System.Windows.Forms;

namespace MB_Studio.Manager.Support
{
    internal class GlassPanel : Panel
    {
        public GlassPanel()
        {
            ResizeRedraw = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        public void ForceBackgroundRedraw()
        {
            base.OnPaintBackground(new PaintEventArgs(CreateGraphics(), new System.Drawing.Rectangle(0, 0, Width, Height)));
        }
    }
}
