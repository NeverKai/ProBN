using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007A3 RID: 1955
	public class EnglishFormationAgent : AgentComponent
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000D6D85 File Offset: 0x000D5185
		public float movability
		{
			get
			{
				return this._movability;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000D6D8D File Offset: 0x000D518D
		public EnglishPatherAgent agentPather
		{
			get
			{
				return this._agentPather;
			}
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000D6D95 File Offset: 0x000D5195
		public override void Setup()
		{
			base.Setup();
			this._agentPather = base.GetComponent<EnglishPatherAgent>();
			this.orderPos = new EnglishFormationAgent.OrderPos(base.agent.navPos.pos, base.agent.navPos, Vector3.forward, this);
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000D6DD8 File Offset: 0x000D51D8
		public static void SwapOrders(EnglishFormationAgent component0, EnglishFormationAgent component1)
		{
			EnglishFormationAgent.OrderPos orderPos = component0.orderPos;
			EnglishFormationAgent.OrderPos orderPos2 = component1.orderPos;
			component0.orderPos = orderPos2;
			component1.orderPos = orderPos;
			orderPos.component = component1;
			orderPos2.component = component0;
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000D6E10 File Offset: 0x000D5210
		public void ModifyPath(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			if (base.enabled && dist < 2f && navPos.TriCast(this.orderPos.navPos))
			{
				Vector3 vector = this.orderPos.navPos.pos - navPos.pos;
				dist = vector.magnitude;
				dir = Vector3.ClampMagnitude(vector, 1f);
			}
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000D6E84 File Offset: 0x000D5284
		public void OnDrawGizmos()
		{
			if (Application.isPlaying && this.orderPos.navPos.valid)
			{
				NavSpotFormationSquad squadCoordinator = base.squad.GetSquadCoordinator<NavSpotFormationSquad>();
				bool arrived = squadCoordinator.arrived;
				Color color = new Color(1f, (float)((!this.orderPos.arrived) ? 0 : 1), (float)((!arrived) ? 0 : 1), 1f);
				Gizmos.color = color;
				ExtraGizmos.DrawCircle(this.orderPos.navPos.pos, base.agent.radius, 8);
				Gizmos.DrawRay(this.orderPos.navPos.pos, this.orderPos.lookDir * base.agent.radius);
				Gizmos.color = color * 2f;
				Gizmos.DrawSphere(this.orderPos.navPos.pos, base.agent.radius * 0.1f);
				Gizmos.color = color;
				Gizmos.DrawLine(base.agent.transform.position, this.orderPos.navPos.pos);
				Gizmos.DrawLine(base.agent.transform.position, this.orderPos.navPos.pos);
				Gizmos.DrawLine(base.agent.transform.position, this.orderPos.navPos.pos);
				Gizmos.DrawLine(base.agent.transform.position, this.orderPos.navPos.pos);
			}
		}

		// Token: 0x04002260 RID: 8800
		[SerializeField]
		private float _movability = 1f;

		// Token: 0x04002261 RID: 8801
		[HideInInspector]
		public EnglishFormationAgent.OrderPos orderPos;

		// Token: 0x04002262 RID: 8802
		private EnglishPatherAgent _agentPather;

		// Token: 0x04002263 RID: 8803
		private const float triCastDistance = 2f;

		// Token: 0x020007A4 RID: 1956
		public class OrderPos
		{
			// Token: 0x060032A1 RID: 12961 RVA: 0x000D7021 File Offset: 0x000D5421
			public OrderPos(Vector3 idealLocalPos, NavPos navPos, Vector3 lookDir, EnglishFormationAgent component)
			{
				this.navPos = navPos;
				this.lookDir = lookDir;
				this.push = Vector3.zero;
				this.component = component;
				this.arrived = false;
			}

			// Token: 0x04002264 RID: 8804
			public Vector3 idealFormPos;

			// Token: 0x04002265 RID: 8805
			public NavPos navPos;

			// Token: 0x04002266 RID: 8806
			public Vector3 lookDir;

			// Token: 0x04002267 RID: 8807
			public Vector3 push;

			// Token: 0x04002268 RID: 8808
			public bool arrived;

			// Token: 0x04002269 RID: 8809
			public EnglishFormationAgent component;
		}
	}
}
