using System;
using System.Collections.Generic;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000660 RID: 1632
	public class NavRoads : IslandComponent, IIslandFirstEnter
	{
		// Token: 0x060029AC RID: 10668 RVA: 0x00092B3C File Offset: 0x00090F3C
		private NavRoads.Node GetNode(Vector3 pos, Dictionary<Vector3, NavRoads.Node> nodeDict, Tri tri)
		{
			NavRoads.Node node;
			if (!nodeDict.TryGetValue(pos, out node))
			{
				node = new NavRoads.Node(pos, tri);
				nodeDict.Add(pos, node);
			}
			return node;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x00092B68 File Offset: 0x00090F68
		private void Connect(Vector3 pos0, Vector3 pos1, Dictionary<Vector3, NavRoads.Node> nodeDict, Tri tri)
		{
			float magnitude = (pos0 - pos1).magnitude;
			int num = 0;
			foreach (NavSpot navSpot in base.island.navSpotter.navSpots)
			{
				if (navSpot.meshBounds.Contains(pos0))
				{
					if (++num == 2)
					{
						return;
					}
					pos0 = navSpot.meshBounds.ClosestPoint(pos1);
				}
				if (navSpot.meshBounds.Contains(pos1))
				{
					if (++num == 2)
					{
						return;
					}
					pos1 = navSpot.meshBounds.ClosestPoint(pos0);
				}
			}
			magnitude = (pos0 - pos1).magnitude;
			int num2 = Mathf.Max(1, (int)(magnitude / this.spacing));
			NavRoads.Node node = this.GetNode(pos0, nodeDict, tri);
			for (float num3 = 0f; num3 < (float)num2; num3 += 1f)
			{
				float t = (num3 + 1f) / (float)num2;
				Vector3 pos2 = Vector3.Lerp(pos0, pos1, t);
				NavRoads.Node node2 = this.GetNode(pos2, nodeDict, tri);
				node.nodes.Add(node2);
				node2.nodes.Add(node);
				node = node2;
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00092CCC File Offset: 0x000910CC
		private void Start()
		{
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00092CD0 File Offset: 0x000910D0
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			GenInfo genInfo = new GenInfo("NavRoads", GenInfo.Mode.interruptable);
			if (!base.enabled)
			{
				yield break;
			}
			NavigationMesh navMesh = island.navMesh;
			NavRoads.NodeTri[] nodeTris = new NavRoads.NodeTri[navMesh.tris.Length];
			List<Tri> tris = new List<Tri>();
			for (int l = 0; l < navMesh.tris.Length; l++)
			{
				Tri tri2 = navMesh.tris[l];
				if (tri2.borderCount >= 3)
				{
					nodeTris[l] = new NavRoads.NodeTri(tri2);
					tris.Add(tri2);
				}
			}
			Dictionary<Vector3, NavRoads.Node> nodeDict = new Dictionary<Vector3, NavRoads.Node>();
			yield return genInfo;
			for (int i = 0; i < tris.Count; i++)
			{
				Tri tri = tris[i];
				List<Edge> edges = new List<Edge>();
				for (int m = 0; m < 3; m++)
				{
					Tri tri3 = tri.tris[m];
					if (tri3 != null)
					{
						if (nodeTris[(int)tri3.index] != null)
						{
							edges.Add(tri.edges[m]);
						}
					}
				}
				if (edges.Count == 1)
				{
					Edge edge = edges[0];
					for (int n = 0; n < 3; n++)
					{
						Edge edge2 = tri.edges[n];
						if (!edge2.border)
						{
							if (edge2 != edge)
							{
								this.Connect(edge.pos, edge2.pos, nodeDict, tri);
								break;
							}
						}
					}
				}
				else if (edges.Count == 2)
				{
					Edge edge3 = edges[0];
					Edge edge4 = edges[1];
					this.Connect(edge3.pos, edge4.pos, nodeDict, tri);
				}
				else if (edges.Count == 3)
				{
					Vector3 vector = Vector3.zero;
					for (int num = 0; num < edges.Count; num++)
					{
						vector += edges[num].pos;
					}
					vector /= 3f;
					for (int num2 = 0; num2 < edges.Count; num2++)
					{
						this.Connect(vector, edges[num2].pos, nodeDict, tri);
					}
				}
				yield return genInfo;
			}
			this.nodes.Clear();
			this.nodes.AddRange(nodeDict.Values);
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < this.nodes.Count; k++)
				{
					NavRoads.Node node = this.nodes[k];
					if (node.nodes.Count != 1)
					{
						Vector3 average = Vector3.zero;
						for (int num3 = 0; num3 < node.nodes.Count; num3++)
						{
							average += node.nodes[num3].pos;
						}
						average /= (float)node.nodes.Count;
						node.pos = Vector3.Lerp(node.pos, average, 0.5f);
						yield return genInfo;
					}
				}
			}
			yield return genInfo;
			if (this.meshThings == null)
			{
				GameObject gameObject = base.gameObject;
				MeshFilter[] componentsInChildren = gameObject.GetComponentsInChildren<MeshFilter>();
				this.meshThings = new NavRoads.MeshThing[componentsInChildren.Length];
				for (int num4 = 0; num4 < componentsInChildren.Length; num4++)
				{
					this.meshThings[num4] = new NavRoads.MeshThing(componentsInChildren[num4]);
				}
				foreach (Transform transform in gameObject.GetComponentsInChildren<Transform>())
				{
					transform.localPosition = Vector3.zero;
					transform.localScale = Vector3.one;
					transform.localRotation = Quaternion.identity;
				}
			}
			for (int num6 = 0; num6 < this.meshThings.Length; num6++)
			{
				this.meshThings[num6].MakeMesh(this.nodes);
			}
			yield return genInfo;
			yield break;
		}

		// Token: 0x04001B15 RID: 6933
		private List<NavRoads.Node> nodes = new List<NavRoads.Node>();

		// Token: 0x04001B16 RID: 6934
		private NavRoads.MeshThing[] meshThings;

		// Token: 0x04001B17 RID: 6935
		public GameObject nodePrefab;

		// Token: 0x04001B18 RID: 6936
		public float width = 0.01f;

		// Token: 0x04001B19 RID: 6937
		public float spacing = 0.1f;

		// Token: 0x04001B1A RID: 6938
		public LayerMask layerMask;

		// Token: 0x04001B1B RID: 6939
		[ConsoleCommand("")]
		public static bool show;

		// Token: 0x02000661 RID: 1633
		private class MeshThing
		{
			// Token: 0x060029B1 RID: 10673 RVA: 0x00092CF4 File Offset: 0x000910F4
			public MeshThing(MeshFilter meshFilter)
			{
				this.srcMesh = meshFilter.sharedMesh;
				this.mergedMesh = new Mesh();
				meshFilter.sharedMesh = this.mergedMesh;
				this.srcMatrix = meshFilter.transform.localToWorldMatrix;
				this.combine = new CombineInstance
				{
					mesh = this.srcMesh
				};
			}

			// Token: 0x060029B2 RID: 10674 RVA: 0x00092D58 File Offset: 0x00091158
			public void MakeMesh(List<NavRoads.Node> nodes)
			{
				this.mergedMesh.Clear();
				CombineInstance[] array = new CombineInstance[nodes.Count];
				for (int i = 0; i < nodes.Count; i++)
				{
					if (nodes[i].nodes.Count == 1)
					{
						this.combine.transform = this.srcMatrix * Matrix4x4.Scale(Vector3.one * 2f);
					}
					else
					{
						this.combine.transform = this.srcMatrix;
					}
					this.combine.transform = nodes[i].pos.GetMoveMatrix() * this.combine.transform;
					array[i] = this.combine;
				}
				this.mergedMesh.CombineMeshes(array);
			}

			// Token: 0x04001B1C RID: 6940
			private Mesh srcMesh;

			// Token: 0x04001B1D RID: 6941
			private Mesh mergedMesh;

			// Token: 0x04001B1E RID: 6942
			private CombineInstance combine;

			// Token: 0x04001B1F RID: 6943
			private Matrix4x4 srcMatrix;
		}

		// Token: 0x02000662 RID: 1634
		private class NodeTri
		{
			// Token: 0x060029B3 RID: 10675 RVA: 0x00092E34 File Offset: 0x00091234
			public NodeTri(Tri tri)
			{
				this.tri = tri;
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x060029B4 RID: 10676 RVA: 0x00092E43 File Offset: 0x00091243
			public int index
			{
				get
				{
					return (int)this.tri.index;
				}
			}

			// Token: 0x04001B20 RID: 6944
			public Tri tri;
		}

		// Token: 0x02000663 RID: 1635
		private class Node
		{
			// Token: 0x060029B5 RID: 10677 RVA: 0x00092E50 File Offset: 0x00091250
			public Node(Vector3 pos, Tri tri)
			{
				this.navPos = new NavPos(tri, pos);
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x060029B6 RID: 10678 RVA: 0x00092E70 File Offset: 0x00091270
			// (set) Token: 0x060029B7 RID: 10679 RVA: 0x00092E7D File Offset: 0x0009127D
			public Vector3 pos
			{
				get
				{
					return this.navPos.pos;
				}
				set
				{
					this.navPos.pos = value;
				}
			}

			// Token: 0x04001B21 RID: 6945
			public List<NavRoads.Node> nodes = new List<NavRoads.Node>();

			// Token: 0x04001B22 RID: 6946
			public NavPos navPos;
		}
	}
}
