using System;

namespace I2.Loc
{
	// Token: 0x020003C7 RID: 967
	public static class PersistentStorage
	{
		// Token: 0x060015A1 RID: 5537 RVA: 0x0002CCE9 File Offset: 0x0002B0E9
		public static void SetSetting_String(string key, string value)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			PersistentStorage.mStorage.SetSetting_String(key, value);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0002CD0B File Offset: 0x0002B10B
		public static string GetSetting_String(string key, string defaultValue)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.GetSetting_String(key, defaultValue);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0002CD2D File Offset: 0x0002B12D
		public static void DeleteSetting(string key)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			PersistentStorage.mStorage.DeleteSetting(key);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0002CD4E File Offset: 0x0002B14E
		public static bool HasSetting(string key)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.HasSetting(key);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0002CD6F File Offset: 0x0002B16F
		public static void ForceSaveSettings()
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			PersistentStorage.mStorage.ForceSaveSettings();
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0002CD8F File Offset: 0x0002B18F
		public static bool CanAccessFiles()
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.CanAccessFiles();
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0002CDAF File Offset: 0x0002B1AF
		public static bool SaveFile(PersistentStorage.eFileType fileType, string fileName, string data, bool logExceptions = true)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.SaveFile(fileType, fileName, data, logExceptions);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0002CDD3 File Offset: 0x0002B1D3
		public static string LoadFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.LoadFile(fileType, fileName, logExceptions);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0002CDF6 File Offset: 0x0002B1F6
		public static bool DeleteFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.DeleteFile(fileType, fileName, logExceptions);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0002CE19 File Offset: 0x0002B219
		public static bool HasFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (PersistentStorage.mStorage == null)
			{
				PersistentStorage.mStorage = new I2CustomPersistentStorage();
			}
			return PersistentStorage.mStorage.HasFile(fileType, fileName, logExceptions);
		}

		// Token: 0x04000DAC RID: 3500
		private static I2CustomPersistentStorage mStorage;

		// Token: 0x020003C8 RID: 968
		public enum eFileType
		{
			// Token: 0x04000DAE RID: 3502
			Raw,
			// Token: 0x04000DAF RID: 3503
			Persistent,
			// Token: 0x04000DB0 RID: 3504
			Temporal,
			// Token: 0x04000DB1 RID: 3505
			Streaming
		}
	}
}
