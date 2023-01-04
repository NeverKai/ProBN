using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000229 RID: 553
	[CallbackIdentity(337)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x04000518 RID: 1304
		public const int k_iCallback = 337;

		// Token: 0x04000519 RID: 1305
		public CSteamID m_steamIDFriend;

		// Token: 0x0400051A RID: 1306
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchConnect;
	}
}
