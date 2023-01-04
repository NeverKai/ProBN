using System;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x0200002E RID: 46
	public class GUISystemMessages : PlatformSystemMessenger
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000070D7 File Offset: 0x000054D7
		protected override void ShowMessage()
		{
			base.ShowMessage();
			this.scrollPosition = Vector2.zero;
			this._optionPicked = -1;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000070F4 File Offset: 0x000054F4
		private void OnGUI()
		{
			if (base.ShowingMessage)
			{
				Rect safeArea = Screen.safeArea;
				safeArea.width = 400f;
				safeArea.height = 300f;
				safeArea.x = Screen.safeArea.width / 2f - 200f;
				safeArea.y = Screen.safeArea.height / 2f - 150f;
				GUILayout.Window(999999, safeArea, new GUI.WindowFunction(this.WindowGUI), "WARNING", new GUILayoutOption[0]);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007190 File Offset: 0x00005590
		private void WindowGUI(int id)
		{
			this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[0]);
			GUILayout.Label(this._activeMessage.messageBody, new GUILayoutOption[0]);
			int optionPicked = this._optionPicked;
			if (GUILayout.Button(this._activeMessage.optionAText, new GUILayoutOption[0]))
			{
				this._optionPicked = ((this._optionPicked >= 0) ? this._optionPicked : 0);
			}
			if (this._activeMessage.HasOptionB && GUILayout.Button(this._activeMessage.optionBText, new GUILayoutOption[0]))
			{
				this._optionPicked = ((this._optionPicked >= 0) ? this._optionPicked : 1);
			}
			if (optionPicked != this._optionPicked)
			{
				BasePlatformManager.Instance.AddToNextUpdate(new Action(this.PickedOptions));
			}
			GUILayout.EndScrollView();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000727A File Offset: 0x0000567A
		private void PickedOptions()
		{
			if (this._optionPicked == 1)
			{
				this.DoOptionB();
			}
			else
			{
				this.DoOptionA();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007299 File Offset: 0x00005699
		private void DoOptionA()
		{
			if (this._activeMessage != null && this._activeMessage.optionAAction != null)
			{
				this._activeMessage.optionAAction();
			}
			this.DiscardMessage();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000072CC File Offset: 0x000056CC
		private void DoOptionB()
		{
			if (this._activeMessage != null && this._activeMessage.optionBAction != null)
			{
				this._activeMessage.optionBAction();
			}
			this.DiscardMessage();
		}

		// Token: 0x0400008A RID: 138
		private int _optionPicked = -1;

		// Token: 0x0400008B RID: 139
		private Vector2 scrollPosition = Vector2.zero;
	}
}
