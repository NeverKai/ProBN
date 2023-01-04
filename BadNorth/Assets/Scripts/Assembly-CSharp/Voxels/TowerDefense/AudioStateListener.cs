using System;
using Fabric;
using RTM.OnScreenDebug;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000025 RID: 37
	public class AudioStateListener : Singleton<AudioStateListener>
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000052AB File Offset: 0x000036AB
		private void Start()
		{
			this.BindStates();
			if (Singleton<Stack>.instance.stateMeta.active)
			{
				this.EnterMainMenu();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000052CD File Offset: 0x000036CD
		public void ResumeSound()
		{
			FabricWrapper.PostEvent("Mus/UnPauseGame");
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000052DA File Offset: 0x000036DA
		private void Update()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000052DC File Offset: 0x000036DC
		private void EnterMainMenu()
		{
			FabricWrapper.PostEvent("Mus/Menu");
			EventManager.Instance.SetParameter("Mus/Menu", "ExitGame", 1f, null);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005304 File Offset: 0x00003704
		private void ExitMainMenu()
		{
			FabricWrapper.PostEvent("Mus/Menu", EventAction.StopSound);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005312 File Offset: 0x00003712
		private void EnterCampaignMap()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005314 File Offset: 0x00003714
		private void ExitCampaignMap()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005316 File Offset: 0x00003716
		private void EnterLevel()
		{
			FabricWrapper.PostEvent("Mus/StartGame");
			FabricWrapper.PostEvent("Amb/Seagulls");
			FabricWrapper.PostEvent("Amb/Ocean");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005339 File Offset: 0x00003739
		private void StartingDeployTroops()
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000533B File Offset: 0x0000373B
		private void FinishedDeployTroops()
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000533D File Offset: 0x0000373D
		private void LevelStart()
		{
			FabricWrapper.PostEvent("Amb/Seagulls", EventAction.StopSound);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000534B File Offset: 0x0000374B
		private void LevelComplete()
		{
			FabricWrapper.PostEvent("Amb/Ocean", EventAction.StopSound);
			FabricWrapper.PostEvent("Amb/Wind", EventAction.StopSound);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005365 File Offset: 0x00003765
		private void LevelShutdown()
		{
			FabricWrapper.PostEvent("Amb/Seagulls", EventAction.StopSound);
			FabricWrapper.PostEvent("Amb/Ocean", EventAction.StopSound);
			FabricWrapper.PostEvent("Amb/Wind", EventAction.StopSound);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000538C File Offset: 0x0000378C
		private void OnLevelEnd(Island island, EndOfLevel.Reason reason)
		{
			switch (reason)
			{
			case EndOfLevel.Reason.Won:
				this.LevelWon();
				break;
			case EndOfLevel.Reason.Wiped:
				this.LevelLost();
				break;
			case EndOfLevel.Reason.Fled:
				this.LevelFled();
				break;
			default:
				throw new NotImplementedException(string.Format("Unhandled end of level condition ({0})", reason));
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000053E9 File Offset: 0x000037E9
		private void LevelWon()
		{
			FabricWrapper.PostEvent("Mus/LevelWon");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000053F6 File Offset: 0x000037F6
		private void LevelLost()
		{
			FabricWrapper.PostEvent("Mus/LevelLost");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005403 File Offset: 0x00003803
		private void LevelFled()
		{
			FabricWrapper.PostEvent("Mus/LevelLost");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005410 File Offset: 0x00003810
		private void OnUpgradeMenuChanged(bool isUpgrading)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005414 File Offset: 0x00003814
		private void BindStates()
		{
			Stack instance = Singleton<Stack>.instance;
			instance.stateMeta.OnActivate += this.EnterMainMenu;
			instance.stateMeta.OnDeactivate += this.ExitMainMenu;
			instance.stateCampaign.OnActivate += this.EnterCampaignMap;
			instance.stateCampaign.OnDeactivate += this.ExitCampaignMap;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005484 File Offset: 0x00003884
		public void BindLevelStates(IslandGameplayManager igm)
		{
			igm.states.gameStates.OnActivate += this.EnterLevel;
			igm.states.Spawning.OnActivate += this.StartingDeployTroops;
			igm.states.Spawning.OnDeactivate += this.FinishedDeployTroops;
			igm.states.running.OnActivate += this.LevelStart;
			igm.states.EndOfLevel.OnActivate += this.LevelComplete;
			igm.endOfLevel.preProcess += this.OnLevelEnd;
			igm.states.root.OnDeactivate += this.LevelShutdown;
		}

		// Token: 0x0400004B RID: 75
		[SerializeField]
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("AudioStateListener", EVerbosity.Quiet, 0);
	}
}
