using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000710 RID: 1808
	public class LevelSpawner : CampaignComponent, Campaign.ICampaignGenerator
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B7AD8 File Offset: 0x000B5ED8
		public LevelNode CreateLevel(Campaign campaign, LevelState levelState, CampaignDifficultySettings difficultySettings)
		{
			LevelNode component = UnityEngine.Object.Instantiate<LevelNode>(this.levelPrefab, this.levelContainer).GetComponent<LevelNode>();
			component.Setup(levelState);
			component.diffiucltySettings = difficultySettings;
			campaign.levels.Add(component);
			return component;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000B7B18 File Offset: 0x000B5F18
		IEnumerator<GenInfo> Campaign.ICampaignGenerator.OnCampaignGeneration(Campaign campaign)
		{
			GenInfo info = new GenInfo("LevelSpawner", GenInfo.Mode.interruptable);
			CampaignDifficultySettings difficultySettings = campaign.GetDifficultySettings();
			foreach (LevelState levelState in campaign.campaignSave.levelStates)
			{
				LevelNode level = this.CreateLevel(campaign, levelState, difficultySettings);
				level.gameObject.SetActive(false);
				level.transform.localPosition = levelState.pos;
				this.MaybeAddToPropagation(level);
				LevelState levelState2 = level.levelState;
				levelState2.onUnlocked = (Action<LevelState>)Delegate.Combine(levelState2.onUnlocked, new Action<LevelState>(delegate(LevelState x)
				{
					this.PropagateUnlockedLevel(level);
				}));
				yield return info;
			}
			this.ResolveUnlockPropagation();
			foreach (LevelNode level2 in campaign.levels)
			{
				IEnumerator enumerator = level2.Setup2();
				while (enumerator.MoveNext())
				{
					yield return info;
				}
			}
			yield return info;
			yield break;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000B7B3A File Offset: 0x000B5F3A
		private void PropagateUnlockedLevel(LevelNode levelNode)
		{
			this.MaybeAddToPropagation(levelNode);
			this.ResolveUnlockPropagation();
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000B7B49 File Offset: 0x000B5F49
		private void MaybeAddToPropagation(LevelNode levelNode)
		{
			if (levelNode.levelState.unlocked)
			{
				levelNode.stepsFromUnlock = 0;
				LevelSpawner.propagationList.Add(levelNode);
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000B7B70 File Offset: 0x000B5F70
		private void ResolveUnlockPropagation()
		{
			while (LevelSpawner.propagationList.Count > 0)
			{
				LevelNode levelNode = LevelSpawner.propagationList[0];
				LevelSpawner.propagationList.RemoveAt(0);
				int num = levelNode.stepsFromUnlock + 1;
				foreach (LevelNode levelNode2 in levelNode.connectedLevels)
				{
					if (num < levelNode2.stepsFromUnlock)
					{
						levelNode2.stepsFromUnlock = num;
						LevelSpawner.propagationList.Add(levelNode2);
					}
				}
			}
		}

		// Token: 0x04001EEA RID: 7914
		[SerializeField]
		private LevelNode levelPrefab;

		// Token: 0x04001EEB RID: 7915
		[Header("Containers")]
		public Transform levelContainer;

		// Token: 0x04001EEC RID: 7916
		public Transform nameContainer;

		// Token: 0x04001EED RID: 7917
		public Transform houseContainer;

		// Token: 0x04001EEE RID: 7918
		public Transform itemContainer;

		// Token: 0x04001EEF RID: 7919
		public Transform bannerContainer;

		// Token: 0x04001EF0 RID: 7920
		public Transform enemyContainer;

		// Token: 0x04001EF1 RID: 7921
		public Transform mapContainer;

		// Token: 0x04001EF2 RID: 7922
		public Transform decorContainer;

		// Token: 0x04001EF3 RID: 7923
		private static List<LevelNode> propagationList = new List<LevelNode>();
	}
}
