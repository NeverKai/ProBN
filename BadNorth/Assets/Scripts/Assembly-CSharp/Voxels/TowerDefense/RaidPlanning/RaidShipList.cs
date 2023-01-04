using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005B3 RID: 1459
	public class RaidShipList : MonoBehaviour
	{
		// Token: 0x0600260B RID: 9739 RVA: 0x00078067 File Offset: 0x00076467
		public Longship GetLongship(int agentCount)
		{
			return this.longships[0];
		}

		// Token: 0x04001830 RID: 6192
		public List<Longship> longships = new List<Longship>();
	}
}
