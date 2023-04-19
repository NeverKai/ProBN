using System;
using System.Diagnostics;
using CS.Platform;
using Voxels.TowerDefense;

namespace CS.VT
{
	// Token: 0x02000387 RID: 903
	public static class CorePlatformUtils
	{
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060014A7 RID: 5287 RVA: 0x0002A468 File Offset: 0x00028868
		// (remove) Token: 0x060014A8 RID: 5288 RVA: 0x0002A49C File Offset: 0x0002889C
		
		public static event Action onForcePrimUserOut;

		// Token: 0x060014A9 RID: 5289 RVA: 0x0002A4D0 File Offset: 0x000288D0
		public static bool ForcePrimUserOut()
		{
			BasePlatformManager.Instance.ClearMainUser();
			if (CorePlatformUtils.onForcePrimUserOut != null)
			{
				CorePlatformUtils.onForcePrimUserOut();
			}
			Singleton<Stack>.instance.stateMeta.SetActive(true);
			Singleton<Stack>.instance.stateMeta.children[0].SetActiveTrue();
			return true;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0002A522 File Offset: 0x00028922
		public static bool DynamicUsers()
		{
			return BasePlatformManager.Instance != null && BasePlatformManager.Instance.DynamicUsers;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0002A541 File Offset: 0x00028941
		// Note: this type is marked as 'beforefieldinit'.
		static CorePlatformUtils()
		{
			CorePlatformUtils.onForcePrimUserOut = delegate()
			{
			};
		}
	}
}
