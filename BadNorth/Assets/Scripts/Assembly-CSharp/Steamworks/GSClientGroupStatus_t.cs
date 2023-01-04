using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200023C RID: 572
	[CallbackIdentity(208)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GSClientGroupStatus_t
	{
		// Token: 0x04000558 RID: 1368
		public const int k_iCallback = 208;

		// Token: 0x04000559 RID: 1369
		public CSteamID m_SteamIDUser;

		// Token: 0x0400055A RID: 1370
		public CSteamID m_SteamIDGroup;

		// Token: 0x0400055B RID: 1371
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bMember;

		// Token: 0x0400055C RID: 1372
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bOfficer;
	}
}
