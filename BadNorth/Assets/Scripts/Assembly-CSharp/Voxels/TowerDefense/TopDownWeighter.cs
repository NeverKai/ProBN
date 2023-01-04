using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000734 RID: 1844
	[Serializable]
	public class TopDownWeighter
	{
		// Token: 0x0600300D RID: 12301 RVA: 0x000C4474 File Offset: 0x000C2874
		public TopDownWeighter(float maxAngle = 45f, float maxDistance = 3.4028235E+38f)
		{
			this.maxAngle = maxAngle;
			this.minDistance = 0f;
			this.maxDistance = maxDistance;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000C44C4 File Offset: 0x000C28C4
		public float GetWeight(Vector3 startPos, Vector3 candidatePos, Vector3 testNormal)
		{
			if (this.minDot > 1f)
			{
				this.minDot = Mathf.Cos(0.017453292f * this.maxAngle);
			}
			Vector3 inVec = candidatePos - startPos;
			Vector3 zeroY = inVec.GetZeroY();
			Vector3 normalized = zeroY.normalized;
			float num = Vector3.Dot(normalized, testNormal);
			float distance = this.GetDistance(zeroY);
			if (num < this.minDot || distance > this.maxDistance || distance < this.minDistance)
			{
				return float.MinValue;
			}
			return num / distance;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000C4550 File Offset: 0x000C2950
		public T GetHighestWeighted<T>(Vector3 startPos, IEnumerable<T> candidates, Vector3 testNormal, out float bestWeight) where T : Component
		{
			bestWeight = float.MinValue;
			T result = (T)((object)null);
			foreach (T t in candidates)
			{
				Vector3 position = t.transform.position;
				float weight = this.GetWeight(startPos, position, testNormal);
				if (weight > bestWeight)
				{
					result = t;
					bestWeight = weight;
				}
			}
			return result;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000C45DC File Offset: 0x000C29DC
		public T GetHighestWeighted<T>(Vector3 startPos, IEnumerable<T> candidates, Vector3 testNormal) where T : Component
		{
			float num;
			return this.GetHighestWeighted<T>(startPos, candidates, testNormal, out num);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000C45F4 File Offset: 0x000C29F4
		private float GetDistance(Vector3 displacement)
		{
			TopDownWeighter.DistanceType distanceType = this.distanceType;
			if (distanceType == TopDownWeighter.DistanceType.Euclidean)
			{
				return displacement.GetZeroY().magnitude;
			}
			if (distanceType != TopDownWeighter.DistanceType.Chebyshev)
			{
				throw new NotImplementedException(string.Format("Unhandled Distance Type {0}", this.distanceType));
			}
			return Mathf.Max(Mathf.Abs(displacement.x), Mathf.Abs(displacement.z));
		}

		// Token: 0x04002020 RID: 8224
		[SerializeField]
		private float maxAngle;

		// Token: 0x04002021 RID: 8225
		private float minDot = float.MaxValue;

		// Token: 0x04002022 RID: 8226
		[SerializeField]
		private float minDistance = 0.05f;

		// Token: 0x04002023 RID: 8227
		[SerializeField]
		private float maxDistance = float.MaxValue;

		// Token: 0x04002024 RID: 8228
		[SerializeField]
		private TopDownWeighter.DistanceType distanceType;

		// Token: 0x02000735 RID: 1845
		private enum DistanceType
		{
			// Token: 0x04002026 RID: 8230
			Euclidean,
			// Token: 0x04002027 RID: 8231
			Chebyshev
		}
	}
}
