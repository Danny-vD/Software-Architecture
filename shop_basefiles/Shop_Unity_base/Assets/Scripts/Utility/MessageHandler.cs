using System;
using System.Collections.Generic;
using Events;
using VDFramework.EventSystem;

namespace Utility
{
	/// <summary>
	/// A facade-ish class to handle messages
	/// </summary>
	public class MessageHandler
	{
		private readonly int maxMessageQueueCount; //it caches the last {x} messages

		private readonly List<string> messages = new List<string>();

		private readonly Action<string> messageHandlerFunction;

		/// <summary>
		/// Initialise a messageHandler that caches messages which can be retrieved later
		/// </summary>
		/// <param name="maxMessageQueueCount">How many messages should be cached
		/// <para>(The length of the GetMessages() array)</para>
		/// </param>
		public MessageHandler(int maxMessageQueueCount)
		{
			this.maxMessageQueueCount = maxMessageQueueCount;

			EventManager.Instance.AddListener<AddMessageEvent>(OnAddMessage);
		}

		/// <summary>
		/// Initialise a messageHandler which invokes a given function each time it receives a new message
		/// </summary>
		/// <param name="functionToInvoke">The message to invoke when a new message is added</param>
		public MessageHandler(Action<string> functionToInvoke)
		{
			messageHandlerFunction = functionToInvoke;
			EventManager.Instance.AddListener<AddMessageEvent>(OnAddMessage);
		}

		/// <summary>
		/// A replacement for Destroy(Component) because this class is not a component
		/// <para>Will cause the class to clean itself up</para>
		/// </summary>
		public void Destroy()
		{
			if (!EventManager.IsInitialized)
			{ 
				return;
			}

			EventManager.Instance.RemoveListener<AddMessageEvent>(OnAddMessage);
		}

		/// <summary>
		/// Retrieves all cached messages
		/// </summary>
		/// <returns>a string array that contains all the cached messages</returns>
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