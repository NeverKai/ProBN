using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.Serialization;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x0200082A RID: 2090
	[Serializable]
	public class HeroClassUpgradeDefinition : HeroUpgradeDefinition
	{
		// Token: 0x0600368E RID: 13966 RVA: 0x000EAE8C File Offset: 0x000E928C
		public string GetSquadLevelTerm(int squadLevel)
		{
			squadLevel = Mathf.Clamp(squadLevel, 0, this.squadLevelTerms.Length - 1);
			return this.squadLevelTerms[squadLevel];
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000EAEA9 File Offset: 0x000E92A9
		public override void OnAttachedToMonoHero(MonoHero monoHero, int level)
		{
			base.OnAttachedToMonoHero(monoHero, level);
			monoHero.SetMinionPrefab(this.minionAgentPrefab);
			monoHero.UpdateLevel(level + 1);
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000EAEC8 File Offset: 0x000E92C8
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			squad.level = upgradeLevel + 1;
		}

		// Token: 0x040024FD RID: 9469
		[SerializeField]
		[TermsPopup("")]
		[FormerlySerializedAs("levelTerms")]
		private string[] squadLevelTerms = new string[4];

		// Token: 0x040024FE RID: 9470
		[SerializeField]
		private Agent minionAgentPrefab;
	}
}
