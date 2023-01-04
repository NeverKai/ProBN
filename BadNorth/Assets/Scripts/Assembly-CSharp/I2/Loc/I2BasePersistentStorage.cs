using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x020003C9 RID: 969
	public abstract class I2BasePersistentStorage
	{
		// Token: 0x060015AC RID: 5548 RVA: 0x0002CE44 File Offset: 0x0002B244
		public virtual void SetSetting_String(string key, string value)
		{
			try
			{
				int length = value.Length;
				int num = 8000;
				if (length <= num)
				{
					PlayerPrefs.SetString(key, value);
				}
				else
				{
					int num2 = Mathf.CeilToInt((float)length / (float)num);
					for (int i = 0; i < num2; i++)
					{
						int num3 = num * i;
						PlayerPrefs.SetString(string.Format("[I2split]{0}{1}", i, key), value.Substring(num3, Mathf.Min(num, length - num3)));
					}
					PlayerPrefs.SetString(key, "[$I2#@div$]" + num2);
				}
			}
			catch (Exception)
			{
				Debug.LogError("Error saving PlayerPrefs " + key);
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0002CEFC File Offset: 0x0002B2FC
		public virtual string GetSetting_String(string key, string defaultValue)
		{
			string result;
			try
			{
				string text = PlayerPrefs.GetString(key, defaultValue);
				if (!string.IsNullOrEmpty(text) && text.StartsWith("[I2split]"))
				{
					int num = int.Parse(text.Substring("[I2split]".Length));
					text = string.Empty;
					for (int i = 0; i < num; i++)
					{
						text += PlayerPrefs.GetString(string.Format("[I2split]{0}{1}", i, key), string.Empty);
					}
				}
				result = text;
			}
			catch (Exception)
			{
				Debug.LogError("Error loading PlayerPrefs " + key);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0002CFAC File Offset: 0x0002B3AC
		public virtual void DeleteSetting(string key)
		{
			try
			{
				string @string = PlayerPrefs.GetString(key, null);
				if (!string.IsNullOrEmpty(@string) && @string.StartsWith("[I2split]"))
				{
					int num = int.Parse(@string.Substring("[I2split]".Length));
					for (int i = 0; i < num; i++)
					{
						PlayerPrefs.DeleteKey(string.Format("[I2split]{0}{1}", i, key));
					}
				}
				PlayerPrefs.DeleteKey(key);
			}
			catch (Exception)
			{
				Debug.LogError("Error deleting PlayerPrefs " + key);
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0002D04C File Offset: 0x0002B44C
		public virtual void ForceSaveSettings()
		{
			PlayerPrefs.Save();
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0002D053 File Offset: 0x0002B453
		public virtual bool HasSetting(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0002D05B File Offset: 0x0002B45B
		public virtual bool CanAccessFiles()
		{
			return true;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0002D060 File Offset: 0x0002B460
		private string UpdateFilename(PersistentStorage.eFileType fileType, string fileName)
		{
			if (fileType != PersistentStorage.eFileType.Persistent)
			{
				if (fileType != PersistentStorage.eFileType.Temporal)
				{
					if (fileType == PersistentStorage.eFileType.Streaming)
					{
						fileName = Application.streamingAssetsPath + "/" + fileName;
					}
				}
				else
				{
					fileName = Application.temporaryCachePath + "/" + fileName;
				}
			}
			else
			{
				fileName = Application.persistentDataPath + "/" + fileName;
			}
			return fileName;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0002D0D0 File Offset: 0x0002B4D0
		public virtual bool SaveFile(PersistentStorage.eFileType fileType, string fileName, string data, bool logExceptions = true)
		{
			if (!this.CanAccessFiles())
			{
				return false;
			}
			bool result;
			try
			{
				fileName = this.UpdateFilename(fileType, fileName);
				File.WriteAllText(fileName, data, Encoding.UTF8);
				result = true;
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Error saving file '",
						fileName,
						"'\n",
						ex
					}));
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0002D150 File Offset: 0x0002B550
		public virtual string LoadFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (!this.CanAccessFiles())
			{
				return null;
			}
			string result;
			try
			{
				fileName = this.UpdateFilename(fileType, fileName);
				result = File.ReadAllText(fileName, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Error loading file '",
						fileName,
						"'\n",
						ex
					}));
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0002D1CC File Offset: 0x0002B5CC
		public virtual bool DeleteFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (!this.CanAccessFiles())
			{
				return false;
			}
			bool result;
			try
			{
				fileName = this.UpdateFilename(fileType, fileName);
				File.Delete(fileName);
				result = true;
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Error deleting file '",
						fileName,
						"'\n",
						ex
					}));
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0002D244 File Offset: 0x0002B644
		public virtual bool HasFile(PersistentStorage.eFileType fileType, string fileName, bool logExceptions = true)
		{
			if (!this.CanAccessFiles())
			{
				return false;
			}
			bool result;
			try
			{
				fileName = this.UpdateFilename(fileType, fileName);
				result = File.Exists(fileName);
			}
			catch (Exception ex)
			{
				if (logExceptions)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Error requesting file '",
						fileName,
						"'\n",
						ex
					}));
				}
				result = false;
			}
			return result;
		}
	}
}
