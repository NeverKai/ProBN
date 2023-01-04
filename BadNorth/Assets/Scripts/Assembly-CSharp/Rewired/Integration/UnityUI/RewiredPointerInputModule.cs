using System;
using System.Collections.Generic;
using System.Text;
using Rewired.UI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000493 RID: 1171
	public abstract class RewiredPointerInputModule : BaseInputModule
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x0004B054 File Offset: 0x00049454
		private RewiredPointerInputModule.UnityInputSource defaultInputSource
		{
			get
			{
				return (this.__m_DefaultInputSource == null) ? (this.__m_DefaultInputSource = new RewiredPointerInputModule.UnityInputSource()) : this.__m_DefaultInputSource;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0004B085 File Offset: 0x00049485
		private IMouseInputSource defaultMouseInputSource
		{
			get
			{
				return this.defaultInputSource;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0004B08D File Offset: 0x0004948D
		protected ITouchInputSource defaultTouchInputSource
		{
			get
			{
				return this.defaultInputSource;
			}
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0004B095 File Offset: 0x00049495
		protected bool IsDefaultMouse(IMouseInputSource mouse)
		{
			return this.defaultMouseInputSource == mouse;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0004B0A0 File Offset: 0x000494A0
		public IMouseInputSource GetMouseInputSource(int playerId, int mouseIndex)
		{
			if (mouseIndex < 0)
			{
				throw new ArgumentOutOfRangeException("mouseIndex");
			}
			if (this.m_MouseInputSourcesList.Count == 0 && this.IsDefaultPlayer(playerId))
			{
				return this.defaultMouseInputSource;
			}
			int count = this.m_MouseInputSourcesList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				IMouseInputSource mouseInputSource = this.m_MouseInputSourcesList[i];
				if (!UnityTools.IsNullOrDestroyed<IMouseInputSource>(mouseInputSource))
				{
					if (mouseInputSource.playerId == playerId)
					{
						if (mouseIndex == num)
						{
							return mouseInputSource;
						}
						num++;
					}
				}
			}
			return null;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0004B13E File Offset: 0x0004953E
		public void RemoveMouseInputSource(IMouseInputSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.m_MouseInputSourcesList.Remove(source);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0004B15E File Offset: 0x0004955E
		public void AddMouseInputSource(IMouseInputSource source)
		{
			if (UnityTools.IsNullOrDestroyed<IMouseInputSource>(source))
			{
				throw new ArgumentNullException("source");
			}
			this.m_MouseInputSourcesList.Add(source);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0004B184 File Offset: 0x00049584
		public int GetMouseInputSourceCount(int playerId)
		{
			if (this.m_MouseInputSourcesList.Count == 0 && this.IsDefaultPlayer(playerId))
			{
				return 1;
			}
			int count = this.m_MouseInputSourcesList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				IMouseInputSource mouseInputSource = this.m_MouseInputSourcesList[i];
				if (!UnityTools.IsNullOrDestroyed<IMouseInputSource>(mouseInputSource))
				{
					if (mouseInputSource.playerId == playerId)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0004B202 File Offset: 0x00049602
		public ITouchInputSource GetTouchInputSource(int playerId, int sourceIndex)
		{
			if (!UnityTools.IsNullOrDestroyed<ITouchInputSource>(this.m_UserDefaultTouchInputSource))
			{
				return this.m_UserDefaultTouchInputSource;
			}
			return this.defaultTouchInputSource;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0004B221 File Offset: 0x00049621
		public void RemoveTouchInputSource(ITouchInputSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (this.m_UserDefaultTouchInputSource == source)
			{
				this.m_UserDefaultTouchInputSource = null;
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0004B247 File Offset: 0x00049647
		public void AddTouchInputSource(ITouchInputSource source)
		{
			if (UnityTools.IsNullOrDestroyed<ITouchInputSource>(source))
			{
				throw new ArgumentNullException("source");
			}
			this.m_UserDefaultTouchInputSource = source;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0004B266 File Offset: 0x00049666
		public int GetTouchInputSourceCount(int playerId)
		{
			return (!this.IsDefaultPlayer(playerId)) ? 0 : 1;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0004B27B File Offset: 0x0004967B
		protected void ClearMouseInputSources()
		{
			this.m_MouseInputSourcesList.Clear();
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0004B288 File Offset: 0x00049688
		protected virtual bool isMouseSupported
		{
			get
			{
				int count = this.m_MouseInputSourcesList.Count;
				if (count == 0)
				{
					return this.defaultMouseInputSource.enabled;
				}
				for (int i = 0; i < count; i++)
				{
					if (this.m_MouseInputSourcesList[i].enabled)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001B00 RID: 6912
		protected abstract bool IsDefaultPlayer(int playerId);

		// Token: 0x06001B01 RID: 6913 RVA: 0x0004B2E0 File Offset: 0x000496E0
		protected bool GetPointerData(int playerId, int pointerIndex, int pointerTypeId, out PlayerPointerEventData data, bool create, PointerEventType pointerEventType)
		{
			Dictionary<int, PlayerPointerEventData>[] array;
			if (!this.m_PlayerPointerData.TryGetValue(playerId, out array))
			{
				array = new Dictionary<int, PlayerPointerEventData>[pointerIndex + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Dictionary<int, PlayerPointerEventData>();
				}
				this.m_PlayerPointerData.Add(playerId, array);
			}
			if (pointerIndex >= array.Length)
			{
				Dictionary<int, PlayerPointerEventData>[] array2 = new Dictionary<int, PlayerPointerEventData>[pointerIndex + 1];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = array[j];
				}
				array2[pointerIndex] = new Dictionary<int, PlayerPointerEventData>();
				array = array2;
				this.m_PlayerPointerData[playerId] = array;
			}
			Dictionary<int, PlayerPointerEventData> dictionary = array[pointerIndex];
			if (!dictionary.TryGetValue(pointerTypeId, out data) && create)
			{
				data = this.CreatePointerEventData(playerId, pointerIndex, pointerTypeId, pointerEventType);
				dictionary.Add(pointerTypeId, data);
				return true;
			}
			data.mouseSource = ((pointerEventType != PointerEventType.Mouse) ? null : this.GetMouseInputSource(playerId, pointerIndex));
			data.touchSource = ((pointerEventType != PointerEventType.Touch) ? null : this.GetTouchInputSource(playerId, pointerIndex));
			return false;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0004B3E8 File Offset: 0x000497E8
		private PlayerPointerEventData CreatePointerEventData(int playerId, int pointerIndex, int pointerTypeId, PointerEventType pointerEventType)
		{
			PlayerPointerEventData playerPointerEventData = new PlayerPointerEventData(base.eventSystem)
			{
				playerId = playerId,
				inputSourceIndex = pointerIndex,
				pointerId = pointerTypeId,
				sourceType = pointerEventType
			};
			if (pointerEventType == PointerEventType.Mouse)
			{
				playerPointerEventData.mouseSource = this.GetMouseInputSource(playerId, pointerIndex);
			}
			else if (pointerEventType == PointerEventType.Touch)
			{
				playerPointerEventData.touchSource = this.GetTouchInputSource(playerId, pointerIndex);
			}
			if (pointerTypeId == -1)
			{
				playerPointerEventData.buttonIndex = 0;
			}
			else if (pointerTypeId == -2)
			{
				playerPointerEventData.buttonIndex = 1;
			}
			else if (pointerTypeId == -3)
			{
				playerPointerEventData.buttonIndex = 2;
			}
			else if (pointerTypeId >= -2147483520 && pointerTypeId <= -2147483392)
			{
				playerPointerEventData.buttonIndex = pointerTypeId - -2147483520;
			}
			return playerPointerEventData;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0004B4B0 File Offset: 0x000498B0
		protected void RemovePointerData(PlayerPointerEventData data)
		{
			Dictionary<int, PlayerPointerEventData>[] array;
			if (this.m_PlayerPointerData.TryGetValue(data.playerId, out array) && data.inputSourceIndex < array.Length)
			{
				array[data.inputSourceIndex].Remove(data.pointerId);
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0004B4F8 File Offset: 0x000498F8
		protected PlayerPointerEventData GetTouchPointerEventData(int playerId, int touchDeviceIndex, Touch input, out bool pressed, out bool released)
		{
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = this.GetPointerData(playerId, touchDeviceIndex, input.fingerId, out playerPointerEventData, true, PointerEventType.Touch);
			playerPointerEventData.Reset();
			pressed = (pointerData || input.phase == TouchPhase.Began);
			released = (input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended);
			if (pointerData)
			{
				playerPointerEventData.position = input.position;
			}
			if (pressed)
			{
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = input.position - playerPointerEventData.position;
			}
			playerPointerEventData.position = input.position;
			playerPointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			return playerPointerEventData;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0004B5DC File Offset: 0x000499DC
		protected virtual RewiredPointerInputModule.MouseState GetMousePointerEventData(int playerId, int mouseIndex)
		{
			IMouseInputSource mouseInputSource = this.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return null;
			}
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = this.GetPointerData(playerId, mouseIndex, -1, out playerPointerEventData, true, PointerEventType.Mouse);
			playerPointerEventData.Reset();
			if (pointerData)
			{
				playerPointerEventData.position = mouseInputSource.screenPosition;
			}
			Vector2 screenPosition = mouseInputSource.screenPosition;
			if (mouseInputSource.locked)
			{
				playerPointerEventData.position = new Vector2(-1f, -1f);
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = screenPosition - playerPointerEventData.position;
				playerPointerEventData.position = screenPosition;
			}
			playerPointerEventData.scrollDelta = mouseInputSource.wheelDelta;
			playerPointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			PlayerPointerEventData playerPointerEventData2;
			this.GetPointerData(playerId, mouseIndex, -2, out playerPointerEventData2, true, PointerEventType.Mouse);
			this.CopyFromTo(playerPointerEventData, playerPointerEventData2);
			playerPointerEventData2.button = PointerEventData.InputButton.Right;
			PlayerPointerEventData playerPointerEventData3;
			this.GetPointerData(playerId, mouseIndex, -3, out playerPointerEventData3, true, PointerEventType.Mouse);
			this.CopyFromTo(playerPointerEventData, playerPointerEventData3);
			playerPointerEventData3.button = PointerEventData.InputButton.Middle;
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				PlayerPointerEventData playerPointerEventData4;
				this.GetPointerData(playerId, mouseIndex, -2147483520 + i, out playerPointerEventData4, true, PointerEventType.Mouse);
				this.CopyFromTo(playerPointerEventData, playerPointerEventData4);
				playerPointerEventData4.button = (PointerEventData.InputButton)(-1);
			}
			this.m_MouseState.SetButtonState(0, this.StateForMouseButton(playerId, mouseIndex, 0), playerPointerEventData);
			this.m_MouseState.SetButtonState(1, this.StateForMouseButton(playerId, mouseIndex, 1), playerPointerEventData2);
			this.m_MouseState.SetButtonState(2, this.StateForMouseButton(playerId, mouseIndex, 2), playerPointerEventData3);
			for (int j = 3; j < mouseInputSource.buttonCount; j++)
			{
				PlayerPointerEventData data;
				this.GetPointerData(playerId, mouseIndex, -2147483520 + j, out data, false, PointerEventType.Mouse);
				this.m_MouseState.SetButtonState(j, this.StateForMouseButton(playerId, mouseIndex, j), data);
			}
			return this.m_MouseState;
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0004B7CC File Offset: 0x00049BCC
		protected PlayerPointerEventData GetLastPointerEventData(int playerId, int pointerIndex, int pointerTypeId, bool ignorePointerTypeId, PointerEventType pointerEventType)
		{
			if (!ignorePointerTypeId)
			{
				PlayerPointerEventData result;
				this.GetPointerData(playerId, pointerIndex, pointerTypeId, out result, false, pointerEventType);
				return result;
			}
			Dictionary<int, PlayerPointerEventData>[] array;
			if (!this.m_PlayerPointerData.TryGetValue(playerId, out array))
			{
				return null;
			}
			if (pointerIndex >= array.Length)
			{
				return null;
			}
			using (Dictionary<int, PlayerPointerEventData>.Enumerator enumerator = array[pointerIndex].GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, PlayerPointerEventData> keyValuePair = enumerator.Current;
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0004B868 File Offset: 0x00049C68
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0004B894 File Offset: 0x00049C94
		protected virtual void ProcessMove(PlayerPointerEventData pointerEvent)
		{
			GameObject newEnterTarget;
			if (pointerEvent.sourceType == PointerEventType.Mouse)
			{
				newEnterTarget = ((!this.GetMouseInputSource(pointerEvent.playerId, pointerEvent.inputSourceIndex).locked) ? pointerEvent.pointerCurrentRaycast.gameObject : null);
			}
			else
			{
				if (pointerEvent.sourceType != PointerEventType.Touch)
				{
					throw new NotImplementedException();
				}
				newEnterTarget = pointerEvent.pointerCurrentRaycast.gameObject;
			}
			base.HandlePointerExitAndEnter(pointerEvent, newEnterTarget);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0004B910 File Offset: 0x00049D10
		protected virtual void ProcessDrag(PlayerPointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (pointerEvent.sourceType == PointerEventType.Mouse && this.GetMouseInputSource(pointerEvent.playerId, pointerEvent.inputSourceIndex).locked)
			{
				return;
			}
			if (!pointerEvent.dragging && RewiredPointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0004BA08 File Offset: 0x00049E08
		public override bool IsPointerOverGameObject(int pointerTypeId)
		{
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				foreach (Dictionary<int, PlayerPointerEventData> dictionary in keyValuePair.Value)
				{
					PlayerPointerEventData playerPointerEventData;
					if (dictionary.TryGetValue(pointerTypeId, out playerPointerEventData))
					{
						if (playerPointerEventData.pointerEnter != null)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0004BAB4 File Offset: 0x00049EB4
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					foreach (KeyValuePair<int, PlayerPointerEventData> keyValuePair2 in value[i])
					{
						base.HandlePointerExitAndEnter(keyValuePair2.Value, null);
					}
					value[i].Clear();
				}
			}
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0004BB98 File Offset: 0x00049F98
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + base.GetType());
			stringBuilder.AppendLine();
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				stringBuilder.AppendLine("<B>Player Id:</b> " + keyValuePair.Key);
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					stringBuilder.AppendLine("<B>Pointer Index:</b> " + i);
					foreach (KeyValuePair<int, PlayerPointerEventData> keyValuePair2 in value[i])
					{
						stringBuilder.AppendLine("<B>Button Id:</b> " + keyValuePair2.Key);
						stringBuilder.AppendLine(keyValuePair2.Value.ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0004BCDC File Offset: 0x0004A0DC
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
			if (eventHandler != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0004BD13 File Offset: 0x0004A113
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0004BD54 File Offset: 0x0004A154
		protected PointerEventData.FramePressState StateForMouseButton(int playerId, int mouseIndex, int buttonId)
		{
			IMouseInputSource mouseInputSource = this.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return PointerEventData.FramePressState.NotChanged;
			}
			bool buttonDown = mouseInputSource.GetButtonDown(buttonId);
			bool buttonUp = mouseInputSource.GetButtonUp(buttonId);
			if (buttonDown && buttonUp)
			{
				return PointerEventData.FramePressState.PressedAndReleased;
			}
			if (buttonDown)
			{
				return PointerEventData.FramePressState.Pressed;
			}
			if (buttonUp)
			{
				return PointerEventData.FramePressState.Released;
			}
			return PointerEventData.FramePressState.NotChanged;
		}

		// Token: 0x040010EF RID: 4335
		public const int kMouseLeftId = -1;

		// Token: 0x040010F0 RID: 4336
		public const int kMouseRightId = -2;

		// Token: 0x040010F1 RID: 4337
		public const int kMouseMiddleId = -3;

		// Token: 0x040010F2 RID: 4338
		public const int kFakeTouchesId = -4;

		// Token: 0x040010F3 RID: 4339
		private const int customButtonsStartingId = -2147483520;

		// Token: 0x040010F4 RID: 4340
		private const int customButtonsMaxCount = 128;

		// Token: 0x040010F5 RID: 4341
		private const int customButtonsLastId = -2147483392;

		// Token: 0x040010F6 RID: 4342
		private readonly List<IMouseInputSource> m_MouseInputSourcesList = new List<IMouseInputSource>();

		// Token: 0x040010F7 RID: 4343
		private Dictionary<int, Dictionary<int, PlayerPointerEventData>[]> m_PlayerPointerData = new Dictionary<int, Dictionary<int, PlayerPointerEventData>[]>();

		// Token: 0x040010F8 RID: 4344
		private ITouchInputSource m_UserDefaultTouchInputSource;

		// Token: 0x040010F9 RID: 4345
		private RewiredPointerInputModule.UnityInputSource __m_DefaultInputSource;

		// Token: 0x040010FA RID: 4346
		private readonly RewiredPointerInputModule.MouseState m_MouseState = new RewiredPointerInputModule.MouseState();

		// Token: 0x02000494 RID: 1172
		protected class MouseState
		{
			// Token: 0x06001B11 RID: 6929 RVA: 0x0004BDB4 File Offset: 0x0004A1B4
			public bool AnyPressesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001B12 RID: 6930 RVA: 0x0004BDFC File Offset: 0x0004A1FC
			public bool AnyReleasesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001B13 RID: 6931 RVA: 0x0004BE44 File Offset: 0x0004A244
			public RewiredPointerInputModule.ButtonState GetButtonState(int button)
			{
				RewiredPointerInputModule.ButtonState buttonState = null;
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						buttonState = this.m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new RewiredPointerInputModule.ButtonState
					{
						button = button,
						eventData = new RewiredPointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			// Token: 0x06001B14 RID: 6932 RVA: 0x0004BEC8 File Offset: 0x0004A2C8
			public void SetButtonState(int button, PointerEventData.FramePressState stateForMouseButton, PlayerPointerEventData data)
			{
				RewiredPointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x040010FB RID: 4347
			private List<RewiredPointerInputModule.ButtonState> m_TrackedButtons = new List<RewiredPointerInputModule.ButtonState>();
		}

		// Token: 0x02000495 RID: 1173
		public class MouseButtonEventData
		{
			// Token: 0x06001B16 RID: 6934 RVA: 0x0004BEFD File Offset: 0x0004A2FD
			public bool PressedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Pressed || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x06001B17 RID: 6935 RVA: 0x0004BF16 File Offset: 0x0004A316
			public bool ReleasedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Released || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x040010FC RID: 4348
			public PointerEventData.FramePressState buttonState;

			// Token: 0x040010FD RID: 4349
			public PlayerPointerEventData buttonData;
		}

		// Token: 0x02000496 RID: 1174
		protected class ButtonState
		{
			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0004BF38 File Offset: 0x0004A338
			// (set) Token: 0x06001B1A RID: 6938 RVA: 0x0004BF40 File Offset: 0x0004A340
			public RewiredPointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0004BF49 File Offset: 0x0004A349
			// (set) Token: 0x06001B1C RID: 6940 RVA: 0x0004BF51 File Offset: 0x0004A351
			public int button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x040010FE RID: 4350
			private int m_Button;

			// Token: 0x040010FF RID: 4351
			private RewiredPointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x02000497 RID: 1175
		private sealed class UnityInputSource : IMouseInputSource, ITouchInputSource
		{
			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0004BF69 File Offset: 0x0004A369
			int IMouseInputSource.playerId
			{
				get
				{
					this.TryUpdate();
					return 0;
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0004BF72 File Offset: 0x0004A372
			int ITouchInputSource.playerId
			{
				get
				{
					this.TryUpdate();
					return 0;
				}
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0004BF7B File Offset: 0x0004A37B
			bool IMouseInputSource.enabled
			{
				get
				{
					this.TryUpdate();
					return true;
				}
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0004BF84 File Offset: 0x0004A384
			bool IMouseInputSource.locked
			{
				get
				{
					this.TryUpdate();
					return Cursor.lockState == CursorLockMode.Locked;
				}
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0004BF94 File Offset: 0x0004A394
			int IMouseInputSource.buttonCount
			{
				get
				{
					this.TryUpdate();
					return 3;
				}
			}

			// Token: 0x06001B23 RID: 6947 RVA: 0x0004BF9D File Offset: 0x0004A39D
			bool IMouseInputSource.GetButtonDown(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButtonDown(button);
			}

			// Token: 0x06001B24 RID: 6948 RVA: 0x0004BFAB File Offset: 0x0004A3AB
			bool IMouseInputSource.GetButtonUp(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButtonUp(button);
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x0004BFB9 File Offset: 0x0004A3B9
			bool IMouseInputSource.GetButton(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButton(button);
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0004BFC7 File Offset: 0x0004A3C7
			Vector2 IMouseInputSource.screenPosition
			{
				get
				{
					this.TryUpdate();
					return Input.mousePosition;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0004BFD9 File Offset: 0x0004A3D9
			Vector2 IMouseInputSource.screenPositionDelta
			{
				get
				{
					this.TryUpdate();
					return this.m_MousePosition - this.m_MousePositionPrev;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0004BFF2 File Offset: 0x0004A3F2
			Vector2 IMouseInputSource.wheelDelta
			{
				get
				{
					this.TryUpdate();
					return Input.mouseScrollDelta;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0004BFFF File Offset: 0x0004A3FF
			bool ITouchInputSource.touchSupported
			{
				get
				{
					this.TryUpdate();
					return Input.touchSupported;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0004C00C File Offset: 0x0004A40C
			int ITouchInputSource.touchCount
			{
				get
				{
					this.TryUpdate();
					return Input.touchCount;
				}
			}

			// Token: 0x06001B2B RID: 6955 RVA: 0x0004C019 File Offset: 0x0004A419
			Touch ITouchInputSource.GetTouch(int index)
			{
				this.TryUpdate();
				return Input.GetTouch(index);
			}

			// Token: 0x06001B2C RID: 6956 RVA: 0x0004C027 File Offset: 0x0004A427
			private void TryUpdate()
			{
				if (Time.frameCount == this.m_LastUpdatedFrame)
				{
					return;
				}
				this.m_LastUpdatedFrame = Time.frameCount;
				this.m_MousePositionPrev = this.m_MousePosition;
				this.m_MousePosition = Input.mousePosition;
			}

			// Token: 0x04001100 RID: 4352
			private Vector2 m_MousePosition;

			// Token: 0x04001101 RID: 4353
			private Vector2 m_MousePositionPrev;

			// Token: 0x04001102 RID: 4354
			private int m_LastUpdatedFrame = -1;
		}
	}
}
