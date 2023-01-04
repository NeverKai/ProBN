using System;
using UnityEngine;

namespace Voxels.TowerDefense.RaidGeneration
{
	// Token: 0x020005B9 RID: 1465
	public class ShipLoad : MonoBehaviour
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x00079383 File Offset: 0x00077783
		public float agentRadius
		{
			get
			{
				return (!this.vikingRef) ? 0.12f : this.vikingRef.agent.radius;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x000793AF File Offset: 0x000777AF
		public float agentDiameter
		{
			get
			{
				return this.agentRadius * 2f;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000793BD File Offset: 0x000777BD
		public Color agentColor
		{
			get
			{
				return (!this.vikingRef) ? Color.white : this.vikingRef.agent.uniqueDebugColor;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000793E9 File Offset: 0x000777E9
		public string agentName
		{
			get
			{
				return (!this.vikingRef) ? "No Agent" : this.vikingRef.name;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x00079410 File Offset: 0x00077810
		public VikingAgent.Type vikingType
		{
			get
			{
				return (!this.vikingRef) ? VikingAgent.Type.Sword : this.vikingRef.type;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x00079433 File Offset: 0x00077833
		public int agentBounty
		{
			get
			{
				return (!this.vikingRef) ? 0 : this.vikingRef.bounty;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x00079456 File Offset: 0x00077856
		public int bounty
		{
			get
			{
				return this.agentBounty * this.count;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x00079465 File Offset: 0x00077865
		public float area
		{
			get
			{
				return ((!this.vikingRef) ? 1f : this.vikingRef.agent.area) * (float)this.count;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x0007949C File Offset: 0x0007789C
		private Bounds bounds
		{
			get
			{
				int num = Mathf.CeilToInt(Mathf.Sqrt((float)this.count) * 0.5f);
				int num2 = Mathf.CeilToInt((float)this.count / (float)num);
				return new Bounds(base.transform.position, new Vector3((float)num, 0f, (float)num2) * this.agentDiameter);
			}
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000794FC File Offset: 0x000778FC
		private void OnDrawGizmos()
		{
			base.name = this.count + " " + ((!this.vikingRef) ? "NULL" : this.vikingRef.name);
		}

		// Token: 0x04001846 RID: 6214
		public VikingReference vikingRef;

		// Token: 0x04001847 RID: 6215
		public int count = 1;

		// Token: 0x04001848 RID: 6216
		public Landing landing;
	}
}
