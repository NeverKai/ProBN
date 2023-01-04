using System;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006B1 RID: 1713
	public class CameraDragger : LevelCameraComponent
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000A4CE9 File Offset: 0x000A30E9
		// (set) Token: 0x06002C4F RID: 11343 RVA: 0x000A4CF1 File Offset: 0x000A30F1
		public bool isDragging { get; private set; }

		// Token: 0x06002C50 RID: 11344 RVA: 0x000A4CFA File Offset: 0x000A30FA
		public void OnDragStart()
		{
			this.dragStartTime = FrameTimeStamp.now;
			this.lastDragTime = FrameTimeStamp.zero;
			this.velocity = Vector2.zero;
			this.isDragging = true;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000A4D24 File Offset: 0x000A3124
		public void OnDragEnd(out Vector2 dragVelocity)
		{
			if (this.lastDragTime.unscaledTimeSince > this.dragReleaseThresholdTime || this.lastDragTime.framesSince > 1 || Mathf.Abs(this.velocity.x) < this.dragReleaseSpeedThreshold)
			{
				dragVelocity = Vector2.zero;
			}
			else
			{
				float unscaledTimeSince = this.dragStartTime.unscaledTimeSince;
				float num = Mathf.Lerp(1.5f, 0.8f, (unscaledTimeSince - 0.0333f) / 0.3f);
				this.velocity.x = this.velocity.x * num;
				this.velocity.x = Mathf.Sign(this.velocity.x) * Mathf.Min(Mathf.Abs(this.velocity.x), this.dragReleaseSpeedLimit);
				dragVelocity = this.velocity;
			}
			this.isDragging = false;
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000A4E0C File Offset: 0x000A320C
		public void OnDrag(Vector2 rawInputDelta)
		{
			Vector2 a = new Vector2(rawInputDelta.x / (float)Screen.width * this.rotationMultiplier, -(rawInputDelta.y / (float)Screen.height) * base.levelCamera.GetOrthoHeight() * Constants.upScale);
			float num = Mathf.Max(this.lastDragTime.unscaledTimeSince, Time.unscaledDeltaTime);
			if (num > this.dragReleaseThresholdTime)
			{
				this.dragStartTime = FrameTimeStamp.now;
				this.velocity = a / Time.unscaledDeltaTime;
			}
			else
			{
				this.velocity = Vector2.Lerp(this.velocity, a / num, num * 20f);
			}
			this.lastDragTime = FrameTimeStamp.now;
			float num2 = Mathf.InverseLerp(400f, 200f, Mathf.Abs(this.velocity.x));
			num2 *= num2;
			a.y *= num2;
			this.velocity.y = this.velocity.y * num2;
			base.levelCamera.RotateBy(a.x);
			base.levelCamera.MovePositionBy(new Vector3(0f, a.y, 0f));
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000A4F3C File Offset: 0x000A333C
		protected override void UpdateInternal()
		{
		}

		// Token: 0x04001D0B RID: 7435
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CameraDragger", EVerbosity.Quiet, 100);

		// Token: 0x04001D0C RID: 7436
		[SerializeField]
		private float rotationMultiplier = 1f;

		// Token: 0x04001D0D RID: 7437
		[SerializeField]
		private float dragReleaseSpeedThreshold = 95f;

		// Token: 0x04001D0E RID: 7438
		[SerializeField]
		private float dragReleaseSpeedLimit = 1000f;

		// Token: 0x04001D0F RID: 7439
		[SerializeField]
		private float dragReleaseThresholdTime = 0.15f;

		// Token: 0x04001D10 RID: 7440
		private FrameTimeStamp dragStartTime;

		// Token: 0x04001D11 RID: 7441
		private FrameTimeStamp lastDragTime;

		// Token: 0x04001D12 RID: 7442
		private Vector2 velocity = Vector2.zero;
	}
}
