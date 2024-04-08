
using System;
using GTA;
using GTA.UI;

namespace NegativeStatsMod
{
	public class NegativeStats : Script
	{
		public NegativeStats()
		{
			Tick += OnTick;
			Notification.Show("Negative Stats Mod Loaded");
		}

		private void OnTick(object sender, EventArgs e)
		{
			if (Game.IsLoading)
			{
				return;
			}

			if (!StoryCharacterStatsData.TryTerminateStatControllerScript())
			{
				Wait(1000);
				return;
			}

			StoryCharacterStatsData.ResetAllCharacterStats();
		}
	}
}