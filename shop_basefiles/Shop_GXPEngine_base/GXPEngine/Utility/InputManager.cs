using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;
using PlayerScripts;

namespace Utility
{
	public static class InputManager
	{
		private struct PlayerButtons
		{
			public readonly int Up;
			public readonly int Down;
			public readonly int Left;
			public readonly int Right;
			
			public readonly int[] ActionKeys;

			public PlayerButtons(int up, int down, int left, int right,
				int action1, int action2, int action3, int action4)
			{
				Up = up;
				Down = down;
				Left = left;
				Right = right;

				ActionKeys = new[] {action1, action2, action3, action4};
			}
		}

		private static readonly Dictionary<Player, PlayerButtons> buttonsPerPlayer =
			new Dictionary<Player, PlayerButtons>();

		private static readonly List<Player> allPlayers = new List<Player>()
		{
			//All names are fictional and any resemblance with any person, living or dead, is purely coincidental
			new Player(0, "Bram"),
			new Player(1, "Yiwei"),

			// new Player(2, "Yvens"), //Ran out of buttons, sorry
		};

		static InputManager()
		{
			PlayerButtons player1Buttons = new PlayerButtons(
				Key.W, Key.S, Key.A, Key.D, Key.SPACE, Key.BACKSPACE, Key.ESCAPE, Key.E);
			
			PlayerButtons player2Buttons = new PlayerButtons(
				Key.UP, Key.DOWN, Key.LEFT, Key.RIGHT, Key.RIGHT_CTRL, Key.RIGHT_ALT, Key.MINUS, Key.RIGHT_SHIFT);

			buttonsPerPlayer.Add(allPlayers[0], player1Buttons);
			buttonsPerPlayer.Add(allPlayers[1], player2Buttons);
		}

		public static int GetHorizontalInput(int player)
		{
			if (Input.GetKeyDown(buttonsPerPlayer[allPlayers[player]].Left))
			{
				return -1;
			}

			if (Input.GetKeyDown(buttonsPerPlayer[allPlayers[player]].Right))
			{
				return 1;
			}

			return 0;
		}

		public static int GetVerticalInput(int player)
		{
			if (Input.GetKeyDown(buttonsPerPlayer[allPlayers[player]].Up))
			{
				return -1;
			}

			if (Input.GetKeyDown(buttonsPerPlayer[allPlayers[player]].Down))
			{
				return 1;
			}

			return 0;
		}

		public static Vector2 GetMovementInput(int player)
		{
			return new Vector2(GetHorizontalInput(player), GetVerticalInput(player));
		}

		public static Vector2 GetMovementInputAndPlayer(out Player inputtingPlayer)
		{
			Vector2 input = Vector2.Zero;
			inputtingPlayer = default;

			for (int i = 0; i < allPlayers.Count; i++)
			{
				Vector2 move = GetMovementInput(i);

				if (move.RawLength() < input.RawLength())
				{
					continue;
				}
				
				input = move;
				inputtingPlayer = allPlayers[i];
			}

			return input;
		}

		public static bool GetActionKey(int player, int action)
		{
			return Input.GetKeyDown(buttonsPerPlayer[allPlayers[player]].ActionKeys[action - 1]);
		}

		public static bool GetActionKey(int action, out Player inputtingPlayer)
		{
			foreach (Player player in allPlayers)
			{
				if (GetActionKey(player.PlayerNumber, action))
				{
					inputtingPlayer = player;
					return true;
				}
			}

			inputtingPlayer = default;
			return false;
		}
	}
}