using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x02000882 RID: 2178
	[Serializable]
	public class Edge
	{
		// Token: 0x060038F8 RID: 14584 RVA: 0x000F7E18 File Offset: 0x000F6218
		public Edge(Vector3 v0, Vector3 v1, Vector3 normal, Vector3 side, string extraString, float normalPrecision, string name = "")
		{
			this.v0 = v0;
			this.v1 = v1;
			this.normal = normal;
			this.extraString = extraString;
			this.normalPrecision = normalPrecision;
			this.name = name;
			this.side = side;
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000F7E55 File Offset: 0x000F6255
		public Vector3 GetVertex(int index)
		{
			if (index == 0)
			{
				return this.v0;
			}
			return this.v1;
		}

		// Token: 0x040026F8 RID: 9976
		public Vector3 v0;

		// Token: 0x040026F9 RID: 9977
		public Vector3 v1;

		// Token: 0x040026FA RID: 9978
		public Vector3 normal;

		// Token: 0x040026FB RID: 9979
		public string name;

		// Token: 0x040026FC RID: 9980
		public string extraString;

		// Token: 0x040026FD RID: 9981
		public float normalPrecision;

		// Token: 0x040026FE RID: 9982
		public Vector3 side;
	}
}
