using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Voxels.TowerDefense.ProfileInternals
{
	// Token: 0x02000592 RID: 1426
	[Serializable]
	public class MetaInventory
	{
		// Token: 0x060024F5 RID: 9461 RVA: 0x000740ED File Offset: 0x000724ED
		public MetaInventory()
		{
			this.InitStartingUpgrades();
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00074108 File Offset: 0x00072508
		private void InitStartingUpgrades()
		{
			this.startingItems = new List<HeroUpgradeDefinition>(16);
			this.startingTraits = new List<HeroUpgradeDefinition>(24);
			foreach (MetaInventory.UpgradeEntry upgradeEntry in this.upgrades)
			{
				HeroUpgradeDefinition definition = upgradeEntry.upgrade.definition;
				if (upgradeEntry.isStarting)
				{
					this.AddToStarting(definition);
				}
			}
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00074198 File Offset: 0x00072598
		private void AddToStarting(HeroUpgradeDefinition def)
		{
			switch (def.typeEnum)
			{
			case HeroUpgradeTypeEnum.Item:
			case HeroUpgradeTypeEnum.Consumable:
				if (!this.startingItems.Contains(def))
				{
					this.startingItems.Add(def);
				}
				return;
			case HeroUpgradeTypeEnum.Trait:
				if (!this.startingTraits.Contains(def))
				{
					this.startingTraits.Add(def);
				}
				return;
			}
			Debug.LogError("HeroUpgrade '{0}' cannot be a starting item");
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0007421E File Offset: 0x0007261E
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			this.InitStartingUpgrades();
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00074228 File Offset: 0x00072628
		private MetaInventory.UpgradeEntry Get(HeroUpgradeDefinition upgrade)
		{
			foreach (MetaInventory.UpgradeEntry upgradeEntry in this.upgrades)
			{
				if (upgradeEntry.upgrade.definition == upgrade)
				{
					return upgradeEntry;
				}
			}
			return null;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000742A0 File Offset: 0x000726A0
		public bool Get(HeroUpgradeDefinition upgrade, out int level, out bool isNew, out bool isStarting)
		{
			MetaInventory.UpgradeEntry upgradeEntry = this.Get(upgrade);
			if (upgradeEntry == null)
			{
				isNew = (isStarting = false);
				level = -1;
				return false;
			}
			isNew = upgradeEntry.isNew;
			isStarting = upgradeEntry.isStarting;
			level = upgradeEntry.level;
			return true;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000742E4 File Offset: 0x000726E4
		public bool IsStartingItem(HeroUpgradeDefinition upgrade)
		{
			MetaInventory.UpgradeEntry upgradeEntry = this.Get(upgrade);
			return upgradeEntry != null && upgradeEntry.isStarting;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00074308 File Offset: 0x00072708
		public void Add(HeroUpgradeDefinition upgrade, int level, bool isStarting = false)
		{
			MetaInventory.UpgradeEntry upgradeEntry = this.Get(upgrade);
			if (upgradeEntry == null)
			{
				this.upgrades.Add(new MetaInventory.UpgradeEntry(upgrade, level, isStarting));
			}
			else
			{
				int level2 = upgradeEntry.level;
				bool isStarting2 = upgradeEntry.isStarting;
				upgradeEntry.level = Mathf.Max(upgradeEntry.level, level);
				upgradeEntry.isStarting = isStarting;
				upgradeEntry.isNew |= (upgradeEntry.level != level2 || upgradeEntry.isStarting != isStarting2);
			}
			if (isStarting)
			{
				this.AddToStarting(upgrade);
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000743A0 File Offset: 0x000727A0
		public void SetNew(HeroUpgradeDefinition upgrade, bool isNew)
		{
			MetaInventory.UpgradeEntry upgradeEntry = this.Get(upgrade);
			if (upgradeEntry != null)
			{
				upgradeEntry.isNew = isNew;
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000743C4 File Offset: 0x000727C4
		public void UnmarkAllAsNew()
		{
			foreach (MetaInventory.UpgradeEntry upgradeEntry in this.upgrades)
			{
				upgradeEntry.isNew = false;
			}
		}

		// Token: 0x040017A0 RID: 6048
		private List<MetaInventory.UpgradeEntry> upgrades = new List<MetaInventory.UpgradeEntry>();

		// Token: 0x040017A1 RID: 6049
		[NonSerialized]
		public List<HeroUpgradeDefinition> startingItems;

		// Token: 0x040017A2 RID: 6050
		[NonSerialized]
		public List<HeroUpgradeDefinition> startingTraits;

		// Token: 0x02000593 RID: 1427
		[ObjectDumper.LeafAttribute]
		[Serializable]
		private class UpgradeEntry
		{
			// Token: 0x060024FF RID: 9471 RVA: 0x00074420 File Offset: 0x00072820
			public UpgradeEntry(SerializableHeroUpgrade upgrade, bool isStarting = false)
			{
				this.upgrade = upgrade;
				this.isStarting = isStarting;
				this.isNew = true;
			}

			// Token: 0x06002500 RID: 9472 RVA: 0x0007443D File Offset: 0x0007283D
			public UpgradeEntry(HeroUpgradeDefinition upgrade, int level, bool isStarting = false) : this(new SerializableHeroUpgrade(upgrade, level), isStarting)
			{
			}

			// Token: 0x170004C5 RID: 1221
			// (get) Token: 0x06002501 RID: 9473 RVA: 0x0007444D File Offset: 0x0007284D
			// (set) Token: 0x06002502 RID: 9474 RVA: 0x0007445A File Offset: 0x0007285A
			public int level
			{
				get
				{
					return this.upgrade.level;
				}
				set
				{
					this.upgrade.level = value;
				}
			}

			// Token: 0x06002503 RID: 9475 RVA: 0x00074468 File Offset: 0x00072868
			public override string ToString()
			{
				return string.Format("{0} [level {1}], isStarting = {2}, isNew = {3}", new object[]
				{
					this.upgrade.definition.uniqueId,
					this.level,
					this.isStarting,
					this.isNew
				});
			}

			// Token: 0x040017A3 RID: 6051
			public SerializableHeroUpgrade upgrade;

			// Token: 0x040017A4 RID: 6052
			public bool isStarting;

			// Token: 0x040017A5 RID: 6053
			public bool isNew;
		}
	}
}
