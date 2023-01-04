using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000697 RID: 1687
	public class Swordsman : CloseCombatBrain, IAttackResponder
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06002B61 RID: 11105 RVA: 0x0009D3DE File Offset: 0x0009B7DE
		public float damage
		{
			get
			{
				return this.damageLevels[base.agent.squad.level];
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0009D3F7 File Offset: 0x0009B7F7
		public float knockback
		{
			get
			{
				return this.knockbackLevels[base.agent.squad.level];
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0009D410 File Offset: 0x0009B810
		public float stun
		{
			get
			{
				return this.stunLevels[base.agent.squad.level];
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x0009D429 File Offset: 0x0009B829
		public float range
		{
			get
			{
				return base.agent.radius * 0.7f;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x0009D43C File Offset: 0x0009B83C
		private float inverseStamina
		{
			get
			{
				return 1f - this.stamina;
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0009D44A File Offset: 0x0009B84A
		public void SetSpearSquadCoordinator(SpearSquadCoordinator spearSquad)
		{
			this.spearSquad = spearSquad;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x0009D453 File Offset: 0x0009B853
		private float walkedDistance
		{
			get
			{
				return base.agent.walkedDistance - this.startDistance;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x0009D467 File Offset: 0x0009B867
		private bool isLeader
		{
			get
			{
				return this.isStandard && base.squad.agents.Count > 1;
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0009D48A File Offset: 0x0009B88A
		private void StartCountingDistance()
		{
			this.startDistance = base.agent.walkedDistance;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0009D4A0 File Offset: 0x0009B8A0
		public override void Setup()
		{
			base.Setup();
			this.shield = base.GetComponent<Shield>();
			this.idle = new AgentState("Idle", this.brainState, false, true);
			this.recover = new AgentState("Recover", this.brainState, false, true);
			this.ready = new AgentState("Ready", this.brainState, false, true);
			this.pursuing = new AgentState("Ready", this.brainState, false, true);
			this.hunting = new AgentState("Hunting", this.brainState, false, true);
			this.attack = new AgentState("Attacking", this.brainState, false, true);
			this.clash = new AgentState("Clashing", this.brainState, false, true);
			base.agent.dangerous = true;
			AgentState brainState = this.brainState;
			brainState.OnChange = (Action<bool>)Delegate.Combine(brainState.OnChange, new Action<bool>(base.agent.SetDangerous));
			AgentState brainState2 = this.brainState;
			brainState2.OnEmpty = (Action)Delegate.Combine(brainState2.OnEmpty, new Action(this.idle.SetActiveTrue));
			this.brainState.OnUpdate += this.UpdateStamina;
			this.idle.OnUpdate += this.IdleUpdate;
			AgentState agentState = this.ready;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this.target = null;
			}));
			AgentState agentState2 = this.ready;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				base.DebugMessage("Ready");
			}));
			this.ready.OnUpdate += this.ReadyUpdate;
			this.hunting.OnUpdate += this.HuntingUpdate;
			AgentState agentState3 = this.hunting;
			agentState3.OnActivate = (Action)Delegate.Combine(agentState3.OnActivate, new Action(this.StartCountingDistance));
			AgentState agentState4 = this.hunting;
			agentState4.OnDeactivate = (Action)Delegate.Combine(agentState4.OnDeactivate, new Action(delegate()
			{
				this.target = null;
			}));
			AgentState agentState5 = this.pursuing;
			agentState5.OnActivate = (Action)Delegate.Combine(agentState5.OnActivate, new Action(this.StartCountingDistance));
			this.pursuing.OnUpdate += this.PursuitUpdate;
			this.attack.OnUpdate += this.AttackUpdate;
			AgentState agentState6 = this.attack;
			agentState6.OnDeactivate = (Action)Delegate.Combine(agentState6.OnDeactivate, new Action(delegate()
			{
				this.target = null;
			}));
			AgentState agentState7 = this.attack;
			agentState7.OnChange = (Action<bool>)Delegate.Combine(agentState7.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			AgentState agentState8 = this.clash;
			agentState8.OnActivate = (Action)Delegate.Combine(agentState8.OnActivate, new Action(this.ClashActivate));
			this.clash.OnUpdate += this.ClashUpdate;
			AgentState agentState9 = this.clash;
			agentState9.OnChange = (Action<bool>)Delegate.Combine(agentState9.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.brainState.OnUpdate += this.UpdateAdjecent;
			AgentState actingState = this.actingState;
			actingState.OnDeactivate = (Action)Delegate.Combine(actingState.OnDeactivate, new Action(this.EvaluateNewState));
			this.recover.OnUpdate += delegate()
			{
				this.ready.SetActive(base.agent.body.hopping.active);
			};
			AgentState agentState10 = this.recover;
			agentState10.OnDeactivate = (Action)Delegate.Combine(agentState10.OnDeactivate, new Action(delegate()
			{
				this.stamina = 0f;
			}));
			AgentState deadState = base.agent.deadState;
			deadState.OnActivate = (Action)Delegate.Combine(deadState.OnActivate, new Action(delegate()
			{
				foreach (Swordsman swordsman in this.adjecent)
				{
					if (swordsman)
					{
						swordsman.stamina -= 0.3f;
					}
				}
			}));
			this.isStandard = base.GetComponent<Standard>();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0009D890 File Offset: 0x0009BC90
		public void EvaluateNewState()
		{
			if (!base.agent.aliveAndGrounded.active)
			{
				return;
			}
			if (base.agent.body.sliding.active)
			{
				this.recover.SetActive(true);
				return;
			}
			if (this.stamina > 0.1f)
			{
				for (int i = 0; i < this.adjecent.Length; i++)
				{
					Swordsman swordsman = this.adjecent[i];
					if (swordsman)
					{
						if (swordsman.pursuing.active)
						{
							this.pursuing.SetActive(true);
							return;
						}
						if (swordsman.hunting.active)
						{
							this.hunting.SetActive(true);
							return;
						}
					}
				}
			}
			this.ready.SetActive(true);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0009D964 File Offset: 0x0009BD64
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

		// Token: 0x06002B6D RID: 11117 RVA: 0x0009DA00 File Offset: 0x0009BE00
		public void UpdateStamina()
		{
			for (int i = 0; i < this.adjecent.Length; i++)
			{
				if (this.adjecent[i])
				{
					float num = (this.stamina - this.adjecent[i].stamina) * 0.1f;
					this.adjecent[i].stamina += num;
					this.stamina -= num;
				}
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0009DA7C File Offset: 0x0009BE7C
		private void IdleUpdate()
		{
			this.stamina = 1f;
			if (base.agent.beats.hz8)
			{
				if (base.MaybeAct())
				{
					return;
				}
				if (base.agent.enemyDist < 4f)
				{
					this.ready.SetActive(true);
					return;
				}
			}
			this.order.ApplyOrder();
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0009DAE4 File Offset: 0x0009BEE4
		private void ReadyUpdate()
		{
			using ("Swordsman.ReadyUpdate()")
			{
				this.stamina = Mathf.MoveTowards(this.stamina, 1f, Time.deltaTime);
				Vector3 vector = base.agent.orderDir;
				vector *= ExtraMath.RemapValue(base.agent.orderDist, 1f, 0f, 0.1f, 1f);
				base.agent.walkDir = vector * Mathf.Clamp01(this.stamina);
				if (base.agent.hardIntimidation > 0f)
				{
					base.agent.walkDir -= base.agent.enemyDir * base.agent.hardIntimidation * 2f;
				}
				if (base.agent.softIntimidation > 0f)
				{
					if (base.agent.isViking)
					{
						base.agent.walkDir *= 0.1f;
					}
					else if (base.agent.enemyAgent && base.agent.orderDist < 0.5f && base.agent.enemyAgent.brain.order.GetOrderDist(base.enSquad.pather.target.navPos) < base.agent.enemyAgent.orderDist)
					{
						base.agent.walkDir += base.agent.enemyAgent.brain.order.GetOrderDir(base.agent.navPos) * 0.3f;
					}
					float num = base.agent.softIntimidation * 1f;
					num *= Mathf.Lerp(1.4f, 0.9f, base.agent.friendRatio);
					num *= Mathf.Lerp(1.4f, 0.9f, this.stamina);
					Vector3 vector2 = -base.agent.enemyDir * num;
					if (base.agent.isEnglish && base.agent.orderDist < 2f)
					{
						vector2 = ExtraMath.ClampVectorToDirection(vector2, vector, 0.5f);
					}
					base.agent.walkDir += vector2;
				}
				else
				{
					base.agent.walkDir -= base.agent.enemyDir * ExtraMath.RemapValue(base.agent.enemyDist, 0.1f, 0.4f, 1f, 0f);
				}
				if (base.agent.beats.hz2)
				{
					Brain enemyBrain = base.agent.enemyBrain;
					if ((!this.isLeader || base.agent.orderDist >= 1f) && base.agent.enemyAgent && (base.agent == base.agent.enemyAgent.enemyAgent || !base.agent.enemyAgent.enemyData.dangerous) && this.stamina > 0f && ((base.agent.isEnglish && (this.order.GetOrderDist(base.agent.enemyAgent.navPos) < 1f || base.agent.intimidated) && base.agent.orderDist < 0.5f && base.agent.enemyAgent.orderDist < 0.2f) || ((base.agent.enemyDist < 1f || base.agent.intimidated) && Vector3.Dot(base.agent.enemyDir, base.agent.orderDir) > 0f && this.order.GetOrderDist(base.agent.enemyAgent.navPos) < base.agent.orderDist + 0.5f)))
					{
						this.Pursue("Blocked");
						return;
					}
					if (base.agent.enemyDist > 4f)
					{
						this.idle.SetActive(true);
						return;
					}
					if (this.ShouldHunt(base.agent.enemyAgent, (float)((!this.isLeader) ? 2 : 1)))
					{
						this.target = base.agent.enemyAgent;
						this.hunting.SetActive(true);
						return;
					}
					if (base.MaybeAct())
					{
						return;
					}
				}
				Vector3 dir = (Vector3.Dot(base.agent.enemyDir, base.agent.orderDir) <= 0f && !base.agent.intimidated && base.agent.enemyDist >= 2f) ? vector : base.agent.enemyDir;
				base.agent.LookInDirection(dir, 720f, 20f);
			}
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0009E070 File Offset: 0x0009C470
		private bool ShouldHunt(Agent victim, float huntDist)
		{
			return victim && victim.aliveAndGrounded.active && !victim.dangerous && ((victim == base.agent.enemyAgent && base.agent.enemyDist < huntDist * 0.5f && Vector3.Dot(base.agent.enemyDir, base.agent.orderDir) > 0f) || this.order.GetOrderDist(victim.navPos) < huntDist);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0009E118 File Offset: 0x0009C518
		private void Pursue(string message = "")
		{
			this.pursuing.SetActive(true);
			int num = this.BroadcastPursuit(base.agent.enemyDir);
			IslandGameplayManager.RequestCombatAudio(this.chargeSound, base.gameObject);
			num++;
			base.DebugMessage(message + " " + num);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0009E170 File Offset: 0x0009C570
		protected override void OnPathTargetChanged(IPathTarget target)
		{
			base.OnPathTargetChanged(target);
			NavSpot navSpot = target as NavSpot;
			if (!navSpot)
			{
				return;
			}
			if (this.hunting.active || this.pursuing.active)
			{
				this.ready.SetActive(true);
			}
			if (this.ready.active && base.agent.enemyAgent)
			{
				Bounds bounds = new Bounds(navSpot.bounds.center, navSpot.bounds.size * 1.5f);
				if (bounds.Contains(base.agent.transform.position))
				{
					this.Pursue("NavSpotAttack");
				}
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0009E238 File Offset: 0x0009C638
		private bool CanHitArcher(Agent targetAgent)
		{
			if (this.shield)
			{
				Archery archery = targetAgent.brain as Archery;
				if ((!archery || !archery.aiming.active || Vector3.Dot(archery.aimDir, base.agent.wPos - targetAgent.wPos) <= 0f) && this.Attack(targetAgent, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0009E2B8 File Offset: 0x0009C6B8
		private void HuntingUpdate()
		{
			if (this.target && !this.target.aliveAndGrounded.active)
			{
				this.target = null;
			}
			if (this.target != base.agent.enemyAgent)
			{
				if (this.ShouldHunt(base.agent.enemyAgent, 3f))
				{
					this.target = base.agent.enemyAgent;
				}
				else if (!this.ShouldHunt(this.target, 3f))
				{
					this.ready.SetActive(true);
					return;
				}
			}
			else if (!this.ShouldHunt(this.target, 3f))
			{
				this.ready.SetActive(true);
				return;
			}
			if (this.Attack(this.target, new Func<Agent, bool>(this.CanHitArcher)))
			{
				return;
			}
			if (base.agent.TriCast(this.target))
			{
				Vector3 vector = this.target.navPos.pos - base.agent.navPos.pos;
				base.agent.walkDir = vector;
				base.agent.LookInDirection(vector, 720f, 20f);
			}
			else
			{
				if (!base.agent.enemyData.hittable || base.agent.enemyData.dangerous)
				{
					this.ready.SetActive(true);
					return;
				}
				base.agent.walkDir = base.agent.enemyDir;
				base.agent.LookInDirection(base.agent.enemyDir, 720f, 20f);
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0009E480 File Offset: 0x0009C880
		private int BroadcastPursuit(Vector3 initiatorDir)
		{
			int num = 0;
			for (int i = 0; i < this.adjecent.Length; i++)
			{
				Swordsman swordsman = this.adjecent[i];
				if (swordsman)
				{
					if (swordsman.ready.active)
					{
						if (swordsman.stamina >= 0f)
						{
							if (Vector3.Dot(initiatorDir.normalized, swordsman.agent.enemyDir.normalized) >= 0.1f)
							{
								if (swordsman.agent.orderDist <= 1f || Vector3.Dot(swordsman.agent.orderDir, swordsman.agent.enemyDir) >= 0f)
								{
									swordsman.pursuing.SetActive(true);
									num++;
									num += swordsman.BroadcastPursuit(base.agent.enemyDir);
									swordsman.DebugMessage("A");
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0009E588 File Offset: 0x0009C988
		private void OnPursued(Agent pursuer)
		{
			if (this.stamina < 0.2f)
			{
				return;
			}
			if (!this.ready.active)
			{
				return;
			}
			if (!base.agent.navPos.TriCast(pursuer.navPos))
			{
				return;
			}
			if (base.agent.isEnglish)
			{
				if (this.spearSquad)
				{
					return;
				}
				if (Vector3.Dot(base.agent.orderDir, base.agent.enemyDir) < 0f)
				{
					return;
				}
			}
			int num = this.BroadcastPursuit(base.agent.enemyDir);
			if (this.ready.active && this.stamina > 0.3f)
			{
				num++;
				this.pursuing.SetActive(true);
			}
			if (num > 0)
			{
				base.DebugMessage("Counter " + num);
			}
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0009E67C File Offset: 0x0009CA7C
		private void PursuitUpdate()
		{
			this.stamina -= Time.deltaTime * 0.3f;
			if (this.stamina < 0f || this.walkedDistance > 2f)
			{
				base.DebugMessage("Tired");
				this.ready.SetActive(true);
				return;
			}
			base.agent.walkDir = base.agent.enemyDir;
			base.agent.LookInDirection(base.agent.enemyDir, 720f, 20f);
			if (base.agent.enemyAgent)
			{
				Agent enemyAgent = base.agent.enemyAgent;
				if (base.agent.isEnglish)
				{
					base.agent.walkDir += enemyAgent.brain.order.GetOrderDir(base.agent.navPos) * 0.3f;
				}
				if (enemyAgent.brain is Swordsman && base.agent.enemyDist < 0.2f + base.agent.radius + enemyAgent.radius + this.range)
				{
					(enemyAgent.brain as Swordsman).OnPursued(base.agent);
				}
				if (this.Attack(enemyAgent, null))
				{
					return;
				}
			}
			if (!base.agent.enemyBrain && base.agent.enemyDist > 1f + this.stamina)
			{
				base.DebugMessage("No enemy");
				this.ready.SetActive(true);
				return;
			}
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0009E82C File Offset: 0x0009CC2C
		private void AttackUpdate()
		{
			if (base.agent.animationDone)
			{
				this.stamina -= this.attackStaminaCost;
				this.attack.SetActive(false);
				if (this.stamina > 0f)
				{
					this.Attack(base.agent.enemyAgent, null);
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

		// Token: 0x06002B79 RID: 11129 RVA: 0x0009E948 File Offset: 0x0009CD48
		public void ClashActivate()
		{
			base.agent.PlayAnimation(Swordsman.clashId);
			base.agent.body.sliding.SetActive(true);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0009E978 File Offset: 0x0009CD78
		private void ClashUpdate()
		{
			if (base.agent.animationDone)
			{
				this.stamina -= 0.5f;
				this.clash.SetActive(false);
				if (this.stamina > 0f)
				{
					this.Attack(base.agent.enemyAgent, null);
				}
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0009E9D7 File Offset: 0x0009CDD7
		public bool MaybeAttack()
		{
			return base.agent.enemyDist < 0.7f && this.Attack(base.agent.enemyAgent, null);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0009EA03 File Offset: 0x0009CE03
		public bool MaybePursue()
		{
			if (base.agent.enemyDist < 0.7f && base.agent.aliveAndGrounded.active)
			{
				this.Pursue(string.Empty);
				return true;
			}
			return false;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0009EA40 File Offset: 0x0009CE40
		public bool Attack(Agent targetAgent, Func<Agent, bool> lastCheck = null)
		{
			if (!targetAgent)
			{
				return false;
			}
			if (!base.agent.body.hopping.active)
			{
				return false;
			}
			if (!base.agent.aliveAndGrounded.active)
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
			if (lastCheck != null && !lastCheck(targetAgent))
			{
				return false;
			}
			this.attack.SetActive(true);
			this.target = targetAgent;
			base.agent.PlayAnimation(Swordsman.attackId);
			IslandGameplayManager.RequestCombatAudio(this.swingSound, base.gameObject);
			Swordsman swordsman = targetAgent.brain as Swordsman;
			if (swordsman && swordsman.shield)
			{
				swordsman.shield.MaybeParry(this);
			}
			return true;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0009EB64 File Offset: 0x0009CF64
		public override Attack GetAttack(Agent target)
		{
			Vector3 vector = target.transform.position - base.agent.transform.position;
			Vector3 pos = (target.wChestPos + base.agent.wChestPos) / 2f;
			Attack result = new Attack(this.damage, this.knockback, 0f, vector.normalized, pos, this, base.agent.squad, this.swordSound, ScriptableObjectSingleton<PrefabManager>.instance.hitEffect);
			result.settings.stun = this.stun;
			return result;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0009EC02 File Offset: 0x0009D002
		public void FirstHit()
		{
			if (this.DoHit() && base.agent.enemyAgent)
			{
				this.target = base.agent.enemyAgent;
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0009EC35 File Offset: 0x0009D035
		public void Hit()
		{
			this.DoHit();
			this.target = null;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0009EC48 File Offset: 0x0009D048
		private bool DoHit()
		{
			if (this.target && this.attack.active)
			{
				float num = (this.target.transform.position - base.agent.transform.position).magnitude;
				num -= base.agent.radius + this.target.radius + this.range;
				if (num < 0f)
				{
					Attack attack = this.GetAttack(this.target);
					Singleton<CameraShaker>.instance.ShakeOnce(0.03f * attack.damage);
					return this.target.DealDamage(attack);
				}
			}
			return false;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0009ED04 File Offset: 0x0009D104
		[ContextMenu("AdjecentHighlight")]
		private void AdjecentHighlight()
		{
			for (int i = 0; i < this.adjecent.Length; i++)
			{
				if (this.adjecent[i] && this.adjecent[i].adjecentHightlight < 1f)
				{
					this.adjecent[i].adjecentHightlight = 1f;
					this.adjecent[i].AdjecentHighlight();
				}
			}
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0009ED74 File Offset: 0x0009D174
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Color black = Color.black;
				black.r = (float)((!base.agent.isViking) ? 0 : 1);
				black.b = (float)((!base.agent.isEnglish) ? 0 : 1);
				black.g = this.stamina;
				black.a = 0.4f;
				Gizmos.color = black;
				if (this.adjecent != null)
				{
					for (int i = 0; i < this.adjecent.Length; i++)
					{
						if (this.adjecent[i])
						{
							Gizmos.DrawLine(base.agent.transform.position, this.adjecent[i].transform.position);
						}
					}
				}
				Gizmos.DrawWireCube(base.agent.transform.position, new Vector3(1f, 0f, 1f) * 0.02f);
				Gizmos.color = base.agent.faction.color;
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0009EE94 File Offset: 0x0009D294
		public void ModifyAttack(ref Attack attack)
		{
			if (this.attack.active && attack.monoAttacker is Swordsman)
			{
				Swordsman swordsman = attack.monoAttacker as Swordsman;
				if (swordsman.attack.active && this.target == swordsman.agent && swordsman.agent.animationTime - base.agent.animationTime < 0.4f)
				{
					Vector3 pos = (base.agent.chestPos + swordsman.agent.chestPos) / 2f;
					ScriptableObjectSingleton<PrefabManager>.instance.swordClash.PlayAt(pos);
					attack.damage = 0f;
					this.clash.SetActive(true);
					swordsman.clash.SetActive(true);
					Attack attack2 = new Attack(0f, this.knockback, 0f, -attack.direction, pos, this, base.squad, this.swordSound, null);
					attack2.soundSuffix = "Clash";
					swordsman.agent.DealDamage(attack2);
					base.DebugMessage("Clash");
					attack.soundSuffix = "Clash";
				}
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x0009EFD0 File Offset: 0x0009D3D0
		private IEnumerator<Agent> adjecentEnumerator
		{
			get
			{
				if (this._ae == null)
				{
					this._ae = AgentEnumerators.MultiFrame(base.agent, 1f, base.agent.faction);
				}
				return this._ae;
			}
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0009F004 File Offset: 0x0009D404
		public IEnumerable<Swordsman> Network(HashSet<Swordsman> hashSet = null)
		{
			if (hashSet == null)
			{
				hashSet = new HashSet<Swordsman>();
			}
			hashSet.Add(this);
			yield return this;
			for (int i = 0; i < this.adjecent.Length; i++)
			{
				Swordsman f = this.adjecent[i];
				if (f && !hashSet.Contains(f))
				{
					foreach (Swordsman j in f.Network(null))
					{
						yield return j;
					}
				}
			}
			yield break;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x0009F035 File Offset: 0x0009D435
		public IEnumerable<Swordsman> friends
		{
			get
			{
				return this.adjecent;
			}
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0009F040 File Offset: 0x0009D440
		private void UpdateAdjecent()
		{
			if (base.agent.beats.hz8)
			{
				this.adjecentEnumerator.MoveNext();
				Swordsman swordsman = (!this.adjecentEnumerator.Current) ? null : (this.adjecentEnumerator.Current.brain as Swordsman);
				if (swordsman != this)
				{
					bool flag = true;
					for (int i = 0; i < this.adjecent.Length; i++)
					{
						if (this.adjecent[i] == swordsman)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.adjecentSwords[this.adjecentSwords.Length - 1].swordsman = swordsman;
					}
				}
				for (int j = 0; j < this.adjecentSwords.Length; j++)
				{
					if (this.adjecentSwords[j].swordsman)
					{
						this.adjecentSwords[j].sqDist = (base.agent.wPos - this.adjecentSwords[j].swordsman.agent.wPos).sqrMagnitude;
						if (this.adjecentSwords[j].sqDist > 1f)
						{
							this.adjecentSwords[j].swordsman = null;
						}
					}
				}
				for (int k = 0; k < this.adjecentSwords.Length - 1; k++)
				{
					Swordsman.AdjecentSword adjecentSword = this.adjecentSwords[k];
					Swordsman.AdjecentSword adjecentSword2 = this.adjecentSwords[k + 1];
					if (adjecentSword2.swordsman)
					{
						if (!adjecentSword.swordsman || adjecentSword2.sqDist < adjecentSword.sqDist)
						{
							this.adjecentSwords[k] = adjecentSword2;
							this.adjecentSwords[k + 1] = adjecentSword;
						}
					}
				}
				for (int l = 0; l < this.adjecent.Length; l++)
				{
					this.adjecent[l] = this.adjecentSwords[l].swordsman;
				}
			}
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0009F294 File Offset: 0x0009D694
		protected override void OnDestroy()
		{
			this.target = null;
			this.spearSquad = null;
			this.adjecent = null;
			this.adjecentSwords = null;
			this._ae = null;
			this.shield = null;
			this.idle = null;
			this.recover = null;
			this.ready = null;
			this.pursuing = null;
			this.hunting = null;
			this.attack = null;
			this.clash = null;
			base.OnDestroy();
		}

		// Token: 0x04001C4B RID: 7243
		[Header("Editor")]
		public float[] damageLevels = new float[]
		{
			1f
		};

		// Token: 0x04001C4C RID: 7244
		public float[] knockbackLevels = new float[]
		{
			1f
		};

		// Token: 0x04001C4D RID: 7245
		public float[] stunLevels = new float[]
		{
			1f
		};

		// Token: 0x04001C4E RID: 7246
		private static AnimId attackId = "Attack";

		// Token: 0x04001C4F RID: 7247
		private static AnimId clashId = "Clash";

		// Token: 0x04001C50 RID: 7248
		[SerializeField]
		private float attackStaminaCost = 0.5f;

		// Token: 0x04001C51 RID: 7249
		private float stamina = 1f;

		// Token: 0x04001C52 RID: 7250
		public Agent target;

		// Token: 0x04001C53 RID: 7251
		private SpearSquadCoordinator spearSquad;

		// Token: 0x04001C54 RID: 7252
		public AgentState idle;

		// Token: 0x04001C55 RID: 7253
		public AgentState recover;

		// Token: 0x04001C56 RID: 7254
		public AgentState ready;

		// Token: 0x04001C57 RID: 7255
		public AgentState pursuing;

		// Token: 0x04001C58 RID: 7256
		public AgentState hunting;

		// Token: 0x04001C59 RID: 7257
		public AgentState attack;

		// Token: 0x04001C5A RID: 7258
		public AgentState clash;

		// Token: 0x04001C5B RID: 7259
		[Header("Sound")]
		public string swingSound = "Sfx/English/Sword/Swing";

		// Token: 0x04001C5C RID: 7260
		public string swordSound = "Sfx/English/Sword";

		// Token: 0x04001C5D RID: 7261
		public string chargeSound = "Sfx/English/SwordShield/Charge";

		// Token: 0x04001C5E RID: 7262
		public Shield shield;

		// Token: 0x04001C5F RID: 7263
		private float startDistance;

		// Token: 0x04001C60 RID: 7264
		private bool isStandard;

		// Token: 0x04001C61 RID: 7265
		private float adjecentHightlight;

		// Token: 0x04001C62 RID: 7266
		private Swordsman[] adjecent = new Swordsman[3];

		// Token: 0x04001C63 RID: 7267
		private Swordsman.AdjecentSword[] adjecentSwords = new Swordsman.AdjecentSword[4];

		// Token: 0x04001C64 RID: 7268
		private IEnumerator<Agent> _ae;

		// Token: 0x02000698 RID: 1688
		[Serializable]
		private struct AdjecentSword
		{
			// Token: 0x04001C65 RID: 7269
			public Swordsman swordsman;

			// Token: 0x04001C66 RID: 7270
			public float sqDist;
		}
	}
}
