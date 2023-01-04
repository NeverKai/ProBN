using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x02000693 RID: 1683
	public abstract class Brain : AgentComponent, ITriFlowObject
	{
		// Token: 0x06002B27 RID: 11047 RVA: 0x0009ADEF File Offset: 0x000991EF
		public virtual void OnProximity(Agent agent)
		{
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x0009ADF1 File Offset: 0x000991F1
		public Data triFlowData
		{
			get
			{
				return new Data(this, base.agent.navPos, base.agent.dangerous, true, 0f, 1f);
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x0009AE1A File Offset: 0x0009921A
		protected virtual void OnDestroy()
		{
			this.actions = null;
			this.brainState = null;
			this.actingState = null;
			this.orderList = null;
			this.order = null;
			this.orderMono = null;
			this.triflow = null;
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x0009AE50 File Offset: 0x00099250
		public bool MaybeAct()
		{
			for (int i = 0; i < this.actions.Count; i++)
			{
				if (this.actions[i].MaybeAct(this))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x0009AE93 File Offset: 0x00099293
		public void RemoveAction(IBrainAction action)
		{
			this.actions.Remove(action);
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x0009AEA4 File Offset: 0x000992A4
		public void PickNewOrder()
		{
			if (this.order == null || !this.order.WantsControl() || this.order == Brain.nullOrder)
			{
				foreach (IAgentOrder agentOrder in this.orderList)
				{
					if (agentOrder.WantsControl())
					{
						this.order = agentOrder;
						this.orderMono = (this.order as MonoBehaviour);
						break;
					}
				}
			}
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x0009AF4C File Offset: 0x0009934C
		public void DebugMessage(string message)
		{
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x0009AF50 File Offset: 0x00099350
		public override void Setup()
		{
			base.Setup();
			this.brainState = new AgentState("Brain", base.agent.exclusives, true, true);
			this.actingState = new AgentState("Acting", base.agent.exclusives, false, true);
			this.orderList.AddRange(base.agent.GetComponentsInChildren<IAgentOrder>(true));
			this.orderList.Add(Brain.nullOrder);
			this.actions.AddRange(base.agent.GetComponentsInChildren<IBrainAction>(true));
			AgentState exclusives = base.agent.exclusives;
			exclusives.OnEmpty = (Action)Delegate.Combine(exclusives.OnEmpty, new Action(this.brainState.SetActiveTrue));
			AgentState agentState = this.actingState;
			agentState.OnEmpty = (Action)Delegate.Combine(agentState.OnEmpty, new Action(this.brainState.SetActiveTrue));
			this.brainState.OnUpdate += this.PickNewOrder;
			this.triflow = new AgentState("TriFlow", base.agent.aliveAndGrounded, true, false);
			this.triflow.OnUpdate += delegate()
			{
				if (base.agent.navPos.island)
				{
					base.agent.faction.presenceObj.AddPending(this.triFlowData);
				}
			};
			AgentState aliveAndGrounded = base.agent.aliveAndGrounded;
			aliveAndGrounded.OnActivate = (Action)Delegate.Combine(aliveAndGrounded.OnActivate, new Action(this.triflow.SetActiveTrue));
			this.PickNewOrder();
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x0009B0BC File Offset: 0x000994BC
		private void Start()
		{
			EnglishPatherAgent component = base.agent.GetComponent<EnglishPatherAgent>();
			if (component)
			{
				component.onPathTargetChanged += this.OnPathTargetChanged;
			}
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x0009B0F3 File Offset: 0x000994F3
		protected virtual void OnPathTargetChanged(IPathTarget target)
		{
		}

		// Token: 0x04001C25 RID: 7205
		public List<IBrainAction> actions = new List<IBrainAction>();

		// Token: 0x04001C26 RID: 7206
		public AgentState brainState;

		// Token: 0x04001C27 RID: 7207
		public AgentState actingState;

		// Token: 0x04001C28 RID: 7208
		private List<IAgentOrder> orderList = new List<IAgentOrder>();

		// Token: 0x04001C29 RID: 7209
		public IAgentOrder order;

		// Token: 0x04001C2A RID: 7210
		public MonoBehaviour orderMono;

		// Token: 0x04001C2B RID: 7211
		public AgentState triflow;

		// Token: 0x04001C2C RID: 7212
		private static Brain.NullOrder nullOrder = new Brain.NullOrder();

		// Token: 0x02000694 RID: 1684
		private class NullOrder : IAgentOrder
		{
			// Token: 0x06002B34 RID: 11060 RVA: 0x0009B140 File Offset: 0x00099540
			void IAgentOrder.ApplyOrder()
			{
			}

			// Token: 0x06002B35 RID: 11061 RVA: 0x0009B142 File Offset: 0x00099542
			void IAgentOrder.ApplyWalk()
			{
			}

			// Token: 0x06002B36 RID: 11062 RVA: 0x0009B144 File Offset: 0x00099544
			bool IAgentOrder.WantsControl()
			{
				return true;
			}

			// Token: 0x06002B37 RID: 11063 RVA: 0x0009B147 File Offset: 0x00099547
			void IAgentOrder.SampleOrder(NavPos navPos, ref Vector3 dir, ref float dist)
			{
				dir = Vector3.zero;
				dist = 0f;
			}
		}
	}
}
