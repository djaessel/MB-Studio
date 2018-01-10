using MB_Decompiler_Library.IO;
using System.Collections.Generic;
using System.IO;

namespace MB_Decompiler_Library.Objects.Support
{
    internal class LocalVariableInterpreter
    {
        private const string DEFAULT_NAME = ":var";
        private static List<ExchangeObjectBox> xChangeObjectsBox = new List<ExchangeObjectBox>();

        private List<LocalVariable> variables = new List<LocalVariable>();

        public LocalVariableInterpreter()
        {
            LocalVariable.ResetLists();
            if (xChangeObjectsBox.Count == 0)
                LoadExchangeObjects();
        }

        private static void LoadExchangeObjects()
        {
            string tmp;
            string[] split;
            using (StreamReader sr = new StreamReader(CodeReader.FILES_PATH + "exchange_objects.py"))
            {
                while (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine().Split('#')[0].Replace(" ", string.Empty);
                    if (tmp.Length > 0)
                    {
                        split = tmp.Split('>');
                        xChangeObjectsBox.Add(new ExchangeObjectBox(split[0], split[1].Split(',')));
                    }
                }
            }
            //printCreationCode();
        }

        /*private static void printCreationCode()
        {
            Console.WriteLine("new LocalVariableInterpreter() {" + Environment.NewLine);
            foreach (ExchangeObjectBox item in xChangeObjectsBox)
            {
                Console.WriteLine('\t' + item.Name + ':');
                foreach (ExchangeObject xobj in item.ExchangeObjects)
                    Console.WriteLine("\t\t> " + xobj.Text + "; IsChangeable = " + xobj.IsChangeable + "; HasCounter = " + xobj.HasCounter + "; IsOptional = " + xobj.IsOptional);
            }
            Console.WriteLine(Environment.NewLine + '}');
        }*/

        public string Interpret(string codeLine, ulong parameter, int codeIndex)
        {
            string[] spl;
            string parameterText = string.Empty;
            int count = (int)(parameter - CodeReader.LOCAL_MIN + 1);

            if (!LocalVariable.CurrentCounts.Contains(count) && !LocalVariable.DefaultVariables.Contains(DEFAULT_NAME + count))
            {
                spl = codeLine.Replace(" ", string.Empty).Split(',');
                codeLine = codeLine.Split(',')[0].Substring(codeLine.IndexOf('(') + 1);
                foreach (ExchangeObjectBox objBox in xChangeObjectsBox)
                {
                    if (objBox.Name.Equals(codeLine))
                    {
                        ExchangeObject xobj = objBox.ExchangeObjects[spl.Length - 2];
                        if (xobj.IsChangeable)
                        {
                            variables.Add(new LocalVariable(':' + xobj.Text, count, codeIndex));
                            parameterText = variables[variables.Count - 1].LocalName;
                        }
                    }
                }
                if (parameterText.Equals(string.Empty))
                {
                    parameterText = DEFAULT_NAME + count;
                    LocalVariable.AddDefaultVariable(parameterText);
                }
            }
            else if (LocalVariable.DefaultVariables.Contains(DEFAULT_NAME + count))
                parameterText = DEFAULT_NAME + count;
            else
                parameterText = GetLVariableLocalName(count, codeIndex);

            //parameterText += " - [" + parameter + "|" + count + "]";
            return parameterText;
        }

        public ulong InterpretBack(string parameterText)
        {
            ulong parameter = ulong.MaxValue;

            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].LocalName.Equals(parameterText))
                {
                    parameter = (ulong)variables[i].InternCount + CodeReader.LOCAL_MIN;
                    i = variables.Count;
                }
            }

            if (parameter == ulong.MaxValue)
            {
                variables.Add(new LocalVariable(parameterText, variables.Count));
                parameter = (ulong)variables.Count + CodeReader.LOCAL_MIN - 1;
            }

            //if (byte.TryParse(parameterText.Substring(DEFAULT_NAME.Length), out byte value))
            //    parameter = value + CodeReader.LOCAL_MIN - 1;

            return parameter;
        }

        private string GetLVariableLocalName(int idx, int codeIndex)
        {
            string localName = DEFAULT_NAME + idx;
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].InternCount == idx)
                {
                    variables[i].AddCodeIndex(codeIndex);
                    localName = variables[i].LocalName;
                    i = variables.Count;
                }
            }
            return localName;
        }

        /*public List<LocalVariable> getUnusedLocalVariables() // use this in decompile to replace the name of unused variables to ":unused"
        {
            List<LocalVariable> unusedVariables = new List<LocalVariable>();
            foreach (LocalVariable _var in variables)
                if (_var.CodeLineIndicies.Length == 1)
                    unusedVariables.Add(_var);
            return unusedVariables;
        }

        public List<LocalVariable> getPossibleUsedLocalVariables()
        {
            List<LocalVariable> possibleUsedVariables = new List<LocalVariable>();
            foreach (LocalVariable _var in variables)
                if (_var.CodeLineIndicies.Length > 1)
                    possibleUsedVariables.Add(_var);
            return possibleUsedVariables;
        }*/

    }
}
