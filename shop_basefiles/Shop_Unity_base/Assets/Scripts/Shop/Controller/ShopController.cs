#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#else
using GXPEngine.Core;
#endif
using Events;
using Interfaces;
using Interfaces.Player;
using Model.Item;
using PlayerScripts;
using Utility;
using VDFramework.EventSystem;

namespace Controller
{
	using Model;

	//This class provides a controller for a ShopModel. The Controller acts as a public interface for a ShopModel.
	//These methods are being called by ShopView, as it implements the user interface. The exception is Initialize(),
	//it is being called by ShopState. We use Initialize() as a replacement for the constructor, as this class is a MonoBehaviour.
	public class ShopController
	{
		private readonly ShopModel shopModel;
		private readonly IShopView shopView;

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Initialize()
		//------------------------------------------------------------------------------------------------------------------------        
		//Ties this controller to a model
		public ShopController(ShopModel shopModel, IShopView shopView)
		{
			this.shopModel = shopModel;
			this.shopView = shopView;
			SelectFirstItem();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Step()
		//------------------------------------------------------------------------------------------------------------------------        
		public void Step()
		{
			HandleNavigation();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  HandleNavigation()
		//------------------------------------------------------------------------------------------------------------------------        
		private void HandleNavigation()
		{
			int itemIndex = shopView.MoveSelection(GetInput(out Player player));
			AbstractItem item = shopModel.GetItemByIndex(itemIndex);
			SelectItem(item);

			if (InputManager.GetActionKey(1, out player))
			{
				Buy(player, item, 1);
			}

			if (InputManager.GetActionKey(2, out player))
			{
				Sell(player, item, 1);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  GetInput()
		//------------------------------------------------------------------------------------------------------------------------
		private static Vector2 GetInput(out Player player)
		{
			return InputManager.GetMovementInputAndPlayer(out player);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  SelectItem()
		//------------------------------------------------------------------------------------------------------------------------
		//attempt to select an item
		public void SelectItem(AbstractItem item)
		{
			if (item != null)
			{
				shopModel.SelectItem(item);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  SelectFirstItem()
		//------------------------------------------------------------------------------------------------------------------------
		private void SelectFirstItem()
		{
			shopModel.SelectItemByIndex(0); //right now all this function does is select the first item in shopModel.
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Buy()
		//------------------------------------------------------------------------------------------------------------------------        
		public static void Buy(IBuyer buyer, AbstractItem item, uint amount)
		{
			EventManager.Instance.RaiseEvent(new BuyItemEvent(buyer, item, amount));
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Sell()
		//------------------------------------------------------------------------------------------------------------------------        
		public static void Sell(IBuyer buyer, AbstractItem item, uint amount)
		{
			EventManager.Instance.RaiseEvent(new SellItemEvent(buyer, item, amount));
		}
	}
}