using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace MB_Studio.Manager
{
    internal class ScriptCommander // Is used for Custom Managers (Scripting)
    {
        private readonly List<ToolForm> customManagers = new List<ToolForm>();

        public ScriptCommander()
        {
            Console.Write(Environment.NewLine + "Initializing ScriptCommander...");

            Console.WriteLine("done.");
        }

        public void LoadManagers()
        {
            string scriptPath = Path.GetFullPath(".\\Scripts");

            if (!Directory.Exists(scriptPath)) return;

            foreach (string dir in Directory.GetDirectories(scriptPath))
            {
                string configFile = dir + "\\" + dir.Substring(dir.LastIndexOf('\\') + 1) + ".xml";
                if (File.Exists(configFile))
                {
                    string managerName = Path.GetFileName(configFile);
                    //managerName = managerName.Remove(managerName.IndexOf('.'));

                    Console.WriteLine(Environment.NewLine + "- - - - - - - - ");
                    Console.WriteLine(managerName);

                    XmlReaderSettings settings = new XmlReaderSettings
                    {
                        Async = false,
                        CheckCharacters = true,
                        CloseInput = true,
                        ConformanceLevel = ConformanceLevel.Auto,
                        IgnoreComments = true,
                        IgnoreProcessingInstructions = true,
                        IgnoreWhitespace = true,
                        MaxCharactersInDocument = 0,
                        //MaxCharactersFromEntities = 0,
                        //ValidationType = ValidationType.DTD,
                        //ValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes,
                    };

                    using (XmlReader xmlReader = XmlReader.Create(File.OpenRead(configFile), settings))
                    {
                        while (xmlReader.Read())
                        {
                            string name = string.Empty;
                            string attributes = string.Empty;
                            string properties = string.Empty;
                            string scripts = string.Empty;

                            bool correctElement = false;

                            xmlReader.ReadStartElement("CustomManager");

                            do
                            {
                                correctElement = xmlReader.Name.Equals("Name");

                                if (!correctElement) continue;

                                name = xmlReader.ReadInnerXml();
                            } while (xmlReader.MoveToElement() && !correctElement);

                            do
                            {
                                correctElement = xmlReader.Name.Equals("Attributes");

                                if (!correctElement) continue;

                                if (xmlReader.HasAttributes)
                                {
                                    int i = -1;
                                    while (xmlReader.MoveToNextAttribute())
                                    {
                                        i++;
                                        attributes += xmlReader.GetAttribute(i) + ';';
                                    }

                                    xmlReader.ReadStartElement();
                                }
                            } while (xmlReader.MoveToElement() && !correctElement);

                            do
                            {
                                correctElement = xmlReader.Name.Equals("Properties");

                                if (!correctElement) continue;

                                if (xmlReader.HasAttributes)
                                {
                                    int i = -1;
                                    while (xmlReader.MoveToNextAttribute())
                                    {
                                        i++;
                                        properties += xmlReader.GetAttribute(i) + ';';
                                    }

                                    xmlReader.ReadStartElement();
                                }
                            } while (xmlReader.MoveToElement() && !correctElement);

                            do
                            {
                                correctElement = xmlReader.Name.Equals("Scripts");

                                if (!correctElement || xmlReader.IsEmptyElement) continue;

                                while (xmlReader.Read())
                                {
                                    if (xmlReader.Name.Equals("Scripts")) break;

                                    if (!xmlReader.HasAttributes) continue;

                                    for (int i = 0; i < xmlReader.AttributeCount; i++)
                                    {
                                        xmlReader.MoveToNextAttribute();
                                        scripts += xmlReader.GetAttribute(i);
                                        if (i < xmlReader.AttributeCount - 1)
                                            scripts += ',';
                                    }

                                    scripts += ';';
                                }

                                xmlReader.ReadEndElement();

                            } while (xmlReader.MoveToElement() && !correctElement);

                            Console.WriteLine("Name: " + name);
                            Console.WriteLine("Attributes: " + attributes);
                            Console.WriteLine("Properties: " + properties);
                            Console.WriteLine("Scripts: " + scripts);
                            Console.WriteLine("- - - - - - - - ");

                            TestCoder(name, new List<string>(scripts.Trim('\t', ' ', ';').Split(';')));
                        }
                    }
                }
            }
        }

        private static void TestCoder(string className, List<string> regionsRaw)
        {
            //MethodInfo function = CreateFunction("x + 2 * y");
            //var betterFunction = (Func<double, double, double>)Delegate.CreateDelegate(typeof(Func<double, double, double>), function);

            string methodIdentifier = "protected override ";
            string constructorIdentifier = "public " + className + "Manager()";
            string basePath = Path.GetFullPath(@".\Scripts") + '\\' + className;
            List<List<string>> functions = new List<List<string>>();

            foreach (string regionRaw in regionsRaw)
            {
                string[] sp = regionRaw.Split(',');
                string codeFile = basePath + sp[1];
                string[] codeLines = File.ReadAllLines(codeFile);
                List<string> curList = null;
                bool addToFunction = false;
                for (int i = 0; i < codeLines.Length; i++)
                {
                    bool isMethod = codeLines[i].StartsWith(methodIdentifier);
                    if (isMethod || codeLines[i].StartsWith(constructorIdentifier))
                    {
                        curList = new List<string>
                        {
                            codeLines[i].Trim().Split()[((isMethod) ? 3 : 1)].Split('(')[0].Replace(" ", string.Empty)
                        };
                    }

                    if (codeLines[i].Contains("{"))
                        addToFunction = true;

                    if (addToFunction && curList != null)
                    {
                        curList.Add(codeLines[i].Trim());
                    }

                    if (codeLines[i].Contains("}"))
                    {
                        addToFunction = false;
                        functions.Add(curList);
                        curList = null;
                    }
                }
            }

            ToolForm customManager = CreateCustomManager(className, functions);
            foreach (System.Windows.Forms.Control c in customManager.Controls)
                Console.WriteLine(c.Name + " : " + c.GetType().Name);
        }

        private static ToolForm CreateCustomManager(string className, List<List<string>> functions)
        {
            string newClassName = className + "Manager";
            string scriptsFolder = Path.GetFullPath(@".\Scripts");
            string managerTemplate = scriptsFolder + @"\Template\CustomManagerTemplate.cs";
            string managerTemplateCode = File.ReadAllText(managerTemplate).Replace("MyClass", className);

            foreach (List<string> function in functions)
            {
                string name = function[0];
                string functionCode = string.Empty;

                for (int i = 1; i < function.Count; i++)
                    functionCode += function[i];

                functionCode = functionCode.TrimStart('{').TrimEnd('}').Trim();

                managerTemplateCode = managerTemplateCode.Replace("/SCRIPT " + name, functionCode);
            }

            File.WriteAllText(scriptsFolder + '\\' + newClassName + ".cs", managerTemplateCode);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(), managerTemplateCode);

            // Needs random dll from appdata folder!!!
            //objects classInstance = results.CompiledAssembly.CreateInstance(newClassName);
            //Toolform customManager = (ToolForm)classInstance;
            //return customManager;

            return null;
        }
    }
}
