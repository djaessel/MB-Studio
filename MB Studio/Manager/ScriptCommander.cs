using System;
using System.IO;
using System.Xml;
using System.Text;
using Microsoft.CSharp;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using importantLib;

namespace MB_Studio.Manager
{
    internal class ScriptCommander
    {
        //private const string METHOD_IDENTIFIER = @"\b(public|protected|private)\s(static\s)?(override\s)?(\w[A-z]+*\w\s)(\w*[A-Z]+*\w)\b[\s]+?[(]";// + "([A-z, 0-9_=<>\[\]]*|)[)]";
        private const string METHOD_IDENTIFIER = "protected override ";
        private const string SCRIPT_MARKER = "// @SCRIPT ";
        private const string ATTRIBUTES_MARKER = "// @ATTRIBUTES";

        private static string ScriptsFolder { get; } = Path.GetFullPath(@".\Manager\Scripts");

        public List<ToolForm> CustomManagers { get; } = new List<ToolForm>();

        private static readonly XmlReaderSettings xmlSettings = new XmlReaderSettings
        {
            Async = false,
            CheckCharacters = true,
            CloseInput = true,
            ConformanceLevel = ConformanceLevel.Auto,
            IgnoreComments = true,
            IgnoreProcessingInstructions = true,
            IgnoreWhitespace = true,
            MaxCharactersInDocument = 0,
        };

        public ScriptCommander() { }


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

