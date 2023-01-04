using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002B0 RID: 688
	[CallbackIdentity(143)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ValidateAuthTicketResponse_t
	{
		// Token: 0x04000717 RID: 1815
		public const int k_iCallback = 143;

		// Token: 0x04000718 RID: 1816
		public CSteamID m_SteamID;

		// Token: 0x04000719 RID: 1817
		public EAuthSessionResponse m_eAuthSessionResponse;

		// Token: 0x0400071A RID: 1818
		public CSteamID m_OwnerSteamID;
	}
}
