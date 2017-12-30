using System.Collections.Generic;
using static skillhunter.Skriptum;

namespace MB_Decompiler_Library.IO
{
    public class DataBankList
    {
        private string kind;
        private int objectTypeID;

        private List<string> m_imports = new List<string>();
        private List<string> m_description_lines = new List<string>();
        private List<string> m_codes = new List<string>();

        public DataBankList(int objectTypeID, string kind)
        {
            this.objectTypeID = objectTypeID;
            this.kind = kind;
        }

        public void AddImport(string line) { m_imports.Add(line); }

        public void AddDescriptionLine(string line) { m_description_lines.Add(line); }

        public void AddCodeLine(string line) { m_codes.Add(line); }

        public int ObjectTypeID { get { return objectTypeID; } }

        public ObjectType ObjectType { get { return (ObjectType)objectTypeID; } }

        public string Kind { get { return kind; } }

        public string[] Imports { get { return m_imports.ToArray(); } }

        public string[] DescriptionLines { get { return m_description_lines.ToArray(); } }

        public string[] CodeLines { get { return m_codes.ToArray(); } }

    }
}
