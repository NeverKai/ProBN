using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReflexCLI.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using Voxels.TowerDefense.SpriteMagic;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x0200067A RID: 1658
	[SelectionBase]
	public class Agent : MonoBehaviour, IPassedClick
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x00096AD9 File Offset: 0x00094ED9
		public AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00096AE6 File Offset: 0x00094EE6
		public bool isEnglish
		{
			get
			{
				return this.faction.side == Faction.Side.English;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x00096AF6 File Offset: 0x00094EF6
		public bool isViking
		{
			get
			{
				return this.faction.side == Faction.Side.Viking;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00096B06 File Offset: 0x00094F06
		public SphereCollider col
		{
			get
			{
				if (this._col == null)
				{
					this._col = base.GetComponent<SphereCollider>();
				}
				return this._col;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x00096B2B File Offset: 0x00094F2B
		// (set) Token: 0x06002A31 RID: 10801 RVA: 0x00096B33 File Offset: 0x00094F33
		public Body body { get; private set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x00096B3C File Offset: 0x00094F3C
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x00096B44 File Offset: 0x00094F44
		public Animator animator { get; private set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x00096B4D File Offset: 0x00094F4D
		// (set) Token: 0x06002A35 RID: 10805 RVA: 0x00096B55 File Offset: 0x00094F55
		public SpriteAnimator spriteAnimator { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x00096B5E File Offset: 0x00094F5E
		// (set) Token: 0x06002A37 RID: 10807 RVA: 0x00096B66 File Offset: 0x00094F66
		public Ragdoller ragdoller { get; private set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002A38 RID: 10808 RVA: 0x00096B6F File Offset: 0x00094F6F
		// (set) Token: 0x06002A39 RID: 10809 RVA: 0x00096B77 File Offset: 0x00094F77
		public Stun stun { get; private set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x00096B80 File Offset: 0x00094F80
		public Vector3 lPos
		{
			get
			{
				return this.navPos.pos;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002A3B RID: 10811 RVA: 0x00096B8D File Offset: 0x00094F8D
		public Vector3 wChestPos
		{
			get
			{
				return this.wPos + this.chestOffset;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x00096BA0 File Offset: 0x00094FA0
		public Vector3 chestPos
		{
			get
			{
				return base.transform.position + this.chestOffset;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x00096BB8 File Offset: 0x00094FB8
		public Vector3 chestOffset
		{
			get
			{
				return Vector3.up * this.chestHeight;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002A3E RID: 10814 RVA: 0x00096BCC File Offset: 0x00094FCC
		public float chestHeight
		{
			get
			{
				return 0.23f * base.transform.localScale.y;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002A3F RID: 10815 RVA: 0x00096BF2 File Offset: 0x00094FF2
		public float totalIntimidation
		{
			get
			{
				return this.hardIntimidation + this.softIntimidation;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x00096C01 File Offset: 0x00095001
		public float maxIntimidation
		{
			get
			{
				return Mathf.Max(this.hardIntimidation, this.softIntimidation);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x00096C14 File Offset: 0x00095014
		public bool intimidated
		{
			get
			{
				return this.totalIntimidation > 0f;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x00096C23 File Offset: 0x00095023
		public Vector3 lookDir
		{
			get
			{
				return (!this.navPos.valid) ? base.transform.forward : Vector3.Cross(base.transform.right, this.navPos.GetNormal());
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x00096C60 File Offset: 0x00095060
		public float animationTime
		{
			get
			{
				return this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x00096C81 File Offset: 0x00095081
		public bool animationDone
		{
			get
			{
				return !this.animationChangeFrame && this.animationTime >= 1f;
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00096CA1 File Offset: 0x000950A1
		public void Throw()
		{
			if (this.onThrow != null)
			{
				this.onThrow();
				this.onThrow = null;
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00096CC0 File Offset: 0x000950C0
		public void StartThrow()
		{
			if (this.onStartThrow != null)
			{
				this.onStartThrow();
				this.onStartThrow = null;
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x00096CDF File Offset: 0x000950DF
		public void PlayAnimation(string anim)
		{
			this.PlayAnimation(Animator.StringToHash(anim));
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00096CED File Offset: 0x000950ED
		public void PlayAnimation(int anim)
		{
			this.animator.Play(anim);
			this.animationChangeFrame = true;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00096D02 File Offset: 0x00095102
		public void PlayAnimation(int anim, float normalizedTime)
		{
			this.animator.Play(anim, -1, normalizedTime);
			this.animationChangeFrame = true;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x00096D19 File Offset: 0x00095119
		// (set) Token: 0x06002A4B RID: 10827 RVA: 0x00096D26 File Offset: 0x00095126
		public bool moveAnimate
		{
			get
			{
				return this.moveAnimateState.active;
			}
			set
			{
				this.moveAnimateState.SetActive(value);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x00096D35 File Offset: 0x00095135
		public float healthFraction
		{
			get
			{
				return this.health / this.maxHealth;
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x00096D44 File Offset: 0x00095144
		public void FillHealth()
		{
			this.health = this.maxHealth;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x00096D52 File Offset: 0x00095152
		public float area
		{
			get
			{
				return this.radius * this.radius * 3.2f;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002A4F RID: 10831 RVA: 0x00096D67 File Offset: 0x00095167
		public float radius
		{
			get
			{
				return this.scale * 0.12f;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x00096D75 File Offset: 0x00095175
		public float mass
		{
			get
			{
				return this.scale * this.scale * this.scale;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06002A51 RID: 10833 RVA: 0x00096D8C File Offset: 0x0009518C
		// (set) Token: 0x06002A52 RID: 10834 RVA: 0x00096DAC File Offset: 0x000951AC
		public float scale
		{
			get
			{
				return base.transform.localScale.x;
			}
			set
			{
				base.transform.localScale = value * Vector3.one;
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06002A53 RID: 10835 RVA: 0x00096DC4 File Offset: 0x000951C4
		// (remove) Token: 0x06002A54 RID: 10836 RVA: 0x00096DFC File Offset: 0x000951FC
		
		public event Agent.MeleeBrainDelegate OnAttackTelegraphed = delegate(Brain A_0)
		{
		};

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06002A55 RID: 10837 RVA: 0x00096E34 File Offset: 0x00095234
		// (remove) Token: 0x06002A56 RID: 10838 RVA: 0x00096E6C File Offset: 0x0009526C
		
		public event Agent.SelectedDelegate OnAgentSelected = delegate(Agent A_0, bool A_1, bool A_2)
		{
		};

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00096EA2 File Offset: 0x000952A2
		public float friendRatio
		{
			get
			{
				if (this._eRatio == null)
				{
					this._eRatio = new FrameCache<float>(() => this.GetFriendRatio());
				}
				return this._eRatio.value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x00096ED1 File Offset: 0x000952D1
		public float enemyRatio
		{
			get
			{
				return 1f - this.friendRatio;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x00096EDF File Offset: 0x000952DF
		public ITriFlowObject enemyEntity
		{
			get
			{
				return this.enemyData.entity;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x00096EEC File Offset: 0x000952EC
		public Agent enemyAgent
		{
			get
			{
				Agent agent = this.enemyData.agent;
				return (!agent || !agent.navPos.valid) ? null : agent;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x00096F28 File Offset: 0x00095328
		public Brain enemyBrain
		{
			get
			{
				Agent agent = this.enemyData.agent;
				return (!agent || !agent.navPos.valid) ? null : agent.brain;
			}
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00096F68 File Offset: 0x00095368
		public void Setup(NavPos navPos)
		{
			using ("SetupAgent")
			{
				base.transform.SetParent(navPos.transform);
				this.wPos = navPos.wPos;
				base.transform.localPosition = navPos.pos;
				this.beats = new LazyBeats(100f);
				this.health = this.maxHealth;
				this.body = base.GetComponent<Body>();
				this.ragdoller = base.GetComponent<Ragdoller>();
				this.stun = base.GetComponent<Stun>();
				this.animator = base.GetComponent<Animator>();
				this.spriteAnimator = base.GetComponentInChildren<SpriteAnimator>();
				this.batchedShadow = this.shadow.GetComponent<BatchedSprite>();
				this.agentComponents = base.GetComponentsInChildren<AgentComponent>(true);
				this.attackResponders = base.GetComponentsInChildren<IAttackResponder>(true).ToList<IAttackResponder>();
				this.postAttacks = base.GetComponentsInChildren<Agent.IPostAttack>(true);
				this.brain = base.GetComponentInChildren<Brain>(true);
				this.uniqueDebugColor = ((!this.isEnglish) ? this.faction.color : ((this.squad as EnglishSquad).hero.color * 1.5f));
				this.navPos = navPos;
				this.rangeWorry = new Worry(this);
				this.aliveAndGrounded = new AgentState("AliveAndGrounded", this.rootState, false, false);
				this.spawned = new AgentState("Spawned", this.rootState, false, false);
				this.exclusives = new AgentState("Exclusives", this.aliveAndGrounded, false, false);
				this.lifeState = new AgentState("Life", this.rootState, false, false);
				this.deadState = new AgentState("Dead", this.lifeState, false, true);
				this.aliveState = new AgentState("Alive", this.lifeState, true, true);
				this.navigationState = new AgentState("Navigation", this.rootState, true, false);
				this.groundedState = new AgentState("Grounded", this.navigationState, true, true);
				this.moveAnimateState = new AgentState("MoveAnimate", this.rootState, true, true);
				AgentState agentState = this.aliveAndGrounded;
				agentState.OnEmpty = (Action)Delegate.Combine(agentState.OnEmpty, new Action(this.exclusives.SetActiveTrue));
				this.aliveAndGrounded.OnUpdate += this.UpdateColor;
				AgentState agentState2 = this.groundedState;
				agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(this.UpdateColor));
				AgentState agentState3 = this.groundedState;
				agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
				{
					this.navPos.SetNull();
				}));
				AgentState agentState4 = this.aliveState;
				agentState4.OnChange = (Action<bool>)Delegate.Combine(agentState4.OnChange, new Action<bool>(this.AliveAndGrounded));
				AgentState agentState5 = this.groundedState;
				agentState5.OnChange = (Action<bool>)Delegate.Combine(agentState5.OnChange, new Action<bool>(this.AliveAndGrounded));
				AgentState agentState6 = this.spawned;
				agentState6.OnChange = (Action<bool>)Delegate.Combine(agentState6.OnChange, new Action<bool>(this.AliveAndGrounded));
				AgentState agentState7 = this.groundedState;
				agentState7.OnChange = (Action<bool>)Delegate.Combine(agentState7.OnChange, new Action<bool>(this.shadow.gameObject.SetActive));
				AgentState agentState8 = this.aliveAndGrounded;
				agentState8.OnDeactivate = (Action)Delegate.Combine(agentState8.OnDeactivate, new Action(delegate()
				{
					this.enemyDist = 0f;
					this.enemyDir = Vector3.zero;
					this.enemyData = Data.empty;
					this.orderDir = Vector3.zero;
					this.orderDist = 0f;
				}));
				IAgentSetup[] componentsInChildren = base.GetComponentsInChildren<IAgentSetup>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].Setup(this);
				}
				AgentState agentState9 = this.groundedState;
				agentState9.OnActivate = (Action)Delegate.Combine(agentState9.OnActivate, new Action(delegate()
				{
					if (!this.navPos.valid)
					{
						UnityEngine.Debug.LogError("Trying to set grounded state active without a valid nav pos!", this);
					}
				}));
				AgentState agentState10 = this.spawned;
				agentState10.OnChange = (Action<bool>)Delegate.Combine(agentState10.OnChange, new Action<bool>(delegate(bool x)
				{
					if (x)
					{
						this.rootState.UpdateEmpties();
						Agent.allActive.Add(this);
						this.faction.agents.Add(this);
					}
					else
					{
						Agent.allActive.Remove(this);
						this.faction.agents.Remove(this);
					}
				}));
				this.deadState.OnUpdate += delegate()
				{
					if (this.aliveAndGrounded.active)
					{
						this.FinalDeath();
						return;
					}
					if (this.deadState.timeSinceActivation > 8f)
					{
						this.FinalDeath();
						return;
					}
				};
				AgentState agentState11 = this.aliveAndGrounded;
				agentState11.OnActivate = (Action)Delegate.Combine(agentState11.OnActivate, new Action(delegate()
				{
					if (this.deadState.active)
					{
						this.FinalDeath();
						return;
					}
				}));
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000973BC File Offset: 0x000957BC
		public void Spawn()
		{
			using ("spawnAgent")
			{
				this.spawned.SetActive(true);
			}
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x00097404 File Offset: 0x00095804
		public float GetFriendRatio()
		{
			float num = this.faction.presence.SampleAmount(this.navPos);
			float num2 = this.faction.enemy.presence.SampleAmount(this.navPos);
			return num / (num + num2);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00097449 File Offset: 0x00095849
		[ContextMenu("IsDangerous?")]
		private void PrintDangerous()
		{
			UnityEngine.Debug.Log("Dangerous = " + this.dangerous);
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00097465 File Offset: 0x00095865
		public void SetDangerous(bool value)
		{
			this.dangerous = value;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0009746E File Offset: 0x0009586E
		public void SetDangerousInverse(bool value)
		{
			this.dangerous = !value;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0009747A File Offset: 0x0009587A
		public void SetMoveAnimate(bool value)
		{
			this.moveAnimate = value;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00097483 File Offset: 0x00095883
		public void SetMoveAnimateInverse(bool value)
		{
			this.moveAnimate = !value;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x0009748F File Offset: 0x0009588F
		public void SetCollider(bool value)
		{
			this.col.enabled = value;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x0009749D File Offset: 0x0009589D
		public void SetColliderInverse(bool value)
		{
			this.SetCollider(!value);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000974A9 File Offset: 0x000958A9
		public void TelegraphAttack(Brain attacker)
		{
			this.OnAttackTelegraphed(attacker);
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000974B8 File Offset: 0x000958B8
		public bool DealDamage(Attack attack)
		{
			bool result;
			using ("DealDamage")
			{
				for (int i = 0; i < this.attackResponders.Count; i++)
				{
					this.attackResponders[i].ModifyAttack(ref attack);
				}
				if (attack.ignore)
				{
					result = false;
				}
				else
				{
					float num = attack.knockback / this.mass;
					Vector3 normalized = attack.direction.normalized;
					Vector3 vector = normalized * num;
					float num2 = Vector3.Dot(this.velocity + vector, normalized);
					if (num2 > num)
					{
						vector -= normalized * (num2 - num);
					}
					if (attack.launchImpulse > 0f && this.ragdoller)
					{
						float num3 = attack.launchImpulse / (this.scale * this.scale);
						if (num3 >= 0.9f)
						{
							Vector3 vector2 = this.velocity + normalized * num;
							vector2.y = Mathf.Max(vector2.y, 0f) + num3;
							this.ragdoller.Launch(vector2);
						}
					}
					bool flag = false;
					if (this.aliveState.active)
					{
						if (attack.damage > 0f || attack.knockback > 0f)
						{
							this.health -= attack.damage;
						}
						if (this.health <= 0f)
						{
							flag = true;
							attack.soundSuffix = "Kill";
							attack.effect = ScriptableObjectSingleton<PrefabManager>.instance.bloodSlash;
							this.col.radius *= 0.5f;
							this.deadState.SetActive(true);
							if (this.squad)
							{
								this.squad.ReportDead(this);
							}
						}
						if (!flag)
						{
							bool flag2 = true;
							int num4 = 0;
							while (num4 < this.postAttacks.Length && flag2)
							{
								this.postAttacks[num4].PostAttack(attack, num, ref flag2);
								num4++;
							}
							IslandGameplayManager.RequestCombatAudio(this.hurtSound, base.gameObject);
						}
					}
					else
					{
						vector /= 2f;
					}
					this.velocity += vector;
					if (attack.effect != null)
					{
						attack.effect.PlayAt(attack.pos, -attack.direction.GetZeroY());
					}
					if (attack.hasSound)
					{
						IslandGameplayManager.RequestCombatAudio(attack.soundPrefix, attack.soundSuffix, base.gameObject);
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000977AC File Offset: 0x00095BAC
		public void DoAnimationAction()
		{
			if (this.animationAction != null)
			{
				this.animationAction();
			}
			this.animationAction = null;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000977CB File Offset: 0x00095BCB
		public void PlayAnimation(int anim, Action animationAction)
		{
			this.animationAction = animationAction;
			this.animator.Play(anim);
			this.animationChangeFrame = true;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000977E7 File Offset: 0x00095BE7
		public void KillImmediate()
		{
			this.squad.ReportDead(this);
			this.FinalDeath();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000977FB File Offset: 0x00095BFB
		public void FinalDeath()
		{
			this.onFinalDeath();
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x00097813 File Offset: 0x00095C13
		public void SetSelection(bool selected, bool showGodray)
		{
			this.OnAgentSelected(this, selected, showGodray);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x00097823 File Offset: 0x00095C23
		private void AliveAndGrounded(bool b)
		{
			this.aliveAndGrounded.SetActive(this.aliveState.active && this.groundedState.active && this.spawned.active);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0009785F File Offset: 0x00095C5F
		public bool IsPathing()
		{
			return this.navPos.valid && this.brain.brainState.active;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00097884 File Offset: 0x00095C84
		public void SetLevel(int level)
		{
			if (this.squad is EnglishSquad)
			{
				EnglishSquad englishSquad = this.squad as EnglishSquad;
				this.scale = 1f + ((float)(englishSquad.level + englishSquad.squadTemplate.level) - 2f) * 0.06f;
			}
			foreach (ILevelComponent levelComponent in base.GetComponentsInChildren<ILevelComponent>(true))
			{
				levelComponent.OnSetLevel(this, level);
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00097900 File Offset: 0x00095D00
		private void OnDestroy()
		{
			if (this.rootState != null)
			{
				this.rootState.SetActive(false);
				this.rootState.OnDestroy();
			}
			if (this.squad)
			{
				this.squad.ReportDestroyed(this);
			}
			if (this.faction)
			{
				this.faction.agents.Remove(this);
			}
			Agent.allActive.Remove(this);
			this.faction = null;
			this.squad = null;
			this.body = null;
			this.ragdoller = null;
			this.stun = null;
			this._col = null;
			this.animator = null;
			this.spriteAnimator = null;
			this.agentComponents = null;
			this.attackResponders = null;
			this.postAttacks = null;
			this.aliveAndGrounded = null;
			this.spawned = null;
			this.exclusives = null;
			this.navigationState = null;
			this.groundedState = null;
			this.moveAnimateState = null;
			this.lifeState = null;
			this.deadState = null;
			this.aliveState = null;
			this.onFinalDeath = null;
			this.OnAttackTelegraphed = null;
			this.OnAgentSelected = null;
			this.brain = null;
			this._eRatio = null;
			this.enemyData = default(Data);
			this.navPos = default(NavPos);
			this.onThrow = null;
			this.onStartThrow = null;
			this.look = default(Agent.Look);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x00097A64 File Offset: 0x00095E64
		public void SetDirection(Vector3 dir)
		{
			dir.y = 0f;
			if (float.IsNaN(dir.x) || (double)dir.sqrMagnitude < 1E-05)
			{
				return;
			}
			base.transform.rotation = Quaternion.LookRotation(dir);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x00097AB6 File Offset: 0x00095EB6
		public void SetNavPos(NavPos newNavPos)
		{
			this.navPos = newNavPos;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x00097AC0 File Offset: 0x00095EC0
		public void LookAt(Vector3 pos, float speed = 720f, float threshold = 20f)
		{
			Vector3 dir = pos - this.wPos;
			this.look.Set(dir, speed, threshold);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00097AE8 File Offset: 0x00095EE8
		public bool TriCast(Agent otherAgent)
		{
			return otherAgent && otherAgent.aliveAndGrounded.active && this.navPos.TriCast(otherAgent.navPos);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x00097B1A File Offset: 0x00095F1A
		public void LookInDirection(Vector3 dir, float speed = 720f, float threshold = 20f)
		{
			this.look.Set(dir, speed, threshold);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00097B2C File Offset: 0x00095F2C
		private void ApplyLook()
		{
			Vector3 zeroY = this.look.dir.GetZeroY();
			if (zeroY == Vector3.zero || float.IsNaN(zeroY.x))
			{
				return;
			}
			Quaternion quaternion = Quaternion.LookRotation(zeroY);
			if (Quaternion.Angle(quaternion, base.transform.rotation) < this.look.threshold)
			{
				return;
			}
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, quaternion, this.look.speed * Time.deltaTime);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x00097BC4 File Offset: 0x00095FC4
		private void MaybeFixNavPos()
		{
			if (!this.navPos.valid)
			{
				UnityEngine.Debug.LogError("Agent navPos invalid for unknown reason, finding new navPos");
				this.navPos = new NavPos(this.faction.island.navMesh, base.transform.position, true, 1f);
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x00097C17 File Offset: 0x00096017
		public void Intimidate(float soft, float hard)
		{
			this.softIntimidation = Mathf.Max(this.softIntimidation, soft);
			this.hardIntimidation = Mathf.Max(this.hardIntimidation, hard);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00097C40 File Offset: 0x00096040
		private void UpdateColor()
		{
			Color color = this.spriteAnimator.color;
			color.b = 1f - this.healthFraction;
			this.spriteAnimator.color = color;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00097C78 File Offset: 0x00096078
		private void FixedUpdateAgent(float dt, float forceDt, float velocityScale)
		{
			this.animationChangeFrame = false;
			this.walkDir = Vector3.zero;
			this.movability = 1f;
			this.enemyMovability = 1f;
			using ("Agent.UpdateFlowData")
			{
				this.faction.enemy.presence.SampleFullData(this.navPos, ref this.enemyDist, ref this.enemyDir, ref this.enemyData);
			}
			using ("Agent UpdateOrderData")
			{
				if (this.navPos.valid)
				{
					this.brain.order.SampleOrder(this.navPos, ref this.orderDir, ref this.orderDist);
				}
				else
				{
					this.orderDist = 100f;
					this.orderDir = Vector3.zero;
				}
			}
			using ("stateRoot.Update")
			{
				this.stateRoot.Update();
			}
			if (this.groundedState.active)
			{
				using ("AgentUpdateGroundState")
				{
					this.MaybeFixNavPos();
					Vector3 vector = this.navPos.pos;
					float sqrMagnitude = this.walkDir.sqrMagnitude;
					if (float.IsNaN(sqrMagnitude) || float.IsInfinity(sqrMagnitude))
					{
						this.walkDir = Vector3.zero;
					}
					else if (sqrMagnitude != 0f && this.body.hopping.active)
					{
						Vector3 normalized = this.walkDir.GetZeroY().normalized;
						Vector3 slope = this.navPos.GetSlope();
						float num = Vector3.Dot(normalized, slope);
						if (num > 0f)
						{
							this.speed *= ExtraMath.RemapValue(num, 0f, 0.2f, 1f, 1.4f);
						}
						else if (num < 0f)
						{
							this.speed *= ExtraMath.RemapValue(num, -0.2f, 0f, 0.6f, 1f);
						}
						float value = Vector3.Dot(normalized, base.transform.forward);
						this.speed *= ExtraMath.RemapValue(value, -1f, 1f, 0.7f, 1.1f);
						Vector3 clampedMagnitude = this.walkDir.GetClampedMagnitude(dt * this.speed);
						vector += clampedMagnitude;
					}
					if (!float.IsNaN(this.force.sqrMagnitude))
					{
						vector += this.force * forceDt;
					}
					this.navPos.pos = vector;
					if (this.velocity != Vector3.zero)
					{
						if (float.IsNaN(this.velocity.sqrMagnitude))
						{
							this.velocity = Vector3.zero;
						}
						Vector3 up = this.navPos.tri.up;
						if (!this.navPos.onMain)
						{
							this.navPos.transform.TransformVector(up);
						}
						float num2 = Vector3.Dot(up, this.velocity);
						if (num2 < 0f)
						{
							this.velocity -= up * num2;
						}
						Vector3 vector2 = (!this.navPos.onMain) ? this.navPos.transform.InverseTransformVector(this.velocity) : this.velocity;
						Vector3 target = this.navPos.pos + vector2 * dt;
						Edge edge;
						this.navPos.MoveTo(target, out edge);
						if (edge != null)
						{
							if (edge.cliff || !this.navPos.navigationMesh.island)
							{
								float num3 = Vector3.Dot(vector2, -edge.borderVector);
								if (!this.ragdoller || !this.ragdoller.MaybeRagdoll(num3))
								{
									vector2 -= edge.borderVector * Vector3.Dot(vector2, edge.borderVector);
									this.velocity = this.navPos.transform.TransformVector(vector2);
								}
							}
							else
							{
								vector2 = Vector3.Reflect(vector2, edge.borderVector) * 0.5f;
								this.velocity = this.navPos.transform.TransformVector(vector2);
							}
						}
						this.velocity *= velocityScale;
					}
					if (this.groundedState.active)
					{
						this.wPos = this.navPos.wPos;
					}
					this.force = Vector3.zero;
					this.speed = this.maxSpeed;
					this.ApplyLook();
				}
			}
			else
			{
				this.wPos = base.transform.position;
			}
			using ("Finishing")
			{
				this.look = default(Agent.Look);
				this.health = Mathf.MoveTowards(this.health, this.maxHealth, dt / 60f);
				this.rangeWorry.Update();
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00098260 File Offset: 0x00096660
		public static void UpdateAllAgents(float dt, float forceDt)
		{
			using (new ScopedProfiler("UpdateAllAgents", null))
			{
				for (int i = 0; i < Agent.allActive.Count; i++)
				{
					Agent.allActive[i].hardIntimidation = 0f;
					Agent.allActive[i].softIntimidation = 0f;
				}
				for (int j = 0; j < Agent.allActive.Count; j++)
				{
					Agent agent = Agent.allActive[j];
					if (agent.aliveAndGrounded.active && agent.enemyEntity != null && !agent.enemyEntity.Equals(null))
					{
						agent.enemyEntity.OnProximity(agent);
						Agent enemyAgent = agent.enemyAgent;
						if (enemyAgent && enemyAgent.aliveAndGrounded.active && enemyAgent.enemyAgent != agent)
						{
							agent.brain.OnProximity(enemyAgent);
						}
					}
				}
				float velocityScale = 0.9f;
				using (new ScopedProfiler("FixedUpdateAgent", null))
				{
					for (int k = 0; k < Agent.allActive.Count; k++)
					{
						try
						{
							Agent agent2 = Agent.allActive[k];
							if (agent2)
							{
								agent2.FixedUpdateAgent(dt, forceDt, velocityScale);
							}
						}
						catch (Exception exception)
						{
							Agent agent3 = Agent.allActive[k];
							UnityEngine.Debug.LogException(exception);
							EnglishSquad englishSquad = agent3.squad as EnglishSquad;
							if (!englishSquad || englishSquad.heroAgent != agent3)
							{
								agent3.FinalDeath();
							}
						}
					}
				}
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00098484 File Offset: 0x00096884
		public void OnPassedClick(ClickPasser clickPasser, PointerEventData eventData, RaycastHit raycastHit)
		{
			if (this.squad && this.squad.passedClicker != null)
			{
				this.squad.passedClicker.OnPassedClick(clickPasser, eventData, raycastHit);
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000984BC File Offset: 0x000968BC
		private void OnDrawGizmos()
		{
			if (!Application.isPlaying)
			{
				this.wPos = base.transform.position;
			}
			Gizmos.matrix = ExtraGizmos.CloserToCameraMatrix();
			Gizmos.color = this.uniqueDebugColor;
			ExtraGizmos.DrawCircle(base.transform.position, this.radius, 8);
			Gizmos.DrawRay(base.transform.position, this.lookDir * this.radius);
			Gizmos.DrawLine(base.transform.position, this.chestPos);
			Gizmos.color *= 2f;
			Gizmos.DrawSphere(base.transform.position, this.radius / 4f);
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00098578 File Offset: 0x00096978
		private void OnDrawGizmosSelected()
		{
			if (Application.isPlaying && this.navPos.valid)
			{
				Gizmos.color = this.faction.color;
				Gizmos.DrawLine(base.transform.position, this.wPos);
				Gizmos.color *= 2f;
				Gizmos.DrawSphere(this.wPos, this.radius / 8f);
				Gizmos.color = new Color(1f, 0.8f, 0.6f);
				if (this.enemyAgent)
				{
					Gizmos.DrawLine(base.transform.position, this.enemyAgent.transform.position);
				}
				Gizmos.color = Color.yellow;
				if (this.rangeWorry.valid)
				{
					Gizmos.DrawRay(base.transform.position, this.rangeWorry.dir.normalized);
				}
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00098674 File Offset: 0x00096A74
		public T GetOrAddComponent<T>() where T : AgentComponent
		{
			T t = base.GetComponent<T>();
			if (!t)
			{
				t = base.gameObject.AddComponent<T>();
				t.Setup(this);
				if (this.spawned != null)
				{
				}
			}
			return t;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000986C0 File Offset: 0x00096AC0
		public void UpdateVisuals()
		{
			foreach (Agent.IUpdateVisuals updateVisuals in base.GetComponentsInChildren<Agent.IUpdateVisuals>(true))
			{
				updateVisuals.UpdateVisuals();
			}
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000986F4 File Offset: 0x00096AF4
		[Conditional("UNITY_EDITOR")]
		[Conditional("DEVELOPMENT_BUILD")]
		private void LogStateError(int chn, string msg)
		{
			UnityEngine.Debug.LogErrorFormat("{0} - {1}", new object[]
			{
				msg,
				base.name
			});
			StackTrace stackTrace = new StackTrace();
		}

		// Token: 0x04001B7F RID: 7039
		private static List<Agent> allActive = new List<Agent>();

		// Token: 0x04001B80 RID: 7040
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(16);

		// Token: 0x04001B81 RID: 7041
		public Faction faction;

		// Token: 0x04001B82 RID: 7042
		public NavPos navPos;

		// Token: 0x04001B83 RID: 7043
		public float outwardness = 0.5f;

		// Token: 0x04001B84 RID: 7044
		public Squad squad;

		// Token: 0x04001B85 RID: 7045
		public float walkedDistance;

		// Token: 0x04001B86 RID: 7046
		private SphereCollider _col;

		// Token: 0x04001B87 RID: 7047
		public AgentComponent[] agentComponents;

		// Token: 0x04001B88 RID: 7048
		public List<IAttackResponder> attackResponders;

		// Token: 0x04001B89 RID: 7049
		public Agent.IPostAttack[] postAttacks;

		// Token: 0x04001B8F RID: 7055
		public Vector3 wPos;

		// Token: 0x04001B90 RID: 7056
		public float movability = 1f;

		// Token: 0x04001B91 RID: 7057
		public float enemyMovability = 1f;

		// Token: 0x04001B92 RID: 7058
		public Vector3 velocity;

		// Token: 0x04001B93 RID: 7059
		public Vector3 walkDir;

		// Token: 0x04001B94 RID: 7060
		public Vector3 force;

		// Token: 0x04001B95 RID: 7061
		public float hardIntimidation;

		// Token: 0x04001B96 RID: 7062
		public float softIntimidation;

		// Token: 0x04001B97 RID: 7063
		private bool animationChangeFrame;

		// Token: 0x04001B98 RID: 7064
		public FabricEventReference hurtSound = string.Empty;

		// Token: 0x04001B99 RID: 7065
		private Agent.Look look = default(Agent.Look);

		// Token: 0x04001B9A RID: 7066
		public SpriteRenderer shadow;

		// Token: 0x04001B9B RID: 7067
		[NonSerialized]
		public BatchedSprite batchedShadow;

		// Token: 0x04001B9C RID: 7068
		public Action onStartThrow;

		// Token: 0x04001B9D RID: 7069
		public Action onThrow;

		// Token: 0x04001B9E RID: 7070
		public AgentState aliveAndGrounded;

		// Token: 0x04001B9F RID: 7071
		public AgentState spawned;

		// Token: 0x04001BA0 RID: 7072
		public AgentState exclusives;

		// Token: 0x04001BA1 RID: 7073
		public AgentState navigationState;

		// Token: 0x04001BA2 RID: 7074
		public AgentState groundedState;

		// Token: 0x04001BA3 RID: 7075
		public AgentState moveAnimateState;

		// Token: 0x04001BA4 RID: 7076
		public AgentState lifeState;

		// Token: 0x04001BA5 RID: 7077
		public AgentState deadState;

		// Token: 0x04001BA6 RID: 7078
		public AgentState aliveState;

		// Token: 0x04001BA7 RID: 7079
		[Header("Worries")]
		public Worry rangeWorry;

		// Token: 0x04001BA8 RID: 7080
		[Header("Flags")]
		public bool shield;

		// Token: 0x04001BA9 RID: 7081
		public float maxHealth = 1f;

		// Token: 0x04001BAA RID: 7082
		public float health = 1f;

		// Token: 0x04001BAB RID: 7083
		[ConsoleCommand("")]
		public float maxSpeed = 2f;

		// Token: 0x04001BAC RID: 7084
		public float speed = 2f;

		// Token: 0x04001BAD RID: 7085
		public Color uniqueDebugColor;

		// Token: 0x04001BAE RID: 7086
		public Action onFinalDeath = delegate()
		{
		};

		// Token: 0x04001BB1 RID: 7089
		public Brain brain;

		// Token: 0x04001BB2 RID: 7090
		public bool dangerous;

		// Token: 0x04001BB3 RID: 7091
		private FrameCache<float> _eRatio;

		// Token: 0x04001BB4 RID: 7092
		public float enemyDist;

		// Token: 0x04001BB5 RID: 7093
		public Vector3 enemyDir;

		// Token: 0x04001BB6 RID: 7094
		public Data enemyData;

		// Token: 0x04001BB7 RID: 7095
		public float orderDist;

		// Token: 0x04001BB8 RID: 7096
		public Vector3 orderDir;

		// Token: 0x04001BB9 RID: 7097
		public LazyBeats beats;

		// Token: 0x04001BBA RID: 7098
		public Action animationAction;

		// Token: 0x0200067B RID: 1659
		[Serializable]
		private struct Look
		{
			// Token: 0x06002A8D RID: 10893 RVA: 0x0009884D File Offset: 0x00096C4D
			public Look(Vector3 dir, float speed, float threshold)
			{
				this.dir = dir;
				this.speed = speed;
				this.threshold = threshold;
			}

			// Token: 0x06002A8E RID: 10894 RVA: 0x00098864 File Offset: 0x00096C64
			public void Set(Vector3 dir, float speed, float threshold)
			{
				this.dir = dir;
				this.speed = speed;
				this.threshold = threshold;
			}

			// Token: 0x04001BBE RID: 7102
			public Vector3 dir;

			// Token: 0x04001BBF RID: 7103
			public float speed;

			// Token: 0x04001BC0 RID: 7104
			public float threshold;
		}

		// Token: 0x0200067C RID: 1660
		// (Invoke) Token: 0x06002A90 RID: 10896
		public delegate void MeleeBrainDelegate(Brain attacker);

		// Token: 0x0200067D RID: 1661
		// (Invoke) Token: 0x06002A94 RID: 10900
		public delegate void SelectedDelegate(Agent agent, bool selected, bool showGodray);

		// Token: 0x0200067E RID: 1662
		public interface IPostAttack
		{
			// Token: 0x06002A97 RID: 10903
			void PostAttack(Attack attack, float addedVelocity, ref bool keepGoing);
		}

		// Token: 0x0200067F RID: 1663
		public interface IUpdateVisuals
		{
			// Token: 0x06002A98 RID: 10904
			void UpdateVisuals();
		}
	}
}
