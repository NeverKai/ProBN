using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200088B RID: 2187
	public class BombProjectile : PoolablePrefab, IThrowable
	{
		// Token: 0x0600393B RID: 14651 RVA: 0x000FA8C0 File Offset: 0x000F8CC0
		public override void OnCreated()
		{
			this.effect = base.GetComponentInChildren<ParticleSystem>(true);
			this.flying = new AgentState("Flying", this.root.rootState, false, true);
			this.dying = new AgentState("Dying", this.root.rootState, false, true);
			AgentState agentState = this.flying;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this.effect.Play();
			}));
			AgentState agentState2 = this.flying;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				this.effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}));
			this.flying.OnUpdate += delegate()
			{
				this.projectile.Update();
				this.translator.MovePosition(this.projectile.position);
				this.translator.MoveRotation(Quaternion.identity);
				this.rotator.MovePosition(this.projectile.position);
				this.rotator.MoveRotation(Quaternion.LookRotation(this.projectile.launchVelocity) * Quaternion.Euler(this.startAngle + this.projectile.currentTime * this.angularVelocity, 0f, 0f));
				this.disableTimer = Time.time;
				if (this.projectile.hasArrived)
				{
					this.Explode(this.explosionOverride, this.target);
				}
			};
			this.dying.OnUpdate += delegate()
			{
				if (this.dying.timeSinceActivation > 4f)
				{
					base.ReturnToPool();
				}
			};
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000FA990 File Offset: 0x000F8D90
		public void Setup(NavPos target, IProjectileSolver solver, float gravity, Agent owningAgent = null, ExplosionDef explosionOverride = null)
		{
			this.target = target;
			this.solver = solver;
			this.gravity = gravity;
			this.owningAgent = owningAgent;
			this.explosionOverride = explosionOverride;
			this.effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000FA9C4 File Offset: 0x000F8DC4
		public override void OnReturnedToPool()
		{
			this.root.rootState.SetActive(false);
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000FA9D8 File Offset: 0x000F8DD8
		private void FixedUpdate()
		{
			this.root.Update();
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000FA9E8 File Offset: 0x000F8DE8
		private void Explode(ExplosionDef explosionOverride, NavPos navPos)
		{
			ExplosionDef definition = (!explosionOverride) ? this.explosionDefinition : explosionOverride;
			ExplosionHelpers.CreateExplosion(navPos, UnityEngine.Random.insideUnitSphere, definition, base.gameObject, this.owningAgent);
			this.dying.SetActive(true);
			this.visible.SetActive(false);
			this.effect.Stop();
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000FAA49 File Offset: 0x000F8E49
		void IThrowable.AttachTo(Transform newParent)
		{
			base.transform.SetParent(newParent);
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000FAA77 File Offset: 0x000F8E77
		void IThrowable.SetVisible(bool visibility)
		{
			this.visible.SetActive(visibility);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000FAA88 File Offset: 0x000F8E88
		void IThrowable.ThrowAt(Vector3 worldSpaceTarget)
		{
			this.projectile.Launch(base.transform.position, worldSpaceTarget, this.gravity, this.solver);
			base.transform.position = this.projectile.position;
			this.flying.SetActive(true);
			base.transform.SetParent(this.owningAgent.faction.island.runContainer);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000FAAFB File Offset: 0x000F8EFB
		void IThrowable.Drop()
		{
			base.transform.position = this.owningAgent.navPos.wPos;
			this.Explode(this.droppedExplosion, this.owningAgent.navPos);
		}

		// Token: 0x04002749 RID: 10057
		[SerializeField]
		private GameObject visible;

		// Token: 0x0400274A RID: 10058
		private ParticleSystem effect;

		// Token: 0x0400274B RID: 10059
		[SerializeField]
		private Rigidbody translator;

		// Token: 0x0400274C RID: 10060
		[SerializeField]
		private Rigidbody rotator;

		// Token: 0x0400274D RID: 10061
		[SerializeField]
		private ExplosionDef explosionDefinition;

		// Token: 0x0400274E RID: 10062
		private ExplosionDef explosionOverride;

		// Token: 0x0400274F RID: 10063
		[SerializeField]
		private ExplosionDef droppedExplosion;

		// Token: 0x04002750 RID: 10064
		[SerializeField]
		private float angularVelocity = 90f;

		// Token: 0x04002751 RID: 10065
		[SerializeField]
		private float startAngle = 90f;

		// Token: 0x04002752 RID: 10066
		private IProjectileSolver solver;

		// Token: 0x04002753 RID: 10067
		private float gravity;

		// Token: 0x04002754 RID: 10068
		private Agent owningAgent;

		// Token: 0x04002755 RID: 10069
		private SimpleProjectile projectile = new SimpleProjectile();

		// Token: 0x04002756 RID: 10070
		private NavPos target;

		// Token: 0x04002757 RID: 10071
		private float disableTimer;

		// Token: 0x04002758 RID: 10072
		[SerializeField]
		private AgentStateRoot root;

		// Token: 0x04002759 RID: 10073
		private AgentState flying;

		// Token: 0x0400275A RID: 10074
		private AgentState dying;
	}
}
