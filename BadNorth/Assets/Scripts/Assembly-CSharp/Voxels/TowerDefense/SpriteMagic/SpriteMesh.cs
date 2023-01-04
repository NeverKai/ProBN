using System;
using UnityEngine;

namespace Voxels.TowerDefense.SpriteMagic
{
	// Token: 0x020007D5 RID: 2005
	public class SpriteMesh
	{
		// Token: 0x060033FD RID: 13309 RVA: 0x000E0364 File Offset: 0x000DE764
		public SpriteMesh(int count, SpriteBoundsPool boundsPool)
		{
			this.mesh = new Mesh();
			this.Resize(count);
			this.boundsPool = boundsPool;
			this.mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 20f);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000E03B4 File Offset: 0x000DE7B4
		public SpriteMesh(int count)
		{
			this.mesh = new Mesh();
			this.Resize(count);
			this.boundsPool = new SpriteBoundsPool();
			this.mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 20f);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000E0408 File Offset: 0x000DE808
		public static int[] GetTriangleArray(int count)
		{
			int[] array = new int[count * 6];
			for (int i = 0; i < count; i++)
			{
				int num = i * 6;
				int num2 = i * 4;
				array[num] = num2;
				array[num + 1] = num2 + 1;
				array[num + 2] = num2 + 2;
				array[num + 3] = num2 + 2;
				array[num + 4] = num2 + 1;
				array[num + 5] = num2 + 3;
			}
			return array;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000E0468 File Offset: 0x000DE868
		public void Resize(int count)
		{
			if (this.count == count && this.vert != null)
			{
				for (int i = 0; i < this.vert.Length; i++)
				{
					this.vert[i] = Vector3.zero;
					this.uv2[i] = Vector2.zero;
				}
				this.mesh.Clear();
			}
			else
			{
				this.count = count;
				int num = count * 4;
				this.tris = SpriteMesh.GetTriangleArray(count);
				this.vert = new Vector3[num];
				this.norm = new Vector3[num];
				this.tan = new Vector4[num];
				this.uv = new Vector2[num];
				this.uv2 = new Vector2[num];
				this.col = new Color32[num];
				this.mesh.Clear();
				this.mesh.vertices = this.vert;
				this.mesh.triangles = this.tris;
			}
			this.mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 20f);
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000E0594 File Offset: 0x000DE994
		public void Apply()
		{
			this.mesh.vertices = this.vert;
			this.mesh.normals = this.norm;
			this.mesh.tangents = this.tan;
			this.mesh.uv = this.uv;
			this.mesh.uv2 = this.uv2;
			this.mesh.colors32 = this.col;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000E0608 File Offset: 0x000DEA08
		public void SetVerts(int index, SpriteStampDef stamp)
		{
			using (new ScopedProfiler("SetVerts(int, SpriteBounds, NavPos)", null))
			{
				SpriteBounds spriteBoundsStatic = SpriteBoundsPool.GetSpriteBoundsStatic(stamp.sprite);
				int num = index * 4;
				int num2 = num;
				int num3 = num + 1;
				int num4 = num + 2;
				int num5 = num + 3;
				float x = (!stamp.flipX) ? spriteBoundsStatic.uv.xMin : spriteBoundsStatic.uv.xMax;
				float y = (!stamp.flipY) ? spriteBoundsStatic.uv.yMin : spriteBoundsStatic.uv.yMax;
				float x2 = (!stamp.flipX) ? spriteBoundsStatic.uv.xMax : spriteBoundsStatic.uv.xMin;
				float y2 = (!stamp.flipY) ? spriteBoundsStatic.uv.yMax : spriteBoundsStatic.uv.yMin;
				this.uv[num2] = new Vector2(x, y);
				this.uv[num3] = new Vector2(x2, y);
				this.uv[num4] = new Vector2(x, y2);
				this.uv[num5] = new Vector2(x2, y2);
				if (stamp.navPos.valid)
				{
					Vector4 column = stamp.matrix.GetColumn(2);
					Color32 color = stamp.color.SetA(stamp.navPos.GetGrass());
					Vector3 lhs = Vector3.Normalize(stamp.matrix.GetColumn(1));
					float num6 = Vector3.Dot(lhs, stamp.navPos.pos);
					for (int i = 0; i < 4; i++)
					{
						Vector3 vector = spriteBoundsStatic.GetUv2Corner(i);
						vector = stamp.matrix.MultiplyPoint(vector);
						NavPos navPos = stamp.navPos;
						navPos.pos = vector;
						vector = stamp.navPos.tri.ClampToPlane(vector.GetXZ());
						vector = Vector3.MoveTowards(navPos.pos, vector, 0.1f);
						int num7 = num + i;
						float num8 = Vector3.Dot(lhs, stamp.navPos.pos);
						num8 = Mathf.Max(num6, num8);
						column.w = num6 - num8 + 0.1f;
						this.vert[num7] = vector;
						this.norm[num7] = navPos.GetNormal();
						this.uv2[num7] = Vector2.zero;
						this.col[num7] = color;
						this.tan[num7] = column;
					}
				}
				else
				{
					Rect v = spriteBoundsStatic.v;
					float num9 = Vector3.Magnitude(stamp.matrix.GetColumn(0));
					float num10 = Vector3.Magnitude(stamp.matrix.GetColumn(1));
					this.uv2[num2] = new Vector2(v.min.x * num9, v.min.y * num10);
					this.uv2[num3] = new Vector2(v.max.x * num9, v.min.y * num10);
					this.uv2[num4] = new Vector2(v.min.x * num9, v.max.y * num10);
					this.uv2[num5] = new Vector2(v.max.x * num9, v.max.y * num10);
					Vector3 vector2 = stamp.matrix.GetColumn(3);
					Vector3 vector3 = stamp.matrix.GetColumn(0).normalized;
					Vector4 normalized = stamp.matrix.GetColumn(1).normalized;
					normalized.w = spriteBoundsStatic.v.width / 2f - spriteBoundsStatic.v.center.y;
					Color32 color2 = stamp.color;
					for (int j = num; j < num + 4; j++)
					{
						this.vert[j] = vector2;
						this.norm[j] = vector3;
						this.tan[j] = normalized;
						this.col[j] = color2;
					}
				}
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000E0B34 File Offset: 0x000DEF34
		public void ClearVerts()
		{
			for (int i = 0; i < this.vert.Length; i++)
			{
				this.vert[i] = Vector3.zero;
				this.uv2[i] = Vector3.zero;
			}
			this.mesh.vertices = this.vert;
			this.mesh.uv2 = this.uv2;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000E0BB0 File Offset: 0x000DEFB0
		public void ClearVerts(int index)
		{
			this.vert[index * 4] = Vector3.zero;
			this.vert[index * 4 + 1] = Vector3.zero;
			this.vert[index * 4 + 2] = Vector3.zero;
			this.vert[index * 4 + 3] = Vector3.zero;
			this.uv2[index * 4] = Vector2.zero;
			this.uv2[index * 4 + 1] = Vector2.zero;
			this.uv2[index * 4 + 2] = Vector2.zero;
			this.uv2[index * 4 + 3] = Vector2.zero;
		}

		// Token: 0x0400236A RID: 9066
		private int[] tris;

		// Token: 0x0400236B RID: 9067
		private Vector3[] vert;

		// Token: 0x0400236C RID: 9068
		private Vector3[] norm;

		// Token: 0x0400236D RID: 9069
		private Vector4[] tan;

		// Token: 0x0400236E RID: 9070
		private Vector2[] uv;

		// Token: 0x0400236F RID: 9071
		private Vector2[] uv2;

		// Token: 0x04002370 RID: 9072
		private Color32[] col;

		// Token: 0x04002371 RID: 9073
		public int count;

		// Token: 0x04002372 RID: 9074
		private SpriteBoundsPool boundsPool;

		// Token: 0x04002373 RID: 9075
		public Mesh mesh;
	}
}
