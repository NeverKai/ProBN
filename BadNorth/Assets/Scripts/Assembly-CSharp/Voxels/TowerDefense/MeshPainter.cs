using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A0 RID: 1952
	public class MeshPainter : MonoBehaviour
	{
		// Token: 0x06003281 RID: 12929 RVA: 0x000D6528 File Offset: 0x000D4928
		private void Setup()
		{
			this.mesh = UnityEngine.Object.Instantiate<Mesh>(this.target.sharedMesh);
			this.target.sharedMesh = this.mesh;
			this.verts = this.mesh.vertices;
			this.normals = this.mesh.normals;
			this.colors = this.mesh.colors32;
			if (this.verts.Length != this.colors.Length)
			{
				this.colors = new Color32[this.verts.Length];
			}
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000D65B8 File Offset: 0x000D49B8
		public void Paint(Vector3 worldPos)
		{
			if (!this.mesh)
			{
				this.Setup();
			}
			Vector3 vector = this.target.transform.InverseTransformPoint(worldPos);
			float num = 0.5f;
			float num2 = num * num;
			for (int i = 0; i < this.verts.Length; i++)
			{
				Vector3 vector2 = this.verts[i];
				float sqrMagnitude = (vector2 - vector).sqrMagnitude;
				if (sqrMagnitude <= num2)
				{
					float num3 = 1f - Mathf.Sqrt(sqrMagnitude) / num;
					num3 *= Mathf.Clamp01(Vector3.Dot((vector - vector2).normalized, this.normals[i]) + 1f);
					Color c = this.colors[i];
					c.r = Mathf.Lerp(c.r, 1f, num3);
					this.colors[i] = c;
				}
			}
			this.mesh.colors32 = this.colors;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000D66E6 File Offset: 0x000D4AE6
		private void OnDestroy()
		{
			if (this.mesh)
			{
				UnityEngine.Object.Destroy(this.mesh);
			}
		}

		// Token: 0x04002253 RID: 8787
		public MeshFilter target;

		// Token: 0x04002254 RID: 8788
		private Mesh mesh;

		// Token: 0x04002255 RID: 8789
		private Vector3[] verts;

		// Token: 0x04002256 RID: 8790
		private Vector3[] normals;

		// Token: 0x04002257 RID: 8791
		private Color32[] colors;
	}
}
