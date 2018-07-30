using importantLib;

namespace MB_Studio_Library.Objects.Support
{
    internal class ExchangeObject
    {
        private bool changeAble = false;
        private bool hasCounter = false;
        private bool optional = false;

        private string text;

        public ExchangeObject(string text)
        {
            if (!ImportantMethods.IsNumeric(text, true))
            {
                if (text.Substring(0, 1).Equals("["))
                {
                    optional = !optional;
                    text = text.Substring(1, text.Length - 2);
                }
                if (!ImportantMethods.IsNumeric(text, true))
                {
                    changeAble = !changeAble;
                    if (text.Contains("[X]"))
                    {
                        if (text.Substring(text.IndexOf("[X]")).Equals("[X]"))
                        {
                            hasCounter = !hasCounter;
                            text = text.Replace("[X]", string.Empty);
                        }
                    }
                }
            }
            this.text = text;
        }

        public bool IsChangeable { get { return changeAble; } }

        public bool HasCounter { get { return hasCounter; } }

        public bool IsOptional { get { return optional; } }

        public string Text { get { return text; } }

    }
}
