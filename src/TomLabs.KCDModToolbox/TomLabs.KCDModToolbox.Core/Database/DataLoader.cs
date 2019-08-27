using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TomLabs.KCDModToolbox.Core.Database.Entities.Buffs;
using TomLabs.KCDModToolbox.Core.Database.Entities.Items;
using TomLabs.KCDModToolbox.Core.Database.Entities.Souls;
using TomLabs.KCDModToolbox.Core.Database.Localizations;
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

		public ELocalizations LocalizationLanguage { get; private set; }

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

		private LocalizationsLoader Localizations { get; set; }

		/// <summary>
		/// Sets paths to Tables.pak and working directory where databse will be extracted.
		/// </summary>
		public void SetPaths(string kcdDirectory, string workingDirectory, ELocalizations locLanguage)
		{
			KCDDirectory = kcdDirectory;
			DbFilePath = $@"{KCDDirectory}Data\Tables.pak";
			WorkingDirectory = workingDirectory;
			LocalizationLanguage = locLanguage;
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

			// Add localization
			string localizationsDir = $@"{WorkingDirectory}\Localizations_{LocalizationLanguage.ToString().Substring(0, 3).ToLower()}";
			if (Directory.Exists(localizationsDir))
			{
				Directory.Delete(localizationsDir, true);
			}
			Directory.CreateDirectory(localizationsDir);
			ZipFile.ExtractToDirectory($@"{KCDDirectory}\Localization\{LocalizationLanguage}_xml.pak", localizationsDir);

			Database = new DatabaseDescriptor(WorkingDirectory, localizationsDir);
			Localizations = new LocalizationsLoader(_database); // Can't use prop because of custom getter
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

		public List<Buff> GetBuffs()
		{
			var buffs = Database.GetTable(Buff.TABLE_NAME, false).LoadTableData().AsEntities<Buff>();

			var buffClasses = GetBuffClasses();
			var localizations = Localizations.GetSoulLocalizations();

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

		public List<BuffClass> GetBuffClasses()
		{
			return Database.GetTable(BuffClass.TABLE_NAME, false).LoadTableData().AsEntities<BuffClass>();
		}

		public List<VSoulCharacterData> GetVSoulCharacterData()
		{
			return Database.GetTable(VSoulCharacterData.TABLE_NAME, false).LoadTableData().AsEntities<VSoulCharacterData>();
		}

		public List<Soul> GetSouls(bool withRelations = false)
		{
			var souls = Database.GetTable(Soul.TABLE_NAME, false).LoadTableData().AsEntities<Soul>();

			var vSoulCharData = GetVSoulCharacterData();
			var localizations = Localizations.GetSoulLocalizations();
			var weapon2weaponPresets = GetWeapon2WeaponPresets();

			souls.ForEach(s =>
			{
				s.UIName = vSoulCharData.FirstOrDefault(l => l.Id == s.Id).UIName;
				s.LocalizedName = localizations.FirstOrDefault(l => l.Key == s.UIName).Value;
				if (withRelations)
				{
					s.InitialWeaponPreset = weapon2weaponPresets.FirstOrDefault(p => p.WeaponPresetId == s.InitialWeaponPresetId);
				}
			});

			return souls;
		}

		public async Task<List<Soul>> GetSoulsAsync(bool withRelations = false)
		{
			return await Task.Run(() => GetSouls(withRelations));
		}

		public List<Item> GetItems()
		{
			var items = Database.GetTable("item", false).LoadTableData().AsEntities<Item>();

			var localizations = Localizations.GetItemLocalizations();

			items.ForEach(i =>
			{
				i.LocalizedName = localizations.FirstOrDefault(l => l.Key == $"ui_in_{i.Name}").Value;
			});

			return items;
		}

		public List<Weapon2WeaponPreset> GetWeapon2WeaponPresets()
		{
			var presets = Database.GetTable(Weapon2WeaponPreset.TABLE_NAME, false).LoadTableData().AsEntities<Weapon2WeaponPreset>();

			var items = GetItems();

			presets.ForEach(p =>
			{
				p.Item = items.FirstOrDefault(i => i.Id == p.ItemId);
			});

			return presets;
		}

		public List<Inventory2InventoryPreset> GetInventory2InventoryPresets()
		{
			var table = Database.GetTable("inventory2inventory_preset", false).LoadTableData();
			return table.Rows.Select(r =>
			   new Inventory2InventoryPreset(
				   r.AsDict("inventory_id").ToGuid(),
				   r.AsDict("inventory_preset_id").ToGuid())
				).ToList();
		}

		public List<Inventory2Item> GetInventory2Item2()
		{
			var table = Database.GetTable("inventory2item", false).LoadTableData();
			return table.Rows.Select(r =>
				   new Inventory2Item(r.AsDict("inventory_id").ToGuid(), r.AsDict("item_id").ToGuid())
				   {
					   Amount = r.AsDict("amount").ToString().ToInt()
				   }
				).ToList();
		}
	}
}
