using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000503 RID: 1283
	public class Wake : MonoBehaviour
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x000595D4 File Offset: 0x000579D4
		private int v0
		{
			get
			{
				return this.index * 2;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x000595DE File Offset: 0x000579DE
		private int v1
		{
			get
			{
				return this.index * 2 + 1;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x000595EA File Offset: 0x000579EA
		private int v2
		{
			get
			{
				return (this.index * 2 + 2) % 200;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x000595FC File Offset: 0x000579FC
		private int v3
		{
			get
			{
				return (this.index * 2 + 3) % 200;
			}
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00059610 File Offset: 0x00057A10
		private void Start()
		{
			this.lastDrop = base.transform.position;
			this.lastDropTime = Time.time;
			this.mesh = base.GetComponentInParent<Island>().meshPool.GetMesh();
			this.mesh.vertices = this.vertices;
			this.mesh.normals = this.normals;
			this.mesh.uv = this.uvs;
			this.mesh.uv2 = this.uv2s;
			this.mesh.uv3 = this.uv3s;
			this.mesh.uv4 = this.uv4s;
			base.GetComponent<MeshFilter>().sharedMesh = this.mesh;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000596C8 File Offset: 0x00057AC8
		private void LateUpdate()
		{
			if (Vector3.SqrMagnitude(this.lastDrop - base.transform.position) > this.minDist * this.minDist)
			{
				this.lastDrop = base.transform.position;
				this.lastDropTime = Time.time;
				this.index++;
				if (this.index >= 100)
				{
					this.index = 0;
				}
				int num = this.index * 6 + this.triangles.Length;
				this.triangles[num % this.triangles.Length] = this.v1;
				this.triangles[(num - 5) % this.triangles.Length] = this.v1;
				this.triangles[(num - 2) % this.triangles.Length] = this.v1;
				this.triangles[(num + 2) % this.triangles.Length] = this.v0;
				this.triangles[(num + 3) % this.triangles.Length] = this.v0;
				this.triangles[(num - 1) % this.triangles.Length] = this.v0;
				this.uv2s[this.v0] = new Vector2(this.radius, 0f);
				this.uv2s[this.v1] = new Vector2(this.radius, 0f);
				this.mesh.triangles = this.triangles;
				this.mesh.uv2 = this.uv2s;
			}
			else if (Time.time - this.lastDropTime > this.maxAge)
			{
				base.gameObject.SetActive(false);
			}
			Vector2 xz = base.transform.right.GetXZ();
			this.uv3s[this.v0] = base.transform.position.GetXZ();
			this.uv3s[this.v1] = base.transform.position.GetXZ();
			this.uv4s[this.v0] = xz;
			this.uv4s[this.v1] = -xz;
			this.uvs[this.v0] = new Vector2(1f, Time.timeSinceLevelLoad);
			this.uvs[this.v1] = new Vector2(-1f, Time.timeSinceLevelLoad);
			this.mesh.normals = this.normals;
			this.mesh.uv = this.uvs;
			this.mesh.uv3 = this.uv3s;
			this.mesh.uv4 = this.uv4s;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0005999C File Offset: 0x00057D9C
		private void OnDestroy()
		{
			if (this.mesh)
			{
				Island componentInParentIncludingInactive = base.gameObject.GetComponentInParentIncludingInactive<Island>();
				if (componentInParentIncludingInactive && componentInParentIncludingInactive.meshPool)
				{
					componentInParentIncludingInactive.meshPool.ReturnMesh(ref this.mesh);
				}
				else
				{
					UnityEngine.Object.Destroy(this.mesh);
				}
			}
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00059A01 File Offset: 0x00057E01
		private void OnDrawGizmos()
		{
			ExtraGizmos.DrawCircle(base.transform.position, this.radius, 8);
		}

		// Token: 0x0400146E RID: 5230
		private Mesh mesh;

		// Token: 0x0400146F RID: 5231
		public float radius = 1f;

		// Token: 0x04001470 RID: 5232
		private const int maxCount = 100;

		// Token: 0x04001471 RID: 5233
		private float minDist = 0.2f;

		// Token: 0x04001472 RID: 5234
		public float maxAge = 20f;

		// Token: 0x04001473 RID: 5235
		private int index;

		// Token: 0x04001474 RID: 5236
		private Vector3[] vertices = new Vector3[200];

		// Token: 0x04001475 RID: 5237
		private Vector3[] normals = new Vector3[200];

		// Token: 0x04001476 RID: 5238
		private Vector2[] uvs = new Vector2[200];

		// Token: 0x04001477 RID: 5239
		private Vector2[] uv2s = new Vector2[200];

		// Token: 0x04001478 RID: 5240
		private Vector2[] uv3s = new Vector2[200];

		// Token: 0x04001479 RID: 5241
		private Vector2[] uv4s = new Vector2[200];

		// Token: 0x0400147A RID: 5242
		private int[] triangles = new int[600];

		// Token: 0x0400147B RID: 5243
		private Vector3 lastDrop;

		// Token: 0x0400147C RID: 5244
		private float lastDropTime;
	}
}
