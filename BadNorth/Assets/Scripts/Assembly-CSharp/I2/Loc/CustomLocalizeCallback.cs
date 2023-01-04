using System;
using UnityEngine;
using UnityEngine.Events;

namespace I2.Loc
{
	// Token: 0x0200040B RID: 1035
	[AddComponentMenu("I2/Localization/I2 Localize Callback")]
	public class CustomLocalizeCallback : MonoBehaviour
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x0003CA03 File Offset: 0x0003AE03
		public void Enable()
		{
			LocalizationManager.OnLocalizeEvent -= this.OnLocalize;
			LocalizationManager.OnLocalizeEvent += this.OnLocalize;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0003CA27 File Offset: 0x0003AE27
		public void OnDisable()
		{
			LocalizationManager.OnLocalizeEvent -= this.OnLocalize;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0003CA3A File Offset: 0x0003AE3A
		public void OnLocalize()
		{
			this._OnLocalize.Invoke();
		}

		// Token: 0x04000EAE RID: 3758
		public UnityEvent _OnLocalize = new UnityEvent();
	}
}
