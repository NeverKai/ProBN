using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200065D RID: 1629
	public class Edge
	{
		// Token: 0x06002967 RID: 10599 RVA: 0x00091358 File Offset: 0x0008F758
		public Edge(Vert v0, Vert v1)
		{
			this.verts[0] = v0;
			this.verts[1] = v1;
			Vector3 a = v1.pos - v0.pos;
			this.pos = (this.verts[0].pos + this.verts[1].pos) / 2f;
			this.length = a.magnitude;
			this.dir = a / this.length;
			this.cost = this.length;
			Vector2 vector = v1.xz - v0.xz;
			this.xzLength = vector.magnitude;
			this.xzDir = vector.normalized;
			this.pipes[0] = new Pipe(this, true);
			this.pipes[1] = new Pipe(this, false);
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x00091452 File Offset: 0x0008F852
		public Vert vert0
		{
			get
			{
				return this.verts.x;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x0009145F File Offset: 0x0008F85F
		public Vert vert1
		{
			get
			{
				return this.verts.y;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x0009146C File Offset: 0x0008F86C
		public Vector3 wPos
		{
			get
			{
				return this.tris[0].navigationMesh.transform.TransformPoint(this.pos);
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x0009148F File Offset: 0x0008F88F
		public bool beach
		{
			get
			{
				return this.cliff && this.pos.y < 0f;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000914B1 File Offset: 0x0008F8B1
		public bool wall
		{
			get
			{
				return this.border && !this.cliff;
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000914CA File Offset: 0x0008F8CA
		public void OnDestroy()
		{
			this.verts.SetNull();
			this.tris.SetNull();
			this.pipes.SetNull();
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000914ED File Offset: 0x0008F8ED
		public void Setup()
		{
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000914F0 File Offset: 0x0008F8F0
		public void Setup2()
		{
			if (this.border)
			{
				this.verts[0].border = true;
				this.verts[1].border = true;
				this.borderVector = Vector3.Cross(this.dir, -this.tris[0].up);
				if (this.tris[0].navigationMesh.island)
				{
					Vector3 normalized = this.borderVector.GetZeroY().normalized;
					Vector3 zeroY = this.tris[0].up.GetZeroY();
					if (Vector3.Dot(normalized, zeroY) < 0.1f)
					{
						Vector3 vector = this.pos + Vector3.up * 0.15f + normalized * 0.05f;
						Vector3 a = this.pos + Vector3.down * 0.03f - normalized * 0.3f;
						Vector3 direction = a - vector;
						Ray ray = new Ray(vector, direction);
						this.cliff = !Physics.Raycast(ray, direction.magnitude, LayerMaster.moduleMask);
					}
				}
				if (this.cliff)
				{
					this.verts[0].cliffVector += this.borderVector / 2f;
					this.verts[1].cliffVector += this.borderVector / 2f;
					this.verts[0].cliffyness += 0.5f;
					this.verts[1].cliffyness += 0.5f;
					this.verts[0].distanceToCliff = 0f;
					this.verts[1].distanceToCliff = 0f;
				}
				else
				{
					this.verts[0].wallVector += this.borderVector / 2f;
					this.verts[1].wallVector += this.borderVector / 2f;
					this.verts[0].distanceToWall = 0f;
					this.verts[1].distanceToWall = 0f;
				}
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0009178D File Offset: 0x0008FB8D
		public void DebugDraw(Color color)
		{
			Debug.DrawLine(this.verts[0].pos, this.verts[1].pos, color, 4f);
		}

		// Token: 0x04001AFF RID: 6911
		public ushort index;

		// Token: 0x04001B00 RID: 6912
		public StructArray2<Vert> verts;

		// Token: 0x04001B01 RID: 6913
		public StructArray2<Tri> tris;

		// Token: 0x04001B02 RID: 6914
		public Vector3 pos;

		// Token: 0x04001B03 RID: 6915
		public Vector3 dir;

		// Token: 0x04001B04 RID: 6916
		public float xzLength;

		// Token: 0x04001B05 RID: 6917
		public Vector2 xzDir;

		// Token: 0x04001B06 RID: 6918
		public bool border = true;

		// Token: 0x04001B07 RID: 6919
		public bool cliff;

		// Token: 0x04001B08 RID: 6920
		public Vector3 borderVector;

		// Token: 0x04001B09 RID: 6921
		public float length;

		// Token: 0x04001B0A RID: 6922
		public float cost;

		// Token: 0x04001B0B RID: 6923
		public StructArray2<Pipe> pipes;
	}
}
