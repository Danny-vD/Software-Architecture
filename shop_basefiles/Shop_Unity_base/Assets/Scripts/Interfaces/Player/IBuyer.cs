using Model.Item;

namespace Interfaces.Player
{
	/// <summary>
	/// Used by the shop to take in a reference to whoever tries to buy/sell something to allow for multiplayer
	/// </summary>
	public interface IBuyer
	{
		void AddItem(AbstractItem item, uint amount = 1);

		void RemoveItem(AbstractItem item, uint amount = 1);
	}
}