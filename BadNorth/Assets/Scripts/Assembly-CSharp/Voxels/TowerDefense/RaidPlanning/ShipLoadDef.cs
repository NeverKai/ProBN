using System;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005B0 RID: 1456
	[Serializable]
	public struct ShipLoadDef
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x00077D10 File Offset: 0x00076110
		public int GetBounty()
		{
			VikingAgent component = this.agentPrefab.GetComponent<VikingAgent>();
			if (component)
			{
				return component.bounty * this.numAgents;
			}
			return 0;
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x00077D43 File Offset: 0x00076143
		public bool IsValid()
		{
			return this.squadPrefab && this.agentPrefab && this.numAgents > 0;
		}

		// Token: 0x0400182B RID: 6187
		public Squad squadPrefab;

		// Token: 0x0400182C RID: 6188
		public Agent agentPrefab;

		// Token: 0x0400182D RID: 6189
		public int numAgents;
	}
}
