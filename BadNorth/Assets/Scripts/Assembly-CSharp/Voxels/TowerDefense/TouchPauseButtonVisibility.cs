using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x02000560 RID: 1376
	public class TouchPauseButtonVisibility : MonoBehaviour, IGameSetup
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x0006FA0C File Offset: 0x0006DE0C
		void IGameSetup.OnGameAwake()
		{
			Platform.onPlatformUpdated += this.UpdateVisibility;
			UserSettings.onUpdated += delegate(UserSettings x)
			{
				this.UpdateVisibility();
			};
			this.UpdateVisibility();
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0006FA38 File Offset: 0x0006DE38
		private void UpdateVisibility()
		{
			bool active = Platform.Is(EPlatform.Touchscreen) || (Profile.userSettings != null && Profile.userSettings.cursorBehaviour == UserSettings.CursorBehaviour.Touch);
			base.gameObject.SetActive(active);
		}
	}
}
