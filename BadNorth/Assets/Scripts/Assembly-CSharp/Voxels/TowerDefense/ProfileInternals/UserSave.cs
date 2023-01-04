using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x020005A4 RID: 1444
	[Serializable]
	public class UserSave : ISaveGameObject
	{
		// Token: 0x060025A4 RID: 9636 RVA: 0x0007704C File Offset: 0x0007544C
		public UserSave()
		{
			this.loadTime = Time.unscaledTime;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000770A0 File Offset: 0x000754A0
		public int timeSinceLoad
		{
			get
			{
				return (int)(Time.unscaledTime - this.loadTime);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000770AF File Offset: 0x000754AF
		public int playTime
		{
			get
			{
				return this.timeSinceLoad + this.savedPlayTime;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000770BE File Offset: 0x000754BE
		string ISaveGameObject.fileName
		{
			get
			{
				return "user";
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000770C8 File Offset: 0x000754C8
		public void RegisterCampaignWin(Difficulty difficulty)
		{
			int num;
			if (this.completionCounts.TryGetValue(difficulty, out num))
			{
				this.completionCounts[difficulty] = num + 1;
			}
			else
			{
				this.completionCounts.Add(difficulty, 1);
			}
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00077109 File Offset: 0x00075509
		[OnSerializing]
		private void PreSave(StreamingContext context)
		{
			this.savedPlayTime = this.playTime;
			this.loadTime = Time.unscaledTime;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00077124 File Offset: 0x00075524
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.loadTime = Time.unscaledTime;
			if (this.campaignPrefs.difficulty == Difficulty.None)
			{
				this.campaignPrefs.difficulty = Difficulty.Normal;
			}
			if (this.completionCounts == null)
			{
				this.completionCounts = new Dictionary<Difficulty, int>();
			}
			this.stats.InitVikingTypesKilled();
			if (this.inventory == null)
			{
				this.inventory = new MetaInventory();
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00077190 File Offset: 0x00075590
		public void FixUpMaxDifficulty()
		{
			if (this.maxDifficulty == Difficulty.None)
			{
				int num;
				this.completionCounts.TryGetValue(Difficulty.Hard, out num);
				int num2;
				this.completionCounts.TryGetValue(Difficulty.VeryHard, out num2);
				this.maxDifficulty = ((num + num2 <= 0) ? Difficulty.Hard : Difficulty.VeryHard);
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000771DC File Offset: 0x000755DC
		void ISaveGameObject.PostCreate(string fileName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040017D9 RID: 6105
		public const string defaultFileName = "user";

		// Token: 0x040017DA RID: 6106
		private int savedPlayTime;

		// Token: 0x040017DB RID: 6107
		public int campaignCount;

		// Token: 0x040017DC RID: 6108
		public CampaignStats stats = new CampaignStats();

		// Token: 0x040017DD RID: 6109
		public CampaignPrefs campaignPrefs = new CampaignPrefs(Difficulty.Normal, false, false);

		// Token: 0x040017DE RID: 6110
		public Dictionary<Difficulty, int> completionCounts = new Dictionary<Difficulty, int>();

		// Token: 0x040017DF RID: 6111
		public Difficulty maxDifficulty = Difficulty.Hard;

		// Token: 0x040017E0 RID: 6112
		public MetaInventory inventory = new MetaInventory();

		// Token: 0x040017E1 RID: 6113
		[NonSerialized]
		public float loadTime;
	}
}
