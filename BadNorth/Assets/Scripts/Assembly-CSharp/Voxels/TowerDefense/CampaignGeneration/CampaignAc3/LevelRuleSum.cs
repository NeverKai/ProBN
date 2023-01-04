using System;
using System.Collections.Generic;
using ArcConsistency;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration.CampaignAc3
{
	// Token: 0x02000706 RID: 1798
	public class LevelRuleSum : ArcRuleEnumerable
	{
		// Token: 0x06002E83 RID: 11907 RVA: 0x000B5734 File Offset: 0x000B3B34
		public override void Setup()
		{
			base.Setup();
			this.protoLevel = base.GetComponentInParent<LevelArcConsistency>();
			this.hash = base.name.GetHashCode();
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000B575C File Offset: 0x000B3B5C
		public override bool Valid(Domain variable, float value)
		{
			float num = this.min.Evaluate(this.protoLevel, this.hash);
			float num2 = this.max.Evaluate(this.protoLevel, this.hash);
			return this.TryHitSum(variable, value, this.domains, num, num2, 0f, 0);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000B57B0 File Offset: 0x000B3BB0
		private bool TryHitSum(Domain variableOverride, float valueOverride, List<Domain> list, float min, float max, float total = 0f, int index = 0)
		{
			if (index == list.Count)
			{
				return total >= min && total <= max;
			}
			foreach (float num in list[index].GetValues(variableOverride, valueOverride))
			{
				if (this.TryHitSum(variableOverride, valueOverride, list, min, max, total + num, index + 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001EBE RID: 7870
		[Space]
		[SerializeField]
		private LevelExpression min;

		// Token: 0x04001EBF RID: 7871
		[SerializeField]
		private LevelExpression max;

		// Token: 0x04001EC0 RID: 7872
		[HideInInspector]
		[SerializeField]
		private LevelArcConsistency protoLevel;

		// Token: 0x04001EC1 RID: 7873
		private int hash;
	}
}
