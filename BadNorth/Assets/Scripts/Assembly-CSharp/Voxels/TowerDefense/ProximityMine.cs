using System;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;
using Voxels.TowerDefense.TriFlow;

namespace Voxels.TowerDefense
{
	// Token: 0x02000893 RID: 2195
	internal class ProximityMine : PoolablePrefab
	{
		// Token: 0x06003971 RID: 14705 RVA: 0x000FB850 File Offset: 0x000F9C50
		public override void OnCreated()
		{
			this.animator = base.GetComponent<Animator>();
			this.carried = new AgentState("Carried", this.root, false, true);
			this.flying = new AgentState("Flying", this.root, false, true);
			this.placed = new AgentState("Placed", this.root, false, true);
			this.projectileSolver = base.GetComponent<IProjectileSolver>();
			this.flying.OnUpdate += delegate()
			{
				this.projectile.Update();
				if (this.projectile.hasArrived)
				{
					this.Place(this.data.navPos);
				}
				else
				{
					base.transform.position = this.projectile.position;
				}
			};
			this.placed.OnUpdate += delegate()
			{
				if (this.placed.timeSinceActivation < 0.5f)
				{
					return;
				}
				this.sample = this.data.flowField.flowField.SampleDistance(this.data.navPos);
				if (this.sample < this.data.detectionDistance)
				{
					this.Trigger();
				}
			};
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000FB8EB File Offset: 0x000F9CEB
		[ContextMenu("Trigger")]
		private void Trigger()
		{
			this.placed.SetActive(false);
			this.animator.Play(ProximityMine.triggeredId);
			IslandGameplayManager.RequestCombatAudio(ProximityMine.jumpAudioId, base.gameObject);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000FB920 File Offset: 0x000F9D20
		public void Explode()
		{
			this.explosion.Play();
			this.stamp.Stamp(this.data.navPos, 1f);
			this.data.navPos.island.painter.Paint(new Bounds(this.data.navPos.pos, Vector3.one), Painter.sootColor);
			ExplosionHelpers.CreateExplosion(this.data.navPos, UnityEngine.Random.insideUnitSphere, this.data.explosionDef, base.gameObject, this.data.owningAgent);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000FB9BD File Offset: 0x000F9DBD
		public void Done()
		{
			base.ReturnToPool();
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000FB9C5 File Offset: 0x000F9DC5
		public void Setup(Agent owningAgent, ExplosionDef explosionDef, float detectionDist)
		{
			this.data = new ProximityMine.Data(owningAgent, explosionDef, detectionDist);
			this.carried.SetActive(true);
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000FB9E4 File Offset: 0x000F9DE4
		public void Throw(NavPos navPos)
		{
			this.data.navPos = navPos;
			base.transform.SetParent(navPos.island.runContainer);
			this.projectile.Launch(base.transform.position, navPos.pos, -15f, this.projectileSolver);
			this.flying.SetActive(true);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000FBA4C File Offset: 0x000F9E4C
		public void Place(NavPos navPos)
		{
			this.data.navPos = navPos;
			base.transform.SetParent(navPos.island.runContainer);
			base.transform.position = navPos.pos;
			Vector3 normal = navPos.GetNormal();
			this.rotatedBody.rotation = Quaternion.LookRotation(normal + Vector3.up);
			Transform transform = this.stamp.transform;
			Quaternion rotation = Quaternion.LookRotation(normal);
			this.shade.rotation = rotation;
			transform.rotation = rotation;
			this.placed.SetActive(true);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000FBAE2 File Offset: 0x000F9EE2
		private void Update()
		{
			this.root.Update();
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000FBAF0 File Offset: 0x000F9EF0
		public override void OnReturnedToPool()
		{
			this.data = default(ProximityMine.Data);
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000FBB0C File Offset: 0x000F9F0C
		private void OnDrawGizmos()
		{
			if (!this.data.navPos.valid)
			{
				return;
			}
			Gizmos.color = ((!this.data.detection) ? Color.white : Color.red);
			ExtraGizmos.DrawCircle(base.transform.position + Vector3.up * 0.05f, this.data.detectionDistance, 16);
			ExtraGizmos.DrawCircle(base.transform.position + Vector3.up * 0.05f, this.sample, 16);
			Gizmos.color = Color.clear;
			Gizmos.DrawSphere(base.transform.position + Vector3.up * 0.05f, 0.2f);
		}

		// Token: 0x04002789 RID: 10121
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("ProximityMine", EVerbosity.Quiet, 100);

		// Token: 0x0400278A RID: 10122
		[SerializeField]
		private float triggerTime = 0.6f;

		// Token: 0x0400278B RID: 10123
		[SerializeField]
		private Transform shade;

		// Token: 0x0400278C RID: 10124
		[SerializeField]
		private Transform rotatedBody;

		// Token: 0x0400278D RID: 10125
		[SerializeField]
		private ParticleSystem explosion;

		// Token: 0x0400278E RID: 10126
		[SerializeField]
		private SpriteStamp stamp;

		// Token: 0x0400278F RID: 10127
		[SerializeField]
		private AgentStateRoot root;

		// Token: 0x04002790 RID: 10128
		private SimpleProjectile projectile = new SimpleProjectile();

		// Token: 0x04002791 RID: 10129
		private IProjectileSolver projectileSolver;

		// Token: 0x04002792 RID: 10130
		private AgentState carried;

		// Token: 0x04002793 RID: 10131
		private AgentState flying;

		// Token: 0x04002794 RID: 10132
		private AgentState placed;

		// Token: 0x04002795 RID: 10133
		private static AnimId triggeredId = "Triggered";

		// Token: 0x04002796 RID: 10134
		private static FabricEventReference jumpAudioId = "Sfx/Mine/Jump";

		// Token: 0x04002797 RID: 10135
		private Animator animator;

		// Token: 0x04002798 RID: 10136
		private ProximityMine.Data data = default(ProximityMine.Data);

		// Token: 0x04002799 RID: 10137
		[SerializeField]
		private float sample;

		// Token: 0x02000894 RID: 2196
		private struct Data
		{
			// Token: 0x0600397E RID: 14718 RVA: 0x000FBCC0 File Offset: 0x000FA0C0
			public Data(Agent owningAgent, ExplosionDef explosionDef, float detectionRadius)
			{
				this.owningAgent = owningAgent;
				this.explosionDef = explosionDef;
				this.detectionDistance = detectionRadius;
				this.owningSquad = owningAgent.squad;
				this.flowField = owningAgent.faction.enemy.presenceObj;
				this.navPos = default(NavPos);
				this.detectionTimer = 0f;
				this.detection = false;
			}

			// Token: 0x0400279A RID: 10138
			public Agent owningAgent;

			// Token: 0x0400279B RID: 10139
			public Squad owningSquad;

			// Token: 0x0400279C RID: 10140
			public NavPos navPos;

			// Token: 0x0400279D RID: 10141
			public TriFlowBehaviour flowField;

			// Token: 0x0400279E RID: 10142
			public ExplosionDef explosionDef;

			// Token: 0x0400279F RID: 10143
			public float detectionDistance;

			// Token: 0x040027A0 RID: 10144
			public float detectionTimer;

			// Token: 0x040027A1 RID: 10145
			public bool detection;
		}
	}
}
