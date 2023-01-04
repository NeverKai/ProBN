using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B2 RID: 690
	[CallbackIdentity(154)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EncryptedAppTicketResponse_t
	{
		// Token: 0x0400071F RID: 1823
		public const int k_iCallback = 154;

		// Token: 0x04000720 RID: 1824
		public EResult m_eResult;
	}
}
