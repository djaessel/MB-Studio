namespace MB_Studio_Library.Objects.Support
{
    public class IntervalCode
    {
        private string code;
        private int value;

        public IntervalCode(string code, int value)
        {
            this.code = code;
            this.value = value;
        }

        public string Code { get { return code; } }

        public int Value { get { return value; } }

    }
}
