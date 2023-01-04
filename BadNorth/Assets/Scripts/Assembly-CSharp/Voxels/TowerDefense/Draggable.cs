using System;

namespace Voxels.TowerDefense
{
	// Token: 0x02000752 RID: 1874
	public class Draggable : SquadComponent, ISquadSetup
	{
		// Token: 0x060030E0 RID: 12512 RVA: 0x000C8970 File Offset: 0x000C6D70
		public void SquadSetup(Squad squad)
		{
			base.transform.position = squad.navPos.pos;
		}
	}
}
