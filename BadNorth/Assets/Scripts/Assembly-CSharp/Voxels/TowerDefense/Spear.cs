using System;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x02000696 RID: 1686
	public class Spear : Brain, IAttackResponder
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x0009BF70 File Offset: 0x0009A370
		public AttackSettings attackSetting
		{
			get
			{
				return this.attackSettings[base.squad.level];
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x0009BF8D File Offset: 0x0009A38D
		private float actualRechargeTime
		{
			get
			{
				return this.rechargeTime[base.squad.level];
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x0009BFA1 File Offset: 0x0009A3A1
		public float sqSpearLength
		{
			get
			{
				return this.spearLength * this.spearLength;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x0009BFB0 File Offset: 0x0009A3B0
		public Vector3 spearDir
		{
			get
			{
				return this.spearAim.forward * this.spearLength;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0009BFC8 File Offset: 0x0009A3C8
		public Vector3 spearMidDir
		{
			get
			{
				return this.spearDir * 0.9f;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x0009BFDA File Offset: 0x0009A3DA
		public Vector3 spearTip
		{
			get
			{
				return this.spearAim.position + this.spearDir;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x0009BFF2 File Offset: 0x0009A3F2
		public Vector3 spearMidPos
		{
			get
			{
				return this.spearAim.position + this.spearMidDir;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x0009C00A File Offset: 0x0009A40A
		public Vector3 spearAnimTip
		{
			get
			{
				return this.spearAnim.TransformPoint(Vector3.forward * this.spearLength);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x0009C027 File Offset: 0x0009A427
		// (set) Token: 0x06002B43 RID: 11075 RVA: 0x0009C02F File Offset: 0x0009A42F
		public Vector3 idealSpearTipDir
		{
			get
			{
				return this._idealSpearTipDir;
			}
			set
			{
				this._idealSpearTipDir = value.normalized;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x0009C03E File Offset: 0x0009A43E
		public SpearSquadCoordinator spearSquad
		{
			get
			{
				return this._spearSquad;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x0009C046 File Offset: 0x0009A446
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x0009C04E File Offset: 0x0009A44E
		private float spearForward
		{
			get
			{
				return this._spearForward;
			}
			set
			{
				if (this._spearForward != value)
				{
					this._spearForwardDirty = true;
					this._spearForward = value;
				}
			}
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0009C06C File Offset: 0x0009A46C
		public override void Setup()
		{
			base.Setup();
			this._spearSquad = base.squad.GetSquadCoordinator<SpearSquadCoordinator>();
			this.englishFormationAgent = base.GetComponent<EnglishFormationAgent>();
			this.spearUp = new AgentState("SpearsUp", this.brainState, false, true);
			this.spearDown = new AgentState("SpearsDown", this.brainState, false, true);
			this.stabbing = new AgentState("Stabbing", this.spearDown, false, true);
			this.charging = new AgentState("Charging", this.spearDown, false, true);
			this.ready = new AgentState("Ready", this.spearDown, false, true);
			this.spearSprite = this.spearAnim.GetComponentInChildren<BatchedSprite>(true);
			this.stabbing.OnUpdate += delegate()
			{
				base.agent.speed = 0.3f;
				base.agent.movability = 0.5f;
				base.agent.LookInDirection(this.spearDir, 720f, 20f);
				this.stabAnim = ExtraMath.RemapValue(this.stabbing.timeSinceActivation, 0.1f, 0f);
				this.spearForward = Mathf.Lerp(1f, -1f, this.stabAnim * this.stabAnim * this.stabAnim * this.stabAnim * this.stabAnim);
				if ((double)this.stabAnim < 0.2 && this.Hit())
				{
					this.target = null;
					this.charging.SetActive(true);
					this.spearForward -= 0.2f;
					return;
				}
				if (this.stabAnim == 0f)
				{
					this.charging.SetActive(true);
					return;
				}
			};
			this.charging.OnUpdate += delegate()
			{
				this.spearForward = Mathf.Lerp(this.spearForward, -1f, Time.deltaTime * 10f);
				this.UpdateSpearPos();
				base.agent.speed = 0.5f;
				if (base.agent.beats.hz2 && this.spearSquad.enemyDist > 3f)
				{
					this.ready.SetActive(true);
					return;
				}
				if ((double)this.spearForward <= -0.9 && this.stabbing.timeSinceDeactivation > this.actualRechargeTime && this.target && this.TestHit(this.target.chestPos + (this.target.body.walkDelta + this.target.velocity) * 0.1f, this.spearAim) && !this.EnemyClaimed(this.target))
				{
					this.stabbing.SetActive(true);
					return;
				}
			};
			this.ready.OnUpdate += delegate()
			{
				using ("SpearReadyUpdate")
				{
					this.spearForward = Mathf.MoveTowards(this.spearForward, 0f, Time.deltaTime * 4f);
					if (base.agent.beats.hz1)
					{
						this.UpdateSpearPos();
					}
					if (base.agent.beats.hz2)
					{
						if (this.spearSquad.enemyDist < 2.5f && Time.time > this.waitTil)
						{
							this.charging.SetActive(true);
						}
						else if (this.spearSquad.spearsUp.active)
						{
							this.spearUp.SetActive(true);
						}
					}
				}
			};
			AgentState agentState = this.spearDown;
			agentState.OnEmpty = (Action)Delegate.Combine(agentState.OnEmpty, new Action(this.ready.SetActiveTrue));
			AgentState agentState2 = this.spearDown;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(delegate(bool x)
			{
				base.agent.SetDangerous(x);
				base.agent.animator.SetBool(Spear.spearID, x);
			}));
			AgentState agentState3 = this.spearDown;
			agentState3.OnActivate = (Action)Delegate.Combine(agentState3.OnActivate, new Action(delegate()
			{
				this.lastDir = this.spearSquad.enemyDir;
				this.waitTil = 0f;
				this.UpdateSpearPos();
				this.spearSquad.OnSpearDown();
			}));
			AgentState agentState4 = this.spearDown;
			agentState4.OnDeactivate = (Action)Delegate.Combine(agentState4.OnDeactivate, new Action(delegate()
			{
				this.idealSpearTipDir = Vector3.up;
			}));
			this.spearDown.OnUpdate += delegate()
			{
				this.order.ApplyWalk();
				base.agent.LookInDirection(this.idealSpearTipDir, 720f, 20f);
				if (base.agent.enemyDist < this.spearLength)
				{
					base.agent.walkDir -= this.spearSquad.enemyDir * ExtraMath.RemapValue(base.agent.enemyDist, this.spearLength * 0.5f, this.spearLength, 0.5f, 0f);
				}
				if (base.agent.beats.hz2 && base.agent.orderDist > 1f)
				{
					this.spearUp.SetActive(true);
				}
			};
			this.spearUp.OnUpdate += delegate()
			{
				this.order.ApplyOrder();
				float @in = (!base.agent.enemyData.dangerous) ? 0.2f : (this.spearLength * 0.8f);
				float in2 = (!base.agent.enemyData.dangerous) ? 0.4f : (this.spearLength + 0.8f);
				base.agent.walkDir -= base.agent.enemyDir * ExtraMath.RemapValue(base.agent.enemyDist, @in, in2, 2f, 0f);
				this.spearForward = Mathf.MoveTowards(this.spearForward, 0f, Time.deltaTime * 2f);
				if (this.spearSquad.spearsDown.active && (!base.agent.enemyData.dangerous || base.agent.enemyDist > this.spearLength + 0.7f) && base.agent.orderDist < 0.1f)
				{
					this.spearDown.SetActive(true);
				}
			};
			AgentState agentState5 = this.spearUp;
			agentState5.OnActivate = (Action)Delegate.Combine(agentState5.OnActivate, new Action(delegate()
			{
				this.idealSpearTipDir = Vector3.up;
			}));
			this.spearUp.SetActive(true);
			AgentState brainState = this.brainState;
			brainState.OnEmpty = (Action)Delegate.Combine(brainState.OnEmpty, new Action(this.spearUp.SetActiveTrue));
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0009C29C File Offset: 0x0009A69C
		public override void OnProximity(Agent otherAgent)
		{
			if (!this.spearDown.active)
			{
				return;
			}
			Vector3 a = otherAgent.navPos.pos - base.agent.navPos.pos;
			float sqrMagnitude = a.sqrMagnitude;
			float num = this.spearLength + 0.7f;
			if (sqrMagnitude > num * num)
			{
				return;
			}
			float num2 = Mathf.Sqrt(sqrMagnitude);
			Vector3 vector = a / num2;
			num2 += ExtraMath.RemapValue(Vector3.Dot(vector, this.spearDir), 1f, -1f, -0.2f, 0.4f);
			if (num2 > num)
			{
				return;
			}
			num2 += ExtraMath.RemapValue(Vector3.Dot(-vector, otherAgent.enemyDir), 1f, -1f, -0.2f, 0.4f);
			if (num2 > num)
			{
				return;
			}
			num2 += ExtraMath.RemapValue(Vector3.Dot(this.spearDir, -base.agent.faction.presence.SampleDirection(this.spearNavPos)), 1f, -1f, -0.2f, 0.3f);
			if (num2 > num)
			{
				return;
			}
			float @in = this.spearLength + otherAgent.radius;
			float num3 = ExtraMath.RemapValue(num2, num, @in);
			float num4 = num3;
			otherAgent.Intimidate(num4 * 0.2f, num4);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0009C3F0 File Offset: 0x0009A7F0
		private bool EnemyClaimed(Agent enemy)
		{
			float num = enemy.health;
			foreach (Spear spear in this.spearSquad.spears)
			{
				if (spear.target == enemy && spear.stabbing.active && (num -= spear.attackSetting.damage) < 0f)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0009C49C File Offset: 0x0009A89C
		protected override void OnPathTargetChanged(IPathTarget target)
		{
			base.OnPathTargetChanged(target);
			this.spearDown.SetActive(false);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0009C4B4 File Offset: 0x0009A8B4
		public Attack GetAttack(Agent target)
		{
			Vector3 forward = this.spearAim.forward;
			Vector3 vector = this.spearAnim.position;
			vector += forward * (Vector3.Dot(forward, target.chestPos - vector) - target.radius);
			vector = Vector3.Lerp(vector, target.chestPos, 0.3f);
			return new Attack(this.attackSetting, forward, vector, this, base.agent.squad, this.spearSound, ScriptableObjectSingleton<PrefabManager>.instance.hitEffect);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0009C53C File Offset: 0x0009A93C
		public bool Hit()
		{
			bool flag = this.target != base.agent.enemyAgent;
			float num = (!flag) ? 0.6666667f : 1f;
			bool result = false;
			if (this.target && this.TestHit(this.target.chestPos, this.spearAnim))
			{
				Attack attack = this.GetAttack(this.target);
				attack.settings *= num;
				this.target.DealDamage(attack);
				result = true;
			}
			if (flag && base.agent.enemyAgent && this.TestHit(base.agent.enemyAgent.chestPos, this.spearAnim))
			{
				num /= 2f;
				Attack attack2 = this.GetAttack(base.agent.enemyAgent);
				attack2.settings *= num;
				base.agent.enemyAgent.DealDamage(attack2);
				result = true;
			}
			return result;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0009C653 File Offset: 0x0009AA53
		public void OnSpearsDownChange(bool active)
		{
			if (active && this.spearUp.active)
			{
				this.ready.SetActive(true);
			}
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0009C678 File Offset: 0x0009AA78
		public bool TestHit(Vector3 enemyPos, Transform spearTransform)
		{
			Vector3 vector = spearTransform.InverseTransformPoint(enemyPos);
			float d = this.spearLength;
			vector /= d;
			vector.z -= 0.5f;
			vector.x *= 2f;
			vector.y *= 2f;
			return Vector3.SqrMagnitude(vector) < 1f;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0009C6E4 File Offset: 0x0009AAE4
		private void UpdateSpearPos()
		{
			using (new ScopedProfiler("UpdateSpearPos", null))
			{
				if (base.agent.enemyEntity != null)
				{
					if (Time.time >= this.waitTil)
					{
						if (!base.agent.enemyAgent || base.agent.enemyAgent.aliveAndGrounded.active)
						{
							this.spearNavPos = base.agent.navPos;
							Vector3 b = (this.spearSquad.enemyDir * 0.7f + base.agent.enemyDir).normalized * this.spearLength;
							Edge edge;
							this.spearNavPos.MoveTo(this.spearNavPos.pos + b, out edge);
							Data data = base.agent.faction.enemy.presence.SampleData(this.spearNavPos);
							this.target = data.agent;
							Vector3 vector = this.spearNavPos.pos - base.agent.navPos.pos;
							if (this.target)
							{
								Vector3 vector2 = data.navPos.pos - base.agent.navPos.pos;
								if (vector2.sqrMagnitude < 5f && Vector3.Dot(vector2, this.spearSquad.enemyDir) > 0f && this.spearNavPos.TriCast(data.navPos))
								{
									vector = vector2;
									edge = null;
								}
							}
							else if (data.navPos.valid && base.agent.navPos.TriCast(data.navPos))
							{
								float d = ExtraMath.RemapValue((data.navPos.pos - base.agent.navPos.pos).sqrMagnitude, 0.5f, 3f, 0.5f, 0f);
								vector += data.dir * d;
							}
							vector.Normalize();
							if (Vector3.Dot(vector, this.lastDir) > 0.4f)
							{
								if (edge != null && edge.wall)
								{
									this.discrepancy = 1f - vector.magnitude / this.spearLength;
								}
								else
								{
									this.discrepancy = 0f;
								}
								vector.y *= 1.4f;
								this.idealSpearTipDir = vector;
							}
							else
							{
								this.waitTil = Time.time + 0.3f;
							}
							this.lastDir = vector;
						}
					}
				}
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0009C9E4 File Offset: 0x0009ADE4
		private void SpearRotationUpdate()
		{
			using (new ScopedProfiler("SpearRotationUpdate", null))
			{
				float num = Mathf.Min(Time.deltaTime, 0.02f);
				this.interpolatedTip = Vector3.MoveTowards(this.interpolatedTip, this.idealSpearTipDir, num * 4f);
				Vector3 a = this.interpolatedTip;
				a.y += Mathf.Max(this.discrepancy, 0f);
				Vector3 a2 = a - this.movingTip;
				this.movingTipVelocity += a2 * num * 80f;
				this.movingTipVelocity -= base.agent.body.walkDelta * num * 8f;
				this.movingTip += this.movingTipVelocity * num;
				this.movingTipVelocity -= this.movingTipVelocity * num * 8f;
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0009CB18 File Offset: 0x0009AF18
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.target = null;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0009CB28 File Offset: 0x0009AF28
		public void LateUpdate()
		{
			this.SpearRotationUpdate();
			this.spearAim.rotation = Quaternion.LookRotation(this.movingTip, base.transform.right) * Quaternion.Euler(0f, 0f, 90f);
			base.agent.animator.SetFloat(Spear.spearUpID, this.movingTip.y);
			if (this._spearForwardDirty)
			{
				base.agent.animator.SetFloat(Spear.spearForwardID, this.spearForward);
				this.spearSprite.UpdateScale();
				this._spearForwardDirty = false;
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x0009CBD8 File Offset: 0x0009AFD8
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Color black = Color.black;
				black.r = ((!this.target) ? 0f : 1f);
				Gizmos.color = black;
				Gizmos.DrawLine(this.spearAim.position, this.spearTip);
				if (this.target)
				{
					black.a = 0.5f;
					Gizmos.DrawLine(this.target.transform.position + this.target.chestOffset, this.spearTip);
				}
			}
			else if (this.spearAim)
			{
				Gizmos.DrawLine(this.spearAim.position, this.spearTip);
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0009CCA9 File Offset: 0x0009B0A9
		private bool MaybeBreak()
		{
			if (this.ready.active || this.charging.active)
			{
				this.spearUp.SetActive(true);
				return true;
			}
			return false;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0009CCDC File Offset: 0x0009B0DC
		public void ModifyAttack(ref Attack attack)
		{
			if (attack.damage <= 0f)
			{
				return;
			}
			int num = 0;
			if (this.MaybeBreak())
			{
				num++;
			}
			foreach (Spear spear in this.spearSquad.spears)
			{
				if (spear.MaybeBreak())
				{
					num++;
					if (num == 3)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04001C2D RID: 7213
		[Header("Editor")]
		[SerializeField]
		public AttackSettings[] attackSettings;

		// Token: 0x04001C2E RID: 7214
		[SerializeField]
		private float[] rechargeTime = new float[]
		{
			0f,
			0.35f,
			0.6f,
			1.1f
		};

		// Token: 0x04001C2F RID: 7215
		private static AnimId spearID = "Spear";

		// Token: 0x04001C30 RID: 7216
		private static AnimId spearUpID = "SpearUp";

		// Token: 0x04001C31 RID: 7217
		private static AnimId spearForwardID = "SpearForward";

		// Token: 0x04001C32 RID: 7218
		public float spearLength = 0.6f;

		// Token: 0x04001C33 RID: 7219
		public Transform spearAim;

		// Token: 0x04001C34 RID: 7220
		public BatchedSprite spearSprite;

		// Token: 0x04001C35 RID: 7221
		public Transform spearAnim;

		// Token: 0x04001C36 RID: 7222
		private EnglishFormationAgent englishFormationAgent;

		// Token: 0x04001C37 RID: 7223
		public AgentState charging;

		// Token: 0x04001C38 RID: 7224
		public AgentState stabbing;

		// Token: 0x04001C39 RID: 7225
		public AgentState ready;

		// Token: 0x04001C3A RID: 7226
		public AgentState spearDown;

		// Token: 0x04001C3B RID: 7227
		public AgentState spearUp;

		// Token: 0x04001C3C RID: 7228
		private float stabAnim;

		// Token: 0x04001C3D RID: 7229
		[Header("Sound")]
		public string swingSound = "Sfx/English/Spear/Swing";

		// Token: 0x04001C3E RID: 7230
		public string spearSound = "Sfx/English/Spear";

		// Token: 0x04001C3F RID: 7231
		private Agent target;

		// Token: 0x04001C40 RID: 7232
		private Vector3 _idealSpearTipDir = Vector3.up;

		// Token: 0x04001C41 RID: 7233
		private SpearSquadCoordinator _spearSquad;

		// Token: 0x04001C42 RID: 7234
		private bool _spearForwardDirty;

		// Token: 0x04001C43 RID: 7235
		private float _spearForward;

		// Token: 0x04001C44 RID: 7236
		private float discrepancy;

		// Token: 0x04001C45 RID: 7237
		private float waitTil;

		// Token: 0x04001C46 RID: 7238
		private Vector3 lastDir;

		// Token: 0x04001C47 RID: 7239
		private NavPos spearNavPos;

		// Token: 0x04001C48 RID: 7240
		private Vector3 interpolatedTip = Vector3.up;

		// Token: 0x04001C49 RID: 7241
		private Vector3 movingTip = Vector3.up;

		// Token: 0x04001C4A RID: 7242
		private Vector3 movingTipVelocity;
	}
}
