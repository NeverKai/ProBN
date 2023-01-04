using System;
using System.Collections.Generic;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003B2 RID: 946
	public interface ISavedGameClient
	{
		// Token: 0x06001534 RID: 5428
		void OpenWithAutomaticConflictResolution(string filename, DataSource source, ConflictResolutionStrategy resolutionStrategy, Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

		// Token: 0x06001535 RID: 5429
		void OpenWithManualConflictResolution(string filename, DataSource source, bool prefetchDataOnConflict, ConflictCallback conflictCallback, Action<SavedGameRequestStatus, ISavedGameMetadata> completedCallback);

		// Token: 0x06001536 RID: 5430
		void ReadBinaryData(ISavedGameMetadata metadata, Action<SavedGameRequestStatus, byte[]> completedCallback);

		// Token: 0x06001537 RID: 5431
		void ShowSelectSavedGameUI(string uiTitle, uint maxDisplayedSavedGames, bool showCreateSaveUI, bool showDeleteSaveUI, Action<SelectUIStatus, ISavedGameMetadata> callback);

		// Token: 0x06001538 RID: 5432
		void CommitUpdate(ISavedGameMetadata metadata, SavedGameMetadataUpdate updateForMetadata, byte[] updatedBinaryData, Action<SavedGameRequestStatus, ISavedGameMetadata> callback);

		// Token: 0x06001539 RID: 5433
		void FetchAllSavedGames(DataSource source, Action<SavedGameRequestStatus, List<ISavedGameMetadata>> callback);

		// Token: 0x0600153A RID: 5434
		void Delete(ISavedGameMetadata metadata);
	}
}
