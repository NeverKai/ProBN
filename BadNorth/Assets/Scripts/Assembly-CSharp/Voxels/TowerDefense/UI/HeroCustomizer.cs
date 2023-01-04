using System;
using System.Collections.Generic;
using System.Diagnostics;
using I2.Loc;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.CampaignGeneration;
using Voxels.TowerDefense.HeroGeneration;
using Voxels.TowerDefense.ScriptAnimations;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008F8 RID: 2296
	public class HeroCustomizer : MonoBehaviour
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x001108F8 File Offset: 0x0010ECF8
		// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x00110900 File Offset: 0x0010ED00
		public HeroDefinition heroDef { get; private set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x00110909 File Offset: 0x0010ED09
		// (set) Token: 0x06003CFA RID: 15610 RVA: 0x00110911 File Offset: 0x0010ED11
		public bool hasDeluxe
		{
			get
			{
				return this._hasDeluxe;
			}
			set
			{
				if (this._hasDeluxe != value)
				{
					this.dirtyFlags |= HeroGeneratorUI.ChangeFlags.Portrait;
					this._hasDeluxe = value;
				}
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x00110938 File Offset: 0x0010ED38
		public FabricEventReference heroSelectAudio
		{
			get
			{
				HeroTraitGiant heroTraitGiant = (this.upgrades[0] == null) ? null : (this.upgrades[0].definition as HeroTraitGiant);
				if (heroTraitGiant)
				{
					HeroVoice voiceFor = heroTraitGiant.GetVoiceFor(this.heroDef);
					if (voiceFor != null && voiceFor.portraitSelectAudio != null)
					{
						return voiceFor.portraitSelectAudio;
					}
				}
				return this.heroDef.voice.portraitSelectAudio;
			}
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x001109B9 File Offset: 0x0010EDB9
		public void DecrementHandler()
		{
			this.ChangeHandler(-1);
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x001109C2 File Offset: 0x0010EDC2
		public void IncrementHandler()
		{
			this.ChangeHandler(1);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x001109CC File Offset: 0x0010EDCC
		public void ChangeHandler(int stepSize)
		{
			this.dirtyFlags = ((stepSize == 0) ? this.dirtyFlags : HeroGeneratorUI.ChangeFlags.All);
			this.stopwatch.Reset();
			this.stopwatch.Start();
			HeroGeneratorUI.instance.RandomizeStartingHero(this.heroDef, this.upgrades, this.hasDeluxe, ref this.seed, stepSize, this.dirtyFlags, this.otherHeroes);
			this.stopwatch.Stop();
			this.portraitSprite.Set(this.heroDef.graphics);
			this.nameLocalize.Term = this.heroDef.nameTerm;
			foreach (BannerPolygon bannerPolygon in this.bannerGraphics)
			{
				bannerPolygon.Setup(this.heroDef, false);
			}
			this.dirtyFlags = HeroGeneratorUI.ChangeFlags.None;
			if (stepSize != 0)
			{
				FabricWrapper.PostEvent(this.heroSelectAudio);
				FabricWrapper.PostEvent(FabricID.settingChange);
			}
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x00110AC4 File Offset: 0x0010EEC4
		public void SetTrait(HeroUpgradeDefinition trait)
		{
			object obj = (this.upgrades[0] == null) ? null : this.upgrades[0].definition;
			this.SetDirtyFlags(this.upgrades[0], trait);
			this.dirtyFlags |= HeroGeneratorUI.ChangeFlags.Banner;
			this.upgrades[0] = trait;
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x00110B33 File Offset: 0x0010EF33
		public void SetItem(HeroUpgradeDefinition item)
		{
			this.SetDirtyFlags(this.upgrades[1], item);
			this.upgrades[1] = item;
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x00110B60 File Offset: 0x0010EF60
		private void SetDirtyFlags(HeroUpgradeDefinition oldUpgrade, HeroUpgradeDefinition newUpgrade)
		{
			this.dirtyFlags |= ((!oldUpgrade || !oldUpgrade.affectsPortrait) ? HeroGeneratorUI.ChangeFlags.None : HeroGeneratorUI.ChangeFlags.Portrait);
			this.dirtyFlags |= ((!newUpgrade || !newUpgrade.affectsPortrait) ? HeroGeneratorUI.ChangeFlags.None : HeroGeneratorUI.ChangeFlags.Portrait);
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x00110BC3 File Offset: 0x0010EFC3
		public void SetOtherHero(HeroDefinition heroDef)
		{
			this.otherHeroes.Clear();
			if (heroDef)
			{
				this.otherHeroes.Add(heroDef);
			}
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x00110BE8 File Offset: 0x0010EFE8
		public void Init(bool hasDeluxe)
		{
			this._hasDeluxe = hasDeluxe;
			this.bannerGraphics = base.GetComponentsInChildren<BannerPolygon>(true);
			this.upgrades.Add(null);
			this.upgrades.Add(null);
			UINavigable component = base.GetComponent<UINavigable>();
			component.onConsumedNavigation += this.Navigable_onConsumedNavigation;
			MaskedSprite.BorderSettings focusBorderSettings = this.portraitSprite.borders[1];
			MaskedSprite.BorderSettings unfocusBorderSettings = focusBorderSettings;
			unfocusBorderSettings.outlineWidth = 0f;
			unfocusBorderSettings.width = 0f;
			AnimatedState focus = new AnimatedState("focus", this.stateRoot.rootState, false, false);
			component.onFocusChanged += delegate(bool f)
			{
				focus.active = f;
			};
			TargetAnimator<float> anim = focus.anim;
			anim.setFunc = (Action<float>)Delegate.Combine(anim.setFunc, new Action<float>(delegate(float a)
			{
				this.portraitSprite.borders[1] = MaskedSprite.BorderSettings.Lerp(unfocusBorderSettings, focusBorderSettings, a);
				this.portraitSprite.SetDirty();
			}));
			focus.ForceToTarget();
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x00110CF5 File Offset: 0x0010F0F5
		private void Navigable_onConsumedNavigation(Vector2 dir)
		{
			if (dir.x < 0f)
			{
				this.DecrementHandler();
			}
			else if (dir.x > 0f)
			{
				this.IncrementHandler();
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x00110D2A File Offset: 0x0010F12A
		public void Setup(int seed, int heroIdx, MonoHero monoHero)
		{
			this.seed = seed;
			this.heroDef = new HeroDefinition(heroIdx);
			this.heroDef.monoHero = monoHero;
			this.dirtyFlags = HeroGeneratorUI.ChangeFlags.All;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x00110D58 File Offset: 0x0010F158
		public HeroDefinition ExtractHero(out HeroUpgradeDefinition trait, out HeroUpgradeDefinition item)
		{
			HeroDefinition heroDef = this.heroDef;
			trait = this.upgrades[0];
			item = this.upgrades[1];
			this.heroDef = null;
			int i = 0;
			int count = this.upgrades.Count;
			while (i < count)
			{
				this.upgrades[i] = null;
				i++;
			}
			return heroDef;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x00110DC5 File Offset: 0x0010F1C5
		private void LateUpdate()
		{
			this.Generate(false);
			this.stateRoot.Update();
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x00110DD9 File Offset: 0x0010F1D9
		public void Generate(bool force)
		{
			if (force || this.dirtyFlags != HeroGeneratorUI.ChangeFlags.None)
			{
				this.ChangeHandler(0);
			}
		}

		// Token: 0x04002A86 RID: 10886
		[SerializeField]
		private MaskedSprite portraitSprite;

		// Token: 0x04002A87 RID: 10887
		[SerializeField]
		private Localize nameLocalize;

		// Token: 0x04002A88 RID: 10888
		[SerializeField]
		private AgentStateRoot stateRoot = new AgentStateRoot(4);

		// Token: 0x04002A89 RID: 10889
		private BannerPolygon[] bannerGraphics;

		// Token: 0x04002A8A RID: 10890
		private HeroGeneratorUI.ChangeFlags dirtyFlags;

		// Token: 0x04002A8B RID: 10891
		private int seed;

		// Token: 0x04002A8D RID: 10893
		private List<HeroDefinition> otherHeroes = new List<HeroDefinition>(2);

		// Token: 0x04002A8E RID: 10894
		private List<SerializableHeroUpgrade> upgrades = new List<SerializableHeroUpgrade>(2);

		// Token: 0x04002A8F RID: 10895
		private bool _hasDeluxe;

		// Token: 0x04002A90 RID: 10896
		private Stopwatch stopwatch = new Stopwatch();
	}
}
