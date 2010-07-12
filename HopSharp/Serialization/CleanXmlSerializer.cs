using System.IO;
using System.Xml.Serialization;

namespace RedHopSharp.Serialization
{
	//Wrap XML serialization and do not generate processing instructions on document start 
	//as well as xsi and xsd namespace definitions
	public class CleanXmlSerializer<TRoot>
	{
		XmlSerializerNamespaces namespaces;

		public CleanXmlSerializer()
		{
			//Create our own namespaces for the output
			this.namespaces = new XmlSerializerNamespaces();

			//Add an empty namespace and empty value
			this.namespaces.Add("", "");
		}

		public string ToXml(TRoot source)
		{
			var writer = new StringWriter();
			var xmlWriter = new XmlTextWriterFormattedNoDeclaration(writer);

			var serializer = new XmlSerializer(typeof(TRoot));
			serializer.Serialize(xmlWriter, source, this.namespaces);
           
            var content = writer.GetStringBuilder().ToString();
			return content;
		}

		class XmlTextWriterFormattedNoDeclaration : System.Xml.XmlTextWriter
		{
			public XmlTextWriterFormattedNoDeclaration(System.IO.TextWriter w)
				: base(w)
			{
				Formatting = System.Xml.Formatting.Indented;
			}

			public override void WriteStartDocument() { } // suppress
		}
	}
}