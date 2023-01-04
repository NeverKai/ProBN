using System;
using UnityEngine;

// Token: 0x02000522 RID: 1314
public class GraphMesh : MonoBehaviour
{
	// Token: 0x0600220E RID: 8718 RVA: 0x00061BBE File Offset: 0x0005FFBE
	private void Awake()
	{
		this.graph = GraphTools.MeshToGraph(base.GetComponent<MeshFilter>().sharedMesh);
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x00061BD8 File Offset: 0x0005FFD8
	private void OnDrawGizmosSelected()
	{
		if (this.graph != null)
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Gizmos.color = Color.blue;
			for (int i = 0; i < this.graph.edges.Count; i++)
			{
				Gizmos.DrawSphere(this.graph.edges[i].pos, 0.01f);
				Vector3 pos = this.graph.edges[i].pos;
				for (int j = 0; j < this.graph.edges[i].nodes.Length; j++)
				{
					Vector3 vector = this.graph.edges[i].nodes[j].pos;
					Vector3 vector2 = this.graph.edges[i].nodes[(j + 1) % this.graph.edges[i].nodes.Length].pos;
					vector = Vector3.Lerp(vector, pos, 0.1f);
					vector2 = Vector3.Lerp(vector2, pos, 0.1f);
					Gizmos.DrawLine(vector, vector2);
				}
			}
			if (this.graph != null)
			{
				for (int k = 0; k < 2; k++)
				{
					Gizmos.color = ((k != 0) ? Color.yellow : Color.red);
					for (int l = 0; l < this.graph.nodes[k].Count; l++)
					{
						Graph.Node node = this.graph.nodes[k][l];
						Vector3 pos2 = node.pos;
						Gizmos.DrawSphere(pos2, 0.01f);
						Gizmos.DrawLine(pos2, pos2 + node.normal * 0.02f);
						for (int m = 0; m < node.others.Count; m++)
						{
							Vector3 vector3 = node.others[m].pos;
							Vector3 vector4 = node.others[(m + 1) % node.others.Count].pos;
							vector3 = Vector3.Lerp(vector3, pos2, 0.1f);
							vector4 = Vector3.Lerp(vector4, pos2, 0.1f);
							Gizmos.DrawLine(vector3, vector4);
							Gizmos.DrawLine(Vector3.Lerp(pos2, node.sames[m].pos, 0.3f), pos2);
						}
					}
				}
			}
		}
	}

	// Token: 0x040014BC RID: 5308
	public Graph graph;
}
