using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using I2.Loc;
using UnityEngine;

// Token: 0x02000509 RID: 1289
public static class GameObjectExtensions
{
	// Token: 0x06002118 RID: 8472 RVA: 0x0005ACCB File Offset: 0x000590CB
	public static GameObject AddEmptyChild(this GameObject parent, string name = null)
	{
		return parent.AddEmptyChild(Vector3.zero, Quaternion.identity, Vector3.one, name);
	}

	// Token: 0x06002119 RID: 8473 RVA: 0x0005ACE3 File Offset: 0x000590E3
	public static GameObject AddEmptyChild(this GameObject parent, Vector3 localPosition, Quaternion localRotation, string name = null)
	{
		return parent.AddEmptyChild(localPosition, localRotation, Vector3.one, name);
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x0005ACF4 File Offset: 0x000590F4
	public static GameObject AddEmptyChild(this GameObject parent, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, string name = null)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = parent.transform;
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localRotation = localRotation;
		gameObject.transform.localScale = localScale;
		if (!string.IsNullOrEmpty(name))
		{
			gameObject.name = name;
		}
		return gameObject;
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x0005AD51 File Offset: 0x00059151
	public static GameObject InstantiateChild(this GameObject parent, GameObject original, string name = null)
	{
		return parent.InstantiateChild(original, Vector3.zero, Quaternion.identity, name);
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x0005AD68 File Offset: 0x00059168
	public static T GetDisabledComponentInParent<T>(this MonoBehaviour mono) where T : Component
	{
		Transform transform = mono.transform;
		while (transform != null)
		{
			T component = transform.gameObject.GetComponent<T>();
			if (component)
			{
				return component;
			}
			transform = transform.parent;
		}
		return (T)((object)null);
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x0005ADB8 File Offset: 0x000591B8
	public static GameObject InstantiateChild(this GameObject parent, GameObject original, Vector3 position, Quaternion rotation, string name = null)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, position, rotation);
		if (parent.transform is RectTransform)
		{
			gameObject.transform.SetParent(parent.transform, false);
		}
		else
		{
			gameObject.transform.parent = parent.transform;
		}
		if (!string.IsNullOrEmpty(name))
		{
			gameObject.name = name;
		}
		return gameObject;
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x0005AE1C File Offset: 0x0005921C
	public static T InstantiateChild<T>(this GameObject parent, T original, string name = null) where T : Component
	{
		GameObject gameObject = parent.InstantiateChild(original.gameObject, Vector3.zero, Quaternion.identity, name);
		return (!gameObject) ? ((T)((object)null)) : gameObject.GetComponent<T>();
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x0005AE64 File Offset: 0x00059264
	public static T InstantiateChild<T>(this GameObject parent, T original, Vector3 position, Quaternion rotation, string name = null) where T : Component
	{
		GameObject gameObject = parent.InstantiateChild(original.gameObject, position, rotation, name);
		return (!gameObject) ? ((T)((object)null)) : gameObject.GetComponent<T>();
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x0005AEA5 File Offset: 0x000592A5
	public static T AddComponent<T>(this GameObject gameObject, T original) where T : Component
	{
		return original.CloneTo(gameObject);
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x0005AEB0 File Offset: 0x000592B0
	public static T GetComponentInParentIncludingInactive<T>(this GameObject gameObject) where T : Component
	{
		Transform transform = gameObject.transform;
		while (transform)
		{
			T component = transform.gameObject.GetComponent<T>();
			if (component)
			{
				return component;
			}
			transform = transform.parent;
		}
		return (T)((object)null);
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x0005AF00 File Offset: 0x00059300
	public static IEnumerable<T> GetActiveComponentsInChildren<T>(this GameObject gameObject)
	{
		int i = 0;
		int count = gameObject.transform.childCount;
		while (i < count)
		{
			GameObject child = gameObject.transform.GetChild(i).gameObject;
			if (child.activeSelf)
			{
				T c = child.GetComponent<T>();
				if (c != null)
				{
					yield return c;
				}
				foreach (T c2 in child.GetActiveComponentsInChildren<T>())
				{
					yield return c2;
				}
			}
			i++;
		}
		yield break;
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x0005AF24 File Offset: 0x00059324
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T component = gameObject.GetComponent<T>();
		return (!component) ? gameObject.AddComponent<T>() : component;
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x0005AF54 File Offset: 0x00059354
	public static IEnumerable<T> GetComponentsInDirectChildren<T>(this GameObject gameObject, bool allowInactive = false)
	{
		int i = 0;
		int count = gameObject.transform.childCount;
		while (i < count)
		{
			GameObject child = gameObject.transform.GetChild(i).gameObject;
			if (allowInactive || child.activeSelf)
			{
				T c = child.GetComponent<T>();
				if (c != null)
				{
					yield return c;
				}
			}
			i++;
		}
		yield break;
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x0005AF80 File Offset: 0x00059380
	public static bool IsPrefab(this GameObject gameObject)
	{
		return gameObject != null && !gameObject.scene.IsValid();
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x0005AFAD File Offset: 0x000593AD
	public static void StartCoroutineTimed(this MonoBehaviour monoBehavior, float milliseconds, params IEnumerator[] coroutine)
	{
		monoBehavior.StartCoroutine(CoroutineUtils.GenerateTimer(milliseconds, coroutine));
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x0005AFBD File Offset: 0x000593BD
	public static void StartCoroutineTimed(this MonoBehaviour monoBehaviour, float milliseconds, IEnumerator<bool> coroutine)
	{
		monoBehaviour.StartCoroutine(CoroutineUtils.GenerateTimer(milliseconds, coroutine));
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x0005AFD0 File Offset: 0x000593D0
	public static void StartCoroutineInstant(this MonoBehaviour monoBehavior, params IEnumerator[] coroutines)
	{
		foreach (IEnumerator enumerator in coroutines)
		{
			while (enumerator.MoveNext())
			{
			}
		}
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x0005B007 File Offset: 0x00059407
	public static void StartCoroutines(this MonoBehaviour monoBehavior, params IEnumerator[] coroutines)
	{
		monoBehavior.StartCoroutine(coroutines.GetEnumerator());
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x0005B016 File Offset: 0x00059416
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void SetEditorName(this UnityEngine.Object obj, string name)
	{
		obj.name = name;
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x0005B01F File Offset: 0x0005941F
	[Conditional("UNITY_EDITOR")]
	[Conditional("DEVELOPMENT_BUILD")]
	public static void SetEditorNameLocalized(this UnityEngine.Object obj, string nameTerm)
	{
		obj.name = ScriptLocalization.Get(nameTerm, true, 0, true, false, null, null);
	}
}
