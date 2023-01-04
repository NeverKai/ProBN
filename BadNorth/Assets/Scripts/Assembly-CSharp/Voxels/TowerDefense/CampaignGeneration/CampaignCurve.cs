using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense.CampaignGeneration
{
	// Token: 0x020006D0 RID: 1744
	[Serializable]
	public class CampaignCurve
	{
		// Token: 0x06002D3A RID: 11578 RVA: 0x000A9530 File Offset: 0x000A7930
		public CampaignCurve(float startMin, float startMax, float endMin, float endMax)
		{
			this.startKeys = new List<Vector2>
			{
				new Vector2(startMin, startMax)
			};
			this.midKeys = new List<Vector3>();
			this.endKeys = new List<Vector2>
			{
				new Vector2(endMin, endMax)
			};
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000A95F0 File Offset: 0x000A79F0
		public Vector2 SampleMinMax(int stepsFromStart, int stepsFromEnd, float fraction)
		{
			if (stepsFromStart < this.startKeys.Count)
			{
				return this.startKeys[stepsFromStart];
			}
			if (stepsFromEnd < this.endKeys.Count)
			{
				return this.endKeys[stepsFromEnd];
			}
			float num = (float)(stepsFromStart + stepsFromEnd);
			float b = (this.midKeys.Count <= 0) ? 1f : this.midKeys[0].z;
			float b2 = (this.midKeys.Count <= 0) ? 0f : this.midKeys.Last<Vector3>().z;
			Vector3 v = this.startKeys[this.startKeys.Count - 1].SetZ(Mathf.Min((float)(this.startKeys.Count - 1) / num, b));
			Vector3 v2 = this.endKeys[this.endKeys.Count - 1].SetZ(Mathf.Max(1f - (float)(this.endKeys.Count - 1) / num, b2));
			for (int i = 0; i < this.midKeys.Count; i++)
			{
				Vector3 vector = this.midKeys[i];
				if (vector.z > fraction)
				{
					v2 = vector;
					break;
				}
				v = vector;
			}
			return Vector2.Lerp(v, v2, ExtraMath.RemapValue(fraction, v.z, v2.z));
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x000A9780 File Offset: 0x000A7B80
		public float Sample(int stepsFromStart, int stepsFromEnd, float fraction, float interpolator)
		{
			Vector2 vector = this.SampleMinMax(stepsFromStart, stepsFromEnd, fraction);
			return Mathf.Lerp(vector.x, vector.y, interpolator);
		}

		// Token: 0x04001DBE RID: 7614
		public List<Vector2> startKeys = new List<Vector2>
		{
			new Vector2(0f, 1f)
		};

		// Token: 0x04001DBF RID: 7615
		public List<Vector3> midKeys = new List<Vector3>
		{
			new Vector3(0f, 1f, 0.5f)
		};

		// Token: 0x04001DC0 RID: 7616
		public List<Vector2> endKeys = new List<Vector2>
		{
			new Vector2(0f, 1f)
		};
	}
}
