using System;
using System.Linq;
using Utility;

namespace View
{
	using UnityEngine;
	using Model;

	public class ShopMessageView : MonoBehaviour
	{
		private MessageHandler messageHandler;

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Initialize()
		//------------------------------------------------------------------------------------------------------------------------
		//This method is used to initialize values, because we can't use a constructor.
		public void Initialize()
		{
			messageHandler = new MessageHandler(Debug.Log);
		}

		private void OnDestroy()
		{
			messageHandler.Destroy();
		}
	}
}