using System;
using System.Collections.Generic;
using Rewired;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x02000737 RID: 1847
	public class CursorManager : MonoBehaviour, IGameSetup
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06003017 RID: 12311 RVA: 0x000C4834 File Offset: 0x000C2C34
		private CursorManager.IDragListener dragListener
		{
			get
			{
				return this.dragListeners.Last<CursorManager.IDragListener>();
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x000C4841 File Offset: 0x000C2C41
		private CursorManager.IPointerCursor pointerCursor
		{
			get
			{
				return this.pointerCursors.Last<CursorManager.IPointerCursor>();
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x000C484E File Offset: 0x000C2C4E
		private CursorManager.IJoystickCursor joystickCursor
		{
			get
			{
				return this.joystickCursors.Last<CursorManager.IJoystickCursor>();
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600301A RID: 12314 RVA: 0x000C485B File Offset: 0x000C2C5B
		private bool joystickActive
		{
			get
			{
				return InputHelpers.ControllerTypeIs(ControllerType.Joystick);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x000C4863 File Offset: 0x000C2C63
		private bool pointerActive
		{
			get
			{
				return !this.joystickActive;
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000C4870 File Offset: 0x000C2C70
		private static void Add<T>(T cursor, List<T> stack, bool activeInputType) where T : class, CursorManager.ICursor
		{
			if (activeInputType)
			{
				T t = stack.Last<T>();
				if (t != null)
				{
					t.SetActive(false);
				}
				stack.Add(cursor);
				cursor.SetActive(true);
			}
			else
			{
				stack.Add(cursor);
			}
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000C48C4 File Offset: 0x000C2CC4
		private static void Remove<T>(T cursor, List<T> stack, bool activeInputType) where T : class, CursorManager.ICursor
		{
			T t = stack.Last<T>();
			if (t == cursor)
			{
				stack.Remove(cursor);
				cursor.SetActive(false);
				if (activeInputType)
				{
					t = stack.Last<T>();
					if (t != null)
					{
						t.SetActive(true);
					}
				}
			}
			else
			{
				stack.Remove(cursor);
			}
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000C4932 File Offset: 0x000C2D32
		public void Add(CursorManager.IDragListener listener)
		{
			CursorManager.Add<CursorManager.IDragListener>(listener, this.dragListeners, this.pointerActive);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000C4946 File Offset: 0x000C2D46
		public void Add(CursorManager.IPointerCursor cursor)
		{
			CursorManager.Add<CursorManager.IPointerCursor>(cursor, this.pointerCursors, this.pointerActive);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000C495A File Offset: 0x000C2D5A
		public void Add(CursorManager.IJoystickCursor cursor)
		{
			CursorManager.Add<CursorManager.IJoystickCursor>(cursor, this.joystickCursors, this.joystickActive);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000C496E File Offset: 0x000C2D6E
		public void Remove(CursorManager.IDragListener listener)
		{
			CursorManager.Remove<CursorManager.IDragListener>(listener, this.dragListeners, this.pointerActive);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000C4982 File Offset: 0x000C2D82
		public void Remove(CursorManager.IPointerCursor cursor)
		{
			CursorManager.Remove<CursorManager.IPointerCursor>(cursor, this.pointerCursors, this.pointerActive);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000C4996 File Offset: 0x000C2D96
		public void Remove(CursorManager.IJoystickCursor cursor)
		{
			CursorManager.Remove<CursorManager.IJoystickCursor>(cursor, this.joystickCursors, this.joystickActive);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000C49AA File Offset: 0x000C2DAA
		public bool Contains(CursorManager.IDragListener listener)
		{
			return this.dragListeners.Contains(listener);
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000C49B8 File Offset: 0x000C2DB8
		public bool Contains(CursorManager.IPointerCursor pointer)
		{
			return this.pointerCursors.Contains(pointer);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000C49C6 File Offset: 0x000C2DC6
		public bool Contains(CursorManager.IJoystickCursor joyStick)
		{
			return this.joystickCursors.Contains(joyStick);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000C49D4 File Offset: 0x000C2DD4
		void IGameSetup.OnGameAwake()
		{
			this.pointer.onDragStart += this.OnDragStart;
			this.pointer.onDragEnd += this.OnDragEnd;
			this.pointer.onDrag += this.OnDrag;
			this.pointer.onButtonDown += this.OnButtonDown;
			this.pointer.onClick += this.OnButtonUp;
			this.rwPlayer = ReInput.players.GetPlayer(this.rwPlayerId);
			this.joystickXaxis = ReInput.mapping.GetActionId(this.joystickXAxisName);
			this.joystickYAxis = ReInput.mapping.GetActionId(this.joystickYAxisName);
			this.joystickSelectAction = ReInput.mapping.GetActionId(this.joystickSelectActionName);
			InputHelpers.onControllerTypeChanged += this.OnControllerTypeChanged;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000C4AC0 File Offset: 0x000C2EC0
		private void OnControllerTypeChanged(ControllerType obj)
		{
			if (this.dragListener != null)
			{
				this.dragListener.SetActive(this.pointerActive);
			}
			if (this.pointerCursor != null)
			{
				this.pointerCursor.SetActive(this.pointerActive);
			}
			if (this.joystickCursor != null)
			{
				this.joystickCursor.SetActive(this.joystickActive);
			}
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000C4B24 File Offset: 0x000C2F24
		private void Update()
		{
			PointerRationalizer.State state = this.pointer.state;
			Texture2D texture = null;
			Vector2 zero = Vector2.zero;
			if (this.pointerActive)
			{
				if (this.pointerCursor != null)
				{
					this.pointerCursor.UpdateHoverTarget(state, Input.mousePosition);
					if (state > PointerRationalizer.State.None && state < PointerRationalizer.State.Dragging)
					{
						this.pointerCursor.OverrideCursorTexture(state, ref texture, ref zero);
					}
				}
				if (this.dragListener != null && state == PointerRationalizer.State.Dragging)
				{
					this.dragListener.OverrideCursorTexture(state, ref texture, ref zero);
				}
				CursorWrapper.SetCursor(texture, zero, CursorMode.Auto);
			}
			if (this.joystickActive)
			{
				if (this.joystickCursor != null)
				{
					if (this.rwPlayer.GetButtonDown(this.joystickSelectAction))
					{
						this.joystickCursor.OnSelectButtonDown();
					}
					else if (this.rwPlayer.GetButtonUp(this.joystickSelectAction))
					{
						this.joystickCursor.OnSelectButtonUp();
					}
				}
				if (this.joystickCursor != null)
				{
					Vector2 axis2D = this.rwPlayer.GetAxis2D(this.joystickXaxis, this.joystickYAxis);
					this.joystickCursor.SetMoveInput(axis2D);
				}
			}
			for (int i = this.dragListeners.Count - 2; i >= 0; i--)
			{
			}
			for (int j = this.pointerCursors.Count - 2; j >= 0; j--)
			{
			}
			for (int k = this.joystickCursors.Count - 2; k >= 0; k--)
			{
			}
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000C4CAE File Offset: 0x000C30AE
		private void OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos)
		{
			if (this.pointerCursor != null && this.pointerActive)
			{
				this.pointerCursor.OnButtonDown(button, screenPos);
			}
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000C4CD3 File Offset: 0x000C30D3
		private void OnButtonUp(PointerEventData.InputButton button, Vector2 screenPos)
		{
			if (this.pointerCursor != null && this.pointerActive)
			{
				this.pointerCursor.OnButtonUp(button, screenPos);
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000C4CF8 File Offset: 0x000C30F8
		private void OnDragStart(PointerEventData.InputButton button)
		{
			if (this.dragListener != null && this.pointerActive)
			{
				this.dragListener.OnDragStart(button);
			}
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000C4D1C File Offset: 0x000C311C
		private void OnDragEnd(PointerEventData.InputButton button)
		{
			if (this.dragListener != null && this.pointerActive)
			{
				this.dragListener.OnDragEnd(button);
			}
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000C4D40 File Offset: 0x000C3140
		private void OnDrag(PointerEventData.InputButton button, Vector2 delta)
		{
			if (this.dragListener != null && this.pointerActive)
			{
				this.dragListener.OnDrag(button, delta);
			}
		}

		// Token: 0x0400202B RID: 8235
		[SerializeField]
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CursorManager", EVerbosity.Quiet, 0);

		// Token: 0x0400202C RID: 8236
		public PointerRationalizer pointer;

		// Token: 0x0400202D RID: 8237
		[SerializeField]
		private string joystickXAxisName = "UIHorizontal";

		// Token: 0x0400202E RID: 8238
		[SerializeField]
		private string joystickYAxisName = "UIVertical";

		// Token: 0x0400202F RID: 8239
		[SerializeField]
		private string joystickSelectActionName = "UISubmit";

		// Token: 0x04002030 RID: 8240
		[Header("Rewired")]
		[SerializeField]
		private int rwPlayerId;

		// Token: 0x04002031 RID: 8241
		private Player rwPlayer;

		// Token: 0x04002032 RID: 8242
		private List<CursorManager.IDragListener> dragListeners = new List<CursorManager.IDragListener>();

		// Token: 0x04002033 RID: 8243
		private List<CursorManager.IPointerCursor> pointerCursors = new List<CursorManager.IPointerCursor>();

		// Token: 0x04002034 RID: 8244
		private List<CursorManager.IJoystickCursor> joystickCursors = new List<CursorManager.IJoystickCursor>();

		// Token: 0x04002035 RID: 8245
		private int joystickXaxis = -1;

		// Token: 0x04002036 RID: 8246
		private int joystickYAxis = -1;

		// Token: 0x04002037 RID: 8247
		private int joystickSelectAction = -1;

		// Token: 0x02000738 RID: 1848
		public interface ICursor
		{
			// Token: 0x0600302F RID: 12335
			void SetActive(bool active);
		}

		// Token: 0x02000739 RID: 1849
		public interface IDragListener : CursorManager.ICursor
		{
			// Token: 0x06003030 RID: 12336
			void OnDragStart(PointerEventData.InputButton button);

			// Token: 0x06003031 RID: 12337
			void OnDragEnd(PointerEventData.InputButton button);

			// Token: 0x06003032 RID: 12338
			void OnDrag(PointerEventData.InputButton button, Vector2 delta);

			// Token: 0x06003033 RID: 12339
			void OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position);
		}

		// Token: 0x0200073A RID: 1850
		public interface IPointerCursor : CursorManager.ICursor
		{
			// Token: 0x06003034 RID: 12340
			void OnButtonDown(PointerEventData.InputButton button, Vector2 screenPos);

			// Token: 0x06003035 RID: 12341
			void OnButtonUp(PointerEventData.InputButton button, Vector2 screenPos);

			// Token: 0x06003036 RID: 12342
			void UpdateHoverTarget(PointerRationalizer.State state, Vector2 screenPos);

			// Token: 0x06003037 RID: 12343
			void OverrideCursorTexture(PointerRationalizer.State state, ref Texture2D texture, ref Vector2 position);
		}

		// Token: 0x0200073B RID: 1851
		public interface IJoystickCursor : CursorManager.ICursor
		{
			// Token: 0x06003038 RID: 12344
			void SetMoveInput(Vector2 input);

			// Token: 0x06003039 RID: 12345
			void OnSelectButtonDown();

			// Token: 0x0600303A RID: 12346
			void OnSelectButtonUp();
		}
	}
}
