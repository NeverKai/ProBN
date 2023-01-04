using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000699 RID: 1689
	public abstract class CloseCombatBrain : Brain
	{
		// Token: 0x06002B93 RID: 11155
		public abstract Attack GetAttack(Agent target);
	}
}
