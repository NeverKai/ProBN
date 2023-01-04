using System;
using RTM.OnScreenDebug;
using Voxels.TowerDefense.HeroGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x02000834 RID: 2100
	internal class HeroUpgradeAgentComponent<T> : HeroUpgradeDefinition where T : AgentComponent
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x000EB76D File Offset: 0x000E9B6D
		public override void OnAttachedToMonoHero(MonoHero monoHero, int level)
		{
			base.OnAttachedToMonoHero(monoHero, level);
			monoHero.onMinionPrefabChanged += this.MonoHero_onMinionChanged;
			this.MonoHero_onMinionChanged(monoHero.minionPrefab);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000EB795 File Offset: 0x000E9B95
		private void MonoHero_onMinionChanged(Agent agent)
		{
			this.AddComponent(agent);
		}

		// Token: 0x060036B7 RID: 14007 RVA: 0x000EB79F File Offset: 0x000E9B9F
		public override void OnAppliedToSquad(EnglishSquad squad, int upgradeLevel)
		{
			base.OnAppliedToSquad(squad, upgradeLevel);
			this.AddComponent(squad.heroAgent);
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000EB7B8 File Offset: 0x000E9BB8
		private T AddComponent(Agent agent)
		{
			T orAddComponent = agent.GetOrAddComponent<T>();
			this.OnComponentAdded(agent, orAddComponent);
			return orAddComponent;
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000EB7D5 File Offset: 0x000E9BD5
		protected virtual void OnComponentAdded(Agent agent, T component)
		{
		}

		// Token: 0x0400251D RID: 9501
		protected DebugChannelGroup dbgGroup = new DebugChannelGroup("HeroUpgradeAgentComponent", EVerbosity.Quiet, 0);
	}
}
