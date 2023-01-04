using System;
using UnityEngine;

namespace Voxels.TowerDefense.Upgrades
{
	// Token: 0x02000859 RID: 2137
	public class PikeChargeComponent : AgentComponent, IAttackResponder
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060037F7 RID: 14327 RVA: 0x000F1A1D File Offset: 0x000EFE1D
		// (set) Token: 0x060037F6 RID: 14326 RVA: 0x000F1A14 File Offset: 0x000EFE14
		public Spear spear { get; private set; }

		// Token: 0x060037F8 RID: 14328 RVA: 0x000F1A28 File Offset: 0x000EFE28
		public PikeChargeComponent SetupPikeChargeComponent(PikeChargeAbility pikeChargeAbility)
		{
			this.pikeCharge = new AgentState("PikeCharge", base.agent.exclusives, false, true);
			this.anticipation = new AgentState("Anticipation", this.pikeCharge, false, true);
			this.charge = new AgentState("Charge", this.pikeCharge, false, true);
			this.travelling = new AgentState("Travelling", this.charge, false, true);
			this.arrived = new AgentState("Arrived", this.charge, false, true);
			this.pikeChargeAbility = pikeChargeAbility;
			AgentState agentState = this.charge;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(delegate(bool x)
			{
				this.travelling.SetActive(x);
			}));
			this.spear = base.GetComponent<Spear>();
			this.formation = base.GetComponent<EnglishFormationAgent>();
			this.pikeCharge.OnUpdate += delegate()
			{
				if (this.spear)
				{
					this.spear.idealSpearTipDir = this.dir;
				}
			};
			this.anticipation.OnUpdate += delegate()
			{
			};
			this.charge.OnUpdate += delegate()
			{
				NavPos current = pikeChargeAbility.current;
				current.pos += this.positionInFormation;
				this.agent.navPos = current;
				this.agent.LookInDirection(this.dir, 720f, 20f);
			};
			this.travelling.OnUpdate += delegate()
			{
				this.agent.movability = 0.2f;
				this.agent.enemyMovability = 0.1f;
			};
			return this;
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000F1B80 File Offset: 0x000EFF80
		private Attack GetAttack(Agent enemy)
		{
			Vector3 pos = (this.spear.spearMidPos + enemy.chestPos) / 2f;
			Vector3 direction = Vector3.up + (enemy.chestPos - base.agent.chestPos).normalized + UnityEngine.Random.insideUnitSphere * 0.3f;
			AttackSettings settings = this.spear.attackSetting + this.pikeChargeAbility.settings.attackSettings;
			settings.damage *= this.energy;
			Attack result = new Attack(settings, direction, pos, this.spear, base.squad, PikeChargeComponent.attackPrefix, null);
			return result;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000F1C40 File Offset: 0x000F0040
		public void StartAnticipation(Vector3 dir)
		{
			if (base.agent.aliveAndGrounded.active)
			{
				this.dir = dir;
				this.anticipation.SetActive(true);
			}
			this.positionInFormation = this.formation.orderPos.idealFormPos;
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000F1C8C File Offset: 0x000F008C
		public void StartCharging()
		{
			if (this.anticipation.active)
			{
				this.charge.SetActive(true);
			}
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000F1CAB File Offset: 0x000F00AB
		public void AbilityDone()
		{
			if (this.pikeCharge.SetActive(false) && this.spear)
			{
				this.spear.spearDown.SetActive(true);
			}
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000F1CE0 File Offset: 0x000F00E0
		void IAttackResponder.ModifyAttack(ref Attack attack)
		{
			if (this.pikeCharge.active)
			{
				attack.ignore = true;
			}
		}

		// Token: 0x04002620 RID: 9760
		public AgentState pikeCharge;

		// Token: 0x04002621 RID: 9761
		public AgentState anticipation;

		// Token: 0x04002622 RID: 9762
		public AgentState charge;

		// Token: 0x04002623 RID: 9763
		public AgentState travelling;

		// Token: 0x04002624 RID: 9764
		public AgentState arrived;

		// Token: 0x04002625 RID: 9765
		private PikeChargeAbility pikeChargeAbility;

		// Token: 0x04002626 RID: 9766
		private Vector3 dir;

		// Token: 0x04002627 RID: 9767
		private Agent target;

		// Token: 0x04002628 RID: 9768
		public float walkSpeed;

		// Token: 0x04002629 RID: 9769
		private float walkLerp;

		// Token: 0x0400262B RID: 9771
		private EnglishFormationAgent formation;

		// Token: 0x0400262C RID: 9772
		public float energy;

		// Token: 0x0400262D RID: 9773
		private const float energyDepletePerHit = 0.5f;

		// Token: 0x0400262E RID: 9774
		private const float energyRegainSpeed = 0.75f;

		// Token: 0x0400262F RID: 9775
		private static string attackPrefix = "Sfx/Ability/PikeCharge";

		// Token: 0x04002630 RID: 9776
		private Vector3 positionInFormation;
	}
}
