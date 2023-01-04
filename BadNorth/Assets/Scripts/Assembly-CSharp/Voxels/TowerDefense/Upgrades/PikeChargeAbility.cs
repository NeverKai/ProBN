using System;
using System.Collections.Generic;
using Fabric;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000857 RID: 2135
	public class PikeChargeAbility : NavSpotTargetableAbility
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000F0CA9 File Offset: 0x000EF0A9
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x000F0CB1 File Offset: 0x000EF0B1
		public PikeChargeAbility.Settings settings { get; private set; }

		// Token: 0x060037EE RID: 14318 RVA: 0x000F0CBC File Offset: 0x000EF0BC
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.settings = this.levelSettings[base.upgradeLevel];
			LineChargeTargeter component = base.GetComponent<LineChargeTargeter>();
			component.range = this.settings.range;
			this.compoonentsInFormation = new List<PikeChargeComponent>(9);
			this.components = new List<PikeChargeComponent>(9);
			this.components.Add(base.squad.standard.gameObject.GetOrAddComponent<PikeChargeComponent>().SetupPikeChargeComponent(this));
			base.squad.onAgentCreated += delegate(Agent agent)
			{
				this.components.Add(agent.GetOrAddComponent<PikeChargeComponent>().SetupPikeChargeComponent(this));
			};
			base.squad.onAgentRemoved += delegate(Agent agent)
			{
				this.components.Remove(agent.GetComponent<PikeChargeComponent>());
			};
			AgentState prepare = new AgentState("Prepare", this.active, false, true);
			this.charging = new AgentState("Charging", this.active, false, true);
			AgentState arrived = new AgentState("Arrived", this.active, false, true);
			AgentState active = this.active;
			active.OnActivate = (Action)Delegate.Combine(active.OnActivate, new Action(delegate()
			{
				prepare.SetActive(true);
			}));
			prepare.OnUpdate += delegate()
			{
				float num = prepare.timeSinceActivation / 0.5f;
				foreach (PikeChargeComponent pikeChargeComponent in this.components)
				{
					if (!pikeChargeComponent.anticipation.active && pikeChargeComponent.agent.aliveAndGrounded.active && this.startSpot.distanceField.SampleDistance(pikeChargeComponent.agent.navPos) < (1f - num) * 0.5f)
					{
						pikeChargeComponent.StartAnticipation(this.dir);
					}
				}
				if (num > 1f)
				{
					this.charging.SetActive(true);
				}
			};
			AgentState agentState = this.charging;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				foreach (PikeChargeComponent pikeChargeComponent in this.components)
				{
					pikeChargeComponent.StartCharging();
				}
				this.hitAgents.Clear();
				this.energy = 1f;
				EventManager.Instance.PostEvent(PikeChargeAbility.chargeSound[this.upgradeLevel], this.squad.heroAgent.gameObject);
			}));
			this.charging.OnUpdate += delegate()
			{
				Vector3 vector = Vector3.Lerp(this.start.pos, this.end.pos, this.charging.timeSinceActivation / this.duration);
				if (!this.current.MoveTo(vector))
				{
					this.current = new NavPos(this.current.navigationMesh, vector, true, 1f);
				}
				this.energy = Mathf.MoveTowards(this.energy, 1f, Time.deltaTime * this.settings.speed);
				if (this.charging.timeSinceActivation < this.duration)
				{
					foreach (Agent agent in AgentEnumerators.GetStaticListRadius(vector + this.dir * this.spearLength / 2f, this.radius, this.squad.faction.enemy))
					{
						if (!this.hitAgents.Contains(agent))
						{
							Spear spear = this.components[0].spear;
							float num = float.MaxValue;
							foreach (PikeChargeComponent pikeChargeComponent in this.components)
							{
								if (pikeChargeComponent.spear && (pikeChargeComponent.spear.spearMidPos - agent.chestPos).sqrMagnitude < num)
								{
									spear = pikeChargeComponent.spear;
									num = (pikeChargeComponent.spear.spearMidPos - agent.chestPos).sqrMagnitude;
								}
							}
							Vector3 pos = agent.chestPos - this.dir * agent.radius * 0.7f;
							Attack attack = new Attack(this.attackSettings, this.dir, pos, spear, this.squad, "Spear", ScriptableObjectSingleton<PrefabManager>.instance.hitEffect);
							attack.settings.damage = attack.settings.damage * this.energy;
							agent.DealDamage(attack);
							this.hitAgents.Add(agent);
							this.energy *= 0.8f;
							break;
						}
					}
				}
				else
				{
					foreach (Agent agent2 in AgentEnumerators.GetStaticListRadius(this.end.pos, this.radius, this.squad.faction.enemy))
					{
						if (agent2.aliveAndGrounded.active)
						{
							Vector3 direction = (agent2.transform.position - this.end.pos).normalized * 0.6f + this.dir;
							Vector3 pos2 = Vector3.MoveTowards(agent2.transform.position, this.end.pos, agent2.radius * 0.7f) + agent2.chestOffset;
							Attack attack2 = new Attack(this.attackSettings, direction, pos2, this, this.squad, "Spear", null);
							attack2.settings.damage = attack2.settings.damage * 0.3f;
							attack2.settings.knockback = attack2.settings.knockback + 2f;
							agent2.DealDamage(attack2);
						}
					}
					arrived.SetActive(true);
				}
			};
			arrived.OnUpdate += delegate()
			{
				if (arrived.timeSinceActivation > 0.3f)
				{
					this.OnEnded();
				}
			};
			AgentState active2 = this.active;
			active2.OnDeactivate = (Action)Delegate.Combine(active2.OnDeactivate, new Action(delegate()
			{
				foreach (PikeChargeComponent pikeChargeComponent in this.components)
				{
					pikeChargeComponent.AbilityDone();
				}
				EventManager.Instance.PostEvent(PikeChargeAbility.chargeSound[this.upgradeLevel], EventAction.StopSound, this.squad.heroAgent.gameObject);
			}));
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000F0E78 File Offset: 0x000EF278
		protected override void DoTargetedAction(NavSpot heroNavSpot, NavSpot target)
		{
			SquadMover.MoveToDirect(base.squad, target, true);
			this.startSpot = heroNavSpot;
			this.endSpot = target;
			this.start = this.startSpot.vert.navPos;
			this.end = this.endSpot.vert.navPos;
			this.current = this.start;
			Vector3 vector = this.end.pos - this.start.pos;
			this.dir = vector.normalized;
			this.duration = vector.magnitude / this.settings.speed;
			Bounds bounds = heroNavSpot.bounds;
			bounds.extents += Vector3.one / 2f;
			foreach (PikeChargeComponent pikeChargeComponent in this.compoonentsInFormation)
			{
				if (pikeChargeComponent.agent.aliveAndGrounded.active)
				{
					pikeChargeComponent.StartAnticipation(this.dir);
				}
			}
			foreach (PikeChargeComponent pikeChargeComponent2 in this.components)
			{
				if (pikeChargeComponent2.spear)
				{
					float num = Mathf.Sqrt((float)this.components.Count);
					float num2 = pikeChargeComponent2.spear.spearLength;
					this.radius = pikeChargeComponent2.agent.radius * num * 1.2f + 0.3f;
					this.spearOffset = num2 * 0.3f * this.dir;
					this.attackSettings = pikeChargeComponent2.spear.attackSetting + this.settings.attackSettings;
					this.attackSettings.damage = this.attackSettings.damage * (1f + num * 0.1f);
					break;
				}
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000F10A4 File Offset: 0x000EF4A4
		public override bool IsBlocked()
		{
			return base.IsBlocked() || !this.inFormation;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000F10C0 File Offset: 0x000EF4C0
		public override void UpdateSquadSelected()
		{
			base.UpdateSquadSelected();
			this.compoonentsInFormation.Clear();
			if (base.squad.heroAgent.aliveAndGrounded.active)
			{
				NavSpot navSpot = (!base.heroNavSpot) ? base.squad.GetHeroNavSpot() : base.heroNavSpot;
				Agent heroAgent = base.squad.heroAgent;
				Vector3 position = heroAgent.transform.position;
				if ((navSpot.bounds.ClosestPoint(position) - position).sqrMagnitude < 0.040000003f)
				{
					float num = 1f;
					num *= num;
					foreach (PikeChargeComponent pikeChargeComponent in this.components)
					{
						if (!pikeChargeComponent.spear || (pikeChargeComponent.agent.transform.position - position).sqrMagnitude < num)
						{
							this.compoonentsInFormation.Add(pikeChargeComponent);
						}
					}
				}
			}
			this.inFormation = (this.compoonentsInFormation.Count > 1 + this.components.Count / 3);
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000F121C File Offset: 0x000EF61C
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			string text;
			pv = (text = null);
			pn = text;
			if (this.components.Count <= 1)
			{
				return "UPGRADES/COMMON/TOOLTIPS/PIKE_CHARGE_TOO_FEW";
			}
			if (!this.inFormation)
			{
				return "UPGRADES/COMMON/TOOLTIPS/PIKE_CHARGE_FORMATION";
			}
			return base.GetNotificationTerm(out pn, out pv);
		}

		// Token: 0x04002609 RID: 9737
		[Header("Pike Charge")]
		[SerializeField]
		private PikeChargeAbility.Settings[] levelSettings = new PikeChargeAbility.Settings[3];

		// Token: 0x0400260B RID: 9739
		public List<PikeChargeComponent> components;

		// Token: 0x0400260C RID: 9740
		private List<PikeChargeComponent> compoonentsInFormation;

		// Token: 0x0400260D RID: 9741
		public AgentState charging;

		// Token: 0x0400260E RID: 9742
		public NavSpot startSpot;

		// Token: 0x0400260F RID: 9743
		public NavSpot endSpot;

		// Token: 0x04002610 RID: 9744
		public NavPos start;

		// Token: 0x04002611 RID: 9745
		public NavPos end;

		// Token: 0x04002612 RID: 9746
		public NavPos current;

		// Token: 0x04002613 RID: 9747
		public Vector3 dir;

		// Token: 0x04002614 RID: 9748
		public Vector3 spearOffset;

		// Token: 0x04002615 RID: 9749
		private float spearLength;

		// Token: 0x04002616 RID: 9750
		private float radius;

		// Token: 0x04002617 RID: 9751
		private AttackSettings attackSettings;

		// Token: 0x04002618 RID: 9752
		private float duration;

		// Token: 0x04002619 RID: 9753
		private bool inFormation;

		// Token: 0x0400261A RID: 9754
		private float energy = 1f;

		// Token: 0x0400261B RID: 9755
		private static FabricEventArray chargeSound = new FabricEventArray("Sfx/Ability/PikeCharge/Charge{0:00}", 1, 3);

		// Token: 0x0400261C RID: 9756
		private List<Agent> hitAgents = new List<Agent>();

		// Token: 0x02000858 RID: 2136
		[Serializable]
		public class Settings
		{
			// Token: 0x0400261D RID: 9757
			public AttackSettings attackSettings;

			// Token: 0x0400261E RID: 9758
			public float range = 3.25f;

			// Token: 0x0400261F RID: 9759
			public float speed = 4f;
		}
	}
}
