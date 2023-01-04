using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C0 RID: 960
	public class CallbackNotification : MonoBehaviour
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x0002C6C0 File Offset: 0x0002AAC0
		public void OnModifyLocalization()
		{
			if (string.IsNullOrEmpty(Localize.MainTranslation))
			{
				return;
			}
			string translation = LocalizationManager.GetTranslation("Color/Red", true, 0, true, false, null, null);
			Localize.MainTranslation = Localize.MainTranslation.Replace("{PLAYER_COLOR}", translation);
		}
	}
}
