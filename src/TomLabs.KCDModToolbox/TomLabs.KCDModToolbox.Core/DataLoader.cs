using System.IO;
using System.IO.Compression;

namespace TomLabs.KCDModToolbox.Core
{
	public class DataLoader
	{
		private readonly string dbFilePath;
		private readonly string workingDirectory;

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
		}

		public DatabaseDescriptor GetDatabase()
		{
			return new DatabaseDescriptor(workingDirectory);
		}
	}
}
