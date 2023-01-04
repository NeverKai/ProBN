using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x02000007 RID: 7
	public abstract class ArcRuleEnumerable : ArcRule
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000317C File Offset: 0x0000157C
		public override void Setup()
		{
			base.Setup();
			switch (this.addDomainsFrom)
			{
			case ArcRuleEnumerable.Mode.DirectChildren:
				for (int i = 0; i < base.transform.childCount; i++)
				{
					if (base.transform.GetChild(i).gameObject.activeSelf)
					{
						foreach (Domain item in base.transform.GetChild(i).GetComponents<Domain>())
						{
							if (!this.domains.Contains(item))
							{
								this.domains.Add(item);
							}
						}
					}
				}
				break;
			case ArcRuleEnumerable.Mode.AllChildren:
				for (int k = 0; k < base.transform.childCount; k++)
				{
					if (base.transform.GetChild(k).gameObject.activeSelf)
					{
						foreach (Domain item2 in base.transform.GetChild(k).GetComponentsInChildren<Domain>())
						{
							if (!this.domains.Contains(item2))
							{
								this.domains.Add(item2);
							}
						}
					}
				}
				break;
			}
			foreach (Domain domain in this.domains)
			{
				Arc.NewArc(domain, this);
			}
		}

		// Token: 0x04000010 RID: 16
		[Space]
		[SerializeField]
		private ArcRuleEnumerable.Mode addDomainsFrom;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		public List<Domain> domains;

		// Token: 0x02000008 RID: 8
		private enum Mode
		{
			// Token: 0x04000013 RID: 19
			DirectChildren,
			// Token: 0x04000014 RID: 20
			AllChildren,
			// Token: 0x04000015 RID: 21
			Nowhere
		}
	}
}
