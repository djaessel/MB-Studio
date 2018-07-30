namespace MB_Studio_Library.Objects.Support
{
    internal class ExchangeObjectBox
    {
        private string name;
        private ExchangeObject[] xChangeObjects;

        public ExchangeObjectBox(string name, string[] objects)
        {
            this.name = name;
            xChangeObjects = ConfigureExchangeObjects(objects);
        }

        private static ExchangeObject[] ConfigureExchangeObjects(string[] objects)
        {
            ExchangeObject[] xobjs = new ExchangeObject[objects.Length];
            for (int i = 0; i < objects.Length; i++)
                xobjs[i] = new ExchangeObject(objects[i]);
            return xobjs;
        }

        public string Name { get { return name; } }

        public ExchangeObject[] ExchangeObjects { get { return xChangeObjects; } }

    }
}
