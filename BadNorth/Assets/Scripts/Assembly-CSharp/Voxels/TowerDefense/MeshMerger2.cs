using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200078A RID: 1930
	public class MeshMerger2 : IslandComponent, IIslandProcessor, IIslandFirstEnter, IIslandDestroy
	{
		// Token: 0x060031E2 RID: 12770 RVA: 0x000D1814 File Offset: 0x000CFC14
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			if (this.processOn == MeshMerger2.ProcessOn.OnIslandProcess)
			{
				foreach (GenInfo x in this.Process(island))
				{
					yield return x;
				}
			}
			yield break;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000D1838 File Offset: 0x000CFC38
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			if (this.processOn == MeshMerger2.ProcessOn.OnIslandFirstEnter)
			{
				foreach (GenInfo x in this.Process(island))
				{
					yield return x;
				}
			}
			yield break;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000D185C File Offset: 0x000CFC5C
		private IEnumerable<GenInfo> Process(Island island)
		{
			GenInfo genInfo = new GenInfo("MeshMerger", GenInfo.Mode.interruptable);
			SavedWave savedWave = island.savedWave;
			int srcVertCount = 0;
			int dstVertCount = 0;
			foreach (SavedWave.SavedModule savedModule2 in savedWave.dominos)
			{
				foreach (MeshFilter meshFilter in savedModule2.placement.firstModule.meshFilters)
				{
					if (meshFilter.GetComponent<MeshRenderer>().sharedMaterial == this.srcMaterial)
					{
						srcVertCount = Mathf.Max(srcVertCount, meshFilter.sharedMesh.vertexCount);
						dstVertCount += meshFilter.sharedMesh.vertexCount;
					}
				}
			}
			yield return genInfo;
			if (srcVertCount == 0)
			{
				yield break;
			}
			MeshFilter mf = base.gameObject.GetOrAddComponent<MeshFilter>();
			MeshRenderer mr = base.gameObject.GetOrAddComponent<MeshRenderer>();
			yield return genInfo;
			this.mesh = island.meshPool.GetMesh();
			mf.sharedMesh = this.mesh;
			yield return genInfo;
			List<Vector3> srcVerts = ListPool<Vector3>.GetList(srcVertCount);
			List<Vector2> srcUvs = ListPool<Vector2>.GetList(srcVertCount);
			List<Vector3> srcNorms = ListPool<Vector3>.GetList(srcVertCount);
			List<Color32> srcCols = ListPool<Color32>.GetList(srcVertCount);
			List<int> srcTris = ListPool<int>.GetList(srcVertCount * 3);
			List<Vector3> dstVerts = ListPool<Vector3>.GetList(dstVertCount);
			List<Vector2> dstUvs = ListPool<Vector2>.GetList(dstVertCount);
			List<Vector3> dstNorms = ListPool<Vector3>.GetList(dstVertCount);
			List<Color32> dstCols = ListPool<Color32>.GetList(dstVertCount);
			List<int> dstTris = ListPool<int>.GetList(dstVertCount * 3);
			yield return genInfo;
			foreach (SavedWave.SavedModule savedModule in savedWave.dominos)
			{
				foreach (MeshFilter mf2 in savedModule.orientedModule.module.meshFilters)
				{
					if (mf2.GetComponent<MeshRenderer>().sharedMaterial == this.srcMaterial)
					{
						OrientedModule orientedModule = savedModule.orientedModule;
						Matrix4x4 matrix = savedWave.module2World * Matrix4x4.Translate(savedModule.offset) * orientedModule.settings.matrix;
						Mesh srcMesh = mf2.sharedMesh;
						srcMesh.GetVertices(srcVerts);
						srcMesh.GetUVs(0, srcUvs);
						srcMesh.GetNormals(srcNorms);
						srcMesh.GetColors(srcCols);
						srcMesh.GetTriangles(srcTris, 0);
						if (orientedModule.settings.GetFlipped())
						{
							for (int num = 0; num < srcTris.Count; num += 3)
							{
								int value = srcTris[num];
								srcTris[num] = srcTris[num + 1];
								srcTris[num + 1] = value;
							}
						}
						for (int num2 = 0; num2 < srcVerts.Count; num2++)
						{
							srcVerts[num2] = matrix.MultiplyPoint(srcVerts[num2]);
							srcNorms[num2] = matrix.MultiplyVector(srcNorms[num2]);
						}
						int indexOffset = dstVerts.Count;
						for (int num3 = 0; num3 < srcTris.Count; num3++)
						{
							dstTris.Add(srcTris[num3] + indexOffset);
						}
						dstVerts.AddRange(srcVerts);
						dstUvs.AddRange(srcUvs);
						dstNorms.AddRange(srcNorms);
						dstCols.AddRange(srcCols);
						srcVerts.Clear();
						srcUvs.Clear();
						srcNorms.Clear();
						srcTris.Clear();
						yield return genInfo;
					}
				}
			}
			yield return genInfo;
			for (int i = 0; i < dstVerts.Count; i++)
			{
				List<Vector3> list;
				int index;
				(list = dstVerts)[index = i] = list[index] + island.voxelSpace.GetPosOffset(dstVerts[i]);
				yield return genInfo;
			}
			yield return genInfo;
			if (this.cullTriangles == MeshMerger2.CullTriangles.Grass || this.cullTriangles == MeshMerger2.CullTriangles.Cliffs)
			{
				bool shouldBeGrass = this.cullTriangles == MeshMerger2.CullTriangles.Grass;
				Texture2D tex = this.srcMaterial.mainTexture as Texture2D;
				for (int j = 0; j < dstTris.Count; j += 3)
				{
					bool isGrass = false;
					int num4 = 0;
					while (num4 < 3 && !isGrass)
					{
						Vector2 vector = dstUvs[dstTris[j + num4]];
						if (tex.GetPixelBilinear(vector.x, vector.y).g > 0.1f)
						{
							isGrass = true;
						}
						num4++;
					}
					if (!isGrass)
					{
						Vector2 vector2 = (dstUvs[dstTris[j]] + dstUvs[dstTris[j + 1]] + dstUvs[dstTris[j + 2]]) / 3f;
						if (tex.GetPixelBilinear(vector2.x, vector2.y).g > 0.1f)
						{
							isGrass = true;
						}
					}
					if (isGrass)
					{
						Bounds bounds = new Bounds(dstVerts[dstTris[j]], Vector3.zero);
						bounds.Encapsulate(dstVerts[dstTris[j + 1]]);
						bounds.Encapsulate(dstVerts[dstTris[j + 2]]);
						bounds.extents += new Vector3(0.02f, 0.1f, 0.02f);
						int num5 = 0;
						while (num5 < island.navManager.unreachedTris.Count && isGrass)
						{
							Tri tri = island.navManager.unreachedTris[num5];
							Bounds bounds2 = new Bounds(tri.verts[0].pos, Vector3.zero);
							bounds2.Encapsulate(tri.verts[1].pos);
							bounds2.Encapsulate(tri.verts[2].pos);
							if (bounds2.Intersects(bounds))
							{
								isGrass = false;
							}
							num5++;
						}
					}
					if (isGrass != shouldBeGrass)
					{
						dstTris.RemoveRange(j, 3);
						j -= 3;
					}
					yield return genInfo;
				}
			}
			yield return genInfo;
			if (this.cullTriangles == MeshMerger2.CullTriangles.Water)
			{
				for (int k = 0; k < dstTris.Count; k += 3)
				{
					bool isCoast = false;
					int num6 = 0;
					while (num6 < 3 && !isCoast)
					{
						if (dstUvs[dstTris[k + num6]].y > 0.1f)
						{
							isCoast = true;
						}
						num6++;
					}
					if (!isCoast)
					{
						dstTris.RemoveRange(k, 3);
						k -= 3;
					}
					yield return genInfo;
				}
			}
			yield return genInfo;
			if (this.invertTriangles)
			{
				for (int num7 = 0; num7 < dstTris.Count; num7 += 3)
				{
					int value2 = dstTris[num7];
					dstTris[num7] = dstTris[num7 + 1];
					dstTris[num7 + 1] = value2;
				}
			}
			yield return genInfo;
			if (this.invertBounds)
			{
				Bounds bounds3 = new Bounds(dstVerts[0], Vector3.zero);
				foreach (Vector3 vector3 in dstVerts)
				{
					bounds3.Encapsulate(vector3.SetY(-vector3.y));
				}
				bounds3.extents += Vector3.one * 0.1f;
				this.mesh.bounds = bounds3;
			}
			else
			{
				Bounds bounds4 = new Bounds(dstVerts[0], Vector3.zero);
				for (int num8 = 0; num8 < dstVerts.Count; num8++)
				{
					bounds4.Encapsulate(dstVerts[num8]);
				}
				bounds4.extents += Vector3.one * 0.1f;
				this.mesh.bounds = bounds4;
			}
			yield return genInfo;
			this.mesh.SetVertices(dstVerts);
			this.mesh.SetUVs(0, dstUvs);
			this.mesh.SetNormals(dstNorms);
			this.mesh.SetColors(dstCols);
			this.mesh.SetTriangles(dstTris, 0);
			yield return genInfo;
			if (this.tangentMode != MeshMerger2.TangentMode.None)
			{
				List<Vector4> dstTans = ListPool<Vector4>.GetList(dstVertCount);
				if (this.tangentMode == MeshMerger2.TangentMode.SoftNormals)
				{
					for (int num9 = 0; num9 < dstVerts.Count; num9++)
					{
						dstTans.Add(Vector4.zero);
					}
					for (int l = 0; l < dstTris.Count; l += 3)
					{
						Vector3 v0 = dstVerts[dstTris[l]];
						Vector3 v = dstVerts[dstTris[l + 1]];
						Vector3 v2 = dstVerts[dstTris[l + 2]];
						Vector3 dir = v - v0;
						Vector3 tan = v2 - v0;
						if (!(dir == Vector3.zero))
						{
							if (!(tan == Vector3.zero))
							{
								Vector3 normal = Vector3.Cross(tan, dir).normalized;
								float dirLength = dir.magnitude;
								float height = Vector3.Dot(tan, Vector3.Cross(dir / dirLength, normal));
								float area = dirLength * height * 0.5f;
								normal *= area;
								Vector4 tangent = normal.SetW(0f);
								for (int num10 = 0; num10 < 3; num10++)
								{
									for (int num11 = 0; num11 < dstVerts.Count; num11++)
									{
										if (dstVerts[num11] == dstVerts[dstTris[l + num10]])
										{
											List<Vector4> list2;
											int index2;
											(list2 = dstTans)[index2 = num11] = list2[index2] + tangent;
										}
									}
								}
								yield return genInfo;
							}
						}
					}
					for (int num12 = 0; num12 < dstVerts.Count; num12++)
					{
						dstTans[num12] = dstTans[num12].normalized;
					}
				}
				else if (this.tangentMode == MeshMerger2.TangentMode.VoxelsNormals)
				{
					for (int m = 0; m < dstVerts.Count; m++)
					{
						Vector4 sample = island.voxelSpace.GetNormalLinear(dstVerts[m]);
						Vector3 n = sample.normalized;
						dstTans.Add(new Vector4(n.x, n.y, n.z, sample.w));
						yield return genInfo;
					}
				}
				this.mesh.SetTangents(dstTans);
				dstTans.ReturnToListPool<Vector4>();
			}
			yield return genInfo;
			int key = base.transform.GetSiblingIndex();
			Material material;
			if (!MeshMerger2.materialDict.TryGetValue(key, out material))
			{
				material = UnityEngine.Object.Instantiate<Material>((!this.overrideMaterial) ? this.srcMaterial : this.overrideMaterial);
				material.SetFloat("_Stencil", (float)this.stencil);
				for (int num13 = 0; num13 < this.keywords.Length; num13++)
				{
					material.EnableKeyword(this.keywords[num13]);
				}
				material.EnableKeyword("_STATIC_ON");
				MeshMerger2.materialDict.Add(key, material);
			}
			mr.sharedMaterial = material;
			yield return genInfo;
			srcVerts.ReturnToListPool<Vector3>();
			srcUvs.ReturnToListPool<Vector2>();
			srcNorms.ReturnToListPool<Vector3>();
			srcCols.ReturnToListPool<Color32>();
			srcTris.ReturnToListPool<int>();
			dstVerts.ReturnToListPool<Vector3>();
			dstUvs.ReturnToListPool<Vector2>();
			dstNorms.ReturnToListPool<Vector3>();
			dstCols.ReturnToListPool<Color32>();
			dstTris.ReturnToListPool<int>();
			yield return genInfo;
			yield break;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000D1886 File Offset: 0x000CFC86
		[ContextMenu("SetupName")]
		private void SetupName()
		{
			base.name = this.srcMaterial.name + " " + string.Join(" ", this.keywords);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000D18B3 File Offset: 0x000CFCB3
		void IIslandDestroy.OnIslandDestroy(Island island)
		{
			island.meshPool.ReturnMesh(ref this.mesh);
		}

		// Token: 0x040021B5 RID: 8629
		private static Dictionary<int, Material> materialDict = new Dictionary<int, Material>(8);

		// Token: 0x040021B6 RID: 8630
		[SerializeField]
		private Material srcMaterial;

		// Token: 0x040021B7 RID: 8631
		[SerializeField]
		private Material overrideMaterial;

		// Token: 0x040021B8 RID: 8632
		[SerializeField]
		private string[] keywords;

		// Token: 0x040021B9 RID: 8633
		[SerializeField]
		private MeshMerger2.ProcessOn processOn;

		// Token: 0x040021BA RID: 8634
		[SerializeField]
		private MeshMerger2.CullTriangles cullTriangles;

		// Token: 0x040021BB RID: 8635
		[SerializeField]
		private MeshMerger2.TangentMode tangentMode;

		// Token: 0x040021BC RID: 8636
		[SerializeField]
		private bool invertTriangles;

		// Token: 0x040021BD RID: 8637
		[SerializeField]
		private bool invertBounds;

		// Token: 0x040021BE RID: 8638
		private int key;

		// Token: 0x040021BF RID: 8639
		public Mesh mesh;

		// Token: 0x040021C0 RID: 8640
		[SerializeField]
		private int stencil;

		// Token: 0x0200078B RID: 1931
		private enum ProcessOn
		{
			// Token: 0x040021C2 RID: 8642
			OnIslandFirstEnter,
			// Token: 0x040021C3 RID: 8643
			OnIslandProcess
		}

		// Token: 0x0200078C RID: 1932
		private enum CullTriangles
		{
			// Token: 0x040021C5 RID: 8645
			All,
			// Token: 0x040021C6 RID: 8646
			Grass,
			// Token: 0x040021C7 RID: 8647
			Cliffs,
			// Token: 0x040021C8 RID: 8648
			Water
		}

		// Token: 0x0200078D RID: 1933
		private enum TangentMode
		{
			// Token: 0x040021CA RID: 8650
			None,
			// Token: 0x040021CB RID: 8651
			VoxelsNormals,
			// Token: 0x040021CC RID: 8652
			SoftNormals
		}
	}
}
