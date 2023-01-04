using System;
using System.Collections.Generic;
using UnityEngine;

namespace MeshModifer
{
	// Token: 0x02000629 RID: 1577
	public class MeshBuilder
	{
		// Token: 0x06002879 RID: 10361 RVA: 0x00087283 File Offset: 0x00085683
		public MeshBuilder(Vector3 pos, bool flipped = false)
		{
			this.pos = pos;
			this.flipped = flipped;
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000872AF File Offset: 0x000856AF
		public void AddTriangle(Triangle triangle)
		{
			this.AddTriangle(triangle.verts);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000872C0 File Offset: 0x000856C0
		public void AddTriangle(Vertex[] inVerts)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = this.verts.IndexOf(inVerts[i]);
				if (num == -1)
				{
					this.triangles.Add(this.verts.Count);
					this.verts.Add(inVerts[i]);
				}
				else
				{
					this.triangles.Add(num);
				}
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0008733C File Offset: 0x0008573C
		public void GridSnapVerts(Matrix4x4 grid, float tolerance = 0.01f)
		{
			Matrix4x4 inverse = grid.inverse;
			for (int i = 0; i < this.verts.Count; i++)
			{
				Vertex value = this.verts[i];
				Vector3 vector = grid.MultiplyPoint(value.pos);
				Vector3 vector2 = ExtraMath.Round(vector);
				if (Mathf.Abs(vector2.x - vector.x) < tolerance)
				{
					vector.x = vector2.x;
				}
				if (Mathf.Abs(vector2.y - vector.y) < tolerance)
				{
					vector.y = vector2.y;
				}
				if (Mathf.Abs(vector2.z - vector.z) < tolerance)
				{
					vector.z = vector2.z;
				}
				value.pos = inverse.MultiplyPoint(vector);
				this.verts[i] = value;
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x00087428 File Offset: 0x00085828
		public void ApplyToMesh(Mesh mesh)
		{
			Vector3[] array = new Vector3[this.verts.Count];
			Vector3[] array2 = new Vector3[this.verts.Count];
			Vector4[] tangents = new Vector4[this.verts.Count];
			Vector2[] array3 = new Vector2[this.verts.Count];
			Color32[] array4 = new Color32[this.verts.Count];
			Vector3 b = this.pos;
			for (int i = 0; i < this.verts.Count; i++)
			{
				array[i] = this.verts[i].pos - b;
				array2[i] = this.verts[i].normal;
				array3[i] = this.verts[i].uv;
				array4[i] = this.verts[i].color;
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.tangents = tangents;
			mesh.colors32 = array4;
			mesh.uv = array3;
			mesh.triangles = this.triangles.ToArray();
			mesh.RecalculateBounds();
			if (this.flipped)
			{
				mesh.Invert();
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x00087594 File Offset: 0x00085994
		public Mesh GetMesh()
		{
			Mesh mesh = new Mesh();
			mesh.MarkDynamic();
			this.ApplyToMesh(mesh);
			return mesh;
		}

		// Token: 0x040019FA RID: 6650
		private List<Vertex> verts = new List<Vertex>();

		// Token: 0x040019FB RID: 6651
		private List<int> triangles = new List<int>();

		// Token: 0x040019FC RID: 6652
		private bool flipped;

		// Token: 0x040019FD RID: 6653
		private Vector3 pos;
	}
}
