using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006B3 RID: 1715
	public class CameraLimiter : LevelCameraComponent
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x000A4FD1 File Offset: 0x000A33D1
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x000A4FD9 File Offset: 0x000A33D9
		public float islandWidth { get; private set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000A4FE2 File Offset: 0x000A33E2
		// (set) Token: 0x06002C5A RID: 11354 RVA: 0x000A4FEA File Offset: 0x000A33EA
		public float levelWidth { get; private set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000A4FF3 File Offset: 0x000A33F3
		public float orthoWidthMin
		{
			get
			{
				return this.platformSettings.Get().orthoWidthMin;
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000A5005 File Offset: 0x000A3405
		public float GetIslandHeight(bool includeCoins, out float vMin, out float vMax)
		{
			vMin = this.islandVerticalMin;
			vMax = ((!includeCoins) ? this.islandVerticalMax : Mathf.Max(this.islandVerticalMax, this.coinVerticalMax));
			return vMax - vMin;
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000A5038 File Offset: 0x000A3438
		public float GetIslandHeight(bool includeCoins)
		{
			float num;
			float num2;
			return this.GetIslandHeight(includeCoins, out num, out num2);
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000A5050 File Offset: 0x000A3450
		public float orthoWidthMax
		{
			get
			{
				return Mathf.Max(this.levelWidth, this.platformSettings.Get().orthoWidthMax);
			}
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000A506D File Offset: 0x000A346D
		protected override void UpdateInternal()
		{
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000A506F File Offset: 0x000A346F
		private void LateUpdate()
		{
			base.levelCamera.SetOrthoWidth(Mathf.Clamp(base.levelCamera.GetOrthoWidth(), this.orthoWidthMin, this.orthoWidthMax));
			if (!base.levelCamera.lockPanY)
			{
				this.ClampCameraVertical();
			}
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000A50B0 File Offset: 0x000A34B0
		protected override void ResetLevelView()
		{
			float pitch = base.levelCamera.GetPitch();
			float num = Mathf.Tan(pitch * 0.017453292f);
			this.UpdateVerticalLimits();
			base.levelCamera.position = Vector3.up * Mathf.Lerp(this.islandVerticalMin, this.islandVerticalMax, 0.6f);
			base.levelCamera.yaw = 45f;
			this.levelWidth = base.island.fog.maxRad * 2f;
			this.islandWidth = Mathf.Sqrt((float)(base.island.levelNode.size.x * base.island.levelNode.size.z));
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000A5170 File Offset: 0x000A3570
		public void ClampCameraVertical()
		{
			Vector3 position = base.levelCamera.position;
			position.y = this.GetClampedCameraVertical(position.y);
			base.levelCamera.position = position;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000A51AC File Offset: 0x000A35AC
		private float GetClampedCameraVertical(float inPos)
		{
			float num = base.levelCamera.GetOrthoHeight() * Constants.upScale;
			float num2 = num * base.levelCamera.defaultScreenRect.yMin;
			float num3 = num * (1f - base.levelCamera.defaultScreenRect.yMax);
			float num4 = this.verticalMax - this.verticalMin;
			float a = num4 - num + num2 + num3;
			float num5 = this.verticalMin + num * 0.5f - num2;
			float max = num5 + Mathf.Max(a, 0f);
			return Mathf.Clamp(inPos, num5, max);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000A5244 File Offset: 0x000A3644
		private void UpdateVerticalLimits()
		{
			this.islandVerticalMin = float.MaxValue;
			this.islandVerticalMax = (this.fogVertical = (this.coinVerticalMax = float.MinValue));
			Island island = Singleton<CampaignManager>.instance.campaign.currentLevel.island;
			float pitch = base.levelCamera.GetPitch();
			float tan = Mathf.Tan(pitch * 0.017453292f);
			List<BoxCollider> colliders = island.GetComponentInChildren<VoxelColliders>(true).colliders;
			foreach (BoxCollider boxCollider in colliders)
			{
				Bounds bounds = boxCollider.bounds;
				Vector3 position = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
				Vector3 position2 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
				Vector3 position3 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
				Vector3 position4 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
				CameraLimiter.UpdateCamHeights(position, tan, ref this.islandVerticalMin, ref this.islandVerticalMax);
				CameraLimiter.UpdateCamHeights(position2, tan, ref this.islandVerticalMin, ref this.islandVerticalMax);
				CameraLimiter.UpdateCamHeights(position3, tan, ref this.islandVerticalMin, ref this.islandVerticalMax);
				CameraLimiter.UpdateCamHeights(position4, tan, ref this.islandVerticalMin, ref this.islandVerticalMax);
			}
			float maxValue = float.MaxValue;
			CameraLimiter.GetCamHeights(Vector3.right * island.fog.maxRad, tan, out maxValue, out this.fogVertical);
			foreach (House house in island.village.houses)
			{
				Bounds coinBounds = house.coinBounds;
				float maxValue2 = float.MaxValue;
				CameraLimiter.UpdateCamHeights(coinBounds.center + Vector3.up * coinBounds.extents.y, tan, ref maxValue2, ref this.coinVerticalMax);
			}
			this.verticalMin = Mathf.Min(-this.fogVertical, this.islandVerticalMin);
			this.verticalMax = Mathf.Max(new float[]
			{
				this.fogVertical,
				this.islandVerticalMax,
				this.coinVerticalMax
			});
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000A5550 File Offset: 0x000A3950
		private static void UpdateCamHeights(Vector3 position, float tan, ref float min, ref float max)
		{
			float b;
			float b2;
			CameraLimiter.GetCamHeights(position, tan, out b, out b2);
			min = Mathf.Min(min, b);
			max = Mathf.Max(max, b2);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000A557C File Offset: 0x000A397C
		private static void GetCamHeights(Vector3 position, float tan, out float min, out float max)
		{
			float num = position.GetHorizontalMagnitude() * tan;
			min = position.y - num;
			max = position.y + num;
		}

		// Token: 0x04001D14 RID: 7444
		[SerializeField]
		private CameraLimiter.PlatformSettings platformSettings = new CameraLimiter.PlatformSettings();

		// Token: 0x04001D15 RID: 7445
		private float islandVerticalMin;

		// Token: 0x04001D16 RID: 7446
		private float islandVerticalMax;

		// Token: 0x04001D17 RID: 7447
		private float fogVertical;

		// Token: 0x04001D18 RID: 7448
		private float coinVerticalMax;

		// Token: 0x04001D19 RID: 7449
		private float verticalMin;

		// Token: 0x04001D1A RID: 7450
		private float verticalMax;

		// Token: 0x020006B4 RID: 1716
		[Serializable]
		private class Settings
		{
			// Token: 0x04001D1D RID: 7453
			public float orthoWidthMin = 7.5f;

			// Token: 0x04001D1E RID: 7454
			public float orthoWidthMax = 12f;
		}

		// Token: 0x020006B5 RID: 1717
		[Serializable]
		private class SettingsMap : PlatformSettingsMap<CameraLimiter.Settings>
		{
		}

		// Token: 0x020006B6 RID: 1718
		[Serializable]
		private class PlatformSettings : PlatformVariantSettings<CameraLimiter.Settings, CameraLimiter.SettingsMap>
		{
		}
	}
}
