using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000655 RID: 1621
	public class NavigationMesh : MonoBehaviour
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x0008E1FC File Offset: 0x0008C5FC
		public static NavigationMesh instance
		{
			get
			{
				return Singleton<CampaignManager>.instance.campaign.currentLevel.island.navMesh;
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06002916 RID: 10518 RVA: 0x0008E218 File Offset: 0x0008C618
		// (remove) Token: 0x06002917 RID: 10519 RVA: 0x0008E250 File Offset: 0x0008C650
		
		public event NavigationMesh.ForestDelegate onUnreacables = delegate(List<Tri> A_0)
		{
		};

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06002918 RID: 10520 RVA: 0x0008E288 File Offset: 0x0008C688
		// (remove) Token: 0x06002919 RID: 10521 RVA: 0x0008E2C0 File Offset: 0x0008C6C0
		
		public event NavigationMesh.ForestDelegate onReachables = delegate(List<Tri> A_0)
		{
		};

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x0600291A RID: 10522 RVA: 0x0008E2F8 File Offset: 0x0008C6F8
		// (remove) Token: 0x0600291B RID: 10523 RVA: 0x0008E330 File Offset: 0x0008C730
		
		public event NavigationMesh.NavMeshDelegate onWipe = delegate(NavigationMesh A_0)
		{
		};

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x0600291C RID: 10524 RVA: 0x0008E368 File Offset: 0x0008C768
		// (remove) Token: 0x0600291D RID: 10525 RVA: 0x0008E3A0 File Offset: 0x0008C7A0
		
		public event NavigationMesh.NavMeshDelegate onProcess = delegate(NavigationMesh A_0)
		{
		};

		// Token: 0x0600291E RID: 10526 RVA: 0x0008E3D8 File Offset: 0x0008C7D8
		private void OnDestroy()
		{
			this.island = null;
			if (this.pipes != null)
			{
				foreach (Pipe pipe in this.pipes)
				{
					pipe.OnDestroy();
				}
			}
			if (this.tris != null)
			{
				foreach (Tri tri in this.tris)
				{
					tri.OnDestroy();
				}
			}
			if (this.verts != null)
			{
				foreach (Vert vert in this.verts)
				{
					vert.OnDestroy();
				}
			}
			if (this.edges != null)
			{
				foreach (Edge edge in this.edges)
				{
					edge.OnDestroy();
				}
			}
			this.pipes = null;
			this.tris = null;
			this.verts = null;
			this.edges = null;
			this.onUnreacables = null;
			this.onReachables = null;
			this.onWipe = null;
			this.onProcess = null;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0008E500 File Offset: 0x0008C900
		public Mesh GetNewMesh(Mesh mesh)
		{
			if (!mesh)
			{
				mesh = new Mesh();
			}
			Vector3[] array = new Vector3[this.verts.Length];
			int[] array2 = new int[this.tris.Length * 3];
			for (int i = 0; i < this.verts.Length; i++)
			{
				array[i] = this.verts[i].pos;
			}
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = (int)this.tris[j / 3].verts[j % 3].index;
			}
			mesh.vertices = array;
			mesh.triangles = array2;
			return mesh;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0008E5B4 File Offset: 0x0008C9B4
		public IEnumerator<GenInfo> Setup(List<Vector3> mVerts, List<int> mTris, Island island)
		{
			List<Pipe> pipeList = ListPool<Pipe>.GetList(1800);
			List<Tri> triList = ListPool<Tri>.GetList(600);
			List<Vert> vertList = ListPool<Vert>.GetList(600);
			List<Edge> edgeList = ListPool<Edge>.GetList(1200);
			this.island = island;
			Vert[] vertLookup = new Vert[mVerts.Count];
			for (int i = 0; i < mVerts.Count; i++)
			{
				//NavigationMesh.<Setup>c__Iterator0.<Setup>c__AnonStorey2 <Setup>c__AnonStorey = new NavigationMesh.<Setup>c__Iterator0.<Setup>c__AnonStorey2();
				//<Setup>c__AnonStorey.<>f__ref$0 = this;
				var pos = mVerts[i];
				// NavigationMesh.<Setup>c__Iterator0.<Setup>c__AnonStorey2 <Setup>c__AnonStorey2 = <Setup>c__AnonStorey;
				pos.y = pos.y + -0.09f;
				Vert vert = null;
				vert = vertList.FirstOrDefault((Vert x) => (x.pos - pos).sqrMagnitude < 0.0001f);
				if (vert == null)
				{
					vert = new Vert(pos);
					vert.index = (ushort)vertList.Count;
					vertList.Add(vert);
				}
				vertLookup[i] = vert;
				yield return new GenInfo("navmesh vert", GenInfo.Mode.interruptable);
			}
			for (int j = 0; j < mTris.Count; j += 3)
			{
				Tri tri = new Tri();
				tri.navigationMesh = this;
				tri.index = (ushort)(j / 3);
				triList.Add(tri);
				for (int num = 0; num < 3; num++)
				{
					int num2 = mTris[j + num];
					int num3 = mTris[j + (num + 1) % 3];
					Vert vert2 = vertLookup[num2];
					Vert vert3 = vertLookup[num3];
					tri.verts[num] = vert2;
					vert2.tris.Add(tri);
					Vector3 pos = (vert2.pos + vert3.pos) / 2f;
					Edge edge = edgeList.FirstOrDefault((Edge x) => (x.pos - pos).sqrMagnitude < 0.0001f);
					if (edge == null)
					{
						edge = new Edge(vert2, vert3);
						edge.index = (ushort)edgeList.Count;
						edgeList.Add(edge);
						edge.tris[0] = tri;
					}
					else
					{
						edge.border = false;
						edge.tris[1] = tri;
					}
					tri.edges[(num + 2) % 3] = edge;
				}
				yield return new GenInfo("navmesh tri", GenInfo.Mode.interruptable);
			}
			for (int k = 0; k < edgeList.Count; k++)
			{
				//using ("edgeList")
				{
					edgeList[k].Setup();
				}
				yield return new GenInfo("EdgeList.Setup", GenInfo.Mode.interruptable);
			}
			for (int l = 0; l < triList.Count; l++)
			{
				//using ("triList")
				{
					triList[l].Setup();
				}
				yield return new GenInfo("TriList.Setup", GenInfo.Mode.interruptable);
			}
			for (int m = 0; m < vertList.Count; m++)
			{
				//using ("vertList")
				{
					vertList[m].Setup();
				}
				yield return new GenInfo("VertList.Setup2", GenInfo.Mode.interruptable);
			}
			for (int n = 0; n < edgeList.Count; n++)
			{
				//using ("edgeList2")
				{
					edgeList[n].Setup2();
				}
				yield return new GenInfo("EdgeList.Setup2", GenInfo.Mode.interruptable);
			}
			for (int j2 = 0; j2 < vertList.Count; j2++)
			{
				if (vertList[j2].Setup2())
				{
					yield return new GenInfo("VertList.Setup2", GenInfo.Mode.interruptable);
				}
				else
				{
					triList.ReturnToListPool<Tri>();
					vertList.ReturnToListPool<Vert>();
					edgeList.ReturnToListPool<Edge>();
					pipeList.ReturnToListPool<Pipe>();
					yield return new GenInfo("VertList.Setup2", GenInfo.Mode.broken);
				}
			}
			IEnumerator cliffDistances = this.SpreadDistance(from x in vertList
			where x.distanceToCliff == 0f
			select x, (Vert v) => v.distanceToCliff, delegate(Vert v, float f)
			{
				v.distanceToCliff = f;
			});
			while (cliffDistances.MoveNext())
			{
				yield return new GenInfo("Calulating cliff distance", GenInfo.Mode.interruptable);
			}
			IEnumerator wallDistances = this.SpreadDistance(from x in vertList
			where x.distanceToWall == 0f
			select x, (Vert v) => v.distanceToWall, delegate(Vert v, float f)
			{
				v.distanceToWall = f;
			});
			while (wallDistances.MoveNext())
			{
				yield return new GenInfo("Calulating wall distance", GenInfo.Mode.interruptable);
			}
			if (island)
			{
				List<List<Tri>> newTriLists = new List<List<Tri>>();
				int totalCount = triList.Count;
				for (int num4 = 0; num4 < triList.Count; num4++)
				{
					triList[num4].index = (ushort)num4;
				}
				bool[] takenTris = new bool[triList.Count];
				while (triList.Count > 0)
				{
					List<Tri> newList = new List<Tri>();
					newTriLists.Add(newList);
					newList.Add(triList[0]);
					takenTris[(int)triList[0].index] = true;
					triList.RemoveAt(0);
					for (int num5 = 0; num5 < newList.Count; num5++)
					{
						Tri tri2 = newList[num5];
						for (int num6 = 0; num6 < 3; num6++)
						{
							Tri tri3 = tri2.tris[num6];
							if (tri3 != null && !takenTris[(int)tri3.index])
							{
								newList.Add(tri3);
								takenTris[(int)tri3.index] = true;
								triList.Remove(tri3);
							}
						}
					}
					yield return new GenInfo("Sorting reachability", GenInfo.Mode.interruptable);
				}
				yield return new GenInfo("Navmesh: Island pt1", GenInfo.Mode.interruptable);
				newTriLists.Sort((List<Tri> a, List<Tri> b) => (-a.Count).CompareTo(-b.Count));
				triList.AddRange(newTriLists[0]);
				triList.Sort((Tri a, Tri b) => a.pos.y.CompareTo(b.pos.y));
				List<Tri> removedTris = new List<Tri>();
				for (int j3 = 1; j3 < newTriLists.Count; j3++)
				{
					removedTris.AddRange(newTriLists[j3]);
					yield return default(GenInfo);
				}
				this.reachRatio = (float)(totalCount - removedTris.Count) / (float)totalCount;
				yield return new GenInfo("pre-unreachables", GenInfo.Mode.interruptable);
				//using ("unreachables")
				{
					this.onUnreacables(removedTris);
				}
				yield return new GenInfo("post-unreachables", GenInfo.Mode.interruptable);
				vertList.Clear();
				edgeList.Clear();
				for (int j4 = 0; j4 < triList.Count; j4++)
				{
					using (new ScopedProfiler("removedTris.AddRange", null))
					{
						Tri tri4 = triList[j4];
						for (int num7 = 0; num7 < 3; num7++)
						{
							if (!vertList.Contains(tri4.verts[num7]))
							{
								vertList.Add(tri4.verts[num7]);
							}
							if (!edgeList.Contains(tri4.edges[num7]))
							{
								edgeList.Add(tri4.edges[num7]);
							}
						}
					}
					yield return new GenInfo("removedTris.AddRange", GenInfo.Mode.interruptable);
				}
			}
			yield return new GenInfo("pre-reachables", GenInfo.Mode.interruptable);
			//using ("reachables")
			{
				this.onReachables(triList);
			}
			yield return new GenInfo("post-reachables", GenInfo.Mode.interruptable);
			for (int num8 = triList.Count - 1; num8 >= 0; num8--)
			{
				triList[num8].index = (ushort)num8;
			}
			for (int num9 = vertList.Count - 1; num9 >= 0; num9--)
			{
				vertList[num9].index = (ushort)num9;
			}
			for (int num10 = edgeList.Count - 1; num10 >= 0; num10--)
			{
				edgeList[num10].index = (ushort)num10;
			}
			this.bounds = new Bounds(vertList[0].pos, Vector3.zero);
			for (int num11 = vertList.Count - 1; num11 >= 0; num11--)
			{
				pipeList.AddRange(vertList[num11].pipes);
				this.bounds.Encapsulate(vertList[num11].pos);
			}
			this.triCount = triList.Count;
			this.vertCount = vertList.Count;
			this.edgeCount = edgeList.Count;
			this.tris = triList.ToArray();
			this.verts = vertList.ToArray();
			this.edges = edgeList.ToArray();
			this.pipes = pipeList.ToArray();
			triList.ReturnToListPool<Tri>();
			vertList.ReturnToListPool<Vert>();
			edgeList.ReturnToListPool<Edge>();
			pipeList.ReturnToListPool<Pipe>();
			this.active = true;
			this.onProcess(this);
			yield break;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0008E5E4 File Offset: 0x0008C9E4
		public Vert GetClosestVert(Vector3 pos)
		{
			float num = float.MaxValue;
			Vert result = null;
			foreach (Vert vert in this.verts)
			{
				float sqrMagnitude = (vert.pos - pos).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = vert;
					num = sqrMagnitude;
				}
			}
			return result;
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x0008E644 File Offset: 0x0008CA44
		private IEnumerator SpreadDistance(IEnumerable<Vert> startVerts, Func<Vert, float> Get, Action<Vert, float> Set)
		{
			List<Pipe> cliffPipes = new List<Pipe>();
			foreach (Vert vert in startVerts)
			{
				cliffPipes.AddRange(from p in vert.pipes
				where !startVerts.Any((Vert v) => p.outVert == v)
				select p);
				yield return null;
			}
			yield return null;
			cliffPipes.Sort((Pipe a, Pipe b) => a.edge.length.CompareTo(b.edge.length));
			yield return null;
			for (int i = 0; i < cliffPipes.Count; i++)
			{
				Pipe pipe0 = cliffPipes[i];
				float dist0 = Get(pipe0.inVert) + pipe0.edge.length;
				if (Get(pipe0.outVert) > dist0)
				{
					Set(pipe0.outVert, dist0);
					foreach (Pipe pipe in pipe0.outVert.pipes)
					{
						float dist = Get(pipe.inVert) + pipe.edge.length;
						if (Get(pipe.outVert) > dist)
						{
							int j;
							for (j = i + 1; j < cliffPipes.Count; j++)
							{
								Pipe pipe2 = cliffPipes[j];
								float num = Get(pipe2.inVert) + pipe2.edge.length;
								if (dist < num)
								{
									break;
								}
							}
							cliffPipes.Insert(j, pipe);
						}
						yield return null;
					}
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x0008E66D File Offset: 0x0008CA6D
		public void Wipe()
		{
			this.onWipe(this);
			this.tris = null;
			this.verts = null;
			this.edges = null;
			this.pipes = null;
			this.active = false;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x0008E6A0 File Offset: 0x0008CAA0
		private void OnDrawGizmos()
		{
			Gizmos.color = new Color(0f, 0f, 0f, 0.1f);
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix() * base.transform.localToWorldMatrix;
			Color black = Color.black;
			if (this.verts == null)
			{
				return;
			}
			for (int i = 0; i < this.edges.Length; i++)
			{
				Edge edge = this.edges[i];
				black.a = ((!edge.border) ? 0.1f : ((!edge.cliff) ? 0.3f : ((!edge.beach) ? 0.5f : 1f)));
				Gizmos.color = black;
				Gizmos.DrawLine(this.edges[i].verts[0].pos, this.edges[i].verts[1].pos);
			}
			NavigationMesh.GizmoMode gizmoMode = NavigationMesh.gizmoMode;
			if (gizmoMode != NavigationMesh.GizmoMode.None)
			{
				if (gizmoMode == NavigationMesh.GizmoMode.Weight)
				{
					Gizmos.color = Color.white;
					for (int j = 0; j < this.verts.Length; j++)
					{
						Vert vert = this.verts[j];
						for (int k = 0; k < vert.pipes.Count; k++)
						{
							Pipe pipe = vert.pipes[k];
							Gizmos.DrawRay(vert.pos, pipe.dir * pipe.weight * 0.5f);
						}
					}
				}
			}
		}

		// Token: 0x04001AC5 RID: 6853
		public Island island;

		// Token: 0x04001AC6 RID: 6854
		public Pipe[] pipes;

		// Token: 0x04001AC7 RID: 6855
		public Tri[] tris;

		// Token: 0x04001AC8 RID: 6856
		public Vert[] verts;

		// Token: 0x04001AC9 RID: 6857
		public Edge[] edges;

		// Token: 0x04001ACA RID: 6858
		public float grass = 0.5f;

		// Token: 0x04001ACB RID: 6859
		public bool active;

		// Token: 0x04001ACC RID: 6860
		public Bounds bounds;

		// Token: 0x04001ACD RID: 6861
		public Vector3 velocity = Vector3.zero;

		// Token: 0x04001ACE RID: 6862
		[Header("Reach Data")]
		public float reachRatio = 1f;

		// Token: 0x04001ACF RID: 6863
		[Header("Count Data")]
		public int triCount;

		// Token: 0x04001AD0 RID: 6864
		public int vertCount;

		// Token: 0x04001AD1 RID: 6865
		public int edgeCount;

		// Token: 0x04001AD6 RID: 6870
		[ConsoleCommand("")]
		private static NavigationMesh.GizmoMode gizmoMode;

		// Token: 0x04001AD7 RID: 6871
		public const float verticalOffset = -0.09f;

		// Token: 0x02000656 RID: 1622
		// (Invoke) Token: 0x0600292A RID: 10538
		public delegate void ForestDelegate(List<Tri> tris);

		// Token: 0x02000657 RID: 1623
		// (Invoke) Token: 0x0600292E RID: 10542
		public delegate void NavMeshDelegate(NavigationMesh navMesh);

		// Token: 0x02000658 RID: 1624
		private enum GizmoMode
		{
			// Token: 0x04001ADD RID: 6877
			None,
			// Token: 0x04001ADE RID: 6878
			Weight
		}
	}
}
