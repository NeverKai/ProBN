using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024F RID: 591
	[CallbackIdentity(4513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_LinkAtPosition_t
	{
		// Token: 0x040005B3 RID: 1459
		public const int k_iCallback = 4513;

		// Token: 0x040005B4 RID: 1460
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005B5 RID: 1461
		public uint x;

		// Token: 0x040005B6 RID: 1462
		public uint y;

		// Token: 0x040005B7 RID: 1463
		public string pchURL;

		// Token: 0x040005B8 RID: 1464
		[MarshalAs(UnmanagedType.I1)]
		public bool bInput;

		// Token: 0x040005B9 RID: 1465
		[MarshalAs(UnmanagedType.I1)]
		public bool bLiveLink;
	}
}
