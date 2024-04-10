
using System;
using GTA;

namespace ZeroStatsMod
{
	/// <summary>
	/// Handles the story character's stats periodically setting them to zero to prevent them from increasing.
	/// <see cref="StoryCharacterStatsData.TryTerminateStatControllerScript"/> is used to terminate the original script that handles the stats.
	/// Note: Disabling this script will reset the stats to their original values, this is a temporary change!
	/// </summary>
	public class CharacterStatsHandler : Script
	{
		public CharacterStatsHandler()
		{
			Tick += OnTick;
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
			}

			StoryCharacterStatsData.ResetAllCharacterStats();
			Wait(250);
		}
	}
}