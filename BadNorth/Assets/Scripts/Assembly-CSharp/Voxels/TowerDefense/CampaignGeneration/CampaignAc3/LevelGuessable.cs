using System;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000701 RID: 1793
	[RequireComponent(typeof(Domain))]
	public class LevelGuessable : MonoBehaviour, IGuessable
	{
		// Token: 0x06002E74 RID: 11892 RVA: 0x000B5309 File Offset: 0x000B3709
		private void Awake()
		{
			this.hash = base.name.GetHashCode();
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000B531C File Offset: 0x000B371C
		IEnumerable<Guess> IGuessable.GetGuesses()
		{
			LevelArcConsistency protoLevel = base.GetComponentInParent<LevelArcConsistency>();
			Domain domain = base.GetComponent<Domain>();
			float p = this.probability.Evaluate(protoLevel, this.hash);
			yield return new Guess(domain, 1f, p);
			yield break;
		}

		// Token: 0x04001EB4 RID: 7860
		[Space]
		[SerializeField]
		private LevelExpression probability;

		// Token: 0x04001EB5 RID: 7861
		private int hash;
	}
}
