using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006A1 RID: 1697
	public class Ragdoller : AgentComponent
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000A1530 File Offset: 0x0009F930
		public override void Setup()
		{
			base.Setup();
			this.ragdollState = new AgentState("Ragdoll", base.agent.navigationState, false, true);
			this.riseState = new AgentState("Rise", base.agent.exclusives, false, true);
			AgentState agentState = this.riseState;
			agentState.OnChange = (Action<bool>)Delegate.Combine(agentState.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			AgentState agentState2 = this.riseState;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				base.agent.body.sliding.SetActive(true);
			}));
			this.riseState.OnUpdate += delegate()
			{
				if (base.agent.animationDone)
				{
					this.riseState.SetActive(false);
				}
			};
			AgentState agentState3 = this.riseState;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				base.agent.body.sliding.SetActive(false);
			}));
			AgentState agentState4 = this.riseState;
			agentState4.OnActivate = (Action)Delegate.Combine(agentState4.OnActivate, new Action(delegate()
			{
				base.agent.PlayAnimation(Ragdoller.riseId);
			}));
			AgentState agentState5 = this.ragdollState;
			agentState5.OnChange = (Action<bool>)Delegate.Combine(agentState5.OnChange, new Action<bool>(base.agent.SetMoveAnimateInverse));
			this.ragdollState.OnUpdate += this.RagdollUpdate;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000A1678 File Offset: 0x0009FA78
		public void Launch(Vector3 velocity)
		{
			this.BeginRagdoll(velocity);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000A1684 File Offset: 0x0009FA84
		public bool MaybeRagdoll(float speed)
		{
			if (!base.agent.navPos.navigationMesh.island)
			{
				speed *= ((!this.canFallOffShip) ? 0f : 0.8f);
			}
			if (speed < 2f)
			{
				return false;
			}
			if (this.ragdoll)
			{
				return false;
			}
			this.BeginRagdoll(base.agent.velocity + base.agent.navPos.GetNormal() * 0.67f);
			return true;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000A1720 File Offset: 0x0009FB20
		private void BeginRagdoll(Vector3 velocity)
		{
			this.ragdoll = ScriptableObjectSingleton<PrefabManager>.instance.ragdoll.GetInstance<Ragdoll>();
			this.ragdollState.SetActive(true);
			base.agent.animator.Play(Ragdoller.RagdollID, -1, UnityEngine.Random.value);
			this.ragdoll.ragdoller = this;
			this.ragdoll.col.radius = base.agent.radius;
			this.ragdoll.rb.mass = base.agent.mass;
			this.ragdoll.transform.position = base.agent.transform.position + base.agent.chestOffset;
			this.ragdoll.transform.rotation = base.agent.transform.rotation;
			this.ragdoll.rb.isKinematic = false;
			this.ragdoll.rb.velocity = velocity;
			base.agent.body.SetGrass(1f);
			base.transform.SetParent(base.agent.faction.island.runContainer);
			base.agent.navPos.SetNull();
			base.agent.velocity = Vector3.zero;
			this.maxY = this.ragdoll.transform.position.y;
			IslandGameplayManager.RequestCombatAudio(Ragdoller.fallAudio, base.gameObject);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000A18A6 File Offset: 0x0009FCA6
		private void OnDestroy()
		{
			this.DisableRagdoll();
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000A18B0 File Offset: 0x0009FCB0
		public void OnColEnter(Collision collision)
		{
			if (base.agent.deadState.active)
			{
				this.Smash(collision);
			}
			else
			{
				float num = Vector3.Dot(collision.contacts[0].normal, -collision.relativeVelocity);
				if (num > 12f)
				{
					this.Smash(collision);
				}
				else
				{
					Singleton<DustParticles>.instance.SpawnParticles(collision.contacts[0].point, collision.contacts[0].normal);
					base.agent.animator.Play(Ragdoller.RagdollID, -1, UnityEngine.Random.value);
					this.OnColStay(collision);
				}
			}
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000A196C File Offset: 0x0009FD6C
		public void OnColStay(Collision collision)
		{
			if (!this.ragdoll)
			{
				return;
			}
			float sqrMagnitude = this.ragdoll.rb.velocity.sqrMagnitude;
			if (sqrMagnitude < 1f && collision.contacts[0].normal.y > 0f && this.ragdoll.rb.velocity.y <= 0f && !this.TryAttach() && sqrMagnitude < 0.040000003f)
			{
				this.Smash(collision);
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000A1A10 File Offset: 0x0009FE10
		private void Smash(Collision collision)
		{
			if (collision.collider)
			{
				NavPos empty = NavPos.empty;
				Longship componentInParent = collision.collider.GetComponentInParent<Longship>();
				ScriptableObjectSingleton<PrefabManager>.instance.bloodSplat.PlayAt(collision.contacts[0].point, collision.contacts[0].normal);
				base.agent.GetComponent<Death>().Die(empty);
			}
			else
			{
				ScriptableObjectSingleton<PrefabManager>.instance.bloodSplat.PlayAt(base.agent.chestPos, Vector3.up);
				base.agent.GetComponent<Death>().Die();
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000A1AB8 File Offset: 0x0009FEB8
		private bool TryAttach()
		{
			if (base.agent.deadState.active)
			{
				return false;
			}
			if (this.ragdollState.timeSinceActivation < 0.2f)
			{
				return false;
			}
			NavPos navPos = new NavPos(base.agent.faction.island.navManager.navigationMesh, this.ragdoll.transform.position, true, 1f);
			if ((navPos.pos - this.ragdoll.transform.position).sqrMagnitude > 0.4f)
			{
				return false;
			}
			Debug.DrawLine(this.ragdoll.transform.position, navPos.wPos, Color.cyan, 6f);
			base.agent.velocity = this.ragdoll.rb.velocity * 0.2f;
			base.agent.navPos = navPos;
			base.agent.body.SetGrass(navPos);
			this.riseState.SetActive(true);
			base.agent.SetDirection(base.agent.transform.forward);
			base.agent.transform.position = this.ragdoll.transform.position - base.agent.transform.up * base.agent.chestHeight;
			base.agent.transform.rotation = Quaternion.LookRotation(Vector3.up, -base.agent.transform.forward) * Quaternion.Euler(90f, 0f, 0f);
			base.agent.transform.SetParent(navPos.transform);
			this.DisableRagdoll();
			IslandGameplayManager.RequestCombatAudio(Ragdoller.landAudio, base.gameObject);
			return true;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000A1CA8 File Offset: 0x000A00A8
		private void RagdollUpdate()
		{
			base.agent.transform.position = this.ragdoll.rb.transform.position - base.agent.transform.up * base.agent.chestHeight;
			this.maxY = Mathf.Max(this.maxY, this.ragdoll.transform.position.y);
			if (this.ragdoll.rb.position.y < -0.205f)
			{
				Vector3 position = this.ragdoll.rb.position;
				position.y = -0.105f;
				ScriptableObjectSingleton<PrefabManager>.instance.splash.PlayAt(position, Vector3.up);
				IslandGameplayManager.RequestCombatAudio(Ragdoller.waterSplashAudio, base.gameObject);
				this.ragdoll.rb.velocity = Vector3.zero;
				base.agent.FinalDeath();
				this.DisableRagdoll();
			}
			else if ((this.ragdoll.rb.velocity == Vector3.zero || this.ragdoll.rb.IsSleeping()) && !this.TryAttach())
			{
				this.Smash(null);
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000A1E00 File Offset: 0x000A0200
		private void DisableRagdoll()
		{
			if (!this.ragdoll)
			{
				return;
			}
			this.ragdoll.gameObject.SetActive(false);
			this.ragdoll.ragdoller = null;
			this.ragdoll.rb.isKinematic = true;
			this.ragdoll = null;
		}

		// Token: 0x04001C95 RID: 7317
		[SerializeField]
		private bool canFallOffShip = true;

		// Token: 0x04001C96 RID: 7318
		private Ragdoll ragdoll;

		// Token: 0x04001C97 RID: 7319
		private const float attachSpeed = 1f;

		// Token: 0x04001C98 RID: 7320
		private const float escapeSpeed = 2f;

		// Token: 0x04001C99 RID: 7321
		private const float shipSpeedMultiplier = 0.8f;

		// Token: 0x04001C9A RID: 7322
		private float maxY = -1f;

		// Token: 0x04001C9B RID: 7323
		private static AnimId RagdollID = "Ragdoll";

		// Token: 0x04001C9C RID: 7324
		private static AnimId riseId = "Rise";

		// Token: 0x04001C9D RID: 7325
		private static FabricEventReference fallAudio = "Sfx/Fall/Fall";

		// Token: 0x04001C9E RID: 7326
		private static FabricEventReference landAudio = "Sfx/Fall/Land";

		// Token: 0x04001C9F RID: 7327
		private static FabricEventReference waterSplashAudio = "Sfx/Fall/Water";

		// Token: 0x04001CA0 RID: 7328
		private AgentState ragdollState;

		// Token: 0x04001CA1 RID: 7329
		private AgentState riseState;
	}
}
