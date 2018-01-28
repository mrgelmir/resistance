using UnityEngine;

namespace Resistance.Helpers
{
	public static class TransformHelpers
	{
		public static void DestroyChildren(this Transform transform)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject.Destroy(transform.GetChild(i).gameObject);
			}
		}

	}
}
