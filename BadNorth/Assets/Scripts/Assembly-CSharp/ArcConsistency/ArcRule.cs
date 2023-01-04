using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcConsistency
{
	// Token: 0x02000006 RID: 6
	public abstract class ArcRule : MonoBehaviour
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000316D File Offset: 0x0000156D
		public virtual void Setup()
		{
		}

		// Token: 0x06000015 RID: 21
		public abstract bool Valid(Domain variable, float value);

		// Token: 0x06000016 RID: 22 RVA: 0x0000316F File Offset: 0x0000156F
		private void Start()
		{
		}

		// Token: 0x0400000E RID: 14
		[NonSerialized]
		public List<Arc> arcs = new List<Arc>();

		// Token: 0x0400000F RID: 15
		[NonSerialized]
		public List<Domain> allDomains = new List<Domain>();
	}
}
