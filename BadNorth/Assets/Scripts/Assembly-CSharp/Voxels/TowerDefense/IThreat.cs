using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000686 RID: 1670
	public interface IThreat
	{
		// Token: 0x06002AB6 RID: 10934
		Vector3 GetPos(Agent victim);

		// Token: 0x06002AB7 RID: 10935
		Vector3 GetThreatDir(Agent victim);

		// Token: 0x06002AB8 RID: 10936
		float GetThreatDistance(Agent victim);

		// Token: 0x06002AB9 RID: 10937
		bool GetTreatValid(Agent victim);
	}
}
