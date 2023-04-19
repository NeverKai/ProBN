using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.GdcDebuggers
{
	// Token: 0x0200060E RID: 1550
	public class NavMeshDebugger : MonoBehaviour, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x0008205C File Offset: 0x0008045C
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00082069 File Offset: 0x00080469
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x00082078 File Offset: 0x00080478
		private void OnDrawGizmos()
		{
			if (!this.island)
			{
				return;
			}
			if (!this.island.navMesh)
			{
				return;
			}
			Vert[] verts = this.island.navMesh.verts;
			Edge[] edges = this.island.navMesh.edges;
			Gizmos.color = new Color(0f, 0f, 0f, 0.1f);
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			Color black = Color.black;
			if (verts == null)
			{
				return;
			}
			for (int i = 0; i < edges.Length; i++)
			{
				Edge edge = edges[i];
				black.a = ((!edge.border) ? 0.3f : ((!edge.cliff) ? 0.5f : 1f));
				Gizmos.color = black;
				Gizmos.DrawLine(edges[i].verts[0].pos, edges[i].verts[1].pos);
			}
			NavMeshDebugger.GizmoMode gizmoMode = this.gizmoMode;
			if (gizmoMode != NavMeshDebugger.GizmoMode.None)
			{
				if (gizmoMode == NavMeshDebugger.GizmoMode.Weight)
				{
					Gizmos.color = Color.white;
					foreach (Vert vert in verts)
					{
						for (int k = 0; k < vert.pipes.Count; k++)
						{
							Pipe pipe = vert.pipes[k];
							Gizmos.DrawRay(vert.pos, pipe.dir * pipe.weight * 0.5f);
						}
					}
				}
			}
		}

		// Token: 0x04001993 RID: 6547
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x04001994 RID: 6548
		[SerializeField]
		private NavMeshDebugger.GizmoMode gizmoMode;

		// Token: 0x0200060F RID: 1551
		private enum GizmoMode
		{
			// Token: 0x04001996 RID: 6550
			None,
			// Token: 0x04001997 RID: 6551
			Weight
		}
	}
}
