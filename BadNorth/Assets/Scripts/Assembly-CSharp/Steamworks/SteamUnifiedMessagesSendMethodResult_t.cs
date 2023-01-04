using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002A9 RID: 681
	[CallbackIdentity(2501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUnifiedMessagesSendMethodResult_t
	{
		// Token: 0x04000703 RID: 1795
		public const int k_iCallback = 2501;

		// Token: 0x04000704 RID: 1796
		public ClientUnifiedMessageHandle m_hHandle;

		// Token: 0x04000705 RID: 1797
		public ulong m_unContext;

		// Token: 0x04000706 RID: 1798
		public EResult m_eResult;

		// Token: 0x04000707 RID: 1799
		public uint m_unResponseSize;
	}
}
