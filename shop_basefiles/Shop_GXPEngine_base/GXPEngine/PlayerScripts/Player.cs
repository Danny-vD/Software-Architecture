using Interfaces.Player;
using Model.Item;

namespace PlayerScripts
{
	/// <summary>
	/// Empty class, doesn't need anything
	/// but could have a wallet or inventory or anything you want, it doesn't matter
	/// </summary>
	public class Player : IBuyer
	{
		public readonly int PlayerNumber;
		public readonly string Name;

		public Player(int playerNumber, string name = "John Doe")
		{
			PlayerNumber = playerNumber;
			Name = name;
		}

		public void AddItem(AbstractItem item, uint amount = 1)
		{
			// Empty
		}

		public void RemoveItem(AbstractItem item, uint amount = 1)
		{
			// Empty
		}
	}
}