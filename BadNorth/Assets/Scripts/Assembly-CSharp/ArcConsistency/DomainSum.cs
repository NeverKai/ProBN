using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x0200000D RID: 13
	[RequireComponent(typeof(ArcRuleSum))]
	public class DomainSum : Domain
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000036D7 File Offset: 0x00001AD7
		private ArcRuleSum rule
		{
			get
			{
				if (!this._rule)
				{
					this._rule = base.GetComponent<ArcRuleSum>();
				}
				return this._rule;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000036FC File Offset: 0x00001AFC
		protected override List<float> GenerateValues()
		{
			List<float> result = new List<float>(1);
			DomainSum.AddSum(this.rule.domains, this.values, 0f, 0);
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003730 File Offset: 0x00001B30
		private static void AddSum(List<Domain> domainList, List<float> values, float total = 0f, int index = 0)
		{
			if (index == domainList.Count)
			{
				if (!values.Contains(total))
				{
					values.Add(total);
				}
			}
			else
			{
				foreach (float num in domainList[index].GetInitialValues())
				{
					DomainSum.AddSum(domainList, values, total + num, index + 1);
				}
			}
		}

		// Token: 0x04000022 RID: 34
		private ArcRuleSum _rule;
	}
}
