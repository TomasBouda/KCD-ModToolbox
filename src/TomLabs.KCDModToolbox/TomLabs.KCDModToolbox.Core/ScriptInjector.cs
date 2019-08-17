using System.IO;

namespace TomLabs.KCDModToolbox.Core
{
	public class ScriptInjector
	{
		private static string BridgeFilePath { get; set; }

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

		public static void SetBridgeFilePath(string filePath)
		{
			BridgeFilePath = filePath;
		}

		public void Inject(string command)
		{
			File.WriteAllText(BridgeFilePath, command);
		}

		public void InjectCommand(string command)
		{
			File.WriteAllText(BridgeFilePath, $"System.ExecuteCommand('{command}')");
		}
	}
}
