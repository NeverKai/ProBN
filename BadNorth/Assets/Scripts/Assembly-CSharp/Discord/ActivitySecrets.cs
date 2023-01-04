using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000CB RID: 203
	public struct ActivitySecrets
	{
		// Token: 0x040003B2 RID: 946
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Match;

		// Token: 0x040003B3 RID: 947
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Join;

		// Token: 0x040003B4 RID: 948
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Spectate;
	}
}
