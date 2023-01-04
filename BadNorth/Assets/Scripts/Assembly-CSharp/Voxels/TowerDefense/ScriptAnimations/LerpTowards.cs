using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x0200080F RID: 2063
	[Serializable]
	internal class LerpTowards : ITargetAnimFuncs<float>
	{
		// Token: 0x060035F5 RID: 13813 RVA: 0x000E8F56 File Offset: 0x000E7356
		public LerpTowards(float lerp = 14f, float moveTowards = 0.2f)
		{
			this.lerp = lerp;
			this.moveTowards = moveTowards;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000E8F82 File Offset: 0x000E7382
		float ITargetAnimFuncs<float>.UpdateCurrent(float current, float target, float dt)
		{
			return Mathf.MoveTowards(Mathf.Lerp(target, current, Mathf.Exp(-dt * this.lerp)), target, dt * this.moveTowards);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000E8FA7 File Offset: 0x000E73A7
		bool ITargetAnimFuncs<float>.ShouldTrigger(float current, float target)
		{
			return Mathf.Abs(current - target) < 0.01f;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000E8FB8 File Offset: 0x000E73B8
		bool ITargetAnimFuncs<float>.IsDone(float current, float target)
		{
			return Mathf.Abs(current - target) < 0.001f;
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000E8FC9 File Offset: 0x000E73C9
		void ITargetAnimFuncs<float>.OnActivate(float current, float target)
		{
		}

		// Token: 0x040024B0 RID: 9392
		[SerializeField]
		private float lerp = 10f;

		// Token: 0x040024B1 RID: 9393
		[SerializeField]
		private float moveTowards = 0.2f;

		// Token: 0x040024B2 RID: 9394
		public static LerpTowards standard = new LerpTowards(14f, 0.2f);
	}
}
