using System;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007C2 RID: 1986
	[Serializable]
	public struct Attributes
	{
		// Token: 0x06003380 RID: 13184 RVA: 0x000DD48F File Offset: 0x000DB88F
		public Attributes(Vector2 startVelocity)
		{
			this.startVelocity = startVelocity;
			this.impactVelocity = Vector2.zero;
			this.highPoint = Vector2.zero;
			this.midPoint = Vector2.zero;
			this.impactTime = 0f;
			this.validity = 0f;
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000DD4CF File Offset: 0x000DB8CF
		// (set) Token: 0x06003382 RID: 13186 RVA: 0x000DD4DE File Offset: 0x000DB8DE
		public bool valid
		{
			get
			{
				return this.validity > 0.5f;
			}
			set
			{
				this.validity = (float)((!value) ? 0 : 1);
			}
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000DD4F4 File Offset: 0x000DB8F4
		public static Attributes operator *(Attributes a, float b)
		{
			a.startVelocity *= b;
			a.impactVelocity *= b;
			a.highPoint *= b;
			a.midPoint *= b;
			a.impactTime *= b;
			a.validity *= b;
			return a;
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000DD56C File Offset: 0x000DB96C
		public static Attributes operator +(Attributes a, Attributes b)
		{
			a.startVelocity += b.startVelocity;
			a.impactVelocity += b.impactVelocity;
			a.highPoint += b.highPoint;
			a.midPoint += b.midPoint;
			a.impactTime += b.impactTime;
			a.validity += b.validity;
			return a;
		}

		// Token: 0x04002311 RID: 8977
		public Vector2 startVelocity;

		// Token: 0x04002312 RID: 8978
		public Vector2 impactVelocity;

		// Token: 0x04002313 RID: 8979
		public Vector2 highPoint;

		// Token: 0x04002314 RID: 8980
		public Vector2 midPoint;

		// Token: 0x04002315 RID: 8981
		public float impactTime;

		// Token: 0x04002316 RID: 8982
		public float validity;
	}
}
