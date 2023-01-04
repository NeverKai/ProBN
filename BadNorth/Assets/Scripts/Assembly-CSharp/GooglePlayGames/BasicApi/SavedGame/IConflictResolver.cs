using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003B3 RID: 947
	public interface IConflictResolver
	{
		// Token: 0x0600153B RID: 5435
		void ChooseMetadata(ISavedGameMetadata chosenMetadata);

		// Token: 0x0600153C RID: 5436
		void ResolveConflict(ISavedGameMetadata chosenMetadata, SavedGameMetadataUpdate metadataUpdate, byte[] updatedData);
	}
}
