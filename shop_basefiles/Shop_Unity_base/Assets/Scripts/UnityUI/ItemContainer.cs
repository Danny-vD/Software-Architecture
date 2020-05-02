using Model.Item;
using UnityEngine;
using UnityEngine.UI;
using UnityUI.Helpers;
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
		private AbstractItem item;

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
			//store item
			this.item = item;

			caption.text = item.GetName();
			amountMarker.text = amountWriter.UpdateText(amountOfItem);
			priceTag.text = priceWriter.UpdateText(priceOfItem);

			//set checkmark visibility
			if (isSelected)
			{
				checkMark.SetActive(true);
			}

			//set button image
			Image image = GetComponentInChildren<Image>();
			Sprite sprite = SpriteCache.Get(item.IconName);

			if (sprite != null)
			{
				image.sprite = sprite;
			}
		}
	}
}