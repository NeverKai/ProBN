using System;
using UnityEngine;

namespace MeshModifer
{
	// Token: 0x02000627 RID: 1575
	public struct Vertex
	{
		// Token: 0x0600286C RID: 10348 RVA: 0x00086F20 File Offset: 0x00085320
		public Vertex(Vector3 pos)
		{
			this.pos = pos;
			this.normal = Vector3.zero;
			this.tangent = Vector3.zero;
			this.uv = Vector2.zero;
			this.uv2 = Vector2.zero;
			this.color = Color.white;
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x00086F78 File Offset: 0x00085378
		public Vertex(Vector3 pos, Vector2 uv)
		{
			this.pos = pos;
			this.normal = Vector3.zero;
			this.tangent = Vector3.zero;
			this.uv = uv;
			this.uv2 = Vector2.zero;
			this.color = Color.white;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x00086FCC File Offset: 0x000853CC
		public Vertex(Vector3 pos, Vector2 uv, Vector2 uv2)
		{
			this.pos = pos;
			this.normal = Vector3.zero;
			this.tangent = Vector3.zero;
			this.uv = uv;
			this.uv2 = uv2;
			this.color = Color.white;
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0008701C File Offset: 0x0008541C
		public Vertex(Vector3 pos, Vector3 normal, Vector3 uv, Color color)
		{
			this.pos = pos;
			this.normal = normal;
			this.tangent = Vector3.zero;
			this.uv = uv;
			this.uv2 = Vector2.zero;
			this.color = color;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x0008706C File Offset: 0x0008546C
		public Vertex(Vector3 pos, Vector3 normal, Vector3 tangent, Vector3 uv, Color color)
		{
			this.pos = pos;
			this.normal = normal;
			this.tangent = tangent;
			this.uv = uv;
			this.uv2 = Vector2.zero;
			this.color = color;
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000870B8 File Offset: 0x000854B8
		public static Vertex Transform(Matrix4x4 matrix, Vertex vertex)
		{
			vertex.pos = matrix.MultiplyPoint(vertex.pos);
			vertex.normal = matrix.MultiplyVector(vertex.normal).normalized;
			return vertex;
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000870F8 File Offset: 0x000854F8
		public void Transform(Matrix4x4 matrix)
		{
			this.pos = matrix.MultiplyPoint(this.pos);
			this.normal = matrix.MultiplyVector(this.normal);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00087120 File Offset: 0x00085520
		public override bool Equals(object obj)
		{
			return obj is Vertex && this == (Vertex)obj;
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00087144 File Offset: 0x00085544
		public override int GetHashCode()
		{
			return this.pos.GetHashCode() + this.normal.GetHashCode() + this.uv.GetHashCode() + this.color.GetHashCode();
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00087198 File Offset: 0x00085598
		public static bool operator ==(Vertex x, Vertex y)
		{
			return x.pos == y.pos && x.normal == y.normal && x.uv == y.uv && x.color == y.color;
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0008720D File Offset: 0x0008560D
		public static bool operator !=(Vertex x, Vertex y)
		{
			return !(x == y);
		}

		// Token: 0x040019F3 RID: 6643
		public Vector3 pos;

		// Token: 0x040019F4 RID: 6644
		public Vector3 normal;

		// Token: 0x040019F5 RID: 6645
		public Vector4 tangent;

		// Token: 0x040019F6 RID: 6646
		public Vector2 uv;

		// Token: 0x040019F7 RID: 6647
		public Vector2 uv2;

		// Token: 0x040019F8 RID: 6648
		public Color32 color;
	}
}
