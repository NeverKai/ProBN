using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace I2.Loc
{
	// Token: 0x02000414 RID: 1044
	public class ResourceManager : MonoBehaviour
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0003D4F8 File Offset: 0x0003B8F8
		public static ResourceManager pInstance
		{
			get
			{
				bool flag = ResourceManager.mInstance == null;
				if (ResourceManager.mInstance == null)
				{
					ResourceManager.mInstance = (ResourceManager)UnityEngine.Object.FindObjectOfType(typeof(ResourceManager));
				}
				if (ResourceManager.mInstance == null)
				{
					GameObject gameObject = new GameObject("I2ResourceManager", new Type[]
					{
						typeof(ResourceManager)
					});
					gameObject.hideFlags |= HideFlags.HideAndDontSave;
					ResourceManager.mInstance = gameObject.GetComponent<ResourceManager>();
					if (ResourceManager.<>f__mg$cache0 == null)
					{
						ResourceManager.<>f__mg$cache0 = new UnityAction<Scene, LoadSceneMode>(ResourceManager.MyOnLevelWasLoaded);
					}
					SceneManager.sceneLoaded += ResourceManager.<>f__mg$cache0;
				}
				if (flag && Application.isPlaying)
				{
					UnityEngine.Object.DontDestroyOnLoad(ResourceManager.mInstance.gameObject);
				}
				return ResourceManager.mInstance;
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0003D5C8 File Offset: 0x0003B9C8
		public static void MyOnLevelWasLoaded(Scene scene, LoadSceneMode mode)
		{
			ResourceManager.pInstance.CleanResourceCache();
			LocalizationManager.UpdateSources();
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0003D5DC File Offset: 0x0003B9DC
		public T GetAsset<T>(string Name) where T : UnityEngine.Object
		{
			T t = this.FindAsset(Name) as T;
			if (t != null)
			{
				return t;
			}
			return this.LoadFromResources<T>(Name);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0003D618 File Offset: 0x0003BA18
		private UnityEngine.Object FindAsset(string Name)
		{
			if (this.Assets != null)
			{
				int i = 0;
				int num = this.Assets.Length;
				while (i < num)
				{
					if (this.Assets[i] != null && this.Assets[i].name == Name)
					{
						return this.Assets[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0003D680 File Offset: 0x0003BA80
		public bool HasAsset(UnityEngine.Object Obj)
		{
			return this.Assets != null && Array.IndexOf<UnityEngine.Object>(this.Assets, Obj) >= 0;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0003D6A4 File Offset: 0x0003BAA4
		public T LoadFromResources<T>(string Path) where T : UnityEngine.Object
		{
			T result;
			try
			{
				UnityEngine.Object @object;
				if (string.IsNullOrEmpty(Path))
				{
					result = (T)((object)null);
				}
				else if (this.mResourcesCache.TryGetValue(Path, out @object) && @object != null)
				{
					result = (@object as T);
				}
				else
				{
					T t = (T)((object)null);
					if (Path.EndsWith("]", StringComparison.OrdinalIgnoreCase))
					{
						int num = Path.LastIndexOf("[", StringComparison.OrdinalIgnoreCase);
						int length = Path.Length - num - 2;
						string value = Path.Substring(num + 1, length);
						Path = Path.Substring(0, num);
						T[] array = Resources.LoadAll<T>(Path);
						int i = 0;
						int num2 = array.Length;
						while (i < num2)
						{
							if (array[i].name.Equals(value))
							{
								t = array[i];
								break;
							}
							i++;
						}
					}
					else
					{
						t = (Resources.Load(Path, typeof(T)) as T);
					}
					if (t == null)
					{
						t = this.LoadFromBundle<T>(Path);
					}
					if (t != null)
					{
						this.mResourcesCache[Path] = t;
					}
					result = t;
				}
			}
			catch (Exception ex)
			{
				Debug.LogErrorFormat("Unable to load {0} '{1}'\nERROR: {2}", new object[]
				{
					typeof(T),
					Path,
					ex.ToString()
				});
				result = (T)((object)null);
			}
			return result;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0003D850 File Offset: 0x0003BC50
		public T LoadFromBundle<T>(string path) where T : UnityEngine.Object
		{
			int i = 0;
			int count = this.mBundleManagers.Count;
			while (i < count)
			{
				if (this.mBundleManagers[i] != null)
				{
					T t = this.mBundleManagers[i].LoadFromBundle(path, typeof(T)) as T;
					if (t != null)
					{
						return t;
					}
				}
				i++;
			}
			return (T)((object)null);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0003D8CC File Offset: 0x0003BCCC
		public void CleanResourceCache()
		{
			this.mResourcesCache.Clear();
			Resources.UnloadUnusedAssets();
			base.CancelInvoke();
		}

		// Token: 0x04000EBD RID: 3773
		private static ResourceManager mInstance;

		// Token: 0x04000EBE RID: 3774
		public List<IResourceManager_Bundles> mBundleManagers = new List<IResourceManager_Bundles>();

		// Token: 0x04000EBF RID: 3775
		public UnityEngine.Object[] Assets;

		// Token: 0x04000EC0 RID: 3776
		private readonly Dictionary<string, UnityEngine.Object> mResourcesCache = new Dictionary<string, UnityEngine.Object>(StringComparer.Ordinal);

		// Token: 0x04000EC1 RID: 3777
		[CompilerGenerated]
		private static UnityAction<Scene, LoadSceneMode> <>f__mg$cache0;
	}
}
