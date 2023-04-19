using System;
using System.Diagnostics;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000821 RID: 2081
	internal class UiBehaviorDelegates : UIBehaviour
	{
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06003655 RID: 13909 RVA: 0x000EA1C4 File Offset: 0x000E85C4
		// (remove) Token: 0x06003656 RID: 13910 RVA: 0x000EA1FC File Offset: 0x000E85FC
		
		public event Action onRectTransformDimensionsChange = delegate()
		{
		};

		// Token: 0x06003657 RID: 13911 RVA: 0x000EA232 File Offset: 0x000E8632
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			this.onRectTransformDimensionsChange();
		}
	}
}
