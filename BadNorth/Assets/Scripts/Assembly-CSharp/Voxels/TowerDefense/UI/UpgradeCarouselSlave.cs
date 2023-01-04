using System;
using RTM.Pools;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000904 RID: 2308
	internal class UpgradeCarouselSlave : MonoBehaviour
	{
		// Token: 0x06003DB1 RID: 15793 RVA: 0x00114FC8 File Offset: 0x001133C8
		public void Init()
		{
			this.items = new LocalPool<UpgradeCarouselItem>(base.GetComponentsInChildren<UpgradeCarouselItem>(true), null);
			this.items.ExpandTo(2);
			this.driver.onDisplayUpdated += this.OnDisplayUpdated;
			this.driver.onSnapToPosition += this.OnSnapToPosition;
			this.driver.onInteractibleStateChanged += this.OnInteractibleStateChanged;
			this.driver.onFlash += this.Flash;
			this.OnSnapToPosition(this.driver.idx, this.driver.selectedUpgrade);
			this.OnInteractibleStateChanged(this.driver.interactableState);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00115080 File Offset: 0x00113480
		private void OnDisplayUpdated(float currentPos, float targetPos, HeroUpgradeDefinition upgradeBelow, HeroUpgradeDefinition upgradeAbove)
		{
			for (int i = this.items.inUse.Count - 1; i >= 0; i--)
			{
				UpgradeCarouselItem upgradeCarouselItem = this.items.inUse[i];
				if (upgradeCarouselItem.upgradeDef != upgradeBelow && upgradeCarouselItem.upgradeDef != upgradeAbove)
				{
					this.items.ReturnToPool(upgradeCarouselItem);
				}
			}
			this.UpdatePosition(Mathf.FloorToInt(currentPos), currentPos, targetPos, upgradeBelow);
			this.UpdatePosition(Mathf.CeilToInt(currentPos), currentPos, targetPos, upgradeAbove);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x00115110 File Offset: 0x00113510
		private void OnSnapToPosition(int rawIdx, HeroUpgradeDefinition upgradeDef)
		{
			this.items.ReturnAll();
			UpgradeCarouselItem instance = this.items.GetInstance();
			instance.Setup(upgradeDef, rawIdx, this.driver.interactableState, true);
			instance.Animate((float)rawIdx, (float)rawIdx, false);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x00115154 File Offset: 0x00113554
		private void UpdatePosition(int itemPos, float currentPos, float targetPos, HeroUpgradeDefinition upgradeDef)
		{
			foreach (UpgradeCarouselItem upgradeCarouselItem in this.items.inUse)
			{
				if (upgradeCarouselItem.upgradeDef == upgradeDef)
				{
					upgradeCarouselItem.Animate(currentPos, targetPos, false);
					return;
				}
			}
			UpgradeCarouselItem instance = this.items.GetInstance();
			instance.Setup(upgradeDef, itemPos, this.driver.interactableState, !this.driver.isUnavailable(upgradeDef));
			instance.Animate(currentPos, targetPos, false);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x00115208 File Offset: 0x00113608
		private void OnInteractibleStateChanged(UIInteractable.State state)
		{
			foreach (UpgradeCarouselItem upgradeCarouselItem in this.items.inUse)
			{
				upgradeCarouselItem.SetState(state);
			}
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0011526C File Offset: 0x0011366C
		public void Flash(float holdTime = 0f)
		{
			foreach (UpgradeCarouselItem upgradeCarouselItem in this.items.inUse)
			{
				upgradeCarouselItem.Flash(holdTime);
			}
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x001152D0 File Offset: 0x001136D0
		public void Awake()
		{
			this.Init();
		}

		// Token: 0x04002B19 RID: 11033
		[SerializeField]
		private UpgradeCarousel driver;

		// Token: 0x04002B1A RID: 11034
		private LocalPool<UpgradeCarouselItem> items;
	}
}
