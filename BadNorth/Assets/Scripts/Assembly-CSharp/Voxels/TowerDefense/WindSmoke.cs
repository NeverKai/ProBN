using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000877 RID: 2167
	public class WindSmoke : MonoBehaviour, IIslandFirstEnter, IIslandLeave, IIslandEnter
	{
		// Token: 0x060038C3 RID: 14531 RVA: 0x000F58BC File Offset: 0x000F3CBC
		private static Texture2D GetTexture()
		{
			if (textures.Count > 0)
			{
				Texture2D texture2D = textures.Pop();
				if (texture2D)
				{
					return texture2D;
				}
			}
			return new Texture2D(8, 8, TextureFormat.ARGB32, false)
			{
				wrapMode = TextureWrapMode.Clamp
			};
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000F5904 File Offset: 0x000F3D04
		private static Mesh GetMesh()
		{
			if (meshes.Count > 0)
			{
				return meshes.Pop();
			}
			Mesh mesh = new Mesh();
			int num = 8;
			Vector3[] vertices = new Vector3[num * 4];
			Vector2[] array = new Vector2[num * 4];
			Vector2[] array2 = new Vector2[num * 4];
			Vector2[] array3 = new Vector2[num * 4];
			Vector2[] uv = new Vector2[num * 4];
			int[] array4 = new int[num * 6];
			for (int i = 0; i < num; i++)
			{
				int num2 = i * 4;
				float x = ((float)i + UnityEngine.Random.value * 0.5f) / (float)num;
				Vector2 vector = new Vector2(x, UnityEngine.Random.value);
				Vector2 vector2 = ExtraMath.Rotate2D(new Vector2(1f, 1f), (float)UnityEngine.Random.Range(0, 360));
				Vector2 vector3 = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
				Vector2 vector4 = new Vector2(1f, 1f);
				for (int j = 0; j < 4; j++)
				{
					array[num2 + j] = vector;
					array2[num2 + j] = vector2;
					array3[num2 + j] = vector3;
					vector2 = new Vector2(-vector2.y, vector2.x);
					vector4 = new Vector2(-vector4.y, vector4.x);
				}
			}
			for (int k = 0; k < num; k++)
			{
				array4[k * 6] = k * 4;
				array4[k * 6 + 1] = k * 4 + 1;
				array4[k * 6 + 2] = k * 4 + 2;
				array4[k * 6 + 3] = k * 4;
				array4[k * 6 + 4] = k * 4 + 2;
				array4[k * 6 + 5] = k * 4 + 3;
			}
			mesh.vertices = vertices;
			mesh.uv = array;
			mesh.uv2 = array2;
			mesh.uv3 = array3;
			mesh.uv4 = uv;
			mesh.triangles = array4;
			mesh.bounds = new Bounds(Vector3.up, Vector3.one * 100f);
			return mesh;
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000F5B24 File Offset: 0x000F3F24
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			GenInfo genInfo = new GenInfo("WindSmoke");
			Wind wind = island.wind;
			this.mr = base.gameObject.GetOrAddComponent<MeshRenderer>();
			this.mf = base.gameObject.GetOrAddComponent<MeshFilter>();
			Vector3 constantForce = Vector3.up;
			Bounds bounds = new Bounds(base.transform.position, Vector3.zero);
			float maxDist = 0f;
			this.tex = GetTexture();
			this.mf.sharedMesh = GetMesh();
			for (int y = 0; y < 8; y++)
			{
				Vector3 pos = base.transform.position;
				float totalDist = 0f;
				float t = (float)y / 7f;
				t *= t;
				for (int x = 0; x < 8; x++)
				{
					nodes[x, y].pos = pos;
					nodes[x, y].distance = totalDist;
					Vector3 windVelocity = wind.GetWindDirectionLinear(pos);
					Vector3 normal;
					float openness;
					wind.island.voxelSpace.SampleNormalOpenness(pos, out normal, out openness);
					float coverage = 1f - openness;
					windVelocity += normal;
					Vector3 fastSpeed = windVelocity;
					fastSpeed += Vector3.up * ExtraMath.RemapValue(totalDist, 0f, 0.5f, 0.5f, 0f);
					Vector3 slowSpeed = Vector3.up;
					fastSpeed *= 0.5f;
					slowSpeed *= 0.3f;
					Vector3 force = Vector3.Lerp(fastSpeed, slowSpeed, t);
					force *= 0.7f;
					pos += force;
					bounds.Encapsulate(pos);
					totalDist += force.magnitude;
					yield return genInfo;
				}
				maxDist = Mathf.Max(maxDist, totalDist);
			}
			bounds.size += Vector3.one;
			for (int x2 = 0; x2 < 8; x2++)
			{
				for (int i = 0; i < 8; i++)
				{
					WindSmoke.Node node = WindSmoke.nodes[x2, i];
					Vector3 pos2 = node.pos;
					Vector3 vector = ExtraMath.RemapValue(pos2, bounds.min, bounds.max);
					Color color = new Color(vector.x, vector.y, vector.z, node.distance / maxDist);
					this.tex.SetPixel(x2, i, color);
				}
				yield return genInfo;
			}
			this.tex.Apply();
			this.block = new MaterialPropertyBlock();
			this.block.SetTexture(WindSmoke.posTexId, this.tex);
			this.block.SetVector(WindSmoke.boundsMinId, bounds.min);
			this.block.SetVector(WindSmoke.boundsMaxId, bounds.max);
			this.block.SetFloat(WindSmoke.totalDistanceId, maxDist);
			this.block.SetFloat(WindSmoke.randomId, UnityEngine.Random.value);
			this.mr.SetPropertyBlock(this.block);
			yield return genInfo;
			yield break;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000F5B46 File Offset: 0x000F3F46
		public void SetMaterialProperty(int propertyId, float value)
		{
			this.block.SetFloat(propertyId, value);
			this.mr.SetPropertyBlock(this.block);
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000F5B66 File Offset: 0x000F3F66
		private void OnDestroy()
		{
			if (this.tex)
			{
				WindSmoke.textures.Push(this.tex);
				this.tex = null;
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000F5B90 File Offset: 0x000F3F90
		IEnumerator<GenInfo> IIslandEnter.OnIslandEnter(Island island)
		{
			this.mf.sharedMesh = WindSmoke.GetMesh();
			yield return new GenInfo("WindSmoke", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x000F5BAC File Offset: 0x000F3FAC
		IEnumerator<GenInfo> IIslandLeave.OnIslandLeave(Island island)
		{
			WindSmoke.meshes.Push(this.mf.sharedMesh);
			this.mf.sharedMesh = null;
			yield return new GenInfo("WindSmoke", GenInfo.Mode.interruptable);
			yield break;
		}

		// Token: 0x040026A6 RID: 9894
		private MeshRenderer mr;

		// Token: 0x040026A7 RID: 9895
		private MeshFilter mf;

		// Token: 0x040026A8 RID: 9896
		private static Node[,] nodes = new Node[8, 8];

		// Token: 0x040026A9 RID: 9897
		private Texture2D tex;

		// Token: 0x040026AA RID: 9898
		private const int width = 8;

		// Token: 0x040026AB RID: 9899
		private const int height = 8;

		// Token: 0x040026AC RID: 9900
		private static ShaderId posTexId = "_PosTex";

		// Token: 0x040026AD RID: 9901
		private static ShaderId boundsMinId = "_Bounds_Min";

		// Token: 0x040026AE RID: 9902
		private static ShaderId boundsMaxId = "_Bounds_Max";

		// Token: 0x040026AF RID: 9903
		private static ShaderId totalDistanceId = "_TotalDistance";

		// Token: 0x040026B0 RID: 9904
		private static ShaderId randomId = "_Random";

		// Token: 0x040026B1 RID: 9905
		private MaterialPropertyBlock block;

		// Token: 0x040026B2 RID: 9906
		private static Stack<Texture2D> textures = new Stack<Texture2D>();

		// Token: 0x040026B3 RID: 9907
		private static Stack<Mesh> meshes = new Stack<Mesh>();

		// Token: 0x02000878 RID: 2168
		public struct Node
		{
			// Token: 0x040026B4 RID: 9908
			public Vector3 pos;

			// Token: 0x040026B5 RID: 9909
			public float distance;
		}
	}
}
