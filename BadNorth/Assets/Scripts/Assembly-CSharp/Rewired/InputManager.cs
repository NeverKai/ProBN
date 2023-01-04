using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Rewired.Platforms;
using Rewired.Utils;
using Rewired.Utils.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rewired
{
	// Token: 0x020004AD RID: 1197
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class InputManager : InputManager_Base
	{
		// Token: 0x06001DEC RID: 7660 RVA: 0x00050862 File Offset: 0x0004EC62
		protected override void OnInitialized()
		{
			this.SubscribeEvents();
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0005086A File Offset: 0x0004EC6A
		protected override void OnDeinitialized()
		{
			this.UnsubscribeEvents();
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00050874 File Offset: 0x0004EC74
		protected override void DetectPlatform()
		{
			this.scriptingBackend = ScriptingBackend.Mono;
			this.scriptingAPILevel = ScriptingAPILevel.Net20;
			this.editorPlatform = EditorPlatform.None;
			this.platform = Rewired.Platforms.Platform.Unknown;
			this.webplayerPlatform = WebplayerPlatform.None;
			this.isEditor = false;
			string text = SystemInfo.deviceName ?? string.Empty;
			string text2 = SystemInfo.deviceModel ?? string.Empty;
			this.platform = Rewired.Platforms.Platform.Windows;
			this.scriptingBackend = ScriptingBackend.Mono;
			this.scriptingAPILevel = ScriptingAPILevel.Net20Subset;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000508E4 File Offset: 0x0004ECE4
		protected override void CheckRecompile()
		{
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000508E6 File Offset: 0x0004ECE6
		protected override IExternalTools GetExternalTools()
		{
			return new ExternalTools();
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000508ED File Offset: 0x0004ECED
		private bool CheckDeviceName(string searchPattern, string deviceName, string deviceModel)
		{
			return Regex.IsMatch(deviceName, searchPattern, RegexOptions.IgnoreCase) || Regex.IsMatch(deviceModel, searchPattern, RegexOptions.IgnoreCase);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00050907 File Offset: 0x0004ED07
		private void SubscribeEvents()
		{
			this.UnsubscribeEvents();
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00050920 File Offset: 0x0004ED20
		private void UnsubscribeEvents()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00050933 File Offset: 0x0004ED33
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			base.OnSceneLoaded();
		}
	}
}
