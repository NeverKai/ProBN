using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using ControlledRandomness;
using CS.Platform;
using I2.Loc;
using ReflexCLI.Attributes;
using UnityEngine;
using Voxels.TowerDefense.HeroGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.Reflex;
using Voxels.TowerDefense.UI.UpgradeScreen;

namespace Voxels.TowerDefense
{
	// Token: 0x020006EC RID: 1772
	[DebuggerDisplay("{dbgName} ({id}) [HeroDefinition]")]
	[ObjectDumper.LeafAttribute]
	[Serializable]
	public class HeroDefinition
	{
		// Token: 0x06002DAF RID: 11695 RVA: 0x000B1D00 File Offset: 0x000B0100
		public HeroDefinition(int id)
		{
			this.id = id;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B1D75 File Offset: 0x000B0175
		public string dbgName
		{
			get
			{
				return ScriptLocalization.Get(this.nameTerm, true, 0, true, false, null, null);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000B1D88 File Offset: 0x000B0188
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000B1D95 File Offset: 0x000B0195
		public UnityEngine.Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color.uColor = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000B1DA3 File Offset: 0x000B01A3
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000B1DAC File Offset: 0x000B01AC
		public bool alive
		{
			get
			{
				return this._alive;
			}
			set
			{
				if (!value && this.alive && Profile.userSave != null)
				{
					Profile.userSave.stats.heroesDied++;
					Profile.campaign.stats.heroesDied++;
				}
				this._alive = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x000B1E09 File Offset: 0x000B0209
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x000B1E11 File Offset: 0x000B0211
		[Obsolete]
		public int coins
		{
			get
			{
				return this._coins;
			}
			set
			{
				this._coins = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000B1E1A File Offset: 0x000B021A
		public bool fatigued
		{
			get
			{
				return this.timesUsedThisTurn >= this.maxUsesPerTurn && this.alive;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000B1E36 File Offset: 0x000B0236
		public bool hasCornucopiaDeployAvailable
		{
			get
			{
				return this.maxUsesPerTurn - this.timesUsedThisTurn > 1 && this.alive;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000B1E54 File Offset: 0x000B0254
		public IHeroStats stats
		{
			get
			{
				return (!this.statistics) ? HeroStatsBlank.inst : this.statistics;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x000B1E76 File Offset: 0x000B0276
		public IHeroGraphics graphics
		{
			get
			{
				return this.monoHero;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000B1E7E File Offset: 0x000B027E
		public bool recruitable
		{
			get
			{
				return !this.recruited && this.alive;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000B1E94 File Offset: 0x000B0294
		public bool available
		{
			get
			{
				return this.recruited && this.alive;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000B1EAA File Offset: 0x000B02AA
		public bool availableThisTurn
		{
			get
			{
				return this.available && !this.fatigued;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000B1EC3 File Offset: 0x000B02C3
		public string loadoutPickupSound
		{
			get
			{
				return "Sfx/Characters/AldricPickup";
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x000B1ECA File Offset: 0x000B02CA
		public string loadoutPlaceSound
		{
			get
			{
				return "Sfx/Characters/AldricSelect";
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B1ED1 File Offset: 0x000B02D1
		public static implicit operator bool(HeroDefinition hero)
		{
			return hero != null;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B1EDA File Offset: 0x000B02DA
		[ConsoleCommand("")]
		public void LogPropertyBank()
		{
			UnityEngine.Debug.Log(this.dbgName + "\n" + this.propertyBank.GetPrint());
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B1EFC File Offset: 0x000B02FC
		public SerializableHeroUpgrade GetUpgrade(HeroUpgradeTypeEnum typeEnum)
		{
			switch (typeEnum)
			{
			case HeroUpgradeTypeEnum.Item:
				return this.itemUpgrade;
			case HeroUpgradeTypeEnum.Class:
				return this.classUpgrade;
			case HeroUpgradeTypeEnum.Skill:
				return this.skillUpgrade;
			case HeroUpgradeTypeEnum.Trait:
				return this.traitUpgrade;
			}
			return null;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000B1F3A File Offset: 0x000B033A
		public int squadLevel
		{
			get
			{
				if (this.classUpgrade != null)
				{
					return this.classUpgrade.level + 1;
				}
				return 0;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B1F58 File Offset: 0x000B0358
		[DebugSetting("Recruit One", "雇佣一个", DebugSettingLocation.Campaign)]
		private static void RecruitOne()
		{
			List<HeroDefinition> heroes = Profile.campaign.heroes;
			int i = 0;
			int count = heroes.Count;
			while (i < count)
			{
				HeroDefinition heroDefinition = heroes[i];
				if (!heroDefinition.recruited)
				{
					heroDefinition.recruited = true;
					return;
				}
				i++;
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B1FA4 File Offset: 0x000B03A4
		[ConsoleCommand("")]
		[DebugSetting("Recruit All", "雇佣全部", DebugSettingLocation.Campaign)]
		private static void RecruitAll()
		{
			foreach (HeroDefinition heroDefinition in Profile.campaign.heroes)
			{
				heroDefinition.recruited = true;
			}
			SuperUpgradeMenu.instance.RefreshPortraits();
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B2010 File Offset: 0x000B0410
		public void SetupHeroState(bool recruited, HeroUpgradeDefinition traitDef, HeroUpgradeDefinition classDef, HeroUpgradeDefinition itemDef)
		{
			this.SetupHeroState(recruited, traitDef, new HeroUpgradeDefintionWithLevel(classDef, 0), itemDef);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B2024 File Offset: 0x000B0424
		public void SetupHeroState(bool recruited, HeroUpgradeDefinition traitDef, HeroUpgradeDefintionWithLevel classDef, HeroUpgradeDefinition itemDef)
		{
			this.recruited = recruited;
			this.RemoveAllUpgrades();
			if (classDef.def)
			{
				this.AddUpgrade(ref this.classUpgrade, classDef.def, classDef.level);
			}
			if (traitDef)
			{
				this.AddUpgrade(ref this.traitUpgrade, traitDef, 0);
			}
			if (itemDef)
			{
				this.AddUpgrade(ref this.itemUpgrade, itemDef, 0);
			}
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B20A0 File Offset: 0x000B04A0
		public bool Has(HeroUpgradeDefinition upgradeDef)
		{
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in this.upgrades)
			{
				if (serializableHeroUpgrade.definition == upgradeDef)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B2110 File Offset: 0x000B0510
		private void RefreshUpgradesList()
		{
			this.upgrades.Clear();
			this.upgrades.Add(this.classUpgrade);
			this.upgrades.Add(this.skillUpgrade);
			this.upgrades.Add(this.itemUpgrade);
			this.upgrades.Add(this.traitUpgrade);
			this.upgrades.RemoveAll((SerializableHeroUpgrade x) => x == null);
			if (this.classUpgrade != null && !this.classUpgrade.hasNextLevel && this.skillUpgrade != null && !this.skillUpgrade.hasNextLevel && this.itemUpgrade != null && !this.itemUpgrade.hasNextLevel)
			{
				BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_READY_FOR_ANYTHING");
			}
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000B21F8 File Offset: 0x000B05F8
		public int GetUpgradeLevel(HeroUpgradeDefinition upgradeDef)
		{
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in this.upgrades)
			{
				if (serializableHeroUpgrade.definition == upgradeDef)
				{
					return serializableHeroUpgrade.level;
				}
			}
			return -1;
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000B2270 File Offset: 0x000B0670
		public bool TryPurchase(HeroUpgradeDefinition upgradeDef, int level, ref int coinBank)
		{
			HeroUpgradeTypeEnum typeEnum = upgradeDef.typeEnum;
			switch (typeEnum)
			{
			case HeroUpgradeTypeEnum.Item:
				return this.TryPurchase(ref this.itemUpgrade, upgradeDef, level, ref coinBank);
			case HeroUpgradeTypeEnum.Class:
				return this.TryPurchase(ref this.classUpgrade, upgradeDef, level, ref coinBank);
			case HeroUpgradeTypeEnum.Skill:
				return this.TryPurchase(ref this.skillUpgrade, upgradeDef, level, ref coinBank);
			case HeroUpgradeTypeEnum.Consumable:
				return this.TryPurchaseConsumable(upgradeDef, level, ref coinBank);
			default:
				throw new NotImplementedException(string.Format("unknown type : {0}", typeEnum));
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000B22F0 File Offset: 0x000B06F0
		private bool TryPurchase(ref SerializableHeroUpgrade slot, HeroUpgradeDefinition upgradeDef, int level, ref int coinBank)
		{
			int num = (slot != null) ? slot.level : -1;
			int numLevels = upgradeDef.numLevels;
			int upgradeCost = this.GetUpgradeCost(upgradeDef, level);
			if (coinBank >= upgradeCost)
			{
				this.AddUpgrade(ref slot, upgradeDef, level);
				coinBank -= upgradeCost;
				return true;
			}
			return false;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000B2340 File Offset: 0x000B0740
		public int GetUpgradeCost(HeroUpgradeDefinition def, int level)
		{
			bool flag;
			return this.GetUpgradeCost(def, level, out flag);
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000B2358 File Offset: 0x000B0758
		public int GetUpgradeCost(HeroUpgradeDefinition def, int level, out bool discounted)
		{
			int levelCost = def.GetLevelCost(level);
			discounted = (this.discountType == def.typeEnum);
			float num = (!discounted) ? 0f : this.discount;
			return Mathf.Max(0, Mathf.CeilToInt((float)levelCost * (1f - num)));
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000B23AC File Offset: 0x000B07AC
		private bool TryPurchaseConsumable(HeroUpgradeDefinition upgradeDef, int level, ref int coinBank)
		{
			int numLevels = upgradeDef.numLevels;
			int upgradeCost = this.GetUpgradeCost(upgradeDef, level);
			if (coinBank >= upgradeCost)
			{
				this.AddUpgrade(upgradeDef, level);
				coinBank -= upgradeCost;
				return true;
			}
			return false;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000B23E2 File Offset: 0x000B07E2
		private void AddUpgrade(ref SerializableHeroUpgrade slot, HeroUpgradeDefinition upgradeDef, int level)
		{
			if (slot == null)
			{
				slot = upgradeDef;
			}
			slot.level = level;
			this.AddUpgrade(upgradeDef, level);
			this.RefreshUpgradesList();
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000B2409 File Offset: 0x000B0809
		private void AddUpgrade(HeroUpgradeDefinition upgradeDef, int level)
		{
			upgradeDef.OnPurchased(this, level);
			upgradeDef.OnAttachedToHero(this, level);
			if (this.monoHero)
			{
				upgradeDef.OnAttachedToMonoHero(this.monoHero, level);
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000B2438 File Offset: 0x000B0838
		private void RemoveAllUpgrades()
		{
			this.classUpgrade = (this.skillUpgrade = (this.itemUpgrade = null));
			this.RefreshUpgradesList();
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B2464 File Offset: 0x000B0864
		public override string ToString()
		{
			return string.Format("[{1}] {0} : {2}{3}{4}[{5} coins] ({6})", new object[]
			{
				this.dbgName,
				this.id,
				(this.classUpgrade == null) ? "Militia, " : (this.classUpgrade + ", "),
				(this.traitUpgrade == null) ? string.Empty : (this.traitUpgrade.definition.dbgName + ", "),
				(this.itemUpgrade == null) ? string.Empty : (this.itemUpgrade + ", "),
				this._coins,
				(!this.alive) ? "dead" : ((!this.recruited) ? "available" : "recruited")
			});
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000B255A File Offset: 0x000B095A
		[OnSerializing]
		private void PreSave(StreamingContext context)
		{
			this.voiceName = this.voice.name;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000B2570 File Offset: 0x000B0970
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.upgrades = new List<SerializableHeroUpgrade>(3);
			this.RefreshUpgradesList();
			foreach (SerializableHeroUpgrade serializableHeroUpgrade in this.upgrades)
			{
				serializableHeroUpgrade.definition.OnAttachedToHero(this, serializableHeroUpgrade.level);
			}
			this.voice = ResourceList<HeroVoice>.Get(this.voiceName);
			if (!this.voice)
			{
				this.voice = Singleton<CampaignManager>.instance.campaign.heroGenerator.maleVoices[0];
			}
			if (this.maxUsesPerTurn == 0)
			{
				this.maxUsesPerTurn = 1;
				this.timesUsedThisTurn = ((!this.usedThisTurn) ? 0 : 1);
				this.usedThisTurn = false;
			}
			if (this.timesUsedThisTurn > this.maxUsesPerTurn)
			{
				this.timesUsedThisTurn = this.maxUsesPerTurn;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x000B2678 File Offset: 0x000B0A78
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x000B2680 File Offset: 0x000B0A80
		[ConsoleCommand("alive")]
		private bool consoleAlive
		{
			get
			{
				return this.alive;
			}
			set
			{
				this.alive = value;
				if (!value && this.deathLevelId == -1)
				{
					List<LevelState> levelStates = Profile.campaign.levelStates;
					for (int i = levelStates.Count - 1; i >= 0; i--)
					{
						if (levelStates[i].playCount > 0 && levelStates[i].hasSprite)
						{
							this.deathLevelId = i;
							break;
						}
					}
				}
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x000B26F9 File Offset: 0x000B0AF9
		[ConsoleCommand("")]
		private int vikingsKilled
		{
			get
			{
				return this.stats.GetVikingsKilled();
			}
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B2706 File Offset: 0x000B0B06
		[ConsoleCommand("")]
		private int GetVikingsKilled(VikingAgent.Type type)
		{
			return this.stats.GetVikingsKilled(type);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000B2714 File Offset: 0x000B0B14
		[ConsoleCommand("")]
		private void RegisterVikingKilled(VikingAgent.Type type, int count = 1)
		{
			this.stats.KilledViking(type, count);
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x000B2723 File Offset: 0x000B0B23
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x000B2730 File Offset: 0x000B0B30
		[ConsoleCommand("")]
		private int bountiesCollected
		{
			get
			{
				return this.stats.bountiesCollected;
			}
			set
			{
				this.stats.bountiesCollected = value;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000B273E File Offset: 0x000B0B3E
		// (set) Token: 0x06002DDE RID: 11742 RVA: 0x000B274B File Offset: 0x000B0B4B
		[ConsoleCommand("")]
		private int soldiersLost
		{
			get
			{
				return this.stats.soldiersLost;
			}
			set
			{
				this.stats.soldiersLost = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000B2759 File Offset: 0x000B0B59
		// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x000B2766 File Offset: 0x000B0B66
		[ConsoleCommand("")]
		private int islandsVisited
		{
			get
			{
				return this.stats.islandsVisited;
			}
			set
			{
				this.stats.islandsVisited = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000B2774 File Offset: 0x000B0B74
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x000B2781 File Offset: 0x000B0B81
		[ConsoleCommand("")]
		private int timesFled
		{
			get
			{
				return this.stats.timesFled;
			}
			set
			{
				this.stats.timesFled = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000B278F File Offset: 0x000B0B8F
		// (set) Token: 0x06002DE4 RID: 11748 RVA: 0x000B279C File Offset: 0x000B0B9C
		[ConsoleCommand("")]
		private int islandsWon
		{
			get
			{
				return this.stats.islandsWon;
			}
			set
			{
				this.stats.islandsWon = value;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x000B27AA File Offset: 0x000B0BAA
		// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x000B27B7 File Offset: 0x000B0BB7
		[ConsoleCommand("")]
		private int collectedCoins
		{
			get
			{
				return this.stats.collectedCoins;
			}
			set
			{
				this.stats.collectedCoins = value;
			}
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000B27C8 File Offset: 0x000B0BC8
		[ConsoleCommand("")]
		private void RandomVikingKills()
		{
			int numTypes = VikingAgent.numTypes;
			int i = 0;
			int numTypes2 = VikingAgent.numTypes;
			while (i < numTypes2)
			{
				this.RegisterVikingKilled((VikingAgent.Type)i, (UnityEngine.Random.value <= 0.3f) ? 0 : UnityEngine.Random.Range(1, 226));
				i++;
			}
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000B281C File Offset: 0x000B0C1C
		[ConsoleCommand("")]
		private static void AllRandomKillsAndLosses()
		{
			foreach (HeroDefinition heroDefinition in Profile.campaign.heroes)
			{
				heroDefinition.RandomVikingKills();
				heroDefinition.soldiersLost += ((UnityEngine.Random.value >= 0.3f) ? UnityEngine.Random.Range(11, 226) : UnityEngine.Random.Range(1, 10));
			}
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000B28B0 File Offset: 0x000B0CB0
		[ConsoleCommand("")]
		public void SetTrait([UpgradeType(HeroUpgradeTypeEnum.Trait)] HeroUpgradeDefinition traitDef)
		{
			this.AddUpgrade(ref this.traitUpgrade, traitDef, 0);
		}

		// Token: 0x04001E55 RID: 7765
		public int id = -1;

		// Token: 0x04001E56 RID: 7766
		public string nameTerm;

		// Token: 0x04001E57 RID: 7767
		public float hue;

		// Token: 0x04001E58 RID: 7768
		[ObjectDumper.HideValuesAttribute]
		public PropertyBank propertyBank = new PropertyBank();

		// Token: 0x04001E59 RID: 7769
		private Voxels.TowerDefense.ProfileInternals.Color _color = new Voxels.TowerDefense.ProfileInternals.Color();

		// Token: 0x04001E5A RID: 7770
		private string voiceName = string.Empty;

		// Token: 0x04001E5B RID: 7771
		[NonSerialized]
		public HeroVoice voice;

		// Token: 0x04001E5C RID: 7772
		public int deathLevelId = -1;

		// Token: 0x04001E5D RID: 7773
		[ConsoleCommand("")]
		public bool recruited;

		// Token: 0x04001E5E RID: 7774
		[SerializeField]
		private bool _alive = true;

		// Token: 0x04001E5F RID: 7775
		[SerializeField]
		private int _coins;

		// Token: 0x04001E60 RID: 7776
		public SerializableHeroUpgrade classUpgrade;

		// Token: 0x04001E61 RID: 7777
		public SerializableHeroUpgrade itemUpgrade;

		// Token: 0x04001E62 RID: 7778
		public SerializableHeroUpgrade skillUpgrade;

		// Token: 0x04001E63 RID: 7779
		public SerializableHeroUpgrade traitUpgrade;

		// Token: 0x04001E64 RID: 7780
		[NonSerialized]
		public List<SerializableHeroUpgrade> upgrades = new List<SerializableHeroUpgrade>(3);

		// Token: 0x04001E65 RID: 7781
		[Obsolete("usedThisTurn is obselete - use HeroDefinition.fatigued instead")]
		private bool usedThisTurn;

		// Token: 0x04001E66 RID: 7782
		public byte maxUsesPerTurn = 1;

		// Token: 0x04001E67 RID: 7783
		public byte timesUsedThisTurn;

		// Token: 0x04001E68 RID: 7784
		public float discount;

		// Token: 0x04001E69 RID: 7785
		public HeroUpgradeTypeEnum discountType = HeroUpgradeTypeEnum.Consumable;

		// Token: 0x04001E6A RID: 7786
		private HeroStats statistics = new HeroStats();

		// Token: 0x04001E6B RID: 7787
		[ObjectDumper.HideValuesAttribute]
		[NonSerialized]
		public MonoHero monoHero;
	}
}
