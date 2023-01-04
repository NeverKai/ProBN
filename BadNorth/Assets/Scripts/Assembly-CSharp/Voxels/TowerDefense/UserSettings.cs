using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using CS.Platform;
using I2.Loc;
using UnityEngine;
using Voxels.TowerDefense.ProfileInternals;

namespace Voxels.TowerDefense
{
	// Token: 0x020005A5 RID: 1445
	[Serializable]
	public class UserSettings : ISaveGameObject
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x000771E4 File Offset: 0x000755E4
		public UserSettings()
		{
			this._cursorBehaviour = this.GetDefaultCursorBehaviour();
			if (this.cursorBehaviour == UserSettings.CursorBehaviour.Touch)
			{
				CursorWrapper.BlockVisibility(this);
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00077278 File Offset: 0x00075678
		static UserSettings()
		{
			UserSettings.onUpdated = delegate(UserSettings A_0)
			{
			};
			UserSettings.LanguageDisplayNames = new string[]
			{
				"English",
				"Français",
				"Italiano",
				"Deutsch",
				"Español",
				"日本語",
				"Português",
				"Русский",
				"简体中文",
				"繁體中文",
				"한국어",
				"Türkçe"
			};
			UserSettings.I2LanguageIDs = new string[]
			{
				"English",
				"French",
				"Italian",
				"German",
				"Spanish",
				"Japanese",
				"Portuguese",
				"Russian",
				"Chinese (Simplified)",
				"Chinese (Traditional)",
				"Korean",
				"Turkish"
			};
			UserSettings.isoMappings = new Dictionary<string, UserSettings.Language>
			{
				{
					"ja",
					UserSettings.Language.Japanese
				},
				{
					"en",
					UserSettings.Language.English
				},
				{
					"es",
					UserSettings.Language.Spanish
				},
				{
					"fr",
					UserSettings.Language.French
				},
				{
					"de",
					UserSettings.Language.German
				},
				{
					"it",
					UserSettings.Language.Italian
				},
				{
					"pt",
					UserSettings.Language.Portugese
				},
				{
					"ru",
					UserSettings.Language.Russian
				},
				{
					"ko",
					UserSettings.Language.Korean
				},
				{
					"tr",
					UserSettings.Language.Turkish
				},
				{
					"zh",
					UserSettings.Language.ChineseSimplified
				},
				{
					"zh-CHS",
					UserSettings.Language.ChineseSimplified
				},
				{
					"zh-CHT",
					UserSettings.Language.ChineseTraditional
				}
			};
			if (UserSettings.<>f__mg$cache0 == null)
			{
				UserSettings.<>f__mg$cache0 = new PlatformEvents.PlatformBoolEventDel(UserSettings.OnPlatformGamePauseEvent);
			}
			PlatformEvents.OnPlatformGamePauseEvent += UserSettings.<>f__mg$cache0;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x00077441 File Offset: 0x00075841
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x00077449 File Offset: 0x00075849
		public bool dirty { get; private set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x00077452 File Offset: 0x00075852
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x0007745A File Offset: 0x0007585A
		public bool showBlood
		{
			get
			{
				return this.blood;
			}
			set
			{
				this.blood = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00077463 File Offset: 0x00075863
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x0007746B File Offset: 0x0007586B
		public float displaySafeArea
		{
			get
			{
				return this._displaySafeArea;
			}
			set
			{
				this._displaySafeArea = Mathf.Clamp(value, 0.9f, 1f);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x00077483 File Offset: 0x00075883
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x0007749D File Offset: 0x0007589D
		public UserSettings.CursorBehaviour cursorBehaviour
		{
			get
			{
				if (!Platform.Is(EPlatform.PC))
				{
					return this.GetDefaultCursorBehaviour();
				}
				return this._cursorBehaviour;
			}
			set
			{
				this._cursorBehaviour = value;
			}
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000774A6 File Offset: 0x000758A6
		private UserSettings.CursorBehaviour GetDefaultCursorBehaviour()
		{
			if (Platform.Is(EPlatform.Touchscreen))
			{
				return UserSettings.CursorBehaviour.Touch;
			}
			if (Platform.Is(EPlatform.Console))
			{
				return UserSettings.CursorBehaviour.None;
			}
			return UserSettings.CursorBehaviour.TwoButton;
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060025B8 RID: 9656 RVA: 0x000774CC File Offset: 0x000758CC
		// (remove) Token: 0x060025B9 RID: 9657 RVA: 0x00077500 File Offset: 0x00075900
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<UserSettings> onUpdated;

		// Token: 0x060025BA RID: 9658 RVA: 0x00077534 File Offset: 0x00075934
		public void ProcessUpdate(bool dirty = true)
		{
			QualitySettings.vSyncCount = ((!this.vSync) ? 0 : 1);
			QualitySettings.antiAliasing = ((this.antiAliasingLevel <= UserSettings.AntiAliasOption.None) ? 0 : 8);
			if (this.showBlood)
			{
				Shader.EnableKeyword("_BLOOD_ON");
			}
			else
			{
				Shader.DisableKeyword("_BLOOD_ON");
			}
			LocalizationManager.CurrentLanguage = UserSettings.I2LanguageIDs[(int)this.language.value];
			if (this.cursorBehaviour != UserSettings.CursorBehaviour.Touch)
			{
				CursorWrapper.UnblockVisibility(this);
			}
			UserSettings.onUpdated(this);
			this.dirty = dirty;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000775D3 File Offset: 0x000759D3
		string ISaveGameObject.fileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000775DB File Offset: 0x000759DB
		void ISaveGameObject.PostCreate(string fileName)
		{
			this.fileName = fileName;
			this.ProcessUpdate(false);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000775EC File Offset: 0x000759EC
		[OnDeserialized]
		private void PostLoad(StreamingContext context)
		{
			if (this.language == null)
			{
				this.language = UserSettings.GetSystemLanguage();
			}
			this._displaySafeArea = ((this._displaySafeArea >= 0.9f) ? Mathf.Clamp(this._displaySafeArea, 0.9f, 1f) : 1f);
			this.ProcessUpdate(false);
			if (this.cursorBehaviour == UserSettings.CursorBehaviour.Touch)
			{
				CursorWrapper.BlockVisibility(this);
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00077662 File Offset: 0x00075A62
		[OnSerialized]
		private void PostSave(StreamingContext context)
		{
			this.dirty = false;
			if (this.cursorBehaviour == UserSettings.CursorBehaviour.Touch)
			{
				CursorWrapper.BlockVisibility(this);
			}
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0007767D File Offset: 0x00075A7D
		private static void OnPlatformGamePauseEvent(bool effect)
		{
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0007767F File Offset: 0x00075A7F
		public UserSettings Clone()
		{
			return (UserSettings)base.MemberwiseClone();
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0007768C File Offset: 0x00075A8C
		public static implicit operator bool(UserSettings settings)
		{
			return settings != null;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x00077698 File Offset: 0x00075A98
		private static UserSettings.Language GetSystemLanguage()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			string name = currentCulture.Name;
			string twoLetterISOLanguageName = currentCulture.TwoLetterISOLanguageName;
			UserSettings.Language result;
			if ((!(twoLetterISOLanguageName == "zh") || currentCulture.Parent == null || !UserSettings.isoMappings.TryGetValue(currentCulture.Parent.Name, out result)) && !UserSettings.isoMappings.TryGetValue(name, out result) && !UserSettings.isoMappings.TryGetValue(twoLetterISOLanguageName, out result) && !UserSettings.GetOSFallbackLanguage(out result))
			{
				result = UserSettings.Language.English;
			}
			return result;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00077723 File Offset: 0x00075B23
		private static bool GetOSFallbackLanguage(out UserSettings.Language lang)
		{
			lang = UserSettings.Language.English;
			return false;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00077729 File Offset: 0x00075B29
		public static void I2LanguageInitToSystem()
		{
			LocalizationManager.CurrentLanguage = UserSettings.I2LanguageIDs[(int)UserSettings.GetSystemLanguage()];
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0007773C File Offset: 0x00075B3C
		public static UserSettings.Language GetLanguageEnumFromI2Code(string i2Code)
		{
			for (int i = 0; i < UserSettings.I2LanguageIDs.Length; i++)
			{
				if (UserSettings.I2LanguageIDs[i] == i2Code)
				{
					return (UserSettings.Language)i;
				}
			}
			UnityEngine.Debug.LogFormat("unknown Language Code {0}", new object[]
			{
				i2Code
			});
			return UserSettings.Language.English;
		}

		// Token: 0x040017E2 RID: 6114
		public const string defaultFileName = "settings";

		// Token: 0x040017E3 RID: 6115
		private const float minSafeArea = 0.9f;

		// Token: 0x040017E4 RID: 6116
		private const int vSyncOff = 0;

		// Token: 0x040017E5 RID: 6117
		private const int vSync60 = 1;

		// Token: 0x040017E6 RID: 6118
		private const int vSync30 = 2;

		// Token: 0x040017E8 RID: 6120
		public SerializeFriendlyEnum<UserSettings.Language> language = UserSettings.GetSystemLanguage();

		// Token: 0x040017E9 RID: 6121
		public PlatformCanvasScaler.PCScaleMode pcScaleMode;

		// Token: 0x040017EA RID: 6122
		public bool vSync = true;

		// Token: 0x040017EB RID: 6123
		public int targetFramerate = 30;

		// Token: 0x040017EC RID: 6124
		public SerializeFriendlyEnum<UserSettings.AntiAliasOption> antiAliasingLevel = UserSettings.AntiAliasOption.MSAA8x;

		// Token: 0x040017ED RID: 6125
		private bool blood = true;

		// Token: 0x040017EE RID: 6126
		private float _displaySafeArea = 1f;

		// Token: 0x040017EF RID: 6127
		public bool invertGamePadCameraX;

		// Token: 0x040017F0 RID: 6128
		public bool invertGamePadCameraY;

		// Token: 0x040017F1 RID: 6129
		public bool suppressCameraRotateSnap;

		// Token: 0x040017F2 RID: 6130
		public bool suppressCameraZoomSnap;

		// Token: 0x040017F3 RID: 6131
		public UserSettings.GamepadLayout gamepadLayout;

		// Token: 0x040017F4 RID: 6132
		private UserSettings.CursorBehaviour _cursorBehaviour;

		// Token: 0x040017F5 RID: 6133
		public int sfxVolume = 10;

		// Token: 0x040017F6 RID: 6134
		public int musicVolume = 10;

		// Token: 0x040017F7 RID: 6135
		public int ambianceVolume = 10;

		// Token: 0x040017F8 RID: 6136
		private string fileName = string.Empty;

		// Token: 0x040017FA RID: 6138
		public static readonly string[] LanguageDisplayNames;

		// Token: 0x040017FB RID: 6139
		public static readonly string[] I2LanguageIDs;

		// Token: 0x040017FC RID: 6140
		private static Dictionary<string, UserSettings.Language> isoMappings;

		// Token: 0x040017FD RID: 6141
		[CompilerGenerated]
		private static PlatformEvents.PlatformBoolEventDel <>f__mg$cache0;

		// Token: 0x020005A6 RID: 1446
		public enum AntiAliasOption
		{
			// Token: 0x040017FF RID: 6143
			None,
			// Token: 0x04001800 RID: 6144
			MSAA8x,
			// Token: 0x04001801 RID: 6145
			AACamera
		}

		// Token: 0x020005A7 RID: 1447
		public enum CursorBehaviour
		{
			// Token: 0x04001803 RID: 6147
			TwoButton,
			// Token: 0x04001804 RID: 6148
			OneButton,
			// Token: 0x04001805 RID: 6149
			Touch,
			// Token: 0x04001806 RID: 6150
			None
		}

		// Token: 0x020005A8 RID: 1448
		public enum GamepadLayout
		{
			// Token: 0x04001808 RID: 6152
			Classic,
			// Token: 0x04001809 RID: 6153
			QuickSelect
		}

		// Token: 0x020005A9 RID: 1449
		public enum Language
		{
			// Token: 0x0400180B RID: 6155
			English,
			// Token: 0x0400180C RID: 6156
			French,
			// Token: 0x0400180D RID: 6157
			Italian,
			// Token: 0x0400180E RID: 6158
			German,
			// Token: 0x0400180F RID: 6159
			Spanish,
			// Token: 0x04001810 RID: 6160
			Japanese,
			// Token: 0x04001811 RID: 6161
			Portugese,
			// Token: 0x04001812 RID: 6162
			Russian,
			// Token: 0x04001813 RID: 6163
			ChineseSimplified,
			// Token: 0x04001814 RID: 6164
			ChineseTraditional,
			// Token: 0x04001815 RID: 6165
			Korean,
			// Token: 0x04001816 RID: 6166
			Turkish
		}
	}
}
