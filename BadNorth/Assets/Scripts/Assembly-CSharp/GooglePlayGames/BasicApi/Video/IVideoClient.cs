using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020003B8 RID: 952
	public interface IVideoClient
	{
		// Token: 0x0600154F RID: 5455
		void GetCaptureCapabilities(Action<ResponseStatus, VideoCapabilities> callback);

		// Token: 0x06001550 RID: 5456
		void ShowCaptureOverlay();

		// Token: 0x06001551 RID: 5457
		void GetCaptureState(Action<ResponseStatus, VideoCaptureState> callback);

		// Token: 0x06001552 RID: 5458
		void IsCaptureAvailable(VideoCaptureMode captureMode, Action<ResponseStatus, bool> callback);

		// Token: 0x06001553 RID: 5459
		bool IsCaptureSupported();

		// Token: 0x06001554 RID: 5460
		void RegisterCaptureOverlayStateChangedListener(CaptureOverlayStateListener listener);

		// Token: 0x06001555 RID: 5461
		void UnregisterCaptureOverlayStateChangedListener();
	}
}
