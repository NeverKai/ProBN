using System;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x02000572 RID: 1394
	public class PlatformCanvasScaler : MonoBehaviour, IGameSetup
	{
		// Token: 0x0600243D RID: 9277 RVA: 0x00071B1C File Offset: 0x0006FF1C
		void IGameSetup.OnGameAwake()
		{
			this.canvas = base.GetComponent<Canvas>();
			this.scaler = base.GetComponent<CanvasScaler>();
			this.UpdatePlatform();
			Platform.onPlatformUpdated += this.UpdatePlatform;
			UserSettings.onUpdated += this.UpdateUserSettings;
			MobileScreenDetector.onSafeZoneChanged += this.UpdateMobileSafeZone;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00071B7C File Offset: 0x0006FF7C
		private void UpdateMobileSafeZone(Vector4 safeZone, DeviceOrientation deviceOrientation)
		{
			if (Platform.Is(EPlatform.IOSPhone))
			{
				if (deviceOrientation == DeviceOrientation.LandscapeLeft)
				{
					PlatformCanvasScaler.NotchBehavior notchBehavior = this.iosNotchBehaviourLeft;
					if (notchBehavior != PlatformCanvasScaler.NotchBehavior.NotchSideOnly)
					{
						if (notchBehavior == PlatformCanvasScaler.NotchBehavior.Ignore)
						{
							safeZone.x = (safeZone.y = 0f);
						}
					}
					else
					{
						safeZone.y = 0f;
					}
				}
				else if (deviceOrientation == DeviceOrientation.LandscapeRight)
				{
					PlatformCanvasScaler.NotchBehavior notchBehavior2 = this.iosNotchBehaviourRight;
					if (notchBehavior2 != PlatformCanvasScaler.NotchBehavior.NotchSideOnly)
					{
						if (notchBehavior2 == PlatformCanvasScaler.NotchBehavior.Ignore)
						{
							safeZone.x = (safeZone.y = 0f);
						}
					}
					else
					{
						safeZone.x = 0f;
					}
				}
				if (!this.offsetForIOSHomeBar)
				{
					safeZone.w = 0f;
				}
			}
			this.SetSafeZoneNormalized(safeZone.x, safeZone.y, safeZone.z, safeZone.w);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00071C6D File Offset: 0x0007006D
		private void UpdateUserSettings(UserSettings settings)
		{
			if (!Platform.Is(EPlatform.PC))
			{
				return;
			}
			this.UpdateSafeArea(settings.displaySafeArea);
			if (this.scaleMode != settings.pcScaleMode)
			{
				this.scaleMode = settings.pcScaleMode;
				this.UpdatePlatform();
			}
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00071CAC File Offset: 0x000700AC
		public void UpdatePlatform()
		{
			if (Platform.Is(EPlatform.SwitchHandheld))
			{
				this.scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
				this.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				this.UpdateSafeArea(1f);
			}
			else if (Platform.Is(EPlatform.Phone))
			{
				this.scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				this.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				this.scaler.referenceResolution = new Vector2(1280f, 720f) / this.phoneScale;
				this.UpdateMobileSafeZone(MobileScreenDetector.safeZone, MobileScreenDetector.deviceOrientation);
			}
			else if (Platform.Is(EPlatform.Tablet))
			{
				this.scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				this.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				this.scaler.referenceResolution = new Vector2(1920f, 1080f);
			}
			else
			{
				this.scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				this.scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
				if (Platform.Is(EPlatform.PC) && this.scaleMode == PlatformCanvasScaler.PCScaleMode.Desktop)
				{
					this.ScaleForDesktop();
				}
				else
				{
					this.scaler.referenceResolution = new Vector2(1920f, 1080f);
				}
				if (Profile.userSettings)
				{
					this.UpdateSafeArea(Profile.userSettings.displaySafeArea);
				}
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00071E08 File Offset: 0x00070208
		private void ScaleForDesktop()
		{
			Vector2 a = new Vector2(1920f, 1080f);
			Vector2 b = new Vector2(2560f, 1440f);
			this.scaler.referenceResolution = Vector2.Lerp(a, b, this.pcShrinkFactor);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00071E50 File Offset: 0x00070250
		private void UpdateSafeArea(float safeArea)
		{
			if (Platform.Is(EPlatform.SwitchHandheld))
			{
				safeArea = 1f;
			}
			if (this.displaySafeArea == safeArea)
			{
				return;
			}
			this.displaySafeArea = safeArea;
			if (this.safeZoneScaler)
			{
				float num = (1f - safeArea) * 0.5f;
				Vector2 vector = new Vector2(num, num);
				this.safeZoneScaler.anchorMin = vector;
				this.safeZoneScaler.anchorMax = Vector2.one - vector;
				this.safeZoneScaler.ForceChildLayoutUpdates(false);
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00071EDC File Offset: 0x000702DC
		private void SetSafeZoneNormalized(float left, float right, float top, float bottom)
		{
			if (this.safeZoneScaler)
			{
				this.safeZoneScaler.anchorMin = new Vector2(left, bottom);
				this.safeZoneScaler.anchorMax = new Vector2(1f - right, 1f - top);
				this.safeZoneScaler.ForceChildLayoutUpdates(false);
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x00071F36 File Offset: 0x00070336
		private void OnDestroy()
		{
			Platform.onPlatformUpdated -= this.UpdatePlatform;
			UserSettings.onUpdated -= this.UpdateUserSettings;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00071F5C File Offset: 0x0007035C
		~PlatformCanvasScaler()
		{
			Platform.onPlatformUpdated -= this.UpdatePlatform;
			UserSettings.onUpdated -= this.UpdateUserSettings;
		}

		// Token: 0x040016E2 RID: 5858
		private static DebugChannelGroup dbgGroup = new DebugChannelGroup("PlatformCanvasScaler", EVerbosity.Quiet, 100);

		// Token: 0x040016E3 RID: 5859
		[SerializeField]
		[Tooltip("Optional - REctTransform under this canvas that will be used to implement safe zones")]
		private RectTransform safeZoneScaler;

		// Token: 0x040016E4 RID: 5860
		[SerializeField]
		[Range(0f, 1f)]
		private float pcShrinkFactor = 1f;

		// Token: 0x040016E5 RID: 5861
		[Header("Mobile")]
		[Space]
		[SerializeField]
		private float phoneScale = 1f;

		// Token: 0x040016E6 RID: 5862
		[SerializeField]
		private PlatformCanvasScaler.NotchBehavior iosNotchBehaviourLeft;

		// Token: 0x040016E7 RID: 5863
		[SerializeField]
		private PlatformCanvasScaler.NotchBehavior iosNotchBehaviourRight;

		// Token: 0x040016E8 RID: 5864
		[SerializeField]
		private bool offsetForIOSHomeBar = true;

		// Token: 0x040016E9 RID: 5865
		private Canvas canvas;

		// Token: 0x040016EA RID: 5866
		private CanvasScaler scaler;

		// Token: 0x040016EB RID: 5867
		private float displaySafeArea;

		// Token: 0x040016EC RID: 5868
		private PlatformCanvasScaler.PCScaleMode scaleMode;

		// Token: 0x02000573 RID: 1395
		public enum PCScaleMode
		{
			// Token: 0x040016EE RID: 5870
			Desktop,
			// Token: 0x040016EF RID: 5871
			TV
		}

		// Token: 0x02000574 RID: 1396
		public enum NotchBehavior
		{
			// Token: 0x040016F1 RID: 5873
			Mirrored,
			// Token: 0x040016F2 RID: 5874
			NotchSideOnly,
			// Token: 0x040016F3 RID: 5875
			Ignore
		}
	}
}
