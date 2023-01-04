using System;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x02000711 RID: 1809
	public class LevelTransform : ChildComponent<LevelVisuals>, LevelNode.ILevelSetup
	{
		// Token: 0x06002EEE RID: 12014 RVA: 0x000B7FD0 File Offset: 0x000B63D0
		void LevelNode.ILevelSetup.OnLevelSetup(LevelNode level)
		{
			base.transform.localPosition = (base.transform.localPosition * Mathf.Lerp(1f, base.manager.scaling, this.offset)).SetZ(base.transform.localPosition.z);
			base.transform.localScale = (base.transform.localScale * Mathf.Lerp(1f, base.manager.scaling, this.scaling)).SetZ(base.transform.localScale.z);
		}

		// Token: 0x04001EF4 RID: 7924
		[Range(0f, 1f)]
		[SerializeField]
		private float offset = 1f;

		// Token: 0x04001EF5 RID: 7925
		[Range(0f, 1f)]
		[SerializeField]
		private float scaling;
	}
}
