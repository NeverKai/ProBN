using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020002C4 RID: 708
	[CallbackIdentity(703)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAPICallCompleted_t
	{
		// Token: 0x04000759 RID: 1881
		public const int k_iCallback = 703;

		// Token: 0x0400075A RID: 1882
		public SteamAPICall_t m_hAsyncCall;

		// Token: 0x0400075B RID: 1883
		public int m_iCallback;

		// Token: 0x0400075C RID: 1884
		public uint m_cubParam;
	}
}
