using System;
using System.Collections.Generic;
using Events;
using VDFramework.EventSystem;

namespace Utility
{
	public class MessageHandler
	{
		private readonly int maxMessageQueueCount; //it caches the last {x} messages

		private readonly List<string> messages = new List<string>();

		private readonly Action<string> messageHandlerFunction;

		public MessageHandler(int maxMessageQueueCount)
		{
			this.maxMessageQueueCount = maxMessageQueueCount;

			EventManager.Instance.AddListener<AddMessageEvent>(OnAddMessage);
		}

		public MessageHandler(Action<string> functionToInvoke)
		{
			messageHandlerFunction = functionToInvoke;
			EventManager.Instance.AddListener<AddMessageEvent>(OnAddMessage);
		}

		public void Destroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<AddMessageEvent>(OnAddMessage);
		}

		public string[] GetMessages()
		{
			return messages.ToArray();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  AddMessage()
		//------------------------------------------------------------------------------------------------------------------------
		//adds a message to the cache, cleaning it up if the limit is exceeded
		private void AddMessage(string message)
		{
			messages.Add(message);

			while (messages.Count > maxMessageQueueCount)
			{
				messages.RemoveAt(0);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  EventHandlers
		//------------------------------------------------------------------------------------------------------------------------
		private void OnAddMessage(AddMessageEvent addMessageEvent)
		{
			if (messageHandlerFunction != null)
			{
				messageHandlerFunction(addMessageEvent.Message);
				return;
			}

			AddMessage(addMessageEvent.Message);
		}
	}
}