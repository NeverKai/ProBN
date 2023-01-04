using System;

namespace Steamworks
{
	// Token: 0x02000313 RID: 787
	public enum EBroadcastUploadResult
	{
		// Token: 0x04000B77 RID: 2935
		k_EBroadcastUploadResultNone,
		// Token: 0x04000B78 RID: 2936
		k_EBroadcastUploadResultOK,
		// Token: 0x04000B79 RID: 2937
		k_EBroadcastUploadResultInitFailed,
		// Token: 0x04000B7A RID: 2938
		k_EBroadcastUploadResultFrameFailed,
		// Token: 0x04000B7B RID: 2939
		k_EBroadcastUploadResultTimeout,
		// Token: 0x04000B7C RID: 2940
		k_EBroadcastUploadResultBandwidthExceeded,
		// Token: 0x04000B7D RID: 2941
		k_EBroadcastUploadResultLowFPS,
		// Token: 0x04000B7E RID: 2942
		k_EBroadcastUploadResultMissingKeyFrames,
		// Token: 0x04000B7F RID: 2943
		k_EBroadcastUploadResultNoConnection,
		// Token: 0x04000B80 RID: 2944
		k_EBroadcastUploadResultRelayFailed,
		// Token: 0x04000B81 RID: 2945
		k_EBroadcastUploadResultSettingsChanged,
		// Token: 0x04000B82 RID: 2946
		k_EBroadcastUploadResultMissingAudio,
		// Token: 0x04000B83 RID: 2947
		k_EBroadcastUploadResultTooFarBehind,
		// Token: 0x04000B84 RID: 2948
		k_EBroadcastUploadResultTranscodeBehind
	}
}
