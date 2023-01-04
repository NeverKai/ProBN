using System;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000704 RID: 1796
	[RequireComponent(typeof(DomainSum))]
	public class LevelRule1 : ArcRule
	{
		// Token: 0x06002E7D RID: 11901 RVA: 0x000B55A6 File Offset: 0x000B39A6
		public override void Setup()
		{
			base.Setup();
			this.domain = base.GetComponent<Domain>();
			Arc.NewArc(this.domain, this);
			this.protoLevel = base.GetComponentInParent<LevelArcConsistency>();
			this.hash = base.name.GetHashCode();
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000B55E4 File Offset: 0x000B39E4
		public override bool Valid(Domain domain, float value)
		{
			foreach (float a in domain.GetValues(domain, value))
			{
				if (this.condition.Evaluate(this.protoLevel, this.hash, a))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001EBA RID: 7866
		[Space]
		[SerializeField]
		private LevelExpression1 condition;

		// Token: 0x04001EBB RID: 7867
		[HideInInspector]
		[SerializeField]
		private Domain domain;

		// Token: 0x04001EBC RID: 7868
		private LevelArcConsistency protoLevel;

		// Token: 0x04001EBD RID: 7869
		private int hash;
	}
}
