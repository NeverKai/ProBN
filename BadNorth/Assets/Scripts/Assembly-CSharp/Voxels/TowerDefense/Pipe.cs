using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200065E RID: 1630
	public class Pipe
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x000917BC File Offset: 0x0008FBBC
		public Pipe(Edge edge, bool forward)
		{
			this.forward = forward;
			this.edge = edge;
			this.inVert.pipes.Add(this);
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06002972 RID: 10610 RVA: 0x000917E3 File Offset: 0x0008FBE3
		public Vert inVert
		{
			get
			{
				return (!this.forward) ? this.edge.verts.y : this.edge.verts.x;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x00091815 File Offset: 0x0008FC15
		public Vert outVert
		{
			get
			{
				return (!this.forward) ? this.edge.verts.x : this.edge.verts.y;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x00091847 File Offset: 0x0008FC47
		public Vector3 dir
		{
			get
			{
				return (!this.forward) ? (-this.edge.dir) : this.edge.dir;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x00091874 File Offset: 0x0008FC74
		public Tri tri0
		{
			get
			{
				return (!this.forward) ? this.edge.tris[1] : this.edge.tris[0];
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002976 RID: 10614 RVA: 0x000918A8 File Offset: 0x0008FCA8
		public Tri tri1
		{
			get
			{
				return (!this.forward) ? this.edge.tris[0] : this.edge.tris[1];
			}
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000918DC File Offset: 0x0008FCDC
		public void OnDestroy()
		{
			this.edge = null;
		}

		// Token: 0x04001B0C RID: 6924
		public Edge edge;

		// Token: 0x04001B0D RID: 6925
		public float weight;

		// Token: 0x04001B0E RID: 6926
		public bool forward;
	}
}
