using System;
using System.Diagnostics;
using CS.Platform.Utils.Data;
using Rewired.Integration.UnityUI;
using RTM.UISystem;

namespace CS.Platform
{
	// Token: 0x0200005D RID: 93
	public static class PlatformEvents
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600040E RID: 1038 RVA: 0x00012748 File Offset: 0x00010B48
		// (remove) Token: 0x0600040F RID: 1039 RVA: 0x0001277C File Offset: 0x00010B7C
		
		public static event PlatformEvents.PlatformVoidEventDel OnGameSetup;

		// Token: 0x06000410 RID: 1040 RVA: 0x000127B0 File Offset: 0x00010BB0
		public static void GameSetup()
		{
			if (PlatformEvents.OnGameSetup != null)
			{
				PlatformEvents.OnGameSetup();
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000411 RID: 1041 RVA: 0x000127C8 File Offset: 0x00010BC8
		// (remove) Token: 0x06000412 RID: 1042 RVA: 0x000127FC File Offset: 0x00010BFC
		
		public static event PlatformEvents.PlatformVoidEventDel OnOnlineConnectionLost;

		// Token: 0x06000413 RID: 1043 RVA: 0x00012830 File Offset: 0x00010C30
		public static void OnlineConnectionLost()
		{
			if (PlatformEvents.OnOnlineConnectionLost != null)
			{
				PlatformEvents.OnOnlineConnectionLost();
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000414 RID: 1044 RVA: 0x00012848 File Offset: 0x00010C48
		// (remove) Token: 0x06000415 RID: 1045 RVA: 0x0001287C File Offset: 0x00010C7C
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyJoinedEvent;

		// Token: 0x06000416 RID: 1046 RVA: 0x000128B0 File Offset: 0x00010CB0
		public static void LobbyJoined()
		{
			if (PlatformEvents.OnLobbyJoinedEvent != null)
			{
				PlatformEvents.OnLobbyJoinedEvent();
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000417 RID: 1047 RVA: 0x000128C8 File Offset: 0x00010CC8
		// (remove) Token: 0x06000418 RID: 1048 RVA: 0x000128FC File Offset: 0x00010CFC
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyLeftEvent;

		// Token: 0x06000419 RID: 1049 RVA: 0x00012930 File Offset: 0x00010D30
		public static void LobbyLeft()
		{
			if (PlatformEvents.OnLobbyLeftEvent != null)
			{
				PlatformEvents.OnLobbyLeftEvent();
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600041A RID: 1050 RVA: 0x00012948 File Offset: 0x00010D48
		// (remove) Token: 0x0600041B RID: 1051 RVA: 0x0001297C File Offset: 0x00010D7C
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyHostEvent;

		// Token: 0x0600041C RID: 1052 RVA: 0x000129B0 File Offset: 0x00010DB0
		public static void LobbyHost()
		{
			if (PlatformEvents.OnLobbyHostEvent != null)
			{
				PlatformEvents.OnLobbyHostEvent();
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600041D RID: 1053 RVA: 0x000129C8 File Offset: 0x00010DC8
		// (remove) Token: 0x0600041E RID: 1054 RVA: 0x000129FC File Offset: 0x00010DFC
		
		public static event PlatformEvents.PlatformVoidEventDel OnSaveLocalStartedEvent;

		// Token: 0x0600041F RID: 1055 RVA: 0x00012A30 File Offset: 0x00010E30
		public static void SaveLocalStarted()
		{
			if (PlatformEvents.OnSaveLocalStartedEvent != null)
			{
				PlatformEvents.OnSaveLocalStartedEvent();
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000420 RID: 1056 RVA: 0x00012A48 File Offset: 0x00010E48
		// (remove) Token: 0x06000421 RID: 1057 RVA: 0x00012A7C File Offset: 0x00010E7C
		
		public static event PlatformEvents.PlatformVoidEventDel OnSaveLocalCompleteEvent;

		// Token: 0x06000422 RID: 1058 RVA: 0x00012AB0 File Offset: 0x00010EB0
		public static void SaveLocalComplete()
		{
			if (PlatformEvents.OnSaveLocalCompleteEvent != null)
			{
				PlatformEvents.OnSaveLocalCompleteEvent();
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000423 RID: 1059 RVA: 0x00012AC8 File Offset: 0x00010EC8
		// (remove) Token: 0x06000424 RID: 1060 RVA: 0x00012AFC File Offset: 0x00010EFC
		
		public static event PlatformEvents.PlatformVoidEventDel OnLoadLocalStartedEvent;

		// Token: 0x06000425 RID: 1061 RVA: 0x00012B30 File Offset: 0x00010F30
		public static void LoadLocalStarted()
		{
			if (PlatformEvents.OnLoadLocalStartedEvent != null)
			{
				PlatformEvents.OnLoadLocalStartedEvent();
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000426 RID: 1062 RVA: 0x00012B48 File Offset: 0x00010F48
		// (remove) Token: 0x06000427 RID: 1063 RVA: 0x00012B7C File Offset: 0x00010F7C
		
		public static event PlatformEvents.PlatformVoidEventDel OnLoadLocalCompleteEvent;

		// Token: 0x06000428 RID: 1064 RVA: 0x00012BB0 File Offset: 0x00010FB0
		public static void LoadLocalComplete()
		{
			if (PlatformEvents.OnLoadLocalCompleteEvent != null)
			{
				PlatformEvents.OnLoadLocalCompleteEvent();
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000429 RID: 1065 RVA: 0x00012BC8 File Offset: 0x00010FC8
		// (remove) Token: 0x0600042A RID: 1066 RVA: 0x00012BFC File Offset: 0x00010FFC
		
		public static event PlatformEvents.PlatformVoidEventDel OnReceivedGameInviteEvent;

		// Token: 0x0600042B RID: 1067 RVA: 0x00012C30 File Offset: 0x00011030
		public static void ReceivedGameInvite()
		{
			if (PlatformEvents.OnReceivedGameInviteEvent != null)
			{
				PlatformEvents.OnReceivedGameInviteEvent();
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600042C RID: 1068 RVA: 0x00012C48 File Offset: 0x00011048
		// (remove) Token: 0x0600042D RID: 1069 RVA: 0x00012C7C File Offset: 0x0001107C
		
		public static event PlatformEvents.PlatformVoidEventDel OnUsedGameInviteEvent;

		// Token: 0x0600042E RID: 1070 RVA: 0x00012CB0 File Offset: 0x000110B0
		public static void UsedGameInviteEvent()
		{
			if (PlatformEvents.OnUsedGameInviteEvent != null)
			{
				PlatformEvents.OnUsedGameInviteEvent();
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600042F RID: 1071 RVA: 0x00012CC8 File Offset: 0x000110C8
		// (remove) Token: 0x06000430 RID: 1072 RVA: 0x00012CFC File Offset: 0x000110FC
		
		public static event PlatformEvents.PlatformVoidEventDel OnReceivedLobbyInviteEvent;

		// Token: 0x06000431 RID: 1073 RVA: 0x00012D30 File Offset: 0x00011130
		public static void ReceivedLobbyInvite()
		{
			if (PlatformEvents.OnReceivedLobbyInviteEvent != null)
			{
				PlatformEvents.OnReceivedLobbyInviteEvent();
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000432 RID: 1074 RVA: 0x00012D48 File Offset: 0x00011148
		// (remove) Token: 0x06000433 RID: 1075 RVA: 0x00012D7C File Offset: 0x0001117C
		
		public static event PlatformEvents.PlatformVoidEventDel OnUsedLobbyInviteEvent;

		// Token: 0x06000434 RID: 1076 RVA: 0x00012DB0 File Offset: 0x000111B0
		public static void UsedLobbyInviteEvent()
		{
			if (PlatformEvents.OnUsedLobbyInviteEvent != null)
			{
				PlatformEvents.OnUsedLobbyInviteEvent();
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000435 RID: 1077 RVA: 0x00012DC8 File Offset: 0x000111C8
		// (remove) Token: 0x06000436 RID: 1078 RVA: 0x00012DFC File Offset: 0x000111FC
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyDataUpdatedEvent;

		// Token: 0x06000437 RID: 1079 RVA: 0x00012E30 File Offset: 0x00011230
		public static void LobbyDataUpdated()
		{
			if (PlatformEvents.OnLobbyDataUpdatedEvent != null)
			{
				PlatformEvents.OnLobbyDataUpdatedEvent();
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000438 RID: 1080 RVA: 0x00012E48 File Offset: 0x00011248
		// (remove) Token: 0x06000439 RID: 1081 RVA: 0x00012E7C File Offset: 0x0001127C
		
		public static event Action<int> OnOnlineCheckStartedEvent;

		// Token: 0x0600043A RID: 1082 RVA: 0x00012EB0 File Offset: 0x000112B0
		public static void OnlineCheckStarted(int controller)
		{
			if (PlatformEvents.OnOnlineCheckStartedEvent != null)
			{
				PlatformEvents.OnOnlineCheckStartedEvent(controller);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600043B RID: 1083 RVA: 0x00012EC8 File Offset: 0x000112C8
		// (remove) Token: 0x0600043C RID: 1084 RVA: 0x00012EFC File Offset: 0x000112FC
		
		public static event Action<int, TryOnlineResult> OnOnlineCheckCompleteEvent;

		// Token: 0x0600043D RID: 1085 RVA: 0x00012F30 File Offset: 0x00011330
		public static void OnlineCheckComplete(int controller, TryOnlineResult result)
		{
			if (PlatformEvents.OnOnlineCheckCompleteEvent != null)
			{
				PlatformEvents.OnOnlineCheckCompleteEvent(controller, result);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600043E RID: 1086 RVA: 0x00012F48 File Offset: 0x00011348
		// (remove) Token: 0x0600043F RID: 1087 RVA: 0x00012F7C File Offset: 0x0001137C
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyJoiningEvent;

		// Token: 0x06000440 RID: 1088 RVA: 0x00012FB0 File Offset: 0x000113B0
		public static void LobbyJoining()
		{
			if (PlatformEvents.OnLobbyJoiningEvent != null)
			{
				PlatformEvents.OnLobbyJoiningEvent();
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000441 RID: 1089 RVA: 0x00012FC8 File Offset: 0x000113C8
		// (remove) Token: 0x06000442 RID: 1090 RVA: 0x00012FFC File Offset: 0x000113FC
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyCreatingEvent;

		// Token: 0x06000443 RID: 1091 RVA: 0x00013030 File Offset: 0x00011430
		public static void LobbyCreating()
		{
			if (PlatformEvents.OnLobbyCreatingEvent != null)
			{
				PlatformEvents.OnLobbyCreatingEvent();
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000444 RID: 1092 RVA: 0x00013048 File Offset: 0x00011448
		// (remove) Token: 0x06000445 RID: 1093 RVA: 0x0001307C File Offset: 0x0001147C
		
		public static event PlatformEvents.PlatformVoidEventDel OnLobbyLeavingEvent;

		// Token: 0x06000446 RID: 1094 RVA: 0x000130B0 File Offset: 0x000114B0
		public static void LobbyLeaving()
		{
			if (PlatformEvents.OnLobbyLeavingEvent != null)
			{
				PlatformEvents.OnLobbyLeavingEvent();
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000447 RID: 1095 RVA: 0x000130C8 File Offset: 0x000114C8
		// (remove) Token: 0x06000448 RID: 1096 RVA: 0x000130FC File Offset: 0x000114FC
		
		public static event PlatformEvents.PlatformVoidEventDel OnPlatformInitializedEvent;

		// Token: 0x06000449 RID: 1097 RVA: 0x00013130 File Offset: 0x00011530
		public static void PlatformInitialized()
		{
			if (PlatformEvents.OnPlatformInitializedEvent != null)
			{
				PlatformEvents.OnPlatformInitializedEvent();
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600044A RID: 1098 RVA: 0x00013148 File Offset: 0x00011548
		// (remove) Token: 0x0600044B RID: 1099 RVA: 0x0001317C File Offset: 0x0001157C
		
		public static event PlatformEvents.PlatformVoidEventDel OnSystemWaking;

		// Token: 0x0600044C RID: 1100 RVA: 0x000131B0 File Offset: 0x000115B0
		public static void SystemWake()
		{
			if (PlatformEvents.OnSystemWaking != null)
			{
				PlatformEvents.OnSystemWaking();
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600044D RID: 1101 RVA: 0x000131C8 File Offset: 0x000115C8
		// (remove) Token: 0x0600044E RID: 1102 RVA: 0x000131FC File Offset: 0x000115FC
		
		public static event PlatformEvents.PlatformJoinEventDel OnJoinGameEvent;

		// Token: 0x0600044F RID: 1103 RVA: 0x00013230 File Offset: 0x00011630
		public static void JoinGame(string IP, int port, string password)
		{
			if (PlatformEvents.OnJoinGameEvent != null)
			{
				PlatformEvents.OnJoinGameEvent(IP, port, password);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000450 RID: 1104 RVA: 0x0001324C File Offset: 0x0001164C
		// (remove) Token: 0x06000451 RID: 1105 RVA: 0x00013280 File Offset: 0x00011680
		
		public static event PlatformEvents.PlatformUserEventDel OnUserPictureLoadedEvent;

		// Token: 0x06000452 RID: 1106 RVA: 0x000132B4 File Offset: 0x000116B4
		public static void UserPictureLoaded(BaseUserInfo info)
		{
			if (PlatformEvents.OnUserPictureLoadedEvent != null)
			{
				PlatformEvents.OnUserPictureLoadedEvent(info);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000453 RID: 1107 RVA: 0x000132CC File Offset: 0x000116CC
		// (remove) Token: 0x06000454 RID: 1108 RVA: 0x00013300 File Offset: 0x00011700
		
		public static event PlatformEvents.PlatformUserEventDel OnLobbyUserJoinedEvent;

		// Token: 0x06000455 RID: 1109 RVA: 0x00013334 File Offset: 0x00011734
		public static void LobbyUserJoined(BaseUserInfo info)
		{
			if (PlatformEvents.OnLobbyUserJoinedEvent != null)
			{
				PlatformEvents.OnLobbyUserJoinedEvent(info);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000456 RID: 1110 RVA: 0x0001334C File Offset: 0x0001174C
		// (remove) Token: 0x06000457 RID: 1111 RVA: 0x00013380 File Offset: 0x00011780
		
		public static event PlatformEvents.PlatformUserEventDel OnLobbyUserLeftEvent;

		// Token: 0x06000458 RID: 1112 RVA: 0x000133B4 File Offset: 0x000117B4
		public static void LobbyUserLeft(BaseUserInfo info)
		{
			if (PlatformEvents.OnLobbyUserLeftEvent != null)
			{
				PlatformEvents.OnLobbyUserLeftEvent(info);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000459 RID: 1113 RVA: 0x000133CC File Offset: 0x000117CC
		// (remove) Token: 0x0600045A RID: 1114 RVA: 0x00013400 File Offset: 0x00011800
		
		public static event PlatformEvents.PlatformUserEventDel OnLobbyNewHostEvent;

		// Token: 0x0600045B RID: 1115 RVA: 0x00013434 File Offset: 0x00011834
		public static void LobbyNewHost(BaseUserInfo info)
		{
			if (PlatformEvents.OnLobbyNewHostEvent != null)
			{
				PlatformEvents.OnLobbyNewHostEvent(info);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600045C RID: 1116 RVA: 0x0001344C File Offset: 0x0001184C
		// (remove) Token: 0x0600045D RID: 1117 RVA: 0x00013480 File Offset: 0x00011880
		
		public static event PlatformEvents.PlatformUserBoolEventDel OnVoiceActiveStateChangedEvent;

		// Token: 0x0600045E RID: 1118 RVA: 0x000134B4 File Offset: 0x000118B4
		public static void VoiceActiveStateChanged(BaseUserInfo info, bool active)
		{
			if (PlatformEvents.OnVoiceActiveStateChangedEvent != null)
			{
				PlatformEvents.OnVoiceActiveStateChangedEvent(info, active);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600045F RID: 1119 RVA: 0x000134CC File Offset: 0x000118CC
		// (remove) Token: 0x06000460 RID: 1120 RVA: 0x00013500 File Offset: 0x00011900
		
		public static event PlatformEvents.PlatformUserBoolEventDel OnVoiceMuteStateChangedEvent;

		// Token: 0x06000461 RID: 1121 RVA: 0x00013534 File Offset: 0x00011934
		public static void VoiceMuteStateChanged(BaseUserInfo info, bool mute)
		{
			if (PlatformEvents.OnVoiceMuteStateChangedEvent != null)
			{
				PlatformEvents.OnVoiceMuteStateChangedEvent(info, mute);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000462 RID: 1122 RVA: 0x0001354C File Offset: 0x0001194C
		// (remove) Token: 0x06000463 RID: 1123 RVA: 0x00013580 File Offset: 0x00011980
		
		public static event PlatformEvents.PlatformBoolEventDel OnPlatformGamePauseEvent;

		// Token: 0x06000464 RID: 1124 RVA: 0x000135B4 File Offset: 0x000119B4
		public static void PlatformGamePause(bool on)
		{
			//RewiredStandaloneInputModule.systemBlocked = on;
			Singleton<UIManager>.instance.blockUIInput = on;
			if (PlatformEvents.OnPlatformGamePauseEvent != null)
			{
				PlatformEvents.OnPlatformGamePauseEvent(on);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000465 RID: 1125 RVA: 0x000135DC File Offset: 0x000119DC
		// (remove) Token: 0x06000466 RID: 1126 RVA: 0x00013610 File Offset: 0x00011A10
		
		public static event PlatformEvents.PlatformBoolEventDel OnVoiceMicChangeEvent;

		// Token: 0x06000467 RID: 1127 RVA: 0x00013644 File Offset: 0x00011A44
		public static void VoiceMicChange(bool allowed)
		{
			if (PlatformEvents.OnVoiceMicChangeEvent != null)
			{
				PlatformEvents.OnVoiceMicChangeEvent(allowed);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000468 RID: 1128 RVA: 0x0001365C File Offset: 0x00011A5C
		// (remove) Token: 0x06000469 RID: 1129 RVA: 0x00013690 File Offset: 0x00011A90
		
		public static event PlatformEvents.PlatformBoolEventDel OnEntitlementChangedEvent;

		// Token: 0x0600046A RID: 1130 RVA: 0x000136C4 File Offset: 0x00011AC4
		public static void EntitlementChanged(bool passed)
		{
			if (PlatformEvents.OnEntitlementChangedEvent != null)
			{
				PlatformEvents.OnEntitlementChangedEvent(passed);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600046B RID: 1131 RVA: 0x000136DC File Offset: 0x00011ADC
		// (remove) Token: 0x0600046C RID: 1132 RVA: 0x00013710 File Offset: 0x00011B10
		
		public static event Action<bool> OnSaveLoadFailEvent;

		// Token: 0x0600046D RID: 1133 RVA: 0x00013744 File Offset: 0x00011B44
		public static void SaveLoadFail(bool save)
		{
			if (PlatformEvents.OnSaveLoadFailEvent != null)
			{
				PlatformEvents.OnSaveLoadFailEvent(save);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600046E RID: 1134 RVA: 0x0001375C File Offset: 0x00011B5C
		// (remove) Token: 0x0600046F RID: 1135 RVA: 0x00013790 File Offset: 0x00011B90
		
		public static event PlatformEvents.PlatformMessageEventDel OnReceivedLobbyHostMessageEvent;

		// Token: 0x06000470 RID: 1136 RVA: 0x000137C4 File Offset: 0x00011BC4
		public static void ReceivedLobbyHostMessage(DataReader message)
		{
			if (PlatformEvents.OnReceivedLobbyHostMessageEvent != null)
			{
				PlatformEvents.OnReceivedLobbyHostMessageEvent(message);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000471 RID: 1137 RVA: 0x000137DC File Offset: 0x00011BDC
		// (remove) Token: 0x06000472 RID: 1138 RVA: 0x00013810 File Offset: 0x00011C10
		
		public static event PlatformEvents.PlatformUserMessageEventDel OnReceivedLobbyClientMessageEvent;

		// Token: 0x06000473 RID: 1139 RVA: 0x00013844 File Offset: 0x00011C44
		public static void ReceivedLobbyClientMessage(BaseUserInfo user, DataReader message)
		{
			if (PlatformEvents.OnReceivedLobbyClientMessageEvent != null)
			{
				PlatformEvents.OnReceivedLobbyClientMessageEvent(user, message);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000474 RID: 1140 RVA: 0x0001385C File Offset: 0x00011C5C
		// (remove) Token: 0x06000475 RID: 1141 RVA: 0x00013890 File Offset: 0x00011C90
		
		public static event PlatformEvents.PlatformUserMessageEventDel OnReceivedLobbyUserMessageEvent;

		// Token: 0x06000476 RID: 1142 RVA: 0x000138C4 File Offset: 0x00011CC4
		public static void ReceivedLobbyUserMessage(BaseUserInfo user, DataReader message)
		{
			if (PlatformEvents.OnReceivedLobbyUserMessageEvent != null)
			{
				PlatformEvents.OnReceivedLobbyUserMessageEvent(user, message);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000477 RID: 1143 RVA: 0x000138DC File Offset: 0x00011CDC
		// (remove) Token: 0x06000478 RID: 1144 RVA: 0x00013910 File Offset: 0x00011D10
		
		public static event PlatformEvents.PlatformUserMessageEventDel OnReceivedUserMessageEvent;

		// Token: 0x06000479 RID: 1145 RVA: 0x00013944 File Offset: 0x00011D44
		public static void ReceivedUserMessage(BaseUserInfo user, DataReader message)
		{
			if (PlatformEvents.OnReceivedUserMessageEvent != null)
			{
				PlatformEvents.OnReceivedUserMessageEvent(user, message);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600047A RID: 1146 RVA: 0x0001395C File Offset: 0x00011D5C
		// (remove) Token: 0x0600047B RID: 1147 RVA: 0x00013990 File Offset: 0x00011D90
		
		public static event PlatformEvents.PlatformErrorCodesEventDel OnLobbyCreationFailedEvent;

		// Token: 0x0600047C RID: 1148 RVA: 0x000139C4 File Offset: 0x00011DC4
		public static void LobbyCreationFailed(ErrorCode errorCode)
		{
			if (PlatformEvents.OnLobbyCreationFailedEvent != null)
			{
				PlatformEvents.OnLobbyCreationFailedEvent(errorCode);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600047D RID: 1149 RVA: 0x000139DC File Offset: 0x00011DDC
		// (remove) Token: 0x0600047E RID: 1150 RVA: 0x00013A10 File Offset: 0x00011E10
		
		public static event PlatformEvents.PlatformErrorCodesEventDel OnLobbyJoiningFailedEvent;

		// Token: 0x0600047F RID: 1151 RVA: 0x00013A44 File Offset: 0x00011E44
		public static void LobbyJoiningFailed(ErrorCode errorCode)
		{
			if (PlatformEvents.OnLobbyJoiningFailedEvent != null)
			{
				PlatformEvents.OnLobbyJoiningFailedEvent(errorCode);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000480 RID: 1152 RVA: 0x00013A5C File Offset: 0x00011E5C
		// (remove) Token: 0x06000481 RID: 1153 RVA: 0x00013A90 File Offset: 0x00011E90
		
		public static event PlatformEvents.PlatformUserChangeEventDel OnLocalUserChanged;

		// Token: 0x06000482 RID: 1154 RVA: 0x00013AC4 File Offset: 0x00011EC4
		public static void LocalUserChanged(int slot, int user, bool joined)
		{
			if (PlatformEvents.OnLocalUserChanged != null)
			{
				PlatformEvents.OnLocalUserChanged(slot, user, joined);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000483 RID: 1155 RVA: 0x00013AE0 File Offset: 0x00011EE0
		// (remove) Token: 0x06000484 RID: 1156 RVA: 0x00013B14 File Offset: 0x00011F14
		
		public static event PlatformEvents.PlatformUserChangeEventDel OnOnlineAllowedChangeEvent;

		// Token: 0x06000485 RID: 1157 RVA: 0x00013B48 File Offset: 0x00011F48
		public static void OnlineAllowedChange(int slot, int localuserID, bool allowed)
		{
			if (PlatformEvents.OnOnlineAllowedChangeEvent != null)
			{
				PlatformEvents.OnOnlineAllowedChangeEvent(slot, localuserID, allowed);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000486 RID: 1158 RVA: 0x00013B64 File Offset: 0x00011F64
		// (remove) Token: 0x06000487 RID: 1159 RVA: 0x00013B98 File Offset: 0x00011F98
		
		public static event PlatformEvents.PlatformBoolEventDel OnMainUserStateEvent;

		// Token: 0x06000488 RID: 1160 RVA: 0x00013BCC File Offset: 0x00011FCC
		public static void MainUserState(bool joined)
		{
			if (PlatformEvents.OnMainUserStateEvent != null)
			{
				PlatformEvents.OnMainUserStateEvent(joined);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000489 RID: 1161 RVA: 0x00013BE4 File Offset: 0x00011FE4
		// (remove) Token: 0x0600048A RID: 1162 RVA: 0x00013C18 File Offset: 0x00012018
		
		public static event PlatformEvents.PlatformVoidEventDel OnMainUserClearedEvent;

		// Token: 0x0600048B RID: 1163 RVA: 0x00013C4C File Offset: 0x0001204C
		public static void MainUserCleared()
		{
			if (PlatformEvents.OnMainUserClearedEvent != null)
			{
				PlatformEvents.OnMainUserClearedEvent();
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600048C RID: 1164 RVA: 0x00013C64 File Offset: 0x00012064
		// (remove) Token: 0x0600048D RID: 1165 RVA: 0x00013C98 File Offset: 0x00012098
		
		public static event PlatformEvents.PlatformVoidEventDel OnPlatformLoadUnblockedEvent;

		// Token: 0x0600048E RID: 1166 RVA: 0x00013CCC File Offset: 0x000120CC
		public static void PlatformLoadUnblocked()
		{
			if (PlatformEvents.OnPlatformLoadUnblockedEvent != null)
			{
				PlatformEvents.OnPlatformLoadUnblockedEvent();
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600048F RID: 1167 RVA: 0x00013CE4 File Offset: 0x000120E4
		// (remove) Token: 0x06000490 RID: 1168 RVA: 0x00013D18 File Offset: 0x00012118
		
		public static event Action<BaseUserInfo[]> OnFriendsListUpdatedEvent;

		// Token: 0x06000491 RID: 1169 RVA: 0x00013D4C File Offset: 0x0001214C
		public static void FriendsListUpdated(BaseUserInfo[] newList)
		{
			if (PlatformEvents.OnFriendsListUpdatedEvent != null)
			{
				PlatformEvents.OnFriendsListUpdatedEvent(newList);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000492 RID: 1170 RVA: 0x00013D64 File Offset: 0x00012164
		// (remove) Token: 0x06000493 RID: 1171 RVA: 0x00013D98 File Offset: 0x00012198
		
		public static event Action<string[]> OnSaveLocalFilesCompleteEvent;

		// Token: 0x06000494 RID: 1172 RVA: 0x00013DCC File Offset: 0x000121CC
		public static void SaveLocalFilesCompleteEvent(string[] files)
		{
			if (PlatformEvents.OnSaveLocalFilesCompleteEvent != null)
			{
				PlatformEvents.OnSaveLocalFilesCompleteEvent(files);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000495 RID: 1173 RVA: 0x00013DE4 File Offset: 0x000121E4
		// (remove) Token: 0x06000496 RID: 1174 RVA: 0x00013E18 File Offset: 0x00012218
		
		public static event Action<string[]> OnLoadLocalFilesCompleteEvent;

		// Token: 0x06000497 RID: 1175 RVA: 0x00013E4C File Offset: 0x0001224C
		public static void LoadLocalFilesComplete(string[] files)
		{
			if (PlatformEvents.OnLoadLocalFilesCompleteEvent != null)
			{
				PlatformEvents.OnLoadLocalFilesCompleteEvent(files);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000498 RID: 1176 RVA: 0x00013E64 File Offset: 0x00012264
		// (remove) Token: 0x06000499 RID: 1177 RVA: 0x00013E98 File Offset: 0x00012298
		
		public static event Action<string, byte[]> OnOffMainThreadDataLoaded;

		// Token: 0x0600049A RID: 1178 RVA: 0x00013ECC File Offset: 0x000122CC
		public static void OffMainThreadDataLoaded(string file, byte[] data)
		{
			if (PlatformEvents.OnOffMainThreadDataLoaded != null)
			{
				PlatformEvents.OnOffMainThreadDataLoaded(file, data);
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00013EE4 File Offset: 0x000122E4
		public static void FlushEvents()
		{
			PlatformEvents.OnEntitlementChangedEvent = null;
			PlatformEvents.OnPlatformGamePauseEvent = null;
			PlatformEvents.OnJoinGameEvent = null;
			PlatformEvents.OnUserPictureLoadedEvent = null;
			PlatformEvents.OnLobbyJoinedEvent = null;
			PlatformEvents.OnLobbyHostEvent = null;
			PlatformEvents.OnLobbyLeftEvent = null;
			PlatformEvents.OnLobbyUserJoinedEvent = null;
			PlatformEvents.OnLobbyUserLeftEvent = null;
			PlatformEvents.OnLobbyDataUpdatedEvent = null;
			PlatformEvents.OnLobbyNewHostEvent = null;
			PlatformEvents.OnSaveLocalStartedEvent = null;
			PlatformEvents.OnSaveLocalCompleteEvent = null;
			PlatformEvents.OnLoadLocalStartedEvent = null;
			PlatformEvents.OnLoadLocalCompleteEvent = null;
			PlatformEvents.OnSaveLoadFailEvent = null;
			PlatformEvents.OnReceivedGameInviteEvent = null;
			PlatformEvents.OnReceivedLobbyInviteEvent = null;
			PlatformEvents.OnReceivedUserMessageEvent = null;
			PlatformEvents.OnReceivedLobbyHostMessageEvent = null;
			PlatformEvents.OnReceivedLobbyClientMessageEvent = null;
			PlatformEvents.OnReceivedLobbyUserMessageEvent = null;
			PlatformEvents.OnOnlineAllowedChangeEvent = null;
			PlatformEvents.OnOnlineConnectionLost = null;
			PlatformEvents.OnVoiceMicChangeEvent = null;
			PlatformEvents.OnVoiceActiveStateChangedEvent = null;
			PlatformEvents.OnVoiceMuteStateChangedEvent = null;
		}

		// Token: 0x0200005E RID: 94
		// (Invoke) Token: 0x0600049E RID: 1182
		public delegate void PlatformVoidEventDel();

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x060004A2 RID: 1186
		public delegate void PlatformJoinEventDel(string IP, int port, string password);

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x060004A6 RID: 1190
		public delegate void PlatformUserEventDel(BaseUserInfo info);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x060004AA RID: 1194
		public delegate void PlatformUserBoolEventDel(BaseUserInfo info, bool effect);

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x060004AE RID: 1198
		public delegate void PlatformBoolEventDel(bool effect);

		// Token: 0x02000063 RID: 99
		// (Invoke) Token: 0x060004B2 RID: 1202
		public delegate void PlatformMessageEventDel(DataReader message);

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x060004B6 RID: 1206
		public delegate void PlatformUserMessageEventDel(BaseUserInfo user, DataReader message);

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x060004BA RID: 1210
		public delegate void PlatformErrorCodesEventDel(ErrorCode value);

		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x060004BE RID: 1214
		public delegate void PlatformUserChangeEventDel(int slot, int localuserID, bool joined);
	}
}
