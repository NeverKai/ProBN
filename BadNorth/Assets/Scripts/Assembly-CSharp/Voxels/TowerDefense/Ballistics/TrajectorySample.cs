using System;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007C3 RID: 1987
	[Serializable]
	public struct TrajectorySample
	{
		// Token: 0x06003385 RID: 13189 RVA: 0x000DD608 File Offset: 0x000DBA08
		public TrajectorySample(Vector2 dirXZ, Attributes attributes)
		{
			this.dirXZ = dirXZ;
			this.attributes = attributes;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000DD618 File Offset: 0x000DBA18
		public Vector3 startVelocity
		{
			get
			{
				return this.To3D(this.attributes.startVelocity);
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000DD62B File Offset: 0x000DBA2B
		public Vector3 impactVelocity
		{
			get
			{
				return this.To3D(this.attributes.impactVelocity);
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x000DD63E File Offset: 0x000DBA3E
		public Vector3 highPoint
		{
			get
			{
				return this.To3D(this.attributes.highPoint);
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x000DD651 File Offset: 0x000DBA51
		public Vector3 midPoint
		{
			get
			{
				return this.To3D(this.attributes.midPoint);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x000DD664 File Offset: 0x000DBA64
		public float impactTime
		{
			get
			{
				return this.attributes.impactTime;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x000DD671 File Offset: 0x000DBA71
		public float validity
		{
			get
			{
				return this.attributes.validity;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x000DD67E File Offset: 0x000DBA7E
		public bool valid
		{
			get
			{
				return this.attributes.validity > 0.5f;
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000DD692 File Offset: 0x000DBA92
		private Vector3 To3D(Vector2 vector)
		{
			return new Vector3(vector.x * this.dirXZ.x, vector.y, vector.x * this.dirXZ.y);
		}

		// Token: 0x04002317 RID: 8983
		private Attributes attributes;

		// Token: 0x04002318 RID: 8984
		private Vector2 dirXZ;
	}
}
