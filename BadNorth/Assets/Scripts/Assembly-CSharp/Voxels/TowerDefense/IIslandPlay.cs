using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x02000782 RID: 1922
	public interface IIslandPlay
	{
		// Token: 0x060031CF RID: 12751
		IEnumerator<GenInfo> OnIslandPlay(Island island);
	}
}
