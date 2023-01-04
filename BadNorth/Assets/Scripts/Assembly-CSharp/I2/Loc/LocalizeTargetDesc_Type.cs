using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003F7 RID: 1015
	public class LocalizeTargetDesc_Type<T, G> : LocalizeTargetDesc<G> where T : UnityEngine.Object where G : LocalizeTarget<T>
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x0003B685 File Offset: 0x00039A85
		public override bool CanLocalize(Localize cmp)
		{
			return cmp.GetComponent<T>() != null;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0003B698 File Offset: 0x00039A98
		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			T component = cmp.GetComponent<T>();
			if (component == null)
			{
				return null;
			}
			G g = ScriptableObject.CreateInstance<G>();
			g.mTarget = component;
			return g;
		}
	}
}
