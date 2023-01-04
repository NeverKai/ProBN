using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007AF RID: 1967
	public static class IAgentOrderExtension
	{
		// Token: 0x060032F4 RID: 13044 RVA: 0x000D9C18 File Offset: 0x000D8018
		public static Vector3 GetOrderDir(this IAgentOrder order, NavPos navPos)
		{
			Vector3 zero = Vector3.zero;
			float num = 0f;
			order.SampleOrder(navPos, ref zero, ref num);
			return zero;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000D9C40 File Offset: 0x000D8040
		public static float GetOrderDist(this IAgentOrder order, NavPos navPos)
		{
			Vector3 zero = Vector3.zero;
			float result = 0f;
			order.SampleOrder(navPos, ref zero, ref result);
			return result;
		}
	}
}
