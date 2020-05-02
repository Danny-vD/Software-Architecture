using Interfaces.Player;
using Model;
using Model.Item;
using VDFramework.EventSystem;

namespace Events
{
	public class TransactionSucceededEvent : VDEvent
	{
		public readonly IBuyer Buyer;
		public readonly AbstractItem TradedItem;
		public readonly uint AmountTraded;

		public TransactionSucceededEvent(IBuyer buyer, AbstractItem item, uint amountTraded)
		{
			Buyer = buyer;
			TradedItem = item.Clone();
			AmountTraded = amountTraded;
		}
	}
}