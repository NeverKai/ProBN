using System;
using I2.Loc;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000835 RID: 2101
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public class HeroUpgradeDefinition : ScriptableObject
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000EAB39 File Offset: 0x000E8F39
		public int numLevels
		{
			get
			{
				return this.levels.Length;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000EAB43 File Offset: 0x000E8F43
		public string descriptionTerm
		{
			get
			{
				return this.levels[0].description;
			}
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000EAB56 File Offset: 0x000E8F56
		public string GetLevelDescription(int level)
		{
			return this.levels[level].description;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000EAB69 File Offset: 0x000E8F69
		public int GetLevelCost(int level)
		{
			return this.levels[level].cost;
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000EAB7C File Offset: 0x000E8F7C
		public string GetLevelDiplayNumber(int level)
		{
			return IntStringCache.GetClean(level + 1);
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000EAB86 File Offset: 0x000E8F86
		public HeroUpgradeTypeEnum typeEnum
		{
			get
			{
				return this.upgradeType.typeEnum;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000EAB93 File Offset: 0x000E8F93
		public bool canBeStartingItem
		{
			get
			{
				return this.upgradeType.canBeStartItem;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x000EABA0 File Offset: 0x000E8FA0
		public string dbgName
		{
			get
			{
				return ScriptLocalization.Get(this.nameTerm, true, 0, true, false, null, null);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000EABB3 File Offset: 0x000E8FB3
		public FabricEventReference uiPurchaseAudioId
		{
			get
			{
				return (!string.IsNullOrEmpty(this._uiPurchaseAudioId.name)) ? this._uiPurchaseAudioId : HeroUpgradeDefinition.defaultPurchaseAudioId;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000EABDA File Offset: 0x000E8FDA
		// (set) Token: 0x060036C5 RID: 14021 RVA: 0x000EABE2 File Offset: 0x000E8FE2
		public string uniqueId { get; private set; }

		// Token: 0x060036C6 RID: 14022 RVA: 0x000EABEB File Offset: 0x000E8FEB
		public void OnEnable()
		{
			this.uniqueId = base.name;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000EABFC File Offset: 0x000E8FFC
		public Sprite GetSprite()
		{
			if (this.infoSprite)
			{
				return this.infoSprite;
			}
			float h = ExtraMath.RemapValue((float)this.nameTerm.GetHashCode(), -2.1474836E+09f, 2.1474836E+09f, 0f, 1f);
			Color backgroundColor = Color.HSVToRGB(h, 0.7f, 1f);
			return TextOnTexture.GetSprite(128, 128, Vector2.one / 2f, this.nameTerm, backgroundColor, Color.black);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000EAC84 File Offset: 0x000E9084
		public bool AvailableFor(HeroDefinition hero)
		{
			if (hero.alive && (!this.prerequisite || hero.Has(this.prerequisite)))
			{
				foreach (SerializableHeroUpgrade serializableHeroUpgrade in hero.upgrades)
				{
					if (serializableHeroUpgrade.definition.upgradeType == this.upgradeType && serializableHeroUpgrade.definition != this)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000EAD40 File Offset: 0x000E9140
		public virtual bool AvailableFor(HeroDefinition hero, int atLevel)
		{
			return this.numLevels > atLevel && hero.GetUpgradeLevel(this) == atLevel - 1 && this.AvailableFor(hero);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000EAD67 File Offset: 0x000E9167
		public virtual void OnPurchased(HeroDefinition hero, int level)
		{
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000EAD69 File Offset: 0x000E9169
		public virtual void OnAttachedToHero(HeroDefinition hero, int level)
		{
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000EAD6B File Offset: 0x000E916B
		public virtual void OnAttachedToMonoHero(MonoHero monoHero, int level)
		{
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000EAD6D File Offset: 0x000E916D
		public virtual void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			if (this.AbilityPrefab)
			{
				squad.upgradeManager.AddUpgrade(this.AbilityPrefab, upgradeLevel);
			}
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000EAD91 File Offset: 0x000E9191
		public override string ToString()
		{
			return this.dbgName;
		}

		// Token: 0x0400251E RID: 9502
		public HeroUpgradeType upgradeType;

		// Token: 0x0400251F RID: 9503
		[SerializeField]
		[SpritePreview]
		public Sprite infoSprite;

		// Token: 0x04002520 RID: 9504
		public HeroUpgradeDefinition.UnlockValue unlockValue;

		// Token: 0x04002521 RID: 9505
		private static FabricEventReference defaultPurchaseAudioId = "UI/Upgrade/Ability";

		// Token: 0x04002522 RID: 9506
		[TermsPopup("")]
		public string nameTerm = string.Empty;

		// Token: 0x04002523 RID: 9507
		[TermsPopup("")]
		public string shortDescription = string.Empty;

		// Token: 0x04002524 RID: 9508
		public bool affectsPortrait;

		// Token: 0x04002525 RID: 9509
		public HeroUpgradeDefinition.Level[] levels = new HeroUpgradeDefinition.Level[1];

		// Token: 0x04002526 RID: 9510
		[Header("Audio")]
		[SerializeField]
		private FabricEventReference _uiPurchaseAudioId = string.Empty;

		// Token: 0x04002527 RID: 9511
		[Header("Conditions")]
		public HeroUpgradeDefinition prerequisite;

		// Token: 0x04002528 RID: 9512
		[Header("Upgrade Ability")]
		public GameObject AbilityPrefab;

		// Token: 0x02000836 RID: 2102
		public enum UnlockValue
		{
			// Token: 0x0400252B RID: 9515
			Normal,
			// Token: 0x0400252C RID: 9516
			High,
			// Token: 0x0400252D RID: 9517
			Negative
		}

		// Token: 0x02000837 RID: 2103
		[Serializable]
		public struct Level
		{
			// Token: 0x0400252E RID: 9518
			public int cost;

			// Token: 0x0400252F RID: 9519
			[TermsPopup("")]
			public string description;

			// Token: 0x04002530 RID: 9520
			[TermsPopup("")]
			public string prepurchaseDescription;
		}
	}
}
