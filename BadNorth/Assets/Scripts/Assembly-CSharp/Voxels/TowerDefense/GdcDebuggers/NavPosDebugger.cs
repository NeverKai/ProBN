using System;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense.GdcDebuggers
{
	// Token: 0x02000610 RID: 1552
	public class NavPosDebugger : MonoBehaviour, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x00082246 File Offset: 0x00080646
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00082253 File Offset: 0x00080653
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00082264 File Offset: 0x00080664
		private void Update()
		{
			if (!this.island)
			{
				return;
			}
			if (!this.island.navMesh)
			{
				return;
			}
			if (this.island.navMesh.tris == null)
			{
				return;
			}
			this.navPos = new NavPos(this.island.navMesh, base.transform.position, true, 0.1f);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000822D8 File Offset: 0x000806D8
		private void OnDrawGizmos()
		{
			if (!this.navPos.valid)
			{
				return;
			}
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			Tri tri = this.navPos.tri;
			Gizmos.color = Color.white;
			for (int i = 0; i < 3; i++)
			{
				Edge edge = tri.edges[i];
				Gizmos.DrawLine(edge.vert0.pos, edge.vert1.pos);
			}
			Gizmos.color = Color.white;
			switch (this.gizmoMode)
			{
			case NavPosDebugger.GizmoMode.BarycentricCoordinates:
			{
				float d = 0.5f;
				Gizmos.color = Color.black;
				for (int j = 0; j < 3; j++)
				{
					Vert vert = tri.verts[j];
					Gizmos.DrawRay(vert.pos, Vector3.up * d);
				}
				Gizmos.color = Color.white;
				for (int k = 0; k < 3; k++)
				{
					Vert vert2 = tri.verts[k];
					Gizmos.DrawRay(vert2.pos, Vector3.up * d * this.navPos.bary.GetComponent(k));
				}
				break;
			}
			case NavPosDebugger.GizmoMode.BarycentricStrings:
				for (int l = 0; l < 3; l++)
				{
					Vert vert3 = tri.verts[l];
					Gizmos.color = Color.gray.SetComponent(l, 1f);
					Gizmos.DrawLine(vert3.pos, this.navPos.pos);
					Gizmos.DrawRay(vert3.pos, Vector3.up * 0.5f * this.navPos.bary.GetComponent(l));
				}
				break;
			case NavPosDebugger.GizmoMode.BarycentricVectors:
				for (int m = 0; m < 3; m++)
				{
					Vert vert4 = tri.verts[m];
					Gizmos.color = Color.gray.SetComponent(m, 1f);
					Vector3 vector = vert4.pos - tri.acrossEdge[m] * (1f - this.navPos.bary.GetComponent(m)) * tri.acrossEdgeLength[m];
					vector = tri.ClampToPlane(vector);
					Gizmos.DrawLine(vector, vert4.pos);
					Gizmos.DrawLine(vector, this.navPos.pos);
					Gizmos.color *= 2f;
					Gizmos.DrawSphere(vector, 0.02f);
				}
				break;
			case NavPosDebugger.GizmoMode.TriFlowEnglish:
			case NavPosDebugger.GizmoMode.TriFlowViking:
			{
				Faction faction = (this.gizmoMode != NavPosDebugger.GizmoMode.TriFlowEnglish) ? this.island.vikings : this.island.english;
				FlowField presence = faction.presence;
				for (int n = 0; n < 3; n++)
				{
					Vert vert5 = tri.verts[n];
					Data data = presence.SampleData(vert5);
					Gizmos.color = ((!data.agent) ? faction.color : data.agent.uniqueDebugColor);
					Gizmos.DrawRay(vert5.pos, presence.SampleDirection(vert5) * 0.5f);
				}
				Data data2 = presence.SampleData(this.navPos);
				Gizmos.color = ((!data2.agent) ? faction.color : data2.agent.uniqueDebugColor);
				Gizmos.DrawRay(this.navPos.pos, presence.SampleDirection(this.navPos) * 0.5f);
				Gizmos.color = Color.white;
				Agent agent = data2.agent;
				if (agent)
				{
					Gizmos.DrawLine(agent.transform.position, this.navPos.pos);
				}
				break;
			}
			}
			Gizmos.color = Color.white * 2f;
			Gizmos.DrawSphere(this.navPos.pos, 0.02f);
			Gizmos.DrawLine(this.navPos.pos, base.transform.position);
			Gizmos.DrawSphere(base.transform.position, 0.04f);
		}

		// Token: 0x04001998 RID: 6552
		[SerializeField]
		private NavPosDebugger.GizmoMode gizmoMode;

		// Token: 0x04001999 RID: 6553
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x0400199A RID: 6554
		private NavPos navPos;

		// Token: 0x02000611 RID: 1553
		private enum GizmoMode
		{
			// Token: 0x0400199C RID: 6556
			None,
			// Token: 0x0400199D RID: 6557
			BarycentricCoordinates,
			// Token: 0x0400199E RID: 6558
			BarycentricStrings,
			// Token: 0x0400199F RID: 6559
			BarycentricVectors,
			// Token: 0x040019A0 RID: 6560
			TriFlowEnglish,
			// Token: 0x040019A1 RID: 6561
			TriFlowViking
		}
	}
}
