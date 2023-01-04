using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000781 RID: 1921
	public interface IIslandWipe
	{
		// Token: 0x060031CE RID: 12750
		IEnumerator<GenInfo> OnIslandWipe(Island island);
	}
}
