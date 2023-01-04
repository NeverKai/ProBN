using System;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000825 RID: 2085
	internal class UIVisibilityInstant : MonoBehaviour, IUIVisibility
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000EA3E9 File Offset: 0x000E87E9
		// (set) Token: 0x06003666 RID: 13926 RVA: 0x000EA3F6 File Offset: 0x000E87F6
		bool IUIVisibility.visible
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				((IUIVisibility)this).SetVisible(value, false);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000EA400 File Offset: 0x000E8800
		float IUIVisibility.alpha
		{
			get
			{
				return (!((IUIVisibility)this).visible) ? 0f : 1f;
			}
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000EA41C File Offset: 0x000E881C
		void IUIVisibility.SetVisible(bool visible, bool snap)
		{
			base.gameObject.SetActive(visible);
		}
	}
}
