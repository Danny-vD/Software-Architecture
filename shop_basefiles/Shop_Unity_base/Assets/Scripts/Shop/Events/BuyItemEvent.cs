using Interfaces.Player;
using Model;
using Model.Item;
using VDFramework.EventSystem;

namespace Events
{
	public class BuyItemEvent : VDEvent
	{
		public readonly IBuyer Buyer;
		public readonly AbstractItem Item;
		public readonly uint Amount;

		public BuyItemEvent(IBuyer buyer, AbstractItem item, uint amount)
		{
			Buyer = buyer;
			Item = item;
			Amount = amount;
		}
	}
}