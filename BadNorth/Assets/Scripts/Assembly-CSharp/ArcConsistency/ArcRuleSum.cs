using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x0200001E RID: 30
	[RequireComponent(typeof(DomainSum))]
	public class ArcRuleSum : ArcRuleEnumerable
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000457E File Offset: 0x0000297E
		public override void Setup()
		{
			base.Setup();
			this.domainSum = base.gameObject.GetOrAddComponent<DomainSum>();
			Arc.NewArc(this.domainSum, this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000045A4 File Offset: 0x000029A4
		public override bool Valid(Domain variable, float value)
		{
			foreach (float target in this.domainSum.GetValues(variable, value))
			{
				if (ArcRuleSum.TryHitSum(variable, value, this.domains, target, 0f, 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004624 File Offset: 0x00002A24
		private static bool TryHitSum(Domain variableOverride, float valueOverride, List<Domain> list, float target, float total = 0f, int index = 0)
		{
			if (index == list.Count)
			{
				return total == target;
			}
			if (total > target)
			{
				return false;
			}
			foreach (float num in list[index].GetValues(variableOverride, valueOverride))
			{
				if (ArcRuleSum.TryHitSum(variableOverride, valueOverride, list, target, total + num, index + 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400003A RID: 58
		private DomainSum domainSum;
	}
}
