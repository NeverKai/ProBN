using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000881 RID: 2177
	[Serializable]
	public class Corner
	{
		// Token: 0x060038F6 RID: 14582 RVA: 0x000F7DF3 File Offset: 0x000F61F3
		public Corner(Vector3 pos)
		{
			this.pos = pos;
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000F7E02 File Offset: 0x000F6202
		public Corner(Vector3 pos, bool inside)
		{
			this.pos = pos;
			this.inside = inside;
		}

		// Token: 0x040026F6 RID: 9974
		public Vector3 pos;

		// Token: 0x040026F7 RID: 9975
		public bool inside;
	}
}
