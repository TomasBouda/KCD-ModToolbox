using System.Collections.Generic;
using System.Windows.Input;
using TomLabs.KCDModToolbox.Core.Sandbox;
using TomLabs.Shadowgem.Extensions.String;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.Time
{
	public class TimePanelViewModel : BaseViewModel
	{
		public int GameTimeHours { get; set; }
		public int GameTimeMinutes { get; set; }
		public int GameTimeSeconds { get; set; }

		public int TimeSpeed { get; set; }

		public ICommand GetTimeCmd { get; set; }
		public ICommand SetTimeCmd { get; set; }

		public ICommand SetTimeSpeedCmd { get; set; }

		public TimePanelViewModel()
		{
			GetTimeCmd = new RelayCommand(GetTime);
			SetTimeCmd = new RelayCommand(SetTime);
			SetTimeSpeedCmd = new RelayCommand(SetTimeSpeed);
		}

		private async void GetTime()
		{
			var timeDict = await CommandBuilder.GetTime().ExecuteWithResultAsync();
			GameTimeHours = timeDict.GetValueOrDefault("hours", "0")?.ToString().ToInt() ?? 0;
			GameTimeMinutes = timeDict.GetValueOrDefault("minutes", "0")?.ToString().ToInt() ?? 0;
			GameTimeSeconds = timeDict.GetValueOrDefault("seconds", "0")?.ToString().ToInt() ?? 0;
		}

		private async void SetTime()
		{
			var timeDict = await CommandBuilder.GetTime().ExecuteWithResultAsync();
			var currentTimeHours = timeDict.GetValueOrDefault("hours", "0")?.ToString().ToInt() ?? 0;
			if (currentTimeHours < GameTimeHours)
			{
				await CommandBuilder.TimeAddHours(GameTimeHours - currentTimeHours).ExecuteAsync();
			}
			else
			{
				int toMidnight = 24 - currentTimeHours;
				await CommandBuilder.TimeAddHours(GameTimeHours + toMidnight).ExecuteAsync();
			}
		}

		private async void SetTimeSpeed()
		{
			await CommandBuilder.SetTimeSpeed(TimeSpeed).ExecuteAsync();
		}
	}
}
