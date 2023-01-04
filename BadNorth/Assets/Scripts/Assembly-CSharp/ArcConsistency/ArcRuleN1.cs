using System;
using System.Collections.Generic;
using InspectorExpressions;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x02000019 RID: 25
	public class ArcRuleN1 : ArcRuleEnumerable
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00004204 File Offset: 0x00002604
		public override void Setup()
		{
			base.Setup();
			Arc.NewArc(this.a, this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000421C File Offset: 0x0000261C
		public override bool Valid(Domain variable, float value)
		{
			foreach (float num in this.a.GetValues(variable, value))
			{
				if (this.TryHitSum(variable, value, this.domains, num, 0f, 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000429C File Offset: 0x0000269C
		private bool TryHitSum(Domain variableOverride, float valueOverride, List<Domain> list, float A, float sum = 0f, int index = 0)
		{
			if (index == list.Count)
			{
				return this.condition.Evaluate(A, sum, (float)index);
			}
			foreach (float variable in list[index].GetValues(variableOverride, valueOverride))
			{
				float num = this.iterator.Evaluate(A, sum, variable);
				if (this.TryHitSum(variableOverride, valueOverride, list, num, (float)(index + 1), 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000031 RID: 49
		[Space]
		[SerializeField]
		private ArcRuleN1.Condition condition;

		// Token: 0x04000032 RID: 50
		[Space]
		[SerializeField]
		private ArcRuleN1.Iterator iterator;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		private Domain a;

		// Token: 0x0200001A RID: 26
		[Serializable]
		private class Iterator : ExpressionSerialized<ArcRuleN1.Iterator>
		{
			// Token: 0x06000056 RID: 86 RVA: 0x00004354 File Offset: 0x00002754
			[ExpressionEvaluator]
			public float Evaluate(float A, float Total, float Variable)
			{
				return base.EvaluateFloat(new double[]
				{
					(double)Total,
					(double)Variable
				});
			}
		}

		// Token: 0x0200001B RID: 27
		[Serializable]
		private class Condition : ExpressionSerialized<ArcRuleN1.Condition>
		{
			// Token: 0x06000058 RID: 88 RVA: 0x00004374 File Offset: 0x00002774
			[ExpressionEvaluator]
			public bool Evaluate(float A, float Total, float Count)
			{
				return base.EvaluateBool(new double[]
				{
					(double)A,
					(double)Total,
					(double)Count
				});
			}
		}
	}
}
