namespace TomLabs.KCDModToolbox.Core.Sandbox
{
	public static class CommandBuilder
	{
		/// <summary>
		/// Clears bridge file invoke_command.txt
		/// </summary>
		public static void Clear()
		{
			ScriptInjector.Instance.Inject("");
		}

		public static Command GetTime()
		{
			return new Command("cheat_get_time", @"time=(?<hours>\d+):(?<minutes>\d+):(?<seconds>\d+)");
		}

		public static Command TimeAddHours(int hours)
		{
			return new Command($"cheat_set_time hours:{hours}");
		}

		public static Command SetTimeSpeed(int ratio)
		{
			return new Command($"cheat_set_time_speed ratio:{ratio}");
		}

		public static Command AddBuff(string id)
		{
			return new Command($"cheat_add_buff id:{id}");
		}

		public static Command RemoveBuff(string id)
		{
			return new Command($"cheat_remove_buff id:{id}");
		}

		public static Command AddBuffImmortal()
		{
			return new Command("cheat_add_buff_immortal");
		}

		public static Command RemoveBuffImmortal()
		{
			return new Command("cheat_remove_buff_immortal");
		}

		public static Command AddBuffInvisible()
		{
			return new Command("cheat_add_buff_invisible");
		}

		public static Command RemoveBuffInvisible()
		{
			return new Command("cheat_remove_buff_invisible");
		}

		public static Command AddBuffHeal()
		{
			return new Command("cheat_add_buff_heal");
		}

		public static Command SpawnNpc(string id, int distance = 5, int count = 1)
		{
			return new Command($"cheat_spawn_npc token:{id} distance:{distance} count:{count}");
		}

		public static Command TeleportPlayer(int x, int y, int z)
		{
			return new Command($"cheat_teleport x:{x} y:{y} z:{z}");
		}
	}
}
