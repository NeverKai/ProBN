using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C1 RID: 961
	public class Example_ChangeLanguage : MonoBehaviour
	{
		// Token: 0x06001588 RID: 5512 RVA: 0x0002C70B File Offset: 0x0002AB0B
		public void SetLanguage_English()
		{
			this.SetLanguage("English");
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0002C718 File Offset: 0x0002AB18
		public void SetLanguage_French()
		{
			this.SetLanguage("French");
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0002C725 File Offset: 0x0002AB25
		public void SetLanguage_Spanish()
		{
			this.SetLanguage("Spanish");
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0002C732 File Offset: 0x0002AB32
		public void SetLanguage(string LangName)
		{
			if (LocalizationManager.HasLanguage(LangName, true, true, true))
			{
				LocalizationManager.CurrentLanguage = LangName;
			}
		}
	}
}
