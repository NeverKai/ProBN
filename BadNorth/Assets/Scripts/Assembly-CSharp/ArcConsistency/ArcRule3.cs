using System;
using InspectorExpressions;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArcConsistency
{
	// Token: 0x02000014 RID: 20
	public class ArcRule3 : ArcRule
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003F54 File Offset: 0x00002354
		public override void Setup()
		{
			Arc.NewArc(this.a, this);
			Arc.NewArc(this.b, this);
			Arc.NewArc(this.c, this);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003F7D File Offset: 0x0000237D
		private void Reset()
		{
			if (!this.a)
			{
				this.a = base.GetComponent<Domain>();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003F9C File Offset: 0x0000239C
		public override bool Valid(Domain variable, float value)
		{
			foreach (float num in this.a.GetValues(variable, value))
			{
				foreach (float num2 in this.b.GetValues(variable, value))
				{
					foreach (float num3 in this.c.GetValues(variable, value))
					{
						if (this.condition.Evaluate(num, num2, num3))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0400002A RID: 42
		[Space]
		[FormerlySerializedAs("logic")]
		[SerializeField]
		private ArcRule3.Condition condition;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		private Domain a;

		// Token: 0x0400002C RID: 44
		[SerializeField]
		private Domain b;

		// Token: 0x0400002D RID: 45
		[SerializeField]
		private Domain c;

		// Token: 0x02000015 RID: 21
		[Serializable]
		private class Condition : ExpressionSerialized<ArcRule3.Condition>
		{
			// Token: 0x06000048 RID: 72 RVA: 0x000040B8 File Offset: 0x000024B8
			[ExpressionEvaluator]
			public bool Evaluate(float A, float B, float C)
			{
				return base.EvaluateBool(new double[]
				{
					(double)A,
					(double)B,
					(double)C
				});
			}
		}
	}
}
