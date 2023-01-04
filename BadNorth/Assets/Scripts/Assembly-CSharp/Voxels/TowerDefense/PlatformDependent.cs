using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000575 RID: 1397
	public class PlatformDependent : MonoBehaviour, IGameSetup
	{
		// Token: 0x06002448 RID: 9288 RVA: 0x00071FC4 File Offset: 0x000703C4
		private void Awake()
		{
			this.OnPlatformChanged();
			this.Initialize();
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00071FD2 File Offset: 0x000703D2
		public void OnGameAwake()
		{
			this.OnPlatformChanged();
			this.Initialize();
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00071FE0 File Offset: 0x000703E0
		public void Initialize()
		{
			if (!this.init)
			{
				Platform.onPlatformUpdated += this.OnPlatformChanged;
				this.init = true;
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00072005 File Offset: 0x00070405
		private void OnDestroy()
		{
			Platform.onPlatformUpdated -= this.OnPlatformChanged;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00072018 File Offset: 0x00070418
		private void OnPlatformChanged()
		{
			base.gameObject.SetActive(Platform.Is(this.targetPlatform));
		}

		// Token: 0x040016F4 RID: 5876
		[SerializeField]
		private EPlatform targetPlatform;

		// Token: 0x040016F5 RID: 5877
		private bool init;
	}
}
