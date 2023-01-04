using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000279 RID: 633
	[CallbackIdentity(4013)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		// Token: 0x04000635 RID: 1589
		public const int k_iCallback = 4013;

		// Token: 0x04000636 RID: 1590
		public int nID;
	}
}
