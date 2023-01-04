using System;
using UnityEngine;

namespace Voxels.TowerDefense.RaidPlanning
{
	// Token: 0x020005AB RID: 1451
	[RequireComponent(typeof(RaidShipToken))]
	public class RaidAgentToken : MonoBehaviour
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x0007779B File Offset: 0x00075B9B
		public float agentRadius
		{
			get
			{
				return (!this.agent) ? 0.12f : this.agent.radius;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000777C2 File Offset: 0x00075BC2
		public float agentDiameter
		{
			get
			{
				return this.agentRadius * 2f;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060025CC RID: 9676 RVA: 0x000777D0 File Offset: 0x00075BD0
		public Color agentColor
		{
			get
			{
				return (!this.agent) ? Color.white : this.agent.uniqueDebugColor;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000777F7 File Offset: 0x00075BF7
		public string agentName
		{
			get
			{
				return (!this.agent) ? "No Agent" : this.agent.name;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060025CE RID: 9678 RVA: 0x00077820 File Offset: 0x00075C20
		public int agentBounty
		{
			get
			{
				if (!this.agent)
				{
					return 0;
				}
				VikingAgent component = this.agent.GetComponent<VikingAgent>();
				if (!component)
				{
					return 0;
				}
				return component.bounty;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0007785E File Offset: 0x00075C5E
		public int totalBounty
		{
			get
			{
				return this.agentBounty * this.count;
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00077870 File Offset: 0x00075C70
		public ShipLoadDef GetShipLoadDef()
		{
			return new ShipLoadDef
			{
				agentPrefab = this.agent,
				numAgents = this.count,
				squadPrefab = base.GetComponentInParent<RaidSquadDef>().squadPrefab
			};
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000778B4 File Offset: 0x00075CB4
		private Bounds bounds
		{
			get
			{
				int num = Mathf.CeilToInt(Mathf.Sqrt((float)this.count) * 0.5f);
				int num2 = Mathf.CeilToInt((float)this.count / (float)num);
				return new Bounds(base.transform.position, new Vector3((float)num, 0f, (float)num2) * this.agentDiameter);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00077912 File Offset: 0x00075D12
		public float area
		{
			get
			{
				return (float)this.count * this.agentDiameter * this.agentDiameter;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00077929 File Offset: 0x00075D29
		public RaidShipToken ship
		{
			get
			{
				return base.GetComponent<RaidShipToken>();
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00077931 File Offset: 0x00075D31
		public void OnValidate()
		{
			if (this.ship)
			{
				this.ship.OnValidate();
			}
		}

		// Token: 0x04001817 RID: 6167
		public Agent agent;

		// Token: 0x04001818 RID: 6168
		public int count = 1;
	}
}
