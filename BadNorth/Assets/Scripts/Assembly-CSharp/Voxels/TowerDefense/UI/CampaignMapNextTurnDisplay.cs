using I2.Loc;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.ProfileInternals;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008AB RID: 2219
	public class CampaignMapNextTurnDisplay : MonoBehaviour, IGameSetup, CampaignManager.INewCampaign
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000FE985 File Offset: 0x000FCD85
		public bool promptingHeroesAvailable
		{
			get
			{
				return this.state == CampaignMapNextTurnDisplay.State.SomeAvailable;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000FE990 File Offset: 0x000FCD90
		public bool promptingAllFatigued
		{
			get
			{
				return this.state == CampaignMapNextTurnDisplay.State.AllFatigued;
			}
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000FE99B File Offset: 0x000FCD9B
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000FE9A8 File Offset: 0x000FCDA8
		public void UpdateHeroesDisplay()
		{
			this.encourage.active = !this.campaign.Target.campaignSave.HeroesAvailableThisTurn();
			this.wantsHeroDisplayUpdate = true;
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000FE9D4 File Offset: 0x000FCDD4
		void IGameSetup.OnGameAwake()
		{
			this.MaybeInitialize();
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000FE9DC File Offset: 0x000FCDDC
		private void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			CampaignMapUI.nextTurnRotators.Add(this.rotating);
			UnityEngine.Color color = this.backgroundMask.color;
			this.stateRoot = new AgentStateRoot(4);
			float flashAlpha = 0f;
			this.flashAnim = new TargetAnimator("Flash", () => flashAlpha, delegate(float v)
			{
				flashAlpha = v;
			}, this.stateRoot.rootState, new LerpTowards(4f, 1f));
			TargetAnimator targetAnimator = this.flashAnim;
			targetAnimator.setFunc = (Action<float>)Delegate.Combine(targetAnimator.setFunc, new Action<float>(delegate(float a)
			{
				this.flashModifier.alpha = a;
			}));
			CanvasGroup extraInfoCanvasGroup = this.extraInfoTransform.GetComponent<CanvasGroup>();
			CanvasGroup nextTurnCanvasGroup = this.nextTurnTransform.GetComponent<CanvasGroup>();
			CanvasGroup commandersCanvasGroup = this.commandersTransform.GetComponent<CanvasGroup>();
			Canvas componentInParentIncludingInactive = base.gameObject.GetComponentInParentIncludingInactive<Canvas>();
			Action<float> action = delegate(float a)
			{
				float size = 160f;
				if (a > 0f)
				{
					float height = this.extraInfoTransform.rect.height;
					float b = 160f + height;
					size = Mathf.Lerp(160f, b, a);
				}
				this.container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
				float num = Mathf.Clamp01(a * 2f);
				extraInfoCanvasGroup.alpha = num;
				commandersCanvasGroup.alpha = num;
				nextTurnCanvasGroup.alpha = 1f - num;
			};
			this.expand = new AnimatedState("expand", this.stateRoot.rootState, false, false, this.expandAnimParams);
			TargetAnimator<float> anim = this.expand.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, action);
			action(0f);
			this.expand.OnUpdate += delegate()
			{
				if (this.expand.unscaledTimeSinceActivation > 5f)
				{
					this.Close();
				}
			};
			this.clickable = this.buttonTransform.GetComponentInChildren<UIClickable>(true);
			this.clickable.onClickFailed += delegate()
			{
				CampaignSave campaignSave = this.campaign.Target.campaignSave;
				if (campaignSave.GetNumHeroesAvailableThisTurn() == campaignSave.GetNumAvailableHeroes())
				{
					this.Open(CampaignMapNextTurnDisplay.State.AllAvailable);
				}
			};
			this.clickable.onClick += delegate()
			{
				this.encourage.SetActive(false);
			};
			this.buttonAnimator.MaybeInitialize();
			this.buttonAnimator.disabled.anim.Subscribe(delegate(float x)
			{
				this.buttonDisableModifier.alpha = x;
			});
			this.bounceAnimator = new TargetAnimator("PosY", () => this.iconBounce.localPosition.y, delegate(float x)
			{
				this.iconBounce.localPosition = this.iconBounce.localPosition.SetY(x);
			}, this.stateRoot.rootState, LerpTowards.standard);
			this.encourage = new AnimatedState("Encourage", this.stateRoot.rootState, false, false);
			this.encourage.OnUpdate += delegate()
			{
				this.bounceAnimator.SetTargetOrCurrent(CoinGraphic.pulse);
			};
			AnimatedState animatedState = this.encourage;
			animatedState.OnDeactivate = (Action)Delegate.Combine(animatedState.OnDeactivate, new Action(delegate()
			{
				this.bounceAnimator.SetTarget(0f, null, null, null, 0f, null);
			}));
			TargetAnimator<float> anim2 = this.encourage.anim;
			anim2.setFunc = (Action<float>)Delegate.Combine(anim2.setFunc, new Action<float>(delegate(float a)
			{
				this.encourageModifier.alpha = a;
			}));
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000FEC82 File Offset: 0x000FD082
		private void Bounce(float pos)
		{
			this.iconBounce.transform.localPosition = this.iconBounce.transform.localPosition.SetY(pos);
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000FECAA File Offset: 0x000FD0AA
		void CampaignManager.INewCampaign.OnNewCampaign(CampaignManager manager, Campaign campaign)
		{
			this.MaybeInitialize();
			this.campaign.Target = campaign;
			this.UpdateHeroesDisplay();
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000FECC4 File Offset: 0x000FD0C4
		public void PromptHeroesAvailable()
		{
			this.Open(CampaignMapNextTurnDisplay.State.SomeAvailable);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000FECCD File Offset: 0x000FD0CD
		public void PromptAllHeroesFatigued()
		{
			this.Open(CampaignMapNextTurnDisplay.State.AllFatigued);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000FECD8 File Offset: 0x000FD0D8
		private void Open(CampaignMapNextTurnDisplay.State state)
		{
			this.state = state;
			FabricWrapper.PostEvent(this.openAudioId);
			this.Flash();
			this.extraInfoTransform.ForceChildLayoutUpdates(false);
			if (this.wantsHeroDisplayUpdate)
			{
				int numHeroesAvailableThisTurn = this.campaign.Target.campaignSave.GetNumHeroesAvailableThisTurn();
				int numAvailableHeroes = this.campaign.Target.campaignSave.GetNumAvailableHeroes();
				this.heroesAvailableText.text = string.Format("{0}/{1}", numHeroesAvailableThisTurn, numAvailableHeroes);
				this.wantsHeroDisplayUpdate = false;
			}
			this.extraInfoText.Term = ((state != CampaignMapNextTurnDisplay.State.AllAvailable) ? this.nextTurnConfirmTerm : this.nextTurnDenyTerm);
			this.expand.SetActive(false);
			this.expand.SetActive(true);
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000FEDA6 File Offset: 0x000FD1A6
		public void Close()
		{
			if (this.expand.active)
			{
				FabricWrapper.PostEvent(this.closeAudioId);
			}
			this.expand.SetActive(false);
			this.state = CampaignMapNextTurnDisplay.State.Closed;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000FEDD8 File Offset: 0x000FD1D8
		private void OnDisable()
		{
			this.Close();
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000FEDE0 File Offset: 0x000FD1E0
		private void Flash()
		{
			this.encourage.ForceToTarget();
			this.flashAnim.SetCurrent(1f);
			this.flashAnim.SetTarget(0f, null, null, null, 0f, null);
		}

		// Token: 0x04002821 RID: 10273
		[SerializeField]
		private Text heroesAvailableText;

		// Token: 0x04002822 RID: 10274
		[SerializeField]
		private Localize extraInfoText;

		// Token: 0x04002823 RID: 10275
		[SerializeField]
		private RectTransform container;

		// Token: 0x04002824 RID: 10276
		[SerializeField]
		private RectTransform buttonTransform;

		// Token: 0x04002825 RID: 10277
		[SerializeField]
		private RectTransform extraInfoTransform;

		// Token: 0x04002826 RID: 10278
		[SerializeField]
		private RectTransform nextTurnTransform;

		// Token: 0x04002827 RID: 10279
		[SerializeField]
		private RectTransform commandersTransform;

		// Token: 0x04002828 RID: 10280
		[SerializeField]
		private Graphic backgroundMask;

		// Token: 0x04002829 RID: 10281
		[SerializeField]
		private ColorModifier flashModifier;

		// Token: 0x0400282A RID: 10282
		[SerializeField]
		private ColorModifier encourageModifier;

		// Token: 0x0400282B RID: 10283
		[SerializeField]
		private LerpTowards expandAnimParams = new LerpTowards(8f, 1f);

		// Token: 0x0400282C RID: 10284
		[SerializeField]
		private Transform rotating;

		// Token: 0x0400282D RID: 10285
		[SerializeField]
		private Transform iconBounce;

		// Token: 0x0400282E RID: 10286
		private WeakReference<Campaign> campaign = new WeakReference<Campaign>(null);

		// Token: 0x0400282F RID: 10287
		private TargetAnimator flashAnim;

		// Token: 0x04002830 RID: 10288
		private TargetAnimator bounceAnimator;

		// Token: 0x04002831 RID: 10289
		private AnimatedState expand;

		// Token: 0x04002832 RID: 10290
		private AnimatedState encourage;

		// Token: 0x04002833 RID: 10291
		private FabricEventReference openAudioId = "UI/InGame/NotificationOn";

		// Token: 0x04002834 RID: 10292
		private FabricEventReference closeAudioId = "UI/InGame/NotificationOff";

		// Token: 0x04002835 RID: 10293
		private string nextTurnConfirmTerm = "UI/CAMPAIGN/NEXT_TURN/WARNING/DESCRIPTION_2";

		// Token: 0x04002836 RID: 10294
		private string nextTurnDenyTerm = "UI/CAMPAIGN/NEXT_TURN/WARNING/DESCRIPTION_3";

		// Token: 0x04002837 RID: 10295
		[SerializeField]
		private AgentStateRoot stateRoot;

		// Token: 0x04002838 RID: 10296
		[SerializeField]
		private DistanceFieldAnimator buttonAnimator;

		// Token: 0x04002839 RID: 10297
		[SerializeField]
		private ColorModifier buttonDisableModifier;

		// Token: 0x0400283A RID: 10298
		private UIClickable clickable;

		// Token: 0x0400283B RID: 10299
		private CampaignMapNextTurnDisplay.State state;

		// Token: 0x0400283C RID: 10300
		private bool initialized;

		// Token: 0x0400283D RID: 10301
		private bool wantsHeroDisplayUpdate = true;

		// Token: 0x020008AC RID: 2220
		private enum State
		{
			// Token: 0x0400283F RID: 10303
			Closed,
			// Token: 0x04002840 RID: 10304
			AllFatigued,
			// Token: 0x04002841 RID: 10305
			SomeAvailable,
			// Token: 0x04002842 RID: 10306
			AllAvailable
		}
	}
}
