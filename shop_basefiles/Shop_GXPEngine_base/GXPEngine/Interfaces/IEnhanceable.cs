using Enums;

namespace Interfaces
{
	public interface IEnhanceable
	{
		void AddEnhancement(Enhancement enhancement);
		
		void RemoveEnhancement(Enhancement enhancement);
		
		bool HasEnhancement(Enhancement enhancement);
		
		void AddRandomEnhancements(int amount);
	}
}