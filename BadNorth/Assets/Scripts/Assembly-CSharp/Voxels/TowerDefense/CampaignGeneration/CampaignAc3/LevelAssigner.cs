using System;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x020006FC RID: 1788
	[RequireComponent(typeof(DomainBool))]
	public abstract class LevelAssigner : MonoBehaviour
	{
		// Token: 0x06002E5A RID: 11866 RVA: 0x000B4990 File Offset: 0x000B2D90
		public IEnumerable<LevelObjectReference> MaybeAssign()
		{
			if (base.GetComponent<DomainBool>().definetlyTrue)
			{
				foreach (LevelObjectReference s in this.GetAssignments())
				{
					yield return s;
				}
			}
			yield break;
		}

		// Token: 0x06002E5B RID: 11867
		protected abstract IEnumerable<LevelObjectReference> GetAssignments();

		// Token: 0x04001EA9 RID: 7849
		[SerializeField]
		protected LevelObjectReference.Key key;
	}
}
