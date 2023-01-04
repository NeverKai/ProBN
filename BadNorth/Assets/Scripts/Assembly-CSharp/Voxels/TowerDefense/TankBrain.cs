using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200069B RID: 1691
	public class TankBrain : CloseCombatBrain
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x0009F6B8 File Offset: 0x0009DAB8
		public float range
		{
			get
			{
				return base.agent.radius * 0.7f;
			}
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x0009F6CC File Offset: 0x0009DACC
		public override void Setup()
		{
			base.Setup();
			base.agent.dangerous = true;
			this.idle = new AgentState("Idle", this.brainState, false, true);
			this.pursuing = new AgentState("Pursuing", this.brainState, false, true);
			this.attack = new AgentState("Attacking", this.brainState, false, true);
			this.recover = new AgentState("Recovering", this.brainState, false, true);
			AgentState brainState = this.brainState;
			brainState.OnChange = (Action<bool>)Delegate.Combine(brainState.OnChange, new Action<bool>(base.agent.SetDangerous));
			AgentState brainState2 = this.brainState;
			brainState2.OnEmpty = (Action)Delegate.Combine(brainState2.OnEmpty, new Action(this.idle.SetActiveTrue));
			this.idle.OnUpdate += this.IdleUpdate;
			this.pursuing.OnUpdate += this.PursuitUpdate;
			this.recover.OnUpdate += this.RecoverUpdate;
			this.attack.OnUpdate += this.AttackUpdate;
			AgentState agentState = this.attack;
			agentState.OnDeactivate = (Action)Delegate.Combine(agentState.OnDeactivate, new Action(delegate()
			{
				this.target = null;
			}));
			AgentState agentState2 = this.attack;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.idle.SetActive(true);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0009F860 File Offset: 0x0009DC60
		public override void OnProximity(Agent otherAgent)
		{
			if (!base.agent.aliveAndGrounded.active)
			{
				return;
			}
			float sqrMagnitude = (otherAgent.navPos.pos - base.agent.navPos.pos).sqrMagnitude;
			float num = 1f;
			if (sqrMagnitude > num * num)
			{
				return;
			}
			float @in = base.agent.radius + this.range + otherAgent.radius;
			float soft = ExtraMath.RemapValue(Mathf.Sqrt(sqrMagnitude), @in, num, 1f, 0f);
			otherAgent.Intimidate(soft, 0f);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0009F8FC File Offset: 0x0009DCFC
		private void IdleUpdate()
		{
			this.stamina = Mathf.MoveTowards(this.stamina, 1f, Time.deltaTime * 0.2f);
			if (base.agent.beats.hz2)
			{
				Brain enemyBrain = base.agent.enemyBrain;
				if (base.agent.enemyDist < 2f)
				{
					this.pursuing.SetActive(true);
					return;
				}
				if (base.MaybeAct())
				{
					return;
				}
			}
			this.order.ApplyOrder();
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0009F988 File Offset: 0x0009DD88
		private void PursuitUpdate()
		{
			this.stamina = Mathf.MoveTowards(this.stamina, 1f, Time.deltaTime * 0.2f);
			base.agent.walkDir = base.agent.enemyDir;
			base.agent.LookInDirection(base.agent.enemyDir, 720f, 20f);
			if (base.agent.enemyAgent)
			{
				Agent enemyAgent = base.agent.enemyAgent;
				if (this.Attack(enemyAgent))
				{
					return;
				}
			}
			if (base.agent.enemyDist > 3f)
			{
				this.idle.SetActive(true);
				return;
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0009FA40 File Offset: 0x0009DE40
		private void RecoverUpdate()
		{
			base.agent.LookInDirection(base.agent.enemyDir, 720f, 20f);
			base.agent.walkDir -= base.agent.enemyDir * ExtraMath.RemapValue(base.agent.enemyDist, 0.2f, 0.3f + base.agent.radius, 1f, 0f);
			if (this.recover.timeSinceActivation > 1f)
			{
				this.stamina = 1f;
				if (base.agent.enemyDist < 2f)
				{
					this.pursuing.SetActive(true);
				}
				else
				{
					this.idle.SetActive(true);
				}
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0009FB18 File Offset: 0x0009DF18
		private void AttackUpdate()
		{
			if (base.agent.animationDone)
			{
				this.stamina -= this.attackStaminaCost;
				if (this.stamina < 0f)
				{
					this.recover.SetActive(true);
				}
				else
				{
					this.pursuing.SetActive(true);
				}
				return;
			}
			if (this.target && this.target.aliveAndGrounded.active)
			{
				Vector3 a = this.target.lPos - base.agent.lPos;
				float magnitude = a.magnitude;
				Vector3 a2 = a / magnitude;
				base.agent.walkDir = a2 * (magnitude - base.agent.radius - this.target.radius);
				base.agent.SetDirection(this.target.transform.position - base.transform.position);
			}
			else
			{
				base.agent.walkDir = Vector3.zero;
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0009FC34 File Offset: 0x0009E034
		public override Attack GetAttack(Agent target)
		{
			Vector3 vector = target.transform.position - base.agent.transform.position;
			Vector3 pos = (target.wChestPos + base.agent.wChestPos) / 2f;
			return new Attack(this.damage, this.knockback, 0f, vector.normalized, pos, this, base.agent.squad, this.swordSound, null);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0009FCB4 File Offset: 0x0009E0B4
		public bool Attack(Agent targetAgent)
		{
			if (!targetAgent)
			{
				return false;
			}
			if (!base.agent.body.hopping.active)
			{
				return false;
			}
			float num = base.agent.radius + targetAgent.radius + this.range;
			Vector3 pos = targetAgent.navPos.pos;
			Vector3 pos2 = base.agent.navPos.pos;
			if ((pos - pos2).sqrMagnitude > num * num)
			{
				return false;
			}
			this.attack.SetActive(true);
			this.target = targetAgent;
			base.agent.PlayAnimation(TankBrain.attackId);
			FabricWrapper.PostEvent(this.swingSound);
			Swordsman swordsman = targetAgent.brain as Swordsman;
			if (swordsman && swordsman.shield)
			{
				swordsman.shield.MaybeParry(this);
			}
			return true;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0009FDA4 File Offset: 0x0009E1A4
		public void Hit()
		{
			if (this.target && this.attack.active)
			{
				Vector3 vector = this.target.transform.position - base.agent.transform.position;
				float num = vector.magnitude;
				num -= base.agent.radius + this.target.radius + this.range;
				if (num < 0f)
				{
					Vector3 pos = (this.target.wChestPos + base.agent.wChestPos) / 2f;
					Attack attack = new Attack(this.damage, this.knockback, 0f, vector.normalized, pos, this, base.agent.squad, this.swordSound, null);
					this.target.DealDamage(attack);
					Singleton<CameraShaker>.instance.ShakeOnce(0.05f * attack.damage);
				}
				this.target = null;
			}
		}

		// Token: 0x04001C68 RID: 7272
		[Header("Editor")]
		public float damage = 1f;

		// Token: 0x04001C69 RID: 7273
		public float knockback = 1f;

		// Token: 0x04001C6A RID: 7274
		private static int attackId = Animator.StringToHash("Attack");

		// Token: 0x04001C6B RID: 7275
		public Agent target;

		// Token: 0x04001C6C RID: 7276
		private AgentState idle;

		// Token: 0x04001C6D RID: 7277
		private AgentState pursuing;

		// Token: 0x04001C6E RID: 7278
		private AgentState attack;

		// Token: 0x04001C6F RID: 7279
		private AgentState recover;

		// Token: 0x04001C70 RID: 7280
		[Header("Sound")]
		public string swingSound = "Sfx/English/Sword/Swing";

		// Token: 0x04001C71 RID: 7281
		public string swordSound = "Sfx/English/Sword";

		// Token: 0x04001C72 RID: 7282
		public string chargeSound = "Sfx/English/SwordShield/Charge";

		// Token: 0x04001C73 RID: 7283
		private const float recoverTime = 1f;

		// Token: 0x04001C74 RID: 7284
		private float attackStaminaCost = 0.3f;

		// Token: 0x04001C75 RID: 7285
		private float stamina = 1f;
	}
}
