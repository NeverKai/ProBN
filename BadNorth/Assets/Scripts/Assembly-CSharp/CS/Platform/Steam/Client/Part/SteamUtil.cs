using System;
using System.Collections;
using CS.Platform.Utils;
using Steamworks;
using UnityEngine;

namespace CS.Platform.Steam.Client.Part
{
	// Token: 0x0200004B RID: 75
	public class SteamUtil : MonoBehaviour
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000FDDB File Offset: 0x0000E1DB
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000FDE3 File Offset: 0x0000E1E3
		public bool PlatformGUIActive
		{
			get
			{
				return this._platformGUI;
			}
			set
			{
				this._platformGUI = value;
				this.GUIActive = this._platformGUI;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000FDF8 File Offset: 0x0000E1F8
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000FE00 File Offset: 0x0000E200
		public bool GUIActive
		{
			get
			{
				return this._guiActive;
			}
			set
			{
				if (!value && this._platformGUI)
				{
					value = true;
				}
				if (!value && this._Manager.Dialog.ShowingMessage)
				{
					value = true;
				}
				if (!value && this._discordLink != null && this._discordLink.PlatformGUIActive)
				{
					value = true;
				}
				if (this._guiActive != value)
				{
					this._guiActive = value;
					PlatformEvents.PlatformGamePause(this._guiActive);
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000FE87 File Offset: 0x0000E287
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000FE8F File Offset: 0x0000E28F
		public int DiscordLinkUI { get; private set; }

		// Token: 0x06000314 RID: 788 RVA: 0x0000FE98 File Offset: 0x0000E298
		public void Awake()
		{
			this._Manager = base.GetComponent<SteamManager>();
			this._callbackOverlayChanged = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnOverlayChanged));
			this._callbackUserInfoReceived = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(this.OnUserInfoReceived));
			PlatformSystemMessenger.OnMessageChange += this.PlatformSystemMessenger_OnMessageChange;
			this._discordLink = null;
			this._discordLink = base.gameObject.AddComponent<SteamDiscordLink>();
			if (!this._discordLink.Connected)
			{
				UnityEngine.Object.Destroy(this._discordLink);
				this._discordLink = null;
			}
			else
			{
				SteamDiscordLink discordLink = this._discordLink;
				discordLink.OnPlatformChange = (Action<bool>)Delegate.Combine(discordLink.OnPlatformChange, new Action<bool>(this.OnDiscordUI));
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000FF56 File Offset: 0x0000E356
		private void OnDestroy()
		{
			if (this._discordLink != null)
			{
				SteamDiscordLink discordLink = this._discordLink;
				discordLink.OnPlatformChange = (Action<bool>)Delegate.Remove(discordLink.OnPlatformChange, new Action<bool>(this.OnDiscordUI));
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000FF90 File Offset: 0x0000E390
		private void OnDiscordUI(bool obj)
		{
			this.GUIActive = obj;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000FF99 File Offset: 0x0000E399
		private void PlatformSystemMessenger_OnMessageChange(bool obj)
		{
			this.PlatformGUIActive = obj;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000FFA2 File Offset: 0x0000E3A2
		private void OnOverlayChanged(GameOverlayActivated_t overlayEffect)
		{
			this._guiActive = Convert.ToBoolean(overlayEffect.m_bActive);
			PlatformEvents.PlatformGamePause(this._guiActive);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000FFC1 File Offset: 0x0000E3C1
		private void OnUserInfoReceived(PersonaStateChange_t personChanged)
		{
			if (personChanged.m_nChangeFlags == EPersonaChange.k_EPersonaChangeAvatar)
			{
				var name = string.Empty;
				// SteamFriends.GetFriendPersonaName((CSteamID) senderID);
				PlatformEvents.UserPictureLoaded(new BaseUserInfo(personChanged.m_ulSteamID, this._Manager.Key, 
					name));
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000FFFE File Offset: 0x0000E3FE
		public void AddPlayerToPlayedList(CSteamID userID)
		{
			//SteamFriends.SetPlayedWith(userID);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010006 File Offset: 0x0000E406
		public void WantsUserInfo(CSteamID userID, bool picture)
		{
			//SteamFriends.RequestUserInformation(userID, picture);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010010 File Offset: 0x0000E410
		public void SetUpJoinInfo(string serverIP = "", string serverPort = "")
		{
			if (serverIP == string.Empty || serverPort == string.Empty)
			{
				//SteamFriends.SetRichPresence("connect", string.Empty);
			}
			else
			{
				//SteamFriends.SetRichPresence("connect", serverIP + ":" + serverPort);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001006C File Offset: 0x0000E46C
		public void SetRichPresenceDetails(string presenceKey = null, string[] parameters = null)
		{
			CS.Platform.Utils.Debug.LogInfo("[STEAMUTIL] SetRichPresenceDetails: Set | Key: {0}", new object[]
			{
				presenceKey
			});
			if (presenceKey == null)
			{
				this._details = null;
			}
			else
			{
				string steamPresence = Presence.GetSteamPresence(presenceKey);
				if (!string.IsNullOrEmpty(steamPresence))
				{
					this._details = Requests.GetTextLocalise(steamPresence, parameters);
				}
				else
				{
					this._details = null;
				}
			}
			if (this._discordLink != null)
			{
				this._discordLink.SetRichPresenceDetails(presenceKey, parameters);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000100E8 File Offset: 0x0000E4E8
		public void SetRichPresenceStatus(string statusKey = null)
		{
			CS.Platform.Utils.Debug.LogInfo("[STEAMUTIL] SetRichPresenceStatus: Set | Key: {0}", new object[]
			{
				statusKey
			});
			if (statusKey == null)
			{
				this._status = null;
			}
			else
			{
				string steamStatus = Presence.GetSteamStatus(statusKey);
				if (!string.IsNullOrEmpty(steamStatus))
				{
					this._status = Requests.GetTextLocalise(steamStatus, null);
				}
				else
				{
					this._status = null;
				}
			}
			if (this._discordLink != null)
			{
				this._discordLink.SetRichPresenceStatus(statusKey);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00010164 File Offset: 0x0000E564
		public void SendRichPresence()
		{
			string text = this._status;
			if (text != null && this._details != null)
			{
				text = this._status + Presence.GetConnecter() + this._details;
			}
			else if (this._details != null)
			{
				text = this._details;
			}
			CS.Platform.Utils.Debug.LogInfo("[STEAMUTIL] SendRichPresence: Set | Sending: {0}", new object[]
			{
				text
			});
			if (this._discordLink != null)
			{
				this._discordLink.SendRichPresence();
			}
			//SteamFriends.SetRichPresence("status", text);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000101F3 File Offset: 0x0000E5F3
		public void SetRichPresenceLargeImate(string imageKey = null)
		{
			CS.Platform.Utils.Debug.LogInfo("[STEAMUTIL] SetRichPresenceLargeImate: Set | Key: {0}", new object[]
			{
				imageKey
			});
			if (this._discordLink != null)
			{
				this._discordLink.SetRichPresenceLargeImate(imageKey);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00010226 File Offset: 0x0000E626
		public void SetRichPresenceSmallImage(string imageKey = null)
		{
			CS.Platform.Utils.Debug.LogInfo("[STEAMUTIL] SetRichPresenceSmallImage: Set | Key: {0}", new object[]
			{
				imageKey
			});
			if (this._discordLink != null)
			{
				this._discordLink.SetRichPresenceSmallImage(imageKey);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001025C File Offset: 0x0000E65C
		public bool LoadProfileImage(CSteamID userID, ref Texture2D imageLocation)
		{
			bool result = false;
			// int mediumFriendAvatar = SteamFriends.GetMediumFriendAvatar(userID);
			int mediumFriendAvatar = 0;
			if (mediumFriendAvatar != 0)
			{
				if (SteamUtils.GetImageRGBA(mediumFriendAvatar, this.imageBuffer64, this.imageBuffer64.Length))
				{
					if (imageLocation == null)
					{
						imageLocation = CS.Platform.Utils.Random.CreateNewImageF(false, 64, 64, TextureFormat.RGBA32, false);
					}
					else if (imageLocation.width != 64 || imageLocation.height != 64 || imageLocation.format != TextureFormat.RGBA32)
					{
						imageLocation = CS.Platform.Utils.Random.CreateNewImageF(false, 64, 64, TextureFormat.RGBA32, false);
					}
					for (int i = 0; i < 32; i++)
					{
						Buffer.BlockCopy(this.imageBuffer64, (63 - i) * this.imageBuffer64Row.Length, this.imageBuffer64Row, 0, this.imageBuffer64Row.Length);
						Buffer.BlockCopy(this.imageBuffer64, i * this.imageBuffer64Row.Length, this.imageBuffer64, (63 - i) * this.imageBuffer64Row.Length, this.imageBuffer64Row.Length - 1);
						Buffer.BlockCopy(this.imageBuffer64Row, 0, this.imageBuffer64, i * this.imageBuffer64Row.Length, this.imageBuffer64Row.Length);
					}
					imageLocation.LoadRawTextureData(this.imageBuffer64);
					imageLocation.Apply();
					result = true;
				}
				else
				{
					CS.Platform.Utils.Debug.LogWarning("[Steamworks] Failed to load medium user image data.", new object[0]);
				}
			}
			else
			{
				CS.Platform.Utils.Debug.LogInfo("[Steamworks] Failed to find medium user image index.", new object[0]);
			}
			return result;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000103B9 File Offset: 0x0000E7B9
		public void UpdateUserCloudData()
		{
			if (this._updateCall == null)
			{
				this._updateCall = base.StartCoroutine(this.UpdateCloudStorage());
			}
			else
			{
				this._waitingStorageRecall = true;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000103E4 File Offset: 0x0000E7E4
		private IEnumerator UpdateCloudStorage()
		{
			SteamUserStats.StoreStats();
			yield return new WaitForSeconds(5f);
			if (this._waitingStorageRecall)
			{
				this._updateCall = base.StartCoroutine(this.UpdateCloudStorage());
			}
			else
			{
				this._updateCall = null;
			}
			yield break;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00010400 File Offset: 0x0000E800
		public void ParseCommandLine(string connectString, string password)
		{
			int num = connectString.IndexOf(":");
			if (num != -1)
			{
				PlatformEvents.JoinGame(connectString.Substring(0, num), Convert.ToInt32(connectString.Substring(num + 1)), password);
			}
		}

		// Token: 0x0400014A RID: 330
		private SteamManager _Manager;

		// Token: 0x0400014B RID: 331
		private bool _guiActive;

		// Token: 0x0400014C RID: 332
		private bool _platformGUI;

		// Token: 0x0400014D RID: 333
		private SteamDiscordLink _discordLink;

		// Token: 0x0400014F RID: 335
		protected Callback<GameOverlayActivated_t> _callbackOverlayChanged;

		// Token: 0x04000150 RID: 336
		protected Callback<PersonaStateChange_t> _callbackUserInfoReceived;

		// Token: 0x04000151 RID: 337
		private string _details;

		// Token: 0x04000152 RID: 338
		private string _status;

		// Token: 0x04000153 RID: 339
		private byte[] imageBuffer64 = new byte[32768];

		// Token: 0x04000154 RID: 340
		private byte[] imageBuffer64Row = new byte[256];

		// Token: 0x04000155 RID: 341
		private Coroutine _updateCall;

		// Token: 0x04000156 RID: 342
		private bool _waitingStorageRecall;

		// Token: 0x04000157 RID: 343
		private const float SteamRepeatDelay = 5f;

		// Token: 0x04000158 RID: 344
		public static readonly string CommandConnectKey = "+connect";

		// Token: 0x04000159 RID: 345
		public static readonly string CommandPasswordKey = "+password";
	}
}
