using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

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
                        }
                    }
                }
            }
        }
    }
}