                    using (XmlReader xmlReader = XmlReader.Create(File.OpenRead(configFile), xmlSettings))
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
                            CustomManagers.Add(newManager);
                        }
                    }
                }
            }
        }

        private static List<string> GenerateInitializeGUIMethod(string className, out List<string> classAttributes)
        {
            string groupBox = "GroupBox";

            string guiFile = ScriptsFolder + "\\" + className + "\\" + className + ".Designer.xml";

            classAttributes = new List<string>();

            List<string> alwaysStart = new List<string>()
            {
                "InitializeComponent",
                "{",
                "this.toolPanel.SuspendLayout();" + Environment.NewLine + "\t\t\t",
                "this.groupBox_0_gb.SuspendLayout();" + Environment.NewLine + "\t\t\t",
                "this.SuspendLayout();" + Environment.NewLine + Environment.NewLine + "\t\t\t",
            };

            List<string> alreadyHere = new List<string>()
            {
                "save_translation_btn",
                "language_cbb",
                "language_lbl",
                "singleNameTranslation_txt",
                "singleNameTranslation_lbl",
                "pluralNameTranslation_txt",
                "pluralNameTranslation_lbl",
            };

            List<string> specialAttributes = new List<string>()
            {
                "childIndex",
                "label",
                "lines",
            };

            List<string> func = new List<string>();

            func.AddRange(alwaysStart);

            using (XmlReader reader = XmlReader.Create(File.OpenRead(guiFile), xmlSettings))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement(groupBox))
                    {
                        int id = int.Parse(reader.GetAttribute("id"));
                        int lastId = id - 1;
                        int height = int.Parse(reader.GetAttribute("height"));
                        string text = reader.GetAttribute("text");

                        if (id > 0)
                        {
                            classAttributes.Add("Button|showGroup_" + id + "_btn");
                            classAttributes.Add("GroupBox|groupBox_" + id + "_gb");

                            /// CODE AUSLAGERN IN DATEI UND LEDIGLICH ID UND LASTID AUSTAUSCHE !!!

                            func.Add("this.Height = this.Height + this.groupBox_" + lastId + "_gb.Height + 8;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.toolPanel.Height = this.toolPanel.Height + this.groupBox_" + lastId + "_gb.Height + 8;" + Environment.NewLine + "\t\t\t");

                            func.Add("this.showGroup_" + id + "_btn = new Button();" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.Height = this.showGroup_" + lastId + "_btn.Height;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.Width = this.showGroup_" + lastId + "_btn.Width;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.Left = this.showGroup_" + lastId + "_btn.Left;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.Top = this.showGroup_" + lastId + "_btn.Top + this.showGroup_" + lastId + "_btn.Height + 4;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.Text = this.showGroup_" + lastId + "_btn.Text;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.Name = \"showGroup_" + id + "_btn\";" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.FlatStyle = FlatStyle.Flat;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.showGroup_" + id + "_btn.BackColor = Color.DimGray;" + Environment.NewLine + "\t\t\t");

                            func.Add("this.toolPanel.Controls.Add(this.showGroup_" + id + "_btn);" + Environment.NewLine + "\t\t\t");

                            func.Add("this.groupBox_" + id + "_gb = new GroupBox();" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Height = this.groupBox_" + lastId + "_gb.Height;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Width = this.groupBox_" + lastId + "_gb.Width;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Left = this.groupBox_" + lastId + "_gb.Left;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Top = this.groupBox_" + lastId + "_gb.Top + this.groupBox_" + lastId + "_gb.Height + 4;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Text = \"" + text + "\";" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Name = \"groupBox_" + id + "_gb\";" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.ForeColor = Color.White;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.FlatStyle = FlatStyle.Flat;" + Environment.NewLine + "\t\t\t");
                            func.Add("this.groupBox_" + id + "_gb.Font = new Font(\"Microsoft Sans Serif\", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);");
                            func.Add("this.toolPanel.Controls.Add(this.groupBox_" + id + "_gb);" + Environment.NewLine + "\t\t\t");

                            /// CODE AUSLAGERN IN DATEI UND LEDIGLICH ID UND LASTID AUSTAUSCHE !!!
                        }

                        func.Add("this.showGroup_" + id + "_btn.Tag = \"" + (height - ToolForm.GROUP_HEIGHT_MAX) + "\";" + Environment.NewLine + Environment.NewLine + "\t\t\t");

                        bool read, isEndElement;
                        do
                        {
                            read = reader.Read();
                            isEndElement = (reader.Name.Equals(groupBox) && !reader.IsStartElement());

                            string name = reader.GetAttribute("name");

                            if (!alreadyHere.Contains(name) && !isEndElement)
                            {
                                classAttributes.Add(reader.Name + '|' + name);

                                name = "this." + name;

                                func.Add(name + " = new " + reader.Name + "();");

                                if (reader.HasAttributes)
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        if (!specialAttributes.Contains(reader.Name))
                                        {
                                            bool isNumeric = ImportantMethods.IsNumeric(reader.Value, true);
                                            bool isBoolean = bool.TryParse(reader.Value, out bool result);

                                            string codeLine = name + '.' + reader.Name.Substring(0, 1).ToUpper() + reader.Name.Substring(1) + " = ";

                                            if (isNumeric || isBoolean)
                                                codeLine += reader.Value;
                                            else
                                                codeLine += '\"' + reader.Value + '\"';

                                            codeLine += ';' + Environment.NewLine + "\t\t\t";

                                            func.Add(codeLine);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Special Attribute: " + reader.Name + '=' + reader.Value);
                                        }
                                    }

                                    reader.MoveToElement();
                                }

                                func.Add("this.groupBox_" + id + "_gb.Controls.Add(" + name + ");" + Environment.NewLine + "\t\t\t");
                            }

                        } while (read && !isEndElement);

                        //reader.ReadEndElement();
                    }
                }
            }

            List<string> alwaysEnd = new List<string>()
            {
                Environment.NewLine + "\t\t\t",
                "this.Name = \"" + className + "Manager\";" + Environment.NewLine + "\t\t\t",
                "this.Text = this.Name;" + Environment.NewLine + "\t\t\t",
                Environment.NewLine + "\t\t\t",
                "this.toolPanel.ResumeLayout(false);" + Environment.NewLine + "\t\t\t",
                "this.groupBox_0_gb.ResumeLayout(false);" + Environment.NewLine + "\t\t\t",
                "this.groupBox_0_gb.PerformLayout();" + Environment.NewLine + "\t\t\t",
                "this.ResumeLayout(false);" + Environment.NewLine + "\t\t\t",
                "this.PerformLayout();" + Environment.NewLine + "\t\t",
                "}"
            };

            func.AddRange(alwaysEnd);

            return func;
        }

        private static ToolForm CreateCustomManagerFromScripts(string className, List<string> regionsRaw)
        {
            string constructorIdentifier = "public " + className + "Manager()";
            string basePath = ScriptsFolder + '\\' + className;

            List<List<string>> functions = new List<List<string>> {
                GenerateInitializeGUIMethod(className, out List<string> classAttributes)
            };

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
                        curList = new List<string> {
                            codeLines[i].Trim().Split()[((isMethod) ? 3 : 1)].Split('(')[0].Replace(" ", string.Empty)
                        };
                    }

                    if (codeLines[i].Contains("{"))
                        addToFunction = true;

                    if (addToFunction && curList != null)
                        curList.Add(codeLines[i].Trim());

                    if (codeLines[i].Contains("}"))
                    {
                        addToFunction = false;
                        functions.Add(curList);
                        curList = null;
                    }
                }
            }

            return CreateCustomManagerByCode(className, classAttributes, functions);
        }

        private static ToolForm CreateCustomManagerByCode(string className, List<string> classAttributes, List<List<string>> functions)
        {
            string exeName = Assembly.GetEntryAssembly().Location;
            string newClassName = "MB_Studio.Manager." + className + "Manager";
            string managerTemplateFile = ScriptsFolder + @"\Template\CustomManagerTemplate.cs";
            string managerTemplateCode = File.ReadAllText(managerTemplateFile).Replace("MyClass", className);
            string genSourceFile = ScriptsFolder + '\\' + newClassName + ".cs";

            StringBuilder attributeBlock = new StringBuilder();
            foreach (string attribute in classAttributes)
            {
                string[] sp = attribute.Split('|');
                attributeBlock.Append("private " + sp[0] + " " + sp[1] + ';' + Environment.NewLine + "\t\t");
            }

            managerTemplateCode = managerTemplateCode.Replace(ATTRIBUTES_MARKER, attributeBlock.ToString());

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

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("importantLib.dll");
            parameters.ReferencedAssemblies.Add("MB_Decompiler_Library.dll");
            parameters.ReferencedAssemblies.Add(exeName);//parameters.ReferencedAssemblies.Add(typeof(ToolForm).Assembly.CodeBase);

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
