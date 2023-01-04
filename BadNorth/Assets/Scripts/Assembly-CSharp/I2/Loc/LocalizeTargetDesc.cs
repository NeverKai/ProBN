using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003F6 RID: 1014
	public abstract class LocalizeTargetDesc<T> : ILocalizeTargetDescriptor where T : ILocalizeTarget
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x0003B665 File Offset: 0x00039A65
		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			return ScriptableObject.CreateInstance<T>();
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0003B671 File Offset: 0x00039A71
		public override Type GetTargetType()
		{
			return typeof(T);
		}
	}
}
