using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000246 RID: 582
	[CallbackIdentity(4504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CloseBrowser_t
	{
		// Token: 0x0400058A RID: 1418
		public const int k_iCallback = 4504;

		// Token: 0x0400058B RID: 1419
		public HHTMLBrowser unBrowserHandle;
	}
}
