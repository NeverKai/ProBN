using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200061C RID: 1564
	public class IslandMeshColliders : IslandComponent, IIslandProcessor, IIslandDestroy
	{
		// Token: 0x0600283E RID: 10302 RVA: 0x000849B8 File Offset: 0x00082DB8
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			List<int> tris = ListPool<int>.GetList(256);
			List<Vector3> verts = ListPool<Vector3>.GetList(128);
			for (int i = 0; i < savedWave.dominos.Count; i++)
			{
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				SavedWave.SavedModule savedModule = savedWave.dominos[i];
				OrientedModule orientedModule = savedModule.orientedModule;
				for (int j = 0; j < orientedModule.module.meshFilters.Count; j++)
				{
					MeshFilter mf = orientedModule.module.meshFilters[j];
					MeshRenderer mr = mf.GetComponent<MeshRenderer>();
					if (mr && !(mr.sharedMaterial != this.colliderMaterial))
					{
						mf.sharedMesh.GetTriangles(tris, 0);
						mf.sharedMesh.GetVertices(verts);
						Matrix4x4 matrix = savedWave.module2World * Matrix4x4.Translate(savedModule.offset) * orientedModule.settings.matrix;
						for (int k = 0; k < verts.Count; k++)
						{
							Vector3 v = verts[k];
							v = matrix.MultiplyPoint(v);
							v += island.voxelSpace.GetPosOffset(v);
							yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
							verts[k] = v;
						}
						if (orientedModule.settings.GetFlipped())
						{
							for (int l = 0; l < tris.Count; l += 3)
							{
								int value = tris[l];
								tris[l] = tris[l + 1];
								tris[l + 1] = value;
							}
						}
						Mesh mesh = island.meshPool.GetMesh();
						mesh.Clear();
						mesh.SetVertices(verts);
						mesh.SetTriangles(tris, 0);
						verts.Clear();
						tris.Clear();
						mesh.RecalculateBounds();
						MeshCollider collider = base.gameObject.AddComponent<MeshCollider>();
						collider.sharedMesh = mesh;
						this.colliders.Add(collider);
					}
				}
			}
			ListPool<int>.ReturnList(tris);
			ListPool<Vector3>.ReturnList(verts);
			yield return new GenInfo("Merging Meshes", GenInfo.Mode.forceInterrupt);
			yield break;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000849E4 File Offset: 0x00082DE4
		void IIslandDestroy.OnIslandDestroy(Island island)
		{
			for (int i = 0; i < this.colliders.Count; i++)
			{
				Mesh sharedMesh = this.colliders[i].sharedMesh;
				island.meshPool.ReturnMesh(ref sharedMesh);
				this.colliders[i].sharedMesh = null;
				this.colliders[i] = null;
			}
			this.colliders.Clear();
		}

		// Token: 0x040019C8 RID: 6600
		[SerializeField]
		private List<MeshCollider> colliders = new List<MeshCollider>();

		// Token: 0x040019C9 RID: 6601
		public Material colliderMaterial;
	}
}
