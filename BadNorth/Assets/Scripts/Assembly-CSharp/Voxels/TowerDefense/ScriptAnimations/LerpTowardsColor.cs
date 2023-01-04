using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000812 RID: 2066
	internal class LerpTowardsColor : ITargetAnimFuncs<Color>
	{
		// Token: 0x06003607 RID: 13831 RVA: 0x000E9153 File Offset: 0x000E7553
		public LerpTowardsColor(float lerp = 20f)
		{
			this.lerp = lerp;
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000E916D File Offset: 0x000E756D
		Color ITargetAnimFuncs<Color>.UpdateCurrent(Color current, Color target, float dt)
		{
			return Color.Lerp(current, target, dt * this.lerp);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000E9180 File Offset: 0x000E7580
		private float GetAbsDiff(Color c0, Color c1)
		{
			float a = 0f;
			a = Mathf.Max(a, Mathf.Abs(c0.r - c1.r));
			a = Mathf.Max(a, Mathf.Abs(c0.g - c1.g));
			a = Mathf.Max(a, Mathf.Abs(c0.b - c1.b));
			return Mathf.Max(a, Mathf.Abs(c0.a - c1.a));
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000E9200 File Offset: 0x000E7600
		bool ITargetAnimFuncs<Color>.ShouldTrigger(Color current, Color target)
		{
			return this.GetAbsDiff(current, target) < 0.1f;
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000E9211 File Offset: 0x000E7611
		bool ITargetAnimFuncs<Color>.IsDone(Color current, Color target)
		{
			return this.GetAbsDiff(current, target) < 0.001f;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000E9222 File Offset: 0x000E7622
		void ITargetAnimFuncs<Color>.OnActivate(Color current, Color target)
		{
		}

		// Token: 0x040024B9 RID: 9401
		[SerializeField]
		private float lerp = 10f;

		// Token: 0x040024BA RID: 9402
		public static LerpTowardsColor standard = new LerpTowardsColor(20f);
	}
}
