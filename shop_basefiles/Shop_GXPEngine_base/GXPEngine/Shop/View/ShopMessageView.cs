using System.Collections.Generic;
using GXPEngine;
using Model;
using System.Drawing;
using Utility;

namespace View
{
	//This class will draw a messagebox containing messages from the Shop that is observed.
	public class ShopMessageView : Canvas
	{
		private const int fontHeight = 20;
		private MessageHandler messageHandler;

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  ShopMessageDisplay()
		//------------------------------------------------------------------------------------------------------------------------
		public ShopMessageView() : base(800, 100)
		{
			messageHandler = new MessageHandler(4);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Step()
		//------------------------------------------------------------------------------------------------------------------------
		public void Step()
		{
			DrawBackground();
			DrawMessages();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  OnDestroy()
		//------------------------------------------------------------------------------------------------------------------------
		protected override void OnDestroy()
		{
			messageHandler.Destroy();
			messageHandler = null;
			
			base.OnDestroy();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  DrawBackground()
		//------------------------------------------------------------------------------------------------------------------------
		//Draw background color
		private void DrawBackground()
		{
			graphics.Clear(Color.White);
			graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, game.width, fontHeight));
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  DrawMessages()
		//------------------------------------------------------------------------------------------------------------------------
		//Draw messages onto this messagebox
		private void DrawMessages()
		{
			graphics.DrawString("Use ARROWKEYS to navigate. Press SPACE to buy, BKSPACE to sell.",
				SystemFonts.CaptionFont, Brushes.White, 0, 0);

			string[] messagesArray = GetMessages();

			for (int index = 0; index < messagesArray.Length; index++)
			{
				string message = messagesArray[index];
				graphics.DrawString(message, SystemFonts.CaptionFont, Brushes.Black, 0,
					fontHeight + index * fontHeight);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetMessage()
		//------------------------------------------------------------------------------------------------------------------------        
		//returns the cached list of messages from the messageHandler
		private string[] GetMessages()
		{
			return messageHandler.GetMessages();
		}
	}
}