using System;
using System.Collections;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020007AD RID: 1965
	public class NavSpotFormationSquad : SquadCoordinatorAgentTracker<EnglishFormationAgent>
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x000D8E10 File Offset: 0x000D7210
		public NavSpot navSpot
		{
			get
			{
				return this._navSpot;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060032DF RID: 13023 RVA: 0x000D8E18 File Offset: 0x000D7218
		private IPathTarget target
		{
			get
			{
				return this._navSpot;
			}
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000D8E20 File Offset: 0x000D7220
		public override void Setup(Squad squad)
		{
			base.Setup(squad);
			base.enSquad.onSquadSpawnComplete += delegate()
			{
				this.squadSpawned = true;
				this.UpdateFormation();
			};
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000D8E40 File Offset: 0x000D7240
		private void Start()
		{
			this.squadPather = base.GetComponent<EnglishPatherSquad>();
			this.squadPather.onPathTargetChanged += this.OnPathTargetChanged;
			base.squad.onAgentSpawned += this.OnSquadChanged;
			base.squad.onAgentRemoved += this.OnSquadChanged;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000D8E9E File Offset: 0x000D729E
		protected override void OnAgentComponentAdded(EnglishFormationAgent agentComponent)
		{
			this.orders.Add(agentComponent.orderPos);
			agentComponent.enabled = base.enabled;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000D8EBD File Offset: 0x000D72BD
		protected override void OnAgentComponentRemoved(EnglishFormationAgent agentComponent)
		{
			this.orders.Remove(agentComponent.orderPos);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000D8ED1 File Offset: 0x000D72D1
		public void SetNavSpot(NavSpot newNavSpot)
		{
			this._navSpot = newNavSpot;
			base.enabled = true;
			this.UpdateFormation();
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000D8EE7 File Offset: 0x000D72E7
		private void OnSquadChanged(Agent agent)
		{
			this.UpdateFormation();
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000D8EEF File Offset: 0x000D72EF
		private void OnPathTargetChanged(IPathTarget target)
		{
			base.StopAllCoroutines();
			if (!(target is NavSpot))
			{
				base.enabled = false;
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000D8F0C File Offset: 0x000D730C
		private void OnEnable()
		{
			foreach (EnglishFormationAgent englishFormationAgent in this.agentComponents)
			{
				englishFormationAgent.enabled = true;
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000D8F68 File Offset: 0x000D7368
		private void OnDisable()
		{
			foreach (EnglishFormationAgent englishFormationAgent in this.agentComponents)
			{
				englishFormationAgent.enabled = false;
			}
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000D8FC4 File Offset: 0x000D73C4
		public void UpdateFormation()
		{
			base.StopAllCoroutines();
			if (GameMaster.isApplicationQuitting || !base.isActiveAndEnabled || !this.squadSpawned)
			{
				return;
			}
			using ("UpdateFormation")
			{
				Vector3 vector = Vector3.forward;
				float oldDist = 0f;
				IPathTarget pathTarget = this.oldNavSpot;
				if (pathTarget != null)
				{
					pathTarget.SampleDistanceDir(this._navSpot.navPos, ref vector, ref oldDist);
					vector = -vector;
				}
				Vector3 lookDir = this._navSpot.lookDir;
				SquadFormation squadFormation = new SquadFormation(this.orders.Count, this._navSpot);
				bool flag = false;
				for (int i = 0; i < this.orders.Count; i++)
				{
					EnglishFormationAgent.OrderPos orderPos = this.orders[i];
					Vector3 vector2 = squadFormation.Get(i);
					vector2 *= orderPos.component.agent.radius * 1.01f * 2f;
					NavPos navPos = this._navSpot.navPos;
					if (!navPos.Move(vector2))
					{
						flag = true;
					}
					orderPos.idealFormPos = vector2;
					orderPos.navPos = navPos;
					orderPos.lookDir = vector;
					orderPos.push = Vector3.zero;
				}
				if (flag)
				{
					IEnumerator enumerator = this.SlotPusher();
					using ("SlotPusherWarmup")
					{
						int num = 0;
						while (num < 4 && enumerator.MoveNext())
						{
							num++;
						}
					}
					base.StartCoroutine(enumerator);
				}
				base.StartCoroutine(this.AwaitArrival(oldDist, vector, lookDir));
				this.oldNavSpot = this._navSpot;
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000D91C4 File Offset: 0x000D75C4
		private IEnumerator AwaitArrival(float oldDist, Vector3 oldDir, Vector3 newDir)
		{
			bool maybeNew = oldDist > 1.5f || !this.arrived;
			if (maybeNew && Vector3.Dot(oldDir, newDir) < 0.7f)
			{
				this.arrived = false;
				for (int l = 0; l < this.orders.Count; l++)
				{
					EnglishFormationAgent.OrderPos orderPos = this.orders[l];
					orderPos.arrived = false;
					orderPos.lookDir = oldDir;
				}
				yield return null;
				int i = 0;
				for (;;)
				{
					EnglishFormationAgent component = this.agentComponents[i];
					if (component.agent.orderDist < 0.1f)
					{
						break;
					}
					yield return null;
					i = (i + 1) % this.agentComponents.Count;
				}
				float timer = Time.time + 0.5f;
				int j = 0;
				while (Time.time < timer)
				{
					EnglishFormationAgent component2 = this.agentComponents[j];
					if (!component2.orderPos.arrived && component2.agent.orderDist < 0.2f)
					{
						component2.orderPos.arrived = true;
						timer = Mathf.Max(timer, Time.time + 0.2f);
					}
					yield return null;
					j = (j + 1) % this.agentComponents.Count;
				}
				this.arrived = true;
				WaitForSeconds wait = new WaitForSeconds(0.2f / (float)this.agentComponents.Count);
				for (int k = 0; k < this.orders.Count; k++)
				{
					EnglishFormationAgent.OrderPos order = this.orders[k];
					order.arrived = true;
					order.lookDir = (newDir + order.navPos.GetNormal() * 3f).GetZeroY().normalized;
					yield return wait;
				}
			}
			else
			{
				this.arrived = true;
				for (int m = 0; m < this.orders.Count; m++)
				{
					EnglishFormationAgent.OrderPos orderPos2 = this.orders[m];
					orderPos2.arrived = true;
					orderPos2.lookDir = (newDir + orderPos2.navPos.GetNormal() * 3f).GetZeroY().normalized;
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000D91F4 File Offset: 0x000D75F4
		private void Update()
		{
			if (this.navSpot)
			{
				this.CenterSwapper();
				this.SlotSwapper();
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000D9214 File Offset: 0x000D7614
		private void CenterSwapper()
		{
			if (this.agentComponents.Count < 2)
			{
				return;
			}
			EnglishFormationAgent englishFormationAgent = this.agentComponents[0];
			if (!englishFormationAgent.agent.navPos.valid)
			{
				return;
			}
			Vector3 a = this.target.navPos.pos - englishFormationAgent.agent.enemyDir * englishFormationAgent.agent.radius * base.enSquad.idealHeroDist;
			float num = (a - englishFormationAgent.orderPos.navPos.pos).sqrMagnitude;
			EnglishFormationAgent englishFormationAgent2 = null;
			for (int i = 1; i < this.agentComponents.Count; i++)
			{
				EnglishFormationAgent englishFormationAgent3 = this.agentComponents[i];
				if (!englishFormationAgent3.agent.navPos.valid)
				{
					return;
				}
				float sqrMagnitude = (a - englishFormationAgent3.orderPos.navPos.pos).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					englishFormationAgent2 = englishFormationAgent3;
					num = sqrMagnitude;
				}
			}
			if (englishFormationAgent2)
			{
				EnglishFormationAgent.SwapOrders(englishFormationAgent, englishFormationAgent2);
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000D9348 File Offset: 0x000D7748
		private void SlotSwapper()
		{
			using ("SlotSwapper")
			{
				if (this.agentComponents.Count >= 2)
				{
					if (++this.swapIndex >= this.agentComponents.Count)
					{
						this.swapIndex = 1;
					}
					EnglishFormationAgent englishFormationAgent = this.agentComponents[this.swapIndex];
					if (englishFormationAgent.agent.navPos.valid)
					{
						float orderDist = englishFormationAgent.agent.orderDist;
						for (int i = 1; i < this.agentComponents.Count; i++)
						{
							if (i != this.swapIndex)
							{
								EnglishFormationAgent englishFormationAgent2 = this.agentComponents[i];
								if (englishFormationAgent2.agent.navPos.valid)
								{
									float orderDist2 = englishFormationAgent2.agent.orderDist;
									if (orderDist + orderDist2 >= 0.1f)
									{
										float orderDist3 = englishFormationAgent.agent.brain.order.GetOrderDist(englishFormationAgent2.agent.navPos);
										float orderDist4 = englishFormationAgent2.agent.brain.order.GetOrderDist(englishFormationAgent.agent.navPos);
										float num = Mathf.Max(orderDist, orderDist2) + orderDist + orderDist2;
										float num2 = Mathf.Max(orderDist3, orderDist4) + orderDist3 + orderDist4;
										if (num2 < num * 0.99f)
										{
											EnglishFormationAgent.SwapOrders(englishFormationAgent, englishFormationAgent2);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000D9500 File Offset: 0x000D7900
		private IEnumerator SlotPusher()
		{
			bool anyChange = true;
			while (anyChange)
			{
				using ("SlotPusher")
				{
					anyChange = false;
					for (int i = 0; i < this.orders.Count; i++)
					{
						EnglishFormationAgent.OrderPos orderPos = this.orders[i];
						for (int j = i + 1; j < this.orders.Count; j++)
						{
							EnglishFormationAgent.OrderPos orderPos2 = this.orders[j];
							Vector3 pos = orderPos.navPos.pos;
							Vector3 pos2 = orderPos2.navPos.pos;
							Vector3 a = pos - pos2;
							float radius = orderPos.component.agent.radius;
							float radius2 = orderPos2.component.agent.radius;
							float num = radius + radius2;
							num *= 1.01f;
							float sqrMagnitude = a.sqrMagnitude;
							if (sqrMagnitude < num * num)
							{
								float num2 = Mathf.Sqrt(sqrMagnitude);
								Vector3 a2 = (num2 <= 0f) ? UnityEngine.Random.insideUnitSphere : (a / num2);
								Vector3 b = a2 * (num2 - num) * 0.2f;
								orderPos.push -= b;
								orderPos2.push += b;
								anyChange = true;
							}
						}
					}
					for (int k = 0; k < this.orders.Count; k++)
					{
						EnglishFormationAgent.OrderPos orderPos3 = this.orders[k];
						if (!(orderPos3.push == Vector3.zero))
						{
							EnglishFormationAgent.OrderPos orderPos4 = orderPos3;
							orderPos4.navPos.pos = orderPos4.navPos.pos + orderPos3.push;
							orderPos3.push = Vector3.zero;
						}
					}
				}
				yield return null;
			}
			yield return null;
			yield break;
		}

		// Token: 0x0400229A RID: 8858
		private DebugChannelGroup dbgChan = new DebugChannelGroup("NavSpotFormation", EVerbosity.Quiet, 0);

		// Token: 0x0400229B RID: 8859
		private NavSpot _navSpot;

		// Token: 0x0400229C RID: 8860
		private NavSpot oldNavSpot;

		// Token: 0x0400229D RID: 8861
		private EnglishPatherSquad squadPather;

		// Token: 0x0400229E RID: 8862
		public List<EnglishFormationAgent.OrderPos> orders = new List<EnglishFormationAgent.OrderPos>();

		// Token: 0x0400229F RID: 8863
		private bool squadSpawned;

		// Token: 0x040022A0 RID: 8864
		public bool arrived;

		// Token: 0x040022A1 RID: 8865
		private int swapIndex;
	}
}
