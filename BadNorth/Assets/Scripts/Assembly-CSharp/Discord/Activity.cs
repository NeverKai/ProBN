using System;
using System.Runtime.InteropServices;

namespace Discord
{
	// Token: 0x020000CC RID: 204
	public struct Activity
	{
		// Token: 0x040003B5 RID: 949
		public ActivityType Type;

		// Token: 0x040003B6 RID: 950
		public long ApplicationId;

		// Token: 0x040003B7 RID: 951
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Name;

		// Token: 0x040003B8 RID: 952
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string State;

		// Token: 0x040003B9 RID: 953
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string Details;

		// Token: 0x040003BA RID: 954
		public ActivityTimestamps Timestamps;

		// Token: 0x040003BB RID: 955
		public ActivityAssets Assets;

		// Token: 0x040003BC RID: 956
		public ActivityParty Party;

		// Token: 0x040003BD RID: 957
		public ActivitySecrets Secrets;

		// Token: 0x040003BE RID: 958
		public bool Instance;
	}
}
