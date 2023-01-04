using System;
using UnityEngine;

namespace CS.Platform.Utils
{
	// Token: 0x02000075 RID: 117
	public static class Random
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x000154E6 File Offset: 0x000138E6
		public static void SetGameBasedUI(string asset)
		{
			CS.Platform.Utils.Random._gameBaseUIObject = asset;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000154EE File Offset: 0x000138EE
		public static void SetGameBasedUI(Type asset)
		{
			CS.Platform.Utils.Random._gameBaseUIScript = asset;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x000154F8 File Offset: 0x000138F8
		public static PlatformSystemMessenger CreateGameBasedUI(GameObject gameObject)
		{
			PlatformSystemMessenger platformSystemMessenger = null;
			if (CS.Platform.Utils.Random._gameBaseUIScript != null)
			{
				platformSystemMessenger = (PlatformSystemMessenger)gameObject.AddComponent(CS.Platform.Utils.Random._gameBaseUIScript);
				if (platformSystemMessenger == null)
				{
					Debug.LogError("[PlatformUtil] Dialog Setup Failed: Converting to PlatformSystemMessenger | Made: {0}", new object[]
					{
						CS.Platform.Utils.Random._gameBaseUIScript
					});
				}
			}
			else if (!string.IsNullOrEmpty(CS.Platform.Utils.Random._gameBaseUIObject))
			{
				GameObject gameObject2 = (GameObject)Resources.Load(CS.Platform.Utils.Random._gameBaseUIObject);
				if (gameObject2 != null)
				{
					gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject2, gameObject.transform, true);
					platformSystemMessenger = gameObject2.GetComponent<PlatformSystemMessenger>();
					if (platformSystemMessenger == null)
					{
						Debug.LogError("[PlatformUtil] Dialog Setup Failed: Couldn't find PlatformSystemMessenger | On: {0}", new object[]
						{
							CS.Platform.Utils.Random._gameBaseUIObject
						});
					}
				}
				else
				{
					Debug.LogError("[PlatformUtil] Dialog Setup Failed: Couldn't find object | Load: {0}", new object[]
					{
						CS.Platform.Utils.Random._gameBaseUIObject
					});
				}
			}
			else
			{
				Debug.LogError("[PlatformUtil] Dialog Setup Failed: No component or object found | Script: {0} | Object: {1}", new object[]
				{
					CS.Platform.Utils.Random._gameBaseUIScript,
					CS.Platform.Utils.Random._gameBaseUIObject
				});
			}
			if (platformSystemMessenger == null)
			{
				Debug.LogError("[PlatformUtil] Dialog Setup Failed: Using GUI fallback | Made: {0}", new object[]
				{
					typeof(GUISystemMessages)
				});
				platformSystemMessenger = gameObject.AddComponent<GUISystemMessages>();
				if (platformSystemMessenger == null)
				{
					Debug.LogError("[PlatformUtil] Dialog Setup Fallback Failed: Converting to PlatformSystemMessenger | Made: {0}", new object[]
					{
						typeof(GUISystemMessages)
					});
				}
			}
			return platformSystemMessenger;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001564B File Offset: 0x00013A4B
		public static bool LogMainUserIn(int target = 0)
		{
			if (0 <= BasePlatformManager.Instance.MainUserID)
			{
				return true;
			}
			Debug.LogInfo("[PlatformUtil] LogMainUserIn: Not logged in forcing relog on 0", new object[0]);
			BasePlatformManager.Instance.UserLeave(target);
			return BasePlatformManager.Instance.UserJoined(target);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015688 File Offset: 0x00013A88
		public static Texture2D CreateNewImage(bool apply = true, int w = 64, int h = 64)
		{
			Texture2D texture2D = new Texture2D(w, h);
			if (apply)
			{
				for (int i = 0; i < w; i++)
				{
					for (int j = 0; j < h; j++)
					{
						texture2D.SetPixel(i, j, CS.Platform.Utils.Random._NewImageColour);
					}
				}
				texture2D.Apply();
			}
			return texture2D;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000156DC File Offset: 0x00013ADC
		public static Texture2D CreateNewImageF(bool apply = true, int w = 64, int h = 64, TextureFormat format = TextureFormat.RGBA32, bool mipmap = false)
		{
			Texture2D texture2D = new Texture2D(w, h, format, mipmap);
			if (apply)
			{
				for (int i = 0; i < w; i++)
				{
					for (int j = 0; j < h; j++)
					{
						texture2D.SetPixel(i, j, CS.Platform.Utils.Random._NewImageColour);
					}
				}
				texture2D.Apply();
			}
			return texture2D;
		}

		// Token: 0x04000227 RID: 551
		private static string _gameBaseUIObject = null;

		// Token: 0x04000228 RID: 552
		private static Type _gameBaseUIScript = null;

		// Token: 0x04000229 RID: 553
		private static Color _NewImageColour = new Color(0f, 0f, 0f, 0f);
	}
}
