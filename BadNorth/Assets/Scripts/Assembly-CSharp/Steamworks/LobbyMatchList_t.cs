using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000267 RID: 615
	[CallbackIdentity(510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyMatchList_t
	{
		// Token: 0x04000617 RID: 1559
		public const int k_iCallback = 510;

		// Token: 0x04000618 RID: 1560
		public uint m_nLobbiesMatching;
	}
}
