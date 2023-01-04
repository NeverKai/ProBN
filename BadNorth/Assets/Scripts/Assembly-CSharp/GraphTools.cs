using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000520 RID: 1312
internal class GraphTools
{
	// Token: 0x06002209 RID: 8713 RVA: 0x000616C0 File Offset: 0x0005FAC0
	public static Graph MeshToGraph(Mesh mesh)
	{
		Graph graph = new Graph();
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;
		Dictionary<Vector3, Graph.Point> dictionary = new Dictionary<Vector3, Graph.Point>();
		for (int i = 0; i < vertices.Length; i++)
		{
			if (!dictionary.ContainsKey(vertices[i]))
			{
				Graph.Point point = new Graph.Point();
				point.pos = vertices[i];
				point.normal = normals[i];
				dictionary.Add(vertices[i], point);
				graph.points.Add(point);
			}
		}
		List<GraphTools.Triangle> list = new List<GraphTools.Triangle>();
		Dictionary<int, GraphTools.Triangle> dictionary2 = new Dictionary<int, GraphTools.Triangle>();
		List<List<GraphTools.Triangle>> list2 = new List<List<GraphTools.Triangle>>();
		for (int j = 0; j < triangles.Length; j += 3)
		{
			GraphTools.Triangle triangle = new GraphTools.Triangle();
			for (int k = 0; k < 3; k++)
			{
				triangle.indices[k] = triangles[j + k];
			}
			for (int l = 0; l < 3; l++)
			{
				triangle.pos += vertices[triangle.indices[l]];
			}
			for (int m = 0; m < 3; m++)
			{
				dictionary2.Add(triangle.GetReverseEdge(m), triangle);
			}
			triangle.pos /= 3f;
			list.Add(triangle);
		}
		for (int n = 0; n < list.Count; n++)
		{
			GraphTools.Triangle triangle2 = null;
			for (int num = 0; num < 3; num++)
			{
				dictionary2.TryGetValue(list[n].GetEdge(num), out triangle2);
				list[n].triangles[num] = triangle2;
			}
		}
		int num2 = 0;
		Debug.Log("tri count= " + list.Count);
		while (list.Count > 0 && num2 < 1000000)
		{
			List<GraphTools.Triangle> list3 = new List<GraphTools.Triangle>
			{
				list[0]
			};
			List<GraphTools.Triangle> list4 = new List<GraphTools.Triangle>();
			List<GraphTools.Triangle> list5 = new List<GraphTools.Triangle>
			{
				list[0]
			};
			num2++;
			while (list3.Count > 0 && num2 < 1000000)
			{
				num2++;
				for (int num3 = 0; num3 < list3.Count; num3++)
				{
					list.Remove(list3[num3]);
					for (int num4 = 0; num4 < 3; num4++)
					{
						GraphTools.Triangle triangle3 = list3[num3].triangles[num4];
						if (triangle3 != null && !list5.Contains(triangle3))
						{
							list4.Add(triangle3);
							list5.Add(triangle3);
						}
					}
				}
				list3.Clear();
				list3.AddRange(list4);
				list4.Clear();
			}
			list2.Add(list5);
		}
		for (int num5 = 0; num5 < list2.Count; num5++)
		{
			Graph.Face face = new Graph.Face();
			for (int num6 = 0; num6 < list2[num5].Count; num6++)
			{
				for (int num7 = 0; num7 < 3; num7++)
				{
					Vector3 key = vertices[list2[num5][num6].indices[num7]];
					Graph.Point point2 = dictionary[key];
					if (!face.points.Contains(point2))
					{
						face.others.Add(point2);
						point2.others.Add(face);
					}
				}
			}
			face.pos = Vector3.zero;
			for (int num8 = 0; num8 < face.points.Count; num8++)
			{
				face.pos += face.points[num8].pos;
			}
			face.pos /= (float)face.points.Count;
			graph.faces.Add(face);
		}
		graph.Setup();
		Debug.Log(graph.faces.Count);
		Debug.Log(graph.points.Count);
		return graph;
	}

	// Token: 0x02000521 RID: 1313
	private class Triangle
	{
		// Token: 0x0600220B RID: 8715 RVA: 0x00061B7C File Offset: 0x0005FF7C
		public int GetEdge(int i)
		{
			return this.indices[i] * 7283 + this.indices[(i + 1) % 3];
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00061B99 File Offset: 0x0005FF99
		public int GetReverseEdge(int i)
		{
			return this.indices[i] + this.indices[(i + 1) % 3] * 7283;
		}

		// Token: 0x040014B9 RID: 5305
		public GraphTools.Triangle[] triangles = new GraphTools.Triangle[3];

		// Token: 0x040014BA RID: 5306
		public int[] indices = new int[3];

		// Token: 0x040014BB RID: 5307
		public Vector3 pos;
	}
}
