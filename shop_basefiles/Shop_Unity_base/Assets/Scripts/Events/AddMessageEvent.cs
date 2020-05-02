using VDFramework.EventSystem;

namespace Events
{
	public class AddMessageEvent : VDEvent
	{
		public readonly string Message;

		public AddMessageEvent(string message)
		{
			Message = message;
		}
	}
}