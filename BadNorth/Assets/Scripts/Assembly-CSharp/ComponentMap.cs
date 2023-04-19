using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020004FE RID: 1278
public class ComponentMap : MonoBehaviour
{
	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x060020A9 RID: 8361 RVA: 0x000587EC File Offset: 0x00056BEC
	private Dictionary<string, ComponentMap.Copy[]> sourceDict
	{
		get
		{
			if (this._sourceDict == null)
			{
				this._sourceDict = base.transform.GetChildren().ToDictionary((Transform x) => x.name, delegate(Transform x)
				{
					IEnumerable<MonoBehaviour> components = x.GetComponents<MonoBehaviour>();
					if (ComponentMap.action == null)
					{
						ComponentMap.action = new Func<MonoBehaviour, ComponentMap.Copy>(ComponentMap.Copy.Get<MonoBehaviour>);
					}
					return components.Select(ComponentMap.action).ToArray<ComponentMap.Copy>();
				});
			}
			return this._sourceDict;
		}
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x0005885C File Offset: 0x00056C5C
	public GameObject GetMappedCopy(Transform transform)
	{
		GameObject gameObject = (!transform) ? new GameObject("Mapped") : transform.AddEmptyChild("Mapped").gameObject;
		foreach (GameObject original in this.sources)
		{
			UnityEngine.Object.Instantiate<GameObject>(original, gameObject.transform);
		}
		Transform transform2 = gameObject.transform;
		if (ComponentMap.action1 == null)
		{
			ComponentMap.action1 = new Func<Transform, IEnumerable<Transform>>(TransformExtensions.GetChildren);
		}
		foreach (Transform transform3 in Enumeration.Traverse<Transform>(transform2, ComponentMap.action1))
		{
			foreach (string key in transform3.name.Split(new char[]
			{
				' '
			}))
			{
				ComponentMap.Copy[] array3;
				if (this.sourceDict.TryGetValue(key, out array3))
				{
					foreach (ComponentMap.Copy copy in array3)
					{
						copy.Apply(transform3.gameObject);
					}
				}
			}
		}
		return gameObject;
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x000589B0 File Offset: 0x00056DB0
	public static T CopyComponent<T>(T original, GameObject destination) where T : Component
	{
		Type type = original.GetType();
		Component component = destination.GetComponent(type);
		if (!component)
		{
			component = destination.AddComponent(type);
		}
		FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			fieldInfo.SetValue(component, fieldInfo.GetValue(original));
		}
		return component as T;
	}

	// Token: 0x04001444 RID: 5188
	[SerializeField]
	private GameObject[] sources;

	// Token: 0x04001445 RID: 5189
	private Dictionary<string, ComponentMap.Copy[]> _sourceDict;

	// Token: 0x04001446 RID: 5190
	[CompilerGenerated]
	private static Func<MonoBehaviour, ComponentMap.Copy> action;

	// Token: 0x04001449 RID: 5193
	[CompilerGenerated]
	private static Func<Transform, IEnumerable<Transform>> action1;

	// Token: 0x020004FF RID: 1279
	private class Copy
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x00058A70 File Offset: 0x00056E70
		public static ComponentMap.Copy Get<T>(T original) where T : Component
		{
			ComponentMap.Copy copy = new ComponentMap.Copy();
			copy.original = original;
			copy.type = original.GetType();
			copy.fields = copy.type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return copy;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00058AB8 File Offset: 0x00056EB8
		public void Apply(GameObject destination)
		{
			Component obj = destination.AddComponent(this.type);
			foreach (FieldInfo fieldInfo in this.fields)
			{
				fieldInfo.SetValue(obj, fieldInfo.GetValue(this.original));
			}
		}

		// Token: 0x0400144A RID: 5194
		private Component original;

		// Token: 0x0400144B RID: 5195
		private FieldInfo[] fields;

		// Token: 0x0400144C RID: 5196
		private Type type;
	}
}
