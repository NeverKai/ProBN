using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x02000409 RID: 1033
	public class AutoChangeCultureInfo : MonoBehaviour
	{
		// Token: 0x060017FB RID: 6139 RVA: 0x0003C96B File Offset: 0x0003AD6B
		public void Start()
		{
			LocalizationManager.EnableChangingCultureInfo(true);
		}
	}
}
