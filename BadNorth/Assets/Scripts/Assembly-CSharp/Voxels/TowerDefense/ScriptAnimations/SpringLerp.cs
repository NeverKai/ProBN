using System;
using UnityEngine;

namespace Voxels.TowerDefense.ScriptAnimations
{
	// Token: 0x0200080E RID: 2062
	[Serializable]
	internal class SpringLerp : ITargetAnimFuncs<float>
	{
		// Token: 0x060035F0 RID: 13808 RVA: 0x000E8E78 File Offset: 0x000E7278
		public SpringLerp(float acceleration = 150f, float decelerration = 10f)
		{
			this.acceleration = acceleration;
			this.decelerration = decelerration;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000E8EA4 File Offset: 0x000E72A4
		float ITargetAnimFuncs<float>.UpdateCurrent(float current, float target, float dt)
		{
			float num = target - current;
			this.speed += num * this.acceleration * dt;
			current += this.speed * dt;
			this.speed -= this.speed * this.decelerration * dt;
			current = Mathf.MoveTowards(current, target, dt * 0.1f);
			current = Mathf.Lerp(current, target, dt);
			return current;
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x000E8F0F File Offset: 0x000E730F
		bool ITargetAnimFuncs<float>.ShouldTrigger(float current, float target)
		{
			return Mathf.Abs(current - target) < 0.01f;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000E8F20 File Offset: 0x000E7320
		bool ITargetAnimFuncs<float>.IsDone(float current, float target)
		{
			return Mathf.Abs(current - target) < 0.0001f && Mathf.Abs(this.speed) < 0.0001f;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000E8F49 File Offset: 0x000E7349
		void ITargetAnimFuncs<float>.OnActivate(float current, float target)
		{
			this.speed = 0f;
		}

		// Token: 0x040024AD RID: 9389
		[NonSerialized]
		public float speed;

		// Token: 0x040024AE RID: 9390
		[SerializeField]
		private float acceleration = 150f;

		// Token: 0x040024AF RID: 9391
		[SerializeField]
		private float decelerration = 10f;
	}
}
