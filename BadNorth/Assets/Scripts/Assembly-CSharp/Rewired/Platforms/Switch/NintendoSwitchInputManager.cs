using System;
using System.Collections.Generic;
using Rewired.Data;
using Rewired.Utils.Interfaces;
using UnityEngine;

namespace Rewired.Platforms.Switch
{
	// Token: 0x020004AF RID: 1199
	[AddComponentMenu("Rewired/Nintendo Switch Input Manager")]
	[RequireComponent(typeof(InputManager))]
	public sealed class NintendoSwitchInputManager : MonoBehaviour, IExternalInputManager
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x00050B6F File Offset: 0x0004EF6F
		object IExternalInputManager.Initialize(Platform platform, ConfigVars configVars)
		{
			return null;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00050B72 File Offset: 0x0004EF72
		void IExternalInputManager.Deinitialize()
		{
		}

		// Token: 0x040012DE RID: 4830
		[SerializeField]
		private NintendoSwitchInputManager.UserData _userData = new NintendoSwitchInputManager.UserData();

		// Token: 0x020004B0 RID: 1200
		[Serializable]
		private class UserData : IKeyedData<int>
		{
			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x06001E3E RID: 7742 RVA: 0x00050C22 File Offset: 0x0004F022
			// (set) Token: 0x06001E3F RID: 7743 RVA: 0x00050C2A File Offset: 0x0004F02A
			public int allowedNpadStyles
			{
				get
				{
					return this._allowedNpadStyles;
				}
				set
				{
					this._allowedNpadStyles = value;
				}
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00050C33 File Offset: 0x0004F033
			// (set) Token: 0x06001E41 RID: 7745 RVA: 0x00050C3B File Offset: 0x0004F03B
			public int joyConGripStyle
			{
				get
				{
					return this._joyConGripStyle;
				}
				set
				{
					this._joyConGripStyle = value;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x06001E42 RID: 7746 RVA: 0x00050C44 File Offset: 0x0004F044
			// (set) Token: 0x06001E43 RID: 7747 RVA: 0x00050C4C File Offset: 0x0004F04C
			public bool adjustIMUsForGripStyle
			{
				get
				{
					return this._adjustIMUsForGripStyle;
				}
				set
				{
					this._adjustIMUsForGripStyle = value;
				}
			}

			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06001E44 RID: 7748 RVA: 0x00050C55 File Offset: 0x0004F055
			// (set) Token: 0x06001E45 RID: 7749 RVA: 0x00050C5D File Offset: 0x0004F05D
			public int handheldActivationMode
			{
				get
				{
					return this._handheldActivationMode;
				}
				set
				{
					this._handheldActivationMode = value;
				}
			}

			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06001E46 RID: 7750 RVA: 0x00050C66 File Offset: 0x0004F066
			// (set) Token: 0x06001E47 RID: 7751 RVA: 0x00050C6E File Offset: 0x0004F06E
			public bool assignJoysticksByNpadId
			{
				get
				{
					return this._assignJoysticksByNpadId;
				}
				set
				{
					this._assignJoysticksByNpadId = value;
				}
			}

			// Token: 0x170003FB RID: 1019
			// (get) Token: 0x06001E48 RID: 7752 RVA: 0x00050C77 File Offset: 0x0004F077
			// (set) Token: 0x06001E49 RID: 7753 RVA: 0x00050C7F File Offset: 0x0004F07F
			public bool useVibrationThread
			{
				get
				{
					return this._useVibrationThread;
				}
				set
				{
					this._useVibrationThread = value;
				}
			}

			// Token: 0x170003FC RID: 1020
			// (get) Token: 0x06001E4A RID: 7754 RVA: 0x00050C88 File Offset: 0x0004F088
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo1
			{
				get
				{
					return this._npadNo1;
				}
			}

			// Token: 0x170003FD RID: 1021
			// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00050C90 File Offset: 0x0004F090
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo2
			{
				get
				{
					return this._npadNo2;
				}
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x06001E4C RID: 7756 RVA: 0x00050C98 File Offset: 0x0004F098
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo3
			{
				get
				{
					return this._npadNo3;
				}
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00050CA0 File Offset: 0x0004F0A0
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo4
			{
				get
				{
					return this._npadNo4;
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00050CA8 File Offset: 0x0004F0A8
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo5
			{
				get
				{
					return this._npadNo5;
				}
			}

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00050CB0 File Offset: 0x0004F0B0
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo6
			{
				get
				{
					return this._npadNo6;
				}
			}

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x06001E50 RID: 7760 RVA: 0x00050CB8 File Offset: 0x0004F0B8
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo7
			{
				get
				{
					return this._npadNo7;
				}
			}

			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00050CC0 File Offset: 0x0004F0C0
			private NintendoSwitchInputManager.NpadSettings_Internal npadNo8
			{
				get
				{
					return this._npadNo8;
				}
			}

			// Token: 0x17000404 RID: 1028
			// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00050CC8 File Offset: 0x0004F0C8
			private NintendoSwitchInputManager.NpadSettings_Internal npadHandheld
			{
				get
				{
					return this._npadHandheld;
				}
			}

			// Token: 0x17000405 RID: 1029
			// (get) Token: 0x06001E53 RID: 7763 RVA: 0x00050CD0 File Offset: 0x0004F0D0
			public NintendoSwitchInputManager.DebugPadSettings_Internal debugPad
			{
				get
				{
					return this._debugPad;
				}
			}

			// Token: 0x17000406 RID: 1030
			// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00050CD8 File Offset: 0x0004F0D8
			private Dictionary<int, object[]> delegates
			{
				get
				{
					if (this.__delegates != null)
					{
						return this.__delegates;
					}
					Dictionary<int, object[]> dictionary = new Dictionary<int, object[]>();
					dictionary.Add(0, new object[]
					{
						new Func<int>(this.get_allowedNpadStyles),
						new Action<int>(delegate(int x)
						{
							this.allowedNpadStyles = x;
						})
					});
					dictionary.Add(1, new object[]
					{
						new Func<int>(this.get_joyConGripStyle),
						new Action<int>(delegate(int x)
						{
							this.joyConGripStyle = x;
						})
					});
					dictionary.Add(2, new object[]
					{
						new Func<bool>(this.get_adjustIMUsForGripStyle),
						new Action<bool>(delegate(bool x)
						{
							this.adjustIMUsForGripStyle = x;
						})
					});
					dictionary.Add(3, new object[]
					{
						new Func<int>(this.get_handheldActivationMode),
						new Action<int>(delegate(int x)
						{
							this.handheldActivationMode = x;
						})
					});
					dictionary.Add(4, new object[]
					{
						new Func<bool>(this.get_assignJoysticksByNpadId),
						new Action<bool>(delegate(bool x)
						{
							this.assignJoysticksByNpadId = x;
						})
					});
					Dictionary<int, object[]> dictionary2 = dictionary;
					int key = 5;
					object[] array = new object[2];
					array[0] = new Func<object>(this.get_npadNo1);
					dictionary2.Add(key, array);
					Dictionary<int, object[]> dictionary3 = dictionary;
					int key2 = 6;
					object[] array2 = new object[2];
					array2[0] = new Func<object>(this.get_npadNo2);
					dictionary3.Add(key2, array2);
					Dictionary<int, object[]> dictionary4 = dictionary;
					int key3 = 7;
					object[] array3 = new object[2];
					array3[0] = new Func<object>(this.get_npadNo3);
					dictionary4.Add(key3, array3);
					Dictionary<int, object[]> dictionary5 = dictionary;
					int key4 = 8;
					object[] array4 = new object[2];
					array4[0] = new Func<object>(this.get_npadNo4);
					dictionary5.Add(key4, array4);
					Dictionary<int, object[]> dictionary6 = dictionary;
					int key5 = 9;
					object[] array5 = new object[2];
					array5[0] = new Func<object>(this.get_npadNo5);
					dictionary6.Add(key5, array5);
					Dictionary<int, object[]> dictionary7 = dictionary;
					int key6 = 10;
					object[] array6 = new object[2];
					array6[0] = new Func<object>(this.get_npadNo6);
					dictionary7.Add(key6, array6);
					Dictionary<int, object[]> dictionary8 = dictionary;
					int key7 = 11;
					object[] array7 = new object[2];
					array7[0] = new Func<object>(this.get_npadNo7);
					dictionary8.Add(key7, array7);
					Dictionary<int, object[]> dictionary9 = dictionary;
					int key8 = 12;
					object[] array8 = new object[2];
					array8[0] = new Func<object>(this.get_npadNo8);
					dictionary9.Add(key8, array8);
					Dictionary<int, object[]> dictionary10 = dictionary;
					int key9 = 13;
					object[] array9 = new object[2];
					array9[0] = new Func<object>(this.get_npadHandheld);
					dictionary10.Add(key9, array9);
					Dictionary<int, object[]> dictionary11 = dictionary;
					int key10 = 14;
					object[] array10 = new object[2];
					array10[0] = new Func<object>(this.get_debugPad);
					dictionary11.Add(key10, array10);
					dictionary.Add(15, new object[]
					{
						new Func<bool>(this.get_useVibrationThread),
						new Action<bool>(delegate(bool x)
						{
							this.useVibrationThread = x;
						})
					});
					return this.__delegates = dictionary;
				}
			}

			// Token: 0x06001E55 RID: 7765 RVA: 0x00050F28 File Offset: 0x0004F328
			bool IKeyedData<int>.TryGetValue<T>(int key, out T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					value = default(T);
					return false;
				}
				Func<T> func = array[0] as Func<T>;
				if (func == null)
				{
					value = default(T);
					return false;
				}
				value = func();
				return true;
			}

			// Token: 0x06001E56 RID: 7766 RVA: 0x00050F88 File Offset: 0x0004F388
			bool IKeyedData<int>.TrySetValue<T>(int key, T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					return false;
				}
				Action<T> action = array[1] as Action<T>;
				if (action == null)
				{
					return false;
				}
				action(value);
				return true;
			}

			// Token: 0x040012DF RID: 4831
			[SerializeField]
			private int _allowedNpadStyles = -1;

			// Token: 0x040012E0 RID: 4832
			[SerializeField]
			private int _joyConGripStyle = 1;

			// Token: 0x040012E1 RID: 4833
			[SerializeField]
			private bool _adjustIMUsForGripStyle = true;

			// Token: 0x040012E2 RID: 4834
			[SerializeField]
			private int _handheldActivationMode;

			// Token: 0x040012E3 RID: 4835
			[SerializeField]
			private bool _assignJoysticksByNpadId = true;

			// Token: 0x040012E4 RID: 4836
			[SerializeField]
			private bool _useVibrationThread = true;

			// Token: 0x040012E5 RID: 4837
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo1 = new NintendoSwitchInputManager.NpadSettings_Internal(0);

			// Token: 0x040012E6 RID: 4838
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo2 = new NintendoSwitchInputManager.NpadSettings_Internal(1);

			// Token: 0x040012E7 RID: 4839
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo3 = new NintendoSwitchInputManager.NpadSettings_Internal(2);

			// Token: 0x040012E8 RID: 4840
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo4 = new NintendoSwitchInputManager.NpadSettings_Internal(3);

			// Token: 0x040012E9 RID: 4841
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo5 = new NintendoSwitchInputManager.NpadSettings_Internal(4);

			// Token: 0x040012EA RID: 4842
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo6 = new NintendoSwitchInputManager.NpadSettings_Internal(5);

			// Token: 0x040012EB RID: 4843
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo7 = new NintendoSwitchInputManager.NpadSettings_Internal(6);

			// Token: 0x040012EC RID: 4844
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadNo8 = new NintendoSwitchInputManager.NpadSettings_Internal(7);

			// Token: 0x040012ED RID: 4845
			[SerializeField]
			private NintendoSwitchInputManager.NpadSettings_Internal _npadHandheld = new NintendoSwitchInputManager.NpadSettings_Internal(0);

			// Token: 0x040012EE RID: 4846
			[SerializeField]
			private NintendoSwitchInputManager.DebugPadSettings_Internal _debugPad = new NintendoSwitchInputManager.DebugPadSettings_Internal(0);

			// Token: 0x040012EF RID: 4847
			private Dictionary<int, object[]> __delegates;
		}

		// Token: 0x020004B1 RID: 1201
		[Serializable]
		private sealed class NpadSettings_Internal : IKeyedData<int>
		{
			// Token: 0x06001E5D RID: 7773 RVA: 0x00050FF9 File Offset: 0x0004F3F9
			internal NpadSettings_Internal(int playerId)
			{
				this._rewiredPlayerId = playerId;
			}

			// Token: 0x17000407 RID: 1031
			// (get) Token: 0x06001E5E RID: 7774 RVA: 0x00051016 File Offset: 0x0004F416
			// (set) Token: 0x06001E5F RID: 7775 RVA: 0x0005101E File Offset: 0x0004F41E
			private bool isAllowed
			{
				get
				{
					return this._isAllowed;
				}
				set
				{
					this._isAllowed = value;
				}
			}

			// Token: 0x17000408 RID: 1032
			// (get) Token: 0x06001E60 RID: 7776 RVA: 0x00051027 File Offset: 0x0004F427
			// (set) Token: 0x06001E61 RID: 7777 RVA: 0x0005102F File Offset: 0x0004F42F
			private int rewiredPlayerId
			{
				get
				{
					return this._rewiredPlayerId;
				}
				set
				{
					this._rewiredPlayerId = value;
				}
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06001E62 RID: 7778 RVA: 0x00051038 File Offset: 0x0004F438
			// (set) Token: 0x06001E63 RID: 7779 RVA: 0x00051040 File Offset: 0x0004F440
			private int joyConAssignmentMode
			{
				get
				{
					return this._joyConAssignmentMode;
				}
				set
				{
					this._joyConAssignmentMode = value;
				}
			}

			// Token: 0x1700040A RID: 1034
			// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0005104C File Offset: 0x0004F44C
			private Dictionary<int, object[]> delegates
			{
				get
				{
					if (this.__delegates != null)
					{
						return this.__delegates;
					}
					return this.__delegates = new Dictionary<int, object[]>
					{
						{
							0,
							new object[]
							{
								new Func<bool>(this.get_isAllowed),
								new Action<bool>(delegate(bool x)
								{
									this.isAllowed = x;
								})
							}
						},
						{
							1,
							new object[]
							{
								new Func<int>(this.get_rewiredPlayerId),
								new Action<int>(delegate(int x)
								{
									this.rewiredPlayerId = x;
								})
							}
						},
						{
							2,
							new object[]
							{
								new Func<int>(this.get_joyConAssignmentMode),
								new Action<int>(delegate(int x)
								{
									this.joyConAssignmentMode = x;
								})
							}
						}
					};
				}
			}

			// Token: 0x06001E65 RID: 7781 RVA: 0x000510FC File Offset: 0x0004F4FC
			bool IKeyedData<int>.TryGetValue<T>(int key, out T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					value = default(T);
					return false;
				}
				Func<T> func = array[0] as Func<T>;
				if (func == null)
				{
					value = default(T);
					return false;
				}
				value = func();
				return true;
			}

			// Token: 0x06001E66 RID: 7782 RVA: 0x0005115C File Offset: 0x0004F55C
			bool IKeyedData<int>.TrySetValue<T>(int key, T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					return false;
				}
				Action<T> action = array[1] as Action<T>;
				if (action == null)
				{
					return false;
				}
				action(value);
				return true;
			}

			// Token: 0x040012F0 RID: 4848
			[Tooltip("Determines whether this Npad id is allowed to be used by the system.")]
			[SerializeField]
			private bool _isAllowed = true;

			// Token: 0x040012F1 RID: 4849
			[Tooltip("The Rewired Player Id assigned to this Npad id.")]
			[SerializeField]
			private int _rewiredPlayerId;

			// Token: 0x040012F2 RID: 4850
			[Tooltip("Determines how Joy-Cons should be handled.\n\nUnmodified: Joy-Con assignment mode will be left at the system default.\nDual: Joy-Cons pairs are handled as a single controller.\nSingle: Joy-Cons are handled as individual controllers.")]
			[SerializeField]
			private int _joyConAssignmentMode = -1;

			// Token: 0x040012F3 RID: 4851
			private Dictionary<int, object[]> __delegates;
		}

		// Token: 0x020004B2 RID: 1202
		[Serializable]
		private sealed class DebugPadSettings_Internal : IKeyedData<int>
		{
			// Token: 0x06001E6A RID: 7786 RVA: 0x000511B2 File Offset: 0x0004F5B2
			internal DebugPadSettings_Internal(int playerId)
			{
				this._rewiredPlayerId = playerId;
			}

			// Token: 0x1700040B RID: 1035
			// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000511C1 File Offset: 0x0004F5C1
			// (set) Token: 0x06001E6C RID: 7788 RVA: 0x000511C9 File Offset: 0x0004F5C9
			private int rewiredPlayerId
			{
				get
				{
					return this._rewiredPlayerId;
				}
				set
				{
					this._rewiredPlayerId = value;
				}
			}

			// Token: 0x1700040C RID: 1036
			// (get) Token: 0x06001E6D RID: 7789 RVA: 0x000511D2 File Offset: 0x0004F5D2
			// (set) Token: 0x06001E6E RID: 7790 RVA: 0x000511DA File Offset: 0x0004F5DA
			private bool enabled
			{
				get
				{
					return this._enabled;
				}
				set
				{
					this._enabled = value;
				}
			}

			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x06001E6F RID: 7791 RVA: 0x000511E4 File Offset: 0x0004F5E4
			private Dictionary<int, object[]> delegates
			{
				get
				{
					if (this.__delegates != null)
					{
						return this.__delegates;
					}
					return this.__delegates = new Dictionary<int, object[]>
					{
						{
							0,
							new object[]
							{
								new Func<bool>(this.get_enabled),
								new Action<bool>(delegate(bool x)
								{
									this.enabled = x;
								})
							}
						},
						{
							1,
							new object[]
							{
								new Func<int>(this.get_rewiredPlayerId),
								new Action<int>(delegate(int x)
								{
									this.rewiredPlayerId = x;
								})
							}
						}
					};
				}
			}

			// Token: 0x06001E70 RID: 7792 RVA: 0x0005126C File Offset: 0x0004F66C
			bool IKeyedData<int>.TryGetValue<T>(int key, out T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					value = default(T);
					return false;
				}
				Func<T> func = array[0] as Func<T>;
				if (func == null)
				{
					value = default(T);
					return false;
				}
				value = func();
				return true;
			}

			// Token: 0x06001E71 RID: 7793 RVA: 0x000512CC File Offset: 0x0004F6CC
			bool IKeyedData<int>.TrySetValue<T>(int key, T value)
			{
				object[] array;
				if (!this.delegates.TryGetValue(key, out array))
				{
					return false;
				}
				Action<T> action = array[1] as Action<T>;
				if (action == null)
				{
					return false;
				}
				action(value);
				return true;
			}

			// Token: 0x040012F4 RID: 4852
			[Tooltip("Determines whether the Debug Pad will be enabled.")]
			[SerializeField]
			private bool _enabled;

			// Token: 0x040012F5 RID: 4853
			[Tooltip("The Rewired Player Id to which the Debug Pad will be assigned.")]
			[SerializeField]
			private int _rewiredPlayerId;

			// Token: 0x040012F6 RID: 4854
			private Dictionary<int, object[]> __delegates;
		}
	}
}
