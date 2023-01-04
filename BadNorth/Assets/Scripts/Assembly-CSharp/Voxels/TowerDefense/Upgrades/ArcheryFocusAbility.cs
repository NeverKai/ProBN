using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.Ballistics;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000842 RID: 2114
	public class ArcheryFocusAbility : NavSpotTargetableAbility, IThreat
	{
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600373E RID: 14142 RVA: 0x000ED7B6 File Offset: 0x000EBBB6
		private ArcherSquadCoordinator archerySquad
		{
			get
			{
				return (!base.squad) ? null : base.squad.GetSquadCoordinator<ArcherSquadCoordinator>();
			}
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000ED7DC File Offset: 0x000EBBDC
		protected override void DoSquadSpawnAction_Implementation()
		{
			base.DoSquadSpawnAction_Implementation();
			this.settings = this.levelSettings[base.upgradeLevel];
			this.startAudioId = base.squad.hero.voice.archeryFocusStartAudioId.GetElementClamped(base.upgradeLevel);
			this.stopAudioId = base.squad.hero.voice.archeryFocusStopAudioId.GetElementClamped(base.upgradeLevel);
			this.active.OnUpdate += delegate()
			{
				if (this.active.timeSinceActivation > this.threatStartTime)
				{
					foreach (Agent agent in AgentEnumerators.GetStaticListRadius(this.targetPos, 1f, base.squad.faction.enemy))
					{
						agent.rangeWorry.PoseThreat(this);
					}
				}
				for (int i = this.components.Count - 1; i >= 0; i--)
				{
					ArcheryFocusComponent archeryFocusComponent = this.components[i];
					if (!archeryFocusComponent || !archeryFocusComponent.focus.active)
					{
						this.components.RemoveAt(i);
					}
				}
				if (this.components.Count == 0)
				{
					FabricWrapper.PostEvent(this.stopAudioId, base.squad.heroAgent.gameObject);
					base.OnEnded();
				}
			};
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x000ED870 File Offset: 0x000EBC70
		protected override void DoTargetedAction(NavSpot heroNavSpot, NavSpot target)
		{
			this.navSpot = target;
			ArcherSquadCoordinator archerySquad = this.archerySquad;
			if (archerySquad)
			{
				this.targetPos = target.navPos.pos;
				Vector3 vector = target.navPos.pos - base.squad.bounds.center;
				this.direction = vector.GetZeroY().normalized;
				this.tangent = new Vector3(this.direction.z, 0f, -this.direction.x);
				this.components.Clear();
				float impactTime = this.settings.trajectoryCalculator.Sample(vector).impactTime;
				this.threatStartTime = impactTime - 0.2f;
				this.threatEndTime = impactTime + 1f;
				foreach (Archery archery in archerySquad.archers)
				{
					if (archery.brainState.active)
					{
						ArcheryFocusComponent orAddComponent = archery.gameObject.GetOrAddComponent<ArcheryFocusComponent>();
						orAddComponent.MaybeSetup();
						orAddComponent.order = Vector3.Dot(this.tangent, archery.transform.position - base.squad.bounds.center);
						int num = 0;
						while (num < this.components.Count && orAddComponent.order > this.components[num].order)
						{
							num++;
						}
						this.components.Insert(num, orAddComponent);
					}
				}
				this.count = this.components.Count;
				float a = 0.1f;
				this.tangent *= Mathf.Lerp(a, 1.5f / (float)this.count, 0.5f);
				this.direction *= Mathf.Lerp(a, 1.5f / (float)this.settings.ammo, 0.5f);
				for (int i = 0; i < this.components.Count; i++)
				{
					ArcheryFocusComponent archeryFocusComponent = this.components[i];
					Vector3 a2 = this.navSpot.navPos.pos;
					a2 -= this.tangent * ((float)(this.count - 1) / 2f - (float)i);
					a2 -= this.direction * (float)(this.settings.ammo - 1) * 0.5f;
					archeryFocusComponent.ShootAt(this, this.settings, a2, this.direction);
				}
				FabricWrapper.PostEvent(this.startAudioId, base.squad.heroAgent.gameObject);
			}
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x000EDB70 File Offset: 0x000EBF70
		protected override void OnEffectEnded()
		{
			base.OnEffectEnded();
			this.navSpot = null;
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x000EDB7F File Offset: 0x000EBF7F
		public void ModifyArrow(Arrow arrow)
		{
			arrow.onHitGround = new Action<Arrow>(this.OnArrowHitGround);
			arrow.attackSettings += this.settings.attackSettings;
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000EDBAF File Offset: 0x000EBFAF
		public override bool IsBlocked()
		{
			return base.IsBlocked() || base.squad.livingAgents.Count <= 1;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000EDBD8 File Offset: 0x000EBFD8
		public void OnArrowHitGround(Arrow arrow)
		{
			float radius = 0.5f;
			List<Agent> staticListRadiusSorted = AgentEnumerators.GetStaticListRadiusSorted(arrow.transform.position, radius, null);
			Singleton<DustParticles>.instance.SpawnParticles(arrow.transform.position, Vector3.up);
			AttackSettings attackSettings = this.settings.attackSettings;
			attackSettings.damage *= 0.3f;
			int num = 0;
			foreach (Agent agent in staticListRadiusSorted)
			{
				Vector3 vector = agent.transform.position - arrow.transform.position;
				Vector3 pos = Vector3.MoveTowards(agent.chestPos, arrow.transform.position, agent.radius * 0.5f);
				Attack attack = new Attack(attackSettings, vector, pos, this, base.squad, "Stun", null);
				if (agent.faction == base.squad.faction)
				{
					attack.damage = 0f;
					attack.stun *= 0.2f;
					attack.knockback *= 0.3f;
				}
				agent.DealDamage(attack);
				attackSettings.damage *= 0.3f;
				attackSettings.stun *= 0.7f;
				attackSettings.knockback *= 0.5f;
				if (++num > 4)
				{
					break;
				}
			}
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000EDD88 File Offset: 0x000EC188
		protected override string GetNotificationTerm(out string pn, out string pv)
		{
			if (base.squad.livingAgents.Count <= 1)
			{
				string text;
				pv = (text = null);
				pn = text;
				return "UPGRADES/COMMON/TOOLTIPS/VOLLEY_TOO_FEW";
			}
			return base.GetNotificationTerm(out pn, out pv);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000EDDC1 File Offset: 0x000EC1C1
		Vector3 IThreat.GetPos(Agent victim)
		{
			return base.squad.bounds.center;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000EDDD4 File Offset: 0x000EC1D4
		Vector3 IThreat.GetThreatDir(Agent victim)
		{
			return (base.squad.bounds.center - victim.transform.position).normalized + Vector3.up;
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000EDE14 File Offset: 0x000EC214
		float IThreat.GetThreatDistance(Agent victim)
		{
			return (base.squad.bounds.center - victim.transform.position).sqrMagnitude;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000EDE4C File Offset: 0x000EC24C
		bool IThreat.GetTreatValid(Agent victim)
		{
			return this.active.timeSinceDeactivation < this.threatEndTime && (victim.transform.position - this.targetPos).sqrMagnitude < 1f;
		}

		// Token: 0x0400257F RID: 9599
		[Header("Archery Focus")]
		[SerializeField]
		private TrajectoryUtility trajectoryCalculator;

		// Token: 0x04002580 RID: 9600
		[SerializeField]
		private ArcheryFocusComponent.Settings[] levelSettings = new ArcheryFocusComponent.Settings[3];

		// Token: 0x04002581 RID: 9601
		private ArcheryFocusComponent.Settings settings;

		// Token: 0x04002582 RID: 9602
		private FabricEventReference startAudioId;

		// Token: 0x04002583 RID: 9603
		private FabricEventReference stopAudioId;

		// Token: 0x04002584 RID: 9604
		private NavSpot navSpot;

		// Token: 0x04002585 RID: 9605
		[NonSerialized]
		public Vector3 direction;

		// Token: 0x04002586 RID: 9606
		[NonSerialized]
		public Vector3 tangent;

		// Token: 0x04002587 RID: 9607
		private List<ArcheryFocusComponent> components = new List<ArcheryFocusComponent>(8);

		// Token: 0x04002588 RID: 9608
		private int count;

		// Token: 0x04002589 RID: 9609
		private Vector3 targetPos;

		// Token: 0x0400258A RID: 9610
		private const float targetRadius = 1f;

		// Token: 0x0400258B RID: 9611
		private float threatStartTime;

		// Token: 0x0400258C RID: 9612
		private float threatEndTime;
	}
}
