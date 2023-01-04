using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A5 RID: 1701
	public class Selectable : AgentComponent, ISquadSelector
	{
		// Token: 0x06002BEE RID: 11246 RVA: 0x000A20E0 File Offset: 0x000A04E0
		EnglishSquad ISquadSelector.GetSelectableSquad()
		{
			return base.agent.squad as EnglishSquad;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000A20F2 File Offset: 0x000A04F2
		bool ISquadSelector.wantsHoverEffect
		{
			get
			{
				return true;
			}
		}
	}
}
