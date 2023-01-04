using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006BA RID: 1722
	public class CameraSeeker : LevelCameraComponent
	{
		// Token: 0x06002C83 RID: 11395 RVA: 0x000A5DFC File Offset: 0x000A41FC
		public void SeekToLatch(float direction, int numSteps = 1)
		{
			for (int i = 0; i < numSteps; i++)
			{
				float currentAngle = (!this.IsSeeking()) ? base.levelCamera.yaw : this.seeker.target;
				this.BeginSeek(this.GetSeekAngle(currentAngle, direction));
			}
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000A5E50 File Offset: 0x000A4250
		private float GetSeekAngle(float currentAngle, float direction)
		{
			float num = 360f / (float)this.numberOfLatches;
			direction = Mathf.Sign(direction);
			return (Mathf.Round((currentAngle - this.latchGlobalOffset) / num) + direction) * num + direction * this.latchOvershootDegrees + this.latchGlobalOffset;
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000A5E96 File Offset: 0x000A4296
		public void BeginSeek(float angle)
		{
			this.seeker.target = angle;
			base.enabled = true;
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000A5EAB File Offset: 0x000A42AB
		public void EndSeek()
		{
			base.enabled = false;
			this.seeker.EndSeek();
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000A5EBF File Offset: 0x000A42BF
		public void EndIfOpposed(float direction)
		{
			this.seeker.EndIfOpposed(direction);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000A5ECD File Offset: 0x000A42CD
		public bool IsSeeking()
		{
			return base.enabled;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000A5ED8 File Offset: 0x000A42D8
		protected override void UpdateInternal()
		{
			float yaw = base.levelCamera.yaw;
			this.seeker.position = yaw;
			if (this.seeker.Update(base.deltaTime, out yaw))
			{
				base.levelCamera.yaw = yaw;
			}
			else
			{
				this.EndSeek();
			}
		}

		// Token: 0x04001D30 RID: 7472
		[Header("Latching")]
		[SerializeField]
		private int numberOfLatches = 4;

		// Token: 0x04001D31 RID: 7473
		[SerializeField]
		private float latchGlobalOffset = 45f;

		// Token: 0x04001D32 RID: 7474
		[SerializeField]
		private float latchOvershootDegrees = 10f;

		// Token: 0x04001D33 RID: 7475
		[Header("Seeking")]
		[SerializeField]
		private SimpleLinearSeeker seeker = new SimpleLinearSeeker();
	}
}
