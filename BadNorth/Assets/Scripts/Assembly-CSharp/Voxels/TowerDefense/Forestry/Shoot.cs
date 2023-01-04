using System;
using UnityEngine;

namespace Voxels.TowerDefense.Forestry
{
	// Token: 0x0200075D RID: 1885
	public class Shoot
	{
		// Token: 0x0600312A RID: 12586 RVA: 0x000CB670 File Offset: 0x000C9A70
		public Shoot(NavPos navPos, ForestDef.Forest forestDef)
		{
			this.forestDef = forestDef;
			this.navPos = navPos;
			this.baseRadius = forestDef.forestDef.GetBaseRadius();
			this.UpdateRadius();
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000CB6A0 File Offset: 0x000C9AA0
		public static void PushApart(Shoot shoot0, Shoot shoot1)
		{
			Vector3 vector = shoot0.navPos.pos - shoot1.navPos.pos;
			float sqrMagnitude = vector.sqrMagnitude;
			float num = shoot0.radius + shoot1.radius;
			if (sqrMagnitude > num * num)
			{
				return;
			}
			float num2 = Mathf.Sqrt(sqrMagnitude);
			float num3 = 1f - num2 / num;
			num3 *= 0.1f;
			Vector3 b = vector.normalized * num3;
			shoot0.move += b;
			shoot1.move -= b;
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000CB73C File Offset: 0x000C9B3C
		public void ComittMove()
		{
			Vector3 pos = this.navPos.pos;
			this.navPos.pos = this.navPos.pos + this.move;
			this.UpdateRadius();
			this.move = Vector3.zero;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000CB782 File Offset: 0x000C9B82
		public void UpdateRadius()
		{
			this.radius = this.forestDef.GetRadius(this.navPos) * this.baseRadius;
		}

		// Token: 0x040020FE RID: 8446
		public NavPos navPos;

		// Token: 0x040020FF RID: 8447
		public float baseRadius;

		// Token: 0x04002100 RID: 8448
		public ForestDef.Forest forestDef;

		// Token: 0x04002101 RID: 8449
		public Vector3 move;

		// Token: 0x04002102 RID: 8450
		public float radius;

		// Token: 0x04002103 RID: 8451
		public float fraction;
	}
}
