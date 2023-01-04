using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000C3 RID: 195
	public struct User
	{
		// Token: 0x0400039B RID: 923
		public long Id;

		// Token: 0x0400039C RID: 924
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string Username;

		// Token: 0x0400039D RID: 925
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
		public string Discriminator;

		// Token: 0x0400039E RID: 926
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Avatar;

		// Token: 0x0400039F RID: 927
		public bool Bot;
	}
}
