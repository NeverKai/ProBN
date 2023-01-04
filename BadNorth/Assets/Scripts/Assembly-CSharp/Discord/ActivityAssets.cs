using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000C8 RID: 200
	public struct ActivityAssets
	{
		// Token: 0x040003AA RID: 938
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string LargeImage;

		// Token: 0x040003AB RID: 939
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string LargeText;

		// Token: 0x040003AC RID: 940
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string SmallImage;

		// Token: 0x040003AD RID: 941
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string SmallText;
	}
}
