using System;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007BF RID: 1983
	public class ThrowingAxe : Shootable
	{
		// Token: 0x06003372 RID: 13170 RVA: 0x000DCEB8 File Offset: 0x000DB2B8
		protected override void OnInstantiate()
		{
			base.OnInstantiate();
			this.projectiling.OnUpdate += this.ProjectilingUpdate;
			this.projectiling.OnUpdate += this.MaybeSplash;
			this.tumbling.OnUpdate += this.MaybeSplash;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000DCF10 File Offset: 0x000DB310
		private void ProjectilingUpdate()
		{
			Vector3 pos = this.projectile.pos;
			this.projectile.Update();
			Vector3 pos2 = this.projectile.pos;
			Vector3 direction = pos2 - pos;
			Ray ray = new Ray(pos, direction);
			float magnitude = direction.magnitude;
			LayerMask mask = (this.projectile.velocity.y <= 0f) ? this.mask1 : this.mask0;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, magnitude, mask))
			{
				Collider collider = raycastHit.collider;
				if (collider.gameObject.layer != this.hitLayer)
				{
					Singleton<DustParticles>.instance.SpawnParticles(raycastHit.point);
					this.Bounce(raycastHit.normal, raycastHit.normal * 4f);
					return;
				}
				Agent component = collider.gameObject.GetComponent<Agent>();
				if (component && component.aliveState.active)
				{
					Vector3 point = raycastHit.point;
					Attack attack = new Attack(this.attackSettings, this.projectile.velocity, point, this, (!this.shooter) ? null : this.shooter.Target.squad, this.attackSound, ScriptableObjectSingleton<PrefabManager>.instance.hitEffect);
					component.DealDamage(attack);
					this.Bounce(Vector3.up, Vector3.up * 7f);
					return;
				}
				base.gameObject.SetActive(false);
			}
			else
			{
				Vector3 one = Vector3.one;
				one.z += this.projectile.velocity.magnitude * 0.4f * Time.timeScale;
				base.transform.localScale = one;
				this.rb.MoveRotation(Quaternion.LookRotation(this.projectile.velocity));
				this.rb.MovePosition(this.projectile.pos);
			}
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000DD11C File Offset: 0x000DB51C
		private void Update()
		{
			if (this.projectiling.active)
			{
				this.rotator.transform.localRotation *= Quaternion.Euler(Time.deltaTime * 1500f, 0f, 0f);
			}
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000DD170 File Offset: 0x000DB570
		private void MaybeSplash()
		{
			if (base.transform.position.y < -0.105f)
			{
				this.Splash(base.transform.position);
			}
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000DD1AC File Offset: 0x000DB5AC
		private void OnCollisionEnter(Collision collision)
		{
			if (++this.bounces > 4)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000DD1DC File Offset: 0x000DB5DC
		private void OnCollisionStay(Collision collision)
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000DD1EC File Offset: 0x000DB5EC
		public void Bounce(Vector3 normal, Vector3 addedVelocity)
		{
			IslandGameplayManager.RequestCombatAudio(this.bounceSound, base.gameObject);
			this.bounces = 0;
			base.transform.localScale = Vector3.one;
			this.rb.GetComponentInChildren<Collider>().enabled = true;
			base.gameObject.layer = LayerMaster.debrisLayer.id;
			this.rb.velocity = Vector3.Reflect(this.projectile.velocity, normal) + addedVelocity * UnityEngine.Random.Range(0.8f, 1.2f);
			this.rb.angularVelocity = Vector3.right * (float)UnityEngine.Random.Range(-10000, 10000);
			this.tumbling.SetActive(true);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000DD2B0 File Offset: 0x000DB6B0
		public void Splash(Vector3 pos)
		{
			IslandGameplayManager.RequestCombatAudio(this.splashSound, base.gameObject);
			pos.y = -0.105f;
			ScriptableObjectSingleton<PrefabManager>.instance.splash.PlayAt(pos, Vector3.up);
			base.gameObject.SetActive(false);
			Singleton<CameraShaker>.instance.ShakeOnce(0.02f);
		}

		// Token: 0x04002300 RID: 8960
		[SerializeField]
		private Transform rotator;

		// Token: 0x04002301 RID: 8961
		[Space]
		[SerializeField]
		private float damage = 1.5f;

		// Token: 0x04002302 RID: 8962
		[SerializeField]
		private float knockback = 3f;

		// Token: 0x04002303 RID: 8963
		[Header("Audio")]
		[SerializeField]
		private string attackSound = "Sfx/Viking/Axe";

		// Token: 0x04002304 RID: 8964
		private FabricEventReference bounceSound = "Sfx/Viking/Axe/Bounce";

		// Token: 0x04002305 RID: 8965
		private FabricEventReference splashSound = "Sfx/Viking/Axe/Splash";

		// Token: 0x04002306 RID: 8966
		private FabricEventReference throwSound = "Sfx/Viking/Axe/Throw";

		// Token: 0x04002307 RID: 8967
		private const float rotationSpeed = 1500f;

		// Token: 0x04002308 RID: 8968
		private int bounces;
	}
}
