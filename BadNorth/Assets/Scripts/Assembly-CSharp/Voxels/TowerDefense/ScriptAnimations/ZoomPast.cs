using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x0200080D RID: 2061
	[Serializable]
	internal class ZoomPast : ITargetAnimFuncs<float>
	{
		// Token: 0x060035EA RID: 13802 RVA: 0x000E8DF3 File Offset: 0x000E71F3
		public ZoomPast()
		{
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000E8E06 File Offset: 0x000E7206
		public ZoomPast(float acceleration)
		{
			this.acceleration = acceleration;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000E8E20 File Offset: 0x000E7220
		float ITargetAnimFuncs<float>.UpdateCurrent(float current, float target, float dt)
		{
			float num = target - current;
			this.speed += num * this.acceleration * dt;
			current = Mathf.MoveTowards(current, target, Mathf.Abs(this.speed * dt));
			return current;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000E8E5F File Offset: 0x000E725F
		bool ITargetAnimFuncs<float>.ShouldTrigger(float current, float target)
		{
			return current == target;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000E8E65 File Offset: 0x000E7265
		bool ITargetAnimFuncs<float>.IsDone(float current, float target)
		{
			return current == target;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000E8E6B File Offset: 0x000E726B
		void ITargetAnimFuncs<float>.OnActivate(float current, float target)
		{
			this.speed = 0f;
		}

		// Token: 0x040024AB RID: 9387
		private float speed;

		// Token: 0x040024AC RID: 9388
		[SerializeField]
		private float acceleration = 150f;
	}
}
