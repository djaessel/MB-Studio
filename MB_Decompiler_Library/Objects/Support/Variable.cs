namespace MB_Studio_Library.Objects.Support
{
    public class Variable
    {
        private decimal value;
        private string name;

        public Variable(string name, decimal value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name { get { return name; } }

        public decimal Value { get { return value; } }

    }
}
