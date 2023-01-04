using System;

namespace I2.Loc
{
	// Token: 0x020003F5 RID: 1013
	public abstract class ILocalizeTargetDescriptor
	{
		// Token: 0x0600176E RID: 5998
		public abstract bool CanLocalize(Localize cmp);

		// Token: 0x0600176F RID: 5999
		public abstract ILocalizeTarget CreateTarget(Localize cmp);

		// Token: 0x06001770 RID: 6000
		public abstract Type GetTargetType();

		// Token: 0x04000E8A RID: 3722
		public string Name;

		// Token: 0x04000E8B RID: 3723
		public int Priority;
	}
}
