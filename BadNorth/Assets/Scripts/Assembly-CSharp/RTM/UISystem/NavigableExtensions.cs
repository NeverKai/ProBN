using System;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004D1 RID: 1233
	public static class NavigableExtensions
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x00053554 File Offset: 0x00051954
		public static Vector3 GetPosition(this IUINavigable navigable)
		{
			return navigable.transform.position;
		}
	}
}
