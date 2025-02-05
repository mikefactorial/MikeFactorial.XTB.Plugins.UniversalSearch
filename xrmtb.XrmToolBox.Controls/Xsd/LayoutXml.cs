using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MikeFactorial.XTB.Plugins.Xsd
{
    [XmlRoot(ElementName = "cell")]
    public class LayoutCell
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
    }

    [XmlRoot(ElementName = "row")]
    public class LayoutRow
    {
        [XmlElement(ElementName = "cell")]
        public List<LayoutCell> Cell { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "grid")]
    public class LayoutXml
    {
        private static XmlSerializer serializer = null;

        public string ViewName { get; set; }

        [XmlElement(ElementName = "row")]
        public LayoutRow Row { get; set; }
        [XmlAttribute(AttributeName = "preview")]
        public string Preview { get; set; }
        [XmlAttribute(AttributeName = "icon")]
        public string Icon { get; set; }
        [XmlAttribute(AttributeName = "select")]
        public string Select { get; set; }
        [XmlAttribute(AttributeName = "jump")]
        public string Jump { get; set; }
        [XmlAttribute(AttributeName = "object")]
        public string Object { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        public static LayoutXml Deserialize(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                return (LayoutXml)Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader));
            }
        }
        private static XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new XmlSerializer(typeof(LayoutXml));
                }
                return serializer;
            }
        }

    }
}
