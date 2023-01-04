using System;
using UnityEngine;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x0200080A RID: 2058
	public class FlowField
	{
		// Token: 0x060035CB RID: 13771 RVA: 0x000E7C8F File Offset: 0x000E608F
		private FlowField()
		{
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000E7C98 File Offset: 0x000E6098
		public FlowField(NavigationMesh navMesh)
		{
			this.navMesh = navMesh;
			this.flowContents = new Content[navMesh.verts.Length];
			for (int i = 0; i < this.flowContents.Length; i++)
			{
				this.flowContents[i] = default(Content);
				this.flowContents[i].Clear(1000000f);
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000E7D10 File Offset: 0x000E6110
		public void CopyFrom(FlowField other)
		{
			if (other.flowContents.Length != this.flowContents.Length)
			{
				this.flowContents = new Content[other.flowContents.Length];
			}
			Content[] array = other.flowContents;
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				this.flowContents[i] = array[i];
				i++;
			}
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000E7D80 File Offset: 0x000E6180
		public void Clear()
		{
			for (int i = 0; i < this.flowContents.Length; i++)
			{
				this.flowContents[i].Clear(1000000f);
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000E7DBC File Offset: 0x000E61BC
		public Content GetClosestFlowContent(NavPos navPos)
		{
			Tri tri = navPos.tri;
			Content result = this.flowContents[(int)tri.verts[0].index];
			float num = float.MaxValue;
			for (int i = 0; i < 3; i++)
			{
				Vert vert = tri.verts[i];
				Content content = this.flowContents[(int)vert.index];
				float num2 = content.distance + (vert.pos - navPos.pos).sqrMagnitude;
				if (num2 < num)
				{
					num = num2;
					result = content;
				}
			}
			return result;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000E7E67 File Offset: 0x000E6267
		public Data SampleData(Vert vert)
		{
			return this.flowContents[(int)vert.index].data;
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000E7E80 File Offset: 0x000E6280
		public Data SampleData(NavPos navPos)
		{
			if (navPos.navigationMesh != this.navMesh)
			{
				return Data.empty;
			}
			return this.GetClosestFlowContent(navPos).data;
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000E7EB9 File Offset: 0x000E62B9
		public Vector3 SampleDirection(Vert vert)
		{
			return this.flowContents[(int)vert.index].direction;
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000E7ED4 File Offset: 0x000E62D4
		public void SampleFullData(NavPos navPos, ref float dist, ref Vector3 dir, ref Agent agent)
		{
			Data data = default(Data);
			this.SampleFullData(navPos, ref dist, ref dir, ref data);
			agent = data.agent;
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000E7F00 File Offset: 0x000E6300
		public void SampleFullData(NavPos navPos, ref float dist, ref Vector3 dir, ref Data data)
		{
			if (navPos.navigationMesh != this.navMesh)
			{
				dir = Vector3.zero;
				dist = 1000000f;
				data = Data.empty;
				return;
			}
			float num = dist;
			Vector3 vector = dir;
			dist = 0f;
			dir = Vector3.zero;
			Vector3 bary = navPos.bary;
			Tri tri = navPos.tri;
			Content content = this.flowContents[(int)tri.verts[0].index];
			if (content.distance < 6f)
			{
				content = this.GetClosestFlowContent(navPos);
				float num2 = 0f;
				for (int i = 0; i < 3; i++)
				{
					Content other = this.flowContents[(int)tri.verts[i].index];
					if (content.Comparable(other) && Vector3.Dot(content.direction, other.direction) > 0f)
					{
						float x = bary.x;
						bary = new Vector3(bary.y, bary.z, bary.x);
						if (other.occupied)
						{
							Vector3 a = other.data.navPos.pos - navPos.pos;
							float magnitude = a.magnitude;
							if (magnitude > 1f)
							{
								a /= magnitude;
							}
							dist += (magnitude + data.distance) * x;
							dir += a * x;
						}
						else
						{
							dist += other.distance * x;
							dir += other.direction * x;
						}
						num2 += x;
					}
				}
				dir /= num2;
				if (data.valid && data.navPos.tri == navPos.tri)
				{
					dist = Vector3.Distance(navPos.pos, data.navPos.pos) + data.distance;
				}
				else
				{
					dist /= num2;
				}
			}
			else
			{
				float num3 = float.MaxValue;
				for (int j = 0; j < 3; j++)
				{
					float x2 = bary.x;
					bary = new Vector3(bary.y, bary.z, bary.x);
					Content content2 = this.flowContents[(int)tri.verts[j].index];
					dist += content2.distance * x2;
					dir += content2.direction * x2;
					if (content2.distance < num3)
					{
						num3 = content2.distance;
						content = content2;
					}
				}
			}
			data = content.data;
			if ((int)tri.singleBorderIndex != -1)
			{
				float num4 = Vector3.Dot(dir, tri.singleBorder.borderVector);
				if (num4 < 0f)
				{
					float distance = this.flowContents[(int)tri.singleBorder.vert0.index].distance;
					float distance2 = this.flowContents[(int)tri.singleBorder.vert1.index].distance;
					Vector3 b = (distance <= distance2) ? (-tri.singleBorder.dir) : tri.singleBorder.dir;
					float num5 = 1f - bary.GetComponent((int)tri.singleBorderIndex);
					float num6 = -num4;
					dir = Vector3.Lerp(dir, b, num5 * num6);
				}
			}
			if (float.IsNaN(dist))
			{
				dist = num;
			}
			if (float.IsNaN(dir.x))
			{
				dir = vector;
			}
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000E8310 File Offset: 0x000E6710
		public Vector3 SampleDirection(NavPos navPos)
		{
			if (navPos.navigationMesh != this.navMesh)
			{
				return Vector3.zero;
			}
			Content closestFlowContent = this.GetClosestFlowContent(navPos);
			Vector3 vector = Vector3.zero;
			Vector3 bary = navPos.bary;
			Tri tri = navPos.tri;
			float num = 0f;
			for (int i = 0; i < 3; i++)
			{
				Content other = this.flowContents[(int)tri.verts[i].index];
				if (closestFlowContent.Comparable(other) && Vector3.Dot(closestFlowContent.direction, other.direction) > 0f)
				{
					float component = bary.GetComponent(i);
					if (other.occupied)
					{
						vector += (other.data.navPos.pos - navPos.pos).GetClampedMagnitude(1f) * component;
					}
					else
					{
						vector += other.direction * component;
					}
					num += component;
				}
			}
			vector /= num;
			if ((int)tri.singleBorderIndex != -1)
			{
				float num2 = Vector3.Dot(vector, tri.singleBorder.borderVector);
				if (num2 < 0f)
				{
					float distance = this.flowContents[(int)tri.singleBorder.vert0.index].distance;
					float distance2 = this.flowContents[(int)tri.singleBorder.vert1.index].distance;
					Vector3 b = (distance <= distance2) ? (-tri.singleBorder.dir) : tri.singleBorder.dir;
					float num3 = 1f - bary.GetComponent((int)tri.singleBorderIndex);
					float num4 = -num2;
					vector = Vector3.Lerp(vector, b, num3 * num4);
				}
			}
			if (float.IsNaN(vector.x))
			{
				vector = Vector3.zero;
			}
			return vector;
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000E8514 File Offset: 0x000E6914
		public float SampleDistance(Vert vert)
		{
			return this.flowContents[(int)vert.index].distance;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000E852C File Offset: 0x000E692C
		public float SampleDistance(NavPos navPos)
		{
			if (navPos.navigationMesh != this.navMesh)
			{
				return 1000000f;
			}
			Data data = this.SampleData(navPos);
			if (data.valid)
			{
				NavPos navPos2 = data.navPos;
				if (navPos.tri == navPos2.tri)
				{
					return Vector3.Distance(navPos.pos, navPos2.pos) + data.distance;
				}
			}
			float num = 0f;
			for (int i = 0; i < 3; i++)
			{
				num += this.SampleDistance(navPos.tri.verts[i]) * navPos.bary.GetComponent(i);
			}
			return num;
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000E85E3 File Offset: 0x000E69E3
		public float SampleAmount(Vert vert)
		{
			return this.flowContents[(int)vert.index].amount / 100000f;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000E8604 File Offset: 0x000E6A04
		public float SampleAmount(NavPos navPos)
		{
			if (navPos.navigationMesh != this.navMesh)
			{
				return 0f;
			}
			float num = 0f;
			Vector3 bary = navPos.bary;
			Tri tri = navPos.tri;
			for (int i = 0; i < 3; i++)
			{
				num += this.flowContents[(int)tri.verts[i].index].amount * bary.GetComponent(i);
			}
			if (float.IsNaN(num))
			{
				return 0f;
			}
			num /= 100000f;
			return Mathf.Clamp01(num);
		}

		// Token: 0x0400249E RID: 9374
		public const float scale = 100000f;

		// Token: 0x0400249F RID: 9375
		public const float maxDistance = 1000000f;

		// Token: 0x040024A0 RID: 9376
		public NavigationMesh navMesh;

		// Token: 0x040024A1 RID: 9377
		public Content[] flowContents;
	}
}
