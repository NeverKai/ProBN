using System;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x06002682 RID: 9858 RVA: 0x00004CBD File Offset: 0x000030BD
	public static T instance
	{
		get
		{
			if (!Singleton<T>._instance)
			{
				Singleton<T>._instance = UnityEngine.Object.FindObjectOfType<T>();
			}
			return Singleton<T>._instance;
		}
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x00004CE2 File Offset: 0x000030E2
	protected virtual void Awake()
	{
		Singleton<T>._instance = (this as T);
	}

	// Token: 0x04001883 RID: 6275
	protected static T _instance;
}
