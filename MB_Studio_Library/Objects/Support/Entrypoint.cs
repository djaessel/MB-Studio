using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects.Support
{
    public class Entrypoint
    {
        private readonly string[] raw_data;

        public Entrypoint(string[] raw_data)
        {
            this.raw_data = raw_data;
            SpawnItems = new string[raw_data.Length - 8];
            for (int i = 0; i < SpawnItems.Length; i++)
                SpawnItems[i] = /*'\"' + */CodeReader.Items[int.Parse(raw_data[i + 7])]/* + '\"'*/;
        }

        public int EntryPointNo { get { return int.Parse(raw_data[0]); } }

        public int SpawnFlags { get { return int.Parse(raw_data[1]); } }

        public int AlterFlags { get { return int.Parse(raw_data[2]); } }

        public int AIFlags { get { return int.Parse(raw_data[3]); } }

        public int TroopCount { get { return int.Parse(raw_data[4]); } }

        public string[] SpawnItems { get; }
    }
}
