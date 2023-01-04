using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000322 RID: 802
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CallbackMsg_t
	{
		// Token: 0x04000C1E RID: 3102
		public int m_hSteamUser;

		// Token: 0x04000C1F RID: 3103
		public int m_iCallback;

		// Token: 0x04000C20 RID: 3104
		public IntPtr m_pubParam;

		// Token: 0x04000C21 RID: 3105
		public int m_cubParam;
	}
}
