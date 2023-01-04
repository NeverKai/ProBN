using System;
using UnityEngine;

namespace Voxels.TowerDefense.Ballistics
{
	// Token: 0x020007C0 RID: 1984
	public struct Projectile
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x000DD30B File Offset: 0x000DB70B
		public Projectile(Vector3 pos, Vector3 velocity, ProjectileSettings settings)
		{
			this.pos = velocity.normalized * settings.startOffset + pos;
			this.velocity = velocity;
			this.settings = settings;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000DD339 File Offset: 0x000DB739
		public Projectile(Vector3 velocity, ProjectileSettings settings)
		{
			this.pos = velocity.normalized * settings.startOffset;
			this.velocity = velocity;
			this.settings = settings;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000DD361 File Offset: 0x000DB761
		public void ApplyVelocity(float dt)
		{
			this.pos += this.velocity * dt;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000DD380 File Offset: 0x000DB780
		public void UpdateVelocity(float dt)
		{
			this.velocity.y = this.velocity.y - this.settings.gravity * Time.fixedDeltaTime;
			this.velocity -= this.settings.drag * this.velocity * Time.fixedDeltaTime;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000DD3E4 File Offset: 0x000DB7E4
		public void Update()
		{
			this.velocity -= Time.fixedDeltaTime * this.velocity * this.settings.drag;
			this.velocity.y = this.velocity.y - this.settings.gravity * Time.fixedDeltaTime;
			this.pos += this.velocity * Time.fixedDeltaTime;
		}

		// Token: 0x04002309 RID: 8969
		public Vector3 pos;

		// Token: 0x0400230A RID: 8970
		public Vector3 velocity;

		// Token: 0x0400230B RID: 8971
		public ProjectileSettings settings;
	}
}
