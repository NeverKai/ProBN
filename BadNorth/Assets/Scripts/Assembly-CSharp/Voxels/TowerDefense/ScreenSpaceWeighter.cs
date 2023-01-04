using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000736 RID: 1846
	[Serializable]
	public class ScreenSpaceWeighter
	{
		// Token: 0x06003012 RID: 12306 RVA: 0x000C4661 File Offset: 0x000C2A61
		public ScreenSpaceWeighter(float maxAngle = 45f, float maxDistance = 3.4028235E+38f)
		{
			this.maxAngle = maxAngle;
			this.maxDistance = maxDistance;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000C4684 File Offset: 0x000C2A84
		public float GetWeight(Vector2 currentPos, Vector2 candidatePos, Vector2 stickInputNormal)
		{
			if (this.minDot > 1f)
			{
				this.minDot = Mathf.Cos(0.017453292f * this.maxAngle);
			}
			Vector2 normalized = (candidatePos - currentPos).normalized;
			float num = Vector2.Dot(normalized, stickInputNormal);
			float num2 = Vector2.Distance(currentPos, candidatePos);
			if (num < this.minDot || num2 > this.maxDistance || num2 < 0.001f)
			{
				return float.MinValue;
			}
			return num / num2;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000C4704 File Offset: 0x000C2B04
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

		// Token: 0x06003015 RID: 12309 RVA: 0x000C47A0 File Offset: 0x000C2BA0
		public T GetHighestWeighted<T>(Vector3 startPos, IEnumerable<T> candidates, Vector3 testNormal) where T : Component
		{
			float num;
			return this.GetHighestWeighted<T>(startPos, candidates, testNormal, out num);
		}

		// Token: 0x04002028 RID: 8232
		[SerializeField]
		private float maxAngle;

		// Token: 0x04002029 RID: 8233
		private float minDot = float.MaxValue;

		// Token: 0x0400202A RID: 8234
		[SerializeField]
		private float maxDistance;
	}
}
