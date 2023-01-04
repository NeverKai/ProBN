using System;
using InspectorExpressions;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArcConsistency
{
	// Token: 0x02000010 RID: 16
	public class ArcRule1 : ArcRule
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003D5C File Offset: 0x0000215C
		public override void Setup()
		{
			Arc.NewArc(this.a, this);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003D6B File Offset: 0x0000216B
		private void Reset()
		{
			if (!this.a)
			{
				this.a = base.GetComponent<Domain>();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003D8C File Offset: 0x0000218C
		public override bool Valid(Domain domain, float value)
		{
			foreach (float num in this.a.GetValues(domain, value))
			{
				if (this.condition.Evaluate(num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000025 RID: 37
		[Space]
		[FormerlySerializedAs("logic")]
		[SerializeField]
		private ArcRule1.Condition condition;

		// Token: 0x04000026 RID: 38
		[SerializeField]
		private Domain a;

		// Token: 0x02000011 RID: 17
		[Serializable]
		private class Condition : ExpressionSerialized<ArcRule1.Condition>
		{
			// Token: 0x0600003C RID: 60 RVA: 0x00003E0C File Offset: 0x0000220C
			[ExpressionEvaluator]
			public bool Evaluate(float A)
			{
				return base.EvaluateBool(new double[]
				{
					(double)A
				});
			}
		}
	}
}
