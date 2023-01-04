using System;
using I2.Loc;
using UnityEngine;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000929 RID: 2345
	public class UpgradeInfo : SelectableInfo
	{
		// Token: 0x06003F02 RID: 16130 RVA: 0x0011C764 File Offset: 0x0011AB64
		public UpgradeInfo Setup(HeroUpgradeDefinition def, int level, bool prepuchase)
		{
			this.header.Term = def.nameTerm;
			this.type.Term = def.upgradeType.nameTerm;
			this.body.Term = ((!prepuchase) ? def.levels[level].description : def.levels[level].prepurchaseDescription);
			string[] levelTerms = this.GetLevelTerms(def.typeEnum);
			string text = (!levelTerms.IsValidIndex(level)) ? string.Empty : levelTerms[level];
			this.levelText.Term = text;
			this.levelText.gameObject.SetActive(!string.IsNullOrEmpty(text));
			return this;
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0011C820 File Offset: 0x0011AC20
		private string[] GetLevelTerms(HeroUpgradeTypeEnum type)
		{
			switch (type)
			{
			case HeroUpgradeTypeEnum.Item:
				return this.itemLevels;
			case HeroUpgradeTypeEnum.Class:
				return this.classLevels;
			case HeroUpgradeTypeEnum.Skill:
				return this.skillLevels;
			case HeroUpgradeTypeEnum.Consumable:
				return this.ConsumableLevels;
			default:
				throw new NotImplementedException(string.Format("unknown type {0}", type));
			}
		}

		// Token: 0x04002C14 RID: 11284
		[SerializeField]
		private Localize header;

		// Token: 0x04002C15 RID: 11285
		[SerializeField]
		private Localize type;

		// Token: 0x04002C16 RID: 11286
		[SerializeField]
		private Localize body;

		// Token: 0x04002C17 RID: 11287
		[SerializeField]
		private Localize levelText;

		// Token: 0x04002C18 RID: 11288
		[TermsPopup("")]
		[SerializeField]
		private string[] classLevels;

		// Token: 0x04002C19 RID: 11289
		[TermsPopup("")]
		[SerializeField]
		private string[] itemLevels;

		// Token: 0x04002C1A RID: 11290
		[TermsPopup("")]
		[SerializeField]
		private string[] skillLevels;

		// Token: 0x04002C1B RID: 11291
		[TermsPopup("")]
		[SerializeField]
		private string[] ConsumableLevels;
	}
}
