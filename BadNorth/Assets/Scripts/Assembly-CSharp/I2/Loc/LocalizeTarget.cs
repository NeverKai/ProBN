using System;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003F4 RID: 1012
	public abstract class LocalizeTarget<T> : ILocalizeTarget where T : UnityEngine.Object
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x0003B5C0 File Offset: 0x000399C0
		public override bool IsValid(Localize cmp)
		{
			if (this.mTarget != null)
			{
				Component component = this.mTarget as Component;
				if (component != null && component.gameObject != cmp.gameObject)
				{
					this.mTarget = (T)((object)null);
				}
			}
			if (this.mTarget == null)
			{
				this.mTarget = cmp.GetComponent<T>();
			}
			return this.mTarget != null;
		}

		// Token: 0x04000E89 RID: 3721
		public T mTarget;
	}
}
