using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000220 RID: 544
	[CallbackIdentity(1021)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x040004F7 RID: 1271
		public const int k_iCallback = 1021;

		// Token: 0x040004F8 RID: 1272
		public EResult m_eResult;

		// Token: 0x040004F9 RID: 1273
		public uint m_nAppID;

		// Token: 0x040004FA RID: 1274
		public uint m_cchKeyLength;

		// Token: 0x040004FB RID: 1275
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		public string m_rgchKey;
	}
}
