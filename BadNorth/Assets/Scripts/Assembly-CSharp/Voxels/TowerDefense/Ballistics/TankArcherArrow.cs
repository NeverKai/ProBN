using System;
using UnityEngine;
using Voxels.TowerDefense.SpriteMagic;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007BE RID: 1982
	public class TankArcherArrow : Shootable
	{
		// Token: 0x06003363 RID: 13155 RVA: 0x000DC723 File Offset: 0x000DAB23
		protected override void OnGet()
		{
			base.OnGet();
			this.spriteRenderer.color = Color.white;
			this.batchedSprite = this.spriteRenderer.GetComponentInChildren<BatchedSprite>();
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000DC74C File Offset: 0x000DAB4C
		public override void Shoot(Agent shooter, Vector3 velocity, ProjectileSettings projectileSettings, AttackSettings attackSettings, LayerMask mask0, LayerMask mask1)
		{
			base.Shoot(shooter, velocity, projectileSettings, attackSettings, mask0, mask1);
			this.bounces = 0;
			this.energy = 1f;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000DC76F File Offset: 0x000DAB6F
		protected override void OnInstantiate()
		{
			base.OnInstantiate();
			this.projectiling.OnUpdate += delegate()
			{
				Ray ray = new Ray(this.projectile.pos, this.projectile.velocity);
				float magnitude = this.projectile.velocity.magnitude;
				float maxDistance = magnitude * Time.fixedDeltaTime;
				Vector3 lhs = this.projectile.velocity / magnitude;
				LayerMask mask = (this.projectile.velocity.y <= 0f) ? this.mask1 : this.mask0;
				int num = Physics.RaycastNonAlloc(ray, TankArcherArrow.hits, maxDistance, mask);
				int i = 0;
				while (i < num)
				{
					RaycastHit hit = TankArcherArrow.hits[i];
					Collider collider = hit.collider;
					if (collider.gameObject.layer == this.hitLayer)
					{
						Agent component = collider.gameObject.GetComponent<Agent>();
						if (component)
						{
							Vector3 point = hit.point;
							Attack attack = new Attack(this.attackSettings, this.projectile.velocity, point, this, (!this.shooter) ? null : this.shooter.Target.squad, this.attackSound, null);
							attack.damage *= this.energy * this.energy * this.energy;
							attack.knockback *= this.energy;
							attack.launchImpulse *= this.energy;
							if (component.DealDamage(attack))
							{
								this.spriteRenderer.color = Color.Lerp(this.spriteRenderer.color, Singleton<ShaderConstants>.instance.bloodColor, 0.3f);
							}
							Singleton<CameraShaker>.instance.ShakeOnce(0.1f * this.energy);
							this.projectile.velocity = this.projectile.velocity * 0.8f;
							this.energy *= 0.8f;
							if (this.energy < this.minEnergy)
							{
								this.Tumble();
								break;
							}
						}
						i++;
					}
					else
					{
						float num2 = Vector3.Dot(lhs, -hit.normal);
						if (num2 >= 0.5f)
						{
							this.Stick(hit);
							return;
						}
						float num3 = 1f - num2;
						this.projectile.velocity = Vector3.Reflect(this.projectile.velocity, hit.normal);
						this.projectile.velocity = this.projectile.velocity * num3;
						this.energy *= num3;
						this.arrowSmash.PlayAt(hit.point, hit.normal + this.projectile.velocity);
						if (this.energy < this.minEnergy)
						{
							this.Tumble();
							break;
						}
						break;
					}
				}
				if (this.projectiling.active)
				{
					base.transform.localScale = Vector3.one.SetZ(1f + magnitude * 0.1f * Time.timeScale);
					this.projectile.Update();
					this.rb.MoveRotation(Quaternion.LookRotation(this.projectile.velocity));
					this.rb.MovePosition(this.projectile.pos);
					this.batchedSprite.UpdateScale();
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

		// Token: 0x06003366 RID: 13158 RVA: 0x000DC7A8 File Offset: 0x000DABA8
		private void OnCollisionEnter(Collision collision)
		{
			if (++this.bounces > 5)
			{
				base.ReturnToPool();
			}
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000DC7D2 File Offset: 0x000DABD2
		private void OnCollisionStay(Collision collision)
		{
			base.ReturnToPool();
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000DC7DC File Offset: 0x000DABDC
		public void Splash(Vector3 pos)
		{
			IslandGameplayManager.RequestCombatAudio(this.splashSound, base.gameObject);
			this.arrowSplash.PlayAt(pos.SetY(-0.105f), Vector3.up);
			base.ReturnToPool();
			Singleton<CameraShaker>.instance.ShakeOnce(0.02f);
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000DC82B File Offset: 0x000DAC2B
		public void Smash(Vector3 pos, Vector3 dir)
		{
			IslandGameplayManager.RequestCombatAudio(this.smashSound, base.gameObject);
			this.arrowSmash.PlayAt(pos, dir);
			base.ReturnToPool();
			Singleton<CameraShaker>.instance.ShakeOnce(0.05f);
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000DC864 File Offset: 0x000DAC64
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
				this.StampTo(componentInParent);
			}
			base.ReturnToPool();
			Singleton<CameraShaker>.instance.ShakeOnce(0.1f);
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000DC94C File Offset: 0x000DAD4C
		public void StampTo(SpriteStamperRoot stamperRoot)
		{
			base.transform.localScale = Vector3.one;
			stamperRoot.Add(new SpriteStampDef(this.spriteRenderer)
			{
				sprite = this.stampedSprites[UnityEngine.Random.Range(0, this.stampedSprites.Length)]
			});
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000DC99C File Offset: 0x000DAD9C
		public void Tumble()
		{
			base.transform.localScale = Vector3.one;
			this.bounces = 0;
			this.rb.velocity = this.projectile.velocity;
			this.rb.angularVelocity += UnityEngine.Random.insideUnitSphere * 1000f;
			this.tumbling.SetActive(true);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000DCA08 File Offset: 0x000DAE08
		public void OnDrawGizmos()
		{
			if (this.projectiling != null && this.projectiling.active)
			{
				Gizmos.DrawSphere(this.projectile.pos, 0.05f);
				Gizmos.DrawRay(this.projectile.pos, this.projectile.velocity * 0.05f);
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(this.rb.worldCenterOfMass, 0.1f);
			}
		}

		// Token: 0x040022F1 RID: 8945
		[Header("Prefab References")]
		[SerializeField]
		private ReusableParticle arrowSmash;

		// Token: 0x040022F2 RID: 8946
		[SerializeField]
		private ReusableParticle arrowSplash;

		// Token: 0x040022F3 RID: 8947
		public SpriteRenderer spriteRenderer;

		// Token: 0x040022F4 RID: 8948
		private BatchedSprite batchedSprite;

		// Token: 0x040022F5 RID: 8949
		[SerializeField]
		private Sprite[] stampedSprites;

		// Token: 0x040022F6 RID: 8950
		private int bounces;

		// Token: 0x040022F7 RID: 8951
		[Header("Sound")]
		public string attackSound = "Sfx/Juice/Arrow/";

		// Token: 0x040022F8 RID: 8952
		public string bounceSound = "Sfx/Juice/ImpactArrowGroundBounce";

		// Token: 0x040022F9 RID: 8953
		public string stickSound = "Sfx/Juice/ImpactArrowGroundStick";

		// Token: 0x040022FA RID: 8954
		public string stickShieldSound = "Sfx/Juice/ImpactArrowShieldStick";

		// Token: 0x040022FB RID: 8955
		public string smashSound = "Sfx/Juice/ImpactArrowGroundSmash";

		// Token: 0x040022FC RID: 8956
		public string splashSound = "Sfx/Juice/ImpactArrowWater";

		// Token: 0x040022FD RID: 8957
		private float energy = 1f;

		// Token: 0x040022FE RID: 8958
		private float minEnergy = 0.5f;

		// Token: 0x040022FF RID: 8959
		private static RaycastHit[] hits = new RaycastHit[8];
	}
}
