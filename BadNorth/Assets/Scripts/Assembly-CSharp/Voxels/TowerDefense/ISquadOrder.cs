using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000689 RID: 1673
	public interface ISquadOrder
	{
		// Token: 0x06002ABC RID: 10940
		void GetSquadOrder(Agent agent, ref Vector3 walkDir, ref Vector3 lookDir, ref float movaility);

		// Token: 0x06002ABD RID: 10941
		Vector3 GetOrderWalkDir(Agent agent);

		// Token: 0x06002ABE RID: 10942
		Vector3 GetOrderWalkDir(NavPos agent);

		// Token: 0x06002ABF RID: 10943
		float GetDistance(NavPos navPos);
	}
}
