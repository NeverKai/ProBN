using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006AD RID: 1709
	public struct Attack
	{
		// Token: 0x06002C26 RID: 11302 RVA: 0x000A3EF4 File Offset: 0x000A22F4
		public Attack(float damage, float knockback, float launchImpulse, Vector3 direction, Vector3 pos, MonoBehaviour monoAttacker, Squad killerSquad, string weapon, ReusableEffect effect = null)
		{
			this.settings = new AttackSettings(damage, knockback, launchImpulse, 1f);
			this.killerSquad = killerSquad;
			this.direction = direction;
			this.pos = pos;
			this.monoAttacker = monoAttacker;
			this.soundPrefix = weapon;
			this.effect = effect;
			this.soundSuffix = "Hit";
			this.ignore = false;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000A3F58 File Offset: 0x000A2358
		public Attack(AttackSettings settings, Vector3 direction, Vector3 pos, MonoBehaviour monoAttacker, Squad killerSquad, string weapon, ReusableEffect effect = null)
		{
			this.settings = settings;
			this.killerSquad = killerSquad;
			this.direction = direction;
			this.pos = pos;
			this.monoAttacker = monoAttacker;
			this.soundPrefix = weapon;
			this.effect = effect;
			this.soundSuffix = "Hit";
			this.ignore = false;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000A3FAC File Offset: 0x000A23AC
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x000A3FB9 File Offset: 0x000A23B9
		public float damage
		{
			get
			{
				return this.settings.damage;
			}
			set
			{
				this.settings.damage = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000A3FC7 File Offset: 0x000A23C7
		// (set) Token: 0x06002C2B RID: 11307 RVA: 0x000A3FD4 File Offset: 0x000A23D4
		public float knockback
		{
			get
			{
				return this.settings.knockback;
			}
			set
			{
				this.settings.knockback = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000A3FE2 File Offset: 0x000A23E2
		// (set) Token: 0x06002C2D RID: 11309 RVA: 0x000A3FEF File Offset: 0x000A23EF
		public float launchImpulse
		{
			get
			{
				return this.settings.launchImpulse;
			}
			set
			{
				this.settings.launchImpulse = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000A3FFD File Offset: 0x000A23FD
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x000A400A File Offset: 0x000A240A
		public float stun
		{
			get
			{
				return this.settings.stun;
			}
			set
			{
				this.settings.stun = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000A4018 File Offset: 0x000A2418
		public string sound
		{
			get
			{
				return string.Format("{0}/{1}", this.soundPrefix, this.soundSuffix);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000A4030 File Offset: 0x000A2430
		public bool hasSound
		{
			get
			{
				return this.soundSuffix != string.Empty && this.soundPrefix != string.Empty;
			}
		}

		// Token: 0x04001CE0 RID: 7392
		public AttackSettings settings;

		// Token: 0x04001CE1 RID: 7393
		public Vector3 direction;

		// Token: 0x04001CE2 RID: 7394
		public MonoBehaviour monoAttacker;

		// Token: 0x04001CE3 RID: 7395
		public Squad killerSquad;

		// Token: 0x04001CE4 RID: 7396
		public Vector3 pos;

		// Token: 0x04001CE5 RID: 7397
		public string soundPrefix;

		// Token: 0x04001CE6 RID: 7398
		public string soundSuffix;

		// Token: 0x04001CE7 RID: 7399
		public bool ignore;

		// Token: 0x04001CE8 RID: 7400
		public ReusableEffect effect;
	}
}
