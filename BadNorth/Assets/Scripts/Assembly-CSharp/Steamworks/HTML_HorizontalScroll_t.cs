using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024D RID: 589
	[CallbackIdentity(4511)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HorizontalScroll_t
	{
		// Token: 0x040005A5 RID: 1445
		public const int k_iCallback = 4511;

		// Token: 0x040005A6 RID: 1446
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005A7 RID: 1447
		public uint unScrollMax;

		// Token: 0x040005A8 RID: 1448
		public uint unScrollCurrent;

		// Token: 0x040005A9 RID: 1449
		public float flPageScale;

		// Token: 0x040005AA RID: 1450
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x040005AB RID: 1451
		public uint unPageSize;
	}
}
