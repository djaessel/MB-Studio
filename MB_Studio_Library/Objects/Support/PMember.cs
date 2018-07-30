using MB_Studio_Library.IO;

namespace MB_Studio_Library.Objects.Support
{
    public class PMember
    {
        private string troop;
        private int flags, minTroops, maxTroops;

        public const string INVALID_TROOP = "-1";
        public static PMember DEFAULT_MEMBER { get { return new PMember(new string[] { "-1", "0", "0", "0" }); } }

        public PMember(string[] raw_data)
        {
            if (!raw_data[0].Equals(INVALID_TROOP))
                troop = CodeReader.Troops[int.Parse(raw_data[0])];
            else
                troop = INVALID_TROOP;
            minTroops = int.Parse(raw_data[1]);
            maxTroops = int.Parse(raw_data[2]);
            flags = int.Parse(raw_data[3]);
        }

        public string Troop { get { return troop; } }

        public int MinimumTroops { get { return minTroops; } }

        public int MaximumTroops { get { return maxTroops; } }

        public int Flags { get { return flags; } }

    }
}