using System;
using System.Diagnostics;
using UnityEngine;

namespace CS.Lights
{
	// Token: 0x02000391 RID: 913
	public class LightEffectManager : MonoBehaviour
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0002AAF4 File Offset: 0x00028EF4
		public static bool Active
		{
			get
			{
				return LightEffectManager._instance != null;
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x060014D4 RID: 5332 RVA: 0x0002AB04 File Offset: 0x00028F04
		// (remove) Token: 0x060014D5 RID: 5333 RVA: 0x0002AB38 File Offset: 0x00028F38
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected static event Action<SetLightingEvent> _OnEffecting;

		// Token: 0x060014D6 RID: 5334 RVA: 0x0002AB6C File Offset: 0x00028F6C
		public static void RegisterLightEffecter(Action<SetLightingEvent> logic)
		{
			object eventLocker = LightEffectManager._eventLocker;
			lock (eventLocker)
			{
				LightEffectManager._OnEffecting += logic;
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0002ABA8 File Offset: 0x00028FA8
		public static void DeregisterLightEffecter(Action<SetLightingEvent> logic)
		{
			object eventLocker = LightEffectManager._eventLocker;
			lock (eventLocker)
			{
				LightEffectManager._OnEffecting -= logic;
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0002ABE4 File Offset: 0x00028FE4
		protected static void ApplyEffect(SetLightingEvent effect)
		{
			object eventLocker = LightEffectManager._eventLocker;
			lock (eventLocker)
			{
				if (LightEffectManager._OnEffecting != null)
				{
					LightEffectManager._OnEffecting(effect);
				}
			}
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0002AC30 File Offset: 0x00029030
		// Note: this type is marked as 'beforefieldinit'.
		static LightEffectManager()
		{
			LightEffectManager._OnEffecting = null;
			LightEffectManager._eventLocker = new object();
		}

		// Token: 0x04000CF5 RID: 3317
		protected static LightEffectManager _instance = null;

		// Token: 0x04000CF7 RID: 3319
		protected static object _eventLocker;
	}
}
