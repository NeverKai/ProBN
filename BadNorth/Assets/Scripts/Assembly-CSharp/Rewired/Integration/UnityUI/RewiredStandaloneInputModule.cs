using System;
using System.Collections.Generic;
using Rewired.Components;
using Rewired.UI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000499 RID: 1177
	[AddComponentMenu("Event/Rewired Standalone Input Module")]
	public sealed class RewiredStandaloneInputModule : RewiredPointerInputModule
	{
		// Token: 0x06001B2D RID: 6957 RVA: 0x0004C064 File Offset: 0x0004A464
		private RewiredStandaloneInputModule()
		{
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0004C0FD File Offset: 0x0004A4FD
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0004C105 File Offset: 0x0004A505
		public InputManager_Base RewiredInputManager
		{
			get
			{
				return this.rewiredInputManager;
			}
			set
			{
				this.rewiredInputManager = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0004C10E File Offset: 0x0004A50E
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0004C118 File Offset: 0x0004A518
		public bool UseAllRewiredGamePlayers
		{
			get
			{
				return this.useAllRewiredGamePlayers;
			}
			set
			{
				bool flag = value != this.useAllRewiredGamePlayers;
				this.useAllRewiredGamePlayers = value;
				if (flag)
				{
					this.SetupRewiredVars();
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0004C145 File Offset: 0x0004A545
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x0004C150 File Offset: 0x0004A550
		public bool UseRewiredSystemPlayer
		{
			get
			{
				return this.useRewiredSystemPlayer;
			}
			set
			{
				bool flag = value != this.useRewiredSystemPlayer;
				this.useRewiredSystemPlayer = value;
				if (flag)
				{
					this.SetupRewiredVars();
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0004C17D File Offset: 0x0004A57D
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0004C18F File Offset: 0x0004A58F
		public int[] RewiredPlayerIds
		{
			get
			{
				return (int[])this.rewiredPlayerIds.Clone();
			}
			set
			{
				this.rewiredPlayerIds = ((value == null) ? new int[0] : ((int[])value.Clone()));
				this.SetupRewiredVars();
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0004C1B9 File Offset: 0x0004A5B9
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0004C1C1 File Offset: 0x0004A5C1
		public bool UsePlayingPlayersOnly
		{
			get
			{
				return this.usePlayingPlayersOnly;
			}
			set
			{
				this.usePlayingPlayersOnly = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0004C1CA File Offset: 0x0004A5CA
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0004C1D7 File Offset: 0x0004A5D7
		public List<PlayerMouse> PlayerMice
		{
			get
			{
				return new List<PlayerMouse>(this.playerMice);
			}
			set
			{
				if (value == null)
				{
					this.playerMice = new List<PlayerMouse>();
					this.SetupRewiredVars();
					return;
				}
				this.playerMice = new List<PlayerMouse>(value);
				this.SetupRewiredVars();
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0004C203 File Offset: 0x0004A603
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0004C20B File Offset: 0x0004A60B
		public bool MoveOneElementPerAxisPress
		{
			get
			{
				return this.moveOneElementPerAxisPress;
			}
			set
			{
				this.moveOneElementPerAxisPress = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0004C214 File Offset: 0x0004A614
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0004C21C File Offset: 0x0004A61C
		public bool allowMouseInput
		{
			get
			{
				return this.m_allowMouseInput;
			}
			set
			{
				this.m_allowMouseInput = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0004C225 File Offset: 0x0004A625
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0004C22D File Offset: 0x0004A62D
		public bool allowMouseInputIfTouchSupported
		{
			get
			{
				return this.m_allowMouseInputIfTouchSupported;
			}
			set
			{
				this.m_allowMouseInputIfTouchSupported = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0004C236 File Offset: 0x0004A636
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x0004C23E File Offset: 0x0004A63E
		public bool allowTouchInput
		{
			get
			{
				return this.m_allowTouchInput;
			}
			set
			{
				this.m_allowTouchInput = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0004C247 File Offset: 0x0004A647
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0004C24F File Offset: 0x0004A64F
		public bool SetActionsById
		{
			get
			{
				return this.setActionsById;
			}
			set
			{
				if (this.setActionsById == value)
				{
					return;
				}
				this.setActionsById = value;
				this.SetupRewiredVars();
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0004C26B File Offset: 0x0004A66B
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x0004C274 File Offset: 0x0004A674
		public int HorizontalActionId
		{
			get
			{
				return this.horizontalActionId;
			}
			set
			{
				if (value == this.horizontalActionId)
				{
					return;
				}
				this.horizontalActionId = value;
				if (ReInput.isReady)
				{
					this.m_HorizontalAxis = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0004C2CF File Offset: 0x0004A6CF
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0004C2D8 File Offset: 0x0004A6D8
		public int VerticalActionId
		{
			get
			{
				return this.verticalActionId;
			}
			set
			{
				if (value == this.verticalActionId)
				{
					return;
				}
				this.verticalActionId = value;
				if (ReInput.isReady)
				{
					this.m_VerticalAxis = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0004C333 File Offset: 0x0004A733
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x0004C33C File Offset: 0x0004A73C
		public int SubmitActionId
		{
			get
			{
				return this.submitActionId;
			}
			set
			{
				if (value == this.submitActionId)
				{
					return;
				}
				this.submitActionId = value;
				if (ReInput.isReady)
				{
					this.m_SubmitButton = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0004C397 File Offset: 0x0004A797
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x0004C3A0 File Offset: 0x0004A7A0
		public int CancelActionId
		{
			get
			{
				return this.cancelActionId;
			}
			set
			{
				if (value == this.cancelActionId)
				{
					return;
				}
				this.cancelActionId = value;
				if (ReInput.isReady)
				{
					this.m_CancelButton = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0004C3FB File Offset: 0x0004A7FB
		protected override bool isMouseSupported
		{
			get
			{
				return base.isMouseSupported && this.m_allowMouseInput && (!this.isTouchSupported || this.m_allowMouseInputIfTouchSupported);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0004C42E File Offset: 0x0004A82E
		private bool isTouchAllowed
		{
			get
			{
				return this.m_allowTouchInput;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0004C436 File Offset: 0x0004A836
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x0004C43E File Offset: 0x0004A83E
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead")]
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0004C447 File Offset: 0x0004A847
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x0004C44F File Offset: 0x0004A84F
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0004C458 File Offset: 0x0004A858
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0004C460 File Offset: 0x0004A860
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0004C469 File Offset: 0x0004A869
		// (set) Token: 0x06001B55 RID: 6997 RVA: 0x0004C471 File Offset: 0x0004A871
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0004C47A File Offset: 0x0004A87A
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x0004C482 File Offset: 0x0004A882
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				if (this.m_HorizontalAxis == value)
				{
					return;
				}
				this.m_HorizontalAxis = value;
				if (ReInput.isReady)
				{
					this.horizontalActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0004C4B8 File Offset: 0x0004A8B8
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x0004C4C0 File Offset: 0x0004A8C0
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				if (this.m_VerticalAxis == value)
				{
					return;
				}
				this.m_VerticalAxis = value;
				if (ReInput.isReady)
				{
					this.verticalActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0004C4F6 File Offset: 0x0004A8F6
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0004C4FE File Offset: 0x0004A8FE
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				if (this.m_SubmitButton == value)
				{
					return;
				}
				this.m_SubmitButton = value;
				if (ReInput.isReady)
				{
					this.submitActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0004C534 File Offset: 0x0004A934
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0004C53C File Offset: 0x0004A93C
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				if (this.m_CancelButton == value)
				{
					return;
				}
				this.m_CancelButton = value;
				if (ReInput.isReady)
				{
					this.cancelActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0004C574 File Offset: 0x0004A974
		protected override void Awake()
		{
			base.Awake();
			this.isTouchSupported = base.defaultTouchInputSource.touchSupported;
			TouchInputModule component = base.GetComponent<TouchInputModule>();
			if (component != null)
			{
				component.enabled = false;
			}
			ReInput.InitializedEvent += this.OnRewiredInitialized;
			this.InitializeRewired();
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0004C5CC File Offset: 0x0004A9CC
		public override void UpdateModule()
		{
			this.CheckEditorRecompile();
			if (this.recompiling)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			if (RewiredStandaloneInputModule.systemBlocked)
			{
				return;
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0004C618 File Offset: 0x0004AA18
		public override bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0004C61C File Offset: 0x0004AA1C
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (this.recompiling)
			{
				return false;
			}
			if (!ReInput.isReady)
			{
				return false;
			}
			bool flag = this.m_ForceModuleActive;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						flag |= this.GetButtonDown(player, this.submitActionId);
						flag |= this.GetButtonDown(player, this.cancelActionId);
						if (this.moveOneElementPerAxisPress)
						{
							flag |= (this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId));
							flag |= (this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId));
						}
						else
						{
							flag |= !Mathf.Approximately(this.GetAxis(player, this.horizontalActionId), 0f);
							flag |= !Mathf.Approximately(this.GetAxis(player, this.verticalActionId), 0f);
						}
					}
				}
			}
			if (this.isMouseSupported)
			{
				flag |= this.DidAnyMouseMove();
				flag |= this.GetMouseButtonDownOnAnyMouse(0);
			}
			if (this.isTouchAllowed)
			{
				for (int j = 0; j < base.defaultTouchInputSource.touchCount; j++)
				{
					Touch touch = base.defaultTouchInputSource.GetTouch(j);
					flag |= (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary);
				}
			}
			return flag;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0004C7DC File Offset: 0x0004ABDC
		public override void ActivateModule()
		{
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			if (RewiredStandaloneInputModule.systemBlocked)
			{
				return;
			}
			base.ActivateModule();
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0004C847 File Offset: 0x0004AC47
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0004C858 File Offset: 0x0004AC58
		public override void Process()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (RewiredStandaloneInputModule.systemBlocked)
			{
				return;
			}
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
			if (!this.ProcessTouchEvents() && this.isMouseSupported)
			{
				this.ProcessMouseEvents();
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0004C900 File Offset: 0x0004AD00
		private bool ProcessTouchEvents()
		{
			if (!this.isTouchAllowed)
			{
				return false;
			}
			for (int i = 0; i < base.defaultTouchInputSource.touchCount; i++)
			{
				Touch touch = base.defaultTouchInputSource.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool pressed;
					bool flag;
					PlayerPointerEventData touchPointerEventData = base.GetTouchPointerEventData(0, 0, touch, out pressed, out flag);
					this.ProcessTouchPress(touchPointerEventData, pressed, flag);
					if (!flag)
					{
						this.ProcessMove(touchPointerEventData);
						this.ProcessDrag(touchPointerEventData);
					}
					else
					{
						base.RemovePointerData(touchPointerEventData);
					}
				}
			}
			return base.defaultTouchInputSource.touchCount > 0;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0004C9A0 File Offset: 0x0004ADA0
		private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					base.HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					float num = unscaledTime - pointerEvent.clickTime;
					if (num < 0.3f)
					{
						pointerEvent.clickCount++;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0004CBF4 File Offset: 0x0004AFF4
		private bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			if (this.recompiling || RewiredStandaloneInputModule.systemBlocked)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						if (this.GetButtonDown(player, this.submitActionId))
						{
							ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
							break;
						}
						if (this.GetButtonDown(player, this.cancelActionId))
						{
							ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
							break;
						}
					}
				}
			}
			return baseEventData.used;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0004CCEC File Offset: 0x0004B0EC
		private Vector2 GetRawMoveVector()
		{
			if (this.recompiling || RewiredStandaloneInputModule.systemBlocked)
			{
				return Vector2.zero;
			}
			Vector2 zero = Vector2.zero;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						if (this.moveOneElementPerAxisPress)
						{
							float num = 0f;
							if (this.GetButtonDown(player, this.horizontalActionId))
							{
								num = 1f;
							}
							else if (this.GetNegativeButtonDown(player, this.horizontalActionId))
							{
								num = -1f;
							}
							float num2 = 0f;
							if (this.GetButtonDown(player, this.verticalActionId))
							{
								num2 = 1f;
							}
							else if (this.GetNegativeButtonDown(player, this.verticalActionId))
							{
								num2 = -1f;
							}
							zero.x += num;
							zero.y += num2;
						}
						else
						{
							zero.x += this.GetAxis(player, this.horizontalActionId);
							zero.y += this.GetAxis(player, this.verticalActionId);
						}
						flag |= (this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId));
						flag2 |= (this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId));
					}
				}
			}
			if (flag)
			{
				if (zero.x < 0f)
				{
					zero.x = -1f;
				}
				if (zero.x > 0f)
				{
					zero.x = 1f;
				}
			}
			if (flag2)
			{
				if (zero.y < 0f)
				{
					zero.y = -1f;
				}
				if (zero.y > 0f)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0004CF2C File Offset: 0x0004B32C
		private bool SendMoveEventToSelectedObject()
		{
			if (this.recompiling || RewiredStandaloneInputModule.systemBlocked)
			{
				return false;
			}
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
			bool flag2;
			bool flag3;
			this.CheckButtonOrKeyMovement(unscaledTime, out flag2, out flag3);
			AxisEventData axisEventData = null;
			bool flag4 = flag2 || flag3;
			if (flag4)
			{
				axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
				MoveDirection moveDir = axisEventData.moveDir;
				flag4 = (((moveDir == MoveDirection.Up || moveDir == MoveDirection.Down) && flag3) || ((moveDir == MoveDirection.Left || moveDir == MoveDirection.Right) && flag2));
			}
			if (!flag4)
			{
				if (this.m_RepeatDelay > 0f)
				{
					if (flag && this.m_ConsecutiveMoveCount == 1)
					{
						flag4 = (unscaledTime > this.m_PrevActionTime + this.m_RepeatDelay);
					}
					else
					{
						flag4 = (unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond);
					}
				}
				else
				{
					flag4 = (unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond);
				}
			}
			if (!flag4)
			{
				return false;
			}
			if (axisEventData == null)
			{
				axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			}
			if (axisEventData.moveDir != MoveDirection.None)
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				if (!flag)
				{
					this.m_ConsecutiveMoveCount = 0;
				}
				if (this.m_ConsecutiveMoveCount == 0 || (!flag2 && !flag3))
				{
					this.m_ConsecutiveMoveCount++;
				}
				this.m_PrevActionTime = unscaledTime;
				this.m_LastMoveVector = rawMoveVector;
			}
			else
			{
				this.m_ConsecutiveMoveCount = 0;
			}
			return axisEventData.used;
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0004D134 File Offset: 0x0004B534
		private void CheckButtonOrKeyMovement(float time, out bool downHorizontal, out bool downVertical)
		{
			downHorizontal = false;
			downVertical = false;
			if (RewiredStandaloneInputModule.systemBlocked)
			{
				return;
			}
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						downHorizontal |= (this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId));
						downVertical |= (this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId));
					}
				}
			}
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0004D1F4 File Offset: 0x0004B5F4
		private void ProcessMouseEvents()
		{
			if (RewiredStandaloneInputModule.systemBlocked)
			{
				return;
			}
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						int mouseInputSourceCount = base.GetMouseInputSourceCount(this.playerIds[i]);
						for (int j = 0; j < mouseInputSourceCount; j++)
						{
							this.ProcessMouseEvent(this.playerIds[i], j);
						}
					}
				}
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0004D290 File Offset: 0x0004B690
		private void ProcessMouseEvent(int playerId, int pointerIndex)
		{
			RewiredPointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(playerId, pointerIndex);
			if (mousePointerEventData == null)
			{
				return;
			}
			RewiredPointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(0).eventData;
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(1).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(1).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(2).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(2).eventData.buttonData);
			IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, pointerIndex);
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				this.ProcessMousePress(mousePointerEventData.GetButtonState(i).eventData);
				this.ProcessDrag(mousePointerEventData.GetButtonState(i).eventData.buttonData);
			}
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(eventHandler, eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0004D3C8 File Offset: 0x0004B7C8
		private bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0004D414 File Offset: 0x0004B814
		private void ProcessMousePress(RewiredPointerInputModule.MouseButtonEventData data)
		{
			if (RewiredStandaloneInputModule.systemBlocked)
			{
				return;
			}
			PlayerPointerEventData buttonData = data.buttonData;
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					float num = unscaledTime - buttonData.clickTime;
					if (num < 0.3f)
					{
						buttonData.clickCount++;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (data.ReleasedThisFrame())
			{
				ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
				}
				else if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
				}
				buttonData.eligibleForClick = false;
				buttonData.pointerPress = null;
				buttonData.rawPointerPress = null;
				if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
				}
				buttonData.dragging = false;
				buttonData.pointerDrag = null;
				if (gameObject != buttonData.pointerEnter)
				{
					base.HandlePointerExitAndEnter(buttonData, null);
					base.HandlePointerExitAndEnter(buttonData, gameObject);
				}
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0004D643 File Offset: 0x0004BA43
		private void OnApplicationFocus(bool hasFocus)
		{
			this.m_HasFocus = hasFocus;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0004D64C File Offset: 0x0004BA4C
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return !ReInput.isReady || ReInput.configuration.ignoreInputWhenAppNotInFocus;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0004D664 File Offset: 0x0004BA64
		protected override void OnDestroy()
		{
			base.OnDestroy();
			ReInput.InitializedEvent -= this.OnRewiredInitialized;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0004D680 File Offset: 0x0004BA80
		protected override bool IsDefaultPlayer(int playerId)
		{
			if (this.playerIds == null)
			{
				return false;
			}
			if (!ReInput.isReady)
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < this.playerIds.Length; j++)
				{
					Player player = ReInput.players.GetPlayer(this.playerIds[j]);
					if (player != null)
					{
						if (i >= 1 || !this.usePlayingPlayersOnly || player.isPlaying)
						{
							if (i >= 2 || player.controllers.hasMouse)
							{
								return this.playerIds[j] == playerId;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0004D73C File Offset: 0x0004BB3C
		private void InitializeRewired()
		{
			if (!ReInput.isReady)
			{
				Debug.LogError("Rewired is not initialized! Are you missing a Rewired Input Manager in your scene?");
				return;
			}
			ReInput.ShutDownEvent -= this.OnRewiredShutDown;
			ReInput.ShutDownEvent += this.OnRewiredShutDown;
			ReInput.EditorRecompileEvent -= this.OnEditorRecompile;
			ReInput.EditorRecompileEvent += this.OnEditorRecompile;
			this.SetupRewiredVars();
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0004D7A8 File Offset: 0x0004BBA8
		private void SetupRewiredVars()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.SetUpRewiredActions();
			if (this.useAllRewiredGamePlayers)
			{
				IList<Player> list = (!this.useRewiredSystemPlayer) ? ReInput.players.Players : ReInput.players.AllPlayers;
				this.playerIds = new int[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					this.playerIds[i] = list[i].id;
				}
			}
			else
			{
				bool flag = false;
				List<int> list2 = new List<int>(this.rewiredPlayerIds.Length + 1);
				for (int j = 0; j < this.rewiredPlayerIds.Length; j++)
				{
					Player player = ReInput.players.GetPlayer(this.rewiredPlayerIds[j]);
					if (player != null)
					{
						if (!list2.Contains(player.id))
						{
							list2.Add(player.id);
							if (player.id == 9999999)
							{
								flag = true;
							}
						}
					}
				}
				if (this.useRewiredSystemPlayer && !flag)
				{
					list2.Insert(0, ReInput.players.GetSystemPlayer().id);
				}
				this.playerIds = list2.ToArray();
			}
			this.SetUpRewiredPlayerMice();
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0004D8F8 File Offset: 0x0004BCF8
		private void SetUpRewiredPlayerMice()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			base.ClearMouseInputSources();
			for (int i = 0; i < this.playerMice.Count; i++)
			{
				PlayerMouse playerMouse = this.playerMice[i];
				if (!UnityTools.IsNullOrDestroyed<PlayerMouse>(playerMouse))
				{
					base.AddMouseInputSource(playerMouse);
				}
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0004D958 File Offset: 0x0004BD58
		private void SetUpRewiredActions()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.setActionsById)
			{
				this.horizontalActionId = ReInput.mapping.GetActionId(this.m_HorizontalAxis);
				this.verticalActionId = ReInput.mapping.GetActionId(this.m_VerticalAxis);
				this.submitActionId = ReInput.mapping.GetActionId(this.m_SubmitButton);
				this.cancelActionId = ReInput.mapping.GetActionId(this.m_CancelButton);
			}
			else
			{
				InputAction action = ReInput.mapping.GetAction(this.horizontalActionId);
				this.m_HorizontalAxis = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.horizontalActionId = -1;
				}
				action = ReInput.mapping.GetAction(this.verticalActionId);
				this.m_VerticalAxis = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.verticalActionId = -1;
				}
				action = ReInput.mapping.GetAction(this.submitActionId);
				this.m_SubmitButton = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.submitActionId = -1;
				}
				action = ReInput.mapping.GetAction(this.cancelActionId);
				this.m_CancelButton = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.cancelActionId = -1;
				}
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0004DAC0 File Offset: 0x0004BEC0
		private bool GetButtonDown(Player player, int actionId)
		{
			return actionId >= 0 && player.GetButtonDown(actionId);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0004DAD2 File Offset: 0x0004BED2
		private bool GetNegativeButtonDown(Player player, int actionId)
		{
			return actionId >= 0 && player.GetNegativeButtonDown(actionId);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0004DAE4 File Offset: 0x0004BEE4
		private float GetAxis(Player player, int actionId)
		{
			if (actionId < 0)
			{
				return 0f;
			}
			return player.GetAxis(actionId);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0004DAFA File Offset: 0x0004BEFA
		private void CheckEditorRecompile()
		{
			if (!this.recompiling)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			this.recompiling = false;
			this.InitializeRewired();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0004DB20 File Offset: 0x0004BF20
		private void OnEditorRecompile()
		{
			this.recompiling = true;
			this.ClearRewiredVars();
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0004DB2F File Offset: 0x0004BF2F
		private void ClearRewiredVars()
		{
			Array.Clear(this.playerIds, 0, this.playerIds.Length);
			base.ClearMouseInputSources();
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0004DB4C File Offset: 0x0004BF4C
		private bool DidAnyMouseMove()
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				int playerId = this.playerIds[i];
				Player player = ReInput.players.GetPlayer(playerId);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						int mouseInputSourceCount = base.GetMouseInputSourceCount(playerId);
						for (int j = 0; j < mouseInputSourceCount; j++)
						{
							IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, j);
							if (mouseInputSource != null)
							{
								if (mouseInputSource.screenPositionDelta.sqrMagnitude > 0f)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0004DC00 File Offset: 0x0004C000
		private bool GetMouseButtonDownOnAnyMouse(int buttonIndex)
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				int playerId = this.playerIds[i];
				Player player = ReInput.players.GetPlayer(playerId);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						int mouseInputSourceCount = base.GetMouseInputSourceCount(playerId);
						for (int j = 0; j < mouseInputSourceCount; j++)
						{
							IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, j);
							if (mouseInputSource != null)
							{
								if (mouseInputSource.GetButtonDown(buttonIndex))
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0004DCA6 File Offset: 0x0004C0A6
		private void OnRewiredInitialized()
		{
			this.InitializeRewired();
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0004DCAE File Offset: 0x0004C0AE
		private void OnRewiredShutDown()
		{
			this.ClearRewiredVars();
		}

		// Token: 0x04001106 RID: 4358
		private const string DEFAULT_ACTION_MOVE_HORIZONTAL = "UIHorizontal";

		// Token: 0x04001107 RID: 4359
		private const string DEFAULT_ACTION_MOVE_VERTICAL = "UIVertical";

		// Token: 0x04001108 RID: 4360
		private const string DEFAULT_ACTION_SUBMIT = "UISubmit";

		// Token: 0x04001109 RID: 4361
		private const string DEFAULT_ACTION_CANCEL = "UICancel";

		// Token: 0x0400110A RID: 4362
		[Tooltip("(Optional) Link the Rewired Input Manager here for easier access to Player ids, etc.")]
		[SerializeField]
		private InputManager_Base rewiredInputManager;

		// Token: 0x0400110B RID: 4363
		[SerializeField]
		[Tooltip("Use all Rewired game Players to control the UI. This does not include the System Player. If enabled, this setting overrides individual Player Ids set in Rewired Player Ids.")]
		private bool useAllRewiredGamePlayers;

		// Token: 0x0400110C RID: 4364
		[SerializeField]
		[Tooltip("Allow the Rewired System Player to control the UI.")]
		private bool useRewiredSystemPlayer;

		// Token: 0x0400110D RID: 4365
		[SerializeField]
		[Tooltip("A list of Player Ids that are allowed to control the UI. If Use All Rewired Game Players = True, this list will be ignored.")]
		private int[] rewiredPlayerIds = new int[1];

		// Token: 0x0400110E RID: 4366
		[SerializeField]
		[Tooltip("Allow only Players with Player.isPlaying = true to control the UI.")]
		private bool usePlayingPlayersOnly;

		// Token: 0x0400110F RID: 4367
		[SerializeField]
		[Tooltip("Player Mice allowed to interact with the UI. Each Player that owns a Player Mouse must also be allowed to control the UI or the Player Mouse will not function.")]
		private List<PlayerMouse> playerMice = new List<PlayerMouse>();

		// Token: 0x04001110 RID: 4368
		[SerializeField]
		[Tooltip("Makes an axis press always move only one UI selection. Enable if you do not want to allow scrolling through UI elements by holding an axis direction.")]
		private bool moveOneElementPerAxisPress;

		// Token: 0x04001111 RID: 4369
		[SerializeField]
		[Tooltip("If enabled, Action Ids will be used to set the Actions. If disabled, string names will be used to set the Actions.")]
		private bool setActionsById;

		// Token: 0x04001112 RID: 4370
		[SerializeField]
		[Tooltip("Id of the horizontal Action for movement (if axis events are used).")]
		private int horizontalActionId = -1;

		// Token: 0x04001113 RID: 4371
		[SerializeField]
		[Tooltip("Id of the vertical Action for movement (if axis events are used).")]
		private int verticalActionId = -1;

		// Token: 0x04001114 RID: 4372
		[SerializeField]
		[Tooltip("Id of the Action used to submit.")]
		private int submitActionId = -1;

		// Token: 0x04001115 RID: 4373
		[SerializeField]
		[Tooltip("Id of the Action used to cancel.")]
		private int cancelActionId = -1;

		// Token: 0x04001116 RID: 4374
		[SerializeField]
		[Tooltip("Name of the horizontal axis for movement (if axis events are used).")]
		private string m_HorizontalAxis = "UIHorizontal";

		// Token: 0x04001117 RID: 4375
		[SerializeField]
		[Tooltip("Name of the vertical axis for movement (if axis events are used).")]
		private string m_VerticalAxis = "UIVertical";

		// Token: 0x04001118 RID: 4376
		[SerializeField]
		[Tooltip("Name of the action used to submit.")]
		private string m_SubmitButton = "UISubmit";

		// Token: 0x04001119 RID: 4377
		[SerializeField]
		[Tooltip("Name of the action used to cancel.")]
		private string m_CancelButton = "UICancel";

		// Token: 0x0400111A RID: 4378
		[SerializeField]
		[Tooltip("Number of selection changes allowed per second when a movement button/axis is held in a direction.")]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x0400111B RID: 4379
		[SerializeField]
		[Tooltip("Delay in seconds before vertical/horizontal movement starts repeating continouously when a movement direction is held.")]
		private float m_RepeatDelay;

		// Token: 0x0400111C RID: 4380
		[SerializeField]
		[Tooltip("Allows the mouse to be used to select elements.")]
		private bool m_allowMouseInput = true;

		// Token: 0x0400111D RID: 4381
		[SerializeField]
		[Tooltip("Allows the mouse to be used to select elements if the device also supports touch control.")]
		private bool m_allowMouseInputIfTouchSupported = true;

		// Token: 0x0400111E RID: 4382
		[SerializeField]
		[Tooltip("Allows touch input to be used to select elements.")]
		private bool m_allowTouchInput = true;

		// Token: 0x0400111F RID: 4383
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		[Tooltip("Forces the module to always be active.")]
		private bool m_ForceModuleActive;

		// Token: 0x04001120 RID: 4384
		[NonSerialized]
		private int[] playerIds;

		// Token: 0x04001121 RID: 4385
		private bool recompiling;

		// Token: 0x04001122 RID: 4386
		[NonSerialized]
		private bool isTouchSupported;

		// Token: 0x04001123 RID: 4387
		[NonSerialized]
		private float m_PrevActionTime;

		// Token: 0x04001124 RID: 4388
		[NonSerialized]
		private Vector2 m_LastMoveVector;

		// Token: 0x04001125 RID: 4389
		[NonSerialized]
		private int m_ConsecutiveMoveCount;

		// Token: 0x04001126 RID: 4390
		[NonSerialized]
		private bool m_HasFocus = true;

		// Token: 0x04001127 RID: 4391
		public static bool systemBlocked;

		// Token: 0x0200049A RID: 1178
		[Serializable]
		public class PlayerSetting
		{
			// Token: 0x06001B82 RID: 7042 RVA: 0x0004DCB8 File Offset: 0x0004C0B8
			public PlayerSetting()
			{
			}

			// Token: 0x06001B83 RID: 7043 RVA: 0x0004DCCC File Offset: 0x0004C0CC
			private PlayerSetting(RewiredStandaloneInputModule.PlayerSetting other)
			{
				if (other == null)
				{
					throw new ArgumentNullException("other");
				}
				this.playerId = other.playerId;
				this.playerMice = new List<PlayerMouse>();
				if (other.playerMice != null)
				{
					foreach (PlayerMouse item in other.playerMice)
					{
						this.playerMice.Add(item);
					}
				}
			}

			// Token: 0x06001B84 RID: 7044 RVA: 0x0004DD74 File Offset: 0x0004C174
			public RewiredStandaloneInputModule.PlayerSetting Clone()
			{
				return new RewiredStandaloneInputModule.PlayerSetting(this);
			}

			// Token: 0x04001128 RID: 4392
			public int playerId;

			// Token: 0x04001129 RID: 4393
			public List<PlayerMouse> playerMice = new List<PlayerMouse>();
		}
	}
}
