using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007AE RID: 1966
	public interface IAgentOrder
	{
		// Token: 0x060032F0 RID: 13040
		void ApplyOrder();

		// Token: 0x060032F1 RID: 13041
		void ApplyWalk();

		// Token: 0x060032F2 RID: 13042
		void SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist);

		// Token: 0x060032F3 RID: 13043
		bool WantsControl();
	}
}
