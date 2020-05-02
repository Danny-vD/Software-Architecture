using Events;
using Model.Item;
using UnityEngine;
using UnityEngine.UI;
using UnityUI.Helpers;
using VDFramework.EventSystem;
using VDFramework.Utility;

//This class is applied to a button that represents an Item in the View. It is a visual representation of the item
//when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to the one specified in the Item data,
//and it enables or disables the checkbox to indicate if the Item is selected.
namespace UnityUI
{
	public class ItemContainer : MonoBehaviour
	{
		//link to the checkmark image (set in prefab)
		[SerializeField] private GameObject checkMark = null;

		[SerializeField] private Text caption = null;
		[SerializeField] private Text amountMarker = null;
		[SerializeField] private Text priceTag = null;

		//link to the original item (set in Initialize)
		private uint amount;

		private StringVariableWriter amountWriter;
		private StringVariableWriter priceWriter;
		

		private void Awake()
		{
			amountWriter = new StringVariableWriter(amountMarker.text);
			priceWriter = new StringVariableWriter(priceTag.text);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Initialize()
		//------------------------------------------------------------------------------------------------------------------------
		public void Initialize(AbstractItem item, bool isSelected, uint amountOfItem, float priceOfItem)
		{
			amount = amountOfItem;
			
			caption.text = item.GetName();
			amountMarker.text = amountWriter.UpdateText(amountOfItem);
			priceTag.text = priceWriter.UpdateText(priceOfItem);

			//set checkmark visibility
			SetSelected(isSelected);

			//set button image
			Image image = GetComponentInChildren<Image>();
			Sprite sprite = SpriteCache.Get(item.IconName);

			if (sprite != null)
			{
				image.sprite = sprite;
			}
		}

		private void OnDestroy()
		{
			RemoveListeners();
		}

		private void AddListeners()
		{
			EventManager.Instance.AddListener<SelectedItemEvent>(OnSelectedItem);
			EventManager.Instance.AddListener<BuyItemEvent>(OnBuyItem);
			EventManager.Instance.AddListener<SellItemEvent>(OnSellItem);
		}
		
		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
    
			EventManager.Instance.RemoveListener<SelectedItemEvent>(OnSelectedItem);
			EventManager.Instance.RemoveListener<BuyItemEvent>(OnBuyItem);
			EventManager.Instance.RemoveListener<SellItemEvent>(OnSellItem);
		}

		public void SetSelected(bool isSelected)
		{
			checkMark.SetActive(isSelected);

			if (isSelected)
			{
				AddListeners();
			}
			else
			{
				RemoveListeners();
			}
		}

		private void UpdateAmount(uint newAmount)
		{
			amountMarker.text = amountWriter.UpdateText(newAmount);
		}

		private void UpdatePrice(uint newPrice)
		{
			priceTag.text = priceWriter.UpdateText(newPrice);
		}
		
		private void OnSelectedItem()
		{
			SetSelected(false);
		}

		private void OnBuyItem(BuyItemEvent buyItemEvent)
		{
			// We buy from the shop, so the shop item decreases in amount
			amount -= buyItemEvent.Amount;
			UpdateAmount(amount);
		}

		private void OnSellItem(SellItemEvent sellItemEvent)
		{
			// We sell to the shop, so the shop item increases in amount
			amount += sellItemEvent.Amount;
			UpdateAmount(amount);
		}
	}
}