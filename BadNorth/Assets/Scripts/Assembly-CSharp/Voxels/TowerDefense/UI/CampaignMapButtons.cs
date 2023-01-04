using System;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008A7 RID: 2215
	public class CampaignMapButtons : MonoBehaviour, IGameSetup, CampaignManager.INewCampaign
	{
		// Token: 0x060039DF RID: 14815 RVA: 0x000FD6A0 File Offset: 0x000FBAA0
		void IGameSetup.OnGameAwake()
		{
			this.visibility = new AnimatedState("visibility", this.stateRoot.rootState, true, false);
			RectTransform rt = (RectTransform)base.transform;
			CanvasGroup cg = base.GetComponent<CanvasGroup>();
			Action<float> setFunc = delegate(float x)
			{
				rt.pivot = rt.pivot.SetY(Mathf.Lerp(this.hiddenPivotPos, 0f, x));
				cg.alpha = x;
			};
			this.visibility.Subscribe(new Action<bool>(base.gameObject.SetActive), setFunc);
			AnimatedState animatedState = this.visibility;
			animatedState.OnChange = (Action<bool>)Delegate.Combine(animatedState.OnChange, new Action<bool>(delegate(bool a)
			{
				cg.interactable = a;
			}));
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000FD745 File Offset: 0x000FBB45
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			this.UpdateButtonGroup(manager.campaign.campaignSave.wonGame);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000FD75D File Offset: 0x000FBB5D
		public void UpdateButtonGroup(bool gameWon)
		{
			this.normalButtons.SetActive(!gameWon);
			this.wonGameButtons.SetActive(gameWon);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000FD77A File Offset: 0x000FBB7A
		public void SetVisible(bool visible, bool snap = false)
		{
			this.visibility.SetActive(visible);
			if (snap)
			{
				this.visibility.ForceToTarget();
			}
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000FD79A File Offset: 0x000FBB9A
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x040027E7 RID: 10215
		private LerpTowards animParams = new LerpTowards(10f, 5f);

		// Token: 0x040027E8 RID: 10216
		[SerializeField]
		private float hiddenPivotPos = 0.9f;

		// Token: 0x040027E9 RID: 10217
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040027EA RID: 10218
		[SerializeField]
		private GameObject normalButtons;

		// Token: 0x040027EB RID: 10219
		[SerializeField]
		private GameObject wonGameButtons;

		// Token: 0x040027EC RID: 10220
		public UIClickable wonGameMainManu;

		// Token: 0x040027ED RID: 10221
		public UIClickable wonGameUpgrade;

		// Token: 0x040027EE RID: 10222
		private AnimatedState visibility;
	}
}
