using System.Collections.Generic;
using static MB_Studio_Library.Objects.Skriptum;

namespace MB_Studio_Library.IO
{
    public class DataBankList
    {
        private List<string> m_imports = new List<string>();
        private List<string> m_description_lines = new List<string>();
        private List<string> m_codes = new List<string>();

        public DataBankList(int objectTypeID, string kind)
        {
            ObjectTypeID = objectTypeID;
            Kind = kind;
        }

        public void AddImport(string line) { m_imports.Add(line); }

        public void AddDescriptionLine(string line) { m_description_lines.Add(line); }

        public void AddCodeLine(string line) { m_codes.Add(line); }

        public int ObjectTypeID { get; }

        public ObjectType ObjectType { get { return (ObjectType)ObjectTypeID; } }

        public string Kind { get; }

        public string[] Imports { get { return m_imports.ToArray(); } }

        public string[] DescriptionLines { get { return m_description_lines.ToArray(); } }

        public string[] CodeLines { get { return m_codes.ToArray(); } }

    }
}
