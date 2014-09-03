namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Xml;

    using SantasToyFactory.DataLayer;
    using SantasToyFactory.Models;

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
                        } while (reader.NodeType != XmlNodeType.Element && reader.Name == "behavior");

                        Behavior childbehavior = (Behavior)Enum.Parse(typeof(Behavior), reader.Value);

                        var child = db.Children.SearchFor(ch => ch.Name == childName).FirstOrDefault();
                        child.Behavior = childbehavior;
                        db.Children.Update(child);
                    }
                }
            }
        }
    }
}
