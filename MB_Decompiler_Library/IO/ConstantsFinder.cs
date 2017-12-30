using importantLib;
using skillhunter;
using System.Collections.Generic;
using System.IO;

namespace MB_Decompiler_Library.Objects.Support
{
    public class ConstantsFinder
    {
        private static Variable[] constants;

        public static void InitializeConstants(string path)
        {
            int deli;
            string line;
            string[] split;
            List<Variable> constantsX = new List<Variable>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split('#')[0];
                    if (line.Contains("="))
                    {
                        split = line.Split('=');
                        split[0] = split[0].Trim();
                        split[1] = split[1].Trim().ToLower();
                        if (ImportantMethods.IsNumericFKZ2(split[1]) || split[1].Contains("0x"))
                        {
                            decimal d;
                            if (ImportantMethods.IsNumericFKZ2(split[1]))
                            {
                                if (split[1].IndexOf('.') == 0)
                                    split[1] = "0" + split[1];
                                d = decimal.Parse(split[1]);
                            }
                            else
                                d = decimal.Parse(SkillHunter.Hex2Dec(split[1].Substring(2)).ToString());
                            deli = -1;
                            for (int i = 0; i < constantsX.Count; i++)
                            {
                                if (constantsX[i].Name.Equals(split[0]))
                                {
                                    deli = i;
                                    i = constantsX.Count;
                                }
                            }
                            if (deli >= 0)
                                constantsX.RemoveAt(deli);
                            constantsX.Add(new Variable(split[0], d));
                        }
                    }
                }
            }
            constants = constantsX.ToArray();
            if (File.Exists("test.txt"))
                File.Delete("test.txt");
        }

        public static string FindConst(string codeLine, ulong code) // jetzt muss noch mit der code Zahl die entsprechende Variabble / Konstante ausgefiltert werden und zudem noch ob es sich um einen false-positive handelt!
        {
            string test = FindSlot(codeLine, code);
            return test;
        }

        private static string FindSlot(string codeLine, ulong code)
        {
            string[] sp, sp2;
            List<string> names = new List<string>();

            sp = codeLine.Split(',');
            codeLine = sp[0];
            sp2 = codeLine.Split('_');

            /*if (!codeLine.Contains("set_slot") && !codeLine.Contains("get_slot"))
            {
                for (int i = 0; i < constants.Length; i++)
                {
                    sp = constants[i].Name.Split('_');
                    if (constants[i].Value == code)
                    {
                        for (int v = 0; v < sp2.Length; v++)
                        {
                            for (int j = 0; j < sp.Length; j++)
                            {
                                if (sp[j].Equals(sp2[v]) && !names.Contains(constants[i].Name))
                                {
                                    names.Add(constants[i].Name);
                                    j = sp.Length;
                                }
                            }
                        }
                    }
                }
                if (names.Count > 0)
                {
                    int x = names.Count;
                    string tmp = codeLine.TrimStart(' ', '(').Split('_')[0];
                    for (int i = 0; i < x + 1; i++)
                    {
                        for (int j = names.Count - 1; j >= 0; j--)
                        {
                            if (names[j].Contains("slot") && !names[j].Contains(tmp))
                            {
                                //Console.WriteLine(tmp + " - " + names[j]);
                                names.RemoveAt(j);
                                j = 0;
                            }
                        }
                    }
                    /*wr.Write(codeLine.TrimStart().Substring(1) + ':' + names.Count + " { ");
                    for (int i = 0; i < names.Count; i++)
                    {
                        wr.Write(names[i]);
                        if (i < names.Count - 1)
                            wr.Write(", ");
                    }
                    wr.WriteLine(" } #" + code);*/
            //}
            //}
            if (codeLine.Contains("set_slot") || codeLine.Contains("get_slot")) //else
            {
                for (int i = 0; i < constants.Length; i++)
                    if (constants[i].Value == code && !names.Contains(constants[i].Name))
                        names.Add(constants[i].Name);
                List<string> removeables = new List<string>();
                sp2 = codeLine.Split('_');
                sp2[0] = sp2[0].TrimStart(' ', '(');
                if (sp.Length == 4 && codeLine.Contains("get_slot") || sp.Length == 3 && codeLine.Contains("set_slot"))
                {
                    foreach (string name in names)
                    {
                        sp = name.Split('_');
                        if (sp.Length > 2)
                        {
                            if (!(sp[0].Equals("slot") && sp[1].Equals(sp2[0])))
                                removeables.Add(name);
                        }
                        else
                            removeables.Add(name);
                    }
                    foreach (string item in removeables)
                        names.Remove(item);
                    //Console.Write(codeLine + ':' + code + " --> (");
                    //foreach (string name in names)
                    //    Console.Write(name + ';');
                    //Console.WriteLine(")");
                }
            }
            if (names.Count == 1) // RETHINK !!!
                codeLine = names[0];
            else
                codeLine = code.ToString();
            return codeLine;
        }

    }
}
