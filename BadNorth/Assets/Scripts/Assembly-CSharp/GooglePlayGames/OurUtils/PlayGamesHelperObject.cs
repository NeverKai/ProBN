using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	// Token: 0x020003BE RID: 958
	public class PlayGamesHelperObject : MonoBehaviour
	{
		// Token: 0x06001577 RID: 5495 RVA: 0x0002C31C File Offset: 0x0002A71C
		public static void CreateObject()
		{
			if (PlayGamesHelperObject.instance != null)
			{
				return;
			}
			if (Application.isPlaying)
			{
				GameObject gameObject = new GameObject("PlayGames_QueueRunner");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				PlayGamesHelperObject.instance = gameObject.AddComponent<PlayGamesHelperObject>();
			}
			else
			{
				PlayGamesHelperObject.instance = new PlayGamesHelperObject();
				PlayGamesHelperObject.sIsDummy = true;
			}
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0002C375 File Offset: 0x0002A775
		public void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0002C382 File Offset: 0x0002A782
		public void OnDisable()
		{
			if (PlayGamesHelperObject.instance == this)
			{
				PlayGamesHelperObject.instance = null;
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0002C39C File Offset: 0x0002A79C
		public static void RunCoroutine(IEnumerator action)
		{
			if (PlayGamesHelperObject.instance != null)
			{
				PlayGamesHelperObject.RunOnGameThread(delegate
				{
					PlayGamesHelperObject.instance.StartCoroutine(action);
				});
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0002C3D8 File Offset: 0x0002A7D8
		public static void RunOnGameThread(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (PlayGamesHelperObject.sIsDummy)
			{
				return;
			}
			object obj = PlayGamesHelperObject.sQueue;
			lock (obj)
			{
				PlayGamesHelperObject.sQueue.Add(action);
				PlayGamesHelperObject.sQueueEmpty = false;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0002C43C File Offset: 0x0002A83C
		public void Update()
		{
			if (PlayGamesHelperObject.sIsDummy || PlayGamesHelperObject.sQueueEmpty)
			{
				return;
			}
			this.localQueue.Clear();
			object obj = PlayGamesHelperObject.sQueue;
			lock (obj)
			{
				this.localQueue.AddRange(PlayGamesHelperObject.sQueue);
				PlayGamesHelperObject.sQueue.Clear();
				PlayGamesHelperObject.sQueueEmpty = true;
			}
			for (int i = 0; i < this.localQueue.Count; i++)
			{
				this.localQueue[i]();
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0002C4E4 File Offset: 0x0002A8E4
		public void OnApplicationFocus(bool focused)
		{
			foreach (Action<bool> action in PlayGamesHelperObject.sFocusCallbackList)
			{
				try
				{
					action(focused);
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnApplicationFocus:" + ex.Message + "\n" + ex.StackTrace);
				}
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0002C578 File Offset: 0x0002A978
		public void OnApplicationPause(bool paused)
		{
			foreach (Action<bool> action in PlayGamesHelperObject.sPauseCallbackList)
			{
				try
				{
					action(paused);
				}
				catch (Exception ex)
				{
					Debug.LogError("Exception in OnApplicationPause:" + ex.Message + "\n" + ex.StackTrace);
				}
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0002C60C File Offset: 0x0002AA0C
		public static void AddFocusCallback(Action<bool> callback)
		{
			if (!PlayGamesHelperObject.sFocusCallbackList.Contains(callback))
			{
				PlayGamesHelperObject.sFocusCallbackList.Add(callback);
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0002C629 File Offset: 0x0002AA29
		public static bool RemoveFocusCallback(Action<bool> callback)
		{
			return PlayGamesHelperObject.sFocusCallbackList.Remove(callback);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0002C636 File Offset: 0x0002AA36
		public static void AddPauseCallback(Action<bool> callback)
		{
			if (!PlayGamesHelperObject.sPauseCallbackList.Contains(callback))
			{
				PlayGamesHelperObject.sPauseCallbackList.Add(callback);
			}
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0002C653 File Offset: 0x0002AA53
		public static bool RemovePauseCallback(Action<bool> callback)
		{
			return PlayGamesHelperObject.sPauseCallbackList.Remove(callback);
		}

		// Token: 0x04000D93 RID: 3475
		private static PlayGamesHelperObject instance = null;

		// Token: 0x04000D94 RID: 3476
		private static bool sIsDummy = false;

		// Token: 0x04000D95 RID: 3477
		private static List<Action> sQueue = new List<Action>();

		// Token: 0x04000D96 RID: 3478
		private List<Action> localQueue = new List<Action>();

		// Token: 0x04000D97 RID: 3479
		private static volatile bool sQueueEmpty = true;

		// Token: 0x04000D98 RID: 3480
		private static List<Action<bool>> sPauseCallbackList = new List<Action<bool>>();

		// Token: 0x04000D99 RID: 3481
		private static List<Action<bool>> sFocusCallbackList = new List<Action<bool>>();
	}
}
