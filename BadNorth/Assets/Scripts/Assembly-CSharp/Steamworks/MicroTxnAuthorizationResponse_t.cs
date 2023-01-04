using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B1 RID: 689
	[CallbackIdentity(152)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MicroTxnAuthorizationResponse_t
	{
		// Token: 0x0400071B RID: 1819
		public const int k_iCallback = 152;

		// Token: 0x0400071C RID: 1820
		public uint m_unAppID;

		// Token: 0x0400071D RID: 1821
		public ulong m_ulOrderID;

		// Token: 0x0400071E RID: 1822
		public byte m_bAuthorized;
	}
}
