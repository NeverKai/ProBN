using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200051B RID: 1307
public class Graph
{
	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x060021ED RID: 8685 RVA: 0x00060D43 File Offset: 0x0005F143
	public List<Graph.Node> faces
	{
		get
		{
			return this.nodes[0];
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x060021EE RID: 8686 RVA: 0x00060D4D File Offset: 0x0005F14D
	public List<Graph.Node> points
	{
		get
		{
			return this.nodes[1];
		}
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x00060D58 File Offset: 0x0005F158
	public void GenerateEdges()
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < this.faces.Count; i++)
		{
			Graph.Node node = this.faces[i];
			for (int j = 0; j < node.others.Count; j++)
			{
				Vector3 vector = node.pos + node.sames[j].pos;
				if (!list.Contains(vector))
				{
					list.Add(vector);
					Graph.Edge edge = new Graph.Edge();
					edge.nodes[0] = node;
					edge.nodes[2] = node.sames[j];
					edge.nodes[1] = node.GetOther(j);
					edge.nodes[3] = node.GetOther(j - 1);
					edge.pos = vector / 2f;
					this.edges.Add(edge);
				}
			}
		}
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x00060E4C File Offset: 0x0005F24C
	public void Setup()
	{
		for (int i = 0; i < this.points.Count; i++)
		{
			this.points[i].SortContinous();
		}
		for (int j = 0; j < this.faces.Count; j++)
		{
			this.faces[j].SortContinous();
		}
		for (int k = 0; k < this.faces.Count; k++)
		{
			this.faces[k].AveragePosition();
		}
		for (int l = 0; l < this.faces.Count; l++)
		{
			this.faces[l].AverageNormal();
		}
		for (int m = 0; m < this.faces.Count; m++)
		{
			this.faces[m].SortClockwise();
		}
		for (int n = 0; n < this.points.Count; n++)
		{
			this.points[n].SortClockwise();
		}
		for (int num = 0; num < this.faces.Count; num++)
		{
			this.faces[num].CollectSames();
		}
		for (int num2 = 0; num2 < this.points.Count; num2++)
		{
			this.points[num2].CollectSames();
		}
		for (int num3 = 0; num3 < this.faces.Count; num3++)
		{
			this.faces[num3].pos.Normalize();
		}
		for (int num4 = 0; num4 < this.points.Count; num4++)
		{
			this.points[num4].pos.Normalize();
		}
		this.faces.TrimExcess();
		this.points.TrimExcess();
		for (int num5 = 0; num5 < this.faces.Count; num5++)
		{
			this.faces[num5].TrimExcess();
		}
		for (int num6 = 0; num6 < this.points.Count; num6++)
		{
			this.points[num6].TrimExcess();
		}
		this.GenerateEdges();
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x000610C4 File Offset: 0x0005F4C4
	public void Smooth()
	{
		float num = 0f;
		int num2 = 0;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < this.nodes[i].Count; j++)
			{
				for (int k = 0; k < this.nodes[i][j].sames.Count; k++)
				{
					num += Vector3.Distance(this.nodes[i][j].pos, this.nodes[i][j].sames[k].pos);
					num += Vector3.Distance(this.nodes[i][j].pos, this.nodes[i][j].others[k].pos);
					num2 += 2;
				}
			}
		}
		num /= (float)num2;
		for (int l = 0; l < 2; l++)
		{
			for (int m = 0; m < this.nodes[l].Count; m++)
			{
				Vector3 vector = Vector3.zero;
				for (int n = 0; n < this.nodes[l][m].sames.Count; n++)
				{
					vector -= (this.nodes[l][m].pos - this.nodes[l][m].sames[n].pos) * 0.1f;
					vector -= (this.nodes[l][m].pos - this.nodes[l][m].others[n].pos) * 0.1f;
				}
				this.nodes[l][m].pos += vector;
				this.nodes[l][m].pos.Normalize();
			}
		}
	}

	// Token: 0x040014B0 RID: 5296
	public List<Graph.Node>[] nodes = new List<Graph.Node>[]
	{
		new List<Graph.Node>(),
		new List<Graph.Node>()
	};

	// Token: 0x040014B1 RID: 5297
	public List<Graph.Edge> edges = new List<Graph.Edge>();

	// Token: 0x0200051C RID: 1308
	public class Node
	{
		// Token: 0x060021F3 RID: 8691 RVA: 0x00061318 File Offset: 0x0005F718
		public Graph.Node GetNode(List<Graph.Node> list, int index)
		{
			return list[(index + list.Count) % list.Count];
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0006132F File Offset: 0x0005F72F
		public Graph.Node GetSame(int index)
		{
			return this.GetNode(this.sames, index);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0006133E File Offset: 0x0005F73E
		public Graph.Node GetOther(int index)
		{
			return this.GetNode(this.others, index);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00061350 File Offset: 0x0005F750
		public static bool NodesAreNeighbours(Graph.Node a, Graph.Node b)
		{
			int num = 0;
			for (int i = 0; i < a.others.Count; i++)
			{
				if (b.others.Contains(a.others[i]))
				{
					num++;
					if (num == 2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000613A8 File Offset: 0x0005F7A8
		public void SortContinous()
		{
			List<Graph.Node> list = new List<Graph.Node>();
			Graph.Node node = this.others[0];
			this.others.Remove(node);
			list.Add(node);
			int num = 0;
			while (this.others.Count > 0 && num < 100)
			{
				num++;
				for (int i = 0; i < this.others.Count; i++)
				{
					if (Graph.Node.NodesAreNeighbours(node, this.others[i]))
					{
						list.Add(this.others[i]);
						node = this.others[i];
						this.others.Remove(node);
						break;
					}
				}
			}
			this.others.Clear();
			this.others.AddRange(list);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0006147C File Offset: 0x0005F87C
		public void SortClockwise()
		{
			Vector3 lhs = this.others[0].pos - this.others[1].pos;
			Vector3 rhs = this.pos - this.others[0].pos;
			Vector3 lhs2 = Vector3.Cross(lhs, rhs);
			float num = Vector3.Dot(lhs2, this.normal);
			if (num > 0f)
			{
				this.others.Reverse();
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000614FC File Offset: 0x0005F8FC
		public void CollectSames()
		{
			for (int i = 0; i < this.others.Count; i++)
			{
				this.sames.Add(this.others[i].GetOther(this.others[i].others.IndexOf(this) + 1));
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0006155C File Offset: 0x0005F95C
		public void AveragePosition()
		{
			this.pos = Vector3.zero;
			for (int i = 0; i < this.others.Count; i++)
			{
				this.pos += this.others[i].pos;
			}
			this.pos /= (float)this.others.Count;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000615D0 File Offset: 0x0005F9D0
		public void AverageNormal()
		{
			this.normal = Vector3.zero;
			for (int i = 0; i < this.others.Count; i++)
			{
				this.normal += this.others[i].normal;
			}
			this.normal.Normalize();
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00061631 File Offset: 0x0005FA31
		public void TrimExcess()
		{
			this.sames.TrimExcess();
			this.others.TrimExcess();
		}

		// Token: 0x040014B2 RID: 5298
		public List<Graph.Node> sames = new List<Graph.Node>();

		// Token: 0x040014B3 RID: 5299
		public List<Graph.Node> others = new List<Graph.Node>();

		// Token: 0x040014B4 RID: 5300
		public UnityEngine.Object content;

		// Token: 0x040014B5 RID: 5301
		public Vector3 pos;

		// Token: 0x040014B6 RID: 5302
		public Vector3 normal;
	}

	// Token: 0x0200051D RID: 1309
	public class Face : Graph.Node
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x00061651 File Offset: 0x0005FA51
		public List<Graph.Node> points
		{
			get
			{
				return this.others;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x00061659 File Offset: 0x0005FA59
		public List<Graph.Node> faces
		{
			get
			{
				return this.sames;
			}
		}
	}

	// Token: 0x0200051E RID: 1310
	public class Edge
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00061675 File Offset: 0x0005FA75
		public Graph.Node faceA
		{
			get
			{
				return this.nodes[0];
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x0006167F File Offset: 0x0005FA7F
		public Graph.Node faceB
		{
			get
			{
				return this.nodes[2];
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00061689 File Offset: 0x0005FA89
		public Graph.Node pointA
		{
			get
			{
				return this.nodes[1];
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00061693 File Offset: 0x0005FA93
		public Graph.Node pointB
		{
			get
			{
				return this.nodes[3];
			}
		}

		// Token: 0x040014B7 RID: 5303
		public Graph.Node[] nodes = new Graph.Node[4];

		// Token: 0x040014B8 RID: 5304
		public Vector3 pos;
	}

	// Token: 0x0200051F RID: 1311
	public class Point : Graph.Node
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000616A5 File Offset: 0x0005FAA5
		public List<Graph.Node> points
		{
			get
			{
				return this.sames;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x000616AD File Offset: 0x0005FAAD
		public List<Graph.Node> faces
		{
			get
			{
				return this.others;
			}
		}
	}
}
