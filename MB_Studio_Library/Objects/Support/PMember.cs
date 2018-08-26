using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects.Support
{
    public class PMember
    {
        public const string INVALID_TROOP = "-1";
        public static PMember DEFAULT_MEMBER { get { return new PMember(new string[] { "-1", "0", "0", "0" }); } }

        public PMember(string[] raw_data)
        {
            if (!raw_data[0].Equals(INVALID_TROOP))
                Troop = CodeReader.Troops[int.Parse(raw_data[0])];
            else
                Troop = INVALID_TROOP;
            MinimumTroops = int.Parse(raw_data[1]);
            MaximumTroops = int.Parse(raw_data[2]);
            Flags = int.Parse(raw_data[3]);
        }

        public string Troop { get; }

        public int MinimumTroops { get; }

        public int MaximumTroops { get; }

        public int Flags { get; }

    }
}