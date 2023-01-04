using System;
using System.Diagnostics;
using I2.Loc;
using RTM.Utilities;
using TrialVersion;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020005D4 RID: 1492
	public class TrialTurnsRemainDisplay : MonoBehaviour, IGameSetup
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x0007C250 File Offset: 0x0007A650
		void IGameSetup.OnGameAwake()
		{
			TrialTurnsRemainDisplay.instance = this;
			this.visibility = base.GetComponentInChildren<IUIVisibility>(true);
			Color backgroundColor = this.background.color;
			this.flash = new AnimatedState("flash", this.stateRoot.rootState, false, false, new LerpTowards(2f, 0.5f));
			Action<float> action = delegate(float x)
			{
				Color a = (this.turnsRemain >= 0) ? backgroundColor : this.trialOverColor;
				this.background.color = Color.Lerp(a, this.flashColor, x);
			};
			TargetAnimator<float> anim = this.flash.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, action);
			this.flash.anim.SetTarget(0f, null, null, null, 0f, null);
			action(0f);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0007C311 File Offset: 0x0007A711
		[Conditional("TRIAL_VERSION")]
		[Conditional("UPGRADEABLE_TRIAL")]
		public static void SetVisible(bool visible, bool force = false)
		{
			visible &= Utils.IsTrial();
			TrialTurnsRemainDisplay.instance.visibility.SetVisible(visible, force);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0007C32D File Offset: 0x0007A72D
		[Conditional("TRIAL_VERSION")]
		[Conditional("UPGRADEABLE_TRIAL")]
		public static void UpdateDisplay(int turnsRemain, bool doFlash)
		{
			if (Utils.IsTrial())
			{
				TrialTurnsRemainDisplay.instance.UpdateDisplay_Impl(turnsRemain, doFlash);
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0007C348 File Offset: 0x0007A748
		private void UpdateDisplay_Impl(int turnsRemain, bool doFlash)
		{
			this.turnsRemain = turnsRemain;
			turnsRemain = Mathf.Max(turnsRemain, 0);
			this.localizeParams.SetParameterValue("NUM", IntStringCache.GetClean(turnsRemain), true);
			base.transform.ForceChildLayoutUpdates(false);
			this.flash.anim.SetCurrentAndActivate((!doFlash) ? 0f : 1f);
			if (doFlash)
			{
				FabricWrapper.PostEvent(this.flashAudio);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0007C3BF File Offset: 0x0007A7BF
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x040018D9 RID: 6361
		[SerializeField]
		private LocalizationParamsManager localizeParams;

		// Token: 0x040018DA RID: 6362
		[SerializeField]
		private CornerImage background;

		// Token: 0x040018DB RID: 6363
		[SerializeField]
		private Color flashColor = Color.white;

		// Token: 0x040018DC RID: 6364
		[SerializeField]
		private Color trialOverColor = Color.red;

		// Token: 0x040018DD RID: 6365
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x040018DE RID: 6366
		private AnimatedState flash;

		// Token: 0x040018DF RID: 6367
		private static TrialTurnsRemainDisplay instance;

		// Token: 0x040018E0 RID: 6368
		private IUIVisibility visibility;

		// Token: 0x040018E1 RID: 6369
		private FabricEventReference flashAudio = "UI/InGame/NotificationOn";

		// Token: 0x040018E2 RID: 6370
		private int turnsRemain = 100;
	}
}
