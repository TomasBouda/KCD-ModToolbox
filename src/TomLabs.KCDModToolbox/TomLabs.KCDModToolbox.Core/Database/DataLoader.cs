using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TomLabs.KCDModToolbox.Core.Database.Buffs;
using TomLabs.KCDModToolbox.Core.Database.Souls;
using TomLabs.KCDModToolbox.Core.Extensions;
using TomLabs.Shadowgem.Extensions.String;

namespace TomLabs.KCDModToolbox.Core.Database
{
	public class DataLoader
	{
		private string KCDDirectory { get; set; }
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
		public void SetPaths(string kcdDirectory, string workingDirectory)
		{
			KCDDirectory = kcdDirectory;
			DbFilePath = $@"{KCDDirectory}Data\Tables.pak";
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

			// Add english localization
			string localizationsDir = $@"{WorkingDirectory}\Localizations";
			Directory.Delete(localizationsDir, true);
			Directory.CreateDirectory(localizationsDir);
			ZipFile.ExtractToDirectory($@"{KCDDirectory}\Localization\English_xml.pak", localizationsDir);

			Database = new DatabaseDescriptor(WorkingDirectory, localizationsDir);

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

		public IDictionary<string, string> GetSoulLocalizations()
		{
			return Database.GetLocalizationTable("text_ui_soul")?.Rows;
		}

		public List<BuffClass> GetBuffClasses()
		{
			var table = Database.GetTable("buff_class", false).LoadTableData();
			return table.Rows.Select(r =>
				new BuffClass(
					r.AsDict("buff_class_id").ToString(),
					r.AsDict("buff_class_name").ToString())
				).ToList();
		}

		public List<Buff> GetBuffs()
		{
			var table = Database.GetTable("buff", false).LoadTableData();
			var buffs = table.Rows.Select(r =>
				new Buff(r.AsDict("buff_id").ToString(), r.AsDict("buff_name").ToString(), r.AsDict("buff_class_id").ToString())
				{
					UIName = r.AsDict("buff_ui_name").ToString(),
					DescriptionKey = r.AsDict("buff_desc").ToString(),
				}
				).ToList();

			var buffClasses = GetBuffClasses();
			var localizations = GetSoulLocalizations();

			buffs.ForEach(b =>
			{
				b.Class = buffClasses.FirstOrDefault(c => c.Id == b.ClassId);
				b.LocalizedName = localizations.FirstOrDefault(l => l.Key == b.UIName).Value;
				b.Description = localizations.FirstOrDefault(l => l.Key == b.DescriptionKey).Value;
			});

			return buffs;
		}

		public async Task<List<Buff>> GetBuffsAsync()
		{
			return await Task.Run(GetBuffs);
		}

		public List<VSoulCharacterData> GetVSoulCharacterData()
		{
			var table = Database.GetTable("v_soul_character_data", false).LoadTableData();
			return table.Rows.Select(r =>
				new VSoulCharacterData(
					r.AsDict("soul_id").ToString(),
					r.AsDict("name_string_id").ToString())
				).ToList();
		}

		public List<Soul> GetSouls()
		{
			var table = Database.GetTable("soul", false).LoadTableData();
			var souls = table.Rows.Select(r =>
				new Soul(r.AsDict("soul_id").ToString(), r.AsDict("soul_name").ToString())
				{
					CombatLevel = r.AsDict("combat_level").ToString().ToInt(),
					Courage = r.AsDict("courage").ToString().ToInt(),
					Faction = r.AsDict("faction").ToString().ToInt(),
					InitialClothingPresetId = r.AsDict("initial_clothing_preset_id").ToString().ToGuid(),
					VIPClassId = r.AsDict("soul_vip_class_id").ToString().ToInt()
				}
				).ToList();

			var vSoulCharData = GetVSoulCharacterData();
			var localizations = GetSoulLocalizations();

			souls.ForEach(s =>
			{
				s.UIName = vSoulCharData.FirstOrDefault(l => l.Id == s.Id).UIName;
				s.LocalizedName = localizations.FirstOrDefault(l => l.Key == s.UIName).Value;
			});

			return souls;
		}

		public async Task<List<Soul>> GetSoulsAsync()
		{
			return await Task.Run(GetSouls);
		}
	}
}
