using Events;
using Interfaces.Player;
using Model.Item;
using PlayerScripts;
using UnityUI;
using VDFramework.EventSystem;
using VDFramework.UnityExtensions;

namespace View
{
	using UnityEngine;
	using UnityEngine.UI;
	using Model;
	using Controller;

	//------------------------------------------------------------------------------------------------------------------------
	//                                                  ShopController()
	//------------------------------------------------------------------------------------------------------------------------        
	public class ShopView : MonoBehaviour
	{
		[SerializeField] private LayoutGroup itemLayoutGroup = null;

		[SerializeField] private GameObject itemPrefab = null;

		[SerializeField] private Button buyButton = null;

		[SerializeField] private Button sellButton = null;

		private ShopModel shopModel;
		private ShopController shopController;

		private IBuyer player;

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Initialize()
		//------------------------------------------------------------------------------------------------------------------------        
		//this method is used to initialize the view, as we can't use a constructor (monobehaviour)
		public void Initialize(ShopModel shopModel, ShopController shopController)
		{
			this.shopModel = shopModel;
			this.shopController = shopController;

			player = new Player(0);

			RepopulateItemIconView(); //we need an Event system instead of this
			InitializeButtons();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  RepopulateItems()
		//------------------------------------------------------------------------------------------------------------------------        
		//clears the icon gridview and repopulates it with new icons (updates the visible icons)
		
		//TODO: make eventsystem
		private void RepopulateItemIconView()
		{
			ClearIconView();
			PopulateItemIconView();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  PopulateItems()
		//------------------------------------------------------------------------------------------------------------------------        
		//adds one icon for each item in the shop
		private void PopulateItemIconView()
		{
			foreach (AbstractItem item in shopModel.GetAllItems())
			{
				AddItemToView(item);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  ClearIconView()
		//------------------------------------------------------------------------------------------------------------------------        
		//remove all existing icons in the gridview
		private void ClearIconView()
		{
			itemLayoutGroup.transform.DestroyChildren();

//			Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
//			
//			foreach (Transform child in allIcons)
//			{
//				if (child != itemLayoutGroup.transform)
//				{
//					Destroy(child.gameObject);
//				}
//			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  AddItemToView()
		//------------------------------------------------------------------------------------------------------------------------        
		//Adds a new icon. An icon is a prefab Button with some additional scripts to link it to the store AbstractItem
		private void AddItemToView(AbstractItem item)
		{
			GameObject newItemIcon = Instantiate(itemPrefab, itemLayoutGroup.transform, true);
			newItemIcon.transform.localScale =
				Vector3.one; //The scale would automatically change in Unity so we set it back to Vector3.one.

			newItemIcon.name = item.GetName();

			ItemContainer itemContainer = newItemIcon.GetComponent<ItemContainer>();
			Debug.Assert(itemContainer != null);
			bool isSelected = (item == shopModel.GetSelectedItem());
			itemContainer.Initialize(item, isSelected, shopModel.GetAmountOfItem(item), shopModel.GetPrice(item));

			//Click behaviour for the button is done here. It seemed more convenient to do this inline than in the editor.
			Button itemButton = itemContainer.GetComponent<Button>();

			itemButton.onClick.AddListener(
				delegate
				{
					shopController.SelectItem(item);
					RepopulateItemIconView(); //we need an Event system instead of this
				}
			);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  InitializeButtons()
		//------------------------------------------------------------------------------------------------------------------------        
		//This method adds a listener to the 'Buy' and 'Sell' button. They are forwarded to the controller to the shop.
		private void InitializeButtons()
		{
			buyButton.onClick.AddListener(
				delegate
				{
					ShopController.Buy(player, shopModel.GetSelectedItem(), 1);
					RepopulateItemIconView(); //we need an Event system instead of this
				}
			);

			sellButton.onClick.AddListener(
				delegate
				{
					ShopController.Sell(player, shopModel.GetSelectedItem(), 1);
					RepopulateItemIconView(); //we need an Event system instead of this
				}
			);
		}
	}
}