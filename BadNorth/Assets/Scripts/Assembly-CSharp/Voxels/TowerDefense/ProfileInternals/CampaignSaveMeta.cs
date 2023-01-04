using System;
using System.Runtime.Serialization;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x02000588 RID: 1416
	[Serializable]
	public class CampaignSaveMeta : ISaveGameObject
	{
		// Token: 0x060024B3 RID: 9395 RVA: 0x00073908 File Offset: 0x00071D08
		private CampaignSaveMeta()
		{
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x00073950 File Offset: 0x00071D50
		public CampaignSaveMeta(CampaignSave campaignSave)
		{
			this.Refresh(campaignSave, false);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000739A0 File Offset: 0x00071DA0
		public void SetSaveSlot(int value)
		{
			this.saveSlot = value;
			this.targetFileName = CampaignSave.FileNameGen(this.saveSlot);
			this.metaFileName = this.targetFileName + ".meta";
			this.checkpointFileName = this.targetFileName + ".checkpoint";
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x000739F1 File Offset: 0x00071DF1
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x000739F9 File Offset: 0x00071DF9
		public int saveSlot { get; private set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x00073A02 File Offset: 0x00071E02
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x00073A0A File Offset: 0x00071E0A
		public string targetFileName { get; private set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00073A13 File Offset: 0x00071E13
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x00073A1B File Offset: 0x00071E1B
		public string metaFileName { get; private set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x00073A24 File Offset: 0x00071E24
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x00073A2C File Offset: 0x00071E2C
		public string checkpointFileName { get; private set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x00073A35 File Offset: 0x00071E35
		public bool gameOver
		{
			get
			{
				return this.gameOverReason != GameOverReason.None;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x00073A43 File Offset: 0x00071E43
		public bool loadToCheckpoint
		{
			get
			{
				return this.gameOver && this.hasCheckpoint;
			}
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00073A59 File Offset: 0x00071E59
		public string GetDisplayName()
		{
			return this.displayName;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060024C1 RID: 9409 RVA: 0x00073A61 File Offset: 0x00071E61
		string ISaveGameObject.fileName
		{
			get
			{
				return "meta";
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00073A68 File Offset: 0x00071E68
		public void Refresh(CampaignSave campaignSave, bool refreshCheckpoint)
		{
			this.serilizedVersion = campaignSave.serializedVersion;
			this.seed = campaignSave.seed;
			this.playTime = campaignSave.playTime;
			this.savedTime = campaignSave.saveTimeStamp;
			this.gameOverReason = campaignSave.gameOverReason;
			this.prefs = campaignSave.prefs;
			this.hasCheckpoint = campaignSave.hasCheckpoint;
			this.campaignFraction = campaignSave.FractionComplete();
			if (refreshCheckpoint)
			{
				this.checkpointPlayTime = this.playTime;
				this.checkpointFraction = this.campaignFraction;
			}
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00073AF4 File Offset: 0x00071EF4
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			if (this.prefs.difficulty == Difficulty.None)
			{
				this.prefs.difficulty = Difficulty.Hard;
			}
			if (this.metaVersion < 1)
			{
				this.campaignFraction = -1f;
				this.checkpointPlayTime = -1;
				this.checkpointFraction = -1f;
			}
			this.metaVersion = 1;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00073B4D File Offset: 0x00071F4D
		public static implicit operator CampaignSaveMeta(CampaignSave save)
		{
			return new CampaignSaveMeta(save);
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00073B55 File Offset: 0x00071F55
		public static implicit operator bool(CampaignSaveMeta meta)
		{
			return meta != null;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x00073B5E File Offset: 0x00071F5E
		public void PostCreate(string fileName)
		{
		}

		// Token: 0x0400173D RID: 5949
		private const int expectedVersion = 1;

		// Token: 0x04001742 RID: 5954
		public int metaVersion = 1;

		// Token: 0x04001743 RID: 5955
		public int serilizedVersion = -1;

		// Token: 0x04001744 RID: 5956
		public int seed;

		// Token: 0x04001745 RID: 5957
		public DateTime savedTime = DateTime.Now;

		// Token: 0x04001746 RID: 5958
		public int playTime;

		// Token: 0x04001747 RID: 5959
		public float campaignFraction;

		// Token: 0x04001748 RID: 5960
		public int checkpointPlayTime;

		// Token: 0x04001749 RID: 5961
		public float checkpointFraction;

		// Token: 0x0400174A RID: 5962
		public GameOverReason gameOverReason;

		// Token: 0x0400174B RID: 5963
		public CampaignPrefs prefs = default(CampaignPrefs);

		// Token: 0x0400174C RID: 5964
		public bool hasCheckpoint;

		// Token: 0x0400174D RID: 5965
		public int checkpointReloads;

		// Token: 0x0400174E RID: 5966
		private string displayName = string.Empty;

		// Token: 0x0400174F RID: 5967
		public int campaignNumber;
	}
}
