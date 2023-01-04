using System;

namespace Voxels.TowerDefense
{
	// Token: 0x020006AC RID: 1708
	[Serializable]
	public struct AttackSettings
	{
		// Token: 0x06002C21 RID: 11297 RVA: 0x000A3D5D File Offset: 0x000A215D
		public AttackSettings(float damage, float knockback, float launchImpulse, float stun)
		{
			this.damage = damage;
			this.knockback = knockback;
			this.launchImpulse = launchImpulse;
			this.stun = stun;
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000A3D7C File Offset: 0x000A217C
		public static AttackSettings operator +(AttackSettings a, AttackSettings b)
		{
			AttackSettings result = a;
			result.damage += b.damage;
			result.knockback += b.knockback;
			result.launchImpulse += b.launchImpulse;
			result.stun += b.stun;
			return result;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000A3DE0 File Offset: 0x000A21E0
		public static AttackSettings operator -(AttackSettings a, AttackSettings b)
		{
			AttackSettings result = a;
			result.damage -= b.damage;
			result.knockback -= b.knockback;
			result.launchImpulse -= b.launchImpulse;
			result.stun -= b.stun;
			return result;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000A3E44 File Offset: 0x000A2244
		public static AttackSettings operator *(AttackSettings a, AttackSettings b)
		{
			AttackSettings result = a;
			result.damage *= b.damage;
			result.knockback *= b.knockback;
			result.launchImpulse *= b.launchImpulse;
			result.stun *= b.stun;
			return result;
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000A3EA8 File Offset: 0x000A22A8
		public static AttackSettings operator *(AttackSettings a, float b)
		{
			AttackSettings result = a;
			result.damage *= b;
			result.knockback *= b;
			result.launchImpulse *= b;
			result.stun *= b;
			return result;
		}

		// Token: 0x04001CDC RID: 7388
		public float damage;

		// Token: 0x04001CDD RID: 7389
		public float knockback;

		// Token: 0x04001CDE RID: 7390
		public float launchImpulse;

		// Token: 0x04001CDF RID: 7391
		public float stun;
	}
}
