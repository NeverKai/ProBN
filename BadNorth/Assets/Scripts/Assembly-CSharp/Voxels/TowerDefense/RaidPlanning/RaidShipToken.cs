using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005B4 RID: 1460
	[SelectionBase]
	public class RaidShipToken : RaidToken
	{
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x0007807D File Offset: 0x0007647D
		public RaidShipList shipDef
		{
			get
			{
				return base.GetComponentInParent<RaidShipList>();
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00078085 File Offset: 0x00076485
		public Longship ship
		{
			get
			{
				return (!this.shipDef) ? null : this.shipDef.GetLongship(this.agentCount);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000780AE File Offset: 0x000764AE
		public float radius
		{
			get
			{
				return (!this.ship) ? 0.3f : this.ship.col.radius;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000780DA File Offset: 0x000764DA
		public float length
		{
			get
			{
				return (!this.ship) ? 0.7f : this.ship.col.height;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x00078106 File Offset: 0x00076506
		private string shipName
		{
			get
			{
				return (!this.ship) ? "No Ship" : this.ship.name;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x0007812D File Offset: 0x0007652D
		public RaidSquadDef squadDef
		{
			get
			{
				return base.GetComponentInParent<RaidSquadDef>();
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x00078135 File Offset: 0x00076535
		public Squad squadPrefab
		{
			get
			{
				return (!this.squadDef) ? null : this.squadDef.squadPrefab;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x00078158 File Offset: 0x00076558
		public string localTimeString
		{
			get
			{
				return this.localTime.ToString("F0") + " s";
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x00078184 File Offset: 0x00076584
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x000781AF File Offset: 0x000765AF
		public int localTime
		{
			get
			{
				return Mathf.RoundToInt(base.transform.localPosition.z * 4f);
			}
			set
			{
				if (this.wave)
				{
					base.transform.localPosition = base.transform.localPosition.SetComponent(2, (float)Mathf.Max(value, 0) / 4f);
				}
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000781EB File Offset: 0x000765EB
		public RaidAgentToken[] agents
		{
			get
			{
				return base.GetComponents<RaidAgentToken>();
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x000781F3 File Offset: 0x000765F3
		public override int agentCount
		{
			get
			{
				return this.agents.Sum((RaidAgentToken x) => x.count);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x0007821D File Offset: 0x0007661D
		public override int bounty
		{
			get
			{
				return this.agents.Sum((RaidAgentToken x) => x.totalBounty);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00078247 File Offset: 0x00076647
		public RaidWaveToken wave
		{
			get
			{
				return this.parent as RaidWaveToken;
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00078254 File Offset: 0x00076654
		public ShipDef GetShipDef()
		{
			ShipDef result = default(ShipDef);
			result.load = new List<ShipLoadDef>();
			foreach (RaidAgentToken raidAgentToken in this.agents)
			{
				result.load.Add(raidAgentToken.GetShipLoadDef());
			}
			result.shipPrefab = this.ship;
			result.delayTime = (float)this.localTime;
			return result;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000782C4 File Offset: 0x000766C4
		public Bounds shipBounds
		{
			get
			{
				Bounds result = new Bounds(Vector3.zero, new Vector3(this.radius * 2f, 0f, this.length));
				result.center += Vector3.forward * (result.size.z / 2f + 0.4f);
				return result;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x00078334 File Offset: 0x00076734
		public override Bounds localDropBounds
		{
			get
			{
				Bounds shipBounds = this.shipBounds;
				shipBounds.extents += new Vector3(0.1f, 0f, 0.1f);
				return shipBounds;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x0007836F File Offset: 0x0007676F
		public override Vector3 localDragPos
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x00078376 File Offset: 0x00076776
		public override float dragRadius
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x0007837D File Offset: 0x0007677D
		public override int depth
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x00078380 File Offset: 0x00076780
		public override RaidToken parent
		{
			get
			{
				return base.GetComponentInParent<RaidWaveToken>();
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00078388 File Offset: 0x00076788
		public override void OnDrawGizmos()
		{
			Gizmos.color = ((!this.parent) ? Color.red : Color.white);
			this.GizmosInternal();
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000783B4 File Offset: 0x000767B4
		public override void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			this.GizmosInternal();
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000783C8 File Offset: 0x000767C8
		private void GizmosInternal()
		{
			float radius = this.radius;
			float length = this.length;
			if (this.wave)
			{
				Gizmos.matrix = this.wave.transform.localToWorldMatrix;
				Gizmos.DrawLine(base.transform.localPosition.SetZ(0.1f), base.transform.localPosition);
			}
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Bounds shipBounds = this.shipBounds;
			bool flag = Vector3.SqrMagnitude(Camera.current.transform.position - base.transform.position) > 1600f;
			if (flag)
			{
				Gizmos.DrawWireCube(shipBounds.center, shipBounds.size);
			}
			else
			{
				Vector3 zero = Vector3.zero;
				zero.z = shipBounds.min.z;
				Vector3 vector = zero;
				vector.z = shipBounds.max.z;
				Gizmos.DrawLine(zero, zero + new Vector3(-radius, 0f, radius));
				Gizmos.DrawLine(zero, zero + new Vector3(radius, 0f, radius));
				Gizmos.DrawLine(vector, vector + new Vector3(-radius, 0f, -radius));
				Gizmos.DrawLine(vector, vector + new Vector3(radius, 0f, -radius));
				Gizmos.DrawLine(zero + new Vector3(-radius, 0f, radius), vector + new Vector3(-radius, 0f, -radius));
				Gizmos.DrawLine(zero + new Vector3(radius, 0f, radius), vector + new Vector3(radius, 0f, -radius));
			}
			base.DrawDragGizmo();
			this.DrawAgents(flag);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x0007859C File Offset: 0x0007699C
		private void DrawAgents(bool simple)
		{
			RaidAgentToken[] agents = this.agents;
			Bounds shipBounds = this.shipBounds;
			shipBounds.size -= new Vector3(0.05f, 0f, 0.05f);
			float num = 0f;
			float num2 = 0.1f;
			float num3 = -num2;
			foreach (RaidAgentToken raidAgentToken in agents)
			{
				float agentDiameter = raidAgentToken.agentDiameter;
				int a = (raidAgentToken.count < 8) ? 1 : 2;
				int num4 = Mathf.Max(a, Mathf.FloorToInt(shipBounds.size.x / agentDiameter));
				int num5 = Mathf.CeilToInt((float)raidAgentToken.count / (float)num4);
				num3 += (float)num5 * agentDiameter;
				num3 += num2;
			}
			foreach (RaidAgentToken raidAgentToken2 in agents)
			{
				float agentDiameter2 = raidAgentToken2.agentDiameter;
				int a2 = (raidAgentToken2.count < 8) ? 1 : 2;
				int num6 = Mathf.Max(a2, Mathf.FloorToInt(shipBounds.size.x / agentDiameter2));
				int num7 = Mathf.CeilToInt((float)raidAgentToken2.count / (float)num6);
				Gizmos.color = raidAgentToken2.agentColor * 2f;
				if (simple)
				{
					Gizmos.DrawCube(Vector3.forward * ((float)num7 * agentDiameter2 / 2f - num3 / 2f + shipBounds.center.z + num), new Vector3((float)num6, 0.5f, (float)num7) * agentDiameter2);
					num += agentDiameter2 * (float)num7;
				}
				else
				{
					for (int k = 0; k < num7; k++)
					{
						int num8 = Mathf.Min(num6, raidAgentToken2.count - k * num6);
						for (int l = 0; l < num8; l++)
						{
							Vector3 vector = Vector3.zero;
							vector.x = ((float)l - ((float)num8 - 1f) / 2f) * agentDiameter2;
							vector.z += num;
							vector += shipBounds.center;
							vector.z -= num3 / 2f;
							vector.z += raidAgentToken2.agentRadius;
							Gizmos.DrawSphere(vector, raidAgentToken2.agentRadius);
						}
						num += agentDiameter2;
					}
				}
				num += num2;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x0007882C File Offset: 0x00076C2C
		public string agentSummary
		{
			get
			{
				if (this.agents.Length == 0)
				{
					return "EMPTY";
				}
				return string.Join(", ", this.agents.ToList<RaidAgentToken>().ConvertAll<string>((RaidAgentToken x) => x.count + " " + x.agentName).ToArray());
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00078888 File Offset: 0x00076C88
		private void SetName()
		{
			base.name = string.Concat(new string[]
			{
				this.shipName,
				", ",
				this.localTimeString,
				", ",
				this.agentSummary
			});
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000788C8 File Offset: 0x00076CC8
		public void OnValidate()
		{
			base.transform.localPosition = base.transform.localPosition.SetY(0f);
			if (this.wave)
			{
				this.wave.OnValidate();
			}
			this.SetName();
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00078916 File Offset: 0x00076D16
		public override void OnArranged(RaidToken parent, int index)
		{
			this.SetName();
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00078920 File Offset: 0x00076D20
		public override void OnArrangeChildren()
		{
			RaidAgentToken[] agents = this.agents;
			Bounds shipBounds = this.shipBounds;
			for (int i = 0; i < agents.Length; i++)
			{
				RaidAgentToken raidAgentToken = agents[i];
				float area = raidAgentToken.area;
				raidAgentToken.transform.localPosition = new Vector3(0f, 0f, ((float)i + 0.5f) / (float)agents.Length * shipBounds.size.z + shipBounds.min.z - base.transform.position.z);
			}
		}

		// Token: 0x020005B5 RID: 1461
		private struct DrawSphere
		{
			// Token: 0x04001834 RID: 6196
			public Vector3 pos;

			// Token: 0x04001835 RID: 6197
			public Color color;

			// Token: 0x04001836 RID: 6198
			public float radius;
		}
	}
}
