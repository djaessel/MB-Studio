using System;
using System.IO;
using System.Xml;
using System.Text;
using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace MB_Studio.Manager
{
    internal class ScriptCommander
    {
        //private const string METHOD_IDENTIFIER = @"/(public|protected|private) (static |)(override |)(\w[A-z]*\w )(\w*[A-Z]*\w)([(])([A-z, 0-9_=<>\[\]]*|)([)])/g";
        private const string METHOD_IDENTIFIER = "protected override ";
        private const string SCRIPT_MARKER = "// @SCRIPT ";

        private static string ScriptsFolder { get; } = Path.GetFullPath(@".\Manager\Scripts");

        private readonly List<ToolForm> customManagers = new List<ToolForm>();

        public ScriptCommander()
        {
            //Console.Write(Environment.NewLine + "Initializing ScriptCommander...");
            //
            //Console.WriteLine("done.");
        }

        public List<ToolForm> GetCustomManagers()
        {
            return customManagers;
        }

        public void LoadManagers()
        {
            if (!Directory.Exists(ScriptsFolder)) return;

            foreach (string dir in Directory.GetDirectories(ScriptsFolder))
            {
                string configFile = dir + "\\" + dir.Substring(dir.LastIndexOf('\\') + 1) + ".xml";
                if (File.Exists(configFile))
                {
                    string managerName = Path.GetFileName(configFile);
                    //managerName = managerName.Remove(managerName.IndexOf('.'));

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

                            Console.WriteLine("Attributes: " + attributes);// TODO: add to CustomManager creation process
                            Console.WriteLine("Properties: " + properties);// TODO: add to CustomManager creation process

                            scripts = scripts.Trim('\t', ' ', ';');

                            ToolForm newManager = CreateCustomManagerFromScripts(name, new List<string>(scripts.Split(';')));
                            customManagers.Add(newManager);
                        }
                    }
                }
            }
        }

        private static ToolForm CreateCustomManagerFromScripts(string className, List<string> regionsRaw)
        {
            string constructorIdentifier = "public " + className + "Manager()";
            string basePath = ScriptsFolder + '\\' + className;

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
                    //Match match = Regex.Match(codeLines[i], METHOD_IDENTIFIER, RegexOptions.Singleline);
                    //bool isMethod = match.Success;
                    bool isMethod = codeLines[i].StartsWith(METHOD_IDENTIFIER);
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

            return CreateCustomManagerByCode(className, functions);
        }

        private static ToolForm CreateCustomManagerByCode(string className, List<List<string>> functions)
        {
            string exeName = Assembly.GetEntryAssembly().Location;
            string newClassName = "MB_Studio.Manager." + className + "Manager";
            string managerTemplate = ScriptsFolder + @"\Template\CustomManagerTemplate.cs";
            string managerTemplateCode = File.ReadAllText(managerTemplate).Replace("MyClass", className);
            string genSourceFile = ScriptsFolder + '\\' + newClassName + ".cs";

            foreach (List<string> function in functions)
            {
                string name = function[0];
                string functionCode = string.Empty;

                for (int i = 1; i < function.Count; i++)
                    functionCode += function[i];

                functionCode = functionCode.TrimStart('{').TrimEnd('}').Trim();

                managerTemplateCode = managerTemplateCode.Replace(SCRIPT_MARKER + name, functionCode);
            }

            File.WriteAllText(genSourceFile, managerTemplateCode);

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters {
                GenerateExecutable = false,
                GenerateInMemory = true,
            };

            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("importantLib.dll");
            parameters.ReferencedAssemblies.Add("skillhunter.dll");
            parameters.ReferencedAssemblies.Add("MB_Decompiler_Library.dll");
            parameters.ReferencedAssemblies.Add(exeName);
            //parameters.ReferencedAssemblies.Add(typeof(ToolForm).Assembly.CodeBase);

            CompilerResults results = provider.CompileAssemblyFromFile(parameters, new string[] { genSourceFile });
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));

                throw new InvalidOperationException(sb.ToString());
            }

            Assembly assembly = results.CompiledAssembly;
            return (ToolForm)assembly.CreateInstance(newClassName, true);
        }
    }
}
