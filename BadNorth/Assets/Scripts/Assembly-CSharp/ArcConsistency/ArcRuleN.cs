using System;
using System.Collections.Generic;
using InspectorExpressions;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArcConsistency
{
	// Token: 0x02000016 RID: 22
	public class ArcRuleN : ArcRuleEnumerable
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000040DD File Offset: 0x000024DD
		public override void Setup()
		{
			base.Setup();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000040E5 File Offset: 0x000024E5
		public override bool Valid(Domain variable, float value)
		{
			return this.TryHitSum(variable, value, this.domains, 0f, 0);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000040FC File Offset: 0x000024FC
		private bool TryHitSum(Domain variableOverride, float valueOverride, List<Domain> list, float total = 0f, int index = 0)
		{
			if (!this.whileCondition.Evaluate(total, (float)index))
			{
				return false;
			}
			if (index == list.Count)
			{
				return this.finalCondition.Evaluate(total, (float)index);
			}
			foreach (float value in list[index].GetValues(variableOverride, valueOverride))
			{
				float total2 = this.iterator.Evaluate(total, value);
				if (this.TryHitSum(variableOverride, valueOverride, list, total2, index + 1))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400002E RID: 46
		[Space]
		[FormerlySerializedAs("condition")]
		[SerializeField]
		private ArcRuleN.Condition finalCondition;

		// Token: 0x0400002F RID: 47
		[Space]
		[SerializeField]
		private ArcRuleN.Condition whileCondition;

		// Token: 0x04000030 RID: 48
		[Space]
		[SerializeField]
		private ArcRuleN.Iterator iterator;

		// Token: 0x02000017 RID: 23
		[Serializable]
		private class Iterator : ExpressionSerialized<ArcRuleN.Iterator>
		{
			// Token: 0x0600004E RID: 78 RVA: 0x000041C4 File Offset: 0x000025C4
			[ExpressionEvaluator]
			public float Evaluate(float Total, float Value)
			{
				return base.EvaluateFloat(new double[]
				{
					(double)Total,
					(double)Value
				});
			}
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		private class Condition : ExpressionSerialized<ArcRuleN.Condition>
		{
			// Token: 0x06000050 RID: 80 RVA: 0x000041E4 File Offset: 0x000025E4
			[ExpressionEvaluator]
			public bool Evaluate(float Total, float Count)
			{
				return base.EvaluateBool(new double[]
				{
					(double)Total,
					(double)Count
				});
			}
		}
	}
}
