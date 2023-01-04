using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000797 RID: 1943
	public class LevelColorer : AgentComponent, ILevelComponent
	{
		// Token: 0x0600321F RID: 12831 RVA: 0x000D497D File Offset: 0x000D2D7D
		void ILevelComponent.OnSetLevel(Agent agent, int level)
		{
		}

		// Token: 0x0400220F RID: 8719
		public bool hero;
	}
}
