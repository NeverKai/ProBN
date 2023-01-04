using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020005BE RID: 1470
	[AssetPath("Assets/Settings/Resources/")]
	[Serializable]
	public class CampaignTuning : ScriptableObjectSingleton<CampaignTuning>
	{
		// Token: 0x04001862 RID: 6242
		[Header("Viking Frontier")]
		public int turnsPerVikingProgress = 2;

		// Token: 0x04001863 RID: 6243
		[Header("Loadout")]
		public int maxSquads = 4;
	}
}
