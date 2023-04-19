using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS.Platform.Utils.Data
{
	// Token: 0x02000091 RID: 145
	public class PlatformStorageDatabase : ScriptableObject, ISerializationCallbackReceiver
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00019243 File Offset: 0x00017643
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00019245 File Offset: 0x00017645
		public void OnAfterDeserialize()
		{
			this.allFiles = new List<string>();
			this.allFiles.AddRange(this.saveLocalFiles);
			this.allFiles.AddRange(this.saveCloudFiles);
		}

		// Token: 0x0400028F RID: 655
		// [SerializeField]
		// public PlatformStorageDatabase.PS4StorageInfo PS4 = new PlatformStorageDatabase.PS4StorageInfo();
		//
		// // Token: 0x04000290 RID: 656
		// [SerializeField]
		// public PlatformStorageDatabase.OculusStorageInfo Oculus = new PlatformStorageDatabase.OculusStorageInfo();
		//
		// // Token: 0x04000291 RID: 657
		// [SerializeField]
		// public PlatformStorageDatabase.XboxStorageInfo Xbox = new PlatformStorageDatabase.XboxStorageInfo();
		//
		// // Token: 0x04000292 RID: 658
		// [SerializeField]
		// public PlatformStorageDatabase.NintendoStorageInfo Nintendo = new PlatformStorageDatabase.NintendoStorageInfo();
		//
		// // Token: 0x04000293 RID: 659
		// [SerializeField]
		// public PlatformStorageDatabase.NoneStorageInfo None = new PlatformStorageDatabase.NoneStorageInfo();

		// Token: 0x04000294 RID: 660
		[SerializeField]
		public List<string> saveLocalFiles = new List<string>();

		// Token: 0x04000295 RID: 661
		[SerializeField]
		public List<string> saveCloudFiles = new List<string>();

		// Token: 0x04000296 RID: 662
		[NonSerialized]
		public List<string> allFiles = new List<string>();

		// Token: 0x02000092 RID: 146
		[Serializable]
		public class PS4SlotInfo
		{
			// Token: 0x060005E4 RID: 1508 RVA: 0x000192B4 File Offset: 0x000176B4
			// public virtual bool SetupSlot(string file, ref SaveLoad.SavedGameSlotParams slot)
			// {
			// 	if (string.IsNullOrEmpty(this.directory) || string.IsNullOrEmpty(this.title) || string.IsNullOrEmpty(this.iconPath))
			// 	{
			// 		Debug.ThreadLogError("[SSB] Slot Info Invalide! | Directory: {0} | File: {1} | Title: {2} | Icon: {3}", new object[]
			// 		{
			// 			this.directory,
			// 			file,
			// 			this.title,
			// 			this.iconPath
			// 		});
			// 		return false;
			// 	}
			// 	slot.dirName = this.directory;
			// 	slot.fileName = file.Replace("/", "\\");
			// 	slot.title = this.title;
			// 	slot.newTitle = this.title;
			// 	slot.subTitle = this.subTitle;
			// 	slot.detail = this.details;
			// 	slot.iconPath = this.iconPath;
			// 	Debug.ThreadLogInfo("[SSB] Slot Set | Dir: {0} | File: {1} | Title: {2} | Sub: {3} | Detail: {4} | Icon: {5}", new object[]
			// 	{
			// 		slot.dirName,
			// 		slot.fileName,
			// 		slot.title,
			// 		slot.subTitle,
			// 		slot.detail,
			// 		slot.iconPath
			// 	});
			// 	return true;
			// }

			// Token: 0x04000297 RID: 663
			public string directory = "SaveData";

			// Token: 0x04000298 RID: 664
			public string title = "Game Save Data";

			// Token: 0x04000299 RID: 665
			public string subTitle = "Game Save";

			// Token: 0x0400029A RID: 666
			public string details = "Game save data for game";

			// Token: 0x0400029B RID: 667
			public string iconPath = "/SaveIcon.png";
		}

		// Token: 0x02000093 RID: 147
		// [Serializable]
		// public class PS4FileRule : PlatformStorageDatabase.PS4SlotInfo
		// {
		// 	// Token: 0x060005E6 RID: 1510 RVA: 0x000193E7 File Offset: 0x000177E7
		// 	public override bool SetupSlot(string file, ref SaveLoad.SavedGameSlotParams slot)
		// 	{
		// 		return this.Files.Contains(file) && base.SetupSlot(file, ref slot);
		// 	}
		//
		// 	// Token: 0x0400029C RID: 668
		// 	public List<string> Files = new List<string>();
		// }

		// Token: 0x02000094 RID: 148
		//[Serializable]
		// public class PS4SelfGen : PlatformStorageDatabase.PS4SlotInfo
		// {
		// 	// Token: 0x060005E8 RID: 1512 RVA: 0x0001940C File Offset: 0x0001780C
		// 	public override bool SetupSlot(string file, ref SaveLoad.SavedGameSlotParams slot)
		// 	{
		// 		if (file.ToUpper().Contains(this.FileContains.ToUpper()))
		// 		{
		// 			slot.fileName = file.Replace("/", "\\");
		// 			int num = slot.fileName.LastIndexOf("\\") + 1;
		// 			int num2 = slot.fileName.IndexOf(".", num);
		// 			slot.dirName = slot.fileName.Substring(num, (num2 >= 0) ? (num2 - num) : (slot.fileName.Length - num)).Replace(" ", string.Empty).Replace("-", string.Empty);
		// 			slot.title = slot.fileName.Substring(num, (num2 >= 0) ? (num2 - num) : (slot.fileName.Length - num));
		// 			slot.newTitle = slot.title;
		// 			slot.subTitle = this.subTitle;
		// 			slot.detail = this.details;
		// 			slot.iconPath = this.iconPath;
		// 			Debug.ThreadLogInfo("[SSB] Slot Set | Dir: {0} | File: {1} | Title: {2} | Sub: {3} | Detail: {4} | Icon: {5}", new object[]
		// 			{
		// 				slot.dirName,
		// 				slot.fileName,
		// 				slot.title,
		// 				slot.subTitle,
		// 				slot.detail,
		// 				slot.iconPath
		// 			});
		// 			return true;
		// 		}
		// 		return false;
		// 	}
		//
		// 	// Token: 0x0400029D RID: 669
		// 	public string FileContains;
		// }

		// Token: 0x02000095 RID: 149
		//[Serializable]
		// public class PS4StorageInfo
		// {
		// 	// Token: 0x060005EA RID: 1514 RVA: 0x000195A8 File Offset: 0x000179A8
		// 	public bool SetupSlot(string file, ref SaveLoad.SavedGameSlotParams slot)
		// 	{
		// 		if (!string.IsNullOrEmpty(file))
		// 		{
		// 			for (int i = 0; i < this.Slots.Count; i++)
		// 			{
		// 				if (this.Slots[i].SetupSlot(file, ref slot))
		// 				{
		// 					return true;
		// 				}
		// 			}
		// 			for (int j = 0; j < this.SelfGen.Count; j++)
		// 			{
		// 				if (this.SelfGen[j].SetupSlot(file, ref slot))
		// 				{
		// 					return true;
		// 				}
		// 			}
		// 		}
		// 		return this.Default.SetupSlot(file, ref slot);
		// 	}

			// Token: 0x0400029E RID: 670
			public int ExpectedSlots = 5;

			// Token: 0x0400029F RID: 671
			public PlatformStorageDatabase.PS4SlotInfo Default = new PlatformStorageDatabase.PS4SlotInfo();

			// Token: 0x040002A0 RID: 672
			// public List<PlatformStorageDatabase.PS4FileRule> Slots = new List<PlatformStorageDatabase.PS4FileRule>();
			//
			// // Token: 0x040002A1 RID: 673
			// public List<PlatformStorageDatabase.PS4SelfGen> SelfGen = new List<PlatformStorageDatabase.PS4SelfGen>();
		}

		// Token: 0x02000096 RID: 150
		[Serializable]
		public class OculusStorageInfo
		{
			// Token: 0x040002A2 RID: 674
			public string bucketName = "SaveData";
		}

		// Token: 0x02000097 RID: 151
		[Serializable]
		public class XboxStorageInfo
		{
			// Token: 0x040002A3 RID: 675
			public string containerName = "SaveData";
		}

		// Token: 0x02000098 RID: 152
		[Serializable]
		public class NintendoStorageInfo
		{
			// Token: 0x040002A4 RID: 676
			public string MountName = "Saves";
		}

		// Token: 0x02000099 RID: 153
		[Serializable]
		public class NoneStorageInfo
		{
			// Token: 0x040002A5 RID: 677
			public string directory = "Saves/";
		}
	}
//}
