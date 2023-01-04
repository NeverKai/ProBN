using System;

namespace I2.Loc
{
	// Token: 0x020003F9 RID: 1017
	public class LocalizeTargetDesc_Child : LocalizeTargetDesc<LocalizeTarget_UnityStandard_Child>
	{
		// Token: 0x06001782 RID: 6018 RVA: 0x0003B7EC File Offset: 0x00039BEC
		public override bool CanLocalize(Localize cmp)
		{
			return cmp.transform.childCount > 1;
		}
	}
}
