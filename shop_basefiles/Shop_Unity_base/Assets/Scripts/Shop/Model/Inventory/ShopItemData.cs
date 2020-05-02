using System;
using Model.Item;

namespace Model
{
	public class ShopItemData : IEquatable<ShopItemData>
	{
		public readonly AbstractItem Item;

		public uint Amount { get; set; }
		public float Price { get; set; }

		public ShopItemData(AbstractItem item, float price, uint amount = 1)
		{
			Item = item;
			Amount = amount;
			Price = price;
		}

		bool IEquatable<ShopItemData>.Equals(ShopItemData other)
		{
			return other != null && Item.Equals(other.Item);
		}
	}
}