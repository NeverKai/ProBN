using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003B4 RID: 948
	public interface ISavedGameMetadata
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600153D RID: 5437
		bool IsOpen { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600153E RID: 5438
		string Filename { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600153F RID: 5439
		string Description { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06001540 RID: 5440
		string CoverImageURL { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06001541 RID: 5441
		TimeSpan TotalTimePlayed { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06001542 RID: 5442
		DateTime LastModifiedTimestamp { get; }
	}
}
