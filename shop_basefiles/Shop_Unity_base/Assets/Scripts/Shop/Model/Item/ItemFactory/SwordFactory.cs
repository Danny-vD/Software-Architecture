using Interfaces.Factory;
using Model.Item;

namespace Model.ItemFactory
{
	internal class SwordFactory : IConcreteItemFactory
	{
		private const string itemName = "Sword";
		private const string iconName = "Item";
		
		public AbstractItem CreateItem()
		{
			return new NormalItem(itemName, iconName);
		}

		public AbstractItem CreateEnhancedItem(int numberOfEnhancements)
		{
			EnhancedItem item = new EnhancedItem(itemName, iconName); 
			
			item.AddRandomEnhancements(numberOfEnhancements);
			return item;
		}
	}
}