using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x0200070E RID: 1806
	public class LevelReferencesTest : ChildComponent<LevelNode>, LevelNode.ILevelSetup
	{
		// Token: 0x06002EDA RID: 11994 RVA: 0x000B6F76 File Offset: 0x000B5376
		void LevelNode.ILevelSetup.OnLevelSetup(LevelNode level)
		{
			this.obects.AddRange(level.levelState.GetReferencedObjects());
		}

		// Token: 0x04001EE8 RID: 7912
		[SerializeField]
		private List<UnityEngine.Object> obects = new List<UnityEngine.Object>();
	}
}
