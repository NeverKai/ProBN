using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000254 RID: 596
	[CallbackIdentity(4522)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SetCursor_t
	{
		// Token: 0x040005CC RID: 1484
		public const int k_iCallback = 4522;

		// Token: 0x040005CD RID: 1485
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005CE RID: 1486
		public uint eMouseCursor;
	}
}
