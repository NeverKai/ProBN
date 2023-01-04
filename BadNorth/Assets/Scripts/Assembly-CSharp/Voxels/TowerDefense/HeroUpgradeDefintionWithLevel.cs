using System;
using System.Diagnostics;

namespace Voxels.TowerDefense
{
	// Token: 0x020006ED RID: 1773
	[DebuggerDisplay("{def.uniqueId} - lvl {level} ({cost} gold)  [HeroDefintionWithLevel]")]
	public struct HeroUpgradeDefintionWithLevel : IComparable<HeroUpgradeDefintionWithLevel>
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x000B28C6 File Offset: 0x000B0CC6
		public HeroUpgradeDefintionWithLevel(HeroUpgradeDefinition def, int level)
		{
			this.def = def;
			this.level = level;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000B28D6 File Offset: 0x000B0CD6
		public HeroUpgradeDefintionWithLevel(SerializableHeroUpgrade upgrade)
		{
			this.def = upgrade.definition;
			this.level = upgrade.level;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000B28F0 File Offset: 0x000B0CF0
		public int cost
		{
			get
			{
				return this.def.GetLevelCost(this.level);
			}
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000B2904 File Offset: 0x000B0D04
		int IComparable<HeroUpgradeDefintionWithLevel>.CompareTo(HeroUpgradeDefintionWithLevel other)
		{
			int num = this.cost - other.cost;
			if (num != 0)
			{
				return num;
			}
			return this.def.uniqueId.CompareTo(other.def.uniqueId);
		}

		// Token: 0x04001E6D RID: 7789
		public HeroUpgradeDefinition def;

		// Token: 0x04001E6E RID: 7790
		public int level;
	}
}
