using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C2 RID: 1730
	public interface ILevelCamera
	{
		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002CD0 RID: 11472
		Camera cameraRef { get; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06002CD1 RID: 11473
		// (set) Token: 0x06002CD2 RID: 11474
		float orthoHeight { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06002CD3 RID: 11475
		// (set) Token: 0x06002CD4 RID: 11476
		float yaw { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002CD5 RID: 11477
		// (set) Token: 0x06002CD6 RID: 11478
		Vector3 position { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002CD7 RID: 11479
		Rect screenRect { get; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002CD8 RID: 11480
		Rect defaultScreenRect { get; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002CD9 RID: 11481
		bool lockPanY { get; }
	}
}
