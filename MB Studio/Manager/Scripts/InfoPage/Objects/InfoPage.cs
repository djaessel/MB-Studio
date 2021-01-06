namespace MB_Studio_Library.Objects
{
    public class InfoPage : Skriptum
    {
        // UNUSED AT THE MOMENT BECAUSE OBJECTS ALREADY EXIST IN LIBRARY
        // ONLY FOR STRUCTURE AND UNDERSTADNING PURPOSES
        // TODO: load this initially instead of library

        public InfoPage(string[] raw_data) : base(raw_data[0], ObjectType.InfoPage)
        {
            Name = raw_data[1].Replace('_', ' ');
            Text = raw_data[2].Replace('_', ' ');
        }

        public string Text { get; }

        public string Name { get; }

    }
}