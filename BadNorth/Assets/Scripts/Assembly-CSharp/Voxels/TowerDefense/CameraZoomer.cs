using System;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006BD RID: 1725
	public class CameraZoomer : LevelCameraComponent
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000A6372 File Offset: 0x000A4772
		public float orthoMin
		{
			get
			{
				return (!this.limiter) ? float.MinValue : this.limiter.orthoWidthMin;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000A6399 File Offset: 0x000A4799
		public float orthoMax
		{
			get
			{
				return (!this.limiter) ? float.MaxValue : this.limiter.orthoWidthMax;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000A63C0 File Offset: 0x000A47C0
		public float current
		{
			get
			{
				return base.levelCamera.GetOrthoWidth();
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000A63CD File Offset: 0x000A47CD
		public float target
		{
			get
			{
				return this.seeker.target;
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000A63DA File Offset: 0x000A47DA
		protected void Awake()
		{
			this.limiter = base.GetComponent<CameraLimiter>();
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000A63E8 File Offset: 0x000A47E8
		protected override void ResetLevelView()
		{
			base.levelCamera.SetOrthoWidth(this.defaultOtrthoWidth);
			this.LockZoom = false;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000A6404 File Offset: 0x000A4804
		public void ZoomTo(float zoomTarget)
		{
			if (this.LockZoom)
			{
				return;
			}
			this.seeker.position = base.levelCamera.GetOrthoWidth();
			this.seeker.target = Mathf.Clamp(zoomTarget, this.limiter.orthoWidthMin, this.limiter.orthoWidthMax);
			this.pinchZoom = false;
			base.enabled = true;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000A6468 File Offset: 0x000A4868
		public void ZoomBy(float zoomChange)
		{
			if (this.LockZoom)
			{
				return;
			}
			if (!this.pinchZoom)
			{
				float num = Mathf.Pow(this.steppedMovementExponent, zoomChange);
				this.seeker.EndIfOpposed(num - 1f);
				float orthoWidth = base.levelCamera.GetOrthoWidth();
				this.seeker.target = Mathf.Clamp(((!this.seeker.IsAtTarget()) ? this.seeker.target : orthoWidth) * num, this.orthoMin, this.orthoMax);
				this.seeker.position = orthoWidth;
				base.enabled = true;
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000A650C File Offset: 0x000A490C
		public void PinchZoom(Vector2 touch0, Vector2 touch1)
		{
			if (this.LockZoom)
			{
				return;
			}
			if (this.pinchZoom)
			{
				float num = (touch0 - touch1).magnitude;
				float b = 1300f * Time.unscaledDeltaTime;
				float f = num - this.lastPinchDist;
				num = (this.lastPinchDist += Mathf.Min(Mathf.Abs(f), b) * Mathf.Sign(f));
				float num2 = this.originalZoom * Mathf.Pow(this.originalPinchDist / num, 0.6666667f);
				float num3 = Mathf.Clamp(num2, this.orthoMin, this.orthoMax);
				base.levelCamera.SetOrthoWidth(num3);
				if (num2 != num3)
				{
					this.BeginPinchZoom(touch0, touch1);
				}
			}
			else
			{
				this.BeginPinchZoom(touch0, touch1);
			}
			this.lastPinchZoomTime = Time.unscaledTime;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000A65E1 File Offset: 0x000A49E1
		private void EndPinchZoom()
		{
			this.pinchZoom = false;
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000A65EC File Offset: 0x000A49EC
		private void BeginPinchZoom(Vector2 touch0, Vector2 touch1)
		{
			this.lastPinchDist = (this.originalPinchDist = (touch0 - touch1).magnitude);
			this.originalZoom = base.levelCamera.GetOrthoWidth();
			this.pinchZoom = true;
			this.seeker.EndSeek();
			base.enabled = true;
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000A6644 File Offset: 0x000A4A44
		protected override void UpdateInternal()
		{
			if (!this.pinchZoom)
			{
				float width;
				if (this.seeker.Update(base.deltaTime, out width))
				{
					base.levelCamera.SetOrthoWidth(width);
				}
				else
				{
					base.enabled = false;
				}
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000A668C File Offset: 0x000A4A8C
		private void LateUpdate()
		{
			if (this.pinchZoom && this.lastPinchZoomTime < Time.unscaledTime)
			{
				base.enabled = false;
				this.EndPinchZoom();
			}
		}

		// Token: 0x04001D47 RID: 7495
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CameraZoomer", EVerbosity.Quiet, 100);

		// Token: 0x04001D48 RID: 7496
		[SerializeField]
		private SimpleLinearSeeker seeker = new SimpleLinearSeeker();

		// Token: 0x04001D49 RID: 7497
		[SerializeField]
		private float steppedMovementExponent = 1.04f;

		// Token: 0x04001D4A RID: 7498
		[SerializeField]
		private float defaultOtrthoWidth = 16f;

		// Token: 0x04001D4B RID: 7499
		private const float maxPinchSpeed = 1300f;

		// Token: 0x04001D4C RID: 7500
		private bool pinchZoom;

		// Token: 0x04001D4D RID: 7501
		private float originalPinchDist;

		// Token: 0x04001D4E RID: 7502
		private float lastPinchDist;

		// Token: 0x04001D4F RID: 7503
		private float originalZoom;

		// Token: 0x04001D50 RID: 7504
		private float lastPinchZoomTime = float.MinValue;

		// Token: 0x04001D51 RID: 7505
		private CameraLimiter limiter;

		// Token: 0x04001D52 RID: 7506
		public bool LockZoom;
	}
}
