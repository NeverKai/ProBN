using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;

namespace RTM.Input
{
	// Token: 0x020004C0 RID: 1216
	public static class RewiredStorageUtils
	{
		// Token: 0x06001EBA RID: 7866 RVA: 0x000529C0 File Offset: 0x00050DC0
		private static void MaybeInitialize()
		{
			if (RewiredStorageUtils.knownActionIds == null)
			{
				RewiredStorageUtils.knownActionIds = (from a in ReInput.mapping.Actions
				select a.id).ToList<int>();
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00052A10 File Offset: 0x00050E10
		public static void ApplyPlayerData(int playerId, RewiredStorageUtils.PlayerData playerData)
		{
			RewiredStorageUtils.MaybeInitialize();
			if (playerData != null)
			{
				Debug.Log("Applying player custom control maps");
				Player player = ReInput.players.GetPlayer(playerId);
				foreach (RewiredStorageUtils.MapData mapData in playerData.mapData)
				{
					if (mapData.ControllerType == ControllerType.Joystick)
					{
						Debug.Log("Skipping Joystick data");
					}
					if (mapData.ControllerType == ControllerType.Keyboard || mapData.ControllerType == ControllerType.Mouse)
					{
						player.controllers.maps.AddMapFromXml(mapData.ControllerType, 0, mapData.Xml);
					}
				}
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00052AD4 File Offset: 0x00050ED4
		public static RewiredStorageUtils.PlayerData ExportPlayerData(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			PlayerSaveData saveData = player.GetSaveData(true);
			RewiredStorageUtils.PlayerData playerData = new RewiredStorageUtils.PlayerData();
			foreach (ControllerMapSaveData controllerMapSaveData in saveData.AllControllerMapSaveData)
			{
				string hardwareGuid = (controllerMapSaveData.controllerType != ControllerType.Joystick) ? string.Empty : ((Joystick)controllerMapSaveData.controller).hardwareTypeGuid.ToString();
				RewiredStorageUtils.MapData item = new RewiredStorageUtils.MapData(controllerMapSaveData.controllerType, hardwareGuid, controllerMapSaveData.map.ToXmlString(), RewiredStorageUtils.knownActionIds);
				playerData.mapData.Add(item);
			}
			return playerData;
		}

		// Token: 0x0400131F RID: 4895
		[SerializeField]
		private static string directory = "ControllerMaps";

		// Token: 0x04001320 RID: 4896
		[SerializeField]
		private static string fileFormat = "controller_map_{0:D3}";

		// Token: 0x04001321 RID: 4897
		private static List<int> knownActionIds;

		// Token: 0x020004C1 RID: 1217
		[Serializable]
		public class MapData
		{
			// Token: 0x06001EBF RID: 7871 RVA: 0x00052BCA File Offset: 0x00050FCA
			public MapData(ControllerType controllerType, string hardwareGuid, string xml, List<int> knownActionIds)
			{
				this.ControllerType = controllerType;
				this.HardwareGuid = hardwareGuid;
				this.Xml = xml;
				this.KnownActionIds = knownActionIds;
			}

			// Token: 0x06001EC0 RID: 7872 RVA: 0x00052BEF File Offset: 0x00050FEF
			private MapData()
			{
			}

			// Token: 0x04001323 RID: 4899
			public ControllerType ControllerType;

			// Token: 0x04001324 RID: 4900
			public string Xml;

			// Token: 0x04001325 RID: 4901
			public List<int> KnownActionIds;

			// Token: 0x04001326 RID: 4902
			public string HardwareGuid;
		}

		// Token: 0x020004C2 RID: 1218
		[Serializable]
		public class PlayerData
		{
			// Token: 0x04001327 RID: 4903
			public List<RewiredStorageUtils.MapData> mapData = new List<RewiredStorageUtils.MapData>();
		}
	}
}
