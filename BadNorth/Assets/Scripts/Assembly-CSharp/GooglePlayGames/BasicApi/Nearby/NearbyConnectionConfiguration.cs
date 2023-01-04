using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020003AD RID: 941
	public struct NearbyConnectionConfiguration
	{
		// Token: 0x0600152D RID: 5421 RVA: 0x0002BC2D File Offset: 0x0002A02D
		public NearbyConnectionConfiguration(Action<InitializationStatus> callback, long localClientId)
		{
			this.mInitializationCallback = Misc.CheckNotNull<Action<InitializationStatus>>(callback);
			this.mLocalClientId = localClientId;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0002BC42 File Offset: 0x0002A042
		public long LocalClientId
		{
			get
			{
				return this.mLocalClientId;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0002BC4A File Offset: 0x0002A04A
		public Action<InitializationStatus> InitializationCallback
		{
			get
			{
				return this.mInitializationCallback;
			}
		}

		// Token: 0x04000D58 RID: 3416
		public const int MaxUnreliableMessagePayloadLength = 1168;

		// Token: 0x04000D59 RID: 3417
		public const int MaxReliableMessagePayloadLength = 4096;

		// Token: 0x04000D5A RID: 3418
		private readonly Action<InitializationStatus> mInitializationCallback;

		// Token: 0x04000D5B RID: 3419
		private readonly long mLocalClientId;
	}
}
