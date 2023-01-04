using System;
using ReflexCLI.Attributes;
using ReflexCLI.User;
using Rewired;
using RTM.Input;
using UnityEngine;
using Voxels.TowerDefense.UI;

namespace Voxels.TowerDefense
{
	// Token: 0x0200073C RID: 1852
	public class GameController : MonoBehaviour, IslandGameplayManager.IAwake, IslandGameplayManager.ISetupIsland
	{
		// Token: 0x0600303C RID: 12348 RVA: 0x000C4E1C File Offset: 0x000C321C
		public void OnAwake(IslandGameplayManager manager)
		{
			this.rwPlayer = ReInput.players.GetPlayer(this.rwPlayerId);
			if (InputHelpers.IsSelectInverted())
			{
				RewiredHelpers.SwapActions(this.rwPlayer, this.SelectButton, this.deselectButton);
			}
			UserSettings.onUpdated += this.OnUserSettingsUpdated;
			this.rwPlayer.controllers.ControllerAddedEvent += delegate(ControllerAssignmentChangedEventArgs x)
			{
				this.UpdateControllerMaps();
			};
			this.OnUserSettingsUpdated(Profile.userSettings);
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000C4EA2 File Offset: 0x000C32A2
		void IslandGameplayManager.ISetupIsland.OnSetup(Island island)
		{
			this.OnUserSettingsUpdated(Profile.userSettings);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000C4EAF File Offset: 0x000C32AF
		private void Update()
		{
			if (!ConsoleStatus.IsConsoleOpen())
			{
				this.CheckSelection();
				this.CheckDeselection();
				this.CheckManualSlomo();
			}
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000C4ED0 File Offset: 0x000C32D0
		private void CheckDeselection()
		{
			if (this.rwPlayer.GetButtonDown(this.deselectButton))
			{
				if (this.squadSelector.selectedSquad)
				{
					bool flag = !InputHelpers.ControllerTypeIs(ControllerType.Joystick) && ConfirmButton.currentConfirmMono && ConfirmButton.currentConfirmMono is ActiveAbilityButton;
					if (flag)
					{
						ConfirmButton.SetCurrent(null);
					}
					else
					{
						this.DeselectSquad();
					}
				}
				else
				{
					FabricWrapper.PostEvent(FabricID.uiError);
				}
			}
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000C4F60 File Offset: 0x000C3360
		private void CheckSelection()
		{
			using ("CheckSelection")
			{
				if (this.rwPlayer.GetButtonDown(this.selectPrevUnitButton))
				{
					this.squadSelector.SelectPrevious();
				}
				else if (this.rwPlayer.GetButtonDown(this.selectNextUnitButton))
				{
					this.squadSelector.SelectNext();
				}
				int i = 0;
				int num = this.selectSquadIdxButtons.Length;
				while (i < num)
				{
					if (this.rwPlayer.GetButtonDown(this.selectSquadIdxButtons[i]))
					{
						if (this.squadSelector.selectedSquadIdx == i)
						{
							this.DeselectSquad();
						}
						else
						{
							this.squadSelector.SelectSquad(i);
						}
					}
					i++;
				}
			}
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000C5050 File Offset: 0x000C3450
		private void DeselectSquad()
		{
			if (this.squadSelector.selectedSquad != null)
			{
				this.squadSelector.SelectSquad(null, false);
				FabricWrapper.PostEvent(this.deselectAudioId);
			}
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000C5084 File Offset: 0x000C3484
		private void CheckManualSlomo()
		{
			if (TimeManager.HasTimeScaleRequest(this))
			{
				if (this.rwPlayer.GetButtonUp(this.manualSlomo))
				{
					TimeManager.RemoveTimeScale(this);
				}
			}
			else if (this.rwPlayer.GetButtonDown(this.manualSlomo))
			{
				TimeManager.RequestTimeScale(this, 0.1f);
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000C50E8 File Offset: 0x000C34E8
		private void OnDisable()
		{
			TimeManager.RemoveTimeScale(this);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000C50F0 File Offset: 0x000C34F0
		private void UpdateControllerMaps()
		{
			if (Profile.userSettings)
			{
				this.OnUserSettingsUpdated(Profile.userSettings);
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000C510C File Offset: 0x000C350C
		private void OnUserSettingsUpdated(UserSettings settings)
		{
			UserSettings.GamepadLayout gamepadLayout = settings.gamepadLayout;
			foreach (JoystickMap joystickMap in this.rwPlayer.controllers.maps.GetAllMapsInCategory<JoystickMap>(0))
			{
				if (joystickMap.layoutId == 1)
				{
					joystickMap.enabled = (gamepadLayout == UserSettings.GamepadLayout.Classic);
				}
				if (joystickMap.layoutId == 6)
				{
					joystickMap.enabled = (gamepadLayout == UserSettings.GamepadLayout.QuickSelect);
				}
			}
		}

		// Token: 0x04002038 RID: 8248
		[SerializeField]
		private SquadSelector squadSelector;

		// Token: 0x04002039 RID: 8249
		[SerializeField]
		private int rwPlayerId;

		// Token: 0x0400203A RID: 8250
		private Player rwPlayer;

		// Token: 0x0400203B RID: 8251
		private RewiredActionReference selectPrevUnitButton = "SelectPreviousSquad";

		// Token: 0x0400203C RID: 8252
		private RewiredActionReference selectNextUnitButton = "SelectNextSquad";

		// Token: 0x0400203D RID: 8253
		private RewiredActionReference[] selectSquadIdxButtons = new RewiredActionReference[]
		{
			"SelectSquad1",
			"SelectSquad2",
			"SelectSquad3",
			"SelectSquad4"
		};

		// Token: 0x0400203E RID: 8254
		private RewiredActionReference SelectButton = "GameplaySelect";

		// Token: 0x0400203F RID: 8255
		private RewiredActionReference deselectButton = "GameplayDeselect";

		// Token: 0x04002040 RID: 8256
		private RewiredActionReference manualSlomo = "ManualSlomo";

		// Token: 0x04002041 RID: 8257
		private FabricEventReference deselectAudioId = "UI/InGame/UnitDeselect";

		// Token: 0x04002042 RID: 8258
		[ConsoleCommand("")]
		public static float gamepadSensitivity = 1f;
	}
}
