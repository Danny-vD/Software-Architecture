using System.Collections.Generic;
using System.Linq;
using Events;
using Model.Item;
using VDFramework.EventSystem;

namespace Model.Inventory
{
	public class ShopInventory
	{
		private const float defaultPrice = 20.0f;

		public int Count => itemList.Count;

		public AbstractItem this[int index] => itemList[index].Item;

		private List<ShopItemData> itemList;

		public ShopInventory()
		{
			itemList = new List<ShopItemData>();
		}

		public bool Contains(AbstractItem item)
		{
			return itemList.Any(shopItemData => shopItemData.Item.Equals(item));
		}

		public uint GetAmountOfItem(AbstractItem item)
		{
			return Contains(item) ? GetItemFromList(item).Amount : 0u;
		}

		public int IndexOf(AbstractItem item)
		{
			for (int index = 0; index < itemList.Count; ++index)
			{
				if (itemList[index].Item.Equals(item))
				{
					return index;
				}
			}

			return -1;
		}

		public void IncreaseAmountOfItem(AbstractItem item, uint amountToIncrease)
		{
			if (!Contains(item))
			{
				Add(item, amountToIncrease);
				return;
			}

			GetItemFromList(item).Amount += amountToIncrease;
		}

		public bool DecreaseAmountOfItem(AbstractItem item, uint amountToDecrease)
		{
			ShopItemData itemToDecrease = GetItemFromList(item);

			if (itemToDecrease == null || itemToDecrease.Amount < amountToDecrease)
			{
				EventManager.Instance.RaiseEvent(new AddMessageEvent("The shop does not have enough of that item."));
				return false;
			}

			itemToDecrease.Amount -= amountToDecrease;
			return true;
		}

		public float GetPriceOfItem(AbstractItem item)
		{
			return GetItemFromList(item).Price;
		}

		public void SetPrice(AbstractItem item, uint price)
		{
			GetItemFromList(item).Price = price;
		}

		/// <summary>
		/// Returns a deep copy of all the items in the inventory
		/// </summary>
		public List<AbstractItem> GetCopyOfAllItems()
		{
			return itemList.Select(shopItemData => shopItemData.Item.Clone()).ToList();
		}

		public List<AbstractItem> GetAllItems()
		{
			return itemList.Select(shopItemData => shopItemData.Item).ToList();
		}

		private void Add(AbstractItem item, uint amount = 1)
		{
			itemList.Add(new ShopItemData(item.Clone(), defaultPrice, amount));
		}

		private ShopItemData GetItemFromList(AbstractItem item)
		{
			return itemList.FirstOrDefault(shopItemData => shopItemData.Item.Equals(item));
		}
	}
}