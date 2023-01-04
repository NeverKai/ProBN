using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006D2 RID: 1746
	[Serializable]
	public class CampaignDifficultySettings : ScriptableObject
	{
		// Token: 0x06002D3F RID: 11583 RVA: 0x000A9909 File Offset: 0x000A7D09
		private void OnEnable()
		{
			this.dbgName = base.name;
			this.RebuildDictionary();
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000A991D File Offset: 0x000A7D1D
		private void OnValidate()
		{
			this.RebuildDictionary();
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000A9928 File Offset: 0x000A7D28
		private void RebuildDictionary()
		{
			this.vikingHealthMultipliers.Clear();
			foreach (CampaignDifficultySettings.VikingHealthModMap vikingHealthModMap in this._vikingHealthMultipliers)
			{
				this.vikingHealthMultipliers.Add(vikingHealthModMap.type, vikingHealthModMap.healthModifier);
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000A99A4 File Offset: 0x000A7DA4
		public override string ToString()
		{
			return this.dbgName;
		}

		// Token: 0x04001DC6 RID: 7622
		[NonSerialized]
		public string dbgName = string.Empty;

		// Token: 0x04001DC7 RID: 7623
		[Space]
		[Header("LevelGraph")]
		[NonSerialized]
		public int levelCount = 60;

		// Token: 0x04001DC8 RID: 7624
		public int frontierTarget = 40;

		// Token: 0x04001DC9 RID: 7625
		[NonSerialized]
		public int totalGold = 400;

		// Token: 0x04001DCA RID: 7626
		[Space]
		[Header("Checkpoints")]
		public int checkpointCount = 2;

		// Token: 0x04001DCB RID: 7627
		public Vector2 checkpointRange = new Vector2(0.4f, 0.6f);

		// Token: 0x04001DCC RID: 7628
		[Space]
		[Header("Raid")]
		public float shipSpeedMultiplier = 1f;

		// Token: 0x04001DCD RID: 7629
		public CampaignCurve waveSpacing = new CampaignCurve(20f, 22f, 20f, 22f);

		// Token: 0x04001DCE RID: 7630
		public CampaignCurve bounty = new CampaignCurve(20f, 22f, 120f, 160f);

		// Token: 0x04001DCF RID: 7631
		public CampaignCurve waves = new CampaignCurve(3f, 5f, 7f, 10f);

		// Token: 0x04001DD0 RID: 7632
		public CampaignCurve enemyTypes = new CampaignCurve(1f, 2f, 3f, 5f);

		// Token: 0x04001DD1 RID: 7633
		[Header("Level Difficulty Modifiers")]
		[NonSerialized]
		public float itemDifficultyModifier = 0.3f;

		// Token: 0x04001DD2 RID: 7634
		[NonSerialized]
		public float heroDifficultyModifier = 0.2f;

		// Token: 0x04001DD3 RID: 7635
		[NonSerialized]
		public float eldoradoDifficultyModifier = 2f;

		// Token: 0x04001DD4 RID: 7636
		[NonSerialized]
		public float checkpointDifficultyModifier = 1f;

		// Token: 0x04001DD5 RID: 7637
		[NonSerialized]
		public float offshootDifficultyModifier = 2f;

		// Token: 0x04001DD6 RID: 7638
		[Header("Ability Modifiers")]
		public float replenishTimeMultiplier = 1f;

		// Token: 0x04001DD7 RID: 7639
		[Header("Arsonist")]
		public float torchDamage = 0.3f;

		// Token: 0x04001DD8 RID: 7640
		public AnimationCurve timerPerArsonist = new AnimationCurve();

		// Token: 0x04001DD9 RID: 7641
		[Header("English Agents")]
		public float englishHealthModifier = 1f;

		// Token: 0x04001DDA RID: 7642
		[Header("Viking Agents")]
		[SerializeField]
		private List<CampaignDifficultySettings.VikingHealthModMap> _vikingHealthMultipliers = new List<CampaignDifficultySettings.VikingHealthModMap>();

		// Token: 0x04001DDB RID: 7643
		public Dictionary<VikingAgent.Type, float> vikingHealthMultipliers = new Dictionary<VikingAgent.Type, float>();

		// Token: 0x020006D3 RID: 1747
		[Serializable]
		public struct VikingHealthModMap
		{
			// Token: 0x04001DDC RID: 7644
			public VikingAgent.Type type;

			// Token: 0x04001DDD RID: 7645
			public float healthModifier;
		}
	}
}
