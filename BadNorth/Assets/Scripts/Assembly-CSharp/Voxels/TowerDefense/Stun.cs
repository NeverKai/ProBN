﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A7 RID: 1703
	public class Stun : AgentComponent, Agent.IPostAttack
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000A2E9E File Offset: 0x000A129E
		// (set) Token: 0x06002C03 RID: 11267 RVA: 0x000A2EA8 File Offset: 0x000A12A8
		public bool disableTriflowWhenDown
		{
			get
			{
				return this._disableTriflowWhenDown;
			}
			set
			{
				this._disableTriflowWhenDown = value;
				if (this.fall != null && this.fall.active)
				{
					base.agent.brain.triflow.SetActive(!this._disableTriflowWhenDown);
				}
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000A2EF8 File Offset: 0x000A12F8
		public override void Setup()
		{
			base.Setup();
			base.agent.aliveAndGrounded.OnUpdate += delegate()
			{
				this.accumulatedStun = Mathf.MoveTowards(this.accumulatedStun, 0f, Time.deltaTime / 4f);
			};
			this.fall = new AgentState("Stun", base.agent.exclusives, false, true);
			this.slide = new AgentState("Slide", base.agent.body.sliding, false, true);
			this.fall.OnDebugString.Add(() => this.stunTimer.ToString("F2"));
			AgentState agentState = this.fall;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this.enumerator = this.FallOver();
			}));
			this.fall.OnUpdate += delegate()
			{
				base.agent.movability = 0.1f;
				if (!this.enumerator.MoveNext())
				{
					this.fall.SetActive(false);
				}
			};
			AgentState agentState2 = this.fall;
			agentState2.OnChange = (Action<bool>)Delegate.Combine(agentState2.OnChange, new Action<bool>(delegate(bool x)
			{
				base.agent.SetMoveAnimate(!x);
				base.agent.body.sliding.SetActive(x);
			}));
			AgentState agentState3 = this.fall;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				this.accumulatedStun = 0f;
			}));
			this.slide.OnDebugString.Add(() => this.accumulatedStun.ToString("F3"));
			this.slide.OnUpdate += delegate()
			{
				if (base.agent.velocity.sqrMagnitude < 0.04f)
				{
					base.agent.body.sliding.SetActive(false);
				}
			};
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000A3044 File Offset: 0x000A1444
		private IEnumerator<object> FallOver()
		{
			base.agent.PlayAnimation(Stun.fallId, 0f);
			if (this.disableTriflowWhenDown)
			{
				base.agent.brain.triflow.SetActive(false);
			}
			yield return null;
			while (base.agent.velocity.sqrMagnitude > 4f)
			{
				base.agent.PlayAnimation(Stun.fallId, 0f);
				yield return null;
			}
			IslandGameplayManager.RequestCombatAudio(Stun.hitGroundSound, base.gameObject);
			while (!base.agent.animationDone)
			{
				yield return null;
			}
			base.agent.PlayAnimation(Stun.sitId);
			while (this.stunTimer > 0f)
			{
				yield return null;
				this.stunTimer -= Time.deltaTime;
			}
			IslandGameplayManager.RequestCombatAudio(Stun.getUpSound, base.gameObject);
			base.agent.PlayAnimation(Stun.riseId);
			while (!base.agent.animationDone)
			{
				yield return null;
			}
			if (this.disableTriflowWhenDown)
			{
				base.agent.brain.triflow.SetActive(true);
			}
			yield break;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000A3060 File Offset: 0x000A1460
		void Agent.IPostAttack.PostAttack(Attack attack, float addedSpeed, ref bool keepGoing)
		{
			if (!base.agent.aliveAndGrounded.active)
			{
				return;
			}
			this.accumulatedStun += attack.stun * this.stunMultiplier / base.agent.mass;
			if (this.canFall && this.accumulatedStun > UnityEngine.Random.Range(1f, 3f) && !this.fall.active)
			{
				this.fall.SetActive(true);
				this.stunTimer = Mathf.Max(Mathf.Sqrt(this.accumulatedStun), 2f) * UnityEngine.Random.Range(0.5f, 1f);
				keepGoing = false;
				return;
			}
			if (this.canSlide && this.accumulatedStun > UnityEngine.Random.Range(0.7f, 1.2f))
			{
				this.slide.SetActive(true);
				return;
			}
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000A314E File Offset: 0x000A154E
		[ContextMenu("Do Stun")]
		private void DoStun()
		{
			this.stunTimer = UnityEngine.Random.Range(1f, 3f);
			this.fall.SetActive(true);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000A3174 File Offset: 0x000A1574
		private void StunUpdate()
		{
			if (base.agent.body.hopping.active && base.agent.velocity.magnitude < 0.001f && this.fall.timeSinceActivation > 0.5f)
			{
				base.agent.body.sliding.SetActive(false);
			}
		}

		// Token: 0x04001CBF RID: 7359
		private static AnimId fallId = "Fall";

		// Token: 0x04001CC0 RID: 7360
		private static AnimId riseId = "Rise";

		// Token: 0x04001CC1 RID: 7361
		private static AnimId sitId = "Sit";

		// Token: 0x04001CC2 RID: 7362
		private IEnumerator<object> enumerator;

		// Token: 0x04001CC3 RID: 7363
		private float stunTimer;

		// Token: 0x04001CC4 RID: 7364
		private float accumulatedStun;

		// Token: 0x04001CC5 RID: 7365
		[SerializeField]
		private bool canFall = true;

		// Token: 0x04001CC6 RID: 7366
		[SerializeField]
		private bool canSlide = true;

		// Token: 0x04001CC7 RID: 7367
		[SerializeField]
		[Range(0f, 1f)]
		public float stunMultiplier = 1f;

		// Token: 0x04001CC8 RID: 7368
		[SerializeField]
		private bool _disableTriflowWhenDown;

		// Token: 0x04001CC9 RID: 7369
		private static FabricEventReference getUpSound = "Sfx/GetUp";

		// Token: 0x04001CCA RID: 7370
		private static FabricEventReference hitGroundSound = "Sfx/HitGround";

		// Token: 0x04001CCB RID: 7371
		public AgentState slide;

		// Token: 0x04001CCC RID: 7372
		public AgentState fall;
	}
}