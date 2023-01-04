using System;
using Rewired;
using RTM.Input;
using RTM.UISystem;
using UnityEngine;
using UnityEngine.UI;

namespace Voxels.TowerDefense.UI
{
	// Token: 0x02000731 RID: 1841
	public class ActiveAbilityUIController : MonoBehaviour
	{
		// Token: 0x06002FCA RID: 12234 RVA: 0x000C3294 File Offset: 0x000C1694
		public void Init(SquadSelectedBanner banner, ActiveAbilityButtonContainer abilities)
		{
			banner.onOpen += this.Banner_onOpen;
			this.abilities = abilities;
			Player player = ReInput.players.GetPlayer(0);
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnNavigate), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ActiveAbilityUIController.uiHorizontal);
			player.AddInputEventDelegate(new Action<InputActionEventData>(this.OnNavigate), UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed, ActiveAbilityUIController.uiHorizontal);
			int i = 0;
			int num = ActiveAbilityUIController.directSelect.Length;
			while (i < num)
			{
				int cp = i;
				player.AddInputEventDelegate(delegate(InputActionEventData x)
				{
					this.OnSelectAbility(cp);
				}, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ActiveAbilityUIController.directSelect[i]);
				i++;
			}
			UserSettings.onUpdated += delegate(UserSettings s)
			{
				this.UpdateDpadVisibility();
			};
			InputHelpers.onControllerTypeChanged += this.OnControllerTypeChanged;
			this.UpdateDpadVisibility();
			this.SetIcons();
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000C337F File Offset: 0x000C177F
		private void Banner_onOpen()
		{
			this.OnControllerTypeChanged(InputHelpers.GetControllerType());
			this.dpadPromptAnim.localPosition = Vector3.zero;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000C339C File Offset: 0x000C179C
		private void OnControllerTypeChanged(ControllerType type)
		{
			if (base.isActiveAndEnabled)
			{
				bool flag = type == ControllerType.Joystick;
				if (flag && !this.abilities.GetActiveButton())
				{
					this.abilities.FocusDefault();
				}
				this.UpdateDpadVisibility();
				this.SetIcons();
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000C33EC File Offset: 0x000C17EC
		private void UpdateDpadVisibility()
		{
			bool active = InputHelpers.ControllerTypeIs(ControllerType.Joystick) && Profile.userSettings.gamepadLayout == UserSettings.GamepadLayout.Classic;
			this.dpadPrompt.SetActive(active);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000C3424 File Offset: 0x000C1824
		private void OnNavigate(InputActionEventData data)
		{
			float num = Mathf.Sign(data.GetAxis());
			if (base.isActiveAndEnabled)
			{
				this.abilities.Navigate(num);
			}
			this.dpadPromptAnim.localPosition = Vector3.right * num * 3f;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000C3475 File Offset: 0x000C1875
		private void OnSelectAbility(int idx)
		{
			if (base.isActiveAndEnabled)
			{
				this.abilities.Select(idx);
			}
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000C348E File Offset: 0x000C188E
		private void Update()
		{
			this.dpadPromptAnim.localPosition = Vector3.MoveTowards(this.dpadPromptAnim.localPosition, Vector3.zero, Time.unscaledDeltaTime * 20f);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000C34BC File Offset: 0x000C18BC
		private void SetIcons()
		{
			GamepadIconCollection iconCollection = Singleton<UIManager>.instance.GetIconCollection();
			this.dpadPromptImage.sprite = iconCollection.dPadLeftRight;
		}

		// Token: 0x04001FEF RID: 8175
		private static RewiredActionReference uiHorizontal = "NavigateAbilities";

		// Token: 0x04001FF0 RID: 8176
		private static RewiredActionReference[] directSelect = new RewiredActionReference[]
		{
			"ActiveAbility1",
			"ActiveAbility2",
			"ActiveAbility3",
			"ActiveAbility4",
			"ActiveAbility5",
			"ActiveAbility6"
		};

		// Token: 0x04001FF1 RID: 8177
		private ActiveAbilityButtonContainer abilities;

		// Token: 0x04001FF2 RID: 8178
		[SerializeField]
		private GameObject dpadPrompt;

		// Token: 0x04001FF3 RID: 8179
		[SerializeField]
		private Transform dpadPromptAnim;

		// Token: 0x04001FF4 RID: 8180
		[SerializeField]
		private Image dpadPromptImage;
	}
}
