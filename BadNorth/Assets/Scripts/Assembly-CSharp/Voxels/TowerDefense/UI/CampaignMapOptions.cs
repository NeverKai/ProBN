using System;
using UnityEngine;
using Voxels.TowerDefense.UI.MetaInventory;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x020008AD RID: 2221
	public class CampaignMapOptions : GeneratedMenu, IGameSetup
	{
		// Token: 0x06003A17 RID: 14871 RVA: 0x000FF00B File Offset: 0x000FD40B
		public override void OpenMenu()
		{
			if (this.closeTime.isThisFrame)
			{
				return;
			}
			this.visibility.SetVisible(true, false);
			base.OpenMenu();
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000FF031 File Offset: 0x000FD431
		public override void CloseMenu()
		{
			if (this.openTime.isThisFrame)
			{
				return;
			}
			this.visibility.SetVisible(false, false);
			base.CloseMenu();
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000FF057 File Offset: 0x000FD457
		protected override void OnGainedFocus()
		{
			base.OnGainedFocus();
			this.contentsVisibility.SetVisible(true, false);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000FF06C File Offset: 0x000FD46C
		protected override void Initialize()
		{
			base.Initialize();
			this.visibility = base.GetComponent<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			this.contentsVisibility = this.contents.GetComponent<IUIVisibility>();
			this.contentsVisibility.SetVisible(true, true);
			base.gameObject.SetActive(false);
			this.AddButton("UI/PAUSE/RESUME", delegate()
			{
				this.CloseMenu();
				return true;
			}, null, null).SetSuccessAudio(FabricID.uiBack);
			this.AddButton("CHECKPOINT/LOAD/BUTTON", delegate()
			{
				this.CloseMenu();
				return this.mapUI.ReloadCheckpoint(true, 0.35f);
			}, null, this.textIconButtonPrefab).SetVisibilityCallback(new Func<bool>(this.HasCheckpoint));
			this.AddButton("UI/COMMON/SETTINGS", delegate()
			{
				UserSettingsMenu.Open(1f);
				return true;
			}, null, null).SetSuccessAudio("UI/Menu/Settings");
			this.AddButton("META_INVENTORY/TITLE", delegate()
			{
				MetaInventoryMenu.OpenMenuStatic(null, null);
				return true;
			}, null, null).SetSuccessAudio("UI/Menu/Inventory");
			this.AddButton("UI/CAMPAIGN/EXIT_MAIN", new Func<bool>(this.ExitToMainMenu), null, null);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000FF1A4 File Offset: 0x000FD5A4
		private bool ExitToMainMenu()
		{
			return MetaMenuHelpers.ExitToMainMenu();
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000FF1AB File Offset: 0x000FD5AB
		private bool HasCheckpoint()
		{
			return Profile.campaign && Profile.campaign.hasCheckpoint;
		}

		// Token: 0x04002843 RID: 10307
		[SerializeField]
		private Transform contents;

		// Token: 0x04002844 RID: 10308
		[NonSerialized]
		public CampaignMapUI mapUI;

		// Token: 0x04002845 RID: 10309
		private IUIVisibility visibility;

		// Token: 0x04002846 RID: 10310
		private IUIVisibility contentsVisibility;
	}
}
