using System;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000927 RID: 2343
	public class HeroInfo : SelectableInfo
	{
		// Token: 0x06003EFA RID: 16122 RVA: 0x0011C198 File Offset: 0x0011A598
		protected override void OnInitialize()
		{
			foreach (VikingReference vikingReference in LevelStateObjectReferences.GetReferencedObjects<VikingReference>())
			{
				AgentInfo agentInfo = UnityEngine.Object.Instantiate<AgentInfo>(this.enemyInfo, this.enemyInfo.transform.parent);
				agentInfo.transform.SetSiblingIndex(this.enemyInfo.transform.GetSiblingIndex());
				agentInfo.image.sprite = vikingReference.sprite2;
				this.enemyInfo.image.transform.localScale = Vector3.one * Mathf.Lerp(vikingReference.agent.scale, 1f, 0.5f);
				agentInfo.vikingReference = vikingReference;
				this.enemies.Add(agentInfo);
			}
			this.enemyInfo.gameObject.SetActive(false);
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x0011C290 File Offset: 0x0011A690
		public HeroInfo Setup(HeroDefinition heroDef)
		{
			base.MaybeInitialize();
			foreach (Localize localize in this.nameTexts)
			{
				localize.Term = heroDef.nameTerm;
			}
			foreach (BannerPolygon bannerPolygon in this.bannerGraphics)
			{
				bannerPolygon.Setup(heroDef, false);
			}
			LevelNode levelNode = Singleton<CampaignManager>.instance.campaign.levels[0];
			foreach (LevelNode levelNode2 in Singleton<CampaignManager>.instance.campaign.levels)
			{
				if (levelNode2.levelState.heroId == heroDef.id)
				{
					levelNode = levelNode2;
					break;
				}
			}
			this.homeIslandImage.sprite = levelNode.levelVisuals.sprite;
			this.homeIslandName.Term = levelNode.levelState.nameTerm;
			if (heroDef.deathLevelId != -1)
			{
				LevelNode levelNode3 = Singleton<CampaignManager>.instance.campaign.levels[heroDef.deathLevelId];
				this.deathIslandImage.sprite = levelNode3.levelVisuals.sprite;
				this.deatIslandName.Term = levelNode3.levelState.nameTerm;
				this.deathIslandImage.transform.parent.gameObject.SetActive(true);
			}
			else
			{
				this.deathIslandImage.transform.parent.gameObject.SetActive(false);
			}
			if (heroDef.stats.valid)
			{
				this.statsContainer.gameObject.SetActive(true);
				int soldiersLost = heroDef.stats.soldiersLost;
				bool flag = soldiersLost > 0;
				if (flag)
				{
					this.lossesInfo.gameObject.SetActive(true);
					this.lossesInfo.image.sprite = heroDef.graphics.agentSpriteMinion;
					this.lossesInfo.image.transform.localScale = Vector3.one * Mathf.Lerp(heroDef.monoHero.minionPrefab.scale, 1f, 0.5f);
					this.lossesInfo.text.text = soldiersLost.ToString();
				}
				else
				{
					this.lossesInfo.gameObject.SetActive(false);
				}
				bool flag2 = false;
				foreach (AgentInfo agentInfo in this.enemies)
				{
					int vikingsKilled = heroDef.stats.GetVikingsKilled(agentInfo.vikingReference.type);
					if (vikingsKilled > 0)
					{
						agentInfo.gameObject.SetActive(true);
						agentInfo.text.text = vikingsKilled.ToString();
						flag2 = true;
					}
					else
					{
						agentInfo.gameObject.SetActive(false);
					}
				}
				this.statsSeparator.gameObject.SetActive(flag2 && flag);
			}
			else
			{
				this.statsContainer.gameObject.SetActive(false);
			}
			HeroUpgradeDefinition heroUpgradeDefinition = heroDef.traitUpgrade;
			if (heroUpgradeDefinition)
			{
				this.traitName.Term = heroUpgradeDefinition.nameTerm;
				this.traitDescription.Term = ((!string.IsNullOrEmpty(heroUpgradeDefinition.shortDescription)) ? heroUpgradeDefinition.shortDescription : heroUpgradeDefinition.descriptionTerm);
			}
			this.traitName.gameObject.SetActive(heroUpgradeDefinition);
			this.traitDescription.gameObject.SetActive(heroUpgradeDefinition);
			this.deadStatus.gameObject.SetActive(!heroDef.alive);
			this.fatiguedStatus.gameObject.SetActive(heroDef.fatigued);
			this.energizedStatus.gameObject.SetActive(heroDef.alive && heroDef.maxUsesPerTurn > 1 && heroDef.timesUsedThisTurn < heroDef.maxUsesPerTurn);
			if (this.energizedStatus.gameObject.activeSelf)
			{
				for (int k = 0; k < this.energizedIcons.Length; k++)
				{
					this.energizedIcons[k].SetActive(heroDef.maxUsesPerTurn > 1 && k < (int)heroDef.maxUsesPerTurn && (int)heroDef.timesUsedThisTurn <= k);
				}
			}
			base.transform.ForceChildLayoutUpdates(false);
			return this;
		}

		// Token: 0x04002C01 RID: 11265
		[SerializeField]
		private Localize[] nameTexts;

		// Token: 0x04002C02 RID: 11266
		[SerializeField]
		private Localize traitName;

		// Token: 0x04002C03 RID: 11267
		[SerializeField]
		private Localize traitDescription;

		// Token: 0x04002C04 RID: 11268
		[SerializeField]
		private Localize homeIslandName;

		// Token: 0x04002C05 RID: 11269
		[SerializeField]
		private Localize deatIslandName;

		// Token: 0x04002C06 RID: 11270
		[SerializeField]
		private Image homeIslandImage;

		// Token: 0x04002C07 RID: 11271
		[SerializeField]
		private Image deathIslandImage;

		// Token: 0x04002C08 RID: 11272
		[SerializeField]
		private BannerPolygon[] bannerGraphics;

		// Token: 0x04002C09 RID: 11273
		[SerializeField]
		private GameObject deadStatus;

		// Token: 0x04002C0A RID: 11274
		[SerializeField]
		private GameObject fatiguedStatus;

		// Token: 0x04002C0B RID: 11275
		[SerializeField]
		private GameObject energizedStatus;

		// Token: 0x04002C0C RID: 11276
		[SerializeField]
		private GameObject[] energizedIcons;

		// Token: 0x04002C0D RID: 11277
		[SerializeField]
		private AgentInfo lossesInfo;

		// Token: 0x04002C0E RID: 11278
		[SerializeField]
		private AgentInfo enemyInfo;

		// Token: 0x04002C0F RID: 11279
		[SerializeField]
		private GameObject statsSeparator;

		// Token: 0x04002C10 RID: 11280
		[SerializeField]
		private GameObject statsContainer;

		// Token: 0x04002C11 RID: 11281
		private List<AgentInfo> enemies = new List<AgentInfo>(8);

		// Token: 0x04002C12 RID: 11282
		private Dictionary<VikingReference, AgentInfo> enemyDict = new Dictionary<VikingReference, AgentInfo>(8);
	}
}
