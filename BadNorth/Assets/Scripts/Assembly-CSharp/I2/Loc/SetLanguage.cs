using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x0200041C RID: 1052
	[AddComponentMenu("I2/Localization/SetLanguage Button")]
	public class SetLanguage : MonoBehaviour
	{
		// Token: 0x06001844 RID: 6212 RVA: 0x0003F0E8 File Offset: 0x0003D4E8
		private void OnClick()
		{
			this.ApplyLanguage();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0003F0F0 File Offset: 0x0003D4F0
		public void ApplyLanguage()
		{
			if (LocalizationManager.HasLanguage(this._Language, true, true, true))
			{
				LocalizationManager.CurrentLanguage = this._Language;
			}
		}

		// Token: 0x04000F1E RID: 3870
		public string _Language;
	}
}
