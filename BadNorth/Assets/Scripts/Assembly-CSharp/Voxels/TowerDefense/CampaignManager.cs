using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using I2.Loc;
using ReflexCLI.Attributes;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020006D8 RID: 1752
	[ConsoleCommandClassCustomizer("Campaign")]
	public class CampaignManager : Singleton<CampaignManager>
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000ABB75 File Offset: 0x000A9F75
		public Campaign campaign
		{
			get
			{
				return this._campaign;
			}
		}

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06002D57 RID: 11607 RVA: 0x000ABB84 File Offset: 0x000A9F84
		// (remove) Token: 0x06002D58 RID: 11608 RVA: 0x000ABBB8 File Offset: 0x000A9FB8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<CampaignManager, Campaign> onNewCampaign;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06002D59 RID: 11609 RVA: 0x000ABBEC File Offset: 0x000A9FEC
		// (remove) Token: 0x06002D5A RID: 11610 RVA: 0x000ABC20 File Offset: 0x000AA020
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<CampaignManager, Campaign> onExitCampaign;

		// Token: 0x06002D5B RID: 11611 RVA: 0x000ABC54 File Offset: 0x000AA054
		protected override void Awake()
		{
			base.Awake();
			this.newCampaigners = base.GetComponentsInChildren<CampaignManager.INewCampaign>(true);
			this.newCampaignSavers = base.GetComponentsInChildren<CampaignManager.INewCampaignSave>(true);
			this.campaignExiters = base.GetComponentsInChildren<CampaignManager.IExitCampaign>(true);
			Singleton<Stack>.instance.stateCampaign.OnChange += this.campaignContainer.gameObject.SetActive;
			Singleton<Stack>.instance.stateMeta.OnActivate += this.ClearCampaign;
			this.campaignContainer.gameObject.SetActive(false);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000ABCE0 File Offset: 0x000AA0E0
		public void ClearCampaign()
		{
			if (this.campaign)
			{
				this.campaign.campaignSave.Unload();
				foreach (CampaignManager.IExitCampaign exitCampaign in this.campaignExiters)
				{
					exitCampaign.OnCampaignExit(this, this.campaign);
				}
				CampaignManager.onExitCampaign(this, this.campaign);
				Profile.campaign = null;
				if (this._campaign.Target)
				{
					this._campaign.Target.Destroy();
				}
				this._campaign.Target = null;
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000ABD84 File Offset: 0x000AA184
		public static IEnumerator GenerateCampaign(CampaignSave campaignSave, bool campaignIsNew)
		{
			Profile.SaveUserSave();
			IEnumerator routine = ScenePreloader.Activate();
			while (routine.MoveNext())
			{
				yield return null;
			}
			while (!Singleton<CampaignManager>.instance)
			{
				yield return null;
			}
			IEnumerator<object> enumerator = Singleton<CampaignManager>.instance.GenerateCampaignInternal(campaignSave, campaignIsNew);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				yield return obj;
			}
			yield break;
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000ABDA8 File Offset: 0x000AA1A8
		public IEnumerator<object> GenerateCampaignInternal(CampaignSave campaignSave, bool campaignIsNew)
		{
			Localize.ClearLocalizeTargets();
			this.islandGenerator.Clear();
			if (this._campaign.Target)
			{
				UnityEngine.Object.Destroy(this._campaign.Target.gameObject);
			}
			this._campaign = UnityEngine.Object.Instantiate<GameObject>(this.campaignPrefab.gameObject, this.campaignContainer).GetComponent<Campaign>();
			List<IEnumerator<GenInfo>> enumerators = new List<IEnumerator<GenInfo>>();
			if (campaignIsNew)
			{
				enumerators.Add(this.campaign.Create(campaignSave));
				enumerators.AddRange(from x in this.newCampaignSavers
				select x.OnCampaignSaveCreated(campaignSave));
			}
			enumerators.Add(this.campaign.Setup(campaignSave));
			this.campaignContainer.gameObject.SetActive(true);
			foreach (IEnumerator<GenInfo> enumerator in enumerators)
			{
				IEnumerator wrapped = CoroutineUtils.GenerateTimer(20f, enumerator);
				while (wrapped.MoveNext())
				{
					yield return null;
				}
			}
			enumerators = null;
			foreach (CampaignManager.INewCampaign newCampaign in this.newCampaigners)
			{
				newCampaign.OnNewCampaign(this, this.campaign);
			}
			CampaignManager.onNewCampaign(this, this.campaign);
			LoadingScreen.UpdateDescription(ScriptLocalization.LOAD_SCREEN.GENERATING_ISLANDS);
			if (campaignIsNew)
			{
				IEnumerator generationWaiter = this.islandGenerator.GenerateLevelsForCampaignInit();
				while (generationWaiter.MoveNext())
				{
					yield return null;
				}
				this.campaignContainer.gameObject.SetActive(false);
			}
			else if (!this.campaign.trialOver)
			{
				IEnumerator generationWaiter2 = this.islandGenerator.GenerateLevelsForCampaignLoad(this.campaign, 6, 30f);
				while (generationWaiter2.MoveNext())
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000ABDD4 File Offset: 0x000AA1D4
		[ConsoleCommand("")]
		[DebugSetting("Reseed All Levels", "重玩所有关卡", DebugSettingLocation.Campaign)]
		public static void ReseedAllLevels()
		{
			foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
			{
				levelNode.levelState.seed++;
			}
			Profile.SaveCampaign(false);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000ABE4C File Offset: 0x000AA24C
		[ConsoleCommand("")]
		[DebugSetting("Unlock All Levels", "解锁所有关卡", DebugSettingLocation.Campaign)]
		public static void UnlockAllLevels()
		{
			foreach (LevelNode levelNode in Singleton<CampaignManager>.instance.campaign.levels)
			{
				levelNode.levelState.unlocked = true;
				levelNode.unlocked.SetActive(true);
			}
			CampaignCameraController campaignCameraController = UnityEngine.Object.FindObjectOfType<CampaignCameraController>();
			if (campaignCameraController)
			{
				campaignCameraController.UpdateLimits();
			}
			Profile.SaveCampaign(false);
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000ABEE0 File Offset: 0x000AA2E0
		[ConsoleCommand("")]
		public static int seed
		{
			get
			{
				return Singleton<CampaignManager>.instance.campaign.seed;
			}
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000ABEF4 File Offset: 0x000AA2F4
		[ConsoleCommand("")]
		[DebugSetting("Unlock 10 Levels", "解锁10个关卡", DebugSettingLocation.Campaign)]
		public static void UnlockLevels(int count = 10)
		{
			List<LevelNode> list = (from x in Singleton<CampaignManager>.instance.campaign.levels
			where x.IsUnlocked()
			select x).ToList<LevelNode>();
			List<LevelNode> list2 = (from x in Singleton<CampaignManager>.instance.campaign.levels
			where !x.IsUnlocked()
			select x).ToList<LevelNode>();
			for (int i = 0; i < list.Count; i++)
			{
				LevelNode levelNode = list[i];
				foreach (LevelNode levelNode2 in levelNode.connectedLevels)
				{
					if (list2.Remove(levelNode2))
					{
						levelNode2.levelState.unlocked = true;
						list.Add(levelNode2);
						levelNode2.unlocked.SetActive(true);
						count--;
						if (count <= 0)
						{
							break;
						}
					}
				}
				if (count <= 0)
				{
					break;
				}
			}
			CampaignCameraController campaignCameraController = UnityEngine.Object.FindObjectOfType<CampaignCameraController>();
			if (campaignCameraController)
			{
				campaignCameraController.UpdateLimits();
			}
			Profile.SaveCampaign(false);
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000AC048 File Offset: 0x000AA448
		[ConsoleCommand("")]
		private static int progression
		{
			get
			{
				return Singleton<CampaignManager>.instance.campaign.campaignSave.PercentComplete();
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000AC060 File Offset: 0x000AA460
		[DebugSetting("Paint All Islands", DebugSettingLocation.Campaign)]
		[ConsoleCommand("")]
		private static void PaintAllIslands()
		{
			List<LevelNode> levels = Singleton<CampaignManager>.instance.campaign.levels;
			foreach (LevelNode levelNode in levels)
			{
				IslandProxy islandProxy = levelNode.islandProxy;
				Island island = (islandProxy.state != IslandProxy.State.Ready) ? null : islandProxy.island;
				if (island)
				{
					island.painter.DebugPaint();
				}
			}
			Singleton<CampaignManager>.instance.campaign.paintAtlas.SavePixels();
			foreach (LevelNode levelNode2 in levels)
			{
				levelNode2.levelState.playCount = 1;
				levelNode2.played.SetActive(true);
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000AC16C File Offset: 0x000AA56C
		// Note: this type is marked as 'beforefieldinit'.
		static CampaignManager()
		{
			CampaignManager.onNewCampaign = delegate(CampaignManager A_0, Campaign A_1)
			{
			};
			CampaignManager.onExitCampaign = delegate(CampaignManager A_0, Campaign A_1)
			{
			};
		}

		// Token: 0x04001DF3 RID: 7667
		[Header("Level References")]
		public IslandGenerator islandGenerator;

		// Token: 0x04001DF4 RID: 7668
		[SerializeField]
		private Transform campaignContainer;

		// Token: 0x04001DF5 RID: 7669
		[Header("Prefabs")]
		[SerializeField]
		private Campaign campaignPrefab;

		// Token: 0x04001DF6 RID: 7670
		private WeakReference<Campaign> _campaign = new WeakReference<Campaign>(null);

		// Token: 0x04001DF7 RID: 7671
		private CampaignManager.INewCampaign[] newCampaigners;

		// Token: 0x04001DF8 RID: 7672
		private CampaignManager.INewCampaignSave[] newCampaignSavers;

		// Token: 0x04001DF9 RID: 7673
		private CampaignManager.IExitCampaign[] campaignExiters;

		// Token: 0x020006D9 RID: 1753
		public interface INewCampaignSave
		{
			// Token: 0x06002D6A RID: 11626
			IEnumerator<GenInfo> OnCampaignSaveCreated(CampaignSave campaignSave);
		}

		// Token: 0x020006DA RID: 1754
		public interface INewCampaign
		{
			// Token: 0x06002D6B RID: 11627
			void OnNewCampaign(CampaignManager manager, Campaign campaign);
		}

		// Token: 0x020006DB RID: 1755
		public interface IExitCampaign
		{
			// Token: 0x06002D6C RID: 11628
			void OnCampaignExit(CampaignManager manager, Campaign campaign);
		}
	}
}
