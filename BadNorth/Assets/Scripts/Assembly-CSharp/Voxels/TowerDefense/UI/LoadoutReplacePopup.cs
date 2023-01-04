using System;
using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008DB RID: 2267
	public class LoadoutReplacePopup : UIMenu, IslandUIManager.IAwake, IslandGameplayManager.ISetupIslandCoroutine, IslandGameplayManager.IWipeIsland, CursorManager.IPointerCursor, CursorManager.ICursor
	{
		// Token: 0x06003BEF RID: 15343 RVA: 0x0010A838 File Offset: 0x00108C38
		void IslandUIManager.IAwake.OnAwake(IslandUIManager manager)
		{
			this.banners = new LocalPool<LoadoutUIListBanner>(base.GetComponentsInChildren<LoadoutUIListBanner>(true), null);
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			base.gameObject.SetActive(false);
			this.curves = new LocalPool<LoadoutCurve>(base.GetComponentsInChildren<LoadoutCurve>(true), null);
			this.scrollRect = ((!this.scrollRect) ? base.GetComponentInChildren<ScrollRect>() : this.scrollRect);
			this.scrollRect.onValueChanged.AddListener(delegate(Vector2 v)
			{
				this.OnScroll();
			});
			IslandGameplayManager gameplayManager = manager.gameplayManager;
			this.cursorManager = gameplayManager.cursorManager;
			this.notificationManager = gameplayManager.notificationManager;
			this.changeHero = new Action<LoadoutUIListBanner>(this.ChangeHero);
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x0010A904 File Offset: 0x00108D04
		IEnumerator IslandGameplayManager.ISetupIslandCoroutine.OnSetup(Island island)
		{
			CampaignSave campaignSave = island.levelNode.campaign.campaignSave;
			int numHeroes = campaignSave.GetNumAvailableHeroes();
			while (this.curves.capacity < numHeroes)
			{
				this.curves.AddInstance();
				yield return null;
			}
			while (this.banners.capacity < numHeroes)
			{
				this.banners.AddInstance();
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x0010A928 File Offset: 0x00108D28
		public void OpenMenu(RectTransform mainListTransform, LoadoutUIListBanner selectedBanner, List<HeroDefinition> availableHeroes)
		{
			this.banners.ReturnAll();
			this.curves.ReturnAll();
			if (base.isOpen && this.target == selectedBanner)
			{
				FabricWrapper.PostEvent(FabricID.uiBack);
				this.CloseMenu();
				return;
			}
			this.target = selectedBanner;
			base.transform.position = base.transform.position.SetX(mainListTransform.GetWorldSpaceRect().xMax);
			float y = selectedBanner.transform.position.y;
			float height = (base.transform.parent as RectTransform).GetWorldSpaceRect().height;
			float num = y / height;
			num = Mathf.Lerp(0.5f, num, 0.95f);
			this.verticalAdjustTransform.pivot = this.verticalAdjustTransform.pivot.SetY(num);
			this.Setup(selectedBanner, availableHeroes);
			if (!base.isOpen)
			{
				this.OpenMenu();
			}
			this.scrollRect.verticalNormalizedPosition = 1f;
			base.transform.ForceChildLayoutUpdates(false);
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x0010AA46 File Offset: 0x00108E46
		public override void OpenMenu()
		{
			base.OpenMenu();
			this.cursorManager.Add(this);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x0010AA5C File Offset: 0x00108E5C
		public override void CloseMenu()
		{
			this.target = null;
			this.originalHero = null;
			this.defaultNavigable = null;
			this.cursorManager.Remove(this);
			if (this.currentNotification)
			{
				this.currentNotification.Close();
			}
			this.currentNotification = null;
			base.CloseMenu();
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x0010AAB2 File Offset: 0x00108EB2
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.visibility.SetVisible(true, false);
			this.activeState.SetActive(true);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x0010AAD3 File Offset: 0x00108ED3
		protected override void OnLostFocus()
		{
			base.OnLostFocus();
			this.visibility.SetVisible(false, false);
			this.activeState.SetActive(false);
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x0010AAF4 File Offset: 0x00108EF4
		protected override IUINavigable GetDefaultNavigable()
		{
			return (this.defaultNavigable == null) ? base.GetFirstNavigable() : this.defaultNavigable;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x0010AB14 File Offset: 0x00108F14
		private void Setup(LoadoutUIListBanner selectedBanner, List<HeroDefinition> availableHeroes)
		{
			this.originalHero = selectedBanner.heroDef;
			if (this.originalHero)
			{
				this.AddBanner(selectedBanner, null);
			}
			bool flag = false;
			foreach (HeroDefinition heroDefinition in availableHeroes)
			{
				flag |= !heroDefinition.availableThisTurn;
				this.AddBanner(selectedBanner, heroDefinition);
			}
			if (!base.isOpen && flag)
			{
				this.currentNotification = this.notificationManager.PostMessage(this.fatiguedHeroWarningTerm, IslandUINotification.Priority.Tutorial, this.fatiguedHeroLeftIcon, this.fatiguedHeroRightIcon, true, 0f);
				FabricWrapper.PostEvent(this.fatiuguedPopupAudio);
			}
			base.transform.ForceChildLayoutUpdates(false);
			foreach (LoadoutCurve loadoutCurve in this.curves.inUse)
			{
				loadoutCurve.SetDirty();
			}
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x0010AC44 File Offset: 0x00109044
		private LoadoutUIListBanner AddBanner(LoadoutUIListBanner selectedBanner, HeroDefinition heroDef)
		{
			LoadoutUIListBanner instance = this.banners.GetInstance();
			instance.emptySlotTerm = "LOADOUT/CLEAR";
			instance.Setup(heroDef, this.changeHero, null, null);
			instance.transform.SetAsLastSibling();
			LoadoutCurve instance2 = this.curves.GetInstance();
			instance2.Setup(selectedBanner, instance);
			return instance;
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x0010AC98 File Offset: 0x00109098
		private void ChangeHero(LoadoutUIListBanner banner)
		{
			if (banner.heroDef && !banner.heroDef.availableThisTurn)
			{
				if (this.currentNotification)
				{
					this.currentNotification.Flash(UnityEngine.Color.Lerp(banner.heroDef.color, UnityEngine.Color.black, 0.4f));
				}
				FabricWrapper.PostEvent(FabricID.uiError);
			}
			else if (this.target.heroDef == banner.heroDef)
			{
				FabricWrapper.PostEvent(FabricID.uiBack);
				this.CloseMenu();
			}
			else
			{
				this.target.UpdateHero(banner.heroDef);
				if (banner.heroDef)
				{
					FabricWrapper.PostEvent(banner.selectAudio);
					FabricWrapper.PostEvent(banner.voiceAudio);
				}
				else
				{
					FabricWrapper.PostEvent(this.clearAudio);
				}
				this.CloseMenu();
			}
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x0010AD86 File Offset: 0x00109186
		public void HandleCancelButton()
		{
			if (base.isOpen)
			{
				this.CloseMenu();
				FabricWrapper.PostEvent(FabricID.uiBack);
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x0010ADA4 File Offset: 0x001091A4
		private void OnScroll()
		{
			RectTransform rt = base.transform as RectTransform;
			foreach (LoadoutCurve loadoutCurve in this.curves.inUse)
			{
				loadoutCurve.SetDirty();
				loadoutCurve.UpdateAlpha(rt.GetWorldSpaceRect());
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x0010AE1C File Offset: 0x0010921C
		void IslandGameplayManager.IWipeIsland.OnWipe(Island island)
		{
			this.banners.ReturnAll();
			this.curves.ReturnAll();
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x0010AE34 File Offset: 0x00109234
		void CursorManager.IPointerCursor.OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos)
		{
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x0010AE36 File Offset: 0x00109236
		void CursorManager.IPointerCursor.OnButtonUp(PointerEventData.InputButton button, Vector2 screenPos)
		{
			this.HandleCancelButton();
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x0010AE3E File Offset: 0x0010923E
		void CursorManager.IPointerCursor.UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos)
		{
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x0010AE40 File Offset: 0x00109240
		void CursorManager.IPointerCursor.OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position)
		{
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x0010AE42 File Offset: 0x00109242
		void CursorManager.ICursor.SetActive(bool active)
		{
		}

		// Token: 0x040029B5 RID: 10677
		[SerializeField]
		private State activeState;

		// Token: 0x040029B6 RID: 10678
		[SerializeField]
		private RectTransform verticalAdjustTransform;

		// Token: 0x040029B7 RID: 10679
		[SerializeField]
		[TermsPopup("")]
		private string fatiguedHeroWarningTerm = string.Empty;

		// Token: 0x040029B8 RID: 10680
		[SerializeField]
		[SpritePreview]
		private Sprite fatiguedHeroLeftIcon;

		// Token: 0x040029B9 RID: 10681
		[SerializeField]
		[SpritePreview]
		private Sprite fatiguedHeroRightIcon;

		// Token: 0x040029BA RID: 10682
		private LocalPool<LoadoutUIListBanner> banners;

		// Token: 0x040029BB RID: 10683
		private LocalPool<LoadoutCurve> curves;

		// Token: 0x040029BC RID: 10684
		private LoadoutUIListBanner target;

		// Token: 0x040029BD RID: 10685
		private HeroDefinition originalHero;

		// Token: 0x040029BE RID: 10686
		private IUIVisibility visibility;

		// Token: 0x040029BF RID: 10687
		private CursorManager cursorManager;

		// Token: 0x040029C0 RID: 10688
		private IslandUINotificationManager notificationManager;

		// Token: 0x040029C1 RID: 10689
		private IslandUINotification currentNotification;

		// Token: 0x040029C2 RID: 10690
		private FabricEventReference fatiuguedPopupAudio = "UI/InGame/CommandersFatigued";

		// Token: 0x040029C3 RID: 10691
		private FabricEventReference clearAudio = "UI/InGame/UnitDeselect";

		// Token: 0x040029C4 RID: 10692
		private IUINavigable defaultNavigable;

		// Token: 0x040029C5 RID: 10693
		private Action<LoadoutUIListBanner> changeHero;
	}
}
