using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static MB_Studio_Library.Objects.Skriptum;

namespace MB_Studio_Library.IO
{
    public class ImportsManager
    {
        private const byte IMPORT = 0;
        private const byte DESCRIPTION = 1;
        private const byte CODE = 2;
        private readonly string path;

        public ImportsManager(string path = "")
        {
            this.path = path + "imports.wdb";
        }

        public DataBankList[] ReadDataBankInfos()
        {
            List<DataBankList> list = new List<DataBankList>();
            using (StreamReader sr = new StreamReader(path))
            {
                DataBankList dataBankList = null;
                string[] split;
                string s;
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Trim(' ', '\t').Split('#')[0];
                    if (s.Contains("{"))
                    {
                        split = s.Split('{')[0].Split(':');
                        dataBankList = new DataBankList(int.Parse(split[1].Trim()), split[0].Trim());

                        s = sr.ReadLine().Split('#')[0];
                        if (s.Contains("IMPORTS") && s.Contains("["))
                        {
                            ProcessInformation(sr, s, dataBankList, IMPORT);
                            s = sr.ReadLine().Split('#')[0];
                        }
                        if (s.Contains("DESCRIPTION") && s.Contains("["))
                        {
                            ProcessInformation(sr, s, dataBankList, DESCRIPTION);
                            s = sr.ReadLine();
                            if (s.Contains("CODE") && s.Contains("["))
                                ProcessInformation(sr, s, dataBankList, CODE);
                        }

                        while (!s.Contains("}"))
                            s = sr.ReadLine().Split('#')[0].Trim();

                        list.Add(dataBankList);
                    }
                }
            }
            return list.ToArray();
        }

        private static DataBankList ProcessInformation(StreamReader sr, string s, DataBankList dataBankList, byte type = 0) // MAKE SHORTER (SO EVERY LIST CAN BE USED IN HERE WITHOUT ADDING NEW LINES) 
        {
            string[] split;
            s = s.Substring(s.IndexOf('[') + 1);
            //if (containsEndingBracket(s))
            //    s = s.Split(']')[0];
            if (type == IMPORT)
                s = s.Trim(' ', '\t');

            split = s.Split(';');
            for (int i = 0; i < split.Length - 1; i++)
            {
                if (split[i].Length > 5)
                {
                    if (type == IMPORT)
                        dataBankList.AddImport(split[i].Trim(' ', '\t'));
                    else if (type == DESCRIPTION)
                        dataBankList.AddDescriptionLine(split[i]);
                    else if (type == CODE)
                        dataBankList.AddCodeLine(split[i]);
                }
            }
            while (/*!s.Contains("]") || */!ContainsEndingBracket(s))
            {
                s = sr.ReadLine();
                if (type == IMPORT)
                    s = s.Trim(' ', '\t');
                if (type == DESCRIPTION)
                    s = s.Split('#')[0];
                //if (containsEndingBracket(s))
                //    s = s.Split(']')[0];
                if (type == IMPORT)
                    split = s.Trim(' ', '\t').Split(';');
                else
                    split = s.Split(';');
                for (int i = 0; i < split.Length - 1; i++)
                {
                    if (split[i].Length > 5 || (type == CODE && split[i].Length > 1))
                    {
                        if (type == IMPORT)
                            dataBankList.AddImport(split[i].Trim(' ', '\t'));
                        else if (type == DESCRIPTION)
                            dataBankList.AddDescriptionLine(split[i]);
                        else if (type == CODE)
                            dataBankList.AddCodeLine(split[i]);
                    }
                }
            }
            return dataBankList;
        }

        private static bool ContainsEndingBracket(string s)
        {
            bool b = false;
            if (s.Contains("]"))
                b = s.Substring(s.LastIndexOf(']') + 1).Equals(string.Empty);
            return b;
        }

        public void WriteDataBankInfos(ObjectType objType, List<List<string>> infos)
        {
            int idx = -1;
            DataBankList[] dataBank = ReadDataBankInfos();

            for (int i = 0; i < dataBank.Length; i++)
                if (dataBank[i].ObjectType == objType)
                    for (int j = 0; j < infos.Count; j++)
                        idx = i;

            if (idx >= 0)
            {
                DataBankList curDataList = new DataBankList(dataBank[idx].ObjectTypeID, dataBank[idx].Kind);
                for (int j = 0; j < infos.Count; j++)
                    for (int k = 0; k < infos[j].Count; k++) // change later when only one line is needed!!!
                        if (j == 0)
                            curDataList.AddImport(infos[j][k]);
                        else if (j == 1)
                            curDataList.AddDescriptionLine(infos[j][k]);
                        else
                            curDataList.AddCodeLine(infos[j][k]);

                dataBank[idx] = curDataList;

                using (StreamWriter wr = new StreamWriter(path))
                {
                    wr.WriteLine("# !!! 27 Import-Data-Blocks are needed / recommended !!! #" + Environment.NewLine);

                    foreach (DataBankList listItem in dataBank)
                    {
                        wr.WriteLine(listItem.Kind + ':' + listItem.ObjectTypeID + " {");

                        if (listItem.Imports.Length > 0)
                        {
                            wr.WriteLine("  IMPORTS [");
                            for (int i = 0; i < listItem.Imports.Length; i++)
                                wr.WriteLine('\t' + listItem.Imports[i] + ';');
                            wr.WriteLine("\t]");
                        }

                        if (listItem.DescriptionLines.Length > 0)
                        {
                            wr.WriteLine("  DESCRIPTION [");
                            for (int i = 0; i < listItem.DescriptionLines.Length; i++)
                                wr.WriteLine(listItem.DescriptionLines[i] + ';');
                            wr.WriteLine("\t]");
                        }

                        if (listItem.CodeLines.Length > 0)
                        {
                            wr.WriteLine("  CODE [");
                            for (int i = 0; i < listItem.CodeLines.Length; i++)
                                wr.WriteLine(listItem.CodeLines[i] + ';');
                            wr.WriteLine("\t]");
                        }

                        wr.WriteLine("}");
                    }

                    wr.WriteLine("# END OF DATA");
                }
            }
            else
                MessageBox.Show("ERROR: 0x12");
        }

    }
}
