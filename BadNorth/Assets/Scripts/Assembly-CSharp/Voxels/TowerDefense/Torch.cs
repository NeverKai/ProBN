using System;
using RTM.Utilities;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000801 RID: 2049
	public class Torch : SelfPoolingPrefab
	{
		// Token: 0x060035A5 RID: 13733 RVA: 0x000E6540 File Offset: 0x000E4940
		public void Shoot(Vector3 pos, House target)
		{
			this.house.Target = target;
			Vector3 vector = target.worldTargetPos + UnityEngine.Random.insideUnitSphere.GetZeroY() * 0.1f;
			this.projectile.Launch(pos, vector, this.projectileGravityScale * Physics.gravity.y, this.solver);
			base.transform.position = pos;
			Vector3 zeroY = (vector - pos).GetZeroY();
			this.initialQuat = Quaternion.LookRotation(zeroY, Vector3.up);
			target.TorchThrow(this);
			FabricWrapper.PostEvent(this.throwSound, base.gameObject);
			base.ReturnToParent();
			this.thrown = true;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000E65F0 File Offset: 0x000E49F0
		private void Awake()
		{
			this.solver = base.GetComponent<IProjectileSolver>();
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000E6600 File Offset: 0x000E4A00
		private void Update()
		{
			if (!this.thrown)
			{
				return;
			}
			this.projectile.Update();
			base.transform.position = this.projectile.position;
			base.transform.rotation = this.initialQuat * Quaternion.Euler((Time.time - this.projectile.launchTime) * this.rotationSpeed, 0f, 0f);
			if (this.projectile.hasArrived)
			{
				FabricWrapper.PostEvent(this.impactAudioID, base.gameObject);
				Singleton<CameraShaker>.instance.ShakeOnce(this.cameraShake);
				if (this.impactParticle)
				{
					this.impactParticle.PlayAt(base.transform.position);
				}
				this.house.Target.TorchLand(this);
				base.ReturnToPool();
				this.thrown = false;
			}
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000E66EE File Offset: 0x000E4AEE
		private void OnEnable()
		{
			this.thrown = false;
		}

		// Token: 0x04002466 RID: 9318
		private WeakReference<House> house = new WeakReference<House>(null);

		// Token: 0x04002467 RID: 9319
		private Quaternion initialQuat;

		// Token: 0x04002468 RID: 9320
		private SimpleProjectile projectile = new SimpleProjectile();

		// Token: 0x04002469 RID: 9321
		[Header("Projectile Motion")]
		[SerializeField]
		private float projectileGravityScale = 1.1f;

		// Token: 0x0400246A RID: 9322
		[SerializeField]
		private float rotationSpeed = 500f;

		// Token: 0x0400246B RID: 9323
		[SerializeField]
		private string throwSound = "Sfx/Torch/Throw";

		// Token: 0x0400246C RID: 9324
		[Header("Impact Effects")]
		public ReusableParticle impactParticle;

		// Token: 0x0400246D RID: 9325
		public string impactAudioID = string.Empty;

		// Token: 0x0400246E RID: 9326
		public float cameraShake = 0.25f;

		// Token: 0x0400246F RID: 9327
		private IProjectileSolver solver;

		// Token: 0x04002470 RID: 9328
		private bool thrown;
	}
}
