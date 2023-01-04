using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Rewired.Utils.Libraries.TinyJson;
using UnityEngine;

namespace Rewired.Data
{
	// Token: 0x020004A8 RID: 1192
	public class UserDataStore_PlayerPrefs : UserDataStore
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0004E9E4 File Offset: 0x0004CDE4
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0004E9EC File Offset: 0x0004CDEC
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0004E9F5 File Offset: 0x0004CDF5
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0004E9FD File Offset: 0x0004CDFD
		public bool LoadDataOnStart
		{
			get
			{
				return this.loadDataOnStart;
			}
			set
			{
				this.loadDataOnStart = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x0004EA06 File Offset: 0x0004CE06
		// (set) Token: 0x06001D93 RID: 7571 RVA: 0x0004EA0E File Offset: 0x0004CE0E
		public bool LoadJoystickAssignments
		{
			get
			{
				return this.loadJoystickAssignments;
			}
			set
			{
				this.loadJoystickAssignments = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x0004EA17 File Offset: 0x0004CE17
		// (set) Token: 0x06001D95 RID: 7573 RVA: 0x0004EA1F File Offset: 0x0004CE1F
		public bool LoadKeyboardAssignments
		{
			get
			{
				return this.loadKeyboardAssignments;
			}
			set
			{
				this.loadKeyboardAssignments = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0004EA28 File Offset: 0x0004CE28
		// (set) Token: 0x06001D97 RID: 7575 RVA: 0x0004EA30 File Offset: 0x0004CE30
		public bool LoadMouseAssignments
		{
			get
			{
				return this.loadMouseAssignments;
			}
			set
			{
				this.loadMouseAssignments = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0004EA39 File Offset: 0x0004CE39
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0004EA41 File Offset: 0x0004CE41
		public string PlayerPrefsKeyPrefix
		{
			get
			{
				return this.playerPrefsKeyPrefix;
			}
			set
			{
				this.playerPrefsKeyPrefix = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0004EA4A File Offset: 0x0004CE4A
		private string playerPrefsKey_controllerAssignments
		{
			get
			{
				return string.Format("{0}_{1}", this.playerPrefsKeyPrefix, "ControllerAssignments");
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0004EA61 File Offset: 0x0004CE61
		private bool loadControllerAssignments
		{
			get
			{
				return this.loadKeyboardAssignments || this.loadMouseAssignments || this.loadJoystickAssignments;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0004EA84 File Offset: 0x0004CE84
		private List<int> allActionIds
		{
			get
			{
				if (this.__allActionIds != null)
				{
					return this.__allActionIds;
				}
				List<int> list = new List<int>();
				IList<InputAction> actions = ReInput.mapping.Actions;
				for (int i = 0; i < actions.Count; i++)
				{
					list.Add(actions[i].id);
				}
				this.__allActionIds = list;
				return list;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0004EAE8 File Offset: 0x0004CEE8
		private string allActionIdsString
		{
			get
			{
				if (!string.IsNullOrEmpty(this.__allActionIdsString))
				{
					return this.__allActionIdsString;
				}
				StringBuilder stringBuilder = new StringBuilder();
				List<int> allActionIds = this.allActionIds;
				for (int i = 0; i < allActionIds.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(allActionIds[i]);
				}
				this.__allActionIdsString = stringBuilder.ToString();
				return this.__allActionIdsString;
			}
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0004EB63 File Offset: 0x0004CF63
		public override void Save()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveAll();
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0004EB82 File Offset: 0x0004CF82
		public override void SaveControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0004EBA4 File Offset: 0x0004CFA4
		public override void SaveControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0004EBC5 File Offset: 0x0004CFC5
		public override void SavePlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SavePlayerDataNow(playerId);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0004EBE5 File Offset: 0x0004CFE5
		public override void SaveInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0004EC08 File Offset: 0x0004D008
		public override void Load()
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadAll();
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0004EC34 File Offset: 0x0004D034
		public override void LoadControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0004EC64 File Offset: 0x0004D064
		public override void LoadControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0004EC94 File Offset: 0x0004D094
		public override void LoadPlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadPlayerDataNow(playerId);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0004ECC0 File Offset: 0x0004D0C0
		public override void LoadInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0004ECED File Offset: 0x0004D0ED
		protected override void OnInitialize()
		{
			if (this.loadDataOnStart)
			{
				this.Load();
				if (this.loadControllerAssignments && ReInput.controllers.joystickCount > 0)
				{
					this.SaveControllerAssignments();
				}
			}
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0004ED24 File Offset: 0x0004D124
		protected override void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == ControllerType.Joystick)
			{
				int num = this.LoadJoystickData(args.controllerId);
				if (this.loadDataOnStart && this.loadJoystickAssignments && !this.wasJoystickEverDetected)
				{
					base.StartCoroutine(this.LoadJoystickAssignmentsDeferred());
				}
				if (this.loadJoystickAssignments && !this.deferredJoystickAssignmentLoadPending)
				{
					this.SaveControllerAssignments();
				}
				this.wasJoystickEverDetected = true;
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0004EDA8 File Offset: 0x0004D1A8
		protected override void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == ControllerType.Joystick)
			{
				this.SaveJoystickData(args.controllerId);
			}
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0004EDCE File Offset: 0x0004D1CE
		protected override void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x0004EDF0 File Offset: 0x0004D1F0
		public override void SaveControllerMap(int playerId, ControllerMap controllerMap)
		{
			if (controllerMap == null)
			{
				return;
			}
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			this.SaveControllerMap(player, controllerMap);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0004EE20 File Offset: 0x0004D220
		public override ControllerMap LoadControllerMap(int playerId, ControllerIdentifier controllerIdentifier, int categoryId, int layoutId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return null;
			}
			Controller controller = ReInput.controllers.GetController(controllerIdentifier);
			if (controller == null)
			{
				return null;
			}
			return this.LoadControllerMap(player, controller, categoryId, layoutId);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0004EE64 File Offset: 0x0004D264
		private int LoadAll()
		{
			int num = 0;
			if (this.loadControllerAssignments && this.LoadControllerAssignmentsNow())
			{
				num++;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				num += this.LoadPlayerDataNow(allPlayers[i]);
			}
			return num + this.LoadAllJoystickCalibrationData();
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x0004EEC9 File Offset: 0x0004D2C9
		private int LoadPlayerDataNow(int playerId)
		{
			return this.LoadPlayerDataNow(ReInput.players.GetPlayer(playerId));
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0004EEDC File Offset: 0x0004D2DC
		private int LoadPlayerDataNow(Player player)
		{
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			num += this.LoadInputBehaviors(player.id);
			num += this.LoadControllerMaps(player.id, ControllerType.Keyboard, 0);
			num += this.LoadControllerMaps(player.id, ControllerType.Mouse, 0);
			foreach (Joystick joystick in player.controllers.Joysticks)
			{
				num += this.LoadControllerMaps(player.id, ControllerType.Joystick, joystick.id);
			}
			this.RefreshLayoutManager(player.id);
			return num;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0004EF94 File Offset: 0x0004D394
		private int LoadAllJoystickCalibrationData()
		{
			int num = 0;
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				num += this.LoadJoystickCalibrationData(joysticks[i]);
			}
			return num;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0004EFD6 File Offset: 0x0004D3D6
		private int LoadJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return 0;
			}
			return (!joystick.ImportCalibrationMapFromXmlString(this.GetJoystickCalibrationMapXml(joystick))) ? 0 : 1;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0004EFF9 File Offset: 0x0004D3F9
		private int LoadJoystickCalibrationData(int joystickId)
		{
			return this.LoadJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0004F00C File Offset: 0x0004D40C
		private int LoadJoystickData(int joystickId)
		{
			int num = 0;
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(ControllerType.Joystick, joystickId))
				{
					num += this.LoadControllerMaps(player.id, ControllerType.Joystick, joystickId);
					this.RefreshLayoutManager(player.id);
				}
			}
			return num + this.LoadJoystickCalibrationData(joystickId);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0004F084 File Offset: 0x0004D484
		private int LoadControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0;
			num += this.LoadControllerMaps(playerId, controllerType, controllerId);
			this.RefreshLayoutManager(playerId);
			return num + this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0004F0B4 File Offset: 0x0004D4B4
		private int LoadControllerDataNow(ControllerType controllerType, int controllerId)
		{
			int num = 0;
			if (controllerType == ControllerType.Joystick)
			{
				num += this.LoadJoystickCalibrationData(controllerId);
			}
			return num;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0004F0D8 File Offset: 0x0004D4D8
		private int LoadControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0;
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return num;
			}
			Controller controller = ReInput.controllers.GetController(controllerType, controllerId);
			if (controller == null)
			{
				return num;
			}
			IList<InputMapCategory> mapCategories = ReInput.mapping.MapCategories;
			for (int i = 0; i < mapCategories.Count; i++)
			{
				InputMapCategory inputMapCategory = mapCategories[i];
				if (inputMapCategory.userAssignable)
				{
					IList<InputLayout> list = ReInput.mapping.MapLayouts(controller.type);
					for (int j = 0; j < list.Count; j++)
					{
						InputLayout inputLayout = list[j];
						ControllerMap controllerMap = this.LoadControllerMap(player, controller, inputMapCategory.id, inputLayout.id);
						if (controllerMap != null)
						{
							player.controllers.maps.AddMap(controller, controllerMap);
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0004F1C8 File Offset: 0x0004D5C8
		private ControllerMap LoadControllerMap(Player player, Controller controller, int categoryId, int layoutId)
		{
			if (player == null || controller == null)
			{
				return null;
			}
			string controllerMapXml = this.GetControllerMapXml(player, controller, categoryId, layoutId);
			if (string.IsNullOrEmpty(controllerMapXml))
			{
				return null;
			}
			ControllerMap controllerMap = ControllerMap.CreateFromXml(controller.type, controllerMapXml);
			if (controllerMap == null)
			{
				return null;
			}
			List<int> controllerMapKnownActionIds = this.GetControllerMapKnownActionIds(player, controller, categoryId, layoutId);
			this.AddDefaultMappingsForNewActions(controllerMap, controllerMapKnownActionIds);
			return controllerMap;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0004F228 File Offset: 0x0004D628
		private int LoadInputBehaviors(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			IList<InputBehavior> inputBehaviors = ReInput.mapping.GetInputBehaviors(player.id);
			for (int i = 0; i < inputBehaviors.Count; i++)
			{
				num += this.LoadInputBehaviorNow(player, inputBehaviors[i]);
			}
			return num;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0004F288 File Offset: 0x0004D688
		private int LoadInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return 0;
			}
			return this.LoadInputBehaviorNow(player, inputBehavior);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0004F2C8 File Offset: 0x0004D6C8
		private int LoadInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return 0;
			}
			string inputBehaviorXml = this.GetInputBehaviorXml(player, inputBehavior.id);
			if (inputBehaviorXml == null || inputBehaviorXml == string.Empty)
			{
				return 0;
			}
			return (!inputBehavior.ImportXmlString(inputBehaviorXml)) ? 0 : 1;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0004F31C File Offset: 0x0004D71C
		private bool LoadControllerAssignmentsNow()
		{
			try
			{
				UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = this.LoadControllerAssignmentData();
				if (controllerAssignmentSaveInfo == null)
				{
					return false;
				}
				if (this.loadKeyboardAssignments || this.loadMouseAssignments)
				{
					this.LoadKeyboardAndMouseAssignmentsNow(controllerAssignmentSaveInfo);
				}
				if (this.loadJoystickAssignments)
				{
					this.LoadJoystickAssignmentsNow(controllerAssignmentSaveInfo);
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0004F38C File Offset: 0x0004D78C
		private bool LoadKeyboardAndMouseAssignmentsNow(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player.id))
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player.id)];
						if (this.loadKeyboardAssignments)
						{
							player.controllers.hasKeyboard = playerInfo.hasKeyboard;
						}
						if (this.loadMouseAssignments)
						{
							player.controllers.hasMouse = playerInfo.hasMouse;
						}
					}
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0004F47C File Offset: 0x0004D87C
		private bool LoadJoystickAssignmentsNow(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (ReInput.controllers.joystickCount == 0)
				{
					return false;
				}
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					player.controllers.ClearControllersOfType(ControllerType.Joystick);
				}
				List<UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo> list = (!this.loadJoystickAssignments) ? null : new List<UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo>();
				foreach (Player player2 in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player2.id))
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player2.id)];
						for (int i = 0; i < playerInfo.joystickCount; i++)
						{
							UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo2 = playerInfo.joysticks[i];
							if (joystickInfo2 != null)
							{
								Joystick joystick = this.FindJoystickPrecise(joystickInfo2);
								if (joystick != null)
								{
									if (list.Find((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.joystick == joystick) == null)
									{
										list.Add(new UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo(joystick, joystickInfo2.id));
									}
									player2.controllers.AddController(joystick, false);
								}
							}
						}
					}
				}
				if (this.allowImpreciseJoystickAssignmentMatching)
				{
					foreach (Player player3 in ReInput.players.AllPlayers)
					{
						if (data.ContainsPlayer(player3.id))
						{
							UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo2 = data.players[data.IndexOfPlayer(player3.id)];
							for (int j = 0; j < playerInfo2.joystickCount; j++)
							{
								UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo = playerInfo2.joysticks[j];
								if (joystickInfo != null)
								{
									Joystick joystick2 = null;
									int num = list.FindIndex((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.oldJoystickId == joystickInfo.id);
									if (num >= 0)
									{
										joystick2 = list[num].joystick;
									}
									else
									{
										List<Joystick> list2;
										if (!this.TryFindJoysticksImprecise(joystickInfo, out list2))
										{
											goto IL_30F;
										}
										using (List<Joystick>.Enumerator enumerator4 = list2.GetEnumerator())
										{
											while (enumerator4.MoveNext())
											{
												Joystick match = enumerator4.Current;
												if (list.Find((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.joystick == match) == null)
												{
													joystick2 = match;
													break;
												}
											}
										}
										if (joystick2 == null)
										{
											goto IL_30F;
										}
										list.Add(new UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo(joystick2, joystickInfo.id));
									}
									player3.controllers.AddController(joystick2, false);
								}
								IL_30F:;
							}
						}
					}
				}
			}
			catch
			{
			}
			if (ReInput.configuration.autoAssignJoysticks)
			{
				ReInput.controllers.AutoAssignJoysticks();
			}
			return true;
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0004F870 File Offset: 0x0004DC70
		private UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo LoadControllerAssignmentData()
		{
			UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo result;
			try
			{
				if (!PlayerPrefs.HasKey(this.playerPrefsKey_controllerAssignments))
				{
					result = null;
				}
				else
				{
					string @string = PlayerPrefs.GetString(this.playerPrefsKey_controllerAssignments);
					if (string.IsNullOrEmpty(@string))
					{
						result = null;
					}
					else
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = JsonParser.FromJson<UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo>(@string);
						if (controllerAssignmentSaveInfo == null || controllerAssignmentSaveInfo.playerCount == 0)
						{
							result = null;
						}
						else
						{
							result = controllerAssignmentSaveInfo;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0004F8F4 File Offset: 0x0004DCF4
		private IEnumerator LoadJoystickAssignmentsDeferred()
		{
			this.deferredJoystickAssignmentLoadPending = true;
			yield return new WaitForEndOfFrame();
			if (!ReInput.isReady)
			{
				yield break;
			}
			if (this.LoadJoystickAssignmentsNow(null))
			{
			}
			this.SaveControllerAssignments();
			this.deferredJoystickAssignmentLoadPending = false;
			yield break;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0004F910 File Offset: 0x0004DD10
		private void SaveAll()
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				this.SavePlayerDataNow(allPlayers[i]);
			}
			this.SaveAllJoystickCalibrationData();
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
			PlayerPrefs.Save();
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0004F969 File Offset: 0x0004DD69
		private void SavePlayerDataNow(int playerId)
		{
			this.SavePlayerDataNow(ReInput.players.GetPlayer(playerId));
			PlayerPrefs.Save();
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0004F984 File Offset: 0x0004DD84
		private void SavePlayerDataNow(Player player)
		{
			if (player == null)
			{
				return;
			}
			PlayerSaveData saveData = player.GetSaveData(true);
			this.SaveInputBehaviors(player, saveData);
			this.SaveControllerMaps(player, saveData);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0004F9B0 File Offset: 0x0004DDB0
		private void SaveAllJoystickCalibrationData()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				this.SaveJoystickCalibrationData(joysticks[i]);
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0004F9EC File Offset: 0x0004DDEC
		private void SaveJoystickCalibrationData(int joystickId)
		{
			this.SaveJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0004FA00 File Offset: 0x0004DE00
		private void SaveJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return;
			}
			JoystickCalibrationMapSaveData calibrationMapSaveData = joystick.GetCalibrationMapSaveData();
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			PlayerPrefs.SetString(joystickCalibrationMapPlayerPrefsKey, calibrationMapSaveData.map.ToXmlString());
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0004FA34 File Offset: 0x0004DE34
		private void SaveJoystickData(int joystickId)
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(ControllerType.Joystick, joystickId))
				{
					this.SaveControllerMaps(player.id, ControllerType.Joystick, joystickId);
				}
			}
			this.SaveJoystickCalibrationData(joystickId);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0004FA97 File Offset: 0x0004DE97
		private void SaveControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			this.SaveControllerMaps(playerId, controllerType, controllerId);
			this.SaveControllerDataNow(controllerType, controllerId);
			PlayerPrefs.Save();
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0004FAAF File Offset: 0x0004DEAF
		private void SaveControllerDataNow(ControllerType controllerType, int controllerId)
		{
			if (controllerType == ControllerType.Joystick)
			{
				this.SaveJoystickCalibrationData(controllerId);
			}
			PlayerPrefs.Save();
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0004FAC4 File Offset: 0x0004DEC4
		private void SaveControllerMaps(Player player, PlayerSaveData playerSaveData)
		{
			foreach (ControllerMapSaveData controllerMapSaveData in playerSaveData.AllControllerMapSaveData)
			{
				this.SaveControllerMap(player, controllerMapSaveData.map);
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0004FB24 File Offset: 0x0004DF24
		private void SaveControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			if (!player.controllers.ContainsController(controllerType, controllerId))
			{
				return;
			}
			ControllerMapSaveData[] mapSaveData = player.controllers.maps.GetMapSaveData(controllerType, controllerId, true);
			if (mapSaveData == null)
			{
				return;
			}
			for (int i = 0; i < mapSaveData.Length; i++)
			{
				this.SaveControllerMap(player, mapSaveData[i].map);
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0004FB98 File Offset: 0x0004DF98
		private void SaveControllerMap(Player player, ControllerMap controllerMap)
		{
			string key = this.GetControllerMapPlayerPrefsKey(player, controllerMap.controller, controllerMap.categoryId, controllerMap.layoutId, true);
			PlayerPrefs.SetString(key, controllerMap.ToXmlString());
			key = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controllerMap.controller, controllerMap.categoryId, controllerMap.layoutId, true);
			PlayerPrefs.SetString(key, this.allActionIdsString);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0004FBF4 File Offset: 0x0004DFF4
		private void SaveInputBehaviors(Player player, PlayerSaveData playerSaveData)
		{
			if (player == null)
			{
				return;
			}
			InputBehavior[] inputBehaviors = playerSaveData.inputBehaviors;
			for (int i = 0; i < inputBehaviors.Length; i++)
			{
				this.SaveInputBehaviorNow(player, inputBehaviors[i]);
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0004FC30 File Offset: 0x0004E030
		private void SaveInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return;
			}
			this.SaveInputBehaviorNow(player, inputBehavior);
			PlayerPrefs.Save();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0004FC74 File Offset: 0x0004E074
		private void SaveInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return;
			}
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, inputBehavior.id);
			PlayerPrefs.SetString(inputBehaviorPlayerPrefsKey, inputBehavior.ToXmlString());
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0004FCA8 File Offset: 0x0004E0A8
		private bool SaveControllerAssignments()
		{
			try
			{
				UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo(ReInput.players.allPlayerCount);
				for (int i = 0; i < ReInput.players.allPlayerCount; i++)
				{
					Player player = ReInput.players.AllPlayers[i];
					UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo();
					controllerAssignmentSaveInfo.players[i] = playerInfo;
					playerInfo.id = player.id;
					playerInfo.hasKeyboard = player.controllers.hasKeyboard;
					playerInfo.hasMouse = player.controllers.hasMouse;
					UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[] array = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[player.controllers.joystickCount];
					playerInfo.joysticks = array;
					for (int j = 0; j < player.controllers.joystickCount; j++)
					{
						Joystick joystick = player.controllers.Joysticks[j];
						array[j] = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo
						{
							instanceGuid = joystick.deviceInstanceGuid,
							id = joystick.id,
							hardwareIdentifier = joystick.hardwareIdentifier
						};
					}
				}
				PlayerPrefs.SetString(this.playerPrefsKey_controllerAssignments, JsonWriter.ToJson(controllerAssignmentSaveInfo));
				PlayerPrefs.Save();
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0004FDF4 File Offset: 0x0004E1F4
		private bool ControllerAssignmentSaveDataExists()
		{
			if (!PlayerPrefs.HasKey(this.playerPrefsKey_controllerAssignments))
			{
				return false;
			}
			string @string = PlayerPrefs.GetString(this.playerPrefsKey_controllerAssignments);
			return !string.IsNullOrEmpty(@string);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0004FE30 File Offset: 0x0004E230
		private string GetBasePlayerPrefsKey(Player player)
		{
			string str = this.playerPrefsKeyPrefix;
			return str + "|playerName=" + player.name;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0004FE58 File Offset: 0x0004E258
		private string GetControllerMapPlayerPrefsKey(Player player, Controller controller, int categoryId, int layoutId, bool includeDuplicateIndex)
		{
			string text = this.GetBasePlayerPrefsKey(player);
			text += "|dataType=ControllerMap";
			text = text + "|controllerMapType=" + controller.mapTypeString;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"|categoryId=",
				categoryId,
				"|layoutId=",
				layoutId
			});
			text = text + "|hardwareIdentifier=" + controller.hardwareIdentifier;
			if (controller.type == ControllerType.Joystick)
			{
				text = text + "|hardwareGuid=" + ((Joystick)controller).hardwareTypeGuid.ToString();
				if (includeDuplicateIndex)
				{
					text = text + "|duplicate=" + UserDataStore_PlayerPrefs.GetDuplicateIndex(player, controller).ToString();
				}
			}
			return text;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0004FF2C File Offset: 0x0004E32C
		private string GetControllerMapKnownActionIdsPlayerPrefsKey(Player player, Controller controller, int categoryId, int layoutId, bool includeDuplicateIndex)
		{
			string text = this.GetBasePlayerPrefsKey(player);
			text += "|dataType=ControllerMap_KnownActionIds";
			text = text + "|controllerMapType=" + controller.mapTypeString;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"|categoryId=",
				categoryId,
				"|layoutId=",
				layoutId
			});
			text = text + "|hardwareIdentifier=" + controller.hardwareIdentifier;
			if (controller.type == ControllerType.Joystick)
			{
				text = text + "|hardwareGuid=" + ((Joystick)controller).hardwareTypeGuid.ToString();
				if (includeDuplicateIndex)
				{
					text = text + "|duplicate=" + UserDataStore_PlayerPrefs.GetDuplicateIndex(player, controller).ToString();
				}
			}
			return text;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00050000 File Offset: 0x0004E400
		private string GetJoystickCalibrationMapPlayerPrefsKey(Joystick joystick)
		{
			string str = this.playerPrefsKeyPrefix;
			str += "|dataType=CalibrationMap";
			str = str + "|controllerType=" + joystick.type.ToString();
			str = str + "|hardwareIdentifier=" + joystick.hardwareIdentifier;
			return str + "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString();
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00050074 File Offset: 0x0004E474
		private string GetInputBehaviorPlayerPrefsKey(Player player, int inputBehaviorId)
		{
			string text = this.GetBasePlayerPrefsKey(player);
			text += "|dataType=InputBehavior";
			return text + "|id=" + inputBehaviorId;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000500A8 File Offset: 0x0004E4A8
		private string GetControllerMapXml(Player player, Controller controller, int categoryId, int layoutId)
		{
			string controllerMapPlayerPrefsKey = this.GetControllerMapPlayerPrefsKey(player, controller, categoryId, layoutId, true);
			if (!PlayerPrefs.HasKey(controllerMapPlayerPrefsKey))
			{
				if (controller.type != ControllerType.Joystick)
				{
					return string.Empty;
				}
				controllerMapPlayerPrefsKey = this.GetControllerMapPlayerPrefsKey(player, controller, categoryId, layoutId, false);
				if (!PlayerPrefs.HasKey(controllerMapPlayerPrefsKey))
				{
					return string.Empty;
				}
			}
			return PlayerPrefs.GetString(controllerMapPlayerPrefsKey);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x00050104 File Offset: 0x0004E504
		private List<int> GetControllerMapKnownActionIds(Player player, Controller controller, int categoryId, int layoutId)
		{
			List<int> list = new List<int>();
			string controllerMapKnownActionIdsPlayerPrefsKey = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controller, categoryId, layoutId, true);
			if (!PlayerPrefs.HasKey(controllerMapKnownActionIdsPlayerPrefsKey))
			{
				if (controller.type != ControllerType.Joystick)
				{
					return list;
				}
				controllerMapKnownActionIdsPlayerPrefsKey = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controller, categoryId, layoutId, false);
				if (!PlayerPrefs.HasKey(controllerMapKnownActionIdsPlayerPrefsKey))
				{
					return list;
				}
			}
			string @string = PlayerPrefs.GetString(controllerMapKnownActionIdsPlayerPrefsKey);
			if (string.IsNullOrEmpty(@string))
			{
				return list;
			}
			string[] array = @string.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					int item;
					if (int.TryParse(array[i], out item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000501C4 File Offset: 0x0004E5C4
		private string GetJoystickCalibrationMapXml(Joystick joystick)
		{
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			if (!PlayerPrefs.HasKey(joystickCalibrationMapPlayerPrefsKey))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString(joystickCalibrationMapPlayerPrefsKey);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000501F0 File Offset: 0x0004E5F0
		private string GetInputBehaviorXml(Player player, int id)
		{
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, id);
			if (!PlayerPrefs.HasKey(inputBehaviorPlayerPrefsKey))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString(inputBehaviorPlayerPrefsKey);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00050220 File Offset: 0x0004E620
		private void AddDefaultMappingsForNewActions(ControllerMap controllerMap, List<int> knownActionIds)
		{
			if (controllerMap == null || knownActionIds == null)
			{
				return;
			}
			if (knownActionIds == null || knownActionIds.Count == 0)
			{
				return;
			}
			ControllerMap controllerMapInstance = ReInput.mapping.GetControllerMapInstance(controllerMap.controller, controllerMap.categoryId, controllerMap.layoutId);
			if (controllerMapInstance == null)
			{
				return;
			}
			List<int> list = new List<int>();
			foreach (int item in this.allActionIds)
			{
				if (!knownActionIds.Contains(item))
				{
					list.Add(item);
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			foreach (ActionElementMap actionElementMap in controllerMapInstance.AllMaps)
			{
				if (list.Contains(actionElementMap.actionId))
				{
					if (!controllerMap.DoesElementAssignmentConflict(actionElementMap))
					{
						ElementAssignment elementAssignment = new ElementAssignment(controllerMap.controllerType, actionElementMap.elementType, actionElementMap.elementIdentifierId, actionElementMap.axisRange, actionElementMap.keyCode, actionElementMap.modifierKeyFlags, actionElementMap.actionId, actionElementMap.axisContribution, actionElementMap.invert);
						controllerMap.CreateElementMap(elementAssignment);
					}
				}
			}
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000503A0 File Offset: 0x0004E7A0
		private Joystick FindJoystickPrecise(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo)
		{
			if (joystickInfo == null)
			{
				return null;
			}
			if (joystickInfo.instanceGuid == Guid.Empty)
			{
				return null;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (joysticks[i].deviceInstanceGuid == joystickInfo.instanceGuid)
				{
					return joysticks[i];
				}
			}
			return null;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00050414 File Offset: 0x0004E814
		private bool TryFindJoysticksImprecise(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo, out List<Joystick> matches)
		{
			matches = null;
			if (joystickInfo == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(joystickInfo.hardwareIdentifier))
			{
				return false;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (string.Equals(joysticks[i].hardwareIdentifier, joystickInfo.hardwareIdentifier, StringComparison.OrdinalIgnoreCase))
				{
					if (matches == null)
					{
						matches = new List<Joystick>();
					}
					matches.Add(joysticks[i]);
				}
			}
			return matches != null;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000504A4 File Offset: 0x0004E8A4
		private static int GetDuplicateIndex(Player player, Controller controller)
		{
			int num = 0;
			foreach (Controller controller2 in player.controllers.Controllers)
			{
				if (controller2.type == controller.type)
				{
					bool flag = false;
					if (controller.type == ControllerType.Joystick)
					{
						if ((controller2 as Joystick).hardwareTypeGuid != (controller as Joystick).hardwareTypeGuid)
						{
							continue;
						}
						if ((controller as Joystick).hardwareTypeGuid != Guid.Empty)
						{
							flag = true;
						}
					}
					if (flag || !(controller2.hardwareIdentifier != controller.hardwareIdentifier))
					{
						if (controller2 == controller)
						{
							return num;
						}
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0005059C File Offset: 0x0004E99C
		private void RefreshLayoutManager(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			player.controllers.maps.layoutManager.Apply();
		}

		// Token: 0x040012C2 RID: 4802
		private const string thisScriptName = "UserDataStore_PlayerPrefs";

		// Token: 0x040012C3 RID: 4803
		private const string logPrefix = "Rewired: ";

		// Token: 0x040012C4 RID: 4804
		private const string editorLoadedMessage = "\n***IMPORTANT:*** Changes made to the Rewired Input Manager configuration after the last time XML data was saved WILL NOT be used because the loaded old saved data has overwritten these values. If you change something in the Rewired Input Manager such as a Joystick Map or Input Behavior settings, you will not see these changes reflected in the current configuration. Clear PlayerPrefs using the inspector option on the UserDataStore_PlayerPrefs component.";

		// Token: 0x040012C5 RID: 4805
		private const string playerPrefsKeySuffix_controllerAssignments = "ControllerAssignments";

		// Token: 0x040012C6 RID: 4806
		[Tooltip("Should this script be used? If disabled, nothing will be saved or loaded.")]
		[SerializeField]
		private bool isEnabled = true;

		// Token: 0x040012C7 RID: 4807
		[Tooltip("Should saved data be loaded on start?")]
		[SerializeField]
		private bool loadDataOnStart = true;

		// Token: 0x040012C8 RID: 4808
		[Tooltip("Should Player Joystick assignments be saved and loaded? This is not totally reliable for all Joysticks on all platforms. Some platforms/input sources do not provide enough information to reliably save assignments from session to session and reboot to reboot.")]
		[SerializeField]
		private bool loadJoystickAssignments = true;

		// Token: 0x040012C9 RID: 4809
		[Tooltip("Should Player Keyboard assignments be saved and loaded?")]
		[SerializeField]
		private bool loadKeyboardAssignments = true;

		// Token: 0x040012CA RID: 4810
		[Tooltip("Should Player Mouse assignments be saved and loaded?")]
		[SerializeField]
		private bool loadMouseAssignments = true;

		// Token: 0x040012CB RID: 4811
		[Tooltip("The PlayerPrefs key prefix. Change this to change how keys are stored in PlayerPrefs. Changing this will make saved data already stored with the old key no longer accessible.")]
		[SerializeField]
		private string playerPrefsKeyPrefix = "RewiredSaveData";

		// Token: 0x040012CC RID: 4812
		[NonSerialized]
		private bool allowImpreciseJoystickAssignmentMatching = true;

		// Token: 0x040012CD RID: 4813
		[NonSerialized]
		private bool deferredJoystickAssignmentLoadPending;

		// Token: 0x040012CE RID: 4814
		[NonSerialized]
		private bool wasJoystickEverDetected;

		// Token: 0x040012CF RID: 4815
		[NonSerialized]
		private List<int> __allActionIds;

		// Token: 0x040012D0 RID: 4816
		[NonSerialized]
		private string __allActionIdsString;

		// Token: 0x020004A9 RID: 1193
		private class ControllerAssignmentSaveInfo
		{
			// Token: 0x06001DE0 RID: 7648 RVA: 0x000505D1 File Offset: 0x0004E9D1
			public ControllerAssignmentSaveInfo()
			{
			}

			// Token: 0x06001DE1 RID: 7649 RVA: 0x000505DC File Offset: 0x0004E9DC
			public ControllerAssignmentSaveInfo(int playerCount)
			{
				this.players = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[playerCount];
				for (int i = 0; i < playerCount; i++)
				{
					this.players[i] = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo();
				}
			}

			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0005061A File Offset: 0x0004EA1A
			public int playerCount
			{
				get
				{
					return (this.players == null) ? 0 : this.players.Length;
				}
			}

			// Token: 0x06001DE3 RID: 7651 RVA: 0x00050638 File Offset: 0x0004EA38
			public int IndexOfPlayer(int playerId)
			{
				for (int i = 0; i < this.playerCount; i++)
				{
					if (this.players[i] != null)
					{
						if (this.players[i].id == playerId)
						{
							return i;
						}
					}
				}
				return -1;
			}

			// Token: 0x06001DE4 RID: 7652 RVA: 0x00050684 File Offset: 0x0004EA84
			public bool ContainsPlayer(int playerId)
			{
				return this.IndexOfPlayer(playerId) >= 0;
			}

			// Token: 0x040012D1 RID: 4817
			public UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[] players;

			// Token: 0x020004AA RID: 1194
			public class PlayerInfo
			{
				// Token: 0x170003F3 RID: 1011
				// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0005069B File Offset: 0x0004EA9B
				public int joystickCount
				{
					get
					{
						return (this.joysticks == null) ? 0 : this.joysticks.Length;
					}
				}

				// Token: 0x06001DE7 RID: 7655 RVA: 0x000506B8 File Offset: 0x0004EAB8
				public int IndexOfJoystick(int joystickId)
				{
					for (int i = 0; i < this.joystickCount; i++)
					{
						if (this.joysticks[i] != null)
						{
							if (this.joysticks[i].id == joystickId)
							{
								return i;
							}
						}
					}
					return -1;
				}

				// Token: 0x06001DE8 RID: 7656 RVA: 0x00050704 File Offset: 0x0004EB04
				public bool ContainsJoystick(int joystickId)
				{
					return this.IndexOfJoystick(joystickId) >= 0;
				}

				// Token: 0x040012D2 RID: 4818
				public int id;

				// Token: 0x040012D3 RID: 4819
				public bool hasKeyboard;

				// Token: 0x040012D4 RID: 4820
				public bool hasMouse;

				// Token: 0x040012D5 RID: 4821
				public UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[] joysticks;
			}

			// Token: 0x020004AB RID: 1195
			public class JoystickInfo
			{
				// Token: 0x040012D6 RID: 4822
				public Guid instanceGuid;

				// Token: 0x040012D7 RID: 4823
				public string hardwareIdentifier;

				// Token: 0x040012D8 RID: 4824
				public int id;
			}
		}

		// Token: 0x020004AC RID: 1196
		private class JoystickAssignmentHistoryInfo
		{
			// Token: 0x06001DEA RID: 7658 RVA: 0x0005071B File Offset: 0x0004EB1B
			public JoystickAssignmentHistoryInfo(Joystick joystick, int oldJoystickId)
			{
				if (joystick == null)
				{
					throw new ArgumentNullException("joystick");
				}
				this.joystick = joystick;
				this.oldJoystickId = oldJoystickId;
			}

			// Token: 0x040012D9 RID: 4825
			public readonly Joystick joystick;

			// Token: 0x040012DA RID: 4826
			public readonly int oldJoystickId;
		}
	}
}
