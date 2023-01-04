using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200025A RID: 602
	[CallbackIdentity(2102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestHeadersReceived_t
	{
		// Token: 0x040005E0 RID: 1504
		public const int k_iCallback = 2102;

		// Token: 0x040005E1 RID: 1505
		public HTTPRequestHandle m_hRequest;

		// Token: 0x040005E2 RID: 1506
		public ulong m_ulContextValue;
	}
}
