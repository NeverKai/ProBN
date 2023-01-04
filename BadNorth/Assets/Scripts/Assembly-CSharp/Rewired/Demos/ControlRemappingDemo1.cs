using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000468 RID: 1128
	[AddComponentMenu("")]
	public class ControlRemappingDemo1 : MonoBehaviour
	{
		// Token: 0x060019A0 RID: 6560 RVA: 0x000438B9 File Offset: 0x00041CB9
		private void Awake()
		{
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			this.Initialize();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000438F8 File Offset: 0x00041CF8
		private void OnEnable()
		{
			this.Subscribe();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00043900 File Offset: 0x00041D00
		private void OnDisable()
		{
			this.Unsubscribe();
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00043908 File Offset: 0x00041D08
		private void Initialize()
		{
			this.dialog = new ControlRemappingDemo1.DialogHelper();
			this.actionQueue = new Queue<ControlRemappingDemo1.QueueEntry>();
			this.selectedController = new ControlRemappingDemo1.ControllerSelection();
			ReInput.ControllerConnectedEvent += this.JoystickConnected;
			ReInput.ControllerPreDisconnectEvent += this.JoystickPreDisconnect;
			ReInput.ControllerDisconnectedEvent += this.JoystickDisconnected;
			this.ResetAll();
			this.initialized = true;
			ReInput.userDataStore.Load();
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00043990 File Offset: 0x00041D90
		private void Setup()
		{
			if (this.setupFinished)
			{
				return;
			}
			this.style_wordWrap = new GUIStyle(GUI.skin.label);
			this.style_wordWrap.wordWrap = true;
			this.style_centeredBox = new GUIStyle(GUI.skin.box);
			this.style_centeredBox.alignment = TextAnchor.MiddleCenter;
			this.setupFinished = true;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000439F2 File Offset: 0x00041DF2
		private void Subscribe()
		{
			this.Unsubscribe();
			this.inputMapper.ConflictFoundEvent += this.OnConflictFound;
			this.inputMapper.StoppedEvent += this.OnStopped;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00043A28 File Offset: 0x00041E28
		private void Unsubscribe()
		{
			this.inputMapper.RemoveAllEventListeners();
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00043A38 File Offset: 0x00041E38
		public void OnGUI()
		{
			if (!this.initialized)
			{
				return;
			}
			this.Setup();
			this.HandleMenuControl();
			if (!this.showMenu)
			{
				this.DrawInitialScreen();
				return;
			}
			this.SetGUIStateStart();
			this.ProcessQueue();
			this.DrawPage();
			this.ShowDialog();
			this.SetGUIStateEnd();
			this.busy = false;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00043A94 File Offset: 0x00041E94
		private void HandleMenuControl()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (Event.current.type == EventType.Layout && ReInput.players.GetSystemPlayer().GetButtonDown("Menu"))
			{
				if (this.showMenu)
				{
					ReInput.userDataStore.Save();
					this.Close();
				}
				else
				{
					this.Open();
				}
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00043B01 File Offset: 0x00041F01
		private void Close()
		{
			this.ClearWorkingVars();
			this.showMenu = false;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00043B10 File Offset: 0x00041F10
		private void Open()
		{
			this.showMenu = true;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00043B1C File Offset: 0x00041F1C
		private void DrawInitialScreen()
		{
			ActionElementMap firstElementMapWithAction = ReInput.players.GetSystemPlayer().controllers.maps.GetFirstElementMapWithAction("Menu", true);
			GUIContent content;
			if (firstElementMapWithAction != null)
			{
				content = new GUIContent("Press " + firstElementMapWithAction.elementIdentifierName + " to open the menu.");
			}
			else
			{
				content = new GUIContent("There is no element assigned to open the menu!");
			}
			GUILayout.BeginArea(this.GetScreenCenteredRect(300f, 50f));
			GUILayout.Box(content, this.style_centeredBox, new GUILayoutOption[]
			{
				GUILayout.ExpandHeight(true),
				GUILayout.ExpandWidth(true)
			});
			GUILayout.EndArea();
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00043BB8 File Offset: 0x00041FB8
		private void DrawPage()
		{
			if (GUI.enabled != this.pageGUIState)
			{
				GUI.enabled = this.pageGUIState;
			}
			Rect screenRect = new Rect(((float)Screen.width - (float)Screen.width * 0.9f) * 0.5f, ((float)Screen.height - (float)Screen.height * 0.9f) * 0.5f, (float)Screen.width * 0.9f, (float)Screen.height * 0.9f);
			GUILayout.BeginArea(screenRect);
			this.DrawPlayerSelector();
			this.DrawJoystickSelector();
			this.DrawMouseAssignment();
			this.DrawControllerSelector();
			this.DrawCalibrateButton();
			this.DrawMapCategories();
			this.actionScrollPos = GUILayout.BeginScrollView(this.actionScrollPos, new GUILayoutOption[0]);
			this.DrawCategoryActions();
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00043C84 File Offset: 0x00042084
		private void DrawPlayerSelector()
		{
			if (ReInput.players.allPlayerCount == 0)
			{
				GUILayout.Label("There are no players.", new GUILayoutOption[0]);
				return;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Players:", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			foreach (Player player in ReInput.players.GetPlayers(true))
			{
				if (this.selectedPlayer == null)
				{
					this.selectedPlayer = player;
				}
				bool flag = player == this.selectedPlayer;
				bool flag2 = GUILayout.Toggle(flag, (!(player.descriptiveName != string.Empty)) ? player.name : player.descriptiveName, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag2 != flag && flag2)
				{
					this.selectedPlayer = player;
					this.selectedController.Clear();
					this.selectedMapCategoryId = -1;
				}
			}
			GUILayout.EndHorizontal();
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00043DB8 File Offset: 0x000421B8
		private void DrawMouseAssignment()
		{
			bool enabled = GUI.enabled;
			if (this.selectedPlayer == null)
			{
				GUI.enabled = false;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Assign Mouse:", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool flag = this.selectedPlayer != null && this.selectedPlayer.controllers.hasMouse;
			bool flag2 = GUILayout.Toggle(flag, "Assign Mouse", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				if (flag2)
				{
					this.selectedPlayer.controllers.hasMouse = true;
					foreach (Player player in ReInput.players.Players)
					{
						if (player != this.selectedPlayer)
						{
							player.controllers.hasMouse = false;
						}
					}
				}
				else
				{
					this.selectedPlayer.controllers.hasMouse = false;
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00043F04 File Offset: 0x00042304
		private void DrawJoystickSelector()
		{
			bool enabled = GUI.enabled;
			if (this.selectedPlayer == null)
			{
				GUI.enabled = false;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Assign Joysticks:", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool flag = this.selectedPlayer == null || this.selectedPlayer.controllers.joystickCount == 0;
			bool flag2 = GUILayout.Toggle(flag, "None", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				this.selectedPlayer.controllers.ClearControllersOfType(ControllerType.Joystick);
				this.ControllerSelectionChanged();
			}
			if (this.selectedPlayer != null)
			{
				foreach (Joystick joystick in ReInput.controllers.Joysticks)
				{
					flag = this.selectedPlayer.controllers.ContainsController(joystick);
					flag2 = GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[]
					{
						GUILayout.ExpandWidth(false)
					});
					if (flag2 != flag)
					{
						this.EnqueueAction(new ControlRemappingDemo1.JoystickAssignmentChange(this.selectedPlayer.id, joystick.id, flag2));
					}
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00044084 File Offset: 0x00042484
		private void DrawControllerSelector()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(15f);
			GUILayout.Label("Controller to Map:", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (!this.selectedController.hasSelection)
			{
				this.selectedController.Set(0, ControllerType.Keyboard);
				this.ControllerSelectionChanged();
			}
			bool flag = this.selectedController.type == ControllerType.Keyboard;
			bool flag2 = GUILayout.Toggle(flag, "Keyboard", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				this.selectedController.Set(0, ControllerType.Keyboard);
				this.ControllerSelectionChanged();
			}
			if (!this.selectedPlayer.controllers.hasMouse)
			{
				GUI.enabled = false;
			}
			flag = (this.selectedController.type == ControllerType.Mouse);
			flag2 = GUILayout.Toggle(flag, "Mouse", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				this.selectedController.Set(0, ControllerType.Mouse);
				this.ControllerSelectionChanged();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			foreach (Joystick joystick in this.selectedPlayer.controllers.Joysticks)
			{
				flag = (this.selectedController.type == ControllerType.Joystick && this.selectedController.id == joystick.id);
				flag2 = GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag2 != flag)
				{
					this.selectedController.Set(joystick.id, ControllerType.Joystick);
					this.ControllerSelectionChanged();
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0004428C File Offset: 0x0004268C
		private void DrawCalibrateButton()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(10f);
			Controller controller = (!this.selectedController.hasSelection) ? null : this.selectedPlayer.controllers.GetController(this.selectedController.type, this.selectedController.id);
			if (controller == null || this.selectedController.type != ControllerType.Joystick)
			{
				GUI.enabled = false;
				GUILayout.Button("Select a controller to calibrate", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			else if (GUILayout.Button("Calibrate " + controller.name, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				Joystick joystick = controller as Joystick;
				if (joystick != null)
				{
					CalibrationMap calibrationMap = joystick.calibrationMap;
					if (calibrationMap != null)
					{
						this.EnqueueAction(new ControlRemappingDemo1.Calibration(this.selectedPlayer, joystick, calibrationMap));
					}
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000443A8 File Offset: 0x000427A8
		private void DrawMapCategories()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			if (!this.selectedController.hasSelection)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(15f);
			GUILayout.Label("Categories:", new GUILayoutOption[0]);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			foreach (InputMapCategory inputMapCategory in ReInput.mapping.UserAssignableMapCategories)
			{
				if (!this.selectedPlayer.controllers.maps.ContainsMapInCategory(this.selectedController.type, inputMapCategory.id))
				{
					GUI.enabled = false;
				}
				else if (this.selectedMapCategoryId < 0)
				{
					this.selectedMapCategoryId = inputMapCategory.id;
					this.selectedMap = this.selectedPlayer.controllers.maps.GetFirstMapInCategory(this.selectedController.type, this.selectedController.id, inputMapCategory.id);
				}
				bool flag = inputMapCategory.id == this.selectedMapCategoryId;
				bool flag2 = GUILayout.Toggle(flag, (!(inputMapCategory.descriptiveName != string.Empty)) ? inputMapCategory.name : inputMapCategory.descriptiveName, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag2 != flag)
				{
					this.selectedMapCategoryId = inputMapCategory.id;
					this.selectedMap = this.selectedPlayer.controllers.maps.GetFirstMapInCategory(this.selectedController.type, this.selectedController.id, inputMapCategory.id);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000445AC File Offset: 0x000429AC
		private void DrawCategoryActions()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			if (this.selectedMapCategoryId < 0)
			{
				return;
			}
			bool enabled = GUI.enabled;
			if (this.selectedMap == null)
			{
				return;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Actions:", new GUILayoutOption[0]);
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(this.selectedMapCategoryId);
			if (mapCategory == null)
			{
				return;
			}
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(mapCategory.name);
			if (actionCategory == null)
			{
				return;
			}
			float width = 150f;
			foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory(actionCategory.id))
			{
				string text = (!(inputAction.descriptiveName != string.Empty)) ? inputAction.name : inputAction.descriptiveName;
				if (inputAction.type == InputActionType.Button)
				{
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.Label(text, new GUILayoutOption[]
					{
						GUILayout.Width(width)
					});
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap in this.selectedMap.AllMaps)
					{
						if (actionElementMap.actionId == inputAction.id)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap, actionElementMap);
						}
					}
					GUILayout.EndHorizontal();
				}
				else if (inputAction.type == InputActionType.Axis)
				{
					if (this.selectedController.type != ControllerType.Keyboard)
					{
						GUILayout.BeginHorizontal(new GUILayoutOption[0]);
						GUILayout.Label(text, new GUILayoutOption[]
						{
							GUILayout.Width(width)
						});
						this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Full, this.selectedController, this.selectedMap);
						foreach (ActionElementMap actionElementMap2 in this.selectedMap.AllMaps)
						{
							if (actionElementMap2.actionId == inputAction.id)
							{
								if (actionElementMap2.elementType != ControllerElementType.Button)
								{
									if (actionElementMap2.axisType != AxisType.Split)
									{
										this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Full, this.selectedController, this.selectedMap, actionElementMap2);
										this.DrawInvertButton(this.selectedPlayer.id, inputAction, Pole.Positive, this.selectedController, this.selectedMap, actionElementMap2);
									}
								}
							}
						}
						GUILayout.EndHorizontal();
					}
					string text2 = (!(inputAction.positiveDescriptiveName != string.Empty)) ? (inputAction.descriptiveName + " +") : inputAction.positiveDescriptiveName;
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.Label(text2, new GUILayoutOption[]
					{
						GUILayout.Width(width)
					});
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap3 in this.selectedMap.AllMaps)
					{
						if (actionElementMap3.actionId == inputAction.id)
						{
							if (actionElementMap3.axisContribution == Pole.Positive)
							{
								if (actionElementMap3.axisType != AxisType.Normal)
								{
									this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap, actionElementMap3);
								}
							}
						}
					}
					GUILayout.EndHorizontal();
					string text3 = (!(inputAction.negativeDescriptiveName != string.Empty)) ? (inputAction.descriptiveName + " -") : inputAction.negativeDescriptiveName;
					GUILayout.BeginHorizontal(new GUILayoutOption[0]);
					GUILayout.Label(text3, new GUILayoutOption[]
					{
						GUILayout.Width(width)
					});
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Negative, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap4 in this.selectedMap.AllMaps)
					{
						if (actionElementMap4.actionId == inputAction.id)
						{
							if (actionElementMap4.axisContribution == Pole.Negative)
							{
								if (actionElementMap4.axisType != AxisType.Normal)
								{
									this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Negative, this.selectedController, this.selectedMap, actionElementMap4);
								}
							}
						}
					}
					GUILayout.EndHorizontal();
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00044B58 File Offset: 0x00042F58
		private void DrawActionAssignmentButton(int playerId, InputAction action, AxisRange actionRange, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap, ActionElementMap elementMap)
		{
			if (GUILayout.Button(elementMap.elementIdentifierName, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false),
				GUILayout.MinWidth(30f)
			}))
			{
				InputMapper.Context context = new InputMapper.Context
				{
					actionId = action.id,
					actionRange = actionRange,
					controllerMap = controllerMap,
					actionElementMapToReplace = elementMap
				};
				this.EnqueueAction(new ControlRemappingDemo1.ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType.ReassignOrRemove, context));
				this.startListening = true;
			}
			GUILayout.Space(4f);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00044BDC File Offset: 0x00042FDC
		private void DrawInvertButton(int playerId, InputAction action, Pole actionAxisContribution, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap, ActionElementMap elementMap)
		{
			bool invert = elementMap.invert;
			bool flag = GUILayout.Toggle(invert, "Invert", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag != invert)
			{
				elementMap.invert = flag;
			}
			GUILayout.Space(10f);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00044C28 File Offset: 0x00043028
		private void DrawAddActionMapButton(int playerId, InputAction action, AxisRange actionRange, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap)
		{
			if (GUILayout.Button("Add...", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				InputMapper.Context context = new InputMapper.Context
				{
					actionId = action.id,
					actionRange = actionRange,
					controllerMap = controllerMap
				};
				this.EnqueueAction(new ControlRemappingDemo1.ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType.Add, context));
				this.startListening = true;
			}
			GUILayout.Space(10f);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00044C94 File Offset: 0x00043094
		private void ShowDialog()
		{
			this.dialog.Update();
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00044CA4 File Offset: 0x000430A4
		private void DrawModalWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.dialog.DrawConfirmButton("Okay");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00044D14 File Offset: 0x00043114
		private void DrawModalWindow_OkayOnly(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.dialog.DrawConfirmButton("Okay");
			GUILayout.EndHorizontal();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00044D74 File Offset: 0x00043174
		private void DrawElementAssignmentWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = this.actionQueue.Peek() as ControlRemappingDemo1.ElementAssignmentChange;
			if (elementAssignmentChange == null)
			{
				this.dialog.Cancel();
				return;
			}
			float num;
			if (!this.dialog.busy)
			{
				if (this.startListening && this.inputMapper.status == InputMapper.Status.Idle)
				{
					this.inputMapper.Start(elementAssignmentChange.context);
					this.startListening = false;
				}
				if (this.conflictFoundEventData != null)
				{
					this.dialog.Confirm();
					return;
				}
				num = this.inputMapper.timeRemaining;
				if (num == 0f)
				{
					this.dialog.Cancel();
					return;
				}
			}
			else
			{
				num = this.inputMapper.options.timeout;
			}
			GUILayout.Label("Assignment will be canceled in " + ((int)Mathf.Ceil(num)).ToString() + "...", this.style_wordWrap, new GUILayoutOption[0]);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00044EA0 File Offset: 0x000432A0
		private void DrawElementAssignmentProtectedConflictWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (!(this.actionQueue.Peek() is ControlRemappingDemo1.ElementAssignmentChange))
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Custom1, "Add");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00044F34 File Offset: 0x00043334
		private void DrawElementAssignmentNormalConflictWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (!(this.actionQueue.Peek() is ControlRemappingDemo1.ElementAssignmentChange))
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Confirm, "Replace");
			GUILayout.FlexibleSpace();
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Custom1, "Add");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00044FE0 File Offset: 0x000433E0
		private void DrawReassignOrRemoveElementAssignmentWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			this.dialog.DrawConfirmButton("Reassign");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton("Remove");
			GUILayout.EndHorizontal();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00045054 File Offset: 0x00043454
		private void DrawFallbackJoystickIdentificationWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			ControlRemappingDemo1.FallbackJoystickIdentification fallbackJoystickIdentification = this.actionQueue.Peek() as ControlRemappingDemo1.FallbackJoystickIdentification;
			if (fallbackJoystickIdentification == null)
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.Label("Press any button or axis on \"" + fallbackJoystickIdentification.joystickName + "\" now.", this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Skip", new GUILayoutOption[0]))
			{
				this.dialog.Cancel();
				return;
			}
			if (this.dialog.busy)
			{
				return;
			}
			if (!ReInput.controllers.SetUnityJoystickIdFromAnyButtonOrAxisPress(fallbackJoystickIdentification.joystickId, 0.8f, false))
			{
				return;
			}
			this.dialog.Confirm();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00045138 File Offset: 0x00043538
		private void DrawCalibrationWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			ControlRemappingDemo1.Calibration calibration = this.actionQueue.Peek() as ControlRemappingDemo1.Calibration;
			if (calibration == null)
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, new GUILayoutOption[0]);
			GUILayout.Space(20f);
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			bool enabled = GUI.enabled;
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			this.calibrateScrollPos = GUILayout.BeginScrollView(this.calibrateScrollPos, new GUILayoutOption[0]);
			if (calibration.recording)
			{
				GUI.enabled = false;
			}
			IList<ControllerElementIdentifier> axisElementIdentifiers = calibration.joystick.AxisElementIdentifiers;
			for (int i = 0; i < axisElementIdentifiers.Count; i++)
			{
				ControllerElementIdentifier controllerElementIdentifier = axisElementIdentifiers[i];
				bool flag = calibration.selectedElementIdentifierId == controllerElementIdentifier.id;
				bool flag2 = GUILayout.Toggle(flag, controllerElementIdentifier.name, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag != flag2)
				{
					calibration.selectedElementIdentifierId = controllerElementIdentifier.id;
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			if (calibration.selectedElementIdentifierId >= 0)
			{
				float axisRawById = calibration.joystick.GetAxisRawById(calibration.selectedElementIdentifierId);
				GUILayout.Label("Raw Value: " + axisRawById.ToString(), new GUILayoutOption[0]);
				int axisIndexById = calibration.joystick.GetAxisIndexById(calibration.selectedElementIdentifierId);
				AxisCalibration axis = calibration.calibrationMap.GetAxis(axisIndexById);
				GUILayout.Label("Calibrated Value: " + calibration.joystick.GetAxisById(calibration.selectedElementIdentifierId), new GUILayoutOption[0]);
				GUILayout.Label("Zero: " + axis.calibratedZero, new GUILayoutOption[0]);
				GUILayout.Label("Min: " + axis.calibratedMin, new GUILayoutOption[0]);
				GUILayout.Label("Max: " + axis.calibratedMax, new GUILayoutOption[0]);
				GUILayout.Label("Dead Zone: " + axis.deadZone, new GUILayoutOption[0]);
				GUILayout.Space(15f);
				bool flag3 = GUILayout.Toggle(axis.enabled, "Enabled", "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (axis.enabled != flag3)
				{
					axis.enabled = flag3;
				}
				GUILayout.Space(10f);
				bool flag4 = GUILayout.Toggle(calibration.recording, "Record Min/Max", "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag4 != calibration.recording)
				{
					if (flag4)
					{
						axis.calibratedMax = 0f;
						axis.calibratedMin = 0f;
					}
					calibration.recording = flag4;
				}
				if (calibration.recording)
				{
					axis.calibratedMin = Mathf.Min(new float[]
					{
						axis.calibratedMin,
						axisRawById,
						axis.calibratedMin
					});
					axis.calibratedMax = Mathf.Max(new float[]
					{
						axis.calibratedMax,
						axisRawById,
						axis.calibratedMax
					});
					GUI.enabled = false;
				}
				if (GUILayout.Button("Set Zero", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					axis.calibratedZero = axisRawById;
				}
				if (GUILayout.Button("Set Dead Zone", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					axis.deadZone = axisRawById;
				}
				bool flag5 = GUILayout.Toggle(axis.invert, "Invert", "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (axis.invert != flag5)
				{
					axis.invert = flag5;
				}
				GUILayout.Space(10f);
				if (GUILayout.Button("Reset", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					axis.Reset();
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			else
			{
				GUILayout.Label("Select an axis to begin.", new GUILayoutOption[0]);
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			if (calibration.recording)
			{
				GUI.enabled = false;
			}
			if (GUILayout.Button("Close", new GUILayoutOption[0]))
			{
				this.calibrateScrollPos = default(Vector2);
				this.dialog.Confirm();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00045618 File Offset: 0x00043A18
		private void DialogResultCallback(int queueActionId, ControlRemappingDemo1.UserResponse response)
		{
			foreach (ControlRemappingDemo1.QueueEntry queueEntry in this.actionQueue)
			{
				if (queueEntry.id == queueActionId)
				{
					if (response != ControlRemappingDemo1.UserResponse.Cancel)
					{
						queueEntry.Confirm(response);
					}
					else
					{
						queueEntry.Cancel();
					}
					break;
				}
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0004569C File Offset: 0x00043A9C
		private Rect GetScreenCenteredRect(float width, float height)
		{
			return new Rect((float)Screen.width * 0.5f - width * 0.5f, (float)((double)Screen.height * 0.5 - (double)(height * 0.5f)), width, height);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000456D4 File Offset: 0x00043AD4
		private void EnqueueAction(ControlRemappingDemo1.QueueEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			this.busy = true;
			GUI.enabled = false;
			this.actionQueue.Enqueue(entry);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000456F8 File Offset: 0x00043AF8
		private void ProcessQueue()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (this.busy || this.actionQueue.Count == 0)
			{
				return;
			}
			while (this.actionQueue.Count > 0)
			{
				ControlRemappingDemo1.QueueEntry queueEntry = this.actionQueue.Peek();
				bool flag = false;
				switch (queueEntry.queueActionType)
				{
				case ControlRemappingDemo1.QueueActionType.JoystickAssignment:
					flag = this.ProcessJoystickAssignmentChange((ControlRemappingDemo1.JoystickAssignmentChange)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.ElementAssignment:
					flag = this.ProcessElementAssignmentChange((ControlRemappingDemo1.ElementAssignmentChange)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification:
					flag = this.ProcessFallbackJoystickIdentification((ControlRemappingDemo1.FallbackJoystickIdentification)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.Calibrate:
					flag = this.ProcessCalibration((ControlRemappingDemo1.Calibration)queueEntry);
					break;
				}
				if (!flag)
				{
					break;
				}
				this.actionQueue.Dequeue();
			}
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000457DC File Offset: 0x00043BDC
		private bool ProcessJoystickAssignmentChange(ControlRemappingDemo1.JoystickAssignmentChange entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			Player player = ReInput.players.GetPlayer(entry.playerId);
			if (player == null)
			{
				return true;
			}
			if (!entry.assign)
			{
				player.controllers.RemoveController(ControllerType.Joystick, entry.joystickId);
				this.ControllerSelectionChanged();
				return true;
			}
			if (player.controllers.ContainsController(ControllerType.Joystick, entry.joystickId))
			{
				return true;
			}
			bool flag = ReInput.controllers.IsJoystickAssigned(entry.joystickId);
			if (!flag || entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				player.controllers.AddController(ControllerType.Joystick, entry.joystickId, true);
				this.ControllerSelectionChanged();
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Joystick Reassignment",
				message = "This joystick is already assigned to another player. Do you want to reassign this joystick to " + player.descriptiveName + "?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawModalWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00045908 File Offset: 0x00043D08
		private bool ProcessElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			switch (entry.changeType)
			{
			case ControlRemappingDemo1.ElementAssignmentChangeType.Add:
			case ControlRemappingDemo1.ElementAssignmentChangeType.Replace:
				return this.ProcessAddOrReplaceElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.Remove:
				return this.ProcessRemoveElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.ReassignOrRemove:
				return this.ProcessRemoveOrReassignElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.ConflictCheck:
				return this.ProcessElementAssignmentConflictCheck(entry);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00045960 File Offset: 0x00043D60
		private bool ProcessRemoveOrReassignElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.Remove;
				this.actionQueue.Enqueue(elementAssignmentChange);
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange2 = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange2.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.Replace;
				this.actionQueue.Enqueue(elementAssignmentChange2);
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
			{
				title = "Reassign or Remove",
				message = "Do you want to reassign or remove this assignment?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawReassignOrRemoveElementAssignmentWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00045A3C File Offset: 0x00043E3C
		private bool ProcessRemoveElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				entry.context.controllerMap.DeleteElementMap(entry.context.actionElementMapToReplace.id);
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.DeleteAssignmentConfirmation, new ControlRemappingDemo1.WindowProperties
			{
				title = "Remove Assignment",
				message = "Are you sure you want to remove this assignment?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawModalWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00045B04 File Offset: 0x00043F04
		private bool ProcessAddOrReplaceElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				this.inputMapper.Stop();
				return true;
			}
			if (entry.state != ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				string text;
				if (entry.context.controllerMap.controllerType == ControllerType.Keyboard)
				{
					bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
					if (flag)
					{
						text = "Press any key to assign it to this action. You may also use the modifier keys Command, Control, Alt, and Shift. If you wish to assign a modifier key itself to this action, press and hold the key for 1 second.";
					}
					else
					{
						text = "Press any key to assign it to this action. You may also use the modifier keys Control, Alt, and Shift. If you wish to assign a modifier key itself to this action, press and hold the key for 1 second.";
					}
					if (Application.isEditor)
					{
						text += "\n\nNOTE: Some modifier key combinations will not work in the Unity Editor, but they will work in a game build.";
					}
				}
				else if (entry.context.controllerMap.controllerType == ControllerType.Mouse)
				{
					text = "Press any mouse button or axis to assign it to this action.\n\nTo assign mouse movement axes, move the mouse quickly in the direction you want mapped to the action. Slow movements will be ignored.";
				}
				else
				{
					text = "Press any button or axis to assign it to this action.";
				}
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assign",
					message = text,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
				return false;
			}
			if (Event.current.type != EventType.Layout)
			{
				return false;
			}
			if (this.conflictFoundEventData != null)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.ConflictCheck;
				this.actionQueue.Enqueue(elementAssignmentChange);
			}
			return true;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00045C5C File Offset: 0x0004405C
		private bool ProcessElementAssignmentConflictCheck(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				this.inputMapper.Stop();
				return true;
			}
			if (this.conflictFoundEventData == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				if (entry.response == ControlRemappingDemo1.UserResponse.Confirm)
				{
					this.conflictFoundEventData.responseCallback(InputMapper.ConflictResponse.Replace);
				}
				else
				{
					if (entry.response != ControlRemappingDemo1.UserResponse.Custom1)
					{
						throw new NotImplementedException();
					}
					this.conflictFoundEventData.responseCallback(InputMapper.ConflictResponse.Add);
				}
				return true;
			}
			if (this.conflictFoundEventData.isProtected)
			{
				string message = this.conflictFoundEventData.assignment.elementDisplayName + " is already in use and is protected from reassignment. You cannot remove the protected assignment, but you can still assign the action to this element. If you do so, the element will trigger multiple actions when activated.";
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assignment Conflict",
					message = message,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentProtectedConflictWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			}
			else
			{
				string message2 = this.conflictFoundEventData.assignment.elementDisplayName + " is already in use. You may replace the other conflicting assignments, add this assignment anyway which will leave multiple actions assigned to this element, or cancel this assignment.";
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assignment Conflict",
					message = message2,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentNormalConflictWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			}
			return false;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00045E10 File Offset: 0x00044210
		private bool ProcessFallbackJoystickIdentification(ControlRemappingDemo1.FallbackJoystickIdentification entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Joystick Identification Required",
				message = "A joystick has been attached or removed. You will need to identify each joystick by pressing a button on the controller listed below:",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawFallbackJoystickIdentificationWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback), 1f);
			return false;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00045EA8 File Offset: 0x000442A8
		private bool ProcessCalibration(ControlRemappingDemo1.Calibration entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Calibrate Controller",
				message = "Select an axis to calibrate on the " + entry.joystick.name + ".",
				rect = this.GetScreenCenteredRect(450f, 480f),
				windowDrawDelegate = new Action<string, string>(this.DrawCalibrationWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00045F50 File Offset: 0x00044350
		private void PlayerSelectionChanged()
		{
			this.ClearControllerSelection();
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00045F58 File Offset: 0x00044358
		private void ControllerSelectionChanged()
		{
			this.ClearMapSelection();
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00045F60 File Offset: 0x00044360
		private void ClearControllerSelection()
		{
			this.selectedController.Clear();
			this.ClearMapSelection();
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00045F73 File Offset: 0x00044373
		private void ClearMapSelection()
		{
			this.selectedMapCategoryId = -1;
			this.selectedMap = null;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00045F83 File Offset: 0x00044383
		private void ResetAll()
		{
			this.ClearWorkingVars();
			this.initialized = false;
			this.showMenu = false;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00045F9C File Offset: 0x0004439C
		private void ClearWorkingVars()
		{
			this.selectedPlayer = null;
			this.ClearMapSelection();
			this.selectedController.Clear();
			this.actionScrollPos = default(Vector2);
			this.dialog.FullReset();
			this.actionQueue.Clear();
			this.busy = false;
			this.startListening = false;
			this.conflictFoundEventData = null;
			this.inputMapper.Stop();
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00046008 File Offset: 0x00044408
		private void SetGUIStateStart()
		{
			this.guiState = true;
			if (this.busy)
			{
				this.guiState = false;
			}
			this.pageGUIState = (this.guiState && !this.busy && !this.dialog.enabled && !this.dialog.busy);
			if (GUI.enabled != this.guiState)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00046086 File Offset: 0x00044486
		private void SetGUIStateEnd()
		{
			this.guiState = true;
			if (!GUI.enabled)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000460A4 File Offset: 0x000444A4
		private void JoystickConnected(ControllerStatusChangedEventArgs args)
		{
			if (ReInput.controllers.IsControllerAssigned(args.controllerType, args.controllerId))
			{
				foreach (Player player in ReInput.players.AllPlayers)
				{
					if (player.controllers.ContainsController(args.controllerType, args.controllerId))
					{
						ReInput.userDataStore.LoadControllerData(player.id, args.controllerType, args.controllerId);
					}
				}
			}
			else
			{
				ReInput.userDataStore.LoadControllerData(args.controllerType, args.controllerId);
			}
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00046178 File Offset: 0x00044578
		private void JoystickPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (this.selectedController.hasSelection && args.controllerType == this.selectedController.type && args.controllerId == this.selectedController.id)
			{
				this.ClearControllerSelection();
			}
			if (this.showMenu)
			{
				if (ReInput.controllers.IsControllerAssigned(args.controllerType, args.controllerId))
				{
					foreach (Player player in ReInput.players.AllPlayers)
					{
						if (player.controllers.ContainsController(args.controllerType, args.controllerId))
						{
							ReInput.userDataStore.SaveControllerData(player.id, args.controllerType, args.controllerId);
						}
					}
				}
				else
				{
					ReInput.userDataStore.SaveControllerData(args.controllerType, args.controllerId);
				}
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00046290 File Offset: 0x00044690
		private void JoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (this.showMenu)
			{
				this.ClearWorkingVars();
			}
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000462B3 File Offset: 0x000446B3
		private void OnConflictFound(InputMapper.ConflictFoundEventData data)
		{
			this.conflictFoundEventData = data;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000462BC File Offset: 0x000446BC
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.conflictFoundEventData = null;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x000462C8 File Offset: 0x000446C8
		public void IdentifyAllJoysticks()
		{
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			this.ClearWorkingVars();
			this.Open();
			foreach (Joystick joystick in ReInput.controllers.Joysticks)
			{
				this.actionQueue.Enqueue(new ControlRemappingDemo1.FallbackJoystickIdentification(joystick.id, joystick.name));
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00046358 File Offset: 0x00044758
		protected void CheckRecompile()
		{
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0004635A File Offset: 0x0004475A
		private void RecompileWindow(int windowId)
		{
		}

		// Token: 0x04000FCF RID: 4047
		private const float defaultModalWidth = 250f;

		// Token: 0x04000FD0 RID: 4048
		private const float defaultModalHeight = 200f;

		// Token: 0x04000FD1 RID: 4049
		private const float assignmentTimeout = 5f;

		// Token: 0x04000FD2 RID: 4050
		private ControlRemappingDemo1.DialogHelper dialog;

		// Token: 0x04000FD3 RID: 4051
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x04000FD4 RID: 4052
		private InputMapper.ConflictFoundEventData conflictFoundEventData;

		// Token: 0x04000FD5 RID: 4053
		private bool guiState;

		// Token: 0x04000FD6 RID: 4054
		private bool busy;

		// Token: 0x04000FD7 RID: 4055
		private bool pageGUIState;

		// Token: 0x04000FD8 RID: 4056
		private Player selectedPlayer;

		// Token: 0x04000FD9 RID: 4057
		private int selectedMapCategoryId;

		// Token: 0x04000FDA RID: 4058
		private ControlRemappingDemo1.ControllerSelection selectedController;

		// Token: 0x04000FDB RID: 4059
		private ControllerMap selectedMap;

		// Token: 0x04000FDC RID: 4060
		private bool showMenu;

		// Token: 0x04000FDD RID: 4061
		private bool startListening;

		// Token: 0x04000FDE RID: 4062
		private Vector2 actionScrollPos;

		// Token: 0x04000FDF RID: 4063
		private Vector2 calibrateScrollPos;

		// Token: 0x04000FE0 RID: 4064
		private Queue<ControlRemappingDemo1.QueueEntry> actionQueue;

		// Token: 0x04000FE1 RID: 4065
		private bool setupFinished;

		// Token: 0x04000FE2 RID: 4066
		[NonSerialized]
		private bool initialized;

		// Token: 0x04000FE3 RID: 4067
		private bool isCompiling;

		// Token: 0x04000FE4 RID: 4068
		private GUIStyle style_wordWrap;

		// Token: 0x04000FE5 RID: 4069
		private GUIStyle style_centeredBox;

		// Token: 0x02000469 RID: 1129
		private class ControllerSelection
		{
			// Token: 0x060019DC RID: 6620 RVA: 0x0004635C File Offset: 0x0004475C
			public ControllerSelection()
			{
				this.Clear();
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060019DD RID: 6621 RVA: 0x0004636A File Offset: 0x0004476A
			// (set) Token: 0x060019DE RID: 6622 RVA: 0x00046372 File Offset: 0x00044772
			public int id
			{
				get
				{
					return this._id;
				}
				set
				{
					this._idPrev = this._id;
					this._id = value;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060019DF RID: 6623 RVA: 0x00046387 File Offset: 0x00044787
			// (set) Token: 0x060019E0 RID: 6624 RVA: 0x0004638F File Offset: 0x0004478F
			public ControllerType type
			{
				get
				{
					return this._type;
				}
				set
				{
					this._typePrev = this._type;
					this._type = value;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060019E1 RID: 6625 RVA: 0x000463A4 File Offset: 0x000447A4
			public int idPrev
			{
				get
				{
					return this._idPrev;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000463AC File Offset: 0x000447AC
			public ControllerType typePrev
			{
				get
				{
					return this._typePrev;
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060019E3 RID: 6627 RVA: 0x000463B4 File Offset: 0x000447B4
			public bool hasSelection
			{
				get
				{
					return this._id >= 0;
				}
			}

			// Token: 0x060019E4 RID: 6628 RVA: 0x000463C2 File Offset: 0x000447C2
			public void Set(int id, ControllerType type)
			{
				this.id = id;
				this.type = type;
			}

			// Token: 0x060019E5 RID: 6629 RVA: 0x000463D2 File Offset: 0x000447D2
			public void Clear()
			{
				this._id = -1;
				this._idPrev = -1;
				this._type = ControllerType.Joystick;
				this._typePrev = ControllerType.Joystick;
			}

			// Token: 0x04000FE6 RID: 4070
			private int _id;

			// Token: 0x04000FE7 RID: 4071
			private int _idPrev;

			// Token: 0x04000FE8 RID: 4072
			private ControllerType _type;

			// Token: 0x04000FE9 RID: 4073
			private ControllerType _typePrev;
		}

		// Token: 0x0200046A RID: 1130
		private class DialogHelper
		{
			// Token: 0x060019E6 RID: 6630 RVA: 0x000463F0 File Offset: 0x000447F0
			public DialogHelper()
			{
				this.drawWindowDelegate = new Action<int>(this.DrawWindow);
				this.drawWindowFunction = new GUI.WindowFunction(this.drawWindowDelegate.Invoke);
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00046421 File Offset: 0x00044821
			private float busyTimer
			{
				get
				{
					if (!this._busyTimerRunning)
					{
						return 0f;
					}
					return this._busyTime - Time.realtimeSinceStartup;
				}
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00046440 File Offset: 0x00044840
			// (set) Token: 0x060019E9 RID: 6633 RVA: 0x00046448 File Offset: 0x00044848
			public bool enabled
			{
				get
				{
					return this._enabled;
				}
				set
				{
					if (value)
					{
						if (this._type == ControlRemappingDemo1.DialogHelper.DialogType.None)
						{
							return;
						}
						this.StateChanged(0.25f);
					}
					else
					{
						this._enabled = value;
						this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
						this.StateChanged(0.1f);
					}
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060019EA RID: 6634 RVA: 0x00046485 File Offset: 0x00044885
			// (set) Token: 0x060019EB RID: 6635 RVA: 0x0004649A File Offset: 0x0004489A
			public ControlRemappingDemo1.DialogHelper.DialogType type
			{
				get
				{
					if (!this._enabled)
					{
						return ControlRemappingDemo1.DialogHelper.DialogType.None;
					}
					return this._type;
				}
				set
				{
					if (value == ControlRemappingDemo1.DialogHelper.DialogType.None)
					{
						this._enabled = false;
						this.StateChanged(0.1f);
					}
					else
					{
						this._enabled = true;
						this.StateChanged(0.25f);
					}
					this._type = value;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060019EC RID: 6636 RVA: 0x000464D2 File Offset: 0x000448D2
			public bool busy
			{
				get
				{
					return this._busyTimerRunning;
				}
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x000464DA File Offset: 0x000448DA
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback)
			{
				this.StartModal(queueActionId, type, windowProperties, resultCallback, -1f);
			}

			// Token: 0x060019EE RID: 6638 RVA: 0x000464EC File Offset: 0x000448EC
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback, float openBusyDelay)
			{
				this.currentActionId = queueActionId;
				this.windowProperties = windowProperties;
				this.type = type;
				this.resultCallback = resultCallback;
				if (openBusyDelay >= 0f)
				{
					this.StateChanged(openBusyDelay);
				}
			}

			// Token: 0x060019EF RID: 6639 RVA: 0x0004651F File Offset: 0x0004491F
			public void Update()
			{
				this.Draw();
				this.UpdateTimers();
			}

			// Token: 0x060019F0 RID: 6640 RVA: 0x00046530 File Offset: 0x00044930
			public void Draw()
			{
				if (!this._enabled)
				{
					return;
				}
				bool enabled = GUI.enabled;
				GUI.enabled = true;
				GUILayout.Window(this.windowProperties.windowId, this.windowProperties.rect, this.drawWindowFunction, this.windowProperties.title, new GUILayoutOption[0]);
				GUI.FocusWindow(this.windowProperties.windowId);
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x060019F1 RID: 6641 RVA: 0x000465A9 File Offset: 0x000449A9
			public void DrawConfirmButton()
			{
				this.DrawConfirmButton("Confirm");
			}

			// Token: 0x060019F2 RID: 6642 RVA: 0x000465B8 File Offset: 0x000449B8
			public void DrawConfirmButton(string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, new GUILayoutOption[0]))
				{
					this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x060019F3 RID: 6643 RVA: 0x00046605 File Offset: 0x00044A05
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response)
			{
				this.DrawConfirmButton(response, "Confirm");
			}

			// Token: 0x060019F4 RID: 6644 RVA: 0x00046614 File Offset: 0x00044A14
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response, string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, new GUILayoutOption[0]))
				{
					this.Confirm(response);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x060019F5 RID: 6645 RVA: 0x00046661 File Offset: 0x00044A61
			public void DrawCancelButton()
			{
				this.DrawCancelButton("Cancel");
			}

			// Token: 0x060019F6 RID: 6646 RVA: 0x00046670 File Offset: 0x00044A70
			public void DrawCancelButton(string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, new GUILayoutOption[0]))
				{
					this.Cancel();
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x060019F7 RID: 6647 RVA: 0x000466BC File Offset: 0x00044ABC
			public void Confirm()
			{
				this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
			}

			// Token: 0x060019F8 RID: 6648 RVA: 0x000466C5 File Offset: 0x00044AC5
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.resultCallback(this.currentActionId, response);
				this.Close();
			}

			// Token: 0x060019F9 RID: 6649 RVA: 0x000466DF File Offset: 0x00044ADF
			public void Cancel()
			{
				this.resultCallback(this.currentActionId, ControlRemappingDemo1.UserResponse.Cancel);
				this.Close();
			}

			// Token: 0x060019FA RID: 6650 RVA: 0x000466F9 File Offset: 0x00044AF9
			private void DrawWindow(int windowId)
			{
				this.windowProperties.windowDrawDelegate(this.windowProperties.title, this.windowProperties.message);
			}

			// Token: 0x060019FB RID: 6651 RVA: 0x00046721 File Offset: 0x00044B21
			private void UpdateTimers()
			{
				if (this._busyTimerRunning && this.busyTimer <= 0f)
				{
					this._busyTimerRunning = false;
				}
			}

			// Token: 0x060019FC RID: 6652 RVA: 0x00046745 File Offset: 0x00044B45
			private void StartBusyTimer(float time)
			{
				this._busyTime = time + Time.realtimeSinceStartup;
				this._busyTimerRunning = true;
			}

			// Token: 0x060019FD RID: 6653 RVA: 0x0004675B File Offset: 0x00044B5B
			private void Close()
			{
				this.Reset();
				this.StateChanged(0.1f);
			}

			// Token: 0x060019FE RID: 6654 RVA: 0x0004676E File Offset: 0x00044B6E
			private void StateChanged(float delay)
			{
				this.StartBusyTimer(delay);
			}

			// Token: 0x060019FF RID: 6655 RVA: 0x00046777 File Offset: 0x00044B77
			private void Reset()
			{
				this._enabled = false;
				this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
				this.currentActionId = -1;
				this.resultCallback = null;
			}

			// Token: 0x06001A00 RID: 6656 RVA: 0x00046795 File Offset: 0x00044B95
			private void ResetTimers()
			{
				this._busyTimerRunning = false;
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x0004679E File Offset: 0x00044B9E
			public void FullReset()
			{
				this.Reset();
				this.ResetTimers();
			}

			// Token: 0x04000FEA RID: 4074
			private const float openBusyDelay = 0.25f;

			// Token: 0x04000FEB RID: 4075
			private const float closeBusyDelay = 0.1f;

			// Token: 0x04000FEC RID: 4076
			private ControlRemappingDemo1.DialogHelper.DialogType _type;

			// Token: 0x04000FED RID: 4077
			private bool _enabled;

			// Token: 0x04000FEE RID: 4078
			private float _busyTime;

			// Token: 0x04000FEF RID: 4079
			private bool _busyTimerRunning;

			// Token: 0x04000FF0 RID: 4080
			private Action<int> drawWindowDelegate;

			// Token: 0x04000FF1 RID: 4081
			private GUI.WindowFunction drawWindowFunction;

			// Token: 0x04000FF2 RID: 4082
			private ControlRemappingDemo1.WindowProperties windowProperties;

			// Token: 0x04000FF3 RID: 4083
			private int currentActionId;

			// Token: 0x04000FF4 RID: 4084
			private Action<int, ControlRemappingDemo1.UserResponse> resultCallback;

			// Token: 0x0200046B RID: 1131
			public enum DialogType
			{
				// Token: 0x04000FF6 RID: 4086
				None,
				// Token: 0x04000FF7 RID: 4087
				JoystickConflict,
				// Token: 0x04000FF8 RID: 4088
				ElementConflict,
				// Token: 0x04000FF9 RID: 4089
				KeyConflict,
				// Token: 0x04000FFA RID: 4090
				DeleteAssignmentConfirmation = 10,
				// Token: 0x04000FFB RID: 4091
				AssignElement
			}
		}

		// Token: 0x0200046C RID: 1132
		private abstract class QueueEntry
		{
			// Token: 0x06001A02 RID: 6658 RVA: 0x000467AC File Offset: 0x00044BAC
			public QueueEntry(ControlRemappingDemo1.QueueActionType queueActionType)
			{
				this.id = ControlRemappingDemo1.QueueEntry.nextId;
				this.queueActionType = queueActionType;
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x06001A03 RID: 6659 RVA: 0x000467C6 File Offset: 0x00044BC6
			// (set) Token: 0x06001A04 RID: 6660 RVA: 0x000467CE File Offset: 0x00044BCE
			public int id { get; protected set; }

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06001A05 RID: 6661 RVA: 0x000467D7 File Offset: 0x00044BD7
			// (set) Token: 0x06001A06 RID: 6662 RVA: 0x000467DF File Offset: 0x00044BDF
			public ControlRemappingDemo1.QueueActionType queueActionType { get; protected set; }

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06001A07 RID: 6663 RVA: 0x000467E8 File Offset: 0x00044BE8
			// (set) Token: 0x06001A08 RID: 6664 RVA: 0x000467F0 File Offset: 0x00044BF0
			public ControlRemappingDemo1.QueueEntry.State state { get; protected set; }

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000467F9 File Offset: 0x00044BF9
			// (set) Token: 0x06001A0A RID: 6666 RVA: 0x00046801 File Offset: 0x00044C01
			public ControlRemappingDemo1.UserResponse response { get; protected set; }

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0004680C File Offset: 0x00044C0C
			protected static int nextId
			{
				get
				{
					int result = ControlRemappingDemo1.QueueEntry.uidCounter;
					ControlRemappingDemo1.QueueEntry.uidCounter++;
					return result;
				}
			}

			// Token: 0x06001A0C RID: 6668 RVA: 0x0004682C File Offset: 0x00044C2C
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Confirmed;
				this.response = response;
			}

			// Token: 0x06001A0D RID: 6669 RVA: 0x0004683C File Offset: 0x00044C3C
			public void Cancel()
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Canceled;
			}

			// Token: 0x04001000 RID: 4096
			private static int uidCounter;

			// Token: 0x0200046D RID: 1133
			public enum State
			{
				// Token: 0x04001002 RID: 4098
				Waiting,
				// Token: 0x04001003 RID: 4099
				Confirmed,
				// Token: 0x04001004 RID: 4100
				Canceled
			}
		}

		// Token: 0x0200046E RID: 1134
		private class JoystickAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x06001A0E RID: 6670 RVA: 0x00046845 File Offset: 0x00044C45
			public JoystickAssignmentChange(int newPlayerId, int joystickId, bool assign) : base(ControlRemappingDemo1.QueueActionType.JoystickAssignment)
			{
				this.playerId = newPlayerId;
				this.joystickId = joystickId;
				this.assign = assign;
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06001A0F RID: 6671 RVA: 0x00046863 File Offset: 0x00044C63
			// (set) Token: 0x06001A10 RID: 6672 RVA: 0x0004686B File Offset: 0x00044C6B
			public int playerId { get; private set; }

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00046874 File Offset: 0x00044C74
			// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0004687C File Offset: 0x00044C7C
			public int joystickId { get; private set; }

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00046885 File Offset: 0x00044C85
			// (set) Token: 0x06001A14 RID: 6676 RVA: 0x0004688D File Offset: 0x00044C8D
			public bool assign { get; private set; }
		}

		// Token: 0x0200046F RID: 1135
		private class ElementAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x06001A15 RID: 6677 RVA: 0x00046896 File Offset: 0x00044C96
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType changeType, InputMapper.Context context) : base(ControlRemappingDemo1.QueueActionType.ElementAssignment)
			{
				this.changeType = changeType;
				this.context = context;
			}

			// Token: 0x06001A16 RID: 6678 RVA: 0x000468AD File Offset: 0x00044CAD
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange other) : this(other.changeType, other.context.Clone())
			{
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000468C6 File Offset: 0x00044CC6
			// (set) Token: 0x06001A18 RID: 6680 RVA: 0x000468CE File Offset: 0x00044CCE
			public ControlRemappingDemo1.ElementAssignmentChangeType changeType { get; set; }

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000468D7 File Offset: 0x00044CD7
			// (set) Token: 0x06001A1A RID: 6682 RVA: 0x000468DF File Offset: 0x00044CDF
			public InputMapper.Context context { get; private set; }
		}

		// Token: 0x02000470 RID: 1136
		private class FallbackJoystickIdentification : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x06001A1B RID: 6683 RVA: 0x000468E8 File Offset: 0x00044CE8
			public FallbackJoystickIdentification(int joystickId, string joystickName) : base(ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification)
			{
				this.joystickId = joystickId;
				this.joystickName = joystickName;
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06001A1C RID: 6684 RVA: 0x000468FF File Offset: 0x00044CFF
			// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00046907 File Offset: 0x00044D07
			public int joystickId { get; private set; }

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00046910 File Offset: 0x00044D10
			// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00046918 File Offset: 0x00044D18
			public string joystickName { get; private set; }
		}

		// Token: 0x02000471 RID: 1137
		private class Calibration : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x06001A20 RID: 6688 RVA: 0x00046921 File Offset: 0x00044D21
			public Calibration(Player player, Joystick joystick, CalibrationMap calibrationMap) : base(ControlRemappingDemo1.QueueActionType.Calibrate)
			{
				this.player = player;
				this.joystick = joystick;
				this.calibrationMap = calibrationMap;
				this.selectedElementIdentifierId = -1;
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00046946 File Offset: 0x00044D46
			// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0004694E File Offset: 0x00044D4E
			public Player player { get; private set; }

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00046957 File Offset: 0x00044D57
			// (set) Token: 0x06001A24 RID: 6692 RVA: 0x0004695F File Offset: 0x00044D5F
			public ControllerType controllerType { get; private set; }

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00046968 File Offset: 0x00044D68
			// (set) Token: 0x06001A26 RID: 6694 RVA: 0x00046970 File Offset: 0x00044D70
			public Joystick joystick { get; private set; }

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00046979 File Offset: 0x00044D79
			// (set) Token: 0x06001A28 RID: 6696 RVA: 0x00046981 File Offset: 0x00044D81
			public CalibrationMap calibrationMap { get; private set; }

			// Token: 0x04001010 RID: 4112
			public int selectedElementIdentifierId;

			// Token: 0x04001011 RID: 4113
			public bool recording;
		}

		// Token: 0x02000472 RID: 1138
		private struct WindowProperties
		{
			// Token: 0x04001012 RID: 4114
			public int windowId;

			// Token: 0x04001013 RID: 4115
			public Rect rect;

			// Token: 0x04001014 RID: 4116
			public Action<string, string> windowDrawDelegate;

			// Token: 0x04001015 RID: 4117
			public string title;

			// Token: 0x04001016 RID: 4118
			public string message;
		}

		// Token: 0x02000473 RID: 1139
		private enum QueueActionType
		{
			// Token: 0x04001018 RID: 4120
			None,
			// Token: 0x04001019 RID: 4121
			JoystickAssignment,
			// Token: 0x0400101A RID: 4122
			ElementAssignment,
			// Token: 0x0400101B RID: 4123
			FallbackJoystickIdentification,
			// Token: 0x0400101C RID: 4124
			Calibrate
		}

		// Token: 0x02000474 RID: 1140
		private enum ElementAssignmentChangeType
		{
			// Token: 0x0400101E RID: 4126
			Add,
			// Token: 0x0400101F RID: 4127
			Replace,
			// Token: 0x04001020 RID: 4128
			Remove,
			// Token: 0x04001021 RID: 4129
			ReassignOrRemove,
			// Token: 0x04001022 RID: 4130
			ConflictCheck
		}

		// Token: 0x02000475 RID: 1141
		public enum UserResponse
		{
			// Token: 0x04001024 RID: 4132
			Confirm,
			// Token: 0x04001025 RID: 4133
			Cancel,
			// Token: 0x04001026 RID: 4134
			Custom1,
			// Token: 0x04001027 RID: 4135
			Custom2
		}
	}
}
