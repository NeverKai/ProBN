using System;
using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003B5 RID: 949
	public struct SavedGameMetadataUpdate
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x0002BC54 File Offset: 0x0002A054
		private SavedGameMetadataUpdate(SavedGameMetadataUpdate.Builder builder)
		{
			this.mDescriptionUpdated = builder.mDescriptionUpdated;
			this.mNewDescription = builder.mNewDescription;
			this.mCoverImageUpdated = builder.mCoverImageUpdated;
			this.mNewPngCoverImage = builder.mNewPngCoverImage;
			this.mNewPlayedTime = builder.mNewPlayedTime;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0002BCA2 File Offset: 0x0002A0A2
		public bool IsDescriptionUpdated
		{
			get
			{
				return this.mDescriptionUpdated;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0002BCAA File Offset: 0x0002A0AA
		public string UpdatedDescription
		{
			get
			{
				return this.mNewDescription;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0002BCB2 File Offset: 0x0002A0B2
		public bool IsCoverImageUpdated
		{
			get
			{
				return this.mCoverImageUpdated;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0002BCBA File Offset: 0x0002A0BA
		public byte[] UpdatedPngCoverImage
		{
			get
			{
				return this.mNewPngCoverImage;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0002BCC4 File Offset: 0x0002A0C4
		public bool IsPlayedTimeUpdated
		{
			get
			{
				return this.mNewPlayedTime != null;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0002BCDF File Offset: 0x0002A0DF
		public TimeSpan? UpdatedPlayedTime
		{
			get
			{
				return this.mNewPlayedTime;
			}
		}

		// Token: 0x04000D70 RID: 3440
		private readonly bool mDescriptionUpdated;

		// Token: 0x04000D71 RID: 3441
		private readonly string mNewDescription;

		// Token: 0x04000D72 RID: 3442
		private readonly bool mCoverImageUpdated;

		// Token: 0x04000D73 RID: 3443
		private readonly byte[] mNewPngCoverImage;

		// Token: 0x04000D74 RID: 3444
		private readonly TimeSpan? mNewPlayedTime;

		// Token: 0x020003B6 RID: 950
		public struct Builder
		{
			// Token: 0x0600154A RID: 5450 RVA: 0x0002BCE7 File Offset: 0x0002A0E7
			public SavedGameMetadataUpdate.Builder WithUpdatedDescription(string description)
			{
				this.mNewDescription = Misc.CheckNotNull<string>(description);
				this.mDescriptionUpdated = true;
				return this;
			}

			// Token: 0x0600154B RID: 5451 RVA: 0x0002BD02 File Offset: 0x0002A102
			public SavedGameMetadataUpdate.Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
			{
				this.mCoverImageUpdated = true;
				this.mNewPngCoverImage = newPngCoverImage;
				return this;
			}

			// Token: 0x0600154C RID: 5452 RVA: 0x0002BD18 File Offset: 0x0002A118
			public SavedGameMetadataUpdate.Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
			{
				if (newPlayedTime.TotalMilliseconds > 1.8446744073709552E+19)
				{
					throw new InvalidOperationException("Timespans longer than ulong.MaxValue milliseconds are not allowed");
				}
				this.mNewPlayedTime = new TimeSpan?(newPlayedTime);
				return this;
			}

			// Token: 0x0600154D RID: 5453 RVA: 0x0002BD4C File Offset: 0x0002A14C
			public SavedGameMetadataUpdate Build()
			{
				return new SavedGameMetadataUpdate(this);
			}

			// Token: 0x04000D75 RID: 3445
			internal bool mDescriptionUpdated;

			// Token: 0x04000D76 RID: 3446
			internal string mNewDescription;

			// Token: 0x04000D77 RID: 3447
			internal bool mCoverImageUpdated;

			// Token: 0x04000D78 RID: 3448
			internal byte[] mNewPngCoverImage;

			// Token: 0x04000D79 RID: 3449
			internal TimeSpan? mNewPlayedTime;
		}
	}
}
