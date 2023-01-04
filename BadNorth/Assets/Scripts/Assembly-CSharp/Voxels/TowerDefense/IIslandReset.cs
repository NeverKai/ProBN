using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000783 RID: 1923
	public interface IIslandReset
	{
		// Token: 0x060031D0 RID: 12752
		IEnumerator<GenInfo> OnIslandReset(Island island);
	}
}
