using System;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020003BA RID: 954
	public class VideoCaptureState
	{
		// Token: 0x0600155F RID: 5471 RVA: 0x0002BEB1 File Offset: 0x0002A2B1
		internal VideoCaptureState(bool isCapturing, VideoCaptureMode captureMode, VideoQualityLevel qualityLevel, bool isOverlayVisible, bool isPaused)
		{
			this.mIsCapturing = isCapturing;
			this.mCaptureMode = captureMode;
			this.mQualityLevel = qualityLevel;
			this.mIsOverlayVisible = isOverlayVisible;
			this.mIsPaused = isPaused;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0002BEDE File Offset: 0x0002A2DE
		public bool IsCapturing
		{
			get
			{
				return this.mIsCapturing;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0002BEE6 File Offset: 0x0002A2E6
		public VideoCaptureMode CaptureMode
		{
			get
			{
				return this.mCaptureMode;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0002BEEE File Offset: 0x0002A2EE
		public VideoQualityLevel QualityLevel
		{
			get
			{
				return this.mQualityLevel;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x0002BEF6 File Offset: 0x0002A2F6
		public bool IsOverlayVisible
		{
			get
			{
				return this.mIsOverlayVisible;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0002BEFE File Offset: 0x0002A2FE
		public bool IsPaused
		{
			get
			{
				return this.mIsPaused;
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0002BF08 File Offset: 0x0002A308
		public override string ToString()
		{
			return string.Format("[VideoCaptureState: mIsCapturing={0}, mCaptureMode={1}, mQualityLevel={2}, mIsOverlayVisible={3}, mIsPaused={4}]", new object[]
			{
				this.mIsCapturing,
				this.mCaptureMode.ToString(),
				this.mQualityLevel.ToString(),
				this.mIsOverlayVisible,
				this.mIsPaused
			});
		}

		// Token: 0x04000D81 RID: 3457
		private bool mIsCapturing;

		// Token: 0x04000D82 RID: 3458
		private VideoCaptureMode mCaptureMode;

		// Token: 0x04000D83 RID: 3459
		private VideoQualityLevel mQualityLevel;

		// Token: 0x04000D84 RID: 3460
		private bool mIsOverlayVisible;

		// Token: 0x04000D85 RID: 3461
		private bool mIsPaused;
	}
}
