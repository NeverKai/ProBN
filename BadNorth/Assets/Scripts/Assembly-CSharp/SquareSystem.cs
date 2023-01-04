using System;
using UnityEngine;

// Token: 0x020005D2 RID: 1490
public class SquareSystem : MonoBehaviour
{
	// Token: 0x060026C8 RID: 9928 RVA: 0x0007B92C File Offset: 0x00079D2C
	public void PlaceSquares()
	{
		Vector3 b = new Vector3(0.5f, 0f, 0.5f);
		float num = 0f;
		for (int i = 0; i < this.graph.edges.Count; i++)
		{
			num += Vector3.Distance(this.graph.edges[i].pointA.pos, this.graph.edges[i].pointB.pos);
		}
		num /= (float)this.graph.edges.Count;
		for (int j = 0; j < this.graph.faces.Count; j++)
		{
			Graph.Node node = this.graph.faces[j];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.srcObj.gameObject);
			gameObject.transform.SetParent(base.transform, false);
			gameObject.transform.localPosition = node.pos;
			gameObject.transform.localScale = num * Vector3.one;
			gameObject.transform.localRotation = Quaternion.LookRotation(node.normal, node.others[0].pos - node.GetOther(-1).pos) * Quaternion.Euler(90f, 0f, 0f);
			Matrix4x4[] array = new Matrix4x4[4];
			for (int k = 0; k < 4; k++)
			{
				Matrix4x4 matrix4x = Matrix4x4.identity;
				Vector4 column = node.others[k].pos;
				column.w = 1f;
				matrix4x.SetColumn(3, column);
				Vector4 column2 = node.others[k].normal * num;
				column2.w = 0f;
				matrix4x.SetColumn(1, column2);
				Vector4 column3 = node.GetOther(k + 1).pos - node.others[k].pos;
				Vector4 a = node.GetOther(k - 1).pos - node.others[k].pos;
				column3.w = 0f;
				a.w = 0f;
				matrix4x.SetColumn(0, column3);
				matrix4x.SetColumn(2, -a);
				matrix4x *= Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, (float)(-90 * k), 0f), Vector3.one);
				array[k] = matrix4x;
			}
			MeshFilter[] componentsInChildren = gameObject.GetComponentsInChildren<MeshFilter>();
			for (int l = 0; l < componentsInChildren.Length; l++)
			{
				Mesh mesh = UnityEngine.Object.Instantiate<Mesh>(componentsInChildren[l].sharedMesh);
				componentsInChildren[l].sharedMesh = mesh;
				Vector3[] vertices = mesh.vertices;
				Vector3[] normals = mesh.normals;
				Matrix4x4 matrix4x2 = gameObject.transform.worldToLocalMatrix * componentsInChildren[l].transform.localToWorldMatrix;
				Matrix4x4 matrix4x3 = base.transform.worldToLocalMatrix * componentsInChildren[l].transform.localToWorldMatrix;
				Matrix4x4[] array2 = new Matrix4x4[4];
				for (int m = 0; m < 4; m++)
				{
					array2[m] = matrix4x3.inverse * array[m];
				}
				for (int n = 0; n < vertices.Length; n++)
				{
					Vector3 vector = matrix4x2.MultiplyPoint(vertices[n]) + b;
					float[] array3 = new float[]
					{
						(1f - vector.x) * vector.z,
						vector.x * vector.z,
						vector.x * (1f - vector.z),
						(1f - vector.x) * (1f - vector.z)
					};
					Vector3 vector2 = Vector3.zero;
					Vector3 a2 = Vector3.zero;
					Vector3 point = new Vector3(0f, vector.y, 0f);
					Vector3 vector3 = matrix4x2.MultiplyVector(normals[n]);
					for (int num2 = 0; num2 < 4; num2++)
					{
						vector2 += array2[num2].MultiplyPoint(point) * array3[num2];
						a2 += array2[num2].MultiplyVector(vector3) * array3[num2];
					}
					vertices[n] = vector2;
					normals[n] = a2.normalized;
				}
				mesh.vertices = vertices;
				mesh.normals = normals;
				mesh.RecalculateBounds();
			}
		}
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x0007BE46 File Offset: 0x0007A246
	private void Start()
	{
		this.graph = base.GetComponent<GraphMesh>().graph;
		this.PlaceSquares();
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x0007BE5F File Offset: 0x0007A25F
	private void Update()
	{
	}

	// Token: 0x040018BE RID: 6334
	public GameObject srcObj;

	// Token: 0x040018BF RID: 6335
	private Graph graph;
}
