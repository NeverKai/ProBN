using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006BC RID: 1724
	public class CameraSpinner : LevelCameraComponent
	{
		// Token: 0x06002C96 RID: 11414 RVA: 0x000A6252 File Offset: 0x000A4652
		public void BeginSpin(float velocity)
		{
			this.velocity = Mathf.Clamp(velocity, -this.maxSpinVelocity, this.maxSpinVelocity);
			base.enabled = true;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000A6274 File Offset: 0x000A4674
		public void EndSpin()
		{
			this.velocity = 0f;
			base.enabled = false;
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000A6288 File Offset: 0x000A4688
		public bool IsSpinning()
		{
			return base.enabled;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000A6290 File Offset: 0x000A4690
		protected override void UpdateInternal()
		{
			base.levelCamera.RotateBy(this.velocity * base.deltaTime);
			this.velocity *= Mathf.Pow(this.exponentialSlowdown, base.deltaTime);
			this.velocity = Mathf.MoveTowards(this.velocity, 0f, base.deltaTime * this.linearSlowdown);
			if (Mathf.Abs(this.velocity) < this.minSpinVelocity)
			{
				this.EndSpin();
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000A6312 File Offset: 0x000A4712
		protected override void ResetLevelView()
		{
			base.ResetLevelView();
			this.EndSpin();
		}

		// Token: 0x04001D42 RID: 7490
		[SerializeField]
		[Range(0f, 100f)]
		private float linearSlowdown = 5f;

		// Token: 0x04001D43 RID: 7491
		[SerializeField]
		[Range(0f, 1f)]
		private float exponentialSlowdown = 0.6f;

		// Token: 0x04001D44 RID: 7492
		[SerializeField]
		private float maxSpinVelocity = 360f;

		// Token: 0x04001D45 RID: 7493
		[SerializeField]
		private float minSpinVelocity = 0.01f;

		// Token: 0x04001D46 RID: 7494
		private float velocity;
	}
}
