using System;
using System.Diagnostics;
using RTM.Input;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Voxels.TowerDefense
{
	// Token: 0x0200073D RID: 1853
	public class PointerRationalizer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06003049 RID: 12361 RVA: 0x000C52B0 File Offset: 0x000C36B0
		public PointerRationalizer.State state
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000C52B8 File Offset: 0x000C36B8
		public PointerEventData.InputButton inputButton
		{
			get
			{
				return this._inputButton;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600304B RID: 12363 RVA: 0x000C52C0 File Offset: 0x000C36C0
		private bool unblocked
		{
			get
			{
				return this._unblocked;
			}
		}

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x0600304C RID: 12364 RVA: 0x000C52C8 File Offset: 0x000C36C8
		// (remove) Token: 0x0600304D RID: 12365 RVA: 0x000C5300 File Offset: 0x000C3700
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerRationalizer.State> onStateChanged = delegate(PointerRationalizer.State A_0)
		{
		};

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x0600304E RID: 12366 RVA: 0x000C5338 File Offset: 0x000C3738
		// (remove) Token: 0x0600304F RID: 12367 RVA: 0x000C5370 File Offset: 0x000C3770
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerEventData.InputButton, Vector2> onButtonDown = delegate(PointerEventData.InputButton A_0, Vector2 A_1)
		{
		};

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06003050 RID: 12368 RVA: 0x000C53A8 File Offset: 0x000C37A8
		// (remove) Token: 0x06003051 RID: 12369 RVA: 0x000C53E0 File Offset: 0x000C37E0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerEventData.InputButton, Vector2> onClick = delegate(PointerEventData.InputButton A_0, Vector2 A_1)
		{
		};

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06003052 RID: 12370 RVA: 0x000C5418 File Offset: 0x000C3818
		// (remove) Token: 0x06003053 RID: 12371 RVA: 0x000C5450 File Offset: 0x000C3850
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerEventData.InputButton, Vector2> onDrag = delegate(PointerEventData.InputButton A_0, Vector2 A_1)
		{
		};

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06003054 RID: 12372 RVA: 0x000C5488 File Offset: 0x000C3888
		// (remove) Token: 0x06003055 RID: 12373 RVA: 0x000C54C0 File Offset: 0x000C38C0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerEventData.InputButton> onDragStart = delegate(PointerEventData.InputButton A_0)
		{
		};

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06003056 RID: 12374 RVA: 0x000C54F8 File Offset: 0x000C38F8
		// (remove) Token: 0x06003057 RID: 12375 RVA: 0x000C5530 File Offset: 0x000C3930
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerEventData.InputButton> onDragEnd = delegate(PointerEventData.InputButton A_0)
		{
		};

		// Token: 0x06003058 RID: 12376 RVA: 0x000C5568 File Offset: 0x000C3968
		private void SetState(PointerRationalizer.State state)
		{
			PointerRationalizer.State state2 = this._state;
			this._state = state;
			if (this._state != state2)
			{
				this.onStateChanged(this._state);
			}
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000C55A0 File Offset: 0x000C39A0
		public bool Is(PointerRationalizer.State state)
		{
			return this.state == state;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000C55AB File Offset: 0x000C39AB
		public bool Is(PointerEventData.InputButton button)
		{
			return this.state >= PointerRationalizer.State.ButtonDown && this.inputButton == button;
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000C55C5 File Offset: 0x000C39C5
		private void OnDisable()
		{
			this.SetState(PointerRationalizer.State.None);
			this._unblocked = false;
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000C55D8 File Offset: 0x000C39D8
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (this._state < PointerRationalizer.State.Dragging)
			{
				this.SetState(PointerRationalizer.State.Dragging);
				this._inputButton = inputButton;
				this.onDragStart(this._inputButton);
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000C561C File Offset: 0x000C3A1C
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (inputButton == this._inputButton)
			{
				this.onDrag(this._inputButton, eventData.delta);
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000C5658 File Offset: 0x000C3A58
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (inputButton == this._inputButton)
			{
				this.SetState((!this._unblocked) ? PointerRationalizer.State.None : PointerRationalizer.State.Hover);
				this.onDragEnd(this._inputButton);
			}
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000C56A8 File Offset: 0x000C3AA8
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (this._state < PointerRationalizer.State.ButtonDown)
			{
				this.SetState(PointerRationalizer.State.ButtonDown);
				this._inputButton = inputButton;
				this.onButtonDown(this._inputButton, eventData.position);
			}
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000C56F4 File Offset: 0x000C3AF4
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000C5710 File Offset: 0x000C3B10
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			PointerEventData.InputButton inputButton = InputHelpers.ProcessMouseButton(eventData.button);
			if (this._state == PointerRationalizer.State.ButtonDown && this._inputButton == inputButton)
			{
				this.SetState((!this._unblocked) ? PointerRationalizer.State.None : PointerRationalizer.State.Hover);
				this.onClick(this._inputButton, eventData.position);
			}
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000C5770 File Offset: 0x000C3B70
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			this._unblocked = true;
			if (this._state < PointerRationalizer.State.ButtonDown)
			{
				this.SetState(PointerRationalizer.State.Hover);
			}
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000C578C File Offset: 0x000C3B8C
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			this._unblocked = false;
			if (this._state < PointerRationalizer.State.ButtonDown)
			{
				this.SetState(PointerRationalizer.State.None);
			}
		}

		// Token: 0x04002043 RID: 8259
		[SerializeField]
		private DebugChannelGroup dbgGroup = new DebugChannelGroup("CursorController", EVerbosity.Quiet, 100);

		// Token: 0x04002044 RID: 8260
		private PointerRationalizer.State _state;

		// Token: 0x04002045 RID: 8261
		private PointerEventData.InputButton _inputButton;

		// Token: 0x04002046 RID: 8262
		private bool _unblocked;

		// Token: 0x0200073E RID: 1854
		public enum State
		{
			// Token: 0x04002054 RID: 8276
			None,
			// Token: 0x04002055 RID: 8277
			Hover,
			// Token: 0x04002056 RID: 8278
			ButtonDown,
			// Token: 0x04002057 RID: 8279
			Dragging
		}
	}
}
