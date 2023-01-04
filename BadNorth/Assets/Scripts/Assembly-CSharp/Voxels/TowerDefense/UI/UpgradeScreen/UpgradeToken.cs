using System;
using System.Collections.Generic;
using UnityEngine;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI.UpgradeScreen
{
	// Token: 0x02000939 RID: 2361
	public class UpgradeToken : SelectableToken
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x00121B22 File Offset: 0x0011FF22
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x00121B2A File Offset: 0x0011FF2A
		public int level { get; private set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x00121B33 File Offset: 0x0011FF33
		public HeroUpgradeDefinition.Level levelDef
		{
			get
			{
				return this.upgradeDef.levels[this.level];
			}
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x00121B50 File Offset: 0x0011FF50
		protected override void OnInitialize()
		{
			this.coin.MaybeInitialize();
			this.saturationAnim = new TargetAnimator("saturation", () => this.maskedSprite.saturation, delegate(float s)
			{
				this.maskedSprite.saturation = s;
			}, base.rootState, LerpTowards.standard);
			this.yAnim = new TargetAnimator("PosY", () => this.maskedSprite.transform.localPosition.y, delegate(float x)
			{
				this.maskedSprite.transform.localPosition = this.maskedSprite.transform.localPosition.SetY(x);
			}, base.rootState, LerpTowards.standard);
			this.bouncy = new AgentState("bouncy", base.rootState.stateRoot, false, false);
			this.bouncy.OnUpdate += delegate()
			{
				this.yAnim.SetTargetOrCurrent(CoinGraphic.pulse * ((!this.coin.visible.active) ? 2f : 1f));
			};
			this.onReturnToPool = (Action)Delegate.Combine(this.onReturnToPool, new Action(delegate()
			{
				this.slot = null;
			}));
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00121C24 File Offset: 0x00120024
		private void Awake()
		{
			base.MaybeInitialize();
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x00121C2C File Offset: 0x0012002C
		public UpgradeToken Setup(SuperUpgradeMenu menu, HeroUpgradeDefinition upgradeDef, int level)
		{
			base.MaybeInitialize();
			this.upgradeDef = upgradeDef;
			this.level = level;
			this.isConsumable = (upgradeDef.typeEnum == HeroUpgradeTypeEnum.Consumable);
			this.isItem = ((upgradeDef.typeEnum == HeroUpgradeTypeEnum.Item && level == 0) || this.isConsumable);
			this.maskedSprite.Set(upgradeDef, level);
			if (upgradeDef)
			{
				this.coin.number = upgradeDef.GetLevelCost(level);
			}
			base.navigable.focusAudio = UpgradeToken.focusAudio;
			return this;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x00121CB8 File Offset: 0x001200B8
		public void SetAvailable(bool available)
		{
			this.coin.bouncy.SetActive(available);
			this.coin.unaffordable.SetActive(!available);
			this.bouncy.SetActive(available);
			this.saturationAnim.SetTarget((!available) ? 0.33f : 1f, null, null, null, 0f, null);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x00121D22 File Offset: 0x00120122
		protected override void OnClicked()
		{
			this.menu.OnClicked(this);
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x00121D30 File Offset: 0x00120130
		protected override void OnConsumedNavigation(Vector2 dir)
		{
			this.menu.OnConsumedNavigation(this, dir);
		}

		// Token: 0x04002CB7 RID: 11447
		private static FabricEventReference focusAudio = "UI/Upgrade/Hover";

		// Token: 0x04002CB8 RID: 11448
		public SlotToken slot;

		// Token: 0x04002CB9 RID: 11449
		public HeroUpgradeDefinition upgradeDef;

		// Token: 0x04002CBB RID: 11451
		public bool isItem;

		// Token: 0x04002CBC RID: 11452
		public bool isConsumable;

		// Token: 0x04002CBD RID: 11453
		public List<UpgradeCurve> curves = new List<UpgradeCurve>();

		// Token: 0x04002CBE RID: 11454
		private TargetAnimator saturationAnim;

		// Token: 0x04002CBF RID: 11455
		private TargetAnimator yAnim;

		// Token: 0x04002CC0 RID: 11456
		private AgentState bouncy;
	}
}
