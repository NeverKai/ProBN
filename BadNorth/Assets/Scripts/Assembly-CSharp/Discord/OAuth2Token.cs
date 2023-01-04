using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000C4 RID: 196
	public struct OAuth2Token
	{
		// Token: 0x040003A0 RID: 928
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string AccessToken;

		// Token: 0x040003A1 RID: 929
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
		public string Scopes;

		// Token: 0x040003A2 RID: 930
		public long Expires;
	}
}
