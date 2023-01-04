using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007BC RID: 1980
	public abstract class Shootable : SelfPoolingPrefab
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000DB622 File Offset: 0x000D9A22
		public Vector3 velocity
		{
			get
			{
				return this.projectile.velocity;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000DB62F File Offset: 0x000D9A2F
		public AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000DB63C File Offset: 0x000D9A3C
		protected override void OnInstantiate()
		{
			this.rb = base.GetComponent<Rigidbody>();
			this.rb.isKinematic = true;
			this.col = base.GetComponent<Collider>();
			this.col.enabled = false;
			base.gameObject.layer = LayerMaster.debrisLayer.id;
			this.held = new AgentState("Held", this.rootState, true, true);
			this.flying = new AgentState("Flying", this.rootState, false, true);
			this.tumbling = new AgentState("Tumbling", this.flying, false, true);
			this.projectiling = new AgentState("Projectiling", this.flying, false, true);
			AgentState agentState = this.projectiling;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				base.ReturnToParent();
				base.transform.localScale = Vector3.one;
			}));
			AgentState agentState2 = this.tumbling;
			agentState2.OnActivate = (Action)Delegate.Combine(agentState2.OnActivate, new Action(delegate()
			{
				this.rb.isKinematic = false;
				this.col.enabled = true;
				base.ReturnToParent();
			}));
			AgentState agentState3 = this.tumbling;
			agentState3.OnDeactivate = (Action)Delegate.Combine(agentState3.OnDeactivate, new Action(delegate()
			{
				this.rb.isKinematic = true;
				this.col.enabled = false;
			}));
			AgentState agentState4 = this.held;
			agentState4.OnActivate = (Action)Delegate.Combine(agentState4.OnActivate, new Action(delegate()
			{
				this.rb.interpolation = RigidbodyInterpolation.None;
			}));
			AgentState agentState5 = this.held;
			agentState5.OnDeactivate = (Action)Delegate.Combine(agentState5.OnDeactivate, new Action(delegate()
			{
				this.rb.interpolation = RigidbodyInterpolation.Interpolate;
			}));
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000DB7B1 File Offset: 0x000D9BB1
		protected override void OnGet()
		{
			this.held.SetActive(true);
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000DB7C0 File Offset: 0x000D9BC0
		public virtual void Shoot(Agent shooter, Vector3 velocity, ProjectileSettings projectileSettings, AttackSettings attackSettings, LayerMask mask0, LayerMask mask1)
		{
			using ("Shootable.Shoot")
			{
				this.shooter = shooter;
				this.attackSettings = attackSettings;
				this.projectileSettings = projectileSettings;
				this.projectile = new Projectile(velocity, projectileSettings);
				this.projectile.pos = this.projectile.pos + base.transform.position;
				base.transform.position = this.projectile.pos;
				base.transform.rotation = Quaternion.LookRotation(velocity);
				this.rb.drag = projectileSettings.drag;
				using ("ReturnToParent()")
				{
					base.ReturnToParent();
				}
				LayerMaster.LayerRef layerRef = (!shooter.isEnglish) ? LayerMaster.enBodies : LayerMaster.viBodies;
				this.hitLayer = layerRef.id;
				this.mask0 = (mask0 | layerRef.mask);
				this.mask1 = (mask1 | layerRef.mask);
				if (shooter.isEnglish)
				{
					this.mask1 |= LayerMaster.longshipModulesMask;
				}
				this.fineMask = LayerMaster.arrowHigh;
				using ("projectiling.setActive()")
				{
					this.projectiling.SetActive(true);
				}
			}
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000DB9A4 File Offset: 0x000D9DA4
		private void FixedUpdate()
		{
			this.rootState.Update();
		}

		// Token: 0x040022DC RID: 8924
		protected Rigidbody rb;

		// Token: 0x040022DD RID: 8925
		protected Collider col;

		// Token: 0x040022DE RID: 8926
		protected Projectile projectile;

		// Token: 0x040022DF RID: 8927
		protected WeakReference<Agent> shooter = new WeakReference<Agent>(null);

		// Token: 0x040022E0 RID: 8928
		public AttackSettings attackSettings;

		// Token: 0x040022E1 RID: 8929
		protected ProjectileSettings projectileSettings;

		// Token: 0x040022E2 RID: 8930
		protected LayerMask mask0;

		// Token: 0x040022E3 RID: 8931
		protected LayerMask mask1;

		// Token: 0x040022E4 RID: 8932
		protected LayerMask fineMask;

		// Token: 0x040022E5 RID: 8933
		protected int hitLayer;

		// Token: 0x040022E6 RID: 8934
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040022E7 RID: 8935
		public AgentState held;

		// Token: 0x040022E8 RID: 8936
		public AgentState tumbling;

		// Token: 0x040022E9 RID: 8937
		public AgentState projectiling;

		// Token: 0x040022EA RID: 8938
		public AgentState flying;
	}
}
