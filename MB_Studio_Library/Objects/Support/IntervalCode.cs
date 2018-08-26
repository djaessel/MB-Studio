namespace MB_Studio_Library.Objects.Support
{
    public class IntervalCode
    {
        public IntervalCode(string code, int value)
        {
            Code = code;
            Value = value;
        }

        public string Code { get; }

        public int Value { get; }

    }
}
