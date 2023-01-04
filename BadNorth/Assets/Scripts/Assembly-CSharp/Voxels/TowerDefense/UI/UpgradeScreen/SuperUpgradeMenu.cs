using System;
using System.Collections.Generic;
using CS.Lights;
using CS.Platform;
using Fabric;
using I2.Loc;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;
using Voxels.TowerDefense.UI.GridScrolling;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000933 RID: 2355
	public class SuperUpgradeMenu : UIMenu, IGameSetup
	{
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x0011D603 File Offset: 0x0011BA03
		// (set) Token: 0x06003F2B RID: 16171 RVA: 0x0011D60A File Offset: 0x0011BA0A
		public static SuperUpgradeMenu instance { get; private set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x0011D612 File Offset: 0x0011BA12
		private AgentState rootState
		{
			get
			{
				return this.stateRoot.rootState;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x0011D61F File Offset: 0x0011BA1F
		// (set) Token: 0x06003F2E RID: 16174 RVA: 0x0011D627 File Offset: 0x0011BA27
		public bool upgradesAvailable { get; private set; }

		// Token: 0x06003F2F RID: 16175 RVA: 0x0011D630 File Offset: 0x0011BA30
		private void MaybeInitialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			SuperUpgradeMenu.instance = this;
			this.heroInfo.gameObject.SetActive(false);
			this.emptySlotInfo.gameObject.SetActive(false);
			this.upgradeInfo.gameObject.SetActive(false);
			this.portraitPool = new LocalPool<SelectableToken>(this.portraitExample, this.portraitExample.transform.parent);
			this.upgradePool = new LocalPool<SelectableToken>(this.upgradeExample, this.upgradeExample.transform.parent);
			this.slotPools.Add(this.slotExampleItem.typeEnum, new LocalPool<SelectableToken>(this.slotExampleItem, this.slotExampleItem.transform.parent));
			this.slotPools.Add(this.slotExampleClass.typeEnum, new LocalPool<SelectableToken>(this.slotExampleClass, this.slotExampleItem.transform.parent));
			this.slotPools.Add(this.slotExampleSkill.typeEnum, new LocalPool<SelectableToken>(this.slotExampleSkill, this.slotExampleItem.transform.parent));
			this.curvePool = new LocalPool<UpgradeCurve>(this.curveExample, this.curveExample.transform.parent);
			this.campaignCoinBankContainer = this.proxy.campaignCoinTransform;
			this.coinBankCanvasGroup = this.coinBank.gameObject.AddComponent<CanvasGroup>();
			this.coinBankCanvasGroup.interactable = false;
			this.coinBankCanvasGroup.blocksRaycasts = false;
			this.bannerColorAnim = new TargetAnimator<Color>(() => this.bannerImage.color, delegate(Color x)
			{
				this.bannerImage.color = x;
			}, this.rootState, new LerpTowardsColor(20f));
			(this.proxy as SuperUpgradesCampaignProxy).campaignMap.onGameOver += delegate(GameOverReason x)
			{
				this.SetGameOver(true);
			};
			Func<float> getFunc = () => this.sliders[0].anchorMax.y;
			RectTransform canvasRt = this.GetDisabledComponentInParent<Canvas>().GetComponent<RectTransform>();
			RectTransform rt = base.transform as RectTransform;
			UiBehaviorDelegates uiBehaviorDelegates = base.gameObject.AddComponent<UiBehaviorDelegates>();
			CanvasGroup bannerCanvasGroup = this.bannerRt.GetComponent<CanvasGroup>();
			Action<float> setFunc = delegate(float x)
			{
				if (Singleton<CampaignManager>.instance && Singleton<CampaignManager>.instance.campaign)
				{
					Singleton<CampaignManager>.instance.campaign.gameObject.SetActive(x < 1f);
				}
				foreach (RectTransform rectTransform in this.sliders)
				{
					rectTransform.anchorMin = rectTransform.anchorMin.SetY(x);
					rectTransform.anchorMax = rectTransform.anchorMax.SetY(x);
				}
				Rect worldSpaceRect = canvasRt.GetWorldSpaceRect();
				float num = canvasRt.TransformPoint(canvasRt.rect.min).y - 10f;
				float y = rt.TransformPoint(rt.rect.min + Vector3.up * this.topBarDown).y;
				float y2 = rt.TransformPoint(rt.rect.max + Vector3.down * this.topBarUp).y;
				float num2 = canvasRt.TransformPoint(canvasRt.rect.max).y + 10f;
				num = this.transform.InverseTransformPoint(new Vector3(0f, num)).y;
				y = this.transform.InverseTransformPoint(new Vector3(0f, y)).y;
				y2 = this.transform.InverseTransformPoint(new Vector3(0f, y2)).y;
				num2 = this.transform.InverseTransformPoint(new Vector3(0f, num2)).y;
				float num3 = Mathf.Lerp(num, y2, x);
				float num4 = Mathf.Lerp(y, num2, x);
				this.topBarRt.sizeDelta = this.topBarRt.sizeDelta.SetY(num4 - num3);
				this.topBarRt.localPosition = this.topBarRt.localPosition.SetY(num4);
				float num5 = Mathf.SmoothStep(0f, 1f, x);
				this.bannerRt.localScale = new Vector3(num5, 1f, 1f);
				bannerCanvasGroup.alpha = num5;
				if (this.IsUpgradingAllowed())
				{
					this.coinBank.position = Vector2.Lerp(this.campaignCoinBankContainer.position, this.localCoinBankContainer.position, x);
				}
				else
				{
					this.coinBankCanvasGroup.alpha = Mathf.Clamp01(x * 3f - 2f);
					this.coinBank.position = new Vector2(this.localCoinBankContainer.position.x, Mathf.Lerp(0f, this.localCoinBankContainer.position.y, x));
				}
			};
			TargetAnimator<float> topAnim = new TargetAnimator<float>(getFunc, setFunc, this.rootState, new LerpTowards(14f, 0.2f));
			topAnim.SetCurrent(0f);
			uiBehaviorDelegates.onRectTransformDimensionsChange += topAnim.Update;
			ScrollGrid scrollGrid = this.portraitGrid;
			scrollGrid.onSetOffsetTarget = (Action<int>)Delegate.Combine(scrollGrid.onSetOffsetTarget, new Action<int>(this.SetCenter));
			Action onTopDown = delegate()
			{
				this.activeItemTokens.Clear();
				this.itemGrid.Clear();
				this.upgradeGrid.Clear();
				this.upgradePool.ReturnAll();
				this.curvePool.ReturnAll();
				this.upgradeTokens.Clear();
				this.UpdateInfo();
				foreach (GameObject gameObject in this.toDeactivate)
				{
					gameObject.SetActive(false);
				}
				if (this.IsUpgradingAllowed())
				{
					this.coinBank.SetParent(this.campaignCoinBankContainer);
					this.coinBank.transform.localScale = Vector3.one;
					this.coinBank.transform.localPosition = Vector3.zero;
				}
			};
			Action onTopUp = delegate()
			{
				this.coinBank.SetParent(this.localCoinBankContainer);
				this.coinBank.transform.localScale = Vector3.one;
				this.coinBank.transform.localPosition = Vector3.zero;
			};
			this.up = new AgentState("Up", this.rootState, false, false);
			AgentState agentState = this.up;
			agentState.OnActivate = (Action)Delegate.Combine(agentState.OnActivate, new Action(delegate()
			{
				this.clickBlocker.SetActive(true);
				if (topAnim.current != 0f)
				{
					onTopDown();
				}
				this.coinBank.SetParent(this.transform);
				this.coinBank.transform.localScale = Vector3.one;
				foreach (SerializableHeroUpgrade definition in this.proxy.inventory)
				{
					UpgradeToken upgradeToken = (this.upgradePool.GetInstance() as UpgradeToken).Setup(this, definition, 0);
					this.itemGrid.AddItem(upgradeToken.scrollItem);
					this.activeItemTokens.Add(upgradeToken);
					this.upgradeTokens.Add(upgradeToken);
				}
				foreach (HeroUpgradeDefinition heroUpgradeDefinition in ResourceList<HeroUpgradeDefinition>.list)
				{
					bool flag = heroUpgradeDefinition.typeEnum == HeroUpgradeTypeEnum.Item || heroUpgradeDefinition.typeEnum == HeroUpgradeTypeEnum.Consumable;
					for (int i = (!flag) ? 0 : 1; i < heroUpgradeDefinition.numLevels; i++)
					{
						UpgradeToken upgradeToken2 = (this.upgradePool.GetInstance() as UpgradeToken).Setup(this, heroUpgradeDefinition, i);
						this.upgradeTokens.Add(upgradeToken2);
						upgradeToken2.visibleState.SetActive(false);
					}
				}
				foreach (GameObject gameObject in this.toDeactivate)
				{
					gameObject.SetActive(true);
				}
				this.portraitGrid.ChangeSetup(this.upGridDef, false, (!this.selectedPortrait) ? null : this.selectedPortrait.scrollItem);
				this.SetDeadHeroVisibile(true);
				topAnim.SetTarget(1f, onTopUp, null, null, 0f, null);
				this.canvasGroup.interactable = true;
				this.RefreshPortraits();
				ScrollGrid scrollGrid2 = this.portraitGrid;
				scrollGrid2.onSetOffsetDragging = (Action<float>)Delegate.Combine(scrollGrid2.onSetOffsetDragging, new Action<float>(this.OnDragging));
				FabricWrapper.PostEvent(this.openAudioId);
				FabricWrapper.PostEvent(this.ambianceAudioId);
				this.upgradeSnapshot.TransitionTo(0.5f);
				this.UpdateInfo();
			}));
			this.coinBank.SetParent(this.campaignCoinBankContainer);
			this.coinBank.transform.localScale = Vector3.one;
			this.coinBank.transform.localPosition = Vector3.zero;
			AgentState agentState2 = this.up;
			agentState2.OnDeactivate = (Action)Delegate.Combine(agentState2.OnDeactivate, new Action(delegate()
			{
				Singleton<CampaignManager>.instance.campaign.gameObject.SetActive(true);
				this.coinBank.SetParent(this.transform);
				this.coinBank.transform.localScale = Vector3.one;
				this.clickBlocker.SetActive(false);
				ScrollGrid scrollGrid2 = this.portraitGrid;
				scrollGrid2.onSetOffsetDragging = (Action<float>)Delegate.Remove(scrollGrid2.onSetOffsetDragging, new Action<float>(this.OnDragging));
				this.curvePool.ReturnAll();
				if (!(this.selected is PortraitToken))
				{
					this.selected = this.focusedPortrait;
				}
				this.canvasGroup.interactable = false;
				this.RefreshPortraits();
				this.SetDeadHeroVisibile(this.proxy.gameOverReason != GameOverReason.None);
				topAnim.SetTarget(0f, onTopDown, null, null, 0f, null);
				this.availablePortraits.Sort();
				this.portraitGrid.ChangeSetup((!this.gameOver) ? this.downGridDef : this.wonGridDef, true, null);
				this.focusedPortrait = null;
				FabricWrapper.PostEvent(this.closeAudioId);
				FabricWrapper.PostEvent(this.ambianceAudioId, EventAction.StopSound);
				this.defaultSnapshot.TransitionTo(0.5f);
				this.CloseMenu();
			}));
			this.canvasGroup.interactable = false;
			onTopDown();
			this.portraitGrid.ChangeSetup(this.downGridDef, false, null);
			this.buttonState = new AgentState("Button", this.up, false, false);
			this.buttonCanvas = this.buttonRt.GetComponentInChildren<CanvasGroup>(true);
			this.buttonClickable = this.buttonRt.GetComponentInChildren<UIClickable>(true);
			this.buttonLoc = this.buttonRt.GetComponentInChildren<Localize>(true);
			this.buttonCoin = this.buttonRt.GetComponentInChildren<CoinGraphic>(true);
			this.buttonAnim = new TargetAnimator(() => this.buttonRt.pivot.y, delegate(float x)
			{
				this.buttonRt.pivot = this.buttonRt.pivot.SetY(x);
			}, this.rootState, new LerpTowards(14f, 0.2f));
			this.buttonAnim.SetCurrent(0f);
			this.buttonClickable.onClick += this.OnButtonClicked;
			Action buttonDeactivate = delegate()
			{
				this.buttonRt.gameObject.SetActive(false);
			};
			AgentState agentState3 = this.buttonState;
			agentState3.OnActivate = (Action)Delegate.Combine(agentState3.OnActivate, new Action(delegate()
			{
				this.buttonCanvas.blocksRaycasts = true;
				this.buttonRt.gameObject.SetActive(true);
				this.buttonAnim.SetTarget(0f, null, null, null, 0f, null);
			}));
			AgentState agentState4 = this.buttonState;
			agentState4.OnDeactivate = (Action)Delegate.Combine(agentState4.OnDeactivate, new Action(delegate()
			{
				this.buttonCanvas.blocksRaycasts = false;
				this.buttonAnim.SetTarget(1f, null, buttonDeactivate, null, 0f, null);
			}));
			this.buttonState.OnDeactivate();
			this.squadLight = base.GetComponent<SquadLight>();
			this.coinBankGraphic.MaybeInitialize();
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x0011DB20 File Offset: 0x0011BF20
		public void SetGameOver(bool isGameOver)
		{
			if (isGameOver != this.gameOver)
			{
				this.gameOver = isGameOver;
				this.portraitGrid.ChangeSetup((!this.gameOver) ? this.downGridDef : this.wonGridDef, true, null);
				this.portraitGrid.ForceAllToTarget();
			}
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x0011DB74 File Offset: 0x0011BF74
		public override bool HandleBackButton()
		{
			if (this.up.active)
			{
				this.up.SetActive(false);
			}
			return true;
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x0011DB94 File Offset: 0x0011BF94
		public void HandleTertiaryAction()
		{
			this.HandleBackButton();
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x0011DB9D File Offset: 0x0011BF9D
		public void HandleMapTab(int direction)
		{
			if (this.portraitGrid.ShiftOffsetTarget(-direction))
			{
				FabricWrapper.PostEvent(this.switchHeroAudioId);
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x0011DBC0 File Offset: 0x0011BFC0
		private void CompareNavigation(SelectableToken token, Vector2 pos, Vector2 navVec, ref SelectableToken bestToken, ref float bestScore)
		{
			if (!token)
			{
				return;
			}
			Vector2 vector = token.transform.position - pos;
			Debug.DrawRay(pos, vector, Color.red, 2f);
			float magnitude = vector.magnitude;
			Vector2 lhs = vector / magnitude;
			float num = Vector2.Dot(lhs, navVec);
			if (num < 0.6f)
			{
				return;
			}
			float num2 = num / magnitude;
			if (num2 > bestScore)
			{
				bestScore = num2;
				bestToken = token;
			}
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0011DC48 File Offset: 0x0011C048
		private void CompareNavigationList<T>(List<T> list, SelectableToken token, Vector2 pos, Vector2 navVec, ref SelectableToken bestToken, ref float bestScore) where T : SelectableToken
		{
			T t = token as T;
			int num = (!t) ? -1 : list.IndexOf(t);
			if (num == -1)
			{
				foreach (T t2 in list)
				{
					this.CompareNavigation(t2, pos, navVec, ref bestToken, ref bestScore);
				}
			}
			if (num > 0)
			{
				this.CompareNavigation(list[num - 1], pos, navVec, ref bestToken, ref bestScore);
			}
			if (num < list.Count - 1)
			{
				this.CompareNavigation(list[num + 1], pos, navVec, ref bestToken, ref bestScore);
			}
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0011DD28 File Offset: 0x0011C128
		public void OnConsumedNavigation(PortraitToken token, Vector2 vector)
		{
			Vector2 pos = token.transform.position;
			vector.Normalize();
			SelectableToken selectableToken = null;
			float num = 0f;
			this.CompareNavigationList<PortraitToken>(this.availablePortraits, token, pos, vector, ref selectableToken, ref num);
			this.CompareNavigationList<SlotToken>(this.activeSlots, token, pos, vector, ref selectableToken, ref num);
			if (selectableToken)
			{
				this.selected = selectableToken;
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0011DDA0 File Offset: 0x0011C1A0
		public void OnConsumedNavigation(SlotToken token, Vector2 vector)
		{
			Vector2 pos = token.transform.position;
			vector.Normalize();
			SelectableToken selectableToken = null;
			float num = 0f;
			this.CompareNavigation(this.focusedPortrait, pos, vector, ref selectableToken, ref num);
			this.CompareNavigationList<SlotToken>(this.activeSlots, token, pos, vector, ref selectableToken, ref num);
			this.CompareNavigationList<UpgradeToken>(this.activeItemTokens, token, pos, vector, ref selectableToken, ref num);
			this.CompareNavigationList<UpgradeToken>(this.activeUpgradeTokens, token, pos, vector, ref selectableToken, ref num);
			if (selectableToken)
			{
				this.selected = selectableToken;
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x0011DE3C File Offset: 0x0011C23C
		public void OnConsumedNavigation(UpgradeToken token, Vector2 vector)
		{
			Vector2 pos = token.transform.position;
			vector.Normalize();
			SelectableToken selectableToken = null;
			float num = 0f;
			bool flag = vector.x == 0f || vector.y == 0f;
			this.CompareNavigation(token.slot, pos, vector, ref selectableToken, ref num);
			if (token.isItem)
			{
				this.CompareNavigationList<UpgradeToken>(this.activeItemTokens, token, pos, vector, ref selectableToken, ref num);
			}
			else
			{
				this.CompareNavigationList<UpgradeToken>(this.activeUpgradeTokens, token, pos, vector, ref selectableToken, ref num);
			}
			if (!flag || !selectableToken)
			{
				this.CompareNavigationList<SlotToken>(this.activeSlots, token, pos, vector, ref selectableToken, ref num);
			}
			if (selectableToken)
			{
				this.selected = selectableToken;
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x0011DF1A File Offset: 0x0011C31A
		private PortraitToken GetCenterPortrait(int offset)
		{
			return this.portraitGrid.GetCenterItem(offset).GetComponent<PortraitToken>();
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0011DF30 File Offset: 0x0011C330
		private void OnDragging(float dragOffset)
		{
			PortraitToken centerPortrait = this.GetCenterPortrait(Mathf.RoundToInt(dragOffset));
			float num = (float)this.portraitGrid.windowMin - dragOffset;
			PortraitToken portraitToken = this.availablePortraits[Mathf.Clamp(Mathf.FloorToInt(num), 0, this.availablePortraits.Count - 1)];
			PortraitToken portraitToken2 = this.availablePortraits[Mathf.Clamp(Mathf.CeilToInt(num), 0, this.availablePortraits.Count - 1)];
			this.bannerColorAnim.SetTargetOrCurrent(Color.Lerp(portraitToken.heroDef.color, portraitToken2.heroDef.color, num % 1f));
			if (this.selectedPortrait)
			{
				this.selectedPortrait = centerPortrait;
			}
			if (centerPortrait != this.focusedPortrait)
			{
				this.focusedPortrait = null;
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0011E000 File Offset: 0x0011C400
		private void SetCenter(int offset)
		{
			if (this.up.active)
			{
				PortraitToken centerPortrait = this.GetCenterPortrait(offset);
				this.focusedPortrait = centerPortrait;
				this.selectedPortrait = centerPortrait;
				if (centerPortrait)
				{
					this.bannerColorAnim.SetTarget(centerPortrait.heroDef.color, null, null, null, 0f, null);
				}
			}
			else if (this.selectedPortrait)
			{
				int indexOf = this.portraitGrid.GetIndexOf(this.selectedPortrait.scrollItem);
				int num = this.portraitGrid.windowMin - offset;
				int num2 = this.portraitGrid.windowMax - offset;
				if (indexOf < num)
				{
					this.selected = this.availablePortraits[num];
				}
				else if (indexOf > num2)
				{
					this.selected = this.availablePortraits[num2];
				}
			}
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0011E0DC File Offset: 0x0011C4DC
		private void StartRemoveToken(UpgradeToken token, List<UpgradeToken> list)
		{
			list.Remove(token);
			token.StartRemove();
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x0011E0EC File Offset: 0x0011C4EC
		private void OnButtonClicked()
		{
			if (this.buttonState.active)
			{
				this.OnClicked(this.selectedUpgradeToken);
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003F3E RID: 16190 RVA: 0x0011E10A File Offset: 0x0011C50A
		// (set) Token: 0x06003F3F RID: 16191 RVA: 0x0011E114 File Offset: 0x0011C514
		private SelectableToken selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				if (value == this._selected)
				{
					return;
				}
				SelectableToken selected = this._selected;
				if (selected)
				{
					selected.selectedState.SetActive(false);
				}
				this._selected = value;
				if (this._selected)
				{
					base.FocusOn(this._selected.navigable);
					this._selected.selectedState.SetActive(true);
					if (this._selected.scrollItem)
					{
						this._selected.scrollItem.MaybeSetFocus();
					}
					this.UpdateInfo();
				}
				if (this.selectedPortrait)
				{
					float num = Time.unscaledTime - this.timeOfLastHeroSelectSound;
					if (num < 0.3f && selected && selected is PortraitToken)
					{
						FabricWrapper.PostEvent(this.switchHeroAudioId);
					}
					else if (this.selectedPortrait.heroDef.alive)
					{
						FabricWrapper.PostEvent(this.selectedPortrait.heroDef.voice.portraitSelectAudio);
					}
					else
					{
						FabricWrapper.PostEvent(this.deadHeroAudioId);
					}
					this.timeOfLastHeroSelectSound = Time.unscaledTime;
				}
				if (this.selectedUpgradeToken)
				{
					HeroDefinition heroDef = this.focusedPortrait.heroDef;
					HeroUpgradeDefinition upgradeDef = this.selectedUpgradeToken.upgradeDef;
					int level = this.selectedUpgradeToken.level;
					bool active;
					int upgradeCost = heroDef.GetUpgradeCost(upgradeDef, level, out active);
					bool flag = this.IsUpgradingAllowed() && upgradeCost <= this.proxy.coinBank;
					bool flag2 = this.selectedUpgradeToken.isItem && !upgradeDef.AvailableFor(heroDef, level);
					string term = this.buyTerm;
					if (this.selectedUpgradeToken.isConsumable)
					{
						term = this.consumeTerm;
					}
					else if (this.selectedUpgradeToken.isItem)
					{
						term = this.eqiupTerm;
					}
					this.buttonLoc.Term = term;
					this.buttonState.SetActive(!flag2 && this.IsUpgradingAllowed());
					this.buttonCoin.number = upgradeCost;
					this.buttonCoin.SetAffordable(flag);
					this.buttonCoin.discount.SetActive(active);
					this.buttonClickable.disabled = (!flag || flag2);
				}
				else
				{
					this.buttonState.SetActive(false);
				}
			}
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x0011E394 File Offset: 0x0011C794
		private void SortSelectable(SelectableToken token, Vector2 pos, ref SelectableToken bestToken, ref float lowestCost)
		{
			if (!token)
			{
				return;
			}
			float sqrMagnitude = (pos - token.transform.position).sqrMagnitude;
			if (sqrMagnitude < lowestCost)
			{
				lowestCost = sqrMagnitude;
				bestToken = token;
			}
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x0011E3DC File Offset: 0x0011C7DC
		private SelectableToken GetTokenClosestTo(Vector2 pos)
		{
			SelectableToken result = null;
			float maxValue = float.MaxValue;
			foreach (UpgradeToken token in this.activeUpgradeTokens)
			{
				this.SortSelectable(token, pos, ref result, ref maxValue);
			}
			foreach (SlotToken token2 in this.activeSlots)
			{
				this.SortSelectable(token2, pos, ref result, ref maxValue);
			}
			foreach (UpgradeToken token3 in this.activeItemTokens)
			{
				this.SortSelectable(token3, pos, ref result, ref maxValue);
			}
			this.SortSelectable(this.focusedPortrait, pos, ref result, ref maxValue);
			return result;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x0011E500 File Offset: 0x0011C900
		private void UpdateInfo()
		{
			if (this.selectedInfo)
			{
				this.selectedInfo.gameObject.SetActive(false);
			}
			if (this.up.active)
			{
				if (this.selectedPortrait)
				{
					this.selectedInfo = this.heroInfo.Setup(this.selectedPortrait.heroDef);
				}
				else if (this.selectedUpgradeToken)
				{
					this.selectedInfo = this.upgradeInfo.Setup(this.selectedUpgradeToken.upgradeDef, this.selectedUpgradeToken.level, true);
				}
				else if (this.selectedSlot)
				{
					if (this.selectedSlot.heroUpgrade == null)
					{
						this.selectedInfo = this.emptySlotInfo.Setup(this.selectedSlot.upgradeType);
					}
					else
					{
						this.selectedInfo = this.upgradeInfo.Setup(this.selectedSlot.heroUpgrade, this.selectedSlot.heroUpgrade.level, false);
					}
				}
				if (this.selectedInfo)
				{
					this.selectedInfo.transform.localPosition = Vector3.zero;
					this.selectedInfo.gameObject.SetActive(true);
					this.selectedInfo.SetupBase();
				}
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x0011E660 File Offset: 0x0011CA60
		// (set) Token: 0x06003F44 RID: 16196 RVA: 0x0011E66D File Offset: 0x0011CA6D
		private PortraitToken selectedPortrait
		{
			get
			{
				return this.selected as PortraitToken;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x0011E676 File Offset: 0x0011CA76
		// (set) Token: 0x06003F46 RID: 16198 RVA: 0x0011E683 File Offset: 0x0011CA83
		private UpgradeToken selectedUpgradeToken
		{
			get
			{
				return this.selected as UpgradeToken;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x0011E68C File Offset: 0x0011CA8C
		// (set) Token: 0x06003F48 RID: 16200 RVA: 0x0011E699 File Offset: 0x0011CA99
		private SlotToken selectedSlot
		{
			get
			{
				return this.selected as SlotToken;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x0011E6A2 File Offset: 0x0011CAA2
		// (set) Token: 0x06003F4A RID: 16202 RVA: 0x0011E6AC File Offset: 0x0011CAAC
		private PortraitToken focusedPortrait
		{
			get
			{
				return this._focusedPortrait;
			}
			set
			{
				if (this._focusedPortrait == value)
				{
					return;
				}
				if (this._focusedPortrait)
				{
					if (this.skillSlot)
					{
						this.skillSlot.StartRemove();
					}
					if (this.itemSlot)
					{
						this.itemSlot.StartRemove();
					}
					if (this.classSlot)
					{
						this.classSlot.StartRemove();
					}
					this.itemSlot = null;
					this.classSlot = null;
					this.skillSlot = null;
				}
				this._focusedPortrait = value;
				if (this._focusedPortrait)
				{
					this.portraitGrid.SetFocus(this._focusedPortrait.scrollItem);
					this.bannerColorAnim.SetTarget(this._focusedPortrait.heroDef.color, null, null, null, 0f, null);
					this.UpdateFocusedHero();
				}
				else
				{
					this.ClearUpgradeGrid();
				}
			}
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x0011E7A4 File Offset: 0x0011CBA4
		private void ClearUpgradeGrid()
		{
			this.upgradeGrid.Clear();
			foreach (UpgradeToken upgradeToken in this.upgradeTokens)
			{
				upgradeToken.SetAvailable(false);
				if (!upgradeToken.isItem && upgradeToken.visibleState.active)
				{
					upgradeToken.slot = null;
					upgradeToken.scrollItem.Uproot();
					upgradeToken.visibleState.SetActive(false);
				}
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0011E848 File Offset: 0x0011CC48
		private void UpdateFocusedHero()
		{
			HeroDefinition heroDef = this.focusedPortrait.heroDef;
			this.ClearUpgradeGrid();
			this.activeSlots.Clear();
			this.activeUpgradeTokens.Clear();
			this.itemSlot = this.SetupSlot(this.focusedPortrait, this.itemSlot, HeroUpgradeTypeEnum.Item);
			this.classSlot = this.SetupSlot(this.focusedPortrait, this.classSlot, HeroUpgradeTypeEnum.Class);
			if (heroDef.classUpgrade != null)
			{
				this.skillSlot = this.SetupSlot(this.focusedPortrait, this.skillSlot, HeroUpgradeTypeEnum.Skill);
			}
			if (this.selectedUpgradeToken && !this.selectedUpgradeToken.visibleState.active)
			{
				this.selected = this.focusedPortrait;
			}
			this.squadLight.SetActive(true);
			this.squadLight.SetSquadColour(heroDef.color);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x0011E924 File Offset: 0x0011CD24
		private SlotToken SetupSlot(PortraitToken portrait, SlotToken slot, HeroUpgradeTypeEnum typeEnum)
		{
			if (slot)
			{
				slot.UpdateUpgrade();
			}
			else
			{
				slot = (this.slotPools[typeEnum].GetInstance() as SlotToken).Setup(this, portrait);
			}
			this.activeSlots.Add(slot);
			slot.avaliableUpgradeTokens.Clear();
			foreach (UpgradeToken upgradeToken in this.upgradeTokens)
			{
				if (slot.upgradeTypes.Contains(upgradeToken.upgradeDef.upgradeType) && upgradeToken.upgradeDef.AvailableFor(portrait.heroDef, upgradeToken.level) && this.IsUpgradingAllowed())
				{
					upgradeToken.visibleState.SetActive(true);
					upgradeToken.slot = slot;
					if (slot.typeEnum == upgradeToken.upgradeDef.typeEnum)
					{
						bool flag = false;
						for (int i = 0; i < upgradeToken.curves.Count; i++)
						{
							if (upgradeToken.curves[i].slot == slot)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							this.curvePool.GetInstance().Setup(upgradeToken, slot);
						}
					}
					bool active;
					int upgradeCost = portrait.heroDef.GetUpgradeCost(upgradeToken.upgradeDef, upgradeToken.level, out active);
					upgradeToken.coin.number = upgradeCost;
					upgradeToken.coin.discount.SetActive(active);
					upgradeToken.SetAvailable(this.proxy.coinBank >= upgradeCost && this.IsUpgradingAllowed());
					if (!upgradeToken.scrollItem.grid && !upgradeToken.isItem)
					{
						this.activeUpgradeTokens.Add(upgradeToken);
						this.upgradeGrid.AddItem(upgradeToken.scrollItem);
					}
					slot.avaliableUpgradeTokens.Add(upgradeToken);
				}
			}
			return slot;
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x0011EB40 File Offset: 0x0011CF40
		void IGameSetup.OnGameAwake()
		{
			this.clickBlocker.SetActive(false);
			if (Singleton<Stack>.instance)
			{
				GameObject gameObject = base.transform.root.gameObject;
				gameObject.SetActive(false);
				Singleton<Stack>.instance.stateCampaign.OnChange += gameObject.SetActive;
			}
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x0011EB9C File Offset: 0x0011CF9C
		public void OnNewCampaign(IUpgradesProxy proxy)
		{
			this.proxy = proxy;
			this.SetGameOver(false);
			this.MaybeInitialize();
			this.SetupHeroes();
			this.portraitGrid.ForceAllToTarget();
			this.UpdateCoinBank();
			this.coinBank.SetParent(this.campaignCoinBankContainer);
			this.coinBank.localPosition = Vector3.zero;
			this.coinBank.localScale = Vector3.one;
			this.coinBankCanvasGroup.alpha = 1f;
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x0011EC18 File Offset: 0x0011D018
		private void SetupHeroes()
		{
			foreach (HeroDefinition heroDefinition in this.proxy.heroes)
			{
				PortraitToken portraitToken = (this.portraitPool.GetInstance() as PortraitToken).Setup(heroDefinition, this);
				this.allPortraits.Add(portraitToken);
				if (heroDefinition.recruited && (heroDefinition.alive || this.gameOver))
				{
					portraitToken.visibleState.SetActive(true);
					this.portraitGrid.AddItem(portraitToken.scrollItem);
					this.availablePortraits.Add(portraitToken);
				}
				else
				{
					portraitToken.visibleState.SetActive(false);
					portraitToken.visibleState.ForceToTarget();
				}
			}
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x0011ED00 File Offset: 0x0011D100
		public void RefreshHeroDefs()
		{
			this.portraitPool.ReturnAll();
			this.availablePortraits.Clear();
			this.allPortraits.Clear();
			this.SetupHeroes();
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x0011ED2C File Offset: 0x0011D12C
		public void UpdateCoinBank()
		{
			if (this.proxy.gameOverReason != GameOverReason.None && this.proxy.gameOverReason != GameOverReason.Won)
			{
				this.coinBankGraphic.visible.SetActive(false);
				return;
			}
			int number = this.coinBankGraphic.number;
			int num = this.proxy.coinBank;
			bool flag = false;
			this.upgradesAvailable = false;
			bool flag2 = this.IsUpgradingAllowed();
			foreach (PortraitToken portraitToken in this.allPortraits)
			{
				HeroDefinition heroDef = portraitToken.heroDef;
				bool flag3 = false;
				if (flag2 && heroDef.recruited)
				{
					flag = (flag3 = (heroDef.alive && heroDef.GetCheapestAvailableUpgradeCost() <= num));
					if (!flag3)
					{
						foreach (SerializableHeroUpgrade serializableHeroUpgrade in this.proxy.inventory)
						{
							if (serializableHeroUpgrade.definition.AvailableFor(heroDef, 0))
							{
								flag3 = true;
								break;
							}
						}
					}
				}
				portraitToken.upgradesAvailable.SetActive(flag3);
				if (flag3)
				{
					this.upgradesAvailable = true;
				}
			}
			this.coinBankGraphic.bouncy.SetActive(flag);
			this.coinBankGraphic.unaffordable.SetActive(!flag);
			this.coinBankGraphic.number = num;
			this.coinBankGraphic.visible.SetActive(true);
			if (number != num)
			{
				this.coinBankGraphic.PulseHighlight();
			}
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0011EF00 File Offset: 0x0011D300
		private void UpdateHeroUpgradeability(PortraitToken portrait, bool upgradesAllowed, int coins)
		{
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0011EF02 File Offset: 0x0011D302
		public void OnCampaignExit()
		{
			this.proxy = null;
			this.portraitPool.ReturnAll();
			this.availablePortraits.Clear();
			this.allPortraits.Clear();
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0011EF2C File Offset: 0x0011D32C
		public override void OpenMenu()
		{
			if (this.availablePortraits.Count > 0)
			{
				PortraitToken upAndSelect = null;
				if (this.proxy.IsUpgradingAllowed())
				{
					int num = 0;
					foreach (SelectableToken selectableToken in this.portraitPool.inUse)
					{
						PortraitToken portraitToken = (PortraitToken)selectableToken;
						HeroDefinition heroDef = portraitToken.heroDef;
						if (heroDef.alive && heroDef.recruited)
						{
							int mostExpensiveAvailableUpgradeCost = heroDef.GetMostExpensiveAvailableUpgradeCost(this.proxy.coinBank);
							if (mostExpensiveAvailableUpgradeCost > num)
							{
								num = mostExpensiveAvailableUpgradeCost;
								upAndSelect = portraitToken;
							}
						}
					}
				}
				this.SetUpAndSelect(upAndSelect);
			}
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x0011F000 File Offset: 0x0011D400
		public void OpenMenu(PortraitToken selectedPortrait)
		{
			this.SetUpAndSelect(selectedPortrait);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0011F00C File Offset: 0x0011D40C
		public override void CloseMenu()
		{
			this.focusedPortrait = null;
			this.selectedPortrait = null;
			this.up.SetActive(false);
			this.proxy.OnMenuClosed();
			this.clickBlocker.SetActive(false);
			base.CloseMenu();
			this.squadLight.SetActive(false);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0011F05D File Offset: 0x0011D45D
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0011F06A File Offset: 0x0011D46A
		[ContextMenu("Toggle")]
		public void Toggle()
		{
			this.up.SetActive(!this.up.active);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0011F086 File Offset: 0x0011D486
		public void OnClicked(PortraitToken portrait)
		{
			if (this.up.active)
			{
				if (this.selectedPortrait != portrait)
				{
					this.selectedPortrait = portrait;
					this.focusedPortrait = portrait;
				}
			}
			else
			{
				this.SetUpAndSelect(portrait);
			}
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0011F0C4 File Offset: 0x0011D4C4
		private void SetUpAndSelect(PortraitToken portrait)
		{
			this.selectedPortrait = portrait;
			this.up.SetActive(true);
			this.bannerColorAnim.ForceToTarget();
			if (!base.isOpen)
			{
				this.proxy.OnMenuOpened();
				base.OpenMenu();
				FabricWrapper.PostEvent(this.focusedPortrait.navigable.focusAudio);
			}
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x0011F124 File Offset: 0x0011D524
		public void OnClicked(SlotToken slot)
		{
			if (this.selectedSlot != slot)
			{
				this.selected = slot;
			}
			else if (slot.avaliableUpgradeTokens.Count > 0)
			{
				this.selected = slot.avaliableUpgradeTokens[0];
			}
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x0011F174 File Offset: 0x0011D574
		public void OnClicked(UpgradeToken upgradeToken)
		{
			if (this.selectedUpgradeToken != upgradeToken)
			{
				this.selected = upgradeToken;
			}
			else
			{
				HeroDefinition heroDef = this.focusedPortrait.heroDef;
				HeroUpgradeDefinition upgradeDef = upgradeToken.upgradeDef;
				int level = upgradeToken.level;
				int num = this.proxy.coinBank;
				if (!upgradeDef.AvailableFor(heroDef, level) || !this.IsUpgradingAllowed())
				{
					FabricWrapper.PostEvent(FabricID.uiError);
				}
				else if (heroDef.TryPurchase(upgradeDef, level, ref num))
				{
					this.proxy.coinBank = num;
					this.UpdateCoinBank();
					Profile.userSave.inventory.Add(upgradeDef, level, false);
					BasePlatformManager.Instance.UnlockAchievement("ACHIEVEMENT_READY_FOR_BATTLE");
					this.focusedPortrait.RefreshVisuals();
					SlotToken slot = upgradeToken.slot;
					if (slot)
					{
						this.selected = slot;
						slot.PlayFlair();
					}
					else
					{
						this.selected = this.selectedPortrait;
					}
					if (upgradeToken.isItem)
					{
						slot = this.itemSlot;
						upgradeToken.StartRemove();
						for (int i = slot.curves.Count - 1; i >= 0; i--)
						{
							this.curvePool.ReturnToPool(slot.curves[i]);
						}
						this.upgradeTokens.Remove(upgradeToken);
						this.activeItemTokens.Remove(upgradeToken);
						this.proxy.RemoveFromInventory(upgradeDef);
					}
					this.UpdateFocusedHero();
					this.proxy.OnUpgradePurchased();
					FabricWrapper.PostEvent(upgradeDef.uiPurchaseAudioId);
					FabricWrapper.PostEvent(this.purchaseFlairAudioId);
				}
				else
				{
					FabricWrapper.PostEvent(this.insufficientFundsAudioId);
				}
			}
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x0011F322 File Offset: 0x0011D722
		public void CampaignMapAnimating(bool isAnimating)
		{
			this.blockingInput = isAnimating;
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x0011F32C File Offset: 0x0011D72C
		public bool SetDeadHeroVisibile(bool visible)
		{
			bool result = false;
			foreach (PortraitToken portraitToken in this.allPortraits)
			{
				if (portraitToken.heroDef.recruited && !portraitToken.heroDef.alive && this.SetPortraitVisible(portraitToken, visible))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0011F3B4 File Offset: 0x0011D7B4
		public bool RevealRecruitedHeroes()
		{
			bool result = false;
			foreach (PortraitToken portraitToken in this.allPortraits)
			{
				if (portraitToken.heroDef.recruited && portraitToken.heroDef.alive && this.SetPortraitVisible(portraitToken, true))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0011F43C File Offset: 0x0011D83C
		private bool SetPortraitVisible(PortraitToken portrait, bool visible)
		{
			if (visible == portrait.visibleState.active)
			{
				return false;
			}
			if (visible)
			{
				portrait.visibleState.SetActive(true);
				this.portraitGrid.AddItem(portrait.scrollItem);
				portrait.scrollItem.moveAnim.ForceToTarget();
				this.availablePortraits.Add(portrait);
			}
			else
			{
				this.portraitGrid.RemoveItem(portrait.scrollItem);
				portrait.StartRemove();
				this.availablePortraits.Remove(portrait);
			}
			return true;
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x0011F4C6 File Offset: 0x0011D8C6
		public void NextTurnAnimation()
		{
			this.RefreshPortraits();
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0011F4D0 File Offset: 0x0011D8D0
		public void RefreshPortraits()
		{
			foreach (PortraitToken portraitToken in this.allPortraits)
			{
				portraitToken.RefreshVisuals();
			}
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x0011F52C File Offset: 0x0011D92C
		public PortraitToken GetPortraitForHero(HeroDefinition heroDef)
		{
			foreach (SelectableToken selectableToken in this.portraitPool.inUse)
			{
				PortraitToken portraitToken = (PortraitToken)selectableToken;
				if (portraitToken.heroDef == heroDef)
				{
					return portraitToken;
				}
			}
			return null;
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0011F5A4 File Offset: 0x0011D9A4
		public bool IsUpgradingAllowed()
		{
			return this.proxy != null && this.proxy.IsUpgradingAllowed();
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0011F5C0 File Offset: 0x0011D9C0
		private void OnDrawGizmos()
		{
			Gizmos.matrix = base.transform.localToWorldMatrix;
			Rect rect = (base.transform as RectTransform).rect;
			Gizmos.DrawRay(rect.min + Vector2.up * this.topBarDown, new Vector2(rect.width, 0f));
			Gizmos.DrawRay(rect.max + Vector2.down * this.topBarUp, new Vector2(-rect.width, 0f));
		}

		// Token: 0x04002C35 RID: 11317
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002C36 RID: 11318
		public AgentState up;

		// Token: 0x04002C37 RID: 11319
		private AgentState dragging;

		// Token: 0x04002C38 RID: 11320
		[Header("UpgradesMenu")]
		[SerializeField]
		private GameObject clickBlocker;

		// Token: 0x04002C39 RID: 11321
		[Header("Portraits")]
		[SerializeField]
		private PortraitToken portraitExample;

		// Token: 0x04002C3A RID: 11322
		private LocalPool<SelectableToken> portraitPool;

		// Token: 0x04002C3B RID: 11323
		private List<PortraitToken> allPortraits = new List<PortraitToken>(10);

		// Token: 0x04002C3C RID: 11324
		private List<PortraitToken> availablePortraits = new List<PortraitToken>(10);

		// Token: 0x04002C3D RID: 11325
		[SerializeField]
		private ScrollGrid portraitGrid;

		// Token: 0x04002C3E RID: 11326
		[SerializeField]
		private ScrollGrid.Def upGridDef;

		// Token: 0x04002C3F RID: 11327
		[SerializeField]
		private ScrollGrid.Def downGridDef;

		// Token: 0x04002C40 RID: 11328
		[SerializeField]
		private ScrollGrid.Def wonGridDef;

		// Token: 0x04002C41 RID: 11329
		[Header("Banner")]
		[SerializeField]
		private Graphic bannerImage;

		// Token: 0x04002C42 RID: 11330
		private TargetAnimator<Color> bannerColorAnim;

		// Token: 0x04002C43 RID: 11331
		[Header("Structure")]
		[SerializeField]
		private RectTransform[] sliders;

		// Token: 0x04002C44 RID: 11332
		[SerializeField]
		private GameObject[] toDeactivate;

		// Token: 0x04002C45 RID: 11333
		[SerializeField]
		private RectTransform topBarRt;

		// Token: 0x04002C46 RID: 11334
		[SerializeField]
		private RectTransform bannerRt;

		// Token: 0x04002C47 RID: 11335
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04002C48 RID: 11336
		private TargetAnimator<float> upAnim;

		// Token: 0x04002C49 RID: 11337
		[SerializeField]
		private float topBarDown = 100f;

		// Token: 0x04002C4A RID: 11338
		[SerializeField]
		private float topBarUp = 100f;

		// Token: 0x04002C4B RID: 11339
		[Header("Items")]
		[SerializeField]
		private ScrollGrid itemGrid;

		// Token: 0x04002C4C RID: 11340
		[Header("Upgrades")]
		[SerializeField]
		private ScrollGrid upgradeGrid;

		// Token: 0x04002C4D RID: 11341
		[SerializeField]
		private UpgradeToken upgradeExample;

		// Token: 0x04002C4E RID: 11342
		private LocalPool<SelectableToken> upgradePool;

		// Token: 0x04002C4F RID: 11343
		[Header("Slots")]
		[SerializeField]
		private SlotToken slotExampleItem;

		// Token: 0x04002C50 RID: 11344
		[SerializeField]
		private SlotToken slotExampleClass;

		// Token: 0x04002C51 RID: 11345
		[SerializeField]
		private SlotToken slotExampleSkill;

		// Token: 0x04002C52 RID: 11346
		private SlotToken itemSlot;

		// Token: 0x04002C53 RID: 11347
		private SlotToken classSlot;

		// Token: 0x04002C54 RID: 11348
		private SlotToken skillSlot;

		// Token: 0x04002C55 RID: 11349
		private Dictionary<HeroUpgradeTypeEnum, LocalPool<SelectableToken>> slotPools = new Dictionary<HeroUpgradeTypeEnum, LocalPool<SelectableToken>>(3);

		// Token: 0x04002C56 RID: 11350
		private List<SlotToken> activeSlots = new List<SlotToken>();

		// Token: 0x04002C57 RID: 11351
		[Header("Curves")]
		[SerializeField]
		private UpgradeCurve curveExample;

		// Token: 0x04002C58 RID: 11352
		private LocalPool<UpgradeCurve> curvePool;

		// Token: 0x04002C59 RID: 11353
		[Header("Audio")]
		[SerializeField]
		private AudioMixerSnapshot defaultSnapshot;

		// Token: 0x04002C5A RID: 11354
		[SerializeField]
		private AudioMixerSnapshot upgradeSnapshot;

		// Token: 0x04002C5B RID: 11355
		private List<UpgradeToken> upgradeTokens = new List<UpgradeToken>();

		// Token: 0x04002C5C RID: 11356
		private List<UpgradeToken> activeUpgradeTokens = new List<UpgradeToken>();

		// Token: 0x04002C5D RID: 11357
		private List<UpgradeToken> activeItemTokens = new List<UpgradeToken>();

		// Token: 0x04002C5E RID: 11358
		[Header("Info")]
		[SerializeField]
		private HeroInfo heroInfo;

		// Token: 0x04002C5F RID: 11359
		[SerializeField]
		private EmptySlotInfo emptySlotInfo;

		// Token: 0x04002C60 RID: 11360
		[SerializeField]
		private UpgradeInfo upgradeInfo;

		// Token: 0x04002C61 RID: 11361
		private SelectableInfo selectedInfo;

		// Token: 0x04002C62 RID: 11362
		[Header("Button")]
		[SerializeField]
		private RectTransform buttonRt;

		// Token: 0x04002C63 RID: 11363
		private Localize buttonLoc;

		// Token: 0x04002C64 RID: 11364
		private CanvasGroup buttonCanvas;

		// Token: 0x04002C65 RID: 11365
		private UIClickable buttonClickable;

		// Token: 0x04002C66 RID: 11366
		private CoinGraphic buttonCoin;

		// Token: 0x04002C67 RID: 11367
		[TermsPopup("")]
		[SerializeField]
		private string eqiupTerm = string.Empty;

		// Token: 0x04002C68 RID: 11368
		[TermsPopup("")]
		[SerializeField]
		private string buyTerm = string.Empty;

		// Token: 0x04002C69 RID: 11369
		[TermsPopup("")]
		[SerializeField]
		private string consumeTerm = string.Empty;

		// Token: 0x04002C6A RID: 11370
		private AgentState buttonState;

		// Token: 0x04002C6B RID: 11371
		private TargetAnimator buttonAnim;

		// Token: 0x04002C6C RID: 11372
		[Header("Coin Bank")]
		[SerializeField]
		private CoinGraphic coinBankGraphic;

		// Token: 0x04002C6D RID: 11373
		[SerializeField]
		private Transform coinBank;

		// Token: 0x04002C6E RID: 11374
		[SerializeField]
		private Transform localCoinBankContainer;

		// Token: 0x04002C6F RID: 11375
		private Transform campaignCoinBankContainer;

		// Token: 0x04002C70 RID: 11376
		private CanvasGroup coinBankCanvasGroup;

		// Token: 0x04002C72 RID: 11378
		private FabricEventReference openAudioId = "UI/Upgrade/Enter";

		// Token: 0x04002C73 RID: 11379
		private FabricEventReference closeAudioId = FabricID.uiBack;

		// Token: 0x04002C74 RID: 11380
		private FabricEventReference ambianceAudioId = "Amb/Abilities";

		// Token: 0x04002C75 RID: 11381
		private FabricEventReference purchaseFlairAudioId = "UI/Upgrade/Ability";

		// Token: 0x04002C76 RID: 11382
		private FabricEventReference insufficientFundsAudioId = "UI/Upgrade/Insufficient";

		// Token: 0x04002C77 RID: 11383
		private FabricEventReference switchHeroAudioId = "UI/Menu/UpgradeSwitchCharacter";

		// Token: 0x04002C78 RID: 11384
		private FabricEventReference deadHeroAudioId = "UI/InGame/UnitSelectDead";

		// Token: 0x04002C79 RID: 11385
		private IUpgradesProxy proxy;

		// Token: 0x04002C7A RID: 11386
		public SquadLight squadLight;

		// Token: 0x04002C7B RID: 11387
		private float timeOfLastHeroSelectSound;

		// Token: 0x04002C7C RID: 11388
		private bool initialized;

		// Token: 0x04002C7D RID: 11389
		private bool gameOver;

		// Token: 0x04002C7E RID: 11390
		private SelectableToken _selected;

		// Token: 0x04002C7F RID: 11391
		private PortraitToken _focusedPortrait;
	}
}
