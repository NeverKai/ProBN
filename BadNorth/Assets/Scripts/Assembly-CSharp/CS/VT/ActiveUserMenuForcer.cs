using System;
using CS.Platform;
using UnityEngine;
using Voxels.TowerDefense;

namespace CS.VT
{
	// Token: 0x02000382 RID: 898
	public class ActiveUserMenuForcer : MonoBehaviour
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x00029D98 File Offset: 0x00028198
		private void OnEnable()
		{
			PlatformEvents.OnMainUserStateEvent += this.PlatformEvents_OnMainUserStateEvent;
			if (BasePlatformManager.Instance != null && !Singleton<Stack>.instance.stateMeta.children[0].active && !BasePlatformManager.Instance.MainUserSignedIn)
			{
				CorePlatformUtils.ForcePrimUserOut();
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00029DF6 File Offset: 0x000281F6
		private void PlatformEvents_OnMainUserStateEvent(bool effect)
		{
			if (!effect)
			{
				CorePlatformUtils.ForcePrimUserOut();
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00029E04 File Offset: 0x00028204
		private void OnDisable()
		{
			PlatformEvents.OnMainUserStateEvent -= this.PlatformEvents_OnMainUserStateEvent;
		}
	}
}
