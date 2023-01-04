using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200050E RID: 1294
public static class TransformExtensions
{
	// Token: 0x0600213B RID: 8507 RVA: 0x0005B840 File Offset: 0x00059C40
	public static Transform FindChildRecursive(this Transform aParent, string aName)
	{
		Transform transform = aParent.Find(aName);
		if (transform != null)
		{
			return transform;
		}
		IEnumerator enumerator = aParent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform aParent2 = (Transform)obj;
				transform = aParent2.FindChildRecursive(aName);
				if (transform != null)
				{
					return transform;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	// Token: 0x0600213C RID: 8508 RVA: 0x0005B8D0 File Offset: 0x00059CD0
	public static Transform[] GetChildren(this Transform inTransform)
	{
		int childCount = inTransform.childCount;
		Transform[] array = new Transform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = inTransform.GetChild(i);
		}
		return array;
	}

	// Token: 0x0600213D RID: 8509 RVA: 0x0005B908 File Offset: 0x00059D08
	public static void DestroyChildren(this Transform inTransform)
	{
		Transform[] children = inTransform.GetChildren();
		foreach (Transform transform in children)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(transform.gameObject);
			}
		}
	}

	// Token: 0x0600213E RID: 8510 RVA: 0x0005B95B File Offset: 0x00059D5B
	public static Transform Set(this Transform inTransform, Transform refTransform)
	{
		inTransform.position = refTransform.position;
		inTransform.rotation = refTransform.rotation;
		inTransform.localScale = refTransform.localScale;
		return inTransform;
	}

	// Token: 0x0600213F RID: 8511 RVA: 0x0005B984 File Offset: 0x00059D84
	public static Transform SetX(this Transform inTransform, float x)
	{
		Vector3 position = inTransform.position;
		position.x = x;
		inTransform.position = position;
		return inTransform;
	}

	// Token: 0x06002140 RID: 8512 RVA: 0x0005B9A8 File Offset: 0x00059DA8
	public static Transform SetY(this Transform inTransform, float y)
	{
		Vector3 position = inTransform.position;
		position.y = y;
		inTransform.position = position;
		return inTransform;
	}

	// Token: 0x06002141 RID: 8513 RVA: 0x0005B9CC File Offset: 0x00059DCC
	public static Transform SetZ(this Transform inTransform, float z)
	{
		Vector3 position = inTransform.position;
		position.z = z;
		inTransform.position = position;
		return inTransform;
	}

	// Token: 0x06002142 RID: 8514 RVA: 0x0005B9F0 File Offset: 0x00059DF0
	public static Transform SetXY(this Transform inTransform, float x, float y)
	{
		Vector3 position = inTransform.position;
		position.x = x;
		position.y = y;
		inTransform.position = position;
		return inTransform;
	}

	// Token: 0x06002143 RID: 8515 RVA: 0x0005BA1C File Offset: 0x00059E1C
	public static Transform SetXY(this Transform inTransform, Vector2 xy)
	{
		Vector3 position = inTransform.position;
		position.x = xy.x;
		position.y = xy.y;
		inTransform.position = position;
		return inTransform;
	}

	// Token: 0x06002144 RID: 8516 RVA: 0x0005BA54 File Offset: 0x00059E54
	public static Transform SetLocalX(this Transform inTransform, float x)
	{
		Vector3 localPosition = inTransform.localPosition;
		localPosition.x = x;
		inTransform.localPosition = localPosition;
		return inTransform;
	}

	// Token: 0x06002145 RID: 8517 RVA: 0x0005BA78 File Offset: 0x00059E78
	public static Transform SetLocalY(this Transform inTransform, float y)
	{
		Vector3 localPosition = inTransform.localPosition;
		localPosition.y = y;
		inTransform.localPosition = localPosition;
		return inTransform;
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x0005BA9C File Offset: 0x00059E9C
	public static Transform SetLocalZ(this Transform inTransform, float z)
	{
		Vector3 localPosition = inTransform.localPosition;
		localPosition.z = z;
		inTransform.localPosition = localPosition;
		return inTransform;
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x0005BAC0 File Offset: 0x00059EC0
	public static Transform SetLocalXY(this Transform inTransform, float x, float y)
	{
		Vector3 localPosition = inTransform.localPosition;
		localPosition.x = x;
		localPosition.y = y;
		inTransform.localPosition = localPosition;
		return inTransform;
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x0005BAEC File Offset: 0x00059EEC
	public static Transform SetLocalXY(this Transform inTransform, Vector2 xy)
	{
		Vector3 localPosition = inTransform.localPosition;
		localPosition.x = xy.x;
		localPosition.y = xy.y;
		inTransform.localPosition = localPosition;
		return inTransform;
	}

	// Token: 0x06002149 RID: 8521 RVA: 0x0005BB24 File Offset: 0x00059F24
	public static Transform SetRotationX(this Transform inTransform, float rotX)
	{
		Vector3 eulerAngles = inTransform.rotation.eulerAngles;
		eulerAngles.x = rotX;
		inTransform.rotation = Quaternion.Euler(eulerAngles);
		return inTransform;
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x0005BB58 File Offset: 0x00059F58
	public static Transform SetRotationY(this Transform inTransform, float rotY)
	{
		Vector3 eulerAngles = inTransform.rotation.eulerAngles;
		eulerAngles.y = rotY;
		inTransform.rotation = Quaternion.Euler(eulerAngles);
		return inTransform;
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x0005BB8C File Offset: 0x00059F8C
	public static Transform SetRotationZ(this Transform inTransform, float rotZ)
	{
		Vector3 eulerAngles = inTransform.rotation.eulerAngles;
		eulerAngles.z = rotZ;
		inTransform.rotation = Quaternion.Euler(eulerAngles);
		return inTransform;
	}

	// Token: 0x0600214C RID: 8524 RVA: 0x0005BBC0 File Offset: 0x00059FC0
	public static Transform SetLocalRotationX(this Transform inTransform, float rotX)
	{
		Vector3 eulerAngles = inTransform.localRotation.eulerAngles;
		eulerAngles.x = rotX;
		inTransform.localRotation = Quaternion.Euler(eulerAngles);
		return inTransform;
	}

	// Token: 0x0600214D RID: 8525 RVA: 0x0005BBF4 File Offset: 0x00059FF4
	public static Transform SetLocalRotationY(this Transform inTransform, float rotY)
	{
		Vector3 eulerAngles = inTransform.localRotation.eulerAngles;
		eulerAngles.y = rotY;
		inTransform.localRotation = Quaternion.Euler(eulerAngles);
		return inTransform;
	}

	// Token: 0x0600214E RID: 8526 RVA: 0x0005BC28 File Offset: 0x0005A028
	public static Transform SetLocalRotationZ(this Transform inTransform, float rotZ)
	{
		Vector3 eulerAngles = inTransform.localRotation.eulerAngles;
		eulerAngles.z = rotZ;
		inTransform.localRotation = Quaternion.Euler(eulerAngles);
		return inTransform;
	}

	// Token: 0x0600214F RID: 8527 RVA: 0x0005BC59 File Offset: 0x0005A059
	public static Transform SetLocalScale(this Transform inTransform, float allAxes)
	{
		inTransform.localScale = new Vector3(allAxes, allAxes, allAxes);
		return inTransform;
	}

	// Token: 0x06002150 RID: 8528 RVA: 0x0005BC6C File Offset: 0x0005A06C
	public static Transform CorrectNegativeScale(this Transform inTransform)
	{
		Vector3 lossyScale = inTransform.lossyScale;
		Vector3 localScale = inTransform.localScale;
		localScale.x *= Mathf.Sign(lossyScale.x);
		localScale.y *= Mathf.Sign(lossyScale.y);
		localScale.z *= Mathf.Sign(lossyScale.z);
		inTransform.localScale = localScale;
		return inTransform;
	}

	// Token: 0x06002151 RID: 8529 RVA: 0x0005BCDD File Offset: 0x0005A0DD
	public static void AttachTo(this Transform child, Transform newParent)
	{
		child.parent = newParent;
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x0005BCE6 File Offset: 0x0005A0E6
	public static void AttachTo(this Transform child, GameObject newParent)
	{
		child.parent = newParent.transform;
	}

	// Token: 0x06002153 RID: 8531 RVA: 0x0005BCF4 File Offset: 0x0005A0F4
	public static void AttachTo(this GameObject child, Transform newParent)
	{
		child.transform.parent = newParent;
	}

	// Token: 0x06002154 RID: 8532 RVA: 0x0005BD02 File Offset: 0x0005A102
	public static void AttachTo(this GameObject child, GameObject newParent)
	{
		child.transform.parent = newParent.transform;
	}

	// Token: 0x06002155 RID: 8533 RVA: 0x0005BD15 File Offset: 0x0005A115
	public static Transform AddEmptyChild(this Transform parent, string name = null)
	{
		return parent.AddEmptyChild(Vector3.zero, Quaternion.identity, name);
	}

	// Token: 0x06002156 RID: 8534 RVA: 0x0005BD28 File Offset: 0x0005A128
	public static Transform AddEmptyChild(this Transform parent, Vector3 localPosition, Quaternion localRotation, string name = null)
	{
		return parent.gameObject.AddEmptyChild(localPosition, localRotation, name).transform;
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x0005BD3D File Offset: 0x0005A13D
	public static T AddComponent<T>(this GameObject gameObject, T original) where T : Component
	{
		return original.CloneTo(gameObject);
	}

	// Token: 0x06002158 RID: 8536 RVA: 0x0005BD48 File Offset: 0x0005A148
	public static IEnumerable<Transform> GetChildEnumerable(this Transform transform, bool includeInactive = false)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (includeInactive || child.gameObject.activeSelf)
			{
				yield return child;
			}
		}
		yield break;
	}

	// Token: 0x06002159 RID: 8537 RVA: 0x0005BD72 File Offset: 0x0005A172
	public static bool IsDescendentOf(this Transform transform, Transform ancestor)
	{
		while (transform)
		{
			if (transform == ancestor)
			{
				return true;
			}
			transform = transform.parent;
		}
		return false;
	}

	// Token: 0x0600215A RID: 8538 RVA: 0x0005BD9C File Offset: 0x0005A19C
	public static void ForceChildLayoutUpdates(this Transform transform, bool includeInactive = false)
	{
		transform.GetComponentsInChildren<LayoutGroup>(includeInactive, TransformExtensions.layoutCache);
		foreach (LayoutGroup layoutGroup in TransformExtensions.layoutCache)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.transform as RectTransform);
		}
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x0005BE0C File Offset: 0x0005A20C
	public static void MarkChildLayoutsForRebuild(this Transform transform, bool includeInactive = false)
	{
		transform.GetComponentsInChildren<LayoutGroup>(includeInactive, TransformExtensions.layoutCache);
		foreach (LayoutGroup layoutGroup in TransformExtensions.layoutCache)
		{
			LayoutRebuilder.MarkLayoutForRebuild(layoutGroup.transform as RectTransform);
		}
	}

	// Token: 0x04001480 RID: 5248
	private static List<LayoutGroup> layoutCache = new List<LayoutGroup>(16);
}
