using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B3 RID: 691
	[CallbackIdentity(163)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAuthSessionTicketResponse_t
	{
		// Token: 0x04000721 RID: 1825
		public const int k_iCallback = 163;

		// Token: 0x04000722 RID: 1826
		public HAuthTicket m_hAuthTicket;

		// Token: 0x04000723 RID: 1827
		public EResult m_eResult;
	}
}
