namespace MB_Studio_Library.Objects.Support
{
    public class Variable
    {
        public Variable(string name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public decimal Value { get; }

    }
}
