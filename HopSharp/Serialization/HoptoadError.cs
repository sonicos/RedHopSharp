using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RedHopSharp.Serialization
{
	[XmlInclude(typeof(TraceLine))]
	public class HoptoadError
	{
		[XmlElement("class")]
		public string Class { get; set; }

		[XmlElement("message")]
		public string Message { get; set; }

		[XmlArray("backtrace")]
		[XmlArrayItem("line")]
		public TraceLine[] Backtrace { get; set; }
	}
}