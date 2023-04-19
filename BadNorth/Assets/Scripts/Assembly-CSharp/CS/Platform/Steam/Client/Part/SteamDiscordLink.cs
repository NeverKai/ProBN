using System;
using CS.Platform.Utils;
using Discord;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x02000041 RID: 65
	public class SteamDiscordLink : MonoBehaviour
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C4E6 File Offset: 0x0000A8E6
		public bool Connected
		{
			get
			{
				return this._connectedToSystem;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000C4EE File Offset: 0x0000A8EE
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000C4F6 File Offset: 0x0000A8F6
		public bool PlatformGUIActive
		{
			get
			{
				return this._platformGUI;
			}
			set
			{
				this._platformGUI = value;
				if (this.OnPlatformChange != null)
				{
					this.OnPlatformChange(value);
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C518 File Offset: 0x0000A918
		public void Awake()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			foreach (string text in commandLineArgs)
			{
				if (text.ToLower() == "nodiscordlink")
				{
					UnityEngine.Debug.Log("Disabling DiscordLink");
					this._connectedToSystem = false;
					return;
				}
			}
			try
			{
				this._sdk = new Discord.Discord(487564993236172802L, 1UL);
				if (CS.Platform.Utils.Debug.IsDebug)
				{
					// this._sdk.SetLogHook(LogLevel.Info, new Discord.SetLogHookHandler(this.ErrorLogger));
				}
				this._activitiesSDK = this._sdk.GetActivityManager();
				this._overlaySDK = this._sdk.GetOverlayManager();
				this._userSDK = this._sdk.GetUserManager();
				this._userSDK.OnCurrentUserUpdate += this.SDKReady;
				this._activitiesSDK.RegisterCommand();
				this._activitiesSDK.RegisterSteam((uint)SteamManager.DesiredGameID);
				PlatformSystemMessenger.OnMessageChange += this.PlatformSystemMessenger_OnMessageChange;
				this._overlaySDK.OnToggle += this.OverlaySDK_OnOverlayLocked;
				this.PlatformGUIActive = !this._overlaySDK.IsLocked();
				this._gameStartTime = DateTime.UtcNow;
			}
			catch (Exception ex)
			{
				CS.Platform.Utils.Debug.LogWarning("failed to initialize discord SDK in SteamDiscordLink : " + ex.Message, new object[0]);
				this.ShutDown();
				this._connectedToSystem = false;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C69C File Offset: 0x0000AA9C
		private void SDKReady()
		{
			CS.Platform.Utils.Debug.LogError("[DISCORDLINK] Ready", new object[0]);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C6AE File Offset: 0x0000AAAE
		private void OnDestroy()
		{
			this.ShutDown();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C6B8 File Offset: 0x0000AAB8
		private void ShutDown()
		{
			this.OnPlatformChange = null;
			PlatformSystemMessenger.OnMessageChange -= this.PlatformSystemMessenger_OnMessageChange;
			if (this._overlaySDK != null)
			{
				this._overlaySDK.OnToggle -= this.OverlaySDK_OnOverlayLocked;
			}
			if (this._sdk != null)
			{
				this._sdk.Dispose();
			}
			this._overlaySDK = null;
			this._sdk = null;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C724 File Offset: 0x0000AB24
		private void ErrorLogger(LogLevel level, string message)
		{
			switch (level)
			{
			case LogLevel.Error:
				CS.Platform.Utils.Debug.LogError("[DISCORDLINK] {0} | {1}", new object[]
				{
					level,
					message
				});
				return;
			case LogLevel.Warn:
				CS.Platform.Utils.Debug.LogWarning("[DISCORDLINK] {0} | {1}", new object[]
				{
					level,
					message
				});
				return;
			}
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] {0} | {1}", new object[]
			{
				level,
				message
			});
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C7B0 File Offset: 0x0000ABB0
		private void OverlaySDK_OnOverlayLocked(bool locked)
		{
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] Overlay lock changed: {0}", new object[]
			{
				locked
			});
			this.PlatformGUIActive = !locked;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C7D5 File Offset: 0x0000ABD5
		private void PlatformSystemMessenger_OnMessageChange(bool obj)
		{
			this.PlatformGUIActive = obj;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C7E0 File Offset: 0x0000ABE0
		public void Update()
		{
			try
			{
				if (this._sdk != null)
				{
					this._sdk.RunCallbacks();
				}
			}
			catch (Exception ex)
			{
				CS.Platform.Utils.Debug.LogError("[DISCORDLINK] Update Exception: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C838 File Offset: 0x0000AC38
		public void SetRichPresenceDetails(string presenceKey = null, string[] peramiters = null)
		{
			if (this._sdk == null)
			{
				return;
			}
			if (presenceKey == null)
			{
				this._richMessage.Details = null;
			}
			else
			{
				string discordPresence = CS.Platform.Utils.Presence.GetDiscordPresence(presenceKey);
				if (!string.IsNullOrEmpty(discordPresence))
				{
					this._richMessage.Details = Requests.GetTextLocalise(discordPresence, peramiters);
				}
				else
				{
					this._richMessage.Details = null;
				}
			}
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] SetRichPresenceDetails: Set | Details: {0}", new object[]
			{
				this._richMessage.Details
			});
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C8BC File Offset: 0x0000ACBC
		public void SetRichPresenceStatus(string statusKey = null)
		{
			if (this._sdk == null)
			{
				return;
			}
			if (statusKey == null)
			{
				this._richMessage.State = null;
			}
			else
			{
				string discordStatus = CS.Platform.Utils.Presence.GetDiscordStatus(statusKey);
				if (!string.IsNullOrEmpty(discordStatus))
				{
					this._richMessage.State = Requests.GetTextLocalise(discordStatus, null);
				}
				else
				{
					this._richMessage.State = null;
				}
			}
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] SetRichPresenceStatus: Set | State: {0}", new object[]
			{
				this._richMessage.State
			});
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C940 File Offset: 0x0000AD40
		public void SetRichPresenceLargeImate(string imageKey = null)
		{
			if (this._sdk == null)
			{
				return;
			}
			if (imageKey == null)
			{
				this._richMessage.Assets.LargeImage = null;
				this._richMessage.Assets.LargeText = null;
			}
			else
			{
				this._richMessage.Assets.LargeImage = CS.Platform.Utils.Presence.GetDiscordImage(imageKey);
				if (string.IsNullOrEmpty(this._richMessage.Assets.LargeImage))
				{
					this._richMessage.Assets.LargeImage = null;
				}
				string discordImageText = CS.Platform.Utils.Presence.GetDiscordImageText(imageKey);
				if (discordImageText != null)
				{
					this._richMessage.Assets.LargeText = Requests.GetTextLocalise(discordImageText, null);
				}
				if (!string.IsNullOrEmpty(this._richMessage.Assets.LargeText))
				{
					this._richMessage.Assets.LargeText = this._richMessage.Assets.LargeText.Trim();
				}
				if (string.IsNullOrEmpty(this._richMessage.Assets.LargeText))
				{
					this._richMessage.Assets.LargeText = this._richMessage.Assets.LargeImage;
				}
			}
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] SetRichPresenceLargeImate: Set | LargeText: {0} | LargeImage: {1}", new object[]
			{
				this._richMessage.Assets.LargeText,
				this._richMessage.Assets.LargeImage
			});
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000CA9C File Offset: 0x0000AE9C
		public void SetRichPresenceSmallImage(string imageKey = null)
		{
			if (this._sdk == null)
			{
				return;
			}
			if (imageKey == null)
			{
				this._richMessage.Assets.SmallImage = null;
				this._richMessage.Assets.SmallText = null;
			}
			else
			{
				this._richMessage.Assets.SmallImage = CS.Platform.Utils.Presence.GetDiscordImage(imageKey);
				if (string.IsNullOrEmpty(this._richMessage.Assets.SmallImage))
				{
					this._richMessage.Assets.SmallImage = null;
				}
				string discordImageText = CS.Platform.Utils.Presence.GetDiscordImageText(imageKey);
				if (discordImageText != null)
				{
					this._richMessage.Assets.SmallText = Requests.GetTextLocalise(discordImageText, null);
				}
				if (!string.IsNullOrEmpty(this._richMessage.Assets.SmallText))
				{
					this._richMessage.Assets.SmallText = this._richMessage.Assets.SmallText.Trim();
				}
				if (string.IsNullOrEmpty(this._richMessage.Assets.SmallText))
				{
					this._richMessage.Assets.SmallText = this._richMessage.Assets.SmallImage;
				}
			}
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] SetRichPresenceSmallImage: Set | SmallText: {0} | SmallImage: {1}", new object[]
			{
				this._richMessage.Assets.SmallText,
				this._richMessage.Assets.SmallImage
			});
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CBF8 File Offset: 0x0000AFF8
		public void SendRichPresence()
		{
			if (this._sdk == null)
			{
				return;
			}
			CS.Platform.Utils.Debug.LogInfo("[DUTILS] SendRichPresence: Updating", new object[0]);
			this._activitiesSDK.UpdateActivity(this._richMessage, new ActivityManager.UpdateActivityHandler(this.OnPresenceUpdated));
			CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] SendRichPresence: Updating Sent", new object[0]);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CC4E File Offset: 0x0000B04E
		private void OnPresenceUpdated(Result result)
		{
			if (result == Result.Ok)
			{
				CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] Presence Updated", new object[0]);
			}
			else
			{
				CS.Platform.Utils.Debug.LogInfo("[DISCORDLINK] Presence Updated | Result: {0}", new object[]
				{
					result
				});
			}
		}

		// Token: 0x04000100 RID: 256
		private Discord.Discord _sdk;

		// Token: 0x04000101 RID: 257
		private ActivityManager _activitiesSDK;

		// Token: 0x04000102 RID: 258
		private OverlayManager _overlaySDK;

		// Token: 0x04000103 RID: 259
		private UserManager _userSDK;

		// Token: 0x04000104 RID: 260
		private bool _connectedToSystem;

		// Token: 0x04000105 RID: 261
		private bool _guiActive;

		// Token: 0x04000106 RID: 262
		private bool _platformGUI;

		// Token: 0x04000107 RID: 263
		private DateTime _gameStartTime;

		// Token: 0x04000108 RID: 264
		private Activity _richMessage = default(Activity);

		// Token: 0x04000109 RID: 265
		public Action<bool> OnPlatformChange;
	}
}
