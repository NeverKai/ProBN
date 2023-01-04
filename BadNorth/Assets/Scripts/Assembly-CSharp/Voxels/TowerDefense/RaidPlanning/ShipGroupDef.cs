using System;
using System.Collections.Generic;
using System.Linq;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005AE RID: 1454
	[Serializable]
	public struct ShipGroupDef
	{
		// Token: 0x060025E6 RID: 9702 RVA: 0x00077B82 File Offset: 0x00075F82
		public int GetBounty()
		{
			return this.ships.Sum((ShipDef ship) => ship.GetBounty());
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00077BAC File Offset: 0x00075FAC
		public int GetAgentCount()
		{
			return this.ships.Sum((ShipDef ship) => ship.GetAgentCount());
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00077BD6 File Offset: 0x00075FD6
		public bool IsValid()
		{
			return this.AreAllChildrenValid();
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x00077BE0 File Offset: 0x00075FE0
		private bool AreAllChildrenValid()
		{
			for (int i = 0; i < this.ships.Count; i++)
			{
				if (!this.ships[i].IsValid())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001823 RID: 6179
		public List<ShipDef> ships;
	}
}
