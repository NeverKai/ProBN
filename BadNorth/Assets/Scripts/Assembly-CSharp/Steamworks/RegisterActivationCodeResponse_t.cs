using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021E RID: 542
	[CallbackIdentity(1008)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RegisterActivationCodeResponse_t
	{
		// Token: 0x040004F3 RID: 1267
		public const int k_iCallback = 1008;

		// Token: 0x040004F4 RID: 1268
		public ERegisterActivationCodeResult m_eResult;

		// Token: 0x040004F5 RID: 1269
		public uint m_unPackageRegistered;
	}
}
