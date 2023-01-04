using System;
using System.Collections.Generic;
using Fabric;
using RTM.OnScreenDebug;
using RTM.Utilities;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x020008AA RID: 2218
	internal class CampaignMapMusic : MonoBehaviour, CampaignManager.INewCampaign, CampaignManager.IExitCampaign
	{
		// Token: 0x060039FB RID: 14843 RVA: 0x000FE36A File Offset: 0x000FC76A
		private void OnEnable()
		{
			this.EnterCampaignMap();
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000FE372 File Offset: 0x000FC772
		private void OnDisable()
		{
			this.ExitCampaignMap();
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000FE37A File Offset: 0x000FC77A
		public void Update()
		{
			if (this.gameOverReason == GameOverReason.None)
			{
				this.UpdateVolumes();
			}
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000FE390 File Offset: 0x000FC790
		private void EnterCampaignMap()
		{
			if (this.gameOverReason == GameOverReason.None)
			{
				FabricWrapper.PostEvent(this.campaignProgression);
				FabricWrapper.PostEvent(this.frontierClose);
				FabricWrapper.PostEvent(this.frontierFar);
				this.prevNoMovesAvailable = this.campaign.Target.heroesAvaliable.active;
				this.prevEndLevelAvailable = !this.campaign.Target.endLevel.IsAvailable();
				this.prevNeutral = (!this.prevNeutral && !this.prevEndLevelAvailable);
				this.UpdateVolumes();
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000FE428 File Offset: 0x000FC828
		private void ExitCampaignMap()
		{
			this.EndAll();
			FabricWrapper.PostEvent(this.gameWin, EventAction.StopSound);
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000FE43D File Offset: 0x000FC83D
		public void OnGameOver(GameOverReason reason)
		{
			this.EndAll();
			this.gameOverReason = reason;
			if (reason != GameOverReason.Won)
			{
				FabricWrapper.PostEvent(this.gameOver);
			}
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000FE460 File Offset: 0x000FC860
		private void EndAll()
		{
			this.prevFrontierProximity = -1f;
			this.prevFrontierNeutral = -1f;
			this.prevFrontierDistance = -1f;
			this.prevProgress = -1f;
			this.prevNeutral = false;
			this.prevNoMovesAvailable = false;
			this.prevEndLevelAvailable = false;
			FabricWrapper.PostEvent(this.campaignMap, EventAction.StopSound);
			FabricWrapper.PostEvent(this.campaignProgression, EventAction.StopSound);
			FabricWrapper.PostEvent(this.frontierClose, EventAction.StopSound);
			FabricWrapper.PostEvent(this.frontierFar, EventAction.StopSound);
			FabricWrapper.PostEvent(this.finalLevel, EventAction.StopSound);
			FabricWrapper.PostEvent(this.outOfMoves, EventAction.StopSound);
			FabricWrapper.PostEvent(this.gameOver, EventAction.StopSound);
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000FE509 File Offset: 0x000FC909
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			this.campaign.Target = campaign;
			this.frontier.Target = campaign.GetComponentInChildren<LineFrontier>();
			this.campaignMapUI.onGameOver += this.OnGameOver;
			this.gameOverReason = GameOverReason.None;
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000FE546 File Offset: 0x000FC946
		void CampaignManager.IExitCampaign.OnCampaignExit(CampaignManager manager, Campaign campaign)
		{
			this.campaign.Target = null;
			this.frontier.Target = null;
			this.campaignMapUI.onGameOver -= this.OnGameOver;
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000FE578 File Offset: 0x000FC978
		private void UpdateVolumes()
		{
			int num = (int)this.frontier.Target.mainLineAnim.target;
			int num2 = 0;
			int num3 = int.MaxValue;
			foreach (LevelNode levelNode in this.campaign.Target.levels)
			{
				if (levelNode.IsAvailable())
				{
					num2 = Mathf.Max(num2, levelNode.frontierDepth - num);
					num3 = Mathf.Min(num3, (int)levelNode.levelState.stepsFromEnd);
				}
			}
			bool flag = !this.campaign.Target.heroesAvaliable.active;
			bool flag2 = this.campaign.Target.endLevel.IsAvailable();
			bool flag3 = !flag && !flag2;
			float num4 = this.GetSafeValue(this.frontierNeutralLevels, num2);
			float num5 = this.GetSafeValue(this.frontierCloseLevels, num2);
			float num6 = this.GetSafeValue(this.frontierDistanceLevels, num2);
			float num7 = 1f - (float)num3 / (float)this.campaign.Target.startLevel.levelState.stepsFromEnd;
			float num8 = (flag || flag2) ? 0f : 1f;
			float num9 = flag2 ? 0f : 1f;
			num4 = this.LerpVolume(this.prevFrontierNeutral, num8 * num4, 0.5f);
			num5 = this.LerpVolume(this.prevFrontierNeutral, num9 * num5, 0.5f);
			num6 = this.LerpVolume(this.prevFrontierDistance, num9 * num6, 0.5f);
			num7 = this.LerpVolume(this.prevProgress, num9 * num7, 0.5f);
			bool flag4 = flag && !flag2;
			if (this.prevNoMovesAvailable != flag4)
			{
				FabricWrapper.PostEvent(this.outOfMoves, (!flag4) ? EventAction.StopSound : EventAction.PlaySound);
				this.prevNoMovesAvailable = flag4;
			}
			if (this.prevEndLevelAvailable != flag2)
			{
				FabricWrapper.PostEvent(this.finalLevel, (!flag2) ? EventAction.StopSound : EventAction.PlaySound);
				this.prevEndLevelAvailable = flag2;
			}
			if (this.prevNeutral != flag3)
			{
				FabricWrapper.PostEvent(this.campaignMap, (!flag3) ? EventAction.StopSound : EventAction.PlaySound);
				this.prevNeutral = flag3;
			}
			if (this.prevFrontierNeutral != num4)
			{
				EventManager.Instance.SetParameter(this.campaignMap, "Intensity", num4, null);
				this.prevFrontierNeutral = num4;
			}
			if (this.prevFrontierProximity != num5)
			{
				EventManager.Instance.SetParameter(this.frontierClose, "Proximity", num5, null);
				this.prevFrontierProximity = num5;
			}
			if (this.prevFrontierDistance != num6)
			{
				EventManager.Instance.SetParameter(this.frontierFar, "Distance", num6, null);
				this.prevFrontierDistance = num6;
			}
			if (this.prevProgress != num7)
			{
				EventManager.Instance.SetParameter(this.campaignProgression, "Progress", num7, null);
				this.prevProgress = num7;
			}
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000FE8BC File Offset: 0x000FCCBC
		private float LerpVolume(float previous, float target, float duration)
		{
			return Mathf.Clamp(Mathf.MoveTowards(previous, target, Time.unscaledDeltaTime / duration), 0f, 1f);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000FE8DC File Offset: 0x000FCCDC
		private float GetSafeValue(List<float> list, int index)
		{
			int count = list.Count;
			return (count <= 0) ? 0f : list[Mathf.Clamp(index, 0, count - 1)];
		}

		// Token: 0x0400280A RID: 10250
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CampaignMapMusic", EVerbosity.Quiet, 100);

		// Token: 0x0400280B RID: 10251
		[SerializeField]
		private CampaignMapUI campaignMapUI;

		// Token: 0x0400280C RID: 10252
		[SerializeField]
		private List<float> frontierNeutralLevels = new List<float>
		{
			0.2f,
			0.8f,
			1f
		};

		// Token: 0x0400280D RID: 10253
		[SerializeField]
		private List<float> frontierCloseLevels = new List<float>
		{
			1f,
			0.5f,
			0f
		};

		// Token: 0x0400280E RID: 10254
		[SerializeField]
		private List<float> frontierDistanceLevels = new List<float>
		{
			0f,
			0f,
			0f,
			0.5f,
			1f
		};

		// Token: 0x0400280F RID: 10255
		private FabricEventReference frontierClose = "Mus/MapEnemyClose";

		// Token: 0x04002810 RID: 10256
		private FabricEventReference frontierFar = "Mus/Ahead";

		// Token: 0x04002811 RID: 10257
		private FabricEventReference campaignMap = "Mus/Campaignmap";

		// Token: 0x04002812 RID: 10258
		private FabricEventReference campaignProgression = "Mus/CampaignmapLate";

		// Token: 0x04002813 RID: 10259
		private FabricEventReference finalLevel = "Mus/LastWorldCampaign";

		// Token: 0x04002814 RID: 10260
		private FabricEventReference outOfMoves = "Mus/MenuOutOfMoves";

		// Token: 0x04002815 RID: 10261
		private FabricEventReference gameOver = "Mus/GameOver";

		// Token: 0x04002816 RID: 10262
		private FabricEventReference gameWin = "Mus/GameWin";

		// Token: 0x04002817 RID: 10263
		private WeakReference<Campaign> campaign = new WeakReference<Campaign>(null);

		// Token: 0x04002818 RID: 10264
		private WeakReference<LineFrontier> frontier = new WeakReference<LineFrontier>(null);

		// Token: 0x04002819 RID: 10265
		private float prevFrontierNeutral = -1f;

		// Token: 0x0400281A RID: 10266
		private float prevFrontierProximity = -1f;

		// Token: 0x0400281B RID: 10267
		private float prevFrontierDistance = -1f;

		// Token: 0x0400281C RID: 10268
		private float prevProgress = -1f;

		// Token: 0x0400281D RID: 10269
		private bool prevNeutral;

		// Token: 0x0400281E RID: 10270
		private bool prevNoMovesAvailable;

		// Token: 0x0400281F RID: 10271
		private bool prevEndLevelAvailable;

		// Token: 0x04002820 RID: 10272
		private GameOverReason gameOverReason;
	}
}
