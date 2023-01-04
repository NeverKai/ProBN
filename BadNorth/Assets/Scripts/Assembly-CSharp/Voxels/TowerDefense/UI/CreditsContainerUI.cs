using System;
using RTM.UISystem;
using UnityEngine;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000747 RID: 1863
	internal class CreditsContainerUI : UIMenu, IGameSetup
	{
		// Token: 0x060030A0 RID: 12448 RVA: 0x000C6C05 File Offset: 0x000C5005
		void IGameSetup.OnGameAwake()
		{
			CreditsContainerUI.instance = this;
			this.visibility = base.GetComponentInChildren<IUIVisibility>();
			this.visibility.SetVisible(false, true);
			base.gameObject.SetActive(false);
			this.closeMenu = new Action(this.CloseMenu);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000C6C45 File Offset: 0x000C5045
		public static void Show()
		{
			CreditsContainerUI.instance.OpenMenu();
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000C6C51 File Offset: 0x000C5051
		protected override void OnGainedFocus()
		{
			this.visibility.SetVisible(true, false);
			base.OnGainedFocus();
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C6C66 File Offset: 0x000C5066
		public override void OpenMenu()
		{
			base.OpenMenu();
			CreditsUI.onCreditsComplete += CreditsContainerUI.instance.closeMenu;
			base.Invoke("StartCredits", 0.75f);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C6C8D File Offset: 0x000C508D
		public override void CloseMenu()
		{
			base.CancelInvoke("StartCredits");
			CreditsUI.Cancel();
			CreditsUI.onCreditsComplete -= CreditsContainerUI.instance.closeMenu;
			this.visibility.SetVisible(false, false);
			base.CloseMenu();
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000C6CC1 File Offset: 0x000C50C1
		private void StartCredits()
		{
			CreditsUI.Roll(TextAlignment.Center, Color.white);
		}

		// Token: 0x04002076 RID: 8310
		private IUIVisibility visibility;

		// Token: 0x04002077 RID: 8311
		private static CreditsContainerUI instance;

		// Token: 0x04002078 RID: 8312
		private Action closeMenu;
	}
}
