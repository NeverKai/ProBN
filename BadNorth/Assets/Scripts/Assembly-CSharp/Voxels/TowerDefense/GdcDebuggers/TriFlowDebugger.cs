using System;
using System.Collections.Generic;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense.GdcDebuggers
{
	// Token: 0x02000612 RID: 1554
	public class TriFlowDebugger : MonoBehaviour, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x0008275C File Offset: 0x00080B5C
		private Island island
		{
			get
			{
				return this._island;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x00082769 File Offset: 0x00080B69
		private bool useAgentColors
		{
			get
			{
				return this.colorMode == TriFlowDebugger.ColorMode.AgentColors;
			}
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00082774 File Offset: 0x00080B74
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this._island.Target = island;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00082784 File Offset: 0x00080B84
		public void OnDrawGizmos()
		{
			if (!this.island)
			{
				return;
			}
			Faction faction = (this.side != Faction.Side.English) ? this.island.vikings : this.island.english;
			FlowField presence = faction.presence;
			NavigationMesh navMesh = this.island.navMesh;
			Color color = faction.color;
			if (presence == null || presence.flowContents == null)
			{
				return;
			}
			Content[] flowContents = presence.flowContents;
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			switch (this.gizmoMode)
			{
			case TriFlowDebugger.GizmoMode.Distance:
			{
				List<Vert> list = new List<Vert>();
				for (int i = 0; i < navMesh.tris.Length; i++)
				{
					Tri tri = navMesh.tris[i];
					list.Clear();
					list.Add(tri.verts.x);
					list.Add(tri.verts.y);
					list.Add(tri.verts.z);
					list.Sort((Vert a, Vert b) => flowContents[(int)a.index].distance.CompareTo(flowContents[(int)b.index].distance));
					float num = flowContents[(int)list[0].index].distance;
					float num2 = flowContents[(int)list[1].index].distance;
					float num3 = flowContents[(int)list[2].index].distance;
					if (num != num3)
					{
						if (num3 - num <= 4f)
						{
							num = Mathf.Min(num * 3f, Mathf.Sqrt(num * 20f));
							num2 = Mathf.Min(num2 * 3f, Mathf.Sqrt(num2 * 20f));
							num3 = Mathf.Min(num3 * 3f, Mathf.Sqrt(num3 * 20f));
							for (int j = Mathf.CeilToInt(num); j <= Mathf.FloorToInt(num3); j++)
							{
								Vector3 vector = Vector3.Lerp(list[0].pos, list[2].pos, ExtraMath.RemapValue((float)j, num, num3));
								Vector3 vector2;
								if ((float)j < num2)
								{
									vector2 = Vector3.Lerp(list[0].pos, list[1].pos, ExtraMath.RemapValue((float)j, num, num2));
								}
								else
								{
									vector2 = Vector3.Lerp(list[1].pos, list[2].pos, ExtraMath.RemapValue((float)j, num2, num3));
								}
								Data data = presence.SampleData(new NavPos(tri, (vector + vector2) / 2f));
								Gizmos.color = ((!data.agent || !this.useAgentColors) ? color : data.agent.uniqueDebugColor).SetA(ExtraMath.RemapValue((float)j, 50f, 0f));
								Gizmos.DrawLine(vector, vector2);
							}
						}
					}
				}
				break;
			}
			case TriFlowDebugger.GizmoMode.Direction:
				for (int k = 0; k < navMesh.verts.Length; k++)
				{
					Vert vert = navMesh.verts[k];
					Content content = flowContents[k];
					if (this.useAgentColors)
					{
						Gizmos.color = ((!content.data.agent) ? color : content.data.agent.uniqueDebugColor);
					}
					Gizmos.DrawRay(vert.pos, content.direction * 0.2f);
				}
				for (int l = 0; l < navMesh.tris.Length; l++)
				{
					Tri tri2 = navMesh.tris[l];
					NavPos navPos = tri2.navPos;
					Data data2 = presence.SampleData(navPos);
					if (this.useAgentColors)
					{
						Gizmos.color = ((!data2.agent) ? color : data2.agent.uniqueDebugColor);
					}
					Gizmos.DrawRay(tri2.pos, presence.SampleDirection(navPos) * 0.2f);
				}
				break;
			case TriFlowDebugger.GizmoMode.Tree:
				for (int m = 0; m < navMesh.verts.Length; m++)
				{
					Vert vert2 = navMesh.verts[m];
					Content content2 = flowContents[m];
					if (this.useAgentColors)
					{
						Gizmos.color = ((!content2.data.agent) ? color : content2.data.agent.uniqueDebugColor);
					}
					Gizmos.DrawRay(vert2.pos, content2.inVector);
				}
				break;
			}
		}

		// Token: 0x040019A2 RID: 6562
		[SerializeField]
		private Faction.Side side;

		// Token: 0x040019A3 RID: 6563
		private WeakReference<Island> _island = new WeakReference<Island>(null);

		// Token: 0x040019A4 RID: 6564
		[SerializeField]
		private TriFlowDebugger.GizmoMode gizmoMode;

		// Token: 0x040019A5 RID: 6565
		[SerializeField]
		private TriFlowDebugger.ColorMode colorMode;

		// Token: 0x02000613 RID: 1555
		private enum GizmoMode
		{
			// Token: 0x040019A7 RID: 6567
			Distance,
			// Token: 0x040019A8 RID: 6568
			Direction,
			// Token: 0x040019A9 RID: 6569
			Tree,
			// Token: 0x040019AA RID: 6570
			Amount
		}

		// Token: 0x02000614 RID: 1556
		private enum ColorMode
		{
			// Token: 0x040019AC RID: 6572
			AgentColors,
			// Token: 0x040019AD RID: 6573
			FactionColors
		}
	}
}
