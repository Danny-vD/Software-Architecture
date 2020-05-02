using System.Drawing;
using Model;

namespace Interfaces
{
	public interface IShopItemDrawer
	{
#if !UNITY_2017_1_OR_NEWER
		
		void DrawShopItem(Graphics graphics, int x, int y, ShopModel shop);

		void FlickerDraw(Graphics graphics, int x, int y, ShopModel shop, int timeBetweenFlicker);
#endif
	}
}