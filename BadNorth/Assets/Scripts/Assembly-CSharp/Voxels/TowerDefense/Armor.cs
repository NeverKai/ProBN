using System;

namespace Voxels.TowerDefense
{
	// Token: 0x0200068C RID: 1676
	public class Armor : AgentComponent, IAttackResponder
	{
		// Token: 0x06002ACD RID: 10957 RVA: 0x00098EC7 File Offset: 0x000972C7
		public void ModifyAttack(ref Attack attack)
		{
			attack.damage /= this.armor[base.agent.squad.level];
		}

		// Token: 0x04001BD2 RID: 7122
		public float[] armor = new float[]
		{
			0.7f,
			1f,
			1.3f
		};
	}
}
