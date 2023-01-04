using System;
using I2.Loc;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008F1 RID: 2289
	[AddComponentMenu("Meta Inventory - Upgrade Level Info")]
	internal class UpgradeLevelInfo : InfoPanelEntry<UpgradeLevelInfo>
	{
		// Token: 0x06003CB0 RID: 15536 RVA: 0x0010ECFC File Offset: 0x0010D0FC
		public void Setup(HeroUpgradeDefinition upgradeDef, int level, bool isKnown, float appearDelay)
		{
			if (!this.descriptionText)
			{
				this.descriptionText = this.desciptionLocalize.GetComponent<Text>();
			}
			int levelCost = upgradeDef.GetLevelCost(level);
			bool flag = levelCost > 0;
			bool active = upgradeDef.numLevels > 1;
			if (flag)
			{
				this.SetAsCoin((!isKnown) ? "?" : IntStringCache.GetClean(levelCost));
			}
			else
			{
				this.icon.Set(upgradeDef, level);
				if (!isKnown)
				{
					this.icon.sprite = upgradeDef.upgradeType.unknownIcon;
				}
			}
			this.icon.shaderSettings.outlineWidth = ((!flag) ? 1.5f : 1f);
			this.goldText.gameObject.SetActive(flag);
			this.icon.gameObject.SetActive(active);
			this.icon.brightness = ((!isKnown) ? 0.8f : 1f);
			this.icon.saturation = ((!isKnown) ? 0.4f : 1f);
			HeroUpgradeDefinition.Level level2 = upgradeDef.levels[level];
			if (isKnown)
			{
				this.desciptionLocalize.Term = ((!string.IsNullOrEmpty(level2.prepurchaseDescription)) ? level2.prepurchaseDescription : level2.description);
			}
			else if (level == 0)
			{
				this.desciptionLocalize.Term = upgradeDef.upgradeType.unknownDescriptionTerm;
			}
			else
			{
				this.desciptionLocalize.Term = "META_INVENTORY/UNKNOWN/UPGRADE_TEXT";
			}
			float value = (!isKnown) ? 0.8f : 1f;
			this.descriptionText.color = this.descriptionText.color.SetA(value);
			this.goldText.color = this.goldText.color.SetA(value);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x0010EEE6 File Offset: 0x0010D2E6
		private void SetAsCoin(string text)
		{
			this.goldText.text = text;
			this.icon.sprite = this.coinSprite;
			this.icon.mask = this.coinMask;
		}

		// Token: 0x04002A5A RID: 10842
		[Space]
		[Header("UpgradeLevelInfo")]
		[SerializeField]
		private MaskedSprite icon;

		// Token: 0x04002A5B RID: 10843
		[SerializeField]
		private Text goldText;

		// Token: 0x04002A5C RID: 10844
		[SerializeField]
		private Localize desciptionLocalize;

		// Token: 0x04002A5D RID: 10845
		private Text descriptionText;

		// Token: 0x04002A5E RID: 10846
		[SerializeField]
		private Sprite coinSprite;

		// Token: 0x04002A5F RID: 10847
		[SerializeField]
		private Sprite coinMask;
	}
}
