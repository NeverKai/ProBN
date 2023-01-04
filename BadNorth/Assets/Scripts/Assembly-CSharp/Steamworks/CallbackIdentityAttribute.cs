using System;

namespace Steamworks
{
	// Token: 0x02000330 RID: 816
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : Attribute
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x0002709F File Offset: 0x0002549F
		public CallbackIdentityAttribute(int callbackNum)
		{
			this.Identity = callbackNum;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x000270AE File Offset: 0x000254AE
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x000270B6 File Offset: 0x000254B6
		public int Identity { get; set; }
	}
}
