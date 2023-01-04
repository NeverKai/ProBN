using System;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000702 RID: 1794
	[RequireComponent(typeof(DomainBool))]
	public class LevelRule : ArcRule
	{
		// Token: 0x06002E77 RID: 11895 RVA: 0x000B546C File Offset: 0x000B386C
		public override void Setup()
		{
			base.Setup();
			this.domain = base.GetComponent<Domain>();
			Arc.NewArc(this.domain, this);
			this.protoLevel = base.GetComponentInParent<LevelArcConsistency>();
			this.hash = base.name.GetHashCode();
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000B54AA File Offset: 0x000B38AA
		public override bool Valid(Domain domain, float value)
		{
			return value <= this.condition.Evaluate(this.protoLevel, this.hash);
		}

		// Token: 0x04001EB6 RID: 7862
		[Space]
		[SerializeField]
		private LevelExpression condition;

		// Token: 0x04001EB7 RID: 7863
		[HideInInspector]
		[SerializeField]
		private Domain domain;

		// Token: 0x04001EB8 RID: 7864
		private LevelArcConsistency protoLevel;

		// Token: 0x04001EB9 RID: 7865
		private int hash;
	}
}
