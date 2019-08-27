using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml;
using TomLabs.KCDModToolbox.Core.Database.Entities;

namespace TomLabs.KCDModToolbox.Core.Database
{
	public class TableDescriptor
	{
		public string XmlPath { get; protected set; }

		public List<Column> IsReferencedBy { get; protected set; } = new List<Column>();

		public List<Column> Columns { get; set; } = new List<Column>();

		public List<ExpandoObject> Rows { get; set; } = new List<ExpandoObject>();

		public List<object> Entities { get; set; }

		public string Name { get; set; }

		[Obsolete("Only for serialization!")]
		public TableDescriptor()
		{

		}

		public TableDescriptor(string xmlPath, string name = null)
		{
			Name = name ?? Path.GetFileNameWithoutExtension(xmlPath);
			XmlPath = xmlPath;
		}

		public void AddReferenceToColumn(Column column)
		{
			if (IsReferencedBy.FirstOrDefault(c => c.Name == column.Name) == null)
			{
				IsReferencedBy.Add(column);
			}
			if (column.ReferencesTo.FirstOrDefault(t => t.Name == this.Name) == null)
			{
				column.ReferencesTo.Add(this);
			}
		}

		public TableDescriptor LoadTableData(string columnName, string value)
		{
			XmlDocument xml = new XmlDocument();
			Rows.Clear();
			xml.Load(XmlPath);

			var rows = xml.DocumentElement.SelectNodes($"//row[@{columnName}='{value}']");
			if (rows != null)
			{
				LoadRows(rows);
			}

			return this;
		}

		public TableDescriptor LoadTableData(bool forceReload = false)
		{
			if (forceReload || Rows?.Count == 0)
			{
				XmlDocument xml = new XmlDocument();
				Rows.Clear();
				xml.Load(XmlPath);

				XmlNode rows = xml.DocumentElement.SelectSingleNode("//rows");
				if (rows?.ChildNodes != null)
				{
					LoadRows(rows.ChildNodes);
				}
			}

			return this;
		}

		public List<T> AsEntities<T>() where T : IEntity
		{
			return Rows.Select(typeof(T).GetProperty("Selector").GetValue(null) as Func<ExpandoObject, T>).ToList();
		}

		private void LoadRows(XmlNodeList nodeList)
		{
			foreach (XmlNode row in nodeList)
			{
				dynamic rowObj = new ExpandoObject();
				foreach (XmlAttribute attribute in row.Attributes)
				{
					IDictionary<string, object> dictionary = (IDictionary<string, object>)rowObj;
					dictionary.Add(attribute.Name, attribute.Value);
				}
				Rows.Add(rowObj);
			}
		}
	}
}
