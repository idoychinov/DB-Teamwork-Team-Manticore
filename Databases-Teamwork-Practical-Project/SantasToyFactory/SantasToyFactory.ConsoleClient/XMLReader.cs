namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Xml;

    using SantasToyFactory.DataLayer;
    using SantasToyFactory.Models;

    public class XMLReader
    {
        private SantasToyFactoryDatabase context;

        public XMLReader(string filePath, SantasToyFactoryDatabase db)
        {
            this.FilePath = filePath;
            this.context = db;
        }


        public string FilePath { get; set; }

        public void LoadBehaviorData()
        {
            var context = new SantasToyFactoryDatabase();
            using (XmlReader reader = XmlReader.Create(this.FilePath))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "child"))
                    {
                        string childName = reader.GetAttribute("name");
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "behavior")
                            {
                                Behavior childbehavior = (Behavior)Enum.Parse(typeof(Behavior), reader.ReadInnerXml());
                                var child = context.Children.SearchFor(ch => ch.Name == childName).FirstOrDefault();
                                if (child != null)
                                {
                                    child.Behavior = childbehavior;
                                    context.Children.Update(child);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
