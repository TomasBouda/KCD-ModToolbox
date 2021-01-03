using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using TomLabs.KCDModToolbox.Core.Database.Attributes;
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
			XmlPath = xmlPath;
			Name = name ?? Path.GetFileNameWithoutExtension(XmlPath);
		}

		internal void AddReferenceToColumn(Column column)
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

		internal List<T> AsEntities<T>() where T : IEntity
		{
			return Rows.Select(typeof(T).GetProperty("Selector").GetValue(null) as Func<ExpandoObject, T>).ToList();
		}

		internal bool SaveEntity<T>(T entity) where T : IEntity
		{
			try
			{
				var props = entity.GetType()
					.GetProperties(BindingFlags.Public | BindingFlags.Instance)
					.Where(p => Attribute.IsDefined(p, typeof(ColumnNameAttribute)) && !Attribute.IsDefined(p, typeof(KeyAttribute)))
					.ToArray();

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(File.ReadAllText(XmlPath));

				var keyColumn = entity.GetType().GetProperties().SingleOrDefault(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
				var attributeName = keyColumn.GetCustomAttribute(typeof(ColumnNameAttribute)) as ColumnNameAttribute;

				var row = doc.SelectSingleNode($"//row[@{attributeName.Name}='{keyColumn.GetValue(entity)}']");

				foreach (var columnProp in props)
				{
					var colNameAttr = columnProp.GetCustomAttribute(typeof(ColumnNameAttribute)) as ColumnNameAttribute;
					row.Attributes[colNameAttr.Name].Value = columnProp.GetValue(entity).ToString();
				}

				doc.Save(XmlPath);

				return true;
			}
			catch (Exception)
			{
				// TODO serilog
				return false;
			}
		}
	}
}
