using System;
using ReflexCLI.Attributes;
using RTM.Git;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense
{
	// Token: 0x020005DC RID: 1500
	public class DebugDisplay : MonoBehaviour, IScenePostprocessor, IGameSetup
	{
		// Token: 0x060026EB RID: 9963 RVA: 0x0007D21C File Offset: 0x0007B61C
		void IGameSetup.OnGameAwake()
		{
			DebugDisplay.instance = this;
			this.UpdateSeedText();
			bool flag = true;
			DebugDisplay.Enabled = (PlayerPrefs.GetInt("DebugDisplay", (!flag) ? 0 : 1) != 0);
			DebugDisplay.ShowSafeArea = (PlayerPrefs.GetInt("DisplaySafeArea", 0) != 0);
			this.smoothedDeltaTime = Time.unscaledDeltaTime;
			this.campaignManager = Singleton<CampaignManager>.instance;
			if (!this.campaignManager)
			{
				CampaignManager.onNewCampaign += delegate(CampaignManager m, Campaign c)
				{
					this.campaignManager = Singleton<CampaignManager>.instance;
				};
			}
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x0007D2A6 File Offset: 0x0007B6A6
		private void Start()
		{
			base.transform.ForceChildLayoutUpdates(false);
			base.Invoke("BakeLayouts", 0.3f);
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x0007D2C4 File Offset: 0x0007B6C4
		private void Update()
		{
			if (this.campaignManager && this.campaignManager.campaign)
			{
				int seed = Singleton<CampaignManager>.instance.campaign.seed;
				if (seed != this.campaignSeed || this.prevRenderScale != SmallerRenderCamera.renderScaleInUse)
				{
					this.campaignSeed = seed;
					this.prevRenderScale = SmallerRenderCamera.renderScaleInUse;
					this.UpdateSeedText();
				}
			}
			this.UpdateFPSText();
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0007D340 File Offset: 0x0007B740
		private void UpdateFPSText()
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			int num = Mathf.RoundToInt(1f / unscaledDeltaTime);
			this.fpsRawText.text = this.fpsRawStrings.Get(num);
			this.fpsRawText.color = (((float)num >= 59f) ? Color.white : Color.red);
			float num2 = 0.05f;
			this.smoothedDeltaTime = unscaledDeltaTime * num2 + (1f - num2) * this.smoothedDeltaTime;
			int num3 = Mathf.RoundToInt(1f / this.smoothedDeltaTime);
			this.fpsText.text = this.fpsStrings.Get(num3);
			this.fpsText.color = (((float)num3 >= 59f) ? Color.white : Color.red);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0007D40C File Offset: 0x0007B80C
		private void UpdateSeedText()
		{
			this.seedText.text = string.Format("Campaign Seed: 0x{0:X8} ({0}) MS{1} SCALE{2} DEV:{3}", new object[]
			{
				this.campaignSeed,
				QualitySettings.antiAliasing,
				SmallerRenderCamera.renderScaleInUse,
				(!DeviceCaps.isLowendDevice) ? "HI" : "LO"
			});
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x0007D478 File Offset: 0x0007B878
		// (set) Token: 0x060026F1 RID: 9969 RVA: 0x0007D489 File Offset: 0x0007B889
		[ConsoleCommand("")]
		private static bool Enabled
		{
			get
			{
				return DebugDisplay.instance.textRoot.activeInHierarchy;
			}
			set
			{
				PlayerPrefs.SetInt("DebugDisplay", (!value) ? 0 : 1);
				DebugDisplay.instance.textRoot.SetActive(value);
				if (value)
				{
					DebugDisplay.instance.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0007D4C8 File Offset: 0x0007B8C8
		[DebugSetting("Toggle Debug Display", DebugSettingLocation.All)]
		public static void Toggle()
		{
			DebugDisplay.Enabled = !DebugDisplay.Enabled;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x0007D4D7 File Offset: 0x0007B8D7
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x0007D4E8 File Offset: 0x0007B8E8
		[ConsoleCommand("")]
		private static bool ShowSafeArea
		{
			get
			{
				return DebugDisplay.instance.safeArea.activeInHierarchy;
			}
			set
			{
				DebugDisplay.instance.safeArea.SetActive(value);
				PlayerPrefs.SetInt("DisplaySafeArea", (!value) ? 0 : 1);
			}
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0007D511 File Offset: 0x0007B911
		[DebugSetting("Toggle Safe Area", DebugSettingLocation.All)]
		public static void ToggleSafeArea()
		{
			DebugDisplay.ShowSafeArea = !DebugDisplay.ShowSafeArea;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0007D520 File Offset: 0x0007B920
		[ConsoleCommand("")]
		[DebugSetting("Reset DebugDisplay PlayerPrefs", DebugSettingLocation.All)]
		public static void ResetPlayerPrefs()
		{
			PlayerPrefs.DeleteKey("DebugDisplay");
			PlayerPrefs.DeleteKey("DisplaySafeArea");
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0007D536 File Offset: 0x0007B936
		void IScenePostprocessor.OnPostprocessScene()
		{
			this.commitHashText.text = GitCommands.GetHeadRevisionShortHash();
			this.commitDateText.text = string.Format("{0} (branch: {1})", GitCommands.GetHeadRevisionDate(), GitCommands.GetCurrentBranchName());
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0007D568 File Offset: 0x0007B968
		private void BakeLayouts()
		{
			foreach (LayoutGroup layoutGroup in base.transform.GetComponentsInChildren<LayoutGroup>(true))
			{
				layoutGroup.enabled = false;
			}
			foreach (ContentSizeFitter contentSizeFitter in base.transform.GetComponentsInChildren<ContentSizeFitter>(true))
			{
				contentSizeFitter.enabled = false;
			}
			foreach (LayoutElement layoutElement in base.transform.GetComponentsInChildren<LayoutElement>(true))
			{
				layoutElement.enabled = false;
			}
		}

		// Token: 0x040018ED RID: 6381
		private const string playerPrefsIdDebugDisplay = "DebugDisplay";

		// Token: 0x040018EE RID: 6382
		private const string playerPrefsIdSafeArea = "DisplaySafeArea";

		// Token: 0x040018EF RID: 6383
		private static DebugDisplay instance;

		// Token: 0x040018F0 RID: 6384
		[SerializeField]
		private Text fpsText;

		// Token: 0x040018F1 RID: 6385
		[SerializeField]
		private Text fpsRawText;

		// Token: 0x040018F2 RID: 6386
		[SerializeField]
		private Text seedText;

		// Token: 0x040018F3 RID: 6387
		[SerializeField]
		private Text commitHashText;

		// Token: 0x040018F4 RID: 6388
		[SerializeField]
		private Text commitDateText;

		// Token: 0x040018F5 RID: 6389
		[SerializeField]
		private GameObject textRoot;

		// Token: 0x040018F6 RID: 6390
		[SerializeField]
		private GameObject safeArea;

		// Token: 0x040018F7 RID: 6391
		public int campaignSeed = -1;

		// Token: 0x040018F8 RID: 6392
		public IntStringCache fpsRawStrings = "FPS: {0,4}";

		// Token: 0x040018F9 RID: 6393
		public IntStringCache fpsStrings = " / {0,4}";

		// Token: 0x040018FA RID: 6394
		private CampaignManager campaignManager;

		// Token: 0x040018FB RID: 6395
		private float smoothedDeltaTime;

		// Token: 0x040018FC RID: 6396
		private float prevRenderScale = -1f;

		// Token: 0x040018FD RID: 6397
		private float measureFPSInterval = 0.7f;

		// Token: 0x040018FE RID: 6398
		private float measureFPSTimer;

		// Token: 0x040018FF RID: 6399
		private int numFrames;
	}
}
