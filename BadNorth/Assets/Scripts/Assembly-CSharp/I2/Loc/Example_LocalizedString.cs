using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C2 RID: 962
	public class Example_LocalizedString : MonoBehaviour
	{
		// Token: 0x0600158D RID: 5517 RVA: 0x0002C750 File Offset: 0x0002AB50
		public void Start()
		{
			Debug.Log(this._MyLocalizedString);
			Debug.Log(LocalizationManager.GetTranslation(this._NormalString, true, 0, true, false, null, null));
			Debug.Log(LocalizationManager.GetTranslation(this._StringWithTermPopup, true, 0, true, false, null, null));
			LocalizedString s = "Term2";
			string message = s;
			Debug.Log(message);
			LocalizedString myLocalizedString = this._MyLocalizedString;
			Debug.Log(myLocalizedString);
			LocalizedString localizedString = "Term3";
			Debug.Log(localizedString);
			LocalizedString localizedString2 = "Term3";
			localizedString2.mRTL_IgnoreArabicFix = true;
			Debug.Log(localizedString2);
			LocalizedString localizedString3 = "Term3";
			localizedString3.mRTL_ConvertNumbers = true;
			localizedString3.mRTL_MaxLineLength = 20;
			Debug.Log(localizedString3);
			LocalizedString localizedString4 = localizedString3;
			Debug.Log(localizedString4);
		}

		// Token: 0x04000DA6 RID: 3494
		public LocalizedString _MyLocalizedString;

		// Token: 0x04000DA7 RID: 3495
		public string _NormalString;

		// Token: 0x04000DA8 RID: 3496
		[TermsPopup("")]
		public string _StringWithTermPopup;
	}
}
