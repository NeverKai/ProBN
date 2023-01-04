using System;
using InspectorExpressions;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArcConsistency
{
	// Token: 0x02000012 RID: 18
	public class ArcRule2 : ArcRule
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003E27 File Offset: 0x00002227
		public override void Setup()
		{
			Arc.NewArc(this.a, this);
			Arc.NewArc(this.b, this);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003E43 File Offset: 0x00002243
		private void Reset()
		{
			if (!this.a)
			{
				this.a = base.GetComponent<Domain>();
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003E64 File Offset: 0x00002264
		public override bool Valid(Domain variable, float value)
		{
			foreach (float num in this.a.GetValues(variable, value))
			{
				foreach (float num2 in this.b.GetValues(variable, value))
				{
					if (this.condition.Evaluate(num, num2))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000027 RID: 39
		[Space]
		[FormerlySerializedAs("logic")]
		[SerializeField]
		private ArcRule2.Condition condition;

		// Token: 0x04000028 RID: 40
		[SerializeField]
		private Domain a;

		// Token: 0x04000029 RID: 41
		[SerializeField]
		private Domain b;

		// Token: 0x02000013 RID: 19
		[Serializable]
		private class Condition : ExpressionSerialized<ArcRule2.Condition>
		{
			// Token: 0x06000042 RID: 66 RVA: 0x00003F34 File Offset: 0x00002334
			[ExpressionEvaluator]
			public bool Evaluate(float A, float B)
			{
				return base.EvaluateBool(new double[]
				{
					(double)A,
					(double)B
				});
			}
		}
	}
}
