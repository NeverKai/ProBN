using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x02000490 RID: 1168
	[AddComponentMenu("")]
	public class SimpleControlRemapping : MonoBehaviour
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x0004A677 File Offset: 0x00048A77
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(0);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0004A684 File Offset: 0x00048A84
		private ControllerMap controllerMap
		{
			get
			{
				if (this.controller == null)
				{
					return null;
				}
				return this.player.controllers.maps.GetMap(this.controller.type, this.controller.id, "Default", "Default");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0004A6D3 File Offset: 0x00048AD3
		private Controller controller
		{
			get
			{
				return this.player.controllers.GetController(this.selectedControllerType, this.selectedControllerId);
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0004A6F4 File Offset: 0x00048AF4
		private void OnEnable()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			ReInput.ControllerConnectedEvent += this.OnControllerChanged;
			ReInput.ControllerDisconnectedEvent += this.OnControllerChanged;
			this.inputMapper.InputMappedEvent += this.OnInputMapped;
			this.inputMapper.StoppedEvent += this.OnStopped;
			this.InitializeUI();
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0004A799 File Offset: 0x00048B99
		private void OnDisable()
		{
			this.inputMapper.Stop();
			this.inputMapper.RemoveAllEventListeners();
			ReInput.ControllerConnectedEvent -= this.OnControllerChanged;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerChanged;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0004A7D4 File Offset: 0x00048BD4
		private void RedrawUI()
		{
			if (this.controller == null)
			{
				this.ClearUI();
				return;
			}
			this.controllerNameUIText.text = this.controller.name;
			for (int i = 0; i < this.rows.Count; i++)
			{
				SimpleControlRemapping.Row row = this.rows[i];
				InputAction action = this.rows[i].action;
				string text = string.Empty;
				int actionElementMapId = -1;
				foreach (ActionElementMap actionElementMap in this.controllerMap.ElementMapsWithAction(action.id))
				{
					if (actionElementMap.ShowInField(row.actionRange))
					{
						text = actionElementMap.elementIdentifierName;
						actionElementMapId = actionElementMap.id;
						break;
					}
				}
				row.text.text = text;
				row.button.onClick.RemoveAllListeners();
				int index = i;
				row.button.onClick.AddListener(delegate()
				{
					this.OnInputFieldClicked(index, actionElementMapId);
				});
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0004A924 File Offset: 0x00048D24
		private void ClearUI()
		{
			if (this.selectedControllerType == ControllerType.Joystick)
			{
				this.controllerNameUIText.text = "No joysticks attached";
			}
			else
			{
				this.controllerNameUIText.text = string.Empty;
			}
			for (int i = 0; i < this.rows.Count; i++)
			{
				this.rows[i].text.text = string.Empty;
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0004A99C File Offset: 0x00048D9C
		private void InitializeUI()
		{
			IEnumerator enumerator = this.actionGroupTransform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			IEnumerator enumerator2 = this.fieldGroupTransform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Transform transform2 = (Transform)obj2;
					UnityEngine.Object.Destroy(transform2.gameObject);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory("Default"))
			{
				if (inputAction.type == InputActionType.Axis)
				{
					this.CreateUIRow(inputAction, AxisRange.Full, inputAction.descriptiveName);
					this.CreateUIRow(inputAction, AxisRange.Positive, string.IsNullOrEmpty(inputAction.positiveDescriptiveName) ? (inputAction.descriptiveName + " +") : inputAction.positiveDescriptiveName);
					this.CreateUIRow(inputAction, AxisRange.Negative, string.IsNullOrEmpty(inputAction.negativeDescriptiveName) ? (inputAction.descriptiveName + " -") : inputAction.negativeDescriptiveName);
				}
				else if (inputAction.type == InputActionType.Button)
				{
					this.CreateUIRow(inputAction, AxisRange.Positive, inputAction.descriptiveName);
				}
			}
			this.RedrawUI();
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0004AB68 File Offset: 0x00048F68
		private void CreateUIRow(InputAction action, AxisRange actionRange, string label)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.textPrefab);
			gameObject.transform.SetParent(this.actionGroupTransform);
			gameObject.transform.SetAsLastSibling();
			gameObject.GetComponent<Text>().text = label;
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab);
			gameObject2.transform.SetParent(this.fieldGroupTransform);
			gameObject2.transform.SetAsLastSibling();
			this.rows.Add(new SimpleControlRemapping.Row
			{
				action = action,
				actionRange = actionRange,
				button = gameObject2.GetComponent<Button>(),
				text = gameObject2.GetComponentInChildren<Text>()
			});
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0004AC0C File Offset: 0x0004900C
		private void SetSelectedController(ControllerType controllerType)
		{
			bool flag = false;
			if (controllerType != this.selectedControllerType)
			{
				this.selectedControllerType = controllerType;
				flag = true;
			}
			int num = this.selectedControllerId;
			if (this.selectedControllerType == ControllerType.Joystick)
			{
				if (this.player.controllers.joystickCount > 0)
				{
					this.selectedControllerId = this.player.controllers.Joysticks[0].id;
				}
				else
				{
					this.selectedControllerId = -1;
				}
			}
			else
			{
				this.selectedControllerId = 0;
			}
			if (this.selectedControllerId != num)
			{
				flag = true;
			}
			if (flag)
			{
				this.inputMapper.Stop();
				this.RedrawUI();
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0004ACB7 File Offset: 0x000490B7
		public void OnControllerSelected(int controllerType)
		{
			this.SetSelectedController((ControllerType)controllerType);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0004ACC0 File Offset: 0x000490C0
		private void OnInputFieldClicked(int index, int actionElementMapToReplaceId)
		{
			if (index < 0 || index >= this.rows.Count)
			{
				return;
			}
			if (this.controller == null)
			{
				return;
			}
			base.StartCoroutine(this.StartListeningDelayed(index, actionElementMapToReplaceId));
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0004ACF8 File Offset: 0x000490F8
		private IEnumerator StartListeningDelayed(int index, int actionElementMapToReplaceId)
		{
			yield return new WaitForSeconds(0.1f);
			this.inputMapper.Start(new InputMapper.Context
			{
				actionId = this.rows[index].action.id,
				controllerMap = this.controllerMap,
				actionRange = this.rows[index].actionRange,
				actionElementMapToReplace = this.controllerMap.GetElementMap(actionElementMapToReplaceId)
			});
			this.player.controllers.maps.SetMapsEnabled(false, "UI");
			this.statusUIText.text = "Listening...";
			yield break;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0004AD21 File Offset: 0x00049121
		private void OnControllerChanged(ControllerStatusChangedEventArgs args)
		{
			this.SetSelectedController(this.selectedControllerType);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0004AD2F File Offset: 0x0004912F
		private void OnInputMapped(InputMapper.InputMappedEventData data)
		{
			this.RedrawUI();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0004AD37 File Offset: 0x00049137
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.statusUIText.text = string.Empty;
			this.player.controllers.maps.SetMapsEnabled(true, "UI");
		}

		// Token: 0x040010D8 RID: 4312
		private const string category = "Default";

		// Token: 0x040010D9 RID: 4313
		private const string layout = "Default";

		// Token: 0x040010DA RID: 4314
		private const string uiCategory = "UI";

		// Token: 0x040010DB RID: 4315
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x040010DC RID: 4316
		public GameObject buttonPrefab;

		// Token: 0x040010DD RID: 4317
		public GameObject textPrefab;

		// Token: 0x040010DE RID: 4318
		public RectTransform fieldGroupTransform;

		// Token: 0x040010DF RID: 4319
		public RectTransform actionGroupTransform;

		// Token: 0x040010E0 RID: 4320
		public Text controllerNameUIText;

		// Token: 0x040010E1 RID: 4321
		public Text statusUIText;

		// Token: 0x040010E2 RID: 4322
		private ControllerType selectedControllerType;

		// Token: 0x040010E3 RID: 4323
		private int selectedControllerId;

		// Token: 0x040010E4 RID: 4324
		private List<SimpleControlRemapping.Row> rows = new List<SimpleControlRemapping.Row>();

		// Token: 0x02000491 RID: 1169
		private class Row
		{
			// Token: 0x040010E5 RID: 4325
			public InputAction action;

			// Token: 0x040010E6 RID: 4326
			public AxisRange actionRange;

			// Token: 0x040010E7 RID: 4327
			public Button button;

			// Token: 0x040010E8 RID: 4328
			public Text text;
		}
	}
}
