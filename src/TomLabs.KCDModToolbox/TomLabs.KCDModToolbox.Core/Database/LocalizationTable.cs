using System.Collections.Generic;
using System.Xml;

namespace TomLabs.KCDModToolbox.Core.Database
{
	public class LocalizationTable
	{
		public string Name { get; set; }
		public IDictionary<string, string> Rows { get; set; } = new Dictionary<string, string>();

		public LocalizationTable(string filePath)
		{
			XmlDocument xml = new XmlDocument();
			xml.Load(filePath);

			var rows = xml.DocumentElement.SelectNodes("//Row");
			if (rows != null)
			{
				foreach (XmlNode node in rows)
				{
					Rows.Add(node.FirstChild.InnerText, node.LastChild.InnerText);
				}
			}
		}
	}
}
