using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Voxels.TowerDefense.Portraits
{
	// Token: 0x02000777 RID: 1911
	public class PortraitGenerator : MonoBehaviour
	{
		// Token: 0x06003192 RID: 12690 RVA: 0x000CE784 File Offset: 0x000CCB84
		private void Start()
		{
			foreach (Transform baseTransform in this.baseCharacterContainer.GetChildren())
			{
				this.partCollections.Add(new PortraitGenerator.PartCollection(baseTransform));
			}
			base.StartCoroutine(this.InfiniteGenerator());
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000CE7D4 File Offset: 0x000CCBD4
		private IEnumerator InfiniteGenerator()
		{
			Transform container = base.gameObject.AddEmptyChild("GeneratedCharacters").transform;
			Vector3 position = Vector3.zero;
			for (int i = 0; i < 100; i++)
			{
				IEnumerator<GenInfo> generator = this.GenerateRoutine(container, position);
				while (generator.MoveNext())
				{
					GenInfo genInfo = generator.Current;
					Debug.Log(genInfo.text);
					yield return null;
				}
				position.x += 3f;
			}
			yield break;
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000CE7F0 File Offset: 0x000CCBF0
		private IEnumerator<GenInfo> GenerateRoutine(Transform parent, Vector3 position)
		{
			Transform[] baseCharacters = this.baseCharacterContainer.GetChildren();
			Transform srcCharacter = baseCharacters[UnityEngine.Random.Range(0, baseCharacters.Length)];
			Transform character = UnityEngine.Object.Instantiate<GameObject>(srcCharacter.gameObject, parent).transform;
			character.name = srcCharacter.name;
			if (UnityEngine.Random.value > 0.5f)
			{
				character.transform.localScale = character.transform.localScale.SetX(character.transform.localScale.x);
			}
			character.transform.localPosition = position;
			IEnumerator<GenInfo> swap = this.Swap(character, character);
			while (swap.MoveNext())
			{
				GenInfo genInfo = swap.Current;
				yield return genInfo;
			}
			yield break;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000CE81C File Offset: 0x000CCC1C
		private IEnumerator<GenInfo> Swap(Transform character, Transform part)
		{
			for (int i = 0; i < part.childCount; i++)
			{
				Transform child = part.GetChild(i);
				string name = child.name;
				yield return new GenInfo("Finding " + name, GenInfo.Mode.interruptable);
				Transform[] siblings = (from x in character.GetComponentsInChildren<Transform>()
				where x.name == name
				select x).ToArray<Transform>();
				int startIndex = UnityEngine.Random.Range(0, this.partCollections.Count);
				for (int j = 0; j < this.partCollections.Count; j++)
				{
					PortraitGenerator.PartCollection baseCharacter = this.partCollections[(j + startIndex) % this.partCollections.Count];
					List<Transform> swappables;
					if (baseCharacter.dict.TryGetValue(name, out swappables) && siblings.Length == swappables.Count)
					{
						yield return new GenInfo("Swapping " + name, GenInfo.Mode.interruptable);
						for (int l = 0; l < siblings.Length; l++)
						{
							Transform transform = siblings[l];
							Transform transform2 = UnityEngine.Object.Instantiate<GameObject>(swappables[l].gameObject).transform;
							transform2.name = name;
							transform2.SetParent(transform.parent);
							transform2.localPosition = transform.localPosition;
							transform2.localScale = transform.localScale;
							transform2.localRotation = transform.localRotation;
							transform2.SetSiblingIndex(transform.GetSiblingIndex());
							Debug.Log("Destroying old " + transform.name);
							UnityEngine.Object.Destroy(transform.gameObject);
						}
						break;
					}
				}
			}
			for (int k = 0; k < part.childCount; k++)
			{
				Transform child2 = part.GetChild(k);
				IEnumerator<GenInfo> swap = this.Swap(character, child2);
				while (swap.MoveNext())
				{
					GenInfo genInfo = swap.Current;
					yield return genInfo;
				}
			}
			yield break;
		}

		// Token: 0x04002155 RID: 8533
		public Transform baseCharacterContainer;

		// Token: 0x04002156 RID: 8534
		private List<PortraitGenerator.PartCollection> partCollections = new List<PortraitGenerator.PartCollection>();

		// Token: 0x02000778 RID: 1912
		private class PartCollection
		{
			// Token: 0x06003196 RID: 12694 RVA: 0x000CE848 File Offset: 0x000CCC48
			public PartCollection(Transform baseTransform)
			{
				foreach (Transform transform in baseTransform.GetComponentsInChildren<Transform>())
				{
					string name = transform.name;
					List<Transform> list;
					if (!this.dict.TryGetValue(name, out list))
					{
						list = new List<Transform>();
						this.dict.Add(name, list);
					}
					list.Add(transform);
				}
			}

			// Token: 0x04002157 RID: 8535
			public Dictionary<string, List<Transform>> dict = new Dictionary<string, List<Transform>>();
		}
	}
}
