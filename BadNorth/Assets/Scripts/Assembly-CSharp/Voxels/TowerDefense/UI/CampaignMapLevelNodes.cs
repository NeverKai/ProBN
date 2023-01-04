using System;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008A9 RID: 2217
	public class CampaignMapLevelNodes : MonoBehaviour, IGameSetup, CampaignManager.INewCampaign, CampaignManager.IExitCampaign
	{
		// Token: 0x060039F1 RID: 14833 RVA: 0x000FDEEC File Offset: 0x000FC2EC
		void IGameSetup.OnGameAwake()
		{
			this.levelNodeProxies = new LocalPool<LevelNodeUIProxy>(base.GetComponentsInChildren<LevelNodeUIProxy>(true), null);
			CampaignMapUI disabledComponentInParent = this.GetDisabledComponentInParent<CampaignMapUI>();
			disabledComponentInParent.onFocusedNavigableChanged += this.OnFocusedNavigableChanged;
			disabledComponentInParent.onGameOver += this.OnGameOver;
			this.cameraController.onLimitsUpdated += this.OnScrollLimitsUpdated;
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000FDF4E File Offset: 0x000FC34E
		private void OnGameOver(GameOverReason reason)
		{
			if (reason != GameOverReason.Won)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000FDF63 File Offset: 0x000FC363
		private void OnScrollLimitsUpdated(float min, float max)
		{
			min += 15f;
			max -= 15f;
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000FDF78 File Offset: 0x000FC378
		private void OnFocusedNavigableChanged(IUINavigable obj)
		{
			LevelNodeUIProxy levelNodeUIProxy = (obj == null) ? null : obj.transform.GetComponent<LevelNodeUIProxy>();
			if (levelNodeUIProxy)
			{
				this.cameraController.SeekTo(levelNodeUIProxy.levelNode, false);
			}
			else
			{
				this.cameraController.CancelSeek();
			}
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000FDFCC File Offset: 0x000FC3CC
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			CampaignMapUI disabledComponentInParent = this.GetDisabledComponentInParent<CampaignMapUI>();
			float magnitude = (this.cameraRef.WorldToScreenPoint(Vector3.right) - this.cameraRef.WorldToScreenPoint(Vector3.zero)).magnitude;
			float num = magnitude / 100f * 1.25f;
			foreach (LevelNode levelNode in campaign.levels)
			{
				LevelNodeUIProxy instance = this.levelNodeProxies.GetInstance();
				instance.Setup(disabledComponentInParent, levelNode, levelNode.outerRadius * num);
				instance.clickable.onStateChanged += this.OnStateChanged;
			}
			this.cameraController.UpdateLimits();
			base.gameObject.SetActive(true);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000FE0B8 File Offset: 0x000FC4B8
		private void OnStateChanged(UIInteractable.State state)
		{
			if (state != UIInteractable.State.None)
			{
				this.upgradesProxy.CloseMenu();
			}
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000FE0CC File Offset: 0x000FC4CC
		public void LateUpdate()
		{
			foreach (LevelNodeUIProxy levelNodeUIProxy in this.levelNodeProxies.inUse)
			{
				if (levelNodeUIProxy.isActiveAndEnabled)
				{
					levelNodeUIProxy.transform.position = this.cameraRef.WorldToScreenPoint(levelNodeUIProxy.levelNode.transform.position);
				}
			}
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000FE158 File Offset: 0x000FC558
		public LevelNodeUIProxy GetProxy(LevelNode levelNode)
		{
			foreach (LevelNodeUIProxy levelNodeUIProxy in this.levelNodeProxies.inUse)
			{
				if (levelNodeUIProxy.levelNode == levelNode)
				{
					return levelNodeUIProxy;
				}
			}
			return null;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000FE1D0 File Offset: 0x000FC5D0
		void CampaignManager.IExitCampaign.OnCampaignExit(CampaignManager manager, Campaign campaign)
		{
			this.levelNodeProxies.ReturnAll();
		}

		// Token: 0x04002805 RID: 10245
		private const float navigableMargin = 15f;

		// Token: 0x04002806 RID: 10246
		[SerializeField]
		private Camera cameraRef;

		// Token: 0x04002807 RID: 10247
		[SerializeField]
		private CampaignCameraController cameraController;

		// Token: 0x04002808 RID: 10248
		[SerializeField]
		private SuperUpgradesCampaignProxy upgradesProxy;

		// Token: 0x04002809 RID: 10249
		private LocalPool<LevelNodeUIProxy> levelNodeProxies;
	}
}
