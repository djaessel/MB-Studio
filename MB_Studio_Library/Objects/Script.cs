namespace MB_Studio_Library.Objects
{
    public class Script : Skriptum
    {
        public Script(string[] raw_data) : base(raw_data[0], ObjectType.Script)
        {
            Code = new string[raw_data.Length - 1];
            for (int i = 0; i < Code.Length; i++)
                Code[i] = raw_data[i + 1];
        }

        public string[] Code { get; }

    }
}
