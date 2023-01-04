using System;
using UnityEngine;

namespace Voxels
{
	// Token: 0x0200062E RID: 1582
	public static class ModuleAo
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x00087DC0 File Offset: 0x000861C0
		public static void BakeAo(Mesh mesh, MeshCollider mc)
		{
			Vector3[] vertices = mesh.vertices;
			int[] triangles = mesh.triangles;
			Color[] array = new Color[vertices.Length];
			float[] array2 = new float[vertices.Length];
			for (int i = 0; i < triangles.Length; i += 3)
			{
				Vector3 normalized = Vector3.Cross(vertices[triangles[i + 1]] - vertices[triangles[i]], vertices[triangles[i + 2]] - vertices[triangles[i]]).normalized;
				for (int j = 0; j < 16; j++)
				{
					Vector3 vector = UnityEngine.Random.onUnitSphere;
					if (Vector3.Dot(vector, normalized) < 0f)
					{
						vector = -vector;
					}
					Vector3 a = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
					a /= a.x + a.y + a.z;
					a = Vector3.one / 3f;
					Vector3 vector2 = vertices[triangles[i]] * a.x + vertices[triangles[i + 1]] * a.y + vertices[triangles[i + 2]] * a.z;
					vector2 = mc.transform.TransformPoint(vector2);
					Ray ray = new Ray(vector2, vector);
					float num = 1f;
					RaycastHit raycastHit;
					Color a2;
					if (mc.Raycast(ray, out raycastHit, num))
					{
						a2 = Color.white * (raycastHit.distance / num);
					}
					else
					{
						a2 = Color.white;
					}
					array[triangles[i]] += a2 * a.x;
					array[triangles[i + 1]] += a2 * a.y;
					array[triangles[i + 2]] += a2 * a.z;
					array2[triangles[i]] += a.x;
					array2[triangles[i + 1]] += a.y;
					array2[triangles[i + 2]] += a.z;
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				array[k] /= array2[k];
			}
			for (int l = 0; l < array.Length; l++)
			{
				array[l].a = 1f;
			}
			Color32[] array3 = new Color32[array.Length];
			for (int m = 0; m < array.Length; m++)
			{
				array3[m] = array[m];
			}
			mesh.colors32 = array3;
		}
	}
}
