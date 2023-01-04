using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000247 RID: 583
	[CallbackIdentity(4505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_URLChanged_t
	{
		// Token: 0x0400058C RID: 1420
		public const int k_iCallback = 4505;

		// Token: 0x0400058D RID: 1421
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400058E RID: 1422
		public string pchURL;

		// Token: 0x0400058F RID: 1423
		public string pchPostData;

		// Token: 0x04000590 RID: 1424
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;

		// Token: 0x04000591 RID: 1425
		public string pchPageTitle;

		// Token: 0x04000592 RID: 1426
		[MarshalAs(UnmanagedType.I1)]
		public bool bNewNavigation;
	}
}
