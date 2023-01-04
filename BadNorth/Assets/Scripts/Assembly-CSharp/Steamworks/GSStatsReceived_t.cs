using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000240 RID: 576
	[CallbackIdentity(1800)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GSStatsReceived_t
	{
		// Token: 0x0400056D RID: 1389
		public const int k_iCallback = 1800;

		// Token: 0x0400056E RID: 1390
		public EResult m_eResult;

		// Token: 0x0400056F RID: 1391
		public CSteamID m_steamIDUser;
	}
}
