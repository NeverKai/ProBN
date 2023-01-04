using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlledRandomness
{
	// Token: 0x020004F5 RID: 1269
	public class GenOption : MonoBehaviour, IGenOption
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x00057568 File Offset: 0x00055968
		IEnumerable<Range> IGenOption.ranges
		{
			get
			{
				return this.ranges;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00057570 File Offset: 0x00055970
		IEnumerable<Tag> IGenOption.tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x00057578 File Offset: 0x00055978
		float IGenOption.probability
		{
			get
			{
				return this.probability;
			}
		}

		// Token: 0x0400141E RID: 5150
		[SerializeField]
		public List<Tag> tags = new List<Tag>();

		// Token: 0x0400141F RID: 5151
		[SerializeField]
		public List<Range> ranges = new List<Range>();

		// Token: 0x04001420 RID: 5152
		[SerializeField]
		private float probability = 1f;
	}
}
