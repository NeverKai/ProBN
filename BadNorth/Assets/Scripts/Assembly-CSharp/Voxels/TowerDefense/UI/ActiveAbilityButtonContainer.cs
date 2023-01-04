using System;
using System.Collections.Generic;
using RTM.Pools;
using UnityEngine;
using Voxels.TowerDefense.Upgrades;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000840 RID: 2112
	public class ActiveAbilityButtonContainer : MonoBehaviour
	{
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000EC7D6 File Offset: 0x000EABD6
		private List<ActiveAbilityButton> buttons
		{
			get
			{
				return this.buttonPool.inUse;
			}
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x000EC7E3 File Offset: 0x000EABE3
		public void Init()
		{
			this.buttonPool = new LocalPool<ActiveAbilityButton>(base.GetComponentsInChildren<ActiveAbilityButton>(true), null);
			this.buttonPool.ExpandTo(6);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x000EC804 File Offset: 0x000EAC04
		public void Setup(IslandUIManager islandUiManager, EnglishSquad squad)
		{
			foreach (UpgradeComponent upgradeComponent in squad.upgradeManager)
			{
				ActiveAbility activeAbility = upgradeComponent as ActiveAbility;
				if (activeAbility)
				{
					ActiveAbilityButton activeAbilityButton = this.CreateButtonFor(islandUiManager, activeAbility);
					if (activeAbility is JoystickMoveAbility)
					{
						this.moveButton = activeAbilityButton;
					}
				}
			}
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x000EC884 File Offset: 0x000EAC84
		public int GetActiveButtonIdx()
		{
			int i = 0;
			int count = this.buttons.Count;
			while (i < count)
			{
				if (this.buttons[i].hasFocus)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x000EC8C8 File Offset: 0x000EACC8
		public ActiveAbilityButton GetActiveButton()
		{
			int activeButtonIdx = this.GetActiveButtonIdx();
			if (this.buttons.IsValidIndex(activeButtonIdx))
			{
				return this.buttons[activeButtonIdx];
			}
			return null;
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x000EC8FC File Offset: 0x000EACFC
		public void Navigate(float direction)
		{
			int num = (direction <= 0f) ? -1 : 1;
			int activeButtonIdx = this.GetActiveButtonIdx();
			if (this.buttons.IsValidIndex(activeButtonIdx))
			{
				int i = 1;
				int count = this.buttons.Count;
				while (i < count)
				{
					int index = (activeButtonIdx + count + i * num) % count;
					if (this.buttons[index].CanDisplay())
					{
						ConfirmButton.SetCurrent(this.buttons[index]);
						return;
					}
					i++;
				}
			}
			else
			{
				this.FocusDefault();
			}
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000EC994 File Offset: 0x000EAD94
		public void Select(int idx)
		{
			int num = -1;
			foreach (ActiveAbilityButton activeAbilityButton in this.buttons)
			{
				if (activeAbilityButton.CanDisplay())
				{
					num++;
					if (num == idx)
					{
						if (activeAbilityButton.hasFocus)
						{
							ConfirmButton.SetCurrent(null);
						}
						else
						{
							ConfirmButton.SetCurrent(activeAbilityButton);
						}
						return;
					}
				}
			}
			FabricWrapper.PostEvent(FabricID.uiError);
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x000ECA30 File Offset: 0x000EAE30
		public void FocusDefault()
		{
			int activeButtonIdx = this.GetActiveButtonIdx();
			if (this.buttons.IsValidIndex(activeButtonIdx))
			{
				ConfirmButton.SetCurrent(this.buttons[activeButtonIdx]);
			}
			if (this.moveButton)
			{
				ConfirmButton.SetCurrent(this.moveButton);
			}
			else if (this.buttons.Count > 0)
			{
				ConfirmButton.SetCurrent(this.buttons[0]);
			}
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000ECAA8 File Offset: 0x000EAEA8
		private ActiveAbilityButton CreateButtonFor(IslandUIManager islandUiManager, ActiveAbility ability)
		{
			ActiveAbilityButton result;
			using ("ActiveAbilityButtonContainer.CreateButtonFor()")
			{
				ActiveAbilityButton activeAbilityButton = null;
				using ("GetInstance")
				{
					activeAbilityButton = this.buttonPool.GetInstance();
				}
				using ("SetupButton")
				{
					activeAbilityButton.SetupButton(islandUiManager, ability);
				}
				activeAbilityButton.transform.SetAsLastSibling();
				result = activeAbilityButton;
			}
			return result;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000ECB60 File Offset: 0x000EAF60
		private void UpdateVisibility()
		{
			foreach (ActiveAbilityButton activeAbilityButton in this.buttonPool.inUse)
			{
				activeAbilityButton.gameObject.SetActive(activeAbilityButton.CanDisplay());
			}
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000ECBCC File Offset: 0x000EAFCC
		private void OnEnable()
		{
			this.UpdateVisibility();
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x000ECBD4 File Offset: 0x000EAFD4
		private void Update()
		{
			this.UpdateVisibility();
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x000ECBDC File Offset: 0x000EAFDC
		public void Clear()
		{
			this.buttonPool.ReturnAll();
			this.moveButton = null;
		}

		// Token: 0x04002571 RID: 9585
		private LocalPool<ActiveAbilityButton> buttonPool;

		// Token: 0x04002572 RID: 9586
		private ActiveAbilityButton moveButton;
	}
}
