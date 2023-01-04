using System;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x020005F7 RID: 1527
	[Serializable]
	public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x00051702 File Offset: 0x0004FB02
		public static T instance
		{
			get
			{
				return ScriptableObjectSingleton<T>.GetOrCreateinstance();
			}
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00051709 File Offset: 0x0004FB09
		public static bool instanceExists()
		{
			return ScriptableObjectSingleton<T>._instance != null;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x0005171C File Offset: 0x0004FB1C
		public static T GetOrCreateinstance()
		{
			if (ScriptableObjectSingleton<T>._instance)
			{
				return ScriptableObjectSingleton<T>._instance;
			}
			ScriptableObjectSingleton<T>._instance = (Resources.Load(ScriptableObjectSingleton<T>.AssetName) as T);
			if (!ScriptableObjectSingleton<T>._instance)
			{
				T instance = ScriptableObject.CreateInstance<T>();
				ScriptableObjectSingleton<T>._instance = instance;
			}
			return ScriptableObjectSingleton<T>._instance;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00051781 File Offset: 0x0004FB81
		protected virtual void Awake()
		{
			if (ScriptableObjectSingleton<T>._instance)
			{
				Debug.LogError("creating a " + typeof(T).ToString() + " when one already exists!");
			}
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000517BA File Offset: 0x0004FBBA
		public void OnDestroy()
		{
			if (ScriptableObjectSingleton<T>._instance == this)
			{
				ScriptableObjectSingleton<T>._instance = (T)((object)null);
			}
		}

		// Token: 0x0400193F RID: 6463
		private static string AssetName = typeof(T).Name;

		// Token: 0x04001940 RID: 6464
		private static T _instance = (T)((object)null);
	}
}
