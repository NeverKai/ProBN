using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000624 RID: 1572
	public class MeshMerger : IslandComponent, IIslandProcessor, IIslandFirstEnter
	{
		// Token: 0x06002862 RID: 10338 RVA: 0x00085C03 File Offset: 0x00084003
		private static Mesh GetMesh()
		{
			return (MeshMerger.meshStack.Count <= 0) ? new Mesh() : MeshMerger.meshStack.Pop();
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x00085C2C File Offset: 0x0008402C
		private static Material GetMaterial(Material srcMaterial, string keyword, string floatword)
		{
			int key = keyword.GetHashCode() + srcMaterial.GetHashCode();
			Material material;
			if (!MeshMerger.materialDict.TryGetValue(key, out material))
			{
				material = UnityEngine.Object.Instantiate<Material>(srcMaterial);
				material.name = string.Format("{0} {1}", srcMaterial.name, keyword);
				material.EnableKeyword(keyword);
				material.SetFloat(floatword, 1f);
				MeshMerger.materialDict.Add(key, material);
			}
			return material;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00085C98 File Offset: 0x00084098
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			this.container = new GameObject("Mesh Merger Container").transform;
			this.container.SetParent(base.transform);
			Dictionary<Material, List<CombineInstance>> combines = new Dictionary<Material, List<CombineInstance>>();
			for (int i = 0; i < savedWave.dominos.Count; i++)
			{
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				SavedWave.SavedModule savedModule = savedWave.dominos[i];
				OrientedModule orientedModule = savedModule.orientedModule;
				if (orientedModule.module.meshFilters.Count > 0)
				{
					Matrix4x4 transform = savedWave.module2World * Matrix4x4.Translate(savedModule.offset) * orientedModule.settings.matrix;
					for (int l = 0; l < orientedModule.module.meshFilters.Count; l++)
					{
						MeshFilter meshFilter = orientedModule.module.meshFilters[l];
						MeshRenderer component = meshFilter.GetComponent<MeshRenderer>();
						if (component)
						{
							if (!(component.sharedMaterial == island.navManager.navMaterial))
							{
								List<CombineInstance> list;
								if (!combines.TryGetValue(component.sharedMaterial, out list))
								{
									list = new List<CombineInstance>();
									combines.Add(component.sharedMaterial, list);
								}
								list.Add(new CombineInstance
								{
									mesh = meshFilter.sharedMesh,
									transform = transform
								});
							}
						}
					}
				}
			}
			yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
			KeyValuePair<Material, List<CombineInstance>>[] materialsCombines = combines.ToArray<KeyValuePair<Material, List<CombineInstance>>>();
			for (int j = 0; j < materialsCombines.Length; j++)
			{
				Mesh mesh = MeshMerger.GetMesh();
				mesh.CombineMeshes(materialsCombines[j].Value.ToArray());
				Mesh mirroredMesh = MeshMerger.GetMesh();
				mirroredMesh.CombineMeshes(materialsCombines[j].Value.ToArray());
				IEnumerator<GenInfo> softenRoutine = this.SoftenMesh(mesh, mirroredMesh);
				while (softenRoutine.MoveNext())
				{
					GenInfo genInfo = softenRoutine.Current;
					yield return genInfo;
				}
				this.meshes.Add(mesh);
				this.meshes.Add(mirroredMesh);
				IEnumerator tangentRoutine = mirroredMesh.SoftNormalsInTangetsEnumerator();
				while (tangentRoutine.MoveNext())
				{
					yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				}
				List<int> tris = ListPool<int>.GetList(2048);
				mirroredMesh.GetTriangles(tris, 0);
				for (int m = 0; m < tris.Count; m += 3)
				{
					int value = tris[m];
					tris[m] = tris[m + 1];
					tris[m + 1] = value;
				}
				mirroredMesh.SetTriangles(tris, 0);
				ListPool<int>.ReturnList(tris);
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				Material srcMaterial = materialsCombines[j].Key;
				GameObject pObj = new GameObject();
				pObj.transform.SetParent(this.container, false);
				this.gameObjects.Add(pObj);
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				GameObject gameObject = new GameObject();
				gameObject.layer = LayerMaster.moduleLayer.id;
				gameObject.transform.SetParent(pObj.transform, false);
				MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
				MeshFilter meshFilter2 = gameObject.AddComponent<MeshFilter>();
				this.meshRenderers.Add(meshRenderer);
				meshRenderer.sharedMaterial = MeshMerger.GetMaterial(srcMaterial, "_STATIC_ON", "_Static");
				meshFilter2.sharedMesh = mesh;
				pObj.gameObject.SetActive(false);
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				if (srcMaterial.HasProperty(MeshMerger.mirrorId))
				{
					GameObject gameObject2 = new GameObject();
					gameObject2.transform.SetParent(pObj.transform, false);
					MeshRenderer meshRenderer2 = gameObject2.AddComponent<MeshRenderer>();
					MeshFilter meshFilter3 = gameObject2.AddComponent<MeshFilter>();
					meshRenderer2.sharedMaterial = MeshMerger.GetMaterial(srcMaterial, "_MIRROR_ON", "_Mirror");
					meshFilter3.sharedMesh = mirroredMesh;
					gameObject2.layer = LayerMaster.mirror.id;
				}
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				if (srcMaterial == this.landMaterial)
				{
					GameObject mObj = new GameObject();
					mObj.transform.SetParent(pObj.transform, false);
					MeshRenderer mr = mObj.AddComponent<MeshRenderer>();
					MeshFilter mf = mObj.AddComponent<MeshFilter>();
					mr.sharedMaterial = MeshMerger.GetMaterial(srcMaterial, "_OUTLINE_ON", "_Outline");
					mf.sharedMesh = mirroredMesh;
					yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
					this.land = new MeshMerger.Land(pObj);
				}
				else if (srcMaterial == this.waterMaterial)
				{
					Mesh fogMesh = MeshMerger.GetMesh();
					this.meshes.Add(fogMesh);
					List<Vector3> verts = ListPool<Vector3>.GetList(1024);
					List<Vector2> uvs = ListPool<Vector2>.GetList(1024);
					tris = ListPool<int>.GetList(2048);
					mesh.GetVertices(verts);
					mesh.GetTriangles(tris, 0);
					mesh.GetUVs(0, uvs);
					int startTriCount = tris.Count;
					int startVertCount = verts.Count;
					Vector3 offset0 = new Vector3(island.size.x / 2f - 0.04f, verts[0].y, island.size.z / 2f - 0.04f);
					Vector3 offset = new Vector3(0f, verts[0].y, island.size.x * 30f);
					for (int n = 0; n < 4; n++)
					{
						verts.Add(offset0);
						verts.Add(offset);
						tris.Add(startVertCount + n * 2 % 8);
						tris.Add(startVertCount + (n * 2 + 1) % 8);
						tris.Add(startVertCount + (n * 2 + 3) % 8);
						tris.Add(startVertCount + n * 2 % 8);
						tris.Add(startVertCount + (n * 2 + 3) % 8);
						tris.Add(startVertCount + (n * 2 + 2) % 8);
						offset0 = new Vector3(offset0.z, offset0.y, -offset0.x);
						offset = new Vector3(offset.z, offset.y, -offset.x);
					}
					fogMesh.SetVertices(verts);
					fogMesh.SetTriangles(tris, 0);
					GameObject mObj2 = this.container.AddEmptyChild("Fog").gameObject;
					MeshRenderer mr2 = mObj2.AddComponent<MeshRenderer>();
					MeshFilter mf2 = mObj2.AddComponent<MeshFilter>();
					mObj2.layer = LayerMaster.backgroundLayer.id;
					mr2.sharedMaterial = this.fogMaterial;
					mf2.sharedMesh = fogMesh;
					this.meshRenderers.Add(mr2);
					tris.RemoveRange(startTriCount, tris.Count - startTriCount);
					for (int k = tris.Count - 3; k >= 0; k -= 3)
					{
						if (uvs[tris[k]].y < 0.1f && uvs[tris[k + 1]].y < 0.1f && uvs[tris[k + 2]].y < 0.1f)
						{
							tris.RemoveRange(k, 3);
						}
						yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
					}
					mesh.SetTriangles(tris, 0);
					ListPool<Vector3>.ReturnList(verts);
					ListPool<Vector2>.ReturnList(uvs);
					ListPool<int>.ReturnList(tris);
				}
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
			}
			yield break;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00085CC4 File Offset: 0x000840C4
		public IEnumerator<GenInfo> BakeTangent(Mesh mesh)
		{
			List<Vector3> verts = ListPool<Vector3>.GetList(4096);
			List<Vector4> tans = ListPool<Vector4>.GetList(4096);
			mesh.GetVertices(verts);
			for (int i = 0; i < verts.Count; i++)
			{
				Vector4 sample = base.island.voxelSpace.GetNormalLinear(verts[i]);
				Vector3 j = sample.normalized;
				tans.Add(new Vector4(j.x, j.y, j.z, sample.w));
				if (i % 100 == 0)
				{
					yield return new GenInfo("SoftenTangents", GenInfo.Mode.interruptable);
				}
			}
			mesh.SetTangents(tans);
			ListPool<Vector3>.ReturnList(verts);
			ListPool<Vector4>.ReturnList(tans);
			yield break;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00085CE8 File Offset: 0x000840E8
		public IEnumerator<GenInfo> SoftenMesh(Mesh mesh0, Mesh mesh1)
		{
			List<Vector3> verts = ListPool<Vector3>.GetList(4096);
			List<Vector4> tans = ListPool<Vector4>.GetList(4096);
			mesh0.GetVertices(verts);
			for (int i = 0; i < verts.Count; i++)
			{
				Vector3 offset = base.island.voxelSpace.GetPosOffset(verts[i]);
				List<Vector3> list;
				int index;
				(list = verts)[index = i] = list[index] + offset;
				Vector3 j;
				float openness;
				base.island.voxelSpace.SampleNormalOpenness(verts[i], out j, out openness);
				tans.Add(new Vector4(0f, 0f, 0f, openness));
				if (i % 40 == 0)
				{
					yield return new GenInfo("SoftenTangents", GenInfo.Mode.interruptable);
				}
			}
			mesh0.SetTangents(tans);
			mesh0.SetVertices(verts);
			mesh1.SetVertices(verts);
			ListPool<Vector3>.ReturnList(verts);
			ListPool<Vector4>.ReturnList(tans);
			yield break;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x00085D14 File Offset: 0x00084114
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			IEnumerator<GenInfo> routine = this.BakeTangent(this.land.meshes[0]);
			while (routine.MoveNext())
			{
				GenInfo genInfo = routine.Current;
				yield return genInfo;
			}
			yield break;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x00085D30 File Offset: 0x00084130
		private void OnDestroy()
		{
			foreach (Mesh mesh in this.meshes)
			{
				mesh.Clear();
				MeshMerger.meshStack.Push(mesh);
			}
			this.meshes.Clear();
			this.meshRenderers.Clear();
		}

		// Token: 0x040019E4 RID: 6628
		private static Dictionary<int, Material> materialDict = new Dictionary<int, Material>();

		// Token: 0x040019E5 RID: 6629
		private static Stack<Mesh> meshStack = new Stack<Mesh>();

		// Token: 0x040019E6 RID: 6630
		private List<GameObject> gameObjects = new List<GameObject>();

		// Token: 0x040019E7 RID: 6631
		private List<Mesh> meshes = new List<Mesh>(6);

		// Token: 0x040019E8 RID: 6632
		public List<MeshRenderer> meshRenderers = new List<MeshRenderer>(3);

		// Token: 0x040019E9 RID: 6633
		public MeshMerger.Land land;

		// Token: 0x040019EA RID: 6634
		public Transform container;

		// Token: 0x040019EB RID: 6635
		public Material fogMaterial;

		// Token: 0x040019EC RID: 6636
		public Material waterMaterial;

		// Token: 0x040019ED RID: 6637
		public Material landMaterial;

		// Token: 0x040019EE RID: 6638
		private static ShaderId mirrorId = "_Mirror";

		// Token: 0x02000625 RID: 1573
		public class Land
		{
			// Token: 0x0600286A RID: 10346 RVA: 0x00085DD4 File Offset: 0x000841D4
			public Land(GameObject baseObj)
			{
				foreach (MeshRenderer mr in baseObj.GetComponentsInChildren<MeshRenderer>())
				{
					MeshMerger.Land.Obj obj = new MeshMerger.Land.Obj(mr);
					if (!this.meshes.Contains(obj.mf.sharedMesh))
					{
						this.meshes.Add(obj.mf.sharedMesh);
					}
					this.objs.Add(obj);
				}
			}

			// Token: 0x040019EF RID: 6639
			public List<Mesh> meshes = new List<Mesh>();

			// Token: 0x040019F0 RID: 6640
			public List<MeshMerger.Land.Obj> objs = new List<MeshMerger.Land.Obj>();

			// Token: 0x02000626 RID: 1574
			public class Obj
			{
				// Token: 0x0600286B RID: 10347 RVA: 0x00085E60 File Offset: 0x00084260
				public Obj(MeshRenderer mr)
				{
					this.mr = mr;
					this.mf = mr.gameObject.GetComponent<MeshFilter>();
				}

				// Token: 0x040019F1 RID: 6641
				public MeshRenderer mr;

				// Token: 0x040019F2 RID: 6642
				public MeshFilter mf;
			}
		}
	}
}
