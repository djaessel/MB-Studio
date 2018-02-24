using System;
using System.Windows.Forms;

namespace importantLib
{
    public class SearchTextBox : TextBox
    {
        private const string SEARCH = "Search ...";
        private bool isForced = false;

        public SearchTextBox() : base()
        {
            Text = SEARCH;
            TextChanged += SearchTextBox_TextChanged;
            KeyDown += SearchTextBox_KeyDown;
            MouseClick += SearchTextBox_MouseClick;
            LostFocus += SearchTextBox_LostFocus;
        }

        private void SearchTextBox_LostFocus(object sender, EventArgs e)
        {
            if (Text.Length == 0)
                SetSearchText();
        }

        private void SearchTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (Text.Contains(SEARCH))
                Clear();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && Text.Length <= 1)
                SetSearchText();
            RemoveSearchPlaceHolder();
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            RemoveSearchPlaceHolder(true);
        }

        protected void RemoveSearchPlaceHolder(bool textChangedEvent = false)
        {
            if (!isForced)
            {
                if (Text.Contains(SEARCH) && textChangedEvent)
                    Text = Text.Replace(SEARCH, string.Empty);
                if (Text.Length >= 2)
                    if (Text.Contains(SEARCH))
                        Text = Text.Replace(SEARCH, string.Empty);
            }
        }

        private void SetSearchText()
        {
            isForced = true;
            Text = SEARCH;
            isForced = false;
        }
    }
}
