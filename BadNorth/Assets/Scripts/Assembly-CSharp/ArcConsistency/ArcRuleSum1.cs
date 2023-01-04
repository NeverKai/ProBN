using System;
using System.Collections.Generic;
using InspectorExpressions;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x0200001F RID: 31
	public class ArcRuleSum1 : ArcRuleEnumerable
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000046C8 File Offset: 0x00002AC8
		private void Reset()
		{
			this.a = base.GetComponent<Domain>();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000046D6 File Offset: 0x00002AD6
		public override void Setup()
		{
			base.Setup();
			Arc.NewArc(this.a, this);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000046EC File Offset: 0x00002AEC
		public override bool Valid(Domain variable, float value)
		{
			foreach (float num in this.a.GetValues(variable, value))
			{
				if (ArcRuleSum1.TryHitSum(variable, value, this.domains, this.min.Evaluate(num, (float)this.domains.Count), this.max.Evaluate(num, (float)this.domains.Count), 0f, 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000479C File Offset: 0x00002B9C
		private static bool TryHitSum(Domain variableOverride, float valueOverride, List<Domain> list, float min, float max, float total = 0f, int index = 0)
		{
			if (index == list.Count)
			{
				return total >= min && total <= max;
			}
			if (total > max)
			{
				return false;
			}
			foreach (float num in list[index].GetValues(variableOverride, valueOverride))
			{
				if (ArcRuleSum1.TryHitSum(variableOverride, valueOverride, list, min, max, total + num, index + 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400003B RID: 59
		[Space]
		[SerializeField]
		private ArcRuleSum1.SumCondition min;

		// Token: 0x0400003C RID: 60
		[SerializeField]
		private ArcRuleSum1.SumCondition max;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		private Domain a;

		// Token: 0x02000020 RID: 32
		[Serializable]
		private class SumCondition : ExpressionSerialized<ArcRuleSum1.SumCondition>
		{
			// Token: 0x06000067 RID: 103 RVA: 0x00004854 File Offset: 0x00002C54
			[ExpressionEvaluator]
			public float Evaluate(float A, float count)
			{
				return base.EvaluateFloat(new double[]
				{
					(double)A,
					(double)count
				});
			}
		}
	}
}
