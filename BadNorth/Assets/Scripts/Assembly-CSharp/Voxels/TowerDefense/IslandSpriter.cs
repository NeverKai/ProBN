using System;
using System.Collections.Generic;

namespace Voxels.TowerDefense
{
	// Token: 0x0200078F RID: 1935
	public class IslandSpriter : IslandComponent, IIslandProcessor
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x000D3548 File Offset: 0x000D1948
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			foreach (object i in island.levelNode.campaign.levelSpriteBaker.BakeSprite(island))
			{
				yield return default(GenInfo);
			}
			yield break;
		}
	}
}
