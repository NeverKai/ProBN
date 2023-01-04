using System;
using RTM.OnScreenDebug;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005E2 RID: 1506
public static class ExtraSceneManager
{
	// Token: 0x0600270B RID: 9995 RVA: 0x0007DB38 File Offset: 0x0007BF38
	public static Scene LoadSceneIfNotLoaded(string sceneName)
	{
		Scene result;
		if (!ExtraSceneManager.GetSceneByName(sceneName, out result))
		{
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			ExtraSceneManager.GetSceneByName(sceneName, out result);
		}
		return result;
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x0007DB65 File Offset: 0x0007BF65
	public static AsyncOperation LoadSceneIfNotLoadedAsync(string sceneName)
	{
		if (!ExtraSceneManager.SceneIsLoaded(sceneName))
		{
			return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		}
		return null;
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x0007DB7C File Offset: 0x0007BF7C
	public static bool SceneIsLoaded(string sceneName)
	{
		Scene scene;
		return ExtraSceneManager.GetSceneByName(sceneName, out scene);
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x0007DB94 File Offset: 0x0007BF94
	public static Scene GetSceneByName(string sceneName)
	{
		Scene result;
		ExtraSceneManager.GetSceneByName(sceneName, out result);
		return result;
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x0007DBAC File Offset: 0x0007BFAC
	public static bool GetSceneByName(string sceneName, out Scene scene)
	{
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			scene = SceneManager.GetSceneAt(i);
			if (scene.name == sceneName)
			{
				return true;
			}
		}
		scene = default(Scene);
		return false;
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x0007DBF8 File Offset: 0x0007BFF8
	public static T FindInSceneRootObjects<T>(string sceneName) where T : class
	{
		Scene scene;
		if (ExtraSceneManager.GetSceneByName(sceneName, out scene) && scene.isLoaded)
		{
			GameObject[] rootGameObjects = scene.GetRootGameObjects();
			foreach (GameObject gameObject in rootGameObjects)
			{
				T component = gameObject.GetComponent<T>();
				if (component != null)
				{
					return component;
				}
			}
		}
		return (T)((object)null);
	}

	// Token: 0x04001905 RID: 6405
	private static DebugChannelGroup dbgGroup = new DebugChannelGroup("ExtraSceneManagement", EVerbosity.Quiet, 0);
}
