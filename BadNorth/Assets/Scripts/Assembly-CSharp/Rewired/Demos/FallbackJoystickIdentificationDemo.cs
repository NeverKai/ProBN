using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x0200047E RID: 1150
	[AddComponentMenu("")]
	public class FallbackJoystickIdentificationDemo : MonoBehaviour
	{
		// Token: 0x06001A64 RID: 6756 RVA: 0x00047B99 File Offset: 0x00045F99
		private void Awake()
		{
			if (!ReInput.unityJoystickIdentificationRequired)
			{
				return;
			}
			ReInput.ControllerConnectedEvent += this.JoystickConnected;
			ReInput.ControllerDisconnectedEvent += this.JoystickDisconnected;
			this.IdentifyAllJoysticks();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00047BCE File Offset: 0x00045FCE
		private void JoystickConnected(ControllerStatusChangedEventArgs args)
		{
			this.IdentifyAllJoysticks();
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00047BD6 File Offset: 0x00045FD6
		private void JoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			this.IdentifyAllJoysticks();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00047BE0 File Offset: 0x00045FE0
		public void IdentifyAllJoysticks()
		{
			this.Reset();
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			Joystick[] joysticks = ReInput.controllers.GetJoysticks();
			if (joysticks == null)
			{
				return;
			}
			this.identifyRequired = true;
			this.joysticksToIdentify = new Queue<Joystick>(joysticks);
			this.SetInputDelay();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00047C2E File Offset: 0x0004602E
		private void SetInputDelay()
		{
			this.nextInputAllowedTime = Time.time + 1f;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00047C44 File Offset: 0x00046044
		private void OnGUI()
		{
			if (!this.identifyRequired)
			{
				return;
			}
			if (this.joysticksToIdentify == null || this.joysticksToIdentify.Count == 0)
			{
				this.Reset();
				return;
			}
			Rect screenRect = new Rect((float)Screen.width * 0.5f - 125f, (float)Screen.height * 0.5f - 125f, 250f, 250f);
			GUILayout.Window(0, screenRect, new GUI.WindowFunction(this.DrawDialogWindow), "Joystick Identification Required", new GUILayoutOption[0]);
			GUI.FocusWindow(0);
			if (Time.time < this.nextInputAllowedTime)
			{
				return;
			}
			if (!ReInput.controllers.SetUnityJoystickIdFromAnyButtonOrAxisPress(this.joysticksToIdentify.Peek().id, 0.8f, false))
			{
				return;
			}
			this.joysticksToIdentify.Dequeue();
			this.SetInputDelay();
			if (this.joysticksToIdentify.Count == 0)
			{
				this.Reset();
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00047D38 File Offset: 0x00046138
		private void DrawDialogWindow(int windowId)
		{
			if (!this.identifyRequired)
			{
				return;
			}
			if (this.style == null)
			{
				this.style = new GUIStyle(GUI.skin.label);
				this.style.wordWrap = true;
			}
			GUILayout.Space(15f);
			GUILayout.Label("A joystick has been attached or removed. You will need to identify each joystick by pressing a button on the controller listed below:", this.style, new GUILayoutOption[0]);
			Joystick joystick = this.joysticksToIdentify.Peek();
			GUILayout.Label("Press any button on \"" + joystick.name + "\" now.", this.style, new GUILayoutOption[0]);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Skip", new GUILayoutOption[0]))
			{
				this.joysticksToIdentify.Dequeue();
				return;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00047DF6 File Offset: 0x000461F6
		private void Reset()
		{
			this.joysticksToIdentify = null;
			this.identifyRequired = false;
		}

		// Token: 0x0400105C RID: 4188
		private const float windowWidth = 250f;

		// Token: 0x0400105D RID: 4189
		private const float windowHeight = 250f;

		// Token: 0x0400105E RID: 4190
		private const float inputDelay = 1f;

		// Token: 0x0400105F RID: 4191
		private bool identifyRequired;

		// Token: 0x04001060 RID: 4192
		private Queue<Joystick> joysticksToIdentify;

		// Token: 0x04001061 RID: 4193
		private float nextInputAllowedTime;

		// Token: 0x04001062 RID: 4194
		private GUIStyle style;
	}
}
