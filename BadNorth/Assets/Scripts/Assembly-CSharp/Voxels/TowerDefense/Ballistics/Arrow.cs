using System;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007B6 RID: 1974
	public class Arrow : Shootable
	{
		// Token: 0x06003324 RID: 13092 RVA: 0x000DBA78 File Offset: 0x000D9E78
		public override void Shoot(Agent shooter, Vector3 velocity, ProjectileSettings projectileSettings, AttackSettings attackSettings, LayerMask mask0, LayerMask mask1)
		{
			using ("Arrow.Shoot()")
			{
				this.onHitGround = null;
				base.Shoot(shooter, velocity, projectileSettings, attackSettings, mask0, mask1);
			}
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000DBACC File Offset: 0x000D9ECC
		protected override void OnInstantiate()
		{
			base.OnInstantiate();
			this.projectiling.OnUpdate += delegate()
			{
				Ray ray = new Ray(this.projectile.pos, this.projectile.velocity);
				float maxDistance = this.projectile.velocity.magnitude * Time.fixedDeltaTime;
				LayerMask mask = (this.projectile.velocity.y <= 0f) ? this.mask1 : this.mask0;
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, maxDistance, mask))
				{
					this.projectiling.SetActive(false);
					Collider collider = raycastHit.collider;
					if (collider.gameObject.layer == this.hitLayer)
					{
						bool flag = false;
						Agent component = collider.gameObject.GetComponent<Agent>();
						if (component)
						{
							Vector3 point = raycastHit.point;
							Attack attack = new Attack(this.attackSettings, this.projectile.velocity, point, this, (!this.shooter) ? null : this.shooter.Target.squad, this.attackSound, ScriptableObjectSingleton<PrefabManager>.instance.hitEffect);
							if (!component.DealDamage(attack) && base.gameObject.activeSelf)
							{
								this.Bounce((raycastHit.normal - this.projectile.velocity).normalized);
								flag = true;
							}
							Singleton<CameraShaker>.instance.ShakeOnce(0.02f);
						}
						if (!flag)
						{
							base.gameObject.SetActive(false);
						}
						return;
					}
					if (this.onHitGround != null)
					{
						this.onHitGround(this);
					}
					if (UnityEngine.Random.value < Vector3.Dot(-this.projectile.velocity.normalized, raycastHit.normal))
					{
						RaycastHit hit;
						if (UnityEngine.Random.value > 0.5f && Physics.Raycast(ray, out hit, maxDistance, this.fineMask))
						{
							this.Stick(hit);
						}
						else
						{
							this.Smash(raycastHit.point, raycastHit.normal);
						}
					}
					else
					{
						this.Bounce(raycastHit.normal);
					}
				}
				else
				{
					this.projectile.Update();
					this.rb.MoveRotation(Quaternion.LookRotation(this.projectile.velocity));
					this.rb.MovePosition(this.projectile.pos);
				}
			};
			this.flying.OnUpdate += delegate()
			{
				if (base.transform.position.y < -0.105f)
				{
					this.Splash(base.transform.position);
				}
			};
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000DBB04 File Offset: 0x000D9F04
		private void OnCollisionEnter(Collision collision)
		{
			Collider collider = collision.collider;
			this.bounces++;
			if (this.bounces > 3)
			{
				base.ReturnToPool();
			}
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000DBB38 File Offset: 0x000D9F38
		private void OnCollisionStay(Collision collision)
		{
			base.ReturnToPool();
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000DBB40 File Offset: 0x000D9F40
		public void Splash(Vector3 pos)
		{
			IslandGameplayManager.RequestCombatAudio(this.splashSound, base.gameObject);
			pos.y = -0.105f;
			this.arrowSplash.PlayAt(pos, Vector3.up);
			base.ReturnToPool();
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000DBB77 File Offset: 0x000D9F77
		public void Smash(Vector3 pos, Vector3 dir)
		{
			IslandGameplayManager.RequestCombatAudio(this.smashSound, base.gameObject);
			this.arrowSmash.PlayAt(pos, dir);
			base.ReturnToPool();
			Singleton<CameraShaker>.instance.ShakeOnce(0.02f);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000DBBB0 File Offset: 0x000D9FB0
		private void Stick(RaycastHit hit)
		{
			base.transform.localScale = Vector3.one;
			SpriteStamperRoot componentInParent = hit.collider.GetComponentInParent<SpriteStamperRoot>();
			if (componentInParent)
			{
				IslandGameplayManager.RequestCombatAudio(this.stickSound, base.gameObject);
				base.transform.position = hit.point;
				Vector3 vector = this.projectile.velocity.normalized * 0.7f * UnityEngine.Random.value;
				vector += -hit.normal * UnityEngine.Random.value * 0.3f;
				vector += UnityEngine.Random.insideUnitSphere * 0.2f;
				base.transform.rotation = Quaternion.LookRotation(vector);
				componentInParent.Add(this.GetStamp());
			}
			base.ReturnToPool();
			Singleton<CameraShaker>.instance.ShakeOnce(0.05f);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000DBC9C File Offset: 0x000DA09C
		public SpriteStampDef GetStamp()
		{
			return new SpriteStampDef(this.spriteRenderer)
			{
				sprite = this.stampedSprites[UnityEngine.Random.Range(0, this.stampedSprites.Length)]
			};
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000DBCD4 File Offset: 0x000DA0D4
		public void Bounce(Vector3 normal)
		{
			base.transform.localScale = Vector3.one;
			IslandGameplayManager.RequestCombatAudio(this.bounceSound, base.gameObject);
			this.rb.velocity = Vector3.Reflect(this.projectile.velocity, normal);
			this.rb.angularVelocity += UnityEngine.Random.insideUnitSphere * 1000f;
			this.tumbling.SetActive(true);
		}

		// Token: 0x040022C9 RID: 8905
		[Header("Prefab References")]
		[SerializeField]
		private ReusableParticle arrowSmash;

		// Token: 0x040022CA RID: 8906
		[SerializeField]
		private ReusableParticle arrowSplash;

		// Token: 0x040022CB RID: 8907
		public SpriteRenderer spriteRenderer;

		// Token: 0x040022CC RID: 8908
		[SerializeField]
		private Sprite[] stampedSprites;

		// Token: 0x040022CD RID: 8909
		private int bounces;

		// Token: 0x040022CE RID: 8910
		[Header("Sound")]
		public string attackSound = "Sfx/Juice/Arrow/";

		// Token: 0x040022CF RID: 8911
		public string bounceSound = "Sfx/Juice/ImpactArrowGroundBounce";

		// Token: 0x040022D0 RID: 8912
		public string stickSound = "Sfx/Juice/ImpactArrowGroundStick";

		// Token: 0x040022D1 RID: 8913
		public string stickShieldSound = "Sfx/Juice/ImpactArrowShieldStick";

		// Token: 0x040022D2 RID: 8914
		public string smashSound = "Sfx/Juice/ImpactArrowGroundSmash";

		// Token: 0x040022D3 RID: 8915
		public string splashSound = "Sfx/Juice/ImpactArrowWater";

		// Token: 0x040022D4 RID: 8916
		public Action<Arrow> onHitGround;
	}
}
