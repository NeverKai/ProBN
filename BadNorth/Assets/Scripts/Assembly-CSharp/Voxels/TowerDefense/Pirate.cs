using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A0 RID: 1696
	public class Pirate : AgentComponent, IAgentOrder, IBrainAction
	{
		// Token: 0x06002BC7 RID: 11207 RVA: 0x000A1188 File Offset: 0x0009F588
		public void AddToLongship(Longship longship)
		{
			this.longship = longship;
			longship.AddAgent(base.agent);
			base.agent.body.hopping.OnUpdate += this.PirateUpdate;
			AgentState groundedState = base.agent.groundedState;
			groundedState.OnDeactivate = (Action)Delegate.Combine(groundedState.OnDeactivate, new Action(this.RemoveFromShip));
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000A11F5 File Offset: 0x0009F5F5
		private void OnDestroy()
		{
			if (this.longship)
			{
				this.longship.RemoveAgent(base.agent);
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000A1218 File Offset: 0x0009F618
		public void PirateUpdate()
		{
			if (base.agent.navPos.island)
			{
				this.RemoveFromShip();
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000A123C File Offset: 0x0009F63C
		private void RemoveFromShip()
		{
			this.longship.RemoveAgent(base.agent);
			this.longship = null;
			AgentState groundedState = base.agent.groundedState;
			groundedState.OnDeactivate = (Action)Delegate.Remove(groundedState.OnDeactivate, new Action(this.RemoveFromShip));
			base.agent.body.hopping.OnUpdate -= this.PirateUpdate;
			base.agent.brain.actions.Remove(this);
			base.agent.brain.PickNewOrder();
			this.onRemovedFromShip();
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000A12E0 File Offset: 0x0009F6E0
		public void ApplyOrder()
		{
			this.ApplyWalk();
			Vector3 dir = this.longship.lookDir - base.agent.navPos.transform.TransformVector(base.agent.navPos.GetBorderVector()).normalized * 0.3f;
			base.agent.LookInDirection(dir, 720f, 20f);
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000A1351 File Offset: 0x0009F751
		public void ApplyWalk()
		{
			base.agent.walkDir += base.agent.orderDir;
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000A1374 File Offset: 0x0009F774
		void IAgentOrder.SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
		{
			if (this.longship.landed)
			{
				dir = Vector3.forward - navPos.GetBorderVector() * 0.4f;
				dist = Mathf.Max(new float[]
				{
					navPos.navigationMesh.bounds.max.z - navPos.pos.z - 0.2f
				}) + navPos.GetBorderDistance();
			}
			else
			{
				dir = (base.agent.navPos.GetBorderVector() - base.agent.navPos.pos) * 0.01f;
				dist = 0f;
			}
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000A143A File Offset: 0x0009F83A
		bool IAgentOrder.WantsControl()
		{
			return this.longship;
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000A1448 File Offset: 0x0009F848
		bool IBrainAction.MaybeAct(Brain swordsman)
		{
			if (this.longship && this.longship.landed && base.agent.orderDist < 0.01f)
			{
				NavPos navPos = this.longship.landing.navPos;
				Vector3 borderVector = base.agent.navPos.GetBorderVector();
				navPos.wPos = base.agent.navPos.transform.TransformPoint(base.agent.navPos.pos - borderVector.normalized * 0.3f);
				base.agent.navPos = navPos;
				this.RemoveFromShip();
				base.agent.brain.RemoveAction(this);
				return true;
			}
			return false;
		}

		// Token: 0x04001C92 RID: 7314
		public Longship longship;

		// Token: 0x04001C93 RID: 7315
		public Action onRemovedFromShip = delegate()
		{
		};
	}
}
