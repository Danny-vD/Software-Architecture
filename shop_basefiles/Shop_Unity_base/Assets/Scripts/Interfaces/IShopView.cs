#if UNITY_2017_1_OR_NEWER
using UnityEngine;

#else
using GXPEngine.Core;
#endif

namespace Interfaces
{
	public interface IShopView
	{
		int MoveSelection(Vector2 vector2);
	}
}