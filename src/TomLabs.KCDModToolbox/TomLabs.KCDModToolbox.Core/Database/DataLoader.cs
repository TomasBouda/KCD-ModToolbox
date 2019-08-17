using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TomLabs.KCDModToolbox.Core.Database.Buffs;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database
{
	public class DataLoader
	{
		private readonly string dbFilePath;
		private readonly string workingDirectory;

		public DatabaseDescriptor Database { get; set; }

		public DataLoader(string dbFilePath, string workingDirectory)
		{
			this.dbFilePath = dbFilePath;
			this.workingDirectory = workingDirectory;

			Init();
		}

		private void Init()
		{
			// Copy Tables.pak into working directory
			if (Directory.GetFiles(workingDirectory, "*.*", SearchOption.AllDirectories).Length == 0)
			{
				ZipFile.ExtractToDirectory(dbFilePath, workingDirectory);
			}

			// Remove all *.pak file so KCD loads .xml files only
			foreach (var tblFile in Directory.GetFiles(workingDirectory, "*.tbl", SearchOption.AllDirectories))
			{
				File.Delete(tblFile);
			}

			Database = new DatabaseDescriptor(workingDirectory);
		}

		public List<Buff> GetBuffs()
		{
			var table = Database.GetTable("buff", false).LoadTableData();
			return table.Rows.Select(r => new Buff(r.AsDict()["buff_id"].ToString(), r.AsDict()["buff_name"].ToString())).ToList();
		}
	}
}
