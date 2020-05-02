using GXPEngine.Core;
using Interfaces;
using Model.Item;

namespace View
{
	using GXPEngine;
	using Model;
	using System.Collections.Generic;
	using System.Drawing;

	//This Class draws the icons for the items in the store
	public class ShopView : Canvas, IShopView
	{
		private const int columns = 4;
		private const int spacing = 80;
		private const int margin = 18;

		private readonly ShopModel shop;

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  ShopView()
		//------------------------------------------------------------------------------------------------------------------------        
		public ShopView(ShopModel shop) : base(340, 340)
		{
			this.shop = shop;

			x = (game.width - width) / 2;
			y = (game.height - height) / 2;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Step()
		//------------------------------------------------------------------------------------------------------------------------        
		public void Step()
		{
			DrawBackground();
			DrawItems();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  MoveSelection()
		//------------------------------------------------------------------------------------------------------------------------        
		public int MoveSelection(Vector2 move)
		{
			return MoveSelection((int) move.x, (int) move.y);
		}
		
		public int MoveSelection(int moveX, int moveY)
		{
			int itemIndex = shop.GetSelectedItemIndex();
			int currentSelectionX = GetColumnByIndex(itemIndex);
			int currentSelectionY = GetRowByIndex(itemIndex);
			int requestedSelectionX = currentSelectionX + moveX;
			int requestedSelectionY = currentSelectionY + moveY;

			if (requestedSelectionX >= 0 && requestedSelectionX < columns) //check horizontal boundaries
			{
				int newItemIndex = GetIndexFromGridPosition(requestedSelectionX, requestedSelectionY);

				if (newItemIndex >= 0 && newItemIndex <= shop.GetItemCount()) //check vertical boundaries
				{
					return newItemIndex;
				}
			}

			return itemIndex;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetColumnByIndex()
		//------------------------------------------------------------------------------------------------------------------------        
		private static int GetIndexFromGridPosition(int column, int row)
		{
			return row * columns + column;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetColumnByIndex()
		//------------------------------------------------------------------------------------------------------------------------        
		private static int GetColumnByIndex(int index)
		{
			return index % columns;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetRowByIndex()
		//------------------------------------------------------------------------------------------------------------------------        
		private int GetRowByIndex(int index)
		{
			return index / columns; //rounds down
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  DrawBackground()
		//------------------------------------------------------------------------------------------------------------------------        
		private void DrawBackground()
		{
			graphics.Clear(Color.White);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  DrawItems()
		//------------------------------------------------------------------------------------------------------------------------        
		private void DrawItems()
		{
			List<AbstractItem> items = shop.GetAllItems();

			for (int index = 0; index < items.Count; index++)
			{
				AbstractItem item = items[index];
				int iconX = GetColumnByIndex(index) * spacing + margin;
				int iconY = GetRowByIndex(index) * spacing + margin;

				if (item == shop.GetSelectedItem())
				{
					DrawSelectedItem(item, iconX, iconY);
				}
				else
				{
					DrawItem(item, iconX, iconY);
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  DrawItem()
		//------------------------------------------------------------------------------------------------------------------------        
		private void DrawItem(IShopItemDrawer item, int iconX, int iconY)
		{
			item.DrawShopItem(graphics, iconX, iconY, shop);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  DrawSelectedItem()
		//------------------------------------------------------------------------------------------------------------------------        
		private void DrawSelectedItem(IShopItemDrawer item, int iconX, int iconY)
		{
			item.FlickerDraw(graphics, iconX, iconY, shop, 500);
		}
	}
}