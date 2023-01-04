using System;
using System.Linq;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Video
{
	// Token: 0x020003B9 RID: 953
	public class VideoCapabilities
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x0002BD59 File Offset: 0x0002A159
		internal VideoCapabilities(bool isCameraSupported, bool isMicSupported, bool isWriteStorageSupported, bool[] captureModesSupported, bool[] qualityLevelsSupported)
		{
			this.mIsCameraSupported = isCameraSupported;
			this.mIsMicSupported = isMicSupported;
			this.mIsWriteStorageSupported = isWriteStorageSupported;
			this.mCaptureModesSupported = captureModesSupported;
			this.mQualityLevelsSupported = qualityLevelsSupported;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0002BD86 File Offset: 0x0002A186
		public bool IsCameraSupported
		{
			get
			{
				return this.mIsCameraSupported;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0002BD8E File Offset: 0x0002A18E
		public bool IsMicSupported
		{
			get
			{
				return this.mIsMicSupported;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0002BD96 File Offset: 0x0002A196
		public bool IsWriteStorageSupported
		{
			get
			{
				return this.mIsWriteStorageSupported;
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0002BD9E File Offset: 0x0002A19E
		public bool SupportsCaptureMode(VideoCaptureMode captureMode)
		{
			if (captureMode != VideoCaptureMode.Unknown)
			{
				return this.mCaptureModesSupported[(int)captureMode];
			}
			Logger.w("SupportsCaptureMode called with an unknown captureMode.");
			return false;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0002BDBB File Offset: 0x0002A1BB
		public bool SupportsQualityLevel(VideoQualityLevel qualityLevel)
		{
			if (qualityLevel != VideoQualityLevel.Unknown)
			{
				return this.mQualityLevelsSupported[(int)qualityLevel];
			}
			Logger.w("SupportsCaptureMode called with an unknown qualityLevel.");
			return false;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0002BDD8 File Offset: 0x0002A1D8
		public override string ToString()
		{
			string format = "[VideoCapabilities: mIsCameraSupported={0}, mIsMicSupported={1}, mIsWriteStorageSupported={2}, mCaptureModesSupported={3}, mQualityLevelsSupported={4}]";
			object[] array = new object[5];
			array[0] = this.mIsCameraSupported;
			array[1] = this.mIsMicSupported;
			array[2] = this.mIsWriteStorageSupported;
			array[3] = string.Join(",", (from p in this.mCaptureModesSupported
			select p.ToString()).ToArray<string>());
			array[4] = string.Join(",", (from p in this.mQualityLevelsSupported
			select p.ToString()).ToArray<string>());
			return string.Format(format, array);
		}

		// Token: 0x04000D7A RID: 3450
		private bool mIsCameraSupported;

		// Token: 0x04000D7B RID: 3451
		private bool mIsMicSupported;

		// Token: 0x04000D7C RID: 3452
		private bool mIsWriteStorageSupported;

		// Token: 0x04000D7D RID: 3453
		private bool[] mCaptureModesSupported;

		// Token: 0x04000D7E RID: 3454
		private bool[] mQualityLevelsSupported;
	}
}
