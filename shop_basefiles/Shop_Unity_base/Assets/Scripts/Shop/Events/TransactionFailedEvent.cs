using Interfaces.Player;
using VDFramework.EventSystem;

namespace Events
{
	public class TransactionFailedEvent : VDEvent
	{
		public readonly IBuyer Buyer;

		public TransactionFailedEvent(IBuyer buyer)
		{
			Buyer = buyer;
		}
	}
}