using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C5 RID: 965
	public class RegisterBundlesManager : MonoBehaviour, IResourceManager_Bundles
	{
		// Token: 0x0600159B RID: 5531 RVA: 0x0002CC46 File Offset: 0x0002B046
		public void OnEnable()
		{
			if (!ResourceManager.pInstance.mBundleManagers.Contains(this))
			{
				ResourceManager.pInstance.mBundleManagers.Add(this);
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0002CC6D File Offset: 0x0002B06D
		public void OnDisable()
		{
			ResourceManager.pInstance.mBundleManagers.Remove(this);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0002CC80 File Offset: 0x0002B080
		public virtual UnityEngine.Object LoadFromBundle(string path, Type assetType)
		{
			return null;
		}
	}
}
