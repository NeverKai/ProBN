using System;
using UnityEngine;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x02000807 RID: 2055
	public struct Content
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x000E7ACC File Offset: 0x000E5ECC
		public void Clear(float maxDistance)
		{
			this.amount = 0f;
			this.direction = Vector3.zero;
			this.amount = 0f;
			this.distance = maxDistance;
			this.newDistance = maxDistance;
			this.data = Data.empty;
			this.newData = Data.empty;
			this.occupied = false;
			this.inVector = Vector3.zero;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000E7B30 File Offset: 0x000E5F30
		public void Decay(float maxDistance, float ratioRemain)
		{
			this.occupied = false;
			this.inVector = Vector3.zero;
			this.newDistance = maxDistance;
			this.newData = Data.empty;
			this.amount *= ratioRemain;
			this.direction = Vector3.zero;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000E7B6F File Offset: 0x000E5F6F
		public bool Comparable(Content other)
		{
			return this.data.dangerous == other.data.dangerous && this.data.hittable == other.data.hittable;
		}

		// Token: 0x0400248F RID: 9359
		public Vector3 direction;

		// Token: 0x04002490 RID: 9360
		public float distance;

		// Token: 0x04002491 RID: 9361
		public float newDistance;

		// Token: 0x04002492 RID: 9362
		public float amount;

		// Token: 0x04002493 RID: 9363
		public Data data;

		// Token: 0x04002494 RID: 9364
		public Data newData;

		// Token: 0x04002495 RID: 9365
		public bool occupied;

		// Token: 0x04002496 RID: 9366
		public Vector3 inVector;
	}
}
