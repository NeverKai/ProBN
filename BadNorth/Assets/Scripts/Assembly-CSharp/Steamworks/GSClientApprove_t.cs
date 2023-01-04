using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000236 RID: 566
	[CallbackIdentity(201)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientApprove_t
	{
		// Token: 0x04000543 RID: 1347
		public const int k_iCallback = 201;

		// Token: 0x04000544 RID: 1348
		public CSteamID m_SteamID;

		// Token: 0x04000545 RID: 1349
		public CSteamID m_OwnerSteamID;
	}
}
