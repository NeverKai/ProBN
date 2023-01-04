using System;
using System.Collections.Generic;
using System.Linq;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005AD RID: 1453
	[Serializable]
	public class WaveDef
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00077A1A File Offset: 0x00075E1A
		public float timePadding
		{
			get
			{
				return this.duration + 20f + (float)this.GetBounty() * 0.1f;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x00077A38 File Offset: 0x00075E38
		public float duration
		{
			get
			{
				float num = this.shipGroups.Max((ShipGroupDef x) => x.ships.Max((ShipDef y) => y.delayTime));
				float num2 = this.shipGroups.Min((ShipGroupDef x) => x.ships.Min((ShipDef y) => y.delayTime));
				return num - num2;
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00077A9A File Offset: 0x00075E9A
		public int GetBounty()
		{
			return this.shipGroups.Sum((ShipGroupDef shipGroup) => shipGroup.GetBounty());
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00077AC4 File Offset: 0x00075EC4
		public bool IsValid()
		{
			return this.AreAllChildrenValid();
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00077ACC File Offset: 0x00075ECC
		private bool AreAllChildrenValid()
		{
			for (int i = 0; i < this.shipGroups.Count; i++)
			{
				if (!this.shipGroups[i].IsValid())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400181D RID: 6173
		public List<ShipGroupDef> shipGroups;
	}
}
