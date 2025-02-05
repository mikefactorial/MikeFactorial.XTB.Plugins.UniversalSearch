using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace MikeFactorial.XTB.Plugins.Xsd
{
    [XmlRoot(ElementName = "label")]
    public class Label
    {
        [XmlAttribute(AttributeName = "languagecode")]
        public string Languagecode { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
    }

    [XmlRoot(ElementName = "labels")]
    public class Labels
    {
        [XmlElement(ElementName = "label")]
        public Label Label { get; set; }
    }

    [XmlRoot(ElementName = "control")]
    public class Control
    {
        [XmlAttribute(AttributeName = "disabled")]
        public string Disabled { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "datafieldname")]
        public string Datafieldname { get; set; }
        [XmlAttribute(AttributeName = "classid")]
        public string Classid { get; set; }
    }

    [XmlRoot(ElementName = "cell")]
    public class Cell
    {
        [XmlElement(ElementName = "labels")]
        public Labels Labels { get; set; }
        [XmlElement(ElementName = "control")]
        public Control Control { get; set; }
        [XmlAttribute(AttributeName = "showlabel")]
        public string Showlabel { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "locklevel")]
        public string Locklevel { get; set; }
    }

    [XmlRoot(ElementName = "row")]
    public class Row
    {
        [XmlElement(ElementName = "cell")]
        public Cell Cell { get; set; }
    }

    [XmlRoot(ElementName = "rows")]
    public class Rows
    {
        [XmlElement(ElementName = "row")]
        public List<Row> Row { get; set; }
    }

    [XmlRoot(ElementName = "section")]
    public class Section
    {
        [XmlElement(ElementName = "labels")]
        public Labels Labels { get; set; }
        [XmlElement(ElementName = "rows")]
        public Rows Rows { get; set; }
        [XmlAttribute(AttributeName = "showlabel")]
        public string Showlabel { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "celllabelposition")]
        public string Celllabelposition { get; set; }
        [XmlAttribute(AttributeName = "celllabelalignment")]
        public string Celllabelalignment { get; set; }
        [XmlAttribute(AttributeName = "labelwidth")]
        public string Labelwidth { get; set; }
        [XmlAttribute(AttributeName = "locklevel")]
        public string Locklevel { get; set; }
        [XmlAttribute(AttributeName = "IsUserDefined")]
        public string IsUserDefined { get; set; }
        [XmlAttribute(AttributeName = "showbar")]
        public string Showbar { get; set; }
        [XmlAttribute(AttributeName = "columns")]
        public string Columns { get; set; }
    }

    [XmlRoot(ElementName = "sections")]
    public class Sections
    {
        [XmlElement(ElementName = "section")]
        public Section Section { get; set; }
    }

    [XmlRoot(ElementName = "column")]
    public class Column
    {
        [XmlElement(ElementName = "sections")]
        public Sections Sections { get; set; }
        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }
    }

    [XmlRoot(ElementName = "columns")]
    public class Columns
    {
        [XmlElement(ElementName = "column")]
        public Column Column { get; set; }
    }

    [XmlRoot(ElementName = "tab")]
    public class Tab
    {
        [XmlElement(ElementName = "labels")]
        public Labels Labels { get; set; }
        [XmlElement(ElementName = "columns")]
        public Columns Columns { get; set; }
        [XmlAttribute(AttributeName = "showlabel")]
        public string Showlabel { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "tabs")]
    public class Tabs
    {
        [XmlElement(ElementName = "tab")]
        public Tab Tab { get; set; }
    }

    [XmlRoot(ElementName = "Role")]
    public class Role
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "DisplayConditions")]
    public class DisplayConditions
    {
        [XmlElement(ElementName = "Role")]
        public List<Role> Role { get; set; }
        [XmlAttribute(AttributeName = "Order")]
        public string Order { get; set; }
    }

    [XmlRoot(ElementName = "form")]
    public class FormXml
    {
        private static XmlSerializer serializer = null;

        public string FormName { get; set; }

        [XmlElement(ElementName = "tabs")]
        public Tabs Tabs { get; set; }
        [XmlElement(ElementName = "DisplayConditions")]
        public DisplayConditions DisplayConditions { get; set; }
        [XmlAttribute(AttributeName = "maxWidth")]
        public string MaxWidth { get; set; }

        public static FormXml Deserialize(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                return (FormXml)Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader));
            }
        }
        private static XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new XmlSerializer(typeof(FormXml));
                }
                return serializer;
            }
        }
    }
}
