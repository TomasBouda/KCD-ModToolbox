using System.IO;

namespace TomLabs.KCDModToolbox.Core
{
	public class ScriptInjector
	{
		private const string BRIDGE_FILE_PATH = @"c:\Program Files (x86)\Steam\steamapps\common\KingdomComeDeliverance\mods\KCD_Bootloader\invoke_command.txt";

		private static ScriptInjector _instance;
		public static ScriptInjector Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ScriptInjector();
				}

				return _instance;
			}
		}

		public void Inject(string command)
		{
			File.WriteAllText(BRIDGE_FILE_PATH, command);
		}

		public void InjectCommand(string command)
		{
			File.WriteAllText(BRIDGE_FILE_PATH, $"System.ExecuteCommand('{command}')");
		}
	}
}
