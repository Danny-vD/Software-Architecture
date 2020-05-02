using Model.Item;

namespace Interfaces.Factory
{
	public interface IConcreteItemFactory
	{	
		AbstractItem CreateItem();

		AbstractItem CreateEnhancedItem(int numberOfEnhancements);
	}
}