using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200077E RID: 1918
	public interface IIslandProcessor
	{
		// Token: 0x060031CB RID: 12747
		IEnumerator<GenInfo> OnIslandProcess(Island island, SavedWave savedWave);
	}
}
