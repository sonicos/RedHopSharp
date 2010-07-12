using System.Xml.Serialization;

namespace RedHopSharp.Serialization
{
	public class HoptoadNotifier
	{
		[XmlElement("name")]
		public string Name { get; set; }
		[XmlElement("version")]
		public string Version { get; set; }
		[XmlElement("url")]
		public string Url { get; set; }
	}
}