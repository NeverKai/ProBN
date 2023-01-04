using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A6 RID: 1958
	public interface IPathTarget
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060032AF RID: 12975
		NavPos navPos { get; }

		// Token: 0x060032B0 RID: 12976
		float GetDistanceFrom(NavPos navPos);

		// Token: 0x060032B1 RID: 12977
		void SampleDistanceDir(NavPos navPos, ref Vector3 dir, ref float dist);

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060032B2 RID: 12978
		Bounds endPointBounds { get; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060032B3 RID: 12979
		Vector3 endPointPosition { get; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060032B4 RID: 12980
		Mesh endPointMesh { get; }
	}
}
