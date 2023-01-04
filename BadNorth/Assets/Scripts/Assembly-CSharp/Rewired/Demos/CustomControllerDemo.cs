using System;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000477 RID: 1143
	[AddComponentMenu("")]
	public class CustomControllerDemo : MonoBehaviour
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x00046ADC File Offset: 0x00044EDC
		private void Awake()
		{
			if (SystemInfo.deviceType == DeviceType.Handheld && Screen.orientation != ScreenOrientation.LandscapeLeft)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
			this.Initialize();
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00046B00 File Offset: 0x00044F00
		private void Initialize()
		{
			ReInput.InputSourceUpdateEvent += this.OnInputSourceUpdate;
			this.joysticks = base.GetComponentsInChildren<TouchJoystickExample>();
			this.buttons = base.GetComponentsInChildren<TouchButtonExample>();
			this.axisCount = this.joysticks.Length * 2;
			this.buttonCount = this.buttons.Length;
			this.axisValues = new float[this.axisCount];
			this.buttonValues = new bool[this.buttonCount];
			Player player = ReInput.players.GetPlayer(this.playerId);
			this.controller = player.controllers.GetControllerWithTag<CustomController>(this.controllerTag);
			if (this.controller == null)
			{
				Debug.LogError("A matching controller was not found for tag \"" + this.controllerTag + "\"");
			}
			if (this.controller.buttonCount != this.buttonValues.Length || this.controller.axisCount != this.axisValues.Length)
			{
				Debug.LogError("Controller has wrong number of elements!");
			}
			if (this.useUpdateCallbacks && this.controller != null)
			{
				this.controller.SetAxisUpdateCallback(new Func<int, float>(this.GetAxisValueCallback));
				this.controller.SetButtonUpdateCallback(new Func<int, bool>(this.GetButtonValueCallback));
			}
			this.initialized = true;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00046C48 File Offset: 0x00045048
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.initialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00046C66 File Offset: 0x00045066
		private void OnInputSourceUpdate()
		{
			this.GetSourceAxisValues();
			this.GetSourceButtonValues();
			if (!this.useUpdateCallbacks)
			{
				this.SetControllerAxisValues();
				this.SetControllerButtonValues();
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00046C8C File Offset: 0x0004508C
		private void GetSourceAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				if (i % 2 != 0)
				{
					this.axisValues[i] = this.joysticks[i / 2].position.y;
				}
				else
				{
					this.axisValues[i] = this.joysticks[i / 2].position.x;
				}
			}
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00046D00 File Offset: 0x00045100
		private void GetSourceButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.buttonValues[i] = this.buttons[i].isPressed;
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00046D3C File Offset: 0x0004513C
		private void SetControllerAxisValues()
		{
			for (int i = 0; i < this.axisValues.Length; i++)
			{
				this.controller.SetAxisValue(i, this.axisValues[i]);
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00046D78 File Offset: 0x00045178
		private void SetControllerButtonValues()
		{
			for (int i = 0; i < this.buttonValues.Length; i++)
			{
				this.controller.SetButtonValue(i, this.buttonValues[i]);
			}
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00046DB2 File Offset: 0x000451B2
		private float GetAxisValueCallback(int index)
		{
			if (index >= this.axisValues.Length)
			{
				return 0f;
			}
			return this.axisValues[index];
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00046DD0 File Offset: 0x000451D0
		private bool GetButtonValueCallback(int index)
		{
			return index < this.buttonValues.Length && this.buttonValues[index];
		}

		// Token: 0x0400102C RID: 4140
		public int playerId;

		// Token: 0x0400102D RID: 4141
		public string controllerTag;

		// Token: 0x0400102E RID: 4142
		public bool useUpdateCallbacks;

		// Token: 0x0400102F RID: 4143
		private int buttonCount;

		// Token: 0x04001030 RID: 4144
		private int axisCount;

		// Token: 0x04001031 RID: 4145
		private float[] axisValues;

		// Token: 0x04001032 RID: 4146
		private bool[] buttonValues;

		// Token: 0x04001033 RID: 4147
		private TouchJoystickExample[] joysticks;

		// Token: 0x04001034 RID: 4148
		private TouchButtonExample[] buttons;

		// Token: 0x04001035 RID: 4149
		private CustomController controller;

		// Token: 0x04001036 RID: 4150
		[NonSerialized]
		private bool initialized;
	}
}
