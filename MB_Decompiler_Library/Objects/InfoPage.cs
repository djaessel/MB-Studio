using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class InfoPage : Skriptum
    {
        private string text, infoPageName;

        public InfoPage(string[] raw_data) : base(raw_data[0].Substring(3), ObjectType.INFO_PAGE)
        {
            infoPageName = raw_data[1].Replace('_', ' ');
            text = raw_data[2].Replace('_', ' ');
        }

        public string Text { get { return text; } }

        public string InfoPageName { get { return infoPageName; } }

    }
}