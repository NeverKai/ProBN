using System;
using System.Diagnostics;
using I2.Loc;
using RTM.Pools;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x0200055C RID: 1372
	public class SquadSelectedBanner : MonoBehaviour, IPoolable
	{
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060023B5 RID: 9141 RVA: 0x0006EC84 File Offset: 0x0006D084
		// (remove) Token: 0x060023B6 RID: 9142 RVA: 0x0006ECBC File Offset: 0x0006D0BC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onOpen = delegate()
		{
		};

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060023B7 RID: 9143 RVA: 0x0006ECF4 File Offset: 0x0006D0F4
		// (remove) Token: 0x060023B8 RID: 9144 RVA: 0x0006ED2C File Offset: 0x0006D12C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action onClose = delegate()
		{
		};

		// Token: 0x060023B9 RID: 9145 RVA: 0x0006ED64 File Offset: 0x0006D164
		private void UpdateMobileSafeZone(Vector4 safeZone, DeviceOrientation deviceOrientation)
		{
			Vector2 offsetMin = this.bannerMin;
			Vector2 offsetMax = this.bannerMax;
			if (Platform.Is(EPlatform.IOSPhone))
			{
				offsetMin.y -= safeZone.w * (float)Screen.height / this.bannerImage.transform.lossyScale.y;
				offsetMax.x += safeZone.w * (float)Screen.height / this.bannerImage.transform.lossyScale.y * 0.3f;
			}
			this.bannerImage.rectTransform.offsetMin = offsetMin;
			this.bannerImage.rectTransform.offsetMax = offsetMax;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0006EE20 File Offset: 0x0006D220
		public void Setup(IslandUIManager manager, EnglishSquad squad)
		{
			this.portrait.Set(squad.hero.graphics);
			this.nameText.Term = squad.hero.nameTerm;
			this.abilityButtons.Setup(manager, squad);
			this.bannerImage.Setup(squad.hero, false);
			this.traitSpacer.SetActive(squad.hero.traitUpgrade != null);
			this.visibility.SetVisible(false, true);
			base.gameObject.SetActive(false);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x0006EEAD File Offset: 0x0006D2AD
		public void Open()
		{
			this.visibility.SetVisible(true, false);
			base.transform.SetAsLastSibling();
			this.onOpen();
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0006EED2 File Offset: 0x0006D2D2
		public void Close()
		{
			this.visibility.SetVisible(false, false);
			this.onClose();
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0006EEEC File Offset: 0x0006D2EC
		void IPoolable.SetPool<T>(LocalPool<T> pool)
		{
			this.visibility = base.GetComponent<IUIVisibility>();
			this.abilityButtons.Init();
			ActiveAbilityUIController componentInChildren = base.GetComponentInChildren<ActiveAbilityUIController>(true);
			componentInChildren.Init(this, this.abilityButtons);
			this.bannerMin = this.bannerImage.rectTransform.offsetMin;
			this.bannerMax = this.bannerImage.rectTransform.offsetMax;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x0006EF51 File Offset: 0x0006D351
		void IPoolable.OnRemoved()
		{
			this.UpdateMobileSafeZone(MobileScreenDetector.safeZone, MobileScreenDetector.deviceOrientation);
			Platform.onPlatformUpdated += this.UpdatePlatform;
			MobileScreenDetector.onSafeZoneChanged += this.UpdateMobileSafeZone;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0006EF85 File Offset: 0x0006D385
		private void UpdatePlatform()
		{
			if (Platform.Is(EPlatform.IOSPhone))
			{
				this.UpdateMobileSafeZone(MobileScreenDetector.safeZone, MobileScreenDetector.deviceOrientation);
			}
			else
			{
				this.UpdateMobileSafeZone(Vector4.zero, MobileScreenDetector.deviceOrientation);
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0006EFB8 File Offset: 0x0006D3B8
		void IPoolable.OnReturned()
		{
			Platform.onPlatformUpdated -= this.UpdatePlatform;
			MobileScreenDetector.onSafeZoneChanged -= this.UpdateMobileSafeZone;
			this.portrait.Clear();
			this.abilityButtons.Clear();
			base.name = "[Unused]";
			this.visibility.SetVisible(false, true);
		}

		// Token: 0x04001651 RID: 5713
		[Header("Scene References")]
		[SerializeField]
		private MaskedSprite portrait;

		// Token: 0x04001652 RID: 5714
		[SerializeField]
		private BannerPolygon bannerImage;

		// Token: 0x04001653 RID: 5715
		[SerializeField]
		private GameObject traitSpacer;

		// Token: 0x04001654 RID: 5716
		[SerializeField]
		private Localize nameText;

		// Token: 0x04001655 RID: 5717
		[SerializeField]
		private ActiveAbilityButtonContainer abilityButtons;

		// Token: 0x04001656 RID: 5718
		private IUIVisibility visibility;

		// Token: 0x04001659 RID: 5721
		private Vector2 bannerMin;

		// Token: 0x0400165A RID: 5722
		private Vector2 bannerMax;
	}
}
