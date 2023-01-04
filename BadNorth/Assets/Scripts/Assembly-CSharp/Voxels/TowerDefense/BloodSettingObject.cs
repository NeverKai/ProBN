using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020006C7 RID: 1735
	public class BloodSettingObject : MonoBehaviour
	{
		// Token: 0x06002D14 RID: 11540 RVA: 0x000A7FF0 File Offset: 0x000A63F0
		private void UpdateSettings(UserSettings settings)
		{
			BloodSettingObject.Mode mode = this.mode;
			if (mode != BloodSettingObject.Mode.BloodOn)
			{
				if (mode == BloodSettingObject.Mode.BloodOff)
				{
					base.gameObject.SetActive(!settings.showBlood);
				}
			}
			else
			{
				base.gameObject.SetActive(settings.showBlood);
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000A804A File Offset: 0x000A644A
		private void Awake()
		{
			UserSettings.onUpdated += this.UpdateSettings;
			this.UpdateSettings(Profile.userSettings);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000A8068 File Offset: 0x000A6468
		private void OnDestroy()
		{
			UserSettings.onUpdated -= this.UpdateSettings;
		}

		// Token: 0x04001D94 RID: 7572
		[SerializeField]
		private BloodSettingObject.Mode mode;

		// Token: 0x020006C8 RID: 1736
		private enum Mode
		{
			// Token: 0x04001D96 RID: 7574
			BloodOn,
			// Token: 0x04001D97 RID: 7575
			BloodOff
		}
	}
}
