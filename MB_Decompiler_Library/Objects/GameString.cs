using skillhunter;

namespace MB_Decompiler_Library.Objects
{
    public class GameString : Skriptum
    {
        private string text;

        public GameString(string[] raw_data) : base(raw_data[0], ObjectType.GAME_STRING)
        {
            text = raw_data[1].Replace('_', ' ');
        }

        public string Text { get { return text; } }

    }
}