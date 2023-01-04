using System;

namespace GooglePlayGames.BasicApi.SavedGame
{
	// Token: 0x020003B1 RID: 945
	// (Invoke) Token: 0x06001531 RID: 5425
	public delegate void ConflictCallback(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData, ISavedGameMetadata unmerged, byte[] unmergedData);
}
