using System;
using System.Collections.Generic;
using InspectorExpressions;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x0200000E RID: 14
	[RequireComponent(typeof(DomainBool))]
	public class GuessableBool : MonoBehaviour, IGuessable
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000037C4 File Offset: 0x00001BC4
		private DomainBool domain
		{
			get
			{
				if (!this._domain)
				{
					this._domain = base.GetComponent<DomainBool>();
				}
				return this._domain;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000037E8 File Offset: 0x00001BE8
		IEnumerable<Guess> IGuessable.GetGuesses()
		{
			yield return new Guess(this.domain, 1f, this.probability.Evaluate(UnityEngine.Random.value));
			yield break;
		}

		// Token: 0x04000023 RID: 35
		private DomainBool _domain;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		private GuessableBool.Probability probability;

		// Token: 0x0200000F RID: 15
		[Serializable]
		private class Probability : ExpressionSerialized<GuessableBool.Probability>
		{
			// Token: 0x06000036 RID: 54 RVA: 0x00003C52 File Offset: 0x00002052
			[ExpressionEvaluator]
			public float Evaluate(float Random)
			{
				return (float)base.EvaluateDouble(new double[]
				{
					(double)Random
				});
			}
		}
	}
}
