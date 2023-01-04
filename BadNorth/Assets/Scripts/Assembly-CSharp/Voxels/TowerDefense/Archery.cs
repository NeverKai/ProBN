using System;
using UnityEngine;
using Voxels.TowerDefense.Ballistics;

namespace Voxels.TowerDefense
{
	// Token: 0x02000691 RID: 1681
	public class Archery : Brain, IThreat
	{
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x0009B184 File Offset: 0x00099584
		public ArcherSquadCoordinator archerySquad
		{
			get
			{
				return this._archerySquad;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0009B18C File Offset: 0x0009958C
		public Vector3 shootPos
		{
			get
			{
				return this.bowAimer.position;
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x0009B199 File Offset: 0x00099599
		public Vector3 GetAgentTargetPos(Agent target)
		{
			return target.wChestPos;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0009B1A1 File Offset: 0x000995A1
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0009B1A9 File Offset: 0x000995A9
		public Vector3 aimDir { get; private set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0009B1B2 File Offset: 0x000995B2
		public int squadLevel
		{
			get
			{
				return base.agent.squad.level;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x0009B1C4 File Offset: 0x000995C4
		public Archery.ArcherySettings archerySettings
		{
			get
			{
				return this._archerySettings[this.squadLevel];
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x0009B1DC File Offset: 0x000995DC
		public Vector3 ShootPos
		{
			get
			{
				return this.bowAimer.transform.position;
			}
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0009B1F0 File Offset: 0x000995F0
		private void UpdateBowAim()
		{
			if (this.aimInterpolator == 0f)
			{
				this.bowAimer.localRotation = Quaternion.identity;
			}
			else if (this.aimDir != Vector3.zero)
			{
				this.bowAimer.rotation = Quaternion.Lerp(this.bowAimer.parent.rotation, Quaternion.LookRotation(this.aimDir), this.aimInterpolator);
			}
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x0009B268 File Offset: 0x00099668
		public bool AimAt(TrajectoryUtility trajectory, Vector3 targetPos, Vector3 targetVelocity)
		{
			Vector3 vector = targetPos - this.ShootPos;
			TrajectorySample trajectorySample = trajectory.Sample(vector);
			if (targetVelocity != Vector3.zero)
			{
				vector += targetVelocity * trajectorySample.impactTime;
				trajectorySample = trajectory.Sample(vector);
			}
			if (trajectorySample.valid)
			{
				this.aimDirTarget = trajectorySample.startVelocity;
			}
			return trajectorySample.valid;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x0009B2D8 File Offset: 0x000996D8
		public Shootable Shoot(Vector3 shootDir, ProjectileSettings projectileSettings)
		{
			Shootable result;
			using ("Shoot")
			{
				base.agent.animator.Play(Archery.ShootID, 1, 0f);
				IslandGameplayManager.RequestCombatAudio(this.shootSound, base.agent.gameObject);
				if (!this.shootable)
				{
					this.shootable = this.arrowPrefab.GetInstance<Shootable>();
					this.shootable.transform.position = this.ShootPos;
					this.shootable.transform.localScale = Vector3.one;
				}
				this.coolDownTime = Time.time + this.archerySettings.cooldown * UnityEngine.Random.Range(0.8f, 1.2f) - this.archerySettings.holdTime;
				this.shootable.Shoot(base.agent, shootDir, projectileSettings, this.archerySettings.attackSettings, this.target.mask0, this.target.mask1);
				Shootable shootable = this.shootable;
				this.shootable = null;
				result = shootable;
			}
			return result;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0009B424 File Offset: 0x00099824
		public override void Setup()
		{
			base.Setup();
			this.lineOfSight = base.GetComponent<LineOfSight>();
			this.lineOfSight.SetupLineOfSight(this.trajectoryCalculator, base.agent.aliveAndGrounded, this);
			this._archerySquad = base.squad.GetSquadCoordinator<ArcherSquadCoordinator>();
			this.aiming = new AgentState("Aiming", this.brainState, false, true);
			this.shooting = new AgentState("Shooting", this.aiming, false, true);
			this.ready = new AgentState("Ready", this.brainState, true, true);
			AgentState agentState = this.shooting;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this.holdTimer = Time.time + this.archerySettings.holdTime;
			}));
			this.shooting.OnUpdate += delegate()
			{
				if (!this.lineOfSight.InSight(this.target.agent).agent)
				{
					this.target = this.lineOfSight.target;
				}
				if (this.target.agent)
				{
					using ("aiming")
					{
						this.AimAt(this.trajectoryCalculator, this.target.agent.chestPos, this.target.agent.navPos.velocity);
					}
				}
				else
				{
					this.shooting.SetActive(false);
				}
				using ("syncHoldTimes")
				{
					foreach (Archery archery in this.archerySquad.archers)
					{
						if (archery.aiming.active && Vector3.Dot(archery.aimDir, this.aimDir) > 0f && (archery.agent.wPos - base.agent.wPos).sqrMagnitude < 1f)
						{
							this.holdTimer = Mathf.MoveTowards(this.holdTimer, archery.holdTimer, Time.deltaTime);
						}
					}
				}
				this.holdTimer = Mathf.Max(this.holdTimer, this.shooting.timeSinceActivation + 0.5f);
				if (Time.time > this.holdTimer)
				{
					using ("holdTimerExpired")
					{
						Vector3 vector = this.aimDir;
						vector += UnityEngine.Random.insideUnitSphere * this.aimDir.magnitude * this.archerySquad.GetSpread(this.archerySettings.spread);
						this.Shoot(vector, this.trajectoryCalculator.settings);
						this.shooting.SetActive(false);
					}
				}
			};
			AgentState agentState2 = this.aiming;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				this.aimDir = this.bowAimer.forward;
				base.agent.animator.SetBool(Archery.AimID, true);
				if (this.spawnArrowOnAim)
				{
					this.shootable = this.arrowPrefab.GetInstance<Shootable>();
					this.shootable.transform.SetParent(this.arrowAnim);
					this.shootable.transform.localPosition = Vector3.zero;
					this.shootable.transform.localScale = Vector3.one;
					this.shootable.transform.localRotation = Quaternion.identity;
				}
				IslandGameplayManager.RequestCombatAudio(this.drawSound, base.agent.gameObject);
			}));
			this.aiming.OnUpdate += delegate()
			{
				base.agent.movability = 0.3f;
				this.aimDir = Vector3.Lerp(this.aimDir, this.aimDirTarget, Time.deltaTime * 8f);
				base.agent.LookInDirection(this.aimDirTarget, 720f, 0f);
			};
			AgentState agentState3 = this.aiming;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				base.agent.animator.SetBool(Archery.AimID, false);
				this.DumpShootable();
			}));
			AgentState brainState = this.brainState;
			brainState.OnEmpty = (Action)Delegate.Combine(brainState.OnEmpty, new Action(this.ready.SetActiveTrue));
			AgentState agentState4 = this.aiming;
			agentState4.OnEmpty = (Action)Delegate.Combine(agentState4.OnEmpty, new Action(this.aiming.SetActiveFalse));
			base.agent.aliveAndGrounded.OnUpdate += this.UpdateBowAim;
			AgentState actingState = this.actingState;
			actingState.OnDeactivate = (Action)Delegate.Combine(actingState.OnDeactivate, new Action(this.ready.SetActiveTrue));
			this.ready.OnUpdate += this.ReadyUpdate;
			this.pirate = base.GetComponent<Pirate>();
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x0009B620 File Offset: 0x00099A20
		public bool MaybeStartShooting()
		{
			if (this.coolDownTime > Time.time)
			{
				return false;
			}
			if (base.agent.enemyDist < 0.3f)
			{
				return false;
			}
			if (base.agent.isEnglish && base.agent.enemyDist > 1f && base.agent.orderDist > 0.5f && base.enSquad.standard && base.agent.orderDist > base.enSquad.standard.agent.orderDist)
			{
				return false;
			}
			this.target = this.lineOfSight.target;
			if (this.target.agent)
			{
				this.shooting.SetActive(true);
				return true;
			}
			return false;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x0009B704 File Offset: 0x00099B04
		public void ReadyUpdate()
		{
			using ("ArcheryReadyUpdate")
			{
				if (!base.agent.beats.hz16 || !this.MaybeStartShooting())
				{
					if (!base.agent.beats.hz8 || !base.MaybeAct())
					{
						if (base.agent.isEnglish)
						{
							Vector3 dir = Vector3.zero;
							if (this.lineOfSight.target.agent)
							{
								this.order.ApplyWalk();
								dir = this.lineOfSight.target.agent.wPos - base.agent.wPos;
								base.agent.LookInDirection(dir, 720f, 20f);
							}
							else
							{
								this.order.ApplyOrder();
							}
						}
						else if ((double)base.agent.enemyDist < 1.2)
						{
							base.agent.walkDir = -base.agent.enemyDir;
							base.agent.walkDir += base.agent.faction.presence.SampleDirection(base.agent.navPos) * 0.5f;
							base.agent.movability = 0.2f;
							base.agent.LookInDirection(base.agent.enemyDir, 720f, 20f);
						}
						else if (this.lineOfSight.target.agent && (!this.pirate || !this.pirate.longship || !this.pirate.longship.landed))
						{
							LineOfSight.Sight sight = this.lineOfSight.target;
							base.agent.walkDir = Vector3.zero;
							base.agent.movability = 0.6f;
						}
						else
						{
							Vector3 zero = Vector3.zero;
							this.order.ApplyOrder();
						}
					}
				}
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0009B970 File Offset: 0x00099D70
		private void OnDrawGizmos()
		{
			if (this.arrowAnim)
			{
				Gizmos.color *= 2f;
				Gizmos.matrix = this.arrowAnim.transform.localToWorldMatrix;
				Gizmos.DrawWireSphere(Vector3.zero, 0.01f);
				Gizmos.DrawLine(Vector3.back * 0.2f, Vector3.forward * 0.2f);
			}
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0009B9E8 File Offset: 0x00099DE8
		Vector3 IThreat.GetPos(Agent victim)
		{
			return base.agent.wPos;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0009B9F8 File Offset: 0x00099DF8
		float IThreat.GetThreatDistance(Agent victim)
		{
			return (base.agent.wPos - victim.wPos).sqrMagnitude;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0009BA24 File Offset: 0x00099E24
		Vector3 IThreat.GetThreatDir(Agent victim)
		{
			Vector3 a = (base.agent.wPos - victim.wPos).normalized;
			a -= this.trajectoryCalculator.GetShootDirection(victim.wPos - base.agent.wPos, TrajectoryUtility.Attribute.ImpactVelocity).normalized;
			return a / 2f;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0009BA93 File Offset: 0x00099E93
		bool IThreat.GetTreatValid(Agent victim)
		{
			return this.lineOfSight.GetTreatValid(victim);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0009BAA4 File Offset: 0x00099EA4
		protected override void OnDestroy()
		{
			this.DumpShootable();
			this.lineOfSight = null;
			this._archerySquad = null;
			this.target = default(LineOfSight.Sight);
			this.pirate = null;
			base.OnDestroy();
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x0009BAE1 File Offset: 0x00099EE1
		private void DumpShootable()
		{
			if (this.shootable)
			{
				this.shootable.ReturnToPool();
				this.shootable = null;
			}
		}

		// Token: 0x04001C09 RID: 7177
		private Shootable shootable;

		// Token: 0x04001C0A RID: 7178
		[SerializeField]
		private Shootable arrowPrefab;

		// Token: 0x04001C0B RID: 7179
		[SerializeField]
		private Transform bowAimer;

		// Token: 0x04001C0C RID: 7180
		[SerializeField]
		private Transform arrowAnim;

		// Token: 0x04001C0D RID: 7181
		public float aimInterpolator;

		// Token: 0x04001C0E RID: 7182
		private float coolDownTime;

		// Token: 0x04001C0F RID: 7183
		private LineOfSight lineOfSight;

		// Token: 0x04001C10 RID: 7184
		[SerializeField]
		private bool spawnArrowOnAim;

		// Token: 0x04001C11 RID: 7185
		[SerializeField]
		private TrajectoryUtility trajectoryCalculator;

		// Token: 0x04001C12 RID: 7186
		[Header("Sound")]
		public string drawSound = "Sfx/English/Archer/Draw";

		// Token: 0x04001C13 RID: 7187
		public string shootSound = "Sfx/English/Archer/LetLoose";

		// Token: 0x04001C14 RID: 7188
		private ArcherSquadCoordinator _archerySquad;

		// Token: 0x04001C15 RID: 7189
		private LineOfSight.Sight target;

		// Token: 0x04001C17 RID: 7191
		public Vector3 aimDirTarget;

		// Token: 0x04001C18 RID: 7192
		private static int AimID = Animator.StringToHash("Aim");

		// Token: 0x04001C19 RID: 7193
		private static int ShootID = Animator.StringToHash("Shoot_Shoot");

		// Token: 0x04001C1A RID: 7194
		private Pirate pirate;

		// Token: 0x04001C1B RID: 7195
		[Header("Attack")]
		public Archery.ArcherySettings[] _archerySettings;

		// Token: 0x04001C1C RID: 7196
		private float holdTimer = 1f;

		// Token: 0x04001C1D RID: 7197
		public const float minHoldTime = 0.5f;

		// Token: 0x04001C1E RID: 7198
		public AgentState shooting;

		// Token: 0x04001C1F RID: 7199
		public AgentState aiming;

		// Token: 0x04001C20 RID: 7200
		public AgentState ready;

		// Token: 0x02000692 RID: 1682
		[Serializable]
		public struct ArcherySettings
		{
			// Token: 0x04001C21 RID: 7201
			public AttackSettings attackSettings;

			// Token: 0x04001C22 RID: 7202
			public float cooldown;

			// Token: 0x04001C23 RID: 7203
			public float spread;

			// Token: 0x04001C24 RID: 7204
			public float holdTime;
		}
	}
}
