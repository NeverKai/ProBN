using System;
using CS.Platform;
using UnityEngine;
using UnityEngine.UI;
using Voxels.TowerDefense.UI;

namespace CS.VT
{
	// Token: 0x02000392 RID: 914
	public class MenuNameUI : MonoBehaviour
	{
		// Token: 0x060014DB RID: 5339 RVA: 0x0002B473 File Offset: 0x00029873
		private void Awake()
		{
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.tags = base.GetComponentsInChildren<Text>(true);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0002B49B File Offset: 0x0002989B
		private void OnDestroy()
		{
			PlatformEvents.OnMainUserClearedEvent -= this.Hide;
			PlatformEvents.OnMainUserStateEvent -= this.PlatformEvents_OnMainUserStateEvent;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0002B4BF File Offset: 0x000298BF
		private void OnEnable()
		{
			if (BasePlatformManager.Instance == null || !BasePlatformManager.Initialized)
			{
				return;
			}
			if (0 <= BasePlatformManager.Instance.MainUserID)
			{
				this.Show();
			}
			else
			{
				this.Hide();
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0002B4FD File Offset: 0x000298FD
		private void PlatformEvents_OnMainUserStateEvent(bool effect)
		{
			this.Show();
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0002B508 File Offset: 0x00029908
		[ContextMenu("Show")]
		private void Show()
		{
			this.visibility.SetVisible(true, false);
			foreach (Text text in this.tags)
			{
				text.text = BasePlatformManager.Instance.GetUserName();
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0002B551 File Offset: 0x00029951
		[ContextMenu("Hide")]
		private void Hide()
		{
			this.visibility.SetVisible(false, false);
		}

		// Token: 0x04000CF8 RID: 3320
		private Text[] tags;

		// Token: 0x04000CF9 RID: 3321
		private IUIVisibility visibility;
	}
}
