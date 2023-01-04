using System;
using I2.Loc;
using RTM.Pools;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.MetaInventory
{
	// Token: 0x020008EB RID: 2283
	[AddComponentMenu("Meta Inventory - Info Panel")]
	internal class InfoPanel : MonoBehaviour, IPoolable
	{
		// Token: 0x06003C85 RID: 15493 RVA: 0x0010DD54 File Offset: 0x0010C154
		public void Init()
		{
			this.nameText = this.nameLocalize.GetComponent<Text>();
			this.levelInfos = new LocalPool<UpgradeLevelInfo>(base.GetComponentsInChildren<UpgradeLevelInfo>(true), null);
			this.levelInfos.ExpandTo(4);
			this.startingInfo = base.GetComponentInChildren<StartingItemInfo>(true);
			CanvasGroup component = base.GetComponent<CanvasGroup>();
			RectTransform rectTransform = base.transform as RectTransform;
			this.posAnim = new TargetAnimator<Vector2>(() => this.headerIcon.transform.parent.localPosition, delegate(Vector2 x)
			{
				this.headerIcon.transform.parent.localPosition = x;
			}, this.stateRoot.rootState, LerpTowards2.standard);
			this.visible = new AnimatedState("up", this.stateRoot.rootState, false, false);
			this.visible.Subscribe(component);
			AnimatedState animatedState = this.visible;
			animatedState.onActivity = (Action<bool>)Delegate.Combine(animatedState.onActivity, new Action<bool>(delegate(bool x)
			{
				if (!x)
				{
					this.pool.ReturnToPool(this);
				}
			}));
			this.infoContainer = (RectTransform)this.levelContainer.parent;
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x0010DE4A File Offset: 0x0010C24A
		public void OnOpen()
		{
			this.visible.anim.SetCurrent(0f);
			this.visible.SetActive(true);
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x0010DE6E File Offset: 0x0010C26E
		private void Update()
		{
			this.stateRoot.Update();
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x0010DE7C File Offset: 0x0010C27C
		public void Setup(UpgradeToken upgradeToken, int maxLevel, bool isKnown, bool isStartItem)
		{
			this.upgradeToken = upgradeToken;
			HeroUpgradeDefinition upgradeDef = upgradeToken.upgradeDef;
			bool flag = upgradeDef.typeEnum == HeroUpgradeTypeEnum.Trait;
			this.headerTraitStarter.gameObject.SetActive(isKnown && flag && isStartItem);
			this.headerTraitNonStarter.gameObject.SetActive(isKnown && flag && !isStartItem);
			this.headerIcon.gameObject.SetActive(isKnown && !flag);
			this.traitBanner.transform.parent.gameObject.SetActive(isKnown && flag);
			this.headerUnknown.gameObject.SetActive(!isKnown);
			if (isKnown)
			{
				if (flag)
				{
					this.traitBanner.color = upgradeToken.rainbows[0].color;
					this.headerTraitIconStarter.color = upgradeToken.rainbows[0].color;
					this.headerTraitIconStarter.sprite = upgradeDef.infoSprite;
					this.headerTraitIconNonStarter.sprite = upgradeDef.infoSprite;
				}
				else
				{
					this.headerIcon.Set(upgradeDef, (!isKnown) ? 0 : maxLevel);
					MaskedSprite.BorderSettings borderSettings = this.headerIcon.borders[2];
					borderSettings.width = ((!upgradeDef.canBeStartingItem || !isStartItem) ? 0f : 0.5f);
					this.headerIcon.borders[2] = borderSettings;
					this.headerIcon.SetDirty();
				}
			}
			this.nameLocalize.Term = ((!isKnown) ? upgradeDef.upgradeType.unknownNameTerm : upgradeDef.nameTerm);
			for (int i = 0; i < upgradeDef.numLevels; i++)
			{
				UpgradeLevelInfo instance = this.levelInfos.GetInstance();
				instance.Setup(upgradeDef, i, i <= maxLevel, 0f);
				instance.transform.SetAsLastSibling();
			}
			if (upgradeDef.canBeStartingItem)
			{
				this.startingInfo.gameObject.SetActive(true);
				this.startingInfo.Setup(upgradeDef, isStartItem, 0f);
			}
			else
			{
				this.startingInfo.gameObject.SetActive(false);
			}
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x0010E0C9 File Offset: 0x0010C4C9
		private void Start()
		{
			base.transform.ForceChildLayoutUpdates(true);
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x0010E0D7 File Offset: 0x0010C4D7
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.pool = (pool as LocalPool<InfoPanel>);
			this.Init();
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x0010E0F0 File Offset: 0x0010C4F0
		void IPoolable.OnRemoved()
		{
			this.visible.anim.SetCurrent(0f);
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x0010E107 File Offset: 0x0010C507
		void IPoolable.OnReturned()
		{
			this.levelInfos.ReturnAll();
		}

		// Token: 0x04002A2A RID: 10794
		private LocalPool<UpgradeLevelInfo> levelInfos;

		// Token: 0x04002A2B RID: 10795
		private StartingItemInfo startingInfo;

		// Token: 0x04002A2C RID: 10796
		[SerializeField]
		private MaskedSprite headerIcon;

		// Token: 0x04002A2D RID: 10797
		[SerializeField]
		private GameObject headerTraitStarter;

		// Token: 0x04002A2E RID: 10798
		[SerializeField]
		private GameObject headerTraitNonStarter;

		// Token: 0x04002A2F RID: 10799
		[SerializeField]
		private GameObject headerUnknown;

		// Token: 0x04002A30 RID: 10800
		[SerializeField]
		private Image headerTraitIconStarter;

		// Token: 0x04002A31 RID: 10801
		[SerializeField]
		private Image headerTraitIconNonStarter;

		// Token: 0x04002A32 RID: 10802
		[SerializeField]
		private Image traitBanner;

		// Token: 0x04002A33 RID: 10803
		[SerializeField]
		private Localize nameLocalize;

		// Token: 0x04002A34 RID: 10804
		private Text nameText;

		// Token: 0x04002A35 RID: 10805
		[SerializeField]
		private RectTransform levelContainer;

		// Token: 0x04002A36 RID: 10806
		[SerializeField]
		private RectTransform startItemContainer;

		// Token: 0x04002A37 RID: 10807
		private RectTransform infoContainer;

		// Token: 0x04002A38 RID: 10808
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002A39 RID: 10809
		public AnimatedState visible;

		// Token: 0x04002A3A RID: 10810
		public TargetAnimator<Vector2> posAnim;

		// Token: 0x04002A3B RID: 10811
		public UpgradeToken upgradeToken;

		// Token: 0x04002A3C RID: 10812
		private LocalPool<InfoPanel> pool;
	}
}
