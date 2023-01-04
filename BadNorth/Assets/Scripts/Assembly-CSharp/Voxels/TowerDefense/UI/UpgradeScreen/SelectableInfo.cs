using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000928 RID: 2344
	public abstract class SelectableInfo : MonoBehaviour
	{
		// Token: 0x06003EFD RID: 16125 RVA: 0x0011C11D File Offset: 0x0011A51D
		public void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.OnInitialize();
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x0011C138 File Offset: 0x0011A538
		private void Start()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x0011C140 File Offset: 0x0011A540
		public void SetupBase()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0011C148 File Offset: 0x0011A548
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x04002C13 RID: 11283
		private bool initialized;
	}
}
