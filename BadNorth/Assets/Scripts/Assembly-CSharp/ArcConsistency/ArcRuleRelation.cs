using System;
using System.Linq;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x0200001C RID: 28
	[RequireComponent(typeof(DomainBool))]
	public class ArcRuleRelation : ArcRuleEnumerable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00004399 File Offset: 0x00002799
		public override void Setup()
		{
			base.Setup();
			this.a = base.GetComponent<DomainBool>();
			Arc.NewArc(this.a, this);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000043BC File Offset: 0x000027BC
		public override bool Valid(Domain domain, float value)
		{
			foreach (float num in this.a.GetValues(domain, value))
			{
				if (num == 0f)
				{
					return true;
				}
			}
			ArcRuleRelation.Mode mode = this.mode;
			if (mode == ArcRuleRelation.Mode.NeedsOne)
			{
				foreach (Domain domain2 in this.domains)
				{
					foreach (float num2 in domain2.GetValues(domain, value))
					{
						if (num2 == 1f)
						{
							return true;
						}
					}
				}
				return false;
			}
			if (mode != ArcRuleRelation.Mode.CantHaveAny)
			{
				return true;
			}
			foreach (Domain domain3 in this.domains)
			{
				if (domain3.GetValues(domain, value).All((float x) => x == 1f))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000034 RID: 52
		[SerializeField]
		private ArcRuleRelation.Mode mode;

		// Token: 0x04000035 RID: 53
		private DomainBool a;

		// Token: 0x0200001D RID: 29
		private enum Mode
		{
			// Token: 0x04000038 RID: 56
			NeedsOne,
			// Token: 0x04000039 RID: 57
			CantHaveAny
		}
	}
}
