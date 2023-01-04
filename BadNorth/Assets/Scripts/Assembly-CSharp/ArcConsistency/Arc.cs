using System;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x02000002 RID: 2
	public class Arc : MonoBehaviour
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000458
		public void Setup()
		{
			this.domain.arcs.Add(this);
			this.rule.arcs.Add(this);
			if (!this.rule.allDomains.Contains(this.domain))
			{
				this.rule.allDomains.Add(this.domain);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000004B8
		public static Arc NewArc(Domain domain, ArcRule rule)
		{
			Arc arc = domain.gameObject.AddComponent<Arc>();
			arc.domain = domain;
			arc.rule = rule;
			return arc;
		}

		// Token: 0x04000001 RID: 1
		public Domain domain;

		// Token: 0x04000002 RID: 2
		public ArcRule rule;

		// Token: 0x04000003 RID: 3
		[HideInInspector]
		public bool inWorklist;
	}
}
