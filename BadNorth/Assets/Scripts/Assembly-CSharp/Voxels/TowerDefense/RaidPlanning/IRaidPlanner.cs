using System;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005AA RID: 1450
	public interface IRaidPlanner
	{
		// Token: 0x060025C7 RID: 9671
		void UpdateRaidDefinition();

		// Token: 0x060025C8 RID: 9672
		RaidDef GetRaidDefinition();
	}
}
