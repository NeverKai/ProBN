using System;
using UnityEngine;
using Voxels.TowerDefense.Ballistics;

namespace Voxels.TowerDefense
{
	// Token: 0x0200068E RID: 1678
	public class AxeThrowing : AgentComponent, IThreat, IBrainAction
	{
		// Token: 0x06002ADE RID: 10974 RVA: 0x0009945C File Offset: 0x0009785C
		public override void Setup()
		{
			this.swordsman = base.agent.GetComponent<Swordsman>();
			this.pirate = base.agent.GetComponent<Pirate>();
			this.searching = new AgentState("SearchingForAxeTarget", base.agent.aliveAndGrounded, true, false);
			this.lineOfSight = base.gameObject.GetComponent<LineOfSight>();
			this.lineOfSight.SetupLineOfSight(this.trajectoryUtility, this.searching, this);
			this.prepare = new AgentState("PreparingAxeThrow", base.agent.exclusives, false, true);
			this.prepare.OnUpdate += delegate()
			{
				if (!this.lineOfSight.InSight(this.target.agent).agent)
				{
					this.target = this.lineOfSight.target;
				}
				if (this.target.agent)
				{
					base.agent.walkDir += base.agent.enemyDir * (base.agent.enemyDist - this.preferredDistance);
				}
				else
				{
					base.agent.walkDir += base.agent.enemyDir;
				}
				base.agent.LookInDirection(base.agent.enemyDir, 720f, 20f);
				if (base.agent.enemyDist < this.preferredDistance * 0.5f)
				{
					this.prepare.SetActive(false);
					return;
				}
				if (this.prepare.timeSinceActivation > 2f)
				{
					this.prepare.SetActive(false);
				}
				else if (this.prepare.timeSinceActivation > 1f)
				{
					this.MaybeStartThrow();
				}
			};
			this.axeThrowing = new AgentState("AxeThrowing", base.agent.exclusives, false, true);
			AgentState agentState = this.axeThrowing;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				base.agent.PlayAnimation(AxeThrowing.throwId);
				base.agent.moveAnimate = false;
				base.agent.onThrow = new Action(this.ThrowAxe);
			}));
			this.axeThrowing.OnUpdate += delegate()
			{
				if (this.target.agent)
				{
					float shootTime = this.trajectoryUtility.GetShootTime(this.target.agent.chestPos - base.agent.chestPos);
					if (shootTime != 0f)
					{
						Vector3 a = this.target.agent.chestPos + this.target.agent.body.walkDelta * shootTime;
						this.aimDir = this.trajectoryUtility.GetShootDirection(a - base.agent.chestPos, TrajectoryUtility.Attribute.StartVelocity);
						this.aimTime = shootTime;
					}
				}
				if (this.axeThrowing.timeSinceActivation > 0.1f)
				{
					foreach (Swordsman swordsman in this.swordsman.friends)
					{
						if (swordsman)
						{
							AxeThrowing componentInChildren = swordsman.GetComponentInChildren<AxeThrowing>();
							if (componentInChildren)
							{
								componentInChildren.MaybeStartThrow();
							}
						}
					}
				}
				if (base.agent.enemyAgent && base.agent.navPos.TriCast(base.agent.enemyAgent.navPos))
				{
					base.agent.walkDir += base.agent.enemyDir * (base.agent.enemyDist - this.preferredDistance);
				}
				else
				{
					base.agent.walkDir += base.agent.enemyDir;
				}
				base.agent.LookInDirection(this.aimDir, 720f, 20f);
				if (base.agent.animationDone)
				{
					this.axeThrowing.SetActive(false);
				}
			};
			AgentState agentState2 = this.axeThrowing;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				base.agent.moveAnimate = true;
			}));
			AgentState aliveAndGrounded = base.agent.aliveAndGrounded;
			aliveAndGrounded.OnActivate = (Action)Delegate.Combine(aliveAndGrounded.OnActivate, new Action(this.searching.SetActiveTrue));
			AgentState pursuing = this.swordsman.pursuing;
			pursuing.OnActivate = (Action)Delegate.Combine(pursuing.OnActivate, new Action(this.MaybeStartThrow));
			AgentState hunting = this.swordsman.hunting;
			hunting.OnActivate = (Action)Delegate.Combine(hunting.OnActivate, new Action(this.MaybeStartThrow));
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x00099610 File Offset: 0x00097A10
		bool IBrainAction.MaybeAct(Brain swordsman)
		{
			if (this.ammo <= 0)
			{
				return false;
			}
			if (this.pirate && this.pirate.longship && this.pirate.longship.timeRemaining > 1.6f)
			{
				return false;
			}
			this.target = this.GetSight();
			if (!this.target.agent)
			{
				return false;
			}
			if ((this.target.agent.chestPos - base.agent.chestPos).sqrMagnitude > this.preferredDistance * this.preferredDistance)
			{
				return false;
			}
			if (this.prepare.timeSinceDeactivation < 2f)
			{
				return false;
			}
			this.prepare.SetActive(true);
			return true;
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000996F0 File Offset: 0x00097AF0
		private LineOfSight.Sight GetSight()
		{
			if (this.lineOfSight.enemies.Count > 0)
			{
				return this.lineOfSight.enemies[UnityEngine.Random.Range(0, this.lineOfSight.enemies.Count / 3)];
			}
			return default(LineOfSight.Sight);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x00099748 File Offset: 0x00097B48
		private void MaybeStartThrow()
		{
			if (!this.target.agent)
			{
				this.target = this.GetSight();
			}
			if (!this.target.agent)
			{
				return;
			}
			if (!base.agent.moveAnimate)
			{
				return;
			}
			if (this.axeThrowing.timeSinceDeactivation < 1f)
			{
				return;
			}
			Vector3 diff = this.target.agent.chestPos - base.agent.chestPos;
			float sqrMagnitude = diff.sqrMagnitude;
			if (sqrMagnitude < 0.16000001f)
			{
				return;
			}
			this.aimTime = this.trajectoryUtility.GetShootTime(diff);
			if (this.aimTime == 0f)
			{
				return;
			}
			this.aimDir = this.trajectoryUtility.GetShootDirection(diff, TrajectoryUtility.Attribute.StartVelocity);
			if (--this.ammo <= 0)
			{
				this.searching.SetActive(false);
				AgentState aliveAndGrounded = base.agent.aliveAndGrounded;
				aliveAndGrounded.OnActivate = (Action)Delegate.Remove(aliveAndGrounded.OnActivate, new Action(this.searching.SetActiveTrue));
				AgentState pursuing = this.swordsman.pursuing;
				pursuing.OnActivate = (Action)Delegate.Remove(pursuing.OnActivate, new Action(this.MaybeStartThrow));
				AgentState hunting = this.swordsman.hunting;
				hunting.OnActivate = (Action)Delegate.Remove(hunting.OnActivate, new Action(this.MaybeStartThrow));
				this.swordsman.actions.Remove(this);
			}
			this.axeThrowing.SetActive(true);
			FabricWrapper.PostEvent(this.prepareSound, base.gameObject);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x00099904 File Offset: 0x00097D04
		private void ThrowAxe()
		{
			Shootable instance = this.throwingAxePrefab.GetInstance<Shootable>();
			instance.transform.position = base.agent.chestPos;
			instance.Shoot(base.agent, this.aimDir + UnityEngine.Random.insideUnitSphere * this.aimDir.magnitude * 0.01f, this.trajectoryUtility.settings, this.attackSettings, this.target.mask0, this.target.mask1);
			this.target = default(LineOfSight.Sight);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0009999F File Offset: 0x00097D9F
		Vector3 IThreat.GetPos(Agent victim)
		{
			return base.agent.wPos;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000999AC File Offset: 0x00097DAC
		float IThreat.GetThreatDistance(Agent victim)
		{
			return (base.agent.wPos - victim.wPos).sqrMagnitude;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000999D8 File Offset: 0x00097DD8
		Vector3 IThreat.GetThreatDir(Agent victim)
		{
			return (base.agent.wPos - victim.wPos).normalized;
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00099A03 File Offset: 0x00097E03
		bool IThreat.GetTreatValid(Agent victim)
		{
			return this.lineOfSight.GetTreatValid(victim) || this.axeThrowing.timeSinceDeactivation < 1f;
		}

		// Token: 0x04001BDA RID: 7130
		private AgentState searching;

		// Token: 0x04001BDB RID: 7131
		private AgentState prepare;

		// Token: 0x04001BDC RID: 7132
		private AgentState axeThrowing;

		// Token: 0x04001BDD RID: 7133
		private Swordsman swordsman;

		// Token: 0x04001BDE RID: 7134
		private Pirate pirate;

		// Token: 0x04001BDF RID: 7135
		private static AnimId throwId = "Throw";

		// Token: 0x04001BE0 RID: 7136
		[SerializeField]
		public FabricEventReference prepareSound = string.Empty;

		// Token: 0x04001BE1 RID: 7137
		private LineOfSight.Sight target;

		// Token: 0x04001BE2 RID: 7138
		private float preferredDistance = 2f;

		// Token: 0x04001BE3 RID: 7139
		[SerializeField]
		private int ammo = 1;

		// Token: 0x04001BE4 RID: 7140
		private Vector3 aimDir;

		// Token: 0x04001BE5 RID: 7141
		private float aimTime;

		// Token: 0x04001BE6 RID: 7142
		private LineOfSight lineOfSight;

		// Token: 0x04001BE7 RID: 7143
		[SerializeField]
		private Shootable throwingAxePrefab;

		// Token: 0x04001BE8 RID: 7144
		[SerializeField]
		private TrajectoryUtility trajectoryUtility;

		// Token: 0x04001BE9 RID: 7145
		[SerializeField]
		private AttackSettings attackSettings;
	}
}
