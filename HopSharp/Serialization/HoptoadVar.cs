using System.Xml.Serialization;

namespace RedHopSharp.Serialization
{
    [XmlRoot("var")]
    public class Vars
    {
        [XmlAttribute("key")]
        public string Key { get; set; }
        [XmlText]
        public string Value { get; set; }
    }
}
