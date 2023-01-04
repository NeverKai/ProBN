using System;
using RTM.OnScreenDebug;
using UnityEngine;
using Voxels.TowerDefense.Flag;

namespace Voxels.TowerDefense
{
	// Token: 0x0200082E RID: 2094
	internal class HeroTraitGiant : HeroUpgradeDefinition
	{
		// Token: 0x06003699 RID: 13977 RVA: 0x000EB149 File Offset: 0x000E9549
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			this.ModifyHeroAgent(squad.heroAgent, squad.level, squad.hero.dbgName);
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000EB170 File Offset: 0x000E9570
		public override void OnPurchased(HeroDefinition hero, int level)
		{
			base.OnPurchased(hero, level);
			HeroVoice voiceFor = this.GetVoiceFor(hero);
			if (voiceFor)
			{
				hero.voice = voiceFor;
			}
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000EB1A0 File Offset: 0x000E95A0
		public void ModifyHeroAgent(Agent agent, int squadLevel, string heroDbgName)
		{
			float scale = agent.scale;
			agent.scale = this.agentScale[squadLevel];
			agent.hurtSound = this.hurtSound;
			Swordsman component = agent.GetComponent<Swordsman>();
			int i = 0;
			int num = this.attackLevels.Length;
			while (i < num)
			{
				AttackSettings attackSettings = this.attackLevels[i];
				component.damageLevels[i] = attackSettings.damage;
				component.knockbackLevels[i] = attackSettings.knockback;
				component.stunLevels[i] = attackSettings.stun;
				i++;
			}
			Armor component2 = agent.GetComponent<Armor>();
			component2.armor = this.armorLevels;
			Stun component3 = agent.GetComponent<Stun>();
			component3.stunMultiplier = this.stunMultiplierLevels[squadLevel];
			Body body = agent.body;
			Death component4 = agent.GetComponent<Death>();
			agent.GetComponentInChildren<FlagPole>(true).transform.localScale *= scale / agent.scale;
			body.baseMoveSoundRef = this.moveSound;
			component.swordSound = this.swordSoundPrefix;
			component.swingSound = this.swingSound.name;
			component4.deathSound = this.deathSound.name;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000EB2CC File Offset: 0x000E96CC
		public HeroVoice GetVoiceFor(HeroDefinition hero)
		{
			string a = null;
			if (hero.propertyBank.TryGetValue("Gender", ref a))
			{
				return (!(a == "Female")) ? this.maleVoice : this.femaleVoice;
			}
			return null;
		}

		// Token: 0x04002507 RID: 9479
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("HeroTraitGiant", EVerbosity.Minimal, 0);

		// Token: 0x04002508 RID: 9480
		[Space]
		[Header("Giant Settings")]
		[SerializeField]
		private HeroVoice maleVoice;

		// Token: 0x04002509 RID: 9481
		[SerializeField]
		private HeroVoice femaleVoice;

		// Token: 0x0400250A RID: 9482
		[SerializeField]
		private float[] agentScale = new float[]
		{
			1.175f,
			1.2f,
			1.225f,
			1.25f
		};

		// Token: 0x0400250B RID: 9483
		[SerializeField]
		private float[] armorLevels = new float[]
		{
			3f,
			6f,
			8f,
			10f
		};

		// Token: 0x0400250C RID: 9484
		[SerializeField]
		private AttackSettings[] attackLevels;

		// Token: 0x0400250D RID: 9485
		[SerializeField]
		private float[] stunMultiplierLevels = new float[]
		{
			0.8f,
			0.75f,
			0.7f,
			0.65f
		};

		// Token: 0x0400250E RID: 9486
		private string swordSoundPrefix = "Sfx/English/Tank";

		// Token: 0x0400250F RID: 9487
		private FabricEventReference moveSound = "Sfx/English/Tank/Move";

		// Token: 0x04002510 RID: 9488
		private FabricEventReference swingSound = "Sfx/English/Tank/Swing";

		// Token: 0x04002511 RID: 9489
		private FabricEventReference hurtSound = "Sfx/English/Tank/Hurt";

		// Token: 0x04002512 RID: 9490
		private FabricEventReference deathSound = "Sfx/English/Tank/Die";
	}
}
