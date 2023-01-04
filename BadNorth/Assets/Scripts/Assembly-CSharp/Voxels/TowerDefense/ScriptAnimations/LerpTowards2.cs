using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000810 RID: 2064
	internal class LerpTowards2 : ITargetAnimFuncs<Vector2>
	{
		// Token: 0x060035FB RID: 13819 RVA: 0x000E8FE1 File Offset: 0x000E73E1
		public LerpTowards2(float lerp = 20f, float moveTowards = 0.2f)
		{
			this.lerp = lerp;
			this.moveTowards = moveTowards;
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000E900D File Offset: 0x000E740D
		Vector2 ITargetAnimFuncs<Vector2>.UpdateCurrent(Vector2 current, Vector2 target, float dt)
		{
			return Vector2.MoveTowards(Vector2.Lerp(target, current, Mathf.Exp(-dt * this.lerp)), target, dt * this.moveTowards);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000E9034 File Offset: 0x000E7434
		bool ITargetAnimFuncs<Vector2>.ShouldTrigger(Vector2 current, Vector2 target)
		{
			return (current - target).sqrMagnitude < 0.001f;
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000E9058 File Offset: 0x000E7458
		bool ITargetAnimFuncs<Vector2>.IsDone(Vector2 current, Vector2 target)
		{
			return (current - target).sqrMagnitude < 1E-05f;
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000E907B File Offset: 0x000E747B
		void ITargetAnimFuncs<Vector2>.OnActivate(Vector2 current, Vector2 target)
		{
		}

		// Token: 0x040024B3 RID: 9395
		[SerializeField]
		private float lerp = 10f;

		// Token: 0x040024B4 RID: 9396
		[SerializeField]
		private float moveTowards = 0.2f;

		// Token: 0x040024B5 RID: 9397
		public static LerpTowards2 standard = new LerpTowards2(20f, 0.2f);
	}
}
