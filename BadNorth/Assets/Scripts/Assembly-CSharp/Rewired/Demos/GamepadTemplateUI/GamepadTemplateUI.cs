using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x02000481 RID: 1153
	public class GamepadTemplateUI : MonoBehaviour
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x000482E9 File Offset: 0x000466E9
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000482FC File Offset: 0x000466FC
		private void Awake()
		{
			this._uiElementsArray = new GamepadTemplateUI.UIElement[]
			{
				new GamepadTemplateUI.UIElement(0, this.leftStickX),
				new GamepadTemplateUI.UIElement(1, this.leftStickY),
				new GamepadTemplateUI.UIElement(17, this.leftStickButton),
				new GamepadTemplateUI.UIElement(2, this.rightStickX),
				new GamepadTemplateUI.UIElement(3, this.rightStickY),
				new GamepadTemplateUI.UIElement(18, this.rightStickButton),
				new GamepadTemplateUI.UIElement(4, this.actionBottomRow1),
				new GamepadTemplateUI.UIElement(5, this.actionBottomRow2),
				new GamepadTemplateUI.UIElement(6, this.actionBottomRow3),
				new GamepadTemplateUI.UIElement(7, this.actionTopRow1),
				new GamepadTemplateUI.UIElement(8, this.actionTopRow2),
				new GamepadTemplateUI.UIElement(9, this.actionTopRow3),
				new GamepadTemplateUI.UIElement(14, this.center1),
				new GamepadTemplateUI.UIElement(15, this.center2),
				new GamepadTemplateUI.UIElement(16, this.center3),
				new GamepadTemplateUI.UIElement(19, this.dPadUp),
				new GamepadTemplateUI.UIElement(20, this.dPadRight),
				new GamepadTemplateUI.UIElement(21, this.dPadDown),
				new GamepadTemplateUI.UIElement(22, this.dPadLeft),
				new GamepadTemplateUI.UIElement(10, this.leftShoulder),
				new GamepadTemplateUI.UIElement(11, this.leftTrigger),
				new GamepadTemplateUI.UIElement(12, this.rightShoulder),
				new GamepadTemplateUI.UIElement(13, this.rightTrigger)
			};
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElements.Add(this._uiElementsArray[i].id, this._uiElementsArray[i].element);
			}
			this._sticks = new GamepadTemplateUI.Stick[]
			{
				new GamepadTemplateUI.Stick(this.leftStick, 0, 1),
				new GamepadTemplateUI.Stick(this.rightStick, 2, 3)
			};
			ReInput.ControllerConnectedEvent += this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent += this.OnControllerDisconnected;
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00048517 File Offset: 0x00046917
		private void Start()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawLabels();
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0004852A File Offset: 0x0004692A
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerDisconnected;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0004854E File Offset: 0x0004694E
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawActiveElements();
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00048564 File Offset: 0x00046964
		private void DrawActiveElements()
		{
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElementsArray[i].element.Deactivate();
			}
			for (int j = 0; j < this._sticks.Length; j++)
			{
				this._sticks[j].Reset();
			}
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int k = 0; k < actions.Count; k++)
			{
				this.ActivateElements(this.player, actions[k].id);
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000485FC File Offset: 0x000469FC
		private void ActivateElements(Player player, int actionId)
		{
			float axis = player.GetAxis(actionId);
			if (axis == 0f)
			{
				return;
			}
			IList<InputActionSourceData> currentInputSources = player.GetCurrentInputSources(actionId);
			for (int i = 0; i < currentInputSources.Count; i++)
			{
				InputActionSourceData inputActionSourceData = currentInputSources[i];
				IGamepadTemplate template = inputActionSourceData.controller.GetTemplate<IGamepadTemplate>();
				if (template != null)
				{
					template.GetElementTargets(inputActionSourceData.actionElementMap, this._tempTargetList);
					for (int j = 0; j < this._tempTargetList.Count; j++)
					{
						ControllerTemplateElementTarget controllerTemplateElementTarget = this._tempTargetList[j];
						int id = controllerTemplateElementTarget.element.id;
						ControllerUIElement controllerUIElement = this._uiElements[id];
						if (controllerTemplateElementTarget.elementType == ControllerTemplateElementType.Axis)
						{
							controllerUIElement.Activate(axis);
						}
						else if (controllerTemplateElementTarget.elementType == ControllerTemplateElementType.Button && (player.GetButton(actionId) || player.GetNegativeButton(actionId)))
						{
							controllerUIElement.Activate(1f);
						}
						GamepadTemplateUI.Stick stick = this.GetStick(id);
						if (stick != null)
						{
							stick.SetAxisPosition(id, axis * 20f);
						}
					}
				}
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00048730 File Offset: 0x00046B30
		private void DrawLabels()
		{
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElementsArray[i].element.ClearLabels();
			}
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int j = 0; j < actions.Count; j++)
			{
				this.DrawLabels(this.player, actions[j]);
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000487A0 File Offset: 0x00046BA0
		private void DrawLabels(Player player, InputAction action)
		{
			Controller firstControllerWithTemplate = player.controllers.GetFirstControllerWithTemplate<IGamepadTemplate>();
			if (firstControllerWithTemplate == null)
			{
				return;
			}
			IGamepadTemplate template = firstControllerWithTemplate.GetTemplate<IGamepadTemplate>();
			ControllerMap map = player.controllers.maps.GetMap(firstControllerWithTemplate, "Default", "Default");
			if (map == null)
			{
				return;
			}
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				ControllerUIElement element = this._uiElementsArray[i].element;
				int id = this._uiElementsArray[i].id;
				IControllerTemplateElement element2 = template.GetElement(id);
				this.DrawLabel(element, action, map, template, element2);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00048838 File Offset: 0x00046C38
		private void DrawLabel(ControllerUIElement uiElement, InputAction action, ControllerMap controllerMap, IControllerTemplate template, IControllerTemplateElement element)
		{
			if (element.source == null)
			{
				return;
			}
			if (element.source.type == ControllerTemplateElementSourceType.Axis)
			{
				IControllerTemplateAxisSource controllerTemplateAxisSource = element.source as IControllerTemplateAxisSource;
				if (controllerTemplateAxisSource.splitAxis)
				{
					ActionElementMap firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.positiveTarget, action.id, true);
					if (firstElementMapWithElementTarget != null)
					{
						uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Positive);
					}
					firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.negativeTarget, action.id, true);
					if (firstElementMapWithElementTarget != null)
					{
						uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Negative);
					}
				}
				else
				{
					ActionElementMap firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.fullTarget, action.id, true);
					if (firstElementMapWithElementTarget != null)
					{
						uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Full);
					}
					else
					{
						firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(new ControllerElementTarget(controllerTemplateAxisSource.fullTarget)
						{
							axisRange = AxisRange.Positive
						}, action.id, true);
						if (firstElementMapWithElementTarget != null)
						{
							uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Positive);
						}
						firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(new ControllerElementTarget(controllerTemplateAxisSource.fullTarget)
						{
							axisRange = AxisRange.Negative
						}, action.id, true);
						if (firstElementMapWithElementTarget != null)
						{
							uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Negative);
						}
					}
				}
			}
			else if (element.source.type == ControllerTemplateElementSourceType.Button)
			{
				IControllerTemplateButtonSource controllerTemplateButtonSource = element.source as IControllerTemplateButtonSource;
				ActionElementMap firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateButtonSource.target, action.id, true);
				if (firstElementMapWithElementTarget != null)
				{
					uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Full);
				}
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000489B4 File Offset: 0x00046DB4
		private GamepadTemplateUI.Stick GetStick(int elementId)
		{
			for (int i = 0; i < this._sticks.Length; i++)
			{
				if (this._sticks[i].ContainsElement(elementId))
				{
					return this._sticks[i];
				}
			}
			return null;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000489F7 File Offset: 0x00046DF7
		private void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000489FF File Offset: 0x00046DFF
		private void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x04001075 RID: 4213
		private const float stickRadius = 20f;

		// Token: 0x04001076 RID: 4214
		public int playerId;

		// Token: 0x04001077 RID: 4215
		[SerializeField]
		private RectTransform leftStick;

		// Token: 0x04001078 RID: 4216
		[SerializeField]
		private RectTransform rightStick;

		// Token: 0x04001079 RID: 4217
		[SerializeField]
		private ControllerUIElement leftStickX;

		// Token: 0x0400107A RID: 4218
		[SerializeField]
		private ControllerUIElement leftStickY;

		// Token: 0x0400107B RID: 4219
		[SerializeField]
		private ControllerUIElement leftStickButton;

		// Token: 0x0400107C RID: 4220
		[SerializeField]
		private ControllerUIElement rightStickX;

		// Token: 0x0400107D RID: 4221
		[SerializeField]
		private ControllerUIElement rightStickY;

		// Token: 0x0400107E RID: 4222
		[SerializeField]
		private ControllerUIElement rightStickButton;

		// Token: 0x0400107F RID: 4223
		[SerializeField]
		private ControllerUIElement actionBottomRow1;

		// Token: 0x04001080 RID: 4224
		[SerializeField]
		private ControllerUIElement actionBottomRow2;

		// Token: 0x04001081 RID: 4225
		[SerializeField]
		private ControllerUIElement actionBottomRow3;

		// Token: 0x04001082 RID: 4226
		[SerializeField]
		private ControllerUIElement actionTopRow1;

		// Token: 0x04001083 RID: 4227
		[SerializeField]
		private ControllerUIElement actionTopRow2;

		// Token: 0x04001084 RID: 4228
		[SerializeField]
		private ControllerUIElement actionTopRow3;

		// Token: 0x04001085 RID: 4229
		[SerializeField]
		private ControllerUIElement leftShoulder;

		// Token: 0x04001086 RID: 4230
		[SerializeField]
		private ControllerUIElement leftTrigger;

		// Token: 0x04001087 RID: 4231
		[SerializeField]
		private ControllerUIElement rightShoulder;

		// Token: 0x04001088 RID: 4232
		[SerializeField]
		private ControllerUIElement rightTrigger;

		// Token: 0x04001089 RID: 4233
		[SerializeField]
		private ControllerUIElement center1;

		// Token: 0x0400108A RID: 4234
		[SerializeField]
		private ControllerUIElement center2;

		// Token: 0x0400108B RID: 4235
		[SerializeField]
		private ControllerUIElement center3;

		// Token: 0x0400108C RID: 4236
		[SerializeField]
		private ControllerUIElement dPadUp;

		// Token: 0x0400108D RID: 4237
		[SerializeField]
		private ControllerUIElement dPadRight;

		// Token: 0x0400108E RID: 4238
		[SerializeField]
		private ControllerUIElement dPadDown;

		// Token: 0x0400108F RID: 4239
		[SerializeField]
		private ControllerUIElement dPadLeft;

		// Token: 0x04001090 RID: 4240
		private GamepadTemplateUI.UIElement[] _uiElementsArray;

		// Token: 0x04001091 RID: 4241
		private Dictionary<int, ControllerUIElement> _uiElements = new Dictionary<int, ControllerUIElement>();

		// Token: 0x04001092 RID: 4242
		private IList<ControllerTemplateElementTarget> _tempTargetList = new List<ControllerTemplateElementTarget>(2);

		// Token: 0x04001093 RID: 4243
		private GamepadTemplateUI.Stick[] _sticks;

		// Token: 0x02000482 RID: 1154
		private class Stick
		{
			// Token: 0x06001A87 RID: 6791 RVA: 0x00048A08 File Offset: 0x00046E08
			public Stick(RectTransform transform, int xAxisElementId, int yAxisElementId)
			{
				if (transform == null)
				{
					return;
				}
				this._transform = transform;
				this._origPosition = this._transform.anchoredPosition;
				this._xAxisElementId = xAxisElementId;
				this._yAxisElementId = yAxisElementId;
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00048A5C File Offset: 0x00046E5C
			// (set) Token: 0x06001A89 RID: 6793 RVA: 0x00048A8F File Offset: 0x00046E8F
			public Vector2 position
			{
				get
				{
					return (!(this._transform != null)) ? Vector2.zero : (this._transform.anchoredPosition - this._origPosition);
				}
				set
				{
					if (this._transform == null)
					{
						return;
					}
					this._transform.anchoredPosition = this._origPosition + value;
				}
			}

			// Token: 0x06001A8A RID: 6794 RVA: 0x00048ABA File Offset: 0x00046EBA
			public void Reset()
			{
				if (this._transform == null)
				{
					return;
				}
				this._transform.anchoredPosition = this._origPosition;
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x00048ADF File Offset: 0x00046EDF
			public bool ContainsElement(int elementId)
			{
				return !(this._transform == null) && (elementId == this._xAxisElementId || elementId == this._yAxisElementId);
			}

			// Token: 0x06001A8C RID: 6796 RVA: 0x00048B0C File Offset: 0x00046F0C
			public void SetAxisPosition(int elementId, float value)
			{
				if (this._transform == null)
				{
					return;
				}
				Vector2 position = this.position;
				if (elementId == this._xAxisElementId)
				{
					position.x = value;
				}
				else if (elementId == this._yAxisElementId)
				{
					position.y = value;
				}
				this.position = position;
			}

			// Token: 0x04001094 RID: 4244
			private RectTransform _transform;

			// Token: 0x04001095 RID: 4245
			private Vector2 _origPosition;

			// Token: 0x04001096 RID: 4246
			private int _xAxisElementId = -1;

			// Token: 0x04001097 RID: 4247
			private int _yAxisElementId = -1;
		}

		// Token: 0x02000483 RID: 1155
		private class UIElement
		{
			// Token: 0x06001A8D RID: 6797 RVA: 0x00048B66 File Offset: 0x00046F66
			public UIElement(int id, ControllerUIElement element)
			{
				this.id = id;
				this.element = element;
			}

			// Token: 0x04001098 RID: 4248
			public int id;

			// Token: 0x04001099 RID: 4249
			public ControllerUIElement element;
		}
	}
}
