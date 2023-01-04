using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000D2 RID: 210
	public struct SkuPrice
	{
		// Token: 0x040003D0 RID: 976
		public uint Amount;

		// Token: 0x040003D1 RID: 977
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string Currency;
	}
}
