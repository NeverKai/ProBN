using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200088F RID: 2191
	[Serializable]
	public class ExplosionDef : ScriptableObject
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x000FB69E File Offset: 0x000F9A9E
		public bool affectsFriendlyUnits
		{
			get
			{
				return this.friendlyDamage > ExplosionDef.FriendlyFire.None || this.friendlyKnockback > ExplosionDef.FriendlyFire.None;
			}
		}

		// Token: 0x04002766 RID: 10086
		[Header("Source")]
		public ExplosionDef.ExplosionSource source = ExplosionDef.ExplosionSource.Other;

		// Token: 0x04002767 RID: 10087
		[Header("Damage")]
		public float damage = 2f;

		// Token: 0x04002768 RID: 10088
		public float damageRadius = 0.5f;

		// Token: 0x04002769 RID: 10089
		public int maxDamagedAgents = 4;

		// Token: 0x0400276A RID: 10090
		[Header("Knockback")]
		public float maxKnockback = 10f;

		// Token: 0x0400276B RID: 10091
		public float knockbackRadius = 2f;

		// Token: 0x0400276C RID: 10092
		public AnimationCurve normalizedKnockbackFalloff = AnimationCurve.Linear(0f, 1f, 1f, 0.2f);

		// Token: 0x0400276D RID: 10093
		[Header("Launching")]
		public float maxLaunch = 10f;

		// Token: 0x0400276E RID: 10094
		public AnimationCurve normalizedLaunchFalloff = AnimationCurve.Linear(0f, 1f, 1f, 0.2f);

		// Token: 0x0400276F RID: 10095
		[Header("Stun")]
		public float maxStun = 1f;

		// Token: 0x04002770 RID: 10096
		public AnimationCurve normalizedStunFalloff = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		// Token: 0x04002771 RID: 10097
		[Header("FriendlyFire")]
		public ExplosionDef.FriendlyFire friendlyDamage = ExplosionDef.FriendlyFire.All;

		// Token: 0x04002772 RID: 10098
		public ExplosionDef.FriendlyFire friendlyKnockback = ExplosionDef.FriendlyFire.All;

		// Token: 0x04002773 RID: 10099
		[Header("Angle")]
		[Range(0f, 180f)]
		public float angleOfEffect = 180f;

		// Token: 0x04002774 RID: 10100
		public float minorRadius;

		// Token: 0x04002775 RID: 10101
		[Header("Effects")]
		public ReusableParticle explosionParticle;

		// Token: 0x04002776 RID: 10102
		public string fabricAudioID = string.Empty;

		// Token: 0x04002777 RID: 10103
		public string fabricAudioAttackID = string.Empty;

		// Token: 0x04002778 RID: 10104
		public float cameraShake = 0.25f;

		// Token: 0x02000890 RID: 2192
		public enum FriendlyFire
		{
			// Token: 0x0400277A RID: 10106
			None,
			// Token: 0x0400277B RID: 10107
			FriendlySquads,
			// Token: 0x0400277C RID: 10108
			OwnSquad,
			// Token: 0x0400277D RID: 10109
			All
		}

		// Token: 0x02000891 RID: 2193
		public enum ExplosionSource
		{
			// Token: 0x0400277F RID: 10111
			Bomb,
			// Token: 0x04002780 RID: 10112
			Plunge,
			// Token: 0x04002781 RID: 10113
			Hammer,
			// Token: 0x04002782 RID: 10114
			House,
			// Token: 0x04002783 RID: 10115
			Other
		}
	}
}
