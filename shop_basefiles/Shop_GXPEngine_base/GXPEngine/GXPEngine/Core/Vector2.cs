using System;

namespace GXPEngine.Core
{
	public struct Vector2
	{
		public static Vector2 Zero => new Vector2(0, 0);
		public static Vector2 One => new Vector2(1, 1);

		public float x;
		public float y;

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public float RawLength()
		{
			return x * x + y * y;
		}

		public float Length()
		{
			return Mathf.Sqrt(x * x + y * y);
		}

		public override string ToString()
		{
			return "[Vector2 " + x + ", " + y + "]";
		}
	}
}