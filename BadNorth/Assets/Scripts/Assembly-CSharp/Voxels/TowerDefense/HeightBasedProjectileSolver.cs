using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007B7 RID: 1975
	public class HeightBasedProjectileSolver : MonoBehaviour, IProjectileSolver
	{
		// Token: 0x06003330 RID: 13104 RVA: 0x000DC018 File Offset: 0x000DA418
		public bool SolveProjectile(Vector2 targetDisplacement, float gravity, out Vector2 launchVelocity, out float timeToTarget)
		{
			float num = this.GetPeakHeight(targetDisplacement.y);
			if (num < targetDisplacement.y)
			{
				Debug.LogError("HeightBasedProjectileSolver.SolveProjectile failed to get a suitable peak height");
				num = targetDisplacement.y;
			}
			float num2;
			return ProjectileMath.ComputeProjectileWithPeakHeight(targetDisplacement, this.GetPeakHeight(targetDisplacement.y), gravity, out launchVelocity, out num2, out timeToTarget);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000DC06C File Offset: 0x000DA46C
		private float GetPeakHeight(float targetHeight)
		{
			float num = UnityEngine.Random.value * this.projectilePeakError;
			switch (this.peakMeasuredFrom)
			{
			case HeightBasedProjectileSolver.EPeakMeasurement.AboveLaunch:
				return this.projectilePeakHeight + num;
			case HeightBasedProjectileSolver.EPeakMeasurement.AboveTarget:
				return targetHeight + this.projectilePeakHeight + num;
			case HeightBasedProjectileSolver.EPeakMeasurement.AboveHighest:
				return Mathf.Max(targetHeight, 0f) + this.projectilePeakHeight + num;
			default:
				return targetHeight;
			}
		}

		// Token: 0x040022D5 RID: 8917
		[SerializeField]
		private float projectilePeakHeight = 1f;

		// Token: 0x040022D6 RID: 8918
		[SerializeField]
		private float projectilePeakError;

		// Token: 0x040022D7 RID: 8919
		[SerializeField]
		private HeightBasedProjectileSolver.EPeakMeasurement peakMeasuredFrom = HeightBasedProjectileSolver.EPeakMeasurement.AboveHighest;

		// Token: 0x020007B8 RID: 1976
		private enum EPeakMeasurement
		{
			// Token: 0x040022D9 RID: 8921
			AboveLaunch,
			// Token: 0x040022DA RID: 8922
			AboveTarget,
			// Token: 0x040022DB RID: 8923
			AboveHighest
		}
	}
}
