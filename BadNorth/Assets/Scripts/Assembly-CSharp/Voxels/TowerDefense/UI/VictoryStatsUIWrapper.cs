using System;
using System.Collections;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008B3 RID: 2227
	public class VictoryStatsUIWrapper : MonoBehaviour, IGameSetup, CampaignManager.INewCampaign, CampaignManager.IExitCampaign
	{
		// Token: 0x06003A78 RID: 14968 RVA: 0x0010304A File Offset: 0x0010144A
		void IGameSetup.OnGameAwake()
		{
			base.gameObject.SetActive(false);
			this.rectTransform = (RectTransform)base.transform;
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x00103069 File Offset: 0x00101469
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			this.levelNode.Target = campaign.endLevel;
			this.stats.Clear();
			this.backgroundCanvasGroup.alpha = 0f;
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x001030A3 File Offset: 0x001014A3
		void CampaignManager.IExitCampaign.OnCampaignExit(CampaignManager manager, Campaign campaign)
		{
			this.levelNode.Target = null;
			this.stats.Clear();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x001030C8 File Offset: 0x001014C8
		public IEnumerator ShowStats(CampaignStats stats, GameOverReason reason, float timing)
		{
			base.gameObject.SetActive(true);
			if (reason == GameOverReason.Won)
			{
				this.rectTransform.pivot = this.rectTransform.pivot.SetX(0f);
			}
			else
			{
				this.rectTransform.pivot = this.rectTransform.pivot.SetX(0.5f);
				this.rectTransform.localPosition = this.rectTransform.localPosition.SetX(0f);
				this.levelNode.Target = null;
			}
			if (reason == GameOverReason.Won)
			{
				for (float t = 0f; t < 1f; t += Time.unscaledDeltaTime / 0.3f)
				{
					this.backgroundCanvasGroup.alpha = t;
					yield return null;
				}
				this.backgroundCanvasGroup.alpha = 1f;
			}
			else
			{
				this.backgroundCanvasGroup.alpha = 0f;
			}
			yield return this.stats.ShowStats(stats, reason, timing);
			yield break;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x001030F8 File Offset: 0x001014F8
		private void LateUpdate()
		{
			if (this.levelNode)
			{
				LevelNode target = this.levelNode.Target;
				base.transform.position = base.transform.position.SetX(this.cameraRef.WorldToScreenPoint(target.transform.position + target.outerRadius * Vector3.right * 1.8f).x + 20f);
			}
		}

		// Token: 0x0400287F RID: 10367
		[SerializeField]
		public GameOverStatsUI stats;

		// Token: 0x04002880 RID: 10368
		[SerializeField]
		private Camera cameraRef;

		// Token: 0x04002881 RID: 10369
		[SerializeField]
		private CanvasGroup backgroundCanvasGroup;

		// Token: 0x04002882 RID: 10370
		private RectTransform rectTransform;

		// Token: 0x04002883 RID: 10371
		private WeakReference<LevelNode> levelNode = new WeakReference<LevelNode>(null);
	}
}
