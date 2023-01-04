using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024C RID: 588
	[CallbackIdentity(4510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CanGoBackAndForward_t
	{
		// Token: 0x040005A1 RID: 1441
		public const int k_iCallback = 4510;

		// Token: 0x040005A2 RID: 1442
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005A3 RID: 1443
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoBack;

		// Token: 0x040005A4 RID: 1444
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoForward;
	}
}
