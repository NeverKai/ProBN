using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x020007EA RID: 2026
	public class SwordSquadCoordinator : SquadCoordinatorAgentTracker<Swordsman>
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060034A7 RID: 13479 RVA: 0x000E2C80 File Offset: 0x000E1080
		public List<Swordsman> swordsmen
		{
			get
			{
				return this.agentComponents;
			}
		}
	}
}
