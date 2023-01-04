using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000788 RID: 1928
	public class FogMesh : IslandComponent, IIslandFirstEnter
	{
		// Token: 0x060031DF RID: 12767 RVA: 0x000D14CC File Offset: 0x000CF8CC
		IEnumerator<GenInfo> IIslandFirstEnter.OnIslandFirstEnter(Island island)
		{
			Mesh mesh = base.GetComponentInParent<MeshMerger2>().mesh;
			List<Vector3> verts = ListPool<Vector3>.GetList(mesh.vertexCount + 8);
			List<int> tris = ListPool<int>.GetList(mesh.vertexCount * 2);
			mesh.GetVertices(verts);
			mesh.GetTriangles(tris, 0);
			mesh.Clear();
			int startTriCount = tris.Count;
			int startVertCount = verts.Count;
			Vector3 offset0 = new Vector3(island.size.x / 2f - 0.04f, verts[0].y, island.size.z / 2f - 0.04f);
			Vector3 offset = new Vector3(0f, verts[0].y, island.size.x * 30f);
			for (int i = 0; i < 4; i++)
			{
				verts.Add(offset0);
				verts.Add(offset);
				tris.Add(startVertCount + i * 2 % 8);
				tris.Add(startVertCount + (i * 2 + 1) % 8);
				tris.Add(startVertCount + (i * 2 + 3) % 8);
				tris.Add(startVertCount + i * 2 % 8);
				tris.Add(startVertCount + (i * 2 + 3) % 8);
				tris.Add(startVertCount + (i * 2 + 2) % 8);
				offset0 = new Vector3(offset0.z, offset0.y, -offset0.x);
				offset = new Vector3(offset.z, offset.y, -offset.x);
			}
			mesh.SetVertices(verts);
			mesh.SetTriangles(tris, 0);
			verts.ReturnToListPool<Vector3>();
			tris.ReturnToListPool<int>();
			yield return new GenInfo("FogMesh", GenInfo.Mode.interruptable);
			yield break;
		}
	}
}
