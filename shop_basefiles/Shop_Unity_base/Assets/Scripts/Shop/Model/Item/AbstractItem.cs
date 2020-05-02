using System;
using VDFramework.Extensions;
using System.Drawing;
using Interfaces;
using View;

#pragma warning disable 660,661

namespace Model.Item
{
	//This class holds data for an Item. Currently it has a name and an iconName.
	public abstract class AbstractItem : IEquatable<AbstractItem>
#if !UNITY_2017_1_OR_NEWER
		, IShopItemDrawer
#endif
	{
		public readonly string IconName;
		private readonly string name;

#if !UNITY_2017_1_OR_NEWER
		private readonly ShopItemDrawer itemDrawer;
#endif

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  AbstractItem()
		//------------------------------------------------------------------------------------------------------------------------
		protected AbstractItem(string name, string iconName)
		{
			this.name = name;
			IconName = iconName;

#if !UNITY_2017_1_OR_NEWER
			itemDrawer = new ShopItemDrawer(this);
#endif
		}

		protected AbstractItem(AbstractItem itemToCopy) // Copy contructor
		{
			name = itemToCopy.name;
			IconName = itemToCopy.IconName;

#if !UNITY_2017_1_OR_NEWER
			itemDrawer = new ShopItemDrawer(itemToCopy.itemDrawer, this);
#endif
		}

		public abstract AbstractItem Clone();

		public virtual string GetName()
		{
			return name.InsertSpaceBeforeCapitals();
		}

#if !UNITY_2017_1_OR_NEWER
		public void DrawShopItem(Graphics graphics, int x, int y, ShopModel shop)
		{
			itemDrawer.DrawShopItem(graphics, x, y, shop);
		}

		public void FlickerDraw(Graphics graphics, int x, int y, ShopModel shop, int timeBetweenFlicker)
		{
			itemDrawer.FlickerDraw(graphics, x, y, shop, timeBetweenFlicker);
		}
#endif

		//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
		//					Operator overloading
		//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//\\//
		public bool Equals(AbstractItem other)
		{
			return !(other is null) && GetName() == other.GetName();
		}

		public static bool operator ==(AbstractItem lhs, AbstractItem rhs)
		{
			if (lhs is null)
			{
				return rhs is null;
			}

			return (rhs is object) && lhs.GetName() == rhs.GetName();
		}

		public static bool operator !=(AbstractItem lhs, AbstractItem rhs)
		{
			return !(lhs == rhs);
		}
	}
}