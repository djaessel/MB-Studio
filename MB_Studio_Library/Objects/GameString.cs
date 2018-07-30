namespace MB_Studio_Library.Objects
{
    public class GameString : Skriptum
    {
        public GameString(string[] raw_data) : base(raw_data[0], ObjectType.GameString)
        {
            Text = raw_data[1].Replace('_', ' ');
        }

        public string Text { get; }

    }
}