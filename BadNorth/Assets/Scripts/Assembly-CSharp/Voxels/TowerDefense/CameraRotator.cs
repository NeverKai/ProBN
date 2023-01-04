using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006B7 RID: 1719
	public class CameraRotator : LevelCameraComponent
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000A55DE File Offset: 0x000A39DE
		public float velocity
		{
			get
			{
				return this.mover.velocity;
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000A55EB File Offset: 0x000A39EB
		public void SetInput(float input)
		{
			this.mover.input = input;
			base.enabled = true;
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000A5600 File Offset: 0x000A3A00
		public void EndRotationImmediate()
		{
			this.mover.EndMovementImmediate();
			base.enabled = false;
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000A5614 File Offset: 0x000A3A14
		protected override void UpdateInternal()
		{
			float degrees;
			if (this.mover.Update(base.deltaTime, out degrees))
			{
				base.levelCamera.RotateBy(degrees);
			}
			else
			{
				base.enabled = false;
			}
			this.mover.input = 0f;
		}

		// Token: 0x04001D1F RID: 7455
		[SerializeField]
		private SimpleLinearAccelerator mover;
	}
}
