using System.Collections.Generic;
using Enums;
using Interfaces.Factory;
using Model.Item;

namespace Model.ItemFactory
{	
	public class AbstractItemFactory
	{	
		private readonly Dictionary<ItemType, IConcreteItemFactory> factoryPerItemType =
			new Dictionary<ItemType, IConcreteItemFactory>()
			{
				{ItemType.Sword, new SwordFactory()},
				{ItemType.Axe, new AxeFactory()},
				{ItemType.Mace, new MaceFactory()},
				{ItemType.Flamethrower, new FlamethrowerFactory()},
			};

		public AbstractItem CreateItem(ItemType itemType)
		{	
			return factoryPerItemType[itemType].CreateItem();
		}

		public AbstractItem CreateEnhancedItem(ItemType itemType, int numberOfEnhancements)
		{
			return factoryPerItemType[itemType].CreateEnhancedItem(numberOfEnhancements);
		}
	}
}