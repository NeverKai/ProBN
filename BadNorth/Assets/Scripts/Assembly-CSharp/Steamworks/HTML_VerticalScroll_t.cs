using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200024E RID: 590
	[CallbackIdentity(4512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_VerticalScroll_t
	{
		// Token: 0x040005AC RID: 1452
		public const int k_iCallback = 4512;

		// Token: 0x040005AD RID: 1453
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005AE RID: 1454
		public uint unScrollMax;

		// Token: 0x040005AF RID: 1455
		public uint unScrollCurrent;

		// Token: 0x040005B0 RID: 1456
		public float flPageScale;

		// Token: 0x040005B1 RID: 1457
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x040005B2 RID: 1458
		public uint unPageSize;
	}
}
