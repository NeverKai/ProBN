using System;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000831 RID: 2097
	internal class HeroTraitStatsModifier : HeroUpgradeDefinition
	{
		// Token: 0x060036A2 RID: 13986 RVA: 0x000EB478 File Offset: 0x000E9878
		public override void OnAttachedToMonoHero(MonoHero monoHero, int level)
		{
			base.OnAttachedToMonoHero(monoHero, level);
			monoHero.onMinionPrefabChanged += this.MonoHero_onMinionChanged;
			this.MonoHero_onMinionChanged(monoHero.minionPrefab);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000EB4A0 File Offset: 0x000E98A0
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			this.ApplyModifications(squad.heroAgent);
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000EB4B6 File Offset: 0x000E98B6
		private void MonoHero_onMinionChanged(Agent agent)
		{
			this.ApplyModifications(agent);
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000EB4C0 File Offset: 0x000E98C0
		private void ApplyModifications(Agent agent)
		{
			if (!agent)
			{
				return;
			}
			agent.maxSpeed *= this.speedModifier;
			this.ApplyArmourModification(agent.GetComponent<Armor>());
			this.ApplySwordsmanModifications(agent.GetComponent<Swordsman>());
			this.ApplyArcheryModifications(agent.GetComponent<Archery>());
			this.ApplySpearModifications(agent.GetComponent<Spear>());
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000EB51C File Offset: 0x000E991C
		private void ApplyArmourModification(Armor armor)
		{
			if (armor)
			{
				HeroTraitStatsModifier.ModifyFloatList(armor.armor, this.armorMultiplier);
			}
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000EB53C File Offset: 0x000E993C
		private void ApplySwordsmanModifications(Swordsman swordsman)
		{
			if (!swordsman)
			{
				return;
			}
			HeroTraitStatsModifier.ModifyFloatList(swordsman.damageLevels, this.swordsmanAttackMultiplier.damage);
			HeroTraitStatsModifier.ModifyFloatList(swordsman.knockbackLevels, this.swordsmanAttackMultiplier.knockback);
			HeroTraitStatsModifier.ModifyFloatList(swordsman.stunLevels, this.swordsmanAttackMultiplier.stun);
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000EB598 File Offset: 0x000E9998
		private void ApplyArcheryModifications(Archery archery)
		{
			if (!archery)
			{
				return;
			}
			Archery.ArcherySettings[] archerySettings = archery._archerySettings;
			int i = 0;
			int num = archerySettings.Length;
			while (i < num)
			{
				Archery.ArcherySettings[] array = archerySettings;
				int num2 = i;
				array[num2].attackSettings = array[num2].attackSettings * this.archerySettingsMultiplier.attackSettings;
				Archery.ArcherySettings[] array2 = archerySettings;
				int num3 = i;
				array2[num3].cooldown = array2[num3].cooldown * this.archerySettingsMultiplier.cooldown;
				Archery.ArcherySettings[] array3 = archerySettings;
				int num4 = i;
				array3[num4].holdTime = array3[num4].holdTime * this.archerySettingsMultiplier.holdTime;
				Archery.ArcherySettings[] array4 = archerySettings;
				int num5 = i;
				array4[num5].spread = array4[num5].spread * this.archerySettingsMultiplier.spread;
				i++;
			}
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000EB64A File Offset: 0x000E9A4A
		private void ApplySpearModifications(Spear spear)
		{
			if (!spear)
			{
				return;
			}
			HeroTraitStatsModifier.ModifyAttackList(spear.attackSettings, this.spearAttackMultiplier);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000EB66C File Offset: 0x000E9A6C
		private static void ModifyFloatList(List<float> list, float multiplier)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				int index;
				list[index = i] = list[index] * multiplier;
				i++;
			}
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000EB6A8 File Offset: 0x000E9AA8
		private static void ModifyFloatList(float[] list, float multiplier)
		{
			int i = 0;
			int num = list.Length;
			while (i < num)
			{
				list[i] *= multiplier;
				i++;
			}
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000EB6D8 File Offset: 0x000E9AD8
		private static void ModifyAttackList(List<AttackSettings> list, AttackSettings multipliers)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				int index;
				list[index = i] = list[index] * multipliers;
				i++;
			}
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000EB718 File Offset: 0x000E9B18
		private static void ModifyAttackList(AttackSettings[] list, AttackSettings multipliers)
		{
			int i = 0;
			int num = list.Length;
			while (i < num)
			{
				list[i] *= multipliers;
				i++;
			}
		}

		// Token: 0x04002514 RID: 9492
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("HeroTraitStatsModifier", EVerbosity.Quiet, 0);

		// Token: 0x04002515 RID: 9493
		[Space]
		[Header("Common")]
		[SerializeField]
		private float speedModifier = 1f;

		// Token: 0x04002516 RID: 9494
		[SerializeField]
		private float armorMultiplier = 1f;

		// Token: 0x04002517 RID: 9495
		[Header("Swordsman")]
		[SerializeField]
		private AttackSettings swordsmanAttackMultiplier = new AttackSettings(1f, 1f, 1f, 1f);

		// Token: 0x04002518 RID: 9496
		[Header("Archery")]
		[SerializeField]
		private Archery.ArcherySettings archerySettingsMultiplier = new Archery.ArcherySettings
		{
			cooldown = 1f,
			spread = 1f,
			holdTime = 1f,
			attackSettings = new AttackSettings(1f, 1f, 1f, 1f)
		};

		// Token: 0x04002519 RID: 9497
		[Header("Spear")]
		[SerializeField]
		private AttackSettings spearAttackMultiplier = new AttackSettings(1f, 1f, 1f, 1f);
	}
}
