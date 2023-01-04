using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000D0 RID: 208
	public struct FileStat
	{
		// Token: 0x040003CA RID: 970
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string Filename;

		// Token: 0x040003CB RID: 971
		public ulong Size;

		// Token: 0x040003CC RID: 972
		public ulong LastModified;
	}
}
