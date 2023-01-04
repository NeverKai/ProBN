using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000244 RID: 580
	[CallbackIdentity(4502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NeedsPaint_t
	{
		// Token: 0x04000577 RID: 1399
		public const int k_iCallback = 4502;

		// Token: 0x04000578 RID: 1400
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000579 RID: 1401
		public IntPtr pBGRA;

		// Token: 0x0400057A RID: 1402
		public uint unWide;

		// Token: 0x0400057B RID: 1403
		public uint unTall;

		// Token: 0x0400057C RID: 1404
		public uint unUpdateX;

		// Token: 0x0400057D RID: 1405
		public uint unUpdateY;

		// Token: 0x0400057E RID: 1406
		public uint unUpdateWide;

		// Token: 0x0400057F RID: 1407
		public uint unUpdateTall;

		// Token: 0x04000580 RID: 1408
		public uint unScrollX;

		// Token: 0x04000581 RID: 1409
		public uint unScrollY;

		// Token: 0x04000582 RID: 1410
		public float flPageScale;

		// Token: 0x04000583 RID: 1411
		public uint unPageSerial;
	}
}
