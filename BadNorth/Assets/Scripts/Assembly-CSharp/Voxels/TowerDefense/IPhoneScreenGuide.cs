using System;
using ReflexCLI.Attributes;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020005E8 RID: 1512
	internal class IPhoneScreenGuide : MonoBehaviour, IGameSetup
	{
		// Token: 0x0600272F RID: 10031 RVA: 0x0007E654 File Offset: 0x0007CA54
		void IGameSetup.OnGameAwake()
		{
			IPhoneScreenGuide.instance = this;
			base.gameObject.SetActive(PlayerPrefs.GetInt("IPhoneScreenGuide", 0) != 0);
			this.OnChange(MobileScreenDetector.safeZone, MobileScreenDetector.deviceOrientation);
			MobileScreenDetector.onSafeZoneChanged += this.OnChange;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0007E6A4 File Offset: 0x0007CAA4
		private void OnChange(Vector4 safeZone, DeviceOrientation orientation)
		{
			bool flag = orientation == DeviceOrientation.LandscapeLeft;
			this.leftNotch.gameObject.SetActive(flag);
			this.rightNotch.gameObject.SetActive(!flag);
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0007E6DB File Offset: 0x0007CADB
		[ConsoleCommand("")]
		private static void SetVisible(bool visible)
		{
			IPhoneScreenGuide.instance.gameObject.SetActive(visible);
			PlayerPrefs.SetInt("IPhoneScreenGuide", (!visible) ? 0 : 1);
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0007E704 File Offset: 0x0007CB04
		private void SwapNotch_Impl()
		{
			bool flag = !this.leftNotch.activeSelf;
			this.leftNotch.SetActive(flag);
			this.rightNotch.SetActive(!flag);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0007E73B File Offset: 0x0007CB3B
		public void HandleClick()
		{
		}

		// Token: 0x04001916 RID: 6422
		private const string playerPrefsId = "IPhoneScreenGuide";

		// Token: 0x04001917 RID: 6423
		private static IPhoneScreenGuide instance;

		// Token: 0x04001918 RID: 6424
		[SerializeField]
		private GameObject leftNotch;

		// Token: 0x04001919 RID: 6425
		[SerializeField]
		private GameObject rightNotch;
	}
}
