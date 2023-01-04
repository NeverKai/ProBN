using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000585 RID: 1413
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public struct CampaignPrefs
	{
		// Token: 0x06002493 RID: 9363 RVA: 0x00072EE4 File Offset: 0x000712E4
		public CampaignPrefs(Difficulty difficulty, bool skipTutorial = false, bool allowReplays = false)
		{
			this.difficulty = difficulty;
			this.skipTutorial = skipTutorial;
			this.allowReplays = allowReplays;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00072EFB File Offset: 0x000712FB
		public override string ToString()
		{
			return string.Format("{0}, Skip Tutorial = {1}, Allow Replays = {2}", this.difficulty, this.skipTutorial, this.allowReplays);
		}

		// Token: 0x04001716 RID: 5910
		public Difficulty difficulty;

		// Token: 0x04001717 RID: 5911
		public bool skipTutorial;

		// Token: 0x04001718 RID: 5912
		public bool allowReplays;
	}
}
