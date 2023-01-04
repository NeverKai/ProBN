using System;

namespace Steamworks
{
	// Token: 0x0200030C RID: 780
	[Flags]
	public enum EAppType
	{
		// Token: 0x04000B32 RID: 2866
		k_EAppType_Invalid = 0,
		// Token: 0x04000B33 RID: 2867
		k_EAppType_Game = 1,
		// Token: 0x04000B34 RID: 2868
		k_EAppType_Application = 2,
		// Token: 0x04000B35 RID: 2869
		k_EAppType_Tool = 4,
		// Token: 0x04000B36 RID: 2870
		k_EAppType_Demo = 8,
		// Token: 0x04000B37 RID: 2871
		k_EAppType_Media_DEPRECATED = 16,
		// Token: 0x04000B38 RID: 2872
		k_EAppType_DLC = 32,
		// Token: 0x04000B39 RID: 2873
		k_EAppType_Guide = 64,
		// Token: 0x04000B3A RID: 2874
		k_EAppType_Driver = 128,
		// Token: 0x04000B3B RID: 2875
		k_EAppType_Config = 256,
		// Token: 0x04000B3C RID: 2876
		k_EAppType_Hardware = 512,
		// Token: 0x04000B3D RID: 2877
		k_EAppType_Franchise = 1024,
		// Token: 0x04000B3E RID: 2878
		k_EAppType_Video = 2048,
		// Token: 0x04000B3F RID: 2879
		k_EAppType_Plugin = 4096,
		// Token: 0x04000B40 RID: 2880
		k_EAppType_Music = 8192,
		// Token: 0x04000B41 RID: 2881
		k_EAppType_Series = 16384,
		// Token: 0x04000B42 RID: 2882
		k_EAppType_Shortcut = 1073741824,
		// Token: 0x04000B43 RID: 2883
		k_EAppType_DepotOnly = -2147483647
	}
}
