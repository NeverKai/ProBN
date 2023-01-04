using System;
using UnityEngine;

namespace RTM.UISystem
{
	// Token: 0x020004DA RID: 1242
	public class UIMenuAutoAdder : MonoBehaviour
	{
		// Token: 0x06001FBB RID: 8123 RVA: 0x000555E7 File Offset: 0x000539E7
		private void Awake()
		{
			this.menu = base.GetComponent<UIMenu>();
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000555F5 File Offset: 0x000539F5
		private void OnEnable()
		{
			this.menu.OpenMenu();
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00055602 File Offset: 0x00053A02
		private void OnDisable()
		{
			if (Singleton<UIManager>.instance && Singleton<UIManager>.instance.StackContains(this.menu))
			{
				this.menu.CloseMenu();
			}
		}

		// Token: 0x040013B5 RID: 5045
		private UIMenu menu;
	}
}
