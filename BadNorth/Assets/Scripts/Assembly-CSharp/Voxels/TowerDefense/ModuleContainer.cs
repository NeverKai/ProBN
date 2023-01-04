using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A1 RID: 1953
	public class ModuleContainer : IslandComponent, IIslandProcessor
	{
		// Token: 0x06003285 RID: 12933 RVA: 0x000D670C File Offset: 0x000D4B0C
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			IEnumerator<GenInfo> reconstruct = savedWave.Reconstruct(base.transform);
			while (reconstruct.MoveNext())
			{
				GenInfo genInfo = reconstruct.Current;
				yield return genInfo;
			}
			yield break;
		}
	}
}
