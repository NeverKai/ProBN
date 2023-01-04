using System;

namespace Voxels.TowerDefense.Reflex
{
	// Token: 0x02000599 RID: 1433
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
	public class UpgradeTypeAttribute : Attribute
	{
		// Token: 0x06002556 RID: 9558 RVA: 0x00075D0A File Offset: 0x0007410A
		private UpgradeTypeAttribute()
		{
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00075D12 File Offset: 0x00074112
		public UpgradeTypeAttribute(HeroUpgradeTypeEnum type)
		{
			this.type = type;
		}

		// Token: 0x040017BD RID: 6077
		public readonly HeroUpgradeTypeEnum type;
	}
}
