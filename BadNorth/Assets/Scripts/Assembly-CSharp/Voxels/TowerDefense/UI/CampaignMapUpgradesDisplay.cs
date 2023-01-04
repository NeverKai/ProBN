using System;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008AF RID: 2223
	internal class CampaignMapUpgradesDisplay : MonoBehaviour, IGameSetup, CampaignManager.INewCampaign
	{
		// Token: 0x06003A52 RID: 14930 RVA: 0x0010259F File Offset: 0x0010099F
		public void SetAvailable(bool available, bool snap = false)
		{
			this.availableState.SetActive(available);
			if (snap)
			{
				this.availableState.ForceToTarget();
			}
			else if (available)
			{
				FabricWrapper.PostEvent(this.availableAudio);
			}
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x001025D8 File Offset: 0x001009D8
		void IGameSetup.OnGameAwake()
		{
			this.availableState = new AnimatedState("Available", this.stateRoot.rootState, false, false);
			TargetAnimator<float> anim = this.availableState.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				this.colorModifier.alpha = a;
			}));
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0010262E File Offset: 0x00100A2E
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x00102630 File Offset: 0x00100A30
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x0400286C RID: 10348
		[SerializeField]
		private ColorModifier colorModifier;

		// Token: 0x0400286D RID: 10349
		[SerializeField]
		private AgentStateRoot stateRoot;

		// Token: 0x0400286E RID: 10350
		private AnimatedState availableState;

		// Token: 0x0400286F RID: 10351
		private FabricEventReference availableAudio = "UI/Menu/UpgradeEnabled";
	}
}
