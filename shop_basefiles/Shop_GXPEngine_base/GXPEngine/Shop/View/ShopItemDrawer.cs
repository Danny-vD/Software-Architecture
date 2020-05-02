using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using GXPEngine;
using GXPEngine.Core;
using Interfaces;
using Model;
using Model.Item;

namespace View
{
	public class ShopItemDrawer : IShopItemDrawer
	{
		private static readonly Dictionary<string, Texture2D> iconCache = new Dictionary<string, Texture2D>();

		private readonly AbstractItem item;

		private int timePassed;
		private bool shouldDraw;

		public ShopItemDrawer(AbstractItem itemToDraw)
		{
			item = itemToDraw;
		}

		public ShopItemDrawer(ShopItemDrawer itemDrawerToCopy, AbstractItem itemToDraw)
		{
			item = itemToDraw;

			timePassed = itemDrawerToCopy.timePassed;
			shouldDraw = itemDrawerToCopy.shouldDraw;
		}

		public void DrawShopItem(Graphics graphics, int x, int y, ShopModel shop)
		{
			Texture2D iconTexture = GetCachedTexture(item.IconName);
			graphics.DrawImage(iconTexture.bitmap, x, y);

			DrawName();
			DrawPrice();
			DrawAmount();

			void DrawName()
			{
				graphics.DrawString(item.GetName(), SystemFonts.CaptionFont, Brushes.Black, x + 8, y + 8);
			}

			void DrawPrice()
			{
				graphics.DrawString($"price: {shop.GetPrice(item).ToString(CultureInfo.InvariantCulture)}",
					SystemFonts.CaptionFont,
					Brushes.Black, x + 8, y + 20);
			}

			void DrawAmount()
			{
				graphics.DrawString($"Amount: {shop.GetAmountOfItem(item).ToString(CultureInfo.InvariantCulture)}",
					SystemFonts.CaptionFont, Brushes.Black, x + 8, y + 30);
			}
		}

		public void FlickerDraw(Graphics graphics, int x, int y, ShopModel shop, int timeBetweenFlicker)
		{
			timePassed += Time.deltaTime;

			if (timePassed >= timeBetweenFlicker)
			{
				timePassed = 0;
				shouldDraw ^= true;
			}

			if (shouldDraw)
			{
				DrawShopItem(graphics, x, y, shop);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetCachedTexture()
		//------------------------------------------------------------------------------------------------------------------------        
		private static Texture2D GetCachedTexture(string filename)
		{
			if (!iconCache.ContainsKey(filename))
			{
				iconCache.Add(filename, new Texture2D("media/" + filename + ".png"));
			}

			return iconCache[filename];
		}
	}
}