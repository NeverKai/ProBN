using System;
using System.Collections.Generic;
using System.IO;
using CS.Platform.Utils.Data;
using Sony.PS4.SavedGame;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x02000079 RID: 121
	public static class Storage
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00015C74 File Offset: 0x00014074
		public static void StorageSetup(string databaseLocation = null)
		{
			if (string.IsNullOrEmpty(databaseLocation))
			{
				databaseLocation = "Platform/StorageData";
			}
			Storage._storageInfomation = (Resources.Load(databaseLocation) as PlatformStorageDatabase);
			Storage._directory = Path.Combine(Application.persistentDataPath, "BadNorth");
			int num = Storage._directory.LastIndexOf("\\");
			if (num < 0)
			{
				num = Storage._directory.LastIndexOf("/");
			}
			if (num > 0)
			{
				Storage._directory = Storage._directory.Substring(0, num + 1);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00015CF8 File Offset: 0x000140F8
		public static bool StoragePS4SetupSlot(string file, ref SaveLoad.SavedGameSlotParams slot)
		{
			return Storage._storageInfomation != null && Storage._storageInfomation.PS4.SetupSlot(file, ref slot);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00015D1D File Offset: 0x0001411D
		public static int StoragePS4ExpectedSlots()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._storageInfomation.PS4.ExpectedSlots;
			}
			return 1;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00015D40 File Offset: 0x00014140
		public static string StorageNoneDirectory()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._directory + Storage._storageInfomation.None.directory;
			}
			return Storage._directory;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00015D71 File Offset: 0x00014171
		public static int StorageFilesTotal
		{
			get
			{
				if (Storage._storageInfomation != null)
				{
					return Storage._storageInfomation.allFiles.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00015D94 File Offset: 0x00014194
		public static List<string> StorageFiles()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._storageInfomation.allFiles;
			}
			return null;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00015DB2 File Offset: 0x000141B2
		public static List<string> StorageFilesLocal()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._storageInfomation.saveLocalFiles;
			}
			return null;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00015DD0 File Offset: 0x000141D0
		public static string StorageFiles(int i)
		{
			if (Storage._storageInfomation != null && 0 <= i && i < Storage._storageInfomation.allFiles.Count)
			{
				return Storage._storageInfomation.allFiles[i];
			}
			return null;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00015E10 File Offset: 0x00014210
		public static bool StorageFilesNameExpected(string file)
		{
			return Storage._storageInfomation != null && (Storage._storageInfomation.saveLocalFiles.Contains(file) || Storage._storageInfomation.saveCloudFiles.Contains(file));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00015E4C File Offset: 0x0001424C
		public static bool StorageFilesNameExpectedCloud(string file)
		{
			return Storage._storageInfomation != null && Storage._storageInfomation.saveCloudFiles.Contains(file);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00015E70 File Offset: 0x00014270
		public static bool StorageFilesNameExpectedLocal(string file)
		{
			return Storage._storageInfomation != null && Storage._storageInfomation.saveLocalFiles.Contains(file);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00015E94 File Offset: 0x00014294
		public static string StorageOculusBucketName()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._storageInfomation.Oculus.bucketName;
			}
			return null;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00015EB7 File Offset: 0x000142B7
		public static string StorageXboxContainerName()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._storageInfomation.Xbox.containerName;
			}
			return null;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00015EDA File Offset: 0x000142DA
		public static string StorageNintendoMountName()
		{
			if (Storage._storageInfomation != null)
			{
				return Storage._storageInfomation.Nintendo.MountName;
			}
			return null;
		}

		// Token: 0x0400022D RID: 557
		private static string _directory = "C://";

		// Token: 0x0400022E RID: 558
		private static PlatformStorageDatabase _storageInfomation;
	}
}
