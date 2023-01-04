using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200025B RID: 603
	[CallbackIdentity(2103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestDataReceived_t
	{
		// Token: 0x040005E3 RID: 1507
		public const int k_iCallback = 2103;

		// Token: 0x040005E4 RID: 1508
		public HTTPRequestHandle m_hRequest;

		// Token: 0x040005E5 RID: 1509
		public ulong m_ulContextValue;

		// Token: 0x040005E6 RID: 1510
		public uint m_cOffset;

		// Token: 0x040005E7 RID: 1511
		public uint m_cBytesReceived;
	}
}
