using System.Collections.Generic;
using System.Text;
using Enums;
using Interfaces;
using Model.Item;
using VDFramework.Extensions;

namespace Model
{
	public class EnhancedItem : AbstractItem, IEnhanceable
	{
		private readonly List<Enhancement> enhancements;

		public EnhancedItem(string name, string iconName) : base(name, iconName)
		{
			enhancements = new List<Enhancement>();
		}

		private EnhancedItem(EnhancedItem itemToCopy) : base(itemToCopy)
		{
			enhancements = new List<Enhancement>(itemToCopy.enhancements);
		}

		public override AbstractItem Clone()
		{
			return new EnhancedItem(this);
		}

		public override string GetName()
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty);

			int count = enhancements.Count;
			int countMinusOne = count - 1;
			
			for (int i = 0; i < countMinusOne; i++)
			{
				Enhancement enhancement = enhancements[i];

				stringBuilder.Append($"{enhancement} ");
			}

			stringBuilder.Append($"{base.GetName()}");

			if (count > 0)
			{
				stringBuilder.Append($" of {enhancements[countMinusOne]}");
			}

			return stringBuilder.ToString();
		}

		public void AddEnhancement(Enhancement enhancement)
		{
			if (!HasEnhancement(enhancement))
			{
				enhancements.Add(enhancement);
			}
		}

		public void RemoveEnhancement(Enhancement enhancement)
		{
			enhancements.Remove(enhancement);
		}

		public bool HasEnhancement(Enhancement enhancement)
		{
			return enhancements.Contains(enhancement);
		}

		public void AddRandomEnhancements(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				AddEnhancement(default(Enhancement).GetRandomValue());
			}
		}
	}
}