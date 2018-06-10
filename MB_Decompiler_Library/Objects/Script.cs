namespace MB_Decompiler_Library.Objects
{
    public class Script : Skriptum
    {
        public Script(string[] raw_data) : base(raw_data[0], (int)ObjectType.SCRIPT) // base(name, type)
        {
            Code = new string[raw_data.Length - 1];
            for (int i = 0; i < Code.Length; i++)
                Code[i] = raw_data[i + 1];
        }

        public string[] Code { get; }

    }
}
