using System;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007C1 RID: 1985
	[Serializable]
	public class ProjectileSettings
	{
		// Token: 0x0400230C RID: 8972
		[Delayed]
		public float minSpeed = 4f;

		// Token: 0x0400230D RID: 8973
		[Delayed]
		public float maxSpeed = 12f;

		// Token: 0x0400230E RID: 8974
		[Delayed]
		public float drag;

		// Token: 0x0400230F RID: 8975
		[Delayed]
		public float startOffset;

		// Token: 0x04002310 RID: 8976
		[Delayed]
		public float gravity = 9.8f;
	}
}
