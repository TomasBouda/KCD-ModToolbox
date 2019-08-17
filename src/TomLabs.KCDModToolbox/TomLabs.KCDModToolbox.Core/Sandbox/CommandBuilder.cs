namespace TomLabs.KCDModToolbox.Core.Sandbox
{
	public static class CommandBuilder
	{
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
	}
}
