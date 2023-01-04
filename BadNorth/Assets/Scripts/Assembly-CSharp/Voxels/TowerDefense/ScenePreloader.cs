using System;
using System.Collections;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020005F4 RID: 1524
	internal static class ScenePreloader
	{
		// Token: 0x06002755 RID: 10069 RVA: 0x0007F6D0 File Offset: 0x0007DAD0
		public static void Start()
		{
			ScenePreloader.Start(ref ScenePreloader.heroGeneration, "HeroGeneration", true);
			ScenePreloader.Start(ref ScenePreloader.credits, "Credits", true);
			ScenePreloader.Start(ref ScenePreloader.modules, "Modules", false);
			ScenePreloader.Start(ref ScenePreloader.islandGameplay, "IslandGameplay", false);
			ScenePreloader.Start(ref ScenePreloader.upgrades, "Upgrades", false);
			ScenePreloader.Start(ref ScenePreloader.campaignGameplay, "CampaignGameplay", false);
			ScenePreloader.Start(ref ScenePreloader.campaignGeneration, "CampaignGeneration", false);
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0007F750 File Offset: 0x0007DB50
		public static IEnumerator Activate()
		{
			using (new ScopedStopwatch("Activate Scenes", null, 0f))
			{
				if (ScenePreloader.modules != null)
				{
					ScenePreloader.modules.allowSceneActivation = true;
				}
				yield return ScenePreloader.modules;
				if (ScenePreloader.islandGameplay != null)
				{
					ScenePreloader.islandGameplay.allowSceneActivation = true;
				}
				yield return ScenePreloader.islandGameplay;
				if (ScenePreloader.upgrades != null)
				{
					ScenePreloader.upgrades.allowSceneActivation = true;
				}
				yield return ScenePreloader.upgrades;
				if (ScenePreloader.campaignGameplay != null)
				{
					ScenePreloader.campaignGameplay.allowSceneActivation = true;
				}
				yield return ScenePreloader.campaignGameplay;
				if (ScenePreloader.campaignGeneration != null)
				{
					ScenePreloader.campaignGeneration.allowSceneActivation = true;
				}
				yield return ScenePreloader.campaignGeneration;
			}
			yield break;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0007F764 File Offset: 0x0007DB64
		private static void Start(ref AsyncOperation async, string sceneName, bool loadImmediate = false)
		{
			if (async != null)
			{
				return;
			}
			async = ExtraSceneManager.LoadSceneIfNotLoadedAsync(sceneName);
			if (async != null)
			{
				async.allowSceneActivation = loadImmediate;
			}
		}

		// Token: 0x04001935 RID: 6453
		public static AsyncOperation modules;

		// Token: 0x04001936 RID: 6454
		public static AsyncOperation campaignGeneration;

		// Token: 0x04001937 RID: 6455
		public static AsyncOperation campaignGameplay;

		// Token: 0x04001938 RID: 6456
		public static AsyncOperation islandGameplay;

		// Token: 0x04001939 RID: 6457
		public static AsyncOperation heroManagemnt;

		// Token: 0x0400193A RID: 6458
		public static AsyncOperation upgrades;

		// Token: 0x0400193B RID: 6459
		public static AsyncOperation credits;

		// Token: 0x0400193C RID: 6460
		public static AsyncOperation heroGeneration;
	}
}
