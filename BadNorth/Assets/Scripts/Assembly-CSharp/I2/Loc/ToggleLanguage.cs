using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C6 RID: 966
	public class ToggleLanguage : MonoBehaviour
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x0002CC8B File Offset: 0x0002B08B
		private void Start()
		{
			base.Invoke("test", 3f);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0002CCA0 File Offset: 0x0002B0A0
		private void test()
		{
			List<string> allLanguages = LocalizationManager.GetAllLanguages(true);
			int num = allLanguages.IndexOf(LocalizationManager.CurrentLanguage);
			if (num >= 0)
			{
				num = (num + 1) % allLanguages.Count;
			}
			base.Invoke("test", 3f);
		}
	}
}
