using System;
using I2.Loc;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008A8 RID: 2216
	public class CampaignMapGameOverHeader : MonoBehaviour
	{
		// Token: 0x060039E5 RID: 14821 RVA: 0x000FD870 File Offset: 0x000FBC70
		public void Init(CampaignMapUI mapUI)
		{
			RectTransform rt = (RectTransform)base.transform;
			this.visibleState = new AnimatedState("Visible", this.rootState.rootState, false, false, this.visibilityAnims);
			TargetAnimator<float> anim = this.visibleState.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				rt.pivot = rt.pivot.SetY(Mathf.Min(a * 1.025f, 1f));
				this.title.pivot = this.title.pivot.SetY(a);
			}));
			AnimatedState animatedState = this.visibleState;
			animatedState.onActivity = (Action<bool>)Delegate.Combine(animatedState.onActivity, new Action<bool>(delegate(bool a)
			{
				this.root.gameObject.SetActive(a);
			}));
			this.dimState = new AnimatedState("Dim", this.rootState.rootState, false, false, this.dimAnims);
			TargetAnimator<float> anim2 = this.dimState.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, new Action<float>(delegate(float a)
			{
				this.dim.alpha = a;
			}));
			AnimatedState animatedState2 = this.dimState;
			animatedState2.onActivity = (Action<bool>)Delegate.Combine(animatedState2.onActivity, new Action<bool>(this.dim.gameObject.SetActive));
			RectTransform reasonRt = (RectTransform)this.reason.transform;
			CanvasGroup reasonCG = this.reason.GetComponent<CanvasGroup>();
			this.reasonState = new AnimatedState("Reason", this.rootState.rootState, false, false, this.reasonAnims);
			TargetAnimator<float> anim3 = this.reasonState.anim;
			anim3.setFunc = (Action<float>)Delegate.Combine(anim3.setFunc, new Action<float>(delegate(float a)
			{
				reasonCG.alpha = a * 3f;
				reasonRt.pivot = reasonRt.pivot.SetY(2f - a);
			}));
			this.reasonLocalizers = this.reason.GetComponentsInChildren<Localize>(true);
			this.continueState = new AnimatedState("Continue", this.rootState.rootState, false, false);
			TargetAnimator<float> anim4 = this.continueState.anim;
			anim4.setFunc = (Action<float>)Delegate.Combine(anim4.setFunc, new Action<float>(delegate(float a)
			{
				this.continueButton.alpha = a;
			}));
			AnimatedState animatedState3 = this.continueState;
			animatedState3.onActivity = (Action<bool>)Delegate.Combine(animatedState3.onActivity, new Action<bool>(delegate(bool a)
			{
				this.continueButton.gameObject.SetActive(a);
			}));
			this.checkpointState = new AnimatedState("Checkpoint", this.rootState.rootState, false, false);
			TargetAnimator<float> anim5 = this.checkpointState.anim;
			anim5.setFunc = (Action<float>)Delegate.Combine(anim5.setFunc, new Action<float>(delegate(float a)
			{
				this.checkpointButton.alpha = a;
			}));
			AnimatedState animatedState4 = this.checkpointState;
			animatedState4.onActivity = (Action<bool>)Delegate.Combine(animatedState4.onActivity, new Action<bool>(delegate(bool a)
			{
				this.checkpointButton.gameObject.SetActive(a);
			}));
			UIClickable componentInChildren = this.checkpointButton.GetComponentInChildren<UIClickable>(true);
			componentInChildren.onClick += delegate()
			{
				this.visibleState.SetActive(false);
				this.dimState.SetActive(false);
				this.reasonState.SetActive(false);
				mapUI.stats.stats.FadeAway();
				mapUI.ReloadCheckpoint(false, 1.1f);
			};
			UIClickable componentInChildren2 = this.continueButton.GetComponentInChildren<UIClickable>();
			componentInChildren2.onClick += delegate()
			{
				MetaMenuHelpers.ExitToMainMenu();
			};
			this.dim.gameObject.SetActive(false);
			this.root.gameObject.SetActive(false);
			this.checkpointButton.gameObject.SetActive(false);
			this.continueButton.gameObject.SetActive(false);
			this.SetActive(false);
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000FDB9C File Offset: 0x000FBF9C
		public void SetReason(GameOverReason reason)
		{
			bool flag = reason == GameOverReason.Won;
			this.victory.SetActive(flag);
			this.gameOver.SetActive(!flag);
			this.visibleState.anim.SetAnimFuncs((!flag) ? this.gameOverAnims : this.visibilityAnims);
			if (!flag)
			{
				foreach (Localize localize in this.reasonLocalizers)
				{
					localize.Term = this.GetReasonTerm(reason);
				}
			}
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000FDC24 File Offset: 0x000FC024
		public void SetActive(bool active)
		{
			if (!active)
			{
				this.Set(this.visibleState, false, true);
				this.Set(this.dimState, false, true);
				this.Set(this.reasonState, false, true);
				this.Set(this.continueState, false, true);
				this.Set(this.checkpointState, false, true);
			}
			this.rootObject.SetActive(active);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000FDC89 File Offset: 0x000FC089
		public void SetButtonsVisible(bool continueButtonVis, bool checkpointButtonVis, bool snap = false)
		{
			this.Set(this.continueState, continueButtonVis, snap);
			this.Set(this.checkpointState, checkpointButtonVis, snap);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000FDCA7 File Offset: 0x000FC0A7
		public void SetVisible(bool visible, bool snap = false)
		{
			this.Set(this.visibleState, visible, snap);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000FDCB7 File Offset: 0x000FC0B7
		public void SetDimVisibility(bool visible, bool snap = false)
		{
			this.Set(this.dimState, visible, snap);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000FDCC7 File Offset: 0x000FC0C7
		public void SetReasonVisibility(bool visible, bool snap = false)
		{
			this.Set(this.reasonState, visible, snap);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000FDCD7 File Offset: 0x000FC0D7
		private void Set(AnimatedState state, bool active, bool snap)
		{
			state.active = active;
			if (snap)
			{
				state.anim.ForceToTarget();
			}
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000FDCF1 File Offset: 0x000FC0F1
		private void Update()
		{
			this.rootState.Update();
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000FDD00 File Offset: 0x000FC100
		private string GetReasonTerm(GameOverReason reason)
		{
			switch (reason)
			{
			case GameOverReason.AllDead:
				return "GAME_OVER/REASON/NO_COMMANDERS";
			case GameOverReason.VikingFrontier:
				return "GAME_OVER/REASON/VIKING_FRONTIER";
			case GameOverReason.NoLevels:
				return "GAME_OVER/REASON/NO_LEVELS";
			default:
				throw new NotImplementedException(string.Format("unknown game over reason {0}", reason));
			}
		}

		// Token: 0x040027EF RID: 10223
		[SerializeField]
		private GameObject rootObject;

		// Token: 0x040027F0 RID: 10224
		[SerializeField]
		public RectTransform sizeIndicator;

		// Token: 0x040027F1 RID: 10225
		[SerializeField]
		private RectTransform root;

		// Token: 0x040027F2 RID: 10226
		[SerializeField]
		private RectTransform title;

		// Token: 0x040027F3 RID: 10227
		[SerializeField]
		private GameObject victory;

		// Token: 0x040027F4 RID: 10228
		[SerializeField]
		private GameObject gameOver;

		// Token: 0x040027F5 RID: 10229
		[SerializeField]
		private GameObject reason;

		// Token: 0x040027F6 RID: 10230
		[SerializeField]
		private CanvasGroup dim;

		// Token: 0x040027F7 RID: 10231
		[SerializeField]
		private CanvasGroup continueButton;

		// Token: 0x040027F8 RID: 10232
		[SerializeField]
		private CanvasGroup checkpointButton;

		// Token: 0x040027F9 RID: 10233
		[SerializeField]
		private LerpTowards visibilityAnims = new LerpTowards(5f, 0.2f);

		// Token: 0x040027FA RID: 10234
		[SerializeField]
		private LerpTowards gameOverAnims = new LerpTowards(5f, 2f);

		// Token: 0x040027FB RID: 10235
		[SerializeField]
		private LerpTowards dimAnims = new LerpTowards(0.75f, 0.15f);

		// Token: 0x040027FC RID: 10236
		[SerializeField]
		private LerpTowards reasonAnims = new LerpTowards(5f, 0.2f);

		// Token: 0x040027FD RID: 10237
		[SerializeField]
		private AgentStateRoot rootState = new AgentStateRoot(4);

		// Token: 0x040027FE RID: 10238
		private AnimatedState visibleState;

		// Token: 0x040027FF RID: 10239
		private AnimatedState dimState;

		// Token: 0x04002800 RID: 10240
		private AnimatedState reasonState;

		// Token: 0x04002801 RID: 10241
		private AnimatedState continueState;

		// Token: 0x04002802 RID: 10242
		private AnimatedState checkpointState;

		// Token: 0x04002803 RID: 10243
		private Localize[] reasonLocalizers;
	}
}
