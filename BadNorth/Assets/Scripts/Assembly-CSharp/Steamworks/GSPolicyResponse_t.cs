using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200023A RID: 570
	[CallbackIdentity(115)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSPolicyResponse_t
	{
		// Token: 0x04000551 RID: 1361
		public const int k_iCallback = 115;

		// Token: 0x04000552 RID: 1362
		public byte m_bSecure;
	}
}
