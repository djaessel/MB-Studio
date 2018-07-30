namespace MB_Studio_Library.Objects.Support
{
    public class HeaderVariable
    {
        public HeaderVariable(string value, string name)
        {
            VariableName = name;
            VariableValue = value;
        }

        public string VariableName { get; }

        public string VariableValue { get; }

    }
}
