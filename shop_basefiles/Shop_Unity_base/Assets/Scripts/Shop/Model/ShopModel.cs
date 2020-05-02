using System;
using VDFramework.Extensions;
using System.Collections.Generic;
using System.Linq;
using Enums;
using VDFramework.EventSystem;
using Events;
using Interfaces.Player;
using Model.Inventory;
using Model.Item;
using Model.ItemFactory;
using VDFramework.Utility;

namespace Model
{
	//This class holds the model of our Shop. It contains an Inventory. In its current setup, view and controller need to get
	//data via polling. Advisable is, to set up an event system for better integration with View and Controller.
	public class ShopModel
	{
		private static readonly Random random = new Random();
		
		private readonly ShopInventory inventory = new ShopInventory(); //items in the store
		private readonly AbstractItemFactory itemFactory = new AbstractItemFactory();

		private int selectedItemIndex; //selected item index

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  ShopModel()
		//------------------------------------------------------------------------------------------------------------------------        
		public ShopModel()
		{
			AddListeners();
			PopulateInventory(32U, 5); //currently, it has 16 of every item
		}

		public void Destroy()
		{
			RemoveListeners();
		}

		private void AddListeners()
		{
			// maxValue - 1 to allow another class to be called first, if necessary. While ensuring that we'd be the first in all other cases
			EventManager.Instance.AddListener<BuyItemEvent>(OnBuyItem, int.MaxValue - 1);
			EventManager.Instance.AddListener<SellItemEvent>(OnSellItem, int.MaxValue - 1);
		}

		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<BuyItemEvent>(OnBuyItem);
			EventManager.Instance.RemoveListener<SellItemEvent>(OnSellItem);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  PopulateInventory()
		//------------------------------------------------------------------------------------------------------------------------        
		private void PopulateInventory(uint itemCount, uint amountPerItem)
		{
			ItemType[] itemTypes = default(ItemType).GetValues().RandomSort().ToArray();

			for (int index = 0; index < itemCount; index++)
			{
				AbstractItem item = RandomUtil.RandomBool()
					? itemFactory.CreateItem(itemTypes[index % itemTypes.Length])
					: itemFactory.CreateEnhancedItem(itemTypes[index % itemTypes.Length], random.Next(1, 3));

				inventory.IncreaseAmountOfItem(item, amountPerItem);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetSelectedItem()
		//------------------------------------------------------------------------------------------------------------------------        
		//returns the selected item
		public AbstractItem GetSelectedItem()
		{
			if (selectedItemIndex >= 0 && selectedItemIndex < inventory.Count)
			{
				return inventory[selectedItemIndex];
			}

			return null;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  SelectItem()
		//------------------------------------------------------------------------------------------------------------------------
		//attempts to select the given item, fails silently
		public void SelectItem(AbstractItem item)
		{
			if (item == null)
			{
				return;
			}

			int index = inventory.IndexOf(item);

			if (index >= 0)
			{
				selectedItemIndex = index;
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  SelectItemByIndex()
		//------------------------------------------------------------------------------------------------------------------------        
		//attempts to select the item, specified by 'index', fails silently
		public void SelectItemByIndex(int index)
		{
			if (index >= 0 && index < inventory.Count)
			{
				selectedItemIndex = index;
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetSelectedItemIndex()
		//------------------------------------------------------------------------------------------------------------------------
		//returns the index of the current selected item
		public int GetSelectedItemIndex()
		{
			return selectedItemIndex;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetItems()
		//------------------------------------------------------------------------------------------------------------------------        
		//returns a list with copies of all current items in the shop.
		public List<AbstractItem> GetCopyOfItems()
		{
			return inventory.GetCopyOfAllItems(); //returns a deep copy of the list
		}

		// returns a list with all the current items in the shop
		public List<AbstractItem> GetAllItems()
		{
			return inventory.GetAllItems();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetAmountOfItem()
		//------------------------------------------------------------------------------------------------------------------------        
		//returns the amount of a specific item
		public uint GetAmountOfItem(AbstractItem item)
		{
			return inventory.GetAmountOfItem(item);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetItemCount()
		//------------------------------------------------------------------------------------------------------------------------        
		//returns the number of items
		public int GetItemCount()
		{
			return inventory.Count;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetItemByIndex()
		//------------------------------------------------------------------------------------------------------------------------        
		//tries to get an item, specified by index. returns null if unsuccessful
		public AbstractItem GetItemByIndex(int index)
		{
			if (index >= 0 && index < inventory.Count)
			{
				return inventory[index];
			}

			return null;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Buy()
		//------------------------------------------------------------------------------------------------------------------------        
		//not fully implemented yet
		public float GetPrice(AbstractItem item)
		{
			return inventory.GetPriceOfItem(item);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Buy()
		//------------------------------------------------------------------------------------------------------------------------        
		//not fully implemented yet
		private bool Buy(IBuyer buyer, AbstractItem item, uint amountToBuy = 1)
		{
			if (item == null)
			{
				EventManager.Instance.RaiseEvent(new AddMessageEvent("The Item you are trying to sell is null."));
				EventManager.Instance.RaiseEvent(new TransactionFailedEvent(buyer));
				return false;
			}

			//NOTE: to check whether the buyer (seller) actually had enough of that item,
			// we could listen to the "SellItemEvent" in the inventory first and "Consume()" the event if it's not valid
			buyer.RemoveItem(item, amountToBuy);
			inventory.IncreaseAmountOfItem(item, amountToBuy);
			EventManager.Instance.RaiseEvent(new TransactionSucceededEvent(buyer, item, amountToBuy));

			return true;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Sell()
		//------------------------------------------------------------------------------------------------------------------------        
		//not fully implemented yet
		private bool Sell(IBuyer buyer, AbstractItem item, uint amountToSell = 1)
		{
			if (item == null)
			{
				EventManager.Instance.RaiseEvent(new AddMessageEvent("The Item you are trying to buy is null."));
				EventManager.Instance.RaiseEvent(new TransactionFailedEvent(buyer));
				return false;
			}

			if (inventory.DecreaseAmountOfItem(item, amountToSell))
			{
				buyer.AddItem(item.Clone(), amountToSell);
				EventManager.Instance.RaiseEvent(new TransactionSucceededEvent(buyer, item, amountToSell));
				return true;
			}

			EventManager.Instance.RaiseEvent(new TransactionFailedEvent(buyer));
			return false;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  EventHandlers
		//------------------------------------------------------------------------------------------------------------------------
		private void OnBuyItem(BuyItemEvent buyItemEvent)
		{
			if (!Sell(buyItemEvent.Buyer, buyItemEvent.Item, buyItemEvent.Amount))
			{
				buyItemEvent.Consume();
			}
		}

		private void OnSellItem(SellItemEvent sellItemEvent)
		{
			if (!Buy(sellItemEvent.Buyer, sellItemEvent.Item, sellItemEvent.Amount))
			{
				sellItemEvent.Consume();
			}
		}
	}
}