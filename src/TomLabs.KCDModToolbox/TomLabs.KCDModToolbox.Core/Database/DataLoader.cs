using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TomLabs.KCDModToolbox.Core.Database.Buffs;
using TomLabs.KCDModToolbox.Core.Extensions;

namespace TomLabs.KCDModToolbox.Core.Database
{
	public class DataLoader
	{
		private string DbFilePath { get; set; }
		private string WorkingDirectory { get; set; }

		private DatabaseDescriptor _database;
		public DatabaseDescriptor Database
		{
			get
			{
				InitIfNot();
				return _database;
			}
			private set
			{
				_database = value;
			}
		}

		public bool IsInitialized { get; set; }

		private static DataLoader _instance;

		public static DataLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DataLoader();
				}

				return _instance;
			}
		}


		/// <summary>
		/// Sets paths to Tables.pak and working directory where databse will be extracted.
		/// </summary>
		public void SetPaths(string dbFilePath, string workingDirectory)
		{
			DbFilePath = dbFilePath;
			WorkingDirectory = workingDirectory;
		}

		/// <summary>
		/// Initializes local database extracted from Tables.pak
		/// </summary>
		public void Init()
		{
			// Copy Tables.pak into working directory
			if (Directory.GetFiles(WorkingDirectory, "*.*", SearchOption.AllDirectories).Length == 0)
			{
				ZipFile.ExtractToDirectory(DbFilePath, WorkingDirectory);
			}

			// Remove all *.pak file so KCD loads .xml files only
			foreach (var tblFile in Directory.GetFiles(WorkingDirectory, "*.tbl", SearchOption.AllDirectories))
			{
				File.Delete(tblFile);
			}

			Database = new DatabaseDescriptor(WorkingDirectory);

			IsInitialized = true;
		}

		/// <summary>
		/// Initializes <see cref="DataLoader"/> if not already initialized
		/// </summary>
		private void InitIfNot()
		{
			if (!IsInitialized)
			{
				Init();
			}
		}

		public List<BuffClass> GetBuffClasses()
		{
			var table = Database.GetTable("buff_class", false).LoadTableData();
			return table.Rows.Select(r => new BuffClass(r.AsDict("buff_class_id").ToString(), r.AsDict("buff_class_name").ToString())).ToList();
		}

		public List<Buff> GetBuffs()
		{
			var table = Database.GetTable("buff", false).LoadTableData();
			var buffs = table.Rows.Select(r => new Buff(r.AsDict("buff_id").ToString(), r.AsDict("buff_name").ToString(), r.AsDict("buff_class_id").ToString())).ToList();
			var buffClasses = GetBuffClasses();
			buffs.ForEach(b => b.Class = buffClasses.FirstOrDefault(c => c.Id == b.ClassId));

			return buffs;
		}

		public async Task<List<Buff>> GetBuffsAsync()
		{
			return await Task.Run(GetBuffs);
		}
	}
}
