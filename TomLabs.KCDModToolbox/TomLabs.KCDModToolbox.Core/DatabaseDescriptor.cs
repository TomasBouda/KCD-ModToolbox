using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace TomLabs.KCDModToolbox.Core
{
	public class DatabaseDescriptor
	{
		private XmlDocument doc = new XmlDocument();

		private string WorkingDirectory { get; set; }

		public List<TableDescriptor> Tables { get; set; } = new List<TableDescriptor>();

		[Obsolete("Only for serialization!")]
		public DatabaseDescriptor()
		{

		}

		public DatabaseDescriptor(string workingDirectory)
		{
			WorkingDirectory = workingDirectory;
		}

		public TableDescriptor GetTable(string tableName, bool loadRelations = true)
		{
			var table = this.Tables.FirstOrDefault(t => t.Name == tableName);

			if (table != null)
			{
				return table;
			}

			string tablePath = Directory.GetFiles(WorkingDirectory + @"\Libs\Tables", $"{tableName}.xml", SearchOption.AllDirectories)?.SingleOrDefault();
			if (!string.IsNullOrEmpty(tablePath))
			{
				doc.Load(tablePath);
				table = new TableDescriptor(tablePath);
				this.Tables.Add(table);

				GetTableColumns(doc, table, loadRelations);

				if (loadRelations)
				{
					var primaryColumn = table.Columns.FirstOrDefault(c => c.IsKey);
					if (primaryColumn != null)
					{
						// M2M relations
						foreach (var tbl in Directory.GetFiles(WorkingDirectory + @"\Libs\Tables", "*.xml", SearchOption.AllDirectories)
							.Where(t => Path.GetFileNameWithoutExtension(t) != tableName))
						{
							//string xmlContent = File.ReadAllText(tbl);
							bool isRelated = false;
							using (StreamReader sr = File.OpenText(tbl))
							{
								string s = "";
								int i = 0;

								// Read file header / first 100 lines of xml
								while ((s = sr.ReadLine()) != null && i < 100)
								{
									if (s.Contains($"name=\"{primaryColumn.Name}\""))
									{
										isRelated = true;
										break;
									}
									i++;
								}
							}

							if (isRelated)
							{
								doc.Load(tbl);

								XmlNode foreignColumn = doc.DocumentElement.SelectSingleNode($"//header/column[@name='{primaryColumn.Name}']");
								if (foreignColumn != null)
								{
									string relatedTableName = Path.GetFileNameWithoutExtension(tbl);
									var relatedTable = GetTable(relatedTableName);
									relatedTable?.AddReferenceToColumn(primaryColumn);
								}
							}
						}
					}
				}

				return table;
			}
			else
			{
				return null;
			}
		}

		private TableDescriptor GetTableColumns(XmlDocument xml, TableDescriptor table, bool loadRelations = true)
		{
			XmlNode header = xml.DocumentElement.SelectSingleNode("//header");
			if (header != null)
			{
				foreach (XmlNode columnElement in header.ChildNodes)
				{
					var col = new Column(columnElement.Attributes["name"].Value, columnElement.Attributes["type"].Value);

					if (col.Name.EndsWith("id"))
					{
						if (col.Name == $"{table.Name}_id")
						{
							col.IsKey = true;
						}
						else if (loadRelations)
						{
							string relatedTableName = col.Name.Replace("initial_", "").Replace("id", "").TrimEnd('_');
							var relatedTable = GetTable(relatedTableName);
							relatedTable?.AddReferenceToColumn(col);
						}
					}

					table.Columns.Add(col);
				}
			}

			return table;
		}
	}
}
