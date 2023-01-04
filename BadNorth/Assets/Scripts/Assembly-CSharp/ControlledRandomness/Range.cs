using System;
using UnityEngine;

namespace ControlledRandomness
{
	// Token: 0x020004F6 RID: 1270
	[Serializable]
	public struct Range
	{
		// Token: 0x06002070 RID: 8304 RVA: 0x00057580 File Offset: 0x00055980
		public Range(string key, float min = 0f, float max = 1f)
		{
			this.key = key;
			this.min = min;
			this.max = max;
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00057598 File Offset: 0x00055998
		public bool Compatible(Range other)
		{
			return this.key != other.key || (this.max >= other.min && this.min <= other.max);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000575E8 File Offset: 0x000559E8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.key,
				" : ",
				this.min.ToString(),
				" - ",
				this.max.ToString()
			});
		}

		// Token: 0x04001421 RID: 5153
		[SerializeField]
		public string key;

		// Token: 0x04001422 RID: 5154
		[SerializeField]
		public float min;

		// Token: 0x04001423 RID: 5155
		[SerializeField]
		public float max;
	}
}
