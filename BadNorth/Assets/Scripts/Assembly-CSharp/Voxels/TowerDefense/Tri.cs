using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200065B RID: 1627
	public class Tri
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x00090032 File Offset: 0x0008E432
		public Edge singleBorder
		{
			get
			{
				return ((int)this.singleBorderIndex != -1) ? this.edges[(int)this.singleBorderIndex] : null;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600293D RID: 10557 RVA: 0x00090059 File Offset: 0x0008E459
		public NavPos navPos
		{
			get
			{
				return new NavPos(this, this.pos);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x00090067 File Offset: 0x0008E467
		public Vector3 wPos
		{
			get
			{
				return this.navigationMesh.transform.TransformPoint(this.pos);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x0009007F File Offset: 0x0008E47F
		public Vector3 up
		{
			get
			{
				return this.plane.normal;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0009008C File Offset: 0x0008E48C
		public float area
		{
			get
			{
				return this.acrossEdgeLength[0] * this.edges[0].length / 2f;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x000900B4 File Offset: 0x0008E4B4
		public int borderCount
		{
			get
			{
				return ((!this.verts[0].border) ? 0 : 1) + ((!this.verts[1].border) ? 0 : 1) + ((!this.verts[2].border) ? 0 : 1);
			}
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0009011A File Offset: 0x0008E51A
		public void OnDestroy()
		{
			this.navigationMesh = null;
			this.verts.SetNull();
			this.edges.SetNull();
			this.tris.SetNull();
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x00090144 File Offset: 0x0008E544
		public Vector3 GetXYZ(Vector2 xz)
		{
			return new Vector3(xz.x, this.GetY(xz), xz.y);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00090160 File Offset: 0x0008E560
		public float GetY(Vector2 xz)
		{
			return xz.x * this.coefficient.x + xz.y * this.coefficient.y + this.constant;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x00090190 File Offset: 0x0008E590
		public float GetY(Vector3 pos)
		{
			return pos.x * this.coefficient.x + pos.z * this.coefficient.y + this.constant;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000901C0 File Offset: 0x0008E5C0
		public Vector3 WorldToBary(Vector2 xz)
		{
			Vector3 xztoXYZ = xz.GetXZtoXYZ();
			Vector3 zero = Vector3.zero;
			zero.x = Vector3.Dot(this.acrossEdgeInverse[0], xztoXYZ - this.edges[0].pos);
			zero.y = Vector3.Dot(this.acrossEdgeInverse[1], xztoXYZ - this.edges[1].pos);
			zero.z = Vector3.Dot(this.acrossEdgeInverse[2], xztoXYZ - this.edges[2].pos);
			return zero;
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x00090268 File Offset: 0x0008E668
		public Vector3 WorldToBary(Vector3 worldPos)
		{
			Vector3 zero = Vector3.zero;
			zero.x = Vector3.Dot(this.acrossEdgeInverse[0], worldPos - this.edges[0].pos);
			zero.y = Vector3.Dot(this.acrossEdgeInverse[1], worldPos - this.edges[1].pos);
			zero.z = Vector3.Dot(this.acrossEdgeInverse[2], worldPos - this.edges[2].pos);
			return zero;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0009030C File Offset: 0x0008E70C
		public Vector3 BaryToWorld(Vector3 bary)
		{
			return this.verts[0].pos * bary.x + this.verts[1].pos * bary.y + this.verts[2].pos * bary.z;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0009037C File Offset: 0x0008E77C
		public Vector2 ClampXZ(Vector2 xz)
		{
			int num;
			Edge edge;
			Vert vert;
			return this.ClampXZ(xz, out num, out edge, out vert);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x00090398 File Offset: 0x0008E798
		public Vector2 ClampXZ(Vector2 xz, out int edgeIndex, out Edge clampingEdge, out Vert clampingVert)
		{
			for (int i = 0; i < 3; i++)
			{
				Edge edge = this.edges[i];
				Vector2 rhs = xz - edge.verts[0].xz;
				Vector2 xz2 = this.acrossEdge[i].GetXZ();
				float num = Vector2.Dot(xz2, rhs);
				if (num < 0f)
				{
					clampingEdge = edge;
					edgeIndex = i;
					float num2 = Vector2.Dot(edge.xzDir, rhs);
					Vector2 result;
					if (num2 <= 0f)
					{
						clampingVert = edge.verts[0];
						result = clampingVert.xz;
						Color color = Color.cyan;
					}
					else if (num2 >= edge.xzLength)
					{
						clampingVert = edge.verts[1];
						result = clampingVert.xz;
						Color color = Color.magenta;
					}
					else
					{
						clampingVert = null;
						Color color = Color.blue;
						result = xz - xz2 * num;
					}
					return result;
				}
			}
			clampingEdge = null;
			clampingVert = null;
			edgeIndex = -1;
			return xz;
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000904A4 File Offset: 0x0008E8A4
		public Vector3 ClampWorldPos(Vector3 worldPos)
		{
			Vector2 xz = new Vector2(worldPos.x, worldPos.z);
			xz = this.ClampXZ(xz);
			worldPos = this.ClampToPlane(xz);
			return worldPos;
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000904D8 File Offset: 0x0008E8D8
		public Vector3 ClampToPlane(Vector2 xz)
		{
			return new Vector3(xz.x, this.GetY(xz), xz.y);
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000904F4 File Offset: 0x0008E8F4
		public Vector3 ClampToPlane(Vector3 pos)
		{
			pos.y = this.GetY(pos);
			return pos;
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x00090508 File Offset: 0x0008E908
		public int GetBorderIndex(Vector3 worldPos)
		{
			if (Vector3.Dot(this.acrossEdgeInverse[0], worldPos - this.verts[0].pos) < 0f)
			{
				return 0;
			}
			if (Vector3.Dot(this.acrossEdgeInverse[1], worldPos - this.verts[1].pos) < 0f)
			{
				return 1;
			}
			if (Vector3.Dot(this.acrossEdgeInverse[2], worldPos - this.verts[2].pos) < 0f)
			{
				return 2;
			}
			return -1;
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x000905B4 File Offset: 0x0008E9B4
		public bool ContainsXZ(Vector2 xz)
		{
			return Vector2.Dot(this.acrossEdge[0].GetXZ(), xz - this.edges[0].pos.GetXZ()) >= 0f && Vector2.Dot(this.acrossEdge[1].GetXZ(), xz - this.edges[1].pos.GetXZ()) >= 0f && Vector2.Dot(this.acrossEdge[2].GetXZ(), xz - this.edges[2].pos.GetXZ()) >= 0f;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x0009067C File Offset: 0x0008EA7C
		public bool ContainsWorldPos(Vector3 worldPos)
		{
			return this.ContainsXZ(worldPos.GetXZ());
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0009068C File Offset: 0x0008EA8C
		public void Setup()
		{
			Vector3 normalized = Vector3.Cross(this.verts[1].pos - this.verts[0].pos, this.verts[2].pos - this.verts[0].pos).normalized;
			this.plane = new Plane(normalized, this.verts[1].pos);
			this.plane.Raycast(new Ray(Vector3.zero, Vector3.up), out this.constant);
			this.plane.Raycast(new Ray(new Vector3(1f, 0f, 0f), Vector3.up), out this.coefficient.x);
			this.plane.Raycast(new Ray(new Vector3(0f, 0f, 1f), Vector3.up), out this.coefficient.y);
			this.coefficient.x = this.coefficient.x - this.constant;
			this.coefficient.y = this.coefficient.y - this.constant;
			for (int i = 0; i < 3; i++)
			{
				this.tris[i] = ((this.edges[i].tris[0] != this) ? this.edges[i].tris[0] : this.edges[i].tris[1]);
			}
			for (int j = 0; j < 3; j++)
			{
				if (this.edges[j].border)
				{
					this.singleBorderIndex = (sbyte)j;
					break;
				}
			}
			for (int k = 0; k < 3; k++)
			{
				this.alongEdge[k] = this.verts[(k + 1) % 3].pos - this.verts[(k + 2) % 3].pos;
				Vector3 normalized2 = Vector3.Cross(this.alongEdge[k], Vector3.up).normalized;
				this.acrossEdgeLength[k] = Vector3.Dot(normalized2, this.verts[k].pos - this.edges[k].pos);
				this.acrossEdge[k] = normalized2;
				this.acrossEdgeInverse[k] = normalized2 / this.acrossEdgeLength[k];
			}
			float num = 0f;
			this.pos = Vector3.zero;
			for (int l = 0; l < 3; l++)
			{
				num += this.edges[l].length;
				this.pos += this.edges[l].pos * this.edges[l].length;
			}
			this.pos /= num;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000909DF File Offset: 0x0008EDDF
		private int GetEdgeIndex(float min, Vector3 bary)
		{
			if (min == bary.x)
			{
				return 0;
			}
			if (min == bary.y)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x00090A00 File Offset: 0x0008EE00
		public bool TriCast(Tri targetTri)
		{
			return targetTri == this || this.tris.Contains(this) || this.TriCast(targetTri.pos, targetTri);
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x00090A2C File Offset: 0x0008EE2C
		public bool TriCast(Vector3 target, Tri endTri)
		{
			bool result;
			using (new ScopedProfiler("TriCast Pos", null))
			{
				result = Tri.TriCastXZ(this, this.pos.GetXZ(), target.GetXZ(), endTri);
			}
			return result;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x00090A84 File Offset: 0x0008EE84
		public static bool TriCastXZ(Tri tri, Vector2 startXZ, Vector2 endXZ, Tri endTri)
		{
			bool result;
			using (new ScopedProfiler("TriCast Tri", null))
			{
				if (tri == null)
				{
					result = false;
				}
				else if (endTri == tri)
				{
					result = true;
				}
				else
				{
					Edge edge = null;
					endXZ = Vector2.Lerp(endXZ, endTri.pos.GetXZ(), 0.001f);
					for (int i = 0; i < 100; i++)
					{
						startXZ = Vector2.Lerp(startXZ, tri.pos.GetXZ(), 0.001f);
						bool flag = false;
						for (int j = 0; j < 3; j++)
						{
							Edge edge2 = tri.edges[j];
							if (edge2 != edge)
							{
								Vector2 vector = Vector3.zero;
								if (ExtraMath.LineIntersection(startXZ, endXZ, edge2.vert0.pos.GetXZ(), edge2.vert1.pos.GetXZ(), ref vector))
								{
									if (Vector2.SqrMagnitude(vector - endXZ) < 0.001f && Mathf.Abs(tri.GetY(vector) - endTri.GetY(vector)) < 0.1f)
									{
										return true;
									}
									edge = edge2;
									tri = tri.tris[j];
									if (tri == null)
									{
										return false;
									}
									if (endTri == tri)
									{
										return true;
									}
									startXZ = vector;
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							return false;
						}
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04001AE4 RID: 6884
		public NavigationMesh navigationMesh;

		// Token: 0x04001AE5 RID: 6885
		public ushort index;

		// Token: 0x04001AE6 RID: 6886
		public StructArray3<Vert> verts;

		// Token: 0x04001AE7 RID: 6887
		public StructArray3<Edge> edges;

		// Token: 0x04001AE8 RID: 6888
		public sbyte singleBorderIndex = -1;

		// Token: 0x04001AE9 RID: 6889
		public StructArray3<Tri> tris;

		// Token: 0x04001AEA RID: 6890
		public Vector3 pos;

		// Token: 0x04001AEB RID: 6891
		public StructArray3<Vector3> alongEdge;

		// Token: 0x04001AEC RID: 6892
		public StructArray3<Vector3> acrossEdge;

		// Token: 0x04001AED RID: 6893
		public StructArray3<Vector3> acrossEdgeInverse;

		// Token: 0x04001AEE RID: 6894
		public StructArray3<float> acrossEdgeLength;

		// Token: 0x04001AEF RID: 6895
		public Plane plane;

		// Token: 0x04001AF0 RID: 6896
		private Vector2 coefficient;

		// Token: 0x04001AF1 RID: 6897
		private float constant;
	}
}
