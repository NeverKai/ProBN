using System;
using System.Collections;
using UnityEngine;

namespace I2.Loc
{
	// Token: 0x0200040A RID: 1034
	public class CoroutineManager : MonoBehaviour
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0003C97C File Offset: 0x0003AD7C
		private static CoroutineManager pInstance
		{
			get
			{
				if (CoroutineManager.mInstance == null)
				{
					GameObject gameObject = new GameObject("_Coroutiner");
					gameObject.hideFlags = HideFlags.HideAndDontSave;
					CoroutineManager.mInstance = gameObject.AddComponent<CoroutineManager>();
					if (Application.isPlaying)
					{
						UnityEngine.Object.DontDestroyOnLoad(gameObject);
					}
				}
				return CoroutineManager.mInstance;
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0003C9CC File Offset: 0x0003ADCC
		private void Awake()
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0003C9E3 File Offset: 0x0003ADE3
		public static Coroutine Start(IEnumerator coroutine)
		{
			return CoroutineManager.pInstance.StartCoroutine(coroutine);
		}

		// Token: 0x04000EAD RID: 3757
		private static CoroutineManager mInstance;
	}
}
