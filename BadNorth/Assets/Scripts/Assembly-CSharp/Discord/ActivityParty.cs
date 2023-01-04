using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000CA RID: 202
	public struct ActivityParty
	{
		// Token: 0x040003B0 RID: 944
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Id;

		// Token: 0x040003B1 RID: 945
		public PartySize Size;
	}
}
