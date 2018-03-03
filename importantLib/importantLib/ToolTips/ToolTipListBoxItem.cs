/*
 * from https://www.codeproject.com/script/Membership/View.aspx?mid=3015495
*/
namespace importantLib.ToolTipsListBox
{
    public class ToolTipListBoxItem : IToolTipDisplayer
    {
        public string DisplayText { get; private set; }
        public string ToolTipText { get; private set; }

        // Constructor
        public ToolTipListBoxItem(string displayText, string toolTipText)
        {
            DisplayText = displayText;
            ToolTipText = toolTipText;
        }

        // Returns the display text of this item.
        public override string ToString()
        {
            return DisplayText;
        }

        // Returns the tooltip text of this item.
        public string GetToolTipText()
        {
            return ToolTipText;
        }
    }

    /// Interface used by listbox items in ToolTipListBox.
    internal interface IToolTipDisplayer
    {
        string GetToolTipText();
    }
}
