using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000654 RID: 1620
	[RequireComponent(typeof(NavigationMesh))]
	public class NavigationManager : IslandComponent, IIslandProcessor
	{
		// Token: 0x06002910 RID: 10512 RVA: 0x0008D98C File Offset: 0x0008BD8C
		private void onUnreacables(List<Tri> tris)
		{
			this.unreachedTris = new List<Tri>(tris);
			this.undreachedVerts = new List<Vert>(tris.Count * 3);
			foreach (Tri tri in this.unreachedTris)
			{
				if (!this.undreachedVerts.Contains(tri.verts.x))
				{
					this.undreachedVerts.Add(tri.verts.x);
				}
				if (!this.undreachedVerts.Contains(tri.verts.y))
				{
					this.undreachedVerts.Add(tri.verts.y);
				}
				if (!this.undreachedVerts.Contains(tri.verts.z))
				{
					this.undreachedVerts.Add(tri.verts.z);
				}
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0008DA94 File Offset: 0x0008BE94
		IEnumerator<GenInfo> IIslandProcessor.OnIslandProcess(Island island, SavedWave savedWave)
		{
			List<Vector3> verts0 = ListPool<Vector3>.GetList(32);
			List<Vector3> verts = ListPool<Vector3>.GetList(1024);
			List<int> tris0 = ListPool<int>.GetList(64);
			List<int> tris = ListPool<int>.GetList(2048);
			this.navigationMesh.onUnreacables += this.onUnreacables;
			for (int i = 0; i < savedWave.dominos.Count; i++)
			{
				yield return new GenInfo("Merging Meshes", GenInfo.Mode.interruptable);
				SavedWave.SavedModule savedModule = savedWave.dominos[i];
				OrientedModule orientedModule = savedModule.orientedModule;
				for (int j = 0; j < orientedModule.module.meshFilters.Count; j++)
				{
					MeshFilter meshFilter = orientedModule.module.meshFilters[j];
					MeshRenderer component = meshFilter.GetComponent<MeshRenderer>();
					if (component && !(component.sharedMaterial != this.navMaterial))
					{
						Matrix4x4 matrix4x = savedWave.module2World * Matrix4x4.Translate(savedModule.offset) * orientedModule.settings.matrix;
						meshFilter.sharedMesh.GetVertices(verts0);
						meshFilter.sharedMesh.GetTriangles(tris0, 0);
						for (int k = 0; k < verts0.Count; k++)
						{
							verts0[k] = matrix4x.MultiplyPoint(verts0[k]);
						}
						for (int l = 0; l < tris0.Count; l++)
						{
							List<int> list;
							int index;
							(list = tris0)[index = l] = list[index] + verts.Count;
						}
						if (orientedModule.settings.GetFlipped())
						{
							for (int m = 0; m < tris0.Count; m += 3)
							{
								int value = tris0[m];
								tris0[m] = tris0[m + 1];
								tris0[m + 1] = value;
							}
						}
						verts.AddRange(verts0);
						tris.AddRange(tris0);
						verts0.Clear();
						tris0.Clear();
					}
				}
				yield return new GenInfo("NavMesh", GenInfo.Mode.interruptable);
			}
			Vector2 add = island.size.GetXZ() / 2f;
			for (int n = 0; n < verts.Count; n++)
			{
				Vector3 vector = verts[n];
				Vector3 vector2 = vector;
				vector2 += island.voxelSpace.GetPosOffset(vector2);
				vector2.y = Mathf.Max(0.027f, vector2.y);
				Vector2 vector3 = vector.GetXZ() + add;
				Vector2 b = ExtraMath.Round(vector3);
				if ((vector3 - b).sqrMagnitude < 0.001f)
				{
					vector2 = new Vector3(vector.x, vector2.y, vector.z);
				}
				verts[n] = vector2;
			}
			IEnumerator<GenInfo> setupRoutine = this.navigationMesh.Setup(verts, tris, island);
			while (setupRoutine.MoveNext())
			{
				GenInfo genInfo = setupRoutine.Current;
				yield return genInfo;
			}
			ListPool<Vector3>.ReturnList(verts0);
			ListPool<Vector3>.ReturnList(verts);
			ListPool<int>.ReturnList(tris0);
			ListPool<int>.ReturnList(tris);
			float potentialBeachLength = 0f;
			foreach (Edge edge in this.navigationMesh.edges)
			{
				if (edge.beach)
				{
					potentialBeachLength += edge.length;
				}
			}
			if (potentialBeachLength < island.levelNode.minimumBeach)
			{
				yield return new GenInfo("Not enough potential beach!", GenInfo.Mode.broken);
			}
			yield return new GenInfo("NavMesh", GenInfo.Mode.forceInterrupt);
			yield break;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0008DAC0 File Offset: 0x0008BEC0
		public void ScoreWave(MultiWave multiWave, ref float score, ref bool valid)
		{
			if (this.navigationMesh.reachRatio < 0.5f)
			{
				valid = false;
				Debug.LogError("Reach ratio too low: " + this.navigationMesh.reachRatio);
			}
			score += ExtraMath.RemapValue(this.navigationMesh.reachRatio, 0f, 0.92f, -10f, 10f);
			score += this.navigationMesh.bounds.size.y;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0008DB4A File Offset: 0x0008BF4A
		private void OnDestroy()
		{
			this.navigationMesh = null;
			this.unreachedTris = null;
			this.undreachedVerts = null;
		}

		// Token: 0x04001AC1 RID: 6849
		public NavigationMesh navigationMesh;

		// Token: 0x04001AC2 RID: 6850
		[SerializeField]
		public Material navMaterial;

		// Token: 0x04001AC3 RID: 6851
		public List<Vert> undreachedVerts;

		// Token: 0x04001AC4 RID: 6852
		public List<Tri> unreachedTris;
	}
}
