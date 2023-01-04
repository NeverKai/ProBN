using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003F3 RID: 1011
	public abstract class ILocalizeTarget : ScriptableObject
	{
		// Token: 0x06001763 RID: 5987
		public abstract bool IsValid(Localize cmp);

		// Token: 0x06001764 RID: 5988
		public abstract void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm);

		// Token: 0x06001765 RID: 5989
		public abstract void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation);

		// Token: 0x06001766 RID: 5990
		public abstract bool CanUseSecondaryTerm();

		// Token: 0x06001767 RID: 5991
		public abstract bool AllowMainTermToBeRTL();

		// Token: 0x06001768 RID: 5992
		public abstract bool AllowSecondTermToBeRTL();

		// Token: 0x06001769 RID: 5993
		public abstract eTermType GetPrimaryTermType(Localize cmp);

		// Token: 0x0600176A RID: 5994
		public abstract eTermType GetSecondaryTermType(Localize cmp);
	}
}
