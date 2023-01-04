using System;
using System.Collections.Generic;
using System.Diagnostics;
using I2.Loc;
using RTM.OnScreenDebug;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;
using Voxels.TowerDefense.UI.MetaInventory;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008FF RID: 2303
	public class UpgradeCarousel : MonoBehaviour
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x001134C4 File Offset: 0x001118C4
		public UIInteractable.State interactableState
		{
			get
			{
				return (!this.interactible) ? UIInteractable.State.None : this.interactible.state;
			}
		}

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06003D5E RID: 15710 RVA: 0x001134E8 File Offset: 0x001118E8
		// (remove) Token: 0x06003D5F RID: 15711 RVA: 0x00113520 File Offset: 0x00111920
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<float, float, HeroUpgradeDefinition, HeroUpgradeDefinition> onDisplayUpdated = delegate(float A_0, float A_1, HeroUpgradeDefinition A_2, HeroUpgradeDefinition A_3)
		{
		};

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06003D60 RID: 15712 RVA: 0x00113558 File Offset: 0x00111958
		// (remove) Token: 0x06003D61 RID: 15713 RVA: 0x00113590 File Offset: 0x00111990
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, HeroUpgradeDefinition> onSnapToPosition = delegate(int A_0, HeroUpgradeDefinition A_1)
		{
		};

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06003D62 RID: 15714 RVA: 0x001135C8 File Offset: 0x001119C8
		// (remove) Token: 0x06003D63 RID: 15715 RVA: 0x00113600 File Offset: 0x00111A00
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<UIInteractable.State> onInteractibleStateChanged = delegate(UIInteractable.State A_0)
		{
		};

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06003D64 RID: 15716 RVA: 0x00113638 File Offset: 0x00111A38
		// (remove) Token: 0x06003D65 RID: 15717 RVA: 0x00113670 File Offset: 0x00111A70
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<float> onFlash = delegate(float A_0)
		{
		};

		// Token: 0x06003D66 RID: 15718 RVA: 0x001136A6 File Offset: 0x00111AA6
		public bool isUnavailable(HeroUpgradeDefinition upgrade)
		{
			return this.unavailableUpgrades.Contains(upgrade);
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x001136B4 File Offset: 0x00111AB4
		// (set) Token: 0x06003D68 RID: 15720 RVA: 0x001136BC File Offset: 0x00111ABC
		private int rawIdx
		{
			get
			{
				return this._rawIdx;
			}
			set
			{
				HeroUpgradeDefinition selectedUpgrade = this.selectedUpgrade;
				int rawIdx = this._rawIdx;
				this._rawIdx = value;
				if (this._rawIdx != rawIdx)
				{
					this.onIdxChanged(this._rawIdx);
				}
				HeroUpgradeDefinition selectedUpgrade2 = this.selectedUpgrade;
				if (selectedUpgrade2 != selectedUpgrade)
				{
					this.onSelectedUpgradeChanged(selectedUpgrade2);
				}
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x0011371A File Offset: 0x00111B1A
		public int idx
		{
			get
			{
				return this.RemapIdx(this.rawIdx, this.visibleUpgrades.Count);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003D6A RID: 15722 RVA: 0x00113733 File Offset: 0x00111B33
		public HeroUpgradeDefinition selectedUpgrade
		{
			get
			{
				return this.GetUpgradeAt(this.rawIdx);
			}
		}

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06003D6B RID: 15723 RVA: 0x00113744 File Offset: 0x00111B44
		// (remove) Token: 0x06003D6C RID: 15724 RVA: 0x0011377C File Offset: 0x00111B7C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> onIdxChanged = delegate(int A_0)
		{
		};

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06003D6D RID: 15725 RVA: 0x001137B4 File Offset: 0x00111BB4
		// (remove) Token: 0x06003D6E RID: 15726 RVA: 0x001137EC File Offset: 0x00111BEC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<HeroUpgradeDefinition> onSelectedUpgradeChanged = delegate(HeroUpgradeDefinition A_0)
		{
		};

		// Token: 0x06003D6F RID: 15727 RVA: 0x00113824 File Offset: 0x00111C24
		public HeroUpgradeDefinition GetUpgradeAt(int rawIdx)
		{
			return (this.visibleUpgrades == null || this.visibleUpgrades.Count <= 0) ? null : this.visibleUpgrades[this.RemapIdx(rawIdx, this.visibleUpgrades.Count)];
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x00113870 File Offset: 0x00111C70
		public UpgradeCarousel Initialize(string labelLocId)
		{
			this.items = new LocalPool<UpgradeCarouselItem>(base.GetComponentsInChildren<UpgradeCarouselItem>(true), null);
			this.items.ExpandTo(2);
			UINavigable component = base.GetComponent<UINavigable>();
			component.onConsumedNavigation += this.OnConsumedNavigation;
			this.interactible = base.GetComponent<UIInteractable>();
			this.interactible.onStateChanged += this.OnInteractibleStateChanged;
			if (this.label)
			{
				this.label.Term = labelLocId;
			}
			this.onSelectedUpgradeChanged += delegate(HeroUpgradeDefinition x)
			{
			};
			this.handleMetaInventoryClose = delegate(HeroUpgradeDefinition u)
			{
				if (this.SetValue(u, true))
				{
					this.Flash(0.15f);
				}
			};
			return this;
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x0011391C File Offset: 0x00111D1C
		public void Setup(List<HeroUpgradeDefinition> upgrades, bool reset)
		{
			HeroUpgradeDefinition selectedUpgrade = this.selectedUpgrade;
			this.visibleUpgrades.Clear();
			if (this.alwaysShowEmpty)
			{
				this.visibleUpgrades.Add(null);
			}
			this.visibleUpgrades.AddRange(upgrades);
			this.unavailableUpgrades.Clear();
			int idx = 0;
			if (!reset)
			{
				int i = 0;
				int count = this.visibleUpgrades.Count;
				while (i < count)
				{
					if (this.visibleUpgrades[i] == selectedUpgrade)
					{
						idx = i;
						break;
					}
					i++;
				}
			}
			this.SetValue(idx, true);
			this.UpdateNavigability();
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x001139BB File Offset: 0x00111DBB
		public void SetUnavailableUpgrade(HeroUpgradeDefinition def)
		{
			this.unavailableUpgrades.Clear();
			if (this.visibleUpgrades.Contains(def) && def != null)
			{
				this.unavailableUpgrades.Add(def);
			}
			this.UpdateNavigability();
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x001139F8 File Offset: 0x00111DF8
		public void UpdateUnavailableUpgrades(List<HeroUpgradeDefinition> upgrades)
		{
			this.unavailableUpgrades.Clear();
			this.unavailableUpgrades.AddRange(upgrades);
			for (int i = this.unavailableUpgrades.Count - 1; i >= 0; i--)
			{
				if (this.visibleUpgrades == null || !this.visibleUpgrades.Contains(this.unavailableUpgrades[i]))
				{
					this.unavailableUpgrades.RemoveAt(i);
				}
			}
			this.UpdateNavigability();
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00113A73 File Offset: 0x00111E73
		private void OnConsumedNavigation(Vector2 dir)
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

		// Token: 0x06003D75 RID: 15733 RVA: 0x00113AA8 File Offset: 0x00111EA8
		private void OnInteractibleStateChanged(UIInteractable.State state)
		{
			foreach (UpgradeCarouselItem upgradeCarouselItem in this.items.inUse)
			{
				upgradeCarouselItem.SetState(state);
			}
			this.onInteractibleStateChanged(state);
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x00113B18 File Offset: 0x00111F18
		public void DecrementHandler()
		{
			this.ChangeHandler(-1);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x00113B21 File Offset: 0x00111F21
		public void IncrementHandler()
		{
			this.ChangeHandler(1);
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x00113B2C File Offset: 0x00111F2C
		public void ChangeHandler(int stepSize)
		{
			if (this.isNavigable)
			{
				int num = this.rawIdx;
				HeroUpgradeDefinition selectedUpgrade = this.selectedUpgrade;
				bool flag = false;
				HeroUpgradeDefinition heroUpgradeDefinition = this.visibleUpgrades[this.idx];
				while (!flag)
				{
					num += stepSize;
					HeroUpgradeDefinition upgradeAt = this.GetUpgradeAt(num);
					flag = !this.unavailableUpgrades.Contains(upgradeAt);
					stepSize = ((stepSize >= 0) ? 1 : -1);
				}
				this.rawIdx = num;
				FabricWrapper.PostEvent(FabricID.settingChange);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x00113BC1 File Offset: 0x00111FC1
		public void ShowInInventoryHandler()
		{
			if (this.selectedUpgrade != null)
			{
				MetaInventoryMenu.OpenMenuStatic(this.selectedUpgrade, this.handleMetaInventoryClose);
				FabricWrapper.PostEvent(UpgradeCarousel.metaInventoryAudio);
			}
			else
			{
				FabricWrapper.PostEvent(FabricID.uiError);
			}
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x00113C00 File Offset: 0x00112000
		public void SetRandom(bool snap)
		{
			if (this.isNavigable)
			{
				int num = (!this.visibleUpgrades.Contains(null)) ? 0 : 1;
				int num2 = UnityEngine.Random.Range(0, this.visibleUpgrades.Count - this.unavailableUpgrades.Count - num);
				int index = 0;
				int num3 = 0;
				int i = 0;
				while (i <= num2)
				{
					bool flag = this.visibleUpgrades[num3] == null || this.unavailableUpgrades.Contains(this.visibleUpgrades[num3]);
					i += ((!flag) ? 1 : 0);
					index = num3;
					num3++;
				}
				this.SetValue(this.visibleUpgrades[index], snap);
			}
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x00113CC8 File Offset: 0x001120C8
		public bool SetValue(HeroUpgradeDefinition upgrade, bool snap)
		{
			if (!this.unavailableUpgrades.Contains(upgrade))
			{
				int idx;
				if (this.GetUpgradeIndex(upgrade, out idx))
				{
					this.SetValue(idx, snap);
					return true;
				}
			}
			else
			{
				HeroUpgradeDefinition selectedUpgrade = this.selectedUpgrade;
				this.SetValue(0, snap);
				if (this.forceOtherToChange(selectedUpgrade, snap))
				{
					this.SetValue(upgrade, snap);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x00113D30 File Offset: 0x00112130
		public void Flash(float holdTime = 0f)
		{
			foreach (UpgradeCarouselItem upgradeCarouselItem in this.items.inUse)
			{
				if (upgradeCarouselItem.pos == this.rawIdx)
				{
					upgradeCarouselItem.Flash(holdTime);
				}
			}
			this.onFlash(holdTime);
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x00113DB0 File Offset: 0x001121B0
		public bool GetUpgradeIndex(HeroUpgradeDefinition upgrade, out int idx)
		{
			idx = -1;
			for (int i = 0; i < this.visibleUpgrades.Count; i++)
			{
				HeroUpgradeDefinition x = this.visibleUpgrades[i];
				if (x == upgrade)
				{
					idx = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x00113DFC File Offset: 0x001121FC
		public void SetValue(int idx, bool snap)
		{
			this.rawIdx = Mathf.Clamp(idx, 0, this.visibleUpgrades.Count - 1);
			this.UpdateNavigability();
			if (snap)
			{
				this.currentPos = (float)this.rawIdx;
				this.items.ReturnAll();
				UpgradeCarouselItem instance = this.items.GetInstance();
				instance.Setup(this.selectedUpgrade, this.rawIdx, this.interactableState, !this.unavailableUpgrades.Contains(this.selectedUpgrade));
				instance.Animate(this.currentPos, this.currentPos, false);
				this.onSnapToPosition(this.rawIdx, this.selectedUpgrade);
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x00113EAC File Offset: 0x001122AC
		private int RemapIdx(int idx, int max)
		{
			if (max == 0)
			{
				return 0;
			}
			int num = idx % max;
			return (num >= 0) ? num : (num + max);
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x00113ED8 File Offset: 0x001122D8
		private void UpdateNavigability()
		{
			this.isNavigable = (this.visibleUpgrades.Count - this.unavailableUpgrades.Count > 1);
			this.prevArrow.disabled = !this.isNavigable;
			this.nextArrow.disabled = !this.isNavigable;
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x00113F30 File Offset: 0x00112330
		private void LateUpdate()
		{
			float num = (float)this.rawIdx;
			if (this.currentPos != num)
			{
				float num2 = Mathf.Min(Time.unscaledDeltaTime, 0.025f);
				this.currentPos = Mathf.Lerp(this.currentPos, num, num2 * 8f);
				this.currentPos = Mathf.MoveTowards(this.currentPos, num, num2 * 3f);
				if (this.currentPos == num)
				{
					this.items.ReturnAll();
					UpgradeCarouselItem instance = this.items.GetInstance();
					instance.Setup(this.selectedUpgrade, this.rawIdx, this.interactableState, true);
					instance.Animate(this.currentPos, num, true);
					this.onSnapToPosition(this.rawIdx, this.selectedUpgrade);
				}
				else
				{
					int num3 = Mathf.CeilToInt(this.currentPos);
					int num4 = Mathf.FloorToInt(this.currentPos);
					for (int i = this.items.inUse.Count - 1; i >= 0; i--)
					{
						UpgradeCarouselItem upgradeCarouselItem = this.items.inUse[i];
						if (upgradeCarouselItem.pos != num3 && upgradeCarouselItem.pos != num4)
						{
							this.items.ReturnToPool(upgradeCarouselItem);
						}
					}
					HeroUpgradeDefinition heroUpgradeDefinition = this.visibleUpgrades[this.RemapIdx(num3, this.visibleUpgrades.Count)];
					HeroUpgradeDefinition heroUpgradeDefinition2 = this.visibleUpgrades[this.RemapIdx(num4, this.visibleUpgrades.Count)];
					this.UpdatePosition(num4, this.currentPos, num, heroUpgradeDefinition2);
					this.UpdatePosition(num3, this.currentPos, num, heroUpgradeDefinition);
					this.onDisplayUpdated(this.currentPos, num, heroUpgradeDefinition2, heroUpgradeDefinition);
				}
			}
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x001140EC File Offset: 0x001124EC
		private void UpdatePosition(int itemPos, float currentPos, float targetPos, HeroUpgradeDefinition upgradeDef)
		{
			foreach (UpgradeCarouselItem upgradeCarouselItem in this.items.inUse)
			{
				if (upgradeCarouselItem.pos == itemPos)
				{
					upgradeCarouselItem.Animate(currentPos, targetPos, false);
					return;
				}
			}
			UpgradeCarouselItem instance = this.items.GetInstance();
			instance.Setup(upgradeDef, itemPos, this.interactableState, !this.unavailableUpgrades.Contains(upgradeDef));
			instance.Animate(currentPos, targetPos, false);
		}

		// Token: 0x04002ADD RID: 10973
		private const float maxDt = 0.025f;

		// Token: 0x04002ADE RID: 10974
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("UpgradeSelectorCarousel", EVerbosity.Quiet, 100);

		// Token: 0x04002ADF RID: 10975
		[Header("References")]
		[SerializeField]
		private Localize label;

		// Token: 0x04002AE0 RID: 10976
		[SerializeField]
		private UIClickable prevArrow;

		// Token: 0x04002AE1 RID: 10977
		[SerializeField]
		private UIClickable nextArrow;

		// Token: 0x04002AE2 RID: 10978
		[Header("Behaviour")]
		[SerializeField]
		private bool alwaysShowEmpty = true;

		// Token: 0x04002AE3 RID: 10979
		private List<HeroUpgradeDefinition> visibleUpgrades = new List<HeroUpgradeDefinition>(16);

		// Token: 0x04002AE4 RID: 10980
		private List<HeroUpgradeDefinition> unavailableUpgrades = new List<HeroUpgradeDefinition>(4);

		// Token: 0x04002AE5 RID: 10981
		private int _rawIdx;

		// Token: 0x04002AE6 RID: 10982
		private float currentPos;

		// Token: 0x04002AE7 RID: 10983
		private UIInteractable interactible;

		// Token: 0x04002AE8 RID: 10984
		private bool isNavigable;

		// Token: 0x04002AE9 RID: 10985
		private LocalPool<UpgradeCarouselItem> items;

		// Token: 0x04002AEE RID: 10990
		public Func<HeroUpgradeDefinition, bool, bool> forceOtherToChange;

		// Token: 0x04002AEF RID: 10991
		private Action<HeroUpgradeDefinition> handleMetaInventoryClose;

		// Token: 0x04002AF0 RID: 10992
		private static FabricEventReference metaInventoryAudio = "UI/Menu/Inventory";
	}
}
