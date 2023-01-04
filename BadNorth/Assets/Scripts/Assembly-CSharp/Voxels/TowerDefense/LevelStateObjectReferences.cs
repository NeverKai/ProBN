using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voxels.TowerDefense
{
	// Token: 0x0200070F RID: 1807
	public static class LevelStateObjectReferences
	{
		// Token: 0x06002EDB RID: 11995 RVA: 0x000B6F90 File Offset: 0x000B5390
		public static void AddToDict(UnityEngine.Object value)
		{
			string name = value.name;
			if (LevelStateObjectReferences.dict.ContainsKey(name))
			{
				LevelStateObjectReferences.dict[name] = value;
			}
			else
			{
				LevelStateObjectReferences.dict.Add(name, value);
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000B6FD1 File Offset: 0x000B53D1
		public static void AddReferencedObject(this LevelState levelState, LevelObjectReference.Key key, UnityEngine.Object value)
		{
			levelState.objectReferences.Add(new LevelObjectReference(key, value.name));
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000B6FEC File Offset: 0x000B53EC
		public static IEnumerable<T> GetReferencedObjects<T>() where T : UnityEngine.Object
		{
			foreach (UnityEngine.Object value in LevelStateObjectReferences.dict.Values)
			{
				if (value is T)
				{
					yield return value as T;
				}
			}
			yield break;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000B7008 File Offset: 0x000B5408
		public static IEnumerable<UnityEngine.Object> GetReferencedObjects(this LevelState levelState)
		{
			foreach (LevelObjectReference s in levelState.objectReferences)
			{
				UnityEngine.Object value;
				LevelStateObjectReferences.dict.TryGetValue(s.name, out value);
				if (value)
				{
					yield return value;
				}
			}
			yield break;
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000B702C File Offset: 0x000B542C
		public static IEnumerable<string> GetReferencedStrings(this LevelState levelState, LevelObjectReference.Key key)
		{
			foreach (LevelObjectReference s in levelState.objectReferences)
			{
				if (s.key == key)
				{
					yield return s.name;
				}
			}
			yield break;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000B7058 File Offset: 0x000B5458
		public static string GetReferencedString(this LevelState levelState, LevelObjectReference.Key key)
		{
			foreach (LevelObjectReference levelObjectReference in levelState.objectReferences)
			{
				if (levelObjectReference.key == key)
				{
					return levelObjectReference.name;
				}
			}
			return string.Empty;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000B70D0 File Offset: 0x000B54D0
		public static void GetReferencedObjects<T>(this LevelState levelState, LevelObjectReference.Key key, List<T> list) where T : UnityEngine.Object
		{
			list.Clear();
			foreach (LevelObjectReference levelObjectReference in levelState.objectReferences)
			{
				if (levelObjectReference.key == key)
				{
					UnityEngine.Object @object;
					LevelStateObjectReferences.dict.TryGetValue(levelObjectReference.name, out @object);
					T t = @object as T;
					if (t)
					{
						list.Add(t);
					}
					else
					{
						GameObject gameObject = @object as GameObject;
						if (gameObject)
						{
							t = gameObject.GetComponent<T>();
							if (t)
							{
								list.Add(t);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000B71AC File Offset: 0x000B55AC
		public static void GetReferencedObjects<T>(this LevelState levelState, List<T> list) where T : UnityEngine.Object
		{
			foreach (LevelObjectReference levelObjectReference in levelState.objectReferences)
			{
				UnityEngine.Object @object;
				LevelStateObjectReferences.dict.TryGetValue(levelObjectReference.name, out @object);
				T t = @object as T;
				if (t)
				{
					list.Add(t);
				}
				else
				{
					GameObject gameObject = @object as GameObject;
					if (gameObject)
					{
						t = gameObject.GetComponent<T>();
						if (t)
						{
							list.Add(t);
						}
					}
				}
			}
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000B7270 File Offset: 0x000B5670
		public static T GetReferencedObject<T>(this LevelState levelState) where T : UnityEngine.Object
		{
			foreach (LevelObjectReference levelObjectReference in levelState.objectReferences)
			{
				UnityEngine.Object @object;
				LevelStateObjectReferences.dict.TryGetValue(levelObjectReference.name, out @object);
				T t = @object as T;
				if (t)
				{
					return t;
				}
				GameObject gameObject = @object as GameObject;
				if (gameObject)
				{
					t = gameObject.GetComponent<T>();
					if (t)
					{
						return t;
					}
				}
			}
			return (T)((object)null);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000B733C File Offset: 0x000B573C
		public static IEnumerable<T> ReferencedComponentsIncludingChildren<T>(this LevelState levelState) where T : Component
		{
			foreach (LevelObjectReference s in levelState.objectReferences)
			{
				UnityEngine.Object value;
				LevelStateObjectReferences.dict.TryGetValue(s.name, out value);
				GameObject gameObject = value as GameObject;
				if (!gameObject)
				{
					Component component = value as Component;
					if (component)
					{
						gameObject = component.gameObject;
					}
				}
				if (gameObject)
				{
					foreach (T c in gameObject.GetComponentsInChildren<T>())
					{
						yield return c;
					}
				}
			}
			yield break;
		}

		// Token: 0x04001EE9 RID: 7913
		public static Dictionary<string, UnityEngine.Object> dict = new Dictionary<string, UnityEngine.Object>();
	}
}
