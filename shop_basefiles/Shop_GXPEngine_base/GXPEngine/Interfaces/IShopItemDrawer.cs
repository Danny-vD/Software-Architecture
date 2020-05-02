using System.Drawing;
using Model;

namespace Interfaces
{
	public interface IShopItemDrawer
	{
		void DrawShopItem(Graphics graphics, int x, int y, ShopModel shop);

		void FlickerDraw(Graphics graphics, int x, int y, ShopModel shop, int timeBetweenFlicker);
	}
}