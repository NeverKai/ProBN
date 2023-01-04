using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000227 RID: 551
	[CallbackIdentity(335)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClanOfficerListResponse_t
	{
		// Token: 0x04000511 RID: 1297
		public const int k_iCallback = 335;

		// Token: 0x04000512 RID: 1298
		public CSteamID m_steamIDClan;

		// Token: 0x04000513 RID: 1299
		public int m_cOfficers;

		// Token: 0x04000514 RID: 1300
		public byte m_bSuccess;
	}
}
