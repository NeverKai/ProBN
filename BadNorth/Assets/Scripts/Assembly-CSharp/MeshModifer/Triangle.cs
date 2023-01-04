using System;

namespace MeshModifer
{
	// Token: 0x02000628 RID: 1576
	public class Triangle
	{
		// Token: 0x06002878 RID: 10360 RVA: 0x00087230 File Offset: 0x00085630
		public void Invert()
		{
			Vertex vertex = this.verts[0];
			this.verts[0] = this.verts[1];
			this.verts[1] = vertex;
		}

		// Token: 0x040019F9 RID: 6649
		public Vertex[] verts = new Vertex[3];
	}
}
