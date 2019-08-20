using System;
using System.Windows.Input;
using TomLabs.Shadowgem.Extensions.String;
using TomLabs.WPF.Tools;
using TomLabs.WPF.Tools.Commands;

namespace TomLabs.KCDModToolbox.App.ViewModels.Sandbox.NPC
{
	public class NPCSearchViewModel : BaseViewModel
	{
		public string Text { get; set; } = string.Empty;
		public bool OnlyFavorites { get; set; }
		public string VIPClass { get; set; } = string.Empty;
		public string CombatLevel { get; set; } = string.Empty;
		public string Faction { get; set; } = string.Empty;
		public string Strength { get; set; } = string.Empty;
		public string Agility { get; set; } = string.Empty;
		public string Vitality { get; set; } = string.Empty;
		public string Speech { get; set; } = string.Empty;

		public ICommand ToggleFavoritesOnlyCmd { get; set; }
		public ICommand ClearAllCmd { get; set; }

		public NPCSearchViewModel()
		{
			ToggleFavoritesOnlyCmd = new RelayCommand(() => OnlyFavorites = !OnlyFavorites);
			ClearAllCmd = new RelayCommand(ClearAll);
		}

		internal Func<NPCViewModel, bool> Predicate()
		{
			return npc =>
				(
					npc.Details.LocalizedName?.Contains(Text, StringComparison.InvariantCultureIgnoreCase) == true
					||
					npc.Details.Name.Contains(Text, StringComparison.InvariantCultureIgnoreCase)
				)
				&&
				(
					VIPClass == string.Empty || WithOperators(VIPClass, npc.Details.VIPClassId)
				)
				&&
				(
					CombatLevel == string.Empty || WithOperators(CombatLevel, npc.Details.CombatLevel)
				)
				&&
				(
					Faction == string.Empty || WithOperators(Faction, npc.Details.Faction)
				)
				&&
				(
					Strength == string.Empty || WithOperators(Strength, npc.Details.Strength)
				)
				&&
				(
					Vitality == string.Empty || WithOperators(Vitality, npc.Details.Vitality)
				)
				&&
				(
					Speech == string.Empty || WithOperators(Speech, npc.Details.Speech)
				)
				&&
				(
					!OnlyFavorites || (OnlyFavorites && npc.IsFavorite)
				)
				;
		}

		private bool WithOperators(string input, int compareTo)
		{
			if (input.Contains("<>") || input.Contains("!="))
			{
				return CheckOperator(input, compareTo, (a, b) => a != b);
			}
			else if (input.Contains("<="))
			{
				return CheckOperator(input, compareTo, (a, b) => a <= b);
			}
			else if (input.Contains("<"))
			{
				return CheckOperator(input, compareTo, (a, b) => a < b);
			}
			else if (input.Contains(">="))
			{
				return CheckOperator(input, compareTo, (a, b) => a <= b);
			}
			else if (input.Contains(">"))
			{
				return CheckOperator(input, compareTo, (a, b) => a > b);
			}
			else
			{
				return input.ToInt() == compareTo;
			}
		}

		private bool CheckOperator(string input, int compareTo, Func<int, int, bool> copareFunc)
		{
			int intInput = input.ReplaceAll("", "<", "=", ">", "!").Trim().ToInt();

			return copareFunc(compareTo, intInput);
		}

		private void ClearAll()
		{
			Text = string.Empty;
			OnlyFavorites = false;
			VIPClass = string.Empty;
			CombatLevel = string.Empty;
			Faction = string.Empty;
			Strength = string.Empty;
			Agility = string.Empty;
			Vitality = string.Empty;
			Speech = string.Empty;
		}
	}
}
