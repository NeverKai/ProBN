using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000576 RID: 1398
	[ExecuteInEditMode]
	public class PlatformUIScaler : MonoBehaviour
	{
		// Token: 0x0600244E RID: 9294 RVA: 0x00072043 File Offset: 0x00070443
		private void Awake()
		{
			this.UpdateScale();
			this.platformSettings.onUpdated += this.UpdateScale;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00072064 File Offset: 0x00070464
		private void UpdateScale()
		{
			float uiscale = this.platformSettings.Get().UIScale;
			base.transform.localScale = new Vector3(uiscale, uiscale, uiscale);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00072095 File Offset: 0x00070495
		private void OnDestroy()
		{
			this.platformSettings.onUpdated -= this.UpdateScale;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000720B0 File Offset: 0x000704B0
		~PlatformUIScaler()
		{
			Platform.onPlatformUpdated -= this.UpdateScale;
		}

		// Token: 0x040016F6 RID: 5878
		[SerializeField]
		private PlatformUIScaler.PlatformSettings platformSettings = new PlatformUIScaler.PlatformSettings();

		// Token: 0x02000577 RID: 1399
		[Serializable]
		private class Settings
		{
			// Token: 0x040016F7 RID: 5879
			public float UIScale = 1f;
		}

		// Token: 0x02000578 RID: 1400
		[Serializable]
		private class SettingsMap : PlatformSettingsMap<PlatformUIScaler.Settings>
		{
		}

		// Token: 0x02000579 RID: 1401
		[Serializable]
		private class PlatformSettings : PlatformVariantSettings<PlatformUIScaler.Settings, PlatformUIScaler.SettingsMap>
		{
		}
	}
}
