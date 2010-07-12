using System.Xml.Serialization;


namespace RedHopSharp.Serialization
{
    [XmlInclude(typeof(Vars))]
    public class HoptoadRequest
    {
        [XmlElement("url")]
        public string Url { get; set; }
       // [XmlElement("cgi-data")]
       // public string CgiData { get; set; }
        [XmlElement("component")]
        public string Component { get; set; }
        [XmlElement("action")]
        public string Action { get; set; }
        [XmlElement("params")]
        public string Params { get; set; }
        [XmlElement("session")]
        public string Session { get; set; }
        [XmlArray("cgi-data")]
		[XmlArrayItem("var")]
        public Vars[] Vars { get; set; }
    }
}
