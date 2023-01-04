using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x02000811 RID: 2065
	internal class LerpTowards3 : ITargetAnimFuncs<Vector3>
	{
		// Token: 0x06003601 RID: 13825 RVA: 0x000E9093 File Offset: 0x000E7493
		public LerpTowards3(float lerp = 20f, float moveTowards = 0.2f)
		{
			this.lerp = lerp;
			this.moveTowards = moveTowards;
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000E90BF File Offset: 0x000E74BF
		Vector3 ITargetAnimFuncs<Vector3>.UpdateCurrent(Vector3 current, Vector3 target, float dt)
		{
			return Vector2.MoveTowards(Vector3.Lerp(target, current, Mathf.Exp(-dt * this.lerp)), target, dt * this.moveTowards);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000E90F4 File Offset: 0x000E74F4
		bool ITargetAnimFuncs<Vector3>.ShouldTrigger(Vector3 current, Vector3 target)
		{
			return (current - target).sqrMagnitude < 0.001f;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000E9118 File Offset: 0x000E7518
		bool ITargetAnimFuncs<Vector3>.IsDone(Vector3 current, Vector3 target)
		{
			return (current - target).sqrMagnitude < 1E-05f;
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000E913B File Offset: 0x000E753B
		void ITargetAnimFuncs<Vector3>.OnActivate(Vector3 current, Vector3 target)
		{
		}

		// Token: 0x040024B6 RID: 9398
		[SerializeField]
		private float lerp = 10f;

		// Token: 0x040024B7 RID: 9399
		[SerializeField]
		private float moveTowards = 0.2f;

		// Token: 0x040024B8 RID: 9400
		public static LerpTowards3 standard = new LerpTowards3(20f, 0.2f);
	}
}
