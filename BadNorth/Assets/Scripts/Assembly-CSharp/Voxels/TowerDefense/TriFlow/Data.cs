using System;
using UnityEngine;

namespace Voxels.TowerDefense.TriFlow
{
	// Token: 0x02000808 RID: 2056
	[Serializable]
	public struct Data
	{
		// Token: 0x060035C3 RID: 13763 RVA: 0x000E7BA9 File Offset: 0x000E5FA9
		public Data(ITriFlowObject entity, NavPos navPos, bool dangerous, bool hittable, float distance, Vector3 dir, float amount = 1f)
		{
			this.entity = entity;
			this.navPos = navPos;
			this.dangerous = dangerous;
			this.hittable = hittable;
			this.distance = distance;
			this.dir = dir;
			this.amount = amount;
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000E7BE0 File Offset: 0x000E5FE0
		public Data(ITriFlowObject entity, NavPos navPos, bool dangerous, bool hittable, float distance, float amount = 1f)
		{
			this.entity = entity;
			this.navPos = navPos;
			this.dangerous = dangerous;
			this.hittable = hittable;
			this.distance = distance;
			this.dir = Vector3.zero;
			this.amount = amount;
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000E7C1A File Offset: 0x000E601A
		public AgentComponent agentComponent
		{
			get
			{
				return this.entity as AgentComponent;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000E7C27 File Offset: 0x000E6027
		public Agent agent
		{
			get
			{
				return (!this.agentComponent) ? null : this.agentComponent.agent;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x000E7C4A File Offset: 0x000E604A
		public Brain brain
		{
			get
			{
				return this.entity as Brain;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000E7C57 File Offset: 0x000E6057
		public bool valid
		{
			get
			{
				return this.entity != null && !this.entity.Equals(null);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000E7C76 File Offset: 0x000E6076
		public static Data empty
		{
			get
			{
				return new Data(null, NavPos.empty, false, false, float.MaxValue, 1f);
			}
		}

		// Token: 0x04002497 RID: 9367
		public ITriFlowObject entity;

		// Token: 0x04002498 RID: 9368
		public NavPos navPos;

		// Token: 0x04002499 RID: 9369
		public bool dangerous;

		// Token: 0x0400249A RID: 9370
		public bool hittable;

		// Token: 0x0400249B RID: 9371
		public float distance;

		// Token: 0x0400249C RID: 9372
		public Vector3 dir;

		// Token: 0x0400249D RID: 9373
		public float amount;
	}
}
