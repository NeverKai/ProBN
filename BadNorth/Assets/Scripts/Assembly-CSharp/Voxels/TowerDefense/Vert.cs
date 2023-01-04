using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200065C RID: 1628
	public class Vert
	{
		// Token: 0x06002956 RID: 10582 RVA: 0x00090C28 File Offset: 0x0008F028
		public Vert(Vector3 pos)
		{
			this.pos = pos;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x00090C9A File Offset: 0x0008F09A
		public Vector2 xz
		{
			get
			{
				return this.pos.GetXZ();
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x00090CA7 File Offset: 0x0008F0A7
		public Vector3 wPos
		{
			get
			{
				return this.tris[0].navigationMesh.transform.TransformPoint(this.pos);
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x00090CCA File Offset: 0x0008F0CA
		public Vector3 borderVector
		{
			get
			{
				return this.cliffVector + this.wallVector;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x00090CDD File Offset: 0x0008F0DD
		public float walliness
		{
			get
			{
				return (!this.border) ? 0f : (1f - this.cliffyness);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x00090D00 File Offset: 0x0008F100
		public NavigationMesh navMesh
		{
			get
			{
				return this.tris[0].navigationMesh;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x00090D13 File Offset: 0x0008F113
		public float distanceToBorder
		{
			get
			{
				return Mathf.Min(this.distanceToCliff, this.distanceToWall);
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x00090D26 File Offset: 0x0008F126
		public NavPos navPos
		{
			get
			{
				return new NavPos(this);
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00090D2E File Offset: 0x0008F12E
		public void OnDestroy()
		{
			this.tris = null;
			this.pipes = null;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x00090D40 File Offset: 0x0008F140
		public float cellArea
		{
			get
			{
				float num = 0f;
				for (int i = 0; i < this.tris.Count; i++)
				{
					num += this.tris[i].area;
				}
				return num / 3f;
			}
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00090D8C File Offset: 0x0008F18C
		public bool Setup2()
		{
			Pipe startPipe = this.pipes.FirstOrDefault((Pipe x) => x.forward && x.edge.border);
			if (startPipe == null)
			{
				startPipe = this.pipes[0];
			}
			int count = this.pipes.Count;
			if (count == 0)
			{
				return false;
			}
			for (int i = 0; i < count - 1; i++)
			{
				this.pipes.Remove(startPipe);
				this.pipes.Insert(i, startPipe);
				this.tris.Remove(startPipe.tri0);
				this.tris.Insert(i, startPipe.tri0);
				startPipe = this.pipes.FirstOrDefault((Pipe p) => p.tri1 == startPipe.tri0);
				if (startPipe == null)
				{
					return false;
				}
			}
			for (int j = 0; j < this.tris.Count; j++)
			{
				this.normal += this.tris[j].plane.normal * Vector3.Angle(this.pipes[j].dir, this.pipes[(j + 1) % count].dir);
			}
			this.normal.Normalize();
			for (int k = 0; k < count; k++)
			{
				this.slope += this.pipes[k].dir * -this.pipes[k].dir.y;
			}
			this.slope /= (float)count;
			int l = 0;
			int num = (!this.border) ? count : (count - 1);
			while (l < num)
			{
				Pipe pipe = this.pipes[l];
				Pipe pipe2 = this.pipes[(l + 1) % count];
				float num2 = Vector3.Angle(pipe.dir, pipe2.dir);
				pipe.weight += num2;
				pipe2.weight += num2;
				l++;
			}
			if (this.border)
			{
				this.pipes[0].weight *= 2f;
				this.pipes[this.pipes.Count - 1].weight *= 2f;
			}
			float num3 = 0f;
			for (int m = 0; m < count; m++)
			{
				num3 += this.pipes[m].weight;
			}
			for (int n = 0; n < count; n++)
			{
				this.pipes[n].weight /= num3;
			}
			return true;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000910B4 File Offset: 0x0008F4B4
		public NavPos GetCellPos(float rotation, float length)
		{
			length = Mathf.Clamp01(length);
			if (rotation <= 0f)
			{
				Pipe pipe = this.pipes[0];
				return new NavPos(pipe.tri0, Vector3.Lerp(this.pos, pipe.outVert.pos, length / 2f));
			}
			if (rotation >= 1f)
			{
				Pipe pipe2 = this.pipes[(!this.border) ? 0 : (this.pipes.Count - 1)];
				return new NavPos(pipe2.tri1, Vector3.Lerp(this.pos, pipe2.outVert.pos, length / 2f));
			}
			rotation *= (float)this.tris.Count;
			int num = (int)rotation;
			int num2 = num;
			float num3 = rotation * 2f % 1f;
			if (rotation % 1f > 0.5f)
			{
				num3 = 1f - num3;
				num2 = (num2 + 1) % this.pipes.Count;
			}
			Vector3 a = (this.pipes[num2].outVert.pos + this.pos) / 2f;
			Vector3 vector = Vector3.Lerp(a, this.tris[num].pos, num3);
			vector = Vector3.Lerp(this.pos, vector, length);
			return new NavPos(this.tris[num], vector);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x00091224 File Offset: 0x0008F624
		public void Setup()
		{
			float num = 0f;
			for (int i = 0; i < this.pipes.Count; i++)
			{
				num += 1f / this.pipes[i].edge.cost;
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00091272 File Offset: 0x0008F672
		public bool TriCast(Vector3 target)
		{
			return Tri.TriCastXZ(this.tris[0], this.pos.GetXZ(), target.GetXZ(), null);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00091298 File Offset: 0x0008F698
		public bool Adjecent(NavPos navPos)
		{
			for (int i = 0; i < this.tris.Count; i++)
			{
				if (this.tris[i] == navPos.tri)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000912DC File Offset: 0x0008F6DC
		public bool Adjecent(Vert vert)
		{
			for (int i = 0; i < this.pipes.Count; i++)
			{
				if (this.pipes[i].outVert == vert)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001AF2 RID: 6898
		public ushort index;

		// Token: 0x04001AF3 RID: 6899
		public List<Tri> tris = new List<Tri>();

		// Token: 0x04001AF4 RID: 6900
		public Vector3 pos;

		// Token: 0x04001AF5 RID: 6901
		public Vector3 normal = Vector3.zero;

		// Token: 0x04001AF6 RID: 6902
		public Vector3 slope = Vector3.zero;

		// Token: 0x04001AF7 RID: 6903
		public Vector3 cliffVector = Vector3.zero;

		// Token: 0x04001AF8 RID: 6904
		public Vector3 wallVector = Vector3.zero;

		// Token: 0x04001AF9 RID: 6905
		public List<Pipe> pipes = new List<Pipe>();

		// Token: 0x04001AFA RID: 6906
		public bool border;

		// Token: 0x04001AFB RID: 6907
		public float cliffyness;

		// Token: 0x04001AFC RID: 6908
		public float distanceToCliff = float.MaxValue;

		// Token: 0x04001AFD RID: 6909
		public float distanceToWall = float.MaxValue;
	}
}
