using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000D3 RID: 211
	public struct Sku
	{
		// Token: 0x040003D2 RID: 978
		public long Id;

		// Token: 0x040003D3 RID: 979
		public SkuType Type;

		// Token: 0x040003D4 RID: 980
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Name;

		// Token: 0x040003D5 RID: 981
		public SkuPrice Price;
	}
}
