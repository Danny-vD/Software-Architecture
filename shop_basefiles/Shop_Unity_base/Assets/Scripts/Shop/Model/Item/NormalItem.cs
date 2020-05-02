using Model.Item;

namespace Model
{
	/// <summary>
	/// A default item without anything special
	/// </summary>
	public class NormalItem : AbstractItem
	{
		public NormalItem(string name, string iconName) : base(name, iconName)
		{
		}

		private NormalItem(AbstractItem itemToCopy) : base(itemToCopy)
		{
		}

		public override AbstractItem Clone()
		{
			return new NormalItem(this);
		}
	}
}