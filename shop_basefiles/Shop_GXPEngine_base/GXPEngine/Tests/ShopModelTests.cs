using Enums;
using Events;
using Interfaces.Player;
using Model;
using Model.Item;
using Model.ItemFactory;
using NUnit.Framework;
using PlayerScripts;
using VDFramework.EventSystem;
using VDFramework.Extensions;

// Attributes:
// [TestFixture]
// [SetUp]
// [Test]
// [ignore]

namespace Tests
{
	[TestFixture]
	public class ShopModelTests
	{
		private readonly AbstractItemFactory itemFactory = new AbstractItemFactory();

		private IBuyer defaultBuyer;
		private ShopModel shopModel;

		private AbstractItem itemToBePassedAround;

		[SetUp]
		public void TestSetup()
		{
			defaultBuyer = new Player(0);
			
			shopModel = new ShopModel();
			itemToBePassedAround = itemFactory.CreateItem(default(ItemType).GetRandomValue());
		}

		[Test]
		public void SellItemToShopTest()
		{
			uint oldValue = shopModel.GetAmountOfItem(itemToBePassedAround);
			const uint amountToBuy = 5;

			EventManager.Instance.RaiseEvent(new SellItemEvent(defaultBuyer, itemToBePassedAround, amountToBuy));
			EventManager.Instance.RaiseEvent(new SellItemEvent(defaultBuyer, itemToBePassedAround, amountToBuy));
			
			uint newValue = shopModel.GetAmountOfItem(itemToBePassedAround);

			Assert.That(newValue == (oldValue + amountToBuy * 2));
		}

		[Test]
		public void BuyItemFromShopTest()
		{
			uint oldValue = shopModel.GetAmountOfItem(itemToBePassedAround);
			const uint amountToBuy = 5;

			EventManager.Instance.RaiseEvent(new SellItemEvent(defaultBuyer, itemToBePassedAround, amountToBuy));
			EventManager.Instance.RaiseEvent(new SellItemEvent(defaultBuyer, itemToBePassedAround, amountToBuy));

			oldValue += amountToBuy * 2;

			const uint amountToSell = 2;

			EventManager.Instance.RaiseEvent(new BuyItemEvent(defaultBuyer, itemToBePassedAround, amountToSell));
			
			uint newValue = shopModel.GetAmountOfItem(itemToBePassedAround);

			Assert.That(newValue == (oldValue - amountToSell));
		}

		[Test]
		public void BuyNullTest()
		{
			Assert.DoesNotThrow(BuyNull);

			// Local function
			void BuyNull()
			{
				EventManager.Instance.RaiseEvent(new BuyItemEvent(defaultBuyer, null, 1));
			}
		}

		[Test]
		public void SellNullTest()
		{
			Assert.DoesNotThrow(SellNull);

			// Local function
			void SellNull()
			{
				EventManager.Instance.RaiseEvent(new SellItemEvent(defaultBuyer, null, 1));
			}
		}

		[Test]
		public void NormalItemCreationTest()
		{
			AbstractItem item = itemFactory.CreateItem(ItemType.Axe);

			Assert.That(item is NormalItem);
		}

		[Test]
		public void EnhancedItemCreationTest()
		{
			AbstractItem item = itemFactory.CreateEnhancedItem(ItemType.Sword, 3);

			Assert.That(item is EnhancedItem);
		}

		[Test]
		public void FactoriesCreateDifferentItemsTest()
		{
			AbstractItem item = itemFactory.CreateItem(ItemType.Mace);
			AbstractItem anotherItem = itemFactory.CreateEnhancedItem(ItemType.Mace, 3);

			Assert.That(!ReferenceEquals(item, anotherItem));
		}
	}
}