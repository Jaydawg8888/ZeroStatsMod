
using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;

namespace NegativeStatsMod
{
	public static class StoryCharacterStatsData
	{
		/// <summary>
		/// The <see cref="Game.GenerateHash"/> of the stats so that we can set them to zero.
		/// The values are hashed once and cached as we'll be using them multiple times.
		/// </summary>
		private static Dictionary<PedTypes, List<Int32>> StoryCharacterStatHashes = new Dictionary<PedTypes, List<Int32>>
		{
			{
				PedTypes.PED_TYPE_Michael, new List<Int32>
				{
					Game.GenerateHash("SP0_SPECIAL_ABILITY"),
					Game.GenerateHash("SP0_SPECIAL_ABILITY_UNLOCKED"),
					Game.GenerateHash("SP0_STAMINA"),
					Game.GenerateHash("SP0_LUNG_CAPACITY"),
					Game.GenerateHash("SP0_STRENGTH"),
					Game.GenerateHash("SP0_WHEELIE_ABILITY"),
					Game.GenerateHash("SP0_FLYING_ABILITY"),
					Game.GenerateHash("SP0_SHOOTING_ABILITY"),
					Game.GenerateHash("SP0_STEALTH_ABILITY")
				}
			},
			{
				PedTypes.PED_TYPE_Franklin, new List<Int32>
				{
					Game.GenerateHash("SP1_SPECIAL_ABILITY"),
					Game.GenerateHash("SP1_SPECIAL_ABILITY_UNLOCKED"),
					Game.GenerateHash("SP1_STAMINA"),
					Game.GenerateHash("SP1_LUNG_CAPACITY"),
					Game.GenerateHash("SP1_STRENGTH"),
					Game.GenerateHash("SP1_WHEELIE_ABILITY"),
					Game.GenerateHash("SP1_FLYING_ABILITY"),
					Game.GenerateHash("SP1_SHOOTING_ABILITY"),
					Game.GenerateHash("SP1_STEALTH_ABILITY")
				}
			},
			{
				PedTypes.PED_TYPE_Trevor, new List<Int32>
				{
					Game.GenerateHash("SP2_SPECIAL_ABILITY"),
					Game.GenerateHash("SP2_SPECIAL_ABILITY_UNLOCKED"),
					Game.GenerateHash("SP2_STAMINA"),
					Game.GenerateHash("SP2_LUNG_CAPACITY"),
					Game.GenerateHash("SP2_STRENGTH"),
					Game.GenerateHash("SP2_WHEELIE_ABILITY"),
					Game.GenerateHash("SP2_FLYING_ABILITY"),
					Game.GenerateHash("SP2_SHOOTING_ABILITY"),
					Game.GenerateHash("SP2_STEALTH_ABILITY")
				}
			}
		};

		/// <summary>
		/// The hash of the stats controller script. This must be disabled via TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME.
		/// As otherwise, the game will continually reset the stats back to their original values.
		/// </summary>
		private static string StatsControllerScriptHash => "stats_controller";

		/// <summary>
		/// Whether the stats controller script is running.
		/// </summary>
		/// <returns></returns>
		private static bool IsStatsControllerScriptRunning()
		{
			return Function.Call<bool>(Hash.HAS_SCRIPT_LOADED, StatsControllerScriptHash);
		}

		/// <summary>
		/// Terminates the stats controller script.
		/// </summary>
		private static void TerminateStatsControllerScript()
		{
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, StatsControllerScriptHash);
		}

		/// <summary>
		/// Resets all story character stats to zero.
		/// </summary>
		public static void ResetAllCharacterStats()
		{
			foreach (var stats in StoryCharacterStatHashes.Values)
			{
				foreach (int statHash in stats)
				{
					Function.Call(Hash.STAT_SET_INT, statHash, 0, true);
				}

				Function.Call(Hash.UPDATE_SPECIAL_ABILITY_FROM_STAT, Game.Player.Handle, 0);
			}
		}

		/// <summary>
		/// Tries to terminate the <see cref="StatsControllerScriptHash"/>, if it's running.
		/// </summary>
		/// <returns>Whether the script was terminated.</returns>
		public static bool TryTerminateStatControllerScript()
		{
			if (!IsStatsControllerScriptRunning())
			{
				// the script isn't running, so we don't need to terminate it.
				return false;
			}

			// the script was running so set all character stats to zero and then terminate the script.
			ResetAllCharacterStats();
			TerminateStatsControllerScript();
			return true;
		}
	}
}