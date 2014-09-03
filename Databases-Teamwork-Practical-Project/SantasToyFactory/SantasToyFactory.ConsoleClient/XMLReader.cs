namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Xml;

    using SantasToyFactory.DataLayer;

    public class XMLReader
    {
        public XMLReader(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; set; }

        public void LoadBehaviorData()
        {
            var db = new SantasToyFactoryDatabase();
            using (XmlReader reader = XmlReader.Create("../../Children-Behaviors.xml"))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "child"))
                    {
                        string childName = reader.GetAttribute("name");
                        do
                        {
                            reader.Read();
                        } while (reader.NodeType != "behavior");

                        
                    }
                }
            }
            
            using (XmlReader reader = XmlReader.Create("../../library.xml"))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        Console.WriteLine(reader.Name);
                    }
                }
            }
        }
    }
}
