using System;
using System.Diagnostics;
using I2.Loc;
using RTM.Pools;
using RTM.UISystem;
using RTM.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008DD RID: 2269
	public class LoadoutUIListBanner : MonoBehaviour, IPoolable
	{
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x0010BBD0 File Offset: 0x00109FD0
		public RectTransform curveConnectorLeft
		{
			get
			{
				return this._curveConnectorLeft;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x0010BBD8 File Offset: 0x00109FD8
		public RectTransform curveConnectorRight
		{
			get
			{
				return (!this.heroDef) ? this.curveConnectorLeft : this._curveConnectorRight;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x0010BBFB File Offset: 0x00109FFB
		// (set) Token: 0x06003C20 RID: 15392 RVA: 0x0010BC03 File Offset: 0x0010A003
		public UIClickable clickable { get; private set; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x0010BC0C File Offset: 0x0010A00C
		public HeroDefinition heroDef
		{
			get
			{
				return this._heroDef.Target;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x0010BC19 File Offset: 0x0010A019
		public FabricEventReference selectAudio
		{
			get
			{
				return (!this.heroDef) ? FabricEventReference.none : this._selectAudio;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x0010BC3B File Offset: 0x0010A03B
		public FabricEventReference voiceAudio
		{
			get
			{
				return (!this.heroDef) ? FabricID.uiFocus : this.heroDef.voice.portraitSelectAudio;
			}
		}

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06003C24 RID: 15396 RVA: 0x0010BC68 File Offset: 0x0010A068
		// (remove) Token: 0x06003C25 RID: 15397 RVA: 0x0010BCA0 File Offset: 0x0010A0A0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onHeroChanged = delegate()
		{
		};

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x0010BCD6 File Offset: 0x0010A0D6
		// (set) Token: 0x06003C27 RID: 15399 RVA: 0x0010BCE3 File Offset: 0x0010A0E3
		public bool selected
		{
			get
			{
				return this.clickable.selected;
			}
			set
			{
				this.clickable.selected = value;
			}
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x0010BCF4 File Offset: 0x0010A0F4
		private void MaybeInit()
		{
			if (this.clickable == null)
			{
				this.clickable = base.GetComponentInChildren<UIClickable>(true);
				UINavigable component = this.clickable.GetComponent<UINavigable>();
				component.onConsumedNavigation += this.OnConsumedNavigation;
				component.onFocusChanged += this.OnFocusChanged;
				this.closeVis = this.closeIcon.GetComponent<IUIVisibility>();
				this.clickable.onStateChanged += delegate(UIInteractable.State s)
				{
					bool visible = this.heroDef != null && this.closeAction != null && (s == UIInteractable.State.Hover || s == UIInteractable.State.PointerButtonDown) && Profile.userSettings.cursorBehaviour != UserSettings.CursorBehaviour.Touch;
					this.closeVis.SetVisible(visible, false);
				};
			}
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x0010BD77 File Offset: 0x0010A177
		private void OnFocusChanged(bool focus)
		{
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x0010BD7C File Offset: 0x0010A17C
		private void OnConsumedNavigation(Vector2 navDir)
		{
			HeroDefinition heroDefinition = (this.cycleAction == null) ? this.heroDef : this.cycleAction(this.heroDef, (navDir.x <= 0f) ? -1 : 1);
			if (heroDefinition != this.heroDef)
			{
				this.UpdateHero(heroDefinition);
				FabricWrapper.PostEvent(this.voiceAudio);
				FabricWrapper.PostEvent(this.selectAudio);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x0010BE04 File Offset: 0x0010A204
		public void Setup(HeroDefinition heroDef, Action<LoadoutUIListBanner> onClick, Func<HeroDefinition, int, HeroDefinition> cycleAction = null, Action<LoadoutUIListBanner> closeAction = null)
		{
			this.MaybeInit();
			this.onClick = onClick;
			this.cycleAction = cycleAction;
			this.closeAction = closeAction;
			if (!this.upgradeIcons.initialized)
			{
				this.upgradeIcons.Init(base.GetComponentsInChildren<LoadoutUIUpgradeIcon>(true), null);
			}
			this.UpdateHero(heroDef);
			this.closeVis.SetVisible(false, true);
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x0010BE64 File Offset: 0x0010A264
		public void UpdateHero(HeroDefinition heroDef)
		{
			this._heroDef.Target = heroDef;
			this.upgradeIcons.ReturnAll();
			if (heroDef)
			{
				bool flag = !heroDef.fatigued;
				bool hasCornucopiaDeployAvailable = heroDef.hasCornucopiaDeployAvailable;
				this.bannerPolygon.gameObject.SetActive(true);
				this.nameText.gameObject.SetActive(true);
				this.emptySlotText.gameObject.SetActive(false);
				this.icon.Set(heroDef.graphics);
				this.icon.saturation = (float)((!flag) ? 0 : 1);
				this.bannerPolygon.Setup(heroDef, heroDef.fatigued);
				this.nameText.Term = heroDef.nameTerm;
				if (flag)
				{
					foreach (SerializableHeroUpgrade serializableHeroUpgrade in heroDef.upgrades)
					{
						if (serializableHeroUpgrade.definition.typeEnum != HeroUpgradeTypeEnum.Trait)
						{
							LoadoutUIUpgradeIcon instance = this.upgradeIcons.GetInstance();
							instance.Setup(serializableHeroUpgrade, serializableHeroUpgrade.level);
							instance.transform.SetAsLastSibling();
						}
					}
				}
				this.fatiguedText.gameObject.SetActive(!flag);
				this.fatiguedIcon.gameObject.SetActive(!flag);
				this.traitSpacer.SetActive(heroDef.traitUpgrade != null);
				this.traitSpacer.transform.SetAsLastSibling();
				for (int i = 0; i < this.cornucopiaIcons.Length; i++)
				{
					this.cornucopiaIcons[i].gameObject.SetActive(heroDef.maxUsesPerTurn > 1 && i < (int)heroDef.maxUsesPerTurn && (int)heroDef.timesUsedThisTurn <= i);
				}
			}
			else
			{
				this.bannerPolygon.gameObject.SetActive(false);
				this.nameText.gameObject.SetActive(false);
				this.icon.foreground = null;
				this.icon.sprite = null;
				this.emptySlotText.Term = this.emptySlotTerm;
				this.emptySlotText.gameObject.SetActive(true);
				this.fatiguedText.gameObject.SetActive(false);
				this.fatiguedIcon.gameObject.SetActive(false);
				foreach (Image image in this.cornucopiaIcons)
				{
					image.gameObject.SetActive(false);
				}
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform as RectTransform);
			UINavigable uinavigable = (!this.clickable) ? null : this.clickable.GetComponent<UINavigable>();
			if (uinavigable)
			{
				uinavigable.focusAudio = this._selectAudio;
				uinavigable.selectAudio = FabricID.uiFocus;
			}
			this.hitboxGraphic.enabled = (heroDef || this.cycleAction == null);
			this.onHeroChanged();
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x0010C19C File Offset: 0x0010A59C
		public void HandleClick()
		{
			if (this.onClick != null)
			{
				this.onClick(this);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x0010C1C5 File Offset: 0x0010A5C5
		public void HandleCloseClick()
		{
			if (this.closeAction != null)
			{
				this.closeAction(this);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x0010C1EE File Offset: 0x0010A5EE
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x0010C1F0 File Offset: 0x0010A5F0
		void IPoolable.OnRemoved()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x0010C1FE File Offset: 0x0010A5FE
		void IPoolable.OnReturned()
		{
			this.UpdateHero(null);
			this.onClick = null;
			this.cycleAction = null;
			base.gameObject.SetActive(false);
		}

		// Token: 0x040029DC RID: 10716
		[Header("Assets")]
		[TermsPopup("")]
		public string emptySlotTerm = "LOADOUT/AVAILABLE";

		// Token: 0x040029DD RID: 10717
		[Header("ChildReferences")]
		[SerializeField]
		private MaskedSprite icon;

		// Token: 0x040029DE RID: 10718
		[SerializeField]
		private Localize nameText;

		// Token: 0x040029DF RID: 10719
		[SerializeField]
		private Localize emptySlotText;

		// Token: 0x040029E0 RID: 10720
		[SerializeField]
		private Localize fatiguedText;

		// Token: 0x040029E1 RID: 10721
		[SerializeField]
		private GameObject traitSpacer;

		// Token: 0x040029E2 RID: 10722
		[SerializeField]
		private Image fatiguedIcon;

		// Token: 0x040029E3 RID: 10723
		[SerializeField]
		private Image[] cornucopiaIcons;

		// Token: 0x040029E4 RID: 10724
		[SerializeField]
		private BannerPolygon bannerPolygon;

		// Token: 0x040029E5 RID: 10725
		[SerializeField]
		private Graphic hitboxGraphic;

		// Token: 0x040029E6 RID: 10726
		[SerializeField]
		private RectTransform _curveConnectorRight;

		// Token: 0x040029E7 RID: 10727
		[SerializeField]
		private RectTransform _curveConnectorLeft;

		// Token: 0x040029E8 RID: 10728
		[SerializeField]
		private GameObject closeIcon;

		// Token: 0x040029E9 RID: 10729
		private IUIVisibility closeVis;

		// Token: 0x040029EA RID: 10730
		private Action<LoadoutUIListBanner> onClick;

		// Token: 0x040029EB RID: 10731
		private Func<HeroDefinition, int, HeroDefinition> cycleAction;

		// Token: 0x040029EC RID: 10732
		private Action<LoadoutUIListBanner> closeAction;

		// Token: 0x040029EE RID: 10734
		private WeakReference<HeroDefinition> _heroDef = new WeakReference<HeroDefinition>(null);

		// Token: 0x040029EF RID: 10735
		private LocalPool<LoadoutUIUpgradeIcon> upgradeIcons = new LocalPool<LoadoutUIUpgradeIcon>();

		// Token: 0x040029F0 RID: 10736
		public FabricEventReference _selectAudio = "UI/InGame/UnitSelect";
	}
}
