﻿
using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;

namespace NegativeStatsMod
{
	public static class StoryCharacterStatsData
	{
		public static Dictionary<PedTypes, List<Int32>> PedStatsHashes = new Dictionary<PedTypes, List<Int32>>
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
					Game.GenerateHash("SP0_STEALTH_ABILITY"),

					Game.GenerateHash("SP0_SPECIAL_ABILITY_MAXED"),
					Game.GenerateHash("SP0_SPECIAL_ABILITY_UNLOCKED_MAXED"),
					Game.GenerateHash("SP0_STAMINA_MAXED"),
					Game.GenerateHash("SP0_LUNG_CAPACITY_MAXED"),
					Game.GenerateHash("SP0_STRENGTH_MAXED"),
					Game.GenerateHash("SP0_WHEELIE_ABILITY_MAXED"),
					Game.GenerateHash("SP0_FLYING_ABILITY_MAXED"),
					Game.GenerateHash("SP0_SHOOTING_ABILITY_MAXED"),
					Game.GenerateHash("SP0_STEALTH_ABILITY_MAXED")
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
		// private static Int32 StatsControllerScriptHash { get; } = Game.GenerateHash("stats_controller");
		private static string StatsControllerScriptHash => "stats_controller";

		private static bool IsStatsControllerScriptRunning()
		{
			return Function.Call<bool>(Hash.HAS_SCRIPT_LOADED, StatsControllerScriptHash);
		}
		
		private static void TerminateStatsControllerScript()
		{
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, StatsControllerScriptHash);
		}

		public static void ResetAllCharacterStats()
		{
			foreach (var stats in PedStatsHashes.Values)
			{
				foreach (int statHash in stats)
				{
					Function.Call(Hash.STAT_SET_INT, statHash, 0, true);
				}

				Function.Call(Hash.UPDATE_SPECIAL_ABILITY_FROM_STAT, Game.Player.Handle, 0);
			}
		}

		public static bool TryTerminateStatControllerScript()
		{
			if (!IsStatsControllerScriptRunning())
			{
				return false;
			}

			ResetAllCharacterStats();
			TerminateStatsControllerScript();
			return true;
		}
	}
}