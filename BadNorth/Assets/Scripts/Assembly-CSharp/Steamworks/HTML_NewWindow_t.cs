using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000253 RID: 595
	[CallbackIdentity(4521)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NewWindow_t
	{
		// Token: 0x040005C4 RID: 1476
		public const int k_iCallback = 4521;

		// Token: 0x040005C5 RID: 1477
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005C6 RID: 1478
		public string pchURL;

		// Token: 0x040005C7 RID: 1479
		public uint unX;

		// Token: 0x040005C8 RID: 1480
		public uint unY;

		// Token: 0x040005C9 RID: 1481
		public uint unWide;

		// Token: 0x040005CA RID: 1482
		public uint unTall;

		// Token: 0x040005CB RID: 1483
		public HHTMLBrowser unNewWindow_BrowserHandle;
	}
}
