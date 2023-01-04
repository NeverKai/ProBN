using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000259 RID: 601
	[CallbackIdentity(2101)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestCompleted_t
	{
		// Token: 0x040005DA RID: 1498
		public const int k_iCallback = 2101;

		// Token: 0x040005DB RID: 1499
		public HTTPRequestHandle m_hRequest;

		// Token: 0x040005DC RID: 1500
		public ulong m_ulContextValue;

		// Token: 0x040005DD RID: 1501
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bRequestSuccessful;

		// Token: 0x040005DE RID: 1502
		public EHTTPStatusCode m_eStatusCode;

		// Token: 0x040005DF RID: 1503
		public uint m_unBodySize;
	}
}
