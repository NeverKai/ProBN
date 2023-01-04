using System;
using UnityEngine;
using Voxels.TowerDefense.Ballistics;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A6 RID: 1702
	public class Shield : AgentComponent, IAttackResponder
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x000A217B File Offset: 0x000A057B
		private Bounds bounds
		{
			get
			{
				return this.mf.sharedMesh.bounds;
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000A2190 File Offset: 0x000A0590
		public override void Setup()
		{
			base.agent.shield = true;
			this.mf = this.shield.GetComponent<MeshFilter>();
			this.swordsman = base.GetComponent<Swordsman>();
			this.shieldCheck = new AgentState("ShieldCheck", base.agent.rootState, false, false);
			this.noShield = new AgentState("NoShield", this.shieldCheck, false, true);
			this.shielding = new AgentState("Shielding", this.shieldCheck, false, true);
			this.spearShield = new AgentState("SpearShield", this.shielding, false, true);
			this.rangeShield = new AgentState("RangeShield", this.shielding, false, true);
			this.parry = new AgentState("Parry", base.agent.brain.brainState, false, true);
			this.RangeCheck(true);
			AgentState idle = this.swordsman.idle;
			idle.OnChange = (Action<bool>)Delegate.Combine(idle.OnChange, new Action<bool>(this.RangeCheck));
			AgentState ready = this.swordsman.ready;
			ready.OnChange = (Action<bool>)Delegate.Combine(ready.OnChange, new Action<bool>(this.RangeCheck));
			AgentState hunting = this.swordsman.hunting;
			hunting.OnChange = (Action<bool>)Delegate.Combine(hunting.OnChange, new Action<bool>(this.RangeCheck));
			AgentState pursuing = this.swordsman.pursuing;
			pursuing.OnChange = (Action<bool>)Delegate.Combine(pursuing.OnChange, new Action<bool>(this.RangeCheck));
			this.shielding.OnUpdate += this.ShieldingUpdate;
			this.shieldCheck.OnUpdate += this.ShieldCheckUpdate;
			base.agent.groundedState.OnUpdate += this.ShieldAnimParams;
			AgentState agentState = this.shieldCheck;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(this.ShieldCheck));
			this.rangeShield.OnUpdate += this.RangeUpdate;
			AgentState agentState2 = this.rangeShield;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(this.RangeChange));
			this.spearShield.OnUpdate += this.SpearUpdate;
			this.parry.OnUpdate += this.ParryUpdate;
			AgentState agentState3 = this.parry;
			agentState3.OnChange = (Action<bool>)Delegate.Combine(agentState3.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			Agent agent = base.agent;
			agent.onFinalDeath = (Action)Delegate.Combine(agent.onFinalDeath, new Action(this.OnFinalDeath));
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000A244C File Offset: 0x000A084C
		private void OnFinalDeath()
		{
			if (this.spriteStamper)
			{
				this.spriteStamper.ReturnTrunk();
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000A246C File Offset: 0x000A086C
		private void RangeCheck(bool change)
		{
			this.shieldCheck.SetActive(this.swordsman.idle.active || this.swordsman.ready.active || this.swordsman.hunting.active || this.swordsman.pursuing.active);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000A24D8 File Offset: 0x000A08D8
		private void ShieldAnimParams()
		{
			this.shieldLerp = Mathf.MoveTowards(this.shieldLerp, (float)((!this.shielding.active) ? 0 : 1), Time.deltaTime * 4f);
			base.agent.animator.SetFloat(Shield.shieldID, this.shieldLerp);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000A2534 File Offset: 0x000A0934
		private void ShieldingUpdate()
		{
			base.agent.animator.SetFloat(Shield.shieldUpID, this.sheildDirTarget.y);
			base.agent.LookInDirection(this.sheildDirTarget, 900f, 10f);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000A2571 File Offset: 0x000A0971
		private void RangeChange(bool active)
		{
			IslandGameplayManager.RequestCombatAudio((!active) ? this.shieldDownSound : this.shieldUpSound, base.gameObject);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000A2595 File Offset: 0x000A0995
		private void RangeUpdate()
		{
			this.sheildDirTarget = base.agent.rangeWorry.dir;
			base.agent.speed *= 0.7f;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000A25C4 File Offset: 0x000A09C4
		private void SpearUpdate()
		{
			this.sheildDirTarget = base.agent.enemyDir;
			base.agent.speed *= 0.8f;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000A25EE File Offset: 0x000A09EE
		private void ShieldCheckUpdate()
		{
			if (base.agent.beats.hz4)
			{
				this.ShieldCheck();
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000A260C File Offset: 0x000A0A0C
		private void ShieldCheck()
		{
			if (base.agent.intimidated && base.agent.enemyBrain is Spear)
			{
				this.spearShield.SetActive(true);
				return;
			}
			if (base.agent.enemyDist < 1f && base.agent.enemyData.dangerous)
			{
				this.noShield.SetActive(true);
				return;
			}
			if (base.agent.rangeWorry.valid)
			{
				this.rangeShield.SetActive(true);
				return;
			}
			this.noShield.SetActive(true);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000A26B4 File Offset: 0x000A0AB4
		public void ModifyAttack(ref Attack attack)
		{
			CloseCombatBrain exists = attack.monoAttacker as CloseCombatBrain;
			if (exists && this.parryAnim)
			{
				if (this.parryAnim)
				{
					if (Vector3.Dot(-attack.direction, base.agent.lookDir) > 0f)
					{
						IslandGameplayManager.RequestCombatAudio(this.shieldSwordBlockSound, base.gameObject);
						IslandGameplayManager.RequestCombatAudio(this.shieldBlockSound, base.gameObject);
						attack.soundSuffix = "Shield";
						attack.effect = this.shieldSmash;
						attack.damage = 0f;
						this.swordsman.DebugMessage("Parry");
					}
				}
				else if (Vector3.Dot(-attack.direction, base.agent.lookDir) > 0.5f)
				{
					IslandGameplayManager.RequestCombatAudio(this.shieldSwordBlockSound, base.gameObject);
					IslandGameplayManager.RequestCombatAudio(this.shieldBlockSound, base.gameObject);
					attack.soundSuffix = "Shield";
					attack.effect = this.shieldSmash;
				}
			}
			if (this.shielding.active)
			{
				float num = Vector3.Dot(this.shield.forward, -attack.direction.normalized);
				if (num > ((!this.rangeShield.active) ? 0.5f : 0f))
				{
					Shootable shootable = attack.monoAttacker as Shootable;
					if (shootable)
					{
						attack.damage *= ((!base.agent.isEnglish) ? 0.05f : 0f);
						attack.stun *= 0.2f;
						Arrow arrow = shootable as Arrow;
						if (arrow)
						{
							if ((double)UnityEngine.Random.value < 0.5)
							{
								IslandGameplayManager.RequestCombatAudio(this.shieldArrowStickSound, base.gameObject);
								IslandGameplayManager.RequestCombatAudio(this.shieldBlockSound, base.gameObject);
								attack.soundSuffix = "ShieldStick";
								attack.effect = null;
								this.Stick(arrow);
							}
							else if (UnityEngine.Random.value > 0.5f)
							{
								IslandGameplayManager.RequestCombatAudio(this.shieldArrowSmashSound, base.gameObject);
								IslandGameplayManager.RequestCombatAudio(this.shieldBlockSound, base.gameObject);
								attack.soundSuffix = "ShieldSmash";
								attack.effect = null;
								arrow.Smash(this.shield.transform.position, this.shield.transform.forward);
							}
							else
							{
								IslandGameplayManager.RequestCombatAudio(this.shieldArrowBounceSound, base.gameObject);
								IslandGameplayManager.RequestCombatAudio(this.shieldBlockSound, base.gameObject);
								attack.soundSuffix = "ShieldBounce";
								attack.effect = null;
								arrow.Bounce(this.shield.transform.forward + Vector3.up);
							}
						}
						else
						{
							TankArcherArrow exists2 = shootable as TankArcherArrow;
							if (exists2)
							{
								attack.soundSuffix = "Shield";
							}
						}
					}
					ThrowingAxe throwingAxe = attack.monoAttacker as ThrowingAxe;
					if (throwingAxe)
					{
						attack.damage = 0f;
						attack.stun *= 0.2f;
						IslandGameplayManager.RequestCombatAudio(this.shieldArrowBounceSound, base.gameObject);
						IslandGameplayManager.RequestCombatAudio(this.shieldBlockSound, base.gameObject);
						attack.soundSuffix = "ShieldBounce";
						throwingAxe.Bounce(Vector3.up, Vector3.up * 7f);
					}
				}
			}
			if (attack.monoAttacker is Spear && this.spearShield.active && Vector3.Dot(this.shield.forward, -attack.direction.normalized) > 0f)
			{
				attack.soundSuffix = "Shield";
				attack.effect = this.shieldSmash;
				attack.damage *= 0.2f;
				attack.stun *= 0.4f;
			}
			JumpAttack jumpAttack = attack.monoAttacker as JumpAttack;
			if (jumpAttack && this.rangeShield.active && Vector3.Dot(this.shield.forward, -attack.direction.normalized) > 0f)
			{
				Attack attack2 = attack;
				attack.damage = 0f;
				attack.knockback *= 0.6f;
				attack.stun *= 0.3f;
				jumpAttack.hitShield = true;
				attack2.direction = -attack2.direction;
				attack2.damage = 0f;
				attack2.knockback *= 0.8f;
				attack2.stun *= 0.6f;
				attack2.soundPrefix = string.Empty;
				attack2.soundSuffix = string.Empty;
				attack2.monoAttacker = this;
				attack2.killerSquad = base.agent.squad;
				jumpAttack.agent.DealDamage(attack2);
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000A2BDC File Offset: 0x000A0FDC
		public void Stick(Arrow arrow)
		{
			IslandGameplayManager.RequestCombatAudio(arrow.stickShieldSound, arrow.gameObject);
			Vector3 a = Vector3.forward;
			a += UnityEngine.Random.onUnitSphere * 0.4f;
			Vector3 vector = a.normalized;
			vector.x *= this.bounds.extents.x;
			vector.y *= this.bounds.extents.y;
			vector.z = this.bounds.extents.z;
			vector += this.bounds.center;
			arrow.transform.rotation = Quaternion.LookRotation(this.shield.TransformVector(-a));
			arrow.transform.position = this.shield.transform.TransformPoint(vector);
			if (this.spriteStamper == null)
			{
				this.spriteStamper = this.shield.gameObject.AddComponent<SpriteStamperRoot>();
				this.spriteStamper.maxCount = 4;
			}
			this.spriteStamper.Add(arrow.GetStamp());
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000A2D20 File Offset: 0x000A1120
		public void MaybeParry(Brain attacker)
		{
			if (this.swordsman.ready.active || this.swordsman.pursuing.active)
			{
				base.agent.PlayAnimation(Shield.parryId);
				this.attacker = attacker;
				this.parry.SetActive(true);
			}
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000A2D80 File Offset: 0x000A1180
		private void ParryUpdate()
		{
			if (base.agent.animationDone)
			{
				if (base.agent.enemyAgent && base.agent.enemyAgent == this.attacker)
				{
					this.swordsman.Attack(base.agent.enemyAgent, null);
				}
				else
				{
					this.swordsman.ready.SetActive(true);
				}
				this.parryAnim = false;
				this.attacker = null;
			}
			else if (this.attacker)
			{
				this.attacker.agent.LookAt(this.attacker.agent.transform.position, 720f, 20f);
			}
		}

		// Token: 0x04001CA5 RID: 7333
		private static int shieldID = Animator.StringToHash("Shield");

		// Token: 0x04001CA6 RID: 7334
		private static int shieldUpID = Animator.StringToHash("ShieldUp");

		// Token: 0x04001CA7 RID: 7335
		public Transform shield;

		// Token: 0x04001CA8 RID: 7336
		public ReusableParticle shieldSmash;

		// Token: 0x04001CA9 RID: 7337
		private AgentState noShield;

		// Token: 0x04001CAA RID: 7338
		private AgentState shielding;

		// Token: 0x04001CAB RID: 7339
		private AgentState spearShield;

		// Token: 0x04001CAC RID: 7340
		private AgentState rangeShield;

		// Token: 0x04001CAD RID: 7341
		private AgentState shieldCheck;

		// Token: 0x04001CAE RID: 7342
		private AgentState parry;

		// Token: 0x04001CAF RID: 7343
		private float shieldLerp;

		// Token: 0x04001CB0 RID: 7344
		private Vector3 sheildDir;

		// Token: 0x04001CB1 RID: 7345
		private Vector3 sheildDirTarget;

		// Token: 0x04001CB2 RID: 7346
		private MeshFilter mf;

		// Token: 0x04001CB3 RID: 7347
		private SpriteStamperRoot spriteStamper;

		// Token: 0x04001CB4 RID: 7348
		private Swordsman swordsman;

		// Token: 0x04001CB5 RID: 7349
		public bool parryAnim;

		// Token: 0x04001CB6 RID: 7350
		[Header("Sounds")]
		public FabricEventReference shieldSwordBlockSound = "Sfx/English/SwordShield/Deflect";

		// Token: 0x04001CB7 RID: 7351
		public FabricEventReference shieldArrowBounceSound = "Sfx/English/SwordShield/DeflectArrow";

		// Token: 0x04001CB8 RID: 7352
		public FabricEventReference shieldArrowStickSound = "Sfx/English/SwordShield/ArrowStick";

		// Token: 0x04001CB9 RID: 7353
		public FabricEventReference shieldArrowSmashSound = "Sfx/English/SwordShield/ArrowSmash";

		// Token: 0x04001CBA RID: 7354
		public FabricEventReference shieldBlockSound = "Sfx/English/SwordShield/ShieldBlock";

		// Token: 0x04001CBB RID: 7355
		public FabricEventReference shieldUpSound = "Sfx/English/SwordShield/ShieldUp";

		// Token: 0x04001CBC RID: 7356
		public FabricEventReference shieldDownSound = "Sfx/English/SwordShield/ShieldDown";

		// Token: 0x04001CBD RID: 7357
		private static AnimId parryId = "Parry";

		// Token: 0x04001CBE RID: 7358
		private Brain attacker;
	}
}
