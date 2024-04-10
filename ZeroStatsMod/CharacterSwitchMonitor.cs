
using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;

namespace ZeroStatsMod
{
	/// <summary>
	/// Monitors the character switch stats panel if it is active, if so we have to manually update the label values
	/// to zero, as they don't pull the correct set of values, even though we set them to zero and the Character Stats
	/// screen shows them as zero, the switch stats panel still shows the original values.
	/// </summary>
	public class CharacterSwitchMonitor : Script
	{
		/// <summary>
		/// The <see cref="Scaleform"/> instance for the player switch stats panel.
		/// </summary>
		private static Scaleform PlayerSwitchStatsPanel = new Scaleform("PLAYER_SWITCH_STATS_PANEL");

		/// <summary>
		/// All labels that are shown on the character switch stats panel.
		/// </summary>
		private static readonly List<string> CharacterSwitchScaleFormLabels = new List<string>
		{
			"PS_SPEC_AB",
			"PS_STAMINA",
			"PS_LUNG",
			"PS_STRENGTH",
			"PS_DRIVING",
			"PS_FLYING",
			"PS_SHOOTING",
			"PS_STEALTH"
		};

		public CharacterSwitchMonitor()
		{
			Tick += OnTick;
		}

		private void OnTick(object sender, EventArgs e)
		{
			if (Game.IsLoading)
			{
				Wait(1000);
				return;
			}

			if (!PlayerSwitchStatsPanel.IsLoaded)
			{
				// player isn't currently switching characters
				Wait(1000);
				return;
			}

			OverrideStatsToZero();
		}

		private static void OverrideStatsToZero()
		{
			Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, PlayerSwitchStatsPanel.Handle, "SET_STATS_LABELS");

			Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 0);
			Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_BOOL, false);

			foreach (string label in CharacterSwitchScaleFormLabels)
			{
				Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, 0);
				Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, label);
				Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);
			}

			Function.Call(Hash.END_SCALEFORM_MOVIE_METHOD);
		}
	}
}