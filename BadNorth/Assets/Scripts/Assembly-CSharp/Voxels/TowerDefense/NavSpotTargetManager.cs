using System;
using System.Collections.Generic;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense
{
	// Token: 0x0200053C RID: 1340
	public class NavSpotTargetManager : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland, IslandGameplayManager.IWipeIsland
	{
		// Token: 0x060022EC RID: 8940 RVA: 0x00067344 File Offset: 0x00065744
		void IslandGameplayManager.IAwake.OnAwake(IslandGameplayManager manager)
		{
			while (this.available.Count < 12)
			{
				this.available.Push(new NavSpotTargetCache());
			}
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x00067370 File Offset: 0x00065770
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			foreach (NavSpotTargetCache navSpotTargetCache in this.available)
			{
				navSpotTargetCache.Resize(island.navSpotter.navSpots.Count);
			}
			base.StartCoroutine(CoroutineUtils.GenerateTimer(0.4f, this.Process(island)));
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000673F4 File Offset: 0x000657F4
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			base.StopAllCoroutines();
			foreach (Tuple<NavSpotTargetableAbility, NavSpotTargetCache> tuple in this.inUse)
			{
				NavSpotTargetCache item = tuple.item2;
				item.Reset();
				this.available.Push(item);
			}
			this.inUse.Clear();
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x00067474 File Offset: 0x00065874
		public NavSpotTargetCache GetCache(NavSpotTargetableAbility ability)
		{
			NavSpotTargetCache navSpotTargetCache = this.available.Pop();
			this.inUse.Add(new Tuple<NavSpotTargetableAbility, NavSpotTargetCache>(ability, navSpotTargetCache));
			return navSpotTargetCache;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000674A0 File Offset: 0x000658A0
		private IEnumerator<bool> Process(Island island)
		{
			List<NavSpot> navSpots = island.navSpotter.navSpots;
			int numNavSpots = navSpots.Count;
			int idx = 0;
			for (;;)
			{
				if (idx < this.inUse.Count)
				{
					NavSpotTargetableAbility ability = this.inUse[idx].item1;
					NavSpotTargetCache cache = this.inUse[idx].item2;
					FrameTimeStamp startTime = FrameTimeStamp.now;
					for (int i = 0; i < numNavSpots; i++)
					{
						int bestErrorId = -1;
						NavSpot origin = navSpots[i];
						NavSpotTargetCache.Entry entry = cache.entries[i];
						if (entry.errorMessage != null)
						{
							yield return false;
						}
						bool anyTargets = false;
						for (int j = 0; j < numNavSpots; j++)
						{
							int currErrorId = bestErrorId;
							bool canTarget = ability.targeter.IsTargetable(origin, navSpots[j], ref currErrorId);
							entry.targetMask.Set(j, canTarget);
							anyTargets = (anyTargets || canTarget);
							bestErrorId = ((currErrorId <= bestErrorId) ? bestErrorId : currErrorId);
							yield return false;
						}
						string errorMessage = (bestErrorId < 0) ? "UPGRADES/COMMON/TOOLTIPS/NO_TARGETS" : ability.targeter.GetErrorTerm(bestErrorId);
						entry.errorMessage = ((!anyTargets) ? errorMessage : string.Empty);
						cache.entries[i] = entry;
					}
					FrameTimeStamp endTime = FrameTimeStamp.now;
					idx++;
				}
				else
				{
					yield return true;
				}
			}
			yield break;
		}

		// Token: 0x0400154F RID: 5455
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("TargetManager", EVerbosity.Quiet, 0);

		// Token: 0x04001550 RID: 5456
		private const int maxNum = 12;

		// Token: 0x04001551 RID: 5457
		private Stack<NavSpotTargetCache> available = new Stack<NavSpotTargetCache>(12);

		// Token: 0x04001552 RID: 5458
		private List<Tuple<NavSpotTargetableAbility, NavSpotTargetCache>> inUse = new List<Tuple<NavSpotTargetableAbility, NavSpotTargetCache>>(12);
	}
}
