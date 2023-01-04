using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000252 RID: 594
	[CallbackIdentity(4516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FileOpenDialog_t
	{
		// Token: 0x040005C0 RID: 1472
		public const int k_iCallback = 4516;

		// Token: 0x040005C1 RID: 1473
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005C2 RID: 1474
		public string pchTitle;

		// Token: 0x040005C3 RID: 1475
		public string pchInitialFile;
	}
}
