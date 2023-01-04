using System;

namespace Steamworks
{
	// Token: 0x020002E7 RID: 743
	[Flags]
	public enum ERemoteStoragePlatform
	{
		// Token: 0x040009A6 RID: 2470
		k_ERemoteStoragePlatformNone = 0,
		// Token: 0x040009A7 RID: 2471
		k_ERemoteStoragePlatformWindows = 1,
		// Token: 0x040009A8 RID: 2472
		k_ERemoteStoragePlatformOSX = 2,
		// Token: 0x040009A9 RID: 2473
		k_ERemoteStoragePlatformPS3 = 4,
		// Token: 0x040009AA RID: 2474
		k_ERemoteStoragePlatformLinux = 8,
		// Token: 0x040009AB RID: 2475
		k_ERemoteStoragePlatformReserved2 = 16,
		// Token: 0x040009AC RID: 2476
		k_ERemoteStoragePlatformAll = -1
	}
}
