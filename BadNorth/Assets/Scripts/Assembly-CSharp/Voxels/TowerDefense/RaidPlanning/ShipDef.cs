using System;
using System.Collections.Generic;
using System.Linq;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005AF RID: 1455
	[Serializable]
	public struct ShipDef
	{
		// Token: 0x060025EC RID: 9708 RVA: 0x00077C37 File Offset: 0x00076037
		public int GetBounty()
		{
			return this.load.Sum((ShipLoadDef load) => load.GetBounty());
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00077C61 File Offset: 0x00076061
		public int GetAgentCount()
		{
			return this.load.Sum((ShipLoadDef load) => load.numAgents);
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x00077C8B File Offset: 0x0007608B
		public bool IsValid()
		{
			return this.shipPrefab && this.delayTime >= 0f && this.AreAllChildrenValid();
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00077CB8 File Offset: 0x000760B8
		private bool AreAllChildrenValid()
		{
			for (int i = 0; i < this.load.Count; i++)
			{
				if (!this.load[i].IsValid())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001826 RID: 6182
		public float delayTime;

		// Token: 0x04001827 RID: 6183
		public Longship shipPrefab;

		// Token: 0x04001828 RID: 6184
		public List<ShipLoadDef> load;
	}
}
