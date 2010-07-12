using System.Xml.Serialization;

namespace RedHopSharp.Serialization
{
	public class HoptoadServerEnvironment
	{
		[XmlElement("project-root")]
		public string ProjectRoot { get; set; }
		[XmlElement("environment-name")]
		public string EnvironmentName { get; set; }
	}
}